using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerRegisterSummarize
{
	/// <inheritdoc cref="ICustomerRegisterSummarizeAppService" />
	public class CustomerRegisterSummarizeAppService : AppServiceApiProxyBase, ICustomerRegisterSummarizeAppService
	{
		/// <inheritdoc />
		public Task<CustomerRegisterSummarizeDto> GetSummarizeByCustomerRegisterId(EntityDto<Guid> input)
		{
			var url = DynamicUriBuilder.GetAppSettingValue();
			return Task<CustomerRegisterSummarizeDto>.Factory.StartNew(() =>
				GetResult<EntityDto<Guid>, CustomerRegisterSummarizeDto>(input, url));
		}

		/// <inheritdoc />
		public Task<List<CustomerRegisterSummarizeSuggestDto>> GetSummarizeSuggestByCustomerRegisterId(EntityDto<Guid> input)
		{
			var url = DynamicUriBuilder.GetAppSettingValue();
			return Task<List<CustomerRegisterSummarizeSuggestDto>>.Factory.StartNew(() =>
				GetResult<EntityDto<Guid>, List<CustomerRegisterSummarizeSuggestDto>>(input, url));
		}
	}
}
