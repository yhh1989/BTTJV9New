using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccDayStatic
{
   public class OccDayStaticAppService : AppServiceApiProxyBase, IOccDayStaticAppService
    {
        public List<OutOccDayDto> GetOutOccDays(OutOccDayDto input)
        {
            return GetResult<OutOccDayDto, List<OutOccDayDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutOccMothDto> GetOutOccMothDays(INOccMothDto input)
        {
            return GetResult<INOccMothDto, List<OutOccMothDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
