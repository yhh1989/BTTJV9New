using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto
{
 public    class OccYearsStatisticsDto
    {

        /// <summary>
        /// 单位预约Id
        /// </summary>
        public virtual Guid? ClientegId { get; set; }

        /// <summary>
        /// 统计年份
        /// </summary>
        public virtual DateTime? StarDate { get; set; }

    }
}
