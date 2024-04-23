using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExamination.Drivers.Models.HisInterface;
using Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge
{
    public interface IChargeAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 输入折扣率,根据用户最大折扣率和组合最大折扣率计算价格
        /// </summary>
        /// <returns></returns>
        List<GroupMoneyDto> MinlstGroupZKL(SeachChargrDto dto);

        /// <summary>
        /// 计算项目价格,处理换项等情况
        /// </summary>
        /// <returns></returns>
        List<GroupMoneyDto> CusGroupMoney(SeachChargrDto dto);
        /// <summary>
        /// 获取自费详情
        /// </summary>
        ChargeInfoDto Get(ChargeBM input);

        ICollection<MReceiptInfoPerDto> GetReceipt(ChargeCusRegDto input);

        ICollection<ChargeCusInfoDto> ChargeCusInfo(ChargQueryCusDto input);

        List<ChargeTypeDto> ChargeType(ChargeBM Type);

        Guid InsertReceiptInfoDto(CreateReceiptInfoDto input);
        ReceiptInfoPerViewDto GetReceiptDetailed(EntityDto<Guid> input);
       
        List<WebChargeShow> getWebCharge(InSearchBusiCusDto input);
        List<ClientPayCusLisViewDto> GetClientCusList(EntityDto<Guid> input);
        List<ChargeTeamMoneyViewDto> GetClientTeamList(EntityDto<Guid> input);
        MReceiptOrCustomerViewDto GetReceiptViewDto(EntityDto<Guid> input);
        List<MReceiptInfoPerViewDto> GetReceiptOrCustomer(EntityDto<Guid> input);
        decimal MinMoney(SeachChargrDto dto);
        decimal MinGroupZKL(SeachChargrDto dto);
        List<MReceiptClientDto> MInvoiceRecorView(EntityDto<Guid> bM);
        void InsertClientReceiptInfoDto(CreateReceiptInfoDto input);
        void InsertClientReceiptDto(CreateReceiptInfoDto input);
        ICollection<GroupMoneyDto> MinCusGroupMoney(SeachChargrDto dto);
        List<MReceiptInfoDto> GetInvalidReceipt(SearchInvoiceDto searchInvoice);

        MReceiptInfoDto GetInvalidReceiptById(EntityDto<Guid> input);
        List<MReceiptInfoDetailedViewDto> getReceiptInfoDetaileds(EntityDto<Guid> bM);
        /// <summary>
        /// 获取单位可以开发票的结算信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SettlementInfoViewDto> GetCompenyInfo(EntityDto<Guid> input);
        /// <summary>
        /// 打印发票(更新发票号)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MInvoiceRecordDto PrintInvoice(MInvoiceRecordDto input);
        /// <summary>
        /// 指定体检人预约组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         
        List<ChargeGroupsDto> getChargeGroups(List<EntityDto<Guid>> input);
        /// <summary>
        /// 更新预约及组合状态
        /// </summary>
        /// <param name="ChargeGroupLis"></param>
        void updateClientChargeState(UpdateChargeStateDto ChargeGroupLis);
        /// <summary>
        /// 获取组合的所以人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        List<ClientPayCusLisViewDto> GetTeamCusList(List<EntityDto<Guid>> input);
        /// <summary>
        /// 修改封帐状态
        /// </summary>
        /// <param name="input"></param>
        ClientRegDto UpZFState(ChargeBM input);
        /// <summary>
        /// 查询封账状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int GetZFState(EntityDto<Guid> input);
        /// <summary>
        /// 更新个人收费费用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CusPayMoneyViewDto UpCusMoney(SearchPayMoneyDto input);
        /// <summary>
        /// 获取费用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CusPayMoneyViewDto GetCusMoney(EntityDto<Guid> input);
        /// <summary>
        /// 根据结算表Id查出体检人项目缴费信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<TjlCustomerItemGroupDto> GetCustomerItemGroupList(EntityDto<Guid> input);
        /// <summary>
        /// 作废发票1 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MReceiptInfoDto InsertInvalidReceiptInfoDto(GuIdDto input);
        /// <summary>
        /// 保存抹零项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TjlCustomerItemGroupDto InsertMLGroup(SearchMLGroupDto input);
        /// <summary>
        /// 根据ID获取支付方式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ChargeTypeDto ChargeTypeByID(EntityDto<Guid> input);
        /// <summary>
        /// 判断该收费记录所管理项目是否已检
        /// </summary>
        /// <returns></returns>
        bool SFGroupCheck(EntityDto<Guid> input);
        /// <summary>
        /// 获取收费类别明细
        /// </summary>
        /// <returns></returns>
        List<ChargeGroupsDto> getReceiptInfoGroups(ChargeBM input);
        /// <summary>
        /// 单位明细
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<SearchKSGZLStatisticsDto> KSGZLStatistics(QueryClass query);
        /// <summary>
        /// 获取个人收费组合信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ChargeGroupsDto> GetCusGroups(ChargeBM input);
        /// <summary>
        /// 部分退费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<MReceiptInfoDto> GetReceiptInfoBack(List<ChargeGroupsDto> input);
        /// <summary>
        /// 获取作废记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MReceiptInfoDto GetInvalid(EntityDto<Guid> input);

        // List<InterfaceItemComparisonDto> GetItemlst(SearchInterIFaceItemComparisonDto input);
        /// <summary>
        /// 修改费用确认状态
        /// </summary>
        /// <param name="input"></param>
        ClientRegDto UpConfirmState(ChargeBM input);
        /// <summary>
        /// 体检业务查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CustomerSFTypeDto> getCusStType(SearchSFTypeDto input);
        /// <summary>
        /// 单位收费信息
        /// </summary>
        /// <returns></returns>
        List<ClientPaymentDto> getClientPayment(SearchSFTypeDto input);
        /// <summary>
        /// 获取发票
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MInvoiceRecordDto PrintInvoiceNum(ChargeBM input);

        MInvoiceRecordDto PrintInvoiceId(ChargeBM input);
        List<TJSQDto> HISChargstate(SqlWhereDto input);
        List<TJSQDto> HISChargsList(SqlWhereDto input);   
        List<TJSQDto> HISChargstate(List<TJSQDto> input);
        YHCustomerDto geYKInfor(InCarNumDto input);
        OutMessDto HISGroupChageState(HIsInput input);
        bool HISTTCharge(TJSQ hisinput);
        OutMessDto HISPayCharge(INTJSQDto hisinput);
        YHCustomerDto ChargeYHNum(InYKCarNumDto input);
        OutErrDto InsertReceiptState(CreateReceiptInfoDto input);
        void DZPF(DZFPInputDto input);
        CustomerRegCostDto GetsfState(EntityDto<Guid> input);
        List<CusPayListDto> getDailyList(SearchChagelistDto input);

        List<DayCusListDto> getPayDailyList(SearchChagelistDto input);
        List<MReceiptInfoDto> GetReceiptlist(SearchInvoiceDto searchInvoice);
        getCardInfoDto GetNYHCardByNum(ChargeBM input);

        OutCardDto NYHChargeCard(ChargCardDto input);

        bool ChargeCard(InCardChageDto qinput);
        List<CusReceivsDto> GetIndividuality(IndividualityDto input);
        List<IndividualitiesDto> GetIndividualities(IndividualitiesDto input);
        List<DoctorDeparmentDto> GetDoctorDeparment(DoctorDeparmentDto input);
        List<BallCheckDto> GetBallCheck(BallCheckDto input);
        List<ThreeBallCheckDto> GetThreeBallChecks(ThreeBallCheckDto input);
        List<ClientGroupStatisDto> clientGroupStatisDtos(SearchClientGroupDto input);
        void UPClientJSState(searchIDListDto input);
    }
        
}