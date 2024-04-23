using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport
{
    public class GroupReportAppService : AppServiceApiProxyBase, IGroupReportAppService
    {
        public List<SelectClientRewDto> QueryCompany()
        {
            return GetResult<List<SelectClientRewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SelectClientRewDto> QueryCompanyName(SelectClientRewDto input)
        {
            return GetResult<SelectClientRewDto, List<SelectClientRewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<List<GroupClientRegDto>> QueryGroup(List<GroupClientRegDto> input)
        {
            return GetResult<List<GroupClientRegDto>, List<List<GroupClientRegDto>>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<GroupClientSumDto> GRClientCusSum(ClientRegIdDto input)
        {
            return GetResult<ClientRegIdDto, List<GroupClientSumDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<HistoryGroupClientSumDto> GRHisClientCusSum(ClientRegIdDto input)
        {
            return GetResult<ClientRegIdDto, List<HistoryGroupClientSumDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<GroupClientCusDto> GRClientRegCus(ClientRegIdDto input)
        { 
            return GetResult<ClientRegIdDto, List<GroupClientCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SimGroupClientCusDto> GRClientRegCusSmp(ClientRegIdDto input)
        { 
            return GetResult<ClientRegIdDto, List<SimGroupClientCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SimGroupClientCusDto> GRClientRegCusBM(CusRegBMDto input)
        {
            return GetResult<CusRegBMDto, List<SimGroupClientCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<GroupClientItemsDto> GRClientRegItems(ClientRegIdDto input)
        {
            return GetResult<ClientRegIdDto, List<GroupClientItemsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SelectClientRewDto> getreglist(List<EntityDto<Guid>> input)
        {
            return GetResult<List<EntityDto<Guid>>, List<SelectClientRewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<GroupClientRegDto> GetCompanyRegisterIncludeGroupAndPersonnel(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<GroupClientRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<GroupSumBMDto> getClientSum(ClientRegIdDto input)
        {
            return GetResult<ClientRegIdDto, List<GroupSumBMDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SumClientDto> getClientSumResult(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<SumClientDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取单位复查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReviewGroupDto> ReVewGroupCus(EntityDto<List<Guid>> input)
        {
            return GetResult<EntityDto<List<Guid>>, List<CusReviewGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}