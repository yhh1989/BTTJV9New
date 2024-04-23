using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class IsZYBStateHelper
    {
        public static List<IsZYBStateModel> GetZYBStateModels()
        {
            var result = new List<IsZYBStateModel>();
            var ZYB = new IsZYBStateModel
            {
                Id = (int)IsZYBState.ZYB,
                Name = IsZYBState.ZYB.ToString(),
                Display = "职业健康项目"
            };
            result.Add(ZYB);
            var health = new IsZYBStateModel
            {
                Id = (int)IsZYBState.health,
                Name = IsZYBState.health.ToString(),
                Display = "健康体检项目"
            };
            result.Add(health);

            var All = new IsZYBStateModel
            {
                Id = (int)IsZYBState.ALL,
                Name = IsZYBState.ALL.ToString(),
                Display = "职业+健康"
            };
            result.Add(All);
            return result;
        }
    }
}
