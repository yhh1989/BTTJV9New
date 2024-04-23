using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
	/// <inheritdoc cref="ICustomerRegisterItemAppService" />
	public class CustomerRegisterItemAppService : AppServiceApiProxyBase, ICustomerRegisterItemAppService
	{
		/// <inheritdoc />
		public Task<List<CustomerRegisterItemDto>> GetCustomerRegisterItem(EntityDto<Guid> input)
		{
			var url = DynamicUriBuilder.GetAppSettingValue();
			return Task<List<CustomerRegisterItemDto>>.Factory.StartNew(() =>
				GetResult<EntityDto<Guid>, List<CustomerRegisterItemDto>>(input, url));
		}      

    }
}