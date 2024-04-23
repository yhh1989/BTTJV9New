using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear.Dto
{

  public  class OccConclusionYearShowDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual Guid? ClientregID { get; set; }
        
        /// <summary>
        /// 开始年
        /// </summary>
        public int? StartCheckDate { get; set; }

        /// <summary>
        /// 结束年
        /// </summary>
        public int? EndCheckDate { get; set; }

       


    }
}
