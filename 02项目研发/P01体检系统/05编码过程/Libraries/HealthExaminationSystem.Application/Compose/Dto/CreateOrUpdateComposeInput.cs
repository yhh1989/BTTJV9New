using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto
{
    public class CreateOrUpdateComposeInput
    {
        public CreateOrUpdateComposeDto Compose { get; set; }

        public List<CreateOrUpdateComposeGroupInput> ComposeGroups { get; set; }
    }
}
