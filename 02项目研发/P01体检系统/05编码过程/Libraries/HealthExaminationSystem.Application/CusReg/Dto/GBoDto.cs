using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GBoDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 血氧值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 心率
        /// </summary>
        public string PulseValue { get; set; }
    }
}
