using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 耗材管理对照表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TbmConsumablesContrast : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 耗材设置
        /// </summary>
        public virtual TbmConsumables ConsumablesBM { get; set; }

        /// <summary>
        /// 组合编码
        /// </summary>
        public virtual TbmItemGroup ItemGroupBM { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}