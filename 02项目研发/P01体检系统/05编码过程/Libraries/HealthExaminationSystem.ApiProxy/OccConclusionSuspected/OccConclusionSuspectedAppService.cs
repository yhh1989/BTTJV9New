using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionSuspected;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionSuspected.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionSuspected
{
  public  class OccConclusionSuspectedAppService : AppServiceApiProxyBase, IOccConclusionSuspectedAppService
    {
        public List<OccConclusionSuspectedShow> GetOccConclusionSuspected(OccSuspectedGet input)
        {
            return GetResult<OccSuspectedGet, List<OccConclusionSuspectedShow>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
