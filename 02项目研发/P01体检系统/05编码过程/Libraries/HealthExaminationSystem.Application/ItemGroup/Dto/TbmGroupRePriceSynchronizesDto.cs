using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmGroupRePriceSynchronizes))]
#endif
    public class TbmGroupRePriceSynchronizesDto : EntityDto<Guid>
    {
        /// <summary>
        /// 物价标识
        /// </summary>       
        public virtual Guid PriceSynchronizeId { get; set; }

        /// <summary>
        /// 物价
        /// </summary>
        public virtual priceSynDto PriceSynchronize { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal? Count { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public virtual decimal? chkit_costn { get; set; }
    }
}
