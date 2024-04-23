using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
   public class SearchGroupCustomerRegDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual Guid? ClientRegId{ get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndDateTime { get; set; }

        public virtual int? DateType { get; set; }
    }
}
