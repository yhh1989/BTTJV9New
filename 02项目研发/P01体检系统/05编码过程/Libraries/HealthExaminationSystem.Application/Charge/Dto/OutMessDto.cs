using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public class OutMessDto
    {
        /// <summary>
        /// 是否成功 1成功 0异常
        /// </summary>      
        public virtual string code { get; set; }
        /// <summary>
        /// 说明
        /// </summary>      
        public virtual string mess { get; set; }
    }
}
