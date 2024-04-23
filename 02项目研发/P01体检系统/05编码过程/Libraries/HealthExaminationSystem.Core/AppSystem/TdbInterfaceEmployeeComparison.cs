using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 接口医生对应
    /// </summary>
    public class TdbInterfaceEmployeeComparison : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual User Employee { get; set; }

        /// <summary>
        /// 操作人员ID:int?
        /// </summary>
        [ForeignKey(nameof(Employee))]
        public virtual long EmployeeId { get; set; }

        /// <summary>
        /// 操作人员名称
        /// </summary>
        [MaxLength(64)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 对应操作人员ID
        /// </summary>
        public virtual string ObverseEmpId { get; set; }

        /// <summary>
        /// 对应操作人员名称
        /// </summary>
        [MaxLength(64)]
        public virtual string ObverseEmpName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}