using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg
{
    public class CustomerAppService : AppServiceApiProxyBase, ICustomerAppService
    {
        public List<CustomerRegDto> QueryAllCount(QueryAllForNumberInput input)
        {
            return GetResult<QueryAllForNumberInput, List<CustomerRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public PageResultDto<CustomerRegRosterDto> QueryAll(PageInputDto<QueryCustomerRegDto> input)
        {
            return GetResult<PageInputDto<QueryCustomerRegDto>, PageResultDto<CustomerRegRosterDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public AbpUsersDto abpUsersDto()
        {
            return GetResult<AbpUsersDto>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ClientTeamInfoDto> QueryClientTeamInfoes(ClientTeamInfoDto input)
        {
            return GetResult<ClientTeamInfoDto, List<ClientTeamInfoDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TjlCustomerDto> QueryCustomer(CusNameInput input)
        {
            return GetResult<CusNameInput, List<TjlCustomerDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<DisageSumDTO> GetCustomerDisageSum(QueryCustomerRegDto input)
        {
            return GetResult<QueryCustomerRegDto, List<DisageSumDTO>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<QueryCustomerRegDto> QueryCustomerReg(SearchCustomerDto input)
        {
            return GetResult<SearchCustomerDto, List<QueryCustomerRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ClientRegDto> QuereyClientRegInfos(FullClientRegDto dto)
        {
            return GetResult<FullClientRegDto, List<ClientRegDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CusRegListViewDto> QueryRegList(SearchCusRegListDto input)
        {
            return GetResult<SearchCusRegListDto, List<CusRegListViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 批量登记或取消登记
        /// </summary>
        public void BatchReg(BatchRegInputDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询单位预约信息
        /// </summary>
        public List<ClientTeamInfoDto> QueryClientTeamInfos(ClientTeamInfoDto input)
        {
            return GetResult<ClientTeamInfoDto, List<ClientTeamInfoDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 登记
        /// </summary>
        public List<QueryCustomerRegDto> RegCustomer(List<QueryCustomerRegDto> inputDatas)
        {
            return GetResult<List<QueryCustomerRegDto>, List<QueryCustomerRegDto>>(inputDatas, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<QueryCustomerRegDto> RegCustomerNew(QueryCustomerRegDto inputDatas)
        {
            return GetResult<QueryCustomerRegDto, List<QueryCustomerRegDto>>(inputDatas, DynamicUriBuilder.GetAppSettingValue());


        }

        /// <summary>
        /// 取消登记
        /// </summary>
        public void CancelReg(CustomerRegViewDto dto)
        {
            GetResult<CustomerRegViewDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改登记信息的状态
        /// </summary>
        public void EditRegInfoState(EditCustomerRegStatesDto dto)
        {
            GetResult<EditCustomerRegStatesDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CountDto> getupdate(ChargeBM input)
        {
          return   GetResult<ChargeBM, List<CountDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<CustomerRegViewDto> getRepeatCus(ChargeBM input)
        {
            return GetResult<ChargeBM, List<CustomerRegViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerRegEssentialInfoViewDto GetCustomerRegEssentialInfoViewDto(CusNameInput input)
        {
            return GetResult<CusNameInput, CustomerRegEssentialInfoViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerItemGroupBarItemDto> GetLstCustomerItemGroupBarItemDto(CusNameInput input)
        {
            return GetResult<CusNameInput, List<CustomerItemGroupBarItemDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public TjlCustomerDto QueryCustomerByID(CusNameInput input)
        {
            return GetResult<CusNameInput, TjlCustomerDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CustomerRegViewDto GetCustomerRegViewDto(CusNameInput input)
        {
            return GetResult<CusNameInput, CustomerRegViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CustomerRegDto GetCustomerRegDto(CusNameInput cusNameInput)
        {
            return GetResult<CusNameInput, CustomerRegDto>(cusNameInput, DynamicUriBuilder.GetAppSettingValue());
        }
        public CusNameInput GetCustomerRegCountDto(CusNameInput cusNameInput)
        {
            return GetResult<CusNameInput, CusNameInput>(cusNameInput, DynamicUriBuilder.GetAppSettingValue());

        }
        public CusNameInput GetOldCustomerReg(CusNameInput cusNameInput)
        {
            return GetResult<CusNameInput, CusNameInput>(cusNameInput, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 登记窗体展示 查询登记数
        /// </summary>
        public ViewQueryRegNumbersDto QueryRegNumbers()
        {
            return GetResult<ViewQueryRegNumbersDto>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CustomerRegRosterDto> GetAll(QueryCustomerRegDto input)
        {
            return GetResult<QueryCustomerRegDto, List<CustomerRegRosterDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public int UpdateTimes(GuideUpdateCustomerRegDto input)
        {
            return GetResult<GuideUpdateCustomerRegDto, int>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public int GetNumber(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, int>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<QueryAllForNumberDto> QueryAllForNumber(QueryAllForNumberInput input)
        {
            return GetResult<QueryAllForNumberInput, List<QueryAllForNumberDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<QueryAllForPersonDto> QueryAllForPerson(QueryAllForNumberInput input)
        {
            return GetResult<QueryAllForNumberInput, List<QueryAllForPersonDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<PersonTypeDto> GetAllPersonTypes()
        {
            return GetResult<List<PersonTypeDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OneAddQuestionsDto> getOneAddXQuestionnaires()
        {
            return GetResult<List<OneAddQuestionsDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CustomerQuestionDto> getCustomerQuestionDtos(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CustomerQuestionDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerAddPackageDto> getCustomerAddPackage(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CustomerAddPackageDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerAddPackageItemDto> getCustomerAddPackageItem(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CustomerAddPackageItemDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SimpleItemSuitDto> getSearchItemSuitDto(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<SimpleItemSuitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询科室登记人员
        /// </summary>
        public List<DepartMentCustomerRegOutPut> QueryDepartMentCustomerReg(QueryDepartCustomerRegDto input)
        {
            return GetResult<QueryDepartCustomerRegDto, List<DepartMentCustomerRegOutPut>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据ID查体检人收费状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerRegCostDto GetCustomerRegCost(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, CustomerRegCostDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据身份证获取体检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<QueryCustomerRegDto> GetCustomerIDCard(SearchCustomerDto input)
        {
            return GetResult<SearchCustomerDto, List<QueryCustomerRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public HisCusInfoDto geHisvard(InCarNumDto input)
        {
            return GetResult<InCarNumDto, HisCusInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public InCarNumDto SaveHisInfo(InCarNumDto input)
        {
            return GetResult<InCarNumDto, InCarNumDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        //宜兴物价同步
        public List<HisPriceDtos> GetYXHis(InCarNumPriceDto input)
        {
            return GetResult<InCarNumPriceDto, List<HisPriceDtos>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutApplicationDto InsertSFCharg(TJSQDto input)
        {
            return GetResult<TJSQDto, OutApplicationDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public OutErrDto DelApply(TJSQDto input)
        {
            return GetResult<TJSQDto, OutErrDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<HisPriceDtos> InsertYXHis(List<HisPriceDtos> input)
        {
            return GetResult<List<HisPriceDtos>, List<HisPriceDtos>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<OutApplicationDto> getapplication(TJSQDto input)
        {
            return GetResult<TJSQDto, List<OutApplicationDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public CusPayMoneyViewDto GetCusPayMoney(CusNameInput input)
        {
            return GetResult<CusNameInput, CusPayMoneyViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutResult OutDataSouce(GsearchCustomerDto input)
        {
            return GetResult<GsearchCustomerDto, OutResult>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutResult UpdateLisReport(GsearchCustomerDto input)
        {
            return GetResult<GsearchCustomerDto, OutResult>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutResult OutTJData(GsearchCustomerDto input)
        {
            return GetResult<GsearchCustomerDto, OutResult>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CardTypeNameDto getSuitbyCardNum(CusCardDto input)
        {
            return GetResult<CusCardDto, CardTypeNameDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public StartupAlertCusRegDto GetStartupData(StartupAlertGetDto input)
        {
            return GetResult<StartupAlertGetDto, StartupAlertCusRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ChargeBM> NoPriceGroup(List<ChargeBM> input)
        {
            return GetResult<List<ChargeBM>, List<ChargeBM>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ChargeBM> ErrPriceGroup(List<GroupPriceDto> input)
        {
            return GetResult<List<GroupPriceDto>, List<ChargeBM>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<QueryAllForNumberDto> QueryAllForNumbers(QueryAllForNumberInput input)
        {
            return GetResult<QueryAllForNumberInput, List<QueryAllForNumberDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<AbpTenantsDto> GetAbpTenants()
        {
            return GetResult<List<AbpTenantsDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SickNessDto> GetDiseaseAppServices(SickNessDto input)
        {
            return GetResult<SickNessDto, List<SickNessDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ReportHSCusDto> getClientCusHS(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ReportHSCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ImportDataDto> GetImportDatas(SearchImportData input)
        {
            return GetResult<SearchImportData, List<ImportDataDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<BdSatticDto> GetBdCz()
        {
            return GetResult<List<BdSatticDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public OutOldRegDto QueryOldCustomer(SearchOldRegDto input)
        {
            return GetResult<SearchOldRegDto, OutOldRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public QueryCustomerDto GetCusInfoByIDCard(SearchOldRegDto input)
        {
            return GetResult<SearchOldRegDto, QueryCustomerDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改上传状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void UpCusUploadState(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<BusiCusRegDto> SearchBusiCus(InSearchBusiCusDto input)
        {
            return GetResult<InSearchBusiCusDto, List<BusiCusRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<BusiRSDto> SearchBusiCount(InSearchBusiCusDto input)
        {
            return GetResult<InSearchBusiCusDto, List<BusiRSDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<BusiCusGRDtocs> GRBusiCount(InSearchBusiCusDto input)
        {
            return GetResult<InSearchBusiCusDto, List<BusiCusGRDtocs>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public QueryCustomerRegDto SaveSupplementary(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, QueryCustomerRegDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<OccQueCusDto> GetOccQueCus(SearchOccQueCus input)
        {
            return GetResult<SearchOccQueCus, List<OccQueCusDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<CustomerOCCPicDto> getOccQueCusList(OccQueCusDto input)
        {
            return GetResult<OccQueCusDto, List<CustomerOCCPicDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void SaveOccQueCus(OccQueCusDto input)
        {
            GetResult<OccQueCusDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void SaveOccQueCusList(List<OccQueCusDto> input)
        {
              GetResult<List<OccQueCusDto> >(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void   deletOccQue(OccQueCusDto input)
        {
            GetResult<OccQueCusDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void deletOccQueByID(OccQueCusDto input)
        {
            GetResult<OccQueCusDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void ClearOccQueCus(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CusNameInput getCusreg(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, CusNameInput>(input, DynamicUriBuilder.GetAppSettingValue());

        }

     
        public dynamic getFace(dynamic input)
        {
            return GetResult<dynamic, dynamic>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public dynamic UpFace(dynamic input)
        {
            return GetResult<dynamic, dynamic>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }

}
