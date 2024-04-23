using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 复诊时间
    /// </summary>
    public class ReVisitTime : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(256)]
        public virtual string Content { get; set; }
        /// <summary>
        /// 项目组合ID
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid? ItemGroupId { get; set; }

        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 是否已经复查
        /// </summary>
        public bool IsInspect { get; set; }

        /// <summary>
        /// 体检人预约记录标识
        /// </summary>
        [ForeignKey(nameof(CustomerRegister))]
        public Guid CustomerRegisterId { get; set; }

        /// <summary>
        /// 体检人预约记录
        /// </summary>
        public virtual TjlCustomerReg CustomerRegister { get; set; }

        /// <summary>
        /// 单位预约标识
        /// </summary>
        public Guid? CompanyRegisterId { get; set; }
        /// <summary>
        /// 人员id
        /// </summary>
        public Guid? CustomerRegId { get; set; }
    }
}
