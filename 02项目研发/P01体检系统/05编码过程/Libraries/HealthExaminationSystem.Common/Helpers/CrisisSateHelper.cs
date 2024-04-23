using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public  class CrisisSateHelper
    {
        /// <summary>
        /// 危急值状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CrisisSateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(CrisisSate), obj))
            {
                return EnumHelper.GetEnumDesc((CrisisSate)obj);
            }
            return obj.ToString();
        }
    }
}
