using System;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
#endif
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmOPostState))]
#endif
    [Obsolete("暂停使用", true)]
    public class PostStateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual string PostStateName { get; set; }

        public int TenantId { get; set; }
    }
}
