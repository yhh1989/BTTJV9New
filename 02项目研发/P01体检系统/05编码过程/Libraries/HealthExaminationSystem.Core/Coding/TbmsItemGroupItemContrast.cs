using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 项目组合与项目对照表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TbmsItemGroupItemContrast : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual int? ItemGroupBM { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual int? ItemBM { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}