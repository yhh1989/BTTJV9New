using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 耗材设置表
    /// </summary>
  public   class TbmGroupConsumables : FullAuditedEntity<Guid>, IMustHaveTenant
    {
            /// <summary>
            /// 关联耗材
            /// </summary>
        public virtual ICollection<TbmReConsumables> Consumables { get; set; }
        /// <summary>
        /// 组合表
        /// </summary>
        public virtual ICollection<TbmItemGroup> IemGroups { get; set; }        

        /// <summary>
        /// 条码
        /// </summary>
        public virtual ICollection<TbmBarSettings> BarSettings { get; set; }

        public int TenantId { get; set; }

    }
}
