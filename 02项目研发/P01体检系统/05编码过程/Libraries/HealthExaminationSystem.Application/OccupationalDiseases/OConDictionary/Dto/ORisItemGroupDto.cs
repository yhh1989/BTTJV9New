#if !Proxy
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using System;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
#endif

using Abp.Application.Services.Dto;
using System;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmORisItemGroup))]
#endif
    public class ORisItemGroupDto : EntityDto<Guid>
    {

        /// <summary>
        /// 职业健康结论id
        /// </summary>
        OConDictionaryDto OConDictionary { get; set; }
        /// <summary>
        /// 岗位类别
        /// </summary>
    
        public virtual string OPostStates { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
     
        public virtual ORiskFactorDto RiskName { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
      
        public virtual string RiskNames { get; set; }

        /// <summary>
        /// 项目类型 体格检查、必检项目、选检项目
        /// </summary>

        public virtual string ItemType { get; set; }

        /// <summary>
        /// 检查组合编码
        /// </summary>
        public virtual Guid ItemGroupId { get; set; }
        /// <summary>
        /// 检查组合
        /// </summary>
        public SimpleItemGroupDto ItemGroup { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
     
        public virtual string ItemGroupName { get; set; }


        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
