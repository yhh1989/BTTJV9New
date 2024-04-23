using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto;
#if !Proxy
using Abp.Application.Services;
#endif


namespace Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime
{
    public interface IReVisitTimeAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取所有项目组合
        /// </summary>
        /// <returns></returns>
        List<SearchItemGroupDto> GetALLItemGroup(SearchItemGroupDto dto);
        /// <summary>
        /// 按项目查询数据
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        List<SearchReVisitTimeDto> GetALLReVisitTime(QueryData queryData);
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        bool UpdateIsActive(QueryData queryData);
        /// <summary>
        /// 修改/新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool UpdateReVisitTime(SearchReVisitTimeDto dto);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertReVisitTime(List<InsertReVisitTimeDto> dto);
        /// <summary>
        /// 根据预约id删除所有
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool DelReVisitTimeForRegId(InsertReVisitTimeDto dto);
        /// <summary>
        /// 根据预约id查询所有
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<InsertReVisitTimeDto> GetReVisitTimeForRegId(InsertReVisitTimeDto dto);
        /// <summary>
        /// 根据单位预约记录id查询此单位下的所有复诊信息
        /// </summary>
        /// <param name="dto">单位预约id</param>
        /// <returns></returns>
        List<QueryCheckReview> GetReVisitTimeForClientRegId(QueryCheckReview dto);
        /// <summary>
        /// 根据患者id获取未复诊过的项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<SearchReVisitTimeDto> GetReVisitTimeForCustomerId(QueryData id);
        /// <summary>
        /// 根据单位和项目组合编码获取复诊对应数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        List<SearchReVisitTimeDto> GetReVisitTimeContentList(QueryData data);
        /// <summary>
        ///  查询项目金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TbmItemGroupDto GetItemGroupPrice(QueryData data);
    }
}
