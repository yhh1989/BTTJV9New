using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
   public  class TbmSumConflict : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 关键词
        /// </summary>            
        [StringLength(640)]
        public virtual string SumWord { get; set; }
        /// <summary>
        /// 性别限制
        /// </summary>
        public virtual int? IsSex { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 年龄限制
        /// </summary>
        public virtual int? IsAge { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
