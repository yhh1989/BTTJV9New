using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 体检来源
    /// by yjh
    /// </summary>
    public enum ExaminationSource
    {
        [Description("微信预约")]
        ECode1 = 1,

        [Description("到店预约")]
        ECode2 = 2,

        [Description("网上预约")]
        ECode3 = 3,

        [Description("团购")]
        ECode4 = 4,

        [Description("会员卡活动")]
        ECode5 = 5
    }
}