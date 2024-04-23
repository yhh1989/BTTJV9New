using Sw.Hospital.HealthExaminationSystem.Application.Critical;
using Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Critical
{
  public   class CriticalAppService : AppServiceApiProxyBase, ICriticalAppService
    {
        /// <summary>
        /// 查询危急值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CriticalDto> getSearchCriticalDto(SearchCriticalDto input)
        {
            return GetResult<SearchCriticalDto, List<CriticalDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 保存危急值
        /// </summary>
        /// <param name="input"></param>
        public void SaveCritical(CriticalDto input)
        {
             GetResult<CriticalDto >(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 删除危急值
        /// </summary>
        /// <param name="input"></param>
        public void DelCritical(SearchCriticalDto input)
        {
            GetResult<SearchCriticalDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 危急值通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CrisisMessageDto> getCrisisMessageDto(SearchtCrisisMessageDto input)
        {
            return GetResult<SearchtCrisisMessageDto, List<CrisisMessageDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改危急值状态
        /// </summary>
        /// <param name="entityDto"></param>
        public void UpCrisisSate(UpCricalStateDto input)
        {
            GetResult<UpCricalStateDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改危急值通知状态
        /// </summary>
        /// <param name="entityDto"></param>
        public void UpMessSate(UpCricalStateDto input)
        { GetResult<UpCricalStateDto>(input, DynamicUriBuilder.GetAppSettingValue()); }
    }
}
