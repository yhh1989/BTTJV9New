using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Interfase
{
    /// <summary>
    /// 上传数据表
    /// </summary>
  public   class TjlCusUpload : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }
        /// <summary>
        /// 上传ID
        /// </summary>
        [ForeignKey("UploadInfo")]
        public virtual Guid UploadInfoId { get; set; }

        /// <summary>
        ///  上传记录
        /// </summary>
        public virtual TjlUploadInfo UploadInfo { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public virtual DateTime? LastTime { get; set; }

        /// <summary>
        /// 上传状态1成功0失败
        /// </summary>
        public virtual int? UploadState { get; set; }


        /// <summary>
        /// 上传次数
        /// </summary>
        public virtual int? UploadCount { get; set; }


        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
