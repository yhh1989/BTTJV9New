using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerRegister
{
    /// <summary>
    /// 体检人预约的项目组合应用服务
    /// </summary>
    public class CustomerRegisterItemGroupAppService : AppServiceApiProxyBase, ICustomerRegisterItemGroupAppService
    {
        /// <inheritdoc />
        public Task<List<CustomerRegisterItemGroupDtoNo4>> CustomerRegisterItemGroupList(List<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CustomerRegisterItemGroupDtoNo4>>.Factory.StartNew(() =>
                GetResult<List<Guid>, List<CustomerRegisterItemGroupDtoNo4>>(input,
                    url));
        }
    }
}