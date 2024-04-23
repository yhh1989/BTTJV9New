using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.proposal.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.proposal
{
    [Obsolete("暂停使用", true)]
    public interface IProposalAppService
    {
        #region 危害因素
        /// <summary>
        /// 添加职业健康建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OProposalDto Insert(OProposalDto input);
        /// <summary>
        /// 修改职业健康建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OProposalDto Edit(OProposalDto input);
        /// <summary>
        /// 获取单一职业健康建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OProposalDto Get(EntityDto<Guid> input);
        /// <summary>
        /// 获取职业健康建议
        /// </summary>
        List<OProposalDto> GetAll();
        #endregion
    }
}
