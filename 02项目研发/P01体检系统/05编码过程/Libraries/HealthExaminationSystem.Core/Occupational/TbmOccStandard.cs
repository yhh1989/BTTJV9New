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
   /// 职业健康关联标准
   /// </summary>
  public   class TbmOccStandard : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 职业健康Id
        /// </summary>
        [ForeignKey(nameof(OccDiseases))]
        public virtual Guid? OccDiseasesId { get; set; }

        /// <summary>
        /// 职业健康
        /// </summary>
        public virtual TbmOccDisease OccDiseases { get; set; }

        /// <summary>
        /// 标准编号
        /// </summary>
        [StringLength(50)]
        public virtual string StandardNo { get; set; }
        /// <summary>
        /// 职业健康标准
        /// </summary>
        [StringLength(500)]
        public virtual string StandardName { get; set; }
        /// <summary>
        /// 是否默认1默认0否
        /// </summary>     
        public virtual int IsShow { get; set; }
        public virtual int TenantId { get; set; }

    }
}
