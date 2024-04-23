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
    /// 结算明细表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMReceiptDetails))]
#endif
    
    public class SearchMReceiptDetailsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 作废原ID
        /// </summary>
        public virtual Guid? InvalidTjlMReceiptDetailsId { get; set; }

        /// <summary>
        /// 收费分类
        /// </summary>
        [StringLength(64)]
        public virtual string ReceiptTypeName { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal GroupsMoney { get; set; }

        /// <summary>
        /// 折扣后
        /// </summary>
        public virtual decimal GroupsDiscountMoney { get; set; }

        /// <summary>
        /// 平均折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

    }
}
