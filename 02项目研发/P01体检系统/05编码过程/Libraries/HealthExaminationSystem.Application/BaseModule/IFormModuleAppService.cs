using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.BaseModule
{
    /// <summary>
    /// 窗体模块应用服务
    /// </summary>
    public interface IFormModuleAppService
#if !Proxy
        : IApplicationService 
#endif
    {
        /// <summary>
        /// 根据名称列表获取
        /// </summary>
        /// <param name="input">包含名称列表的数据传输对象</param>
        /// <returns></returns>
        List<FormModuleDto> GetByNames(FindNameDto input);

        /// <summary>
        /// 根据名称获取
        /// </summary>
        /// <param name="input">包含名称的数据传出对象</param>
        /// <returns></returns>
        FormModuleDto GetByName(NameDto input);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
       

#if Application
        Task<List<FormModuleDto>> GetAllList();
#elif Proxy
        List<FormModuleDto> GetAllList();
#endif
    }
}