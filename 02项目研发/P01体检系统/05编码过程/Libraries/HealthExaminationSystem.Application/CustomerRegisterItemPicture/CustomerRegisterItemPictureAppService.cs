using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture
{
	/// <inheritdoc cref="ICustomerRegisterItemPictureAppService" />
	[AbpAuthorize]
	public class CustomerRegisterItemPictureAppService : MyProjectAppServiceBase, ICustomerRegisterItemPictureAppService
	{
		/// <summary>
		/// 体检人预约项目结果图像仓储
		/// </summary>
		private readonly IRepository<TjlCustomerItemPic, Guid> _customerRegisterItemPictureRepository;

		/// <inheritdoc />
		public CustomerRegisterItemPictureAppService(IRepository<TjlCustomerItemPic, Guid> customerRegisterItemPictureRepository)
		{
			_customerRegisterItemPictureRepository = customerRegisterItemPictureRepository;
		}

		/// <inheritdoc />
		public async Task<List<CustomerRegisterItemPictureDto>> GetItemPictureByCustomerRegisterId(EntityDto<Guid> input)
		{
			var query = _customerRegisterItemPictureRepository.GetAll().AsNoTracking();
			query = query.Where(r => r.TjlCustomerRegID == input.Id);
			return await query.ProjectToListAsync<CustomerRegisterItemPictureDto>(
				GetConfigurationProvider(typeof(CustomerRegisterItemPictureProfile)));
		}
	}
}