using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 版本维护表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TdbVersion : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 软件名称
        /// </summary>
        [MaxLength(64)]
        public virtual string keyName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(1024)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [MaxLength(64)]
        public virtual string Version { get; set; }

        /// <summary>
        /// 最后脚本时间
        /// </summary>
        public virtual DateTime LastSQLDate { get; set; }

        /// <summary>
        /// 最后升级时间
        /// </summary>
        public virtual DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid ModuleID { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}