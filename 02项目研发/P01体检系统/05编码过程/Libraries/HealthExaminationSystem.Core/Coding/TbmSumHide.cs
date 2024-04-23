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
    public   class TbmSumHide : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检类别 字典
        /// </summary>
        [StringLength(16)]
        public virtual string ClientType { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        [StringLength(1024)]
        public virtual string SumWord { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(1024)]
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 是否正常 1为正常 2为异常
        /// </summary>
        public virtual int? IsNormal { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
