using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public   class OperatorStateHelper
    {
        public static List<OperatorStateModel> GetList()
        {
            var model = new List<OperatorStateModel>();
            var Big = new OperatorStateModel
            {
                Id = (int)OperatorState.Big,
                Name = OperatorState.Big.ToString(),
                Display = "大于"
            };
            model.Add(Big);
            var BigEqual = new OperatorStateModel
            {
                Id = (int)OperatorState.BigEqual,
                Name = OperatorState.BigEqual.ToString(),
                Display = "大于等于"
            };
            model.Add(BigEqual);
            var Equal = new OperatorStateModel
            {
                Id = (int)OperatorState.Equal,
                Name = OperatorState.Equal.ToString(),
                Display = "等于"
            };
            model.Add(Equal);
            var Small = new OperatorStateModel
            {
                Id = (int)OperatorState.Small,
                Name = OperatorState.Small.ToString(),
                Display = "小于"
            };
            model.Add(Small);
            var SmallEqual = new OperatorStateModel
            {
                Id = (int)OperatorState.SmallEqual,
                Name = OperatorState.Big.ToString(),
                Display = "小于等于"
            };
            model.Add(SmallEqual);
            return model;
        }

        public static string OperatorStateFormatter(object obj)
        {
            if (obj == null)
                return "";
            if (obj == "")
                return "";
            if (Enum.IsDefined(typeof(OperatorState), obj))
            {
                return EnumHelper.GetEnumDesc((OperatorState)obj);
            }
            return obj.ToString();
        }
    }
}
