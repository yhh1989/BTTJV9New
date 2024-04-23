using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
   /// <summary>
   /// 职业健康字典
   /// </summary>
   public  class TbmZYBType : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 职业健康类别
        /// </summary>
        [MaxLength(32)]
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [MaxLength(32)]
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>    
        public virtual int? Order { get; set; }


        /// <inheritdoc />
        public int TenantId { get; set; }
      

       
    }
}
