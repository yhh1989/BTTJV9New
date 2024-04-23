using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 团体报表打印记录数据传输对象
    /// </summary>
#if Application
    [AutoMapFrom(typeof(CompanyReportPrintRecord))]
#endif
    public class CompanyReportPrintRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 团体预约标识
        /// </summary>
        public Guid CompanyRegisterId { get; set; }

        /// <summary>
        /// 报表流水号
        /// </summary>
        [StringLength(64)]
        public string ReportSerialNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }
}