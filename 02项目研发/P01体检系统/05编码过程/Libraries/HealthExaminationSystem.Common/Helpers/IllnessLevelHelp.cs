using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 重度等级 
    /// </summary>
    public class IllnessLevelHelp
    {
        // <summary>
        /// 获取重度等级级别
        /// </summary>
        /// <returns></returns>
        public static List<IllnessLevelModel> GetIfTypeModels()
        {
            var result = new List<IllnessLevelModel>();
            var Nothing = new IllnessLevelModel
            {
                Id = (int)IllnessLevel.Nothing,
                Name = IllnessLevel.Nothing.ToString(),
                Display = "无"
            };
            result.Add(Nothing);
            var Abnormal = new IllnessLevelModel
            {
                Id = (int)IllnessLevel.Light,
                Name = IllnessLevel.Light.ToString(),
                Display = "轻微"
            };
            result.Add(Abnormal);
            var Auxiliary = new IllnessLevelModel
            {
                Id = (int)IllnessLevel.Middle,
                Name = IllnessLevel.Middle.ToString(),
                Display = "中度"
            };
            result.Add(Auxiliary);
            var Diagnosis = new IllnessLevelModel
            {
                Id = (int)IllnessLevel.Severe,
                Name = IllnessLevel.Severe.ToString(),
                Display = "重度"
            };
            result.Add(Diagnosis);           
            return result;
        }
        /// <summary>
        /// 疾病状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PositiveStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(IllnessLevel), obj))
            {
                return EnumHelper.GetEnumDesc((IllnessLevel)obj);
            }
            return obj.ToString();
        }
    }
}
