using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary>
    /// 发票抬头
    /// </summary>
    public class TjlMRise : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 抬头
        /// </summary>
        [MaxLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 税号
        /// </summary>
        [MaxLength(64)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [MaxLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [MaxLength(64)]
        public virtual string WBCode { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}