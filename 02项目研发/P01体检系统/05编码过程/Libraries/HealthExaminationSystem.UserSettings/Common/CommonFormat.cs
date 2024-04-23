using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Common
{
    public static class CommonFormat
    {
        public static string YesOrNoFormatter(object obj)
        {
            return (int.TryParse(obj?.ToString(), out int val) && val == 1) ? "是" : "否";
        }

        /// <summary>
        /// 基本字典格式化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string BasicDictionaryFormatter(BasicDictionaryType type, object obj)
        {
            if (obj != null && obj.ToString()!="" && int.TryParse(obj.ToString(),out int Simble))
            {
                var bd = DefinedCacheHelper.GetBasicDictionaryByValue(type, Convert.ToInt32(obj));
                if (bd != null) return bd.Text;
            }
            return obj.ToString();
        }
        
        /// <summary>
        /// 套餐类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ItemSuitTypeFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ItemSuitType), obj))
            {
                return EnumHelper.GetEnumDesc((ItemSuitType)obj);
            }
            return obj.ToString();
        }

    }
}
