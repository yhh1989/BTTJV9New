using System;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
#endif
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto
{
#if !Proxy
    [AutoMap(typeof(JobCategory))]
#endif
    public class JobCategoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual string Name { get; set; }

        public int TenantId { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
