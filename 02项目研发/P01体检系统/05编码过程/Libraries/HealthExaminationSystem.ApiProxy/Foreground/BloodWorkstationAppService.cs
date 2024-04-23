using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Foreground;
using Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Foreground
{
    /// <inheritdoc cref="IBloodWorkstationAppService"/>
    public class BloodWorkstationAppService : AppServiceApiProxyBase, IBloodWorkstationAppService
    {
        /// <inheritdoc />
        public Task<List<CustomerRegisterBarCodePrintInformationDto>> QueryBarCodePrintRecord(CustomerRegisterBarCodePrintInformationConditionInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CustomerRegisterBarCodePrintInformationDto>>.Factory.StartNew(() =>
                GetResult<CustomerRegisterBarCodePrintInformationConditionInput,
                    List<CustomerRegisterBarCodePrintInformationDto>>(input, url));

        }
        public   Task<List<CustomerCount>> QueryBlood(CustomerRegisterBarCodePrintInformationConditionInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CustomerCount>>.Factory.StartNew(() =>
                GetResult<CustomerRegisterBarCodePrintInformationConditionInput,
                    List<CustomerCount>>(input, url));

        }

        /// <inheritdoc />
        public Task<CustomerRegister52Dto> QueryCustomerRegisterById(EntityDto<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<CustomerRegister52Dto>.Factory.StartNew(() =>
                GetResult<EntityDto<Guid>, CustomerRegister52Dto>(input, url));
        }

        /// <inheritdoc />
        public Task<CustomerRegisterBarCodePrintInformationDto> UpdateBarCodeHaveBlood(UpdateBarCodeHaveBloodInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<CustomerRegisterBarCodePrintInformationDto>.Factory.StartNew(() =>
                GetResult<UpdateBarCodeHaveBloodInput, CustomerRegisterBarCodePrintInformationDto>(input, url));
        }
    }
}
