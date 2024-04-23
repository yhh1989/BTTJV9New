using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class GCholDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 总胆固醇值
        /// </summary>
        public string Value { get; set; }
    }
}
