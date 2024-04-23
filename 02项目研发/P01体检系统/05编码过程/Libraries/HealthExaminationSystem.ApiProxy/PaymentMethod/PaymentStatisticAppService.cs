using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod
{
    public class PaymentStatisticAppService : AppServiceApiProxyBase, IPaymentStatisticAppService
    {
        public ViewDailyReportDto GetDailyReport(SearchReceiptInfoDto input)
        {
            return GetResult<SearchReceiptInfoDto, ViewDailyReportDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}