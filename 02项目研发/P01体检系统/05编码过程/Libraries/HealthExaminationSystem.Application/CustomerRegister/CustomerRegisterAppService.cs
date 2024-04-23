using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto.CustomerRegisterAppServiceDot;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister
{
	/// <inheritdoc cref="ICustomerRegisterAppService" />
	public class CustomerRegisterAppService : MyProjectAppServiceBase, ICustomerRegisterAppService
	{
		private readonly IRepository<TjlCustomerReg, Guid> _customerRegisterRepository;

		/// <inheritdoc />
		public CustomerRegisterAppService(IRepository<TjlCustomerReg, Guid> customerRegisterRepository)
		{
			_customerRegisterRepository = customerRegisterRepository;
		}

		/// <inheritdoc />
		public async Task<CustomerRegister1Dto> GetCustomerRegisterById(EntityDto<Guid> input)
		{
			var row = await _customerRegisterRepository.GetAsync(input.Id);
			return ObjectMapper.Map<CustomerRegister1Dto>(row);
		}

        /// <inheritdoc />
        public async Task<List<CustomerRegisterDtoNo3>> CustomerRegisterList(List<Guid> input)
        {
            var query = _customerRegisterRepository.GetAll();
            query = query.Where(r => r.ClientRegId != null && input.Contains(r.ClientRegId.Value));
            return await query.ProjectToListAsync<CustomerRegisterDtoNo3>(
                GetConfigurationProvider<CustomerRegisterDtoNo3>());
        }
    }
}