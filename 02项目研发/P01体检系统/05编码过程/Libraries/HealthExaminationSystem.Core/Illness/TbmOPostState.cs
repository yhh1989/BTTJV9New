using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 岗位类别
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TbmOPostState : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 岗位类别
        /// </summary>
        [MaxLength(32)]
        public virtual string PostStateName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}