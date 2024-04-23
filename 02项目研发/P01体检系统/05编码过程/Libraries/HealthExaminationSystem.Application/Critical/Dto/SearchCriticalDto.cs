using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto
{
  public   class SearchCriticalDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室标识
        /// </summary>      
        public virtual Guid? DepartmentId { get; set; }

        /// <summary>
        /// 项目标识
        /// </summary>      
        public virtual Guid? ItemInfoId { get; set; }
    }
}
