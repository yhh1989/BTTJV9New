using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 启动窗体菜单栏
    /// </summary>
    public class StartupMenuBar : AuditedEntity<Guid>
    {
        /// <summary>
        /// 按钮项名称
        /// </summary>
        public string BarButtonItemName { get; set; }

        /// <summary>
        /// 按钮项标题
        /// </summary>
        public string BarButtonItemCaption { get; set; }

        /// <summary>
        /// 调用者列表
        /// </summary>
        [InverseProperty(nameof(PersonnelIndividuationConfig.StartupMenuBars))]
        public virtual ICollection<PersonnelIndividuationConfig> PersonnelIndividuationConfigs { get; set; }
    }
}