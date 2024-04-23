using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GUaDto
    { /// <summary>
      /// 设备唯一标识
      /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 尿酸值
        /// </summary>
        public string Value { get; set; }
    }
}
