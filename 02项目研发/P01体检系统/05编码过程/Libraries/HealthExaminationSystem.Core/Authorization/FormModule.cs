using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization
{
    /// <summary>
    /// 窗体模块
    /// </summary>
    public class FormModule : AuditedEntity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(128)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 友好名称
        /// </summary>
        [Required]
        [MaxLength(128)]
        public virtual string Nickname { get; set; }
        
        /// <summary>
        /// 分类
        /// </summary>
        [MaxLength(128)]
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public virtual ICollection<FormRole> FormRoles { get; set; }
    }
}