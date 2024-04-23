using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Market
{
    /// <summary>
    /// 合同附件
    /// </summary>
    [Table(name: "ContractAdjunct")]
    public class ContractAdjunct : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 文件的长度（以字节为单位）。
        /// </summary>
        [Column(name: "ContentLength")]
        public int ContentLength { get; set; }

        /// <summary>
        /// 合同
        /// </summary>
        [InverseProperty(property: nameof(ContractInformation.AdjunctCollection))]
        public virtual ContractInformation Contract { get; set; }

        /// <summary>
        /// 合同标识
        /// </summary>
        [Column(name: "ContractId")]
        [ForeignKey(name: nameof(Contract))]
        public Guid ContractId { get; set; }

        /// <summary>
        /// 文件的 MIME 内容类型。
        /// </summary>
        [StringLength(maximumLength: 64)]
        [Column(name: "ContentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [StringLength(maximumLength: 128)]
        [Column(name: "Name")]
        public string Name { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [StringLength(maximumLength: 256)]
        [Column(name: "FileName")]
        public string FileName { get; set; }
    }
}