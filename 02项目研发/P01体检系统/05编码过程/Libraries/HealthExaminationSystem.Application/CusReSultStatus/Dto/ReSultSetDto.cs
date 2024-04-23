using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmReSultSet))]
#endif
    public class ReSultSetDto : EntityDto<Guid>
    {
        /// <summary>
        /// 显示项目名称
        /// </summary>     
        public virtual List<ReSultDepartDto> Departs { get; set; }
        /// <summary>
        /// 显示组合名称
        /// </summary>     
        public virtual List<ReSultCusGroupDto> Groups { get; set; }
        /// <summary>
        /// 显示项目名称
        /// </summary>     
        public virtual List<ReSultCusItemDto> Items { get; set; }
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
