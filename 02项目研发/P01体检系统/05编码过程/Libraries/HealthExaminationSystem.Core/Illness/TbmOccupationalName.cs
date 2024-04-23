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
   public  class TbmOccupationalName : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public string Num { get; set; }
        /// <summary>
        /// 症状名称
        /// </summary>
        [StringLength(32)]
        public string Name { get; set; }

        /// <summary>
        /// 症状分类
        /// </summary>
        [StringLength(32)]
        public string Category { get; set; }
        

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public string HelpCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>     
        public bool IsActive { get; set; }

        public List<OccupationalStandard> OccupationalStandards { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
