using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Common.Dto
{
    /// <summary>
    /// 行政区划
    /// </summary>
#if Application
    [AutoMapFrom(typeof(AdministrativeDivision))]
#endif
    public class AdministrativeDivisionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(10)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(64)]
        [Required]
        public string Name { get; set; }
    }
}