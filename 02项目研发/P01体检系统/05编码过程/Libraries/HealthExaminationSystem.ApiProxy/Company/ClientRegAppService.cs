using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company
{
    public class ClientRegAppService : AppServiceApiProxyBase, IClientRegAppService
    {
        //public ClientRegsDto Add(ClientRegsDto input)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Del(ClientRegsDto input)
        //{
        //    throw new NotImplementedException();
        //}

        public void deleteClientReag(CreateClientRegDto dto)
        {
            GetResult<CreateClientRegDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        //public ClientRegsDto Edit(ClientRegsDto input)
        //{
        //    throw new NotImplementedException();
        //}

        //public ClientRegsDto Get(ClientRegsDto input)
        //{
        //    throw new NotImplementedException();
        //}

        public List<ClientRegDto> GetAll(EntityDto<Guid> dto)
        {
            return GetResult<EntityDto<Guid>, List<ClientRegDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        public ClientTeamRegDto getClientRegList(EntityDto<Guid> dto)
        {
            return GetResult<EntityDto<Guid>, ClientTeamRegDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        //public ClientRegsDto insertClientReag(CreateClientRegDto dto)
        //{
        //    return GetResult<CreateClientRegDto, ClientRegsDto>(dto);
        //}

        public PageResultDto<FullClientRegDto> PageFulls(PageInputDto<ClientRegSelectDto> input)
        {

            return GetResult<PageInputDto<ClientRegSelectDto>, PageResultDto<FullClientRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<CustomerRegsViewDto> PageCustomerRegFulls(PageInputDto<CustomerRegsInputDto> input)
        {
            return GetResult<PageInputDto<CustomerRegsInputDto>, PageResultDto<CustomerRegsViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
            //throw new NotImplementedException();
        }

        //public List<ClientRegsDto> Query(ClientRegsDto input)
        //{
        //    return GetResult<EntityDto<Guid>, List<ClientRegsDto>>(input);
        //}

        public List<CustomerRegSimpleViewDto> QueryCustomerReg(EntityDto<Guid> dto)
        {
            return GetResult<EntityDto<Guid>, List<CustomerRegSimpleViewDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        public void CreateTeamInfos(CreateClientTeamInfoesDto dto)
        {
            GetResult<CreateClientTeamInfoesDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CreateClientTeamInfoesDto> GetClientTeam(EntityDto<Guid> dto)
        {
            return GetResult<EntityDto<Guid>, List<CreateClientTeamInfoesDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientTeamInfosDto> GetClientTeamInfosById(List<Guid> input)
        {
            return GetResult<List<Guid>, List<ClientTeamInfosDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientTeamRegitemViewDto> GetClientTeamRegByClientId(SearchClientTeamInfoDto input)
        {
            return GetResult<EntityDto<Guid>, List<ClientTeamRegitemViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ViewClientRegStatisticsOverTheYearsDto> QueryClientRegStatisticsOverTheYears(SearchClientRegStatisticsOverTheYearsDto input)
        {
            return GetResult<SearchClientRegStatisticsOverTheYearsDto, List<ViewClientRegStatisticsOverTheYearsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ClientTeamInfosDto ClientTeamInfoUpdate(ClientTeamInfosDto input)
        {
            return GetResult<ClientTeamInfosDto, ClientTeamInfosDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void ClientTeamRegItemUpdate(List<CreateClientTeamRegItemDto> input)
        {
            GetResult<List<CreateClientTeamRegItemDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void GetClientTeamById(SearchClientTeamIdorTeamRegDto input)
        {
            GetResult<SearchClientTeamIdorTeamRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpClientCostById(SearchClientTeamIdorTeamRegDto input)
        {
            GetResult<SearchClientTeamIdorTeamRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DelCustomerReg(List<EntityDto<Guid>> input)
        {
            GetResult<List<EntityDto<Guid>>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public ClientRegDto GetClientRegByID(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, ClientRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ClientTeamInfosDto GetClientTeamInfos(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, ClientTeamInfosDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public int GetClientNumber(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, int>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public CreateClientRegDto AddClientReg(ClientTeamRegDto dto)
        {
            return GetResult<ClientTeamRegDto, CreateClientRegDto>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new NotImplementedException();
        }

        public CreateClientRegDto EditClientReg(ClientTeamRegDto dto)
        {
            return GetResult<ClientTeamRegDto, CreateClientRegDto>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new NotImplementedException();
        }
        public List<ClientTeamRegitemViewDto> EditClientRegN(ClientTeamRegDto dto)
        {
            return GetResult<ClientTeamRegDto, List<ClientTeamRegitemViewDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public void SynClientCusItem(List<ClientTeamRegitemViewDto> dto)
        {
             GetResult<List<ClientTeamRegitemViewDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public void AcynCustomerItem(EntityDto<Guid> dto)
        {
            GetResult<EntityDto<Guid>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        public void EditTeam(CreateClientTeamInfoesDto dto)
        {
            GetResult<CreateClientTeamInfoesDto>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DelTeam(EntityDto<Guid> teamId)
        {
            GetResult<EntityDto<Guid>>(teamId, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CustomerRegsViewDto> GetCustomerReg(CustomerRegsInputDto dto)
        {
            return GetResult<CustomerRegsInputDto, List<CustomerRegsViewDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new NotImplementedException();
        }
        public List<ClientTeamCusListDto> GetCustomerRegList(CustomerRegsInputDto dto)
        {
            return GetResult<CustomerRegsInputDto, List<ClientTeamCusListDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientTeamRegitemViewDto> GetClientTeamRegItem(SearchClientTeamInfoDto dto)
        {
            return GetResult<SearchClientTeamInfoDto, List<ClientTeamRegitemViewDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new NotImplementedException();
        }

        public ClientTeamRegDto QueryClientRegList(CreateClientRegDto dto)
        {
            return GetResult<CreateClientRegDto, ClientTeamRegDto>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new NotImplementedException();
        }
        public OutErrDto getSd(EntityDto<Guid> dto)
        {
            return GetResult<EntityDto<Guid>, OutErrDto>(dto, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 按科室统计团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        public List<StatisticsTTJZDDepartmentDto> StatisticsDepartmentTTJZD(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<StatisticsTTJZDDepartmentDto>>(input,DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据人员查询团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        public StatisticsTTJZDPersonalTTJZDDto StatisticsPersonalTTJZD(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, StatisticsTTJZDPersonalTTJZDDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据分组查询团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        public StatisticsTTJZDTeamTTJZDDto StatisticsTeamTTJZD(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, StatisticsTTJZDTeamTTJZDDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 导入人员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<QueryCustomerRegDto> RegCustomer(List<QueryCustomerRegDto> input)
        {
            return GetResult<List<QueryCustomerRegDto>, List<QueryCustomerRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 单位导入excel数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<QueryCustomerRegDto> RegClientCustomer(List<ClientCustomerRegDto> input)
        {
            return GetResult<List<ClientCustomerRegDto>, List<QueryCustomerRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
      
        public List<ClientTeamRegitemViewDto> GetTeamRegItem(SearchClientTeamInfoDto input)
        {
             return GetResult<SearchClientTeamInfoDto, List<ClientTeamRegitemViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取单位预约列表
        /// </summary>
        /// <returns></returns>
        public List<ClientRegNameComDto> getClientRegNameCom()
        {
            return GetResult<List<ClientRegNameComDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        public List<ClientRegLQDto> getClientRegLQ(SearchClientRegLQDto input)
        {
            return GetResult<SearchClientRegLQDto, List<ClientRegLQDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public UpClientMZDto getClientMZ(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, UpClientMZDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpClientMZ(UpClientMZDto input)
        {
             GetResult<UpClientMZDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public InquireGroupCustomerRegDto GetInquireGroupCustomerRegDtos(SearchGroupCustomerRegDto input)
        {
            return GetResult<SearchGroupCustomerRegDto,InquireGroupCustomerRegDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<MonrySummaryDto> GetMonrySummary(ApplicationfromDto input)
        {
            return GetResult<ApplicationfromDto,List<MonrySummaryDto>>(input,DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取单位自费金额
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<MonrySummaryDto> GetGRPayMent(ApplicationfromDto input)
        { return GetResult<ApplicationfromDto, List<MonrySummaryDto>>(input, DynamicUriBuilder.GetAppSettingValue()); }
        /// <summary>
        /// 获取单位预约版本号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetClientVerSionDto getClientVerSion(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, GetClientVerSionDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取单位自费金额明细 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ClientCusMoneyListDto> GetClientCusMoneyList(ApplicationfromDto input)
        {
            return GetResult<ApplicationfromDto, List<ClientCusMoneyListDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 团体预约发送短信
        /// </summary>
        /// <param name="input"></param>
        public void SaveMessage(ShortMessageDto input)
        {
             GetResult<ShortMessageDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}