using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.WorkType
{
    /// <summary>
    /// 工种接口
    /// </summary>
    public interface IWorkTypeAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加工种
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        WorkTypeDto Add(WorkTypeDto input);

        /// <summary>
        /// 删除工种
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Del(WorkTypeDto input);

        /// <summary>
        /// 修改工种
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        WorkTypeDto Edit(WorkTypeDto input);

        /// <summary>
        /// 获取一个工种
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        WorkTypeDto Get(WorkTypeDto input);

        /// <summary>
        /// 获取添加工种列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WorkTypeDto> Query(WorkTypeDto input);
    }
}