using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 危急值
    /// </summary>
    public enum CrisisSate
    {
        /// <summary>
        /// 危急值
        /// </summary>
        [Description("危急值")]
        Abnormal = 1,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 2
    }
}