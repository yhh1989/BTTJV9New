using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusioncontraindication.Dto
{
  public  class OccContraindicationGet
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }
    }
}
