using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BarSetting;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public static class CacheHelper
    { 

        #region 组合缓存
        /// <summary>
        /// 组合缓存Key
        /// </summary>
        private static readonly string CacheItemGroupsKey = $"{nameof(CacheHelper)}_{nameof(CacheItemGroupsKey)}";
        /// <summary>
        /// 清理组合缓存
        /// </summary>
        public static void ClearItemGroups()
        {
            CustomCache.Remove(CacheItemGroupsKey);
        }
        /// <summary>
        /// 获取项目组合列表
        /// </summary>
        /// <param name="reload">是否强制重载</param>
        /// <returns></returns>
        public static List<SimpleItemGroupDto> GetItemGroups(bool reload = false)
        {
            if (reload) CustomCache.Remove(CacheItemGroupsKey);
            try
            {
                List<SimpleItemGroupDto> data = CustomCache.Get(CacheItemGroupsKey, (Func<List<SimpleItemGroupDto>>)(() =>
                {
                    IItemGroupAppService svr = new ItemGroupAppService();
                    return svr.QuerySimples(new SearchItemGroupDto { });
                }), 600); // 10分钟
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
      

       


    }
}
