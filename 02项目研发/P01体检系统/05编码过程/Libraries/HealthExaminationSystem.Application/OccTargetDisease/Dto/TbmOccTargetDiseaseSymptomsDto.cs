using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
    /// <summary>
    /// 目标疾病-症状
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmOccTargetDiseaseSymptoms))]
#endif
    public class TbmOccTargetDiseaseSymptomsDto : EntityDto<Guid>
    {
         
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <inheritdoc />
        public virtual int TenantId { get; set; }
    }
}
