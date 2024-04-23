#if !Proxy
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using System;
#endif

using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmORiskFactor))]
#endif
    public class OConDictionaryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人项目
        /// </summary>
        public virtual ICollection<ORisItemGroupDto> ORisItemGroup { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual string OPostStates { get; set; }
        /// <summary>
        /// 职业健康
        /// </summary>
        public virtual string ZyContents { get; set; }

        /// <summary>
        /// 职业健康禁忌证
        /// </summary>
        public virtual string ZyjjContents { get; set; }

        /// <summary>
        /// 问诊提示
        /// </summary>
        public virtual string Symptom { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual ORiskFactorDto RiskName { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual string RiskNames { get; set; }
        public int TenantId { get; set; }
    }
}
