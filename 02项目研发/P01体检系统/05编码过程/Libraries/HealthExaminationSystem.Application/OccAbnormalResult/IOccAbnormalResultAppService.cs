
using Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult
{
   public interface IOccAbnormalResultAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        List<OutOccAbnormalResult> GetOccAbnormalResult(OutOccAbnormalResult input);
    }
}
