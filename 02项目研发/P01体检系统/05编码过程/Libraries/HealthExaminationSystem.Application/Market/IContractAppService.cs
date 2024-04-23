using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market
{
    /// <summary>
    /// 合同应用服务接口
    /// </summary>
    public interface IContractAppService : IApplicationService
    {
        /// <summary>
        /// 查询合同列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ContractInformationFullDto>> QueryContractList(QueryContractConditionInput input);

        /// <summary>
        /// 查询合同类别列表
        /// </summary>
        /// <returns></returns>
        Task<List<ContractCategoryDto>> QueryContractCategoryList(QueryContractCategoryConditionInput input);

        /// <summary>
        /// 获取单位下拉列表
        /// </summary>
        /// <returns></returns>
        Task<List<CompanyDtoForComboBox>> QueryCompanyComboBoxList();

        /// <summary>
        /// 使用标识查询合同类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractCategoryDto> QueryContractCategoryById(EntityDto<Guid> input);

        /// <summary>
        /// 更新或插入合同类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractCategoryDto> UpdateOrInsertContractCategory(ContractCategoryDto input);

        /// <summary>
        /// 使用标识查询合同信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractInformationDto> QueryContractInformationById(EntityDto<Guid> input);

        /// <summary>
        /// 插入或更新合同
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractInformationDto> InsertOrUpdateContract(ContractInformationDto input);

        /// <summary>
        /// 查询单位预约记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CompanyRegisterDtoForComboBox>> QueryCompanyRegister(EntityDto<Guid> input);

        /// <summary>
        /// 查询合同附件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ContractAdjunctDto>> QueryContractAdjunct(EntityDto<Guid> input);

        /// <summary>
        /// 查询合同回款记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ContractProceedsDto>> QueryContractProceeds(EntityDto<Guid> input);

        /// <summary>
        /// 新增合同回款记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractProceedsDto> InsertContractProceeds(ContractProceedsDto input);

        /// <summary>
        /// 删除回款记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractProceedsDto> DeleteContractProceeds(EntityDto<Guid> input);

        /// <summary>
        /// 查询合同开票记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ContractInvoiceDto>> QueryContractInvoiceList(EntityDto<Guid> input);

        /// <summary>
        /// 新增合同开票记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractInvoiceDto> InsertContractInvoice(ContractInvoiceDto input);

        /// <summary>
        /// 删除合同开票记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractInvoiceDto> DeleteContractInvoice(EntityDto<Guid> input);

        /// <summary>
        /// 删除合同信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ContractInformationDto> DeleteContract(EntityDto<Guid> input);
    }
}