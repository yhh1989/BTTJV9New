using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class PositiveStateHelper
    {
        public static List<PositiveStateModel> GetPositiveStateModels()
        {
            var result = new List<PositiveStateModel>();
            var Abnormal = new PositiveStateModel {
                Id= (int)PositiveSate.Abnormal,
                Name=PositiveSate.Abnormal.ToString(),
                Display="异常"
            };
            result.Add(Abnormal);
            var Normal = new PositiveStateModel
            {
                Id = (int)PositiveSate.Normal,
                Name = PositiveSate.Normal.ToString(),
                Display = "正常"
            };
            result.Add(Normal);
            return result;
        }
        /// <summary>
        /// 阳性格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PositiveStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(PositiveSate), obj))
            {
                return EnumHelper.GetEnumDesc((PositiveSate)obj);
            }
            return obj.ToString();
        }
    }
}
