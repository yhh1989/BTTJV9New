using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
   public  class InputRisksDto
    {
        public virtual List<Guid> Risks { get; set; }
        public virtual string ChekType { get; set; }
    }
}
