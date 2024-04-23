using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterDepartmentSummaryAppServiceDto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
	/// <summary>
	/// 体检人预约科室小结汇总应用服务
	/// </summary>
	public interface ICustomerRegisterDepartmentSummaryAppService : IApplicationService
	{
		/// <summary>
		/// 获取体检人预约的科室小结汇总（总检）
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task<List<CustomerRegisterDepartmentSummaryDto>> GetCustomerRegisterDepartmentSummary(EntityDto<Guid> input);
	}
}