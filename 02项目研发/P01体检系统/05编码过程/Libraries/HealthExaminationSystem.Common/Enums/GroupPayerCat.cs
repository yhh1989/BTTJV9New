using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 项目收费状态
    /// </summary>
    [Obsolete("暂停使用", true)]
    public enum GroupPayerCat
    {
        /// <summary>
        /// 未收费   1未收费2个人已支付3单位已支付4.混合付款
        /// </summary>
        [Description("未收费")]
        NoCharge = 1,
        /// <summary>
        /// 个人已支付
        /// </summary>
        [Description("个人已支付")]
        PersonalCharge = 2,
        /// <summary>
        /// 单位已支付
        /// </summary>
        [Description("单位已支付")]
        ClientCharge = 3,
        /// <summary>
        /// 混合付款
        /// </summary>
        [Description("混合付款")]
        MixedCharge = 4,
        /// <summary>
        /// 混合付款
        /// </summary>
        [Description("赠检")]
        GiveCharge = 5,
        /// <summary>
        /// 已收费
        /// </summary>
        [Description("已收费")]
        Charge = 6,
        /// <summary>
        /// 欠费
        /// </summary>
        [Description("欠费")]
        Arrears = 7
    }
}
