using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 客户端记录表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TdbWorkStation : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        [MaxLength(64)]
        public virtual string WorkStationIP { get; set; }

        /// <summary>
        /// 客户端MAC地址
        /// </summary>
        [MaxLength(64)]
        public virtual string WorkStationMac { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        [MaxLength(64)]
        public virtual string WorkStationName { get; set; }

        /// <summary>
        /// 客户端CPU
        /// </summary>
        [MaxLength(64)]
        public virtual string WorkStationCPU { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1024)]
        public virtual string Note { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}