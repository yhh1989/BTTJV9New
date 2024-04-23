using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public static class MarrySateHelper
    {
        private static readonly List<MarrySateModel> CustomMarrySates = new List<MarrySateModel>();
        private static readonly List<MarrySateModel> CustomMarrySatesForItemInfo = new List<MarrySateModel>();
        static MarrySateHelper()
        {
            CustomMarrySates.Add(new MarrySateModel
            {
                Id = (int)MarrySate.Unmarried,
                Name = MarrySate.Unmarried.ToString(),
                Display = EnumHelper.GetEnumDesc(MarrySate.Unmarried)
            });
            CustomMarrySates.Add(new MarrySateModel
            {
                Id = (int)MarrySate.Married,
                Name = MarrySate.Married.ToString(),
                Display = EnumHelper.GetEnumDesc(MarrySate.Married)
            });
            CustomMarrySatesForItemInfo.Add(new MarrySateModel
            {
                Id = (int)MarrySate.Unstated,
                Name = MarrySate.Unstated.ToString(),
                Display = "未知"
            });
            CustomMarrySatesForItemInfo.Add(new MarrySateModel
            {
                Id = (int)MarrySate.Divorce,
                Name = MarrySate.Divorce.ToString(),
                Display = EnumHelper.GetEnumDesc(MarrySate.Divorce)
            });
            CustomMarrySatesForItemInfo.Add(new MarrySateModel
            {
                Id = (int)MarrySate.FirstMarriage,
                Name = MarrySate.FirstMarriage.ToString(),
                Display = EnumHelper.GetEnumDesc(MarrySate.FirstMarriage)
            });
            CustomMarrySatesForItemInfo.Add(new MarrySateModel
            {
                Id = (int)MarrySate.Remarriage,
                Name = MarrySate.Remarriage.ToString(),
                Display = EnumHelper.GetEnumDesc(MarrySate.Remarriage)
            });
            CustomMarrySatesForItemInfo.Add(new MarrySateModel
            {
                Id = (int)MarrySate.Remarry,
                Name = MarrySate.Remarry.ToString(),
                Display = EnumHelper.GetEnumDesc(MarrySate.Remarry)
            });
            CustomMarrySatesForItemInfo.Add(new MarrySateModel
            {
                Id = (int)MarrySate.Widowhoo,
                Name = MarrySate.Widowhoo.ToString(),
                Display = EnumHelper.GetEnumDesc(MarrySate.Widowhoo)
            });
            CustomMarrySatesForItemInfo.AddRange(CustomMarrySates);
        }

        /// <summary>
        /// 获取系统婚姻状况列表
        /// </summary>
        /// <returns></returns>
        public static List<MarrySateModel> GetMarrySateModels()
        {
            return CustomMarrySates;
        }

        /// <summary>
        /// 获取系统婚姻状况列表
        /// </summary>
        /// <returns></returns>
        public static List<MarrySateModel> GetMarrySateModelsForItemInfo()
        {
            return CustomMarrySatesForItemInfo;
        }

        /// <summary>
        /// 自定义婚姻状况格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CustomMarrySateFormatter(object obj)
        {
            if (int.TryParse(obj?.ToString(), out int val))
            {
                var sex = CustomMarrySatesForItemInfo.FirstOrDefault(m => m.Id == val);
                if (sex != null) return sex.Display;
            }
            return obj?.ToString();
        }

        /// <summary>
        /// 枚举婚姻状况格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string MarrySateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(MarrySate), obj))
            {
                return EnumHelper.GetEnumDesc((MarrySate)obj);
            }
            return obj.ToString();
        }
    }
}
