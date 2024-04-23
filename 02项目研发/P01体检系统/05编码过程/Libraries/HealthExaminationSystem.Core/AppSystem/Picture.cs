using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 图像存储表
    /// </summary>
    public class Picture : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <inheritdoc />
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 图片在程序中的相对路径
        /// </summary>
        public virtual string RelativePath { get; set; }
    
        /// <summary>
        /// 缩略图
        /// </summary>
        public virtual string Thumbnail { get; set; }

        /// <summary>
        /// 归属于
        /// </summary>
        [StringLength(32)]
        public virtual string Belong { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
    }
}