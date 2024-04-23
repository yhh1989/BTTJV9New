using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;

namespace Sw.Hospital.HealthExaminationSystem.Core.Scheduling
{
    /// <summary>
    /// 人工行程安排
    /// </summary>
    [Table("ManualScheduling")]
    public class ManualScheduling : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 租户标识
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 个人排期的名称
        /// </summary>
        [Column("Name")]
        [StringLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 体检人数
        /// </summary>
        [Column("NumberOfCustomer")]
        public int NumberOfCustomer { get; set; }

        /// <summary>
        /// 排期日期
        /// </summary>
        [Column("SchedulingDate")]
        public DateTime SchedulingDate { get; set; }

        /// <summary>
        /// 单位标识
        /// </summary>
        [Column("CompanyId")]
        [ForeignKey(nameof(Company))]
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual TjlClientInfo Company { get; set; }

        /// <summary>
        /// 科室集合
        /// </summary>
        [InverseProperty(nameof(TbmDepartment.ManualSchedulingCollection))]
        public virtual ICollection<TbmDepartment> DepartmentCollection { get; set; }
    }
}