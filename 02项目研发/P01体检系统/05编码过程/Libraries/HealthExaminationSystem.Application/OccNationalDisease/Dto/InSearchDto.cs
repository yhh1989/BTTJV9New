using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
  public   class InSearchDto
    {
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
    }
}
