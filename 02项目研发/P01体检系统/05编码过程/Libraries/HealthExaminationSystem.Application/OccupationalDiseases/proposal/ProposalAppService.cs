using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.proposal.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.proposal
{
    [AbpAuthorize]
    [Obsolete("暂停使用", true)]
    public class ProposalAppService : IProposalAppService
    {
        private readonly IRepository<TbmOProposal, Guid> _proposalrepository;

        public ProposalAppService(IRepository<TbmOProposal, Guid> proposalrepository)
        {
            _proposalrepository = proposalrepository;
        }

        /// <summary>
        /// 职业健康建议添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public OProposalDto Insert(OProposalDto input)
        {
            return (_proposalrepository.Insert(input.MapTo<TbmOProposal>())).MapTo<OProposalDto>();
        }
        /// <summary>
        ///职业健康建议修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OProposalDto Edit(OProposalDto input)
        {
            var dto = Get(new EntityDto<Guid> { Id = input.Id });
            dto.MapTo(input);
            return _proposalrepository.Update(dto.MapTo<TbmOProposal>()).MapTo<OProposalDto>();
        }
        /// <summary>
        /// 获取单一职业健康建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OProposalDto Get(EntityDto<Guid> input)
        {
            return (_proposalrepository.GetAll().Where(o => o.Id == input.Id)).MapTo<OProposalDto>();
        }
        /// <summary>
        /// 获取全部职业健康建议
        /// </summary>
        /// <returns></returns>
        public List<OProposalDto> GetAll()
        {
            return (_proposalrepository.GetAll()).MapTo<List<OProposalDto>>();
        }
    }
}
