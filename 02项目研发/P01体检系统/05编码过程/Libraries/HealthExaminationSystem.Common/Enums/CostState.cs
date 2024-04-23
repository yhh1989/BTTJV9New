using System;
using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 收费状态
    /// </summary>
    [Obsolete("暂停使用", true)]
    public enum CostState
    {
        /// <summary>
        /// 未收费
        /// </summary>
        [Description("未收费")]
        Uncharged = 1,

        /// <summary>
        /// 已收费
        /// </summary>
        [Description("已收费")]
        Charge = 2,

        /// <summary>
        /// 欠费
        /// </summary>
        [Description("欠费")]
        Arrears = 3
    }
}