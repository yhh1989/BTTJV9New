using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination
{
    [AbpAuthorize]
    public class PhysicalExaminationAppService : MyProjectAppServiceBase, IPhysicalExaminationAppService
    {
        private readonly IRepository<TjlClientReg, Guid> _tlClientReg; //单位预约信息
        private readonly IRepository<TjlCustomerReg, Guid> _tjlCustomerReg; //体检人预约登记信息表
        /// <summary>
        /// 用户表仓储
        /// </summary>
        private readonly IRepository<User, long> _userRepository;
        public PhysicalExaminationAppService(
            IRepository<TjlClientReg, Guid> tjlClientReg,
            IRepository<TjlCustomerReg, Guid> tjlCustomerReg,
            IRepository<User, long> userRepository
        )
        {
            _tlClientReg = tjlClientReg;
            _tjlCustomerReg = tjlCustomerReg;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 单位查询
        /// </summary>
        public List<ClientRegPhysicalDto> QueryCompany()
        {
            var PaDtoList = _tlClientReg.GetAllIncluding(r => r.ClientInfo).OrderByDescending(o=>o.StartCheckDate);
            var rows = PaDtoList.MapTo<List<ClientRegPhysicalDto>>();
            return rows;
        }



        /// <summary>
        /// 个人信息查询
        /// </summary>
        public PageResultDto<CustomerRegPhysicalDto> PersonalInformationQuery(PageInputDto<CustomerRegPhysicalDto> input)
        {

            var query = _tjlCustomerReg.GetAll();
            if (input != null)
            {
                ////体检号
                //if (input.Input.CustomerBM != null)
                //    query = query.Where(o => o.CustomerBM.Contains(input.Input.CustomerBM));

                //if (input.Input.Customer != null)
                //{
                //    //姓名
                //    if (input.Input.Customer.Name != null)
                //    {
                //        query = query.Where(o => o.Customer.Name.Contains(input.Input.Customer.Name));
                //    }
                //    //证件号
                //    if (input.Input.Customer.IDCardNo != null)
                //        query = query.Where(o => o.Customer.IDCardNo.Contains(input.Input.Customer.IDCardNo));

                //}
                if (input.Input.Customer != null)
                {
                    if (!string.IsNullOrWhiteSpace(input.Input.Customer.SerchInput))
                    {
                        query = query.Where(o => o.CustomerBM.Contains(input.Input.Customer.SerchInput)
                        || o.Customer.IDCardNo.Contains(input.Input.Customer.SerchInput) 
                        || o.Customer.Name.Contains(input.Input.Customer.SerchInput)
                        || o.Customer.NameAB.ToUpper().Contains(input.Input.Customer.SerchInput.ToUpper()));
                    }
                }


                //体检状态
                if (input.Input.CheckSate != null && input.Input.CheckSate != 0)
                    query = query.Where(o => o.CheckSate == input.Input.CheckSate);
                //总检状态
                if (input.Input.SummSate.HasValue)
                    query = query.Where(o => o.SummSate == input.Input.SummSate);
                //登记状态
                if (input.Input.RegisterState.HasValue)
                    query = query.Where(o => o.RegisterState == input.Input.RegisterState);
                if (input.Input.IsPersonal)
                    query = query.Where(o => !o.ClientTeamInfoId.HasValue&&!o.ClientRegId.HasValue);
                //单位
                if (input.Input.TjlClientReg_Id != Guid.Empty)
                    query = query.Where(o => o.ClientReg.Id == input.Input.TjlClientReg_Id);


                //起止时间
                if (input.Input.BookingDateStart != null && input.Input.BookingDateEnd != null)
                {
                    input.Input.BookingDateStart = input.Input.BookingDateStart.Value.AddDays(-1);
                    input.Input.BookingDateStart = new DateTime(input.Input.BookingDateStart.Value.Year,input.Input.BookingDateStart.Value.Month, input.Input.BookingDateStart.Value.Day, 23,59, 59);
                    input.Input.BookingDateEnd = new DateTime(input.Input.BookingDateEnd.Value.Year,
                        input.Input.BookingDateEnd.Value.Month, input.Input.BookingDateEnd.Value.Day, 23, 59,
                        59);
                    query = query.Where(o => o.BookingDate >input.Input.BookingDateStart && o.BookingDate < input.Input.BookingDateEnd);
                }
                else
                {
                    if (input.Input.BookingDateStart != null && input.Input.BookingDateEnd == null)
                    {
                        input.Input.BookingDateStart = input.Input.BookingDateStart.Value.AddDays(-1);
                        input.Input.BookingDateStart = new DateTime(input.Input.BookingDateStart.Value.Year,
                        input.Input.BookingDateStart.Value.Month, input.Input.BookingDateStart.Value.Day, 23,
                        59, 59);
                        query = query.Where(o => o.BookingDate > input.Input.BookingDateStart);
                    }
                    if (input.Input.BookingDateStart == null && input.Input.BookingDateEnd != null)
                    {
                        input.Input.BookingDateEnd = new DateTime(input.Input.BookingDateEnd.Value.Year,
                            input.Input.BookingDateEnd.Value.Month, input.Input.BookingDateEnd.Value.Day, 23, 59,
                            59);
                        query = query.Where(o => o.BookingDate < input.Input.BookingDateEnd);
                    }
                }
                //芜湖根据订单号查询
                if (!string.IsNullOrEmpty(input.Input.OrderNum))
                {
                    var orderlist = input.Input.OrderNum.Split('|').ToList();
                    if (orderlist.Count > 1)
                    {
                        query = query.Where(o => orderlist.Contains( o.OrderNum ));

                    }
                    else
                    {
                        query = query.Where(o => o.OrderNum == input.Input.OrderNum);
                    }
                }
            }
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            if (query.Count() != 0)
            {
                //query = query.OrderByDescending(o => o.CreationTime);
                query = query.OrderByDescending(o => o.BookingDate);

                var result = new PageResultDto<CustomerRegPhysicalDto>();
                result.CurrentPage = input.CurentPage;
                result.Calculate(query.Count());
                query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
                result.Result = query.MapTo<List<CustomerRegPhysicalDto>>();
                return result;
            }
            else
                return null;
        }
    }
}