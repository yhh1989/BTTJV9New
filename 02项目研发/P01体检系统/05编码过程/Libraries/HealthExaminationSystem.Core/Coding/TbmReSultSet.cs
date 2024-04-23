using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    public class TbmReSultSet : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 显示科室名称
        /// </summary>     
        public virtual ICollection<TbmReSultDepart> Departs { get; set; }

        /// <summary>
        /// 显示组合名称
        /// </summary>     
        public virtual ICollection<TbmReSultCusGroup> Groups { get; set; }
        /// <summary>
        /// 显示项目名称
        /// </summary>     
        public virtual ICollection<TbmReSultCusItem> Items { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        public bool? isAdVice { get; set; }
        /// <summary>
        /// 职业健康
        /// </summary>
        public bool? isOccupational { get; set; }
        /// <summary>
        /// 租户标识
        /// </summary>
        public int TenantId { get; set; }
    }
}
