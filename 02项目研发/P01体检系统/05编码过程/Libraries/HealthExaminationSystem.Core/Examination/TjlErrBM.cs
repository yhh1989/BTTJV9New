using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
 public    class TjlErrBM : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 类别id
        /// </summary>
        [StringLength(64)]
        public virtual string IDType { get; set; }
        /// <summary>
        /// 编码名称
        /// </summary>
        [StringLength(64)]
        public virtual string BM { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
