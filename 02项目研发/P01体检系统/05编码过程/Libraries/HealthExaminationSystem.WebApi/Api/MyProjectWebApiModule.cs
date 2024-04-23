using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application;
using Swashbuckle.Application;

namespace Sw.Hospital.HealthExaminationSystem.WebApi.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(MyProjectApplicationModule))]
    public class MyProjectWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(MyProjectApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
            //不显示文档
            //var applicationXml = $@"{System.AppDomain.CurrentDomain.BaseDirectory}bin\HealthExaminationSystem.Application.xml";
            //var coreXml = $@"{System.AppDomain.CurrentDomain.BaseDirectory}bin\HealthExaminationSystem.Core.xml";
            //Configuration.Modules.AbpWebApi().HttpConfiguration.EnableSwagger(configure =>
            //    {
            //        configure.SingleApiVersion("v1", GetType().Namespace);
            //        configure.ResolveConflictingActions(conflictingActionsResolver => conflictingActionsResolver.First());
            //        configure.UseFullTypeNameInSchemaIds();
            //        if (File.Exists(applicationXml))
            //        {
            //            configure.IncludeXmlComments(applicationXml);
            //        }
            //        if (File.Exists(coreXml))
            //        {
            //            configure.IncludeXmlComments(coreXml);
            //        }
            //    }).EnableSwaggerUi();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                NullValueHandling = NullValueHandling.Ignore
            };
            JsonConvert.DefaultSettings = () => jsonSerializerSettings;
        }
    }
}
