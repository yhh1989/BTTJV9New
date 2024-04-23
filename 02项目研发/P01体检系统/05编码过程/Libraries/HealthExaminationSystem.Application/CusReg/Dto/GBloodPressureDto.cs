using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
 public    class GBloodPressureDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 心率
        /// </summary>
        public string RateValue { get; set; }
        /// <summary>
        /// 收缩压）高压
        /// </summary>
        public string HighPressure { get; set; }
        /// <summary>
        /// 舒张压）低压
        /// </summary>
        public string LowPressure { get; set; }
    }
}
