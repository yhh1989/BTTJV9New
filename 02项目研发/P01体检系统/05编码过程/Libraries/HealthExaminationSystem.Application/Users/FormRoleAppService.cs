using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users
{
    [AbpAuthorize]
    public class FormRoleAppService : MyProjectAppServiceBase, IFormRoleAppService
    {
        private readonly IRepository<FormRole, Guid> _formRoleRepository;

        private readonly IRepository<FormModule, Guid> _formModuleRepository;

        public FormRoleAppService(IRepository<FormRole, Guid> formRoleRepository, IRepository<FormModule, Guid> formModuleRepository)
        {
            _formRoleRepository = formRoleRepository;
            _formModuleRepository = formModuleRepository;
        }

        public List<FormRoleDto> GetAll()
        {
            return _formRoleRepository.GetAllList().MapTo<List<FormRoleDto>>();
        }

        [UnitOfWork(isTransactional: false)]
        public List<FormRoleViewDto> GetAllForView()
        {
            var result = _formRoleRepository.GetAllIncluding(r => r.FormModules);
            return result.MapTo<List<FormRoleViewDto>>();
        }

        public List<FormModuleViewDto> GetAllFormModules()
        {
            var result = _formModuleRepository.GetAllList().OrderBy(r => r.Name);
            return result.MapTo<List<FormModuleViewDto>>();
        }

        public FormRoleViewDto GetById(EntityDto<Guid> input)
        {
            var result = _formRoleRepository.Get(input.Id);
            return result.MapTo<FormRoleViewDto>();
        }

        public void Create(CreateFormRoleDto input)
        {
            if (_formRoleRepository.GetAll().Any(r => r.Name == input.Name))
            {
                throw new FieldVerifyException("角色已经存在！", "角色已经存在！");
            }
            var entity = input.MapTo<FormRole>();
            entity.Id = Guid.NewGuid();
            if (input.Modules != null)
            {
                if (entity.FormModules == null)
                {
                    entity.FormModules = new List<FormModule>();
                }
                foreach (var module in input.Modules)
                {
                    entity.FormModules.Add(_formModuleRepository.Get(module));
                }
            }

            _formRoleRepository.Insert(entity);
        }

        public void Update(UpdateFormRoleDto input)
        {

            if (_formRoleRepository.GetAll().Any(r => r.Name == input.Name && r.Id != input.Id))
            {
                throw new FieldVerifyException("角色已经存在！", "角色已经存在！");
            }
            var entity = _formRoleRepository.Get(input.Id);
            input.MapTo(entity);
            if (input.Modules != null)
            {
                if (entity.FormModules == null)
                {
                    entity.FormModules = new List<FormModule>();
                }
                else
                {
                    entity.FormModules.Clear();
                }
                foreach (var module in input.Modules)
                {
                    entity.FormModules.Add(_formModuleRepository.Get(module));
                }
            }

            _formRoleRepository.Update(entity);
        }

        public void Delete(EntityDto<Guid> input)
        {
            _formRoleRepository.Delete(input.Id);
        }
    }
}