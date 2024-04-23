using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary> 
    /// 疾病状态
    /// </summary>
    public enum IllnessSate
    {
        /// <summary>
        /// 临床诊断
        /// </summary>
        [Description("临床诊断")]
        Diagnosis = 1,

        /// <summary>
        /// 疑似诊断
        /// </summary>
        [Description("疑似诊断")]
        Doubtful = 2,

        /// <summary>
        /// 专科查体异常发现
        /// </summary>
        [Description("专科查体异常发现")]
        Abnormal = 3,

        /// <summary>
        /// 实验室检查异常发现
        /// </summary>
        [Description("实验室检查异常发现")]
        Laboratory = 4,

        /// <summary>
        /// 辅助检查异常发现
        /// </summary>
        [Description("辅助检查异常发现")]
        Auxiliary = 5,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 6
    }
}