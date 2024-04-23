using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground
{
    /// <inheritdoc cref="IBloodWorkstationAppService"/>
    [AbpAuthorize]
    public class BloodWorkstationAppService : MyProjectAppServiceBase, IBloodWorkstationAppService
    {
        /// <summary>
        /// 体检人预约条码打印信息仓储
        /// </summary>
        private readonly IRepository<TjlCustomerBarPrintInfo, Guid> _customerRegisterBarCodePrintInformationRepository;

        /// <summary>
        /// 体检人预约仓储
        /// </summary>
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegisterRepository;

        /// <summary>
        /// 图像仓储
        /// </summary>
        private readonly IRepository<Core.AppSystem.Picture, Guid> _pictureRepository;

        /// <summary>
        /// 字典
        /// </summary>
        private readonly IRepository<TbmBasicDictionary, Guid> _BasicDictionary;

        /// <inheritdoc cref="IBloodWorkstationAppService" />
        public BloodWorkstationAppService(IRepository<TjlCustomerBarPrintInfo, Guid> customerRegisterBarCodePrintInformationRepository, IRepository<TjlCustomerReg, Guid> customerRegisterRepository, IRepository<Core.AppSystem.Picture, Guid> pictureRepository,
            IRepository<TbmBasicDictionary, Guid> BasicDictionary)
        {
            _customerRegisterBarCodePrintInformationRepository = customerRegisterBarCodePrintInformationRepository;
            _customerRegisterRepository = customerRegisterRepository;
            _pictureRepository = pictureRepository;
            _BasicDictionary = BasicDictionary;
        }

        /// <inheritdoc />
        public async Task<List<CustomerRegisterBarCodePrintInformationDto>> QueryBarCodePrintRecord(CustomerRegisterBarCodePrintInformationConditionInput input)
        {
            var query = _customerRegisterBarCodePrintInformationRepository.GetAll();
            var bis = _BasicDictionary.GetAll().Where(p=>p.Type== "SpecimenType" && 
            p.Text.Contains("血") ).Select(p=>p.Value.ToString()).ToList();
            query = query.Where(r => r.BarSettings.Sampletype.Contains("血") || 
            r.BarSettings.Sampletype == "1"  || bis.Contains(r.BarSettings.Sampletype));
            query = query.Where(r => r.CustomerReg != null);
            if (!string.IsNullOrWhiteSpace(input.BarCodeNumber))
            {
                // 如果条码生成规则和体检号生成规则一致，就会出现问题
                query = query.Where(r => r.BarNumBM == input.BarCodeNumber || r.CustomerReg.CustomerBM == input.BarCodeNumber);

                if (input.AutoBlood)
                {
                    await query.Where(r => r.HaveBlood == false).UpdateAsync(r => new TjlCustomerBarPrintInfo
                    {
                        HaveBlood = true,
                        BloodTime = DateTime.Now,
                        BloodUserId = AbpSession.UserId
                    });
                }
                //送检
                if (input.AutoSend)
                {
                    await query.Where(r => r.HaveSend == false).UpdateAsync(r => new TjlCustomerBarPrintInfo
                    {
                        HaveSend = true,
                        SendTime = DateTime.Now,
                        SendUserId = AbpSession.UserId
                    });
                }
                //接收
                if (input.AutoReceive)
                {
                    await query.Where(r => r.HaveReceive == false).UpdateAsync(r => new TjlCustomerBarPrintInfo
                    {
                        HaveReceive = true,
                        ReceiveTime = DateTime.Now,
                        ReceiveUserId = AbpSession.UserId
                    });
                }
                return await query.ProjectToListAsync<CustomerRegisterBarCodePrintInformationDto>(
                    GetConfigurationProvider<CustomerRegisterBarCodePrintInformationDto>());
            }

            if (input.HaveBlood.HasValue)
            {
                if (input.HaveBlood.Value)
                {
                    query = query.Where(r => r.HaveBlood == true);
                    if (input.BloodTime.HasValue)
                    {
                        query = query.Where(r => DbFunctions.DiffDays(r.BloodTime, input.BloodTime) == 0);
                    }

                    if (input.BloodUserId.HasValue)
                    {
                        query = query.Where(r => r.BloodUserId == input.BloodUserId.Value);
                    }
                }
                else
                {
                    query = query.Where(r => r.HaveBlood == false);
                    if (input.BloodTime.HasValue)
                    {
                        query = query.Where(r => DbFunctions.DiffDays(r.BarPrintTime, input.BloodTime) == 0);
                    }
                }
            }
            else
            {
                if (input.BloodUserId.HasValue)
                {
                    if (input.BloodTime.HasValue)
                    {
                        query = query.Where(r =>
                                        DbFunctions.DiffDays(r.BloodTime, input.BloodTime) == 0 && r.BloodUserId == input.BloodUserId ||
                                        DbFunctions.DiffDays(r.BarPrintTime, input.BloodTime) == 0);
                    }
                    else
                    {
                        query = query.Where(r => r.BloodUserId == input.BloodUserId);
                    }
                }
                else
                {
                    if (input.BloodTime.HasValue)
                    {
                        query = query.Where(r =>
                                        DbFunctions.DiffDays(r.BloodTime, input.BloodTime) == 0 ||
                                        DbFunctions.DiffDays(r.BarPrintTime, input.BloodTime) == 0);
                    }
                }
            }

            query = query.OrderByDescending(r => r.BarPrintTime);

            return await query.ProjectToListAsync<CustomerRegisterBarCodePrintInformationDto>(
                GetConfigurationProvider<CustomerRegisterBarCodePrintInformationDto>());
        }
       /// <summary>
       /// 抽血统计
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        public async Task<List<CustomerCount>> QueryBlood(CustomerRegisterBarCodePrintInformationConditionInput input)
        {
            var query = _customerRegisterBarCodePrintInformationRepository.GetAll();
            var bis = _BasicDictionary.GetAll().Where(p => p.Type == "SpecimenType" &&           
            p.Text.Contains("血")).Select(p => p.Value.ToString()).ToList();
            query = query.Where(r => r.BarSettings.Sampletype.Contains("血") ||
               r.BarSettings.Sampletype == "1" ||
            bis.Contains(r.BarSettings.Sampletype));
            query = query.Where(r => r.CustomerReg != null && r.HaveBlood == true);
             
            if (input.StarBloodTime.HasValue)
            {
                query = query.Where(r =>r.BloodTime>= input.StarBloodTime);
            }
            if (input.EndBloodTime.HasValue)
            {
                query = query.Where(r => r.BloodTime <= input.EndBloodTime);
            }
            if (input.BloodUserId.HasValue)
            {
                query = query.Where(r => r.BloodUserId == input.BloodUserId.Value);
            }
            var barcountcus = query.Where(p=>p.BloodUserId!=null).GroupBy(p => new { p.CustomerReg_Id,
                p.BloodUserId
            }).Select(p => new
                {
                    BloodUserName =p.Key.BloodUserId,
                CustomerBM =p.Key.CustomerReg_Id,
                barCount = p.Count()
            }).ToList();

            var bloodcountcus = barcountcus.GroupBy(p => p.BloodUserName).Select(
                p => new CustomerCount
                {
                    UserName = p.First().BloodUserName,
                    BloodBarCount = p.Sum(o => o.barCount),
                    RSCount = p.Count()
                }).ToList();
            return  bloodcountcus;
        }

        /// <inheritdoc />
        public async Task<CustomerRegister52Dto> QueryCustomerRegisterById(EntityDto<Guid> input)
        {
            var customerRegister = await _customerRegisterRepository.GetAsync(input.Id);
            var result = new CustomerRegister52Dto();
            result.Id = customerRegister.Id;
            result.Name = customerRegister.Customer.Name;
            result.Sex = customerRegister.Customer.Sex;
            if (customerRegister.LoginDate.HasValue)
            {
                result.Age = GetAgeByBirthDate(customerRegister.Customer.Birthday.GetValueOrDefault(DateTime.Now),
                    customerRegister.LoginDate.Value);
            }
            else if (customerRegister.BookingDate.HasValue)
            {
                result.Age = GetAgeByBirthDate(customerRegister.Customer.Birthday.GetValueOrDefault(DateTime.Now),
                    customerRegister.BookingDate.Value);
            }
            else
            {
                result.Age = GetAgeByBirthDate(customerRegister.Customer.Birthday.GetValueOrDefault(DateTime.Now),
                    customerRegister.CreationTime);
            }

            if (customerRegister.ClientInfoId.HasValue)
            {
                result.ClientName = customerRegister.ClientInfo.ClientName;
            }

            result.CusPhotoBmId = customerRegister.Customer.CusPhotoBmId;

            return result;
        }

        /// <inheritdoc />
        public async Task<CustomerRegisterBarCodePrintInformationDto> UpdateBarCodeHaveBlood(UpdateBarCodeHaveBloodInput input)
        {
            //送检
            if (input.type.HasValue && input.type == 2)
            {
                var result = await _customerRegisterBarCodePrintInformationRepository.UpdateAsync(input.Id, async r =>
                {
                    await Task.CompletedTask;
                    if (r.HaveSend != input.HaveBlood)
                    {
                        r.HaveSend = input.HaveBlood;
                        if (input.HaveBlood)
                        {
                            r.SendUserId = AbpSession.UserId;
                            r.SendTime = DateTime.Now;
                        }
                        else
                        {
                            r.SendUserId = null;
                            r.SendTime = null;
                        }
                    }
                });
                return ObjectMapper.Map<CustomerRegisterBarCodePrintInformationDto>(result);
            }
            //接收
            else if (input.type.HasValue && input.type == 3)
            {
                var result = await _customerRegisterBarCodePrintInformationRepository.UpdateAsync(input.Id, async r =>
                {
                    await Task.CompletedTask;
                    if (r.HaveReceive != input.HaveBlood)
                    {
                        r.HaveReceive = input.HaveBlood;
                        if (input.HaveBlood)
                        {
                            r.ReceiveUserId = AbpSession.UserId;
                            r.ReceiveTime = DateTime.Now;
                        }
                        else
                        {
                            r.ReceiveUserId = null;
                            r.ReceiveTime = null;
                        }
                    }
                });
                return ObjectMapper.Map<CustomerRegisterBarCodePrintInformationDto>(result);
            }
            else
            {
                var result = await _customerRegisterBarCodePrintInformationRepository.UpdateAsync(input.Id, async r =>
                {
                    await Task.CompletedTask;
                    if (r.HaveBlood != input.HaveBlood)
                    {
                        r.HaveBlood = input.HaveBlood;
                        if (input.HaveBlood)
                        {
                            r.BloodUserId = AbpSession.UserId;
                            r.BloodTime = DateTime.Now;
                        }
                        else
                        {
                            r.BloodUserId = null;
                            r.BloodTime = null;
                        }
                    }
                });
                return ObjectMapper.Map<CustomerRegisterBarCodePrintInformationDto>(result);
            }
        }

        /// <summary>
        /// 根据日期计算年龄
        /// </summary>
        /// <returns></returns>
        private int GetAgeByBirthDate(DateTime birthDate, DateTime date)
        {
            int age = date.Year - birthDate.Year;
            if (date.Month < birthDate.Month || (date.Month == birthDate.Month && date.Day < birthDate.Day))
            {
                age--;
            }

            return age < 0 ? 0 : age;
        }
    }
}