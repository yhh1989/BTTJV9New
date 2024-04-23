using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
    /// <summary>
    /// 窗体角色视图
    /// </summary>
#if !Proxy
    [AutoMapFrom(typeof(FormRole))]
#endif
    public class FormRoleViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 窗体模块
        /// </summary>
        public virtual List<FormModuleViewDto> FormModules { get; set; }
    }
}