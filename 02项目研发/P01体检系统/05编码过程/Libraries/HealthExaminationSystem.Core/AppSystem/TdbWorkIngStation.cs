using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 客户端正在运行表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TdbWorkIngStation : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        [MaxLength(64)]
        public virtual string WorkStationIP { get; set; }

        /// <summary>
        /// 客户端MAC地址
        /// </summary>
        [MaxLength(512)]
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
        /// 当前登录用户
        /// </summary>
        [MaxLength(64)]
        public virtual string LogonUser { get; set; }

        /// <summary>
        /// 当前登录时间
        /// </summary>
        public virtual DateTime LogonTime { get; set; }

        /// <summary>
        /// 最后刷新时间
        /// </summary>
        public virtual DateTime LastTouchTime { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}