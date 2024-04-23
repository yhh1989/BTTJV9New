using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 组单分组
    /// <para>组单 -1-n- 组单分组 -1-n- 分组项目 - 1-1- 项目组合</para>
    /// </summary>
    public class TbmComposeGroup : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 组单Id
        /// </summary>
        [ForeignKey("Compose")]
        public virtual Guid ComposeId { get; set; }

        /// <summary>
        /// 组单
        /// </summary>
        [Required]
        public virtual TbmCompose Compose { get; set; }

        /// <summary>
        /// 组单分组项目（组合）集合
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TbmItemGroup> ItemGroups { get; set; }

        /// <summary>
        /// 组单分组项目（组合）集合
        /// </summary>
        public virtual ICollection<TbmComposeGroupItem> ComposeGroupItems { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 适用性别
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int MaxAge { get; set; }

        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int MaritalStatus { get; set; }

        /// <summary>
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }

        /// <summary>
        /// 是否早餐
        /// </summary>
        public virtual int? BreakfastStatus { get; set; }

        /// <summary>
        /// 是否短信
        /// </summary>
        public virtual int? MessageStatus { get; set; }

        /// <summary>
        /// 是否邮寄
        /// </summary>
        public virtual int? EmailStatus { get; set; }

        /// <summary>
        /// 是否健康管理
        /// </summary>
        public virtual int? HealthyMGStatus { get; set; }

        /// <summary>
        /// 是否盲检查 1正常2盲检
        /// </summary>
        public virtual int BlindSate { get; set; }

        /// <summary>
        /// 体检类型 字典 TJType
        /// </summary>
        public virtual int TJType { get; set; }

        /// <summary>
        /// 分组价格
        /// </summary>
        public virtual decimal? TeamMoney { get; set; }

        /// <summary>       
        /// 分组折扣
        /// </summary>
        public virtual decimal? TeamDiscount { get; set; }

        /// <summary>
        /// 分组折扣后价格
        /// </summary>
        public virtual decimal? TeamDiscountMoney { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        public int TenantId { get; set; }
    }
}