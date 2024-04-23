using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerReportCollection))]
#endif
    public class CustomerReportFullDto : CustomerReportDto
    {
        /// <summary>
        /// 体检人体检ID
        /// </summary>
        public virtual TjlCustomerRegDto CustomerReg { get; set; }

    }
}
