using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics
{
   public interface IOccConclusionStatisticsAppService
#if !Proxy
         : Abp.Application.Services.IApplicationService
#endif
    {
        List<OccConclusionStatisticsShowDto> GetCus(OccStatisticsShowGet input);
        List<DQQuery> StatisticalChar(OccStatisticsShowGet input);

        OccYearsAllStaticsDto getOCCYears(OccYearsStatisticsDto input);
    }
}
