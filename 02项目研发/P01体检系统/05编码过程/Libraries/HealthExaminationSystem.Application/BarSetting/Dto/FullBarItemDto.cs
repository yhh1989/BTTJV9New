
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmBaritem))]
#endif
    public class FullBarItemDto : BarItemDto
    {
        /// <summary>
        /// 关联编号 组合id
        /// </summary>
        public virtual SimpleItemGroupDto ItemGroup { get; set; }

    }
}
