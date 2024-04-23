using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmComposeGroup))]
#endif
    public class FullComposeGroupDto : ComposeGroupDto
    {
        public List<ComposeGroupItemDto> ComposeGroupItems { get; set; }
    }
}
