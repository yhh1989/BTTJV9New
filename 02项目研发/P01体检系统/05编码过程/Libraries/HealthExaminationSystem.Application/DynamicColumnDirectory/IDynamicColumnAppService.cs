using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory
{
    /// <summary>
    /// 动态列应用服务接口
    /// </summary>
    public interface IDynamicColumnAppService : IApplicationService
    {
        /// <summary>
        /// 保存动态列配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SaveDynamicColumnConfigurationList(List<DynamicColumnConfigurationDtoNo1> input);

        /// <summary>
        /// 查询动态列配置列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DynamicColumnConfigurationDtoNo1>> QueryDynamicColumnConfigurationList(DynamicColumnConfigurationDtoNo2 input);
    }
}