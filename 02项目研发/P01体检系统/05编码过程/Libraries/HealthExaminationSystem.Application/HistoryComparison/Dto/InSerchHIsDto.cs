using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
  public  class InSerchHIsDto
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual List<Guid> ItemIds { get; set; }
        
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid? CustomerId { get; set; }
        /// <summary>
        /// 预约人员ID
        /// </summary>
        public Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { get; set; }
    }
}
