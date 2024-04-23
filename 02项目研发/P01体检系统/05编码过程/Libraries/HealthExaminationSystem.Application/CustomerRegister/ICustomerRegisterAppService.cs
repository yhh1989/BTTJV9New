using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto.CustomerRegisterAppServiceDot;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister
{
	/// <summary>
	/// 体检人预约应用服务
	/// </summary>
	public interface ICustomerRegisterAppService : IApplicationService
	{
		/// <summary>
		/// 使用标识获取体检人预约信息（总检）
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task<CustomerRegister1Dto> GetCustomerRegisterById(EntityDto<Guid> input);

        /// <summary>
        /// 查询体检人预约记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CustomerRegisterDtoNo3>> CustomerRegisterList(List<Guid> input);
    }
}