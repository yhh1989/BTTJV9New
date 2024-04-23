using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Company;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company
{
    /// <inheritdoc cref="ICompanyRelatedAppService" />
    [AbpAuthorize]
    public class CompanyRelatedAppService : MyProjectAppServiceBase, ICompanyRelatedAppService
    {
        /// <summary>
        /// 公司报告打印记录仓储
        /// </summary>
        private readonly IRepository<CompanyReportPrintRecord, Guid> _companyReportPrintRecordRepository;

        /// <summary>
        /// 公司预约仓储
        /// </summary>
        private readonly IRepository<TjlClientReg, Guid> _companyRegisterRepository;

        /// <summary>
        /// 公司预约分组信息仓储
        /// </summary>
        private readonly IRepository<TjlClientTeamInfo, Guid> _companyRegisterTeamInformationRepository;

        /// <summary>
        /// 公司信息仓储
        /// </summary>
        private readonly IRepository<TjlClientInfo, Guid> _companyInformationRepository;

        /// <summary>
        /// 初始化团体及团体相关的应用服务
        /// </summary>
        public CompanyRelatedAppService(IRepository<CompanyReportPrintRecord, Guid> companyReportPrintRecordRepository, IRepository<TjlClientReg, Guid> companyRegisterRepository, IRepository<TjlClientTeamInfo, Guid> companyRegisterTeamInformationRepository, IRepository<TjlClientInfo, Guid> companyInformationRepository)
        {
            _companyReportPrintRecordRepository = companyReportPrintRecordRepository;
            _companyRegisterRepository = companyRegisterRepository;
            _companyRegisterTeamInformationRepository = companyRegisterTeamInformationRepository;
            _companyInformationRepository = companyInformationRepository;
        }

        /// <inheritdoc />
        public CompanyReportPrintRecordDto GetLastRecordByCompanyRegisterId(EntityDto<Guid> input)
        {
            var row = _companyReportPrintRecordRepository.FirstOrDefault(r => r.CompanyRegisterId == input.Id);
            if (row == null)
            {
                return null;
            }

            return ObjectMapper.Map<CompanyReportPrintRecordDto>(row);
        }

        /// <inheritdoc />
        public CompanyReportPrintRecordDto CreateCompanyReportPrintRecord(CreateCompanyReportPrintRecordDto input)
        {
            var row = ObjectMapper.Map<CompanyReportPrintRecord>(input);
            row.Id = Guid.NewGuid();
            var result = _companyReportPrintRecordRepository.Insert(row);
            return ObjectMapper.Map<CompanyReportPrintRecordDto>(result);
        }

        /// <inheritdoc />
        public CompanyRegisterForPrintDto GetCompanyRegisterById(EntityDto<Guid> input)
        {
            var query = _companyRegisterRepository.Get(input.Id);
            return ObjectMapper.Map<CompanyRegisterForPrintDto>(query);
        }

        /// <inheritdoc />
        public async Task<List<CompanyRegisterDtoForComboBox>> QueryCompanyRegisterForComboBoxByCompanyId(EntityDto<Guid> input)
        {
            var query = _companyRegisterRepository.GetAll().Where(r => r.ClientInfoId == input.Id).OrderByDescending(r => r.CreationTime);
            return await query.ProjectToListAsync<CompanyRegisterDtoForComboBox>(
                GetConfigurationProvider<CompanyRegisterDtoForComboBox>());
        }

        /// <inheritdoc />
        public async Task<List<CompanyRegisterTeamInformationDtoForComboBox>> QueryCompanyRegisterTeamInformationForComboBoxByCompanyRegisterId(EntityDto<Guid> input)
        {
            var query = _companyRegisterTeamInformationRepository.GetAll().Where(r => r.ClientRegId == input.Id)
                .OrderBy(r => r.TeamBM);
            return await query.ProjectToListAsync<CompanyRegisterTeamInformationDtoForComboBox>(
                GetConfigurationProvider<CompanyRegisterTeamInformationDtoForComboBox>());
        }

        /// <inheritdoc />
        public async Task<CompanyInformationDto> CompanyInformationById(EntityDto<Guid> input)
        {
            var row = await _companyInformationRepository.GetAsync(input.Id);
            return ObjectMapper.Map<CompanyInformationDto>(row);
        }

        /// <inheritdoc />
        public async Task<List<CompanyInformationDtoNo2>> CompanyInformationListByCondition(CompanyFilterInput input)
        {
            var query = _companyInformationRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(input.Code))
            {
                query = query.Where(r => r.Code == input.Code);
            }

            if (!string.IsNullOrWhiteSpace(input.ClientName))
            {
                query = query.Where(r => r.ClientName.Contains(input.ClientName));
            }

            if (input.ClientSourceList != null && input.ClientSourceList.Count != 0)
            {
                query = query.Where(r => input.ClientSourceList.Contains(r.ClientSource));
            }

            if (input.CustomerServiceUserId.HasValue)
            {
                query = query.Where(r => r.UserId == input.CustomerServiceUserId);
            }

            if (!string.IsNullOrWhiteSpace(input.LinkMan))
            {
                query = query.Where(r => r.LinkMan == input.LinkMan);
            }

            if (input.CreationTimeStart.HasValue)
            {
                query = query.Where(r => r.CreationTime >= input.CreationTimeStartValueDate);
            }

            if (input.CreateTimeEnd.HasValue)
            {
                query = query.Where(r => r.CreationTime < input.CreationTimeEndValueDate);
            }

            query = query.OrderByDescending(r => r.CreationTime);

            return await query.ProjectToListAsync<CompanyInformationDtoNo2>(
                GetConfigurationProvider<CompanyInformationDtoNo2>());
        }

        /// <inheritdoc />
        public async Task<List<CompanyInformationDtoNo3>> CompanyInformationListByParentId(EntityDto<Guid> input)
        {
            var query = _companyInformationRepository.GetAll().Where(r => r.ParentId == input.Id);
            return await query.ProjectToListAsync<CompanyInformationDtoNo3>(
                GetConfigurationProvider<CompanyInformationDtoNo3>());
        }
    }
}