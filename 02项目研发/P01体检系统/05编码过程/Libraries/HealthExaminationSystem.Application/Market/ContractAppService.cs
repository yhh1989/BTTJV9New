using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Market;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market
{
    /// <inheritdoc cref="IContractAppService" />
    [AbpAuthorize]
    public class ContractAppService : MyProjectAppServiceBase, IContractAppService
    {
        /// <summary>
        /// 合同信息仓储
        /// </summary>
        private readonly IRepository<ContractInformation, Guid> _contractRepository;

        /// <summary>
        /// 合同类别仓储
        /// </summary>
        private readonly IRepository<ContractCategory, Guid> _contractCategoryRepository;

        /// <summary>
        /// 单位仓储
        /// </summary>
        private readonly IRepository<TjlClientInfo, Guid> _companyRepository;

        /// <summary>
        /// 单位预约仓储
        /// </summary>
        private readonly IRepository<TjlClientReg, Guid> _companyRegisterRepository;

        /// <summary>
        /// 合同附件仓储
        /// </summary>
        private readonly IRepository<ContractAdjunct, Guid> _contractAdjunctRepository;

        /// <summary>
        /// 合同回款记录仓储
        /// </summary>
        private readonly IRepository<ContractProceeds, Guid> _contractProceedsRepository;

        /// <summary>
        /// 合同开票记录
        /// </summary>
        private readonly IRepository<ContractInvoice, Guid> _contractInvoiceRepository;

        /// <inheritdoc />
        public ContractAppService(IRepository<ContractInformation, Guid> contractRepository,
            IRepository<ContractCategory, Guid> contractCategoryRepository,
            IRepository<TjlClientInfo, Guid> companyRepository,
            IRepository<TjlClientReg, Guid> companyRegisterRepository,
            IRepository<ContractAdjunct, Guid> contractAdjunctRepository,
            IRepository<ContractProceeds, Guid> contractProceedsRepository,
            IRepository<ContractInvoice, Guid> contractInvoiceRepository)
        {
            _contractRepository = contractRepository;
            _contractCategoryRepository = contractCategoryRepository;
            _companyRepository = companyRepository;
            _companyRegisterRepository = companyRegisterRepository;
            _contractAdjunctRepository = contractAdjunctRepository;
            _contractProceedsRepository = contractProceedsRepository;
            _contractInvoiceRepository = contractInvoiceRepository;
        }

        /// <inheritdoc />
        public async Task<List<ContractInformationFullDto>> QueryContractList(QueryContractConditionInput input)
        {
            var query = _contractRepository.GetAll().AsNoTracking();
            if (!string.IsNullOrWhiteSpace(input.Number))
            {
                query = query.Where(r => r.Number == input.Number);
            }

            if (input.CompanyId.HasValue)
            {
                query = query.Where(r => r.CompanyId == input.CompanyId);
            }

            if (!string.IsNullOrWhiteSpace(input.Signatory))
            {
                query = query.Where(r => r.Signatory.Contains(input.Signatory));
            }

            if (input.SubmitTimeStart.HasValue)
            {
                var start = input.SubmitTimeStart.Value.Date;
                query = query.Where(r => r.SubmitTime >= start);
            }

            if (input.SubmitTimeEnd.HasValue)
            {
                var end = input.SubmitTimeEnd.Value.AddDays(1).Date;
                query = query.Where(r => r.SubmitTime < end);
            }

            if (input.ValidTimeStart.HasValue)
            {
                var start = input.ValidTimeStart.Value.Date;
                query = query.Where(r => r.ValidTime >= start);
            }

            if (input.ValidTimeEnd.HasValue)
            {
                var end = input.ValidTimeEnd.Value.AddDays(1).Date;
                query = query.Where(r => r.ValidTime < end);
            }

            if (input.CreatorUserId.HasValue)
            {
                query = query.Where(r => r.CreatorUserId == input.CreatorUserId.Value);
            }

            query = query.OrderByDescending(r => r.CreationTime);
            return await query.ProjectToListAsync<ContractInformationFullDto>(
                GetConfigurationProvider<ContractInformationFullDto>());
        }

        /// <inheritdoc />
        public async Task<List<ContractCategoryDto>> QueryContractCategoryList(QueryContractCategoryConditionInput input)
        {
            var query = _contractCategoryRepository.GetAll().AsNoTracking();
            if (input.IsActive)
            {
                query = query.Where(r => r.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(r => r.Name == input.Name);
            }

            query = query.OrderBy(r => r.Name);
            return await query.ProjectToListAsync<ContractCategoryDto>(GetConfigurationProvider<ContractCategoryDto>());
        }

        /// <inheritdoc />
        public async Task<List<CompanyDtoForComboBox>> QueryCompanyComboBoxList()
        {
            var query = _companyRepository.GetAll().AsNoTracking();
            query = query.OrderByDescending(r => r.CreationTime);
            return await query.ProjectToListAsync<CompanyDtoForComboBox>(
                GetConfigurationProvider<CompanyDtoForComboBox>());
        }

        /// <inheritdoc />
        public async Task<ContractCategoryDto> QueryContractCategoryById(EntityDto<Guid> input)
        {
            var contractCategory = await _contractCategoryRepository.GetAsync(input.Id);
            return ObjectMapper.Map<ContractCategoryDto>(contractCategory);
        }

        /// <inheritdoc />
        public async Task<ContractCategoryDto> UpdateOrInsertContractCategory(ContractCategoryDto input)
        {
            if (input.Id == Guid.Empty)
            {
                if (await _contractCategoryRepository.GetAll().AnyAsync(r => r.Name == input.Name))
                {
                    throw new FieldVerifyException("名称重复", "合同分类名称已经存在");
                }
                input.Id = Guid.NewGuid();
                var insertEntity = ObjectMapper.Map<ContractCategory>(input);
                var entity = await _contractCategoryRepository.InsertAsync(insertEntity);
                return ObjectMapper.Map<ContractCategoryDto>(entity);
            }

            if (await _contractCategoryRepository.GetAll().AnyAsync(r => r.Name == input.Name && r.Id != input.Id))
            {
                throw new FieldVerifyException("名称重复", "合同分类名称已经存在");
            }
            var updateEntity = await _contractCategoryRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, updateEntity);
            var entity1 = await _contractCategoryRepository.UpdateAsync(updateEntity);
            return ObjectMapper.Map<ContractCategoryDto>(entity1);
        }

        /// <inheritdoc />
        public async Task<ContractInformationDto> QueryContractInformationById(EntityDto<Guid> input)
        {
            var entity = await _contractRepository.GetAsync(input.Id);
            return ObjectMapper.Map<ContractInformationDto>(entity);
        }

        /// <inheritdoc />
        public async Task<ContractInformationDto> InsertOrUpdateContract(ContractInformationDto input)
        {
            if (input.Id == Guid.Empty)
            {
                if (await _contractRepository.GetAll().AnyAsync(r => r.Number == input.Number))
                {
                    throw new FieldVerifyException("编号重复", "合同编号已经存在");
                }

                input.Id = Guid.NewGuid();
                var insertEntity = ObjectMapper.Map<ContractInformation>(input);
                var entity = await _contractRepository.InsertAsync(insertEntity);
                return ObjectMapper.Map<ContractInformationDto>(entity);
            }

            if (await _contractRepository.GetAll().AnyAsync(r => r.Number == input.Number && r.Id != input.Id))
            {
                throw new FieldVerifyException("编号重复", "合同编号已经存在");
            }

            var updateEntity = await _contractRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, updateEntity);
            var entity1 = await _contractRepository.UpdateAsync(updateEntity);
            return ObjectMapper.Map<ContractInformationDto>(entity1);
        }

        /// <inheritdoc />
        public async Task<List<CompanyRegisterDtoForComboBox>> QueryCompanyRegister(EntityDto<Guid> input)
        {
            var query = _companyRegisterRepository.GetAll().AsNoTracking();
            query = query.Where(r => r.ClientInfoId == input.Id);
            query = query.OrderByDescending(r => r.CreationTime);
            return await query.ProjectToListAsync<CompanyRegisterDtoForComboBox>(
                GetConfigurationProvider<CompanyRegisterDtoForComboBox>());
        }

        /// <inheritdoc />
        public async Task<List<ContractAdjunctDto>> QueryContractAdjunct(EntityDto<Guid> input)
        {
            var query = _contractAdjunctRepository.GetAll().AsNoTracking();
            query = query.Where(r => r.ContractId == input.Id);
            query = query.OrderByDescending(r => r.CreationTime);
            return await query.ProjectToListAsync<ContractAdjunctDto>(GetConfigurationProvider<ContractAdjunctDto>());
        }

        /// <inheritdoc />
        public async Task<List<ContractProceedsDto>> QueryContractProceeds(EntityDto<Guid> input)
        {
            var query = _contractProceedsRepository.GetAll().AsNoTracking();
            query = query.Where(r => r.ContractId == input.Id);
            query = query.OrderByDescending(r => r.Date);
            return await query.ProjectToListAsync<ContractProceedsDto>(GetConfigurationProvider<ContractProceedsDto>());
        }

        /// <inheritdoc />
        public async Task<ContractProceedsDto> InsertContractProceeds(ContractProceedsDto input)
        {
            input.Id = Guid.NewGuid();
            var insertEntity = ObjectMapper.Map<ContractProceeds>(input);
            var entity = await _contractProceedsRepository.InsertAsync(insertEntity);
            return ObjectMapper.Map<ContractProceedsDto>(entity);
        }

        /// <inheritdoc />
        public async Task<ContractProceedsDto> DeleteContractProceeds(EntityDto<Guid> input)
        {
            var entity = await _contractProceedsRepository.GetAsync(input.Id);
            await _contractProceedsRepository.HardDeleteAsync(entity);
            return ObjectMapper.Map<ContractProceedsDto>(entity);
        }

        /// <inheritdoc />
        public async Task<List<ContractInvoiceDto>> QueryContractInvoiceList(EntityDto<Guid> input)
        {
            var query = _contractInvoiceRepository.GetAll().AsNoTracking();
            query = query.Where(r => r.ContractId == input.Id);
            query = query.OrderByDescending(r => r.Date);
            return await query.ProjectToListAsync<ContractInvoiceDto>(GetConfigurationProvider<ContractInvoiceDto>());
        }

        /// <inheritdoc />
        public async Task<ContractInvoiceDto> InsertContractInvoice(ContractInvoiceDto input)
        {
            input.Id = Guid.NewGuid();
            var insertEntity = ObjectMapper.Map<ContractInvoice>(input);
            var entity = await _contractInvoiceRepository.InsertAsync(insertEntity);
            return ObjectMapper.Map<ContractInvoiceDto>(entity);
        }

        /// <inheritdoc />
        public async Task<ContractInvoiceDto> DeleteContractInvoice(EntityDto<Guid> input)
        {
            var entity = await _contractInvoiceRepository.GetAsync(input.Id);
            await _contractInvoiceRepository.HardDeleteAsync(entity);
            return ObjectMapper.Map<ContractInvoiceDto>(entity);
        }

        /// <inheritdoc />
        public async Task<ContractInformationDto> DeleteContract(EntityDto<Guid> input)
        {
            var entity = await _contractRepository.GetAsync(input.Id);
            await _contractRepository.DeleteAsync(entity);
            return ObjectMapper.Map<ContractInformationDto>(entity);
        }
    }
}