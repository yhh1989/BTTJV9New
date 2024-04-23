using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Company;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary>
    /// 单位储值表
    /// </summary>
    public class TjlMClientStoreds : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 单位信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ClientInfo")]
        public virtual Guid ClientInfoId { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        [Required]
        public virtual TjlClientInfo ClientInfo { get; set; }

        /// <summary>
        /// 支付方式外键
        /// </summary>
        [ForeignKey("MPayment")]
        public virtual Guid? MPaymentId { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual TjlMPayment MPayment { get; set; }

        /// <summary>
        /// 储存金额
        /// </summary>
        public virtual decimal StoredMoney { get; set; }

        /// <summary>
        /// 储存状态 1储值-1消费
        /// </summary>
        public virtual decimal StoredState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}