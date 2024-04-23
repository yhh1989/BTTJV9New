using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary>
    /// 结算发票记录表
    /// </summary>
    public class TjlMInvoiceRecord : FullAuditedEntity<Guid>, IMustHaveTenant, ICloneable
    {
        /// <summary>
        /// 作废原ID
        /// </summary>
        public virtual Guid? InvalidTjlMInvoiceRecordId { get; set; }

        /// <summary>
        /// 开票任标识
        /// </summary>
        [ForeignKey("User")]
        public virtual long? UserForMakeInvoiceId { get; set; }

        /// <summary>
        /// 开票人
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TjlMPayment> MPayment { get; set; }

        /// <summary>
        /// 开票时间
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用！", true)]
        public virtual string InvoiceDate { get; set; }

        /// <summary>
        /// 发票状态 1收费2作废
        /// </summary>
        [MaxLength(8)]
        public virtual string State { get; set; }

        /// <summary>
        /// 发票抬头id
        /// </summary>
        public virtual TjlMRise MRise { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        public virtual decimal InvoiceMoney { get; set; }

        /// <summary>
        /// 结算表
        /// </summary>
        public virtual TjlMReceiptInfo MReceiptInfo { get; set; }

        /// <summary>
        /// 结算表标识
        /// </summary>
        [ForeignKey("MReceiptInfo")]
        public virtual Guid? MReceiptInfoId { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        [StringLength(64)]
        public virtual string InvoiceNum { get; set; }

        /// <summary>
        /// 克隆一个对象
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}