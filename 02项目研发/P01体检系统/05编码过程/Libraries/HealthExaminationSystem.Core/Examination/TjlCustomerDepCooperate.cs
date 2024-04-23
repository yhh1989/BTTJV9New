using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 体检会诊协作
    /// </summary>
    public class TjlCustomerDepCooperate : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual TbmDepartment DepartmentBM { get; set; }

        /// <summary>
        /// 诊断级别 1正常2阳性3疾病4重大疾病
        /// </summary>
        public virtual int? DagnosisState { get; set; }

        /// <summary>
        /// 需要协助科室编码
        /// </summary>
        [StringLength(256)]
        public virtual TbmDepartment XZDepartmentBM { get; set; }

        /// <summary>
        /// 协作备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}