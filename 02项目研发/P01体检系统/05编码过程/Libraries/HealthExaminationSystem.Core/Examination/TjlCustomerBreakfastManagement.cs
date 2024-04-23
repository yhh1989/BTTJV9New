using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 早餐管理
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TjlCustomerBreakfastManagement : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }


        /// <summary>
        /// 用餐时间
        /// </summary>
        public virtual DateTime? BreakfastTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public virtual string Memo { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}