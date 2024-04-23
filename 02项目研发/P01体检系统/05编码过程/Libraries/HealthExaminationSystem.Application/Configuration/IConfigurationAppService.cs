using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Configuration.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}