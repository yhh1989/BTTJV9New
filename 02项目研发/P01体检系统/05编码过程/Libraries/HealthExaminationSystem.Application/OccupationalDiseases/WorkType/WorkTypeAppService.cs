using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType
{
    [AbpAuthorize]
    [Obsolete("暂停使用", true)]
    public class WorkTypeAppService : IWorkTypeAppService
    {
        private readonly IRepository<Core.Illness.WorkType, Guid> _worktyperepository;

        public WorkTypeAppService(IRepository<Core.Illness.WorkType, Guid> worktyperepository)
        {
            _worktyperepository = worktyperepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public WorkTypeDto Insert(WorkTypeDto input)
        {
            return (_worktyperepository.Insert(input.MapTo<Core.Illness.WorkType>())).MapTo<WorkTypeDto>();
        }
        //修改
        public WorkTypeDto Edit(WorkTypeDto input)
        {
            var dto = Get(new EntityDto<Guid> { Id = input.Id });
            dto.MapTo(input);
            return _worktyperepository.Update(dto.MapTo<Core.Illness.WorkType>()).MapTo<WorkTypeDto>();
        }
        //获取单一工种
        public WorkTypeDto Get(EntityDto<Guid> input)
        {
            return (_worktyperepository.GetAll().Where(o => o.Id == input.Id)).MapTo<WorkTypeDto>();
        }
        //获取所有工种
        public List<WorkTypeDto> GetAll()
        {
            return (_worktyperepository.GetAll()).MapTo<List<WorkTypeDto>>();
        }
    }
}
