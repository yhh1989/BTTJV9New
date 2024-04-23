using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper.Dto
{
    /// <summary>
    /// 结算支付方式
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMPayment))]
#endif

    public class SearchMPaymentDto : EntityDto<Guid>
    {
        /// <summary>
        /// 作废原ID
        /// </summary>
        public virtual Guid? InvalidTjlMPaymentId { get; set; }

        /// <summary>
        /// 支付方式Id外键
        /// </summary>
        public virtual Guid? MChargeTypeId { get; set; }
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
        
    }
}
