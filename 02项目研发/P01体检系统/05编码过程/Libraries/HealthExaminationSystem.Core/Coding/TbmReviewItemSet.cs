using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 复查项目设置
    /// </summary>
    public class TbmReviewItemSet : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 疾病名称
        /// </summary>    
        [ForeignKey(nameof(SummarizeAdvice))]
        public virtual Guid? SummarizeAdviceId { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(256)]
        public virtual string IllName { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public virtual TbmSummarizeAdvice SummarizeAdvice { get; set; }

        /// <summary>
        /// 复查周期/天
        /// </summary>       
        public virtual int? Checkday { get; set; }

        /// <summary>
        /// 回访周期/天
        /// </summary>      
        public virtual int? KFday { get; set; }
        /// <summary>
        /// 备注
        /// </summary>      
        [StringLength(256)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 复查组合ID
        /// </summary>
        [InverseProperty(nameof(TbmItemGroup.ReviewItemSet))]
        public virtual ICollection<TbmItemGroup> ItemGroupBM { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}