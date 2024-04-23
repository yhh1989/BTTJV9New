using Sw.Hospital.HealthExaminationSystem.Application.OccConclusioncontraindication;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusioncontraindication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusioncontraindication
{
 public   class OccupationalcontraindicationAppService:AppServiceApiProxyBase, IOccupationalcontraindicationAppService
    {
      public List<OccConclusioncontraindicationShowDto> GetOccConclusionSuspected(OccContraindicationGet input)
        {
            return GetResult<OccContraindicationGet, List<OccConclusioncontraindicationShowDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
