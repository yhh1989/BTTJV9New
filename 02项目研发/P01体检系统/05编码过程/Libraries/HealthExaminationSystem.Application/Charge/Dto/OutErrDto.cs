using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  public   class OutErrDto : EntityDto<Guid>
    {
        /// <summary>
        /// 是否成功 1成功 0异常
        /// </summary>      
        public virtual string code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>      
        public virtual string err { get; set; }
        /// <summary>
        /// 会员卡详情
        /// </summary>      
        public virtual string cardInfo { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string merchant_id { get; set; }
        /// <summary>
        /// 支付平台支付订单号
        /// </summary>
        public string pay_order_id { get; set; }
    }
}
