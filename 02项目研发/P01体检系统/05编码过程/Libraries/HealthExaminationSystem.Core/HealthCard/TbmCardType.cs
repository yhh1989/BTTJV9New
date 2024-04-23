using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.HealthCard
{
    /// <summary>
    /// 健康卡类别
    /// </summary>
    public class TbmCardType : AuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 编号
        /// </summary>       
        public virtual int CardNum { get; set; }
        /// <summary>
        /// 卡名
        /// </summary>
        [StringLength(64)]
        public virtual string CardName { get; set; }
        /// <summary>
        /// 1有期限2无期限
        /// </summary>
        public virtual int Term { get; set; }
        /// <summary>
        /// 有期限（多少月）
        /// </summary>
        public virtual int? Months { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        public virtual decimal FaceValue { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual string  CardType { get; set; }
        /// <summary>
        /// 是否启用枚举InvoiceState
        /// </summary>
        public virtual int? Available { get; set; }

        /// <summary>
        /// 关联套餐
        /// </summary>
        public virtual ICollection<TbmItemSuit> ItemSuits { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public int TenantId { get; set; }
    }
}
