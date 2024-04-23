using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    public class UserPicture : AuditedEntity<Guid>, IMustHaveTenant
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
    }
}
