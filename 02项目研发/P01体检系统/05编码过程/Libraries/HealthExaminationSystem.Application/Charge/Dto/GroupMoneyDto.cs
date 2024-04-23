using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class GroupMoneyDto : EntityDto<Guid>
    {  
        
            /// <summary>
            /// 最大折扣 
            /// </summary>
            public virtual decimal? MaxDiscount  { get; set; }
            /// <summary>
            ///  加减状态 1为正常项目2为加项3为减项4调项减5调项加
            /// </summary>
            public virtual int? IsAddMinus { get; set; }

            /// <summary>
            /// 退费状态 1正常2带退费3退费 收费处退费后变为减项状态
            /// </summary>
            public virtual int? RefundState { get; set; }

            /// <summary>
            /// 组合价格
            /// </summary>
            public virtual decimal ItemPrice { get; set; }

            /// <summary>
            /// 折扣率
            /// </summary>
            public virtual decimal DiscountRate { get; set; }

            /// <summary>
            /// 折后价格
            /// </summary>
            public virtual decimal PriceAfterDis { get; set; }
             /// <summary>
             /// 
             /// </summary>
            public virtual int? isExceed { get; set; }
            /// <summary>
            /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
            /// </summary>
            public virtual int PayerCat { get; set; }

            /// <summary>
            /// 团体支付金额
            /// </summary>
            public virtual decimal TTmoney { get; set; }

            /// <summary>
            /// 个人支付金额
            /// </summary>
            public virtual decimal GRmoney { get; set; }

    }
}
