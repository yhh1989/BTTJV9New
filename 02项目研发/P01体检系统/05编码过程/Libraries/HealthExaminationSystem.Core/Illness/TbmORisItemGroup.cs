using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 职业危害因素检查项目
    /// </summary>
    [Obsolete("暂停使用")]
    public class TbmORisItemGroup : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 职业健康结论id
        /// </summary>
        private TbmOConDictionary OConDictionary { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [MaxLength(64)]
        public virtual string OPostStates { get; set; }

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

        /// <summary>
        /// 项目类型 体格检查、必检项目、选检项目
        /// </summary>
        [MaxLength(64)]
        public virtual string ItemType { get; set; }

        /// <summary>
        /// 检查组合编码
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 检查组合
        /// </summary>
        public TbmItemGroup ItemGroup { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}