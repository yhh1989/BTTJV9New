using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
   public  class TbmGroupRePriceSynchronizes : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 组合标识
        /// </summary>
        [Key]
        [ForeignKey(nameof(ItemGroup))]
        [Column(Order = 0)]
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

        /// <summary>
        /// 物价标识
        /// </summary>
        [Key]
        [ForeignKey(nameof(PriceSynchronize))]
        [Column(Order = 1)]
        public virtual Guid PriceSynchronizeId { get; set; }

        /// <summary>
        /// 物价
        /// </summary>
        public virtual TbmPriceSynchronize PriceSynchronize { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal? Count { get; set; }


        /// <summary>
        /// 总价格
        /// </summary>
        public virtual decimal? chkit_costn { get; set; }

        public int TenantId { get; set; }
    }
}
