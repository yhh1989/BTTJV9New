using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// ID生成日志
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TdbIdLog : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// IDName
        /// </summary>
        [MaxLength(64)]
        public virtual string IDName { get; set; }

        /// <summary>
        /// Id类别
        /// </summary>
        [MaxLength(64)]
        public virtual string IDType { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        [MaxLength(64)]
        public virtual string IDAddress { get; set; }

        /// <summary>
        /// 分院编码
        /// </summary>
        public virtual Guid? StoreID { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}