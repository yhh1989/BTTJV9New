using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Market
{
    /// <summary>
    /// 合同类别
    /// </summary>
    [Table(name: "ContractCategory")]
    public class ContractCategory : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 合同列表名称
        /// </summary>
        [Required]
        [StringLength(maximumLength: 64)]
        [Column(name: "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(maximumLength: 32)]
        [Column(name: "MnemonicCode")]
        public string MnemonicCode { get; set; }
    }
}