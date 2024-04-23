using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling
{
    [AbpAuthorize]
    public class SchedulingAppService : MyProjectAppServiceBase, ISchedulingAppService
    {
        private readonly IRepository<TjlClientInfo, Guid> _clientInfoRepository;

        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository;

        private readonly IRepository<TjlScheduling, Guid> _schedulingRepository;

        public SchedulingAppService(IRepository<TjlScheduling, Guid> schedulingRepository,
            IRepository<TbmItemGroup, Guid> itemGroupRepository, IRepository<TjlClientInfo, Guid> clientInfoRepository)
        {
            _schedulingRepository = schedulingRepository;
            _itemGroupRepository = itemGroupRepository;
            _clientInfoRepository = clientInfoRepository;
        }

        public void DeleteScheduling(EntityDto<Guid> input)
        {
            _schedulingRepository.Delete(input.Id);
        }

        /// <inheritdoc />
        public List<ItemGroupOfScheduleDto> GetAllListOfItemGroup()
        {
            var query = _itemGroupRepository.GetAll().AsNoTracking();
            var resultQuery = query.Select(r => new {r.Id, r.ItemGroupName, r.HelpChar, r.ChartName});
            return resultQuery.MapTo<List<ItemGroupOfScheduleDto>>();
        }

        public List<ClientInfoRegDto> GetAllListOfClientInfo()
        {
            var query = _clientInfoRepository.GetAll().AsNoTracking();
            query = query.Where(r => r.HelpCode != null && r.HelpCode.Trim() != string.Empty).OrderByDescending(o=>o.CreationTime);
            var resultQuery = query.Select(r => new
                {r.Id, r.ClientBM, r.ClientName, r.ClientAbbreviation, r.HelpCode, r.LinkMan});
            return resultQuery.MapTo<List<ClientInfoRegDto>>();
        }

        public SchedulingNewDto UpdateScheduling(SchedulingNewDto input)
        {
            var data = _schedulingRepository.Get(input.Id);
            var itemGroups = input.ItemGroups;
            input.ItemGroups = null;
            input.MapTo(data);
            if (data.ClientInfoId.HasValue)
            {
                data.ClientInfo = _clientInfoRepository.Get(data.ClientInfoId.Value);
            }

            if (data.ItemGroups == null)
            {
                data.ItemGroups = new List<TbmItemGroup>();
            }
            else
            {
                data.ItemGroups.Clear();
            }

            foreach (var itemGroup in itemGroups)
            {
                var itemGroupEntity = _itemGroupRepository.Get(itemGroup.Id);
                data.ItemGroups.Add(itemGroupEntity);
            }

            var result = _schedulingRepository.Update(data);
            return result.MapTo<SchedulingNewDto>();
        }

        public SchedulingNewDto InsertScheduling(SchedulingNewDto input)
        {
            var itemGroups = input.ItemGroups;
            input.ItemGroups = null;
            var data = input.MapTo<TjlScheduling>();
            if (data.ClientInfoId.HasValue)
            {
                data.ClientInfo = _clientInfoRepository.Get(data.ClientInfoId.Value);
            }

            data.Id = Guid.NewGuid();
            if (data.ItemGroups == null)
            {
                data.ItemGroups = new List<TbmItemGroup>();
            }
            else
            {
                data.ItemGroups.Clear();
            }

            foreach (var itemGroup in itemGroups)
            {
                var itemGroupEntity = _itemGroupRepository.Get(itemGroup.Id);
                data.ItemGroups.Add(itemGroupEntity);
            }

            var result = _schedulingRepository.Insert(data);
            return result.MapTo<SchedulingNewDto>();
        }

        public List<SchedulingNewDto> GetAllListScheduling()
        {
            var query = _schedulingRepository.GetAll().AsNoTracking();
            var date = DateTime.Now.Date;
            query = query.Where(r => r.ScheduleDate >= date);
            query = query.Include(r => r.ClientInfo);
            query = query.Include(r => r.ItemGroups);
            query = query.OrderBy(r => r.ScheduleDate);
            var result = query.ToList();
            return result.MapTo<List<SchedulingNewDto>>();
        }

        public List<SchedulingNewDto> GetSchedulingByDate(SearchSchedulingNewDto input)
        {
            var query = _schedulingRepository.GetAll().AsNoTracking();
            query = query.Where(r => DbFunctions.DiffDays(r.ScheduleDate, input.Date) == 0);
            query = query.Include(r => r.ClientInfo);
            query = query.Include(r => r.ItemGroups);
            var result = query.ToList();
            return result.MapTo<List<SchedulingNewDto>>();
        }

        public List<SchedulingNewDto> GetSchedulingByMonth(SearchSchedulingNewDtoForMonth input)
        {
            var query = _schedulingRepository.GetAll().AsNoTracking();
            query = query.Where(r => DbFunctions.DiffMonths(r.ScheduleDate, input.Date) == 0);
            query = query.Where(r => r.IsTeam == input.IsTeam);
            query = query.Include(r => r.ClientInfo);
            query = query.Include(r => r.ItemGroups);
            query = query.OrderBy(r => r.ScheduleDate);
            var result = query.ToList();
            return result.MapTo<List<SchedulingNewDto>>();
        }

        public SchedulingNewDto GetSchedulingById(EntityDto<Guid> input)
        {
            var row = _schedulingRepository.Get(input.Id);
            return row.MapTo<SchedulingNewDto>();
        }

        public List<SchedulingNewDto> GetSchedulingByStartEnd(SearchSchedulingStartEndDto input)
        {
            var query = _schedulingRepository.GetAll().AsNoTracking();
            var start = input.Start.Date;
            var end = input.End.Date.AddDays(1);
            query = query.Where(r => r.ScheduleDate >= start && r.ScheduleDate < end);
            query = query.Include(r => r.ClientInfo);
            query = query.Include(r => r.ItemGroups);
            var result = query.ToList();
            return result.MapTo<List<SchedulingNewDto>>();
        }
    }
}