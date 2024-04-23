using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum  ZYBBasicDictionaryType
    {
        ///// <summary>
        ///// 行业大类
        ///// </summary>
        //[Description("行业大类")]
        //IndustryType,

        ///// <summary>
        ///// 行业小类
        ///// </summary>
        //[Description("行业小类")]
        //Industry,

        /// <summary>
        /// 车间
        /// </summary>
        [Description("车间名称")]
        Workshop,
        /// <summary>
        /// 工种
        /// </summary>
        [Description("工种名称")]
        WorkType,

        /// <summary>
        /// 防护措施
        /// </summary>
        [Description("防护措施")]
        Protect,

        /// <summary>
        /// 检查类型
        /// </summary>
        [Description("检查类型")]
        Checktype,

        /// <summary>
        /// 症状大类
        /// </summary>
        [Description("症状大类")]
        SymptomType,

        /// <summary>
        /// 症状小类
        /// </summary>
        [Description("症状小类")]
        Symptom,

        /// <summary>
        /// 病史大类
        /// </summary>
        [Description("病史大类")]
        MedicalHistoryType,
        /// <summary>
        /// 病史小类
        /// </summary>
        [Description("病史小类")]
        MedicalHistory,
        /// <summary>
        /// 危害因素大类
        /// </summary>
        [Description("危害因素大类")]
        HazardHactors,
        /// <summary>
        /// 职业健康大类
        /// </summary>
        [Description("职业病分类")]
        Occupational,
        /// <summary>
        /// 职业禁忌证
        /// </summary>
        [Description("职业禁忌证")]
        Contraindication,
        /// <summary>
        /// 诊断标准
        /// </summary>
        [Description("诊断标准")]
        diagnose,
        /// <summary>
        /// 处理意见
        /// </summary>
        [Description("处理意见")]
        Opinions,
        /// <summary>
        /// 职业健康结论
        /// </summary>
        [Description("职业健康结论")]
        Conclusion,
        /// <summary>
        /// 治疗方式
        /// </summary>
        [Description("治疗方式")]
        Treatment,
        /// <summary>
        /// 参考标准
        /// </summary>
        [Description("参考标准")]
        ReferenceStandard,
        /// <summary>
        /// 职业总检设置
        /// </summary>
        [Description("总检设置")]
        OccSum,

        /// <summary>
        /// 放射线
        /// </summary>
        [Description("放射因素")]
        Radioactive,
    /// <summary>
    /// 照射源
    /// </summary>
        [Description("照射源")]
        Exposure,
        /// <summary>
        /// 医学建议
        /// </summary>
        [Description("医学建议")]
        YXJY





    }
}
