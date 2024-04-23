using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
   public  class BeforSaveSumDto : EntityDto<Guid>
    {
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核5审核不通过
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 组合
        /// </summary>
        public virtual List<CustomerGroupSumDto> cusGroup { get; set; }

    }
}
