using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// ID生成控制表
    /// </summary>
    public class TdbIdNumber : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// Id名称
        /// </summary>
        [StringLength(64)]
        public virtual string IDName { get; set; }

        /// <summary>
        /// 类别id
        /// </summary>
        [StringLength(64)]
        public virtual string IDType { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        [StringLength(64)]
        public virtual string prefix { get; set; }

        /// <summary>
        /// 日期前缀 yyyyMMdd-4
        /// </summary>
        [StringLength(64)]
        public virtual string Dateprefix { get; set; }

        /// <summary>
        /// Id值
        /// </summary>
        public virtual int IDValue { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime? IDTime { get; set; }
        /// <summary>
        /// 数据行版本号
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}