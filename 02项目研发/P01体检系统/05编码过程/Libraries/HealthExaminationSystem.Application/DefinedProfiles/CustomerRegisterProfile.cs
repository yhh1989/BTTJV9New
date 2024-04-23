using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto.CustomerRegisterAppServiceDot;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles
{
	/// <summary>
	/// 体检人预约映射
	/// </summary>
	public class CustomerRegisterProfile : Profile
	{
		/// <summary>
		/// 体检人预约映射
		/// </summary>
		public CustomerRegisterProfile()
		{
			CreateMap<TjlCustomerReg, CustomerRegister1Dto>()
				.ForMember(destinationMember => destinationMember.Name,
					memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Customer.Name))
				.ForMember(destinationMember => destinationMember.Sex,
					memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Customer.Sex))
				.ForMember(destinationMember => destinationMember.ClientName,
					memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.ClientInfo.ClientName));
		}
	}
}