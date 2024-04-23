using System;
using DevExpress.Xpo;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{

    public class AddMinusTypeHelper 
    {
        /// <summary>
        /// 检查项目 加减项类别1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string AddMinusTypeHelperFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(AddMinusType), obj))
            {
                return EnumHelper.GetEnumDesc((AddMinusType)obj);
            }
            return obj.ToString();
        }
    }

}