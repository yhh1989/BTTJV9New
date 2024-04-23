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
    /// 体检人预约总检建议记录应用服务
    /// </summary>
    [AbpAuthorize]
    public class CustomerRegisterSummarizeAdviceAppService : MyProjectAppServiceBase, ICustomerRegisterSummarizeAdviceAppService
    {
        /// <summary>
        /// 体检人预约总检建议仓储
        /// </summary>
        private readonly IRepository<TjlCustomerSummarizeBM, Guid> _customerRegisterSummarizeAdviceRepository;

        /// <summary>
        /// 初始化“体检人预约总检建议记录应用服务”
        /// </summary>
        /// <param name="customerRegisterSummarizeAdviceRepository"></param>
        public CustomerRegisterSummarizeAdviceAppService(IRepository<TjlCustomerSummarizeBM, Guid> customerRegisterSummarizeAdviceRepository)
        {
            _customerRegisterSummarizeAdviceRepository = customerRegisterSummarizeAdviceRepository;
        }

        /// <inheritdoc />
        public async Task<List<CustomerRegisterSummarizeAdviceDtoNo1>> CustomerRegisterSummarizeAdviceListNo1(List<Guid> input)
        {
            var query = _customerRegisterSummarizeAdviceRepository.GetAll();
            query = query.Where(r =>
                r.CustomerReg.ClientRegId != null &&
                input.Contains(r.CustomerReg.ClientRegId.Value));
            return await query.ProjectToListAsync<CustomerRegisterSummarizeAdviceDtoNo1>(
                GetConfigurationProvider<CustomerRegisterSummarizeAdviceDtoNo1>());
        }
    }
}