using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary> 
    /// 结算支付方式记录表
    /// </summary>
    public class TjlMPayment : FullAuditedEntity<Guid>, IMustHaveTenant, ICloneable
    {
        /// <summary>
        /// 作废原ID
        /// </summary>
        public virtual Guid? InvalidTjlMPaymentId { get; set; }

        /// <summary>
        /// 结算发票记录标识
        /// </summary>
        //[NotMapped]
        [Obsolete("暂停使用", true)]
        //[ForeignKey("MInvoiceRecord")]
        public virtual Guid? MInvoiceRecordId { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual TjlMInvoiceRecord MInvoiceRecord { get; set; }

        /// <summary>
        /// 收费记录ID
        /// </summary>
        [ForeignKey("MReceiptInfo")]
        public virtual Guid? MReceiptInfoId { get; set; }

        /// <summary>
        /// 收费记录
        /// </summary>
        public virtual TjlMReceiptInfo MReceiptInfo { get; set; }

        /// <summary>
        /// 支付方式Id外键
        /// </summary>
        [ForeignKey("MChargeType")]
        public virtual Guid? MChargeTypeId { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual TbmMChargeType MChargeType { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        [StringLength(64)]
        public virtual string MChargeTypename { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [StringLength(64)]
        public virtual string CardNum { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 折扣价格
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

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