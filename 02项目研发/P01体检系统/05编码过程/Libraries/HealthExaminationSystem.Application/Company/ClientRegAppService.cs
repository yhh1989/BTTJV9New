using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using AutoMapper;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company
{
    [AbpAuthorize]
    public class ClientRegAppService : MyProjectAppServiceBase, IClientRegAppService
    {
        private readonly IRepository<TjlClientInfo, Guid> _ClientInfo; //单位信息

        private readonly IRepository<TjlClientReg, Guid> _clientRegRepository; //单位预约登记信息

        private readonly IRepository<TjlClientTeamInfo, Guid> _ClientTeamInfo; //单位分组信息   update

        private readonly IRepository<TjlCustomer, Guid> _jlCustomer; //用户表

        private readonly IRepository<TjlCustomerItemGroup, Guid> _CustomerItemGroup; //单位分组登记项目

        private readonly IRepository<TjlCustomerRegItem, Guid> _CustomerRegItemRepository; //体检人检查项目结果表

        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository; //人员预约信息

        private readonly IRepository<TbmItemSuit, Guid> _ItemSuit;

        private readonly IRepository<TbmItemGroup, Guid> _ItemGroup; //项目组合设置

        private readonly IRepository<TjlMcusPayMoney, Guid> _mcusPayMoneyRepository; //个人应收已收

        private readonly IRepository<TjlClientTeamRegitem, Guid> _Regitem; //单位分组登记项目 
        private readonly IRepository<TbmOccHazardFactor, Guid> _TbmOccHazardFactor; //危害因素 
        private readonly IRepository<TbmOccDictionary, Guid> _tbmoccdictionaryRepository;
        private readonly IRepository<TjlApplicationForm, Guid> _TjlApplicationForm;
        private readonly IRepository<User, long> _User;
        private readonly IRepository<TjlMReceiptInfo, Guid> _mReceiptInfo; //收费记录表
        private readonly IRepository<TjlCustomerItemPic, Guid> _customerItemPicRepository; //图片

        private readonly IUserAppService _userAppService;

        private readonly IDepartmentAppService _departmentRepository; //科室设置

        private readonly ISqlExecutor _sqlExecutor;
        private readonly ICommonAppService _commonAppService;
        

        private readonly IRepository<TjlOperationLog, Guid> _tbmOperationLogcs;

        private readonly IRepository<TjlShortMessage, Guid> _tjlShortMessage;

        public ClientRegAppService(
            IRepository<TjlClientReg, Guid> clientRegRepository,
            IRepository<TjlClientInfo, Guid> ClientInfo,
            IUserAppService userAppService,
            IRepository<TjlCustomer, Guid> jlCustomer,
            IRepository<TjlCustomerReg, Guid> CustomerRegRepository,
            IRepository<TjlClientTeamInfo, Guid> ClientTeamInfo,
            IRepository<TbmItemSuit, Guid> ItemSuit,
            IRepository<TbmItemGroup, Guid> ItemGroup,
            IRepository<TjlClientTeamRegitem, Guid> Regitem,
            IRepository<TjlCustomerItemGroup, Guid> CustomerItemGroup,
            IRepository<TjlMcusPayMoney, Guid> mcusPayMoneyRepository,
            IRepository<TjlCustomerRegItem, Guid> CustomerRegItemRepository,
            IRepository<User, long> User,
            IDepartmentAppService departmentRepository,
            ISqlExecutor sqlExecutor,
            ICommonAppService CommonAppService,
            IRepository<TbmOccHazardFactor, Guid> TbmOccHazardFactor,
            IRepository<TbmOccDictionary, Guid> tbmoccdictionaryRepository,
            IRepository<TjlApplicationForm, Guid> TjlApplicationForm,
            IRepository<TjlOperationLog, Guid> tbmOperationLogcs,
            IRepository<TjlMReceiptInfo, Guid> mReceiptInfo,
            IRepository<TjlShortMessage, Guid> tjlShortMessage,
            IRepository<TjlCustomerItemPic, Guid> customerItemPicRepository)
        {
            _clientRegRepository = clientRegRepository;
            _ClientInfo = ClientInfo;
            _userAppService = userAppService;
            _jlCustomer = jlCustomer;
            _customerRegRepository = CustomerRegRepository;
            _ClientTeamInfo = ClientTeamInfo;
            _ItemSuit = ItemSuit;
            _ItemGroup = ItemGroup;
            _Regitem = Regitem;
            _CustomerItemGroup = CustomerItemGroup;
            _mcusPayMoneyRepository = mcusPayMoneyRepository;
            _CustomerRegItemRepository = CustomerRegItemRepository;
            _User = User;
            _departmentRepository = departmentRepository;
            _sqlExecutor = sqlExecutor;
            _commonAppService = CommonAppService;
            _TbmOccHazardFactor = TbmOccHazardFactor;
            _tbmoccdictionaryRepository = tbmoccdictionaryRepository;
            _tbmOperationLogcs = tbmOperationLogcs;
            _TjlApplicationForm = TjlApplicationForm;
            _mReceiptInfo = mReceiptInfo;
            _tjlShortMessage = tjlShortMessage;
            _customerItemPicRepository = customerItemPicRepository;
        }

        /// <summary>
        /// 删除单位预约
        /// </summary>
        /// <param name="dto"></param>
        public void deleteClientReag(CreateClientRegDto dto)
        {
            if (_customerRegRepository.GetAll().Any(o => o.ClientRegId == dto.Id))
            {
                throw new UserFriendlyException("删除错误", "分组下还有人员，无法删除！");
            }
            var teamInfo = _ClientTeamInfo.GetAllIncluding(o => o.ClientTeamRegitem).Where(o => o.ClientRegId == dto.Id).ToList();
            if (teamInfo != null && teamInfo.Count() > 0)
            {
                foreach (var team in teamInfo)
                {
                    var itemInfo = team.ClientTeamRegitem.ToList();
                    if (itemInfo != null && itemInfo.Count > 0)
                        foreach (var item in itemInfo)
                            _Regitem.Delete(item);
                    _ClientTeamInfo.Delete(team);
                }
            }
            if (dto.Id != null && dto.Id != Guid.Empty)
                _clientRegRepository.Delete(dto.Id);
        }

        /// <summary>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ClientRegDto> GetAll(EntityDto<Guid> dto)
        {
            if (dto != null)
            {
                if (dto.Id != Guid.Empty)
                    return _clientRegRepository.GetAllIncluding(r => r.ClientInfo).AsNoTracking().Where(o => o.ClientInfoId == dto.Id).MapTo<List<ClientRegDto>>();
            }

            return _clientRegRepository.GetAllIncluding(r => r.ClientInfo).AsNoTracking().MapTo<List<ClientRegDto>>();
        }

        public OutErrDto getSd(EntityDto<Guid> dto)
        {
            OutErrDto outErr = new OutErrDto();
            outErr.code = "0";
            if (dto.Id !=Guid.Empty)
            {
                var clientReg = _clientRegRepository.Get(dto.Id);
                if (clientReg != null && clientReg.SDState == 1)
                {
                    outErr.code = "1";
                }
            }
            return outErr;
        }
        /// <summary>
        /// 查询团体预约信息
        /// </summary>
        /// <param name="dto"></param>
        public ClientTeamRegDto QueryClientRegList(CreateClientRegDto dto)
        {
            var ctrDto = new ClientTeamRegDto();
            ctrDto.ClientRegDto = new CreateClientRegDto();
            if (dto.Id != null && dto.Id != Guid.Empty)
            {
                var cliReg = _clientRegRepository.Get(dto.Id);
                ctrDto.ClientRegDto = cliReg.MapTo<CreateClientRegDto>();

                ctrDto.ListClientTeam = new List<CreateClientTeamInfoesDto>();
                var cliRegTeamList = _ClientTeamInfo.GetAll().Where(o => o.ClientRegId == dto.Id);
                ctrDto.ListClientTeam = cliRegTeamList.MapTo<List<CreateClientTeamInfoesDto>>();

                ctrDto.ListClientTeamItem = new List<ClientTeamRegitemViewDto>();
                var cliRegTeamItemList = _Regitem.GetAll().Where(o => o.ClientRegId == dto.Id);
                ctrDto.ListClientTeamItem = cliRegTeamItemList.MapTo<List<ClientTeamRegitemViewDto>>();
            }
            else
            {
                var clientReg = _clientRegRepository.GetAll();
                ctrDto.ClientRegDto = clientReg.MapTo<CreateClientRegDto>();
            }
            return ctrDto;
        }

        /// <summary>
        /// 返回分页所需集合
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ClientTeamRegDto getClientRegList(EntityDto<Guid> dto)
        {
            var ctrDto = new ClientTeamRegDto();
            ctrDto.ClientRegDto = new CreateClientRegDto() { RegisterState = false };
            var cliReg = _clientRegRepository.Get(dto.Id);
            ctrDto.ClientRegDto = cliReg.MapTo<CreateClientRegDto>();

            //查询人员预约信息
            var customerReg = _customerRegRepository.GetAll().Where(o => o.ClientRegId == dto.Id);

            //判断是否已登记(没有全部已登记，选择项目权限放开，方便部分没有总检的人加减项)
            if (customerReg != null && customerReg.Count() > 0)
                if (!customerReg.Any(o => o.RegisterState != (int)RegisterState.Yes))
                    ctrDto.ClientRegDto.RegisterState = true;

            ctrDto.ListClientTeamItem = new List<ClientTeamRegitemViewDto>();

            var cliRegTeamList = _ClientTeamInfo.GetAll().Where(o => o.ClientRegId == dto.Id).OrderBy(o => o.TeamBM);
            if (cliRegTeamList != null && cliRegTeamList.Count() > 0)
            {

                ctrDto.ListClientTeam = new List<CreateClientTeamInfoesDto>();
                foreach (var cliRegTeam in cliRegTeamList)
                {

                    var cliRegTeamDto = new CreateClientTeamInfoesDto { RegisterState = false };

                    cliRegTeamDto = cliRegTeam.MapTo<CreateClientTeamInfoesDto>();
                    if (cliRegTeam.OccHazardFactors != null && cliRegTeam.OccHazardFactors.Count > 0)
                    {
                        cliRegTeamDto.ClientTeamRisk = cliRegTeam.OccHazardFactors.Select(o => o.Id).ToList();
                    }
                    //查询人员预约信息
                    var newcustomerReg = customerReg.Where(o => o.ClientTeamInfoId == cliRegTeam.Id);
                    if (newcustomerReg.Any(o => o.RegisterState == (int)RegisterState.Yes))
                        cliRegTeamDto.RegisterState = true;
                    // var customerRegDto = newcustomerReg.MapTo<List<ChargeCusStateDto>>();
                    var customerRegDto = newcustomerReg;
                    decimal sumMcusPayMoneys = 0;
                    //应收金额
                    var cusMoneys = customerRegDto.Where(o => o.McusPayMoney != null && o.McusPayMoney.ClientMoney != null)?.Select(r => r.McusPayMoney).ToList();
                    sumMcusPayMoneys = cusMoneys?.Sum(r => r.ClientMoney) ?? decimal.Parse("0.00");
                    cliRegTeamDto.YingShouJinE = sumMcusPayMoneys;

                    //实检金额
                    sumMcusPayMoneys = 0;
                    var sjMoneys = customerRegDto.Where(o => o.McusPayMoney != null && o.McusPayMoney.ClientMoney != null)?.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney).ToList();
                    sumMcusPayMoneys = sjMoneys?.Sum(r => r.ClientMoney) ?? decimal.Parse("0.00");
                    cliRegTeamDto.ShiJianJinE = sumMcusPayMoneys;

                    //加项金额
                    //sumMcusPayMoneys = 0;
                    ////var jxMoneys = customerRegDto?.Select(r => r.McusPayMoney);
                    //var jxMoneys = cusMoneys.Where(o => o.ClientAddMoney != null);
                    //sumMcusPayMoneys = cusMoneys?.Sum(r => r.ClientMinusMoney) ?? decimal.Parse("0.00");
                    //cliRegTeamDto.JianxiangJinE = sumMcusPayMoneys;

                    //体检人数
                    var count = customerRegDto?.Count();
                    cliRegTeamDto.TiJianRenShu = count ?? 0;
                    //实检人数
                    var sjcount = customerRegDto?.Where(r => r.SummSate == 3).Count();
                    cliRegTeamDto.SumSJRS = sjcount ?? 0;

                    ctrDto.ListClientTeam.Add(cliRegTeamDto);

                    var cliRegTeamItemList = _Regitem.GetAll().Where(o => o.ClientTeamInfoId == cliRegTeam.Id);
                    if (cliRegTeamItemList != null && cliRegTeamItemList.Count() > 0)
                    {
                        ctrDto.ListClientTeamItem.AddRange(cliRegTeamItemList.MapTo<List<ClientTeamRegitemViewDto>>());
                    }
                    //foreach (var cliRegTeamItem in cliRegTeamItemList)
                    //{
                    //    var cliRegTeamItemDto = new ClientTeamRegitemViewDto() { ProcessState = false };
                    //    cliRegTeamItemDto = cliRegTeamItem.MapTo<ClientTeamRegitemViewDto>();
                    //    //判断此项目在此预约分组中是否已检
                    //    if (_CustomerRegItemRepository.GetAll().Any(o => o.ItemId == cliRegTeamItem.Id && o.ProcessState == (int)ProjectIState.Complete))
                    //        cliRegTeamItemDto.ProcessState = true;
                    //    ctrDto.ListClientTeamItem.Add(cliRegTeamItemDto);
                    //}
                }
            }

            return ctrDto;
        }

        /// <summary>
        /// 新增单位预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public CreateClientRegDto AddClientReg(ClientTeamRegDto dto)
        {
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            var BMcout = _clientRegRepository.GetAll().Where(p => p.ClientRegBM ==
            dto.ClientRegDto.ClientRegBM).Count();
            if (BMcout >= 1)
            {
                throw new FieldVerifyException("添加预约错误！", "预约编码重复！");
            }
            var regId = Guid.Empty;
            var ClientReg = dto.ClientRegDto.MapTo<TjlClientReg>();
            ClientReg.ClientInfoId = dto.ClientRegDto.ClientInfo_Id;
            ClientReg.ClientInfo = _ClientInfo.Get(dto.ClientRegDto.ClientInfo_Id);
            ClientReg.ConfirmState = (int)ConfirmState.Unconfirmed;
            var userBM = _User.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue)
            {
                ClientReg.HospitalArea = userBM.HospitalArea;
            }
            //添加单位预约信息
            var cliReg = _clientRegRepository.Insert(ClientReg);
            regId = dto.ClientRegDto.Id;
            string logdetail = "添加单位预约：" + ClientReg.ClientInfo.ClientName + "(" + ClientReg.StartCheckDate + ")；";
            //添加预约分组信息
            if (dto.ListClientTeam != null && dto.ListClientTeam.Count > 0)
                foreach (var regTeam in dto.ListClientTeam)
                {
                    regTeam.ClientRegId = regId;
                    var cliRegTeam = regTeam.MapTo<TjlClientTeamInfo>();
                    cliRegTeam.ClientReg = ClientReg;
                    //危害因素处理
                    if (regTeam.ClientTeamRisk != null && regTeam.ClientTeamRisk.Count > 0)
                    {
                        cliRegTeam.OccHazardFactors = new List<Core.Occupational.TbmOccHazardFactor>();
                        foreach (var risk in regTeam.ClientTeamRisk)
                        {
                            var azardFactor = _TbmOccHazardFactor.Get(risk);
                            cliRegTeam.OccHazardFactors.Add(azardFactor);
                        }
                    }
                    if (userBM != null && userBM.HospitalArea.HasValue)
                    {
                        cliRegTeam.HospitalArea = userBM.HospitalArea;
                    }
                    _ClientTeamInfo.Insert(cliRegTeam);

                    logdetail += "添加分组：" + cliRegTeam.TeamName + "；";
                    if (!string.IsNullOrEmpty(cliRegTeam.ItemSuitName))
                    {
                        logdetail += "套餐：" + cliRegTeam.ItemSuitName + "；";
                    }
                }

            //添加单位预约分组项目信息
            if (dto.ListClientTeamItem != null && dto.ListClientTeamItem.Count > 0)
                foreach (var regTeamItem in dto.ListClientTeamItem)
                {
                    var itemInfo = regTeamItem.MapTo<TjlClientTeamRegitem>();
                    if (itemInfo.ClientRegId != null && itemInfo.ClientRegId != Guid.Empty)
                        itemInfo.ClientRegId = regId;
                    if (itemInfo.TbmItemGroupId != null && itemInfo.TbmItemGroupId != Guid.Empty && string.IsNullOrWhiteSpace(itemInfo.ItemGroupCodeBM))
                        itemInfo.ItemGroupCodeBM = _ItemGroup.Get(itemInfo.TbmItemGroupId.Value).ItemGroupBM;
                    _Regitem.Insert(itemInfo);
                    var itemnew = dto.ListClientTeam.FirstOrDefault(o => o.Id == itemInfo.ClientTeamInfoId);
                    // logdetail += "添加项目：" + itemnew.TeamBM +"-" +itemInfo.ItemGroupName + "；";
                }

            //添加操作日志
            createOpLogDto.LogBM = cliReg.ClientInfo.ClientBM;
            createOpLogDto.LogName = cliReg.ClientInfo.ClientName;
            createOpLogDto.LogText = "添加单位预约：" + cliReg.ClientInfo.ClientName;
            createOpLogDto.LogType = (int)LogsTypes.ClientId;
            if (logdetail.Length > 100)
            {
                logdetail = logdetail.Substring(0, 100);
            }
            createOpLogDto.LogDetail = logdetail;
            _commonAppService.SaveOpLog(createOpLogDto);

            CurrentUnitOfWork.SaveChanges();
            return cliReg.MapTo<CreateClientRegDto>();
        }

        /// <summary>
        /// 编辑单位预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public CreateClientRegDto EditClientReg(ClientTeamRegDto dto)
        {
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            string logdetail = "";
            //编辑单位预约信息
            var clientReg = _clientRegRepository.Get(dto.ClientRegDto.Id);
            var BMcout = _clientRegRepository.GetAll().Where(p => p.ClientRegBM ==
             dto.ClientRegDto.ClientRegBM).Count() ;
            if (BMcout > 1)
            {
                throw new FieldVerifyException("修改错误！", "预约编码重复！");
            }
                if (clientReg != null)
            {
                //_clientRegRepository.Update(dto.ClientRegDto.MapTo(clientReg));

                //编辑单位预约分组信息
                //获取旧分组
                var OldTeamlist = _ClientTeamInfo.GetAll().Where(o => o.ClientRegId == clientReg.Id).ToList();
                var OldClientItems = _Regitem.GetAll().Where(o => o.ClientRegId == clientReg.Id).ToList();
                if (dto.ListClientTeam != null && dto.ListClientTeam.Count > 0)
                {
                    var UpClientCusItems = new List<ClientTeamRegitemViewDto>();
                    foreach (var item in dto.ListClientTeam)
                    {
                        if (OldTeamlist.Any(o => o.Id == item.Id))
                        {
                            var cliTeam = _ClientTeamInfo.Get(item.Id);
                            //危害因素处理
                            if (item.ClientTeamRisk != null && item.ClientTeamRisk.Count > 0)
                            {
                                cliTeam.OccHazardFactors = new List<Core.Occupational.TbmOccHazardFactor>();
                                foreach (var risk in item.ClientTeamRisk)
                                {
                                    var azardFactor = _TbmOccHazardFactor.Get(risk);
                                    cliTeam.OccHazardFactors.Add(azardFactor);
                                }
                            }
                            _ClientTeamInfo.Update(item.MapTo(cliTeam));
                            logdetail += "修改分组：" + cliTeam.TeamName + "；";
                        }
                        else
                        {
                            var cliRegTeam = item.MapTo<TjlClientTeamInfo>();
                            cliRegTeam.ClientReg = clientReg;
                            //危害因素处理
                            if (item.ClientTeamRisk != null && item.ClientTeamRisk.Count > 0)
                            {
                                cliRegTeam.OccHazardFactors = new List<Core.Occupational.TbmOccHazardFactor>();
                                foreach (var risk in item.ClientTeamRisk)
                                {
                                    var azardFactor = _TbmOccHazardFactor.Get(risk);
                                    cliRegTeam.OccHazardFactors.Add(azardFactor);
                                }
                            }
                            _ClientTeamInfo.Insert(cliRegTeam);
                            logdetail += "添加分组：" + cliRegTeam.TeamName + "；";
                            if (!string.IsNullOrEmpty(cliRegTeam.ItemSuitName))
                            {
                                logdetail += "套餐：" + cliRegTeam.ItemSuitName + "；";
                            }
                        }
                        var EditMoney = false; //是否更新项目价格
                        //单位分组项目信息编辑
                        if (dto.ListClientTeamItem != null && dto.ListClientTeamItem.Count > 0)
                        {

                            var cliTeamItemList = dto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == item.Id).ToList();
                            foreach (var cliTeamItemDto in cliTeamItemList)
                                if (OldClientItems.Any(o => o.Id == cliTeamItemDto.Id))
                                {
                                    var cliTeamItem = _Regitem.Get(cliTeamItemDto.Id);

                                    //var aa = cliTeamItemDto.MapTo(cliTeamItem);
                                    var upItem = cliTeamItemDto.MapTo(cliTeamItem);
                                    upItem.ClientRegId = dto.ClientRegDto.Id;
                                    //补充项目编码
                                    if (upItem.TbmItemGroupId != null && upItem.TbmItemGroupId != Guid.Empty && string.IsNullOrWhiteSpace(upItem.ItemGroupCodeBM))
                                        upItem.ItemGroupCodeBM = _ItemGroup.Get(upItem.TbmItemGroupId.Value).ItemGroupBM;
                                    _Regitem.Update(upItem);
                                    // logdetail += "修改分组项目：" + upItem.ClientTeamInfo.TeamBM+"-"+ upItem.ItemGroupName + "；";
                                }
                                else
                                {
                                    var inItem = cliTeamItemDto.MapTo<TjlClientTeamRegitem>();
                                    inItem.ClientRegId = dto.ClientRegDto.Id;
                                    //补充项目编码
                                    if (inItem.TbmItemGroupId != null && inItem.TbmItemGroupId != Guid.Empty && string.IsNullOrWhiteSpace(inItem.ItemGroupCodeBM))
                                        inItem.ItemGroupCodeBM = _ItemGroup.Get(inItem.TbmItemGroupId.Value).ItemGroupBM;
                                    _Regitem.Insert(inItem);
                                    // logdetail += "添加分组项目：" + inItem.ClientTeamInfo.TeamBM + "-" + inItem.ItemGroupName + "；";
                                    //加项
                                    // SynCustomerItem(item.Id, cliTeamItemDto, (int)EditModeType.Add);
                                    var TeamItem = cliTeamItemDto;
                                    TeamItem.IsAddMinus = (int)AddMinusType.Add;
                                    UpClientCusItems.Add(TeamItem);

                                    EditMoney = true;
                                    //有加项改为未确认
                                    clientReg.ConfirmState = (int)ConfirmState.Unconfirmed;
                                }

                            //同分组信息的删除--------下
                            var noeteamlist = dto.ListClientTeamItem.Select(o => o.Id);
                            var tjlCliTeamItem = OldClientItems.Where(o => o.ClientTeamInfoId == item.Id && !noeteamlist.Contains(o.Id));
                            if (tjlCliTeamItem != null && tjlCliTeamItem.Count() > 0)
                                foreach (var cti in tjlCliTeamItem)
                                {
                                    if (noeteamlist.Contains(cti.Id))
                                        continue;

                                    //减项
                                    //SynCustomerItem(item.Id, cti.MapTo<ClientTeamRegitemViewDto>(), (int)EditModeType.Delete);
                                    var TeamItem = cti.MapTo<ClientTeamRegitemViewDto>();
                                    TeamItem.IsAddMinus = (int)AddMinusType.Minus;
                                    UpClientCusItems.Add(TeamItem);
                                    _Regitem.Delete(cti);
                                    // logdetail += "删除分组项目：" + cti.ClientTeamInfo.TeamBM + "-" + cti.ItemGroupName + "；";
                                    EditMoney = true;
                                    //有减项改为未确认
                                    clientReg.ConfirmState = (int)ConfirmState.Unconfirmed;
                                }

                        }

                        //this.CurrentUnitOfWork.SaveChanges();

                        if (EditMoney)
                        {
                            CurrentUnitOfWork.SaveChanges();

                        }

                    }

                    //这是是如果分组进行删除了，这个编辑时可以进行删除
                    //如果前台要实时删除，这里不要了（但是前台要判断好添加时删除操作）
                    var nowTeamIds = dto.ListClientTeam.Select(o => o.Id).ToList();
                    var tjlCliTeam = _ClientTeamInfo.GetAll().Where(o => o.ClientRegId == dto.ClientRegDto.Id && !nowTeamIds.Contains(o.Id));
                    if (tjlCliTeam != null && tjlCliTeam.Count() > 0)
                        foreach (var item in tjlCliTeam)
                        {
                            if (nowTeamIds.Contains(item.Id))
                                continue;
                            //如果有需要放开注释，这个是验证分组下是否有体检人员
                            //if(_customerRegRepository.GetAll().Any(o=>o.ClientTeamInfoId==item.Id))
                            //    throw new FieldVerifyException("修改错误！", item.TeamName+"分组下还有人员，无法删除此分组！");
                            var cliTeamItemList = _Regitem.GetAll().Where(o => o.ClientTeamInfoId == item.Id);
                            foreach (var cliTeamItem in cliTeamItemList)
                                _Regitem.Delete(cliTeamItem);
                            _ClientTeamInfo.Delete(item);
                        }

                    //修改人员项目价格
                    if (UpClientCusItems.Count > 0)
                    {
                        SynClientCusItem(UpClientCusItems);
                        var teamIs = UpClientCusItems.Select(o => o.ClientTeamInfoId).Distinct().ToList();
                        foreach (var teamid in teamIs)
                        {
                            EntityDto<Guid> entityDto = new EntityDto<Guid>();
                            entityDto.Id = teamid.Value;
                            //AcynCustomerItem(entityDto);
                            UpCusMoney(entityDto);
                        }
                    }
                }
                try
                {
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
                //更新团体预约表
                //dto.ClientRegDto.ControlDate = 2;
                //dto.ClientRegDto.ClientRegBM = "100";
                var st = dto.ClientRegDto.MapTo(clientReg);
                if (st.UserId.HasValue)
                {
                    st.user = _User.Get(st.UserId.Value);
                }
                try
                {
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
                _clientRegRepository.Update(dto.ClientRegDto.MapTo(clientReg));
                //添加操作日志
                createOpLogDto.LogBM = clientReg.ClientInfo.ClientBM;
                createOpLogDto.LogName = clientReg.ClientInfo.ClientName;
                createOpLogDto.LogText = "修改单位预约：" + clientReg.ClientInfo.ClientName;
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                if (logdetail.Length > 100)
                {
                    logdetail = logdetail.Substring(0, 100);
                }
                createOpLogDto.LogDetail = logdetail;
                _commonAppService.SaveOpLog(createOpLogDto);
            }

            //CurrentUnitOfWork.SaveChanges();
            var clientRegDto = clientReg.MapTo<CreateClientRegDto>();
            //判断是否有人员登记
            clientRegDto.RegisterState = false;
            if (_customerRegRepository.GetAll().Any(o => o.ClientRegId == clientRegDto.Id && o.RegisterState == (int)RegisterState.Yes))
                clientRegDto.RegisterState = true;
            return clientRegDto;
        }

        /// <summary>
        /// 编辑单位预约信息临时
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ClientTeamRegitemViewDto> EditClientRegN(ClientTeamRegDto dto)
        {
            List<ClientTeamRegitemViewDto> OutclientTeamRegitemDtos = new List<ClientTeamRegitemViewDto>();
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            string logdetail = "";
            //编辑单位预约信息
            var clientReg = _clientRegRepository.Get(dto.ClientRegDto.Id);
            if (clientReg != null)
            {
                //_clientRegRepository.Update(dto.ClientRegDto.MapTo(clientReg));

                //编辑单位预约分组信息
                if (dto.ListClientTeam != null && dto.ListClientTeam.Count > 0)
                {
                    var UpClientCusItems = new List<ClientTeamRegitemViewDto>();
                    foreach (var item in dto.ListClientTeam)
                    {
                        if (_ClientTeamInfo.GetAll().Any(o => o.Id == item.Id))
                        {
                            var cliTeam = _ClientTeamInfo.Get(item.Id);
                            //危害因素处理
                            if (item.ClientTeamRisk != null && item.ClientTeamRisk.Count > 0)
                            {
                                cliTeam.OccHazardFactors = new List<Core.Occupational.TbmOccHazardFactor>();
                                foreach (var risk in item.ClientTeamRisk)
                                {
                                    var azardFactor = _TbmOccHazardFactor.Get(risk);
                                    cliTeam.OccHazardFactors.Add(azardFactor);
                                }
                            }
                            _ClientTeamInfo.Update(item.MapTo(cliTeam));
                            logdetail += "修改分组：" + cliTeam.TeamName + "；";
                        }
                        else
                        {
                            var cliRegTeam = item.MapTo<TjlClientTeamInfo>();
                            cliRegTeam.ClientReg = clientReg;
                            //危害因素处理
                            if (item.ClientTeamRisk != null && item.ClientTeamRisk.Count > 0)
                            {
                                cliRegTeam.OccHazardFactors = new List<Core.Occupational.TbmOccHazardFactor>();
                                foreach (var risk in item.ClientTeamRisk)
                                {
                                    var azardFactor = _TbmOccHazardFactor.Get(risk);
                                    cliRegTeam.OccHazardFactors.Add(azardFactor);
                                }
                            }
                            _ClientTeamInfo.Insert(cliRegTeam);
                            logdetail += "添加分组：" + cliRegTeam.TeamName + "；";
                            if (!string.IsNullOrEmpty(cliRegTeam.ItemSuitName))
                            {
                                logdetail += "套餐：" + cliRegTeam.ItemSuitName + "；";
                            }
                        }
                        var EditMoney = false; //是否更新项目价格
                        //单位分组项目信息编辑
                        if (dto.ListClientTeamItem != null && dto.ListClientTeamItem.Count > 0)
                        {

                            var cliTeamItemList = dto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == item.Id);
                            foreach (var cliTeamItemDto in cliTeamItemList)
                                if (_Regitem.GetAll().Any(o => o.Id == cliTeamItemDto.Id))
                                {
                                    var cliTeamItem = _Regitem.Get(cliTeamItemDto.Id);

                                    //var aa = cliTeamItemDto.MapTo(cliTeamItem);
                                    var upItem = cliTeamItemDto.MapTo(cliTeamItem);
                                    upItem.ClientRegId = dto.ClientRegDto.Id;
                                    //补充项目编码
                                    if (upItem.TbmItemGroupId != null && upItem.TbmItemGroupId != Guid.Empty && string.IsNullOrWhiteSpace(upItem.ItemGroupCodeBM))
                                        upItem.ItemGroupCodeBM = _ItemGroup.Get(upItem.TbmItemGroupId.Value).ItemGroupBM;
                                    _Regitem.Update(upItem);
                                    // logdetail += "修改分组项目：" + upItem.ClientTeamInfo.TeamBM+"-"+ upItem.ItemGroupName + "；";
                                }
                                else
                                {
                                    var inItem = cliTeamItemDto.MapTo<TjlClientTeamRegitem>();
                                    inItem.ClientRegId = dto.ClientRegDto.Id;
                                    //补充项目编码
                                    if (inItem.TbmItemGroupId != null && inItem.TbmItemGroupId != Guid.Empty && string.IsNullOrWhiteSpace(inItem.ItemGroupCodeBM))
                                        inItem.ItemGroupCodeBM = _ItemGroup.Get(inItem.TbmItemGroupId.Value).ItemGroupBM;
                                    _Regitem.Insert(inItem);
                                    // logdetail += "添加分组项目：" + inItem.ClientTeamInfo.TeamBM + "-" + inItem.ItemGroupName + "；";
                                    //加项
                                    // SynCustomerItem(item.Id, cliTeamItemDto, (int)EditModeType.Add);
                                    var TeamItem = cliTeamItemDto;
                                    TeamItem.IsAddMinus = (int)AddMinusType.Add;
                                    UpClientCusItems.Add(TeamItem);

                                    EditMoney = true;
                                    //有加项改为未确认
                                    clientReg.ConfirmState = (int)ConfirmState.Unconfirmed;
                                }

                            //同分组信息的删除--------下
                            var tjlCliTeamItem = _Regitem.GetAll().Where(o => o.ClientTeamInfoId == item.Id);
                            if (tjlCliTeamItem != null && tjlCliTeamItem.Count() > 0)
                                foreach (var cti in tjlCliTeamItem)
                                {
                                    if (dto.ListClientTeamItem.Select(o => o.Id).Contains(cti.Id))
                                        continue;

                                    //减项
                                    //SynCustomerItem(item.Id, cti.MapTo<ClientTeamRegitemViewDto>(), (int)EditModeType.Delete);
                                    var TeamItem = cti.MapTo<ClientTeamRegitemViewDto>();
                                    TeamItem.IsAddMinus = (int)AddMinusType.Minus;
                                    UpClientCusItems.Add(TeamItem);
                                    _Regitem.Delete(cti);
                                    // logdetail += "删除分组项目：" + cti.ClientTeamInfo.TeamBM + "-" + cti.ItemGroupName + "；";
                                    EditMoney = true;
                                    //有减项改为未确认
                                    clientReg.ConfirmState = (int)ConfirmState.Unconfirmed;
                                }

                        }

                        //this.CurrentUnitOfWork.SaveChanges();

                        //修改人员项目价格
                        if (EditMoney)
                        {
                            CurrentUnitOfWork.SaveChanges();

                        }

                    }

                    //这是是如果分组进行删除了，这个编辑时可以进行删除
                    //如果前台要实时删除，这里不要了（但是前台要判断好添加时删除操作）
                    var tjlCliTeam = _ClientTeamInfo.GetAll().Where(o => o.ClientRegId == dto.ClientRegDto.Id);
                    if (tjlCliTeam != null && tjlCliTeam.Count() > 0)
                        foreach (var item in tjlCliTeam)
                        {
                            if (dto.ListClientTeam.Select(o => o.Id).Contains(item.Id))
                                continue;
                            //如果有需要放开注释，这个是验证分组下是否有体检人员
                            //if(_customerRegRepository.GetAll().Any(o=>o.ClientTeamInfoId==item.Id))
                            //    throw new FieldVerifyException("修改错误！", item.TeamName+"分组下还有人员，无法删除此分组！");
                            var cliTeamItemList = _Regitem.GetAll().Where(o => o.ClientTeamInfoId == item.Id);
                            foreach (var cliTeamItem in cliTeamItemList)
                                _Regitem.Delete(cliTeamItem);
                            _ClientTeamInfo.Delete(item);
                        }
                    if (UpClientCusItems.Count > 0)
                    {
                        OutclientTeamRegitemDtos = UpClientCusItems;
                        //SynClientCusItem(UpClientCusItems);
                        //var teamIs = UpClientCusItems.Select(o => o.ClientTeamInfoId).Distinct().ToList();
                        //foreach (var teamid in teamIs)
                        //{
                        //    AcynCustomerItem(teamid.Value);
                        //}
                    }
                }
                try
                {
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
                //更新团体预约表
                //dto.ClientRegDto.ControlDate = 2;
                //dto.ClientRegDto.ClientRegBM = "100";
                var st = dto.ClientRegDto.MapTo(clientReg);
                if (st.UserId.HasValue)
                {
                    st.user = _User.Get(st.UserId.Value);
                }
                try
                {
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
                _clientRegRepository.Update(dto.ClientRegDto.MapTo(clientReg));
                //添加操作日志
                createOpLogDto.LogBM = clientReg.ClientInfo.ClientBM;
                createOpLogDto.LogName = clientReg.ClientInfo.ClientName;
                createOpLogDto.LogText = "修改单位预约：" + clientReg.ClientInfo.ClientName;
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                if (logdetail.Length > 100)
                {
                    logdetail = logdetail.Substring(0, 100);
                }
                createOpLogDto.LogDetail = logdetail;
                _commonAppService.SaveOpLog(createOpLogDto);
            }

            //CurrentUnitOfWork.SaveChanges();
            var clientRegDto = clientReg.MapTo<CreateClientRegDto>();
            //判断是否有人员登记
            clientRegDto.RegisterState = false;
            if (_customerRegRepository.GetAll().Any(o => o.ClientRegId == clientRegDto.Id && o.RegisterState == (int)RegisterState.Yes))
                clientRegDto.RegisterState = true;
            return OutclientTeamRegitemDtos;
        }

        /// <summary>
        /// 操作人员组合和金钱表
        /// </summary>
        /// <param name="TeamId"></param>
        public void AcynCustomerItem(EntityDto<Guid> TeamId)
        {

            var customerReg = _customerRegRepository.GetAllIncluding(o => o.CustomerItemGroup, o => o.McusPayMoney).Where(o => o.ClientTeamInfoId == TeamId.Id && o.RegisterState == (int)RegisterState.No && o.CheckSate == (int)ProjectIState.Not).ToList();
            //var payinfo = _customerRegRepository.GetAllIncluding(o => o.McusPayMoney).ToList();

            foreach (var items in customerReg)
            {
                decimal personalPay = 0; //个人应收
                decimal clientMoney = 0; //团体应收
                decimal personalAdd = 0; //个人加项
                decimal personalMinusMoney = 0; //个人减项
                decimal clientAdd = 0; //团体加项
                decimal clientMinusMoney = 0; //团体减项

                var CustomerGroupItem = items.CustomerItemGroup.ToList();
                var cusGroupls = CustomerGroupItem.Where(o => o.IsAddMinus != (int)AddMinusType.Minus);
                personalPay = cusGroupls.Sum(o => o.GRmoney);
                clientMoney = cusGroupls.Sum(o => o.TTmoney);
                //加项
                var cusAddGroupls = CustomerGroupItem.Where(o => o.IsAddMinus == (int)AddMinusType.Add);
                personalAdd = cusAddGroupls.Sum(o => o.GRmoney);
                clientAdd = cusAddGroupls.Sum(o => o.TTmoney);
                //减项
                var cusMinGroupls = CustomerGroupItem.Where(o => o.IsAddMinus == (int)AddMinusType.Minus);
                clientMinusMoney = cusMinGroupls.Sum(o => o.GRmoney);
                personalMinusMoney = cusMinGroupls.Sum(o => o.TTmoney);

                //foreach (var Gitem in CustomerGroupItem)
                //{
                //    if (Gitem.IsAddMinus != (int)AddMinusType.Minus)
                //    {
                //        personalPay += Gitem.GRmoney;  // 个人应收金额累计
                //        clientMoney += Gitem.TTmoney;  // 团体应收金额累计
                //    }
                //    if (Gitem.IsAddMinus == (int)AddMinusType.Add)
                //    {
                //        //加项
                //        personalAdd += Gitem.GRmoney;
                //        clientAdd += Gitem.TTmoney;
                //    }
                //    if (Gitem.IsAddMinus == (int)AddMinusType.Minus)
                //    {
                //        //减项
                //        clientMinusMoney += Gitem.TTmoney;
                //        personalMinusMoney += Gitem.GRmoney;
                //    }

                //}
                var payinfo = items.McusPayMoney;
                if (payinfo == null)
                {
                    _mcusPayMoneyRepository.Insert(new TjlMcusPayMoney
                    {
                        Customer = items.Customer,
                        CustomerReg = items,
                        ClientInfo = items.ClientInfo,
                        ClientReg = items.ClientReg,
                        ClientTeamInfo = items.ClientTeamInfo,
                        PersonalAddMoney = personalAdd,
                        PersonalMinusMoney = personalMinusMoney,
                        ClientAdjustAddMoney = clientAdd,
                        ClientAdjustMinusMoney = clientMinusMoney,
                        PersonalMoney = personalPay,
                        ClientMoney = clientMoney,
                        PersonalPayMoney = 0
                    });
                }
                else
                {
                    if (payinfo.PersonalMoney != personalPay || payinfo.ClientMoney != clientMoney ||
                        payinfo.PersonalAddMoney != personalAdd || payinfo.ClientAddMoney != personalAdd ||
                        payinfo.ClientMinusMoney != personalMinusMoney)
                    {
                        payinfo.PersonalMoney = personalPay;
                        payinfo.ClientMoney = clientMoney;
                        payinfo.PersonalAddMoney = personalAdd;
                        payinfo.PersonalMinusMoney = personalMinusMoney;
                        payinfo.ClientAddMoney = clientAdd;
                        payinfo.ClientMinusMoney = clientMinusMoney;
                        _mcusPayMoneyRepository.Update(payinfo);
                    }
                }


            }
        }

        public void UpCusMoney(EntityDto<Guid> input)
        {
            //同步金额            
            var client = _ClientTeamInfo.Get(input.Id);
            //新增价格
            SqlParameter[] parameter2 = {
                         new SqlParameter("@CreatorUserId",  AbpSession.UserId),
                          new SqlParameter("@ClientInfo_Id", client.ClientReg.ClientInfoId),
                           new SqlParameter("@ClientReg_Id", client.ClientRegId),
                            new SqlParameter("@ClientTeamInfo_Id", input.Id)

                };
            string MoneycusitemSql = string.Format(@"  insert into [TjlMcusPayMoneys]
 ( [Id]
      ,[PersonalMoney]
      ,[PersonalAddMoney]
      ,[PersonalMinusMoney]
      ,[PersonalAdjustAddMoney]
      ,[PersonalAdjustMinusMoney]
      ,[PersonalPayMoney]
      ,[ClientMoney]
      ,[ClientAddMoney]
      ,[ClientMinusMoney]
      ,[ClientAdjustAddMoney]
      ,[ClientAdjustMinusMoney]
      ,[TenantId]
      ,[IsDeleted] 
      ,[CreationTime]
      ,[CreatorUserId]
      ,[ClientInfo_Id]
      ,[ClientReg_Id]
      ,[ClientTeamInfo_Id]
      ,[Customer_Id]
	  )
 select TjlCustomerItemGroups.CustomerRegBMId,SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else GRmoney end),
  SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then GRmoney else 0 end) ,
    SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then GRmoney else 0 end) ,
	0,0,0,
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else TTmoney end),
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then TTmoney else 0 end),
	  SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then TTmoney else 0 end),
	  0,0,2,0,GETDATE()	 ,  
	  @CreatorUserId,@ClientInfo_Id,@ClientReg_Id,@ClientTeamInfo_Id,	 
	   TjlCustomerRegs.CustomerId
  from TjlCustomerItemGroups ,TjlCustomerRegs where
    TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=TjlCustomerRegs.Id and 
   CustomerRegBMId in (select TjlCustomerRegs.Id  from TjlCustomerRegs 
 left join TjlMcusPayMoneys  on TjlCustomerRegs.Id=TjlMcusPayMoneys.Id where 
 TjlCustomerRegs.ClientTeamInfoId=@ClientTeamInfo_Id and
  TjlMcusPayMoneys.id is null)
 group by CustomerRegBMId ,TjlCustomerRegs.CustomerId
