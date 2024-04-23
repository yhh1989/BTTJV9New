using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class BdSatticDto
    {
        /// <summary>
        /// 键
        /// </summary>
        public virtual int? Value { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public virtual string Text { get; set; }
    }
}
