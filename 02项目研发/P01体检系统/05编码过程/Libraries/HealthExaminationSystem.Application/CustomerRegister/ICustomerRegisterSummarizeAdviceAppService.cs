using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister
{
    /// <summary>
    /// 体检人预约总检建议记录应用服务接口
    /// </summary>
    public interface ICustomerRegisterSummarizeAdviceAppService : IApplicationService
    {
        /// <summary>
        /// 获取体检人预约建议列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CustomerRegisterSummarizeAdviceDtoNo1>> CustomerRegisterSummarizeAdviceListNo1(List<Guid> input);
    }
}