using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    [Obsolete("暂停使用", true)]
    public enum PaymentMethod
    {
        [Description("单位支付")]
        Company = 1,
        [Description("个人支付")]
        Personal = 2,
        [Description("固定金额")]
        FixedAmount = 3
    }
}
