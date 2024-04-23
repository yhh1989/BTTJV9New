using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto
{

#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmItemSuit))]
#endif
    public class TbmItemSuitDto : EntityDto<Guid>
    {
        /// <summary>
        /// 套餐编码
        /// </summary>
        public virtual string ItemSuitID { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public virtual string ItemSuitName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? Price { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
