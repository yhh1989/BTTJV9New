using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmItemGroup))]
#endif
    public class ItemGroupViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }
    }
}