using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users
{
    [AbpAuthorize]
    public class PersonnelIndividuationConfigAppService : MyProjectAppServiceBase,
        IPersonnelIndividuationConfigAppService
    {
        private readonly IRepository<User, long> _userRepository;

        private readonly IRepository<PersonnelIndividuationConfig, long> _personnelIndividuationConfigRepository;

        private readonly IRepository<StartupMenuBar, Guid> _startupMenuBarRepository;

        public PersonnelIndividuationConfigAppService(IRepository<User, long> userRepository, IRepository<PersonnelIndividuationConfig, long> personnelIndividuationConfigRepository, IRepository<StartupMenuBar, Guid> startupMenuBarRepository)
        {
            _userRepository = userRepository;
            _personnelIndividuationConfigRepository = personnelIndividuationConfigRepository;
            _startupMenuBarRepository = startupMenuBarRepository;
        }

        public List<UserIndividuationConfigsDto> GetAllUsers()
        {
            var query = _userRepository.GetAllIncluding(r => r.IndividuationConfig).AsNoTracking();
            var users = query.MapTo<List<UserIndividuationConfigsDto>>();
            return users;
        }

        public List<UserIndividuationConfigsDto> GetUsersByCondition(SearchUserIndividuationConfigDto input)
        {
            var query = _userRepository.GetAllIncluding(r => r.IndividuationConfig).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                query = query.Where(r => r.UserName == input.UserName);
            }
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(r => r.Name == input.Name);
            }
            var users = query.MapTo<List<UserIndividuationConfigsDto>>();
            return users;
        }

        public UserIndividuationConfigsDto GetUserById(EntityDto<long> input)
        {
            var user = _userRepository.Get(input.Id);
            return user.MapTo<UserIndividuationConfigsDto>();
        }

        public void CreatePersonnelIndividuationConfig(CreateOrUpdatePersonnelIndividuationConfigDto input)
        {
            var personnelIndividuationConfig = input.MapTo<PersonnelIndividuationConfig>();
            personnelIndividuationConfig.User = _userRepository.Get(input.UserId);
            if (input.StartupMenuBarIds.Count != 0)
            {
                if (personnelIndividuationConfig.StartupMenuBars == null)
                {
                    personnelIndividuationConfig.StartupMenuBars = new List<StartupMenuBar>();
                }

                foreach (var id in input.StartupMenuBarIds)
                {
                    personnelIndividuationConfig.StartupMenuBars.Add(_startupMenuBarRepository.Get(id));
                }
            }
            _personnelIndividuationConfigRepository.Insert(personnelIndividuationConfig);
        }

        public void UpdatePersonnelIndividuationConfig(CreateOrUpdatePersonnelIndividuationConfigDto input)
        {
            var personnelIndividuationConfig = _personnelIndividuationConfigRepository.Get(input.UserId);
            input.MapTo(personnelIndividuationConfig);
            if (input.StartupMenuBarIds.Count != 0)
            {
                if (personnelIndividuationConfig.StartupMenuBars == null)
                {
                    personnelIndividuationConfig.StartupMenuBars = new List<StartupMenuBar>();
                }
                else
                {
                    personnelIndividuationConfig.StartupMenuBars.Clear();
                }

                foreach (var id in input.StartupMenuBarIds)
                {
                    personnelIndividuationConfig.StartupMenuBars.Add(_startupMenuBarRepository.Get(id));
                }
            }
            else
            {
                personnelIndividuationConfig.StartupMenuBars?.Clear();
            }
            _personnelIndividuationConfigRepository.Update(personnelIndividuationConfig);
        }

        public List<StartupMenuBarDto> GetAllStartupMenuBars()
        {
            var rows = _startupMenuBarRepository.GetAllList();
            return rows.MapTo<List<StartupMenuBarDto>>();
        }
    }
}