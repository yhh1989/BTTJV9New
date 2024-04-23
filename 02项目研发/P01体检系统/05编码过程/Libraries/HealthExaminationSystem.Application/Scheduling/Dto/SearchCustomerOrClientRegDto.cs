using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
    public class SearchCustomerOrClientRegDto : EntityDto<Guid>
    {
        public virtual string QueryText { get; set; }
        public virtual DateTime? Start { get; set; }
        public virtual DateTime? End { get; set; }
    }
}
