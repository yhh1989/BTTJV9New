using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{

    public class BarSettingInput
    {
        public virtual CreateOrUpdateBarSettingDto BarSetting { get; set; }
        public virtual List<BarItemDto> Baritems { get; set; }
        
    }
}
