using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;

namespace Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition
{
    /// <summary>
    /// 人工行程安排应用服务
    /// </summary>
    [AbpAuthorize]
    public class ManualSchedulingAppService : MyProjectAppServiceBase, IManualSchedulingAppService
    {
        /// <summary>
        /// “人工行程安排”仓储
        /// </summary>
        private readonly IRepository<ManualScheduling, Guid> _manualSchedulingRepository;

        /// <summary>
        /// “科室”仓储
        /// </summary>
        private readonly IRepository<TbmDepartment, Guid> _departmentRepository;

        /// <summary>
        /// “公司信息”仓储
        /// </summary>
        private readonly IRepository<TjlClientInfo, Guid> _companyInformationRepository;

        /// <summary>
        /// “体检人预约”仓储
        /// </summary>
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegisterRepository;

        /// <summary>
        /// “单位预约”仓储
        /// </summary>
        private readonly IRepository<TjlClientReg, Guid> _companyRegisterRepository;

        /// <summary>
        /// 初始化“人工行程安排应用服务”
        /// </summary>
        public ManualSchedulingAppService(IRepository<ManualScheduling, Guid> manualSchedulingRepository,
            IRepository<TbmDepartment, Guid> departmentRepository,
            IRepository<TjlClientInfo, Guid> companyInformationRepository,
            IRepository<TjlCustomerReg, Guid> customerRegisterRepository,
            IRepository<TjlClientReg, Guid> companyRegisterRepository)
        {
            _manualSchedulingRepository = manualSchedulingRepository;
            _departmentRepository = departmentRepository;
            _companyInformationRepository = companyInformationRepository;
            _customerRegisterRepository = customerRegisterRepository;
            _companyRegisterRepository = companyRegisterRepository;
        }

        /// <inheritdoc />
        public async Task<List<ManualSchedulingDtoNo1>> QueryManualSchedulingList()
        {
            var date = DateTime.Today.AddDays(-8);
            var manualSchedulingQuery = _manualSchedulingRepository.GetAll().AsNoTracking();
            manualSchedulingQuery = manualSchedulingQuery.Where(r => r.IsActive);
            manualSchedulingQuery = manualSchedulingQuery.Where(r => r.SchedulingDate >= date);
            manualSchedulingQuery = manualSchedulingQuery.OrderBy(r => r.SchedulingDate);
            return await manualSchedulingQuery.ProjectToListAsync<ManualSchedulingDtoNo1>(
                GetConfigurationProvider<ManualSchedulingDtoNo1>());
        }

        /// <inheritdoc />
        public async Task<ManualSchedulingDtoNo1> InsertManualScheduling(ManualSchedulingDtoNo2 input)
        {
            input.Id = Guid.NewGuid();
            var entity = ObjectMapper.Map<ManualScheduling>(input);
            entity.IsActive = true;
            if (entity.DepartmentCollection == null)
            {
                entity.DepartmentCollection = new List<TbmDepartment>();
            }
            if (input.DepartmentIdList != null && input.DepartmentIdList.Count != 0)
            {
                foreach (var guid in input.DepartmentIdList)
                {
                    entity.DepartmentCollection.Add(await _departmentRepository.GetAsync(guid));
                }
            }

            await _manualSchedulingRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            var result = await _manualSchedulingRepository.GetAsync(input.Id);
            var dto = ObjectMapper.Map<ManualSchedulingDtoNo1>(result);
            if (dto.Company == null && dto.CompanyId.HasValue)
            {
                dto.Company =
                    ObjectMapper.Map<CompanyInformationDtoNo1>(
                        await _companyInformationRepository.GetAsync(dto.CompanyId.Value));
            }
            return dto;
        }

        /// <inheritdoc />
        public async Task<ManualSchedulingDtoNo1> UpdateManualScheduling(ManualSchedulingDtoNo2 input)
        {
            var entity = await _manualSchedulingRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, entity);
            if (entity.DepartmentCollection == null)
            {
                entity.DepartmentCollection = new List<TbmDepartment>();
            }
            if (input.DepartmentIdList != null && input.DepartmentIdList.Count != 0)
            {
                var departmentWaitRemoveList = new List<TbmDepartment>();
                foreach (var department in entity.DepartmentCollection)
                {
                    if (input.DepartmentIdList.Contains(department.Id))
                    {
                        // ignored
                    }
                    else
                    {
                        departmentWaitRemoveList.Add(department);
                    }
                }

                foreach (var department in departmentWaitRemoveList)
                {
                    entity.DepartmentCollection.Remove(department);
                }

                foreach (var guid in input.DepartmentIdList)
                {
                    if (entity.DepartmentCollection.Any(r => r.Id == guid))
                    {
                        // ignored
                    }
                    else
                    {
                        entity.DepartmentCollection.Add(await _departmentRepository.GetAsync(guid));
                    }
                }
            }
            else
            {
                entity.DepartmentCollection.Clear();
            }

