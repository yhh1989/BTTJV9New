using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.WorkType
{
    [AbpAuthorize]
    public class WorkTypeAppService : MyProjectAppServiceBase, IWorkTypeAppService
    {
        private readonly IRepository<Core.Illness.WorkType, Guid> _workTypeRepository;

        public WorkTypeAppService(IRepository<Core.Illness.WorkType, Guid> workTypeRepository)
        {
            _workTypeRepository = workTypeRepository;
        }

        public WorkTypeDto Add(WorkTypeDto input)
        {
            if (_workTypeRepository.GetAll().Any(r => r.Name == input.Name))
            {
                throw new FieldVerifyException("名称已经存在！", "名称已经存在！");
            }
            var entity = input.MapTo<Core.Illness.WorkType>();
            entity.Id = Guid.NewGuid();
            //entity.Order = 0;
            entity.HelpChar = ChineseHelper.GetBriefCode(input.Name);
            entity = _workTypeRepository.Insert(entity);
            var dto = entity.MapTo<WorkTypeDto>();
            return dto;
        }

        public void Del(WorkTypeDto input)
        {
            _workTypeRepository.Delete(input.Id);
        }
        /// <summary>
        /// 工种编辑
        /// </summary>
        public WorkTypeDto Edit(WorkTypeDto input)
        {
            if (_workTypeRepository.GetAll().Any(r => r.Name == input.Name&&r.Id!=input.Id))
            {
                throw new FieldVerifyException("名称已经存在！", "名称已经存在！");
            }
            var entity = _workTypeRepository.Get(input.Id);
            input.MapTo(entity); // 赋值
            entity = _workTypeRepository.Update(entity);
            var dto = entity.MapTo<WorkTypeDto>();
            return dto;
        }

        public WorkTypeDto Get(WorkTypeDto input)
        {
            var query = BuildQuery(input);
            var entity = query.FirstOrDefault();
            var dto = entity.MapTo<WorkTypeDto>();
            return dto;
        }

        public List<WorkTypeDto> Query(WorkTypeDto input)
        {
            var query = BuildQuery(input);
            var dtos = query.MapTo<List<WorkTypeDto>>();
       
            return dtos;
        }

        private IQueryable<Core.Illness.WorkType> BuildQuery(WorkTypeDto input)
        {
            var query = _workTypeRepository.GetAll();
            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Id);
                }
                else
                {
                    if (!string.IsNullOrEmpty(input.Name))
                        query = query.Where(m => m.Name.Contains(input.Name));
                    if (!string.IsNullOrEmpty(input.HelpChar))
                        query = query.Where(m => m.HelpChar.Contains(input.HelpChar));
                    
                    if (input.Category != null)
                        query = query.Where(m => m.Category == input.Category);
                }
            }

            return query.OrderBy(o=>o.Order);
        }
    }
}