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
    /// 职业病
    /// </summary>
    public class TbmOccDisease : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 字典类别，来自程序的枚举
        /// </summary>
        public virtual string Type { get; set; }

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
        /// 父级单位标识（职业健康大类）
        /// </summary>
        [ForeignKey(nameof(Parent))]
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual TbmOccDictionary Parent { get; set; }

        /// <summary>
        /// 职业健康标准
        /// </summary>
        public virtual ICollection<TbmOccStandard> TbmOccStandards { get; set; }

        /// <summary>
        /// 目标疾病
        /// </summary>
        public virtual ICollection<TbmOccTargetDisease> OccTargetDisease { get; set; }


        /// <summary>
        /// 危害因素结论 
        /// </summary>
        public virtual List<TjlOccCustomerHazardSum> OccCustomerOccDiseases { get; set; }

     
        /// <inheritdoc />
        public virtual int TenantId { get; set; }
    }
}
