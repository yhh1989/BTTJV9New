using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.AddStatistics.Dto
{
    /// <summary>
    /// 参数
    /// </summary>
  public class SearchStatisticsDto
    {
        /// <summary>
        ///开始时间
        /// </summary>
        public virtual DateTime DataTimeStart { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime DataTimeEnd { get; set; }
        /// <summary>
        /// 检查人 姓名
        /// </summary>
        public virtual long? CheckName { get; set; }
        /// <summary>
        /// 加项名称
        /// </summary>
        public virtual Guid? GroupName { get; set; }

    }
}
