using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto
{
    //查询入参
   public class OccStatisticsShowGet
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位预约Id
        /// </summary>
        public virtual Guid? ClientegId { get; set; }

        /// <summary>
        /// 导诊开始时间
        /// </summary>
        public virtual DateTime? NavigationStartTime { get; set; }

        /// <summary>
        /// 导诊结束时间
        /// </summary>
        public virtual DateTime? NavigationEndTime { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int? TimeType { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }
    }
}
