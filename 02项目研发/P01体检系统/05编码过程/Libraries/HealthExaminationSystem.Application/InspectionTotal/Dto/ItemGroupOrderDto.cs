
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmItemGroup))]
#endif
    public class ItemGroupOrderDto
    {

        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
