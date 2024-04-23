using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{
   public  class InCarNum
    {
        /// <summary>
        /// 诊疗卡号
        /// </summary>
        [StringLength(500)]
        public string CardNum { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(500)]
        public string IdCard { get; set; }

        /// <summary>
        /// His厂家
        /// </summary>
        public string HISName { get; set; }
    }
}
