using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 问卷
    /// </summary>
    public class TbmOneAddXQuestionnaire : FullAuditedEntity<Guid>, IMustHaveTenant
    {
       /// <summary>
       /// 编码
       /// </summary>
        [StringLength(32)]
        public virtual string Coding { get; set; }


        /// <summary>
        /// 外部编码
        /// </summary>
        [StringLength(20)]
        public virtual string ExternalCode { get; set; }

        /// <summary>
        /// 问卷名称
        /// </summary>
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 问卷类别
        /// </summary>
        [StringLength(50)]
        public virtual string Category { get; set; }
        /// <summary>
        /// 问卷助记码
        /// </summary>
        [StringLength(50)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 序号
        /// </summary>        
      
        public virtual int? OrderNumber { get; set; }

        /// <summary>
        /// 套餐集合
        /// </summary>       
        public virtual ICollection<TbmItemSuit> ItemSuits { get; set; }

        public int TenantId { get; set; }
    }
}
