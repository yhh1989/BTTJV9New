using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class SymbolHelper
    {
        /// <summary>
        /// 项目标示格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SymbolFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(Symbol), obj))
            {
                return EnumHelper.GetEnumDesc((Symbol)obj);
            }
            return obj.ToString();
        }
        public static List<LabelingStateModel> GetLabelingStateModels()
        {
            var result = new List<LabelingStateModel>();
            var Normal = new LabelingStateModel
            {
                Id = (int)Symbol.Normal,
                Name = Symbol.Normal.ToString(),
                Display = "正常"
            };
            result.Add(Normal);
            var High = new LabelingStateModel
            {
                Id = (int)Symbol.High,
                Name = Symbol.High.ToString(),
                Display = "偏高"
            };
            result.Add(High);
            var ExtremelyHigh = new LabelingStateModel
            {
                Id = (int)Symbol.Superhigh,
                Name = Symbol.Superhigh.ToString(),
                Display = "极高"
            };
            result.Add(ExtremelyHigh);
            var Low = new LabelingStateModel
            {
                Id = (int)Symbol.Low,
                Name = Symbol.Low.ToString(),
                Display = "偏低"
            };
            result.Add(Low);
            var ExtremelyLow = new LabelingStateModel
            {
                Id = (int)Symbol.UltraLow,
                Name = Symbol.UltraLow.ToString(),
                Display = "极低"
            };
            result.Add(ExtremelyLow);
            var Abnormal = new LabelingStateModel
            {
                Id = (int)Symbol.Abnormal,
                Name = Symbol.Abnormal.ToString(),
                Display = "异常"
            };
            result.Add(Abnormal);
            return result;
        }
    }
}
