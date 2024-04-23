using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory;
using Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.DynamicColumnDirectory
{
    /// <summary>
    /// 动态列应用服务
    /// </summary>
    public class DynamicColumnAppService : AppServiceApiProxyBase, IDynamicColumnAppService
    {
        /// <inheritdoc />
        public Task<bool> SaveDynamicColumnConfigurationList(List<DynamicColumnConfigurationDtoNo1> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<bool>.Factory.StartNew(() =>
                GetResult<List<DynamicColumnConfigurationDtoNo1>, bool>(input, url));
        }

        /// <inheritdoc />
        public Task<List<DynamicColumnConfigurationDtoNo1>> QueryDynamicColumnConfigurationList(DynamicColumnConfigurationDtoNo2 input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<DynamicColumnConfigurationDtoNo1>>.Factory.StartNew(() =>
                GetResult<DynamicColumnConfigurationDtoNo2, List<DynamicColumnConfigurationDtoNo1>>(input, url));
        }
    }
}
