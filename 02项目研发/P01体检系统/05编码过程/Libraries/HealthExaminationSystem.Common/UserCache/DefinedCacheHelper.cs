using System;
using System.Collections.Generic;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Common.UserCache
{
    /// <summary>
    /// 已定义的缓存帮助
    /// </summary>
    public static class DefinedCacheHelper
    {
        /// <summary>
        /// 已定义的 API 代理
        /// </summary>
        public static DefinedApiProxy DefinedApiProxy { get; }

        /// <summary>
        /// 科室缓存键
        /// </summary>
        public const string GetDepartmentsKey = "D82C3B80-018D-4412-ACAA-E2096161DC8C";

        /// <summary>
        /// 窗体模块缓存键
        /// </summary>
        public const string GetFormModulesKey = "21C6FB37-5423-48BD-9E8B-B763A03EDB18";

        /// <summary>
        /// 获取下拉用户缓存键
        /// </summary>
        public const string GetComboUsersKey = "8A801FE0-A0C7-4CAB-B7F1-6583896655E9";

        /// <summary>GetBasicDictionaryKey
        /// 获取项目套餐数据缓存键
        /// </summary>
        public const string GetItemSuitKey = "AD878AA8-CC74-427B-895E-C8432218B6BC";

        /// <summary>
        /// 获取基础字典数据缓存键
        /// </summary>
        public const string GetBasicDictionaryKey = "C5CFAD69-6180-45A8-9F59-64ABDEF75AC7";

        /// <summary>
        /// 获取行政区划缓存键
        /// </summary>
        public const string GetAdministrativeDivisionKey = "A9144F61-D0D5-4F84-BB6B-6FAB0BB7E00B";

        /// <summary>
        /// 单位信息应用服务缓存键
        /// </summary>
        private const string GetAllClientInfoCacheKey = "9904FC21-C39D-4C3F-B3AD-BF4292D99BFB";

        /// <summary>
        /// 单位预约缓存
        /// </summary>
        public const string GetClientRegNameComKey = "A61EE74D-24B9-471F-8103-74968BF29E2F";

        /// <summary>
        /// 项目缓存
        /// </summary>
        public const string GetItemInfosKey = "109417A5-6373-46D8-B4D8-5E4B0E96F3C1";

        /// <summary>
        /// 组合缓存
        /// </summary>
        public const string GetItemGroupKey = "8FD99625-0F30-4406-A532-D566851A4164";

        /// <summary>
        /// 建议缓存
        /// </summary>
        public const string GetSummarizeAdviceKey = "E77D5F67-8549-4ED3-8B1D-32C8FA75EB79";

        /// <summary>
        /// 参考值
        /// </summary>
        public const string GetItemstands = "E77D5F67-8549-4ED3-8B1D-32C8FA75EB89";
        /// <summary>
        /// 总检隐藏诊断
        /// </summary>
        public const string GetSumHideKey = "6F7FF4D3-ACE9-4A6C-AF64-4D32DD6D9EED";
        static DefinedCacheHelper()
        {
            DefinedApiProxy = new DefinedApiProxy();
        }

        /// <summary>
        /// 刷新所有缓存
        /// </summary>
        /// <param name="method">设置 Loading 框内容</param>
        public static void Refresh(Action<string> method)
        {
            method.Invoke("更新科室缓存数据...");
            UpdateDepartments();
            method.Invoke("更新窗体模块缓存数据...");
            UpdateFormModules();
            method.Invoke("更新下拉用户缓存数据...");
            UpdateComboUsers();
            method.Invoke("更新项目套餐缓存数据...");
            UpdateItemSuit();
            method.Invoke("更新基础字典缓存数据...");
            UpdateBasicDictionary();
            method.Invoke("更新行政区划缓存数据...");
            UpdateAdministrativeDivision();
            method.Invoke("更新所有缓存单位信息...");
            UpdateAllClientInfoCache();
            method.Invoke("更新单位预约缓存数据...");
            UpdateClientRegNameComDto();
            method.Invoke("更新组合缓存数据...");
            UpdateItemGroups();


            method.Invoke("更新项目缓存数据...");
            UpdateItemInfos();



            //暂时过滤           
            //method.Invoke("更新建议缓存数据...");
            //UpdateSummarizeAdvices();

            //method.Invoke("更新总检屏蔽诊断...");
            //UpdateSumHides();
        }

        /// <summary>
        /// 获取科室缓存
        /// </summary>
        /// <returns></returns>
        public static List<TbmDepartmentDto> GetDepartments()
        {
            return CacheHelper.GetCacheItem(GetDepartmentsKey, DefinedApiProxy.DepartmentAppService.GetAll);
        }

        /// <summary>
        /// 更新科室缓存
        /// </summary>
        /// <returns></returns>
        public static List<TbmDepartmentDto> UpdateDepartments()
        {
            return CacheHelper.UpdateCacheItem(GetDepartmentsKey, DefinedApiProxy.DepartmentAppService.GetAll);
        }
        /// <summary>
        /// 获取参考值缓存
        /// </summary>
        /// <returns></returns>
        public static List<ItemStandardDto> GetGetGenerateSummarys()
        {
            return CacheHelper.GetCacheItem(GetItemstands, DefinedApiProxy.ItemInfoAppService.GetAllItemStandard);
        }

        /// <summary>
        /// 更新参考值缓存
        /// </summary>
        /// <returns></returns>
        public static List<ItemStandardDto> UpdateGetGenerateSummarys()
        {
            return CacheHelper.UpdateCacheItem(GetItemstands, DefinedApiProxy.ItemInfoAppService.GetAllItemStandard);
        }

        /// <summary>
        /// 获取窗体模块缓存
        /// </summary>
        /// <returns></returns>
        public static List<FormModuleDto> GetFormModules()
        {
            return CacheHelper.GetCacheItem(GetFormModulesKey, DefinedApiProxy.FormModuleAppService.GetAllList);
        }

        /// <summary>
        /// 更新窗体模块缓存
        /// </summary>
        /// <returns></returns>
        public static List<FormModuleDto> UpdateFormModules()
        {
            return CacheHelper.UpdateCacheItem(GetFormModulesKey, DefinedApiProxy.FormModuleAppService.GetAllList);
        }

        /// <summary>
        /// 获取下拉用户列表
        /// </summary>
        /// <returns></returns>
        public static List<UserForComboDto> GetComboUsers()
        {
            return CacheHelper.GetCacheItem(GetComboUsersKey, DefinedApiProxy.UserAppService.GetComboUsers);
        }

        /// <summary>
        /// 更新下拉用户列表
        /// </summary>
        /// <returns></returns>
        public static List<UserForComboDto> UpdateComboUsers()
        {
            return CacheHelper.UpdateCacheItem(GetComboUsersKey, DefinedApiProxy.UserAppService.GetComboUsers);
        }

        /// <summary>
        /// 获取项目套餐缓存
        /// </summary>
        /// <returns></returns>
        public static List<SimpleItemSuitDto> GetItemSuit()
        {
            return CacheHelper.GetCacheItem(GetItemSuitKey, DefinedApiProxy.ItemSuitAppService.QuerySimplesCache);
        }

        /// <summary>
        /// 更新项目套餐缓存
        /// </summary>
        /// <returns></returns>
        public static List<SimpleItemSuitDto> UpdateItemSuit()
        {
            return CacheHelper.UpdateCacheItem(GetItemSuitKey, DefinedApiProxy.ItemSuitAppService.QuerySimplesCache);
        }

        /// <summary>
        /// 获取基础字典数据缓存
        /// </summary>
        /// <returns></returns>
        public static List<BasicDictionaryDto> GetBasicDictionary()
        {
            return CacheHelper.GetCacheItem(GetBasicDictionaryKey, DefinedApiProxy.BasicDictionaryAppService.QueryCache);
        }
        /// <summary>
        /// 获取基础字典数据缓存
        /// </summary>
        /// <returns></returns>
        public static List<BasicDictionaryDto> GetBasicDictionarys(BasicDictionaryType input)
        {
          var basicDiction=  CacheHelper.GetCacheItem(GetBasicDictionaryKey, DefinedApiProxy.BasicDictionaryAppService.QueryCache);
           return basicDiction.Where(o => o.Type == input.ToString()).ToList();
        }
        /// <summary>
        /// 获取基础字典数据缓存
        /// </summary>
        /// <returns></returns>
        public static BasicDictionaryDto GetBasicDictionaryByValue(BasicDictionaryType input,int value)
        {
            var basicDiction = CacheHelper.GetCacheItem(GetBasicDictionaryKey, DefinedApiProxy.BasicDictionaryAppService.QueryCache);
            return basicDiction.FirstOrDefault(o => o.Type == input.ToString() && o.Value== value);
        }
        
        /// <summary>
        ///  更新基础字典数据缓存
        /// </summary>
        /// <returns></returns>
        public static List<BasicDictionaryDto> UpdateBasicDictionary()
        {
            return CacheHelper.UpdateCacheItem(GetBasicDictionaryKey, DefinedApiProxy.BasicDictionaryAppService.QueryCache);
        }

        /// <summary>
        /// 获取行政区划数据缓存
        /// </summary>
        /// <returns></returns>
        public static List<AdministrativeDivisionDto> GetAdministrativeDivision()
        {
            return CacheHelper.GetCacheItem(GetAdministrativeDivisionKey, DefinedApiProxy.CommonAppService.GetAdministrativeDivisions);
        }

        /// <summary>
        ///  更新行政区划数据缓存
        /// </summary>
        /// <returns></returns>
        public static List<AdministrativeDivisionDto> UpdateAdministrativeDivision()
        {
            return CacheHelper.UpdateCacheItem(GetAdministrativeDivisionKey, DefinedApiProxy.CommonAppService.GetAdministrativeDivisions);
        }
        /// <summary>
        ///  获取单位预约缓存
        /// </summary>
        /// <returns></returns>
        public static List<ClientRegNameComDto> GetClientRegNameComDto()
        {
            return CacheHelper.GetCacheItem(GetClientRegNameComKey, DefinedApiProxy.ClientRegAppService.getClientRegNameCom);
        }
        /// <summary>
        ///  更新单位预约缓存
        /// </summary>
        /// <returns></returns>
        public static List<ClientRegNameComDto> UpdateClientRegNameComDto()
        {
            return CacheHelper.UpdateCacheItem(GetClientRegNameComKey, DefinedApiProxy.ClientRegAppService.getClientRegNameCom);
        }

        public static List<ItemInfoSimpleDto> GetItemInfos()
        {
            return CacheHelper.GetCacheItem(GetItemInfosKey, DefinedApiProxy.ItemInfoAppService.QuerySimples);
        }
        public static List<ItemInfoSimpleDto> UpdateItemInfos()
        {
            return CacheHelper.UpdateCacheItem(GetItemInfosKey, DefinedApiProxy.ItemInfoAppService.QuerySimples);
        }
        public static List<SimpleItemGroupDto> GetItemGroups()
        {
            return CacheHelper.GetCacheItem(GetItemGroupKey, DefinedApiProxy.GroupAppService.SimpleGroup);
        }
        public static List<SimpleItemGroupDto> UpdateItemGroups()
        {
            return CacheHelper.UpdateCacheItem(GetItemGroupKey, DefinedApiProxy.GroupAppService.SimpleGroup);
        }

        public static List<SummarizeAdviceDto> GetSummarizeAdvices()
        {
            return CacheHelper.GetCacheItem(GetSummarizeAdviceKey, DefinedApiProxy.SummarizeAdviceAppService.GetAllSummAll);
        }
        public static List<SummarizeAdviceDto> UpdateSummarizeAdvices()
        {
            return CacheHelper.UpdateCacheItem(GetSummarizeAdviceKey, DefinedApiProxy.SummarizeAdviceAppService.GetAllSummAll);
        }
        public static List<TbmSumHideDto>  GetSumHides()
        {
            return CacheHelper.GetCacheItem(GetSumHideKey, DefinedApiProxy.InspectionTotalService.SearchSumHide);
        }
        public static List<TbmSumHideDto> UpdateSumHides()
        {
            return CacheHelper.UpdateCacheItem(GetSumHideKey, DefinedApiProxy.InspectionTotalService.SearchSumHide);
        }

        /// <summary>
        /// 获取所有缓存单位信息
        /// </summary>
        /// <returns></returns>
        public static List<ClientInfoCacheDto> GetAllClientInfoCache()
        {
            return CacheHelper.GetCacheItem(GetAllClientInfoCacheKey,
                () => DefinedApiProxy.ClientInfoesAppService.GetAllForCache().Result);
        }

        /// <summary>
        /// 更新所有缓存单位信息
        /// </summary>
        /// <returns></returns>
        private static void UpdateAllClientInfoCache()
        {
            CacheHelper.UpdateCacheItem(GetAllClientInfoCacheKey,
                () => DefinedApiProxy.ClientInfoesAppService.GetAllForCache().Result);
        }
    }
}