using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using System.ComponentModel.DataAnnotations.Schema;
#endif
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    public class InvoiceViewDto : EntityDto<Guid>
    {
      
        /// <summary>
        /// 开票人
        /// </summary>
        public virtual UserForComboDto User { get; set; }        

        /// <summary>
        /// 发票状态 1收费2作废
        /// </summary>
        [StringLength(8)]
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
        public virtual ReceiptInfoViewDto MReceiptInfo { get; set; }
       

        /// <summary>
        /// 发票号
        /// </summary>
        [StringLength(64)]
        public virtual string InvoiceNum { get; set; }

    }
}
