using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture
{
	/// <inheritdoc cref="ICustomerRegisterItemPictureAppService" />
	public class CustomerRegisterItemPictureAppService : AppServiceApiProxyBase, ICustomerRegisterItemPictureAppService
	{
		/// <inheritdoc />
		public Task<List<CustomerRegisterItemPictureDto>> GetItemPictureByCustomerRegisterId(EntityDto<Guid> input)
		{
			var url = DynamicUriBuilder.GetAppSettingValue();
			return Task<List<CustomerRegisterItemPictureDto>>.Factory.StartNew(() =>
				GetResult<EntityDto<Guid>, List<CustomerRegisterItemPictureDto>>(input, url));
		}
	}
}