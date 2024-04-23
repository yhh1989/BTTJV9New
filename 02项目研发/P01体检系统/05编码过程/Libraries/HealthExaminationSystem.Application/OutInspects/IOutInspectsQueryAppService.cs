using Sw.Hospital.HealthExaminationSystem.Application.OutInspects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OutInspects
{
   public interface IOutInspectsQueryAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        List<OutinspectsQueryDto> OutinspectsQuery(OutCusInfoDto input);
    }
}
