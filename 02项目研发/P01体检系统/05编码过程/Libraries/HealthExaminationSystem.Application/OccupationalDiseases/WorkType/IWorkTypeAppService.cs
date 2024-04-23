using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType
{
    [Obsolete("暂停使用", true)]
    public interface IWorkTypeAppService
    {
        /// <summary>
        /// 添加工种/车间/行业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        WorkTypeDto Insert(WorkTypeDto input);
        /// <summary>
        /// 修改工种/车间/行业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        WorkTypeDto Edit(WorkTypeDto input);
        /// <summary>
        /// 修改工种/车间/行业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        WorkTypeDto Get(EntityDto<Guid> input);
        /// <summary>
        /// 获取全部工种
        /// </summary>
        List<WorkTypeDto> GetAll();
    }
}
