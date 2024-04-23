using System;
using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using Abp.Modules;
using AutoMapper;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.DefinedProfiles;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Roles.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Application
{
	/// <inheritdoc />
	[DependsOn(typeof(MyProjectCoreModule), typeof(AbpAutoMapperModule))]
	public class MyProjectApplicationModule : AbpModule
	{
		/// <inheritdoc />
		public override void PreInitialize()
		{
		}

		/// <inheritdoc />
		public override void Initialize()
		{
			IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

			// TODO: Is there somewhere else to store these, with the dto classes
			Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
			{
				// Role and permission
				cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
				cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

				cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
				cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());

				cfg.CreateMap<UserDto, User>();
				cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

				cfg.CreateMap<CreateUserDto, User>();
				cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

				cfg.AddProfile<CustomerRegisterProfile>();
				cfg.AddProfile<CustomerRegisterSummarizeProfile>();

                //cfg.CreateMissingTypeMaps = true;
                //cfg.ValidateInlineMaps = false;
                cfg.AllowNullCollections = true;
				cfg.EnableNullPropagationForQueryMapping = true;

			});

            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                //空值处理
                setting.NullValueHandling = NullValueHandling.Ignore;



                return setting;


            });
        }
	}
}
