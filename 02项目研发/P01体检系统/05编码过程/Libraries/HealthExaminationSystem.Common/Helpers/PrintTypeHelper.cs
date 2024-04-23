using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
/// 打印状态帮助
/// </summary>
   public static class PrintTypeHelper
    {
        /// <summary>
        /// 打印类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PrintType(object obj)
        {
            if (Enum.IsDefined(typeof(PrintSate), obj))
            {
                return EnumHelper.GetEnumDesc((PrintSate)obj);
            }
            return obj.ToString();
        }
    }
}
