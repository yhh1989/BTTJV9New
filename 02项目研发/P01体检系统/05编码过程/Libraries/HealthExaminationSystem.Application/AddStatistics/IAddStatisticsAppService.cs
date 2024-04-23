using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.AddStatistics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.AddStatistics
{
    /// <summary>
    /// 
    /// </summary>
   public  interface IAddStatisticsAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 加项统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<AddStatisticsDto> AddStatisticsList(SearchStatisticsDto input);
    }
}
