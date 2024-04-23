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
   public  class OccupationalStandard : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(32)]
        public string Num { get; set; }

        /// <summary>
        /// 职业健康标准
        /// </summary>
        [MaxLength(32)]
        public bool ISZYB { get; set; }

        /// <summary>
        /// 职业健康标准名称
        /// </summary>
        [MaxLength(32)]
        public string DiagnosisStandard { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }
        /// <summary>
        /// 职业健康ID
        /// </summary>

        public Guid? tbmOccupationalNameId { get; set; }
        /// <summary>
        /// 职业健康
        /// </summary>

        public TbmOccupationalName tbmOccupationalName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
