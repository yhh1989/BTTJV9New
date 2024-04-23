using System;
using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 基本字典类别
    /// </summary>
    public enum BasicDictionaryType
    {
        /// <summary>
        /// 标本类型
        /// </summary>
        [Description("标本类型字典")]
        SpecimenType,

        /// <summary>
        /// 体检类型
        /// </summary>
        [Description("体检类型字典")]
        ExaminationType,

        /// <summary>
        /// 打印方式
        /// </summary>
        [Description("打印方式字典")]
        PrintMethod,

        /// <summary>
        /// 打印位置
        /// </summary>
        [Description("打印位置字典")]
        PrintPosition,

        /// <summary>
        /// 检验方式
        /// </summary>
        [Description("检验方式字典")]
        TestType,

        /// <summary>
        /// 疾病类别
        /// </summary>
        [Description("疾病类别字典")]
        DiagnosisAType,

        /// <summary>
        /// 客户类别
        /// </summary>
        [Description("客户类别")]
        CustomerType,

        ///// <summary>
        ///// 省 
        ///// </summary>
        //[Description("省")]
        //[Obsolete("禁止使用，2018年10月29日", true)]
        //Province,

        ///// <summary>
        ///// 市
        ///// </summary>
        //[Description("市")]
        //[Obsolete("禁止使用，2018年10月29日", true)]
        //City,

        ///// <summary>
        ///// 区
        ///// </summary>
        //[Description("区")]
        //[Obsolete("禁止使用，2018年10月29日", true)]
        //Area,

        /// <summary>
        /// 行业
        /// </summary>
        [Description("行业")]
        Clientlndutry,

        /// <summary>
        /// 合同性质
        /// </summary>
        [Description("合同性质")]
        Clientcontract,

        /// <summary>
        /// 单位状态
        /// </summary>
        [Description("单位状态")]
        ClientSate,

        /// <summary>
        /// 单位类型
        /// </summary>
        [Description("单位类型")]
        ClientType,

        /// <summary>
        /// 文化程度
        /// </summary>
        [Description("文化程度")]
        DegreeType,

        /// <summary>
        /// 保密级别
        /// </summary>
        [Description("保密级别")]
        SecrecyLevel,

        /// <summary>
        /// 体检报告取得方式
        /// </summary>
        [Description("报告自取")]
        ReportSentType,

        /// <summary>
        /// 证件类型
        /// </summary>
        [Description("证件类型")]
        CertificateType,

        /// <summary>
        /// 体检来源
        /// </summary>
        [Description("体检来源")]
        ClientSource,

        /// <summary>
        /// 条码设置
        /// </summary>
        [Description("条码设置")]
        BarPrintSet,

        /// <summary>
        /// 导引单设置
        /// </summary>
        [Description("导引单设置")]
        GuidanceSet,

        /// <summary>
        /// 个人报告设置 
        /// </summary>
        [Description("个人报告设置")]
        PresentationSet,
        /// <summary>
        /// 团体报告设置
        /// </summary>
        [Description("团体报告设置")]
        GroupReportSet,
        ///// <summary>
        ///// 小结格式
        ///// </summary>
        //[Description("小结格式")]
        //ConclusionForm,
        ///// <summary>
        ///// 项目信息
        ///// </summary>
        //[Description("项目信息")]
        //ProjectInformation,
        /// <summary>
        /// 收费类别
        /// </summary>
        [Description("收费类别")]
        ChargeCategory,
        /// <summary>
        /// 前台功能控制
        /// </summary>
        [Description("前台功能控制")]
        ForegroundFunctionControl,
        /// <summary>
        /// 医生站展示列
        /// </summary>
        [Description("医生站展示列")]
        DoctorStationDisplayColumn,
        ///// <summary>
        ///// 体脂计算
        ///// </summary>
        //[Description("体脂计算")]
        //ComputingProject,
        ///// <summary>
        ///// 消息推送
        ///// </summary>
        //[Description("消息推送")]
        //PushMessage,
        /// <summary>
        /// 危害因素类别
        /// </summary>
        [Description("危害因素类别")]
        HazardCategory,
        ///// <summary>
        ///// 家族史
        ///// </summary>
        //[Description("家族史")]
        //FamilyHistory,
        ///// <summary>
        ///// 工种
        ///// </summary>
        //[Description("工种")]
        //TypeOFWork,
        ///// <summary>
        ///// 车间
        ///// </summary>
        //[Description("车间")]
        //WorkShop,
        ///// <summary>
        ///// 症状
        ///// </summary>
        //[Description("症状")]
        //Symptom,
        ///// <summary>
        ///// 处理意见
        ///// </summary>
        //[Description("处理意见")]
        //HandlingOpinions,
        ///// <summary>
        ///// 总检结论
        ///// </summary>
        //[Description("职业结论")]
        //GeneralConclusion,
        ///// <summary>
        ///// 结论依据
        ///// </summary>
        //[Description("结论依据")]
        //ConclusionBasis,
        ///// <summary>
        ///// 岗位类别
        ///// </summary>
        //[Description("岗位类别")]
        //JobCategory,
        ///// <summary>
        ///// 科室报告设置
        ///// </summary>
        //[Description("科室报告设置")]
        //DepartPresentationSet,
        /// <summary>
        /// 抽血科室编码
        /// </summary>
        [Description("采血科室编码")]
        DrawBloodDepartmentID,
        ///// <summary>
        /// 抽血科室编码
        /// </summary>
        [Description("是否收费")]
        IsCharge,
        /// <summary>
        /// 总检设置
        /// </summary>
        [Description("总检设置")]
        CusSumSet,
        /// <summary>
        /// HIS接口
        /// </summary>
        [Description("HIS接口设置")]
        HisInterface,
        /// <summary>
        /// 打印机设置
        /// </summary>
        [Description("打印机设置")]
        PrintNameSet,
        /// <summary>
        /// 彩图科室
        /// </summary>
        [Description("彩图科室")]
        PicDepartment,
        /// <summary>
        /// 公卫接口配置
        /// </summary>
        [Description("公卫接口配置")]
        PublicHealthURL,
        /// <summary>
        /// 公卫接口配置
        /// </summary>
        [Description("默认检查医生")]
        DepartChackUser,
        /// <summary>
        /// 挂号科室
        /// </summary>
        [Description("挂号科室")]
        NucleicAcidtType,
        /// <summary>
        /// 经济类型
        /// </summary>
        [Description("经济类型")]
        EconomicsType,
        /// <summary>
        /// 企业规模
        /// </summary>
        [Description("企业规模")]
        ScaleType,
        /// <summary>
        /// 企业规模
        /// </summary>
        [Description("纸质报告组合名称")]
        PayerGroup,
        /// <summary>
        /// 体检机构信息
        /// </summary>
        [Description("职业网报信息")]
        Institution,
        /// <summary>
        /// 慢病名称
        /// </summary>
        [Description("慢病种类")]
        Disease,
        ///// <summary>
        /// 科室小结控制
        /// </summary>
        [Description("科室小结控制")]
        DepatSumSet,
        ///// <summary>
        /// 短信模板
        /// </summary>
        [Description("短信模板")]
        ShortModle,
        /// <summary>
        /// 投诉方式
        /// </summary>
        [Description("投诉方式")]
        ComplainWay,
        /// <summary>
        /// 投诉类别
        /// </summary>
        [Description("投诉类别")]
        ComplainCategory,
        /// <summary>
        /// 人体结构
        /// </summary>
        [Description("人体结构")]
        Body,
        /// <summary>
        /// 公卫接口
        /// </summary>
        [Description("公卫接口")]
        GWJK,
        /// <summary>
        /// 电子发票接口
        /// </summary>
        [Description("电子发票接口")]
        DZFP,
        /// <summary>
        /// 线上接口
        /// </summary>
        [Description("线上支付接口")]
        WebPay,
        /// <summary>
        /// 检验通知单类型
        /// </summary>
        [Description("检验通知单类型")]
        TestNotice,
        /// <summary>
        /// 检查通知单类型
        /// </summary>
        [Description("检查通知单类型")]
        InspectNotice,
        /// <summary>
        /// 检查方法
        /// </summary>
        [Description("检查方法")]
        InspectionType,
        /// <summary>
        /// 检查检验类别
        /// </summary>
        [Description("检查检验类别")]
        ChckType,
        /// <summary>
        /// 个人报告附加子报表
        /// </summary>
        [Description("个人报告附加子报表")]
        ReportAdd,
        /// <summary>
        /// 所属院区
        /// </summary>
        [Description("所属院区")]
        HospitalArea,
        /// <summary>
        /// 大科室
        /// </summary>
        [Description("大科室")]
        LargeDepatType,
        /// <summary>
        /// 汇总隐藏诊断
        /// </summary>
        [Description("汇总过滤诊断")]
        SumHide,
        /// <summary>
        /// 360视图地址
        /// </summary>
        [Description("360视图地址")]
        InterUrl,
        /// <summary>
        /// 医保可打折医生
        /// </summary>
        [Description("医保可打折医生")]
        YBDoct,
        /// <summary>
        /// 所见诊断同项目科室
        /// </summary>
        [Description("所见诊断同项目科室")]
        ZDITem,
        /// <summary>
       /// 所见诊断同项目科室
       /// </summary>
        [Description("人员列表展示方式")]
        CusList,
        /// <summary>
        /// 体检推送
        /// </summary>
        [Description("体检推送")]
        DJTS,
        /// <summary>
        /// Word报告
        /// </summary>
        [Description("Word报告")]
        WordReport,
        /// <summary>
        /// 替检备注
        /// </summary>
        [Description("替检备注")]
        PrimaryName,
        ///// <summary>
        ///// 认证核验
        ///// </summary>
        //[Description("认证核验")]
        //Witness
        /// <summary>
        /// 照片尺寸
        /// </summary>
        [Description("登记拍照设置")]
        PhotoSize,
        /// <summary>
        /// 互斥组合
        /// </summary>
        [Description("互斥组合")]
        HCGroup,
        /// <summary>
        /// 历史对比报告
        /// </summary>
        [Description("历史对比报告")]
        HisDB,
        /// <summary>
        /// 人脸设备
        /// </summary>
        [Description("人脸设备")]
        FaceSet,
        /// <summary>
        /// 人脸设备对应
        /// </summary>
        [Description("人脸设备对应")]
        FaceSetIP
    }
}