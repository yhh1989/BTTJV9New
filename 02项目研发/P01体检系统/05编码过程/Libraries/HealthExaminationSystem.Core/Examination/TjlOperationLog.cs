using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
   public  class TjlOperationLog : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        ///体检号/单位编码
        /// </summary>
        [StringLength(24)]
        [Index]
        public virtual string LogBM { get; set; }
        /// <summary>
        ///姓名/单位名称
        /// </summary>
        [StringLength(100)]
        [Index]
        public virtual string LogName { get; set; }
       
        /// <summary>
        ///日志类型
        /// </summary> 
        public virtual int? LogType { get; set; }
        /// <summary>
        ///简介
        /// </summary>
        [StringLength(100)]
        public virtual string LogText { get; set; }
        /// <summary>
        ///详情
        /// </summary>
        [StringLength(500)]
        public virtual string LogDetail { get; set; }

        /// <summary>
        ///IP
        /// </summary>
        [StringLength(24)]
        public virtual string IPAddress { get; set; }

        /// <summary>
        ///操作者
        /// </summary>
        [ForeignKey("UseInfo")]
        public virtual long? UseId { get; set; }
        /// <summary>
        ///操作者
        /// </summary>
        public virtual User UseInfo { get; set; }

        [Index]
        public override DateTime CreationTime { get; set; }
        public int TenantId { get; set; }
    }
}
