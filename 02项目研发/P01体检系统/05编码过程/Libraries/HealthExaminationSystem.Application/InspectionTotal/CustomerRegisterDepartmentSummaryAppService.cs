using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterDepartmentSummaryAppServiceDto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
	/// <inheritdoc cref="ICustomerRegisterDepartmentSummaryAppService" />
	[AbpAuthorize]
	public class CustomerRegisterDepartmentSummaryAppService : MyProjectAppServiceBase, ICustomerRegisterDepartmentSummaryAppService
	{
		/// <summary>
		/// 体检人预约科室小结汇总表
		/// </summary>
		private readonly IRepository<TjlCustomerDepSummary, Guid> _customerDepSummaryRepository;

		/// <inheritdoc />
		public CustomerRegisterDepartmentSummaryAppService(IRepository<TjlCustomerDepSummary, Guid> customerDepSummaryRepository)
		{
			_customerDepSummaryRepository = customerDepSummaryRepository;
		}

		/// <inheritdoc /> 
		public async Task<List<CustomerRegisterDepartmentSummaryDto>> GetCustomerRegisterDepartmentSummary(EntityDto<Guid> input)
		{
			var query = _customerDepSummaryRepository.GetAll().AsNoTracking();

			query = query.Where(r => r.CustomerRegId == input.Id && r.DepartmentBM.Category !="耗材");

			query = query.OrderBy(r => r.DepartmentBM.OrderNum);

			return await query.ProjectToListAsync<CustomerRegisterDepartmentSummaryDto>(
				GetConfigurationProvider(typeof(CustomerRegisterDepartmentSummaryProfile)));
		}
	}
}