using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
    public class InspectionTotalAppService : AppServiceApiProxyBase, IInspectionTotalAppService
    {
        public TjlCustomerSummarizeDto CreateSummarize(TjlCustomerSummarizeDto input)
        {
            return GetResult<TjlCustomerSummarizeDto, TjlCustomerSummarizeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
      
        public CustomerRegisterSummarizeDto SaveSummarize(CustomerRegisterSummarizeDto input)
        {
            return GetResult<CustomerRegisterSummarizeDto, CustomerRegisterSummarizeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void CreateSummarizeBM(List<TjlCustomerSummarizeBMDto> input)
        {
            GetResult<List<TjlCustomerSummarizeBMDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<TjlCustomerRegItemDto> GetCustomerRegItemList(QueryClass query)
        {
            return GetResult<QueryClass, List<TjlCustomerRegItemDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        } 

        public List<TjlCustomerRegForInspectionTotalSearchDto> GetCustomerRegList(TjlCustomerQuery query)
        {
            return GetResult<TjlCustomerQuery, List<TjlCustomerRegForInspectionTotalSearchDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public TjlCustomerRegForInspectionTotalDto Transformation(TjlCustomerRegForInspectionTotalSearchDto input)
        {
            return GetResult<TjlCustomerRegForInspectionTotalSearchDto, TjlCustomerRegForInspectionTotalDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        public List<InspectionTotalListDto> GetInspectionTotalList(TjlCustomerQuery query)
        {
            return GetResult<TjlCustomerQuery, List<InspectionTotalListDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public TjlCustomerSummarizeDto GetSummarize(TjlCustomerQuery query)
        {
            return GetResult<TjlCustomerQuery, TjlCustomerSummarizeDto>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<TjlCustomerSummarizeBMDto> GetSummarizeBM(TjlCustomerQuery query)
        {
            return GetResult<TjlCustomerQuery, List<TjlCustomerSummarizeBMDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public PageResultDto<TjlCustomerRegForInspectionTotalDto> PageFulls(PageInputDto<TjlCustomerQuery> input)
        {
            return GetResult<PageInputDto<TjlCustomerQuery>, PageResultDto<TjlCustomerRegForInspectionTotalDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void UpdateTjlCustomerRegDto(TjlCustomerRegForInspectionTotalDto dto)
        {
            GetResult<TjlCustomerRegForInspectionTotalDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpdateCusSumSate(INCusSumSateDto dto)
        { GetResult<INCusSumSateDto>(dto, DynamicUriBuilder.GetAppSettingValue()); }

        public void UpdateCuslockSate(LockStateDto dto)
        { GetResult<LockStateDto>(dto, DynamicUriBuilder.GetAppSettingValue()); }
        public void UpdateTjlCustomerRegState(EditCustomerRegStateDto dto)
        {
            GetResult<EditCustomerRegStateDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpdateOccTjlCustomerRegState(OccEditCustomerRegStateDto dto)
        {
            GetResult<OccEditCustomerRegStateDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<InspectionTotalStatisticsDto> InspectionTotalStatistics(TjlCustomerQuery query)
        {
            return GetResult<TjlCustomerQuery, List<InspectionTotalStatisticsDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<TjlCustomerSummarizeBMDto> GetSummarizeBM(QueryClass query)
        {
            return GetResult<QueryClass, List<TjlCustomerSummarizeBMDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TjlCustomerSummarizeBMViewDto> GetlstSummarizeBM(QueryClass query)
        {
            return GetResult<QueryClass, List<TjlCustomerSummarizeBMViewDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public void CreateCustomerSummBack(List<TjlCustomerSummBackDto> input)
        {
            GetResult<List<TjlCustomerSummBackDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<TjlCustomerSummBackDto> GetCustomerSummBack(TjlCustomerQuery query)
        {
            return GetResult<TjlCustomerQuery, List<TjlCustomerSummBackDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DelTjlCustomerSummarizeBM(TjlCustomerQuery query)
        {
            GetResult<TjlCustomerQuery>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DelTjlCustomerSummarize(TjlCustomerQuery query)
        {
            GetResult<TjlCustomerQuery>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public ATjlCustomerRegDto GetCustomerRegInfo(EntityDto<Guid> Id)
        {
            return GetResult<EntityDto<Guid>, ATjlCustomerRegDto>(Id, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ATjlCustomerDepSummaryDto> GetCustomerDepSummaryList(EntityDto<Guid> Id)
        {
            return GetResult<EntityDto<Guid>, List<ATjlCustomerDepSummaryDto>>(Id, DynamicUriBuilder.GetAppSettingValue());
        }

        public TjlCustomerRegForInspectionTotalDto GetCustomerRegForIDCode(SearchCustomerRegDto Input)
        {
            return GetResult<SearchCustomerRegDto, TjlCustomerRegForInspectionTotalDto>(Input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据体检人身份证查询所有记录
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetAllCustomerRegForIDCode(SearchCustomerRegDto Input)
        {
            return GetResult<SearchCustomerRegDto, List<TjlCustomerRegForInspectionTotalDto>>(Input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<HistoryComparison.Dto.SearchCustomerRegItemDto> GetTjlCustomerHistoryItemReoprtDtos(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<HistoryComparison.Dto.SearchCustomerRegItemDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        public List<TjlCustomerSummarizeBMViewDto> GetHistorySummarizeBM(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<TjlCustomerSummarizeBMViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerRegInspectItemDto> GetCusInspectItemList(QueryClass input)
        {
            return GetResult<QueryClass, List<CustomerRegInspectItemDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerRegInspectGroupDto> GetCusGroupList(QueryClass input)
        {
            return GetResult<QueryClass, List<CustomerRegInspectGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CusReViewDto> GetCusReViewDto(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CusReViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<CusGiveUpShowDto> GetCusGiveDto(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CusGiveUpShowDto>>(input, DynamicUriBuilder.GetAppSettingValue());


        }
        /// <summary>
        /// 根据阳性匹配复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReViewDto> GetIllReViewDto(List<TjlCustomerSummarizeBMDto> input)
        {
            return GetResult<List<TjlCustomerSummarizeBMDto>, List<CusReViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<CusReViewDto> GetIllReViewNewDto(List<CustomerRegisterSummarizeSuggestDto> input)
        {
            return GetResult<List<CustomerRegisterSummarizeSuggestDto>, List<CusReViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 保存复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReViewDto> SaveCusReViewDto(List<CusReViewDto> input)
        {
            return GetResult<List<CusReViewDto>, List<CusReViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void DelCusReViewDto(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<TjlCustomerSummarizeDto> GetCustomerSummary(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<TjlCustomerSummarizeDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 查询下一位已完成的体检人
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetReCustomerReg(TjlCustomerQuery input)
        {
            return GetResult<TjlCustomerQuery, List<TjlCustomerRegForInspectionTotalDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 添加屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TbmSumHideDto AddSumHide(TbmSumHideDto input)
        {
            return GetResult<TbmSumHideDto, TbmSumHideDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 修改屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TbmSumHideDto EditSumHide(TbmSumHideDto input)
        {
            return GetResult<TbmSumHideDto, TbmSumHideDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 删除屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void delSumHide(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 查询屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<TbmSumHideDto> SearchSumHide()
        {
            return GetResult<List<TbmSumHideDto>>(DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 根据体检人危害因素获取职业健康等
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccTargetDiseaseByRisk GetOccSumByCusReg(EntityDto<Guid> input)
        {
            return GetResult< EntityDto<Guid>, OccTargetDiseaseByRisk > (input,DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 保存职业健康总检
        /// </summary>
        /// <param name="inputOccCusSumDto"></param>
        /// <returns></returns>
        public InputOccCusSumDto SaveOccSum(InputOccCusSumDto inputOccCusSumDto)
        {
            return GetResult<InputOccCusSumDto, InputOccCusSumDto>(inputOccCusSumDto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据体检人获取职业健康总检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
      public InputOccCusSumDto GetCusOccSumByRegId(EntityDto<Guid> input)
        {
           return GetResult<EntityDto<Guid>, InputOccCusSumDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取总检列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutCusListDto> GetOutCus(InSearchCusDto input)
        {
            return GetResult<InSearchCusDto, List<OutCusListDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取已总检人员合格不合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<JKZCusDto> GetJKZOutCus(InSearchCusDto input)
        {
            return GetResult<InSearchCusDto, List<JKZCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<JKZCusDto> GetJKZOutCuslist(InSearchCusDto input)
        {
            return GetResult<InSearchCusDto, List<JKZCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());


        }
        /// <summary>
        /// 判断健康证是否合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ISHG(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, bool>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取体检人组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CustomerGroupSumDto> GetCustomerRegGroupSum(ChargeBM input)
        {
            return GetResult<EntityDto<Guid>, List<CustomerGroupSumDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取体检人基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public BeforSumCustomerRegDto GetCustomerReg(ChargeBM input)
        {
            return GetResult<ChargeBM, BeforSumCustomerRegDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 保存初审
        /// </summary>
        /// <param name="input"></param>
        public void SavePerSum(BeforSaveSumDto input)
        {
              GetResult<BeforSaveSumDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 匹配冲突
        /// </summary>
        public OutMesDto MatchSumConflict(SumAdviseDto input)
        {
           return GetResult<SumAdviseDto,OutMesDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取组合
        /// </summary>
        /// <param name="regID"></param>
        /// <returns></returns>
        public List<CusGroupShowDto> getCusGroup(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CusGroupShowDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void DelOccSum(TjlCustomerQuery query)
        {
             GetResult<TjlCustomerQuery>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 报告获取复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<reportCusReViewDto> GetCusReView(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<reportCusReViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public reportCusReSumDto GetCusReSum(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, reportCusReSumDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}