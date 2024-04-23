#if !Proxy
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemSuit))]
#endif
    public class ItemSuitOutput:EntityDto<Guid>
    {
        /// <summary>
        /// 套餐ID
        /// </summary>
        public virtual string ItemSuitID { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public virtual string ItemSuitName { get; set; }
    }
}
