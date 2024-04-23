using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 危害因素
    /// </summary>
    public class TbmORiskFactor : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(32)]
        public virtual string RiskCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        public virtual string RiskName { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [MaxLength(32)]
        public virtual string RiskNameJP { get; set; }

        /// <summary>
        /// 类别id
        /// </summary>
        [Obsolete("暂停使用", true)]
        public virtual TbmORiskFactorType RiskTypeID { get; set; }

        /// <summary>
        /// 类别id
        /// </summary>
        public virtual string RiskTypeIDs { get; set; }

        /// <summary>
        /// 检测资料
        /// </summary>
        [MaxLength(64)]
        public virtual string Remark { get; set; }


        /// <summary>
        /// 工种
        /// </summary>
        [MaxLength(32)]
        [Obsolete("暂停使用", true)]
        public virtual TbmOWorkType zyWorkTypeName { get; set; }

        /// <summary>
        /// 介绍说明
        /// </summary>
        [MaxLength(128)]
        public virtual string WHContent { get; set; }

        /// <summary>
        /// 防护措施
        /// </summary>
        [MaxLength(64)]
        public virtual string WHadvice { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [Obsolete("暂停使用", true)]
        public virtual TbmOPostState PostStateID { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [MaxLength(64)]
        [Obsolete("暂停使用", true)]
        public virtual string PostStateIDs { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? Ordernum { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}