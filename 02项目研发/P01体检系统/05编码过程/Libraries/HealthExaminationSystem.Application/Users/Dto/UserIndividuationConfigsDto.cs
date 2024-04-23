using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;

#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if Application
    [AutoMapFrom(typeof(User))]
#endif
    public class UserIndividuationConfigsDto : EntityDto<long>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public virtual PersonnelIndividuationConfigDto IndividuationConfig { get; set; }

#if Proxy
        [JsonIgnore]
        public List<StartupMenuBarDto> StartupMenuBars => IndividuationConfig?.StartupMenuBars;
#endif
    }
}