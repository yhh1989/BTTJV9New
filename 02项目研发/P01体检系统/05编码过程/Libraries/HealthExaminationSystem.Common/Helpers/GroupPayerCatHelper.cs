using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class GroupPayerCatHelper
    {
        /// <summary>
        /// 项目检查状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GroupPayerCatHelperFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(PayerCatType), obj))
            {
                return EnumHelper.GetEnumDesc((PayerCatType)obj);
            }
            return obj.ToString();
        }
    }
}
