using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmReSultSet))]
#endif
    public class AddResultSetDto : EntityDto<Guid>
    {
        /// <summary>
        /// 总检建议
        /// </summary>
        public bool? isAdVice { get; set; }
        /// <summary>
        /// 职业健康
        /// </summary>
        public bool? isOccupational { get; set; }
    }
}
