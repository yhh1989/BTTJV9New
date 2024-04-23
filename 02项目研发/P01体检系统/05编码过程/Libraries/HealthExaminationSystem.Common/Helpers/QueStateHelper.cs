using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class QueStateHelper
    {
        private static readonly List<QueStateModel> PrintSateModels = new List<QueStateModel>();

        static QueStateHelper()
        {
            PrintSateModels.Add(new QueStateModel
            {
                Id = (int)QueState.NOSM,
                Name = QueState.NOSM.ToString(),
                Display = "未扫描"
            });
            PrintSateModels.Add(new QueStateModel
            {
                Id = (int)QueState.YSM,
                Name = QueState.YSM.ToString(),
                Display = "已扫描"
            });
            PrintSateModels.Add(new QueStateModel
            {
                Id = 2,
                Name = "All",
                Display = "全部"
            });
        }
        /// <summary>
        /// 扫描状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string QueStateFormatter(object obj)
        {
            if (obj == null)
            {
                return "未知";
            }
            if (Enum.IsDefined(typeof(QueState), obj))
            {
                return EnumHelper.GetEnumDesc((QueState)obj);
            }
            return obj.ToString();
        }
    }
}
