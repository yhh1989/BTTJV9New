using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 个性化配置表
    /// </summary>
    /// <remarks>
    /// 整个租户（医院）公用同一个配置，不包含用户个性化配置
    /// </remarks>
    public class IndividuationConfig : AuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public DefinedIndividuation Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}