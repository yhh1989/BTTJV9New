using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmBaritem))]
#endif
    public class BarItemViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 关联编号 组合id
        /// </summary>
        public virtual ItemGroupViewDto ItemGroup { get; set; }

        /// <summary>
        /// 组合别名 组合别名
        /// </summary>
        public virtual string ItemGroupAlias { get; set; }
    }
}