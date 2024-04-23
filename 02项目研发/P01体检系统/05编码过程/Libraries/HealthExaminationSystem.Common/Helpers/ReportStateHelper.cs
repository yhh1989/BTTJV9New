using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class ReportStateHelper
    {
        public static List<EnumModel> GetReportStates()
        {
            var result = new List<EnumModel>();
            var allowPrint = new EnumModel
            {
                Id = (int)ReportState.AllowPrint,
                Name = ReportState.AllowPrint.ToString(),
                Display = EnumHelper.GetEnumDesc(ReportState.AllowPrint)
            };
            result.Add(allowPrint);
            var notCompleted = new EnumModel
            {
                Id = (int)ReportState.NotCompleted,
                Name = ReportState.NotCompleted.ToString(),
                Display = EnumHelper.GetEnumDesc(ReportState.NotCompleted)
            };
            result.Add(notCompleted);
            var all = new EnumModel
            {
                Id = (int)ReportState.All,
                Name = ReportState.All.ToString(),
                Display = EnumHelper.GetEnumDesc(ReportState.All)
            };
            result.Add(all);
            return result;
        }
        /// <summary>
        /// 自定义报告领取格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ReportStateFormatter(object obj)
        {
            if (obj == null)
                return string.Empty;
            if (Enum.IsDefined(typeof(ReportState), obj))
            {
                return EnumHelper.GetEnumDesc((ReportState)obj);
            }
            return obj.ToString();
        }
    }
}
