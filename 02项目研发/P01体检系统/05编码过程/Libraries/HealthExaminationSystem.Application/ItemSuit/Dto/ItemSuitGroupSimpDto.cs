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

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemSuitItemGroupContrast))]
#endif
    public class ItemSuitGroupSimpDto : EntityDto<Guid>
    {
       

        public virtual Guid ItemSuitId { get; set; }
        /// <summary>
        /// 组合Id
        /// </summary>
        public virtual Guid ItemGroupId { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string DeptmentName { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual Guid DeptmentId { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary> 
        public virtual int dtpOrder { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual int OrderNum { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal? Suitgrouprate { get; set; }

        /// <summary>
        /// 项目原价
        /// </summary>
        public virtual decimal? ItemPrice { get; set; }

        /// <summary>
        /// 折扣后价格
        /// </summary>
        public virtual decimal? PriceAfterDis { get; set; }
    }
}
