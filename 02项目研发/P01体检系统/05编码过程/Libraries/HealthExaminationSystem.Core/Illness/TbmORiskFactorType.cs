using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 危害因素类别
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TbmORiskFactorType : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        ///// <summary>
        ///// 体检人科室小结
        ///// </summary>
        //public virtual ICollection<RiskFactor> RiskFactors { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [MaxLength(32)]
        public virtual string RiskTypeName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}