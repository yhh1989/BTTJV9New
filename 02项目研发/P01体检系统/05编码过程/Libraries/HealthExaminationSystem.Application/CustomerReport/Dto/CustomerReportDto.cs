using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerReportCollection))]
#endif
    public class CustomerReportDto : EntityDto<Guid>
    {
        public virtual Guid CustomerRegId { get; set; }

        /// <summary>
        /// 报告人姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Reportname { get; set; }

        /// <summary>
        /// 交接时间
        /// </summary>
        public virtual DateTime? HandoverTime { get; set; }

        /// <summary>
        /// 交接人
        /// </summary>
        [StringLength(32)]
        public virtual string Handover { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [StringLength(32)]
        public virtual string Receiptor { get; set; }

        /// <summary>
        /// 发单时间
        /// </summary>
        public virtual DateTime? SendTime { get; set; }

        /// <summary>
        /// 发单人
        /// </summary>
        [StringLength(32)]
        public virtual string Sender { get; set; }

        /// <summary>
        /// 领取人
        /// </summary>
        [StringLength(32)]
        public virtual string Receiver { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
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
        [StringLength(64)]
        public virtual string Storagebox { get; set; }

    }
}
