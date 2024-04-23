using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 职业健康包含的项目组合
    /// </summary>
    public class OccupationalDiseaseIncludeItemGroup : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <summary>
        /// 危害因素标识
        /// </summary>
        [ForeignKey(nameof(RiskFactor))]
        public Guid RiskFactorId { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual RiskFactor RiskFactor { get; set; }

        /// <summary>
        /// 岗位类别标识
        /// </summary>
        [ForeignKey(nameof(JobCategory))]
        public Guid JobCategoryId { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual JobCategory JobCategory { get; set; }

        /// <summary>
        /// 包含的必填项目
        /// </summary>
        [InverseProperty(nameof(TbmItemGroup.MustHaveOccupationalDiseaseIncludeItemGroups))]
        public virtual ICollection<TbmItemGroup> MustHaveItemGroups { get; set; }

        /// <summary>
        /// 包含的可选项目
        /// </summary>
        [InverseProperty(nameof(TbmItemGroup.MayHaveOccupationalDiseaseIncludeItemGroups))]
        public virtual ICollection<TbmItemGroup> MayHaveItemGroups { get; set; }

        /// <summary>
        /// 症状询问列表
        /// </summary>
        [InverseProperty(nameof(Symptom.OccupationalDiseaseIncludeItemGroups))]
        public virtual ICollection<Symptom> Symptoms { get; set; }

        /// <summary>
        /// 体检人体系统列表
        /// </summary>
        [InverseProperty(nameof(HumanBodySystem.OccupationalDiseaseIncludeItemGroups))]
        public virtual ICollection<HumanBodySystem> HumanBodySystems { get; set; }

        /// <summary>
        /// 目标疾病与禁忌证列表
        /// </summary>
        [InverseProperty(nameof(DiseaseContraindicationExplain.OccupationalDiseaseIncludeItemGroups))]
        public virtual ICollection<DiseaseContraindicationExplain> DiseaseContraindicationExplains { get; set; }
    }
}