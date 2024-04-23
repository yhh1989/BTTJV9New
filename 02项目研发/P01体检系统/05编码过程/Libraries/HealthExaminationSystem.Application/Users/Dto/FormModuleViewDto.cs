using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(FormModule))]
#endif
    public class FormModuleViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 友好名称
        /// </summary>
        [Required]
        [StringLength(128)]
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [StringLength(128)]
        public virtual string TypeName { get; set; }
    }
}