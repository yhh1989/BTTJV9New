#if !Proxy
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using System;
#endif

using Abp.Application.Services.Dto;
using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto
{
#if !Proxy
    [Obsolete("暂停使用", true)]
    [AutoMap(typeof(Core.Illness.WorkType))]
#endif
    public class WorkTypeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string WorkName { get; set; }
        /// <summary>
        /// 类别 1工种2车间3行业
        /// </summary>
        public virtual int? ZyWorkTypes { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? Ordernum { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public virtual string WorkNamejp { get; set; }
        public int TenantId { get; set; }
    }
}
