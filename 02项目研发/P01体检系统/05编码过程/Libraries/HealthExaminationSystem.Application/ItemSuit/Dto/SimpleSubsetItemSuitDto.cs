using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemSuit))]
#endif
    public class SimpleSubsetItemSuitDto : SimpleItemSuitDto
    {
        /// <summary>
        /// 套餐组合对照表
        /// </summary>
        public virtual List<ItemSuitItemGroupContrastFullDto> ItemSuitItemGroups { get; set; }
    }
}
