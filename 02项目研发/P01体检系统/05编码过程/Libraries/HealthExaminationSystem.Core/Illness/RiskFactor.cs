using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.Extensions.Options;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 危害因素
    /// </summary>
    public class RiskFactor : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [StringLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 检测资料
        /// </summary>
        [StringLength(64)]
        public virtual string Data { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public virtual WorkType WorkType { get; set; }

        /// <summary>
        /// 工种id
        /// </summary>
        [ForeignKey(nameof(WorkType))]
        public virtual Guid? WorkTypeId { get; set; }

        /// <summary>
        /// 介绍说明
        /// </summary>
        [StringLength(256)]
        public virtual string Describe { get; set; }

        /// <summary>
        /// 防护措施
        /// </summary>
        [StringLength(256)]
        public virtual string ProtectiveMeasures { get; set; }

        ///// <summary>
        ///// 岗位类别
        ///// </summary>
        //public virtual JobCategory JobCategory { get; set; }

        ///// <summary>
        ///// 岗位类别id
        ///// </summary>
        //[ForeignKey(nameof(JobCategory))]
        //public virtual Guid? JobCategoryId { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? Order { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 体检人预约危害因素记录
        /// </summary>
        [InverseProperty(nameof(TjlCustomerRegRiskFactors.RiskFactor))]
        public virtual ICollection<TjlCustomerRegRiskFactors> CustomerRegisterRiskFactors { get; set; }

        /// <summary>
        /// 职业健康包含项目
        /// </summary>
        [InverseProperty(nameof(Sw.Hospital.HealthExaminationSystem.Core.Illness.OccupationalDiseaseIncludeItemGroup.RiskFactor))]
        public virtual ICollection<OccupationalDiseaseIncludeItemGroup> OccupationalDiseaseIncludeItemGroup { get; set; }
        //public virtual IConfigureOptions<OccupationalDiseaseIncludeItemGroup> OccupationalDiseaseIncludeItemGroup { get; set; }
    }
}