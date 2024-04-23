using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemProcExpress))]
#endif
    public class CreateItemProExpressDto
    {
        /// <summary>
        /// 项目标识
        /// </summary>      
        public virtual Guid ItemId { get; set; }

        /// <summary>
        /// 项目信息
        /// </summary>    
        public virtual List<ItemInfoSimpleDto> ItemInfoReRelations { get; set; }
        /// <summary>
        /// 公式名称
        /// </summary>      
        [StringLength(640)]
        public virtual string FormulaText { get; set; }
        /// <summary>
        /// 公式值
        /// </summary>      
        [StringLength(640)]
        public virtual string FormulaValue { get; set; }

    }
}
