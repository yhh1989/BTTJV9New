using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Company;

namespace Sw.Hospital.HealthExaminationSystem.Core.Market
{
    /// <summary>
    /// 合同信息
    /// </summary>
    [Table(name: "ContractInformation")]
    public class ContractInformation : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 附件集合
        /// </summary>
        [InverseProperty(property: nameof(ContractAdjunct.Contract))]
        public virtual ICollection<ContractAdjunct> AdjunctCollection { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Column(name: "Amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual ContractCategory Category { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public virtual TjlClientInfo Company { get; set; }

        /// <summary>
        /// 机构标识
        /// </summary>
        [Column(name: "CompanyId")]
        [ForeignKey(name: nameof(Company))]
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 公司预约
        /// </summary>
        public virtual TjlClientReg CompanyRegister { get; set; }

        /// <summary>
        /// 公司预约标识
        /// </summary>
        [Column(name: "CompanyRegisterId")]
        [ForeignKey(name: nameof(CompanyRegister))]
        public Guid? CompanyRegisterId { get; set; }

        /// <summary>
        /// 合同类别标识
        /// </summary>
        [Column(name: "ContractCategoryId")]
        [ForeignKey(name: nameof(Category))]
        public Guid ContractCategoryId { get; set; }

        /// <summary>
        /// 重要事项
        /// </summary>
        [StringLength(maximumLength: 1024)]
        [Column(name: "ImportantMatter")]
        public string ImportantMatter { get; set; }

        /// <summary>
        /// 开票记录集合
        /// </summary>
        [InverseProperty(property: nameof(ContractInvoice.Contract))]
        public virtual ICollection<ContractInvoice> InvoiceCollection { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        [Required]
        [StringLength(maximumLength: 64)]
        [Column(name: "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(maximumLength: 64)]
        [Column(name: "Number")]
        public string Number { get; set; }

        /// <summary>
        /// 回款记录集合
        /// </summary>
        [InverseProperty(property: nameof(ContractProceeds.Contract))]
        public virtual ICollection<ContractProceeds> ProceedsCollection { get; set; }

        /// <summary>
        /// 签字代表
        /// </summary>
        [StringLength(maximumLength: 64)]
        [Column(name: "Signatory")]
        public string Signatory { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        [Column(name: "ValidTime")]
        public DateTime? ValidTime { get; set; }
    }
}