using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Company
{
    /// <inheritdoc cref="Sw.Hospital.HealthExaminationSystem.Application.Company.ICompanyRelatedAppService" />
    public class CompanyRelatedAppService : AppServiceApiProxyBase, ICompanyRelatedAppService
    {
        /// <inheritdoc />
        public CompanyReportPrintRecordDto GetLastRecordByCompanyRegisterId(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return GetResult<EntityDto<Guid>, CompanyReportPrintRecordDto>(input, url);
        }

        /// <inheritdoc />
        public CompanyReportPrintRecordDto CreateCompanyReportPrintRecord(CreateCompanyReportPrintRecordDto input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return GetResult<CreateCompanyReportPrintRecordDto, CompanyReportPrintRecordDto>(input, url);
        }

        /// <inheritdoc />
        public CompanyRegisterForPrintDto GetCompanyRegisterById(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return GetResult<EntityDto<Guid>, CompanyRegisterForPrintDto>(input, url);
        }

        /// <inheritdoc />
        public Task<List<CompanyRegisterDtoForComboBox>> QueryCompanyRegisterForComboBoxByCompanyId(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CompanyRegisterDtoForComboBox>>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, List<CompanyRegisterDtoForComboBox>>(input, url));
        }

        /// <inheritdoc />
        public Task<List<CompanyRegisterTeamInformationDtoForComboBox>> QueryCompanyRegisterTeamInformationForComboBoxByCompanyRegisterId(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CompanyRegisterTeamInformationDtoForComboBox>>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, List<CompanyRegisterTeamInformationDtoForComboBox>>(input, url));
        }

        /// <inheritdoc />
        public Task<CompanyInformationDto> CompanyInformationById(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<CompanyInformationDto>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, CompanyInformationDto>(input, url));
        }

        /// <inheritdoc />
        public Task<List<CompanyInformationDtoNo2>> CompanyInformationListByCondition(CompanyFilterInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CompanyInformationDtoNo2>>.Factory.StartNew(() =>
                GetResult<CompanyFilterInput, List<CompanyInformationDtoNo2>>(input, url));
        }

        /// <inheritdoc />
        public Task<List<CompanyInformationDtoNo3>> CompanyInformationListByParentId(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CompanyInformationDtoNo3>>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, List<CompanyInformationDtoNo3>>(input, url));
        }
    }
}