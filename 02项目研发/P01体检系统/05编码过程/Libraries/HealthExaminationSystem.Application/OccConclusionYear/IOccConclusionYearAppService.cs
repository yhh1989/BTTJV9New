using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear
{
    public  interface IOccConclusionYearAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        List<OutOccMothDto> GetOccAbnormalResult(OccConclusionYearShowDto input);
    }
}
