using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Interfase
{
    /// <summary>
    /// 上传数据主表
    /// </summary>
   public  class TjlUploadInfo : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 返回编码
        /// </summary>     
        [StringLength(20)]
        public virtual string returnCode { get; set; }
        /// <summary>
        /// 对应的错误说明
        /// </summary>
        [StringLength(3072)]
        public virtual string message { get; set; }
        /// <summary>
        /// 对应的错误数据
        /// </summary>
        [StringLength(3072)]
        public virtual string errorDatas { get; set; }

        /// <summary>
        /// 单条有误的个案卡
        /// </summary>
        [StringLength(3072)]
        public virtual string errorData { get; set; }

        /// <summary>
        /// 该条错误个案卡唯一编码
        /// </summary>
        [StringLength(3072)]
        public virtual string cardId { get; set; }
        /// <summary>
        /// 该条错误案卡错误原因
        /// </summary>
        [StringLength(3072)]
        public virtual string errorMessage { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
