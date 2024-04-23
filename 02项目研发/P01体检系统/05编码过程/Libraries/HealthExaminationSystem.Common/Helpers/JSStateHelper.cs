using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public  class JSStateHelper
    {
        private static readonly List<JSStateModel> PrintSateModels = new List<JSStateModel>();
        static JSStateHelper()
        {
            PrintSateModels.Add(new JSStateModel
            {
                Id = (int)JSState.not,
                Name = JSState.not.ToString(),
                Display = "未结算"
            });
            PrintSateModels.Add(new JSStateModel
            {
                Id = (int)JSState.Already,
                Name = JSState.Already.ToString(),
                Display = "已结算"
            });
            PrintSateModels.Add(new JSStateModel
            {
                Id = 2,
                Name = "ALL",
                Display = "全部"
            });
        }
        public static List<JSStateModel> GetJSStateModels()
        {
            return PrintSateModels;
        }
        /// <summary>
        /// 打印类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FZStateType(object obj)
        {
            if (Enum.IsDefined(typeof(JSState), obj))
            {
                return EnumHelper.GetEnumDesc((JSState)obj);
            }
            return obj.ToString();
        }
    }
}
