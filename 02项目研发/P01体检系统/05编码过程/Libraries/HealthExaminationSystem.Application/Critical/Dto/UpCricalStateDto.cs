using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto
{
  public   class UpCricalStateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 危急值回访状态 0未上报1已上报2已取消3已审核
        /// </summary>
        public virtual int? Sate { get; set; }
    }
}
