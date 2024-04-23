using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod
{
    public interface IPaymentStatisticAppService
#if !Proxy
        : IApplicationService
#endif
    {
        ViewDailyReportDto GetDailyReport(SearchReceiptInfoDto input);
    }
}