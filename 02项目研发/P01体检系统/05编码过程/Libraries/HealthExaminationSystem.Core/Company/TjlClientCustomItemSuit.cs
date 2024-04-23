using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Company
{
    /// <summary>
    /// 单位组单记录表
    /// </summary>
    public class TjlClientCustomItemSuit : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 单位
        /// </summary>
        public virtual TjlClientInfo ClientInfo { get; set; }

        /// <summary>
        /// 组单编码
        /// </summary>
        public virtual TbmItemSuit ItemSuit { get; set; }

        /// <summary>
        /// 是否已用，创建预约,如果根据组单创建预约，则修改为已用
        /// </summary>
        public virtual bool IsUse { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}