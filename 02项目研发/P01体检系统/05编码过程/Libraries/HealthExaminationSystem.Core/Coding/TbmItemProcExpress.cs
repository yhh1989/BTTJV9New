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
    /// 项目计算型公式设置
    /// </summary>
    public class TbmItemProcExpress : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 项目标识
        /// </summary>      
        public virtual Guid? ItemId { get; set; }
       /// <summary>
       /// 计算型项目名称
       /// </summary>
        public virtual string ItemName { get; set; }        
        /// <summary>
        /// 项目信息
        /// </summary>   
        public virtual ICollection<TbmItemInfo> ItemInfoReRelations { get; set; }
        /// <summary>
        /// 公式名称
        /// </summary>      
        [StringLength(640)]
        public virtual string FormulaText { get; set; }
        /// <summary>
        /// 公式值
        /// </summary>      
        [StringLength(640)]
        public virtual string FormulaValue { get; set; }
        public int TenantId { get; set; }
    }
}
