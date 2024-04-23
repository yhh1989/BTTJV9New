using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 回访状态
    /// </summary>
    public class VisiteStateHelper
    {
        private static List<EnumModel> _VisitTypeModels = new List<EnumModel>();
        static VisiteStateHelper()
        {
            var whole = new EnumModel
            {
                Id = 3,
                Name = "Whole",
                Display = "全部"
            };
            _VisitTypeModels.Add(whole);
            var notAlwaysCheck = new EnumModel
            {
                Id = (int)VisiteState.NoVisite,
                Name = VisiteState.NoVisite.ToString(),
                Display = "未回访"
            };
            _VisitTypeModels.Add(notAlwaysCheck);

            var tell = new EnumModel
            {
                Id = (int)VisiteState.HasVisite,
                Name = VisiteState.HasVisite.ToString(),
                Display = "已回访"
            };
            _VisitTypeModels.Add(tell);

            var Cancel = new EnumModel
            {
                Id = (int)VisiteState.CancelVisite,
                Name = VisiteState.CancelVisite.ToString(),
                Display = "已取消"
            };
            _VisitTypeModels.Add(Cancel);

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
        public static string VisiteStateFormatter(object obj)
        {
            if (obj == null)
                return string.Empty;
            if (Enum.IsDefined(typeof(VisiteState), obj))
            {
                return EnumHelper.GetEnumDesc((VisiteState)obj);
            }
            return obj.ToString();
        }
    }
}
