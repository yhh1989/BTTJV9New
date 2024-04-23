using System;
using System.ComponentModel.DataAnnotations;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 创建团体报表打印记录数据传输对象
    /// </summary>
#if Application
    [AutoMap(typeof(CompanyReportPrintRecord))]
#endif
    public class CreateCompanyReportPrintRecordDto
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
    }
}