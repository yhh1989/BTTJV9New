using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 回访方式
    /// </summary>
    public class VisitTypeHelper
    {
        private static List<EnumModel> _VisitTypeModels = new List<EnumModel>();
        static VisitTypeHelper()
        {
            var whole = new EnumModel
            {
                Id = 0,
                Name = "Whole",
                Display = "全部"
            };
            _VisitTypeModels.Add(whole);
            var notAlwaysCheck = new EnumModel
            {
                Id = (int)VisitType.Short,
                Name = VisitType.Short.ToString(),
                Display = "短信"
            };
            _VisitTypeModels.Add(notAlwaysCheck);

            var tell = new EnumModel
            {
                Id = (int)VisitType.Tel,
                Name = VisitType.Tel.ToString(),
                Display = "电话"
            };
            _VisitTypeModels.Add(tell);
            
        }

        public static List<EnumModel> GetSelectList()
        {
            return _VisitTypeModels;
        }
        /// <summary>
        /// 回访方式格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string VisitTypeFormatter(object obj)
        {
            if (obj == null)
                return string.Empty;
            if (Enum.IsDefined(typeof(VisitType), obj))
            {
                return EnumHelper.GetEnumDesc((VisitType)obj);
            }
            return obj.ToString();
        }
    }
}
