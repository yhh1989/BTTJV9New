using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 耗材管理
    /// </summary>
    public class TbmConsumables : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 使用的项目组
        /// </summary>
        public virtual ICollection<TbmItemGroup> TbmItemGroups { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(32)]
        public virtual string WBCode { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}