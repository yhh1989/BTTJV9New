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
    /// 职业史-危害因素 
    /// </summary>
    public class TjlOccQuesHisHazardFactors : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 职业史Id
        /// </summary>
        [ForeignKey("OccCareerHistory")]
        public virtual Guid? OccCareerHistoryBMId { get; set; }

        /// <summary>
        /// 职业史
        /// </summary>
        public virtual TjlOccQuesCareerHistory OccCareerHistory { get; set; }
        /// <summary>
        /// CAS编码
        /// </summary>
        [StringLength(50)]
        public virtual string CASBM { get; set; }

        /// <summary>
        /// 危害因素名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [StringLength(500)]
        public virtual string TypeName { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }

    }
}
