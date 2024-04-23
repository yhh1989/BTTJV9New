using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup
{
    public interface ISeleteItemGroupAppService
#if !Proxy
        : IApplicationService
#endif
    {
        List<SeleteItemGroupDto> QueryInfoGroup(SeleteItemGroupDto input);
    }
}