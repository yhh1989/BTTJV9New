using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Core.Illness.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 疾病禁忌证及解释
    /// </summary>
    public class DiseaseContraindicationExplain : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        public virtual string Name { get; set; }
        
        /// <summary>
        /// 解释
        /// </summary>
        [MaxLength(256)]
        public virtual string Explain { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public DiseaseContraindicationType Type { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 所属的危害因素
        /// </summary>
        [InverseProperty(nameof(OccupationalDiseaseIncludeItemGroup.DiseaseContraindicationExplains))]
        public virtual ICollection<OccupationalDiseaseIncludeItemGroup> OccupationalDiseaseIncludeItemGroups { get; set; }
    }
}
