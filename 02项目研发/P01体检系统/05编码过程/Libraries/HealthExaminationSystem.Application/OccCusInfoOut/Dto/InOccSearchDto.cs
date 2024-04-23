using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{
  public   class InOccSearchDto
    {

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? Stardt { get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? Enddt { get; set; }
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

    }
}
