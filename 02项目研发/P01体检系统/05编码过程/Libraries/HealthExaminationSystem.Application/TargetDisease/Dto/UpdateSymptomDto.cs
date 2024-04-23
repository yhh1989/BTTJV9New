
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(Symptom))]
#endif
    public class UpdateSymptomDto : EntityDto<Guid>
    {
        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <summary>
        /// 症状名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public string MnemonicCode { get; set; }

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
