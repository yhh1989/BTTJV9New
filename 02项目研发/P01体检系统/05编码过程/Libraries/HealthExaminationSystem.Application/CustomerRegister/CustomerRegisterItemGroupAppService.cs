using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister
{
    /// <summary>
    /// 体检人预约的项目组合应用服务
    /// </summary>
    [AbpAuthorize]
    public class CustomerRegisterItemGroupAppService : MyProjectAppServiceBase, ICustomerRegisterItemGroupAppService
    {
        /// <summary>
        /// 体检人预约的项目组合仓储
        /// </summary>
        private readonly IRepository<TjlCustomerItemGroup, Guid> _customerRegisterItemGroupRepository;

        /// <summary>
        /// 初始化“体检人预约的项目组合应用服务”
        /// </summary>
        /// <param name="customerRegisterItemGroupRepository"></param>
        public CustomerRegisterItemGroupAppService(IRepository<TjlCustomerItemGroup, Guid> customerRegisterItemGroupRepository)
        {
            _customerRegisterItemGroupRepository = customerRegisterItemGroupRepository;
        }

        /// <inheritdoc />
        public async Task<List<CustomerRegisterItemGroupDtoNo4>> CustomerRegisterItemGroupList(List<Guid> input)
        {
            var query = _customerRegisterItemGroupRepository.GetAll();
            query = query.Where(r => r.CustomerRegBMId != null && input.Contains(r.CustomerRegBMId.Value));
            return await query.ProjectToListAsync<CustomerRegisterItemGroupDtoNo4>(
                GetConfigurationProvider<CustomerRegisterItemGroupDtoNo4>());
        }
    }
}