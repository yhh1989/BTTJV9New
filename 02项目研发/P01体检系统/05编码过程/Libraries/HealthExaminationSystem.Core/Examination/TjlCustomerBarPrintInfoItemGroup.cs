using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 条码打印记录
    /// </summary>
    public class TjlCustomerBarPrintInfoItemGroup : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 条码打印信息标识
        /// </summary>
        [ForeignKey("BarPrintInfo")]
        public virtual Guid? BarPrintInfoId { get; set; }

        /// <summary>
        /// 条码ID
        /// </summary>
        public virtual TjlCustomerBarPrintInfo BarPrintInfo { get; set; }

        /// <summary>
        /// 条码打印信息标识
        /// </summary>
        [ForeignKey("ItemGroup")]
        public virtual Guid? itemgroup_Id { get; set; }
        

        /// <summary>
        /// 组合
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合别名
        /// </summary>
        [StringLength(32)]
        [NotMapped]
        [Obsolete("停止使用！", true)]
        public virtual TbmItemGroup ItemGroupNameB { get; set; }

        /// <summary>
        /// 组合别名
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupNameAlias { get; set; }

        /// <summary>
        /// 核收状态0未核收1已核实
        /// </summary>
        public virtual bool CollectionState { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}