using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 登记信息中 吃不吃早餐类型
    /// </summary>
    public static class BreakfastTypeHelper
    {
        public static List<EnumModel> GetBreakfastTypes()
        {
            var result = new List<EnumModel>();
            var eat = new EnumModel
            {
                Id = (int)BreakfastType.Eat,
                Name = BreakfastType.Eat.ToString(),
                Display = EnumHelper.GetEnumDesc(BreakfastType.Eat)
            };
            result.Add(eat);
            var noteat = new EnumModel
            {
                Id = (int)BreakfastType.NotEat,
                Name = BreakfastType.NotEat.ToString(),
                Display = EnumHelper.GetEnumDesc(BreakfastType.NotEat)
            };
            result.Add(noteat);
            var eaten = new EnumModel
            {
                Id = (int)BreakfastType.Eaten,
                Name = BreakfastType.Eaten.ToString(),
                Display = EnumHelper.GetEnumDesc(BreakfastType.Eaten)
            };
            result.Add(eaten);
            return result;
        }
    }
}
