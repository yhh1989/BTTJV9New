using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [AutoMapTo(typeof(TjlClientTeamRegitem))]
#endif
    public class CreateClientTeamRegItemDto : EntityDto<Guid>
    {
        public virtual Guid? TbmItemGroupId { get; set; }
        /// <summary>
        /// 组合折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

        /// <summary>
        /// 组合折扣后价格
        /// </summary>
        public virtual decimal ItemGroupDiscountMoney { get; set; }
        /// <summary>
        /// 支付方式 个人支付 团体支付
        /// </summary>
        public virtual int PayerCatType { get; set; }
    }
}