using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 检查项目 加减项类别1为正常项目2为加项3为减项4调项减5调项加
    /// </summary>
    public enum AddMinusType
    {
        /// <summary>
        /// 正常项目
        /// </summary>
        [Description("正常")]
        Normal = 1,

        /// <summary>
        /// 加项
        /// </summary>
        [Description("加项")]
        Add = 2,

        /// <summary>
        /// 减项
        /// </summary>
        [Description("减项")]
        Minus = 3,

        /// <summary>
        /// 调项减
        /// </summary>
        [Description("调项减")]
        AdjustMinus = 4,

        /// <summary>
        /// 调项加
        /// </summary>
        [Description("调项加")]
        AdjustAdd = 5
    }
}