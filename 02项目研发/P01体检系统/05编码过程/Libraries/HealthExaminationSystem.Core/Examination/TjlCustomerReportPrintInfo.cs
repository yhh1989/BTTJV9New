using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 报告记录表
    /// </summary>
    public class TjlCustomerReportPrintInfo : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey(nameof(CustomerRegBM))]
        public virtual Guid? CustomerRegBmId { get; set; }

        /// <summary>
        /// 打印人员
        /// </summary>
        public virtual User EmployeeBM { get; set; }

        /// <summary>
        /// 打印人员标识
        /// </summary>
        [ForeignKey(nameof(EmployeeBM))]
        public virtual long? EmployeeBmId { get; set; }

        /// <summary>
        /// 补打原因
        /// </summary>
        [MaxLength(256)]
        public virtual string CauseRepair { get; set; }
        /// <summary>
        /// 报告类型1个人报告2通知单
        /// </summary>
        public virtual int? ReportType { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}