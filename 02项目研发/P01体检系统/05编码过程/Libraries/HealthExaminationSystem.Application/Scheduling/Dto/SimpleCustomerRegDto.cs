using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{

#if !Proxy
    [AutoMapFrom(typeof(TjlCustomerReg))]
#endif
    public class SimpleCustomerRegDto : EntityDto<Guid>
    {

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual SimpleCustomerDto Customer { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

    }
}
