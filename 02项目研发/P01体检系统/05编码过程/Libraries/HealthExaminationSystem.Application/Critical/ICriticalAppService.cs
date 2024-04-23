using Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Critical
{
    public interface ICriticalAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        /// <summary>
        /// 查询危急值列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CriticalDto> getSearchCriticalDto(SearchCriticalDto input);
        /// <summary>
        /// 保存危机值
        /// </summary>
        void SaveCritical(CriticalDto input);
        /// <summary>
        /// 删除危急值
        /// </summary>
        /// <param name="input"></param>
         void DelCritical(SearchCriticalDto input);

        /// <summary>
        /// 危急值通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CrisisMessageDto> getCrisisMessageDto(SearchtCrisisMessageDto input);
        /// <summary>
        /// 修改危急值状态
        /// </summary>
        /// <param name="input"></param>
        void UpCrisisSate(UpCricalStateDto input);
        /// <summary>
        /// 修改通知状态
        /// </summary>
        /// <param name="input"></param>
        void UpMessSate(UpCricalStateDto input);
    }
}
