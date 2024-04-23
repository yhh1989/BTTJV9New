
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
namespace Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemGroup))]
#endif
    public class SearchItemGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}