using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public   class CriticalTypeStateHelper
    { 
        public static List<CriticalTypeStateModel> GetList()
        {
            var model = new List<CriticalTypeStateModel>();
            var Al = new CriticalTypeStateModel
            {
                Id = (int)CriticalTypeState.A,
                Name = CriticalTypeState.A.ToString(),
                Display = "A类"
            };
            model.Add(Al);
            var Bl = new CriticalTypeStateModel
            {
                Id = (int)CriticalTypeState.B,
                Name = CriticalTypeState.B.ToString(),
                Display = "B类"
            };
            model.Add(Bl);
            var CL = new CriticalTypeStateModel
            {
                Id = (int)CriticalTypeState.C,
                Name = CriticalTypeState.C.ToString(),
                Display = "C类"
            };
            model.Add(CL);
            var DL = new CriticalTypeStateModel
            {
                Id = (int)CriticalTypeState.D,
                Name = CriticalTypeState.D.ToString(),
                Display = "D类"
            };
            model.Add(DL);
            return model;
        }
        /// <summary>
        /// 危急值状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CriticalTypeStateFormatter(object obj)
        {
            if (obj == null)
            {
                return "未知";
            }
            if (Enum.IsDefined(typeof(CriticalTypeState), obj))
            {
                return EnumHelper.GetEnumDesc((CriticalTypeState)obj);
            }
            return obj.ToString();
        }
    }
}
