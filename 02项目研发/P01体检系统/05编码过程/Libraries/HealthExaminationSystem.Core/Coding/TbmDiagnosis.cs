using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 复合判断设置
    /// </summary>
    public class TbmDiagnosis : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 复合判断明细集合
        /// </summary>
        public virtual ICollection<TbmDiagnosisData> DiagnosisDatals { get; set; }

        /// <summary>
        /// 复合判断名称
        /// </summary>
        [StringLength(128)]
        public virtual string RuleName { get; set; }

        /// <summary>
        /// 复合判断结论
        /// </summary>
        [StringLength(128)]
        public virtual string Conclusion { get; set; }
        /// <summary>
        /// 复合判断序号
        /// </summary>
        public virtual string OrderNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}