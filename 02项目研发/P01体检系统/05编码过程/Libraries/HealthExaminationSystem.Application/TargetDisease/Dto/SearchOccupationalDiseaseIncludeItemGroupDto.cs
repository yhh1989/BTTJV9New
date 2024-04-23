using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(Core.Illness.OccupationalDiseaseIncludeItemGroup))]
#endif
    public class SearchOccupationalDiseaseIncludeItemGroupDto : EntityDto<Guid>
    {

        /// <summary>
        /// 危害因素标识
        /// </summary>
        public Guid RiskFactorId { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public ORiskFactorDto RiskFactor { get; set; }

        /// <summary>
        /// 岗位类别标识
        /// </summary>
        public Guid JobCategoryId { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        public JobCategoryDto JobCategory { get; set; }

        /// <summary>
        /// 包含的必填项目
        /// </summary>
        public ICollection<SimpleItemGroupDto> MustHaveItemGroups { get; set; }

        /// <summary>
        /// 包含的可选项目
        /// </summary>
        public ICollection<SimpleItemGroupDto> MayHaveItemGroups { get; set; }

        /// <summary>
        /// 症状询问列表
        /// </summary>
        public virtual ICollection<UpdateSymptomDto> Symptoms { get; set; }

        /// <summary>
        /// 体检人体系统列表
        /// </summary>
        public virtual ICollection<UpdateHumanBodySystemDto> HumanBodySystems { get; set; }

        /// <summary>
        /// 目标疾病与禁忌证列表
        /// </summary>
        public virtual ICollection<DiseaseContraindicationExplainDto> DiseaseContraindicationExplains { get; set; }
    }
}
