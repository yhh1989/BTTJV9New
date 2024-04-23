using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg
{
    public interface ICustomerAppService
#if !Proxy
        : IApplicationService
#endif
    {
        List<QueryAllForNumberDto> QueryAllForNumbers(QueryAllForNumberInput input);
        AbpUsersDto abpUsersDto();
        /// <summary>
        /// 获取所有体检人信息
        /// </summary>
        List<TjlCustomerDto> QueryCustomer(CusNameInput input);

        /// <summary>
        /// 获取体检预约信息
        /// </summary>
        List<QueryCustomerRegDto> QueryCustomerReg(SearchCustomerDto input);

        /// <summary>
        /// 查询单位预约分组信息
        /// </summary>
        List<ClientTeamInfoDto> QueryClientTeamInfos(ClientTeamInfoDto input);

        //获取体检人信息
        TjlCustomerDto QueryCustomerByID(CusNameInput input);
        /// <summary>
        /// 查询登记列表
        /// </summary>
        List<CusRegListViewDto> QueryRegList(SearchCusRegListDto input);
        /// <summary>
        /// 查询单位预约信息
        /// </summary>
        /// <returns></returns>
        List<ClientRegDto> QuereyClientRegInfos(FullClientRegDto dto);
        /// <summary>
        /// 批量登记或取消登记
        /// </summary>
        void BatchReg(BatchRegInputDto input);
        /// <summary>
        /// 登记
        /// </summary>
        List<QueryCustomerRegDto> RegCustomer(List<QueryCustomerRegDto> inputDatas);

        List<QueryCustomerRegDto> RegCustomerNew(QueryCustomerRegDto inputDatas);
        /// <summary>
        /// 取消登记
        /// </summary>
        void CancelReg(CustomerRegViewDto dto);
        /// <summary>
        /// 登记窗体展示 查询登记数
        /// </summary>
        ViewQueryRegNumbersDto QueryRegNumbers();
        /// <summary>
        /// 修改登记信息的状态
        /// </summary>
        void EditRegInfoState(EditCustomerRegStatesDto dto);

        List<CountDto> getupdate(ChargeBM input);
        List<CustomerRegViewDto> getRepeatCus(ChargeBM input);
        /// <summary>
        /// 获取所有体检预约信息
        /// </summary>
        PageResultDto<CustomerRegRosterDto> QueryAll(PageInputDto<QueryCustomerRegDto> input);
        List<CustomerRegRosterDto> GetAll(QueryCustomerRegDto input); 
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="input">TjlCustomerReg.id</param>
        /// <returns>CustomerRegEssentialInfoViewDto </returns>
        CustomerRegEssentialInfoViewDto GetCustomerRegEssentialInfoViewDto(CusNameInput input);

        /// <summary>
        /// 获取所有检查组合信息
        /// </summary>
        /// <param name="input">TjlCustomerReg.id</param>
        List<CustomerItemGroupBarItemDto> GetLstCustomerItemGroupBarItemDto(CusNameInput input);

        CustomerRegViewDto GetCustomerRegViewDto(CusNameInput input);

        /// <summary>
        /// 获取体检人信息
        /// </summary>
        /// <param name="cusNameInput">customerreg.id</param>
        /// <returns></returns>
        CustomerRegDto GetCustomerRegDto(CusNameInput cusNameInput);
        /// <summary>
        /// 体检次数
        /// </summary>
        /// <param name="cusNameInput"></param>
        /// <returns></returns>
        CusNameInput GetCustomerRegCountDto(CusNameInput cusNameInput);
        CusNameInput GetOldCustomerReg(CusNameInput cusNameInput);
        /// <summary>
        /// 导引单次数更新
        /// </summary>
        /// <param name="input">input.TjlClientReg_Id</param>
        /// <returns></returns>
        int UpdateTimes(GuideUpdateCustomerRegDto input);
        /// <summary>
        /// 获取组合下用户数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int GetNumber(EntityDto<Guid> input);

        /// <summary>
        /// 人数统计用查询
        /// </summary>
        /// <param name="input"></param>
        List<QueryAllForNumberDto> QueryAllForNumber(QueryAllForNumberInput input);
        /// <summary>
        /// 人数统计用查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<QueryAllForPersonDto> QueryAllForPerson(QueryAllForNumberInput input);
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        List<PersonTypeDto> GetAllPersonTypes();
        /// <summary>
        /// 获取1+X问卷
        /// </summary>
        /// <returns></returns>
        List<OneAddQuestionsDto> getOneAddXQuestionnaires();

        /// <summary>
        /// 获取个人问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        List<CustomerQuestionDto> getCustomerQuestionDtos(EntityDto<Guid> input);

        /// <summary>
        /// 获取个人加项包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CustomerAddPackageDto> getCustomerAddPackage(EntityDto<Guid> input);

        /// <summary>
        /// 获取个人加项包项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CustomerAddPackageItemDto> getCustomerAddPackageItem(EntityDto<Guid> input);
        /// <summary>
        /// 获取个人加项包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SimpleItemSuitDto> getSearchItemSuitDto(EntityDto<Guid> input);
        /// <summary>
        /// 查询科室登记人员
        /// </summary>
        List<DepartMentCustomerRegOutPut> QueryDepartMentCustomerReg(QueryDepartCustomerRegDto input);
        /// <summary>
        /// 根据ID查体检人收费状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerRegCostDto GetCustomerRegCost(EntityDto<Guid> input);
        /// <summary>
        /// 根据身份证获取体检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<QueryCustomerRegDto> GetCustomerIDCard(SearchCustomerDto input);
        /// <summary>
        /// 获取所有疾病列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<DisageSumDTO> GetCustomerDisageSum(QueryCustomerRegDto input);
        HisCusInfoDto geHisvard(InCarNumDto input);
        InCarNumDto SaveHisInfo(InCarNumDto input);
        List<HisPriceDtos> GetYXHis(InCarNumPriceDto input);
        OutApplicationDto InsertSFCharg(TJSQDto input);
        /// <summary>
        /// 删除申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutErrDto DelApply(TJSQDto input);
        List<HisPriceDtos> InsertYXHis(List<HisPriceDtos> input);
   
        List<OutApplicationDto> getapplication(TJSQDto input);
        CusPayMoneyViewDto GetCusPayMoney(CusNameInput cusNameInput);
        OutResult OutDataSouce(GsearchCustomerDto input);
        OutResult UpdateLisReport(GsearchCustomerDto input);
        OutResult OutTJData(GsearchCustomerDto input);
        List<ClientTeamInfoDto> QueryClientTeamInfoes(ClientTeamInfoDto input);
        CardTypeNameDto getSuitbyCardNum(CusCardDto input);
        StartupAlertCusRegDto GetStartupData(StartupAlertGetDto input);
        List<CustomerRegDto> QueryAllCount(QueryAllForNumberInput input);
        List<ChargeBM> NoPriceGroup(List<ChargeBM> input);

        List<ChargeBM> ErrPriceGroup(List<GroupPriceDto> input);
        List<AbpTenantsDto> GetAbpTenants();
        List<SickNessDto> GetDiseaseAppServices(SickNessDto input);
        List<ReportHSCusDto> getClientCusHS(EntityDto<Guid> input);
        List<ImportDataDto> GetImportDatas(SearchImportData input);
        List<BdSatticDto> GetBdCz();
        OutOldRegDto QueryOldCustomer(SearchOldRegDto input);
        QueryCustomerDto GetCusInfoByIDCard(SearchOldRegDto input);
        void UpCusUploadState(EntityDto<Guid> input);
        /// <summary>
        /// 订单进展
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<BusiCusRegDto> SearchBusiCus(InSearchBusiCusDto input);
       /// <summary>
       /// 商务订单
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        List<BusiRSDto> SearchBusiCount(InSearchBusiCusDto input);
        /// <summary>
        /// 商务订单个人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<BusiCusGRDtocs> GRBusiCount(InSearchBusiCusDto input);
        /// <summary>
        /// 保存补检人
        /// </summary>
        /// <param name="input"></param>
        QueryCustomerRegDto SaveSupplementary(EntityDto<Guid> input);
        /// <summary>
        /// 获取职业史扫描人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<OccQueCusDto> GetOccQueCus(SearchOccQueCus input);
        /// <summary>
        /// 保存职业史扫描
        /// </summary>
        /// <param name="input"></param>
        void SaveOccQueCus(OccQueCusDto input);
        List<CustomerOCCPicDto> getOccQueCusList(OccQueCusDto input);
        void SaveOccQueCusList(List<OccQueCusDto> input);
        void deletOccQue(OccQueCusDto input);
        void deletOccQueByID(OccQueCusDto input);

        /// <summary>
        /// 清除职业史扫描
        /// </summary>
        /// <param name="input"></param>
        void ClearOccQueCus(EntityDto<Guid> input);
        /// <summary>
        /// 根据id查询体检号信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CusNameInput getCusreg(EntityDto<Guid> input);

        dynamic getFace(dynamic input);

        dynamic UpFace(dynamic input);
    }
}