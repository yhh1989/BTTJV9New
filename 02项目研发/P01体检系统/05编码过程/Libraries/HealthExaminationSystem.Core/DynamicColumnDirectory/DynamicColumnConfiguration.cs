using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.DynamicColumnDirectory
{
    /// <summary>
    /// 动态列配置
    /// </summary>
    [Table("DynamicColumnConfiguration")]
    public class DynamicColumnConfiguration : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 表格视图标识
        /// </summary>
        [Column("GridViewId")]
        [StringLength(64)]
        public string GridViewId { get; set; }

        /// <summary>
        /// 表格视图列名称呢个
        /// </summary>
        [Column("GridViewColumnName")]
        [StringLength(64)]
        public string GridViewColumnName { get; set; }

        /// <summary>
        /// 可见的
        /// </summary>
        [Column("Visible")]
        public bool Visible { get; set; }

        /// <summary>
        /// 可见的顺序值
        /// </summary>
        [Column("VisibleIndex")]
        public int VisibleIndex { get; set; }

        /// <summary>
        /// 固定列
        /// </summary>
        [Column("Fixed")]
        public bool Fixed { get; set; }

        /// <summary>
        /// 左边固定
        /// </summary>
        [Column("IsLeft")]
        public bool IsLeft { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}