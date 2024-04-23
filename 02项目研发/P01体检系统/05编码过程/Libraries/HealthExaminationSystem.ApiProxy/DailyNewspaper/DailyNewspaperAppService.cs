using Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper;
using Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.DailyNewspaper
{
    /// <summary>
    /// 日报月报
    /// </summary>
    public class DailyNewspaperAppService : AppServiceApiProxyBase, IDailyNewspaperAppService
    {
        /// <summary>
        /// 查询收费记录
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<SearchMReceiptInfoDto> GetMReceiptInfo(QuerySunMoon Query)
        {
            return GetResult<QuerySunMoon, List<SearchMReceiptInfoDto>>(Query, DynamicUriBuilder.GetAppSettingValue());
        }

    }
}
