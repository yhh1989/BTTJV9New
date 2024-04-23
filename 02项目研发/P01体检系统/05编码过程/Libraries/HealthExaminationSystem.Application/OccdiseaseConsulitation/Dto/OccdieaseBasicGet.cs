using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
  public  class OccdieaseBasicGet
    {
        /// <summary>
        /// 体检号
        /// </summary>

        public virtual string CustomerBM { get; set; }



        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }
    }
}
