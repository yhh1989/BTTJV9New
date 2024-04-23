using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Core.Test;
using Sw.Hospital.HealthExaminationSystem.Core.Crisis;
using System;
using Sw.Hospital.HealthExaminationSystem.Core.DynamicColumnDirectory;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
using Sw.Hospital.HealthExaminationSystem.Core.Interfase;
using Sw.Hospital.HealthExaminationSystem.Core.Market;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework
{ 
    public class MyProjectDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public MyProjectDbContext()
            : base("HealthExaminationSystem")
        {
            UnifyConfiguration();
        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MyProjectDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MyProjectDbContext since ABP automatically handles it.
         */
        public MyProjectDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            UnifyConfiguration();
        }

        //This constructor is used in tests
        public MyProjectDbContext(DbConnection existingConnection)
            : base(existingConnection, false)
        {
            UnifyConfiguration();
        }

        public MyProjectDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            UnifyConfiguration();
        }

        /// <summary>
        /// 窗体角色表
        /// </summary>
        public virtual DbSet<FormRole> FormRoles { get; set; }

        /// <summary>
        /// 窗体模块表
        /// </summary>
        public virtual DbSet<FormModule> FormModules { get; set; }

        /// <summary>
        /// 项目设置表
        /// </summary>
        public virtual DbSet<TbmItemInfo> TbmItemInfos { get; set; }

        /// <summary>
        /// 项目组合设置表
        /// </summary>
        public virtual DbSet<TbmItemGroup> TbmItemGroups { get; set; }

        /// <summary>
        /// 科室设置表
        /// </summary>
        public virtual DbSet<TbmDepartment> TbmDepartments { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual DbSet<TjlClientInfo> TjlClientInfos { get; set; }

        /// <summary>
        /// 单位预约登记
        /// </summary>
        public virtual DbSet<TjlClientReg> TjlClientRegs { get; set; }

        /// <summary>
        /// 单位分组信息
        /// </summary>
        public virtual DbSet<TjlClientTeamInfo> TjlClientTeamInfos { get; set; }
        /// <summary>
        /// 体检人危害因素表
        /// </summary>
        public virtual DbSet<TjlCustomerRegRiskFactors> TjlCustomerRegRiskFactors { get; set; }

        /// <summary>
        /// 体检人复查项目
        /// </summary>
        public virtual DbSet<TjlCustomerReviewItemGroup> TjlCustomerReviewItemGroup { get; set; }

        /// <summary>
        /// 体检人复查跟踪表
        /// </summary>
        public virtual DbSet<TjlCustomerReviewFollow> TjlCustomerReviewFollow { get; set; }
        /// <summary>
        /// 单位分组登记项目
        /// </summary>
        public virtual DbSet<TjlClientTeamRegitem> TjlClientTeamRegitems { get; set; }

        /// <summary>
        /// 套餐设置
        /// </summary>
        public virtual DbSet<TbmItemSuit> TbmItemSuits { get; set; }

        /// <summary>
        /// 体检人信息表
        /// </summary>
        public virtual DbSet<TjlCustomer> TjlCustomers { get; set; }

        /// <summary>
        /// 体检人预约登记信息表
        /// </summary>
        public virtual DbSet<TjlCustomerReg> TjlCustomerRegs { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual DbSet<TjlCustomerItemGroup> TjlCustomerItemGroups { get; set; }

        /// <summary>
        /// 项目参考值设置
        /// </summary>
        public virtual DbSet<TbmItemStandard> TbmItemStandards { get; set; }

        /// <summary>
        /// 工种车间
        /// </summary>
        [Obsolete("暂停使用", true)]
        public virtual DbSet<TbmOWorkType> TbmOWorkTypes { get; set; }

        /// <summary>
        /// 复合判断设置
        /// </summary>
        public virtual DbSet<TbmDiagnosis> TbmDiagnoses { get; set; }

        /// <summary>
        /// 复合判断设置明细
        /// </summary>
        public virtual DbSet<TbmDiagnosisData> TbmDiagnosisDatas { get; set; }

        /// <summary>
        /// 基本字典
        /// </summary>
        public virtual DbSet<TbmBasicDictionary> TbmBasicDictionarys { get; set; }

        /// <summary>
        /// 条码设置表
        /// </summary>
        public virtual DbSet<TbmBarSettings> TbmBarSettingses { get; set; }

        /// <summary>
        /// 条码设置项目表
        /// </summary>
        public virtual DbSet<TbmBaritem> TbmBaritems { get; set; }

        /// <summary>
        /// 耗材管理
        /// </summary>
        public virtual DbSet<TbmConsumables> TbmConsumableses { get; set; }

        /// <summary>
        /// 总检建议设置
        /// </summary>
        public virtual DbSet<TbmSummarizeAdvice> TbmSummarizeAdvices { get; set; }

        /// <summary>
        /// 复查项目设置
        /// </summary>
        public virtual DbSet<TbmReviewItemSet> TbmReviewItemSets { get; set; }

        /// <summary>
        /// 发票管理
        /// </summary>
        public virtual DbSet<TbmMReceiptManager> TbmMReceiptManagers { get; set; }

        /// <summary>
        /// 支付方式类别
        /// </summary>
        public virtual DbSet<TbmMChargeType> TbmMChargeTypes { get; set; }

        /// <summary>
        /// 项目字典
        /// </summary>
        public virtual DbSet<TbmItemDictionary> TbmItemDictionaries { get; set; }

        ///// <summary>
        ///// 体检人专科建议
        ///// </summary>
        //public virtual DbSet<TjlCustomerSummary> TjlCustomerSummaries { get; set; }

        /// <summary>
        /// 体检总检建议BM
        /// </summary>
        public virtual DbSet<TjlCustomerSummarizeBM> TjlCustomerSummarizeBms { get; set; }

        /// <summary>
        /// 体检人总检结论表
        /// </summary>
        public virtual DbSet<TjlCustomerSummarize> TjlCustomerSummarizes { get; set; }

        /// <summary>
        /// 复查人员
        /// </summary>
        public virtual DbSet<TjlCustomerReview> TjlCustomerReviews { get; set; }

        /// <summary>
        /// 报告记录表
        /// </summary>
        public virtual DbSet<TjlCustomerReportPrintInfo> TjlCustomerReportPrintInfos { get; set; }

        /// <summary>
        /// 报告领取
        /// </summary>
        public virtual DbSet<TjlCustomerReportCollection> TjlCustomerReportCollections { get; set; }

        /// <summary>
        /// 体检人检查项目结果表
        /// </summary>
        public virtual DbSet<TjlCustomerRegItem> TjlCustomerRegItems { get; set; }

        /// <summary>
        /// 复查操作
        /// </summary>
        public virtual DbSet<TjlCustomerDoCwOrk> TjlCustomerDoCwOrks { get; set; }

        /// <summary>
        /// 体检人科室小结
        /// </summary>
        public virtual DbSet<TjlCustomerDepSummary> TjlCustomerDepSummaries { get; set; }

        /// <summary>
        /// 体检会诊协作
        /// </summary>
        public virtual DbSet<TjlCustomerDepCooperate> TjlCustomerDepCooperates { get; set; }

        /// <summary>
        /// 抽血记录
        /// </summary>
        public virtual DbSet<TjlCustomerBloodNum> TjlCustomerBloodNums { get; set; }

        /// <summary>
        /// 条码打印记录
        /// </summary>
        public virtual DbSet<TjlCustomerBarPrintInfoItemGroup> TjlCustomerBarPrintInfoItemGroups { get; set; }

        /// <summary>
        /// 条码打印记录
        /// </summary>
        public virtual DbSet<TjlCustomerBarPrintInfo> TjlCustomerBarPrintInfos { get; set; }

        /// <summary>
        /// 职业健康问诊
        /// </summary>
        public virtual DbSet<TjlOMedicalHistoryInfo> TjlOMedicalHistoryInfos { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public virtual DbSet<TjlOHistory> TjlOHistories { get; set; }

        /// <summary>
        /// 工作历史
        /// </summary>
        public virtual DbSet<TjlOAHistory> TjlOaHistories { get; set; }

        /// <summary>
        /// 症状字典
        /// </summary>
        [Obsolete("暂停使用", true)]
        public virtual DbSet<TbmOSymPTom> TbmOSymPToms { get; set; }

        /// <summary>
        /// 危害因素类别
        /// </summary>
        [Obsolete("暂停使用", true)]
        public virtual DbSet<TbmORiskFactorType> TbmORiskFactorTypes { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual DbSet<TbmORiskFactor> TbmORiskFactors { get; set; }

        /// <summary>
        /// 职业结论字典
        /// </summary>
        [Obsolete("暂停使用", true)]
        public virtual DbSet<TbmOProposal> TbmOProposals { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [Obsolete("暂停使用", true)]
        public virtual DbSet<TbmOPostState> TbmOPostStates { get; set; }

        /// <summary>
        /// 职业总检结论字典
        /// </summary>
        public virtual DbSet<TbmOConDictionary> TbmOConDictionaries { get; set; }

        /// <summary>
        /// 发票抬头
        /// </summary>
        public virtual DbSet<TjlMRise> TjlMRises { get; set; }

        ///// <summary>
        ///// 结算明细表
        ///// </summary>
        //public virtual DbSet<TjlMReceiptInfoDetailed> TjlMReceiptInfoDetaileds { get; set; }

        /// <summary>
        /// 结算表
        /// </summary>
        public virtual DbSet<TjlMReceiptInfo> TjlMReceiptInfos { get; set; }

        /// <summary>
        /// 结算支付方式记录表
        /// </summary>
        public virtual DbSet<TjlMPayment> TjlMPayments { get; set; }

        /// <summary>
        /// 结算发票记录表
        /// </summary>
        public virtual DbSet<TjlMInvoiceRecord> TjlMInvoiceRecords { get; set; }

        /// <summary>
        /// 日结表
        /// </summary>
        public virtual DbSet<TjlMDiurnalTable> TjlMDiurnalTables { get; set; }

        /// <summary>
        /// 照片存储表
        /// </summary>
        public virtual DbSet<Picture> Pictures { get; set; }

        /// <summary>
        /// 单位组单记录表
        /// </summary>
        public virtual DbSet<TjlClientCustomItemSuit> TjlClientCustomItemSuits { get; set; }

        /// <summary>
        /// 套餐与项目组合关联表
        /// </summary>
        public virtual DbSet<TbmItemSuitItemGroupContrast> TbmItemSuitItemGroupContrasts { get; set; }

        /// <summary>
        /// ID生成控制表
        /// </summary>
        public virtual DbSet<TdbIdNumber> TdbIdNumbers { get; set; }

        /// <summary>
        /// 系统参数表
        /// </summary>
        public virtual DbSet<TdbSetSysSetting> TdbSetSysSettings { get; set; }

        /// <summary>
        /// 排期记录表
        /// </summary>
        public virtual DbSet<TjlScheduling> TjlSchedulings { get; set; }

        /// <summary>
        /// 放弃待查
        /// </summary>
        public virtual DbSet<TjlCusGiveUp> TjlCusGiveUps { get; set; }

        /// <summary>
        /// 个人应收已收
        /// </summary>
        public virtual DbSet<TjlMcusPayMoney> TjlMcusPayMonies { get; set; }

        /// <summary>
        /// 调项金额表
        /// </summary>
        public virtual DbSet<TjlAdjustMoney> TjlAdjustMonies { get; set; }

        /// <summary>
        /// 组单分组
        /// </summary>
        public virtual DbSet<TbmComposeGroup> TbmComposeGroups { get; set; }

        /// <summary>
        /// 组单表
        /// </summary>
        public virtual DbSet<TbmCompose> TbmComposes { get; set; }

        /// <summary>
        /// 检查项目图片
        /// </summary>
        public virtual DbSet<TjlCustomerItemPic> TjlCustomerItemPics { get; set; }

        /// <summary>
        /// 结算明细表
        /// </summary>
        public virtual DbSet<TjlMReceiptDetails> TjlMReceiptDetailses { get; set; }

        /// <summary>
        /// 单位储值表
        /// </summary>
        public virtual DbSet<TjlMClientStoreds> TjlMClientStoredses { get; set; }

        /// <summary>
        /// 总检退回
        /// </summary>
        public virtual DbSet<TjlCustomerSummBack> TjlCustomerSummBacks { get; set; }

        /// <summary>
        /// 组单分组项目（组合）表
        /// </summary>
        public virtual DbSet<TbmComposeGroupItem> TbmComposeGroupItems { get; set; }

        /// <summary>
        /// 测试表1
        /// </summary>
        public virtual DbSet<TestTable1> TestTable1s { get; set; }

        /// <summary>
        /// 测试表2
        /// </summary>
        public virtual DbSet<TestTable2> TestTable2s { get; set; }

        //

        /// <summary>
        /// 职业健康类别
        /// </summary>
        public virtual DbSet<TbmZYBType> TbmZYBTypes { get; set; }

        /// <summary>
        /// 接口项目组合对应
        /// </summary>
        public virtual DbSet<TdbInterfaceItemGroupComparison> TdbInterfaceItemGroupComparisons { get; set; }

        /// <summary>
        /// 接口项目对应
        /// </summary>
        public virtual DbSet<TdbInterfaceItemComparison> TdbInterfaceItemComparisons { get; set; }

        /// <summary>
        /// 接口医生对应
        /// </summary>
        public virtual DbSet<TdbInterfaceEmployeeComparison> TdbInterfaceEmployeeComparisons { get; set; }

        /// <summary>
        /// 行政区划代码
        /// </summary>
        public virtual DbSet<AdministrativeDivision> AdministrativeDivisions { get; set; }

        /// <summary>
        /// 问卷
        /// </summary>
        public virtual DbSet<TjlCustomerQuestion> TjlCustomerQuestions { get; set; }

        /// <summary>
        /// 加项包记录
        /// </summary>
        public virtual DbSet<TjlCustomerAddPackage> TjlCustomerAddPackages { get; set; }

        /// <summary>
        /// 加项包关联项目记录
        /// </summary>
        public virtual DbSet<TjlCustomerAddPackageItem> TjlCustomerAddPackageItems { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual DbSet<PersonnelCategory> PersonnelCategories { get; set; }
        /// <summary>
        /// 客户回访记录表
        /// </summary>
        public virtual DbSet<TjlCustomerServiceCallBack> TjlCustomerServiceCallBacks { get; set; }
        /// <summary>
        /// 危急值设置记录表
        /// </summary>
        public virtual DbSet<TjlCrisisSet> TjlCrisisSets { get; set; }
        /// <summary>
        /// 问卷设置表
        /// </summary>
        public virtual DbSet<TbmOneAddXQuestionnaire> TbmOneAddXQuestionnaires { get; set; }

        /// <summary>
        /// 用户个性化设置表
        /// </summary>
        public virtual DbSet<PersonnelIndividuationConfig> PersonnelIndividuationConfigs { get; set; }

        /// <summary>
        /// 启动窗体菜单表
        /// </summary>
        public virtual DbSet<StartupMenuBar> StartupMenuBars { get; set; }

        /// <summary>
        /// 计算型项目
        /// </summary>
        public virtual DbSet<TbmItemProcExpress> TbmItemProcExpress { get; set; }
        /// <summary>
        /// 复查记录
        /// </summary>
        public virtual DbSet<TjlCusReview> TjlCusReviews { get; set; }
        /// <summary>
        /// 申请单
        /// </summary>
        public virtual DbSet<TjlApplicationForm> TjlApplicationForms { get; set; }
        /// <summary>
        /// 短信记录
        /// </summary>
        public virtual DbSet<TjlCusMessage> TjlCusMessages { get; set; }
        /// <summary>
        /// 职业健康与项目关系表
        /// </summary>
        public virtual DbSet<OccupationalDiseaseIncludeItemGroup> OccupationalDiseaseIncludeItemGroups { get; set; }

        /// <summary>
        /// 症状列表
        /// </summary>
        public virtual DbSet<Symptom> Symptoms { get; set; }

        /// <summary>
        /// 职业健康和禁忌证解释
        /// </summary>
        public virtual DbSet<DiseaseContraindicationExplain> DiseaseContraindicationExplain { get; set; }

        /// <summary>
        /// 人体系统划分
        /// </summary>
        public virtual DbSet<HumanBodySystem> HumanBodySystems { get; set; }
        /// <summary>
        /// 工种车间
        /// </summary>
        public virtual DbSet<WorkType> WorkTypes { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual DbSet<RiskFactor> RiskFactors { get; set; }
        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual DbSet<JobCategory> JobCategorys { get; set; }

        /// <summary>
        /// 结论依据
        /// </summary>
        public virtual DbSet<ConclusionBasis> ConclusionBasies { get; set; }

        /// <summary>
        /// 复诊时间
        /// </summary>
        public virtual DbSet<ReVisitTime> ReVisitTime { get; set; }
        /// <summary>
        /// 总检隐藏诊断
        /// </summary>

        public virtual DbSet<TbmSumHide> TbmSumHides { get; set; }

        /// <summary>
        /// 危急值回访
        /// </summary>

        public virtual DbSet<TjlCrisVisit> TjlCrisVisits { get; set; }

        /// <summary>
        /// 柜子设置
        /// </summary>

        public virtual DbSet<TbmCabinet> TbmCabinets { get; set; }
        /// <summary>
        /// 柜子记录
        /// </summary>
        public virtual DbSet<TjlCusCabit> TjlCusCabits { get; set; }
        /// <summary>
        /// 操作日志
        /// </summary>
        public virtual DbSet<TjlOperationLog> TjlOperationLogs { get; set; }

        /// <summary>
        /// 职业健康字典
        /// </summary>
        public virtual DbSet<TbmOccDictionary> TbmOccDictionarys { get; set; }
        /// <summary>
        /// 职业健康
        /// </summary>
        public virtual DbSet<TbmOccDisease> TbmOccDiseases { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual DbSet<TbmOccHazardFactor> TbmOccHazardFactors { get; set; }

        /// <summary>
        /// 危害因素-防护措施
        /// </summary>
        public virtual DbSet<TbmOccHazardFactorsProtective> TbmOccHazardFactorsProtectives { get; set; }

        /// <summary>
        /// 职业健康关联标准
        /// </summary>
        public virtual DbSet<TbmOccStandard> TbmOccStandards { get; set; }

        /// <summary>
        /// 目标疾病
        /// </summary>
        public virtual DbSet<TbmOccTargetDisease> TbmOccTargetDiseases { get; set; }


        /// <summary>
        /// 目标疾病-禁忌证
        /// </summary>
        public virtual DbSet<TbmOccTargetDiseaseContraindication> TbmOccTargetDiseaseContraindications { get; set; }


        /// <summary>
        /// 目标疾病-症状
        /// </summary>
        public virtual DbSet<TbmOccTargetDiseaseSymptoms> TbmOccTargetDiseaseSymptomss { get; set; }


        /// <summary>
        ///问卷-职业史
        /// </summary>
        public virtual DbSet<TjlOccQuesCareerHistory> TjlOccQuesCareerHistorys { get; set; }


        /// <summary>
        ///职业史-危害因素
        /// </summary>
        public virtual DbSet<TjlOccQuesHisHazardFactors> TjlOccQuesHisHazardFactorss { get; set; }
        /// <summary>
        /// 职业史-防护措施
        /// </summary>
        public virtual DbSet<TjlOccQuesHisProtective> TjlOccQuesHisProtectives { get; set; }

        /// <summary>
        /// 问卷-既往史
        /// </summary>
        public virtual DbSet<TjlOccQuesPastHistory> TjlOccQuesPastHistorys { get; set; }

        /// <summary>
        /// 问卷-家族史
        /// </summary>
        public virtual DbSet<TjlOccQuesFamilyHistory> TjlOccQuesFamilyHistorys { get; set; }


        /// <summary>
        ///问卷-症状
        /// </summary>
        public virtual DbSet<TjlOccQuesSymptom> TjlOccQuesSymptoms { get; set; }
        /// <summary>
        ///问卷
        /// </summary>
        public virtual DbSet<TjlOccQuestionnaire> TjlOccQuestionnaires { get; set; }

        /// <summary>
        ///职业健康总检
        /// </summary>
        public virtual DbSet<TjlOccCustomerSum> TjlOccCustomerSums { get; set; }

        /// <summary>
        ///职业健康总检禁忌证
        /// </summary>
        public virtual DbSet<TjlOccCustomerContraindication> TjlOccCustomerContraindications { get; set; }

        /// <summary>
        /// 职业健康总检-职业健康
        /// </summary>
        public virtual DbSet<TjlOccCustomerOccDiseases> TjlOccCustomerOccDiseasess { get; set; }

        /// <summary>
        /// Lis申请单
        /// </summary>
        public virtual DbSet<TjlLisApply> TjlLisApplys { get; set; }

        /// <summary>
        /// 注册信息
        /// </summary>
        public virtual DbSet<TbmRegsit> TbmRegsits { get; set; }
        /// <summary>
        /// 健康卡类别
        /// </summary>
        public virtual DbSet<TbmCardType> TbmCardTypes { get; set; }

        /// <summary>
        /// 健康卡
        /// </summary>
        public virtual DbSet<TbmCard> TbmCards { get; set; }

        /// <summary>
        /// 医嘱项目表
        /// </summary>
        public virtual DbSet<TbmPriceSynchronize> TjlPriceSynchronizes { get; set; }
        /// <summary>
        /// 医嘱项目关联
        /// </summary>
        public virtual DbSet<TbmGroupRePriceSynchronizes> TbmGroupRePriceSynchronizes { get; set; }
        /// <summary>
        /// 科室设置
        /// </summary>
        public virtual DbSet<TbmReSultSet> TbmReSultSets { get; set; }
        /// <summary>
        /// 科室小结
        /// </summary>
        public virtual DbSet<TbmReSultDepart> TbmReSultDeparts { get; set; }
        /// <summary>
        /// 组合小结
        /// </summary>
        public virtual DbSet<TbmReSultCusGroup> TbmReSultCusGroups { get; set; }
        /// <summary>
        /// 项目结果
        /// </summary>
        public virtual DbSet<TbmReSultCusItem> TbmReSultCusItems { get; set; }


        /// <summary>
        /// 单位审批
        /// </summary>
        public virtual DbSet<TjlOAApproValcs> TjlOAApproValcss { get; set; }


        /// <summary>
        /// 单位审批
        /// </summary>
        public virtual DbSet<TbmClientZKSet> TbmClientZKSets { get; set; }

        /// <summary>
        /// 未使用编码
        /// </summary>
        public virtual DbSet<TjlErrBM> TjlErrBMs { get; set; }
        /// <summary>
        /// 上传数据表
        /// </summary>
        public virtual DbSet<TjlCusUpload> TjlCusUploads { get; set; }
        /// <summary>
        /// 上传数据主表
        /// </summary>
        public virtual DbSet<TjlUploadInfo> TjlUploadInfos { get; set; }

        /// <summary>
        /// 危机值设置
        /// </summary>
        public virtual DbSet<TbmCriticalSet> TbmCriticalSets { get; set; }

        /// <summary>
        /// 危机值设置
        /// </summary>
        public virtual DbSet<TbmCriticalDetail> TbmCriticalDetails { get; set; }

        /// <summary>
        /// 疾控接口设置
        /// </summary>
        public virtual DbSet<TbmCountrySet> TbmCountrySets { get; set; }

        /// <summary>
        /// 回访
        /// </summary>
        public virtual DbSet<TjlCusVisit> TjlCusVisits { get; set; }

        /// <summary>
        /// 回访管理
        /// </summary>
        public virtual DbSet<TjlCusVisitManage> TjlCusVisitManages { get; set; }
        /// <summary>
        /// 短信
        /// </summary>
        public virtual DbSet<TjlShortMessage> TjlShortMessages { get; set; }

        /// <summary>
        /// 冲突关键词
        /// </summary>
        public virtual DbSet<TbmSumConflict> TbmSumConflicts { get; set; }

        /// <summary>
        /// 用户签名照片存储表
        /// </summary>
        public virtual DbSet<UserPicture> UserPictures { get; set; }
        /// <summary>
        /// 问卷
        /// </summary>
        public virtual DbSet<TjlCusQuestion> TjlCusQuestions { get; set; }

        /// <summary>
        /// 答卷题目详情
        /// </summary>
        public virtual DbSet<TjlQuestionBom> TjlQuestionBoms { get; set; }

        /// <summary>
        /// 选项题备选项
        /// </summary>
        public virtual DbSet<TjlQuestiontem> TjlQuestiontems { get; set; }

        /// <summary>
        /// 接口数据
        /// </summary>
        public virtual DbSet<TjlInterFace> TjlInterFaces { get; set; }
        

        /// <summary>
        /// 投诉信息数据表
        /// </summary>
        public virtual DbSet<ComplaintInformation> ComplaintInformationDbSet { get; set; }

        /// <summary>
        /// 合同回款记录
        /// </summary>
        public virtual DbSet<ContractProceeds> ContractProceeds { get; set; }

        /// <summary>
        /// 合同开票记录
        /// </summary>
        public virtual DbSet<ContractInvoice> ContractInvoices { get; set; }

        /// <summary>
        /// 合同信息
        /// </summary>
        public virtual DbSet<ContractInformation> ContractInformations { get; set; }

        /// <summary>
        /// 合同类别
        /// </summary>
        public virtual DbSet<ContractCategory> ContractCategories { get; set; }

        /// <summary>
        /// 合同附件
        /// </summary>
        public virtual DbSet<ContractAdjunct> ContractAdjuncts { get; set; }

        /// <summary>
        /// 支付记录
        /// </summary>
        public virtual DbSet<TjlWebCharge> TjlWebCharges { get; set; }

        /// <summary>
        /// 登录记录
        /// </summary>
        public virtual DbSet<TjlLoginList> TjlLoginLists { get; set; }


        /// <summary>
        /// 危害因素总检
        /// </summary>
        public virtual DbSet<TjlOccCustomerHazardSum> TjlOccCustomerHazardSums { get; set; }

        /// <summary>
        /// 照射种类
        /// </summary>
        public virtual DbSet<TbmRadiation> TbmRadiations { get; set; }

        /// <summary>
        /// 放射职业史
        /// </summary>
        public virtual DbSet<TjlOccQuesRadioactiveCareerHistory> TjlOccQuesRadioactiveCareerHistorys { get; set; }

        /// <summary>
        /// 婚姻史
        /// </summary>
        public virtual DbSet<TjlOccQuesMerriyHistory> TjlOccQuesMerriyHistorys { get; set; }


        /// <summary>
        /// 复合诊断
        /// </summary>
        public virtual DbSet<TbmSummHB> TbmSummHBs { get; set; }

        /// <summary>
        /// 人工行程安排
        /// </summary>
        public virtual DbSet<ManualScheduling> ManualScheduling { get; set; }

        /// <summary>
        /// 动态列配置
        /// </summary>
        public virtual DbSet<DynamicColumnConfiguration> DynamicColumnConfiguration { get; set; }

        /// <summary>
        /// 团体报表打印记录
        /// </summary>
        public virtual DbSet<CompanyReportPrintRecord> CompanyReportPrintRecord { get; set; }

        /// <summary>
        /// 职业史扫描记录
        /// </summary>
        public virtual DbSet<TjlCustomerOCCPic> TjlCustomerOCCPics { get; set; }

        private void UnifyConfiguration()
        {
            Configuration.UseDatabaseNullSemantics = true;
            Configuration.AutoDetectChangesEnabled = true;
            //Configuration.LazyLoadingEnabled = false;
            Database.CommandTimeout = 60 * 60;

            //Database.Log += log =>
            //{
            //    //if (log.Contains("tjlcusquestions"))
            //    //{

            //    var basedirectory = AppDomain.CurrentDomain.BaseDirectory;

            //    var logdirectory = System.IO.Path.Combine(basedirectory, "ef sql log");
            //    if (System.IO.Directory.Exists(logdirectory))
            //    {

            //        // ignored
            //    }
            //    else
            //    {
            //        System.IO.Directory.CreateDirectory(logdirectory);
            //    }
            //    var logfile = System.IO.Path.Combine(logdirectory, $"{DateTime.Now:yyyymmdd} ef sql.log");
            //    while (true)
            //    {
            //        try
            //        {

            //            System.IO.File.AppendAllText(logfile, $"{log}{Environment.NewLine}", System.Text.Encoding.UTF8);

            //            break;
            //        }
            //        catch (System.IO.IOException)
            //        {


            //            // ignored
            //        }
            //    }
            //    //}
            //};
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbmDepartment>().HasMany(r => r.ItemInfos).WithRequired(r => r.Department)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TjlOccQuesCareerHistory>().HasRequired(r => r.CustomerRegBM).WithMany().WillCascadeOnDelete(false);         

            modelBuilder.Entity<TjlClientTeamInfo>().Property(r => r.TeamDiscount).HasPrecision(8, 4);
            modelBuilder.Entity<TjlCustomerItemGroup>().Property(r => r.DiscountRate).HasPrecision(8, 4);
            modelBuilder.Entity<TjlClientTeamRegitem>().Property(r => r.Discount).HasPrecision(8, 4);
            modelBuilder.Entity<TbmItemSuitItemGroupContrast>().Property(r => r.Suitgrouprate).HasPrecision(8, 4);

            modelBuilder.Entity<TdbInterfaceItemGroupComparison>().HasRequired(r => r.ItemGroup).WithMany().WillCascadeOnDelete(false);

            // modelBuilder.Entity<TdbInterfaceItemComparison>().HasRequired(r => r.ItemGroup).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<TbmItemInfo>().HasMany(t => t.ItemInfos).WithMany();

            modelBuilder.Entity<TjlCustomerReg>()
                .HasMany(r => r.OccHazardFactors)
                .WithMany(r => r.TjlCustomerRegs).Map(m =>
            {
                m.ToTable("TbmOccHazardFactorTjlCustomerRegs");
                m.MapLeftKey("TjlCustomerReg_Id");
                m.MapRightKey("TbmOccHazardFactor_Id");
            });
            modelBuilder.Entity<TjlClientTeamInfo>()
        .HasMany(r => r.OccHazardFactors)
        .WithMany(r => r.TjlClientTeamInfos).Map(m =>
        {
            m.ToTable("TbmOccHazardFactorTjlClientTeamInfoes");
            m.MapLeftKey("TjlClientTeamInfo_Id");
            m.MapRightKey("TbmOccHazardFactor_Id");
        });
            //        modelBuilder.Entity<TjlCustomerReg>()
            //.HasMany(t => t.Instructors)
            //.WithMany(t => t.Courses)
            //.Map(m =>
            //{
            //    m.ToTable("CourseInstructor");
            //    m.MapLeftKey("CourseID");
            //    m.MapRightKey("InstructorID");
            //});
            modelBuilder.Entity<TbmItemGroup>()
                .HasMany(r => r.ItemInfos)
                .WithMany(r => r.ItemGroups)
                .Map(m =>
                {
                    m.ToTable("TbmItemInfoTbmItemGroups")
                    .MapLeftKey("TbmItemGroup_Id")
                    .MapRightKey("TbmItemInfo_Id");
                });
            modelBuilder.Entity<ComplaintInformation>().HasRequired(r=>r.CustomerRegister)
                .WithMany()
                .HasForeignKey(r => r.CustomerRegisterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractProceeds>()
                .Property(r => r.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ContractInvoice>()
                .Property(r => r.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ContractInformation>()
                .Property(r => r.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ManualScheduling>()
                .HasMany(r => r.DepartmentCollection)
                .WithMany(r => r.ManualSchedulingCollection)
                .Map(c =>
                {
                    c.ToTable("ManualSchedulingDepartmentRelation")
                        .MapLeftKey("ManualSchedulingId")
                        .MapRightKey("DepartmentId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}