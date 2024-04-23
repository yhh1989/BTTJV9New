using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 是否类帮助
    /// </summary>
    public class IfTypeHelper
    {
        /// <summary>
        /// 获取系统是否类列表
        /// </summary>
        /// <returns></returns>
        public static List<IfTypeModel> GetIfTypeModels()
        {
            var result = new List<IfTypeModel>();
            var ifTrue = new IfTypeModel
            {
                Id = (int)IfType.True,
                Name = IfType.True.ToString(),
                Display = "是"
            };
            result.Add(ifTrue);
            var ifFalse = new IfTypeModel
            {
                Id = (int)IfType.False,
                Name = IfType.False.ToString(),
                Display = "否"
            };
            result.Add(ifFalse);
            return result;
        }
    }
}