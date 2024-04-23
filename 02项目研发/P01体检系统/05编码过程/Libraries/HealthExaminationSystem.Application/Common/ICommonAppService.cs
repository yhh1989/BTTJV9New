using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Common
{
    /// <summary>
    /// 共用应用服务接口
    /// </summary>
    public interface ICommonAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        TimeDto GetDateTimeNow();

        /// <summary>
        /// 获取汉字的简拼
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ChineseDto GetHansBrief(ChineseDto input);

        /// <summary>
        /// 获取所有行政区划
        /// </summary>
        /// <returns></returns>
        
#if Application
        Task<List<AdministrativeDivisionDto>> GetAdministrativeDivisions();
#elif Proxy
        List<AdministrativeDivisionDto> GetAdministrativeDivisions();
#endif
        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<ShowOpLogDto> SeachOpLog(PageInputDto<SearchOpLogDto> pinput);
        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CreateOpLogDto SaveOpLog(CreateOpLogDto input);
    }
}