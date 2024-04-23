 using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 项目检查状态
    /// </summary>
    public enum ProjectIState
    {
        /// <summary>
        /// 未体检
        /// </summary>
        [Description("未体检")]
        Not = 1,
        /// <summary>
        ///  已检查
        /// </summary>
        [Description("已检查")]
        Complete = 2,
        /// <summary>
        /// 部分检查
        /// </summary>
        [Description("部分检查")]
        Part = 3,
        /// <summary>
        /// 放弃
        /// </summary>
        [Description("放弃")]
        GiveUp = 4,
        /// <summary>
        /// 待查
        /// </summary>
        [Description("待查")]
        Stay = 5,
        /// <summary>
        /// 暂存
        /// </summary>
        [Description("暂存")]
        Temporary = 6
    }
}