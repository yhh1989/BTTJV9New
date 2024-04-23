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

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OPostState
{
    [AbpAuthorize]
    public class PostStateAppService : IPostStateAppService
    {
        private readonly IRepository<JobCategory, Guid> _poststaterepository;

        public PostStateAppService(IRepository<JobCategory, Guid> poststaterepository)
        {
            _poststaterepository = poststaterepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JobCategoryDto Insert(JobCategoryDto input)
        {
            return (_poststaterepository.Insert(input.MapTo<JobCategory>())).MapTo<JobCategoryDto>();
        }
        //修改
        public JobCategoryDto Edit(JobCategoryDto input)
        {
            var dto = _poststaterepository.GetAll().FirstOrDefault(o => o.Id == input.Id);
            dto.Name = input.Name;
            return _poststaterepository.Update(dto).MapTo<JobCategoryDto>();
        }
        //获取单一工种
        public JobCategoryDto Get(EntityDto<Guid> input)
        {
            var result = _poststaterepository.GetAll();
            JobCategory job = new JobCategory();
            if (input != null && input.Id != Guid.Empty)
            {
                job = result.FirstOrDefault(o => o.Id == input.Id);
            }
            return job.MapTo<JobCategoryDto>();
        }
        //获取所有工种
        public List<JobCategoryDto> GetAll(JobCategoryDto input)
        {
            return (_poststaterepository.GetAll()).MapTo<List<JobCategoryDto>>();
        }
    }
}
