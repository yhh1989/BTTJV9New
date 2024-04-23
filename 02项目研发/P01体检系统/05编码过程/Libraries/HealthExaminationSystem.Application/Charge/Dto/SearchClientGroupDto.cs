using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
 public    class SearchClientGroupDto
    {

        /// <summary>
        /// 登记开始时间
        /// </summary>
        public virtual DateTime? StarDate { get; set; }

        /// <summary>
        /// 登记结束时间
        /// </summary>
        public virtual DateTime? EndDate { get; set; }


        /// <summary>
        /// 单位预约Id
        /// </summary>        
        public virtual Guid? ClientRegId { get; set; }

    }
}
