using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExamination.Drivers.Models.HisInterface;
using Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge
{
    public class ChargeAppService : AppServiceApiProxyBase, IChargeAppService
    {
        public ChargeInfoDto Get(ChargeBM input)
        {
            return GetResult<ChargeBM, ChargeInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ICollection<MReceiptInfoPerDto> GetReceipt(ChargeCusRegDto input)
        {
            return GetResult<ChargeCusRegDto, ICollection<MReceiptInfoPerDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public ICollection<ChargeCusInfoDto> ChargeCusInfo(ChargQueryCusDto input)
        {
            return GetResult<ChargQueryCusDto, ICollection<ChargeCusInfoDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ChargeTypeDto> ChargeType(ChargeBM input)
        {
            //var value = new NameValueCollection();
            //value.Set(nameof(input), input.ToString());
            return GetResult<ChargeBM, List<ChargeTypeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public Guid InsertReceiptInfoDto(CreateReceiptInfoDto input)
        {
            return GetResult<CreateReceiptInfoDto, Guid>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ReceiptInfoPerViewDto GetReceiptDetailed(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, ReceiptInfoPerViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 线上支付查询
        /// </summary>
        /// <returns></returns>
        public List<WebChargeShow> getWebCharge(InSearchBusiCusDto input)
        {
            return GetResult<InSearchBusiCusDto, List<WebChargeShow>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ClientPayCusLisViewDto> GetClientCusList(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ClientPayCusLisViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ChargeTeamMoneyViewDto> GetClientTeamList(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ChargeTeamMoneyViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public MReceiptOrCustomerViewDto GetReceiptViewDto(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, MReceiptOrCustomerViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<MReceiptInfoPerViewDto> GetReceiptOrCustomer(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<MReceiptInfoPerViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }


        public decimal MinMoney(SeachChargrDto input)
        {
            return GetResult<SeachChargrDto, decimal>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public decimal MinGroupZKL(SeachChargrDto input)
        {
            return GetResult<SeachChargrDto, decimal>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<MReceiptClientDto> MInvoiceRecorView(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<MReceiptClientDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void InsertClientReceiptInfoDto(CreateReceiptInfoDto input)
        {
            GetResult<CreateReceiptInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void InsertClientReceiptDto(CreateReceiptInfoDto input)
        {
            GetResult<CreateReceiptInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ICollection<GroupMoneyDto> MinCusGroupMoney(SeachChargrDto dto)
        {
            return GetResult<SeachChargrDto, ICollection<GroupMoneyDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<MReceiptInfoDto> GetInvalidReceipt(SearchInvoiceDto dto)
        {
            return GetResult<SearchInvoiceDto, List<MReceiptInfoDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<MReceiptInfoDetailedViewDto> getReceiptInfoDetaileds(EntityDto<Guid> dto)
        {
            return GetResult<EntityDto<Guid>, List<MReceiptInfoDetailedViewDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SettlementInfoViewDto> GetCompenyInfo(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<SettlementInfoViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public MInvoiceRecordDto PrintInvoice(MInvoiceRecordDto input)
        {
            return GetResult<MInvoiceRecordDto, MInvoiceRecordDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ChargeGroupsDto> getChargeGroups(List<EntityDto<Guid>> input)
        {
            return GetResult<List<EntityDto<Guid>>, List<ChargeGroupsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void updateClientChargeState(UpdateChargeStateDto input)
        {
            GetResult<UpdateChargeStateDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientPayCusLisViewDto> GetTeamCusList(List<EntityDto<Guid>> input)
        {
            return GetResult<List<EntityDto<Guid>>, List<ClientPayCusLisViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public ClientRegDto UpZFState(ChargeBM input)
        {
            return GetResult<ChargeBM, ClientRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CusPayMoneyViewDto UpCusMoney(SearchPayMoneyDto input)
        {
            return GetResult<SearchPayMoneyDto, CusPayMoneyViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CusPayMoneyViewDto GetCusMoney(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, CusPayMoneyViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TjlCustomerItemGroupDto> GetCustomerItemGroupList(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<TjlCustomerItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public MReceiptInfoDto InsertInvalidReceiptInfoDto(GuIdDto input)
        {
            return GetResult<GuIdDto, MReceiptInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<GroupMoneyDto> MinlstGroupZKL(SeachChargrDto dto)
        {
            return GetResult<SeachChargrDto, List<GroupMoneyDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<GroupMoneyDto> CusGroupMoney(SeachChargrDto dto)
        {
            return GetResult<SeachChargrDto, List<GroupMoneyDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public MReceiptInfoDto GetInvalidReceiptById(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, MReceiptInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public TjlCustomerItemGroupDto InsertMLGroup(SearchMLGroupDto input)
        {
            return GetResult<SearchMLGroupDto, TjlCustomerItemGroupDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public ChargeTypeDto ChargeTypeByID(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, ChargeTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public bool SFGroupCheck(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, bool>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ChargeGroupsDto> getReceiptInfoGroups(ChargeBM input)
        {
            return GetResult<ChargeBM, List<ChargeGroupsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SearchKSGZLStatisticsDto> KSGZLStatistics(QueryClass input)
        {
            return GetResult<QueryClass, List<SearchKSGZLStatisticsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ChargeGroupsDto> GetCusGroups(ChargeBM input)
        {
            return GetResult<ChargeBM, List<ChargeGroupsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<MReceiptInfoDto> GetReceiptInfoBack(List<ChargeGroupsDto> input)
        {
            return GetResult<List<ChargeGroupsDto>, List<MReceiptInfoDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public MReceiptInfoDto GetInvalid(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, MReceiptInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public int GetZFState(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, int>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public ClientRegDto UpConfirmState(ChargeBM input)
        {
            return GetResult<ChargeBM, ClientRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerSFTypeDto> getCusStType(SearchSFTypeDto input)
        {
            return GetResult<SearchSFTypeDto, List<CustomerSFTypeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ClientPaymentDto> getClientPayment(SearchSFTypeDto input)
        {
            return GetResult<SearchSFTypeDto, List<ClientPaymentDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public MInvoiceRecordDto PrintInvoiceNum(ChargeBM input)
        {
            return GetResult<ChargeBM, MInvoiceRecordDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public MInvoiceRecordDto PrintInvoiceId(ChargeBM input)
        {
            return GetResult<ChargeBM, MInvoiceRecordDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<TJSQDto> HISChargstate(SqlWhereDto input)
        {
            return GetResult<SqlWhereDto, List<TJSQDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<TJSQDto> HISChargsList(SqlWhereDto input)
        {
            return GetResult<SqlWhereDto, List<TJSQDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public bool HISTTCharge(TJSQ input)
        {
            return GetResult<TJSQ, bool>(input, DynamicUriBuilder.GetAppSettingValue());


        }
        public List<TJSQDto> HISChargstate(List<TJSQDto> input)
        {
            return GetResult<List<TJSQDto>, List<TJSQDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public YHCustomerDto geYKInfor(InCarNumDto input)
        {
            return GetResult<InCarNumDto, YHCustomerDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public OutMessDto HISGroupChageState(HIsInput input)
        {
            return GetResult<HIsInput, OutMessDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public OutMessDto HISPayCharge(INTJSQDto input)
        {
            return GetResult<INTJSQDto, OutMessDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public YHCustomerDto ChargeYHNum(InYKCarNumDto input)
        {
            return GetResult<InYKCarNumDto, YHCustomerDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public OutErrDto InsertReceiptState(CreateReceiptInfoDto input)
        {
            return GetResult<CreateReceiptInfoDto, OutErrDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void DZPF(DZFPInputDto input)
        {
            GetResult<DZFPInputDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public CustomerRegCostDto GetsfState(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, CustomerRegCostDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<CusPayListDto> getDailyList(SearchChagelistDto input)
        {
            return GetResult<SearchChagelistDto, List<CusPayListDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<DayCusListDto> getPayDailyList(SearchChagelistDto input)
        {
            return GetResult<SearchChagelistDto, List<DayCusListDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<MReceiptInfoDto> GetReceiptlist(SearchInvoiceDto input)
        {
            return GetResult<SearchInvoiceDto, List<MReceiptInfoDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public getCardInfoDto GetNYHCardByNum(ChargeBM input)
        {
            return GetResult<ChargeBM, getCardInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutCardDto NYHChargeCard(ChargCardDto input)
        {
            return GetResult<ChargCardDto, OutCardDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public bool ChargeCard(InCardChageDto qinput)
        {
            return GetResult<InCardChageDto, bool>(qinput, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CusReceivsDto> GetIndividuality(IndividualityDto input)
        {
            return GetResult<IndividualityDto, List<CusReceivsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<IndividualitiesDto> GetIndividualities(IndividualitiesDto input)
        {
            return GetResult<IndividualitiesDto, List<IndividualitiesDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<DoctorDeparmentDto> GetDoctorDeparment(DoctorDeparmentDto input)
        {
            return GetResult<DoctorDeparmentDto, List<DoctorDeparmentDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
       public List<BallCheckDto> GetBallCheck(BallCheckDto input)
        {
            return GetResult<BallCheckDto, List<BallCheckDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ThreeBallCheckDto> GetThreeBallChecks(ThreeBallCheckDto input)
        {
            return GetResult<ThreeBallCheckDto, List<ThreeBallCheckDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
       public  List<ClientGroupStatisDto> clientGroupStatisDtos(SearchClientGroupDto input)
        {
            return GetResult<SearchClientGroupDto, List<ClientGroupStatisDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void UPClientJSState(searchIDListDto input)
        {
            GetResult<searchIDListDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}