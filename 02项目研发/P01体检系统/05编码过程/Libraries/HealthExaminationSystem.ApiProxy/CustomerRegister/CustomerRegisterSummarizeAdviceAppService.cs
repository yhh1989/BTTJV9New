using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerRegister
{
    /// <summary>
    /// 体检人预约总检建议记录应用服务
    /// </summary>
    public class CustomerRegisterSummarizeAdviceAppService : AppServiceApiProxyBase, ICustomerRegisterSummarizeAdviceAppService
    {
        /// <inheritdoc />
        public Task<List<CustomerRegisterSummarizeAdviceDtoNo1>> CustomerRegisterSummarizeAdviceListNo1(List<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CustomerRegisterSummarizeAdviceDtoNo1>>.Factory.StartNew(() =>
                GetResult<List<Guid>, List<CustomerRegisterSummarizeAdviceDtoNo1>>(input,
                    url));
        }
    }
}