#if !Proxy
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using System;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.proposal.Dto
{
    /// <summary>
    /// 职业总检结论字典
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmOProposal))]
#endif
    [Obsolete("暂停使用", true)]
    public class OProposalDto : EntityDto<Guid>
    {
        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual PostStateDto OPostState { get; set; }
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
        public int TenantId { get; set; }
    }
}
