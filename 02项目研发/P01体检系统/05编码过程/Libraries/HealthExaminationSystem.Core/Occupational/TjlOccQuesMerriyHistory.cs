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
    ///问卷-婚姻史
    /// </summary>
    public class TjlOccQuesMerriyHistory : FullAuditedEntity<Guid>, IMustHaveTenant
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
        /// 婚姻日期
        /// </summary>
        public virtual DateTime? StarTime { get; set; }
        /// <summary>
        /// 放射线种类ID
        /// </summary>
        [StringLength(640)]
        public virtual string TbmOccDictionaryIDs { get; set; }

        /// <summary>
        /// 放射线种类
        /// </summary>
        public virtual ICollection<TbmOccDictionary> TbmOccDictionarys { get; set; }
        /// <summary>
        /// 放射线种类
        /// </summary>
        [StringLength(640)]
        public virtual string Radioactive { get; set; }
        /// <summary>
        /// 职业及健康状况
        /// </summary>
        [StringLength(640)]
        public virtual string OccHealth { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
