using System;
using System.Collections.Generic;
using HealthExaminationSystem.Enumerations.Models;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace HealthExaminationSystem.Enumerations.Helpers
{
    /// <summary>
    /// 性别帮助
    /// </summary>
    public static class SexHelper
    {
        private static readonly List<SexModel> CustomSexes = new List<SexModel>();

        private static readonly List<SexModel> CustomSexesForItemInfo = new List<SexModel>();

        static SexHelper()
        {
            CustomSexes.Add(new SexModel
            {
                Id = (int)Sex.Man,
                Name = Sex.Man.ToString(),
                Display = "男"
            });
            CustomSexes.Add(new SexModel
            {
                Id = (int)Sex.Woman,
                Name = Sex.Woman.ToString(),
                Display = "女"
            });
            CustomSexesForItemInfo.AddRange(CustomSexes);
            CustomSexesForItemInfo.Add(new SexModel
            {
                Id = (int)Sex.GenderNotSpecified,
                Name = Sex.GenderNotSpecified.ToString(),
                Display = "未说明的性别"
            });
        }

        /// <summary>
        /// 获取系统性别列表
        /// </summary>
        /// <returns></returns>
        public static List<SexModel> GetSexModels()
        {
            return CustomSexes;
        }
        /// <summary>
        /// 获取性别列表
        /// </summary>
        /// <returns></returns>
        public static List<SexModel> GetSexForPerson()
        {
            var list = CustomSexes;
            if (list.Find(o => o.Id == (int)Sex.GenderNotSpecified) == null)
            {
                list.Add(new SexModel
                {
                    Id = (int)Sex.GenderNotSpecified,
                    Name = Sex.GenderNotSpecified.ToString(),
                    Display = "未说明的性别"
                });
            }
            
            return list;
        }

        /// <summary>
        /// 获取项目设置性别选项列表
        /// </summary>
        /// <returns></returns>
        public static List<SexModel> GetSexModelsForItemInfo()
        {
            return CustomSexesForItemInfo;
        }

        /// <summary>
        /// 自定义性别格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CustomSexFormatter(object obj)
        {
            try
            {
                if (obj == null)
                { return null; }
                else
                {
                    return CustomSexesForItemInfo.Find(r => r.Id == (int)obj).Display;
                }
            }
            catch
            {
                return CustomSexesForItemInfo.Find(r => r.Id == (int)Sex.GenderNotSpecified).Display;
            }
        }

        /// <summary>
        /// 枚举性别格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("暂停使用！", true)]
        public static string SexFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(Sex), obj))
                return EnumHelper.GetEnumDesc((Sex)obj);

            return obj.ToString();
        }
    }
}