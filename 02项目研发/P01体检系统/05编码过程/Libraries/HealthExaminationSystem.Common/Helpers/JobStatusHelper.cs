using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 职位状态帮助
    /// </summary>
    public class JobStatusHelper
    {
        /// <summary>
        /// 获取系统职位状态列表
        /// </summary>
        /// <returns></returns>
        public static List<JobStatusModel> GetJobStatusModels()
        {
            var result = new List<JobStatusModel>();
            var onGuard = new JobStatusModel
            {
                Id = (int)JobStatus.OnGuard,
                Name = JobStatus.OnGuard.ToString(),
                Display = "在岗"
            };
            result.Add(onGuard);
            var dimission = new JobStatusModel
            {
                Id = (int)JobStatus.Dimission,
                Name = JobStatus.Dimission.ToString(),
                Display = "离岗"
            };
            result.Add(dimission);
            return result;
        }
    }
}