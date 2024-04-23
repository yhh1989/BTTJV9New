using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{
   public  class OutHis
    {

        /// <summary>
        /// 门诊号
        /// </summary>
        public string CardNum { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 返回错误
        /// </summary>
        public string Mess { get; set; }
    }
}
