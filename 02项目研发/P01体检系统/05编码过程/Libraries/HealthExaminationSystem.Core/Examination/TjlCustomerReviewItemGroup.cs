using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 复查项目
    /// </summary>
    public class TjlCustomerReviewItemGroup : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}