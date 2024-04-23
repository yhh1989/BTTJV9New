using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MRise;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.MRise
{
    public class MRiseAppService : AppServiceApiProxyBase, IMRiseAppService
    {
        public MRiseDto AddMRise(MRiseDto input)
        {
            return GetResult<MRiseDto, MRiseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<MRiseDto> GetAllMRise()
        {
            return GetResult<List<MRiseDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
