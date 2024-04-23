using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 数据字典明细
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TbmDictionaryDetail : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 数据字段明细主键
        /// </summary>
        public virtual string DataDictionaryDetailId { get; set; }

        /// <summary>
        /// 数据字典主键
        /// </summary>
        public virtual string DataDictionaryId { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 有效
        /// </summary>
        public virtual int? Enabled { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public virtual int? SortCode { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}