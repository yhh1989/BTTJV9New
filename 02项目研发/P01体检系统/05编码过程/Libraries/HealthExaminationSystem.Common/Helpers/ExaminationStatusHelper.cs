using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class ExaminationStatusHelper
    {
        public static List<EnumModel> GetExaminationStatus()
        {
            var result = new List<EnumModel>();
            var unregistered = new EnumModel
            {
                Id = (int)ExaminationStatus.Unregistered,
                Name = ExaminationStatus.Unregistered.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.Unregistered)
            };
            result.Add(unregistered);
            var registered = new EnumModel
            {
                Id = (int)ExaminationStatus.Registered,
                Name = ExaminationStatus.Registered.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.Registered)
            };
            result.Add(registered);

            var examining = new EnumModel
            {
                Id = (int)ExaminationStatus.Examining,
                Name = ExaminationStatus.Examining.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.Examining)
            };
            result.Add(examining);
            var examinationCompleted = new EnumModel
            {
                Id = (int)ExaminationStatus.ExaminationCompleted,
                Name = ExaminationStatus.ExaminationCompleted.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.ExaminationCompleted)
            };
            result.Add(examinationCompleted);

            var totalExaminationCompleted = new EnumModel
            {
                Id = (int)ExaminationStatus.TotalExaminationCompleted,
                Name = ExaminationStatus.TotalExaminationCompleted.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.TotalExaminationCompleted)
            };
            result.Add(totalExaminationCompleted);
            var totalExaminationCheckedCompleted = new EnumModel
            {
                Id = (int)ExaminationStatus.TotalExaminationCheckedCompleted,
                Name = ExaminationStatus.TotalExaminationCheckedCompleted.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.TotalExaminationCheckedCompleted)
            };
            result.Add(totalExaminationCheckedCompleted);
            var reportOupput = new EnumModel
            {
                Id = (int)ExaminationStatus.ReportOupput,
                Name = ExaminationStatus.ReportOupput.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.ReportOupput)
            };
            result.Add(reportOupput);

            var reportPrint = new EnumModel
            {
                Id = (int)ExaminationStatus.ReportPrint,
                Name = ExaminationStatus.ReportPrint.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.ReportPrint)
            };
            result.Add(reportOupput);
            var reportExport = new EnumModel
            {
                Id = (int)ExaminationStatus.ReportExport,
                Name = ExaminationStatus.ReportExport.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationStatus.ReportExport)
            };
            result.Add(reportExport);
            return result;
        }
        /// <summary>
        /// 体检状态格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ExaminationStatusFormatter(object obj)
        {
            if (obj == null)
                return string.Empty;
            if (Enum.IsDefined(typeof(ExaminationStatus), obj))
            {
                return EnumHelper.GetEnumDesc((ExaminationStatus)obj);
            }
            return obj.ToString();
        }
    }
}
