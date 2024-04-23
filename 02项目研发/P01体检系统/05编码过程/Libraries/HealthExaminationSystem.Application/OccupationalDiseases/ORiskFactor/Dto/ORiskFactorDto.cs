using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
#endif
using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto
{
#if !Proxy
    [AutoMap(typeof(Core.Illness.RiskFactor))]
#endif
    public class ORiskFactorDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 检测资料
        /// </summary>
        public virtual string Data { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public virtual Application.WorkType.Dto.WorkTypeDto WorkType { get; set; }

        /// <summary>
        /// 工种id
        /// </summary>
        public virtual Guid? WorkTypeId { get; set; }

        /// <summary>
        /// 介绍说明
        /// </summary>
        public virtual string Describe { get; set; }

        /// <summary>
        /// 防护措施
        /// </summary>
        public virtual string ProtectiveMeasures { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? Order { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public virtual Guid ParentId { get; set; }
    }
}
