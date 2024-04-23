using System;
using System.Collections.Generic;

#if Application
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if Application
    [AutoMapTo(typeof(PersonnelIndividuationConfig))]
#endif
    public class CreateOrUpdatePersonnelIndividuationConfigDto
    {
#if Application
        [IgnoreMap]
#endif
        public long UserId { get; set; }

        public bool IsActive { get; set; }

        public bool AdvancedAlwaysCheck { get; set; }
        
#if Application
        [IgnoreMap]
#endif
        public List<Guid> StartupMenuBarIds { get; set; }
    }
}