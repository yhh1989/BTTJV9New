using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 发票记录
    /// </summary>
#if !Proxy
        [AutoMap(typeof(TjlMInvoiceRecord))]
#endif
    public class MInvoiceRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 开票人
        /// </summary>
        public virtual UserViewDto User { get; set; }

        ///// <summary>
        ///// 开票时间
        ///// </summary>
        //public virtual string InvoiceDate { get; set; }

        /// <summary>
        /// 发票状态 -ZF SF
        /// </summary>
        [StringLength(8)]
        public virtual string State { get; set; }


        /// <summary>
        /// 发票金额
        /// </summary>
        public virtual decimal InvoiceMoney { get; set; }


        /// <summary>
        /// 结算表ID
        /// </summary>
        public virtual Guid? MReceiptInfoId { get; set; }
        public virtual MReceiptInfoDto MReceiptInfo { get; set; }
        /// <summary>
        /// 发票抬头id
        /// </summary>
        public virtual MRiseDto MRise { get; set; }
       

        /// <summary>
        /// 发票号
        /// </summary>
        [StringLength(64)]
        public virtual string InvoiceNum { get; set; }

    }
}