using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles
{
    /// <summary>
    /// 窗体角色
    /// </summary>
    public class FormRole : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// 窗体模块
        /// </summary>
        public virtual ICollection<FormModule> FormModules { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}