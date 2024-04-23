using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Models
{

    /// <summary>
    /// 疾病状态 
    /// </summary>
    public class IllnessSateHelp 
    {
        // <summary>
        /// 获取系统是否类列表
        /// </summary>
        /// <returns></returns>
        public static List<IllnessSateModel> GetIfTypeModels()
        {
            var result = new List<IllnessSateModel>();
            var Abnormal = new IllnessSateModel
            {
                Id = (int)IllnessSate.Abnormal,
                Name = IllnessSate.Abnormal.ToString(),
                Display = "专科查体异常发现"
            };
            result.Add(Abnormal);
            var Auxiliary = new IllnessSateModel
            {
                Id = (int)IllnessSate.Auxiliary,
                Name = IllnessSate.Auxiliary.ToString(),
                Display = "辅助检查异常发现"
            };
            result.Add(Auxiliary);
            var Diagnosis = new IllnessSateModel
            {
                Id = (int)IllnessSate.Diagnosis,
                Name = IllnessSate.Diagnosis.ToString(),
                Display = "临床诊断"
            };
            result.Add(Diagnosis);
            var Doubtful = new IllnessSateModel
            {
                Id = (int)IllnessSate.Doubtful,
                Name = IllnessSate.Doubtful.ToString(),
                Display = "疑似诊断"
            };
            result.Add(Doubtful);
            var Laboratory = new IllnessSateModel
            {
                Id = (int)IllnessSate.Laboratory,
                Name = IllnessSate.Laboratory.ToString(),
                Display = "实验室检查异常发现"
            };
            result.Add(Laboratory);
            return result;
        }
        /// <summary>
        /// 疾病状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PositiveStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(IllnessSate), obj))
            {
                return EnumHelper.GetEnumDesc((IllnessSate)obj);
            }
            return obj.ToString();
        }
    }
}
