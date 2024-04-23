using Sw.Hospital.HealthExaminationSystem.Application.OutInspects;
using Sw.Hospital.HealthExaminationSystem.Application.OutInspects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OutInspects
{
    public class OutInspectsQueryAppService : AppServiceApiProxyBase, IOutInspectsQueryAppService
    {
        public List<OutinspectsQueryDto> OutinspectsQuery(OutCusInfoDto input)
        {
            return GetResult<OutCusInfoDto,List <OutinspectsQueryDto>>(input,DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
