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
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
	/// <inheritdoc cref="ICustomerRegisterItemAppService" />
	[AbpAuthorize]
	public class CustomerRegisterItemAppService : MyProjectAppServiceBase, ICustomerRegisterItemAppService
	{
		/// <summary>
		/// 体检人预约项目体检记录
		/// </summary>
		private readonly IRepository<TjlCustomerRegItem, Guid> _customerRegisterRepository;

		/// <inheritdoc />
		public CustomerRegisterItemAppService(IRepository<TjlCustomerRegItem, Guid> customerRegisterRepository)
		{
			_customerRegisterRepository = customerRegisterRepository;
		}

		/// <inheritdoc />
		public async Task<List<CustomerRegisterItemDto>> GetCustomerRegisterItem(EntityDto<Guid> input)
		{
			var query = _customerRegisterRepository.GetAll().AsNoTracking();
            query = query.Where(o=>o.CustomerItemGroupBM.IsAddMinus != (int)AddMinusType.Minus);
            query = query.Where(o => o.DepartmentBM.Category!="耗材");
            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
            query = query.Where(o => o.ItemGroupBMId != guid);
            query = query.Where(r => r.CustomerRegId == input.Id);
			query = query.OrderBy(r=>r.DepartmentBM.OrderNum).ThenBy(r => r.ItemGroupBM.OrderNum).ThenBy(r => r.ItemBM.OrderNum);
            //去掉科室是其他和抹零项目
           

    //        return await query.ProjectToListAsync<CustomerRegisterItemDto>(
				//GetConfigurationProvider(typeof(CustomerRegisterItemProfile)));


            return await query.ProjectToListAsync<CustomerRegisterItemDto>(
                  GetConfigurationProvider<CustomerRegisterItemDto>());
        }

      
    }
}