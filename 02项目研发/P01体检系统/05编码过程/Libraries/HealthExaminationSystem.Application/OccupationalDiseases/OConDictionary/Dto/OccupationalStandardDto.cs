using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto
{
   public   class OccupationalStandardDto :Entity<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public string Num { get; set; }

        /// <summary>
        /// 职业健康标准
        /// </summary>
        [StringLength(32)]
        public bool ISZYB { get; set; }

        /// <summary>
        /// 职业健康标准名称
        /// </summary>
        [StringLength(32)]
        public string DiagnosisStandard { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TbmOccupationalName tbmOccupationalName { get; set; }

 

    }
}
