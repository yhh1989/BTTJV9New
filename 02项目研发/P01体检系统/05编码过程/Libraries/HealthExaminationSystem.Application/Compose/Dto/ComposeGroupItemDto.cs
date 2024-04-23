using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmComposeGroupItem))]
#endif
    public class ComposeGroupItemDto : EntityDto<Guid>
    {
        public SimpleItemGroupDto ItemGroup { get; set; }

        /// <summary>
        /// 组合价格
        /// </summary>
        public virtual decimal ItemGroupMoney { get; set; }

        /// <summary>
        /// 组合折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

        /// <summary>
        /// 组合折扣后价格
        /// </summary>
        public virtual decimal ItemGroupDiscountMoney { get; set; }

    }
}
