using Sw.Hospital.HealthExaminationSystem.Application.AddStatistics;
using Sw.Hospital.HealthExaminationSystem.Application.AddStatistics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.AddStatistics
{
  public  class AddStatisticsAppService:AppServiceApiProxyBase, IAddStatisticsAppService
    {
        /// <summary>
        /// 加项统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AddStatisticsDto> AddStatisticsList(SearchStatisticsDto input)
        {
            return GetResult<SearchStatisticsDto, List<AddStatisticsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
