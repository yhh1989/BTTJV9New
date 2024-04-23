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
    /// 危害因素-防护措施
    /// </summary>
  public  class TbmOccHazardFactorsProtective : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 危害因素
        /// </summary>
        [ForeignKey(nameof(OccHazardFactors))]
        public virtual Guid? OccHazardFactorsId { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual TbmOccHazardFactor OccHazardFactors { get; set; }
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
