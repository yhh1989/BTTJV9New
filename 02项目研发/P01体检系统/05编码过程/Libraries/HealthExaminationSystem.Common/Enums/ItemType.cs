using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 项目类型
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 数值型
        /// </summary>
        [Description("数值型")]
        Number = 1,

        /// <summary>
        /// 计算型
        /// </summary>
        [Description("计算型")]
        Calculation = 2,

        /// <summary>
        /// 说明型
        /// </summary>
        [Description("数据文字混合型")]
        Explain = 3,

        /// <summary>
        /// 阴阳型
        /// </summary>
        [Description("阴阳型")]
        YinYang = 4
    }
}