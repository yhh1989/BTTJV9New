using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Market;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Market
{
    /// <inheritdoc cref="IContractAppService" />
    public class ContractAppService : AppServiceApiProxyBase, IContractAppService
    {
        /// <inheritdoc />
        public Task<List<ContractInformationFullDto>> QueryContractList(QueryContractConditionInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ContractInformationFullDto>>.Factory.StartNew(() =>
                GetResult<QueryContractConditionInput, List<ContractInformationFullDto>>(input, url));
        }

        /// <inheritdoc />
        public Task<List<ContractCategoryDto>> QueryContractCategoryList(QueryContractCategoryConditionInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ContractCategoryDto>>.Factory.StartNew(() =>
                GetResult<QueryContractCategoryConditionInput, List<ContractCategoryDto>>(input, url));
        }

        /// <inheritdoc />
        public Task<List<CompanyDtoForComboBox>> QueryCompanyComboBoxList()
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CompanyDtoForComboBox>>.Factory.StartNew(() =>
                GetResult<List<CompanyDtoForComboBox>>(url));
        }

        /// <inheritdoc />
        public Task<ContractCategoryDto> QueryContractCategoryById(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractCategoryDto>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, ContractCategoryDto>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractCategoryDto> UpdateOrInsertContractCategory(ContractCategoryDto input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractCategoryDto>.Factory.StartNew(() =>
                GetResult<ContractCategoryDto, ContractCategoryDto>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractInformationDto> QueryContractInformationById(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractInformationDto>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, ContractInformationDto>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractInformationDto> InsertOrUpdateContract(ContractInformationDto input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractInformationDto>.Factory.StartNew(() =>
                GetResult<ContractInformationDto, ContractInformationDto>(input, url));
        }

        /// <inheritdoc />
        public Task<List<CompanyRegisterDtoForComboBox>> QueryCompanyRegister(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CompanyRegisterDtoForComboBox>>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, List<CompanyRegisterDtoForComboBox>>(input, url));
        }

        /// <inheritdoc />
        public Task<List<ContractAdjunctDto>> QueryContractAdjunct(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ContractAdjunctDto>>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, List<ContractAdjunctDto>>(input, url));
        }

        /// <inheritdoc />
        public Task<List<ContractProceedsDto>> QueryContractProceeds(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ContractProceedsDto>>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, List<ContractProceedsDto>>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractProceedsDto> InsertContractProceeds(ContractProceedsDto input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractProceedsDto>.Factory.StartNew(() =>
                GetResult<ContractProceedsDto, ContractProceedsDto>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractProceedsDto> DeleteContractProceeds(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractProceedsDto>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, ContractProceedsDto>(input, url));
        }

        /// <inheritdoc />
        public Task<List<ContractInvoiceDto>> QueryContractInvoiceList(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ContractInvoiceDto>>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, List<ContractInvoiceDto>>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractInvoiceDto> InsertContractInvoice(ContractInvoiceDto input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractInvoiceDto>.Factory.StartNew(() =>
                GetResult<ContractInvoiceDto, ContractInvoiceDto>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractInvoiceDto> DeleteContractInvoice(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractInvoiceDto>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, ContractInvoiceDto>(input, url));
        }

        /// <inheritdoc />
        public Task<ContractInformationDto> DeleteContract(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ContractInformationDto>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, ContractInformationDto>(input, url));
        }
    }
}