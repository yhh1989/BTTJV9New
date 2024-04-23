using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
   public  class HistoryResultDetailDto
    {
        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 登记日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }
        /// <summary>
        /// 项目结果
        /// </summary>
        public virtual string ItemValue { get; set; }
        /// <summary>
        /// 项目标识
        /// </summary>
        public virtual string Symbol { get; set; }
    }
}
