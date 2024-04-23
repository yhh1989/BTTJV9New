using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable
{
    public interface ICrossTableAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 查询预约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CrossTableViewDto QueryCustomerInfo(PageInputDto<QueryInfoDto> input);

        List<CustomerRegForCrossTableViewDto> QueryCustomer(QueryInfoDto input);
        List<CusGiveUpDto> getGiveUps(EntityDto<Guid> input);
        /// <summary>
        /// 编辑交表状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerRegForCrossTableDto EditCrossTableState(CustomerRegForCrossTableDto input);
        /// <summary>
        /// 到检确定
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerRegForCrossTableDto EditCheckDateState(CustomerRegForCrossTableDto input);
        /// <summary>
        /// 编辑项目组状态
        /// </summary>
        /// <returns></returns>
        List<CustomerItemGroupDto> EditItemGroupState(EditItemGroupStateInput input);

        /// <summary>
        /// 抽血
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerBloodNumDto DrawBlood(CustomerBloodNumDto input);

        /// <summary>
        /// 取消抽血
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerBloodNumDto CancelDrawBlood(CustomerRegForCrossTableDto input);
        /// <summary>
        /// 查询放弃待检表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CusGiveUpDto QueryGiveUpInfo(QueryGiveUpDto input);

        List<CustomerRegItemDto> QueryCustomerItems(QuerCustomerItemsDto input);
        /// <summary>
        /// 设置项目加减项
        /// </summary>
        SetItemGroupAddMinusDto SetItemGroupAddMinusState(SetItemGroupAddMinusDto input);
    }
}