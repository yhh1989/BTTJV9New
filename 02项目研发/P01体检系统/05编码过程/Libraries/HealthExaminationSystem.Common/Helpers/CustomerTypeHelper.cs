using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class CustomerTypeHelper
    {
        /// <summary>
        /// 客户类别格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CustomerTypeFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(CustomerType), obj))
            {
                return EnumHelper.GetEnumDesc((CustomerType)obj);
            }
            return obj.ToString();
        }
    }
}
