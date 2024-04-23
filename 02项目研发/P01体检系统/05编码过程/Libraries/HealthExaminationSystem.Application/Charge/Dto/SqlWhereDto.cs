using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  public   class SqlWhereDto
    {

        /// <summary>
        /// 诊疗卡号
        /// </summary>     
        public string CardNum { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        public virtual string SQDH { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// 预约开始时间
        /// </summary>
        public virtual DateTime? DateStartTime { get; set; }

        /// <summary>
        /// 预约结束时间
        /// </summary>
        public virtual DateTime? DateEndTime { get; set; }
    }
}
