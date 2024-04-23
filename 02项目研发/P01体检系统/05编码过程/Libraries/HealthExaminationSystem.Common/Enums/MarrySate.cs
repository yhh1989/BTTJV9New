using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// GB/T 2261.2-2003
    /// </summary>
    public enum MarrySate
    {
        /// <summary>
        /// 未婚
        /// </summary>
        [Description("未婚")]
        Unmarried = 10,
        /// <summary>
        /// 已婚
        /// </summary>
        [Description("已婚")]
        Married = 20,
        /// <summary>
        /// 初婚
        /// </summary>
        [Description("初婚")]
        FirstMarriage = 21,
        /// <summary>
        /// 再婚
        /// </summary>
        [Description("再婚")]
        Remarriage = 22,
        /// <summary>
        /// 复婚
        /// </summary>
        [Description("复婚")]
        Remarry = 23,
        /// <summary>
        /// 丧偶
        /// </summary>
        [Description("丧偶")]
        Widowhoo = 30,
        /// <summary>
        /// 离婚
        /// </summary>
        [Description("离婚")]
        Divorce = 40,
        /// <summary>
        /// 未说明的婚姻状况
        /// </summary>
        [Description("未说明的婚姻状况")]
        Unstated = 90,
    }
}
