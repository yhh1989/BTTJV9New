using Sw.Hospital.HealthExaminationSystem.Application.WorkType;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.WorkType
{

    public class WorkTypeAppService : AppServiceApiProxyBase, IWorkTypeAppService
    {
        public WorkTypeDto Add(WorkTypeDto input)
        {
            return GetResult<WorkTypeDto, WorkTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(WorkTypeDto input)
        {
            GetResult<WorkTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public WorkTypeDto Edit(WorkTypeDto input)
        {
            return GetResult<WorkTypeDto, WorkTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public WorkTypeDto Get(WorkTypeDto input)
        {
            return GetResult<WorkTypeDto, WorkTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<WorkTypeDto> Query(WorkTypeDto input)
        {
            return GetResult<WorkTypeDto, List<WorkTypeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
