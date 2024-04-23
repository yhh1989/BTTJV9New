using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if Application
    [AutoMapFrom(typeof(PersonnelIndividuationConfig))]
#endif
    public class PersonnelIndividuationConfigDto : EntityDto<long>
    {
        public bool IsActive { get; set; }

        public bool AdvancedAlwaysCheck { get; set; }

        public List<StartupMenuBarDto> StartupMenuBars { get; set; }
    }
}