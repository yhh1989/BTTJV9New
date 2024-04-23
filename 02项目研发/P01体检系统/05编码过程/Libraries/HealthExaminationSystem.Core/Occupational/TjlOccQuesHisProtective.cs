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
    ///职业史-防护措施
    /// </summary>
    public class TjlOccQuesHisProtective : FullAuditedEntity<Guid>, IMustHaveTenant
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
        /// 编码
        /// </summary>
        [StringLength(50)]
        public virtual string BM { get; set; }

        /// <summary>
        /// 防护措施
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [StringLength(500)]
        public virtual string TypeName { get; set; }

        /// <inheritdoc />
        public virtual int TenantId { get; set; }
    }
}
