using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GHbDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 血红蛋白值
        /// </summary>
        public string Hb { get; set; }
        
    }
}
