using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 人体系统划分
    /// </summary>
    public class HumanBodySystem : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(128)]
        public string MnemonicCode { get; set; }

        /// <summary>
        /// 职业健康关联
        /// </summary>
        [InverseProperty(nameof(OccupationalDiseaseIncludeItemGroup.HumanBodySystems))]
        public virtual ICollection<OccupationalDiseaseIncludeItemGroup> OccupationalDiseaseIncludeItemGroups { get; set; }
    }
}