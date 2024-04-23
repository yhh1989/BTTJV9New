using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary>
    /// 日结表
    /// </summary>
    public class TjlMDiurnalTable : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 收费员标识
        /// </summary>
        [ForeignKey("User")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 收费员标识
        /// </summary>
        [ForeignKey("CkUser")]
        public virtual long? CkUserId { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 结账日期
        /// </summary>
        public virtual DateTime? CheckOutDate { get; set; }

        /// <summary>
        /// 最小收据号
        /// </summary>
        [MaxLength(64)]
        public virtual string MinReceiptCode { get; set; }

        /// <summary>
        /// 最大收据号
        /// </summary>
        [MaxLength(64)]
        public virtual string MaxReceiptCode { get; set; }

        /// <summary>
        /// 收据张数
        /// </summary>
        public virtual int? ReceiptCodeCount { get; set; }

        /// <summary>
        /// 优惠人数
        /// </summary>
        public virtual int? DiscontCount { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal Discontmoney { get; set; }

        /// <summary>
        /// 退费收据张数
        /// </summary>
        public virtual int? RefundCount { get; set; }

        /// <summary>
        /// 退费金额
        /// </summary>
        public virtual decimal RefundCode { get; set; }

        /// <summary>
        /// 总费用:标准价格
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 总收入:实际收取
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 状态:1已审核2未审核
        /// </summary>
        public virtual int ExaminSate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(64)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 个人体检费用
        /// </summary>
        public virtual decimal PersonalMoney { get; set; }

        /// <summary>
        /// 团体体检费用
        /// </summary>
        public virtual decimal ClientMoney { get; set; }

        /// <summary>
        /// 审核人编码
        /// </summary>
        public virtual User CkUser { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}