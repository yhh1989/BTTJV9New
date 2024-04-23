using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup
{
    public class SeleteItemGroupAppService : AppServiceApiProxyBase, ISeleteItemGroupAppService
    {
        public List<SeleteItemGroupDto> QueryInfoGroup(SeleteItemGroupDto input)
        {
            return GetResult<SeleteItemGroupDto, List <SeleteItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}