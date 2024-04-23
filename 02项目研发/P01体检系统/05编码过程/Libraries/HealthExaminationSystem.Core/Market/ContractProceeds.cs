using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Market
{
    /// <summary>
    /// 合同回款记录
    /// </summary>
    [Table(name: "ContractProceeds")]
    public class ContractProceeds : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 金额
        /// </summary>
        [Column(name: "Amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 合同
        /// </summary>
        [InverseProperty(property: nameof(ContractInformation.ProceedsCollection))]
        public virtual ContractInformation Contract { get; set; }

        /// <summary>
        /// 合同标识
        /// </summary>
        [Column(name: "ContractId")]
        [ForeignKey(name: nameof(Contract))]
        public Guid ContractId { get; set; }

        /// <summary>
        /// 回款日期
        /// </summary>
        [Column(name: "Date")]
        public DateTime Date { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}