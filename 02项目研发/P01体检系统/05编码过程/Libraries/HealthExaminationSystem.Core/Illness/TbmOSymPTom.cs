using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 症状字典
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TbmOSymPTom : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        public virtual string SymptomName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}