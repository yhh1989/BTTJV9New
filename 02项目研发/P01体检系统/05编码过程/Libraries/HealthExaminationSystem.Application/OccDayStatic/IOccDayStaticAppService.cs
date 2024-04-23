using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic
{
    public interface IOccDayStaticAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        List<OutOccDayDto> GetOutOccDays(OutOccDayDto input);
        List<OutOccMothDto> GetOutOccMothDays(INOccMothDto input);
    }
}
