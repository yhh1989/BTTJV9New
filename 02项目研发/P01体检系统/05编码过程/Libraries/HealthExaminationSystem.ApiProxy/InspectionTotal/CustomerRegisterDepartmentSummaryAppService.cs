using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterDepartmentSummaryAppServiceDto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
	/// <inheritdoc cref="ICustomerRegisterDepartmentSummaryAppService" />
	public class CustomerRegisterDepartmentSummaryAppService : AppServiceApiProxyBase, ICustomerRegisterDepartmentSummaryAppService
	{
		/// <inheritdoc />
		public Task<List<CustomerRegisterDepartmentSummaryDto>> GetCustomerRegisterDepartmentSummary(EntityDto<Guid> input)
		{
			var url = DynamicUriBuilder.GetAppSettingValue();

			return Task<List<CustomerRegisterDepartmentSummaryDto>>.Factory.StartNew(() =>
				GetResult<EntityDto<Guid>, List<CustomerRegisterDepartmentSummaryDto>>(input, url));
		}
	}
}