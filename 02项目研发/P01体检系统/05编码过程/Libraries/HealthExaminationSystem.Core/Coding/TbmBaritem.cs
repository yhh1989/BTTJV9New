using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 条码设置项目表
    /// </summary>
    public class TbmBaritem : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 条码设置标识
        /// </summary>
        [ForeignKey(nameof(BarSetting))]
        public virtual Guid BarSettingId { get; set; }

        /// <summary>
        /// 条码设置
        /// </summary>
        [Required]
        public virtual TbmBarSettings BarSetting { get; set; }

        /// <summary>
        /// 项目组合标识
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 项目组合
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

        /// <summary>
        /// 组合别名 组合别名
        /// </summary>
        public virtual string ItemGroupAlias { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public int TenantId { get; set; }
    }
}