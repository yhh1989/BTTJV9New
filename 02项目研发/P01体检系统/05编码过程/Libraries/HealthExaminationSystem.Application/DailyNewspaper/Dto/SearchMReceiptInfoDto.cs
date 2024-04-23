using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper.Dto
{
    /// <summary>
    /// 收费记录
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMReceiptInfo))]
#endif

    public class SearchMReceiptInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 作废原ID
        /// </summary>
        public virtual Guid? InvalidTjlMReceiptInfoId { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual ICollection<SearchMPaymentDto> MPayment { get; set; }

        /// <summary>
        /// 体检人预约Id外键
        /// </summary>
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 个人预约ID
        /// </summary>
        public virtual SearchCustomerRegDto CustomerReg { get; set; }

        /// <summary>
        /// 体检人信息标识
        /// </summary>
        public virtual Guid? CustomerId { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual SearchCustomerDto Customer { get; set; }

        ///// <summary>
        ///// 项目明细
        ///// </summary>
        //[NotMapped]
        //[Obsolete("暂停使用", true)]
        //public virtual ICollection<TjlMReceiptInfoDetailed> MReceiptInfoDetailed { get; set; }

        /// <summary>
        /// 结算明细记录
        /// </summary>
        public virtual ICollection<SearchMReceiptDetailsDto> MReceiptDetailses { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        ///// <summary>
        ///// 单位预约ID
        ///// </summary>
        //public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 体检类型:1单位2体检人
        /// </summary>
        public virtual int TJType { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public virtual DateTime ChargeDate { get; set; }

        /// <summary>
        /// 总费用:原价放在第一笔消费，如果加项放在第二笔
        /// </summary>
        public virtual decimal Summoney { get; set; }

        /// <summary>
        /// 应收:优惠
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

        /// <summary>
        /// 收费员标识
        /// </summary>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        public virtual UserForComboDto User { get; set; }

        /// <summary>
        /// 交费标示:1正常收费2欠费3有退费4预支付
        /// </summary>
        public virtual int ChargeState { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal Discontmoney { get; set; }

        ///// <summary>
        ///// 优惠原因
        ///// </summary>
        //[StringLength(256)]
        //public virtual string DiscontReason { get; set; }

        /// <summary>
        /// 收费状态:1正常收费2已作废3作废
        /// </summary>
        public virtual int ReceiptSate { get; set; }

        /// <summary>
        /// 是否结算:1已结算2未结算
        /// </summary>
        public virtual int SettlementSate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }

        ///// <summary>
        ///// 日结表编码
        ///// </summary>
        //public virtual TjlMDiurnalTable MDiurnalTable { get; set; }
    }
}
