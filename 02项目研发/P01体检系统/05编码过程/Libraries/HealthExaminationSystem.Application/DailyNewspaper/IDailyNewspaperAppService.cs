using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper.Dto;
namespace Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper
{
    /// <summary>
    /// 日报月报
    /// </summary>
    public interface IDailyNewspaperAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 查询收费记录
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        List<SearchMReceiptInfoDto> GetMReceiptInfo(QuerySunMoon Query);
    }
}
