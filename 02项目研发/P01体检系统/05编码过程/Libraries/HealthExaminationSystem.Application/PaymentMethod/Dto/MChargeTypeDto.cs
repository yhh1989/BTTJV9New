using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto
{
    /// <summary>
    /// 支付方式
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmMChargeType))]
#endif
    public class MChargeTypeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 支付方式代码
        /// </summary>
        public virtual int? ChargeCode { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        [StringLength(256)]
        public virtual string ChargeName { get; set; }

        /// <summary>
        /// 支付方式名称简拼
        /// </summary>
        [StringLength(256)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 使用类型 1单位 2个人 3通用
        /// </summary>
        public virtual int? ChargeApply { get; set; }

        /// <summary>
        /// 打印方式:1发票2小票3收据
        /// </summary>
        public virtual int? PrintName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 记账标示:反映该支付方式是否作为实收款进入会计记帐1-不进入记帐2-进入记帐
        /// </summary>
        public virtual int? AccountingState { get; set; }
    }
}