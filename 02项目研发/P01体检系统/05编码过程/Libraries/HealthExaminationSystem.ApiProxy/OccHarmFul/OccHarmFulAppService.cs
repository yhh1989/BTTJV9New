
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul
{
    public class OccHarmFulAppService : AppServiceApiProxyBase, IOccHarmFulAppService
    {     
        public List<OutOccFactoryDto> GetOutOccFactories(OutOccFactoryDto input)
        {
            return GetResult<OutOccFactoryDto, List<OutOccFactoryDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
