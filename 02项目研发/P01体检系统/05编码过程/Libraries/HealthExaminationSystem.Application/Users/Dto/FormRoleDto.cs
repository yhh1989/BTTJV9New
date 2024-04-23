using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if !Proxy
    [AutoMap(typeof(FormRole))]
#endif
    public class FormRoleDto : EntityDto<Guid>
    {
        [Required]
        [StringLength(32)]
        public virtual string Name { get; set; }
    }
}