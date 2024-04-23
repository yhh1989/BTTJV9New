using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 检查项目图片
    /// </summary>
    public class TjlCustomerItemPic : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? TjlCustomerRegID { get; set; }
       
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        [ForeignKey(nameof(ItemBM))]
        public virtual Guid? ItemBMID { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual TjlCustomerRegItem ItemBM { get; set; }
        /// <summary>
        /// 项目组合ID
        /// </summary>
        [ForeignKey("CustomerItemGroup")]
        public virtual Guid? CustomerItemGroupID { get; set; }
        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual TjlCustomerItemGroup CustomerItemGroup { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        [ForeignKey("pictures")]
        public virtual Guid? PictureBM { get; set; }

        public virtual Picture pictures{get;set;}

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}