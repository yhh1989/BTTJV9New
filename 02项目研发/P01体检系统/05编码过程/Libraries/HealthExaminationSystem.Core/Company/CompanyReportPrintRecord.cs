using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Company
{
    /// <summary>
    /// 团体报表打印记录
    /// </summary>
    [Table("CompanyReportPrintRecords")]
    public class CompanyReportPrintRecord : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 团体预约标识
        /// </summary>
        [Column("CompanyRegisterId")]
        public Guid CompanyRegisterId { get; set; }

        /// <summary>
        /// 报表流水号
        /// </summary>
        [StringLength(64)]
        [Column("ReportSerialNumber")]
        public string ReportSerialNumber { get; set; }
    }
}