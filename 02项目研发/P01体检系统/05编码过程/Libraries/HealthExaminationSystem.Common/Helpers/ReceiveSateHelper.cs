using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    //状态领取
    public class ReceiveSateHelper
    {
        /// <summary>
        /// 领取状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ReceiveSateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ReceiveSate), obj))
            {
                return EnumHelper.GetEnumDesc((ReceiveSate)obj);
            }
            return obj.ToString();
        }
    }
}
