using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.Market;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Market
{
    /// <inheritdoc cref="IComplaintAppService" />
    public class ComplaintAppService : AppServiceApiProxyBase, IComplaintAppService
    {
        /// <inheritdoc />
        public Task<List<ComplaintInformationDto>> QueryComplaintInformationCollection(QueryComplaintConditionInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ComplaintInformationDto>>.Factory.StartNew(() =>
                GetResult<QueryComplaintConditionInput, List<ComplaintInformationDto>>(input, url));
        }

        /// <inheritdoc />
        public Task<ComplaintInformationDto> InsertOrUpdateComplaintInformation(UpdateComplaintInformationDto input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ComplaintInformationDto>.Factory.StartNew(() =>
                GetResult<UpdateComplaintInformationDto, ComplaintInformationDto>(input, url));
        }

        /// <inheritdoc />
        public Task<ComplaintInformationDto> QueryEmptyComplaintInformation(QueryComplaintConditionInput input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ComplaintInformationDto>.Factory.StartNew(() =>
                GetResult<QueryComplaintConditionInput, ComplaintInformationDto>(input, url));
        }
    }
}
