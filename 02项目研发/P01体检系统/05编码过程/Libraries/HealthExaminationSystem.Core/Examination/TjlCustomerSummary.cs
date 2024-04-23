using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 体检人专科建议
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TjlCustomerSummary : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约id
        /// </summary>
        [ForeignKey(nameof(CustomerRegBM))]
        public virtual Guid? CustomerRegBMId { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        [ForeignKey(nameof(DepartmentBM))]
        public virtual Guid? DepartmentBMId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual TbmDepartment DepartmentBM { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentCodeBM { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }
        /// <summary>
        /// 专科建议
        /// </summary>
        [MaxLength(3072)]
        public virtual string CharacterSummary { get; set; }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}