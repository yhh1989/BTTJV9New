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
    /// 照射种类
    /// </summary>
 public   class TbmRadiation : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50)]
        public virtual string code { get; set; }
        /// <summary>
        /// 平台编码
        /// </summary>
        [StringLength(50)]
        public virtual string ComCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(500)]
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>      
        public virtual int IsActive { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 父级单位标识（照射源）
        /// </summary>
        [ForeignKey(nameof(Parent))]
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual TbmOccDictionary Parent { get; set; }
       

        /// <inheritdoc />
        public virtual int TenantId { get; set; }
    }
}
