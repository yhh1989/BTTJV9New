using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect
{
    public interface IFrmMakeCollectAppService
#if !Proxy
        : IApplicationService
#endif
    {
        List<ShowMakeCollectDto> GetShowMakeCollects(ShowMakeCollectDto input);

        List<OutDepartCusDto> getDepartCount(InIdDto input);
        List<OutRegCusListDto> getRegCusLis(InIdDto input);
    }
}
