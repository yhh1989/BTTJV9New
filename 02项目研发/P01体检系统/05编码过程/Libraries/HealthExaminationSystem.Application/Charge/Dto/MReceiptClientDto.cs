using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{   /// <summary>
    /// 收费记录表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMReceiptInfo))]
#endif
   public  class MReceiptClientDto : EntityDto<Guid>
    {

        /// <summary>
        /// 发票记录
        /// </summary>
        public virtual ICollection<MInvoiceRecordDto> MInvoiceRecord { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual ICollection<CreatePaymentDto> MPayment { get; set; }
        /// <summary>
        /// 体检人预约Id外键
        /// </summary>      
        public virtual Guid? CustomerRegId { get; set; }      
       
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }    

        /// <summary>
        /// 体检类型:1单位2体检人
        /// </summary>
        public virtual int TJType { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public virtual DateTime ChargeDate { get; set; }

        /// <summary>
        /// 总费用:原价放在第一笔消费，如果加项放在第二笔
        /// </summary>
        public virtual decimal Summoney { get; set; }

        /// <summary>
        /// 应收:优惠
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }    

        /// <summary>
        /// 收费员
        /// </summary>
        public virtual UserForComboDto User { get; set; }

        /// <summary>
        /// 交费标示:3普通4储值5免费
        /// </summary>
        public virtual int ChargeState { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal Discontmoney { get; set; }


        /// <summary>
        /// 收费状态:1正常收费2作废3已作废
        /// </summary>
        public virtual int ReceiptSate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }

     
    }
}
