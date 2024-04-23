using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.HistoryComparison
{
    public class HistoryComparisonAppService : AppServiceApiProxyBase, IHistoryComparisonAppService
    {
        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public List<SearchCustomerRegDto> GetCustomerRegList(SearchClass Search)
        {
            return GetResult<SearchClass, List<SearchCustomerRegDto>>(Search, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<HistoryResultMainDto> GetHistoryResultList(SearchClass Search)
        {
            return GetResult<SearchClass, List<HistoryResultMainDto>>(Search, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<HistoryResultMainDto> geHisvard(SearchHisClassDto input)
        {
            return GetResult<SearchHisClassDto, List<HistoryResultMainDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutHisValuesDto> getHisValue(InSerchHIsDto input)
        {
            return GetResult<InSerchHIsDto, List<OutHisValuesDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public HisDbDto geHisvardReport(SearchHisClassDto input)
        {
            return GetResult<SearchHisClassDto, HisDbDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}
