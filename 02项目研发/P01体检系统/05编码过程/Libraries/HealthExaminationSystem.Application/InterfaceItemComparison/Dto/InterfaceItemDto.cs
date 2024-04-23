using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemInfo))]
#endif
    public class InterfaceItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        [StringLength(64)]
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
