using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
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
    /// 问卷-症状
    /// </summary>
  public   class TjlOccQuesSymptom : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }
        /// <summary>
        /// 问卷Id
        /// </summary>
        [ForeignKey("OccQuestionnaire")]
        public virtual Guid? OccQuestionnaireBMId { get; set; }

        /// <summary>
        /// 问卷
        /// </summary>
        public virtual TjlOccQuestionnaire OccQuestionnaire { get; set; }
        /// <summary>
        ///症状名称
        /// </summary>
        [StringLength(500)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 症状分类
        /// </summary>
        [StringLength(500)]
        public virtual string Type { get; set; }
        /// <summary>
        /// 症状程度
        /// </summary>
        [StringLength(5)]
        public virtual string Degree { get; set; }
        /// <summary>
        /// 出现时间
        /// </summary>     
        public virtual DateTime? StarTime { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