            var result = await _manualSchedulingRepository.UpdateAsync(entity);
            return ObjectMapper.Map<ManualSchedulingDtoNo1>(result);
        }

        /// <inheritdoc />
        public async Task<List<ManualSchedulingDtoNo1>> QueryPersonSchedulingList()
        {
            var date = DateTime.Today.AddDays(-8);
            var customerRegisterQuery = _customerRegisterRepository.GetAll().AsNoTracking();
            customerRegisterQuery = customerRegisterQuery.Where(r => r.ClientInfoId == null);
            customerRegisterQuery = customerRegisterQuery.Where(r => r.BookingDate >= date);
            customerRegisterQuery = customerRegisterQuery.OrderBy(r => r.BookingDate);
            var result = await customerRegisterQuery.ToListAsync();
            var resultGroupByDate = result.GroupBy(r => r.BookingDate.GetValueOrDefault().Date);
            var resultList = new List<ManualSchedulingDtoNo1>();
            foreach (var regs in resultGroupByDate)
            {
                var dto = new ManualSchedulingDtoNo1();
                dto.Name = "个人体检";
                dto.SchedulingDate = regs.Key;
                dto.NumberOfCustomer = regs.Count();
                dto.Description = new List<string>
                {
                    "Title人数详情",
                    $"男性：{regs.Count(r => r.Customer.Sex == (int)Sex.Man)} 人",
                    $"女性：{regs.Count(r => r.Customer.Sex == (int)Sex.Woman)} 人"
                };
                resultList.Add(dto);
            }

            return resultList;
        }

        /// <inheritdoc />
        public async Task<List<ManualSchedulingDtoNo1>> QueryCompanySchedulingList()
        {
            var date = DateTime.Today.AddDays(-8);
            var customerRegisterQuery = _customerRegisterRepository.GetAll().AsNoTracking();
            customerRegisterQuery = customerRegisterQuery.Where(r => r.ClientInfoId != null);
            customerRegisterQuery = customerRegisterQuery.Where(r => r.BookingDate >= date);
            customerRegisterQuery = customerRegisterQuery.OrderBy(r => r.BookingDate);
            var result = await customerRegisterQuery.ToListAsync();
            var resultGroupByDate = result.GroupBy(r => r.BookingDate.GetValueOrDefault().Date);
            var resultList = new List<ManualSchedulingDtoNo1>();
            foreach (var regs in resultGroupByDate)
            {
                var regsGroupByCompany = regs.GroupBy(r => r.ClientInfoId);
                foreach (var customerRegs in regsGroupByCompany)
                {
                    var dto = new ManualSchedulingDtoNo1();
                    dto.CompanyId = customerRegs.Key.GetValueOrDefault();
                    dto.Company = ObjectMapper.Map<CompanyInformationDtoNo1>(
                        await _companyInformationRepository.GetAsync(dto.CompanyId.Value));
                    dto.SchedulingDate = regs.Key;
                    dto.NumberOfCustomer = customerRegs.Count();
                    var companyRegisterIdList =
                        customerRegs.Select(r => r.ClientRegId.GetValueOrDefault()).Distinct().ToList();
                    dto.Description = new List<string>();
                    foreach (var companyRegisterId in companyRegisterIdList)
                    {
                        var companyRegister = await _companyRegisterRepository.GetAsync(companyRegisterId);
                        dto.Description.Add($"Title{companyRegister.RegInfo}-{companyRegister.Remark}");
                        var companyRegisterTeamList = companyRegister.ClientTeamInfo.OrderBy(r=>r.TeamBM).ToList();
                        foreach (var teamInfo in companyRegisterTeamList)
                        {
                            dto.Description.Add($"{teamInfo.TeamName} {customerRegs.Count(r=>r.ClientTeamInfoId == teamInfo.Id)} 人");
                        }
                    }
                    resultList.Add(dto);
                }
            }

            return resultList;
        }
    }
}