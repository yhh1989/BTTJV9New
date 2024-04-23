using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class searchIDListDto
    {
        public virtual List<Guid?> Ids { get; set; }
    }
}
