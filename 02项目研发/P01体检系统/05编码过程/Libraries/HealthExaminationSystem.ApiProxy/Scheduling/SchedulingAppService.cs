using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Scheduling
{
    public class SchedulingAppService : AppServiceApiProxyBase, ISchedulingAppService
    {
        public void DeleteScheduling(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemGroupOfScheduleDto> GetAllListOfItemGroup()
        {
            return GetResult<List<ItemGroupOfScheduleDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientInfoRegDto> GetAllListOfClientInfo()
        {
            return GetResult<List<ClientInfoRegDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public SchedulingNewDto UpdateScheduling(SchedulingNewDto input)
        {
            return GetResult<SchedulingNewDto, SchedulingNewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public SchedulingNewDto InsertScheduling(SchedulingNewDto input)
        {
            return GetResult<SchedulingNewDto, SchedulingNewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SchedulingNewDto> GetAllListScheduling()
        {
            return GetResult<List<SchedulingNewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SchedulingNewDto> GetSchedulingByDate(SearchSchedulingNewDto input)
        {
            return GetResult<SearchSchedulingNewDto, List<SchedulingNewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SchedulingNewDto> GetSchedulingByMonth(SearchSchedulingNewDtoForMonth input)
        {
            return GetResult<SearchSchedulingNewDtoForMonth, List<SchedulingNewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public SchedulingNewDto GetSchedulingById(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, SchedulingNewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SchedulingNewDto> GetSchedulingByStartEnd(SearchSchedulingStartEndDto input)
        {
            return GetResult<SearchSchedulingStartEndDto, List<SchedulingNewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
