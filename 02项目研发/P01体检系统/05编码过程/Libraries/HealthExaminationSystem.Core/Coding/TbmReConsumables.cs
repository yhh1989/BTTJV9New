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
    /// 关联耗材表
    /// </summary>
   public  class TbmReConsumables : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 组合标识
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        ///组合
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Num { get; set; }

        public int TenantId { get; set; }
    }
}
