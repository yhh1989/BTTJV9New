using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public  class CardTypeNameDto : EntityDto<Guid>
    {
        /// <summary>
        /// 卡名
        /// </summary>
        [StringLength(64)]
        public virtual string CardName { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        [StringLength(64)]
        public virtual string SellName { get; set; }

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


        /// <summary>
        /// 关联套餐
        /// </summary>
        public virtual List<CardToSuitNameDto> ItemSuits { get; set; }

        /// <summary>
        /// 面值取卡类别面值
        /// </summary>
        public virtual decimal FaceValue { get; set; }

        /// <summary>
        /// 提示
        /// </summary>
        [StringLength(64)]
        public virtual string Err { get; set; }

        /// <summary>
        /// 1 成功2失败
        /// </summary>       
        public virtual int Code { get; set; }
    }
}
