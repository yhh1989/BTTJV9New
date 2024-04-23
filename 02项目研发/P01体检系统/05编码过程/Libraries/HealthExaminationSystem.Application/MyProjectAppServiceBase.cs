using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNet.Identity;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;

namespace Sw.Hospital.HealthExaminationSystem.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MyProjectAppServiceBase : ApplicationService
    {
        /// <summary>
        /// 租户管理器
        /// </summary>
        public TenantManager TenantManager { get; set; }

        /// <summary>
        /// 用户管理器
        /// </summary>
        public UserManager UserManager { get; set; }

        /// <summary>
        /// 项目应用服务基类
        /// </summary>
        protected MyProjectAppServiceBase()
        {
            LocalizationSourceName = MyProjectConsts.LocalizationSourceName;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        /// <summary>
        /// 用户当前租户
        /// </summary>
        /// <returns></returns>
        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        /// <param name="identityResult"></param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        /// <summary>
        /// 获取一个用于 AutoMapper 的配置提供程序
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        protected virtual IConfigurationProvider GetConfigurationProvider<TSource, TDestination>(
            params Tuple<Type, Type>[] types)
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.CreateMap<TSource, TDestination>();
            if (types == null)
            {
                // ignore
            }
            else
            {
                if (types.Length == 0)
                {
                    // ignore
                }
                else
                {
                    foreach (var (item1, item2) in types)
                    {
                        mapperConfigurationExpression.CreateMap(item1, item2);
                    }
                }
            } 

            return new MapperConfiguration(mapperConfigurationExpression);
        }

        /// <summary>
        /// 获取一个用于 AutoMapper 的配置提供程序
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        protected virtual IConfigurationProvider GetConfigurationProvider<TDestination>()
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            var type = typeof(TDestination);
            GoingCreateMap(mapperConfigurationExpression, type);
            return new MapperConfiguration(mapperConfigurationExpression);
        }

        /// <summary>
		/// 获取一个用于 AutoMapper 的配置提供程序
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TDestination"></typeparam>
		/// <returns></returns>
		protected virtual IConfigurationProvider GetConfigurationProvider<TSource, TDestination>()
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.CreateMap<TSource, TDestination>();
            var type = typeof(TDestination);
            GoingDestinationPropertyType(mapperConfigurationExpression, type);

            return new MapperConfiguration(mapperConfigurationExpression);
        }

        /// <summary>
        /// 下钻到目标属性类型
        /// </summary>
        /// <param name="mapperConfigurationExpression"></param>
        /// <param name="type"></param>
        private void GoingDestinationPropertyType(MapperConfigurationExpression mapperConfigurationExpression, Type type)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType)
                {
                    foreach (var typeArgument in property.PropertyType.GenericTypeArguments)
                    {
                        if (typeArgument.IsClass && !typeArgument.IsPrimitive && typeArgument != typeof(string))
                        {
                            GoingCreateMap(mapperConfigurationExpression, typeArgument);
                        }
                    }
                }
                else if (property.PropertyType.IsClass && !property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                {
                    GoingCreateMap(mapperConfigurationExpression, property.PropertyType);
                }
            }
        }

        /// <summary>
        /// 创建对象映射
        /// </summary>
        /// <param name="mapperConfigurationExpression"></param>
        /// <param name="type"></param>
        private void GoingCreateMap(MapperConfigurationExpression mapperConfigurationExpression, Type type)
        {
            var attributes = type.GetCustomAttributes(false);
            foreach (var attribute in attributes)
            {
                // 这个特性是 AutoMapper 自带特性，在后续版本中会有
                if (attribute is AutoMapAttribute mapAttribute)
                {
                    mapperConfigurationExpression.CreateMap(mapAttribute.SourceType, type);
                    GoingDestinationPropertyType(mapperConfigurationExpression, type);
                }
                else if (attribute.GetType() == typeof(Abp.AutoMapper.AutoMapAttribute) || attribute.GetType() == typeof(Abp.AutoMapper.AutoMapFromAttribute) || attribute.GetType() == typeof(Abp.AutoMapper.AutoMapToAttribute))
                {
                    var types = ((Abp.AutoMapper.AutoMapAttributeBase)attribute).TargetTypes;
                    foreach (var type1 in types)
                    {
                        mapperConfigurationExpression.CreateMap(type1, type);
                    }
                    GoingDestinationPropertyType(mapperConfigurationExpression, type);
                }
            }
        }

        /// <summary>
        /// 获取一个用于 AutoMapper 的配置提供程序
        /// </summary>
        /// <returns></returns>
        protected virtual IConfigurationProvider GetConfigurationProvider(params Type[] types)
        {
            return new MapperConfiguration(cfg =>
            {
                foreach (var item in types)
                {
                    cfg.AddProfile(item);
                }
            });
        }
    }
}