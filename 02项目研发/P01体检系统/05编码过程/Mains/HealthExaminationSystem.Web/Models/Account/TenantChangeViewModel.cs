using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Sessions.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}