using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 组单表
    /// <para>组单 -1-n- 组单分组 -1-n- 分组项目 - 1-1- 项目组合</para>
    /// </summary>
    public class TbmCompose : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 组单分组集合
        /// </summary>
        public virtual ICollection<TbmComposeGroup> ComposeGroups { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(256)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(64)]
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        public int TenantId { get; set; }
    }
}