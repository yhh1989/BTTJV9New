using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
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
    /// 危害因素 
    /// </summary>
   public class TbmOccHazardFactor : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// CAS编码
        /// </summary>
        [StringLength(50)]
        public virtual string CASBM { get; set; }

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
        /// 父级单位标识（危害因素分类）
        /// </summary>
        [ForeignKey(nameof(Parent))]
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual TbmOccDictionary Parent { get; set; }
        /// <summary>
        /// 防护措施
        /// </summary>
        public virtual ICollection<TbmOccHazardFactorsProtective> Protectivis { get; set; }
        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual ICollection<TjlClientTeamInfo> TjlClientTeamInfos { get; set; }

        /// <summary>
        /// 体检预约
        /// </summary>
        public virtual ICollection<TjlCustomerReg> TjlCustomerRegs { get; set; }


        /// <inheritdoc />
        public virtual int TenantId { get; set; }

    }
}
