using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 工种车间
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TbmOWorkType : AuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        public virtual string WorkName { get; set; }


        /// <summary>
        /// 类别 1工种2车间3行业
        /// </summary>
        public virtual int? ZyWorkTypes { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? Ordernum { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [MaxLength(32)]
        public virtual string WorkNamejp { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}