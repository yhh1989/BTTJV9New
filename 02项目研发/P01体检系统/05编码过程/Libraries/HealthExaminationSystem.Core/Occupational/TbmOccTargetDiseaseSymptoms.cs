using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Occupational
{
    /// <summary>
    /// 目标疾病-症状
    /// </summary>
    public class TbmOccTargetDiseaseSymptoms : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 目标疾病Id
        /// </summary>
        [ForeignKey(nameof(OccTargetDisease))]
        public virtual Guid? OccTargetDiseaseId { get; set; }

        /// <summary>
        /// 目标疾病
        /// </summary>
        public virtual TbmOccTargetDisease OccTargetDisease { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <inheritdoc />
        public virtual int TenantId { get; set; }
    }
}
