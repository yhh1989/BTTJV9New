using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
   public  class SimpOccHazardFactorDto : EntityDto<Guid>
    {
        /// <summary>
        /// CAS编码
        /// </summary>
        [StringLength(50)]
        public virtual string CASBM { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }

        /// <summary>
        /// 防护措施
        /// </summary>
        public virtual ICollection<SimpOccHazardFactorsProtectiveDto> Protectivis { get; set; }
    }
}
