using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
	/// <summary>
	/// 体检人预约项目体检记录
	/// </summary>
	public interface ICustomerRegisterItemAppService : IApplicationService
	{
		/// <summary>
		/// 获取体检人预约项目体检记录
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task<List<CustomerRegisterItemDto>> GetCustomerRegisterItem(EntityDto<Guid> input);
	}
}