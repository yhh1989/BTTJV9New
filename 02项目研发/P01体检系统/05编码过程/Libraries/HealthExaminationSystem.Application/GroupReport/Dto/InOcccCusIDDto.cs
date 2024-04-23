using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
  public   class InOcccCusIDDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>       
        public virtual List<Guid> CusRegIDList { get; set; }

        /// <summary>
        /// 复查状态 true包含false不包含
        /// </summary>
        public virtual bool? isfc { get; set; }
    }
}
