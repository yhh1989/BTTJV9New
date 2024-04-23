using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OPostState
{
    public interface IPostStateAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        JobCategoryDto Insert(JobCategoryDto input);
        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        JobCategoryDto Edit(JobCategoryDto input);
        /// <summary>
        /// 修改单一岗位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        JobCategoryDto Get(EntityDto<Guid> input);
        /// <summary>
        /// 获取全部岗位
        /// </summary>
        List<JobCategoryDto> GetAll(JobCategoryDto input);
    }
}
