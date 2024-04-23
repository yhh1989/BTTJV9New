using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget
{
   public interface IOccConclusionTargetAppService
#if !Proxy
         : Abp.Application.Services.IApplicationService
#endif
    {
        List<OccConclusionTargetDto> getTargetCount(TargetGetDto input);
    }
}
