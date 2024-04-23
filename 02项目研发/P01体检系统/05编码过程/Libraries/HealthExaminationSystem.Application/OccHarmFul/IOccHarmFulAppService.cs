using Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul
{
    public interface IOccHarmFulAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        List<OutOccFactoryDto> GetOutOccFactories(OutOccFactoryDto input);
    }
}
