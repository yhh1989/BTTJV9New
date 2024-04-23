using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    public class TbmClientZKSet : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 折扣类型
        /// </summary>     
        public virtual int? ZKType { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>     
        public virtual Decimal? DiscountRate { get; set; }
        /// <summary>
        /// 租户标识
        /// </summary>
        public int TenantId { get; set; }
    }
}
