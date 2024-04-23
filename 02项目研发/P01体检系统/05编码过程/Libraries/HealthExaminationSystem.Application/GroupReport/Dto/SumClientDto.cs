using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
  public   class SumClientDto
    {
        /// <summary>
        /// 异常名称
        /// </summary>
        public virtual string SumName { get; set; }


        /// <summary>
        /// 异常建议
        /// </summary>
        public virtual string SumAdvicee { get; set; }


        /// <summary>
        ///人员列表
        /// </summary>
        public virtual string CusSum { get; set; }
    }
}
