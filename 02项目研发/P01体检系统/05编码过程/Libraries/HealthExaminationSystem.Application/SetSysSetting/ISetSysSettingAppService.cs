using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting
{
    /// <summary>
    /// ID生成接口
    /// </summary>
    public interface ISetSysSettingAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取参数值
        /// </summary>  
        string GetSysSetting(QuerySetSysSettingDto QuerySetSysSetting);

        /// <summary>
        /// 获取所有参数值
        /// </summary>  
        List<SetSysSettingDto> GetAllSysSetting();
    } 
}
