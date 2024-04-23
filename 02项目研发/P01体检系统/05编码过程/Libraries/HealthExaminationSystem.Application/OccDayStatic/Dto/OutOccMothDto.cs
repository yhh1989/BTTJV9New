using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto
{
    public class OutOccMothDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public int? ConName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int? ConCount { get; set; }
    }
}
