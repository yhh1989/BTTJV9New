using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 报告领取
    /// </summary>
    public class TjlCustomerReportCollection : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人体检标识
        /// </summary>
        [ForeignKey(nameof(CustomerReg))]
        public virtual Guid CustomerRegId { get; set; }

        /// <summary>
        /// 体检人体检
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 报告人姓名
        /// </summary>
        [MaxLength(32)]
        public virtual string Reportname { get; set; }

        /// <summary>
        /// 交接时间
        /// </summary>
        public virtual DateTime? HandoverTime { get; set; }

        /// <summary>
        /// 交接人
        /// </summary>
        [MaxLength(32)]
        public virtual string Handover { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [MaxLength(32)]
        public virtual string Receiptor { get; set; }

        /// <summary>
        /// 发单时间
        /// </summary>
        public virtual DateTime? SendTime { get; set; }

        /// <summary>
        /// 发单人
        /// </summary>
        [MaxLength(32)]
        public virtual string Sender { get; set; }

        /// <summary>
        /// 领取人
        /// </summary>
        [MaxLength(32)]
        public virtual string Receiver { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 手写签字
        /// </summary>
        public virtual string Sign { get; set; }

        /// <summary>
        /// 状态 0无1收取2发放
        /// </summary>
        public virtual int State { get; set; }

        /// <summary>
        /// 报表类别 1导引单2报告3团报
        /// </summary>
        public virtual int? ReportType { get; set; }

        /// <summary>
        /// 报告存放箱
        /// </summary>
        [MaxLength(64)]
        public virtual string Storagebox { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}