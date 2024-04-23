using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OPostState
{
    public class PostStateAppService : AppServiceApiProxyBase, IPostStateAppService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JobCategoryDto Insert(JobCategoryDto input)
        {
            return GetResult<JobCategoryDto,JobCategoryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        //修改
        public JobCategoryDto Edit(JobCategoryDto input)
        {
            return GetResult<JobCategoryDto, JobCategoryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        //获取单一工种
        public JobCategoryDto Get(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, JobCategoryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        //获取所有工种
        public List<JobCategoryDto> GetAll(JobCategoryDto input)
        {
            return GetResult<JobCategoryDto, List<JobCategoryDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
