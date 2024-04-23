using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto
{
    public class SearchReceiptInfoDto
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime strChargeDate { get; set; }
        /// <summary>
        /// 收费日期
        /// </summary>
        public virtual DateTime ChargeDate { get; set; }

        /// <summary>
        /// 收费员标识
        /// </summary>
        public virtual long? UserId { get; set; }
        /// <summary>
        /// 结算状态
        /// </summary>
        public virtual int? ReceiptState{get;set;}
        /// <summary>
        /// 收费or作废
        /// </summary>
        public virtual string InvoiceStatus { get; set; }

        /// <summary>
        /// 个人体检类型
        /// </summary>
        public virtual int? PersonalCharge { get; set; }

        /// <summary>
        /// 团体体检类型
        /// </summary>
        public virtual int? GroupCharge { get; set; }
    }
}