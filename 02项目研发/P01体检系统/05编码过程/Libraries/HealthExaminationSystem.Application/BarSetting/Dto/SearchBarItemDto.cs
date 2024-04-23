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

#endif
    public class SearchBarItemDto
    {
        /// <summary>
        /// 所有组合id
        /// </summary>
        public virtual List<Guid> AllItemGroupID { get; set; }
    }
}
