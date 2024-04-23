using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
 public    class CalculationTypeStateHelper
    {
        public static List<CalculationTypeStateModel> GetList()
        {
            var model = new List<CalculationTypeStateModel>();
            var Numerical = new CalculationTypeStateModel
            {
                Id = (int)CalculationTypeState.Numerical,
                Name = CalculationTypeState.Numerical.ToString(),
                Display = "数值型"
            };
            model.Add(Numerical);
            var Text = new CalculationTypeStateModel
            {
                Id = (int)CalculationTypeState.Text,
                Name = CalculationTypeState.Text.ToString(),
                Display = "文本"
            };
            model.Add(Text);
            return model;
        }
    }
}
