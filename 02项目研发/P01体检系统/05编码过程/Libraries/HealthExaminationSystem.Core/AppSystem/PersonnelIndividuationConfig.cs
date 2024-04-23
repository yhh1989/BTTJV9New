using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 个人个性化配置
    /// </summary>
    public class PersonnelIndividuationConfig : AuditedEntity<long>, IMustHaveTenant, IPassivable
    {
        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <inheritdoc />
        [ForeignKey(nameof(User))]
        public override long Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 高级总检用户
        /// </summary>
        public bool AdvancedAlwaysCheck { get; set; }

        /// <summary>
        /// 启动菜单列表
        /// </summary>
        [InverseProperty(nameof(StartupMenuBar.PersonnelIndividuationConfigs))]
        public virtual ICollection<StartupMenuBar> StartupMenuBars { get; set; }
    }
}