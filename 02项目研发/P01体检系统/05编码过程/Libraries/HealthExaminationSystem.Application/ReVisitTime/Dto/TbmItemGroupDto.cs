using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemGroup))]
#endif
    public class TbmItemGroupDto :EntityDto<Guid> 
    {


        /// <summary>
        /// 单价 最小折扣核算后的价格
        /// </summary>
        public virtual decimal? Price { get; set; }
    }
}
