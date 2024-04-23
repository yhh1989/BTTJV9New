using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles
{
	/// <summary>
	/// 体检人预约总检汇总映射
	/// </summary>
	public class CustomerRegisterSummarizeProfile : Profile
	{
		/// <summary>
		/// 体检人预约总检汇总映射
		/// </summary>
		public CustomerRegisterSummarizeProfile()
		{
			CreateMap<TjlCustomerSummarize, CustomerRegisterSummarizeDto>();
		}
	}
}