using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Sw.Hospital.HealthExaminationSystem.Application.Configuration.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Configuration;

namespace Sw.Hospital.HealthExaminationSystem.Application.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MyProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
