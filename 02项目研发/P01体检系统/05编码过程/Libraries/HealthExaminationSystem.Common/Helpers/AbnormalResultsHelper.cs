using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 结果异常帮助类
    /// </summary>
    public class AbnormalResultsHelper
    {
        /// <summary>
        /// 获取结果异常
        /// </summary>
        /// <returns></returns>
        public static List<AbnormalResultsModel> GetAbnormalResultsModels()
        {
            var result= new List<AbnormalResultsModel>();
            var Abnormal = new AbnormalResultsModel
            {
                Id = (int)AbnormalResults.Abnormal,
                Name = AbnormalResults.Abnormal.ToString(),
                Display = "异常"
            };
            result.Add(Abnormal);
            var Normal = new AbnormalResultsModel
            {
                Id = (int)AbnormalResults.Normal,
                Name = AbnormalResults.Normal.ToString(),
                Display = "正常"
            };
            result.Add(Normal);
            return result;
        }
    }
}
