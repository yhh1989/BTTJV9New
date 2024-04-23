using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
#if Application
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlClientTeamInfo))]
#endif
    public class ChargeTeamNameDto : EntityDto<Guid>
    {
        /// <summary>
        /// 分组ID
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        [Obsolete("暂停使用", true)]
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 分组价格
        /// </summary>
        public virtual decimal TeamMoney { get; set; }

        /// <summary>
        /// 分组折扣
        /// </summary>
        public virtual decimal TeamDiscount { get; set; }

        /// <summary>
        /// 分组折扣后价格
        /// </summary>
        public virtual decimal TeamDiscountMoney { get; set; }
    }
}