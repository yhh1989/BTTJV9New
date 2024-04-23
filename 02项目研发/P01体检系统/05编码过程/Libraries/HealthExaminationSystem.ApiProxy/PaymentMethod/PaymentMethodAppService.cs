using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod
{
    public class PaymentMethodAppService : AppServiceApiProxyBase, IPaymentMethodAppService
    {
        public List<MChargeTypeDto> GetMChargeType()
        {
            return GetResult<List<MChargeTypeDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public MChargeTypeDto AddMCargeType(MChargeTypeDto input)
        {
            return GetResult<MChargeTypeDto, MChargeTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public MChargeTypeDto EditMCargeType(MChargeTypeDto input)
        {
            return GetResult<MChargeTypeDto, MChargeTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DeleteMCargeType(MChargeTypeDto input)
        {
            GetResult<MChargeTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}