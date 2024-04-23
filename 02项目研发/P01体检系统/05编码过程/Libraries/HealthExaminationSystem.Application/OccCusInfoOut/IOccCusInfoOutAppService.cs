using Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut
{
  public   interface IOccCusInfoOutAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        OutOccAllDto getOccCusInfoDto(InOccSearchDto input);
    }
}
