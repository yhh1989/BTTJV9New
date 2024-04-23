using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 登记 项目/套餐 收费状态 1未收费2个人已支付3单位已支付4混合付款
    /// </summary>   
    public enum PayerCatType
    {
        /// <summary> 
        /// 未收费  
        /// </summary>
        [Description("未收费")]
        NoCharge = 1,
        /// <summary>
        /// 个人支付
        /// </summary>
        [Description("个人支付")]
        PersonalCharge = 2,
        /// <summary>
        /// 单位支付
        /// </summary>
        [Description("单位支付")]
        ClientCharge = 3,
        /// <summary>
        /// 混合付款
        /// </summary>
        [Description("混合付款")]
        MixedCharge = 4,
        /// <summary>
        /// 赠检
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
        Arrears = 7,
        /// <summary>
        /// 退费
        /// </summary>
        [Description("退费")]
        Refund = 8,
        /// <summary>
        /// 待退费
        /// </summary>
        [Description("待退费")]
        StayRefund = 9,
        /// <summary>
        /// 无退费
        /// </summary>
        [Description("无退费")]
        NotRefund = 10,
        /// <summary>
        /// 限额
        /// </summary>
        [Description("限额")]
        FixedAmount = 11
    }
}
