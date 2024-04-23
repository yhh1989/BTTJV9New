#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{

    /// <summary>
    /// 结算支付方式记录表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMPayment))]
#endif
    public class  CreatePaymentDto
    {
        /// <summary>
        /// 发票号 
        /// </summary>
        // public virtual string ReceiptCode { get; set; }
        public virtual Guid MReceiptInfoId { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        // public virtual int ChargeCode { get; set; }
        public virtual Guid MChargeTypeId { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public virtual string MChargeTypename { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [StringLength(64)]
        public virtual string CardNum { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 折扣价格
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

    }
}
