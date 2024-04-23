#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemSuitItemGroupContrast))]
#endif
    public class ItemSuitItemGroupContrastFullDto : ItemSuitItemGroupContrastDto
    {
        /// <summary>
        /// 组合ID
        /// </summary>
        public virtual SimpleItemGroupDto ItemGroup { get; set; }
        /// <summary>
        /// 套餐编码
        /// </summary>
        public virtual string ItemSuitID { get; set; }

    }
}
