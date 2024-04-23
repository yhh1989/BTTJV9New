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
   public  class TbmRegsit : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 客户名称
        /// </summary>
        [StringLength(2000)]
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 机器码
        /// </summary>
        [StringLength(2000)]
        public virtual string MachineCode { get; set; }
        /// <summary>
        /// 注册码
        /// </summary>
        [StringLength(2000)]
        public virtual string RegistCode { get; set; }

        public int TenantId { get; set; }

    }
}
