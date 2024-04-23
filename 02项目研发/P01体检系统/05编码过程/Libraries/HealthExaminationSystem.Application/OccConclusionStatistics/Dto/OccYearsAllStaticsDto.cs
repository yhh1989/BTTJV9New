using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto
{
    public class OccYearsAllStaticsDto
    {
        /// <summary>
        /// 按用人单位统计
        /// </summary>
        public virtual List<OccYearsSaticsDto> ClientStatic { get; set; }
        /// <summary>
        /// 按职业病危害因素类别统计
        /// </summary>
        public virtual List<OccYearsSaticsDto> RiskTypeStatic { get; set; }
        /// <summary>
        /// 职业病
        /// </summary>
        public virtual List<OccZYBStatisticsDto> ZYBStatistics { get; set; }
        /// <summary>
        /// 禁忌证
        /// </summary>
        public virtual List<OccZYBStatisticsDto> JJZStatistics { get; set; }

    }
}
