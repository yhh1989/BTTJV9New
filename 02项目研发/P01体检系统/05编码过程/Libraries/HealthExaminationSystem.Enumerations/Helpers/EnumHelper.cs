using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 枚举的描述获取类
    /// by yjh
    /// </summary>
    public static class EnumHelper
    {
        public static string GetEnumDesc(Enum e)
        {
            var EnumInfo = e.GetType().GetField(e.ToString());
            if (EnumInfo.IsDefined(typeof(DescriptionAttribute), false))
            {
                var desc = (DescriptionAttribute)EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
                return desc.Description;
            }

            return e.ToString();
        }

        public static List<KeyValuePair<Enum, string>> GetEnumDescs(Type enumType)
        {
            var list = new List<KeyValuePair<Enum, string>>();
            foreach (Enum item in Enum.GetValues(enumType))
                list.Add(new KeyValuePair<Enum, string>(item, GetEnumDesc(item)));
            return list;
        }
    }
}