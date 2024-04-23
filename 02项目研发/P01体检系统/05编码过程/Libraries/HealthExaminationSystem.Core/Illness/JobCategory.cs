using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.Extensions.Options;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 岗位类别
    /// </summary>
    public class JobCategory : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 岗位类别
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
        
        /// <summary>
        /// 职业健康包含项目
        /// </summary>
        [InverseProperty(nameof(Sw.Hospital.HealthExaminationSystem.Core.Illness.OccupationalDiseaseIncludeItemGroup.JobCategory))]
        public virtual ICollection<OccupationalDiseaseIncludeItemGroup> OccupationalDiseaseIncludeItemGroup { get; set; }
        //public virtual IConfigureOptions<OccupationalDiseaseIncludeItemGroup> OccupationalDiseaseIncludeItemGroup { get; set; }
    }
}