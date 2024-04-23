using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
    /// <summary>
    /// 目标疾病-症状
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmOccTargetDiseaseContraindication))]
#endif
    public class TbmOccTargetDiseaseContraindicationDto : EntityDto<Guid>
    {
        ///// <summary>
        ///// 目标疾病Id
        ///// </summary>
        
        //public virtual Guid? OccTargetDiseaseId { get; set; }

        ///// <summary>
        ///// 目标疾病
        ///// </summary>
        //public virtual string OccTargetDisease { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
