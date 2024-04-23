using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 结论依据
    /// </summary>
    public class ConclusionBasis : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 结论依据
        /// </summary>
        [MaxLength(32)]
        public virtual string Name { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        [MaxLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}