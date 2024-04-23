using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition
{
    public interface IOccDisRequisitionAppService
#if !Proxy
        : IApplicationService
#endif
    {
        List<OutOccCustomerSumDto> GetOccDisRequisition(OutOccCustomerSumDto input);
    }
}
