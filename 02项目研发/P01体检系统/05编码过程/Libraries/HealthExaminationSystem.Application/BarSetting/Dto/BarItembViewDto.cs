using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
    /// <summary>
    /// 条码项目视图
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmBaritem))]
#endif
    public class BarItembViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 条码设置  编号
        /// </summary>
        [Required]
        public virtual BarSettingDto BarSetting { get; set; }

        /// <summary>
        /// 关联编号 组合id
        /// </summary>
        public virtual SimpleItemGroupDto ItemGroup { get; set; }

        /// <summary>
        /// 组合别名 组合别名
        /// </summary>
        public virtual string ItemGroupAlias { get; set; }
    }
}