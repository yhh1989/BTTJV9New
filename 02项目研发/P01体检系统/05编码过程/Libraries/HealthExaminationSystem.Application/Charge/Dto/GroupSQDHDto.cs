using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  public  class GroupSQDHDto
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        public string SQDH { get; set; }

        /// <summary>
        /// 申请状态1收费2退费
        /// </summary>
        public int? SQSTATUS { get; set; }


    }
}
