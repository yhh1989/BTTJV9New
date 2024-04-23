using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class WCusGroupDto
    {
        /// <summary>
        /// 组合编码
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupCodeBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

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
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
        /// </summary>
        public virtual int? PayerCat { get; set; }

        /// <summary>
        /// 团体支付金额
        /// </summary>
        public virtual decimal TTmoney { get; set; }

        /// <summary>
        /// 个人支付金额
        /// </summary>
        public virtual decimal GRmoney { get; set; }

        /// <summary>
        /// 套餐编码
        /// </summary>
        public virtual string ItemSuitBM { get; set; }

        /// <summary>
        /// 检查医生
        /// </summary>
        public virtual string CheckName { get; set; }
        /// <summary>
        /// 审核医生
        /// </summary>
        public virtual string EmpName { get; set; }
    }
}
