using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    public class SearchGuidsDto : EntityDto<Guid>
    {

     /// <summary>
     /// Guids集合
     /// </summary>
        public virtual List<Guid> Guids { get; set; }
    }
}
