using System.ComponentModel;

namespace HealthExaminationSystem.Enumerations
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 未知的性别
        /// </summary>
        [Description("未知的性别")]
        Unknown = 0,
        /// <summary>
        ///  男性
        /// </summary>
        [Description("男性")]
        Man = 1,
        /// <summary>
        /// 女性
        /// </summary>
        [Description("女性")]
        Woman = 2,
        /// <summary>
        /// 女性改（变）为男性
        /// </summary>
        [Description("女性改（变）为男性")]
        WomenChangedIntoMen = 5,
        /// <summary>
        /// 男性改（变）为女性
        /// </summary>
        [Description("男性改（变）为女性")]
        ManChangedIntoWoman = 6,
        /// <summary>
        /// 未说明的性别
        /// </summary>
        [Description("未说明的性别")]
        GenderNotSpecified = 9
    }
}