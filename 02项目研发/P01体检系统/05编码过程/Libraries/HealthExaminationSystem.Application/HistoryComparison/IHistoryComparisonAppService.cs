using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison
{
    public interface IHistoryComparisonAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        List<SearchCustomerRegDto> GetCustomerRegList(SearchClass Search);
        /// <summary>
        /// 历年对比报告
        /// </summary>
        /// <param name="Search"></param>
        /// <returns></returns>
        List<HistoryResultMainDto> GetHistoryResultList(SearchClass Search);
        /// <summary>
        /// 获取第三方体检数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<HistoryResultMainDto> geHisvard(SearchHisClassDto input);
        /// <summary>
        /// 历史对比报告
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        HisDbDto geHisvardReport(SearchHisClassDto input);
        /// <summary>
        /// 获历史结果数据 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<OutHisValuesDto> getHisValue(InSerchHIsDto input);
    }
}
