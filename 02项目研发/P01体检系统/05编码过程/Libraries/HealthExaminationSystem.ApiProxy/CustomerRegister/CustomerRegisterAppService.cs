using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto.CustomerRegisterAppServiceDot;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerRegister
{
	/// <inheritdoc cref="ICustomerRegisterAppService" />
	public class CustomerRegisterAppService : AppServiceApiProxyBase, ICustomerRegisterAppService
	{
		/// <inheritdoc />
		public Task<CustomerRegister1Dto> GetCustomerRegisterById(EntityDto<Guid> input)
		{
			var url = DynamicUriBuilder.GetAppSettingValue();
			return Task<CustomerRegister1Dto>.Factory.StartNew(() =>
				GetResult<EntityDto<Guid>, CustomerRegister1Dto>(input, url));
        }

        /// <inheritdoc />
        public Task<List<CustomerRegisterDtoNo3>> CustomerRegisterList(List<Guid> input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<CustomerRegisterDtoNo3>>.Factory.StartNew(() =>
                GetResult<List<Guid>, List<CustomerRegisterDtoNo3>>(input, url));
        }
    }
}
