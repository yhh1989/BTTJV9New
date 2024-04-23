using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Sessions.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
