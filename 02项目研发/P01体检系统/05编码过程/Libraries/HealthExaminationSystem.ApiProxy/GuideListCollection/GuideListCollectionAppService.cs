using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection
{
    public class GuideListCollectionAppService:AppServiceApiProxyBase, IGuideListCollectionAppService
    {
        public PageResultDto<CustomerRegQuery> QueryCompanyName(PageInputDto<CustomerRegQuery> input)
        {
            return GetResult<PageInputDto<CustomerRegQuery>, PageResultDto<CustomerRegQuery>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CustomerRegQuery> PrintCompanyName(CustomerRegQuery input)
        {
            return GetResult<CustomerRegQuery, List<CustomerRegQuery>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}