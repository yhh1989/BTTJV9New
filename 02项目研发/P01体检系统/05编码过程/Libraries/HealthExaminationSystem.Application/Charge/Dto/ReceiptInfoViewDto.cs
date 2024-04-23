using Abp.Application.Services.Dto;
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
  public   class ReceiptInfoViewDto : EntityDto<Guid>
    {       
       

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual ChargeCusDto Customer { get; set; }
        
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
        /// 交费标示:1正常收费2欠费3有退费4预支付
        /// </summary>
        public virtual int ChargeState { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal Discontmoney { get; set; }

        /// <summary>
        /// 收费状态:1正常收费2退费
        /// </summary>
        public virtual int ReceiptSate { get; set; }

        /// <summary>
        /// 是否结算:1已结算2未结算
        /// </summary>
        public virtual int SettlementSate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }
       
    }
}
