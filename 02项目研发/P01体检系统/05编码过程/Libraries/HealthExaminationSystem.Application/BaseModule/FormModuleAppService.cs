using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;

namespace Sw.Hospital.HealthExaminationSystem.Application.BaseModule
{
    /// <inheritdoc cref="IFormModuleAppService" />
    [AbpAuthorize]
    public class FormModuleAppService : MyProjectAppServiceBase, IFormModuleAppService
    {
        private readonly IRepository<FormModule, Guid> _formModuleRepository;

        /// <summary>
        /// 窗体模块应用服务
        /// </summary>
        /// <param name="formModuleRepository"></param>
        public FormModuleAppService(IRepository<FormModule, Guid> formModuleRepository)
        {
            _formModuleRepository = formModuleRepository;
        }

        /// <inheritdoc />
        public List<FormModuleDto> GetByNames(FindNameDto input)
        {
            var names = input.Names;
            var formModules = _formModuleRepository.GetAllIncluding(r => r.FormRoles)
                .Where(r => names.Contains(r.Name));
            return formModules.MapTo<List<FormModuleDto>>();
        }

        /// <inheritdoc />
        public FormModuleDto GetByName(NameDto input)
        {
            var formModule = _formModuleRepository.GetAllIncluding(r => r.FormRoles).First(r => r.Name == input.Name);
            return formModule.MapTo<FormModuleDto>();
        }

        /// <inheritdoc />

        public async Task<List<FormModuleDto>> GetAllList()
        {
          
            var query =  _formModuleRepository.GetAllIncluding(r => r.FormRoles);           
           var queryList =  await query.ToListAsync();
            var result = queryList.MapTo<List<FormModuleDto>>();
            return result;
        }
    }
}