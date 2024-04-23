using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sw.Hospital.HealthExaminationSystem.Core.Occupational
{
    /// <summary>
    /// 职业健康总检-职业健康
    /// </summary>  

        public class TjlOccCustomerOccDiseases : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 职业健康总检Id
        /// </summary>
        [ForeignKey("OccCustomerSum")]
        public virtual Guid? OccCustomerSumId { get; set; }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual TjlOccCustomerSum OccCustomerSum { get; set; }


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
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }


        /// <summary>
        /// 父级单位
        /// </summary>
        [StringLength(500)]
        public virtual string ParentName { get; set; }

        /// <summary>
        /// 职业健康标准
        /// </summary>
        [StringLength(500)]
        public virtual string TbmOccStandard { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
