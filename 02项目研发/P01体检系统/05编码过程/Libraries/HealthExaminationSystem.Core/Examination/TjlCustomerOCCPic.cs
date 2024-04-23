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
    public class TjlCustomerOCCPic : AuditedEntity<Guid>, IMustHaveTenant
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