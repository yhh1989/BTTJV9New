using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company
{

    public interface IClientRegAppService
#if !Proxy
        : IApplicationService
#endif
    {
        #region 团体预约功能

        #region 暂时废弃 新增

        // 注释的时候 Doc 说明一起注释，说的脚趾头都记住了，长个人头都记不住
        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="dto"></param>
        //ClientRegsDto insertClientReag(CreateClientRegDto dto);
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ClientTeamRegDto getClientRegList(EntityDto<Guid> dto);

        /// <summary>
        /// 查询团体预约信息
        /// </summary>
        /// <param name="dto"></param>
        ClientTeamRegDto QueryClientRegList(CreateClientRegDto dto);
        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        OutErrDto getSd(EntityDto<Guid> dto);
        List<ClientRegDto> GetAll(EntityDto<Guid> dto);
        void deleteClientReag(CreateClientRegDto dto);

        /// <summary>
        /// 新增单位预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        CreateClientRegDto AddClientReg(ClientTeamRegDto dto);

        /// <summary>
        /// 编辑单位预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        CreateClientRegDto EditClientReg(ClientTeamRegDto dto);
        List<ClientTeamRegitemViewDto> EditClientRegN(ClientTeamRegDto dto);
        void SynClientCusItem(List<ClientTeamRegitemViewDto> dtolist);
        void AcynCustomerItem(EntityDto<Guid> dto);

        PageResultDto<FullClientRegDto> PageFulls(PageInputDto<ClientRegSelectDto> input);

        /// <summary>
        /// 人员预约信息查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<CustomerRegSimpleViewDto> QueryCustomerReg(EntityDto<Guid> dto);

        /// <summary>
        /// 查询人员预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<CustomerRegsViewDto> GetCustomerReg(CustomerRegsInputDto dto);
        List<ClientTeamCusListDto> GetCustomerRegList(CustomerRegsInputDto dto);
        /// <summary>
        /// 分页查询人员预约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<CustomerRegsViewDto> PageCustomerRegFulls(PageInputDto<CustomerRegsInputDto> input);
        #endregion

        #region 迁移的ClientRegs的接口

        #region 暂时废弃 添加单位

        // 注释的时候 Doc 说明一起注释，说的脚趾头都记住了，长个人头都记不住
        ///// <summary>
        ///// 添加单位列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //ClientRegsDto Add(ClientRegsDto input);

        #endregion

        #region 暂时废弃 删除单位

        ///// <summary>
        ///// 删除单位列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //void Del(ClientRegsDto input);

        #endregion

        #region 暂时废弃 编辑单位

        ///// <summary>
        ///// 修改单位列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //ClientRegsDto Edit(ClientRegsDto input);

        #endregion

        #region 暂时废弃 获取单条单位
        // 注释的时候 Doc 说明一起注释，说的脚趾头都记住了，长个人头都记不住
        ///// <summary>
        ///// 获取一个单位列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //ClientRegsDto Get(ClientRegsDto input);

        #endregion

        #region 暂时废弃 获取单位列表

        ///// <summary>
        ///// 获取添加单位列表列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //List<ClientRegsDto> Query(ClientRegsDto input);
        #endregion

        #endregion

        #region 预约分组管理

        void CreateTeamInfos(CreateClientTeamInfoesDto dto);


        List<CreateClientTeamInfoesDto> GetClientTeam(EntityDto<Guid> dto);
        #endregion
        /// <summary>
        /// 根据分组Id查询分组信息
        /// </summary>
        List<ClientTeamInfosDto> GetClientTeamInfosById(List<Guid> input);
        void GetClientTeamById(SearchClientTeamIdorTeamRegDto input);
        void UpClientCostById(SearchClientTeamIdorTeamRegDto input);
        /// <summary>
        /// 根据分组Id查询单位分组预约信息
        /// </summary>
        /// <returns></returns>
        List<ClientTeamRegitemViewDto> GetClientTeamRegByClientId(SearchClientTeamInfoDto dto);
        /// <summary>
        /// 根据分组Id查询单位分组预约信息
        /// </summary>
        /// <returns></returns>
        List<ClientTeamRegitemViewDto> GetTeamRegItem(SearchClientTeamInfoDto dto);

        /// <summary>
        /// 查询单位预约分组项目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<ClientTeamRegitemViewDto> GetClientTeamRegItem(SearchClientTeamInfoDto dto);

        /// <summary>
        /// 查询单位历年预约统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ViewClientRegStatisticsOverTheYearsDto> QueryClientRegStatisticsOverTheYears(SearchClientRegStatisticsOverTheYearsDto input);
        /// <summary>
        /// 单位分组信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ClientTeamInfosDto ClientTeamInfoUpdate(ClientTeamInfosDto input);
        /// <summary>
        /// 单位分组登记项目信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void ClientTeamRegItemUpdate(List<CreateClientTeamRegItemDto> input);

        /// <summary>
        /// 删除人员预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns> 
        void DelCustomerReg(List<EntityDto<Guid>> dto);

        /// <summary>
        /// 导入人员信息
        /// </summary>
        /// <param name="inputDatas"></param>
        /// <returns></returns>
        List<QueryCustomerRegDto> RegCustomer(List<QueryCustomerRegDto> inputDatas);
        List<QueryCustomerRegDto> RegClientCustomer(List<ClientCustomerRegDto> input);
 
        /// <summary>
        /// 根据GUID单条单位
        /// </summary>
        /// <param name="input">单位类</param>
        /// <returns>单位类</returns>
        ClientRegDto GetClientRegByID(EntityDto<Guid> input);
        /// <summary>
        /// 根据GUID查询分组信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ClientTeamInfosDto GetClientTeamInfos(EntityDto<Guid> input);
        /// <summary>
        /// 查看预约次数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int GetClientNumber(EntityDto<Guid> input);
        /// <summary>
        /// 编辑分组
        /// </summary>
        /// <param name="dto"></param>
        void EditTeam(CreateClientTeamInfoesDto dto);

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="teamId"></param>
        void DelTeam(EntityDto<Guid> teamId);

        /// <summary>
        /// 按科室统计团体结账单
        /// </summary>
        /// <param name="input">单位id</param>
        List<StatisticsTTJZDDepartmentDto> StatisticsDepartmentTTJZD(EntityDto<Guid> input);
        /// <summary>
        /// 根据人员查询团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        StatisticsTTJZDPersonalTTJZDDto StatisticsPersonalTTJZD(EntityDto<Guid> input);
        /// <summary>
        /// 根据分组查询团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        StatisticsTTJZDTeamTTJZDDto StatisticsTeamTTJZD(EntityDto<Guid> input);
        /// <summary>
        /// 获取单位预约列表
        /// </summary>
        /// <returns></returns>
       
 #if Application
        Task<List<ClientRegNameComDto>> getClientRegNameCom();
#elif Proxy
         List<ClientRegNameComDto> getClientRegNameCom();
#endif
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        List<ClientRegLQDto> getClientRegLQ(SearchClientRegLQDto input);
        UpClientMZDto getClientMZ(EntityDto<Guid> input);
       void UpClientMZ(UpClientMZDto input);
        InquireGroupCustomerRegDto GetInquireGroupCustomerRegDtos(SearchGroupCustomerRegDto input);
        List<MonrySummaryDto> GetMonrySummary(ApplicationfromDto input);

        List<MonrySummaryDto> GetGRPayMent(ApplicationfromDto input);
        GetClientVerSionDto getClientVerSion(EntityDto<Guid> input);
        List<ClientCusMoneyListDto> GetClientCusMoneyList(ApplicationfromDto input);
        void SaveMessage(ShortMessageDto input);
    }
}
