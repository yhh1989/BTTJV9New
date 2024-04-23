using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult.Dto;
using System;
using System.Collections.Generic;


namespace Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult
{

    public class OccAbnormalResultAppService : AppServiceApiProxyBase, IOccAbnormalResultAppService
    {
        public List<OutOccAbnormalResult> GetOccAbnormalResult(OutOccAbnormalResult input)
        {
            return GetResult<OutOccAbnormalResult, List<OutOccAbnormalResult>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}

