using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 孕/育状况
    /// </summary>
    public static class BreedStateHelper
    {
        public static List<BreedStateModel> GetBreedStateModels()
        {
            var result = new List<BreedStateModel>();
            var plan = new BreedStateModel
            {
                Id = (int)BreedState.PlanPregnancy,
                Name = BreedState.PlanPregnancy.ToString(),
                Display = EnumHelper.GetEnumDesc(BreedState.PlanPregnancy)
            };
            result.Add(plan);
            var gestation = new BreedStateModel
            {
                Id = (int)BreedState.Gestation,
                Name = BreedState.Gestation.ToString(),
                Display = EnumHelper.GetEnumDesc(BreedState.Gestation)
            };
            result.Add(gestation);
            var lactation = new BreedStateModel
            {
                Id = (int)BreedState.Lactation,
                Name = BreedState.Lactation.ToString(),
                Display = EnumHelper.GetEnumDesc(BreedState.Lactation)
            };
            result.Add(lactation);
            var nostate = new BreedStateModel
            {
                Id = (int)BreedState.No,
                Name = BreedState.No.ToString(),
                Display = EnumHelper.GetEnumDesc(BreedState.No)
            };
            result.Add(nostate);
            return result;
        }

        /// <summary>
        /// 孕/育状况格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string BreedStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(BreedState), obj))
            {
                return EnumHelper.GetEnumDesc((BreedState)obj);
            }
            return obj.ToString();

        }
    }
}
