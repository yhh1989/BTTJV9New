using Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut;
using Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccCusInfoOut
{
    public class OccCusInfoOutAppService : AppServiceApiProxyBase, IOccCusInfoOutAppService
    {
     public OutOccAllDto getOccCusInfoDto(InOccSearchDto input)
        {
            return GetResult<InOccSearchDto, OutOccAllDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
