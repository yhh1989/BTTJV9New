using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionTarget
{
   public class OccConclusionTargetAppService : AppServiceApiProxyBase, IOccConclusionTargetAppService
    {
        public List<OccConclusionTargetDto> getTargetCount(TargetGetDto input)
        {
            return GetResult<TargetGetDto, List<OccConclusionTargetDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

    }
}
