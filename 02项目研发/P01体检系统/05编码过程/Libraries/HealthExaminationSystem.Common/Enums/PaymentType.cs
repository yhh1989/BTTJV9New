using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 支付方式打印类型
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 1,
        /// <summary>
        /// 小票
        /// </summary>
        SmallTicket = 2,
        /// <summary>
        /// 收据
        /// </summary>
        Receipt = 3,
        /// <summary>
        /// 发票
        /// </summary>
        Invoice = 4
    }
}
