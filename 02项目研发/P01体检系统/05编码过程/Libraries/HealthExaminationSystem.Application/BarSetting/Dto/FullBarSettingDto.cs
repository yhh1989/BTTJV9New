using System.Collections.Generic;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmBarSettings))]
#endif
    public class FullBarSettingDto : BarSettingDto
    {
        public virtual List<FullBarItemDto> Baritems { get; set; }
    }
}