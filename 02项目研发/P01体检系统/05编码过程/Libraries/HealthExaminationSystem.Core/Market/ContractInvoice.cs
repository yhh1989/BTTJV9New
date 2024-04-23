using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Market
{
    /// <summary>
    /// 合同开票记录
    /// </summary>
    public class ContractInvoice : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 金额
        /// </summary>
        [Column(name: "Amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 合同
        /// </summary>
        [InverseProperty(property: nameof(ContractInformation.InvoiceCollection))]
        public virtual ContractInformation Contract { get; set; }

        /// <summary>
        /// 合同标识
        /// </summary>
        [Column(name: "ContractId")]
        [ForeignKey(name: nameof(Contract))]
        public Guid ContractId { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        [Column(name: "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        [StringLength(maximumLength: 64)]
        [Column(name: "InvoiceNumber")]
        public string InvoiceNumber { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}