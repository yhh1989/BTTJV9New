using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 系统参数表
    /// </summary>
    public class TdbSetSysSetting : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public virtual string KeyID { get; set; }

        /// <summary>
        /// 参数类别名称:只能一级类别，一级明细。
        /// </summary>
        [StringLength(64)]
        public virtual string KeyCategory { get; set; }

        /// <summary>
        /// 参数明细名称
        /// </summary>
        [StringLength(64)]
        public virtual string KeyName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [StringLength(64)]
        public virtual string KeyValue { get; set; }

        /// <summary>
        /// 参数说明:在参数设置页面会显示，不能输入内部信息。
        /// </summary>
        [StringLength(1024)]
        public virtual string KeyMemo { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}