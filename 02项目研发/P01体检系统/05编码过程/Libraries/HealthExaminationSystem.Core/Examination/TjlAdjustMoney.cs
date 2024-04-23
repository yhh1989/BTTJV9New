using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Company;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 调项金额表
    /// </summary>
    public class TjlAdjustMoney : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 体检人预约
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 单位预约信息标识
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 调项加金额
        /// </summary>
        public virtual decimal AdjustAddMoney { get; set; }

        /// <summary>
        /// 调项减金额
        /// </summary>
        public virtual decimal AdjustMinusMoney { get; set; }

        /// <summary>
        /// 调项差价
        /// </summary>
        public decimal DifferenceMoney { get; set; }

        /// <summary>
        /// 调项差价个付
        /// </summary>
        public decimal DifferencePerMoney { get; set; }

        /// <summary>
        /// 调项差价团体付
        /// </summary>
        public decimal DifferenceClientMoney { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}