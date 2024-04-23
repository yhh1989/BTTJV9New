using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection
{
    public interface IGuideListCollectionAppService
#if Application
        : IApplicationService
#endif
    {
        PageResultDto<CustomerRegQuery> QueryCompanyName(PageInputDto<CustomerRegQuery> input);

        List<CustomerRegQuery> PrintCompanyName(CustomerRegQuery input);
    }
}