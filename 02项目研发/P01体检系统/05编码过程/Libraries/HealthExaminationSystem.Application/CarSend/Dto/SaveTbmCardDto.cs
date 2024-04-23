using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmCard))]
#endif
    
   public  class SaveTbmCardDto : EntityDto<Guid>
    {
        /// <summary>
        /// 卡号
        /// </summary>       
        [StringLength(64)]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 卡类别标识
        /// </summary>
  
        public virtual Guid? CardTypeId { get; set; }
  

        /// <summary>
        /// 面值取卡类别面值
        /// </summary>
        public virtual decimal FaceValue { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 开卡人标识
        /// </summary>
        public virtual long? CreateCardUserId { get; set; }
      

        /// <summary>
        /// 销售员标识
        /// </summary>   
        public virtual long? SellCardUserId { get; set; }
       
        /// <summary>
        /// 反款方式1先返款2后返款
        /// </summary>
        public virtual int PayType { get; set; }

        /// <summary>
        /// 先返款结算金额
        /// </summary>
        public virtual decimal? SellMoney { get; set; }
        /// <summary>
        /// 是否启用枚举InvoiceState
        /// </summary>
        public virtual int? Available { get; set; }
        /// <summary>
        /// 使用状态0未使用1已使用
        /// </summary>
        public virtual int? HasUse { get; set; }
        /// <summary>
        /// 卡类别
        /// </summary>       
        [StringLength(64)]
        public virtual string CardCategory { get; set; }
        /// <summary>
        /// 单位分组ID
        /// </summary>
        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }


        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }

    }
}
