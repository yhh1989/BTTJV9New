using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 职业总检结论字典
    /// </summary>
    [Obsolete("暂停使用")]
    public class TbmOConDictionary : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人项目
        /// </summary>
        public virtual ICollection<TbmORisItemGroup> ORisItemGroup { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [MaxLength(64)]
        [Obsolete("暂停使用", true)]
        public virtual JobCategory OPostState { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [MaxLength(64)]
        public virtual string OPostStates { get; set; }

        /// <summary>
        /// 职业健康
        /// </summary>
        [MaxLength(64)]
        public virtual string ZyContents { get; set; }

        /// <summary>
        /// 职业健康禁忌证
        /// </summary>
        [MaxLength(64)]
        public virtual string ZyjjContents { get; set; }

        /// <summary>
        /// 问诊提示
        /// </summary>
        [MaxLength(256)]
        public virtual string Symptom { get; set; }

        ///// <summary>
        ///// 危害因素标识
        ///// </summary>
        //[ForeignKey(nameof(RiskFactor))]
        //public Guid RiskFactorId { get; set; }

        ///// <summary>
        ///// 危害因素
        ///// </summary>
        //public virtual RiskFactor RiskFactor { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        [MaxLength(64)]
        public virtual string RiskNames { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}