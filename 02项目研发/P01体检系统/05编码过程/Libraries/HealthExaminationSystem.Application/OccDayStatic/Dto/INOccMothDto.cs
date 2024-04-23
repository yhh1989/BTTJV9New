using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto
{
   public  class INOccMothDto
    {
        /// <summary>
        /// 年度
        /// </summary>
        public int? YearTime { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public int? MothTime { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public Guid? ClienRegId { get; set; }

    }
}
