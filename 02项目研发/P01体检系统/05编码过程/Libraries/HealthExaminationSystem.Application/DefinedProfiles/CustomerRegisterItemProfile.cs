using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles
{
	/// <summary>
	/// 体检人预约体检记录映射
	/// </summary>
	public class CustomerRegisterItemProfile : Profile
	{
		/// <summary>
		/// 体检人预约体检记录映射
		/// </summary>
		public CustomerRegisterItemProfile()
		{
			CreateMap<TjlCustomerRegItem, CustomerRegisterItemDto>();
		}
	}
}