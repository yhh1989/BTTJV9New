using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto
{
    /// <summary>
    /// 条码设置数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Coding.TbmBarSettings))]
#endif
    public class BarSettingsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 试管颜色
        /// </summary>0
        [StringLength(32)]
        public virtual string TubeColor { get; set; }
    }
}