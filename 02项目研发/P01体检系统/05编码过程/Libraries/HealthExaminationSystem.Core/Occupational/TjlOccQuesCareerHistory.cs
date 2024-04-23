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
    ///问卷-职业史 
    /// </summary>

    public class TjlOccQuesCareerHistory : FullAuditedEntity<Guid>, IMustHaveTenant
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
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        [StringLength(100)]
        public virtual string  WorkClient { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(100)]
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(100)]
        public virtual string WorkType { get; set; }

        /// <summary>
        /// 工龄
        /// </summary>
       
        public virtual decimal? WorkYears { get; set; }
        /// <summary>
        /// 工龄文本
        /// </summary>

        public virtual string StrWorkYears { get; set; }
        /// <summary>
        /// 工龄单位
        /// </summary>
        [StringLength(2)]
        public virtual string UnitAge { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        [StringLength(8000)]
        public virtual string HazardFactorIds { get; set; }
        /// <summary>
        /// 防护措施
        /// </summary>
        [StringLength(640)]
        public virtual string ProtectiveIds { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual ICollection<TjlOccQuesHisHazardFactors> OccHisHazardFactors { get; set; }
        /// <summary>
        /// 防护措施
        /// </summary>
       public virtual ICollection<TjlOccQuesHisProtective> OccHisProtectives { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
