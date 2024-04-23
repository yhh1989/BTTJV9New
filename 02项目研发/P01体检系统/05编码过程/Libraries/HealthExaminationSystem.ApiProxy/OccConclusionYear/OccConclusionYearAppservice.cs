using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionYear
{
   public class OccConclusionYearAppService : AppServiceApiProxyBase, IOccConclusionYearAppService
    {
       public List<OutOccMothDto> GetOccAbnormalResult(OccConclusionYearShowDto input)
        {
            return GetResult<OccConclusionYearShowDto, List<OutOccMothDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
