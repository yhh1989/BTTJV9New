using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Configuration;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;

namespace Sw.Hospital.HealthExaminationSystem.Core
{
    /// <inheritdoc />
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class MyProjectCoreModule : AbpModule
    {
        /// <inheritdoc />
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = MyProjectConsts.MultiTenancyEnabled;

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    MyProjectConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Sw.Hospital.HealthExaminationSystem.Core.Localization.Source"
                    )
                )
            );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<MyProjectAuthorizationProvider>();

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}