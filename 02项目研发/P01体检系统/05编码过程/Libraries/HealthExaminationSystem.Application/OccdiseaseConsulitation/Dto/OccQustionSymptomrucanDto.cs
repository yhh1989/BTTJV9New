using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
  public  class OccQustionSymptomrucanDto : EntityDto<Guid>
    {
        public OccQustionSymptomrucan occQustionSymptom { get; set; }
    }
}
