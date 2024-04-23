#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
using AutoMapper;using Abp.AutoMapper;
#endif

using Abp.Application.Services.Dto;
using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmItemSuit))]
#endif
    public class ItemSuitsDto:EntityDto<Guid>
    {
        /// <summary>
        /// 套餐编码
        /// </summary>
        public virtual string ItemSuitID { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public virtual string ItemSuitName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? Price { get; set; }
        public int? OrderNum { get; set; }
    }
}
