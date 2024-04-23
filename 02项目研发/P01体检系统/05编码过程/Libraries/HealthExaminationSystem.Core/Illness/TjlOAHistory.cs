using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 工作历史
    /// </summary>
    [Obsolete("暂停使用")]
    public class TjlOAHistory : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约登记信息表 体检人预约登记信息表
        /// </summary>
        [Required]
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(64)]
        public virtual string Unit { get; set; }

        ///// <summary>
        ///// 工种
        ///// </summary>
        //[StringLength(64)]
        //[Obsolete("暂停使用", true)]
        //public virtual WorkType Workshop { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(64)]
        public virtual string Workshops { get; set; }

        ///// <summary>
        ///// 车间
        ///// </summary>
        //[StringLength(64)]
        //[Obsolete("暂停使用", true)]
        //public virtual WorkType Work { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(64)]
        public virtual string Works { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        [StringLength(128)]
        public virtual string RiskName { get; set; }

        /// <summary>
        /// 是否痊愈
        /// </summary>
        public virtual bool? Protect { get; set; }

        /// <summary>
        /// 防护措施
        /// </summary>
        [StringLength(256)]
        public virtual string Fhcs { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}