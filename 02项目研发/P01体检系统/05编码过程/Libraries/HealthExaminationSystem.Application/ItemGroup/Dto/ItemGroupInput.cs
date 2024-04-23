using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
    public class ItemGroupInput
    {
        /// <summary>
        /// 项目组合
        /// </summary>
        public virtual CreateOrUpdateItemGroup ItemGroup { get; set; }
        /// <summary>
        /// 组合项目对照表
        /// </summary>
        public virtual List<Guid> ItemInfoIds { get; set; }
        /// <summary>
        /// 医嘱项目
        /// </summary>
        public virtual List<TbmGroupRePriceSynchronizesDto> PriceSyn { get; set; }
    }
}
