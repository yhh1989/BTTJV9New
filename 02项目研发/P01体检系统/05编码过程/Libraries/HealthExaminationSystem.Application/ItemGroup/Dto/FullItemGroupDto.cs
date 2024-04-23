using System.Collections.Generic;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmItemGroup))] 
#endif
    public class FullItemGroupDto : ItemGroupDto
    {
        /// <summary>
        /// 组合项目对照表
        /// </summary>
        public virtual List<ItemInfoSimpleDto> ItemInfos { get; set; }
        public virtual List<TbmGroupRePriceSynchronizesDto> GroupRePriceSynchronizes { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string GetOccDiseases
        {
            get
            {
                if (GroupRePriceSynchronizes == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = GroupRePriceSynchronizes.Select(o => o.PriceSynchronize.chkit_name).ToList();
                    return string.Join(",", GetDisease);
                }
            }
        }
    }
}