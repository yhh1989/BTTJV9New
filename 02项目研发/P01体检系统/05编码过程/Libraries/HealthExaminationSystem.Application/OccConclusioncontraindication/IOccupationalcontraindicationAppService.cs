using Sw.Hospital.HealthExaminationSystem.Application.OccConclusioncontraindication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusioncontraindication
{
  public  interface IOccupationalcontraindicationAppService
#if !Proxy
         : Abp.Application.Services.IApplicationService
#endif
    {
        List<OccConclusioncontraindicationShowDto> GetOccConclusionSuspected(OccContraindicationGet input);
    }
}
