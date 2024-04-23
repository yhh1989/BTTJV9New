using System.Collections.Generic;
using Abp.Configuration;

namespace Sw.Hospital.HealthExaminationSystem.Core.Configuration
{
    /// <summary>
    /// 应用设置驱动
    /// </summary>
    public class AppSettingProvider : SettingProvider
    {
        /// <summary>
        /// 获取设置定义
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red",
                    scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User,
                    isVisibleToClients: true)
            };
        }
    }
}