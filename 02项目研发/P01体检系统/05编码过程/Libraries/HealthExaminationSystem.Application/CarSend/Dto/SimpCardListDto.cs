using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto
{
   public  class SimpCardListDto : EntityDto<Guid>
    {
        /// <summary>
        /// 卡号
        /// </summary>       
        [StringLength(64)]
        public virtual string CardNo { get; set; }
        
        /// <summary>
        /// 卡名称
        /// </summary>
        public virtual string CardTypeName { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? EndTime { get; set; }


        /// <summary>
        /// 面值取卡类别面值
        /// </summary>
        public virtual decimal FaceValue { get; set; }

        /// <summary>
        /// 开卡人
        /// </summary>
        public virtual string CreateCardUserName { get; set; }

        /// <summary>
        /// 开卡时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public virtual decimal? SellMoney { get; set; }
        
        /// <summary>
        /// 反款方式1先返款2后返款
        /// </summary>
        public virtual string PayTypefomart { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public virtual string ChageUser { get; set; }

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
