using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
  public  class OccQuestionPastAddrucan: EntityDto<Guid>
    {

        

       public OccQuesPastHistoryDto occpast { get; set; }

    }
}
