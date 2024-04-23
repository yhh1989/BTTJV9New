using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 人员类别 
    /// </summary>
    public class PersonnelCategory : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// 体检人预约集合
        /// </summary>
        [InverseProperty(nameof(TjlCustomerReg.PersonnelCategory))]
        public virtual ICollection<TjlCustomerReg> CustomerRegs { get; set; }

        /// <summary>
        /// 单位预约集合
        /// </summary>
        [InverseProperty(nameof(TjlClientReg.PersonnelCategory))]
        public virtual ICollection<TjlClientReg> ClientRegs { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }
    }
}