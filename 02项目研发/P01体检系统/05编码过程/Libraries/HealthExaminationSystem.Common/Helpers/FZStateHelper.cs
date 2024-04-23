using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public  class FZStateHelper
    {
        private static readonly List<FZStateModel> PrintSateModels = new List<FZStateModel>();
        static FZStateHelper()
        {
            PrintSateModels.Add(new FZStateModel
            {
                Id = (int)FZState.not,
                Name = FZState.not.ToString(),
                Display = "未封账"
            });
            PrintSateModels.Add(new FZStateModel
            {
                Id = (int)FZState.Already,
                Name = FZState.Already.ToString(),
                Display = "已封账"
            });
            PrintSateModels.Add(new FZStateModel
            {
                Id = 0,
                Name ="ALL",
                Display = "全部"
            });
        }
        public static List<FZStateModel> GetFZStateModels()
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
            if (Enum.IsDefined(typeof(FZState), obj))
            {
                return EnumHelper.GetEnumDesc((FZState)obj);
            }
            return obj.ToString();
        }
    }
}
