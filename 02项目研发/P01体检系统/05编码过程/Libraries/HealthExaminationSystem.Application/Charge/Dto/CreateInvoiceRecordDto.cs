using System;
using System.ComponentModel.DataAnnotations;
#if Application
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 结算发票记录表
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlMInvoiceRecord))]
#endif
    public class CreateInvoiceRecordDto
    {
        /// <summary>
        /// 开票人
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual Guid UserID { get; set; }

        /// <summary>
        /// 发票状态 -ZF SF
        /// </summary>
        [StringLength(8)]
        public virtual string State { get; set; }

        /// <summary>
        /// 发票抬头id
        /// </summary>

        //public virtual string RiseBM { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual Guid MRiseID { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        public virtual decimal InvoiceMoney { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        [StringLength(64)]
        public virtual string InvoiceNum { get; set; }
    }
}