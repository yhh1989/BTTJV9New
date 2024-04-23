using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness.Enums;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness.Enums;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(Core.Illness.DiseaseContraindicationExplain))]
#endif
    public class DiseaseContraindicationExplainDto : EntityDto<Guid>
    {

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 解释
        /// </summary>
        public virtual string Explain { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public DiseaseContraindicationType Type { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        
    }
}
