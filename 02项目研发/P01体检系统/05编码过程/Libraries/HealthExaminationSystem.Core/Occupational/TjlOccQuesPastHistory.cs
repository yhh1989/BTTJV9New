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
    ///问卷-既往史
    /// </summary>
   public class TjlOccQuesPastHistory : FullAuditedEntity<Guid>, IMustHaveTenant
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
        /// 体检人预约ID
        /// </summary>
        public virtual TjlOccQuestionnaire OccQuestionnaire { get; set; }


        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(100)]
        public virtual string IllName { get; set; }
        /// <summary>
        /// 其他疾病名称
        /// </summary>
        [StringLength(100)]
        public virtual string OtherIllName { get; set; }
        /// <summary>
        /// 诊断日期
        /// </summary>

        public virtual DateTime? DiagnTime { get; set; }
        /// <summary>
        /// 诊断单位
        /// </summary>
        [StringLength(100)]
        public virtual string DiagnosisClient { get; set; }
        /// <summary>
        /// 治疗方式
        /// </summary>
        [StringLength(100)]
        public virtual string Treatment { get; set; }
        /// <summary>
        /// 是否痊愈
        /// </summary>
       
        public virtual int? Iscured { get; set; }


        /// <summary>
        /// 诊断证书编号
        /// </summary>
        [StringLength(100)]
        public virtual string DiagnosticCode { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
