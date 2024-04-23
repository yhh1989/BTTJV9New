using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation
{
    public class DoctorStationAppService : AppServiceApiProxyBase, IDoctorStationAppService
    {
        #region 体检人登记信息
        /// <summary>
        /// 获取科室小结
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerDepSummaryViewDto> GetCustomerDepSummaries(QueryClass query)
        {
            return GetResult<QueryClass, List<CustomerDepSummaryViewDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerItemGroupPrintViewDto> GetCustomerItemGroupPrintViewDtos(QueryClass query)
        {
            return GetResult<QueryClass, List<CustomerItemGroupPrintViewDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerItemPicDto> GetCustomerItemPicDtos(QueryClass query)
        {
            return GetResult<QueryClass, List<CustomerItemPicDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TjlCustomerRegItemReoprtDto> GetTjlCustomerRegItemReoprtDtos(QueryClass query)
        {
            return GetResult<QueryClass, List<TjlCustomerRegItemReoprtDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TjlCustomerRegItemReoprtDto> GetTjlCustomerRegAllItemReoprtDtos(QueryClass query)
        {
            return GetResult<QueryClass, List<TjlCustomerRegItemReoprtDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerRegDto> GetCustomerRegList(QueryClass query)
        {
            return GetResult<QueryClass, List<ATjlCustomerRegDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetCustomerRegListForSearch(QueryClass Query)
        {
            return GetResult<QueryClass, List<TjlCustomerRegForInspectionTotalDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询体检人体检项目
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupDto> GetCustomerItemGroup(QueryClass Query)
        {
            return GetResult<QueryClass, List<ATjlCustomerItemGroupDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询体检人体检项目（包含未收费）
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupDto> GetCustomerAllItemGroup(QueryClass Query)
        {
            return GetResult<QueryClass, List<ATjlCustomerItemGroupDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ATjlCustomerItemGroupDto> GetCustomerItemGroupByBm(QueryClass Query)
        {
            return GetResult<QueryClass, List<ATjlCustomerItemGroupDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
            /// <summary>
            /// 查询体检人体检项目包含科室信息和组合信息不包含结果信息
            /// </summary>
            /// <param name="Query">查询类（可通用）</param>
            /// <returns></returns>
            public List<ATjlCustomerItemGroupPrintGuidanceDto> GetATjlCustomerItemGroupPrintGuidanceDto(QueryClass Query)
        {
            return GetResult<QueryClass, List<ATjlCustomerItemGroupPrintGuidanceDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 获取人员体检记录
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetTjlCustomerRegDto(QueryClassTwo Query)
        {
            return GetResult<QueryClassTwo, List<TjlCustomerRegForInspectionTotalDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 返回当前科室未体检的人员
        /// </summary>
        /// <param name="input">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForDoctorDto> GetSameDayCusTomerReg(QueryClassTwo input)
        {
            return GetResult<QueryClassTwo, List<TjlCustomerRegForDoctorDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取未体检完成的含有危急值的患者列表
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetCriticalValue(QueryClassTwo Query)
        {
            return GetResult<QueryClassTwo, List<TjlCustomerRegForInspectionTotalDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取两次原复查信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerRegSimpleViewDto> GetTjlCustomerRegRevew(QueryClass query)
        {
            return GetResult<QueryClass, List<CustomerRegSimpleViewDto>>(query, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取未体检完成的含有危急值的患者列表
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<CriticalCusDto> GetCriticalCusList(QueryClassTwo Query)
        {
            return GetResult<QueryClassTwo, List<CriticalCusDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 判断是否锁定
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns> 
        public CusLockDto JudgeLocking(QueryClass Query)
        {
            return GetResult<QueryClass, CusLockDto>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        #endregion
        #region 保存信息
        /// <summary>
        /// 体检人组合项目状态修改
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        public ATjlCustomerItemGroupDto GiveUpCheckItemGroup(UpdateClass Update)
        {
            return GetResult<UpdateClass, ATjlCustomerItemGroupDto>(Update, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 单独项目修改状态并返回组合信息
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        public ATjlCustomerItemGroupDto GiveUpCheckItem(UpdateClass Update)
        {
            return GetResult<UpdateClass, ATjlCustomerItemGroupDto>(Update, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="Input">组合集合</param>
        /// <returns></returns>
        public bool UpdateInspectionProject(List<UpdateCustomerItemGroupDto> Input)
        {
            return GetResult<List<UpdateCustomerItemGroupDto>, bool>(Input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 生成小结保存信息
        /// </summary>
        /// <param name="Input">项目组合信息</param>
        public bool UpdateSectionSummary(List<ATjlCustomerItemGroupDto> Input)
        {
            return GetResult<List<ATjlCustomerItemGroupDto>, bool>(Input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 保存科室小结
        /// </summary>
        /// <param name="Input">科室小结</param>
        /// <returns></returns>
        public bool InsertCustomerDepSummary(CreateCustomerDepSummary Input)
        {
            return GetResult<CreateCustomerDepSummary, bool>(Input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取科室小结
        /// </summary>
        /// <param name="Input">科室小结</param>
        /// <returns></returns>
        public List<ATjlCustomerDepSummaryDto> GetCustomerDepSummary(QueryClass Input)
        {
            return GetResult<QueryClass, List<ATjlCustomerDepSummaryDto>>(Input, DynamicUriBuilder.GetAppSettingValue());
        }
        ///// <summary>
        ///// 保存专科建议
        ///// </summary>
        ///// <param name="Input">专科建议</param>
        ///// <returns></returns>
        //public bool InsertCustomerSummary(SearchTjlCustomerSummaryDto Input)
        //{
        //    return GetResult<SearchTjlCustomerSummaryDto, bool>(Input, DynamicUriBuilder.GetAppSettingValue());
        //}
        ///// <summary>
        ///// 保存专科建议
        ///// </summary>
        ///// <param name="Input">专科建议</param>
        ///// <returns></returns>
        //public List<SearchTjlCustomerSummaryDto> GetCustomerSummary(QueryClass Input)
        //{
        //    return GetResult<QueryClass, List<SearchTjlCustomerSummaryDto>>(Input, DynamicUriBuilder.GetAppSettingValue());
        //}
        /// 修改小结
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        public bool UpdateCustomerDepSummary(UpdateClass Update)
        {
            return GetResult<UpdateClass, bool>(Update, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 清除当前用户科室下当前人员体检信息
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public bool DeleteCustomerInspectInformation(QueryClassTwo Query)
        {
            return GetResult<QueryClassTwo, bool>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 生成科室小结
        /// </summary>
        /// <param name="Create">体检人信息，体检人项目，科室信息</param>
        public bool CreateConclusion(CreateConclusionDto Create)
        {
            return GetResult<CreateConclusionDto, bool>(Create, DynamicUriBuilder.GetAppSettingValue());
        }
        #endregion
        #region 获取字典
        /// <summary>
        /// 获取所有医生
        /// </summary>
        /// <returns></returns>
        public List<UserViewDto> GetUserViewDto()
        {
            return GetResult<List<UserViewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询登陆科室项目字典
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public List<BTbmItemDictionaryDto> GetItemDictionarylist(QueryClassTwo query)
        {
            return GetResult<QueryClassTwo, List<BTbmItemDictionaryDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 获取所有项目结果参考
        /// </summary>
        /// <returns></returns>
        public List<SearchItemStandardDto> GetGenerateSummary()
        {
            return GetResult<List<SearchItemStandardDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取所有项目结果参考
        /// </summary>
        /// <returns></returns>
        public List<BasicDictionaryDto> GetDictionaryDto(QueryClass Query)
        {
            return GetResult<QueryClass, List<BasicDictionaryDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询科室项目
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<BTbmItemInfoDto> GetItemInfo(QueryClass Query)
        {
            return GetResult<QueryClass, List<BTbmItemInfoDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询科室健康建议
        /// </summary>
        /// <param name="input">查询类（可通用）</param>
        /// <returns></returns>
        public List<SearchTbmSummarizeAdviceDto> GetSummarizeAdvice(QueryClassTwo input)
        {
            return GetResult<QueryClassTwo, List<SearchTbmSummarizeAdviceDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        #endregion
        #region 检查项目统计
        /// <summary>
        /// 获取当前登录人科室人数统计
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public ReturnClass GetReturnClass(QueryClassTwo Query)
        {
            return GetResult<QueryClassTwo, ReturnClass>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        #endregion
        #region 其他人用的
        /// <summary>
        /// 套餐统计查询
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<SearchItemSuitStatisticsDto> GetItemSuitStatistics(QueryClass Query)
        {
            return GetResult<QueryClass, List<SearchItemSuitStatisticsDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 科室工作量项目细目统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SearchKSGZLItemStatisticsDto> KSGZLItemStatistics(QueryClass query)
        {
            return GetResult<QueryClass, List<SearchKSGZLItemStatisticsDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 检查项目统计
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupViewDto> GettheCheckItemStatistics(StatisticalClass Query)
        {
            return GetResult<StatisticalClass, List<ATjlCustomerItemGroupViewDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 已检、放弃、待查、未检统计
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<PatientExaminationProjectStatisticsViewDto> GetATjlCustomerItemGrouplist(PatientExaminationCondition Condition)
        {
            return GetResult<PatientExaminationCondition, List<PatientExaminationProjectStatisticsViewDto>>(Condition, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 科室工作量统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SearchKSGZLStatisticsDto> KSGZLStatistics(QueryClass query)
        {
            return GetResult<QueryClass, List<SearchKSGZLStatisticsDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SearchKSGZLStatisticsDto> BKSGZLStatistics(QueryClass query)
        {
            return GetResult<QueryClass, List<SearchKSGZLStatisticsDto>>(query, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<SearchKSGZLStatisticsDto> DKSGZLStatistics(QueryClass query)
        {
            return GetResult<QueryClass, List<SearchKSGZLStatisticsDto>>(query, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 科室环比工作量统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<KSHBGZLStatisticsDto> KSHBGZLStatistics(HBQueryClass query)
        {
            return GetResult<HBQueryClass, List<KSHBGZLStatisticsDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 科室压力
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SearchKSYLStatisticsDto> KSYLStatistics(QueryClass query)
        {
            return GetResult<QueryClass, List<SearchKSYLStatisticsDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public DataTable KSGZLSTStatistics(HBQueryClass query)
        {
            return GetResult<HBQueryClass, DataTable>(query, DynamicUriBuilder.GetAppSettingValue());
        }

        public InterfaceBack ConvertInterface(TdbInterfaceDocWhere tdbInterfaceWhere)
        {
            return GetResult<TdbInterfaceDocWhere, InterfaceBack>(tdbInterfaceWhere, DynamicUriBuilder.GetAppSettingValue());
        }

        public InterfaceCustomerRegDto GetInterfaceCustomerReg(TdbInterfaceDocWhere tdbInterfaceWhere)
        {
            return GetResult<TdbInterfaceDocWhere, InterfaceCustomerRegDto>(tdbInterfaceWhere, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 根据条码号查询体检号
        /// </summary>
        /// <param name="query">条码号</param>
        /// <returns></returns>
        public string GetCusBarPrintInfo(QueryClass query)
        {
            return GetResult<QueryClass, string>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改危急值状态
        /// </summary>
        /// <param name="updateClass"></param>
        /// <returns></returns>
        public OutStateDto UpdateCrisisSate(UpdateClass updateClass)
        {
            return GetResult<UpdateClass, OutStateDto>(updateClass, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutStateDto UpdateIllState(UpCusIllStateDto updateClass)
        {
            return GetResult<UpCusIllStateDto, OutStateDto>(updateClass, DynamicUriBuilder.GetAppSettingValue());

        }
        public OutStateDto CancelIllState(UpCusIllStateDto updateClass)
        {
            return GetResult<UpCusIllStateDto, OutStateDto>(updateClass, DynamicUriBuilder.GetAppSettingValue());

        }
        #endregion
        /// <summary>
        /// 重置患者状态 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool ResetCustomerChecked(QueryClass query)
        {
            return GetResult<QueryClass, bool>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取所有单位信息
        /// </summary>
        /// <returns></returns>
        public List<SearchClientInfoDto> GetSearchClientInfoDto(DeparmentCustomerSearch Search)
        {
            return GetResult<DeparmentCustomerSearch, List<SearchClientInfoDto>>(Search, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 科室人员查询
        /// </summary>
        /// <param name="Search"></param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetDeparmentCustomerRegs(DeparmentCustomerSearch Search)
        {
            return GetResult<DeparmentCustomerSearch, List<TjlCustomerRegForInspectionTotalDto>>(Search, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 批量单个项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ATjlCustomerRegDto BatchInsertDodyFat(QueryClass query)
        {
            return GetResult<QueryClass, ATjlCustomerRegDto>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 批量单个项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ATjlCustomerRegDto CalculationConstitution(QueryClass query)
        {
            return GetResult<QueryClass, ATjlCustomerRegDto>(query, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 保存问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool SaveCustomerQuestion(List<SaveCustomerQusTionDto> input)
        {
            return GetResult<List<SaveCustomerQusTionDto>, bool>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 所有计算型公式
        /// </summary>
        /// <returns></returns>
        public List<ItemProcExpressDto> getItemProcExpress()
        {
            return GetResult<List<ItemProcExpressDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpCheckState(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input,DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CheckCusRegGroupDto> getCheckCusGroup(SearchCusGroupDto input)
        {
            return GetResult<SearchCusGroupDto, List<CheckCusRegGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void SaveItemPic(List<CustomerItemPicDto> input)
        {
             GetResult<List<CustomerItemPicDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 生成未小结科室小结
        /// </summary>
        /// <param name="regid"></param>
        /// <returns></returns>
        public bool CreateAllNoConclusion(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, bool>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}