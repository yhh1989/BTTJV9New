using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles
{
	/// <summary>
	/// 体检人预约项目结果图像映射
	/// </summary>
	public class CustomerRegisterItemPictureProfile : Profile
	{
		/// <summary>
		/// 体检人预约项目结果图像映射
		/// </summary>
		public CustomerRegisterItemPictureProfile()
		{
			CreateMap<TjlCustomerItemPic, CustomerRegisterItemPictureDto>();
		}
	}
}