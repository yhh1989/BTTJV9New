using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 结算发票记录表
    /// </summary>
#if !Proxy
    [AutoMapFrom(typeof(TjlMInvoiceRecord))]
#endif
    public class InvoiceRecordViewDto:EntityDto<Guid>
    {
        /// <summary>
        /// 开票人
        /// </summary>
        public virtual Guid UserID { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual ICollection<CreatePaymentDto> MPaymentgr { get; set; }
        /// <summary>
        /// 发票状态 -ZF SF
        /// </summary>
        public virtual string State { get; set; }
        public virtual Guid MRiseID { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public virtual decimal InvoiceMoney { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public virtual string InvoiceNum { get; set; }
    }
}
