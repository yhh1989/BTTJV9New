using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus
{
    public interface ICusReSultStatusAppService
#if !Proxy
        : IApplicationService
#endif
    {
        ReSultSetDto Add(FullReSultSetDto input);

        List<ReSultSetDto> GetReSultDepart();
        List<ReSultCusInfoDto> getCusResult(ShowResultSetDto input);
    }
}
