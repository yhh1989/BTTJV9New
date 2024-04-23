using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Market;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market
{
    /// <inheritdoc cref="IComplaintAppService" />
    [AbpAuthorize]
    public class ComplaintAppService : MyProjectAppServiceBase, IComplaintAppService
    {
        /// <summary>
        /// 投诉信息仓储
        /// </summary>
        private readonly IRepository<ComplaintInformation, Guid> _complaintInformationRepository;

        /// <summary>
        /// 体检人预约仓储
        /// </summary>
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegisterRepository;

        /// <summary>
        /// 体检人仓储
        /// </summary>
        private readonly IRepository<TjlCustomer, Guid> _customerRepository;

        /// <inheritdoc />
        public ComplaintAppService(IRepository<ComplaintInformation, Guid> complaintInformationRepository, IRepository<TjlCustomerReg, Guid> customerRegisterRepository, IRepository<TjlCustomer, Guid> customerRepository)
        {
            _complaintInformationRepository = complaintInformationRepository;
            _customerRegisterRepository = customerRegisterRepository;
            _customerRepository = customerRepository;
        }

        /// <inheritdoc />
        public async Task<List<ComplaintInformationDto>> QueryComplaintInformationCollection(QueryComplaintConditionInput input)
        {
            var query = _complaintInformationRepository.GetAll().AsNoTracking();
            if (!string.IsNullOrWhiteSpace(input.CustomerName))
            {
                query = query.Where(r => r.Customer.Name == input.CustomerName);
            }

            if (!string.IsNullOrWhiteSpace(input.CustomerMobile))
            {
                query = query.Where(r => r.Customer.Mobile == input.CustomerMobile);
            }

            if (!string.IsNullOrWhiteSpace(input.IdCard))
            {
                query = query.Where(r => r.Customer.IDCardNo == input.IdCard);
            }

            if (!string.IsNullOrWhiteSpace(input.CustomerRegisterCode))
            {
                query = query.Where(r => r.CustomerRegister.CustomerBM == input.CustomerRegisterCode);
            }

            if (input.ComplainTimeStart.HasValue)
            {
                var start = input.ComplainTimeStart.Value.Date;
                query = query.Where(r => r.ComplainTime >= start);
            }

            if (input.ComplainTimeEnd.HasValue)
            {
                var end = input.ComplainTimeEnd.Value.Date.AddDays(1);
                query = query.Where(r => r.ComplainTime < end);
            }

            if (input.ComplainUserId.HasValue)
            {
                query = query.Where(r => r.ComplainUserId == input.ComplainUserId);
            }

            if (input.HandlerId.HasValue)
            {
                query = query.Where(r => r.HandlerId == input.HandlerId.Value);
            }

            if (input.ProcessState != null && input.ProcessState.Count != 0)
            {
                query = query.Where(r => input.ProcessState.Contains(r.ProcessState));
            }

            return await query.OrderBy(r=>r.ExigencyLevel).ThenBy(r=>r.ProcessState).ThenBy(r=>r.CreationTime).ProjectToListAsync<ComplaintInformationDto>(
                GetConfigurationProvider<ComplaintInformationDto>());
        }

        /// <inheritdoc />
        public async Task<ComplaintInformationDto> InsertOrUpdateComplaintInformation(UpdateComplaintInformationDto input)
        {
            if (input.Id == Guid.Empty)
            {
                input.Id = Guid.NewGuid();
                var insert = ObjectMapper.Map<ComplaintInformation>(input);
                var insertEntity = await _complaintInformationRepository.InsertAsync(insert);
                await _complaintInformationRepository.EnsurePropertyLoadedAsync(insertEntity, r => r.Customer);
                await _complaintInformationRepository.EnsurePropertyLoadedAsync(insertEntity, r => r.CustomerRegister);
                if (insertEntity.CompanyInformationId.HasValue)
                {
                    await _complaintInformationRepository.EnsurePropertyLoadedAsync(insertEntity, r => r.CompanyInformation);
                }
                if (insertEntity.CompanyRegisterId.HasValue)
                {
                    await _complaintInformationRepository.EnsurePropertyLoadedAsync(insertEntity, r => r.CompanyRegister);
                }
                if (insertEntity.CompanyRegisterTeamId.HasValue)
                {
                    await _complaintInformationRepository.EnsurePropertyLoadedAsync(insertEntity, r => r.CompanyRegisterTeamInformation);
                }
                return ObjectMapper.Map<ComplaintInformationDto>(insertEntity);
            }

            var update = await _complaintInformationRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, update);
            var updateEntity = await _complaintInformationRepository.UpdateAsync(update);
            await _complaintInformationRepository.EnsurePropertyLoadedAsync(updateEntity, r => r.Customer);
            await _complaintInformationRepository.EnsurePropertyLoadedAsync(updateEntity, r => r.CustomerRegister);
            if (updateEntity.CompanyInformationId.HasValue)
            {
                await _complaintInformationRepository.EnsurePropertyLoadedAsync(updateEntity, r => r.CompanyInformation);
            }
            if (updateEntity.CompanyRegisterId.HasValue)
            {
                await _complaintInformationRepository.EnsurePropertyLoadedAsync(updateEntity, r => r.CompanyRegister);
            }
            if (updateEntity.CompanyRegisterTeamId.HasValue)
            {
                await _complaintInformationRepository.EnsurePropertyLoadedAsync(updateEntity, r => r.CompanyRegisterTeamInformation);
            }
            return ObjectMapper.Map<ComplaintInformationDto>(updateEntity);
        }

        /// <inheritdoc />
        public async Task<ComplaintInformationDto> QueryEmptyComplaintInformation(QueryComplaintConditionInput input)
        {
            if (string.IsNullOrWhiteSpace(input.CustomerMobile + input.IdCard + input.CustomerRegisterCode))
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(input.CustomerRegisterCode))
            {
                var customerRegister =
                    await _customerRegisterRepository.FirstOrDefaultAsync(r =>
                        r.CustomerBM == input.CustomerRegisterCode);
                if (customerRegister != null)
                {
                    var output = new ComplaintInformationDto();
                    output.CustomerRegister = ObjectMapper.Map<CustomerRegisterDtoForComplaint>(customerRegister);
                    output.Customer = ObjectMapper.Map<CustomerDtoForComplaint>(customerRegister.Customer);
                    if (customerRegister.ClientTeamInfoId.HasValue)
                    {
                        output.CompanyRegisterTeamInformation =
                            ObjectMapper.Map<CompanyRegisterTeamDtoForComplaint>(customerRegister.ClientTeamInfo);
                        output.CompanyRegister = ObjectMapper.Map<CompanyRegisterDtoForComplaint>(customerRegister.ClientReg);
                        output.CompanyInformation = ObjectMapper.Map<CompanyDtoForComplaint>(customerRegister.ClientInfo);
                    }

                    return output;
                }
            }

            if (!string.IsNullOrWhiteSpace(input.IdCard))
            {
                var customer = await _customerRepository.GetAll().OrderByDescending(r => r.CreationTime).FirstOrDefaultAsync(r => r.IDCardNo == input.IdCard);
                if (customer != null)
                {
                    var customerRegister = await _customerRegisterRepository.GetAll().OrderByDescending(r => r.CreationTime).FirstOrDefaultAsync(r =>
                          r.CustomerId == customer.Id);

                    if (customerRegister != null)
                    {
                        var output = new ComplaintInformationDto();
                        output.CustomerRegister = ObjectMapper.Map<CustomerRegisterDtoForComplaint>(customerRegister);
                        output.Customer = ObjectMapper.Map<CustomerDtoForComplaint>(customer);
                        if (customerRegister.ClientTeamInfoId.HasValue)
                        {
                            output.CompanyRegisterTeamInformation =
                                ObjectMapper.Map<CompanyRegisterTeamDtoForComplaint>(customerRegister.ClientInfo);
                            output.CompanyRegister = ObjectMapper.Map<CompanyRegisterDtoForComplaint>(customerRegister.ClientReg);
                            output.CompanyInformation = ObjectMapper.Map<CompanyDtoForComplaint>(customerRegister.ClientInfo);
                        }

                        return output;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(input.CustomerMobile))
            {
                var customer = await _customerRepository.GetAll().OrderByDescending(r => r.CreationTime).FirstOrDefaultAsync(r => r.Mobile == input.CustomerMobile);
                if (customer != null)
                {
                    var customerRegister = await _customerRegisterRepository.GetAll().OrderByDescending(r => r.CreationTime).FirstOrDefaultAsync(r =>
                        r.CustomerId == customer.Id);

                    if (customerRegister != null)
                    {
                        var output = new ComplaintInformationDto();
                        output.CustomerRegister = ObjectMapper.Map<CustomerRegisterDtoForComplaint>(customerRegister);
                        output.Customer = ObjectMapper.Map<CustomerDtoForComplaint>(customer);
                        if (customerRegister.ClientTeamInfoId.HasValue)
                        {
                            output.CompanyRegisterTeamInformation =
                                ObjectMapper.Map<CompanyRegisterTeamDtoForComplaint>(customerRegister.ClientInfo);
                            output.CompanyRegister = ObjectMapper.Map<CompanyRegisterDtoForComplaint>(customerRegister.ClientReg);
                            output.CompanyInformation = ObjectMapper.Map<CompanyDtoForComplaint>(customerRegister.ClientInfo);
                        }

                        return output;
                    }
                }
            }

            return null;
        }
    }
}