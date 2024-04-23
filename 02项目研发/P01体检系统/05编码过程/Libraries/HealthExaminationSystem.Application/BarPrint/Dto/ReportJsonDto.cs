using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto
{
  public   class ReportJsonDto
    {
        /// <summary>
        /// 参数
        /// </summary>
        public List<MasterDto> Master { get; set; }

        /// <summary>
        /// 明细网格
        /// </summary>
        public List<DetailDto> Detail { get; set; }
    }
}
