using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles
{
	/// <summary>
	/// 总检建议映射
	/// </summary>
	public class CustomerRegisterSummarizeSuggestProfile : Profile
	{
		/// <summary>
		/// 总检建议映射
		/// </summary>
		public CustomerRegisterSummarizeSuggestProfile()
		{
			CreateMap<TjlCustomerSummarizeBM, CustomerRegisterSummarizeSuggestDto>();
		}
	}
}