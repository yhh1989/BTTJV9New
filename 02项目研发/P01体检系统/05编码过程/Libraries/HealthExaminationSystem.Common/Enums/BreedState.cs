using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 孕/育状态
    /// </summary>
    public enum BreedState
    {
        /// <summary>
        /// 备孕
        /// </summary>
        [Description("备孕")]
        PlanPregnancy=0,
        /// <summary>
        /// 已孕
        /// </summary>
        [Description("已孕")]
        Gestation=1,
        /// <summary>
        /// 哺乳期
        /// </summary>
        [Description("哺乳期")]
        Lactation=2,
        /// <summary>
        /// 无允许状态
        /// </summary>
        [Description("无")]
        No=3,
    }
}
