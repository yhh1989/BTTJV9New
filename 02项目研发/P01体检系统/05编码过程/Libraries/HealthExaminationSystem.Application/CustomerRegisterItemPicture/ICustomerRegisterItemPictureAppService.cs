using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture
{
	/// <summary>
	/// 体检人预约项目结果图像应用服务
	/// </summary>
	public interface ICustomerRegisterItemPictureAppService : IApplicationService
	{
		/// <summary>
		/// 使用体检人预约标识获取项目结果图像列表
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task<List<CustomerRegisterItemPictureDto>> GetItemPictureByCustomerRegisterId(EntityDto<Guid> input);
	}
}