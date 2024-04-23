using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GWhrDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 腰围
        /// </summary>
        public string Yao { get; set; }
        /// <summary>
        /// 臀围
        /// </summary>
        public string Tun { get; set; }
        /// <summary>
        /// 腰臀比
        /// </summary>
        public string YaoTunBi { get; set; }
    }
}
