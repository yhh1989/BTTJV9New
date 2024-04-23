using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class GBloodFatDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 高密度蛋白
        /// </summary>
        public string HdlChol { get; set; }
        /// <summary>
        /// 甘油三酯
        /// </summary>
        public string Trig { get; set; }
        /// <summary>
        /// 低密度蛋白
        /// </summary>
        public string CalcLdl { get; set; }
    }
}
