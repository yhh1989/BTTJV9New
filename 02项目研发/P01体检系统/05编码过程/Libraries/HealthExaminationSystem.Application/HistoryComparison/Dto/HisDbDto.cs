using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
   public  class HisDbDto
    {
        /// <summary>
        /// 图标对比
        /// </summary>
        public virtual List<HistoryItemValueDto> HistoryItemChar { get; set; }
        /// <summary>
        /// 项目对比三年对比
        /// </summary>
        public virtual List<HistoryItemValueDto> HistoryItemDb  { get; set; }
        /// <summary>
        /// 项目对比两年数值
        /// </summary>
        public virtual List<HistoryItemDBDto> HistoryItem { get; set; }
        /// <summary>
        /// 科室小节对比
        /// </summary>
        public virtual List<HisDepartSumDto> HisDepartSum { get; set; }
        
        /// <summary>
        /// 总检对比
        /// </summary>
        public virtual List<HisSumDto> HisSum { get; set; }
    }
}
