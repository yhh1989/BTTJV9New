using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
    public interface IInspectionTotalAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 查询体检人登记信息（单条）
        /// </summary> 
        /// <returns></returns>
        List<TjlCustomerRegForInspectionTotalSearchDto> GetCustomerRegList(TjlCustomerQuery query);

        /// <summary>
        /// 总检dto转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TjlCustomerRegForInspectionTotalDto  Transformation(TjlCustomerRegForInspectionTotalSearchDto input);

        /// <summary>
        /// 查询体检人登记信息（列表页）
        /// </summary>
        /// <returns></returns>
        List<InspectionTotalListDto> GetInspectionTotalList(TjlCustomerQuery query);

        /// <summary>
        /// 生成总检结论表
        /// </summary>
        /// <param name="input"></param>
        TjlCustomerSummarizeDto CreateSummarize(TjlCustomerSummarizeDto input);
        /// <summary>
        /// 保存总检审核
        /// </summary>
        /// <param name="input"></param>
        CustomerRegisterSummarizeDto SaveSummarize(CustomerRegisterSummarizeDto input);

        /// <summary>
        /// 生成总检建议表
        /// </summary>
        /// <param name="input"></param>
        void CreateSummarizeBM(List<TjlCustomerSummarizeBMDto> input);

        /// <summary>
        /// 分页获取体检人登记信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<TjlCustomerRegForInspectionTotalDto> PageFulls(PageInputDto<TjlCustomerQuery> input);

        /// <summary>
        /// 查询体检人检查项目结果表
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        List<TjlCustomerRegItemDto> GetCustomerRegItemList(QueryClass query);

        /// <summary>
        /// 修改体检人预约信息表
        /// </summary>
        /// <param name="dto"></param>
        void UpdateTjlCustomerRegDto(TjlCustomerRegForInspectionTotalDto dto);
        /// <summary>
        /// 修改总检状态
        /// </summary>
        /// <param name="dto"></param>

        void UpdateCusSumSate(INCusSumSateDto dto);

        /// <summary>
        /// 修改体检人锁定状态
        /// </summary>
        /// <param name="dto"></param>
        void UpdateCuslockSate(LockStateDto dto);

         void UpdateTjlCustomerRegState(EditCustomerRegStateDto dto);

        void UpdateOccTjlCustomerRegState(OccEditCustomerRegStateDto dto);
        /// <summary>
        /// 获取总检建议
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        TjlCustomerSummarizeDto GetSummarize(TjlCustomerQuery query);
        /// <summary>
        /// 获取总检建议
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<TjlCustomerSummarizeBMDto> GetSummarizeBM(TjlCustomerQuery query);
        /// <summary>
        /// 获取总检建议列表-报告
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<TjlCustomerSummarizeBMViewDto> GetlstSummarizeBM(QueryClass query);
        /// <summary>
        /// 总检医生工作量查询
        /// </summary>
        /// <returns></returns>
        List<InspectionTotalStatisticsDto> InspectionTotalStatistics(TjlCustomerQuery query);

        /// <summary>
        /// 新增总检退回
        /// </summary>
        /// <param name="input"></param>
        void CreateCustomerSummBack(List<TjlCustomerSummBackDto> input);

        /// <summary>
        /// 获取总检退回
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<TjlCustomerSummBackDto> GetCustomerSummBack(TjlCustomerQuery query);

        /// <summary>
        /// 删除建议表（对应多条）
        /// </summary>
        void DelTjlCustomerSummarizeBM(TjlCustomerQuery query);

        /// <summary>
        /// 删除建议汇总表（对应一条）
        /// </summary>
        void DelTjlCustomerSummarize(TjlCustomerQuery query);


        /// <summary>
        /// 查询体检人详细登记信息
        /// </summary>
        /// <returns></returns>
        ATjlCustomerRegDto GetCustomerRegInfo(EntityDto<Guid> Id);

        /// <summary>
        /// 查询体检人科室小结信息
        /// </summary>
        /// <returns>预约记录id</returns>
        List<ATjlCustomerDepSummaryDto> GetCustomerDepSummaryList(EntityDto<Guid> Id);

        /// <summary>
        /// 根据体检人身份证查询预约记录
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        TjlCustomerRegForInspectionTotalDto GetCustomerRegForIDCode(SearchCustomerRegDto Input);
        /// <summary>
        /// 根据体检人身份证查询所有记录
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        List<TjlCustomerRegForInspectionTotalDto> GetAllCustomerRegForIDCode(SearchCustomerRegDto Input);
        /// <summary>
        /// 历年项目结果
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<HistoryComparison.Dto.SearchCustomerRegItemDto> GetTjlCustomerHistoryItemReoprtDtos(EntityDto<Guid> input);
        /// <summary>
        /// 获取历年建议结论
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<TjlCustomerSummarizeBMViewDto> GetHistorySummarizeBM(EntityDto<Guid> input);
        /// <summary>
        /// 总检展示项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<CustomerRegInspectItemDto> GetCusInspectItemList(QueryClass query);
         List<CustomerRegInspectGroupDto> GetCusGroupList(QueryClass input);
        /// <summary>
        /// 获取复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusReViewDto> GetCusReViewDto(EntityDto<Guid> input);
        /// <summary>
        /// 补检
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusGiveUpShowDto> GetCusGiveDto(EntityDto<Guid> input);
        /// <summary>
        /// 根据阳性匹配复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusReViewDto> GetIllReViewDto(List<TjlCustomerSummarizeBMDto> input);
        /// <summary>
        /// 根据阳性匹配复查新总检
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusReViewDto> GetIllReViewNewDto(List<CustomerRegisterSummarizeSuggestDto> input);
        /// <summary>
        /// 保存复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusReViewDto> SaveCusReViewDto(List<CusReViewDto> input);
        /// <summary>
        /// 删除复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void DelCusReViewDto(EntityDto<Guid> input);

        List<TjlCustomerSummarizeDto> GetCustomerSummary(EntityDto<Guid> Id);
        /// <summary>
        /// 查询下一位已完成的体检人
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<TjlCustomerRegForInspectionTotalDto> GetReCustomerReg(TjlCustomerQuery query);
        /// <summary>
        /// 添加屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TbmSumHideDto AddSumHide(TbmSumHideDto input);
        /// <summary>
        /// 修改屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TbmSumHideDto EditSumHide(TbmSumHideDto input);
        /// <summary>
        /// 删除屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void delSumHide(EntityDto<Guid> input);
        /// <summary>
        /// 查询屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<TbmSumHideDto> SearchSumHide();

        /// <summary>
        /// 根据体检人危害因素获取职业健康等
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OccTargetDiseaseByRisk GetOccSumByCusReg(EntityDto<Guid> input);
        /// <summary>
        /// 保存职业健康总检
        /// </summary>
        /// <param name="inputOccCusSumDto"></param>
        /// <returns></returns>
        InputOccCusSumDto SaveOccSum(InputOccCusSumDto inputOccCusSumDto);

        /// <summary>
        /// 根据体检人获取职业健康总检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        InputOccCusSumDto GetCusOccSumByRegId(EntityDto<Guid> input);
        /// <summary>
        /// 获取总检列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<OutCusListDto> GetOutCus(InSearchCusDto input);
        /// <summary>
        /// 获取已总检人员合格不合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<JKZCusDto> GetJKZOutCus(InSearchCusDto input);
        /// <summary>
        /// 合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<JKZCusDto> GetJKZOutCuslist(InSearchCusDto input);
        /// <summary>
        /// 判断健康证是否合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool ISHG(EntityDto<Guid> input);
        List<CustomerGroupSumDto> GetCustomerRegGroupSum(ChargeBM input);
        void SavePerSum(BeforSaveSumDto input);

        BeforSumCustomerRegDto GetCustomerReg(ChargeBM input);
        /// <summary>
        /// 删除职业健康总检
        /// </summary>
        void DelOccSum(TjlCustomerQuery query);
        /// <summary>
        /// 匹配冲突
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutMesDto MatchSumConflict(SumAdviseDto input);
        /// <summary>
        /// 获取组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusGroupShowDto> getCusGroup(EntityDto<Guid> input);
        /// <summary>
        /// 报告获取复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<reportCusReViewDto> GetCusReView(EntityDto<Guid> input);

        /// <summary>
        /// 复查总检
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        reportCusReSumDto GetCusReSum(EntityDto<Guid> input);
    }
}