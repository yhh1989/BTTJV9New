using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition
{
   
    public class OccDisRequisitionAppService : AppServiceApiProxyBase, IOccDisRequisitionAppService
    {

       public List<OutOccCustomerSumDto> GetOccDisRequisition(OutOccCustomerSumDto input)
        {
            return GetResult<OutOccCustomerSumDto, List<OutOccCustomerSumDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
