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
    public class BarItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 组合Id
        /// </summary>
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 组合别名 组合别名
        /// </summary>
        public virtual string ItemGroupAlias { get; set; }

    }
}
