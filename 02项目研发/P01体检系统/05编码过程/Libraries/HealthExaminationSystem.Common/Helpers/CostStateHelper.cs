using System;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 收费状态
    /// </summary>
    public class CostStateHelper 
    {
        /// <summary>
        /// 收费状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CostStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(PayerCatType), obj))
            {
                return EnumHelper.GetEnumDesc((PayerCatType)obj);
            }
            return obj.ToString();
        }
    }

}