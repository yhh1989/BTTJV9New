using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto
{
#if !Proxy
    [AutoMap(typeof(FormModule))] 
#endif
    public class FormModuleDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(128)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(128)]
        public virtual string Nickname { get; set; }

        public virtual List<FormRoleDto> FormRoles { get; set; }
    }
}