using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 复查状态1正常2复查3回访
    /// </summary>
    public enum ReviewSate
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal =1,
        /// <summary>
        /// 复查
        /// </summary>
        [Description("复查")]
        Review =2,
        /// <summary>
        /// 回访
        /// </summary>
        [Description("回访")]
        ReturnVisit =3
    }
}