");
            _sqlExecutor.Execute(MoneycusitemSql, parameter2);

            //更新已有价格
            SqlParameter[] parameter3 = {
                         new SqlParameter("@ClientTeamInfo_Id", input.Id)
                };
            string upMcusitemSql = string.Format(@"update [TjlMcusPayMoneys] set 
[PersonalMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else GRmoney end) from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[PersonalAddMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then GRmoney else 0 end)  from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[PersonalMinusMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then GRmoney else 0 end) from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
     
      ,[ClientMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else TTmoney end) from TjlCustomerItemGroups where  TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[ClientAddMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then TTmoney else 0 end) from TjlCustomerItemGroups where  TjlCustomerItemGroups.IsDeleted=0 and TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[ClientMinusMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then TTmoney else 0 end) from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)  
	 where id in(select Id  from TjlCustomerRegs where ClientTeamInfo_Id=@ClientTeamInfo_Id) and IsDeleted=0");
            _sqlExecutor.Execute(upMcusitemSql, parameter3);
        }
        /// <summary>
        /// 人员加减项目
        /// </summary>
        public void SynCustomerItem(Guid TeamId, ClientTeamRegitemViewDto dto, int EditType)
        {
            var customerReg = _customerRegRepository.GetAll().Where(o => o.ClientTeamInfoId == TeamId && o.RegisterState == (int)RegisterState.No && o.CheckSate == (int)ProjectIState.Not);
            if (customerReg != null && customerReg.Count() > 0)
            {
                //减项，删除人员下的项目
                if (EditType == (int)EditModeType.Delete)
                {
                    var cusRegId = customerReg.Select(o => o.Id);
                    var cusItem = _CustomerRegItemRepository.GetAll().Where(o => o.ItemId == dto.Id && cusRegId.Contains(o.CustomerRegId));
                    var cusResult = cusItem.Select(o => o.Id);
                    if (cusResult.Any())
                        foreach (var item in cusItem)
                        {
                            _CustomerRegItemRepository.Delete(item);
                        }

                    var cusGroup = _CustomerItemGroup.GetAll().Where(o => o.ItemGroupBM_Id == dto.TbmItemGroupid && cusRegId.Contains(o.CustomerRegBMId.Value));
                    var groupResult = cusGroup.Select(o => o.Id);
                    if (groupResult.Any())
                        foreach (var item in groupResult)
                        {
                            _CustomerItemGroup.Delete(item);
                        }
                }
                //加项，每个人员下面增加项目
                else if (EditType == (int)EditModeType.Add)
                {
                    var itemGroup = _ItemGroup.Get(dto.TbmItemGroupid.Value);
                    EntityDto<Guid> input = new EntityDto<Guid>();
                    input.Id = itemGroup.DepartmentId;
                    var depart = _departmentRepository.GetById(input);
                    foreach (var item in customerReg)
                    {
                        //判断此人是否已有此项目，如果已有加减项改为正常
                        if (_CustomerItemGroup.GetAll().Any(o => o.CustomerRegBMId == item.Id && o.ItemGroupBM_Id == itemGroup.Id))
                        {
                            var cusItemGroup = _CustomerItemGroup.FirstOrDefault(o => o.CustomerRegBMId == item.Id && o.ItemGroupBM_Id == itemGroup.Id);
                            cusItemGroup.IsAddMinus = (int)AddMinusType.Normal;
                            _CustomerItemGroup.Update(cusItemGroup);
                        }
                        else
                        {
                            var cusGroup = new TjlCustomerItemGroup();
                            cusGroup.ItemGroupBM_Id = itemGroup.Id;
                            cusGroup.ItemGroupCodeBM = itemGroup.ItemGroupBM;
                            cusGroup.SFType = Convert.ToInt32(itemGroup.ChartCode);
                            cusGroup.ItemPrice = dto.ItemGroupMoney;
                            cusGroup.PriceAfterDis = dto.ItemGroupDiscountMoney;
                            cusGroup.ItemGroupName = dto.ItemGroupName;
                            cusGroup.DiscountRate = dto.Discount;
                            if (dto.PayerCatType == (int)PayerCatType.PersonalCharge)
                                cusGroup.GRmoney = dto.ItemGroupDiscountMoney;
                            else if (dto.PayerCatType == (int)PayerCatType.ClientCharge)
                                cusGroup.TTmoney = dto.ItemGroupDiscountMoney;
                            cusGroup.IsAddMinus = (int)AddMinusType.Normal;
                            cusGroup.ItemGroupOrder = itemGroup.OrderNum;
                            cusGroup.PayerCat = dto.PayerCatType; //(int)PayerCatType.NoCharge;
                            cusGroup.ItemSuitId = dto.ItemSuitId;
                            cusGroup.ItemSuitName = dto.ItemSuitName;
                            if (depart != null)
                            {
                                cusGroup.DepartmentId = depart.Id;
                                cusGroup.DepartmentName = depart.Name;
                                cusGroup.DepartmentOrder = depart.OrderNum;
                                cusGroup.DepartmentCodeBM = depart.DepartmentBM;
                            }
                            cusGroup.CheckState = (int)ProjectIState.Not;
                            cusGroup.CustomerRegBMId = item.Id;
                            _CustomerItemGroup.Insert(cusGroup);
                        }
                    }


                    //var group = new TjlCustomerItemGroupDto();
                    //group.ItemGroupBM_Id = itemGroup.Id;
                    //group.ItemPrice = dto.ItemGroupMoney;
                    //group.PriceAfterDis = dto.ItemGroupDiscountMoney;
                    //group.ItemGroupName = dto.ItemGroupName;
                    //group.DiscountRate = dto.Discount;
                    //if (dto.PayerCatType == (int)PayerCatType.PersonalCharge)
                    //    group.GRmoney = dto.ItemGroupDiscountMoney;
                    //else if (dto.PayerCatType == (int)PayerCatType.ClientCharge)
                    //    group.TTmoney = dto.ItemGroupDiscountMoney;
                    //group.IsAddMinus = (int)AddMinusType.Normal;
                    //group.ItemGroupOrder = dto.ItemGroup.OrderNum;
                    //group.PayerCat = dto.PayerCatType; //(int)PayerCatType.NoCharge;
                    //group.ItemSuitId = dto.ItemSuitId;
                    //group.ItemSuitName = dto.ItemSuitName;
                    //var depart = _departmentRepository.GetById(new EntityDto<Guid>() { Id = itemGroup.Id });
                    //if (depart != null)
                    //{
                    //    group.DepartmentId = depart.Id;
                    //    group.DepartmentName = depart.Name;
                    //    group.DepartmentOrder = depart.OrderNum;

                    //}
                    //group.CheckState = (int)ProjectIState.Not;
                    //_CustomerRegItemRepository.Insert(group.MapTo<TjlCustomerRegItem>());
                }


            }
        }

        /// <summary>
        /// 同步分组项目
        /// </summary>
        public void SynClientCusItem(List<ClientTeamRegitemViewDto> dtolist)
        {
            var teamlist = dtolist.Select(o => o.ClientTeamInfoId).Distinct().ToList();
            var customerReg = _customerRegRepository.GetAll().Where(o => teamlist.Contains(o.ClientTeamInfoId) && o.RegisterState == (int)RegisterState.No && o.CheckSate == (int)ProjectIState.Not).ToList();
            if (customerReg != null && customerReg.Count() > 0)
            {

                foreach (var dto in dtolist)
                {
                    var cusreglis = customerReg.Where(o => o.ClientTeamInfoId == dto.ClientTeamInfoId).ToList();
                    if (cusreglis.Count == 0)
                    {
                        continue;
                    }
                    //var cusItems = cusreglis.SelectMany(o=>o.CustomerRegItems).ToList();
                    //减项，删除人员下的项目
                    if (dto.IsAddMinus == (int)AddMinusType.Minus)
                    {
                        var cusRegId = cusreglis.Select(o => o.Id).ToList();
                        string regIds = string.Join("','", cusRegId);
                        SqlParameter[] parameter = {
                         new SqlParameter("@ItemGroupBMId", dto.TbmItemGroupid)
                };
                        string delcusitemSql = string.Format(@"update TjlCustomerRegItems set IsDeleted=1 
where ItemGroupBMId=@ItemGroupBMId and CustomerRegId in ('{0}'); " +
                            "update TjlCustomerItemGroups set IsDeleted=1 where ItemGroupBM_Id=@ItemGroupBMId " +
                            "and CustomerRegBMId in ('{0}')", regIds);

                        _sqlExecutor.Execute(delcusitemSql, parameter);
                        ////删除项目
                        //var cusItem = _CustomerRegItemRepository.GetAll().Where(o => cusRegId.Contains(o.CustomerRegId) && o.ItemGroupBMId == dto.TbmItemGroupid);
                        //if (cusItem.Count() > 0)
                        //{
                        //    cusItem.Delete();
                        //}

                        ////删除组合
                        //_CustomerItemGroup.GetAll().Where(o => o.ItemGroupBM_Id == dto.TbmItemGroupid && cusRegId.Contains(o.CustomerRegBMId.Value)).Delete();

                    }
                    //加项，每个人员下面增加项目
                    else if (dto.IsAddMinus == (int)AddMinusType.Add)
                    {

                        var cusRegId = cusreglis.Select(o => o.Id).ToList();
                        string regIds = string.Join("','", cusRegId);
                        bool isSuit = true;
                        if (dto.ItemSuitId == null)
                        {
                            isSuit = false;
                        }
                        decimal GRmoney = 0;
                        decimal TTmoney = 0;
                        if (dto.PayerCatType == (int)PayerCatType.PersonalCharge)
                            GRmoney = dto.ItemGroupDiscountMoney;
                        else if (dto.PayerCatType == (int)PayerCatType.ClientCharge)
                            TTmoney = dto.ItemGroupDiscountMoney;
                        SqlParameter[] parameter = {
                         new SqlParameter("@ItemGroupBM_Id", dto.TbmItemGroupid),
                         new SqlParameter("@ItemGroupCodeBM", dto.ItemGroupCodeBM??""),
                         new SqlParameter("@ItemPrice", dto.ItemGroupMoney),
                         new SqlParameter("@PriceAfterDis", dto.ItemGroupDiscountMoney),
                         new SqlParameter("@ItemGroupName", dto.ItemGroupName),
                         new SqlParameter("@DiscountRate", dto.Discount),
                         new SqlParameter("@GRmoney",GRmoney),
                         new SqlParameter("@TTmoney", TTmoney),
                         new SqlParameter("@IsAddMinus", (int)AddMinusType.Normal),
                          new SqlParameter("@ItemGroupOrder", dto.ItemGroupOrder??0),
                         new SqlParameter("@PayerCat", dto.PayerCatType),

                         new SqlParameter("@DepartmentId", dto.TbmDepartmentid.Value),
                         new SqlParameter("@DepartmentName",  dto.DepartmentName),
                         new SqlParameter("@DepartmentOrder",  dto.DepartmentOrder??0),
                         new SqlParameter("@DepartmentCodeBM", dto.DepartmentCodeBM??""),
                         new SqlParameter("@CheckState", (int)ProjectIState.Not)    ,
                          new SqlParameter("@TenantId", AbpSession.TenantId)    ,
                             new SqlParameter("@CreatorUserId", AbpSession.UserId)

                };
                        string delcusitemSql = "update TjlCustomerItemGroups set IsAddMinus = 1  " +
                            "where CustomerRegBMId in ('" + regIds + "') and ItemGroupBM_Id = @ItemGroupBM_Id;";
                        delcusitemSql += string.Format(@"insert into TjlCustomerItemGroups( [Id]
      ,[CustomerRegBMId]
      ,[DepartmentId]
      ,[DepartmentName]
      ,[DepartmentCodeBM]
      ,[DepartmentOrder]
      ,[ItemGroupBM_Id]
      ,[ItemGroupCodeBM]
      ,[ItemGroupName]
      ,[ItemGroupOrder] 
      ,[CheckState]
      ,[SummBackSate] 
      ,[IsAddMinus] 
      ,[ItemPrice]
      ,[DiscountRate]
      ,[PriceAfterDis]
      ,[PayerCat]
      ,[TTmoney]
      ,[GRmoney]  
 
  
      ,[TenantId]
      ,[IsDeleted]      
      ,[CreationTime]
      ,[CreatorUserId]     
) 
select NEWID(),Id  ,@DepartmentId
      ,@DepartmentName
      ,@DepartmentCodeBM
      ,@DepartmentOrder
      ,@ItemGroupBM_Id
      ,@ItemGroupCodeBM
      ,@ItemGroupName
      ,@ItemGroupOrder 
      ,@CheckState
      ,1      
      ,@IsAddMinus 
      ,@ItemPrice
      ,@DiscountRate
      ,@PriceAfterDis
      ,@PayerCat
      ,@TTmoney
      ,@GRmoney
      ,@TenantId
      ,0
      ,GETDATE()
      ,@CreatorUserId  from TjlCustomerRegs where Id in ('{0}') and id not in 
(select distinct  id  from TjlCustomerItemGroups   where CustomerRegBMId in ('{0}') 
and ItemGroupBM_Id=@ItemGroupBM_Id)", regIds);

                        _sqlExecutor.Execute(delcusitemSql, parameter);
                        //foreach (var item in cusreglis)
                        //{
                        //    //判断此人是否已有此项目，如果已有加减项改为正常
                        //    if (item.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == dto.TbmItemGroupid))
                        //    {
                        //        var cusItemGroup = _CustomerItemGroup.FirstOrDefault(o => o.CustomerRegBMId == item.Id && o.ItemGroupBM_Id == dto.TbmItemGroupid);
                        //        cusItemGroup.IsAddMinus = (int)AddMinusType.Normal;
                        //        _CustomerItemGroup.Update(cusItemGroup);
                        //    }
                        //    else
                        //    {
                        //        var cusGroup = new TjlCustomerItemGroup();
                        //        cusGroup.ItemGroupBM_Id = dto.TbmItemGroupid;
                        //        cusGroup.ItemGroupCodeBM = dto.ItemGroupCodeBM;
                        //       // cusGroup.SFType = Convert.ToInt32(dto.ItemGroup.ChartCode);
                        //        cusGroup.ItemPrice = dto.ItemGroupMoney;
                        //        cusGroup.PriceAfterDis = dto.ItemGroupDiscountMoney;
                        //        cusGroup.ItemGroupName = dto.ItemGroupName;
                        //        cusGroup.DiscountRate = dto.Discount;
                        //        if (dto.PayerCatType == (int)PayerCatType.PersonalCharge)
                        //            cusGroup.GRmoney = dto.ItemGroupDiscountMoney;
                        //        else if (dto.PayerCatType == (int)PayerCatType.ClientCharge)
                        //            cusGroup.TTmoney = dto.ItemGroupDiscountMoney;
                        //        cusGroup.IsAddMinus = (int)AddMinusType.Normal;
                        //        cusGroup.ItemGroupOrder = dto.ItemGroupOrder;
                        //        cusGroup.PayerCat = dto.PayerCatType; //(int)PayerCatType.NoCharge;
                        //        cusGroup.ItemSuitId = dto.ItemSuitId;
                        //        cusGroup.ItemSuitName = dto.ItemSuitName;
                        //        cusGroup.DepartmentId = dto.TbmDepartmentid.Value;
                        //        cusGroup.DepartmentName = dto.DepartmentName;
                        //        cusGroup.DepartmentOrder = dto.DepartmentOrder;
                        //        cusGroup.DepartmentCodeBM = dto.DepartmentCodeBM;                               
                        //        cusGroup.CheckState = (int)ProjectIState.Not;
                        //        cusGroup.CustomerRegBMId = item.Id;
                        //        _CustomerItemGroup.Insert(cusGroup);
                        //    }
                        //}

                    }
                }

            }
        }


        /// <summary>
        /// 单位预约信息查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PageResultDto<FullClientRegDto> PageFulls(PageInputDto<ClientRegSelectDto> input)
        {
            var query = _clientRegRepository.GetAll().AsNoTracking();
            if (input.Input.Id != Guid.Empty)
            {
                query = query.Where(r => r.Id == input.Input.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(input.Input.ClientRegBM))
                    query = query.Where(o => o.ClientRegBM == input.Input.ClientRegBM);

                if (input.Input.ClientInfo_Id != null && input.Input.ClientInfo_Id != Guid.Empty)
                    query = query.Where(o => o.ClientInfo.Id == input.Input.ClientInfo_Id);

                if (input.Input.StartCheckDate.HasValue)
                    query = query.Where(o => o.StartCheckDate >= input.Input.StartCheckDate.Value);

                if (input.Input.EndCheckDate.HasValue)
                    query = query.Where(o => o.EndCheckDate <= input.Input.EndCheckDate.Value);

                if (input.Input.FZState.HasValue)
                    query = query.Where(o => o.FZState == input.Input.FZState.Value);

                if (input.Input.SDState.HasValue)
                    query = query.Where(o => o.SDState == input.Input.SDState.Value);

                if (input.Input.ClientSate.HasValue)
                    query = query.Where(o => o.ClientSate == input.Input.ClientSate.Value);

                if (input.Input.ClientCheckSate.HasValue && input.Input.ClientCheckSate != 0)
                    query = query.Where(o => o.ClientCheckSate == input.Input.ClientCheckSate);
            }
            var userBM = _User.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea==null);
            }

            query = query.OrderByDescending(r => r.CreationTime);

            var result = new PageResultDto<FullClientRegDto> { CurrentPage = input.CurentPage };
            result.Calculate(query.Count());
            query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);

            query = query.Include(r => r.ClientInfo);
            //query = query.Include(r => r.CustomerReg.Select(u => u.McusPayMoney));
            query = query.Include(r => r.user);

            //var res = query.Select(o => new { o.Id, o.ClientRegBM,o.ClientInfo,o.ClientCheckSate });

            //var queryResult = res.Select(o => new FullClientRegDto
            //{ Id = o.Id, ClientRegBM = o.ClientRegBM, ClientCheckSate = o.ClientCheckSate, ClientInfo= new ClientInfoesRegsDto { Id=o.ClientInfo.Id,ClientName=o.ClientInfo.ClientName} }).ToList() ;

            var queryResult = query.ToList();
            result.Result = queryResult.MapTo<List<FullClientRegDto>>();
            return result;
        }

        /// <summary>
        /// 根据分组Id查询分组信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ClientTeamInfosDto> GetClientTeamInfosById(List<Guid> input)
        {
            var dto = new List<TjlClientTeamInfo>();
            foreach (var item in input)
                dto.AddRange(_ClientTeamInfo.GetAll().Where(o => o.Id == item));
            return dto.MapTo<List<ClientTeamInfosDto>>();
        }

        /// <summary>
        /// 根据分组Id查询分组信息 体检人预约项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void GetClientTeamById(SearchClientTeamIdorTeamRegDto input)
        {
            var singclient = _ClientTeamInfo.Get(input.Id);
            foreach (var item in singclient.CustomerReg)
            {
                decimal personalPay = 0; //个人应收
                decimal clientMoney = 0; //团体应收
                decimal personalAdd = 0; //个人加项
                decimal personalMinusMoney = 0; //个人减项
                decimal clientAdd = 0; //团体加项
                decimal clientMinusMoney = 0; //团体减项
                foreach (var group in item.CustomerItemGroup)
                {

                    var varitem = input.ClientTeamRegItem.Where(o => o.ItemGroup.Id == group.ItemGroupBM_Id && o.ClientTeamInfoId == item.ClientTeamInfoId).FirstOrDefault();

                    if (varitem != null)
                    {
                        var dto = _CustomerItemGroup.Get(group.Id);
                        group.DiscountRate = varitem.Discount;
                        group.PriceAfterDis = varitem.ItemGroupDiscountMoney;
                        group.PayerCat = varitem.PayerCatType;
                        if (group.PayerCat == 2)
                        {
                            group.GRmoney = varitem.ItemGroupDiscountMoney;
                            group.TTmoney = 0;
                        }
                        if (group.PayerCat == 3)
                        {
                            group.TTmoney = varitem.ItemGroupDiscountMoney;
                            group.GRmoney = 0;
                        }
                        group.MapTo(dto);
                        _CustomerItemGroup.Update(dto);
                    }
                    //已收费的项目不会被删除也不作处理，新加分组数据添加至数据库
                    personalPay += group.GRmoney;
                    clientMoney += group.TTmoney;
                    if (group.IsAddMinus == (int)AddMinusType.Add)
                    {
                        //加项
                        personalAdd += group.GRmoney;
                        clientAdd += group.TTmoney;
                    }
                    else if (group.IsAddMinus == (int)AddMinusType.Minus)
                    {
                        //减项
                        clientMinusMoney += group.GRmoney;
                        personalMinusMoney += group.TTmoney;
                    }

                }
                //组合增加的项目处理
                List<Guid?> groupls = item.CustomerItemGroup.Select(o => o.ItemGroupBM_Id).ToList();
                var clientgroups = input.ClientTeamRegItem.Where(o => !groupls.Contains(o.TbmItemGroupid));

                foreach (var clientgroup in clientgroups)
                {
                    var tjlCustomerGroup = new TjlCustomerItemGroup();
                    tjlCustomerGroup.Id = Guid.NewGuid();
                    tjlCustomerGroup.BarState = 1;
                    tjlCustomerGroup.BillingEmployeeBM = null;
                    tjlCustomerGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
                    tjlCustomerGroup.CheckState = 1;
                    tjlCustomerGroup.CollectionState = 1;
                    tjlCustomerGroup.CustomerRegBM = null;
                    tjlCustomerGroup.CustomerRegBMId = item.Id;
                    tjlCustomerGroup.DepartmentBM = null;
                    tjlCustomerGroup.DepartmentId = clientgroup.TbmDepartmentid.Value;
                    tjlCustomerGroup.DepartmentName = clientgroup.DepartmentName;
                    tjlCustomerGroup.DepartmentCodeBM = clientgroup.DepartmentCodeBM;
                    tjlCustomerGroup.DepartmentOrder = clientgroup.DepartmentOrder;
                    tjlCustomerGroup.DiscountRate = 1;
                    tjlCustomerGroup.DrawSate = 1;
                    tjlCustomerGroup.PriceAfterDis = clientgroup.ItemGroupDiscountMoney;
                    tjlCustomerGroup.PayerCat = clientgroup.PayerCatType;
                    if (singclient.CostType == 2)
                    {
                        tjlCustomerGroup.GRmoney = clientgroup.ItemGroupDiscountMoney;
                        tjlCustomerGroup.TTmoney = 0;
                        //tjlCustomerGroup.PayerCat = 2;
                        tjlCustomerGroup.PayerCat = (int)PayerCatType.NoCharge;


                    }
                    if (singclient.CostType == 3)
                    {
                        tjlCustomerGroup.TTmoney = clientgroup.ItemGroupDiscountMoney;
                        tjlCustomerGroup.GRmoney = 0;
                        //tjlCustomerGroup.PayerCat = 3;    
                        tjlCustomerGroup.PayerCat = (int)PayerCatType.ClientCharge;
                    }
                    tjlCustomerGroup.GuidanceSate = 1;
                    tjlCustomerGroup.IsAddMinus = 1;
                    tjlCustomerGroup.ItemGroupBM = null;
                    tjlCustomerGroup.ItemGroupBM_Id = clientgroup.TbmItemGroupid;
                    tjlCustomerGroup.ItemGroupCodeBM = clientgroup.ItemGroupCodeBM;
                    tjlCustomerGroup.ItemGroupName = clientgroup.ItemGroupName;
                    tjlCustomerGroup.ItemGroupOrder = clientgroup.ItemGroupOrder;
                    tjlCustomerGroup.ItemPrice = clientgroup.ItemGroupMoney;
                    int sfzt = (int)PayerCatType.NotRefund;
                    tjlCustomerGroup.RefundState = sfzt;
                    tjlCustomerGroup.RequestState = 1;
                    if (clientgroup.ItemGroup.ChartCode != null)
                    {
                        if (Regex.IsMatch(clientgroup.ItemGroup.ChartCode, @"^[+-]?\d*[.]?\d*$"))
                            tjlCustomerGroup.SFType = int.Parse(clientgroup.ItemGroup.ChartCode);
                    }
                    tjlCustomerGroup.SummBackSate = 1;
                    tjlCustomerGroup.SuspendState = 1;
                    tjlCustomerGroup.TTmoney = 0;
                    TjlCustomerItemGroup tjlCustomerItemGroupls = _CustomerItemGroup.Insert(tjlCustomerGroup);
                    personalPay += tjlCustomerGroup.GRmoney;
                    clientMoney += tjlCustomerGroup.TTmoney;
                }
                //抹零项处理
                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                var mlgroupl = item.CustomerItemGroup.FirstOrDefault(o => o.ItemGroupBM_Id == guid);
                var mlclientgroup = input.ClientTeamRegItem.FirstOrDefault(o => o.TbmItemGroupid == guid);
                if (mlgroupl != null && mlclientgroup == null)
                {
                    var dto = _CustomerItemGroup.Get(mlgroupl.Id);
                    personalPay = personalPay - dto.GRmoney;
                    clientMoney = clientMoney - dto.TTmoney;

                    _CustomerItemGroup.Delete(dto);
                }

                var payinfo = _mcusPayMoneyRepository.FirstOrDefault(o => o.CustomerReg.Id == item.Id);
                if (payinfo == null)
                {
                    _mcusPayMoneyRepository.Insert(new TjlMcusPayMoney
                    {
                        //Id = Guid.NewGuid(),
                        Customer = item.Customer,
                        CustomerReg = item,
                        ClientInfo = item.ClientInfo,
                        ClientReg = item.ClientReg,
                        ClientTeamInfo = item.ClientTeamInfo,
                        PersonalAddMoney = personalAdd,
                        PersonalMinusMoney = personalMinusMoney,
                        ClientAdjustAddMoney = clientAdd,
                        ClientAdjustMinusMoney = clientMinusMoney,
                        PersonalMoney = personalPay,
                        ClientMoney = clientMoney,
                        PersonalPayMoney = 0
                    });
                }
                else
                {
                    if (payinfo.PersonalMoney != personalPay || payinfo.ClientMoney != clientMoney ||
                        payinfo.PersonalAddMoney != personalAdd || payinfo.ClientAddMoney != personalAdd ||
                        payinfo.ClientMinusMoney != personalMinusMoney)
                    {
                        payinfo.PersonalMoney = personalPay;
                        payinfo.ClientMoney = clientMoney;
                        payinfo.PersonalAddMoney = personalAdd;
                        payinfo.PersonalMinusMoney = personalMinusMoney;
                        payinfo.ClientAddMoney = clientAdd;
                        payinfo.ClientMinusMoney = clientMinusMoney;
                        _mcusPayMoneyRepository.Update(payinfo);
                    }
                }
            }
        }

        /// <summary>
        ///同步金额
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void UpClientCostById(SearchClientTeamIdorTeamRegDto input)
        {
            //更新已有价格
            SqlParameter[] parameter = {
                         new SqlParameter("@ClientTeamInfoId", input.Id)
                };
            string delcusitemSql = string.Format(@"update TjlCustomerItemGroups set DiscountRate=TjlClientTeamRegitems.Discount,
PriceAfterDis=TjlClientTeamRegitems.ItemGroupDiscountMoney,
PayerCat=TjlClientTeamRegitems.PayerCatType,
GRmoney=case TjlClientTeamRegitems.PayerCatType when 3 then 0 else TjlClientTeamRegitems.ItemGroupDiscountMoney end,
TTmoney=case TjlClientTeamRegitems.PayerCatType when 3 then TjlClientTeamRegitems.ItemGroupDiscountMoney else 0  end
from TjlCustomerItemGroups,TjlClientTeamRegitems, TjlCustomerRegs
 where TjlClientTeamRegitems.ClientTeamInfoId=@ClientTeamInfoId 
 and ItemGroupBM_Id=TjlClientTeamRegitems.TbmItemGroupId 
and TjlCustomerRegs.Id=TjlCustomerItemGroups.CustomerRegBMId 
and TjlCustomerRegs.ClientTeamInfoId=TjlClientTeamRegitems.ClientTeamInfoId and TjlClientTeamRegitems.IsDeleted=0 and TjlCustomerItemGroups.IsDeleted=0");
            _sqlExecutor.Execute(delcusitemSql, parameter);

            //抹零项目
            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
            var dto = _Regitem.FirstOrDefault(o => o.ClientTeamInfoId == input.Id
            && o.TbmItemGroupId == guid);
            var cusRegId = _customerRegRepository.GetAll().Where(o => o.ClientTeamInfoId == input.Id)
                   .Select(o => o.Id).ToList();
            string regIds = string.Join("','", cusRegId);
            if (dto != null && regIds != "")
            {
                string delet = string.Format(@"update TjlCustomerItemGroups set  IsDeleted=1 where     CustomerRegBMId in ('{0}') 
and ItemGroupBM_Id = '866488A7-2630-429C-8DF4-905B8A5FF734'", regIds);

                _sqlExecutor.Execute(delet);
                decimal GRmoney = 0;
                decimal TTmoney = 0;
                if (dto.PayerCatType == (int)PayerCatType.PersonalCharge)
                    GRmoney = dto.ItemGroupDiscountMoney;
                else if (dto.PayerCatType == (int)PayerCatType.ClientCharge)
                    TTmoney = dto.ItemGroupDiscountMoney;
                SqlParameter[] parameter1 = {
                         new SqlParameter("@ItemGroupBM_Id", dto.TbmItemGroupId),
                         new SqlParameter("@ItemGroupCodeBM", dto.ItemGroupCodeBM??""),
                         new SqlParameter("@ItemPrice", dto.ItemGroupMoney),
                         new SqlParameter("@PriceAfterDis", dto.ItemGroupDiscountMoney),
                         new SqlParameter("@ItemGroupName", dto.ItemGroupName),
                         new SqlParameter("@DiscountRate", dto.Discount),
                         new SqlParameter("@GRmoney",GRmoney),
                         new SqlParameter("@TTmoney", TTmoney),
                         new SqlParameter("@IsAddMinus", (int)AddMinusType.Normal),
                          new SqlParameter("@ItemGroupOrder", dto.ItemGroupOrder??0),
                         new SqlParameter("@PayerCat", dto.PayerCatType),

                         new SqlParameter("@DepartmentId", dto.TbmDepartmentId.Value),
                         new SqlParameter("@DepartmentName",  dto.DepartmentName),
                         new SqlParameter("@DepartmentOrder",  dto.DepartmentOrder??0),
                         new SqlParameter("@DepartmentCodeBM", dto.DepartmentCodeBM??""),
                         new SqlParameter("@CheckState", (int)ProjectIState.Not)    ,
                          new SqlParameter("@TenantId", AbpSession.TenantId)    ,
                             new SqlParameter("@CreatorUserId", AbpSession.UserId)

                };

                string MLcusitemSql = string.Format(@"insert into TjlCustomerItemGroups( [Id]
      ,[CustomerRegBMId]
      ,[DepartmentId]
      ,[DepartmentName]
      ,[DepartmentCodeBM]
      ,[DepartmentOrder]
      ,[ItemGroupBM_Id]
      ,[ItemGroupCodeBM]
      ,[ItemGroupName]
      ,[ItemGroupOrder] 
      ,[CheckState]
      ,[SummBackSate] 
      ,[IsAddMinus] 
      ,[ItemPrice]
      ,[DiscountRate]
      ,[PriceAfterDis]
      ,[PayerCat]
      ,[TTmoney]
      ,[GRmoney]  
 
  
      ,[TenantId]
      ,[IsDeleted]      
      ,[CreationTime]
      ,[CreatorUserId]     
) 
select NEWID(),Id  ,@DepartmentId
      ,@DepartmentName
      ,@DepartmentCodeBM
      ,@DepartmentOrder
      ,@ItemGroupBM_Id
      ,@ItemGroupCodeBM
      ,@ItemGroupName
      ,@ItemGroupOrder 
      ,@CheckState
      ,1      
      ,@IsAddMinus 
      ,@ItemPrice
      ,@DiscountRate
      ,@PriceAfterDis
      ,@PayerCat
      ,@TTmoney
      ,@GRmoney
      ,@TenantId
      ,0
      ,GETDATE()
      ,@CreatorUserId  from TjlCustomerRegs where Id in ('{0}') ", regIds);

                _sqlExecutor.Execute(MLcusitemSql, parameter1);
            }
            //同步金额            
            var client = _ClientTeamInfo.Get(input.Id);
            //新增价格
            SqlParameter[] parameter2 = {
                         new SqlParameter("@CreatorUserId",  AbpSession.UserId),
                          new SqlParameter("@ClientInfo_Id", client.ClientReg.ClientInfoId),
                           new SqlParameter("@ClientReg_Id", client.ClientRegId),
                            new SqlParameter("@ClientTeamInfo_Id", input.Id)

                };
            string MoneycusitemSql = string.Format(@"  insert into [TjlMcusPayMoneys]
 ( [Id]
      ,[PersonalMoney]
      ,[PersonalAddMoney]
      ,[PersonalMinusMoney]
      ,[PersonalAdjustAddMoney]
      ,[PersonalAdjustMinusMoney]
      ,[PersonalPayMoney]
      ,[ClientMoney]
      ,[ClientAddMoney]
      ,[ClientMinusMoney]
      ,[ClientAdjustAddMoney]
      ,[ClientAdjustMinusMoney]
      ,[TenantId]
      ,[IsDeleted] 
      ,[CreationTime]
      ,[CreatorUserId]
      ,[ClientInfo_Id]
      ,[ClientReg_Id]
      ,[ClientTeamInfo_Id]
      ,[Customer_Id]
	  )
 select TjlCustomerItemGroups.CustomerRegBMId,SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else GRmoney end),
  SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then GRmoney else 0 end) ,
    SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then GRmoney else 0 end) ,
	0,0,0,
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else TTmoney end),
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then TTmoney else 0 end),
	  SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then TTmoney else 0 end),
	  0,0,2,0,GETDATE()	 ,  
	  @CreatorUserId,@ClientInfo_Id,@ClientReg_Id,@ClientTeamInfo_Id,	 
	   TjlCustomerRegs.CustomerId
  from TjlCustomerItemGroups ,TjlCustomerRegs where
    TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=TjlCustomerRegs.Id and 
   CustomerRegBMId in (select TjlCustomerRegs.Id  from TjlCustomerRegs 
 left join TjlMcusPayMoneys  on TjlCustomerRegs.Id=TjlMcusPayMoneys.Id where 
 TjlCustomerRegs.ClientTeamInfoId=@ClientTeamInfo_Id and
  TjlMcusPayMoneys.id is null)
 group by CustomerRegBMId ,TjlCustomerRegs.CustomerId
