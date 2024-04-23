using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 套餐与项目组合关联表
    /// </summary>
    public class TbmItemSuitItemGroupContrast : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 项目套餐标识
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [ForeignKey("ItemSuit")]
        public virtual Guid ItemSuitId { get; set; }

        /// <summary>
        /// 项目分组标识
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [ForeignKey("ItemGroup")]
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 套餐ID
        /// </summary>
        public virtual TbmItemSuit ItemSuit { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

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

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}