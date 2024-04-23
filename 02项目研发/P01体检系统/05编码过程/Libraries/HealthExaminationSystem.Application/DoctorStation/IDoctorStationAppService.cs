using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
#if !Proxy
#endif
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation
{
    public interface IDoctorStationAppService
#if !Proxy
        : IApplicationService
#endif
    {
        #region 体检人登记信息
        /// <summary>
        /// 获取所有组合小结
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<CustomerItemGroupPrintViewDto> GetCustomerItemGroupPrintViewDtos(QueryClass query);
        /// <summary>
        /// 获取科室小结
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<CustomerDepSummaryViewDto> GetCustomerDepSummaries(QueryClass query);
        /// <summary>
        /// 获取报告项目结果
        /// </summary>
        /// <param name="query"></param>
        /// <returns>改体检人所有项目结果</returns>
        List<TjlCustomerRegItemReoprtDto> GetTjlCustomerRegItemReoprtDtos(QueryClass query);
        /// <summary>
        /// 包含放弃项目
        /// </summary>
        List<TjlCustomerRegItemReoprtDto> GetTjlCustomerRegAllItemReoprtDtos(QueryClass query);
        /// <summary>
        /// 查询体检人图片
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<CustomerItemPicDto> GetCustomerItemPicDtos(QueryClass query);
        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        List<ATjlCustomerRegDto> GetCustomerRegList(QueryClass query);
        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<TjlCustomerRegForInspectionTotalDto> GetCustomerRegListForSearch(QueryClass Query);
        /// <summary>
        /// 查询体检人体检项目
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<ATjlCustomerItemGroupDto> GetCustomerItemGroup(QueryClass Query);
        /// <summary>
        /// 查询体检人体检项目（包含未收费）
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<ATjlCustomerItemGroupDto> GetCustomerAllItemGroup(QueryClass Query);
        /// <summary>
        /// 根据体检号获取组合信息
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
         List<ATjlCustomerItemGroupDto> GetCustomerItemGroupByBm(QueryClass Query);
        /// <summary>
        /// 获取人员体检记录
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<TjlCustomerRegForInspectionTotalDto> GetTjlCustomerRegDto(QueryClassTwo Query);
        /// <summary>
        /// 返回当前科室未体检的人员
        /// </summary>
        /// <param name="input">查询类（可通用）</param>
        /// <returns></returns>
        List<TjlCustomerRegForDoctorDto> GetSameDayCusTomerReg(QueryClassTwo input);
        /// <summary>
        /// 获取未体检完成的含有危急值的患者列表
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<TjlCustomerRegForInspectionTotalDto> GetCriticalValue(QueryClassTwo Query);
        /// <summary>
        /// 获取两次原复查信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<CustomerRegSimpleViewDto> GetTjlCustomerRegRevew(QueryClass query);
        /// <summary>
        /// 获取未体检完成的含有危急值的患者列表
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<CriticalCusDto> GetCriticalCusList(QueryClassTwo Query);
        /// <summary>
        /// 查询体检人体检项目包含科室信息和组合信息不包含结果信息
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<ATjlCustomerItemGroupPrintGuidanceDto> GetATjlCustomerItemGroupPrintGuidanceDto(QueryClass Query);
        /// <summary>
        /// 判断是否锁定
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        CusLockDto JudgeLocking(QueryClass Query);
        /// <summary>
        /// 根据条码号查询体检号
        /// </summary>
        /// <param name="query">条码号</param>
        /// <returns></returns>
        string GetCusBarPrintInfo(QueryClass query);
        /// <summary>
        /// 科室人员查询
        /// </summary>
        /// <param name="Search"></param>
        /// <returns></returns>
        List<TjlCustomerRegForInspectionTotalDto> GetDeparmentCustomerRegs(DeparmentCustomerSearch Search);
        #endregion
        #region 保存信息
        /// <summary>
        /// 体检人组合项目状态修改
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        ATjlCustomerItemGroupDto GiveUpCheckItemGroup(UpdateClass Update);
        /// <summary>
        /// 单独项目修改状态并返回组合信息
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        ATjlCustomerItemGroupDto GiveUpCheckItem(UpdateClass Update);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="Input">组合集合</param>
        /// <returns></returns>
        bool UpdateInspectionProject(List<UpdateCustomerItemGroupDto> Input);
        /// <summary>
        /// 生成小结保存信息
        /// </summary>
        /// <param name="Input">项目组合信息</param>
        bool UpdateSectionSummary(List<ATjlCustomerItemGroupDto> Input);
        /// <summary>
        /// 保存科室小结
        /// </summary>
        /// <param name="Input">科室小结</param>
        /// <returns></returns>
        bool InsertCustomerDepSummary(CreateCustomerDepSummary Input);
        /// <summary>
        /// 获取科室小结
        /// </summary>
        /// <param name="Input">科室小结</param>
        /// <returns></returns>
        List<ATjlCustomerDepSummaryDto> GetCustomerDepSummary(QueryClass Input);
        ///// <summary>
        ///// 保存专科建议
        ///// </summary>
        ///// <param name="Input">专科建议</param>
        ///// <returns></returns>
        //bool InsertCustomerSummary(SearchTjlCustomerSummaryDto Input);
        ///// <summary>
        ///// 保存专科建议
        ///// </summary>
        ///// <param name="Input">专科建议</param>
        ///// <returns></returns>
        //List<SearchTjlCustomerSummaryDto> GetCustomerSummary(QueryClass Input);
        /// <summary>
        /// 修改小结
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        bool UpdateCustomerDepSummary(UpdateClass Update);
        /// <summary>
        /// 清除当前用户科室下当前人员体检信息
        /// </summary>
        /// <returns></returns>
        bool DeleteCustomerInspectInformation(QueryClassTwo Query);
        /// <summary>
        /// 生成科室小结
        /// </summary>
        /// <param name="Create">体检人信息，体检人项目，科室信息</param>
        bool CreateConclusion(CreateConclusionDto Create);
        #endregion
        #region 获取字典
        /// <summary>
        /// 获取所有医生
        /// </summary>
        /// <returns></returns>
        [Obsolete("暂停使用", true)]
        List<UserViewDto> GetUserViewDto();
        /// <summary>
        /// 查询登陆科室项目字典
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        List<BTbmItemDictionaryDto> GetItemDictionarylist(QueryClassTwo query);

        /// <summary>
        /// 获取所有项目结果参考
        /// </summary>
        /// <returns></returns>
        List<SearchItemStandardDto> GetGenerateSummary();
        /// <summary>
        /// 获取小结格式和项目信息
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<BasicDictionaryDto> GetDictionaryDto(QueryClass Query);
        /// <summary>
        /// 查询科室项目
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        List<BTbmItemInfoDto> GetItemInfo(QueryClass Query);
        /// <summary>
        /// 查询科室健康建议
        /// </summary>
        /// <param name="input">查询类（可通用）</param>
        /// <returns></returns>
        List<SearchTbmSummarizeAdviceDto> GetSummarizeAdvice(QueryClassTwo input);
        #endregion
        #region 检查项目统计
        /// <summary>
        /// 获取当前登录人科室人数统计
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        ReturnClass GetReturnClass(QueryClassTwo Query);
        #endregion
        #region 其他人用的
        /// <summary>
        ///套餐统计查询 
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        List<SearchItemSuitStatisticsDto> GetItemSuitStatistics(QueryClass Query);
        /// <summary>
        /// 科室工作量项目细目统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<SearchKSGZLItemStatisticsDto> KSGZLItemStatistics(QueryClass query);
        /// <summary>
        /// 科室工作量统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<SearchKSGZLStatisticsDto> KSGZLStatistics(QueryClass query);
        /// <summary>
        /// 大科室工作量
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        List<SearchKSGZLStatisticsDto> BKSGZLStatistics(QueryClass query);
        /// <summary>
        /// 大科室工作量统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<SearchKSGZLStatisticsDto> DKSGZLStatistics(QueryClass query);
        /// <summary>
        /// 科室工作量统计（图形）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        DataTable KSGZLSTStatistics(HBQueryClass query);
        /// <summary>
        /// 获取检查项目统计
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        List<ATjlCustomerItemGroupViewDto> GettheCheckItemStatistics(StatisticalClass Query);

        /// <summary>
        /// 已检、放弃、待查、未检统计
        /// </summary>
        /// <returns></returns>
        List<PatientExaminationProjectStatisticsViewDto> GetATjlCustomerItemGrouplist(PatientExaminationCondition Condition);

        /// <summary>
        /// 科室环比统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<KSHBGZLStatisticsDto> KSHBGZLStatistics(HBQueryClass query);

        /// <summary>
        /// 科室压力
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<SearchKSYLStatisticsDto> KSYLStatistics(QueryClass query);
        #endregion
        #region 调用接口
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="tdbInterfaceWhere"></param>
        /// <returns></returns>
        InterfaceBack ConvertInterface(TdbInterfaceDocWhere tdbInterfaceWhere);

        #endregion
        /// <summary>
        /// 重置患者状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        bool ResetCustomerChecked(QueryClass query);
        /// <summary>
        /// 获取所有单位信息
        /// </summary>
        /// <returns></returns>
        List<SearchClientInfoDto> GetSearchClientInfoDto(DeparmentCustomerSearch Search);
        /// <summary>
        /// 修改危急值状态
        /// </summary>
        /// <param name="updateClass"></param>
        /// <returns></returns>
        OutStateDto UpdateCrisisSate(UpdateClass updateClass);
        /// <summary>
        /// 修改项目阳性状态
        /// </summary>
        /// <param name="updateClass"></param>
        /// <returns></returns>
        OutStateDto UpdateIllState(UpCusIllStateDto updateClass);

        /// <summary>
        /// 取消阳性状态
        /// </summary>
        /// <param name="updateClass"></param>
        /// <returns></returns>
        OutStateDto CancelIllState(UpCusIllStateDto updateClass);
        /// <summary>
        /// 批量单个项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ATjlCustomerRegDto BatchInsertDodyFat(QueryClass query);
        /// <summary>
        /// 计算体脂
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ATjlCustomerRegDto CalculationConstitution(QueryClass query);
        /// <summary>
        /// 保存问卷 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool SaveCustomerQuestion(List<SaveCustomerQusTionDto> input);
        /// <summary>
        /// 所有计算型公式
        /// </summary>
        /// <returns></returns>
        List<ItemProcExpressDto> getItemProcExpress();
        /// <summary>
        /// 修改体检状态
        /// </summary>
        /// <param name="input"></param>
        void UpCheckState(EntityDto<Guid> input);
        /// <summary>
        /// 查询检查项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CheckCusRegGroupDto> getCheckCusGroup(SearchCusGroupDto input);
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="input"></param>
        void SaveItemPic(List<CustomerItemPicDto> input);
        /// <summary>
        /// 生成未小结科室小结
        /// </summary>
        /// <param name="regid"></param>
        /// <returns></returns>
        bool CreateAllNoConclusion(EntityDto<Guid> regid);
    }
}