");
            _sqlExecutor.Execute(MoneycusitemSql, parameter2);

            //更新已有价格
            SqlParameter[] parameter3 = {
                         new SqlParameter("@ClientTeamInfo_Id", input.Id)
                };
            string upMcusitemSql = string.Format(@"update [TjlMcusPayMoneys] set 
[PersonalMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else GRmoney end) from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[PersonalAddMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then GRmoney else 0 end)  from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[PersonalMinusMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then GRmoney else 0 end) from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
     
      ,[ClientMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else TTmoney end) from TjlCustomerItemGroups where  TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[ClientAddMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then TTmoney else 0 end) from TjlCustomerItemGroups where  TjlCustomerItemGroups.IsDeleted=0 and TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)
      ,[ClientMinusMoney]=(select SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then TTmoney else 0 end) from TjlCustomerItemGroups where TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=[TjlMcusPayMoneys].Id)  
	 where id in(select Id  from TjlCustomerRegs where ClientTeamInfoId=@ClientTeamInfo_Id and IsDeleted =0)");
            _sqlExecutor.Execute(upMcusitemSql, parameter3);
            #region MyRegion 注释
            //var singclient = _ClientTeamInfo.Get(input.Id);
            //foreach (var item in singclient.CustomerReg)
            //{
            //    decimal personalPay = 0; //个人应收
            //    decimal clientMoney = 0; //团体应收
            //    decimal personalAdd = 0; //个人加项
            //    decimal personalMinusMoney = 0; //个人减项
            //    decimal clientAdd = 0; //团体加项
            //    decimal clientMinusMoney = 0; //团体减项
            //    foreach (var group in item.CustomerItemGroup)
            //    {

            //        var varitem = input.ClientTeamRegItem.Where(o => o.ItemGroup.Id == group.ItemGroupBM_Id && o.ClientTeamInfoId == item.ClientTeamInfoId).FirstOrDefault();

            //        if (varitem != null)
            //        {
            //            var dto = _CustomerItemGroup.Get(group.Id);
            //            group.DiscountRate = varitem.Discount;
            //            group.PriceAfterDis = varitem.ItemGroupDiscountMoney;
            //            group.PayerCat = varitem.PayerCatType;
            //            if (group.PayerCat == 2)
            //            {
            //                group.GRmoney = varitem.ItemGroupDiscountMoney;
            //                group.TTmoney = 0;
            //            }
            //            if (group.PayerCat == 3)
            //            {
            //                group.TTmoney = varitem.ItemGroupDiscountMoney;
            //                group.GRmoney = 0;
            //            }
            //            group.MapTo(dto);
            //            _CustomerItemGroup.Update(dto);
            //        }
            //        //已收费的项目不会被删除也不作处理，新加分组数据添加至数据库
            //        personalPay += group.GRmoney;
            //        clientMoney += group.TTmoney;
            //        if (group.IsAddMinus == (int)AddMinusType.Add)
            //        {
            //            //加项
            //            personalAdd += group.GRmoney;
            //            clientAdd += group.TTmoney;
            //        }
            //        else if (group.IsAddMinus == (int)AddMinusType.Minus)
            //        {
            //            //减项
            //            clientMinusMoney += group.GRmoney;
            //            personalMinusMoney += group.TTmoney;
            //        }

            //    }
            //    //组合增加的项目处理
            //    List<Guid?> groupls = item.CustomerItemGroup.Select(o => o.ItemGroupBM_Id).ToList();
            //    var clientgroups = input.ClientTeamRegItem.Where(o => !groupls.Contains(o.TbmItemGroupid));

            //    foreach (var clientgroup in clientgroups)
            //    {
            //        var tjlCustomerGroup = new TjlCustomerItemGroup();
            //        tjlCustomerGroup.Id = Guid.NewGuid();
            //        tjlCustomerGroup.BarState = 1;
            //        tjlCustomerGroup.BillingEmployeeBM = null;
            //        tjlCustomerGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
            //        tjlCustomerGroup.CheckState = 1;
            //        tjlCustomerGroup.CollectionState = 1;
            //        tjlCustomerGroup.CustomerRegBM = null;
            //        tjlCustomerGroup.CustomerRegBMId = item.Id;
            //        tjlCustomerGroup.DepartmentBM = null;
            //        tjlCustomerGroup.DepartmentId = clientgroup.TbmDepartmentid.Value;
            //        tjlCustomerGroup.DepartmentName = clientgroup.DepartmentName;
            //        tjlCustomerGroup.DepartmentCodeBM = clientgroup.DepartmentCodeBM;
            //        tjlCustomerGroup.DepartmentOrder = clientgroup.DepartmentOrder;
            //        tjlCustomerGroup.DiscountRate = 1;
            //        tjlCustomerGroup.DrawSate = 1;
            //        tjlCustomerGroup.PriceAfterDis = clientgroup.ItemGroupDiscountMoney;
            //        tjlCustomerGroup.PayerCat = clientgroup.PayerCatType;
            //        if (singclient.CostType == 2)
            //        {
            //            tjlCustomerGroup.GRmoney = clientgroup.ItemGroupDiscountMoney;
            //            tjlCustomerGroup.TTmoney = 0;
            //            //tjlCustomerGroup.PayerCat = 2;
            //            tjlCustomerGroup.PayerCat = (int)PayerCatType.NoCharge;


            //        }
            //        if (singclient.CostType == 3)
            //        {
            //            tjlCustomerGroup.TTmoney = clientgroup.ItemGroupDiscountMoney;
            //            tjlCustomerGroup.GRmoney = 0;
            //            //tjlCustomerGroup.PayerCat = 3;    
            //            tjlCustomerGroup.PayerCat = (int)PayerCatType.ClientCharge;
            //        }
            //        tjlCustomerGroup.GuidanceSate = 1;
            //        tjlCustomerGroup.IsAddMinus = 1;
            //        tjlCustomerGroup.ItemGroupBM = null;
            //        tjlCustomerGroup.ItemGroupBM_Id = clientgroup.TbmItemGroupid;
            //        tjlCustomerGroup.ItemGroupCodeBM = clientgroup.ItemGroupCodeBM;
            //        tjlCustomerGroup.ItemGroupName = clientgroup.ItemGroupName;
            //        tjlCustomerGroup.ItemGroupOrder = clientgroup.ItemGroupOrder;
            //        tjlCustomerGroup.ItemPrice = clientgroup.ItemGroupMoney;
            //        int sfzt = (int)PayerCatType.NotRefund;
            //        tjlCustomerGroup.RefundState = sfzt;
            //        tjlCustomerGroup.RequestState = 1;
            //        if (clientgroup.ItemGroup.ChartCode != null)
            //        {
            //            if (Regex.IsMatch(clientgroup.ItemGroup.ChartCode, @"^[+-]?\d*[.]?\d*$"))
            //                tjlCustomerGroup.SFType = int.Parse(clientgroup.ItemGroup.ChartCode);
            //        }
            //        tjlCustomerGroup.SummBackSate = 1;
            //        tjlCustomerGroup.SuspendState = 1;
            //        tjlCustomerGroup.TTmoney = 0;
            //        TjlCustomerItemGroup tjlCustomerItemGroupls = _CustomerItemGroup.Insert(tjlCustomerGroup);
            //        personalPay += tjlCustomerGroup.GRmoney;
            //        clientMoney += tjlCustomerGroup.TTmoney;
            //    }
            //    //抹零项处理
            //    Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
            //    var mlgroupl = item.CustomerItemGroup.FirstOrDefault(o => o.ItemGroupBM_Id == guid);
            //    var mlclientgroup = input.ClientTeamRegItem.FirstOrDefault(o => o.TbmItemGroupid == guid);
            //    if (mlgroupl != null && mlclientgroup == null)
            //    {
            //        var dto = _CustomerItemGroup.Get(mlgroupl.Id);
            //        personalPay = personalPay - dto.GRmoney;
            //        clientMoney = clientMoney - dto.TTmoney;

            //        _CustomerItemGroup.Delete(dto);
            //    }

            //    var payinfo = _mcusPayMoneyRepository.FirstOrDefault(o => o.CustomerReg.Id == item.Id);
            //    if (payinfo == null)
            //    {
            //        _mcusPayMoneyRepository.Insert(new TjlMcusPayMoney
            //        {
            //            //Id = Guid.NewGuid(),
            //            Customer = item.Customer,
            //            CustomerReg = item,
            //            ClientInfo = item.ClientInfo,
            //            ClientReg = item.ClientReg,
            //            ClientTeamInfo = item.ClientTeamInfo,
            //            PersonalAddMoney = personalAdd,
            //            PersonalMinusMoney = personalMinusMoney,
            //            ClientAdjustAddMoney = clientAdd,
            //            ClientAdjustMinusMoney = clientMinusMoney,
            //            PersonalMoney = personalPay,
            //            ClientMoney = clientMoney,
            //            PersonalPayMoney = 0
            //        });
            //    }
            //    else
            //    {
            //        if (payinfo.PersonalMoney != personalPay || payinfo.ClientMoney != clientMoney ||
            //            payinfo.PersonalAddMoney != personalAdd || payinfo.ClientAddMoney != personalAdd ||
            //            payinfo.ClientMinusMoney != personalMinusMoney)
            //        {
            //            payinfo.PersonalMoney = personalPay;
            //            payinfo.ClientMoney = clientMoney;
            //            payinfo.PersonalAddMoney = personalAdd;
            //            payinfo.PersonalMinusMoney = personalMinusMoney;
            //            payinfo.ClientAddMoney = clientAdd;
            //            payinfo.ClientMinusMoney = clientMinusMoney;
            //            _mcusPayMoneyRepository.Update(payinfo);
            //        }
            //    }
            //} 
            #endregion
        }

        /// <summary>
        /// 根据分组Id查询单位分组预约信息
        /// </summary>
        /// <returns></returns>
        public List<ClientTeamRegitemViewDto> GetClientTeamRegByClientId(SearchClientTeamInfoDto dto)
        {
            var teamId = Guid.Empty;
            if (dto.Id != null && dto.Id != Guid.Empty)
            {
                teamId = dto.Id; //_ClientTeamInfo.Get(dto.Id).Id;
            }
            else
            {
                if (dto.TeamBM.HasValue)
                {
                    var team = _ClientTeamInfo.FirstOrDefault(o => o.TeamBM == dto.TeamBM && o.ClientRegId == dto.ClientRegId);
                    if (team == null)
                        throw new FieldVerifyException("模板错误！", "未查到相关的分组信息");

                    if (dto.Age.HasValue)
                        if (team.MaxAge < dto.Age || team.MinAge > dto.Age)
                            throw new FieldVerifyException("模板错误！", "年龄超出范围");
                    if (dto.Sex.HasValue)
                        if (team.Sex != dto.Sex && team.Sex == (int)Sex.GenderNotSpecified)
                            throw new FieldVerifyException("模板错误！", "年龄超出范围");
                    if (dto.MaritalStatus.HasValue)
                        if (team.MaritalStatus != dto.MaritalStatus)
                            throw new FieldVerifyException("模板错误！", "结婚状态错误");
                    if (dto.ConceiveStatus.HasValue)
                        if (team.ConceiveStatus != dto.ConceiveStatus)
                            throw new FieldVerifyException("模板错误！", "是否备孕错误");
                    teamId = team.Id;
                }

                var CliTeamList = _ClientTeamInfo.GetAll();
                if (dto.ClientRegId != null && dto.ClientRegId != Guid.Empty)
                    CliTeamList = CliTeamList.Where(o => o.ClientRegId == dto.ClientRegId);
                if (dto.TeamBM != null)
                    CliTeamList = CliTeamList.Where(o => o.TeamBM == dto.TeamBM);
                if (dto.Age != null)
                    CliTeamList = CliTeamList.Where(o => o.MinAge < dto.Age && dto.Age < o.MaxAge);
                if (dto.Sex != null)
                    CliTeamList = CliTeamList.Where(o => o.Sex == dto.Sex || o.Sex == (int)Sex.GenderNotSpecified);
                if (dto.MaritalStatus != null)
                    CliTeamList = CliTeamList.Where(o => o.MaritalStatus == dto.MaritalStatus);
                if (dto.ConceiveStatus != null)
                    CliTeamList = CliTeamList.Where(o => o.ConceiveStatus == dto.ConceiveStatus);
                if (CliTeamList.Count() > 1)
                    throw new FieldVerifyException("模板错误！", "模板信息错误，请修改！");
                if (CliTeamList.Count() == 0)
                    throw new FieldVerifyException("模板错误！", "未查到相关的分组信息");

                teamId = CliTeamList.Select(o => o.Id).FirstOrDefault();
            }

            var RegItemList = _Regitem.GetAll().Where(o => o.ClientTeamInfoId == teamId);
            if (RegItemList == null || RegItemList.Count() == 0)
            {
                return new List<ClientTeamRegitemViewDto>();
                //throw new FieldVerifyException("项目分组错误！", "单位预约未添加检查项目");
            }

            return RegItemList.MapTo<List<ClientTeamRegitemViewDto>>();
        }

        /// <summary>
        /// 查询单位预约分组项目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ClientTeamRegitemViewDto> GetClientTeamRegItem(SearchClientTeamInfoDto dto)
        {
            var ctrItem = _Regitem.GetAll();
            if (dto.ClientRegId != null && dto.ClientRegId != Guid.Empty)
                ctrItem = ctrItem.Where(o => o.ClientRegId == dto.ClientRegId);
            return ctrItem.MapTo<List<ClientTeamRegitemViewDto>>();
        }


        /// <summary>
        /// 查询单位预约分组项目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ClientTeamRegitemViewDto> GetTeamRegItem(SearchClientTeamInfoDto dto)
        {
            var ctrItem = _Regitem.GetAll();
            if (dto.Id != null && dto.Id != Guid.Empty)
                ctrItem = ctrItem.Where(o => o.ClientTeamInfoId == dto.Id);
            return ctrItem.MapTo<List<ClientTeamRegitemViewDto>>();
        }

        //public List<ViewClientRegStatisticsOverTheYearsDto> QueryClientRegStatisticsOverTheYears(
        //    SearchClientRegStatisticsOverTheYearsDto input)
        //{
        //    if (input.ClientId == null)
        //        throw new FieldVerifyException("请先选择单位！", "请先选择单位！");
        //    if (input.StartYear == null || input.StartYear < 1949)
        //        throw new FieldVerifyException("请提供正确的开始年份！", "请提供正确的开始年份！");
        //    if (input.EndYear == null || input.EndYear < 1949)
        //        throw new FieldVerifyException("请提供正确的结束年份！", "请提供正确的结束年份！");

        //    var query = _clientRegRepository.GetAll().Where(m =>
        //        m.ClientInfo.Id == input.ClientId && m.StartCheckDate.Year >= input.StartYear.Value &&
        //        m.StartCheckDate.Year <= input.EndYear.Value);
        //    var group = query.GroupBy(m => new { m.ClientInfo, m.StartCheckDate.Year }).ToList();
        //    var list = group.Select(g => new ViewClientRegStatisticsOverTheYearsDto
        //    {
        //        ClientInfo = g.Key.ClientInfo.MapTo<ClientInfosViewDto>(),
        //        Year = g.Key.Year,
        //        RegPersonCount = query.Where(m => m.StartCheckDate.Year == g.Key.Year)
        //            .Sum(m => (long?)m.RegPersonCount ?? 0L)
        //    }).ToList();

        //    return list;
        //}
        public List<ViewClientRegStatisticsOverTheYearsDto> QueryClientRegStatisticsOverTheYears(
            SearchClientRegStatisticsOverTheYearsDto input)
        {
            if (input.ClientId == null)
                throw new FieldVerifyException("请先选择单位！", "请先选择单位！");
            if (input.StartYear == null || input.StartYear < 1949)
                throw new FieldVerifyException("请提供正确的开始年份！", "请提供正确的开始年份！");
            if (input.EndYear == null || input.EndYear < 1949)
                throw new FieldVerifyException("请提供正确的结束年份！", "请提供正确的结束年份！");

            var query = _customerRegRepository.GetAll().Where(m =>
                m.ClientInfo.Id == input.ClientId && m.BookingDate.Value.Year >= input.StartYear.Value &&
                m.BookingDate.Value.Year <= input.EndYear.Value);
            var group = query.GroupBy(m => new { m.ClientInfo, m.BookingDate.Value.Year }).ToList();
            var list = group.Select(g => new ViewClientRegStatisticsOverTheYearsDto
            {
                Year = g.Key.Year,
                RegPersonCount = query.Where(m => m.BookingDate.Value.Year == g.Key.Year).Count()
            }).ToList();
            return list;
        }

        /// <summary>
        /// 单位分组信息 update
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ClientTeamInfosDto ClientTeamInfoUpdate(ClientTeamInfosDto input)
        {
            var dto = _ClientTeamInfo.Get(input.Id);
            dto.TeamDiscount = input.TeamDiscount;
            dto.TeamMoney = input.TeamMoney;
            dto.TeamDiscountMoney = input.TeamDiscountMoney;
            dto.CostType = input.CostType;
            dto.JxType = input.JxType;
            dto.Jxzk = input.Jxzk;
            dto.SWjg = input.SWjg;
            dto.QuotaMoney = input.QuotaMoney;
            var client = _ClientTeamInfo.Update(dto).MapTo<ClientTeamInfosDto>();
            return client;
            //return _ClientTeamInfo.Update(dto).MapTo<ClientTeamInfosDto>();
        }

        public void ClientTeamRegItemUpdate(List<CreateClientTeamRegItemDto> input)
        {
            TjlClientTeamRegitem tjlClientTeamRegitem = new TjlClientTeamRegitem();
            foreach (var item in input)
            {
                var Cl = _Regitem.Get(item.Id);
                var group = _ItemGroup.Get(Cl.TbmItemGroupId.Value);

                var b = item.MapTo(Cl);
                b.ItemGroupMoney = group.Price.Value;
                tjlClientTeamRegitem = Cl;
                _Regitem.Update(b);
            }
            //存储抹零项

            if (tjlClientTeamRegitem != null && tjlClientTeamRegitem.ClientTeamInfoId != null)
            {
                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                var mogroup = _ItemGroup.FirstOrDefault(o => o.Id == guid);
                if (mogroup != null)
                {
                    var allmoney = tjlClientTeamRegitem.ClientTeamInfo.TeamDiscountMoney;
                    var zkmoney = input.Sum(o => o.ItemGroupDiscountMoney);

                    var cj = allmoney - zkmoney;
                    if (cj != 0)
                    {
                        var mlx = _Regitem.FirstOrDefault(o => o.TbmItemGroupId == guid && o.ClientTeamInfoId == tjlClientTeamRegitem.ClientTeamInfoId);
                        //已经包含抹零项
                        if (mlx != null)
                        {
                            var mlmoney = input.Where(o => o.TbmItemGroupId != guid).Sum(o => o.ItemGroupDiscountMoney);
                            var mlcj = allmoney - mlmoney;
                            //去掉抹零项目金额差价=0
                            if (mlcj == 0)
                            {
                                _Regitem.Delete(mlx);
                            }
                            else
                            {
                                mlx.ItemGroupDiscountMoney = mlx.ItemGroupDiscountMoney + cj.Value;
                                mlx.ItemGroupMoney = mlx.ItemGroupMoney + cj.Value;
                                _Regitem.Update(mlx);
                            }

                        }
                        else
                        {
                            //var mochu = _Regitem.FirstOrDefault(o => o.ClientTeamInfoId == tjlClientTeamRegitem.ClientTeamInfoId && o.TbmItemGroupId == guid);
                            //if (mochu != null)
                            //{
                            //    mochu.ItemGroupDiscountMoney = mlx.ItemGroupDiscountMoney + cj.Value;
                            //    mochu.ItemGroupMoney = mlx.ItemGroupMoney + cj.Value;
                            //    _Regitem.Update(mochu);
                            //}
                            //else
                            //{
                            TjlClientTeamRegitem adml = new TjlClientTeamRegitem();
                            //var adml = tjlClientTeamRegitem.MapTo(mlctTeamRegitem);
                            adml.ClientRegId = tjlClientTeamRegitem.ClientRegId;
                            adml.ClientTeamInfoId = tjlClientTeamRegitem.ClientTeamInfoId;
                            adml.PayerCatType = tjlClientTeamRegitem.PayerCatType;

                            adml.Id = Guid.NewGuid();
                            adml.ItemGroup = null;
                            adml.ItemGroupCodeBM = mogroup.ItemGroupBM;
                            adml.ItemGroupDiscountMoney = cj.Value;
                            adml.ItemGroupMoney = cj.Value;
                            adml.ItemGroupName = mogroup.ItemGroupName;
                            adml.ItemGroupOrder = mogroup.OrderNum;
                            adml.ItemSuit = null;
                            adml.ItemSuitId = null;
                            adml.ItemSuitName = "";
                            adml.Department = null;
                            adml.TbmDepartmentId = mogroup.DepartmentId;
                            adml.DepartmentCodeBM = mogroup.Department.DepartmentBM;
                            adml.DepartmentName = mogroup.Department.Name;
                            adml.DepartmentOrder = mogroup.Department.OrderNum;
                            adml.Discount = 1;
                            adml.TbmItemGroupId = mogroup.Id;
                            _Regitem.Insert(adml);
                            //}

                        }

                    }
                    else
                    {
                        var mlx = _Regitem.FirstOrDefault(o => o.TbmItemGroupId == guid && o.ClientTeamInfoId == tjlClientTeamRegitem.ClientTeamInfoId);
                        //已经包含抹零项
                        if (mlx != null && mlx.ItemGroupDiscountMoney == 0)
                        {
                            _Regitem.Delete(mlx);
                        }

                    }
                }
                else
                {
                    var allmoney = tjlClientTeamRegitem.ClientTeamInfo.TeamDiscountMoney;
                    var zkmoney = input.Sum(o => o.ItemGroupDiscountMoney);
                    var cj = allmoney - zkmoney;              
                    var group = _Regitem.FirstOrDefault(o => o.ClientTeamInfoId == tjlClientTeamRegitem.ClientTeamInfoId &&
                    o.ItemGroup.MaxDiscount < o.Discount && o.ItemGroupDiscountMoney != 0 && o.ItemGroupDiscountMoney> cj);
                    if (group != null)
                    {
                       
                        group.ItemGroupDiscountMoney = Math.Round(group.ItemGroupDiscountMoney, 2) + cj.Value;
                        
                    }
                }
            }
        }

        /// <summary>
        /// 根据GUID单条单位
        /// </summary>
        /// <param name="input">单位类</param>
        /// <returns>单位类</returns>
        public ClientRegDto GetClientRegByID(EntityDto<Guid> input)
        {
            var tjlClientReg = _clientRegRepository.Get(input.Id);
            return tjlClientReg.MapTo<ClientRegDto>();
        }

        /// <summary>
        /// 根据ID查询分组信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ClientTeamInfosDto GetClientTeamInfos(EntityDto<Guid> input)
        {
            var TeamInfo = _ClientTeamInfo.Get(input.Id);

            // 不会写不要乱写，Including不是加条件的
            //var TeamInfo = _ClientTeamInfo.GetAllIncluding(o => o.Id == input.Id);

            return TeamInfo.MapTo<ClientTeamInfosDto>();
        }

        public int GetClientNumber(EntityDto<Guid> input)
        {
            var clientReg = _clientRegRepository.GetAll().Where(o => o.ClientInfoId == input.Id);
            var count = clientReg.Count();
            return count;
        }

        /// <summary>
        /// 编辑分组
        /// </summary>
        /// <param name="dto"></param>
        public void EditTeam(CreateClientTeamInfoesDto dto)
        {
            if (_ClientTeamInfo.GetAll().Any(r => r.TeamName == dto.TeamName && r.Id != dto.Id))
                throw new FieldVerifyException("分组名称重复！", "分组名称重复！");
            if (_ClientTeamInfo.GetAll().Any(r => r.TeamBM == dto.TeamBM && r.Id != dto.Id))
                throw new FieldVerifyException("单位编码重复！", "单位编码重复！");

            var info = _ClientTeamInfo.FirstOrDefault(o => o.Id == dto.Id);
            var RegsUpdate = _ClientTeamInfo.Update(dto.MapTo(info));
        }

        /// <summary>
        /// 查询预约信息列表的分组方法
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IQueryable<TjlClientReg> GetQuery(ClientRegSelectDto dto)
        {
            var rglist = _clientRegRepository.GetAll();
            if (dto.Id != null && dto.Id != Guid.Empty)
            {
                rglist = rglist.Where(o => o.Id == dto.Id);
            }
            else
            {
                if (dto.ClientRegBM != null)
                    rglist = rglist.Where(o => o.ClientRegBM == dto.ClientRegBM);
                if (dto.StartCheckDate != null)
                    rglist = rglist.Where(o => o.StartCheckDate == dto.StartCheckDate);
                if (dto.EndCheckDate != null)
                    rglist = rglist.Where(o => o.EndCheckDate == dto.EndCheckDate);
                if (dto.FZState != null)
                    rglist = rglist.Where(o => o.FZState == dto.FZState);
                if (dto.SDState != null)
                    rglist = rglist.Where(o => o.SDState == dto.SDState);
                if (dto.ClientSate != null)
                    rglist = rglist.Where(o => o.ClientSate == dto.ClientSate);
                if (dto.ClientCheckSate != null && dto.ClientCheckSate != 0)
                    rglist = rglist.Where(o => o.ClientCheckSate == dto.ClientCheckSate);
            }

            return rglist.OrderByDescending(o => o.CreationTime);
        }

        #region 暂时废弃
        // 注释的时候 Doc 说明一起注释，说的脚趾头都记住了，长个人头都记不住
        ///// <summary>
        ///// 插入单位预约基本信息
        ///// </summary>
        ///// <param name="dto"></param>
        //public ClientRegsDto insertClientReag(CreateClientRegDto dto)
        //{
        //    ClientRegsDto regs;
        //    if (dto.Id == Guid.Empty)
        //    {

        //        var crg = dto.MapTo<TjlClientReg>();
        //        crg.Id = Guid.NewGuid();
        //        crg.ClientInfo = _ClientInfo.Get(dto.ClientInfo_Id);
        //        if (dto.UserId != null)
        //        {
        //            crg.user = _User.Get(dto.UserId.Value);
        //        }
        //        else
        //        {
        //            crg.user = _User.Get(AbpSession.UserId.Value);
        //        }

        //        var clientRegs = _clientRegRepository.Insert(crg);
        //        regs = clientRegs.MapTo<ClientRegsDto>();
        //    }
        //    else
        //    {
        //        TjlClientReg info = _clientRegRepository.FirstOrDefault(o => o.Id == dto.Id);
        //        var regsUpdate = _clientRegRepository.Update(dto.MapTo(info));
        //        regs = regsUpdate.MapTo<ClientRegsDto>();
        //    }

        //    return regs;
        //}
        #endregion

        #region  迁移过来的实现方法
        #region 暂时废弃 添加单位
        // 注释的时候 Doc 说明一起注释，说的脚趾头都记住了，长个人头都记不住
        ///// <summary>
        ///// 添加单位
        ///// </summary>
        ///// <param name="input">单位类</param>
        ///// <returns>单位类</returns>
        //public ClientRegsDto Add(ClientRegsDto input)
        //{
        //    var entity = input.MapTo<TjlClientReg>();
        //    if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
        //    entity = _clientRegRepository.Insert(entity);
        //    var dto = entity.MapTo<ClientRegsDto>();
        //    return dto;
        //}
        #endregion

        #region 暂时废弃 删除单位
        ///// <summary>
        ///// 删除单位
        ///// </summary>
        ///// <param name="input">单位类</param>
        //public void Del(ClientRegsDto input)
        //{
        //    _clientRegRepository.Delete(input.Id);
        //}
        #endregion

        #region 暂时废弃 编辑单位
        ///// <summary>
        ///// 编辑单位
        ///// </summary>
        ///// <param name="input">单位类</param>
        ///// <returns>单位类</returns>
        //public ClientRegsDto Edit(ClientRegsDto input)
        //{
        //    var entity = _clientRegRepository.Get(input.Id);
        //    input.MapTo(entity); // 赋值
        //    entity = _clientRegRepository.Update(entity);
        //    var dto = entity.MapTo<ClientRegsDto>();
        //    return dto;
        //}
        #endregion

        #region 暂时废弃 获取单挑单位
        ///// <summary>
        ///// 获取单条单位
        ///// </summary>
        ///// <param name="input">单位类</param>
        ///// <returns>单位类</returns>
        //public ClientRegsDto Get(ClientRegsDto input)
        //{
        //    var query = BuildQuery(input);
        //    var entity = query.FirstOrDefault();
        //    var dto = entity.MapTo<ClientRegsDto>();
        //    return dto;
        //}
        #endregion

        #region 暂时废弃 获取单位列表
        ///// <summary>
        ///// 获取单位列表
        ///// </summary>
        ///// <param name="input">单位类</param>
        ///// <returns>单位列表</returns>
        //public List<ClientRegsDto> Query(ClientRegsDto input)
        //{
        //    var query = BuildQuery(input);
        //    var dtos = query.MapTo<List<ClientRegsDto>>();
        //    return dtos;
        //}

        ///// <summary>
        ///// 获取单位列表
        ///// </summary>
        ///// <param name="input">单位类</param>
        ///// <returns>单位列表</returns>
        //private IQueryable<TjlClientReg> BuildQuery(ClientRegsDto input)
        //{
        //    var query = _clientRegRepository.GetAllIncluding(m => m.ClientRegBM, n => n.ClientRegBM);
        //    if (input != null)
        //    {
        //        if (input.Id != Guid.Empty)
        //        {
        //            query = query.Where(m => m.Id == input.Id);
        //        }
        //    }
        //    return query;
        //}
        #endregion
        #endregion

        #region 预约人员管理
        /// <summary>
        /// 人员预约信息查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<CustomerRegSimpleViewDto> QueryCustomerReg(EntityDto<Guid> dto)
        {
            var cliRegList = _customerRegRepository.GetAll().Where(p => p.ClientRegId == dto.Id);
            return cliRegList.MapTo<List<CustomerRegSimpleViewDto>>();
        }

        /// <summary>
        /// 查询人员预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<CustomerRegsViewDto> GetCustomerReg(CustomerRegsInputDto dto)
        {
            var lisCustomerReg = BuildCustomerRegQuery(dto);
            return lisCustomerReg.MapTo<List<CustomerRegsViewDto>>();
        }
        /// <summary>
        /// 查询分组人员 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ClientTeamCusListDto> GetCustomerRegList(CustomerRegsInputDto dto)
        {
            var lisCustomerReg = BuildCustomerRegQuery(dto);
            var outcuslist = lisCustomerReg.Where(p=>p.ClientTeamInfoId !=null).Select(o => new ClientTeamCusListDto
            {
                Age = o.Customer.Age == null ? 0 : o.Customer.Age.Value,
                CustomerBM = o.CustomerBM,
                Id = o.Id,
                Name = o.Customer.Name,
                TeamBM = o.ClientTeamInfo == null ? 0 : o.ClientTeamInfo.TeamBM,
                TeamId = o.ClientTeamInfoId.Value,
                TeamName = o.ClientTeamInfo == null ? "": o.ClientTeamInfo.TeamName,
                CheckSate = o.CheckSate.Value,
                RegisterState = o.RegisterState,
                Sex = o.Customer.Sex==null?3: o.Customer.Sex.Value,
                Remarks = o.Customer.Remarks,
                BarState = o.BarState,
                ClientRegNum = o.ClientRegNum,
                Department = o.Customer.Department,
                GuidanceSate = o.GuidanceSate,
                InfoSource = o.InfoSource,
                RegRemarks = o.Remarks,
                 CustomerId=o.CustomerId,
                  Mobile=o.Customer.Mobile,
                   RegMessageState=o.RegMessageState,
                VisitCard=o.Customer.VisitCard

            }).ToList();
            return outcuslist;
        }
        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IQueryable<TjlCustomerReg> BuildCustomerRegQuery(CustomerRegsInputDto dto)
        {
            var cliRegList = _customerRegRepository.GetAll().Where(o => o.ClientRegId == dto.ClientRegId);
            if (dto.ClientinfoId != Guid.Empty && dto.ClientinfoId != null)
                cliRegList = cliRegList.Where(o => o.ClientInfo.Id == dto.ClientinfoId);
            if (dto.ClientRegId != Guid.Empty && dto.ClientRegId != null)
                cliRegList = cliRegList.Where(o => o.ClientRegId == dto.ClientRegId);
            if (dto.CheckState != null)
                cliRegList = cliRegList.Where(o => o.CheckSate == dto.CheckState);
            if (dto.RegState != null && dto.RegState !=0)
                cliRegList = cliRegList.Where(o => o.RegisterState == dto.RegState);
            if (!string.IsNullOrWhiteSpace(dto.TextValue))
                cliRegList = cliRegList.Where(o =>
                    o.CustomerBM.Contains(dto.TextValue) || o.Customer.Name.Contains(dto.TextValue));

            return cliRegList.OrderBy(o => o.ClientTeamInfo.TeamBM).OrderByDescending(o => o.CreationTime);
        }

        /// <summary>
        /// 分页查询人员预约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PageResultDto<CustomerRegsViewDto> PageCustomerRegFulls(PageInputDto<CustomerRegsInputDto> input)
        {
            return PageHelper.Paging<CustomerRegsInputDto, TjlCustomerReg, CustomerRegsViewDto>(input,
                i => BuildCustomerRegQuery(i));
        }

        /// <summary>
        /// 删除人员预约信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void DelCustomerReg(List<EntityDto<Guid>> dto)
        {
            if (dto == null || dto.Count == 0)
                return; 
            var regidlist = dto.Select(p => p.Id).ToList();
            //删除图片
            _customerItemPicRepository.GetAll().Where(o => o.TjlCustomerRegID!=null && regidlist.Contains(o.TjlCustomerRegID.Value)).Delete();
            //删除项目
            _CustomerRegItemRepository.GetAll().Where(o => regidlist.Contains(o.CustomerRegId)).Delete();
            //删除组合
            _CustomerItemGroup.GetAll().Where(o => regidlist.Contains(o.CustomerRegBMId.Value)).Delete();
            //删除费用
            _mcusPayMoneyRepository.GetAll().Where(o => regidlist.Contains(o.Id)).Delete();
            //删除体检人预约
            _customerRegRepository.GetAll().Where(p => regidlist.Contains(p.Id)).Update(
                p=> new TjlCustomerReg {  IsDeleted = true });


            //foreach (var item in dto)
            //{
            //    var customerReg = _customerRegRepository.FirstOrDefault(o => o.Id == item.Id);
            //    if (_customerRegRepository.GetAll().Any(o => o.Id == item.Id))
            //    {
            //        //体检人项目组合
            //        var customerItemGroups = _CustomerItemGroup.GetAll().Where(o => o.CustomerRegBMId == customerReg.Id);
            //        if (customerItemGroups != null)
            //            foreach (var cusItemGroup in customerItemGroups)
            //                _CustomerItemGroup.Delete(cusItemGroup);

            //        //个人应收已收
            //        var McusPayMoneys = _mcusPayMoneyRepository.GetAll().Where(o => o.Id == customerReg.Id);

            //        if (McusPayMoneys != null && McusPayMoneys.Count() > 0)
            //            foreach (var mpm in McusPayMoneys)
            //                _mcusPayMoneyRepository.Delete(mpm);

            //        //体检人检查项目结果表
            //        var CustomerRegItems = _CustomerRegItemRepository.GetAll().Where(o => o.CustomerRegId == customerReg.Id);
            //        if (CustomerRegItems != null)
            //            foreach (var cri in CustomerRegItems)
            //                _CustomerRegItemRepository.Delete(cri);
            //        _customerRegRepository.DeleteAsync(customerReg);
            //    }
            //}
        }

        /// <summary>
        /// 导入人员信息新
        /// </summary>
        /// <param name="inputDatas"></param>
        /// <returns></returns>
        public List<QueryCustomerRegDto> RegClientCustomer(List<ClientCustomerRegDto> inputDatas)
        {

            if (inputDatas == null)
                throw new FieldVerifyException("无数据", "无数据");
            if (inputDatas.Count == 0)
                throw new FieldVerifyException("无数据", "无数据");
            //判断同单位项目人员名单           
            var Idcards = inputDatas.Where(o => o.Customer.IDCardNo != null && o.Customer.IDCardNo != "")?.Select(o => o.Customer.IDCardNo).ToList();
            if (Idcards != null && Idcards.Count > 0)
            {

                var clientteam = inputDatas.First().ClientTeamInfo_Id;
                var hadcard = _customerRegRepository.GetAll().Where(r => Idcards.Contains(r.Customer.IDCardNo)
                && r.ClientTeamInfoId == clientteam).Select(o => o.Customer.Name).Distinct().ToList();
                if (hadcard.Count > 0)
                {
                    throw new FieldVerifyException(string.Join(",", hadcard), "重复导入人员名单！");
                }

            }

            var result = new List<QueryCustomerRegDto>();
            var workNameList = new List<string>();
            var WorkTypeList = new List<string>();
            int WorkNameOrder = 0;
            int WorkTypeOrder = 0;
            // var bmGropList = _ItemGroup.GetAll();
            int clientBM = 1;
            var clietid = inputDatas.First().ClientRegId;
            var MaxClientBM = _customerRegRepository.GetAll().Where(o => o.ClientRegId == clietid && o.ClientRegNum != null).Select(o => o.ClientRegNum).Max();
            if (MaxClientBM != null)
            { clientBM = int.Parse(MaxClientBM.ToString()) + 1; }

            List<TjlClientReg> ClientReglis = new List<TjlClientReg>();
            List<TjlClientTeamInfo> ClientTeamlis = new List<TjlClientTeamInfo>();
            //批量插入
            List<TjlCustomerReg> custoemrReglist = new List<TjlCustomerReg>();
            List<TjlCustomer> custoemrlist = new List<TjlCustomer>();
            List<TbmOccDictionary> occDictionarylist = new List<TbmOccDictionary>();
            List<TjlOperationLog> OperationLoglist = new List<TjlOperationLog>();
            List<string> workNamelist = new List<string>();
            List<string> workTypelist = new List<string>();
            foreach (var input in inputDatas)
            {
                if (input.ClientTeamInfo_Id == null)
                {
                }
                #region TjlCustomer 客户表处理
                //先添加或更新客户信息

                var ret = new TjlCustomerReg();
                var retCus = new TjlCustomer();
                //关联历史档案
                if (input.Customer.Id != Guid.Empty)
                {
                    retCus = _jlCustomer.Get(input.Customer.Id);
                }
                else if (!string.IsNullOrEmpty(input.Customer.IDCardNo) && input.Customer.Id == Guid.Empty)
                {
                    retCus = _jlCustomer.GetAll().FirstOrDefault(o => o.IDCardNo == input.Customer.IDCardNo
                    && o.Name== input.Customer.Name && o.Sex== input.Customer.Sex);
                    if (retCus != null)
                    {
                        input.Customer.Id = retCus.Id;
                        if (!input.Customer.CusPhotoBmId.HasValue && retCus.CusPhotoBmId.HasValue)
                        {
                            input.Customer.CusPhotoBmId = retCus.CusPhotoBmId;

                        }
                         
                    }
                    else
                    {
                        retCus = new TjlCustomer();
                    }
                }
                if (input.Customer.Id == Guid.Empty)
                {
                    var customerEntity = input.Customer.MapTo<TjlCustomer>();
                    customerEntity.Id = Guid.NewGuid();
                    //retCus = _jlCustomer.Insert(customerEntity);
                    retCus = customerEntity;
                    retCus.NameAB= _commonAppService.GetHansBrief(new ChineseDto { Hans = retCus.Name })?.Brief;

                    custoemrlist.Add(customerEntity);
                }
                else
                {


                    var arc = retCus.ArchivesNum;
                    var cusinfo = input.Customer.MapTo(retCus);
                    cusinfo.ArchivesNum = arc;
                    retCus.NameAB = _commonAppService.GetHansBrief(new ChineseDto { Hans = retCus.Name })?.Brief;

                    retCus = _jlCustomer.Update(cusinfo);
                }
                #endregion

                #region TjlCustomerReg 客户登记表处理

                //再修改客户登记信息
                //先根据体检号码查询一下库里是否有该体检号信息
                if (input.Id == null || input.Id == Guid.Empty)
                {
                    var data = input.MapTo<TjlCustomerReg>();
                    data.Id = Guid.NewGuid();
                    data.ClientTeamInfoId = input.ClientTeamInfo_Id;
                    data.Customer = null;
                    data.CustomerId = retCus.Id;
                    //职业健康危害因素
                    if (!string.IsNullOrEmpty(input.RiskS))
                    {
                        var riskls = input.RiskS.Split('|');
                        data.OccHazardFactors = new List<TbmOccHazardFactor>();
                        foreach (var risk in riskls)
                        {
                           
                            var tbmHazar = _TbmOccHazardFactor.GetAll().FirstOrDefault(o => o.Text == risk);
                            data.OccHazardFactors.Add(tbmHazar);
                        }
                        if (data.OccHazardFactors != null && data.OccHazardFactors.Count > 0)
                        {
                            data.RiskS = input.RiskS.Replace("|", ",");
                        }
                    }
                    data.ClientRegNum = clientBM;
                    data.AppointmentTime = data.BookingDate;
                    //插入取消采用批量插入
                    // data = _customerRegRepository.Insert(data);

                    var userBM = _User.Get(AbpSession.UserId.Value);
                    if (userBM != null && userBM.HospitalArea.HasValue)
                    {
                        data.HospitalArea = userBM.HospitalArea;
                    }

                    custoemrReglist.Add(data);
                    // CurrentUnitOfWork.SaveChanges();
                    clientBM = clientBM + 1;
                    ret = data;
                    #region 保存车间、工种
                    if (!string.IsNullOrEmpty(data.WorkName) && !workNameList.Contains(data.WorkName))
                    {
                        if (!workNamelist.Contains(data.WorkName))
                        {
                            workNamelist.Add(data.WorkName);
                        }

                    }
                    #region 去掉自动保存工种
                    //if (!string.IsNullOrEmpty(data.TypeWork) && !WorkTypeList.Contains(data.TypeWork))
                    //{
                    //    if (!workTypelist.Contains(data.TypeWork))
                    //    {
                    //        workTypelist.Add(data.TypeWork);
                    //    }

                    //} 
                    #endregion

                    #endregion
                }
                TjlClientReg tjlClientReg = new TjlClientReg();
                TjlClientTeamInfo tjlClienTeam = new TjlClientTeamInfo();
                if (input.ClientRegId.HasValue)
                {
                    if (!ClientReglis.Any(o => o.Id == input.ClientRegId.Value))
                    {
                        tjlClientReg = _clientRegRepository.Get(input.ClientRegId.Value);
                        ClientReglis.Add(tjlClientReg);
                    }
                    else
                    {
                        tjlClientReg = ClientReglis.FirstOrDefault(o => o.Id == input.ClientRegId.Value);
                    }
                    if (!ClientTeamlis.Any(o => o.Id == input.ClientTeamInfo_Id.Value))
                    {
                        tjlClienTeam = _ClientTeamInfo.Get(input.ClientTeamInfo_Id.Value);
                        ClientTeamlis.Add(tjlClienTeam);
                    }
                    else
                    {
                        tjlClienTeam = ClientTeamlis.FirstOrDefault(o => o.Id == input.ClientTeamInfo_Id.Value);
                    }

                }
                #endregion            

                //CurrentUnitOfWork.SaveChanges();
                result.Add(ret.MapTo<QueryCustomerRegDto>());
                //添加日志        


                var oplog = new TjlOperationLog();
                oplog.LogBM = ret.CustomerBM;
                oplog.LogName = retCus.Name;
                oplog.LogText = "导入名单";
                if (tjlClientReg != null && tjlClientReg.ClientInfoId != null)
                {
                    oplog.LogText += "：单位：" + tjlClientReg.ClientInfo.ClientName + "，分组：" + tjlClienTeam.TeamName;
                }
                oplog.LogType = (int)LogsTypes.ClientId;
                oplog.LogDetail = "";
                oplog.Id = Guid.NewGuid();
                oplog.UseId = AbpSession.UserId;
                oplog.IPAddress = GetCurrentUserIp();
                // oplog = _tbmOperationLogcs.Insert(oplog);
                OperationLoglist.Add(oplog);

            }
            //批量插入信息
            //字典中没有的车间工种存储起来
            foreach (var workName in workNamelist)
            {
                var Workshoplis = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.Workshop.ToString() &&
                        o.Text == workName).Select(o => o.Id).ToList();
                if (Workshoplis.Count == 0)
                {
                    TbmOccDictionary tbmOccDictionary = new TbmOccDictionary();
                    tbmOccDictionary.Id = Guid.NewGuid();
                    var orderNum = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.Workshop.ToString()).Max(a => a.OrderNum);
                    tbmOccDictionary.Text = workName;
                    tbmOccDictionary.OrderNum = orderNum == null ? 0 : orderNum + 1;
                    tbmOccDictionary.OrderNum += WorkNameOrder;
                    tbmOccDictionary.Type = ZYBBasicDictionaryType.Workshop.ToString();
                    tbmOccDictionary.IsActive = 1;
                    tbmOccDictionary.Remarks = "";
                    //var entity1 = _tbmoccdictionaryRepository.Insert(tbmOccDictionary);
                    occDictionarylist.Add(tbmOccDictionary);
                    WorkNameOrder = WorkNameOrder + 1;
                    workNameList.Add(workName);
                }
            }
            //去掉保存工种
            #region MyRegion
            //foreach (var workType in workTypelist)
            //{
            //    var Workshoplis = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.WorkType.ToString() &&
            //           o.Text == workType).Select(o => o.Id).ToList();
            //    if (Workshoplis.Count == 0)
            //    {
            //        TbmOccDictionary tbmOccDictionary = new TbmOccDictionary();
            //        tbmOccDictionary.Id = Guid.NewGuid();
            //        var orderNum = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.WorkType.ToString()).Max(a => a.OrderNum);
            //        tbmOccDictionary.Text = workType;
            //        tbmOccDictionary.OrderNum = orderNum == null ? 0 : orderNum + 1;
            //        tbmOccDictionary.OrderNum += WorkTypeOrder;
            //        tbmOccDictionary.Type = ZYBBasicDictionaryType.WorkType.ToString();
            //        tbmOccDictionary.IsActive = 1;
            //        tbmOccDictionary.Remarks = "";
            //        //var entity1 = _tbmoccdictionaryRepository.Insert(tbmOccDictionary);
            //        occDictionarylist.Add(tbmOccDictionary);
            //        WorkTypeOrder = WorkTypeOrder + 1;
            //        WorkTypeList.Add(workType);
            //    }
            //} 
            #endregion
            if (custoemrlist.Count > 0)
            {
                _sqlExecutor.DbContext.Set<TjlCustomer>().AddRange(custoemrlist);
            }
            if (custoemrReglist.Count > 0)
            {
                _sqlExecutor.DbContext.Set<TjlCustomerReg>().AddRange(custoemrReglist);
            }
            if (OperationLoglist.Count > 0)
            {
                _sqlExecutor.DbContext.Set<TjlOperationLog>().AddRange(OperationLoglist);
            }
            if (occDictionarylist.Count > 0)
            {
                _sqlExecutor.DbContext.Set<TbmOccDictionary>().AddRange(occDictionarylist);

            }
            CurrentUnitOfWork.SaveChanges();

            #region 检查组合项目一系列处理 TjlMcusPayMoney费用处理
            //查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除    
            //var needAdd = false;
            //更新已有价格
            var clientRegId = custoemrReglist.Select(o => o.Id).ToList();
            var cusRegIds = string.Join("','", clientRegId);
            SqlParameter[] parameter = {
                          new SqlParameter("@userId",AbpSession.UserId)


                };
            string Sql = @"insert into  [TjlCustomerItemGroups]( [Id]
      ,[CustomerRegBMId]
      ,[DepartmentId]
      ,[DepartmentName]
      ,[DepartmentCodeBM]
      ,[DepartmentOrder]
      ,[ItemGroupBM_Id]
      ,[ItemGroupCodeBM]
      ,[ItemGroupName]
      ,[ItemGroupOrder]
      ,[SuspendState]
      ,[SFType]
      ,[CheckState]
      ,[SummBackSate]  
      ,[IsAddMinus]
      ,[RefundState]
      ,[ItemPrice]
      ,[DiscountRate]
      ,[PriceAfterDis]
      ,[PayerCat]
      ,[TTmoney]
      ,[GRmoney]
   
      ,[GuidanceSate]
      ,[BarState]
      ,[CollectionState]
      ,[RequestState]
      ,[DrawSate]
      ,[ItemSuitId]
      ,[ItemSuitName]   
      ,[TenantId]
      ,[IsDeleted]
      ,[CreationTime]
      ,[CreatorUserId],
