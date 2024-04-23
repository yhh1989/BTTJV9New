using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 职业健康字典
    /// </summary>
    public class WorkType : AuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(32)]
        public virtual string Num { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 类别
        /// <para>说明：</para>
        /// <para>0 工种</para>
        /// <para>1 车间</para>
        /// <para>2 行业</para>
        /// </summary>
        public virtual int? Category { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? Order { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [MaxLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(32)]
        public virtual string Content { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}