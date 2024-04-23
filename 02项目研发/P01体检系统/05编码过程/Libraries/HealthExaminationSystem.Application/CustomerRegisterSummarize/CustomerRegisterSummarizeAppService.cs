using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize
{
	/// <inheritdoc cref="ICustomerRegisterSummarizeAppService" />
	[AbpAuthorize]
	public class CustomerRegisterSummarizeAppService : MyProjectAppServiceBase, ICustomerRegisterSummarizeAppService
	{
		/// <summary>
		/// 体检人预约总检汇总仓储
		/// </summary>
		private readonly IRepository<TjlCustomerSummarize, Guid> _customerRegisterSummarizeRepository;

		/// <summary>
		/// 体检人预约总检汇总建议仓储
		/// </summary>
		private readonly IRepository<TjlCustomerSummarizeBM, Guid> _customerSummarizeSuggestRepository;

		/// <inheritdoc />
		public CustomerRegisterSummarizeAppService(IRepository<TjlCustomerSummarize, Guid> customerRegisterSummarizeRepository, IRepository<TjlCustomerSummarizeBM, Guid> customerSummarizeSuggestRepository)
		{
			_customerRegisterSummarizeRepository = customerRegisterSummarizeRepository;
			_customerSummarizeSuggestRepository = customerSummarizeSuggestRepository;
		}

		/// <inheritdoc />
		public async Task<CustomerRegisterSummarizeDto> GetSummarizeByCustomerRegisterId(EntityDto<Guid> input)
		{
			var row = await _customerRegisterSummarizeRepository.FirstOrDefaultAsync(r => r.CustomerRegID == input.Id);
			if (row == null)
			{
				return null;
			}
			return ObjectMapper.Map<CustomerRegisterSummarizeDto>(row);
		}

		/// <inheritdoc />
		public async Task<List<CustomerRegisterSummarizeSuggestDto>> GetSummarizeSuggestByCustomerRegisterId(EntityDto<Guid> input)
		{
			var query = _customerSummarizeSuggestRepository.GetAll().AsNoTracking();
			query = query.Where(r => r.CustomerRegID == input.Id);
			return await query.ProjectToListAsync<CustomerRegisterSummarizeSuggestDto>(
				GetConfigurationProvider(typeof(CustomerRegisterSummarizeSuggestProfile)));
		}
	}
}