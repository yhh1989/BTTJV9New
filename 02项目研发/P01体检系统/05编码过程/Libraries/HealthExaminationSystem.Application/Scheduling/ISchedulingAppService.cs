using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling
{
    public interface ISchedulingAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 删除排期
        /// </summary>
        /// <param name="input"></param>
        void DeleteScheduling(EntityDto<Guid> input);

        /// <summary>
        /// 获取所有项目组合
        /// </summary>
        /// <returns></returns>
        List<ItemGroupOfScheduleDto> GetAllListOfItemGroup();

        List<ClientInfoRegDto> GetAllListOfClientInfo();

        SchedulingNewDto UpdateScheduling(SchedulingNewDto input);

        SchedulingNewDto InsertScheduling(SchedulingNewDto input);

        List<SchedulingNewDto> GetAllListScheduling();

        List<SchedulingNewDto> GetSchedulingByDate(SearchSchedulingNewDto input);

        List<SchedulingNewDto> GetSchedulingByMonth(SearchSchedulingNewDtoForMonth input);

        SchedulingNewDto GetSchedulingById(EntityDto<Guid> input);

        List<SchedulingNewDto> GetSchedulingByStartEnd(SearchSchedulingStartEndDto input);
    }
}