using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if !Proxy
    [AutoMapTo(typeof(FormRole))]
#endif
    public class CreateFormRoleDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(32)]
        public virtual string Name { get; set; }

        public virtual List<Guid> Modules { get; set; }
    }
}