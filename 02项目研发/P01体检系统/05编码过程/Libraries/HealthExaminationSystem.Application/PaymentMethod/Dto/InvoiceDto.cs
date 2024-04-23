using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto
{
   public  class InvoiceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 作废原ID
        /// </summary>
        public virtual Guid? InvalidTjlMInvoiceRecordId { get; set; }

        /// <summary>
        /// 开票人
        /// </summary>
        public virtual UserFormDto User { get; set; }        

        /// <summary>
        /// 发票状态 1收费2作废
        /// </summary>
        [MaxLength(8)]
        public virtual string State { get; set; }

        /// <summary>
        /// 发票抬头id
        /// </summary>
        public virtual MRiseDto MRise { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        public virtual decimal InvoiceMoney { get; set; }

        /// <summary>
        /// 结算表
        /// </summary>
        //public virtual TjlMReceiptInfo MReceiptInfo { get; set; }

        /// <summary>
        /// 结算表标识
        /// </summary>    
        public virtual Guid? MReceiptInfoId { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        [StringLength(64)]
        public virtual string InvoiceNum { get; set; }

        
      
    }
}
