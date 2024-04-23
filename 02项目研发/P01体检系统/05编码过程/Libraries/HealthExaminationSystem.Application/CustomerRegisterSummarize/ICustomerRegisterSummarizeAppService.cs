using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize
{
	/// <summary>
	/// 体检人预约总检汇总应用服务
	/// </summary>
	public interface ICustomerRegisterSummarizeAppService : IApplicationService
	{
		/// <summary>
		/// 使用体检人预约标识获取总检汇总
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task<CustomerRegisterSummarizeDto> GetSummarizeByCustomerRegisterId(EntityDto<Guid> input);

		/// <summary>
		/// 使用体检人预约标识获取总检汇总建议
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task<List<CustomerRegisterSummarizeSuggestDto>> GetSummarizeSuggestByCustomerRegisterId(EntityDto<Guid> input);
	}
}