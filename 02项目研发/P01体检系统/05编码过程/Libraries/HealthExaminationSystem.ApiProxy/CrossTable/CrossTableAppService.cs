using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable
{
    public class CrossTableAppService : AppServiceApiProxyBase, ICrossTableAppService
    {
        public CrossTableViewDto QueryCustomerInfo(PageInputDto<QueryInfoDto> input)
        {
            return GetResult<PageInputDto<QueryInfoDto>, CrossTableViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerRegForCrossTableViewDto> QueryCustomer(QueryInfoDto input)
        {
            return GetResult<QueryInfoDto, List<CustomerRegForCrossTableViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<CusGiveUpDto> getGiveUps(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CusGiveUpDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CustomerRegForCrossTableDto EditCrossTableState(CustomerRegForCrossTableDto input)
        {
            return GetResult<CustomerRegForCrossTableDto, CustomerRegForCrossTableDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CustomerRegForCrossTableDto EditCheckDateState(CustomerRegForCrossTableDto input)
        {
            return GetResult<CustomerRegForCrossTableDto, CustomerRegForCrossTableDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<CustomerItemGroupDto> EditItemGroupState(EditItemGroupStateInput input)
        {
            return GetResult<EditItemGroupStateInput, List<CustomerItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public CustomerBloodNumDto DrawBlood(CustomerBloodNumDto input)
        {
            return GetResult<CustomerBloodNumDto, CustomerBloodNumDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public CustomerBloodNumDto CancelDrawBlood(CustomerRegForCrossTableDto input)
        {
            return GetResult<CustomerRegForCrossTableDto, CustomerBloodNumDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public CusGiveUpDto QueryGiveUpInfo(QueryGiveUpDto input)
        {
            return GetResult<QueryGiveUpDto, CusGiveUpDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CustomerRegItemDto> QueryCustomerItems(QuerCustomerItemsDto input)
        {
            return GetResult<QuerCustomerItemsDto, List<CustomerRegItemDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 设置项目加减项
        /// </summary>
        public SetItemGroupAddMinusDto SetItemGroupAddMinusState(SetItemGroupAddMinusDto input)
        {
            return GetResult<SetItemGroupAddMinusDto, SetItemGroupAddMinusDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}