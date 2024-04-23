using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 组单分组项目（组合）表
    /// <para>组单 -1-n- 组单分组 -1-n- 分组项目 - 1-1- 项目组合</para>
    /// </summary>
    public class TbmComposeGroupItem : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 组单Id
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual Guid ComposeId { get; set; }

        /// <summary>
        /// 组单
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual TbmCompose Compose { get; set; }

        /// <summary>
        /// 组单分组Id
        /// </summary>
        [ForeignKey(nameof(ComposeGroup))]
        public virtual Guid ComposeGroupId { get; set; }

        /// <summary>
        /// 组单分组
        /// </summary>
        [Required]
        public virtual TbmComposeGroup ComposeGroup { get; set; }

        /// <summary>
        /// 项目组合
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 项目组合
        /// </summary>
        [Required]
        public virtual TbmItemGroup ItemGroup { get; set; }
        
        /// <summary>
        /// 组合价格
        /// </summary>
        public virtual decimal ItemGroupMoney { get; set; }

        /// <summary>
        /// 组合折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

        /// <summary>
        /// 组合折扣后价格
        /// </summary>
        public virtual decimal ItemGroupDiscountMoney { get; set; }
        
        /// <summary>
        /// 租户
        /// </summary>
        public int TenantId { get; set; }
    }
}