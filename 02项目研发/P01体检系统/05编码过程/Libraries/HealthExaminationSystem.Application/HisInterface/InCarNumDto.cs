using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HisInterface
{
  public   class InCarNumDto
    {
        /// <summary>
        /// 诊疗卡号
        /// </summary>
        [StringLength(500)]
        public string CardNum { get; set; }

        /// <summary>
        /// His厂家
        /// </summary>
        public string HISName { get; set; }
    }
}
