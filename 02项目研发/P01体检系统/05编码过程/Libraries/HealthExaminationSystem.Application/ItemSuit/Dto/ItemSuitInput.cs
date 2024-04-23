using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{
    public class ItemSuitInput
    {
        /// <summary>
        /// 套餐
        /// </summary>
        public virtual CreateOrUpdateItemSuitDto ItemSuit { get; set; }
        /// <summary>
        /// 套餐组合对照表
        /// </summary>
        public virtual List<ItemSuitItemGroupContrastDto> ItemSuitItemGroups { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public virtual Guid? ClientInfoId { get; set; }
    }
}
