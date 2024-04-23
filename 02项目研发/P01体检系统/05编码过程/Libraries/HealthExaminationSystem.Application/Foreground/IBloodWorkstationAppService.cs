using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground
{
    /// <summary>
    /// 抽血工作站应用服务接口
    /// </summary>
    public interface IBloodWorkstationAppService : IApplicationService
    {
        /// <summary>
        /// 查询条码打印记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CustomerRegisterBarCodePrintInformationDto>> QueryBarCodePrintRecord(
            CustomerRegisterBarCodePrintInformationConditionInput input);
        Task<List<CustomerCount>> QueryBlood(CustomerRegisterBarCodePrintInformationConditionInput input);
        /// <summary>
        /// 查询体检人预约信息试用标识
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerRegister52Dto> QueryCustomerRegisterById(EntityDto<Guid> input);

        /// <summary>
        /// 更新条码记录是否抽血
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerRegisterBarCodePrintInformationDto> UpdateBarCodeHaveBlood(UpdateBarCodeHaveBloodInput input);
    }
}