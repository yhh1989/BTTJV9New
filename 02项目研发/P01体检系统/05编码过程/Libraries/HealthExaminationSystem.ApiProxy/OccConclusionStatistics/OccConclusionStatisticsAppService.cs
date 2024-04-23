using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionStatistics
{
   public class OccConclusionStatisticsAppService : AppServiceApiProxyBase, IOccConclusionStatisticsAppService
    {

        public List<OccConclusionStatisticsShowDto> GetCus(OccStatisticsShowGet input)
        {
            return GetResult<OccStatisticsShowGet, List<OccConclusionStatisticsShowDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
       public List<DQQuery> StatisticalChar(OccStatisticsShowGet input)
        {
            return GetResult<OccStatisticsShowGet, List<DQQuery>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OccYearsAllStaticsDto getOCCYears(OccYearsStatisticsDto input)
        {
            return GetResult<OccYearsStatisticsDto, OccYearsAllStaticsDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}
