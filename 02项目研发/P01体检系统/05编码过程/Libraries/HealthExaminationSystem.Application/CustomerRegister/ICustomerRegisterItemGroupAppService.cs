using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister
{
    /// <summary>
    /// 体检人预约的项目组合应用服务接口
    /// </summary>
    public interface ICustomerRegisterItemGroupAppService : IApplicationService
    {
        /// <summary>
        /// 获取体检人预约项目组合列表
        /// </summary>
        /// <param name="input">体检人预约标识列表</param>
        /// <returns></returns>
        Task<List<CustomerRegisterItemGroupDtoNo4>> CustomerRegisterItemGroupList(List<Guid> input);
    }
}