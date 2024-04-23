using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmCompose))]
#endif
    public class FullComposeDto : ComposeDto
    {
        public List<FullComposeGroupDto> ComposeGroups { get; set; }

    }
}