IsZYB
     
)  
select NEWID(),
TjlCustomerRegs.Id,
TbmDepartmentId,
DepartmentName,
DepartmentCodeBM,
DepartmentOrder,
TbmItemGroupId ,
TbmItemGroups.ItemGroupBM,
TbmItemGroups.ItemGroupName,
TbmItemGroups.OrderNum,
1,
TbmItemGroups.ChartCode,
1,1,1,1,TjlClientTeamRegitems.ItemGroupMoney,
TjlClientTeamRegitems.Discount,TjlClientTeamRegitems.ItemGroupDiscountMoney,
case  PayerCatType when 2 then 1 else PayerCatType end,
case PayerCatType when 2 then 0  else TjlClientTeamRegitems.ItemGroupDiscountMoney end ,
case PayerCatType when 2 then TjlClientTeamRegitems.ItemGroupDiscountMoney  else 0 end ,
1,1,1,1,1,TjlClientTeamRegitems.ItemSuitId,TjlClientTeamRegitems.ItemSuitName,2,0,
GETDATE(),@userId,IsZYB
 from  TjlClientTeamRegitems ,TbmItemGroups,TjlCustomerRegs 
 where TjlCustomerRegs.IsDeleted=0 and  TjlClientTeamRegitems.TbmItemGroupId=TbmItemGroups.Id and TjlCustomerRegs.ClientTeamInfoId=TjlClientTeamRegitems.ClientTeamInfoId and 
 TjlClientTeamRegitems.IsDeleted=0 and TbmItemGroups.IsDeleted=0 and 
  TjlCustomerRegs.Id in ('" + cusRegIds + "')";
            _sqlExecutor.Execute(Sql, parameter);

            SqlParameter[] parameter1 = {


                         new SqlParameter("@CreatorUserId", AbpSession.UserId),


                };
            string sqlcusMoney = @"insert into [TjlMcusPayMoneys]
 ( [Id]
      ,[PersonalMoney]
      ,[PersonalAddMoney]
      ,[PersonalMinusMoney]
      ,[PersonalAdjustAddMoney]
      ,[PersonalAdjustMinusMoney]
      ,[PersonalPayMoney]
      ,[ClientMoney]
      ,[ClientAddMoney]
      ,[ClientMinusMoney]
      ,[ClientAdjustAddMoney]
      ,[ClientAdjustMinusMoney]
      ,[TenantId]
      ,[IsDeleted] 
      ,[CreationTime]
      ,[CreatorUserId]
      ,[ClientInfo_Id]
      ,[ClientReg_Id]
      ,[ClientTeamInfo_Id]
      ,[Customer_Id]
	  )
 select TjlCustomerRegs.Id,SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else GRmoney end),
  SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then GRmoney else 0 end) ,
    SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then GRmoney else 0 end) ,
	0,0,0,
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else TTmoney end),
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then TTmoney else 0 end),
	  SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then TTmoney else 0 end),
	  0,0,2,0,GETDATE()	 ,  
	  @CreatorUserId,TjlCustomerRegs.ClientInfoId,TjlCustomerRegs.ClientRegId,TjlCustomerRegs.ClientTeamInfoId,	 
	   TjlCustomerRegs.CustomerId
  from TjlCustomerItemGroups ,TjlCustomerRegs where TjlCustomerRegs.IsDeleted=0 and 
    TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=TjlCustomerRegs.Id and 
   CustomerRegBMId in ('" + cusRegIds + "')  group by TjlCustomerRegs.Id,TjlCustomerRegs.ClientInfoId,TjlCustomerRegs.ClientRegId,TjlCustomerRegs.ClientTeamInfoId,	 " +
   "       TjlCustomerRegs.CustomerId";
            _sqlExecutor.Execute(sqlcusMoney, parameter1);
            #endregion

            return result;
        }
        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetCurrentUserIp()
        {
            string userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }
        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 导入人员信息新
        /// </summary>
        /// <param name="inputDatas"></param>
        /// <returns></returns>
        public List<QueryCustomerRegDto> RegClientCustomerbak(List<ClientCustomerRegDto> inputDatas)
        {

            if (inputDatas == null)
                throw new FieldVerifyException("无数据", "无数据");
            if (inputDatas.Count == 0)
                throw new FieldVerifyException("无数据", "无数据");
            //判断同单位项目人员名单           
            var Idcards = inputDatas.Where(o => o.Customer.IDCardNo != null && o.Customer.IDCardNo != "")?.Select(o => o.Customer.IDCardNo).ToList();
            if (Idcards != null && Idcards.Count > 0)
            {

                var clientteam = inputDatas.First().ClientTeamInfo_Id;
                var hadcard = _customerRegRepository.GetAll().Where(r => Idcards.Contains(r.Customer.IDCardNo)
                && r.ClientTeamInfoId == clientteam).Select(o => o.Customer.Name).Distinct().ToList();
                if (hadcard.Count > 0)
                {
                    throw new FieldVerifyException(string.Join(",", hadcard), "重复导入人员名单！");
                }

            }

            var result = new List<QueryCustomerRegDto>();
            var workNameList = new List<string>();
            var WorkTypeList = new List<string>();
            int WorkNameOrder = 0;
            int WorkTypeOrder = 0;
            // var bmGropList = _ItemGroup.GetAll();
            int clientBM = 1;
            var clietid = inputDatas.First().ClientRegId;
            var MaxClientBM = _customerRegRepository.GetAll().Where(o => o.ClientRegId == clietid && o.ClientRegNum != null).Select(o => o.ClientRegNum).Max();
            if (MaxClientBM != null)
            { clientBM = int.Parse(MaxClientBM.ToString()) + 1; }
            foreach (var input in inputDatas)
            {

                #region TjlCustomer 客户表处理
                //先添加或更新客户信息

                var ret = new TjlCustomerReg();
                var retCus = new TjlCustomer();
                //关联历史档案
                if (input.Customer.Id != Guid.Empty)
                {
                    retCus = _jlCustomer.Get(input.Customer.Id);
                }
                else if (!string.IsNullOrEmpty(input.Customer.IDCardNo) && input.Customer.Id == Guid.Empty)
                {
                    retCus = _jlCustomer.GetAll().FirstOrDefault(o => o.IDCardNo == input.Customer.IDCardNo);
                    if (retCus != null)
                    {
                        input.Customer.Id = retCus.Id;
                    }
                    else
                    {
                        retCus = new TjlCustomer();
                    }
                }
                if (input.Customer.Id == Guid.Empty)
                {
                    var customerEntity = input.Customer.MapTo<TjlCustomer>();
                    customerEntity.Id = Guid.NewGuid();
                    retCus = _jlCustomer.Insert(customerEntity);
                    //CurrentUnitOfWork.SaveChanges();
                }
                else
                {

                    //if (customerEntity != null && customerEntity.Id != Guid.Empty)
                    //{
                    //    customerEntity = _jlCustomer.Get(input.Customer.Id);
                    //}
                    var arc = retCus.ArchivesNum;
                    var cusinfo = input.Customer.MapTo(retCus);
                    cusinfo.ArchivesNum = arc;
                    retCus = _jlCustomer.Update(cusinfo);
                }
                #endregion

                #region TjlCustomerReg 客户登记表处理

                //再修改客户登记信息
                //先根据体检号码查询一下库里是否有该体检号信息
                if (input.Id == null || input.Id == Guid.Empty)
                {
                    var data = input.MapTo<TjlCustomerReg>();
                    data.Id = Guid.NewGuid();
                    data.ClientTeamInfoId = input.ClientTeamInfo_Id;
                    data.Customer = retCus;
                    //data.CustomerId = retCus.Id;
                    //职业健康危害因素
                    if (!string.IsNullOrEmpty(input.RiskS))
                    {
                        var riskls = input.RiskS.Split('|');

                        foreach (var risk in riskls)
                        {
                            data.OccHazardFactors = new List<TbmOccHazardFactor>();
                            var tbmHazar = _TbmOccHazardFactor.GetAll().FirstOrDefault(o => o.Text == risk);
                            data.OccHazardFactors.Add(tbmHazar);
                        }
                        if (data.OccHazardFactors != null && data.OccHazardFactors.Count > 0)
                        {
                            data.RiskS = input.RiskS.Replace("|", ",");
                        }
                    }
                    data.ClientRegNum = clientBM;
                    data.AppointmentTime = data.BookingDate;
                    data = _customerRegRepository.Insert(data);
                    CurrentUnitOfWork.SaveChanges();
                    clientBM = clientBM + 1;
                    ret = data;
                    #region 保存车间、工种
                    if (!string.IsNullOrEmpty(data.WorkName) && !workNameList.Contains(data.WorkName))
                    {
                        var Workshoplis = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.Workshop.ToString() &&
                         o.Text == data.WorkName).ToList();
                        if (Workshoplis.Count == 0)
                        {
                            TbmOccDictionary tbmOccDictionary = new TbmOccDictionary();
                            tbmOccDictionary.Id = Guid.NewGuid();
                            var orderNum = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.Workshop.ToString()).Max(a => a.OrderNum);
                            tbmOccDictionary.Text = data.WorkName;
                            tbmOccDictionary.OrderNum = orderNum == null ? 0 : orderNum + 1;
                            tbmOccDictionary.OrderNum += WorkNameOrder;
                            tbmOccDictionary.Type = ZYBBasicDictionaryType.Workshop.ToString();
                            tbmOccDictionary.IsActive = 1;
                            tbmOccDictionary.Remarks = "";
                            var entity1 = _tbmoccdictionaryRepository.Insert(tbmOccDictionary);
                            WorkNameOrder = WorkNameOrder + 1;
                            workNameList.Add(data.WorkName);
                        }

                    }
                    if (!string.IsNullOrEmpty(data.TypeWork) && !WorkTypeList.Contains(data.TypeWork))
                    {
                        var Workshoplis = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.WorkType.ToString() &&
                        o.Text == data.TypeWork).ToList();
                        if (Workshoplis.Count == 0)
                        {
                            TbmOccDictionary tbmOccDictionary = new TbmOccDictionary();
                            tbmOccDictionary.Id = Guid.NewGuid();
                            var orderNum = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.WorkType.ToString()).Max(a => a.OrderNum);
                            tbmOccDictionary.Text = data.TypeWork;
                            tbmOccDictionary.OrderNum = orderNum == null ? 0 : orderNum + 1;
                            tbmOccDictionary.OrderNum += WorkTypeOrder;
                            tbmOccDictionary.Type = ZYBBasicDictionaryType.WorkType.ToString();
                            tbmOccDictionary.IsActive = 1;
                            tbmOccDictionary.Remarks = "";
                            var entity1 = _tbmoccdictionaryRepository.Insert(tbmOccDictionary);
                            WorkTypeOrder = WorkTypeOrder + 1;
                            WorkTypeList.Add(data.TypeWork);
                        }
                    }

                    #endregion
                }
                TjlClientReg tjlClientReg = new TjlClientReg();
                TjlClientTeamInfo tjlClienTeam = new TjlClientTeamInfo();
                if (input.ClientRegId.HasValue)
                {
                    tjlClientReg = _clientRegRepository.Get(input.ClientRegId.Value);
                    tjlClienTeam = _ClientTeamInfo.Get(input.ClientTeamInfo_Id.Value);

                }
                #endregion

                #region 检查组合项目一系列处理 TjlMcusPayMoney费用处理
                //查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除    
                //var needAdd = false;
                //更新已有价格
                SqlParameter[] parameter = {
                         new SqlParameter("@ClientTeamInfoId", input.ClientTeamInfo_Id),
                          new SqlParameter("@userId",AbpSession.UserId),
                          new SqlParameter("@CustomerRegBMId",ret.Id)

                };
                string Sql = @"insert into  [TjlCustomerItemGroups]( [Id]
      ,[CustomerRegBMId]
      ,[DepartmentId]
      ,[DepartmentName]
      ,[DepartmentCodeBM]
      ,[DepartmentOrder]
      ,[ItemGroupBM_Id]
      ,[ItemGroupCodeBM]
      ,[ItemGroupName]
      ,[ItemGroupOrder]
      ,[SuspendState]
      ,[SFType]
      ,[CheckState]
      ,[SummBackSate]  
      ,[IsAddMinus]
      ,[RefundState]
      ,[ItemPrice]
      ,[DiscountRate]
      ,[PriceAfterDis]
      ,[PayerCat]
      ,[TTmoney]
      ,[GRmoney]
   
      ,[GuidanceSate]
      ,[BarState]
      ,[CollectionState]
      ,[RequestState]
      ,[DrawSate]
      ,[ItemSuitId]
      ,[ItemSuitName]   
      ,[TenantId]
      ,[IsDeleted]
      ,[CreationTime]
      ,[CreatorUserId]
     
)  
select NEWID(),
@CustomerRegBMId,
TbmDepartmentId,
DepartmentName,
DepartmentCodeBM,
DepartmentOrder,
TbmItemGroupId ,
TbmItemGroups.ItemGroupBM,
TbmItemGroups.ItemGroupName,
TbmItemGroups.OrderNum,
1,
TbmItemGroups.ChartCode,
1,1,1,1,TjlClientTeamRegitems.ItemGroupMoney,
TjlClientTeamRegitems.Discount,TjlClientTeamRegitems.ItemGroupDiscountMoney,
PayerCatType,
case PayerCatType when 2 then 0  else TjlClientTeamRegitems.ItemGroupDiscountMoney end ,
case PayerCatType when 2 then TjlClientTeamRegitems.ItemGroupDiscountMoney  else 0 end ,
1,1,1,1,1,TjlClientTeamRegitems.ItemSuitId,TjlClientTeamRegitems.ItemSuitName,2,0,
GETDATE(),@userId
 from  TjlClientTeamRegitems ,TbmItemGroups
 where TjlClientTeamRegitems.TbmItemGroupId=TbmItemGroups.Id and 
 TjlClientTeamRegitems.IsDeleted=0 and TbmItemGroups.IsDeleted=0 and 
   ClientTeamInfoId=@ClientTeamInfoId";
                _sqlExecutor.Execute(Sql, parameter);

                SqlParameter[] parameter1 = {

                         new SqlParameter("@CustomerRegBMId", ret.Id),
                         new SqlParameter("@CreatorUserId", AbpSession.UserId),
                         new SqlParameter("@ClientInfoId", ret.ClientInfoId),
                         new SqlParameter("@ClientRegId", ret.ClientRegId),
                          new SqlParameter("@ClientTeamInfoId", ret.ClientTeamInfoId),
                         new SqlParameter("@CustomerId", ret.CustomerId)

                };
                string sqlcusMoney = @"insert into [TjlMcusPayMoneys]
 ( [Id]
      ,[PersonalMoney]
      ,[PersonalAddMoney]
      ,[PersonalMinusMoney]
      ,[PersonalAdjustAddMoney]
      ,[PersonalAdjustMinusMoney]
      ,[PersonalPayMoney]
      ,[ClientMoney]
      ,[ClientAddMoney]
      ,[ClientMinusMoney]
      ,[ClientAdjustAddMoney]
      ,[ClientAdjustMinusMoney]
      ,[TenantId]
      ,[IsDeleted] 
      ,[CreationTime]
      ,[CreatorUserId]
      ,[ClientInfo_Id]
      ,[ClientReg_Id]
      ,[ClientTeamInfo_Id]
      ,[Customer_Id]
	  )
 select @CustomerRegBMId,SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else GRmoney end),
  SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then GRmoney else 0 end) ,
    SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then GRmoney else 0 end) ,
	0,0,0,
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then 0 else TTmoney end),
	 SUM(case TjlCustomerItemGroups.IsAddMinus when 2 then TTmoney else 0 end),
	  SUM(case TjlCustomerItemGroups.IsAddMinus when 3 then TTmoney else 0 end),
	  0,0,2,0,GETDATE()	 ,  
	  @CreatorUserId,@ClientInfoId,@ClientRegId,@ClientTeamInfoId,	 
	   @CustomerId
  from TjlCustomerItemGroups ,TjlCustomerRegs where
    TjlCustomerItemGroups.IsDeleted=0 and  TjlCustomerItemGroups.CustomerRegBMId=TjlCustomerRegs.Id and 
   CustomerRegBMId =@CustomerRegBMId";
                _sqlExecutor.Execute(sqlcusMoney, parameter1);
                #endregion

                //CurrentUnitOfWork.SaveChanges();
                result.Add(ret.MapTo<QueryCustomerRegDto>());
                //添加日志           
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = ret.CustomerBM;
                createOpLogDto.LogName = ret.Customer.Name;
                createOpLogDto.LogText = "导入名单";
                if (tjlClientReg != null && tjlClientReg.ClientInfoId != null)
                {
                    createOpLogDto.LogText += "：单位：" + tjlClientReg.ClientInfo.ClientName + "，分组：" + tjlClienTeam.TeamName;
                }
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                createOpLogDto.LogDetail = "";
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            return result;
        }


        /// <summary>
        /// 导入人员信息
        /// </summary>
        /// <param name="inputDatas"></param>
        /// <returns></returns>
        public List<QueryCustomerRegDto> RegCustomer(List<QueryCustomerRegDto> inputDatas)
        {
            List<string> norisk = new List<string>();
            if (inputDatas == null)
                throw new FieldVerifyException("无数据", "无数据");
            if (inputDatas.Count == 0)
                throw new FieldVerifyException("无数据", "无数据");
            foreach (var data in inputDatas)
            {
                if (data.Customer.IDCardNo != "")
                {
                    if (_customerRegRepository.GetAll().Any(r => r.Customer.IDCardNo == data.Customer.IDCardNo && r.ClientRegId == data.ClientRegId))
                    {
                        norisk.Add(data.Customer.Name);

                    }

                }
            }
            if (norisk != null && norisk.Count > 0)
            {
                throw new FieldVerifyException(string.Join(",", norisk), "重复导入人员名单！");
            }
            var result = new List<QueryCustomerRegDto>();
            var workNameList = new List<string>();
            var WorkTypeList = new List<string>();
            int WorkNameOrder = 0;
            int WorkTypeOrder = 0;
            // var bmGropList = _ItemGroup.GetAll();
            foreach (var input in inputDatas)
            {
                //if (_customerRegRepository.GetAll().Any(o => o.CustomerBM == input.CustomerBM && o.Id != input.Id))
                //    throw new FieldVerifyException("体检号重复", "体检号重复，请修改后登记。");
                if (input.RegisterState == (int)RegisterState.Yes)
                {
                    if (input.ClientTeamRegitemInfo == null)
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                    if (input.ClientTeamRegitemInfo.Count == 0)
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                }

                #region TjlCustomer 客户表处理
                //先添加或更新客户信息

                var ret = new TjlCustomerReg();
                var retCus = new TjlCustomer();
                //关联历史档案
                if (input.Customer.Id != Guid.Empty)
                {
                    retCus = _jlCustomer.Get(input.Customer.Id);
                }
                else if (!string.IsNullOrEmpty(input.Customer.IDCardNo) && input.Customer.Id == Guid.Empty)
                {
                    retCus = _jlCustomer.GetAll().FirstOrDefault(o => o.IDCardNo == input.Customer.IDCardNo);
                    if (retCus != null)
                    {
                        input.Customer.Id = retCus.Id;
                    }
                    else
                    {
                        retCus = new TjlCustomer();
                    }
                }
                if (input.Customer.Id == Guid.Empty)
                {
                    var customerEntity = input.Customer.MapTo<TjlCustomer>();
                    customerEntity.Id = Guid.NewGuid();
                    retCus = _jlCustomer.Insert(customerEntity);
                    //CurrentUnitOfWork.SaveChanges();
                }
                else
                {

                    //if (customerEntity != null && customerEntity.Id != Guid.Empty)
                    //{
                    //    customerEntity = _jlCustomer.Get(input.Customer.Id);
                    //}
                    var arc = retCus.ArchivesNum;
                    var cusinfo = input.Customer.MapTo(retCus);
                    cusinfo.ArchivesNum = arc;
                    retCus = _jlCustomer.Update(cusinfo);
                }
                #endregion

                #region TjlCustomerReg 客户登记表处理

                //再修改客户登记信息
                //先根据体检号码查询一下库里是否有该体检号信息
                if (input.Id == null || input.Id == Guid.Empty)
                {
                    var data = input.MapTo<TjlCustomerReg>();
                    data.Id = Guid.NewGuid();
                    data.ClientTeamInfoId = input.ClientTeamInfo_Id;
                    data.Customer = retCus;
                    //data.CustomerId = retCus.Id;
                    //职业健康危害因素
                    if (!string.IsNullOrEmpty(input.RiskS))
                    {
                        var riskls = input.RiskS.Split('|');

                        foreach (var risk in riskls)
                        {
                            data.OccHazardFactors = new List<TbmOccHazardFactor>();
                            var tbmHazar = _TbmOccHazardFactor.GetAll().FirstOrDefault(o => o.Text == risk);
                            data.OccHazardFactors.Add(tbmHazar);
                        }
                        if (data.OccHazardFactors != null && data.OccHazardFactors.Count > 0)
                        {
                            data.RiskS = input.RiskS.Replace("|", ",");
                        }
                    }
                    data = _customerRegRepository.Insert(data);
                    //CurrentUnitOfWork.SaveChanges();
                    ret = data;
                    #region 保存车间、工种
                    if (!string.IsNullOrEmpty(data.WorkName) && !workNameList.Contains(data.WorkName))
                    {
                        var Workshoplis = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.Workshop.ToString() &&
                         o.Text == data.WorkName).ToList();
                        if (Workshoplis.Count == 0)
                        {
                            TbmOccDictionary tbmOccDictionary = new TbmOccDictionary();
                            tbmOccDictionary.Id = Guid.NewGuid();
                            var orderNum = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.Workshop.ToString()).Max(a => a.OrderNum);
                            tbmOccDictionary.Text = data.WorkName;
                            tbmOccDictionary.OrderNum = orderNum == null ? 0 : orderNum + 1;
                            tbmOccDictionary.OrderNum += WorkNameOrder;
                            tbmOccDictionary.Type = ZYBBasicDictionaryType.Workshop.ToString();
                            tbmOccDictionary.IsActive = 1;
                            tbmOccDictionary.Remarks = "";
                            var entity1 = _tbmoccdictionaryRepository.Insert(tbmOccDictionary);
                            WorkNameOrder = WorkNameOrder + 1;
                            workNameList.Add(data.WorkName);
                        }

                    }
                    if (!string.IsNullOrEmpty(data.TypeWork) && !WorkTypeList.Contains(data.TypeWork))
                    {
                        var Workshoplis = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.WorkType.ToString() &&
                        o.Text == data.TypeWork).ToList();
                        if (Workshoplis.Count == 0)
                        {
                            TbmOccDictionary tbmOccDictionary = new TbmOccDictionary();
                            tbmOccDictionary.Id = Guid.NewGuid();
                            var orderNum = _tbmoccdictionaryRepository.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.WorkType.ToString()).Max(a => a.OrderNum);
                            tbmOccDictionary.Text = data.TypeWork;
                            tbmOccDictionary.OrderNum = orderNum == null ? 0 : orderNum + 1;
                            tbmOccDictionary.OrderNum += WorkTypeOrder;
                            tbmOccDictionary.Type = ZYBBasicDictionaryType.WorkType.ToString();
                            tbmOccDictionary.IsActive = 1;
                            tbmOccDictionary.Remarks = "";
                            var entity1 = _tbmoccdictionaryRepository.Insert(tbmOccDictionary);
                            WorkTypeOrder = WorkTypeOrder + 1;
                            WorkTypeList.Add(data.TypeWork);
                        }
                    }

                    #endregion
                }
                TjlClientReg tjlClientReg = new TjlClientReg();
                TjlClientTeamInfo tjlClienTeam = new TjlClientTeamInfo();
                if (input.ClientRegId.HasValue)
                {
                    tjlClientReg = _clientRegRepository.Get(input.ClientRegId.Value);
                    tjlClienTeam = _ClientTeamInfo.Get(input.ClientTeamInfo_Id.Value);

                }
                #endregion

                #region 检查组合项目一系列处理 TjlMcusPayMoney费用处理
                //查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除    
                //var needAdd = false;
                if (input.CustomerItemGroup != null)
                {
                    decimal personalPay = 0; //个人应收
                    decimal clientMoney = 0; //团体应收
                    decimal personalAdd = 0; //个人加项
                    decimal personalMinusMoney = 0; //个人减项
                    decimal clientAdd = 0;
                    decimal clientMinusMoney = 0;
                    ret.CustomerItemGroup = null;
                    foreach (var g in input.ClientTeamRegitemInfo)
                    {

                        var groupItem = g.MapTo<TjlCustomerItemGroup>();
                        groupItem.Id = Guid.NewGuid();
                        groupItem.CustomerRegBM = ret;
                        groupItem.DepartmentBM = null;

                        #region 项目赋值                       
                        groupItem.ItemGroupBM_Id = g.TbmItemGroupid.Value;
                        groupItem.ItemPrice = g.ItemGroupMoney;
                        groupItem.PriceAfterDis = g.ItemGroupDiscountMoney;
                        groupItem.ItemGroupName = g.ItemGroupName;
                        groupItem.DiscountRate = g.Discount;
                        if (g.PayerCatType == (int)PayerCatType.PersonalCharge)
                            groupItem.GRmoney = g.ItemGroupDiscountMoney;
                        else if (g.PayerCatType == (int)PayerCatType.ClientCharge)
                            groupItem.TTmoney = g.ItemGroupDiscountMoney;
                        groupItem.IsAddMinus = (int)AddMinusType.Normal;
                        groupItem.ItemGroupOrder = g.ItemGroupOrder;//.ItemGroup.OrderNum;
                        groupItem.PayerCat = g.PayerCatType; //(int)PayerCatType.NoCharge;
                        groupItem.ItemSuitId = g.ItemSuitId;
                        groupItem.ItemSuitName = g.ItemSuitName;
                        groupItem.DepartmentId = g.TbmDepartmentid.Value;
                        groupItem.DepartmentName = g.DepartmentName;
                        groupItem.DepartmentOrder = g.DepartmentOrder;
                        groupItem.CheckState = (int)ProjectIState.Not;

                        #endregion
                        var grop = _CustomerItemGroup.Insert(groupItem);
                        //已收费的项目不会被删除也不作处理，新加分组数据添加至数据库
                        if (g.IsAddMinus == (int)AddMinusType.Normal || g.IsAddMinus == (int)AddMinusType.Add)
                        {
                            personalPay += groupItem.GRmoney;
                            clientMoney += groupItem.TTmoney;
                        }
                        if (g.IsAddMinus == (int)AddMinusType.Add)
                        {
                            //加项
                            personalAdd += groupItem.GRmoney;
                            clientAdd += groupItem.TTmoney;
                        }
                        else if (g.IsAddMinus == (int)AddMinusType.Minus)
                        {
                            //减项
                            clientMinusMoney += groupItem.TTmoney;
                            personalMinusMoney += groupItem.GRmoney;
                        }

                    }

                    //增加金额
                    _mcusPayMoneyRepository.Insert(new TjlMcusPayMoney
                    {
                        Customer = retCus,
                        CustomerReg = ret,
                        ClientInfo = ret.ClientInfo,
                        ClientReg = ret.ClientReg,
                        ClientTeamInfo = ret.ClientTeamInfo,
                        PersonalAddMoney = personalAdd,
                        PersonalMinusMoney = personalMinusMoney,
                        ClientAdjustAddMoney = clientAdd,
                        ClientAdjustMinusMoney = clientMinusMoney,
                        PersonalMoney = personalPay,
                        ClientMoney = clientMoney,
                        PersonalPayMoney = 0
                    });
                }

                #endregion

                //CurrentUnitOfWork.SaveChanges();
                result.Add(ret.MapTo<QueryCustomerRegDto>());
                //添加日志           
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = ret.CustomerBM;
                createOpLogDto.LogName = ret.Customer.Name;
                createOpLogDto.LogText = "导入名单";
                if (tjlClientReg != null && tjlClientReg.ClientInfoId != null)
                {
                    createOpLogDto.LogText += "：单位：" + tjlClientReg.ClientInfo.ClientName + "，分组：" + tjlClienTeam.TeamName;
                }
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                createOpLogDto.LogDetail = "";
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            return result;


        }

        #endregion

        #region 预约分组管理
        public void CreateTeamInfos(CreateClientTeamInfoesDto dto)
        {
            if (_ClientTeamInfo.GetAll().Any(r => r.TeamName == dto.TeamName))
                throw new FieldVerifyException("分组名称重复！", "分组名称重复！");
            if (_ClientTeamInfo.GetAll().Any(r => r.TeamBM == dto.TeamBM))
                throw new FieldVerifyException("单位编码重复！", "单位编码重复！");

            var crgTeam = dto.MapTo<TjlClientTeamInfo>();
            crgTeam.Id = Guid.NewGuid();
            crgTeam.ClientRegId = dto.ClientRegId;
            crgTeam.ClientReg = _clientRegRepository.Get(dto.ClientRegId);
            //crgTeam.OPostState = null;
            _ClientTeamInfo.Insert(crgTeam);
            //CurrentUnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 分组管理信息展示
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<CreateClientTeamInfoesDto> GetClientTeam(EntityDto<Guid> dto)
        {
            var list = _ClientTeamInfo.GetAll().Where(o => o.ClientRegId == dto.Id);

            return list.MapTo<List<CreateClientTeamInfoesDto>>();
        }

        public void DelTeam(EntityDto<Guid> teamId)
        {


            throw new NotImplementedException();
        }
        #endregion

        //public List<CreateTjlClientTeamRegitemDto> GetClientTeamRegItem(EntityDto<Guid> input) {
        //    var TeamRegItem = _
        //    return null;
        //}



        /// <summary>
        /// 按科室统计团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        public List<StatisticsTTJZDDepartmentDto> StatisticsDepartmentTTJZD(EntityDto<Guid> input)
        {
            var result = new List<StatisticsTTJZDDepartmentDto>();
            //查出该单位预约下所有的人员的项目
            var itemGroups = _CustomerItemGroup.GetAll().Where(o => o.CustomerRegBM.ClientRegId == input.Id);
            //按科室分组
            var departs = itemGroups.GroupBy(o => o.DepartmentId).Select(o => o.FirstOrDefault());
            if (departs == null)
                return result;
            foreach (var depart in departs)
            {
                var data = new StatisticsTTJZDDepartmentDto();
                data.Category = depart.DepartmentBM.Category;
                data.Department = depart.DepartmentName;//查询该科室下的正常项目或加项的相关数据

                var dpGroups = itemGroups.Where(o => o.DepartmentId == depart.DepartmentId && (o.IsAddMinus == (int)AddMinusType.Normal || o.IsAddMinus == (int)AddMinusType.Add)).ToList();
                data.TotalNumber = dpGroups.Count();
                data.TotalFee = dpGroups.Sum(o => o.ItemPrice);
                data.YingShouJinE = dpGroups.Sum(o => o.PriceAfterDis);
                data.ShiShouJinE = dpGroups.Where(o => o.MReceiptInfoClientlId.HasValue).Sum(o => o.PriceAfterDis);
                data.YiJianZongJinE = dpGroups.Where(o => o.CheckState == (int)ProjectIState.Complete).Sum(o => o.PriceAfterDis);
                result.Add(data);
            }
            return result;
        }
        /// <summary>
        /// 根据人员查询团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        public StatisticsTTJZDPersonalTTJZDDto StatisticsPersonalTTJZD(EntityDto<Guid> input)
        {
            var result = new StatisticsTTJZDPersonalTTJZDDto() { FreePersons = new List<IsFree>(), PayPersons = new List<Pay>() };
            var regs = _customerRegRepository.GetAll().Where(o => o.ClientRegId == input.Id);//根据单位登记id获取单位下所有人员
            foreach (var r in regs)
            {
                var free = false;//判断是否免费
                //if (r.IsFree.HasValue)
                //    if (r.IsFree.Value)
                //        free = true;
                if (r.PersonnelCategoryId.HasValue)
                    if (r.PersonnelCategory.IsFree)
                        free = true;
                if (free)
                {
                    var data = new IsFree();
                    data.Age = r.Customer.Age;
                    data.CheckDate = r.BookingDate;
                    data.CustomRegNum = r.CustomerBM;
                    data.IDCardNo = r.Customer.IDCardNo;
                    data.Name = r.Customer.Name;
                    data.TeamName = r.ClientTeamInfo.TeamName;
                    data.Sex = r.Customer.Sex;
                    data.Mobile = r.Customer.Mobile;//获取基本信息，不为减项的金额即为总金额之一
                    data.SumPrice = r.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemPrice);
                    data.ItemGroups = "";
                    foreach (var item in r.CustomerItemGroup)
                    {
                        data.ItemGroups += item.ItemGroupName + "，";
                    }
                    data.SumPrice = decimal.Parse(data.SumPrice.ToString("0.00"));
                    data.ItemGroups = data.ItemGroups.Remove(data.ItemGroups.Length - 1);
                    result.FreePersons.Add(data);
                }
                else
                {
                    var data = new Pay();
                    data.Age = r.Customer.Age;
                    data.CheckDate = r.BookingDate;
                    data.CustomRegNum = r.CustomerBM;
                    data.IDCardNo = r.Customer.IDCardNo;
                    data.Name = r.Customer.Name;
                    data.Mobile = r.Customer.Mobile;
                    data.TeamName = r.ClientTeamInfo.TeamName;
                    data.Sex = r.Customer.Sex;
                    data.SumPrice = r.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemPrice);
                    data.PriceAfterDis = r.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Add).Sum(o => o.PriceAfterDis);
                    foreach (var item in r.CustomerItemGroup.Where(o => o.IsAddMinus == (int)AddMinusType.Add || o.IsAddMinus == (int)AddMinusType.Normal))
                    {//正常项目和加项即为以下统计的有效项目
                        if (item.CheckState == (int)ProjectIState.GiveUp)
                        {//判断放弃检查的项目
                            data.FangQiItemGroup += item.ItemGroupName + ",";
                            data.FangQiItemGroupPrice += item.PriceAfterDis;
                        }
                        else if (item.PayerCat == (int)PayerCatType.NoCharge || item.PayerCat == (int)PayerCatType.PersonalCharge)
                        {//除了放弃检查的项目中找到未支付和个人已支付的，就是个人支付金额
                            data.PersonalPayPrice += item.PriceAfterDis;
                        }
                        if (item.CheckState == (int)ProjectIState.Complete || item.CheckState == (int)ProjectIState.Part)
                        {//已检金额
                            data.CheckPrice += item.PriceAfterDis;
                        }
                        if (item.IsAddMinus == (int)AddMinusType.Add)
                        {//在加项中判断团付加项、个人加项
                            if (item.PayerCat == (int)PayerCatType.ClientCharge)
                            {
                                data.AddItemGroupTeam += item.ItemGroupName + ",";
                                data.AddItemGroupTeamPrice += item.PriceAfterDis;
                            }
                            else if (item.PayerCat != (int)PayerCatType.GiveCharge)
                            {
                                data.AddItemGroupPersonl += item.ItemGroupName + ",";
                                data.AddItemGroupPersonlPrice += item.PriceAfterDis;
                            }
                        }
                        if (item.PayerCat == (int)PayerCatType.GiveCharge)
                        {//在其他项目中判断赠送项目
                            data.ZengjianItemGroup += item.ItemGroupName + ",";
                            data.ZengjianItemGroupPrice += item.PriceAfterDis;
                        }
                    }
                    data.SumPrice = decimal.Parse(data.SumPrice.ToString("0.00"));
                    data.PriceAfterDis = decimal.Parse(data.PriceAfterDis.ToString("0.00"));
                    data.PersonalPayPrice = decimal.Parse(data.PersonalPayPrice.ToString("0.00"));
                    data.AddItemGroupPersonlPrice = decimal.Parse(data.AddItemGroupPersonlPrice.ToString("0.00"));
                    data.AddItemGroupTeamPrice = decimal.Parse(data.AddItemGroupTeamPrice.ToString("0.00"));
                    data.CheckPrice = decimal.Parse(data.CheckPrice.ToString("0.00"));
                    data.FangQiItemGroupPrice = decimal.Parse(data.FangQiItemGroupPrice.ToString("0.00"));
                    data.ZengjianItemGroupPrice = decimal.Parse(data.ZengjianItemGroupPrice.ToString("0.00"));
                    if (!string.IsNullOrWhiteSpace(data.AddItemGroupPersonl))
                        data.AddItemGroupPersonl = data.AddItemGroupPersonl.Remove(data.AddItemGroupPersonl.Length - 1);
                    if (!string.IsNullOrWhiteSpace(data.AddItemGroupTeam))
                        data.AddItemGroupTeam = data.AddItemGroupTeam.Remove(data.AddItemGroupTeam.Length - 1);
                    if (!string.IsNullOrWhiteSpace(data.FangQiItemGroup))
                        data.FangQiItemGroup = data.FangQiItemGroup.Remove(data.FangQiItemGroup.Length - 1);
                    if (!string.IsNullOrWhiteSpace(data.ZengjianItemGroup))
                        data.ZengjianItemGroup = data.ZengjianItemGroup.Remove(data.ZengjianItemGroup.Length - 1);
                    result.PayPersons.Add(data);
                }
            }
            return result;
        }
        /// <summary>
        /// 根据分组查询团体结账单
        /// </summary>
        /// <param name="input">单位预约id</param>
        public StatisticsTTJZDTeamTTJZDDto StatisticsTeamTTJZD(EntityDto<Guid> input)
        {
            var result = new StatisticsTTJZDTeamTTJZDDto() { TeamTTJZDs = new List<TeamTTJZD>() };
            var itemGroups = _CustomerItemGroup.GetAll().Where(o => o.CustomerRegBM.ClientRegId == input.Id && (o.IsAddMinus == (int)AddMinusType.Add || o.IsAddMinus == (int)AddMinusType.Normal));
            var groups = itemGroups.GroupBy(o => o.ItemGroupBM_Id).Select(o => o.FirstOrDefault());
            foreach (var g in groups)
            {
                var data = new TeamTTJZD();
                var items = itemGroups.Where(o => o.ItemGroupBM_Id == g.ItemGroupBM_Id);
                var itemChecked = items.Where(o => o.CheckState == (int)ProjectIState.Complete || o.CheckState == (int)ProjectIState.Part);
                data.CheckedNum = itemChecked.Count();
                if (data.CheckedNum != 0)
                {
                    data.CheckMoney = itemChecked.Sum(o => o.PriceAfterDis);
                }
                data.UnCheckedNum = items.Where(o => o.CheckState == (int)ProjectIState.Not).Count();
                if (g.IsAddMinus == (int)AddMinusType.Normal)
                    data.IsAdd = "正常项";
                else if (g.IsAddMinus == (int)AddMinusType.Add)
                    data.IsAdd = "加项";
                data.IsAddMinus = g.IsAddMinus;
                if (g.PayerCat == (int)PayerCatType.ClientCharge)
                    data.ItemCategory = "团检";
                else if (g.PayerCat == (int)PayerCatType.PersonalCharge || g.PayerCat == (int)PayerCatType.NoCharge)
                    data.ItemCategory = "个人";
                else if (g.PayerCat == (int)PayerCatType.GiveCharge)
                    data.ItemCategory = "赠检";
                data.ItemGroupName = g.ItemGroupName;
                data.PriceAfterDis = items.Sum(o => o.PriceAfterDis);
                data.SumMoney = items.Sum(o => o.ItemPrice);
                data.TeamName = g.CustomerRegBM.ClientTeamInfo.TeamName;
                result.TeamTTJZDs.Add(data);
            }
            if (itemGroups.Any(o => o.IsAddMinus == (int)AddMinusType.Add))
            {
                result.isAddMoney = itemGroups.Where(o => o.IsAddMinus == (int)AddMinusType.Add).Sum(o => (o.GRmoney + o.TTmoney));
            }
            if (itemGroups.Any(o => o.IsAddMinus == (int)AddMinusType.Add && o.CheckState == (int)ProjectIState.Complete || o.CheckState == (int)ProjectIState.Part))
            {
                var additems = itemGroups.Where(o => o.IsAddMinus == (int)AddMinusType.Add && (o.CheckState == (int)ProjectIState.Complete || o.CheckState == (int)ProjectIState.Part));
                if (additems?.ToList().Count() > 0)
                    result.isCheckedAddMoney = additems.Sum(c => (c.PriceAfterDis));
            }
            if (itemGroups.Any(o => o.IsAddMinus == (int)AddMinusType.Add))
                result.isDisAddMoney = itemGroups.Where(o => o.IsAddMinus == (int)AddMinusType.Add).Sum(o => o.PriceAfterDis);
            result.TeamTTJZDs = result.TeamTTJZDs.OrderBy(o => o.TeamName).ThenBy(o => o.ItemCategory).ThenBy(o => o.ItemGroupName).ToList();
            return result;
        }
        /// <summary>
        /// 获取单位预约列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClientRegNameComDto>> getClientRegNameCom()
        {
            //var clientregname = _clientRegRepository.GetAllIncluding(o => o.ClientInfo)./*Select(o => new { o.StartCheckDate, o.ClientInfo.ClientBM, o.ClientInfo.ClientName, o.ClientInfo.HelpCode, o.CreationTime, o.Id }).*/OrderByDescending(o => o.CreationTime);
            //return await clientregname.ProjectToListAsync<ClientRegNameComDto>(GetConfigurationProvider<Core.Company.TjlClientReg, ClientRegNameComDto>());
            ////return clientregname.MapTo<List<ClientRegNameComDto>>();
            var query = _clientRegRepository.GetAll();
            var userBM = _User.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }
            var clientregname = query.Select(o => new ClientRegNameComDto
            {StartCheckDate= o.StartCheckDate,
                ClientBM=  o.ClientInfo.ClientBM,
                ClientName=o.ClientInfo.ClientName,
                HelpCode=o.ClientInfo.HelpCode,
                CreationTime = o.CreationTime,
                Id =o.Id,
             ClientRegBM=o.ClientRegBM}).OrderByDescending(o => o.CreationTime).ToListAsync();

            return await clientregname;
           // return await clientregname.ProjectToListAsync<ClientRegNameComDto>(GetConfigurationProvider<ClientRegNameComDto, ClientRegNameComDto>());
            //return clientregname.MapTo<List<ClientRegNameComDto>>();

        }
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        public List<ClientRegLQDto> getClientRegLQ(SearchClientRegLQDto input)
        {
            var clientregname = _clientRegRepository.GetAll();
            if (!string.IsNullOrEmpty(input.CusCabitBM))
            {
                clientregname = clientregname.Where(o => o.CusCabitBM == input.CusCabitBM);
            }
            if (input.Id != Guid.Empty)
            {
                clientregname = clientregname.Where(o => o.Id == input.Id);
            }
            if (input.StartCheckDate.HasValue && input.EndCheckDate.HasValue)
            {
                var star = input.StartCheckDate.Value;
                var end = input.EndCheckDate.Value.AddDays(1);
                clientregname = clientregname.Where(o => o.StartCheckDate >= star && o.StartCheckDate < end);
            }
            if (input.StarCusCabitTime.HasValue && input.EndCusCabitTime.HasValue)
            {
                var star = input.StarCusCabitTime.Value;
                var end = input.EndCusCabitTime.Value.AddDays(1);
                clientregname = clientregname.Where(o => o.CusCabitTime >= star && o.StartCheckDate < end);
            }
            if (input.CusCabitState.HasValue)
            {
                if (input.CusCabitState == 1)
                {
                    clientregname = clientregname.Where(o => o.CusCabitState == 1);
                }
                if (input.CusCabitState == 0)
                {
                    clientregname = clientregname.Where(o => o.CusCabitState != 1);
                }
            }
            var userBM = _User.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                clientregname = clientregname.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            var sss = clientregname.Select(o => new ClientRegLQDto
            {
                ClientBM = o.ClientInfo.ClientBM,
                ClientName = o.ClientInfo.ClientName,
                CusCabitBM = o.CusCabitBM,
                CusCabitState = o.CusCabitState,
                CusCabitTime = o.CusCabitTime,
                HelpCode = o.ClientInfo.HelpCode,
                Id = o.Id,
                StartCheckDate = o.StartCheckDate
            }).ToList();
            return sss;
        }
        public UpClientMZDto getClientMZ(EntityDto<Guid> input)
        { 
            var client = _clientRegRepository.Get(input.Id);
            return client.MapTo<UpClientMZDto>();
        }
        public void UpClientMZ(UpClientMZDto input)
        {
            _clientRegRepository.Update(input.Id, e => { e.ClientRegBM = input.ClientRegBM; e.Remark = input.Remark; });
        }
        /// <summary>
        /// 团体结账单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InquireGroupCustomerRegDto GetInquireGroupCustomerRegDtos(SearchGroupCustomerRegDto input)
        {
            var result = new InquireGroupCustomerRegDto() { PayPersons = new List<Pas>() };
            var query = _customerRegRepository.GetAll();
            if (input.ClientRegId != null)
            {
                query = query.Where(o => o.ClientRegId == input.ClientRegId);
            }
            //体检日期
            if (input.DateType.HasValue && input.DateType == 2)
            {
                if (input.StartDateTime != null)
                {
                    query = query.Where(o => o.BookingDate !=null && o.BookingDate >= input.StartDateTime);
                }
                if (input.EndDateTime != null)
                {
                    query = query.Where(o => o.BookingDate != null && o.BookingDate <= input.EndDateTime);
                }
            }
            //登记日期
            else if (input.DateType.HasValue && input.DateType == 1)
            {
                if (input.StartDateTime != null)
                {
                    query = query.Where(o => o.LoginDate!=null && o.LoginDate >= input.StartDateTime);
                }
                if (input.EndDateTime != null)
                {
                    query = query.Where(o => o.LoginDate != null && o.LoginDate <= input.EndDateTime);
                }
            }
            else
            {
                if (input.StartDateTime != null)
                {
                    query = query.Where(o => o.ClientReg.StartCheckDate >= input.StartDateTime);
                }
                if (input.EndDateTime != null)
                {
                    query = query.Where(o => o.ClientReg.EndCheckDate <= input.EndDateTime);
                }
            }
         var PayPersons = query.Select(r=>new Pas {
              BookingDate=r.BookingDate,
               LoginDate=r.LoginDate,
             ClientName = r.ClientInfo==null?"": r.ClientInfo.ClientName,
            CustomerRegNum = r.ClientRegNum,
            CustomerBM = r.CustomerBM,
            Name = r.Customer.Name,
            Sex= r.Customer.Sex,           
            Age = r.Customer.Age,
            MarriageStatus = r.Customer.MarriageStatus == 10 ? "未婚" : "已婚",
            Department = r.Customer.Department,
            WorkNumber = r.Customer.WorkNumber,
            CheckSate = r.CheckSate,
            HasCheck = r.CheckSate != 1 ? "●" : "",
            NoCheck = r.CheckSate == 1 ? "▲" : "",
            CostPrice = (r.McusPayMoney.ClientMoney - r.McusPayMoney.ClientAddMoney),
            AddItemGroupTeamPrice = r.McusPayMoney.ClientAddMoney,
            SumPrice = r.McusPayMoney.ClientMoney,
        }).OrderBy(o=>o.CustomerRegNum).ThenBy(p=>p.CustomerBM).ToList();
            result.PayPersons = PayPersons;
           // foreach (var r in query)
           //{
           //     var data = new Pas();
           //     data.ClientName = r.ClientInfo?.ClientName;
           //     data.CustomerRegNum = r.CustomerRegNum;
           //     data.CustomerBM = r.CustomerBM;
           //     data.Name = r.Customer.Name;
           //     data.Sex = r.Customer.Sex;
           //     data.Name = r.Customer.Name;
           //     data.Age = r.Customer.Age;
           //     data.MarriageStatus = r.Customer.MarriageStatus == 10 ? "未婚" : "已婚";
           //     data.Department = r.Customer.Department;
           //     data.WorkNumber = r.Customer.WorkNumber;
           //     data.CheckSate = r.CheckSate;
           //     data.HasCheck = r.CheckSate != 1 ? "●" : "";
           //     data.NoCheck = r.CheckSate == 1 ? "▲" : "";
           //     data.CostPrice = (r.McusPayMoney.ClientMoney - r.McusPayMoney.ClientAddMoney);
           //     data.AddItemGroupTeamPrice = r.McusPayMoney.ClientAddMoney;
           //     data.SumPrice = r.McusPayMoney.ClientMoney;
           //     //if (r.ItemSuitBM!= null)
           //     //{
           //     //    data.CostPrice = r.ItemSuitBM.CostPrice.Value;
           //     //}
           //     //else
           //     //{
           //     //    data.CostPrice = decimal.Parse(data.CostPrice.ToString("0.00"));
           //     //}
           //    // data.SumPrice = r.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemPrice);              
           //     //foreach (var item in r.CustomerItemGroup.Where(o => o.IsAddMinus == (int)AddMinusType.Add || o.IsAddMinus == (int)AddMinusType.Normal))
           //     //{
           //     //    if (item.IsAddMinus == (int)AddMinusType.Add)
           //     //    {//在加项中判断团付加项、个人加项
           //     //        if (item.PayerCat == (int)PayerCatType.ClientCharge)
           //     //        {
           //     //            data.AddItemGroupTeamPrice += item.PriceAfterDis;
           //     //        }
           //     //    }
           //     //}
           //     result.PayPersons.Add(data);
           // }
            return result;
            
        }
        public List<MonrySummaryDto> GetMonrySummary(ApplicationfromDto input)
        {
            //var query = _CustomerItemGroup.GetAll().Where(o => o.PayerCat != (int)PayerCatType.ClientCharge);
            //var query = _TjlApplicationForm.GetAll().Where(o => o.SQSTATUS != 3);
            //if (input.StartDateTime != null)
            //{
            //    query = query.Where(o => o.CreationTime >= input.StartDateTime);
            //}
            //if (input.EndDateTime != null)
            //{
            //    query = query.Where(o => o.CreationTime <= input.EndDateTime);
            //}
            //query = query.Where(o => o.ClientRegId != null);
            ////单位的
            //var result = query.Where(o=> o.ClientRegId !=null).GroupBy(o=>  o.ClientReg.ClientInfo.ClientName).Select(o => new MonrySummaryDto
            //{
            //    ClientName = o.Key,
            //    GroupToPay = o.FirstOrDefault().ClientReg.CustomerReg.Where(n=>n.RegisterState !=1).Sum(m=>m.McusPayMoney.ClientMoney),
            //    GroupHavePay = o.Where(p=>p.SQSTATUS==2).Sum(t=>t.FYZK),
            //    PersonageToPay = o.FirstOrDefault().ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalMoney),
            //    PersonageHavePay = o.FirstOrDefault().ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalPayMoney),
            //}).ToList();


           //收费时间
            if (input.DateType.HasValue && input.DateType == 1)
            {
                //单位
                var query = _mReceiptInfo.GetAll();
                if (input.StartDateTime != null)
                {
                    query = query.Where(o => o.ChargeDate >= input.StartDateTime);
                }
                if (input.EndDateTime != null)
                {
                    query = query.Where(o => o.ChargeDate <= input.EndDateTime);
                }

                
                var result = query.GroupBy(o => new { o.ClientRegId,o.ClientReg }).Select(o => new MonrySummaryDto
                {
                    ClientName = o.Key.ClientRegId==null?"个人":o.Key.ClientReg.ClientInfo.ClientName,
                    GroupToPay = o.Key.ClientRegId == null ? 0:o.Key.ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.ClientMoney),
                    GroupHavePay = o.Key.ClientRegId == null ? 0:o.Sum(p=>p.Actualmoney),
                    PersonageToPay = o.Key.ClientRegId == null?o.Select(p=>p.CustomerReg).Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalMoney):
                    o.Key.ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalMoney),
                    PersonageHavePay = o.Key.ClientRegId == null ? o.Sum(p => p.Actualmoney) :
                    o.Key.ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalPayMoney),
                }).ToList();

              
                return result.MapTo<List<MonrySummaryDto>>();
            }
            //申请单时间
            else if (input.DateType.HasValue && input.DateType == 2)
            {
                var query = _TjlApplicationForm.GetAll().Where(o => o.SQSTATUS != 3);
                if (input.StartDateTime != null)
                {
                    query = query.Where(o => o.CreationTime >= input.StartDateTime);
                }
                if (input.EndDateTime != null)
                {
                    query = query.Where(o => o.CreationTime <= input.EndDateTime);
                }
                query = query.Where(o => o.ClientRegId != null);
                //单位的
                var result = query.Where(o => o.ClientRegId != null).GroupBy(o => new { o.ClientReg.ClientInfo.ClientName,o.ClientReg}).Select(o => new MonrySummaryDto
                {
                    ClientName = o.Key.ClientName  ,
                    GroupToPay = o.Key.ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.ClientMoney),
                    GroupHavePay = o.Where(p => p.SQSTATUS == 2).Sum(t => t.FYZK),
                    PersonageToPay = o.FirstOrDefault().ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalMoney),
                    PersonageHavePay = o.FirstOrDefault().ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalPayMoney),
                }).ToList();
                //个人
             
                var cuslist = query.Where(o => o.ClientRegId == null).Select(o=>new
                {
                    o.CustomerReg.McusPayMoney.PersonalMoney, o.CustomerReg.McusPayMoney.PersonalPayMoney  
                }).ToList();

                MonrySummaryDto monrySummaryDto = new MonrySummaryDto();
                monrySummaryDto.ClientName = "个人";
                monrySummaryDto.GroupToPay = 0;
                monrySummaryDto.GroupHavePay = 0;
                monrySummaryDto.PersonageToPay = cuslist.Sum(o => o.PersonalMoney);
                monrySummaryDto.PersonageHavePay = cuslist.Sum(o => o.PersonalPayMoney);

                result.Add(monrySummaryDto);
                return result.MapTo<List<MonrySummaryDto>>();
            }
            else
            {  
                //单位
                var query = _clientRegRepository.GetAll();
                if (input.StartDateTime != null)
                {
                    query = query.Where(o => o.CustomerReg.Any(p => p.LoginDate >= input.StartDateTime));
                }
                if (input.EndDateTime != null)
                {
                    query = query.Where(o => o.CustomerReg.Any(p => p.LoginDate <= input.EndDateTime));
                }

                //单位的
                var result = query.GroupBy(o => new { o.ClientInfo.ClientName, o.Id }).Select(o => new MonrySummaryDto
                {
                    ClientName = o.Key.ClientName,
                    GroupToPay = o.FirstOrDefault().CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.ClientMoney),
                    GroupHavePay = o.FirstOrDefault().ApplicationForm.Where(p => p.SQSTATUS == 2).Sum(t => t.FYZK),
                    PersonageToPay = o.FirstOrDefault().CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalMoney),
                    PersonageHavePay = o.FirstOrDefault().CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.PersonalPayMoney),
                }).ToList();

                //个人
                var queryGR = _customerRegRepository.GetAll().Where(o => o.ClientRegId == null && o.RegisterState != 1);
                if (input.StartDateTime != null)
                {
                    queryGR = queryGR.Where(o => o.LoginDate >= input.StartDateTime);
                }
                if (input.EndDateTime != null)
                {
                    queryGR = queryGR.Where(o => o.LoginDate <= input.EndDateTime);
                }
                var cuslist = queryGR.Where(o => o.McusPayMoney != null && o.McusPayMoney.PersonalMoney != null).
                    Select(o => new { o.McusPayMoney.PersonalMoney, o.McusPayMoney.PersonalPayMoney }).ToList();
                MonrySummaryDto monrySummaryDto = new MonrySummaryDto();
                monrySummaryDto.ClientName = "个人";
                monrySummaryDto.GroupToPay = 0;
                monrySummaryDto.GroupHavePay = 0;
                monrySummaryDto.PersonageToPay = cuslist.Sum(o => o.PersonalMoney);
                monrySummaryDto.PersonageHavePay = cuslist.Sum(o => o.PersonalPayMoney);

                result.Add(monrySummaryDto);
                return result.MapTo<List<MonrySummaryDto>>();
            }
        }
        /// <summary>
        /// 获取单位自费金额 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<MonrySummaryDto> GetGRPayMent(ApplicationfromDto input)
        {           

            //单位
            var query = _mReceiptInfo.GetAll().Where(p=>p.CustomerRegId !=null);
            if (input.StartDateTime != null)
            {
                query = query.Where(p =>p.ChargeDate>= input.StartDateTime);
            }
            if (input.EndDateTime != null)
            {
                query = query.Where(p => p.ChargeDate <input.EndDateTime);
            }
           var result= query.GroupBy(o => o.CustomerReg.ClientInfo.ClientName).Select(
                p=>new MonrySummaryDto {
                 ClientName=p.Key??"个人",
                 PersonageToPay=p.Sum(o=>o.CustomerReg.McusPayMoney.PersonalMoney),
                 PersonageHavePay=p.Sum(o=>o.Actualmoney)}).ToList();
            return result;
        }
        /// <summary>
        /// 获取单位预约版本号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetClientVerSionDto getClientVerSion(EntityDto<Guid> input)
        {
            var client = _clientRegRepository.Get(input.Id);
            return client.MapTo<GetClientVerSionDto>();
        }

        /// <summary>
        /// 获取单位自费金额明细 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ClientCusMoneyListDto> GetClientCusMoneyList(ApplicationfromDto input)
        {

            //单位
            var query = _mReceiptInfo.GetAll().Where(p => p.CustomerRegId != null && p.CustomerReg !=null );
            if (input.StartDateTime != null)
            {
                query = query.Where(p => p.ChargeDate >= input.StartDateTime);
            }
            if (input.EndDateTime != null)
            {
                query = query.Where(p => p.ChargeDate < input.EndDateTime);
            }
            var result = query.Select(
                 p => new ClientCusMoneyListDto
                 { CustomerBM=p.CustomerReg.CustomerBM,
                  Age=p.Customer.Age,
                   IDCardNo=p.Customer.IDCardNo,
                    Name=p.Customer.Name,
                     Sex=p.Customer.Sex,
                      
                     ClientName = p.CustomerReg.ClientInfo.ClientName,
                     PersonageToPay = p.CustomerReg.McusPayMoney.PersonalMoney,
                     PersonageHavePay = p.Actualmoney,
                     ItemGroupNamelist = p.CustomerReg.CustomerItemGroup.Where(n=>n.IsAddMinus!= (int)AddMinusType.Minus).Select(o=> new ItemGroupNameDto { ItemGroupName= o.ItemGroupName }).ToList(),
                 }).ToList();


            return result;
        }
        /// <summary>
        /// 团体预约发送短信
        /// </summary>
        /// <param name="input"></param>
        public void SaveMessage(ShortMessageDto input)
        {
            var ShortMessage = input.MapTo<TjlShortMessage>();
            ShortMessage.Id = System.Guid.NewGuid();      
            _tjlShortMessage.Insert(ShortMessage);
            var cusreg = _customerRegRepository.Get(input.CustomerRegId);
            if (input.MessageType == 1)
            {
                cusreg.RegMessageState = 1;
            }
            else if (input.MessageType == 2)
            { cusreg.ReportMessageState = 1; }
            else if (input.MessageType == 3)
            { cusreg.VisitMessageState = 1; }
            else if (input.MessageType == 4)
            { cusreg.CrissMessageState = 1; }
            _customerRegRepository.Update(cusreg);


        }
    }
}