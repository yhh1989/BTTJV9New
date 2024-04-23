using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.FrmMakeCollect
{
   public class FrmMakeCollectAppService : AppServiceApiProxyBase, IFrmMakeCollectAppService
    {
        public List<ShowMakeCollectDto> GetShowMakeCollects(ShowMakeCollectDto input)
        {
            return GetResult<ShowMakeCollectDto, List<ShowMakeCollectDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutDepartCusDto> getDepartCount(InIdDto input)
        {
            return GetResult<InIdDto, List<OutDepartCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutRegCusListDto> getRegCusLis(InIdDto input)
        {
            return GetResult<InIdDto, List<OutRegCusListDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
