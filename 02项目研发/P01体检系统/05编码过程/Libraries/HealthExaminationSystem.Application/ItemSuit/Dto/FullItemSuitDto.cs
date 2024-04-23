using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemSuit))]
#endif
    public class FullItemSuitDto : ItemSuitDto
    {
        ///// <summary>
        ///// 岗位类别
        ///// </summary>
        //public virtual PostStateDto OPostState { get; set; }

        /// <summary>
        /// 套餐组合对照表
        /// </summary>
        public virtual List<ItemSuitItemGroupContrastFullDto> ItemSuitItemGroups { get; set; }
    }
}
