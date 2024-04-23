using Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime;
using Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.ReVisitTime
{
    /// <summary>
    /// 复诊时间
    /// </summary>
    public class ReVisitTimeAppService : AppServiceApiProxyBase, IReVisitTimeAppService
    {
        /// <summary>
        /// 根据预约id删除所有
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool DelReVisitTimeForRegId(InsertReVisitTimeDto dto)
        {
            return GetResult<InsertReVisitTimeDto, bool>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 获取所有组合项目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<SearchItemGroupDto> GetALLItemGroup(SearchItemGroupDto dto)
        {
            return GetResult<SearchItemGroupDto, List<SearchItemGroupDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 按项目查询数据
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public List<SearchReVisitTimeDto> GetALLReVisitTime(QueryData queryData)
        {
            return GetResult<QueryData, List<SearchReVisitTimeDto>>(queryData, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 根据单位预约记录id查询此单位下的所有复诊信息
        /// </summary>
        /// <param name="dto">单位预约id</param>
        /// <returns></returns>
        public List<QueryCheckReview> GetReVisitTimeForClientRegId(QueryCheckReview dto)
        {
            return GetResult<QueryCheckReview, List<QueryCheckReview>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 根据预约id查询所有
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<InsertReVisitTimeDto> GetReVisitTimeForRegId(InsertReVisitTimeDto dto)
        {
            return GetResult<InsertReVisitTimeDto, List<InsertReVisitTimeDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool InsertReVisitTime(List<InsertReVisitTimeDto> dto)
        {
            return GetResult<List<InsertReVisitTimeDto>, bool>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public bool UpdateIsActive(QueryData queryData)
        {
            return GetResult<QueryData, bool>(queryData, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改/新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool UpdateReVisitTime(SearchReVisitTimeDto dto)
        {
            return GetResult<SearchReVisitTimeDto, bool>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据患者id获取未复诊过的项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SearchReVisitTimeDto> GetReVisitTimeForCustomerId(QueryData id)
        {
            return GetResult<QueryData, List<SearchReVisitTimeDto>>(id, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据单位和项目组合编码获取复诊对应数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<SearchReVisitTimeDto> GetReVisitTimeContentList(QueryData data)
        {
            return GetResult<QueryData, List<SearchReVisitTimeDto>>(data, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        ///  查询项目金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TbmItemGroupDto GetItemGroupPrice(QueryData data)
        {
            return GetResult<QueryData, TbmItemGroupDto>(data, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
