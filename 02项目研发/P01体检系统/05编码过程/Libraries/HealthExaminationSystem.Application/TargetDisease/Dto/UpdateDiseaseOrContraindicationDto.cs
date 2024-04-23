using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Illness.Enums;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness.Enums;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto
{

#if !Proxy
    [AutoMap(typeof(Core.Illness.DiseaseContraindicationExplain))]
#endif
    /// <summary>
    /// 职业健康和禁忌证更新
    /// </summary>
    public class UpdateDiseaseContraindicationExplainDto : EntityDto<Guid>
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
        /// 危害因素标识
        /// </summary>
        public Guid RiskFactorId { get; set; }

        /// <summary>
        /// 岗位类别标识
        /// </summary>
        public Guid JobCategoryId { get; set; }
    }
}
