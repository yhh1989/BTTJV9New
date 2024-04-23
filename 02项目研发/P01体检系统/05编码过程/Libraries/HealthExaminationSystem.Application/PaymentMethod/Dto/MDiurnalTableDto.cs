using System;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto
{
    public class MDiurnalTableDto
    {
        /// <summary>
        /// 收费员
        /// </summary>
        public virtual UserViewDto User { get; set; }

        /// <summary>
        /// 结账日期
        /// </summary>
        public virtual DateTime? CheckOutDate { get; set; }

        /// <summary>
        /// 最小收据号
        /// </summary>
        [StringLength(64)]
        public virtual string MinReceiptCode { get; set; }

        /// <summary>
        /// 最大收据号
        /// </summary>
        [StringLength(64)]
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
        [StringLength(64)]
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
        public virtual UserViewDto CkUser { get; set; }
    }
}