using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 枚举的描述获取类
    /// by yjh
    /// </summary>
    public static class PhysicalTypeHelper
    {
        static PhysicalTypeHelper()
        {
            PhysicalTypeForItemInfo.Add(new PhysicalTypeModel
            {
                Id = (int)PhysicalType.Health,
                Name = PhysicalType.Health.ToString(),
                Display = "健康体检"
            });
            PhysicalTypeForItemInfo.Add(new PhysicalTypeModel
            {
                Id = (int)PhysicalType.Occupational,
                Name = PhysicalType.Occupational.ToString(),
                Display = "健康体检"
            });
       
            PhysicalTypeForItemInfo.Add(new PhysicalTypeModel
            {
                Id = (int)PhysicalType.HealthCertificate,
                Name = PhysicalType.HealthCertificate.ToString(),
                Display = "健康证体检"
            });
            PhysicalTypeForItemInfo.Add(new PhysicalTypeModel
            {
                Id = (int)PhysicalType.Civil,
                Name = PhysicalType.Civil.ToString(),
                Display = "公务员体检"
            });
            PhysicalTypeForItemInfo.Add(new PhysicalTypeModel
            {
                Id = (int)PhysicalType.Student,
                Name = PhysicalType.Student.ToString(),
                Display = "学生体检"
            });
            PhysicalTypeForItemInfo.Add(new PhysicalTypeModel
            {
                Id = (int)PhysicalType.Driver,
                Name = PhysicalType.Driver.ToString(),
                Display = "驾驶证体检"
            });
            PhysicalTypeForItemInfo.Add(new PhysicalTypeModel
            {
                Id = (int)PhysicalType.Marriage,
                Name = PhysicalType.Marriage.ToString(),
                Display = "婚检"
            });
        }
        private static readonly List<PhysicalTypeModel> PhysicalTypeForItemInfo = new List<PhysicalTypeModel>();
        public static string GetEnumDesc(Enum e)
        {
            FieldInfo EnumInfo = e.GetType().GetField(e.ToString());
            if(EnumInfo.IsDefined(typeof(DescriptionAttribute), false))
            {
                DescriptionAttribute desc = (DescriptionAttribute)EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
                return desc.Description;
            }
            return e.ToString();
        }

        public static List<KeyValuePair<Enum, string>> GetEnumDescs(Type enumType)
        {
            List<KeyValuePair<Enum, string>> list = new List<KeyValuePair<Enum, string>>();
            foreach (Enum item in Enum.GetValues(enumType))
            {
                list.Add(new KeyValuePair<Enum, string>(item, GetEnumDesc((Enum)item)));
            }
            return list;
        }
        /// <summary>
        /// 自定义体检类别格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PhysicalTypeFormatter(object obj)
        {
            try
            {
                return PhysicalTypeForItemInfo.Find(r => r.Id == (int)obj).Display;
            }
            catch
            {
                return PhysicalTypeForItemInfo.Find(r => r.Id == (int)PhysicalType.Health).Display;
            }
        }
    }
}
