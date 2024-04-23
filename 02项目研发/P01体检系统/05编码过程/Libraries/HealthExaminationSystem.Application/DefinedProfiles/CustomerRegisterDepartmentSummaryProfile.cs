using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterDepartmentSummaryAppServiceDto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles
{
	/// <summary>
	/// 体检人预约科室小结汇总映射
	/// </summary>
	public class CustomerRegisterDepartmentSummaryProfile : Profile
	{
		/// <summary>
		/// 体检人预约科室小结汇总映射
		/// </summary>
		public CustomerRegisterDepartmentSummaryProfile()
		{
			CreateMap<TjlCustomerDepSummary, CustomerRegisterDepartmentSummaryDto>()
				.ForMember(destinationMember => destinationMember.DepartmentId,
				memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.DepartmentBMId))
				.ForMember(destinationMember => destinationMember.ExamineEmployeeId,
					memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.ExamineEmployeeBMId));
		}
	}
}