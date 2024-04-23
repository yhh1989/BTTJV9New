using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    public  class TjlLoginList : AuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 开单医生Id
        /// </summary>
        [ForeignKey("User")]
        public virtual long  UserId { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public virtual User User { get; set; }
  

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
