using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionSuspected.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionSuspected
{
   public interface IOccConclusionSuspectedAppService
#if !Proxy
         : Abp.Application.Services.IApplicationService
#endif
    {
        List<OccConclusionSuspectedShow> GetOccConclusionSuspected(OccSuspectedGet input);
    }
}
