using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport
{
    public interface IGroupReportAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 单位/预约查询
        /// </summary>
        List<SelectClientRewDto> QueryCompany();

        /// <summary>
        /// 单位/预约按名称查询
        /// </summary>
        List<SelectClientRewDto> QueryCompanyName(SelectClientRewDto input);

        /// <summary>
        /// 分组/人员查询
        /// </summary>

        List<List<GroupClientRegDto>> QueryGroup(List<GroupClientRegDto> input);
        /// <summary>
        /// 获取单位下总检建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GroupClientSumDto> GRClientCusSum(ClientRegIdDto input);
        /// <summary>
        /// 获取单位历史数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<HistoryGroupClientSumDto> GRHisClientCusSum(ClientRegIdDto input);
       /// <summary>
       /// 获取单位下人员
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        List<GroupClientCusDto> GRClientRegCus(ClientRegIdDto input);
        List<SimGroupClientCusDto> GRClientRegCusSmp(ClientRegIdDto input);

        List<SimGroupClientCusDto> GRClientRegCusBM(CusRegBMDto input);
        /// <summary>
        /// 获取单位下组合项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GroupClientItemsDto> GRClientRegItems(ClientRegIdDto input);
        /// <summary>
        /// 根据ID获取单位预约
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        List<SelectClientRewDto> getreglist(List<EntityDto<Guid>> input);
        List<GroupClientRegDto> GetCompanyRegisterIncludeGroupAndPersonnel(EntityDto<Guid> input);
        List<GroupSumBMDto> getClientSum(ClientRegIdDto input);
        List<SumClientDto> getClientSumResult(EntityDto<Guid> input);
        List<CusReviewGroupDto> ReVewGroupCus(EntityDto<List<Guid>> input);
    }
}