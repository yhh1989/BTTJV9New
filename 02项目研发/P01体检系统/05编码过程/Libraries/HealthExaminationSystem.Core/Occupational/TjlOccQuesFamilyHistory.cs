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
    ///问卷-家族史
    /// </summary>
    public class TjlOccQuesFamilyHistory : FullAuditedEntity<Guid>, IMustHaveTenant
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
        /// 问卷ID
        /// </summary>
        public virtual TjlOccQuestionnaire OccQuestionnaire { get; set; }

        /// <summary>
        /// 家族史疾病
        /// </summary>
        [StringLength(500)]
        public virtual string IllName { get; set; }
        /// <summary>
        /// 疾病关系人
        /// </summary>
        [StringLength(500)]
        public virtual string relatives { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
