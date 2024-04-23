using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using DevExpress.Data.ODataLinq.Helpers;
using HealthExaminationSystem.Enumerations;
using Newtonsoft.Json;
using Sw.Hospital.HealthExamination.Drivers;
using Sw.Hospital.HealthExamination.Drivers.Models.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg
{
    [AbpAuthorize]
    public class CustomerAppService : MyProjectAppServiceBase, ICustomerAppService
    {
        /// <summary>
        /// 单位预约信息仓储
        /// </summary>
        private readonly IRepository<TjlClientReg, Guid> _clientRegRepository;

        /// <summary>
        /// 单位分组表仓储
        /// </summary>
        private readonly IRepository<TjlClientTeamInfo, Guid> _clientTeamInfoRepository;

        /// <summary>
        /// 单位分组登记项目仓储
        /// </summary>
        private readonly IRepository<TjlClientTeamRegitem, Guid> _clientTeamRegItemRepository;

        /// <summary>
        /// 预约组合表仓储
        /// </summary>
        private readonly IRepository<TjlCustomerItemGroup, Guid> _customerItemGroupRepository;

        /// <summary>
        /// 项目预约表仓储
        /// </summary>
        private readonly IRepository<TjlCustomerRegItem, Guid> _customerRegItemRepository;

        /// <summary>
        /// 预约表仓储
        /// </summary>
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository;

        /// <summary>
        /// 体检人表仓储
        /// </summary>
        private readonly IRepository<TjlCustomer, Guid> _customerRepository;

        /// <summary>
        /// 科室编码表仓储
        /// </summary>
        private readonly IRepository<TbmDepartment, Guid> _departmentRepository;

        /// <summary>
        /// 项目组合编码表仓储
        /// </summary>
        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository;

        private readonly IRepository<TbmItemSuitItemGroupContrast, Guid> _ItemSuitItemGroupContrast; 

        /// <summary>
        /// 套餐编码表仓储
        /// </summary>
        private readonly IRepository<TbmItemSuit, Guid> _itemSuitRepository;
        /// <summary>
        /// 个人应收已收表仓储
        /// </summary>
        private readonly IRepository<TjlMcusPayMoney, Guid> _mcusPayMoneyRepository;
        /// <summary>
        /// 总检表
        /// </summary>
        private readonly IRepository<TjlInterFace, Guid> _InterFace;
        /// <summary>
        /// 人员列表仓储
        /// </summary>
        private readonly IRepository<PersonnelCategory, Guid> _personalCategoryRepository;

        /// <summary>
        /// 用户表仓储
        /// </summary>
        private readonly IRepository<User, long> _userRepository;
        /// <summary>
        /// 获取1+X问卷
        /// </summary>

        private readonly IRepository<TbmOneAddXQuestionnaire, Guid> _OneAddXQuestionnaire;
        /// <summary>
        /// 人员问卷
        /// </summary>
        private readonly IRepository<TjlCustomerQuestion, Guid> _CustomerQuestion;
        /// <summary>
        /// 人员加项包
        /// </summary>
        private readonly IRepository<TjlCustomerAddPackage, Guid> _CustomerAddPackage;
        /// <summary>
        /// 人员加项包项目
        /// </summary>
        private readonly IRepository<TjlCustomerAddPackageItem, Guid> _CustomerAddPackageItem;
        /// <summary>
        /// 加项包项目
        /// </summary>
        private readonly IRepository<TbmItemSuitItemGroupContrast, Guid> _itemSuitItemGroupRepository;
        /// <summary>
        /// 挂号科室
        /// </summary>
        private readonly IRepository<TbmBasicDictionary, Guid> _tbmBasicDictionary;
        /// <summary>
        /// 申请单号
        /// </summary>
        private readonly IRepository<TjlApplicationForm, Guid> _TjlApplicationForm;
        /// <summary>
        ///危害因素
        /// </summary>
        private readonly IRepository<TbmOccHazardFactor, Guid> _TbmOccHazardFactor;
        private readonly IRepository<TbmMChargeType, Guid> _tbmMChargeType;//支付方式

        private readonly IRepository<Tenant, int> _Tenant;
        private readonly IRepository<TjlLisApply, Guid> _TjlLisApply;
        private readonly IRepository<TjlCustomerBarPrintInfo, Guid> _TjlCustomerBarPrintInfo;
        private readonly IRepository<TjlCustomerBarPrintInfoItemGroup, Guid> _CustomerBarPrintInfoItemGroup;

        private readonly IRepository<TbmBasicDictionary, Guid> _BasicDictionaryy;
        private readonly IRepository<TbmCardType, Guid> _TbmCardType;
        private readonly IRepository<TbmCard, Guid> _TbmCard;

        private readonly IRepository<TbmPriceSynchronize, Guid> _TbmPriceSynchronize;
        private readonly IRepository<TbmGroupRePriceSynchronizes, Guid> _TbmGroupRePriceSynchronizes;

        private readonly IRepository<TbmOccTargetDisease, Guid> _OccTargetDisease;
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private readonly ISqlExecutor _sqlExecutor;
        private readonly IIDNumberAppService _IDNumberAppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IChargeAppService _ChargeAppService;

        private readonly IIDNumberAppService _idNumberAppService;

        public CustomerAppService(
            IRepository<TbmBasicDictionary, Guid> tbmBasicDictionary,
            IRepository<TjlCustomer, Guid> customerRepository,
            IRepository<TjlCustomerReg, Guid> customerRegRepository,
            IRepository<TjlCustomerItemGroup, Guid> customerItemGroupRepository,
            IRepository<TjlClientTeamInfo, Guid> clientTeamInfoRepository,
            IRepository<TjlClientReg, Guid> clientRegRepository,
            IRepository<TbmItemGroup, Guid> itemGroupRepository,
            IRepository<TjlCustomerRegItem, Guid> customerRegItemRepository,
            IRepository<TjlMcusPayMoney, Guid> mcusPayMoneyRepository,
            IRepository<TbmItemSuit, Guid> itemSuitRepository,
            IRepository<User, long> userRepository,
            IRepository<TjlClientTeamRegitem, Guid> clientTeamRegItemRepository,
            IRepository<TbmDepartment, Guid> departmentRepository,
            IRepository<PersonnelCategory, Guid> personalCategoryRepository,
            IRepository<TbmOneAddXQuestionnaire, Guid> OneAddXQuestionnaire,
            IRepository<TbmItemSuitItemGroupContrast, Guid> itemSuitItemGroupRepository,
            IRepository<TjlCustomerQuestion, Guid> CustomerQuestion,
            IRepository<TjlCustomerAddPackage, Guid> CustomerAddPackage,
            IRepository<TjlCustomerAddPackageItem, Guid> CustomerAddPackageItem,
              ISqlExecutor sqlExecutor,
              IRepository<TjlApplicationForm, Guid> TjlApplicationForm,
              IIDNumberAppService IDNumberAppService,
              ICommonAppService CommonAppService,
              IRepository<TbmOccHazardFactor, Guid> TbmOccHazardFactor,
              IRepository<Tenant, int> Tenant,
              IRepository<TjlLisApply, Guid> TjlLisApply,
              IRepository<TjlCustomerBarPrintInfo, Guid> TjlCustomerBarPrintInfo,
              IRepository<TbmBasicDictionary, Guid> BasicDictionaryy,
              IRepository<TbmCardType, Guid> TbmCardType,
              IRepository<TbmCard, Guid> TbmCard,
              IRepository<TbmMChargeType, Guid> tbmMChargeType,
              IChargeAppService ChargeAppService,
              IRepository<TbmPriceSynchronize, Guid> TbmPriceSynchronize,
              IRepository<TbmItemSuitItemGroupContrast, Guid> ItemSuitItemGroupContrast,
              IRepository<TbmGroupRePriceSynchronizes, Guid> TbmGroupRePriceSynchronizes,
              IRepository<TjlCustomerBarPrintInfoItemGroup, Guid> CustomerBarPrintInfoItemGroup,
              IRepository<TbmOccTargetDisease, Guid> OccTargetDisease,
                IIDNumberAppService idNumberAppService,
                 IRepository<TjlInterFace, Guid> InterFace
        )
        {
            _tbmBasicDictionary = tbmBasicDictionary;
            _customerRepository = customerRepository;
            _customerRegRepository = customerRegRepository;
            _customerItemGroupRepository = customerItemGroupRepository;
            _clientTeamInfoRepository = clientTeamInfoRepository;
            _clientRegRepository = clientRegRepository;
            _customerRegItemRepository = customerRegItemRepository;
            _itemGroupRepository = itemGroupRepository;
            _mcusPayMoneyRepository = mcusPayMoneyRepository;
            _itemSuitRepository = itemSuitRepository;
            _userRepository = userRepository;
            _clientTeamRegItemRepository = clientTeamRegItemRepository;
            _departmentRepository = departmentRepository;
            _personalCategoryRepository = personalCategoryRepository;
            _OneAddXQuestionnaire = OneAddXQuestionnaire;
            _itemSuitItemGroupRepository = itemSuitItemGroupRepository;
            _CustomerQuestion = CustomerQuestion;
            _CustomerAddPackage = CustomerAddPackage;
            _CustomerAddPackageItem = CustomerAddPackageItem;
            _sqlExecutor = sqlExecutor;
            _TjlApplicationForm = TjlApplicationForm;
            _IDNumberAppService = IDNumberAppService;
            _commonAppService = CommonAppService;
            _TbmOccHazardFactor = TbmOccHazardFactor;
            _Tenant = Tenant;
            _TjlLisApply = TjlLisApply;
            _TjlCustomerBarPrintInfo = TjlCustomerBarPrintInfo;
            _BasicDictionaryy = BasicDictionaryy;
            _TbmCardType = TbmCardType;
            _TbmCard = TbmCard;
            _tbmMChargeType = tbmMChargeType;
            _ChargeAppService = ChargeAppService;
            _TbmPriceSynchronize = TbmPriceSynchronize;
            _ItemSuitItemGroupContrast = ItemSuitItemGroupContrast;
            _TbmGroupRePriceSynchronizes = TbmGroupRePriceSynchronizes;
            _CustomerBarPrintInfoItemGroup = CustomerBarPrintInfoItemGroup;

            _OccTargetDisease = OccTargetDisease;
            _idNumberAppService = idNumberAppService;
            _InterFace = InterFace;
        }
        /// <summary>
        /// 登记 保存客户信息、客户登记信息、选择套餐数据
        /// </summary>
        public List<QueryCustomerRegDto> RegCustomer(List<QueryCustomerRegDto> inputDatas)
        {
            if (inputDatas == null)
            {
                throw new FieldVerifyException("无数据", "无数据");
            }
            if (inputDatas.Count == 0)
            {
                throw new FieldVerifyException("无数据", "无数据");
            }
            var result = new List<QueryCustomerRegDto>();
            List<Guid> RiskIds = new List<Guid>();
            foreach (var input in inputDatas)
            {
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                string logText = "";
                string logDetail = "";
                #region  遍历体检人 开始

                #region 条件判断 开始
                if (input.RegisterState == (int)RegisterState.Yes)
                {
                    if (input.CheckSate == (int)ProjectIState.Not || !input.CheckSate.HasValue || !input.LoginDate.HasValue)
                    {
                        input.LoginDate = DateTime.Now;
                    }
                    //登记号
                    if ((input.CheckSate == (int)ProjectIState.Not || !input.CheckSate.HasValue) && !input.CustomerRegNum.HasValue)
                    {
                        //int cusRegBM = 1;
                        //var dttime =DateTime.Parse( System.DateTime.Now.ToShortDateString());
                        //var MaxCusRegBM = _customerRegRepository.GetAll().Where(o => o.LoginDate>= dttime).Select(o => o.CustomerRegNum).Max();
                        //if (MaxCusRegBM != null)
                        //{
                        //    cusRegBM = int.Parse(MaxCusRegBM.ToString()) + 1;

                        //}
                        //input.CustomerRegNum = cusRegBM;
                        //新登记号生成 方法
                        input.CustomerRegNum = _idNumberAppService.CreateRegNum();
                        #region 遵义需求登记重新生成登记号
                        var cxsc = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ForegroundFunctionControl" && p.Value == 22)?.Remarks;
                        var tm = System.DateTime.Now.ToString("yyMMdd");
                        if (!string.IsNullOrEmpty(cxsc) && cxsc == "1" && !input.CustomerBM.Contains(tm))
                        {
                            input.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                        }
                        #endregion

                    }
                }
                var reData =
                    _customerRegRepository.FirstOrDefault(o =>
                        o.CustomerBM == input.CustomerBM && o.Id != input.Id); //判断登记信息中体检号是否重复
                if (reData != null)
                {
                    throw new FieldVerifyException("体检号重复", "体检号重复，请修改后登记。");
                }

                if (input.RegisterState == (int)RegisterState.Yes)
                {
                    if (input.CustomerItemGroup == null)
                    {
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                    }

                    if (input.CustomerItemGroup.Count == 0)
                    {
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                    }
                }
                #endregion 条件判断 结束


                #region 添加或更新客户信息 开始
                var cusGroupls = input.CustomerItemGroup;
                var ret = new TjlCustomerReg();
                var retCus = new TjlCustomer();
                var oldgroplist = new List<TjlCustomerItemGroup>();
                var customer = input.Customer;
                var oldcus = new TjlCustomer();
                if (!string.IsNullOrEmpty(input.Customer.IDCardNo))
                {
                    oldcus = _customerRepository.FirstOrDefault(o => o.IDCardNo == customer.IDCardNo
                    && o.Name == customer.Name && o.Sex == customer.Sex);
                    if (oldcus == null)
                    { oldcus = new TjlCustomer(); }
                }
                if (oldcus.Id == Guid.Empty && !string.IsNullOrWhiteSpace(customer.ArchivesNum))
                {
                    oldcus = _customerRepository.FirstOrDefault(o => o.ArchivesNum == customer.ArchivesNum);
                    //替检查不关联之前档案
                    if (input.ReplaceSate == 2)
                    {
                        //判断不同名或不是同一个身份证号则创建
                        if (oldcus != null && (oldcus.Name != customer.Name || (!string.IsNullOrEmpty(oldcus.IDCardNo) && !string.IsNullOrEmpty(customer.IDCardNo) && oldcus.IDCardNo != customer.IDCardNo)))
                        {
                            oldcus = null;
                            oldcus = new TjlCustomer();
                            customer.Id = Guid.Empty;
                            customer.ArchivesNum = input.CustomerBM;
                        }
                    }

                }
                if (oldcus != null)
                {
                    customer.Id = oldcus.Id;
                    //此次没有照片关联旧照片
                    if (oldcus.CusPhotoBmId.HasValue && !customer.CusPhotoBmId.HasValue)
                    {
                        customer.CusPhotoBmId = oldcus.CusPhotoBmId;
                    }
                }
                if (customer.Id == Guid.Empty)
                {
                    var customerEntity = customer.MapTo<TjlCustomer>();
                    customerEntity.Id = Guid.NewGuid();
                    //行业处理2021-12-31
                    if (input.ClientRegId.HasValue && string.IsNullOrEmpty(input.Customer.CustomerTrade))
                    {
                        var clietint = _clientRegRepository.Get(input.ClientRegId.Value).ClientInfo?.Clientlndutry;
                        if (!string.IsNullOrEmpty(clietint))
                        {
                            customerEntity.CustomerTrade = clietint;
                        }
                    }
                    customerEntity.NameAB = _commonAppService.GetHansBrief(new ChineseDto { Hans = customerEntity.Name })?.Brief;

                    retCus = _customerRepository.Insert(customerEntity);
                    logDetail += "添加档案：" + customerEntity.ArchivesNum + "；";
                }
                else
                {
                    var oldCustomer = input.Customer;
                    var customerEntity = _customerRepository.Get(customer.Id);
                    oldCustomer.ArchivesNum = customerEntity.ArchivesNum;
                    //行业处理2021-12-31
                    if (input.ClientRegId.HasValue && string.IsNullOrEmpty(input.Customer.CustomerTrade))
                    {
                        var clietint = _clientRegRepository.Get(input.ClientRegId.Value).ClientInfo?.Clientlndutry;
                        if (!string.IsNullOrEmpty(clietint))
                        {
                            oldCustomer.CustomerTrade = clietint;
                        }
                    }
                    oldCustomer.MapTo(customerEntity);
                    customerEntity.NameAB = _commonAppService.GetHansBrief(new ChineseDto { Hans = oldCustomer.Name })?.Brief;

                    retCus = _customerRepository.Update(customerEntity);
                    // CurrentUnitOfWork.SaveChanges();
                    logDetail += "修改档案：" + customerEntity.ArchivesNum + "；";

                }
                #endregion 添加或更新客户信息 结束

                input.Customer = null;

                #region 修改客户登记信息 开始
                //先根据体检号码查询一下库里是否有该体检号信息
                var reg = _customerRegRepository.FirstOrDefault(o => o.Id == input.Id);
                if (reg != null)
                {
                    if (reg.CustomerBM != reg.CustomerBM) //根据当前Id查询出登记信息，
                    {
                        input.Id = Guid.Empty;
                    }
                }
                if (input.Id == Guid.Empty)
                {
                    var data = input.MapTo<TjlCustomerReg>();
                    data.CheckSate = (int)PhysicalEState.Not;
                    data.Id = Guid.NewGuid();
                    data.Customer = retCus;

                    if (input.ClientTeamInfo_Id.HasValue)
                    {
                        //新增是直接根据分组信息增加该字段
                        data.ClientTeamInfo = _clientTeamInfoRepository.Get(input.ClientTeamInfo_Id.Value);
                        data.ItemSuitBMId = data.ClientTeamInfo.ItemSuit_Id;
                        data.ItemSuitName = data.ClientTeamInfo.ItemSuitName;
                        if (data.ClientTeamInfo.ClientReg != null)
                        {
                            data.ClientInfo = data.ClientTeamInfo.ClientReg.ClientInfo;
                            data.ClientType = data.ClientTeamInfo.ClientReg.ClientSate.ToString();
                            if (data.ClientInfo != null)
                            {
                                data.ClientInfoId = data.ClientInfo.Id;
                            }
                        }
                    }
                    else
                    {
                        if (input.ClientRegId.HasValue)
                        {
                            data.ClientReg = _clientRegRepository.Get(input.ClientRegId.Value);
                            if (data.ClientReg != null)
                            {
                                data.ClientRegId = data.ClientRegId;
                                data.ClientInfo = data.ClientInfo;
                                if (data.ClientInfo != null)
                                {
                                    data.ClientInfoId = data.ClientInfo.Id;
                                    data.ClientType = data.ClientInfo.ClientSate;
                                }
                                else
                                    data.ClientType = ((int)ClientSate.Normal).ToString();
                            }
                        }
                    }

                    data.CustomerItemGroup = null;
                    //职业健康危害因素
                    if (input.OccHazardFactors != null && input.OccHazardFactors.Count > 0)
                    {
                        data.OccHazardFactors.Clear();
                        if (data.OccHazardFactors == null)
                        {
                            data.OccHazardFactors = new List<TbmOccHazardFactor>();
                        }
                        foreach (var rick in input.OccHazardFactors)
                        {
                            var tbmHazar = _TbmOccHazardFactor.Get(rick.Id);
                            data.OccHazardFactors.Add(tbmHazar);

                            if (!RiskIds.Contains(rick.Id))
                            {
                                RiskIds.Add(rick.Id);
                            }
                        }
                    }
                    if (!data.AppointmentTime.HasValue)
                    {
                        data.AppointmentTime = data.BookingDate;
                    }
                    if (data.ClientRegId.HasValue)
                    {
                        int clientBM = 1;
                        var clietid = data.ClientRegId;
                        var MaxClientBM = _customerRegRepository.GetAll().Where(o => o.ClientRegId == clietid && o.ClientRegNum != null).Select(o => o.ClientRegNum).Max();
                        if (MaxClientBM != null)
                        {
                            clientBM = int.Parse(MaxClientBM.ToString()) + 1;

                        }
                        data.ClientRegNum = clientBM;

                    }
                    #region 健康证号处理
                    if (data.PhysicalType.HasValue)
                    {
                        var tjlbName = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                         && p.Value == data.PhysicalType)?.Text;
                        if (!string.IsNullOrEmpty(tjlbName) &&
                            (tjlbName.Contains("从业") || tjlbName.Contains("健康证") || tjlbName.Contains("食品")))
                        {

                            data.JKZBM = _idNumberAppService.CreateJKZBM();
                        }

                    }
                    #endregion
                    var userBM = _userRepository.Get(AbpSession.UserId.Value);
                    if (userBM != null && userBM.HospitalArea.HasValue)
                    {
                        data.HospitalArea = userBM.HospitalArea;
                    }
                    //出报告时间计算
                    if (data.LoginDate.HasValue)
                    {
                        TjlClientReg _clientReg = null;
                        if (data.ClientRegId.HasValue)
                        { _clientReg = _clientRegRepository.Get(data.ClientRegId.Value); }


                        if (_clientReg != null && _clientReg.ReportDays.HasValue
                            && _clientReg.ReportDays > 0)
                        {

                            data.ReportDate = data.LoginDate.Value.AddDays(_clientReg.ReportDays.Value);
                        }
                        else
                        {
                            if (data.PhysicalType.HasValue)
                            {
                                var days = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                                && p.Value == data.PhysicalType)?.Code;
                                if (!string.IsNullOrEmpty(days) && int.TryParse(days, out int reDays))
                                {
                                    data.ReportDate = data.LoginDate.Value.AddDays(reDays);

                                }
                            }
                        }
                    }
                    data = _customerRegRepository.Insert(data);
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw;
                    //}
                    // CurrentUnitOfWork.SaveChanges();
                    ret = data;
                    logDetail += "添加登记：" + data.CustomerBM + "；";
                    logText = "添加登记：" + data.CustomerBM + "；"; ;
                    if (!string.IsNullOrEmpty(data.ItemSuitName))
                    {
                        logDetail += "套餐：" + data.ItemSuitName + "；";
                        logText += "套餐：" + data.ItemSuitName + "；";
                    }

                }
                else
                {


                    var regEntity = _customerRegRepository.Get(input.Id);
                    oldgroplist = regEntity.CustomerItemGroup?.ToList();

                    //input.CostState = regEntity.CostState;
                    if (regEntity.RequestState.HasValue)
                    {
                        input.RequestState = regEntity.RequestState;
                    }

                    //if (regEntity.ReviewSate.HasValue)
                    //{
                    //    input.ReviewSate = regEntity.ReviewSate;
                    //}

                    if (regEntity.SendToConfirm.HasValue)
                    {
                        input.SendToConfirm = regEntity.SendToConfirm;
                    }

                    if (regEntity.SummLocked.HasValue)
                    {
                        input.SummLocked = regEntity.SummLocked;
                    }

                    if (regEntity.SummSate.HasValue)
                    {
                        input.SummSate = regEntity.SummSate;
                    }

                    if (regEntity.CheckSate.HasValue)
                    {
                        input.CheckSate = regEntity.CheckSate;
                    }

                    if (regEntity.BarState.HasValue)
                    {
                        input.BarState = regEntity.BarState;
                    }
                    var ooo = input.OccHazardFactors;
                    input.OccHazardFactors = null;
                    input.ClientTeamInfo = null;
                    input.CustomerItemGroup = null;
                    input.ClientTeamRegitemInfo = null;
                    input.Customer = null;
                    input.MapTo(regEntity);
                    regEntity.CustomerId = retCus.Id;
                    regEntity.Customer = retCus;
                    regEntity.ClientTeamInfoId = input.ClientTeamInfo_Id;
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception e)
                    //{

                    //    throw;
                    //}

                    var retReg = regEntity;
                    if (retReg.ItemSuitBMId.HasValue)
                    {
                        retReg.ItemSuitBM = _itemSuitRepository.Get(regEntity.ItemSuitBMId.Value);
                    }
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception e)
                    //{

                    //    throw;
                    //}
                    //修改需要需要根据参数修改是否还有分组信息
                    if (input.ClientTeamInfo_Id.HasValue)
                    {
                        retReg.ClientTeamInfo = _clientTeamInfoRepository.Get(input.ClientTeamInfo_Id.Value);
                        retReg.ItemSuitBMId = retReg.ClientTeamInfo.ItemSuit_Id;
                        retReg.ItemSuitName = retReg.ClientTeamInfo.ItemSuitName;
                        ret.ItemSuitBM = retReg.ClientTeamInfo.ItemSuit;
                        retReg.ClientTeamInfoId = input.ClientTeamInfo_Id;
                        retReg.ClientReg = retReg.ClientTeamInfo.ClientReg;
                        retReg.ClientRegId = retReg.ClientReg.Id;
                        if (retReg.ClientTeamInfo.ClientReg != null)
                        {
                            retReg.ClientInfo = retReg.ClientTeamInfo.ClientReg.ClientInfo;
                            if (retReg.ClientInfo != null)
                            {
                                retReg.ClientInfoId = retReg.ClientInfo.Id;
                                retReg.ClientType = retReg.ClientReg.ClientSate.ToString();
                            }
                            else
                                retReg.ClientType = ((int)ClientSate.Normal).ToString();
                        }
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (Exception e)
                        //{

                        //    throw;
                        //}
                    }
                    else
                    {
                        retReg.ClientInfo = null;
                        retReg.ClientInfoId = null;
                        retReg.ClientTeamInfo = null;
                        retReg.ClientReg = null;
                        retReg.ClientRegId = null;
                        if (input.ClientRegId.HasValue)
                        {
                            var clientreg = _clientRegRepository.Get(input.ClientRegId.Value);
                            if (clientreg != null)
                            {
                                retReg.ClientRegId = clientreg.Id;
                                retReg.ClientReg = clientreg;
                                retReg.ClientInfo = clientreg.ClientInfo;
                                if (clientreg.ClientInfo != null)
                                {
                                    retReg.ClientInfoId = clientreg.ClientInfo.Id;
                                    retReg.ClientType = clientreg.ClientSate.ToString();
                                }
                                else
                                    retReg.ClientType = ((int)ClientSate.Normal).ToString();
                            }
                        }
                    }

                    retReg.CustomerItemGroup = null;

                    //职业健康危害因素
                    if (ooo != null && ooo.Count > 0)
                    {
                        if (retReg.OccHazardFactors == null)
                        {
                            retReg.OccHazardFactors = new List<TbmOccHazardFactor>();
                        }
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //    //retReg.OccHazardFactors.Clear();
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (InvalidOperationException e)
                        //{

                        //}
                        //retReg.OccHazardFactors = new List<TbmOccHazardFactor>();
                        foreach (var rick in ooo)
                        {
                            var tbmHazar = _TbmOccHazardFactor.Get(rick.Id);
                            var lst = tbmHazar.Protectivis.ToList();
                            retReg.OccHazardFactors.Add(tbmHazar);
                            if (!RiskIds.Contains(rick.Id))
                            {
                                RiskIds.Add(rick.Id);
                            }
                        }
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (Exception e)
                        //{

                        //    throw;
                        //}
                    }
                    #region 健康证号处理
                    if (retReg.PhysicalType.HasValue)
                    {
                        var tjlbName = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                         && p.Value == retReg.PhysicalType)?.Text;
                        if (!string.IsNullOrEmpty(tjlbName) &&
                            (tjlbName.Contains("从业") || tjlbName.Contains("健康证") || tjlbName.Contains("食品")))
                        {

                            retReg.JKZBM = _idNumberAppService.CreateJKZBM();
                        }

                    }
                    #endregion
                    //出报告时间计算
                    if (retReg.LoginDate.HasValue)
                    {
                        if (retReg.ClientRegId.HasValue && retReg.ClientReg.ReportDays.HasValue
                            && retReg.ClientReg.ReportDays > 0)
                        {
                            retReg.ReportDate = retReg.LoginDate.Value.AddDays(retReg.ClientReg.ReportDays.Value);
                        }
                        else
                        {
                            if (retReg.PhysicalType.HasValue)
                            {
                                var days = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                                && p.Value == retReg.PhysicalType)?.Code;
                                if (!string.IsNullOrEmpty(days) && int.TryParse(days, out int reDays))
                                {
                                    retReg.ReportDate = retReg.LoginDate.Value.AddDays(reDays);

                                }
                            }
                        }
                    }
                    regEntity = _customerRegRepository.Update(retReg);
                    //CurrentUnitOfWork.SaveChanges();
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception e)
                    //{

                    //    throw;
                    //}

                    ret = regEntity;
                    logDetail += "修改登记：" + retReg.CustomerBM + "；";
                    logText = "修改登记：" + retReg.CustomerBM + "；";
                }
                #endregion 再修改客户登记信息 结束



                #region 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 开始
                if (oldgroplist.Count > 0)
                {
                    foreach (var old in oldgroplist)
                        if (!cusGroupls.Any(o => o.Id == old.Id))
                        {
                            var olditems = old.CustomerRegItem?.ToList();
                            if (olditems != null)
                            {
                                foreach (var item in olditems)
                                    _customerRegItemRepository.Delete(item);
                            }

                            _customerItemGroupRepository.Delete(old);
                            //CurrentUnitOfWork.SaveChanges();

                        }
                }
                //CurrentUnitOfWork.SaveChanges();
                //try
                //{
                //    CurrentUnitOfWork.SaveChanges();
                //}
                //catch (Exception e)
                //{

                //    throw;
                //}
                #endregion 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 结束


                #region 组合处理
                if (cusGroupls != null)
                {
                    //遵义优化先取出来
                    var itemGroupIDlist = cusGroupls.Select(p => p.ItemGroupBM_Id).ToList();

                    var cusGroupItemlist = _customerRegItemRepository.GetAll().Where(p => p.CustomerRegId == ret.Id).ToList();
                    var cusGrouplist = _customerItemGroupRepository.GetAll().Where(p => p.CustomerRegBMId == ret.Id).ToList();
                    var tbmitemGrouplist = _itemGroupRepository.GetAll().Where(p => itemGroupIDlist.Contains(p.Id)).ToList();

                    //暂时屏蔽
                    //List<Guid> OccGroupIds = new List<Guid>();
                    #region 获取职业健康必选项目
                    //if (RiskIds.Count > 0 && !string.IsNullOrEmpty( input.PostState))
                    //{
                    // var targetDisease = _OccTargetDisease.GetAll().Where(p=>p.CheckType== input.PostState && RiskIds.Contains(p.OccHazardFactorsId.Value)).ToList();

                    //    var OccGrouplis = targetDisease.SelectMany(p => p.MustIemGroups).ToList();
                    //    OccGroupIds = OccGrouplis.Select(P=>P.Id).Distinct().ToList();

                    //}
                    #endregion
                    #region 单位限额处理
                    if (ret.ClientTeamInfoId.HasValue && ret.ClientTeamInfo != null && ret.ClientTeamInfo.CostType == (int)PayerCatType.FixedAmount &&
                        ret.ClientTeamInfo.QuotaMoney.HasValue)
                    {
                        //限额金额小于团付金额
                        var NopayGroup = cusGroupls.Where(p => p.IsAddMinus != (int)AddMinusType.Minus && p.PayerCat != (int)PayerCatType.Charge).ToList();
                        var TTPayMoney = NopayGroup.Sum(p => p.TTmoney);
                        var GRPayMoney = NopayGroup.Sum(p => p.GRmoney);
                        var GRGeouplist = NopayGroup.Where(p => p.GRmoney > 0).ToList();

                        if (TTPayMoney < ret.ClientTeamInfo.QuotaMoney && GRPayMoney > 0)
                        {

                            foreach (var cusGroup in GRGeouplist)
                            {

                                if (TTPayMoney < ret.ClientTeamInfo.QuotaMoney)
                                {
                                    //cusGroup.TTmoney = cusGroup.PriceAfterDis;
                                    //cusGroup.GRmoney = 0;

                                    var TTPay = TTPayMoney + cusGroup.GRmoney;
                                    if (TTPay == ret.ClientTeamInfo.QuotaMoney)
                                    {
                                        var GRMpney = cusGroup.GRmoney;
                                        cusGroup.TTmoney = cusGroup.TTmoney + GRMpney;
                                        cusGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                        cusGroup.GRmoney = 0;
                                        TTPayMoney += GRMpney;
                                        break;

                                    }
                                    else if (TTPay > ret.ClientTeamInfo.QuotaMoney)
                                    {
                                        decimal grMoney = TTPay - ret.ClientTeamInfo.QuotaMoney.Value;
                                        cusGroup.TTmoney = cusGroup.TTmoney + cusGroup.GRmoney - grMoney;
                                        cusGroup.PayerCat = (int)PayerCatType.PersonalCharge;
                                        TTPayMoney += cusGroup.GRmoney - grMoney;
                                        cusGroup.GRmoney = grMoney;

                                        break;
                                    }
                                    else
                                    {
                                        var GRMpney = cusGroup.GRmoney;
                                        cusGroup.TTmoney = cusGroup.TTmoney + GRMpney;
                                        cusGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                        cusGroup.GRmoney = 0;
                                        TTPayMoney += GRMpney;
                                    }
                                }

                            }
                        }
                    }


                    #endregion
                    decimal personalPay = 0; //个人应收
                    decimal clientMoney = 0; //团体应收
                    decimal personalAdd = 0; //个人加项
                    decimal personalMinusMoney = 0; //个人减项
                    decimal clientAdd = 0;
                    decimal clientMinusMoney = 0;
                    ret.CustomerItemGroup = null;

                    foreach (var g in cusGroupls)
                    {
                        #region 组合处理 遍历内 开始
                        //已收费的项目不会被删除也不作处理，新加分组数据添加至数据库
                        if (g.IsAddMinus == (int)AddMinusType.Normal || g.IsAddMinus == (int)AddMinusType.Add)
                        {
                            personalPay += g.GRmoney;
                            clientMoney += g.TTmoney;
                        }
                        if (g.IsAddMinus == (int)AddMinusType.Add)
                        {
                            //加项
                            personalAdd += g.GRmoney;
                            clientAdd += g.TTmoney;
                        }
                        else if (g.IsAddMinus == (int)AddMinusType.Minus)
                        {
                            //减项
                            clientMinusMoney += g.TTmoney;
                            personalMinusMoney += g.GRmoney;
                        }

                        // TjlCustomerItemGroup old = _customerItemGroupRepository.FirstOrDefault(o => o.Id == g.Id);
                        TjlCustomerItemGroup old = null;
                        var isHasSoftDelete = false;
                        // old = _customerItemGroupRepository.FirstOrDefault(o => o.Id == g.Id);
                        if (cusGrouplist != null)
                        {
                            old = cusGrouplist.FirstOrDefault(o => o.Id == g.Id);
                        }

                        #region 遵义优化1019
                        //SqlParameter[] parameters = {
                        //new SqlParameter("@id",g.Id),
                        //};
                        //string strsql = "select * from TjlCustomerItemGroups where Id=@id";
                        //List<TjlCustomerItemGroup> lstCustomerItemGroups = _sqlExecutor.SqlQuery<TjlCustomerItemGroup>
                        //    (strsql, parameters).ToList();
                        //if (lstCustomerItemGroups != null && lstCustomerItemGroups.Count > 0)
                        //{
                        //    old = _customerItemGroupRepository.FirstOrDefault(o => o.Id == g.Id);
                        //    #region 误删除 进行恢复 开始
                        ////    if (old != null && old.IsDeleted == true)
                        ////    {
                        ////        SqlParameter[] parameter = {
                        //// new SqlParameter("@CustomerRegId",g.CustomerRegBMId),
                        //// new SqlParameter("@ItemGroupBMId", g.Id)
                        ////};
                        ////        strsql = "update TjlCustomerRegItems set IsDeleted=0 where CustomerRegId=@CustomerRegId and ItemGroupBMId=@ItemGroupBMId";
                        ////        int inum = _sqlExecutor.Execute(strsql, parameter);
                        ////    }
                        //    #endregion 误删除 进行恢复 结束
                        //}
                        #endregion




                        if (old == null) //新增组合添加
                        {
                            //var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);
                            var bmGrop = tbmitemGrouplist.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);
                            if (bmGrop != null)
                            {
                                var groupItem = g.MapTo<TjlCustomerItemGroup>();
                                groupItem.Id = Guid.NewGuid();
                                //groupItem.CustomerRegBM = ret;
                                groupItem.CustomerRegBMId = ret.Id;
                                //groupItem.DepartmentBM = bmGrop.Department;                               
                                groupItem.DepartmentId = bmGrop.DepartmentId;
                                groupItem.DepartmentCodeBM = bmGrop.Department.DepartmentBM;
                                //groupItem.ItemGroupBM = bmGrop;
                                groupItem.ItemGroupBM_Id = bmGrop.Id;
                                groupItem.SFType = Convert.ToInt32(bmGrop.ChartCode);
                                groupItem.ItemGroupCodeBM = bmGrop.ItemGroupBM;
                                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                                if (groupItem.DepartmentBM?.Category == "耗材" || groupItem.ItemGroupBM_Id == guid)
                                {
                                    groupItem.CheckState = (int)ProjectIState.Complete;
                                }

                                if (ret.RegisterState == (int)RegisterState.Yes && !groupItem.BillEmployeeBMId.HasValue)
                                {
                                    groupItem.BillEmployeeBMId = AbpSession.UserId.Value;
                                }
                                var grop = _customerItemGroupRepository.Insert(groupItem);
                                //logDetail += "添加组合："+ groupItem.ItemGroupName + "，加项状态："+  groupItem.IsAddMinus + "；"; 
                                if (ret.RegisterState == (int)RegisterState.Yes) //团体保存人员信息用这个方法，但未登记就不要添加项目结果表
                                {
                                    foreach (var info in bmGrop.ItemInfos)
                                        _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                        {
                                            Id = Guid.NewGuid(),
                                            //CustomerRegBM = ret,
                                            CustomerRegId = ret.Id,
                                            //CustomerItemGroupBM = groupItem,
                                            CustomerItemGroupBMid = groupItem.Id,
                                            DepartmentId = groupItem.DepartmentId,
                                            //ItemGroupBM = bmGrop,
                                            ItemGroupBMId = bmGrop.Id,
                                            //ItemBM = info,
                                            ItemId = info.Id,
                                            ItemName = info.Name,
                                            ItemTypeBM = info.moneyType,
                                            ItemOrder = info.OrderNum,
                                            Unit = info.Unit,
                                            ProcessState = (int)ProjectIState.Not,
                                            DiagnSate = info.DiagnosisComplexSate,
                                            SummBackSate = (int)SDState.Unlocked
                                        });
                                }
                            }
                        }
                        else
                        {
                            //已有组合更新，但目前对组合下项目不做修改，仅更新组合信息
                            //查询已有数据的状态，这些状态在登记中不可修改只是查看
                            // var data = g.MapTo(old);
                            //data.CustomerRegBM = ret;
                            // data.CustomerRegBMId = ret.Id;
                            //  TbmItemGroup tbmItemGroup = _itemGroupRepository.Get(g.ItemGroupBM_Id);
                            TbmItemGroup tbmItemGroup = tbmitemGrouplist.FirstOrDefault(p => p.Id == g.ItemGroupBM_Id);

                            g.Id = old.Id;
                            g.CustomerRegBMId = ret.Id;
                            //g.PayerCat = old.PayerCat;
                            g.DrawSate = old.DrawSate;
                            g.BarState = old.BarState;
                            g.CheckState = old.CheckState;
                            //g.RefundState = old.RefundState;
                            g.RequestState = old.RequestState;
                            g.SuspendState = old.SuspendState;
                            g.SummBackSate = old.SummBackSate;
                            g.ItemGroupOrder = tbmItemGroup.OrderNum;
                            g.DepartmentOrder = tbmItemGroup.Department.OrderNum;
                            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                            if (tbmItemGroup.Department.Category == "耗材" || tbmItemGroup.Id == guid)
                            {
                                g.CheckState = (int)ProjectIState.Complete;
                            }
                            g.CollectionState = old.CollectionState;
                            var nowGroup = g.MapTo(old);
                            //if (OccGroupIds.Contains(g.ItemGroupBM_Id))
                            //{
                            //    nowGroup.IsZYB = 1;
                            //}
                            //else
                            //{
                            //    nowGroup.IsZYB = 2;
                            //}
                            if (ret.RegisterState == (int)RegisterState.Yes && !nowGroup.BillEmployeeBMId.HasValue)
                            {
                                nowGroup.BillEmployeeBMId = AbpSession.UserId.Value;
                            }
                            var group = _customerItemGroupRepository.Update(nowGroup);
                            //logDetail += "修改组合："  + group.ItemGroupName + "，加项状态：" + group.IsAddMinus+"；";
                            if (isHasSoftDelete == false)
                            {
                                //var data = _customerRegItemRepository.FirstOrDefault(o => o.CustomerItemGroupBMid == g.Id);
                                //var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);

                                var data = cusGroupItemlist.FirstOrDefault(o => o.CustomerItemGroupBMid == g.Id);
                                var bmGrop = tbmitemGrouplist.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);

                                if (bmGrop == null)
                                {
                                    continue;
                                }
                                if (data == null)
                                {
                                    if (ret.RegisterState == (int)RegisterState.Yes) //团体保存人员信息用这个方法，但未登记就不要添加项目结果表
                                    {
                                        foreach (var info in bmGrop.ItemInfos)
                                        {

                                            _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                            {
                                                Id = Guid.NewGuid(),
                                                // CustomerRegBM = ret,
                                                CustomerRegId = ret.Id,
                                                //CustomerItemGroupBM = group,
                                                CustomerItemGroupBMid = group.Id,
                                                DepartmentId = group.DepartmentId,
                                                //ItemGroupBM = bmGrop,
                                                //ItemBM = info,
                                                ItemId = info.Id,
                                                ItemName = info.Name,
                                                ItemTypeBM = info.moneyType,
                                                ItemOrder = info.OrderNum,
                                                Unit = info.Unit,
                                                ProcessState = (int)ProjectIState.Not,
                                                DiagnSate = info.DiagnosisComplexSate,
                                                SummBackSate = (int)SDState.Unlocked
                                            });
                                        }
                                    }
                                }
                                else//已有项目细项要更新
                                {   //循环已有的 已经从字典去掉的 删除
                                    foreach (var i in old.CustomerRegItem?.ToList())
                                    {
                                        if (bmGrop != null && !(bmGrop.ItemInfos.Any(o => o.Id == i.ItemId)))
                                            _customerRegItemRepository.Delete(i);
                                    }
                                    //循环字典里已有的更新 没有的增加
                                    foreach (var info in bmGrop?.ItemInfos)
                                    {
                                        var regItem = cusGroupItemlist.FirstOrDefault(o => o.CustomerItemGroupBMid == old.Id && o.ItemId == info.Id);
                                        if (regItem != null)
                                        {
                                            regItem.DepartmentId = group.DepartmentId;
                                            regItem.ItemBM = info;
                                            regItem.ItemName = info.Name;
                                            regItem.ItemTypeBM = info.moneyType;
                                            regItem.ItemOrder = info.OrderNum;
                                            if (!string.IsNullOrEmpty(info.Unit) || string.IsNullOrEmpty(regItem.Unit))
                                            {
                                                regItem.Unit = info.Unit;
                                            }
                                            regItem.DiagnSate = info.DiagnosisComplexSate;
                                        }
                                        else
                                        {
                                            _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                            {
                                                Id = Guid.NewGuid(),
                                                //CustomerRegBM = ret,
                                                CustomerRegId = ret.Id,
                                                //CustomerItemGroupBM = group,
                                                DepartmentId = group.DepartmentId,
                                                // ItemGroupBM = bmGrop,
                                                ItemGroupBMId = bmGrop.Id,
                                                //ItemBM = info,
                                                ItemId = info.Id,
                                                ItemName = info.Name,
                                                ItemTypeBM = info.moneyType,
                                                ItemOrder = info.OrderNum,
                                                Unit = info.Unit,
                                                ProcessState = (int)ProjectIState.Not,
                                                DiagnSate = info.DiagnosisComplexSate,
                                                SummBackSate = (int)SDState.Unlocked
                                            });
                                        }
                                    }
                                }
                            }

                        }
                        #endregion 组合处理 遍历内 开始
                        //插入日志

                    }
                    //查询是否有未收费的组合项目。如果有就登记表收费状态即为未收费
                    #region 插入收费项目
                    if (cusGroupls.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Any(o =>
                                           o.PayerCat == (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus)
                                ) //于慧说减项在人员收费后也是未收费状态，所以不参加判断该人员是否收费
                    {
                        ret.CostState = (int)PayerCatType.NoCharge;
                    }
                    else
                    {
                        ret.CostState = (int)PayerCatType.Charge;
                    }

                    var payinfo = _mcusPayMoneyRepository.FirstOrDefault(o => o.CustomerReg.Id == ret.Id);
                    if (payinfo == null)
                    {
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
                    else
                    {
                        if (payinfo.PersonalMoney != personalPay || payinfo.ClientMoney != clientMoney ||
                            payinfo.PersonalAddMoney != personalAdd || payinfo.ClientAddMoney != personalAdd ||
                            payinfo.ClientMinusMoney != clientMinusMoney)
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
                    #endregion
                }
                #endregion 组合处理

                #endregion  遍历体检人 结束

                //添加操作日志
                result.Add(ret.MapTo<QueryCustomerRegDto>());
                createOpLogDto.LogBM = ret.CustomerBM;
                createOpLogDto.LogName = ret.Customer.Name;
                createOpLogDto.LogText = logText;
                if (ret.RegisterState == 1)
                {
                    createOpLogDto.LogText = logText.Replace("登记", "预约");
                }
                if (ret.InfoSource == 2)
                {
                    createOpLogDto.LogText += "(线上预约)";
                }
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                if (logDetail.Length > 4000)
                {
                    logDetail = logDetail.Substring(0, 4000);
                }
                createOpLogDto.LogDetail = logDetail;
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            //DateTime dateTime1 = DateTime.Now;

            //try
            //{
            // CurrentUnitOfWork.SaveChanges();
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            return result;
        }
        public List<QueryCustomerRegDto> RegCustomerNew(QueryCustomerRegDto Reginput)
        {
            var CusregInfoNew = Reginput.MapTo<SaveCusRegInfoDto>();
            var cusInfoNew = Reginput.Customer.MapTo<SaveCusInfoDto>();
            var cusGroupNew = Reginput.CustomerItemGroup.MapTo<List<SaveCusGroupsDto>>();
            var riskinfoNew = Reginput.OccHazardFactors;

            if (CusregInfoNew == null)
            {
                throw new FieldVerifyException("无数据", "无数据");
            }
            var result = new List<QueryCustomerRegDto>();
            List<Guid> RiskIds = new List<Guid>();

            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            string logText = "";
            string logDetail = "";
            #region  遍历体检人 开始

            #region 条件判断 开始
            if (CusregInfoNew.RegisterState == (int)RegisterState.Yes)
            {
                if (CusregInfoNew.CheckSate == (int)ProjectIState.Not || !CusregInfoNew.CheckSate.HasValue || !CusregInfoNew.LoginDate.HasValue)
                {
                    CusregInfoNew.LoginDate = DateTime.Now;
                }
                //登记号
                if ((CusregInfoNew.CheckSate == (int)ProjectIState.Not || !CusregInfoNew.CheckSate.HasValue) && !CusregInfoNew.CustomerRegNum.HasValue)
                {

                    //新登记号生成 方法
                    CusregInfoNew.CustomerRegNum = _idNumberAppService.CreateRegNum();
                    #region 遵义需求登记重新生成登记号
                    var cxsc = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ForegroundFunctionControl" && p.Value == 22)?.Remarks;
                    var tm = System.DateTime.Now.ToString("yyMMdd");
                    if (!string.IsNullOrEmpty(cxsc) && cxsc == "1" && !CusregInfoNew.CustomerBM.Contains(tm))
                    {
                        CusregInfoNew.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                    }
                    #endregion

                }
            }
            var reData =
                _customerRegRepository.FirstOrDefault(o =>
                    o.CustomerBM == CusregInfoNew.CustomerBM && o.Id != CusregInfoNew.Id); //判断登记信息中体检号是否重复
            if (reData != null)
            {
                throw new FieldVerifyException("体检号重复", "体检号重复，请修改后登记。");
            }

            if (CusregInfoNew.RegisterState == (int)RegisterState.Yes)
            {
                if (cusGroupNew == null)
                {
                    throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                }

                if (cusGroupNew.Count == 0)
                {
                    throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                }
            }
            #endregion 条件判断 结束


            #region 添加或更新客户信息 开始
            var cusGroupls = cusInfoNew;
            var ret = new TjlCustomerReg();
            var retCus = new TjlCustomer();
            var oldgroplist = new List<TjlCustomerItemGroup>();
            var customer = cusInfoNew;
            var oldcus = new TjlCustomer();
            if (!string.IsNullOrEmpty(cusInfoNew.IDCardNo))
            {
                oldcus = _customerRepository.FirstOrDefault(o => o.IDCardNo == customer.IDCardNo
                && o.Name == customer.Name && o.Sex == customer.Sex);
                if (oldcus == null)
                { oldcus = new TjlCustomer();
                }
            }
            if (oldcus.Id == Guid.Empty && !string.IsNullOrWhiteSpace(customer.ArchivesNum))
            {
                oldcus = _customerRepository.FirstOrDefault(o => o.ArchivesNum == customer.ArchivesNum);
                //替检查不关联之前档案
                if (CusregInfoNew.ReplaceSate == 2)
                {
                    //判断不同名或不是同一个身份证号则创建
                    if (oldcus != null && (oldcus.Name != customer.Name || (!string.IsNullOrEmpty(oldcus.IDCardNo) && !string.IsNullOrEmpty(customer.IDCardNo) && oldcus.IDCardNo != customer.IDCardNo)))
                    {
                        oldcus = null;
                        oldcus = new TjlCustomer();
                        customer.Id = Guid.Empty;
                        customer.ArchivesNum = CusregInfoNew.CustomerBM;
                    }
                }

            }
            if (oldcus != null)
            {
                customer.Id = oldcus.Id;
                //此次没有照片关联旧照片
                if (oldcus.CusPhotoBmId.HasValue && !customer.CusPhotoBmId.HasValue)
                {
                    customer.CusPhotoBmId = oldcus.CusPhotoBmId;
                }
            }
            if (customer.Id == Guid.Empty)
            {
                var customerEntity = customer.MapTo<TjlCustomer>();
                customerEntity.Id = Guid.NewGuid();
                //行业处理2021-12-31
                if (CusregInfoNew.ClientRegId.HasValue && string.IsNullOrEmpty(cusInfoNew.CustomerTrade))
                {
                    var clietint = _clientRegRepository.Get(CusregInfoNew.ClientRegId.Value).ClientInfo?.Clientlndutry;
                    if (!string.IsNullOrEmpty(clietint))
                    {
                        customerEntity.CustomerTrade = clietint;
                    }
                }
                customerEntity.NameAB = _commonAppService.GetHansBrief(new ChineseDto { Hans = customerEntity.Name })?.Brief;

                retCus = _customerRepository.Insert(customerEntity);
                logDetail += "添加档案：" + customerEntity.ArchivesNum + "；";
            }
            else
            {
                var oldCustomer = cusInfoNew;
                var customerEntity = _customerRepository.Get(customer.Id);
                oldCustomer.ArchivesNum = customerEntity.ArchivesNum;
                //行业处理2021-12-31
                if (CusregInfoNew.ClientRegId.HasValue && string.IsNullOrEmpty(cusInfoNew.CustomerTrade))
                {
                    var clietint = _clientRegRepository.Get(CusregInfoNew.ClientRegId.Value).ClientInfo?.Clientlndutry;
                    if (!string.IsNullOrEmpty(clietint))
                    {
                        oldCustomer.CustomerTrade = clietint;
                    }
                }
                oldCustomer.MapTo(customerEntity);
                customerEntity.NameAB = _commonAppService.GetHansBrief(new ChineseDto { Hans = oldCustomer.Name })?.Brief;

                retCus = _customerRepository.Update(customerEntity);
                // CurrentUnitOfWork.SaveChanges();
                logDetail += "修改档案：" + customerEntity.ArchivesNum + "；";

            }
            #endregion 添加或更新客户信息 结束

            //input.Customer = null;

            #region 修改客户登记信息 开始
            //先根据体检号码查询一下库里是否有该体检号信息
            var reg = _customerRegRepository.FirstOrDefault(o => o.Id == CusregInfoNew.Id);
            if (reg != null)
            {
                if (reg.CustomerBM != reg.CustomerBM) //根据当前Id查询出登记信息，
                {
                    CusregInfoNew.Id = Guid.Empty;
                }
            }
            if (CusregInfoNew.Id == Guid.Empty)
            {
                var data = CusregInfoNew.MapTo<TjlCustomerReg>();
                data.CheckSate = (int)PhysicalEState.Not;
                data.Id = Guid.NewGuid();
                data.Customer = retCus;

                if (CusregInfoNew.ClientTeamInfo_Id.HasValue)
                {
                    //新增是直接根据分组信息增加该字段
                    data.ClientTeamInfo = _clientTeamInfoRepository.Get(CusregInfoNew.ClientTeamInfo_Id.Value);
                    data.ItemSuitBMId = data.ClientTeamInfo.ItemSuit_Id;
                    data.ItemSuitName = data.ClientTeamInfo.ItemSuitName;
                    if (data.ClientTeamInfo.ClientReg != null)
                    {
                        data.ClientInfo = data.ClientTeamInfo.ClientReg.ClientInfo;
                        data.ClientType = data.ClientTeamInfo.ClientReg.ClientSate.ToString();
                        if (data.ClientInfo != null)
                        {
                            data.ClientInfoId = data.ClientInfo.Id;
                        }
                    }
                }
                else
                {
                    if (CusregInfoNew.ClientRegId.HasValue)
                    {
                        data.ClientReg = _clientRegRepository.Get(CusregInfoNew.ClientRegId.Value);
                        if (data.ClientReg != null)
                        {
                            data.ClientRegId = data.ClientRegId;
                            data.ClientInfo = data.ClientInfo;
                            if (data.ClientInfo != null)
                            {
                                data.ClientInfoId = data.ClientInfo.Id;
                                data.ClientType = data.ClientInfo.ClientSate;
                            }
                            else
                                data.ClientType = ((int)ClientSate.Normal).ToString();
                        }
                    }
                }

                data.CustomerItemGroup = null;
                //职业健康危害因素
                if (riskinfoNew != null && riskinfoNew.Count > 0)
                {
                    data.OccHazardFactors.Clear();
                    if (data.OccHazardFactors == null)
                    {
                        data.OccHazardFactors = new List<TbmOccHazardFactor>();
                    }
                    foreach (var rick in riskinfoNew)
                    {
                        var tbmHazar = _TbmOccHazardFactor.Get(rick.Id);
                        data.OccHazardFactors.Add(tbmHazar);

                        if (!RiskIds.Contains(rick.Id))
                        {
                            RiskIds.Add(rick.Id);
                        }
                    }
                }
                if (!data.AppointmentTime.HasValue)
                {
                    data.AppointmentTime = data.BookingDate;
                }
                if (data.ClientRegId.HasValue)
                {
                    int clientBM = 1;
                    var clietid = data.ClientRegId;
                    var MaxClientBM = _customerRegRepository.GetAll().Where(o => o.ClientRegId == clietid && o.ClientRegNum != null).Select(o => o.ClientRegNum).Max();
                    if (MaxClientBM != null)
                    {
                        clientBM = int.Parse(MaxClientBM.ToString()) + 1;

                    }
                    data.ClientRegNum = clientBM;

                }
                #region 健康证号处理
                if (data.PhysicalType.HasValue)
                {
                    var tjlbName = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                     && p.Value == data.PhysicalType)?.Text;
                    if (!string.IsNullOrEmpty(tjlbName) &&
                        (tjlbName.Contains("从业") || tjlbName.Contains("健康证") || tjlbName.Contains("食品")))
                    {

                        data.JKZBM = _idNumberAppService.CreateJKZBM();
                    }

                }
                #endregion
                var userBM = _userRepository.Get(AbpSession.UserId.Value);
                if (userBM != null && userBM.HospitalArea.HasValue)
                {
                    data.HospitalArea = userBM.HospitalArea;
                }
                //出报告时间计算
                if (data.LoginDate.HasValue)
                {
                    TjlClientReg _clientReg = null;
                    if (data.ClientRegId.HasValue)
                    { _clientReg = _clientRegRepository.Get(data.ClientRegId.Value);
                    }


                    if (_clientReg != null && _clientReg.ReportDays.HasValue
                        && _clientReg.ReportDays > 0)
                    {

                        data.ReportDate = data.LoginDate.Value.AddDays(_clientReg.ReportDays.Value);
                    }
                    else
                    {
                        if (data.PhysicalType.HasValue)
                        {
                            var days = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                            && p.Value == data.PhysicalType)?.Code;
                            if (!string.IsNullOrEmpty(days) && int.TryParse(days, out int reDays))
                            {
                                data.ReportDate = data.LoginDate.Value.AddDays(reDays);

                            }
                        }
                    }
                }
                data = _customerRegRepository.Insert(data);
                //try
                //{
                //    CurrentUnitOfWork.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
                // CurrentUnitOfWork.SaveChanges();
                ret = data;
                logDetail += "添加登记：" + data.CustomerBM + "；";
                logText = "添加登记：" + data.CustomerBM + "；"; ;
                if (!string.IsNullOrEmpty(data.ItemSuitName))
                {
                    logDetail += "套餐：" + data.ItemSuitName + "；";
                    logText += "套餐：" + data.ItemSuitName + "；";
                }

            }
            else
            {


                var regEntity = _customerRegRepository.Get(CusregInfoNew.Id);
                oldgroplist = regEntity.CustomerItemGroup?.ToList();

                //input.CostState = regEntity.CostState;
                if (regEntity.RequestState.HasValue)
                {
                    CusregInfoNew.RequestState = regEntity.RequestState;
                }

                //if (regEntity.ReviewSate.HasValue)
                //{
                //    input.ReviewSate = regEntity.ReviewSate;
                //}

                if (regEntity.SendToConfirm.HasValue)
                {
                    CusregInfoNew.SendToConfirm = regEntity.SendToConfirm;
                }

                if (regEntity.SummLocked.HasValue)
                {
                    CusregInfoNew.SummLocked = regEntity.SummLocked;
                }

                if (regEntity.SummSate.HasValue)
                {
                    CusregInfoNew.SummSate = regEntity.SummSate;
                }

                if (regEntity.CheckSate.HasValue)
                {
                    CusregInfoNew.CheckSate = regEntity.CheckSate;
                }

                if (regEntity.BarState.HasValue)
                {
                    CusregInfoNew.BarState = regEntity.BarState;
                }

                CusregInfoNew.MapTo(regEntity);
                regEntity.CustomerId = retCus.Id;
                regEntity.Customer = retCus;
                regEntity.ClientTeamInfoId = CusregInfoNew.ClientTeamInfo_Id;
                //try
                //{
                //    CurrentUnitOfWork.SaveChanges();
                //}
                //catch (Exception e)
                //{

                //    throw;
                //}

                var retReg = regEntity;

                //修改需要需要根据参数修改是否还有分组信息
                if (CusregInfoNew.ClientTeamInfo_Id.HasValue)
                {

                    retReg.ItemSuitBMId = retReg.ClientTeamInfo.ItemSuit_Id;
                    retReg.ItemSuitName = retReg.ClientTeamInfo.ItemSuitName;
                    ret.ItemSuitBM = retReg.ClientTeamInfo.ItemSuit;
                    retReg.ClientTeamInfoId = CusregInfoNew.ClientTeamInfo_Id;
                    retReg.ClientRegId = retReg.ClientReg.Id;


                }
                else
                {

                    if (CusregInfoNew.ClientRegId.HasValue)
                    {
                        var clientreg = _clientRegRepository.Get(CusregInfoNew.ClientRegId.Value);
                        if (clientreg != null)
                        {
                            retReg.ClientRegId = clientreg.Id;
                            retReg.ClientReg = clientreg;
                            retReg.ClientInfo = clientreg.ClientInfo;
                            if (clientreg.ClientInfo != null)
                            {
                                retReg.ClientInfoId = clientreg.ClientInfo.Id;
                                retReg.ClientType = clientreg.ClientSate.ToString();
                            }
                            else
                                retReg.ClientType = ((int)ClientSate.Normal).ToString();
                        }
                    }
                }

                retReg.CustomerItemGroup = null;

                //职业健康危害因素
                if (riskinfoNew != null && riskinfoNew.Count > 0)
                {
                    if (retReg.OccHazardFactors == null)
                    {
                        retReg.OccHazardFactors = new List<TbmOccHazardFactor>();
                    }

                    foreach (var rick in riskinfoNew)
                    {
                        var tbmHazar = _TbmOccHazardFactor.Get(rick.Id);

                        retReg.OccHazardFactors.Add(tbmHazar);
                        if (!RiskIds.Contains(rick.Id))
                        {
                            RiskIds.Add(rick.Id);
                        }
                    }

                }
                #region 健康证号处理
                if (retReg.PhysicalType.HasValue)
                {
                    var tjlbName = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                     && p.Value == retReg.PhysicalType)?.Text;
                    if (!string.IsNullOrEmpty(tjlbName) &&
                        (tjlbName.Contains("从业") || tjlbName.Contains("健康证") || tjlbName.Contains("食品")))
                    {

                        retReg.JKZBM = _idNumberAppService.CreateJKZBM();
                    }

                }
                #endregion
                //出报告时间计算
                if (retReg.LoginDate.HasValue)
                {
                    if (retReg.ClientRegId.HasValue && retReg.ClientReg.ReportDays.HasValue
                        && retReg.ClientReg.ReportDays > 0)
                    {
                        retReg.ReportDate = retReg.LoginDate.Value.AddDays(retReg.ClientReg.ReportDays.Value);
                    }
                    else
                    {
                        if (retReg.PhysicalType.HasValue)
                        {
                            var days = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                            && p.Value == retReg.PhysicalType)?.Code;
                            if (!string.IsNullOrEmpty(days) && int.TryParse(days, out int reDays))
                            {
                                retReg.ReportDate = retReg.LoginDate.Value.AddDays(reDays);

                            }
                        }
                    }
                }
                regEntity = _customerRegRepository.Update(retReg);


                ret = regEntity;
                logDetail += "修改登记：" + retReg.CustomerBM + "；";
                logText = "修改登记：" + retReg.CustomerBM + "；";
            }
            #endregion 再修改客户登记信息 结束



            #region 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 开始
            if (oldgroplist.Count > 0)
            {
                foreach (var old in oldgroplist)
                    if (!cusGroupNew.Any(o => o.Id == old.Id))
                    {
                        var olditems = old.CustomerRegItem?.ToList();
                        if (olditems != null)
                        {
                            foreach (var item in olditems)
                                _customerRegItemRepository.Delete(item);
                        }

                        _customerItemGroupRepository.Delete(old);


                    }
            }

            #endregion 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 结束


            #region 组合处理
            if (cusGroupls != null)
            {
                List<TjlCustomerItemGroup> TjlCustomerItemGroups = new List<TjlCustomerItemGroup>();
                List<TjlCustomerRegItem> TjlCustomerRegItems = new List<TjlCustomerRegItem>();

                CurrentUnitOfWork.SaveChanges();
                //遵义优化先取出来
                var itemGroupIDlist = cusGroupNew.Select(p => p.ItemGroupBM_Id).ToList();

                var cusGroupItemlist = _customerRegItemRepository.GetAll().Where(p => p.CustomerRegId == ret.Id).ToList();
                var cusGrouplist = _customerItemGroupRepository.GetAll().Where(p => p.CustomerRegBMId == ret.Id).ToList();
                var tbmitemGrouplist = _itemGroupRepository.GetAll().Where(p => itemGroupIDlist.Contains(p.Id)).ToList();



                #region 单位限额处理
                if (ret.ClientTeamInfoId.HasValue && ret.ClientTeamInfo != null && ret.ClientTeamInfo.CostType == (int)PayerCatType.FixedAmount &&
                    ret.ClientTeamInfo.QuotaMoney.HasValue)
                {
                    //限额金额小于团付金额
                    var NopayGroup = cusGroupNew.Where(p => p.IsAddMinus != (int)AddMinusType.Minus && p.PayerCat != (int)PayerCatType.Charge).ToList();
                    var TTPayMoney = NopayGroup.Sum(p => p.TTmoney);
                    var GRPayMoney = NopayGroup.Sum(p => p.GRmoney);
                    var GRGeouplist = NopayGroup.Where(p => p.GRmoney > 0).ToList();

                    if (TTPayMoney < ret.ClientTeamInfo.QuotaMoney && GRPayMoney > 0)
                    {

                        foreach (var cusGroup in GRGeouplist)
                        {

                            if (TTPayMoney < ret.ClientTeamInfo.QuotaMoney)
                            {


                                var TTPay = TTPayMoney + cusGroup.GRmoney;
                                if (TTPay == ret.ClientTeamInfo.QuotaMoney)
                                {
                                    var GRMpney = cusGroup.GRmoney;
                                    cusGroup.TTmoney = cusGroup.TTmoney + GRMpney;
                                    cusGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                    cusGroup.GRmoney = 0;
                                    TTPayMoney += GRMpney;
                                    break;

                                }
                                else if (TTPay > ret.ClientTeamInfo.QuotaMoney)
                                {
                                    decimal grMoney = TTPay - ret.ClientTeamInfo.QuotaMoney.Value;
                                    cusGroup.TTmoney = cusGroup.TTmoney + cusGroup.GRmoney - grMoney;
                                    cusGroup.PayerCat = (int)PayerCatType.PersonalCharge;
                                    TTPayMoney += cusGroup.GRmoney - grMoney;
                                    cusGroup.GRmoney = grMoney;

                                    break;
                                }
                                else
                                {
                                    var GRMpney = cusGroup.GRmoney;
                                    cusGroup.TTmoney = cusGroup.TTmoney + GRMpney;
                                    cusGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                    cusGroup.GRmoney = 0;
                                    TTPayMoney += GRMpney;
                                }
                            }

                        }
                    }
                }


                #endregion
                decimal personalPay = 0; //个人应收
                decimal clientMoney = 0; //团体应收
                decimal personalAdd = 0; //个人加项
                decimal personalMinusMoney = 0; //个人减项
                decimal clientAdd = 0;
                decimal clientMinusMoney = 0;
                ret.CustomerItemGroup = null;

                foreach (var g in cusGroupNew)
                {
                    #region 组合处理 遍历内 开始
                    //已收费的项目不会被删除也不作处理，新加分组数据添加至数据库
                    if (g.IsAddMinus == (int)AddMinusType.Normal || g.IsAddMinus == (int)AddMinusType.Add)
                    {
                        personalPay += g.GRmoney;
                        clientMoney += g.TTmoney;
                    }
                    if (g.IsAddMinus == (int)AddMinusType.Add)
                    {
                        //加项
                        personalAdd += g.GRmoney;
                        clientAdd += g.TTmoney;
                    }
                    else if (g.IsAddMinus == (int)AddMinusType.Minus)
                    {
                        //减项
                        clientMinusMoney += g.TTmoney;
                        personalMinusMoney += g.GRmoney;
                    }

                    TjlCustomerItemGroup old = null;
                    var isHasSoftDelete = false;
                    if (cusGrouplist != null)
                    {
                        old = cusGrouplist.FirstOrDefault(o => o.Id == g.Id);
                    }
                    if (old == null) //新增组合添加
                    {
                        //var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);
                        var bmGrop = tbmitemGrouplist.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);
                        if (bmGrop != null)
                        {
                            var groupItem = g.MapTo<TjlCustomerItemGroup>();
                            groupItem.Id = Guid.NewGuid();
                            //groupItem.CustomerRegBM = ret;
                            groupItem.CustomerRegBMId = ret.Id;
                            //groupItem.DepartmentBM = bmGrop.Department;                               
                            groupItem.DepartmentId = bmGrop.DepartmentId;
                            groupItem.DepartmentCodeBM = bmGrop.Department.DepartmentBM;
                            //groupItem.ItemGroupBM = bmGrop;
                            groupItem.ItemGroupBM_Id = bmGrop.Id;
                            groupItem.SFType = Convert.ToInt32(bmGrop.ChartCode);
                            groupItem.ItemGroupCodeBM = bmGrop.ItemGroupBM;
                            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                            if (groupItem.DepartmentBM?.Category == "耗材" || groupItem.ItemGroupBM_Id == guid)
                            {
                                groupItem.CheckState = (int)ProjectIState.Complete;
                            }

                            if (ret.RegisterState == (int)RegisterState.Yes && !groupItem.BillEmployeeBMId.HasValue)
                            {
                                groupItem.BillEmployeeBMId = AbpSession.UserId.Value;
                            }
                            // var grop = _customerItemGroupRepository.Insert(groupItem);
                            TjlCustomerItemGroups.Add(groupItem);
                            #region 注释
                            //                            #region 插入语句
                            //                            //新增价格
                            //                            SqlParameter[] parameter2 ={
                            //                                new SqlParameter("@Id",  groupItem.Id),
                            //                                 new SqlParameter("@CustomerRegBMId",  groupItem.CustomerRegBMId),
                            //                                new SqlParameter("@DepartmentCodeBM",  groupItem.DepartmentCodeBM),
                            //                          new SqlParameter("@DepartmentId", groupItem.DepartmentId),
                            //                           new SqlParameter("@DepartmentName", groupItem.DepartmentName),
                            //                            new SqlParameter("@DepartmentOrder", groupItem.DepartmentOrder),
                            //                             new SqlParameter("@ItemGroupBM_Id", groupItem.ItemGroupBM_Id ),
                            //                               new SqlParameter("@ItemGroupName", groupItem.ItemGroupName),
                            //                                 new SqlParameter("@ItemGroupOrder", groupItem.ItemGroupOrder ),
                            //                                   new SqlParameter("@ItemGroupCodeBM", groupItem.ItemGroupCodeBM),
                            //                                       new SqlParameter("@SFType", groupItem.SFType),
                            //                                         new SqlParameter("@ItemSuitId", groupItem.ItemSuitId==null?(object)DBNull.Value:groupItem.ItemSuitId),
                            //                                           new SqlParameter("@ItemSuitName", groupItem.ItemSuitName==null?(object)DBNull.Value: groupItem.ItemSuitName),
                            //                                             new SqlParameter("@CheckState", groupItem.CheckState),
                            //                                               new SqlParameter("@SummBackSate", groupItem.SummBackSate ),
                            //                                                 new SqlParameter("@BillingEmployeeBMId", groupItem.BillingEmployeeBMId ==null?(object)DBNull.Value: groupItem.BillingEmployeeBMId),
                            //                                                   new SqlParameter("@BillEmployeeBMId", groupItem.BillEmployeeBMId ==null?(object)DBNull.Value: groupItem.BillEmployeeBMId),
                            //                                                     new SqlParameter("@TotalEmployeeBMId", groupItem.TotalEmployeeBMId ==null?(object)DBNull.Value: groupItem.TotalEmployeeBMId),
                            //                                                     new SqlParameter("@IsAddMinus", groupItem.IsAddMinus ),
                            //                                                     new SqlParameter("@RefundState", groupItem.RefundState ),
                            //                                                     new SqlParameter("@ItemPrice", groupItem.ItemPrice ),
                            //                                                     new SqlParameter("@DiscountRate", groupItem.DiscountRate ),
                            //                                                     new SqlParameter("@PriceAfterDis", groupItem.PriceAfterDis ),
                            //                                                     new SqlParameter("@PayerCat", groupItem.PayerCat ),
                            //                                                      new SqlParameter("@TTmoney", groupItem.TTmoney),
                            //                                                       new SqlParameter("@GRmoney", groupItem.GRmoney ),
                            //                                                        new SqlParameter("@GuidanceSate", groupItem.GuidanceSate),
                            //                                                         new SqlParameter("@BarState", groupItem.BarState ),
                            //                                                        new SqlParameter("@RequestState", groupItem.RequestState ),
                            //                                                        new SqlParameter("@IsZYB", groupItem.IsZYB  ),
                            //                                                           new SqlParameter("@CreatorUserId",  AbpSession.UserId)

                            //                            };
                            //                            string MoneycusitemSql = string.Format(@"INSERT [dbo].[TjlCustomerItemGroups]
                            //(Id,CustomerRegBMId,DepartmentCodeBM,DepartmentId,DepartmentName,DepartmentOrder,ItemGroupBM_Id
                            //,ItemGroupName,ItemGroupOrder,ItemGroupCodeBM,SFType,ItemSuitId,ItemSuitName,CheckState,SummBackSate,
                            //BillingEmployeeBMId,BillEmployeeBMId,TotalEmployeeBMId,IsAddMinus,RefundState,ItemPrice,DiscountRate,PriceAfterDis,PayerCat,
                            //TTmoney,GRmoney,GuidanceSate,BarState,RequestState,IsZYB,
                            //[TenantId], [IsDeleted], [CreationTime], [CreatorUserId])
                            //VALUES ( @Id,@CustomerRegBMId,@DepartmentCodeBM,@DepartmentId,@DepartmentName,@DepartmentOrder,
                            //@ItemGroupBM_Id
                            //,@ItemGroupName,@ItemGroupOrder,@ItemGroupCodeBM,@SFType,@ItemSuitId,@ItemSuitName,
                            //@CheckState,@SummBackSate,
                            //@BillingEmployeeBMId,@BillEmployeeBMId,@TotalEmployeeBMId,@IsAddMinus,@RefundState,@ItemPrice,
                            //@DiscountRate,@PriceAfterDis,@PayerCat,
                            //@TTmoney,@GRmoney,@GuidanceSate,@BarState,@RequestState,@IsZYB,2,0,GETDATE()	 ,  
                            //	  @CreatorUserId

                            //) ");
                            //                            _sqlExecutor.Execute(MoneycusitemSql, parameter2);
                            //                            #endregion 
                            #endregion

                            //logDetail += "添加组合："+ groupItem.ItemGroupName + "，加项状态："+  groupItem.IsAddMinus + "；"; 
                            if (ret.RegisterState == (int)RegisterState.Yes) //团体保存人员信息用这个方法，但未登记就不要添加项目结果表
                            {
                                foreach (var info in bmGrop.ItemInfos)
                                {
                                    //_customerRegItemRepository.Insert(new TjlCustomerRegItem
                                    //{
                                    //    Id = Guid.NewGuid(),
                                    //    //CustomerRegBM = ret,
                                    //    CustomerRegId = ret.Id,
                                    //    //CustomerItemGroupBM = groupItem,
                                    //    CustomerItemGroupBMid = groupItem.Id,
                                    //    DepartmentId = groupItem.DepartmentId,
                                    //    //ItemGroupBM = bmGrop,
                                    //    ItemGroupBMId = bmGrop.Id,
                                    //    //ItemBM = info,
                                    //    ItemId = info.Id,
                                    //    ItemName = info.Name,
                                    //    ItemTypeBM = info.moneyType,
                                    //    ItemOrder = info.OrderNum,
                                    //    Unit = info.Unit,
                                    //    ProcessState = (int)ProjectIState.Not,
                                    //    DiagnSate = info.DiagnosisComplexSate,
                                    //    SummBackSate = (int)SDState.Unlocked
                                    //});
                                    TjlCustomerRegItem cusitemgs = new TjlCustomerRegItem();

                                    cusitemgs.Id = Guid.NewGuid();

                                    cusitemgs.CustomerRegId = ret.Id;

                                    cusitemgs.CustomerItemGroupBMid = groupItem.Id;
                                    cusitemgs.DepartmentId = groupItem.DepartmentId;

                                    cusitemgs.ItemGroupBMId = bmGrop.Id;

                                    cusitemgs.ItemId = info.Id;
                                    cusitemgs.ItemName = info.Name;
                                    cusitemgs.ItemTypeBM = info.moneyType;
                                    cusitemgs.ItemOrder = info.OrderNum;
                                    cusitemgs.Unit = info.Unit;
                                    cusitemgs.ProcessState = (int)ProjectIState.Not;
                                    cusitemgs.DiagnSate = info.DiagnosisComplexSate;
                                    cusitemgs.SummBackSate = (int)SDState.Unlocked;
                                    TjlCustomerRegItems.Add(cusitemgs);
                                    #region 插入语句
                                    //                                    //新增价格
                                    //                                    SqlParameter[] parameter ={
                                    //                                new SqlParameter("@Id ",  Guid.NewGuid()),
                                    //                                 new SqlParameter("@CustomerRegId ", ret.Id),
                                    //                                new SqlParameter("@CustomerItemGroupBMid ", groupItem.Id),
                                    //                         new SqlParameter("@DepartmentId ",  groupItem.DepartmentId),
                                    //                          new SqlParameter("@ItemGroupBMId", bmGrop.Id),
                                    //                           new SqlParameter("@ItemId", info.Id),
                                    //                            new SqlParameter("@ItemName", info.Name),
                                    //                             new SqlParameter("@ItemTypeBM", info.moneyType ),
                                    //                               new SqlParameter("@ItemOrder", info.OrderNum),
                                    //                                 new SqlParameter("@Unit",  info.Unit),
                                    //                                   new SqlParameter("@ProcessState", (int)ProjectIState.Not),
                                    //                                     new SqlParameter("@DiagnSate", info.DiagnosisComplexSate),
                                    //                                       new SqlParameter("@SummBackSate",  (int)SDState.Unlocked),
                                    //                                        new SqlParameter("@CreatorUserId",  AbpSession.UserId)
                                    //                            };
                                    //                                    string itemSql = string.Format(@" INSERT [dbo].[TjlCustomerRegItems](Id,CustomerRegId,CustomerItemGroupBMid,DepartmentId,ItemGroupBMId,ItemId,ItemName,ItemTypeBM,
                                    //ItemOrder,Unit,ProcessState,DiagnSate,SummBackSate,[TenantId], [IsDeleted], [CreationTime], [CreatorUserId])
                                    //VALUES (@Id,@CustomerRegId,@CustomerItemGroupBMid,@DepartmentId,@ItemGroupBMId,@ItemId,@ItemName,@ItemTypeBM,
                                    //@ItemOrder,@Unit,@ProcessState,@DiagnSate,@SummBackSate,2,0,GETDATE() ,  @CreatorUserId) ");
                                    //                                    _sqlExecutor.Execute(itemSql, parameter);
                                    #endregion
                                }
                            }
                        }
                    }
                    else
                    {
                        //已有组合更新，但目前对组合下项目不做修改，仅更新组合信息
                        //查询已有数据的状态，这些状态在登记中不可修改只是查看
                        // var data = g.MapTo(old);
                        //data.CustomerRegBM = ret;
                        // data.CustomerRegBMId = ret.Id;
                        //  TbmItemGroup tbmItemGroup = _itemGroupRepository.Get(g.ItemGroupBM_Id);
                        TbmItemGroup tbmItemGroup = tbmitemGrouplist.FirstOrDefault(p => p.Id == g.ItemGroupBM_Id);

                        g.Id = old.Id;
                        g.CustomerRegBMId = ret.Id;
                        //g.PayerCat = old.PayerCat;
                        g.DrawSate = old.DrawSate;
                        g.BarState = old.BarState;
                        g.CheckState = old.CheckState;
                        //g.RefundState = old.RefundState;
                        g.RequestState = old.RequestState;
                        g.SuspendState = old.SuspendState;
                        g.SummBackSate = old.SummBackSate;
                        g.ItemGroupOrder = tbmItemGroup.OrderNum;
                        g.DepartmentOrder = tbmItemGroup.Department.OrderNum;
                        Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                        if (tbmItemGroup.Department.Category == "耗材" || tbmItemGroup.Id == guid)
                        {
                            g.CheckState = (int)ProjectIState.Complete;
                        }
                        g.CollectionState = old.CollectionState;
                        var nowGroup = g.MapTo(old);

                        if (ret.RegisterState == (int)RegisterState.Yes && !nowGroup.BillEmployeeBMId.HasValue)
                        {
                            nowGroup.BillEmployeeBMId = AbpSession.UserId.Value;
                        }
                        var group = _customerItemGroupRepository.Update(nowGroup);
                        //logDetail += "修改组合："  + group.ItemGroupName + "，加项状态：" + group.IsAddMinus+"；";
                        if (isHasSoftDelete == false)
                        {

                            var data = cusGroupItemlist.FirstOrDefault(o => o.CustomerItemGroupBMid == g.Id);
                            var bmGrop = tbmitemGrouplist.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);

                            if (bmGrop == null)
                            {
                                continue;
                            }
                            if (data == null)
                            {
                                if (ret.RegisterState == (int)RegisterState.Yes) //团体保存人员信息用这个方法，但未登记就不要添加项目结果表
                                {
                                    foreach (var info in bmGrop.ItemInfos)
                                    {

                                        _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                        {
                                            Id = Guid.NewGuid(),
                                            // CustomerRegBM = ret,
                                            CustomerRegId = ret.Id,
                                            //CustomerItemGroupBM = group,
                                            CustomerItemGroupBMid = group.Id,
                                            DepartmentId = group.DepartmentId,
                                            //ItemGroupBM = bmGrop,
                                            //ItemBM = info,
                                            ItemId = info.Id,
                                            ItemName = info.Name,
                                            ItemTypeBM = info.moneyType,
                                            ItemOrder = info.OrderNum,
                                            Unit = info.Unit,
                                            ProcessState = (int)ProjectIState.Not,
                                            DiagnSate = info.DiagnosisComplexSate,
                                            SummBackSate = (int)SDState.Unlocked
                                        });
                                    }
                                }
                            }
                            else//已有项目细项要更新
                            {   //循环已有的 已经从字典去掉的 删除
                                foreach (var i in old.CustomerRegItem?.ToList())
                                {
                                    if (bmGrop != null && !(bmGrop.ItemInfos.Any(o => o.Id == i.ItemId)))
                                        _customerRegItemRepository.Delete(i);
                                }
                                //循环字典里已有的更新 没有的增加
                                foreach (var info in bmGrop?.ItemInfos)
                                {
                                    var regItem = cusGroupItemlist.FirstOrDefault(o => o.CustomerItemGroupBMid == old.Id && o.ItemId == info.Id);
                                    if (regItem != null)
                                    {
                                        regItem.DepartmentId = group.DepartmentId;
                                        regItem.ItemBM = info;
                                        regItem.ItemName = info.Name;
                                        regItem.ItemTypeBM = info.moneyType;
                                        regItem.ItemOrder = info.OrderNum;
                                        if (!string.IsNullOrEmpty(info.Unit) || string.IsNullOrEmpty(regItem.Unit))
                                        {
                                            regItem.Unit = info.Unit;
                                        }
                                        regItem.DiagnSate = info.DiagnosisComplexSate;
                                    }
                                    else
                                    {
                                        _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                        {
                                            Id = Guid.NewGuid(),
                                            //CustomerRegBM = ret,
                                            CustomerRegId = ret.Id,
                                            //CustomerItemGroupBM = group,
                                            DepartmentId = group.DepartmentId,
                                            // ItemGroupBM = bmGrop,
                                            ItemGroupBMId = bmGrop.Id,
                                            //ItemBM = info,
                                            ItemId = info.Id,
                                            ItemName = info.Name,
                                            ItemTypeBM = info.moneyType,
                                            ItemOrder = info.OrderNum,
                                            Unit = info.Unit,
                                            ProcessState = (int)ProjectIState.Not,
                                            DiagnSate = info.DiagnosisComplexSate,
                                            SummBackSate = (int)SDState.Unlocked
                                        });
                                    }
                                }
                            }
                        }

                    }
                    #endregion 组合处理 遍历内 开始
                    //插入日志

                }


                if (TjlCustomerItemGroups.Count > 0)
                {
                    _sqlExecutor.DbContext.Set<TjlCustomerItemGroup>().AddRange(TjlCustomerItemGroups);
                    // SqlBulkAdd(TjlCustomerItemGroups, "TjlCustomerItemGroups", _sqlExecutor.DbContext.c); 
                }
                if (TjlCustomerRegItems.Count > 0)
                {
                    _sqlExecutor.DbContext.Set<TjlCustomerRegItem>().AddRange(TjlCustomerRegItems);
                    // SqlBulkAdd(TjlCustomerRegItems, "TjlCustomerRegItems", "");
                }
                //查询是否有未收费的组合项目。如果有就登记表收费状态即为未收费
                #region 插入收费项目
                if (cusGroupNew.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Any(o =>
                                       o.PayerCat == (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus)
                            ) //于慧说减项在人员收费后也是未收费状态，所以不参加判断该人员是否收费
                {
                    ret.CostState = (int)PayerCatType.NoCharge;
                }
                else
                {
                    ret.CostState = (int)PayerCatType.Charge;
                }

                var payinfo = _mcusPayMoneyRepository.FirstOrDefault(o => o.CustomerReg.Id == ret.Id);
                if (payinfo == null)
                {
                    _mcusPayMoneyRepository.Insert(new TjlMcusPayMoney
                    {

                        Customer_Id = retCus.Id,
                        Id = ret.Id,
                        ClientInfo_Id = ret.ClientInfoId,
                        ClientReg_Id = ret.ClientRegId,
                        ClientTeamInfo_Id = ret.ClientTeamInfoId,
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
                    if (payinfo.PersonalMoney != persona
        /// <summary>
        /// 获取体检人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
                        public List<TjlCustomerDto> QueryCustomer(CusNameInput input)
        {
            var query = _customerRepository.GetAll();
            var TjlCustomer = query
                .Where(m => m.ArchivesNum == input.Theme.ToString() || m.Name == input.Theme.ToString() ||
                            m.IDCardNo == input.Theme.ToString() || m.CardNumber == input.Theme.ToString())
                .OrderByDescending(m => ((TjlCustomerReg)m.CustomerReg).CreationTime).ToList();

            return TjlCustomer.MapTo<List<TjlCustomerDto>>();
        }

        /// <summary>
        /// 获取体检人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TjlCustomerDto QueryCustomerByID(CusNameInput input)
        {
            var query = _customerRepository.FirstOrDefault(m => m.Id == input.Id);

            return query.MapTo<TjlCustomerDto>();
        }

        //public List<CustomerRegDto> QueryAll(QueryCustomerRegDto input)
        //{
        //    return BuildQuery(input);
        //}
        public PageResultDto<CustomerRegRosterDto> QueryAll(PageInputDto<QueryCustomerRegDto> input)
        {
            return BuildQuery(input);
        }

        public CustomerRegDto GetCustomerRegDto(CusNameInput cusNameInput)
        {
            var query = _customerRegRepository.FirstOrDefault(p => p.Id == cusNameInput.Id);

            return query.MapTo<CustomerRegDto>();
        }
        /// <summary>
        /// 获取体检次数
        /// </summary>
        /// <param name="cusNameInput"></param>
        /// <returns></returns>
        public CusNameInput GetCustomerRegCountDto(CusNameInput cusNameInput)
        {
            var query = _customerRegRepository.Count(p=>p.Customer.IDCardNo== cusNameInput.Theme && p.SummSate==(int)SummSate.Audited);
            CusNameInput outout = new CusNameInput();
            outout.Theme= query.ToString();
            return outout;
        }
        public CusPayMoneyViewDto GetCusPayMoney(CusNameInput cusNameInput)
        {
            var query = _mcusPayMoneyRepository.FirstOrDefault(p => p.Id == cusNameInput.Id);

            return query.MapTo<CusPayMoneyViewDto>();
        }
        /// <summary>
        /// 根据ID查体检人收费状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerRegCostDto GetCustomerRegCost(EntityDto<Guid> input)
        {
            var query = _customerRegRepository.Get(input.Id);
            return query.MapTo<CustomerRegCostDto>();
        }

        /// <summary>
        /// 查询单位分组信息
        /// </summary>
        public List<ClientTeamInfoDto> QueryClientTeamInfos(ClientTeamInfoDto input)
        {
            var teams = _clientTeamInfoRepository.GetAll();
            if (input.ClientReg_Id != Guid.Empty)
            {
                teams = teams.Where(o => o.ClientReg.Id == input.ClientReg_Id);
                
            }          
            var rows= teams.MapTo<List<ClientTeamInfoDto>>();
            return rows.OrderBy(o=>o.TeamBM).ToList();            
        }
        /// <summary>
        /// 根据单位查询单位分组信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ClientTeamInfoDto> QueryClientTeamInfoes(ClientTeamInfoDto input)
        {
            var teams = _clientTeamInfoRepository.GetAll().Where(o => o.ClientRegId == input.Id);
            var rows= teams.MapTo<List<ClientTeamInfoDto>>();
            return rows;
        }

        /// <summary>
        /// 查询单位预约信息
        /// </summary>
        /// <returns></returns>
        public List<ClientRegDto> QuereyClientRegInfos(FullClientRegDto dto)
        {
            var regs = _clientRegRepository.GetAll().AsNoTracking();
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                regs = regs.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            if (dto.FZState != 0)
            {
                //regs = regs.Where(o => o.FZState == dto.FZState);
                regs = from c in regs where c.FZState == dto.FZState select c;
            }

            if (dto.SDState != 0)
            {
                //regs = regs.Where(o => o.SDState == dto.SDState);
                regs = from c in regs where c.SDState == dto.SDState select c;
            }

            regs = regs.Include(r => r.ClientInfo);
            var task = regs.ToListAsync();
            task.Wait();
            var result = task.Result;
            var rows = result.OrderByDescending(o => o.CreationTime).MapTo<List<ClientRegDto>>();
            return rows;
        }

        /// <summary>
        /// 批量登记或取消登记
        /// </summary>
        public void BatchReg(BatchRegInputDto input)
        {
            if (input.RegIds != null)
            {
                var customerRegs = _customerRegRepository.GetAll().Where(r => input.RegIds.Contains(r.Id)).ToList();
                if (input.IsReg)
                {
                    //更新登记状态和登记时间
                    var LoginDate = DateTime.Now;
                    _customerRegRepository.GetAll().Where(r => input.RegIds.Contains(r.Id)).Update(o => new TjlCustomerReg
                    {
                        RegisterState = (int)RegisterState.Yes,
                        LoginDate = LoginDate
                    });
                    //耗材为已检
                    var isAddMinus = (int)AddMinusType.Minus;
                    _customerItemGroupRepository.GetAll().Where(o => o.IsAddMinus != isAddMinus && input.RegIds.Contains(o.CustomerRegBMId.Value)
                    && o.DepartmentBM.Category == "耗材").Update(o => new TjlCustomerItemGroup { CheckState = (int)ProjectIState.Complete });
                    //更新价格，省略
                    //插入项目
                    var cusGrouIs = _customerItemGroupRepository.GetAll().Where(o => o.IsAddMinus != isAddMinus && input.RegIds.Contains(o.CustomerRegBMId.Value)).
                        Select(o => new { o.CustomerRegBMId, o.Id, o.ItemGroupBM_Id, o.DepartmentId }).ToList();
                    //避免重复查询组合，已有的不再查询
                    var itemgrouplist = new List<TbmItemGroup>();
                    List<TjlCustomerRegItem> createCusItem = new List<TjlCustomerRegItem>();
                    //获取已有项目
                    var cusITem = _customerRegItemRepository.GetAll().Where(o => input.RegIds.Contains(o.CustomerRegId)).Select(o => new
                    {
                        o.CustomerRegId,
                        o.ItemId
                    }).ToList();                 
                  

                    foreach (var cusID in cusGrouIs)
                    {
                        

                        var itemgroup = itemgrouplist.FirstOrDefault(o => o.Id == cusID.ItemGroupBM_Id);
                        if (itemgroup != null)
                        {
                            foreach (var iteminfo in itemgroup.ItemInfos)
                            {
                                //已有项目不重复保存
                                if (cusITem.Any(o => o.CustomerRegId == cusID.CustomerRegBMId && o.ItemId == iteminfo.Id))
                                {
                                    continue;
                                }

                                TjlCustomerRegItem tjlCustomerRegItem = new TjlCustomerRegItem();

                                tjlCustomerRegItem.Id = Guid.NewGuid();
                                tjlCustomerRegItem.CustomerRegId = cusID.CustomerRegBMId.Value;
                                tjlCustomerRegItem.CustomerItemGroupBMid = cusID.Id;
                                tjlCustomerRegItem.DepartmentId = cusID.DepartmentId;
                                tjlCustomerRegItem.ItemGroupBMId = cusID.ItemGroupBM_Id;


                                tjlCustomerRegItem.ItemId = iteminfo.Id; ;
                                tjlCustomerRegItem.ItemName = iteminfo.Name;
                                tjlCustomerRegItem.ItemTypeBM = iteminfo.moneyType;
                                tjlCustomerRegItem.ItemOrder = iteminfo.OrderNum;
                                tjlCustomerRegItem.Unit = iteminfo.Unit;

                                tjlCustomerRegItem.ProcessState = (int)ProjectIState.Not;
                                tjlCustomerRegItem.DiagnSate = iteminfo.DiagnosisComplexSate;
                                tjlCustomerRegItem.SummBackSate = (int)SDState.Unlocked;
                                createCusItem.Add(tjlCustomerRegItem);
                            }
                        }
                        else
                        {
                            itemgroup = _itemGroupRepository.Get(cusID.ItemGroupBM_Id.Value);
                            foreach (var iteminfo in itemgroup.ItemInfos)
                            {
                                //已有项目不重复保存
                                if (cusITem.Any(o => o.CustomerRegId == cusID.CustomerRegBMId && o.ItemId == iteminfo.Id))
                                {
                                    continue;
                                }
                                TjlCustomerRegItem tjlCustomerRegItem = new TjlCustomerRegItem();

                                tjlCustomerRegItem.Id = Guid.NewGuid();
                                tjlCustomerRegItem.CustomerRegId = cusID.CustomerRegBMId.Value;
                                tjlCustomerRegItem.CustomerItemGroupBMid = cusID.Id;
                                tjlCustomerRegItem.DepartmentId = cusID.DepartmentId;
                                tjlCustomerRegItem.ItemGroupBMId = cusID.ItemGroupBM_Id;


                                tjlCustomerRegItem.ItemId = iteminfo.Id; ;
                                tjlCustomerRegItem.ItemName = iteminfo.Name;
                                tjlCustomerRegItem.ItemTypeBM = iteminfo.moneyType;
                                tjlCustomerRegItem.ItemOrder = iteminfo.OrderNum;
                                tjlCustomerRegItem.Unit = iteminfo.Unit;

                                tjlCustomerRegItem.ProcessState = (int)ProjectIState.Not;
                                tjlCustomerRegItem.DiagnSate = iteminfo.DiagnosisComplexSate;
                                tjlCustomerRegItem.SummBackSate = (int)SDState.Unlocked;
                                itemgrouplist.Add(itemgroup);
                                createCusItem.Add(tjlCustomerRegItem);
                            }
                        }
                    }
                    //批量插入
                    if (createCusItem.Count > 0)
                    {
                        _sqlExecutor.DbContext.Set<TjlCustomerRegItem>().AddRange(createCusItem);

                       
                    }
                }               
                else
                {

                    _customerRegRepository.GetAll().Where(r => input.RegIds.Contains(r.Id)).Update(o=> new TjlCustomerReg { RegisterState= (int)RegisterState.No });
                    
                }
                

            }

            //if (input.RegIds != null)
            //{
            //    var customerRegs = _customerRegRepository.GetAll().Where(r => input.RegIds.Contains(r.Id)).ToList();
            //    foreach (var customerReg in customerRegs)
            //    {
            //        if (input.IsReg)
            //        {
            //            customerReg.RegisterState = (int)RegisterState.Yes;

            //            if (!customerReg.LoginDate.HasValue)
            //            {
            //                customerReg.LoginDate = DateTime.Now;
            //            }
            //            if (!customerReg.BookingDate.HasValue)
            //            {
            //                customerReg.BookingDate = DateTime.Now;
            //            }
            //            _customerRegRepository.Update(customerReg);

            //            var isAddMinus = (int)AddMinusType.Minus;
            //            decimal personalPay = 0; //个人应收
            //            decimal clientMoney = 0; //团体应收
            //            decimal personalAdd = 0; //个人加项
            //            decimal personalMinusMoney = 0; //个人减项
            //            decimal clientAdd = 0;
            //            decimal clientMinusMoney = 0;

            //            var customerItemGroups = customerReg.CustomerItemGroup.Where(r => r.IsAddMinus != isAddMinus).ToList();
            //            //耗材设置为已检
            //            var hcgroups = customerItemGroups.Where(o => o.DepartmentBM.Category == "耗材");
            //            if (hcgroups.Count() > 0)
            //            {
            //                foreach (var hc in hcgroups)
            //                {
            //                    hc.CheckState = (int)ProjectIState.Complete;
            //                    _customerItemGroupRepository.Update(hc);
            //                }
            //            }
            //            clientMinusMoney = customerReg.CustomerItemGroup.Where(o => o.IsAddMinus == (int)AddMinusType.Minus).Sum(o => o.TTmoney);
            //            personalMinusMoney = customerReg.CustomerItemGroup.Where(o => o.IsAddMinus == (int)AddMinusType.Minus).Sum(o => o.GRmoney);
            //            foreach (var customerItemGroup in customerItemGroups)
            //            {
            //                if (customerItemGroup.IsAddMinus == (int)AddMinusType.Normal || customerItemGroup.IsAddMinus == (int)AddMinusType.Add)
            //                {
            //                    personalPay += customerItemGroup.GRmoney;
            //                    clientMoney += customerItemGroup.TTmoney;
            //                }

            //                if (customerItemGroup.IsAddMinus == (int)AddMinusType.Add)
            //                {
            //                    //加项
            //                    personalAdd += customerItemGroup.GRmoney;
            //                    clientAdd += customerItemGroup.TTmoney;
            //                }
            //                if (customerItemGroup.CustomerRegItem.Any())
            //                {
            //                    continue;
            //                }

            //                if (customerItemGroup.ItemGroupBM == null)
            //                {
            //                    continue;
            //                }

            //                var itemGroup = customerItemGroup.ItemGroupBM;
            //                var itemInfos = itemGroup.ItemInfos.ToList();
            //                foreach (var itemInfo in itemInfos)
            //                {
            //                    _customerRegItemRepository.Insert(new TjlCustomerRegItem
            //                    {
            //                        Id = Guid.NewGuid(),
            //                        CustomerRegBM = customerReg,
            //                        CustomerItemGroupBM = customerItemGroup,
            //                        DepartmentId = customerItemGroup.DepartmentId,
            //                        ItemGroupBM = itemGroup,
            //                        ItemBM = itemInfo,
            //                        ItemName = itemInfo.Name,
            //                        ItemTypeBM = itemInfo.moneyType,
            //                        ItemOrder = itemInfo.OrderNum,
            //                        Unit = itemInfo.Unit,
            //                        ProcessState = (int)ProjectIState.Not,
            //                        DiagnSate = itemInfo.DiagnosisComplexSate,
            //                        SummBackSate = (int)SDState.Unlocked
            //                    });
            //                }
            //            }
            //            var payinfo = _mcusPayMoneyRepository.FirstOrDefault(o => o.CustomerReg.Id == customerReg.Id);
            //            if (payinfo == null)
            //            {
            //                _mcusPayMoneyRepository.Insert(new TjlMcusPayMoney
            //                {
            //                    //Id = Guid.NewGuid(),
            //                    Customer = customerReg.Customer,
            //                    CustomerReg = customerReg,
            //                    ClientInfo = customerReg.ClientInfo,
            //                    ClientReg = customerReg.ClientReg,
            //                    ClientTeamInfo = customerReg.ClientTeamInfo,
            //                    PersonalAddMoney = personalAdd,
            //                    PersonalMinusMoney = personalMinusMoney,
            //                    ClientAdjustAddMoney = clientAdd,
            //                    ClientAdjustMinusMoney = clientMinusMoney,
            //                    PersonalMoney = personalPay,
            //                    ClientMoney = clientMoney,
            //                    PersonalPayMoney = 0
            //                });
            //            }
            //            else
            //            {
            //                if (payinfo.PersonalMoney != personalPay || payinfo.ClientMoney != clientMoney ||
            //                    payinfo.PersonalAddMoney != personalAdd || payinfo.ClientAddMoney != personalAdd ||
            //                    payinfo.ClientMinusMoney != clientMinusMoney)
            //                {
            //                    payinfo.PersonalMoney = personalPay;
            //                    payinfo.ClientMoney = clientMoney;
            //                    payinfo.PersonalAddMoney = personalAdd;
            //                    payinfo.PersonalMinusMoney = personalMinusMoney;
            //                    payinfo.ClientAddMoney = clientAdd;
            //                    payinfo.ClientMinusMoney = clientMinusMoney;
            //                    _mcusPayMoneyRepository.Update(payinfo);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            customerReg.RegisterState = (int)RegisterState.No;
            //            _customerRegRepository.Update(customerReg);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 查询登记列表
        /// </summary>
        public List<CusRegListViewDto> QueryRegList(SearchCusRegListDto input)
        {
            //var query = _customerRegRepository.GetAll().Where(o => o.RegisterState == (int)RegisterState.Yes)
            //    .AsNoTracking();
            var query = _customerRegRepository.GetAll().AsNoTracking();
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }
            if (input.Satr.HasValue && input.End.HasValue)
            {
                query = query.Where(o => o.LoginDate >= input.Satr && o.LoginDate < input.End);

            }
            else if (input.Day != 0)
            {
                //var start = DateTime.Parse(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 23:59:59");
                //var end = DateTime.Parse(DateTime.Now.Date.AddDays(-input.Day + 1).ToString("yyyy-MM-dd") + " 00:00:00");

                int tian = input.Day;
                tian--;
                var end = Convert.ToDateTime(DateTime.Now.Date.AddDays(-tian).ToString("yyyy-MM-dd") + " 00:00:00");
                var start = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 23:59:59");

                query = query.Where(o => o.LoginDate < start && o.LoginDate > end);
            }
            
            if (input.ClientRegId != Guid.Empty)
            {
                query = query.Where(o => o.ClientRegId == input.ClientRegId);
            }

            if (input.CheckState != 0)
            {
                query = query.Where(o => o.CheckSate == input.CheckState);
            }
            if (input.RegisterState != 0)
            {
                query = query.Where(o => o.RegisterState == input.RegisterState);
            }
            if (input.CostState != 0)
            {
                query = query.Where(o => o.CostState == input.CostState);
            }
            if (input.SendToConfirm != 0)
            {
                query = query.Where(o => o.SendToConfirm == input.SendToConfirm);
            }
            //var vs = query.MapTo<List<CusRegListViewDto>>();

            var result = query.OrderByDescending(o => o.LoginDate).Select(r => new CusRegListViewDto
            {
                Id = r.Id,
                Name = r.Customer.Name,
                CustomerBM = r.CustomerBM,
                CheckSate = r.CheckSate,
                RegisterState=r.RegisterState,
                CostState = r.CostState,
                SendToConfirm = r.SendToConfirm,
                LoginDate = r.LoginDate,
                 ClientName=r.ClientInfo.ClientName,
                  CusRegNum=r.CustomerRegNum,
                   ClientRegNum=r.ClientRegNum

            }).OrderByDescending(p=>p.LoginDate).ToList();
            return result;
        }

        /// <summary>
        /// 获取体检预约信息
        /// </summary>
        public List<QueryCustomerRegDto> QueryCustomerReg(SearchCustomerDto input)
        {
            var query = _customerRegRepository.GetAll().AsNoTracking();
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }
            if (input.NotCheckState.HasValue)
            {
                if (input.NotCheckState == (int)PhysicalEState.Complete)
                    query = query.Where(o => o.CheckSate != input.NotCheckState && o.SummSate != (int)SummSate.Audited);
                else
                    query = query.Where(o => o.CheckSate != input.NotCheckState);
            }
            if (!string.IsNullOrWhiteSpace(input.ArchivesNum))
            {
                query = query.Where(o => o.Customer.ArchivesNum == input.ArchivesNum);
            }
            if (!string.IsNullOrWhiteSpace(input.OrderNum))
            {
                query = query.Where(o => o.OrderNum == input.OrderNum);
            }
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(o => o.Customer.Name.Contains(input.Name));
            }
            if (!string.IsNullOrWhiteSpace(input.IDCardNo))
            {
                query = query.Where(o => o.Customer.IDCardNo == input.IDCardNo);
            }
            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                query = query.Where(o => o.CustomerBM == input.CustomerBM);
            }
            if (!string.IsNullOrWhiteSpace(input.VisitCard))
            {
                query = query.Where(o => o.Customer.VisitCard == input.VisitCard || o.HisSectionNum== input.VisitCard);
            }
            if (!string.IsNullOrEmpty(input.WorkNumber))
            {
                query = query.Where(o => o.Customer.WorkNumber == input.WorkNumber );
            }

            var result = query.OrderByDescending(o => o.CreationTime);
            if (result?.ToList().Count == 0)
            {
                var que = _customerRegRepository.GetAll().Where(o => o.Customer.ArchivesNum == input.CustomerBM && o.CheckSate != input.NotCheckState);

                if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
                {
                    que = que.Where(p => p.HospitalArea == userBM.HospitalArea
                    || p.HospitalArea == null);
                }

                var data = que.OrderByDescending(o => o.CreationTime);
              

                if (data.ToList().Count > 0)
                    return data.MapTo<List<QueryCustomerRegDto>>();
                //体检状态都为完成的时候就只能查询个人的信息 查不到登记信息了
                var customers = _customerRepository.GetAll();
                //if (!string.IsNullOrWhiteSpace(input.ArchivesNum)||!string.IsNullOrWhiteSpace(input.CustomerBM))
                //    customers = customers.Where(o => o.ArchivesNum == input.ArchivesNum||o.ArchivesNum==input.CustomerBM);
                if (!string.IsNullOrWhiteSpace(input.Name))
                {
                    customers = customers.Where(o => o.Name == input.Name);
                }

                if (!string.IsNullOrWhiteSpace(input.IDCardNo))
                {
                    customers = customers.Where(o => o.IDCardNo == input.IDCardNo);
                }

                if (string.IsNullOrWhiteSpace(input.ArchivesNum) && string.IsNullOrWhiteSpace(input.Name) &&
                    string.IsNullOrWhiteSpace(input.IDCardNo))
                {
                    return null;
                }

                var customer = customers.OrderByDescending(o => o.CreationTime).FirstOrDefault();
                if (customer != null)
                {
                    return new List<QueryCustomerRegDto> { new QueryCustomerRegDto { Customer = customer.MapTo<QueryCustomerDto>() } };
                }
            }

            var retdatas = new List<QueryCustomerRegDto>();//result.MapTo<List<QueryCustomerRegDto>>();
            if (result?.ToList().Count > 0)
            {
                foreach (var r in result)
                {
                    var data = r.MapTo<QueryCustomerRegDto>();
                    data.CustomerItemGroup = data.CustomerItemGroup.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder)?.ToList();
                    if (r.ClientInfo != null)
                        data.DanweiMingcheng = r.ClientInfo.ClientName;
                    if (r.LastModifierUserId.HasValue)
                    {
                        var user = _userRepository.FirstOrDefault(o => o.Id == r.LastModifierUserId);
                        if (user != null)
                        {

                            data.KaidanYisheng = user.Name;
                        }
                    }
                    else
                    {
                        var user = _userRepository.FirstOrDefault(o => o.Id == r.CreatorUserId);
                        data.OrderUserId = user.Id;
                    }
                    retdatas.Add(data);
                }

            }
            return retdatas;
        }

        /// <summary>
        /// 登记 保存客户信息、客户登记信息、选择套餐数据
        /// </summary>
        public List<QueryCustomerRegDto> RegCustomer(List<QueryCustomerRegDto> inputDatas)
        {
            if (inputDatas == null)
            {
                throw new FieldVerifyException("无数据", "无数据");
            }
            if (inputDatas.Count == 0)
            {
                throw new FieldVerifyException("无数据", "无数据");
            }
            var result = new List<QueryCustomerRegDto>();
            List<Guid> RiskIds = new List<Guid>();
            foreach (var input in inputDatas)
            {
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                string logText = "";
                string logDetail = "";
                #region  遍历体检人 开始

                #region 条件判断 开始
                if (input.RegisterState == (int)RegisterState.Yes)
                {
                    if (input.CheckSate == (int)ProjectIState.Not || !input.CheckSate.HasValue || !input.LoginDate.HasValue)
                    {
                        input.LoginDate = DateTime.Now;
                    }
                    //登记号
                    if ((input.CheckSate == (int)ProjectIState.Not || !input.CheckSate.HasValue ) && !input.CustomerRegNum.HasValue)
                    {
                        //int cusRegBM = 1;
                        //var dttime =DateTime.Parse( System.DateTime.Now.ToShortDateString());
                        //var MaxCusRegBM = _customerRegRepository.GetAll().Where(o => o.LoginDate>= dttime).Select(o => o.CustomerRegNum).Max();
                        //if (MaxCusRegBM != null)
                        //{
                        //    cusRegBM = int.Parse(MaxCusRegBM.ToString()) + 1;

                        //}
                        //input.CustomerRegNum = cusRegBM;
                        //新登记号生成 方法
                        input.CustomerRegNum = _idNumberAppService.CreateRegNum();
                        #region 遵义需求登记重新生成登记号
                        var cxsc = _tbmBasicDictionary.GetAll().FirstOrDefault(p=>p.Type== "ForegroundFunctionControl" && p.Value==22)?.Remarks;
                        var tm = System.DateTime.Now.ToString("yyMMdd");
                        if (!string.IsNullOrEmpty(cxsc) && cxsc == "1" && !input.CustomerBM.Contains(tm))
                        {
                            input.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                        }
                        #endregion

                    }
                }
                var reData =
                    _customerRegRepository.FirstOrDefault(o =>
                        o.CustomerBM == input.CustomerBM && o.Id != input.Id); //判断登记信息中体检号是否重复
                if (reData != null)
                {
                    throw new FieldVerifyException("体检号重复", "体检号重复，请修改后登记。");
                }

                if (input.RegisterState == (int)RegisterState.Yes)
                {
                    if (input.CustomerItemGroup == null)
                    {
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                    }

                    if (input.CustomerItemGroup.Count == 0)
                    {
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                    }
                }
                #endregion 条件判断 结束


                #region 添加或更新客户信息 开始
                var cusGroupls = input.CustomerItemGroup;
                var ret = new TjlCustomerReg();
                var retCus = new TjlCustomer();
                var oldgroplist = new List<TjlCustomerItemGroup>();
                var customer = input.Customer;
                var oldcus = new TjlCustomer();
                if (!string.IsNullOrEmpty(input.Customer.IDCardNo))
                {
                    oldcus = _customerRepository.FirstOrDefault(o => o.IDCardNo == customer.IDCardNo 
                    && o.Name== customer.Name && o.Sex== customer.Sex);
                    if (oldcus == null)
                    { oldcus = new TjlCustomer(); }
                }
                if (oldcus.Id ==Guid.Empty && !string.IsNullOrWhiteSpace(customer.ArchivesNum))
                {
                    oldcus = _customerRepository.FirstOrDefault(o => o.ArchivesNum == customer.ArchivesNum);
                    //替检查不关联之前档案
                    if (input.ReplaceSate == 2 )
                    {
                        //判断不同名或不是同一个身份证号则创建
                        if (oldcus!=null && ( oldcus.Name != customer.Name || (!string.IsNullOrEmpty(oldcus.IDCardNo) && !string.IsNullOrEmpty(customer.IDCardNo) && oldcus.IDCardNo != customer.IDCardNo)))
                        {
                            oldcus = null;
                            oldcus = new TjlCustomer();
                            customer.Id = Guid.Empty;
                            customer.ArchivesNum = input.CustomerBM;
                        }
                    }
                  
                }
                if (oldcus != null)
                {
                    customer.Id = oldcus.Id;
                    //此次没有照片关联旧照片
                    if (oldcus.CusPhotoBmId.HasValue && !customer.CusPhotoBmId.HasValue)
                    {
                        customer.CusPhotoBmId = oldcus.CusPhotoBmId;
                    }
                }
                if (customer.Id == Guid.Empty)
                {
                    var customerEntity = customer.MapTo<TjlCustomer>();
                    customerEntity.Id = Guid.NewGuid();
                    //行业处理2021-12-31
                    if (input.ClientRegId.HasValue && string.IsNullOrEmpty(input.Customer.CustomerTrade))
                    {
                        var clietint = _clientRegRepository.Get(input.ClientRegId.Value).ClientInfo?.Clientlndutry;
                        if (!string.IsNullOrEmpty(clietint))
                        {
                            customerEntity.CustomerTrade = clietint;
                        }
                    }
                    customerEntity.NameAB = _commonAppService.GetHansBrief(new ChineseDto { Hans = customerEntity.Name })?.Brief;

                    retCus = _customerRepository.Insert(customerEntity);
                    logDetail += "添加档案："+ customerEntity.ArchivesNum +"；";
                }
                else
                {
                    var oldCustomer = input.Customer;
                    var customerEntity = _customerRepository.Get(customer.Id);
                    oldCustomer.ArchivesNum = customerEntity.ArchivesNum;
                    //行业处理2021-12-31
                    if (input.ClientRegId.HasValue && string.IsNullOrEmpty(input.Customer.CustomerTrade))
                    {
                        var clietint = _clientRegRepository.Get(input.ClientRegId.Value).ClientInfo?.Clientlndutry;
                        if (!string.IsNullOrEmpty(clietint))
                        {
                            oldCustomer.CustomerTrade = clietint;
                        }
                    }
                    oldCustomer.MapTo(customerEntity);
                    customerEntity.NameAB = _commonAppService.GetHansBrief(new ChineseDto { Hans = oldCustomer.Name })?.Brief;

                    retCus = _customerRepository.Update(customerEntity);
                   // CurrentUnitOfWork.SaveChanges();
                    logDetail += "修改档案：" + customerEntity.ArchivesNum + "；";

                }
                #endregion 添加或更新客户信息 结束

                input.Customer = null;

                #region 修改客户登记信息 开始
                //先根据体检号码查询一下库里是否有该体检号信息
                var reg = _customerRegRepository.FirstOrDefault(o => o.Id == input.Id);
                if (reg != null)
                {
                    if (reg.CustomerBM != reg.CustomerBM) //根据当前Id查询出登记信息，
                    {
                        input.Id = Guid.Empty;
                    }
                }
                if (input.Id == Guid.Empty)
                {
                    var data = input.MapTo<TjlCustomerReg>();
                    data.CheckSate = (int)PhysicalEState.Not;
                    data.Id = Guid.NewGuid();
                    data.Customer = retCus;
                    
                    if (input.ClientTeamInfo_Id.HasValue)
                    {
                        //新增是直接根据分组信息增加该字段
                        data.ClientTeamInfo = _clientTeamInfoRepository.Get(input.ClientTeamInfo_Id.Value);
                        data.ItemSuitBMId = data.ClientTeamInfo.ItemSuit_Id;
                        data.ItemSuitName = data.ClientTeamInfo.ItemSuitName;
                        if (data.ClientTeamInfo.ClientReg != null)
                        {
                            data.ClientInfo = data.ClientTeamInfo.ClientReg.ClientInfo;
                            data.ClientType = data.ClientTeamInfo.ClientReg.ClientSate.ToString();
                            if (data.ClientInfo != null)
                            {
                                data.ClientInfoId = data.ClientInfo.Id;
                            }
                        }
                    }
                    else
                    {
                        if (input.ClientRegId.HasValue)
                        {
                            data.ClientReg = _clientRegRepository.Get(input.ClientRegId.Value);
                            if (data.ClientReg != null)
                            {
                                data.ClientRegId = data.ClientRegId;
                                data.ClientInfo = data.ClientInfo;
                                if (data.ClientInfo != null)
                                {
                                    data.ClientInfoId = data.ClientInfo.Id;
                                    data.ClientType = data.ClientInfo.ClientSate;
                                }
                                else
                                    data.ClientType = ((int)ClientSate.Normal).ToString();
                            }
                        }
                    }
                   
                    data.CustomerItemGroup = null;
                    //职业健康危害因素
                    if (input.OccHazardFactors != null && input.OccHazardFactors.Count > 0)
                    {
                        data.OccHazardFactors.Clear();
                        if (data.OccHazardFactors == null)
                        {
                            data.OccHazardFactors = new List<TbmOccHazardFactor>();
                        }
                        foreach (var rick in input.OccHazardFactors)
                        {
                            var tbmHazar = _TbmOccHazardFactor.Get(rick.Id);
                            data.OccHazardFactors.Add(tbmHazar);
                             
                            if (!RiskIds.Contains(rick.Id))
                            {
                                RiskIds.Add(rick.Id);
                            }
                        }
                    }
                    if (!data.AppointmentTime.HasValue)
                    {
                         data.AppointmentTime = data.BookingDate;
                    }
                    if (data.ClientRegId.HasValue)
                    {
                        int clientBM = 1;
                        var clietid = data.ClientRegId;
                        var MaxClientBM = _customerRegRepository.GetAll().Where(o => o.ClientRegId == clietid && o.ClientRegNum != null).Select(o => o.ClientRegNum).Max();
                        if (MaxClientBM != null)
                        { clientBM = int.Parse(MaxClientBM.ToString()) + 1;
                            
                        }
                        data.ClientRegNum = clientBM;

                    }
                    #region 健康证号处理
                    if (data.PhysicalType.HasValue)
                    {
                        var tjlbName = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                         && p.Value == data.PhysicalType)?.Text;
                        if (!string.IsNullOrEmpty(tjlbName) && 
                            (tjlbName.Contains("从业") || tjlbName.Contains("健康证") || tjlbName.Contains("食品")))
                        {
                            
                            data.JKZBM = _idNumberAppService.CreateJKZBM();
                        }

                    }
                    #endregion
                    var userBM = _userRepository.Get(AbpSession.UserId.Value);
                    if (userBM != null && userBM.HospitalArea.HasValue)
                    {
                        data.HospitalArea = userBM.HospitalArea;
                    }
                    //出报告时间计算
                    if (data.LoginDate.HasValue)
                    {
                        TjlClientReg _clientReg = null;
                        if (data.ClientRegId.HasValue)
                        { _clientReg = _clientRegRepository.Get(data.ClientRegId.Value); }


                        if (_clientReg!=null && _clientReg.ReportDays.HasValue
                            && _clientReg.ReportDays > 0)
                        {
                          
                            data.ReportDate = data.LoginDate.Value.AddDays(_clientReg.ReportDays.Value);
                        }
                        else
                        {
                            if (data.PhysicalType.HasValue)
                            {
                                var days = _tbmBasicDictionary.GetAll().FirstOrDefault(p=>p.Type== "ExaminationType" 
                                && p.Value== data.PhysicalType)?.Code;
                                if (!string.IsNullOrEmpty(days) && int.TryParse(days, out int reDays))
                                {
                                    data.ReportDate = data.LoginDate.Value.AddDays(reDays);

                                }
                            }
                        }
                    }
                    data = _customerRegRepository.Insert(data);
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw;
                    //}
                    // CurrentUnitOfWork.SaveChanges();
                    ret = data;
                    logDetail += "添加登记："+ data.CustomerBM + "；";
                    logText = "添加登记：" + data.CustomerBM + "；"; ;
                    if (!string.IsNullOrEmpty(data.ItemSuitName))
                    {
                        logDetail += "套餐：" + data.ItemSuitName + "；";
                        logText += "套餐：" + data.ItemSuitName + "；";
                    }
                   
                }
                else
                {
                   

                    var regEntity = _customerRegRepository.Get(input.Id);
                    oldgroplist = regEntity.CustomerItemGroup?.ToList();
                   
                    //input.CostState = regEntity.CostState;
                    if (regEntity.RequestState.HasValue)
                    {
                        input.RequestState = regEntity.RequestState;
                    }

                    //if (regEntity.ReviewSate.HasValue)
                    //{
                    //    input.ReviewSate = regEntity.ReviewSate;
                    //}

                    if (regEntity.SendToConfirm.HasValue)
                    {
                        input.SendToConfirm = regEntity.SendToConfirm;
                    }

                    if (regEntity.SummLocked.HasValue)
                    {
                        input.SummLocked = regEntity.SummLocked;
                    }

                    if (regEntity.SummSate.HasValue)
                    {
                        input.SummSate = regEntity.SummSate;
                    }

                    if (regEntity.CheckSate.HasValue)
                    {
                        input.CheckSate = regEntity.CheckSate;
                    }

                    if (regEntity.BarState.HasValue)
                    {
                        input.BarState = regEntity.BarState;
                    }
                    var ooo = input.OccHazardFactors;
                    input.OccHazardFactors = null;
                    input.ClientTeamInfo = null;                    
                    input.CustomerItemGroup = null;
                    input.ClientTeamRegitemInfo = null;
                    input.Customer = null;
                    input.MapTo(regEntity);
                    regEntity.CustomerId = retCus.Id;
                    regEntity.Customer = retCus;
                    regEntity.ClientTeamInfoId = input.ClientTeamInfo_Id;
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception e)
                    //{

                    //    throw;
                    //}
                    
                    var retReg = regEntity;
                    if (retReg.ItemSuitBMId.HasValue)
                    {
                        retReg.ItemSuitBM = _itemSuitRepository.Get(regEntity.ItemSuitBMId.Value);
                    }
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception e)
                    //{

                    //    throw;
                    //}
                    //修改需要需要根据参数修改是否还有分组信息
                    if (input.ClientTeamInfo_Id.HasValue)
                    {
                        retReg.ClientTeamInfo = _clientTeamInfoRepository.Get(input.ClientTeamInfo_Id.Value);
                        retReg.ItemSuitBMId = retReg.ClientTeamInfo.ItemSuit_Id;
                        retReg.ItemSuitName = retReg.ClientTeamInfo.ItemSuitName;
                        ret.ItemSuitBM = retReg.ClientTeamInfo.ItemSuit;
                        retReg.ClientTeamInfoId = input.ClientTeamInfo_Id;
                        retReg.ClientReg = retReg.ClientTeamInfo.ClientReg;
                        retReg.ClientRegId = retReg.ClientReg.Id;
                        if (retReg.ClientTeamInfo.ClientReg != null)
                        {
                            retReg.ClientInfo = retReg.ClientTeamInfo.ClientReg.ClientInfo;
                            if (retReg.ClientInfo != null)
                            {
                                retReg.ClientInfoId = retReg.ClientInfo.Id;
                                retReg.ClientType = retReg.ClientReg.ClientSate.ToString();
                            }
                            else
                                retReg.ClientType = ((int)ClientSate.Normal).ToString();
                        }
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (Exception e)
                        //{

                        //    throw;
                        //}
                    }
                    else
                    {
                        retReg.ClientInfo = null;
                        retReg.ClientInfoId = null;
                        retReg.ClientTeamInfo = null;
                        retReg.ClientReg = null;
                        retReg.ClientRegId = null;
                        if (input.ClientRegId.HasValue)
                        {
                            var clientreg = _clientRegRepository.Get(input.ClientRegId.Value);
                            if (clientreg != null)
                            {
                                retReg.ClientRegId = clientreg.Id;
                                retReg.ClientReg = clientreg;
                                retReg.ClientInfo = clientreg.ClientInfo;
                                if (clientreg.ClientInfo != null)
                                {
                                    retReg.ClientInfoId = clientreg.ClientInfo.Id;
                                    retReg.ClientType = clientreg.ClientSate.ToString();
                                }
                                else
                                    retReg.ClientType = ((int)ClientSate.Normal).ToString();
                            }
                        }
                    }

                     retReg.CustomerItemGroup = null;
                   
                    //职业健康危害因素
                    if (ooo != null && ooo.Count > 0)
                    {
                        if(retReg.OccHazardFactors == null)
                        {
                            retReg.OccHazardFactors = new List<TbmOccHazardFactor>();
                        }
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //    //retReg.OccHazardFactors.Clear();
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (InvalidOperationException e)
                        //{
                            
                        //}
                        //retReg.OccHazardFactors = new List<TbmOccHazardFactor>();
                        foreach (var rick in ooo)
                        {
                            var tbmHazar = _TbmOccHazardFactor.Get(rick.Id);
                            var lst = tbmHazar.Protectivis.ToList();
                            retReg.OccHazardFactors.Add(tbmHazar);
                            if (!RiskIds.Contains(rick.Id))
                            {
                                RiskIds.Add(rick.Id);
                            }
                        }
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (Exception e)
                        //{

                        //    throw;
                        //}
                    }
                    #region 健康证号处理
                    if (retReg.PhysicalType.HasValue)
                    {
                        var tjlbName = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                         && p.Value == retReg.PhysicalType)?.Text;
                        if (!string.IsNullOrEmpty(tjlbName) &&
                            (tjlbName.Contains("从业") || tjlbName.Contains("健康证") || tjlbName.Contains("食品")))
                        {

                            retReg.JKZBM = _idNumberAppService.CreateJKZBM();
                        }

                    }
                    #endregion
                    //出报告时间计算
                    if (retReg.LoginDate.HasValue)
                    {
                        if (retReg.ClientRegId.HasValue && retReg.ClientReg.ReportDays.HasValue
                            && retReg.ClientReg.ReportDays > 0)
                        {
                            retReg.ReportDate = retReg.LoginDate.Value.AddDays(retReg.ClientReg.ReportDays.Value);
                        }
                        else
                        {
                            if (retReg.PhysicalType.HasValue)
                            {
                                var days = _tbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == "ExaminationType"
                                && p.Value == retReg.PhysicalType)?.Code;
                                if (!string.IsNullOrEmpty(days) && int.TryParse(days, out int reDays))
                                {
                                    retReg.ReportDate = retReg.LoginDate.Value.AddDays(reDays);

                                }
                            }
                        }
                    }
                    regEntity = _customerRegRepository.Update(retReg);
                    //CurrentUnitOfWork.SaveChanges();
                    //try
                    //{
                    //    CurrentUnitOfWork.SaveChanges();
                    //}
                    //catch (Exception e)
                    //{

                    //    throw;
                    //}

                    ret = regEntity;
                    logDetail += "修改登记：" + retReg.CustomerBM + "；";
                    logText = "修改登记：" + retReg.CustomerBM + "；";
                }
                #endregion 再修改客户登记信息 结束



                #region 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 开始
                if (oldgroplist.Count > 0)
                {
                    foreach (var old in oldgroplist)
                        if (!cusGroupls.Any(o => o.Id == old.Id))
                        {
                            var olditems = old.CustomerRegItem?.ToList();
                            if (olditems != null)
                            {
                                foreach (var item in olditems)
                                    _customerRegItemRepository.Delete(item);
                            }

                            _customerItemGroupRepository.Delete(old);
                            //CurrentUnitOfWork.SaveChanges();

                        }
                }
                //CurrentUnitOfWork.SaveChanges();
                //try
                //{
                //    CurrentUnitOfWork.SaveChanges();
                //}
                //catch (Exception e)
                //{

                //    throw;
                //}
                #endregion 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 结束

                #region 组合处理
                if (cusGroupls != null)
                {
                    //暂时屏蔽
                    //List<Guid> OccGroupIds = new List<Guid>();
                    #region 获取职业健康必选项目
                    //if (RiskIds.Count > 0 && !string.IsNullOrEmpty( input.PostState))
                    //{
                    // var targetDisease = _OccTargetDisease.GetAll().Where(p=>p.CheckType== input.PostState && RiskIds.Contains(p.OccHazardFactorsId.Value)).ToList();

                    //    var OccGrouplis = targetDisease.SelectMany(p => p.MustIemGroups).ToList();
                    //    OccGroupIds = OccGrouplis.Select(P=>P.Id).Distinct().ToList();

                    //}
                    #endregion
                    #region 单位限额处理
                    if (ret.ClientTeamInfoId.HasValue && ret.ClientTeamInfo != null && ret.ClientTeamInfo.CostType == (int)PayerCatType.FixedAmount &&
                        ret.ClientTeamInfo.QuotaMoney.HasValue)
                    {
                        //限额金额小于团付金额
                        var NopayGroup = cusGroupls.Where(p => p.IsAddMinus != (int)AddMinusType.Minus && p.PayerCat != (int)PayerCatType.Charge).ToList();
                        var TTPayMoney = NopayGroup.Sum(p=>p.TTmoney );
                        var GRPayMoney = NopayGroup.Sum(p=>p.GRmoney);
                        var GRGeouplist = NopayGroup.Where(p => p.GRmoney > 0).ToList();
                        
                        if (TTPayMoney < ret.ClientTeamInfo.QuotaMoney && GRPayMoney>0)
                        {
                          
                            foreach (var cusGroup in GRGeouplist)
                            {
                                 
                                if (TTPayMoney < ret.ClientTeamInfo.QuotaMoney)
                                {
                                    //cusGroup.TTmoney = cusGroup.PriceAfterDis;
                                    //cusGroup.GRmoney = 0;

                                   var  TTPay  = TTPayMoney+ cusGroup.GRmoney;
                                    if (TTPay == ret.ClientTeamInfo.QuotaMoney)
                                    {
                                        var GRMpney = cusGroup.GRmoney;
                                        cusGroup.TTmoney = cusGroup.TTmoney + GRMpney;
                                        cusGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                        cusGroup.GRmoney = 0;
                                        TTPayMoney += GRMpney;
                                        break;
                                        
                                    }
                                    else if (TTPay > ret.ClientTeamInfo.QuotaMoney)
                                    {
                                        decimal grMoney = TTPay - ret.ClientTeamInfo.QuotaMoney.Value;
                                        cusGroup.TTmoney = cusGroup.TTmoney + cusGroup.GRmoney- grMoney;
                                        cusGroup.PayerCat = (int)PayerCatType.PersonalCharge;
                                        TTPayMoney += cusGroup.GRmoney - grMoney;
                                        cusGroup.GRmoney = grMoney;
                                       
                                        break;
                                    }
                                    else
                                    {
                                        var GRMpney= cusGroup.GRmoney;
                                        cusGroup.TTmoney = cusGroup.TTmoney + GRMpney;
                                        cusGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                        cusGroup.GRmoney = 0;
                                        TTPayMoney += GRMpney;
                                    }
                                }

                            }
                        }
                    }


                    #endregion
                    decimal personalPay = 0; //个人应收
                    decimal clientMoney = 0; //团体应收
                    decimal personalAdd = 0; //个人加项
                    decimal personalMinusMoney = 0; //个人减项
                    decimal clientAdd = 0;
                    decimal clientMinusMoney = 0;
                    ret.CustomerItemGroup = null;
                    foreach (var g in cusGroupls)
                    {
                        #region 组合处理 遍历内 开始
                        //已收费的项目不会被删除也不作处理，新加分组数据添加至数据库
                        if (g.IsAddMinus == (int)AddMinusType.Normal || g.IsAddMinus == (int)AddMinusType.Add)
                        {
                            personalPay += g.GRmoney;
                            clientMoney += g.TTmoney;
                        }
                        if (g.IsAddMinus == (int)AddMinusType.Add)
                        {
                            //加项
                            personalAdd += g.GRmoney;
                            clientAdd += g.TTmoney;
                        }
                        else if (g.IsAddMinus == (int)AddMinusType.Minus)
                        {
                            //减项
                            clientMinusMoney += g.TTmoney;
                            personalMinusMoney += g.GRmoney;
                        }

                        // TjlCustomerItemGroup old = _customerItemGroupRepository.FirstOrDefault(o => o.Id == g.Id);
                        TjlCustomerItemGroup old = null;
                        var isHasSoftDelete = false;
                        SqlParameter[] parameters = {
                        new SqlParameter("@id",g.Id),
                        };
                        string strsql = "select * from TjlCustomerItemGroups where Id=@id";
                        List<TjlCustomerItemGroup> lstCustomerItemGroups = _sqlExecutor.SqlQuery<TjlCustomerItemGroup>
                            (strsql, parameters).ToList();
                        if (lstCustomerItemGroups != null && lstCustomerItemGroups.Count > 0)
                        {
                            old = _customerItemGroupRepository.FirstOrDefault(o => o.Id == g.Id);
                            #region 误删除 进行恢复 开始
                            if (old!=null && old.IsDeleted == true)
                            {
                                SqlParameter[] parameter = {
                         new SqlParameter("@CustomerRegId",g.CustomerRegBMId),
                         new SqlParameter("@ItemGroupBMId", g.Id)
                        };
                                strsql = "update TjlCustomerRegItems set IsDeleted=0 where CustomerRegId=@CustomerRegId and ItemGroupBMId=@ItemGroupBMId";
                                int inum = _sqlExecutor.Execute(strsql, parameter);
                            }
                            #endregion 误删除 进行恢复 结束
                        }
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (Exception e)
                        //{

                        //    throw;
                        //}

                        if (old == null) //新增组合添加
                        {
                            var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);
                            if (bmGrop != null)
                            {
                                var groupItem = g.MapTo<TjlCustomerItemGroup>();
                                groupItem.Id = Guid.NewGuid();
                                groupItem.CustomerRegBM = ret;
                                groupItem.DepartmentBM = bmGrop.Department;
                                groupItem.DepartmentId = bmGrop.DepartmentId;
                                groupItem.DepartmentCodeBM = bmGrop.Department.DepartmentBM;
                                groupItem.ItemGroupBM = bmGrop;
                                groupItem.SFType = Convert.ToInt32(bmGrop.ChartCode);
                                groupItem.ItemGroupCodeBM = bmGrop.ItemGroupBM;
                                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                                if (groupItem.DepartmentBM.Category == "耗材" || groupItem.ItemGroupBM_Id == guid)
                                {
                                    groupItem.CheckState = (int)ProjectIState.Complete;
                                }
                                //职业状态暂时屏蔽
                                //if (OccGroupIds.Contains(groupItem.ItemGroupBM_Id.Value))
                                //{
                                //    groupItem.IsZYB = 1;
                                //}
                                //else
                                //{
                                //    groupItem.IsZYB = 2;
                                //}
                                if  (ret.RegisterState == (int)RegisterState.Yes && !groupItem.BillEmployeeBMId.HasValue)
                                {
                                    groupItem.BillEmployeeBMId= AbpSession.UserId.Value;
                                }
                                var grop = _customerItemGroupRepository.Insert(groupItem);
                                //logDetail += "添加组合："+ groupItem.ItemGroupName + "，加项状态："+  groupItem.IsAddMinus + "；"; 
                                if (ret.RegisterState == (int)RegisterState.Yes) //团体保存人员信息用这个方法，但未登记就不要添加项目结果表
                                {
                                    foreach (var info in bmGrop.ItemInfos)
                                        _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                        {
                                            Id = Guid.NewGuid(),
                                            CustomerRegBM = ret,
                                            CustomerItemGroupBM = groupItem,
                                            DepartmentId = groupItem.DepartmentId,
                                            ItemGroupBM = bmGrop,
                                            ItemBM = info,
                                            ItemName = info.Name,
                                            ItemTypeBM = info.moneyType,
                                            ItemOrder = info.OrderNum,
                                            Unit = info.Unit,
                                            ProcessState = (int)ProjectIState.Not,
                                            DiagnSate = info.DiagnosisComplexSate,
                                            SummBackSate = (int)SDState.Unlocked
                                        });
                                }
                            }
                        }
                        else
                        {
                            //已有组合更新，但目前对组合下项目不做修改，仅更新组合信息
                            //查询已有数据的状态，这些状态在登记中不可修改只是查看
                            // var data = g.MapTo(old);
                            //data.CustomerRegBM = ret;
                            // data.CustomerRegBMId = ret.Id;
                            TbmItemGroup tbmItemGroup = _itemGroupRepository.Get(g.ItemGroupBM_Id);
                            g.Id = old.Id;
                            g.CustomerRegBMId = ret.Id;
                            //g.PayerCat = old.PayerCat;
                            g.DrawSate = old.DrawSate;
                            g.BarState = old.BarState;
                            g.CheckState = old.CheckState;
                            //g.RefundState = old.RefundState;
                            g.RequestState = old.RequestState;
                            g.SuspendState = old.SuspendState;
                            g.SummBackSate = old.SummBackSate;
                            g.ItemGroupOrder = tbmItemGroup.OrderNum;
                            g.DepartmentOrder = tbmItemGroup.Department.OrderNum;
                            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                            if (tbmItemGroup.Department.Category == "耗材" || tbmItemGroup.Id == guid)
                            {
                                g.CheckState = (int)ProjectIState.Complete;
                            }
                            g.CollectionState = old.CollectionState;
                            var nowGroup = g.MapTo(old);
                            //if (OccGroupIds.Contains(g.ItemGroupBM_Id))
                            //{
                            //    nowGroup.IsZYB = 1;
                            //}
                            //else
                            //{
                            //    nowGroup.IsZYB = 2;
                            //}
                            if (ret.RegisterState == (int)RegisterState.Yes && !nowGroup.BillEmployeeBMId.HasValue)
                            {
                                nowGroup.BillEmployeeBMId = AbpSession.UserId.Value;
                            }
                            var group = _customerItemGroupRepository.Update(nowGroup);
                            //logDetail += "修改组合："  + group.ItemGroupName + "，加项状态：" + group.IsAddMinus+"；";
                            if (isHasSoftDelete == false)
                            {
                                var data = _customerRegItemRepository.FirstOrDefault(o => o.CustomerItemGroupBMid == g.Id);
                                var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == g.ItemGroupBM_Id);
                                if (bmGrop == null)
                                {
                                    continue;
                                }
                                if (data == null)
                                {
                                    if (ret.RegisterState == (int)RegisterState.Yes) //团体保存人员信息用这个方法，但未登记就不要添加项目结果表
                                    {
                                        foreach (var info in bmGrop.ItemInfos)
                                        {

                                            _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                            {
                                                Id = Guid.NewGuid(),
                                                CustomerRegBM = ret,
                                                CustomerItemGroupBM = group,
                                                DepartmentId = group.DepartmentId,
                                                ItemGroupBM = bmGrop,
                                                ItemBM = info,
                                                ItemName = info.Name,
                                                ItemTypeBM = info.moneyType,
                                                ItemOrder = info.OrderNum,
                                                Unit = info.Unit,
                                                ProcessState = (int)ProjectIState.Not,
                                                DiagnSate = info.DiagnosisComplexSate,
                                                SummBackSate = (int)SDState.Unlocked
                                            });
                                        }
                                    }
                                }
                                else//已有项目细项要更新
                                {   //循环已有的 已经从字典去掉的 删除
                                    foreach (var i in old.CustomerRegItem?.ToList())
                                    {
                                        if (bmGrop != null && !(bmGrop.ItemInfos.Any(o => o.Id == i.ItemId)))
                                            _customerRegItemRepository.Delete(i);
                                    }
                                    //循环字典里已有的更新 没有的增加
                                    foreach (var info in bmGrop?.ItemInfos)
                                    {
                                        var regItem = _customerRegItemRepository.FirstOrDefault(o => o.CustomerItemGroupBMid == old.Id && o.ItemId == info.Id);
                                        if (regItem != null)
                                        {
                                            regItem.DepartmentId = group.DepartmentId;
                                            regItem.ItemBM = info;
                                            regItem.ItemName = info.Name;
                                            regItem.ItemTypeBM = info.moneyType;
                                            regItem.ItemOrder = info.OrderNum;
                                            if (!string.IsNullOrEmpty(info.Unit) || string.IsNullOrEmpty(regItem.Unit))
                                            {
                                                regItem.Unit = info.Unit;
                                            }
                                            regItem.DiagnSate = info.DiagnosisComplexSate;
                                        }
                                        else
                                        {
                                            _customerRegItemRepository.Insert(new TjlCustomerRegItem
                                            {
                                                Id = Guid.NewGuid(),
                                                CustomerRegBM = ret,
                                                CustomerItemGroupBM = group,
                                                DepartmentId = group.DepartmentId,
                                                ItemGroupBM = bmGrop,
                                                ItemBM = info,
                                                ItemName = info.Name,
                                                ItemTypeBM = info.moneyType,
                                                ItemOrder = info.OrderNum,
                                                Unit = info.Unit,
                                                ProcessState = (int)ProjectIState.Not,
                                                DiagnSate = info.DiagnosisComplexSate,
                                                SummBackSate = (int)SDState.Unlocked
                                            });
                                        }
                                    }
                                }
                            }

                        }
                        #endregion 组合处理 遍历内 开始
                        //插入日志
                       
                    }
                    //查询是否有未收费的组合项目。如果有就登记表收费状态即为未收费
                    #region 插入收费项目
                    if (cusGroupls.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Any(o =>
                                           o.PayerCat == (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus)
                                ) //于慧说减项在人员收费后也是未收费状态，所以不参加判断该人员是否收费
                    {
                        ret.CostState = (int)PayerCatType.NoCharge;
                    }
                    else
                    {
                        ret.CostState = (int)PayerCatType.Charge;
                    }

                    var payinfo = _mcusPayMoneyRepository.FirstOrDefault(o => o.CustomerReg.Id == ret.Id);
                    if (payinfo == null)
                    {
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
                    else
                    {
                        if (payinfo.PersonalMoney != personalPay || payinfo.ClientMoney != clientMoney ||
                            payinfo.PersonalAddMoney != personalAdd || payinfo.ClientAddMoney != personalAdd ||
                            payinfo.ClientMinusMoney != clientMinusMoney)
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
                    #endregion
                }
                #endregion 组合处理

                #endregion  遍历体检人 结束
               
                //添加操作日志
                result.Add(ret.MapTo<QueryCustomerRegDto>());
                createOpLogDto.LogBM = ret.CustomerBM;
                createOpLogDto.LogName = ret.Customer.Name;
                createOpLogDto.LogText = logText;
                if (ret.RegisterState == 1)
                {
                    createOpLogDto.LogText = logText.Replace("登记","预约");
                } 
                if (ret.InfoSource == 2)
                {
                    createOpLogDto.LogText += "(线上预约)";
                }
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                if (logDetail.Length > 4000)
                {
                    logDetail = logDetail.Substring(0, 4000);
                }
                createOpLogDto.LogDetail = logDetail;
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            //DateTime dateTime1 = DateTime.Now;

            //try
            //{
            // CurrentUnitOfWork.SaveChanges();
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
           
            return result;
        }
        /// <summary>
        /// 登记 保存客户信息、客户登记信息、选择套餐数据
        /// </summary>
        public List<QueryCustomerRegDto> RegCustomerNew(List<QueryCustomerRegDto> inputDatas)
        {
            if (inputDatas == null)
            {
                throw new FieldVerifyException("无数据", "无数据");
            }
            if (inputDatas.Count == 0)
            {
                throw new FieldVerifyException("无数据", "无数据");
            }
            var result = new List<QueryCustomerRegDto>();
            foreach (var input in inputDatas)
            {
                #region  遍历体检人 开始

                #region 条件判断 开始
                if (input.RegisterState == (int)RegisterState.Yes)
                {
                    if (input.CheckSate == (int)ProjectIState.Not || !input.CheckSate.HasValue)
                        input.LoginDate = DateTime.Now;
                }
                var reData =
                    _customerRegRepository.FirstOrDefault(o =>
                        o.CustomerBM == input.CustomerBM && o.Id != input.Id); //判断登记信息中体检号是否重复
                if (reData != null)
                {
                    throw new FieldVerifyException("体检号重复", "体检号重复，请修改后登记。");
                }

                if (input.RegisterState == (int)RegisterState.Yes)
                {
                    if (input.CustomerItemGroup == null)
                    {
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                    }

                    if (input.CustomerItemGroup.Count == 0)
                    {
                        throw new FieldVerifyException("不能进行登记", "请先添加组合项目后再确认登记");
                    }
                }
                #endregion 条件判断 结束


                #region 添加或更新客户信息 开始
                var ret = new TjlCustomerReg();
                var retCus = new TjlCustomer();
                var oldgroplist = new List<TjlCustomerItemGroup>();
                var customer = input.Customer;
                var oldcus = _customerRepository.FirstOrDefault(o => o.ArchivesNum == customer.ArchivesNum);
                if (oldcus != null)
                {
                    customer.Id = oldcus.Id;
                }
                if (customer.Id == Guid.Empty)
                {
                    var customerEntity = customer.MapTo<TjlCustomer>();
                    customerEntity.Id = Guid.NewGuid();
                    retCus = _customerRepository.Insert(customerEntity);
                }
                else
                {
                    var oldCustomer = input.Customer;
                    var customerEntity = _customerRepository.Get(customer.Id);
                    retCus = _customerRepository.Update(oldCustomer.MapTo(customerEntity));
                }
                #endregion 添加或更新客户信息 结束

                input.Customer = null;

                #region 修改客户登记信息 开始
                //先根据体检号码查询一下库里是否有该体检号信息
                var reg = _customerRegRepository.FirstOrDefault(o => o.Id == input.Id);
                if (reg != null)
                {
                    if (reg.CustomerBM != reg.CustomerBM) //根据当前Id查询出登记信息，
                    {
                        input.Id = Guid.Empty;
                    }
                }

                if (input.Id == Guid.Empty)
                {
                    var data = input.MapTo<TjlCustomerReg>();
                    data.CheckSate = (int)PhysicalEState.Not;
                    data.Id = Guid.NewGuid();
                    data.Customer = retCus;
                    if (input.ClientTeamInfo_Id.HasValue)
                    {
                        //新增是直接根据分组信息增加该字段
                        data.ClientTeamInfo = _clientTeamInfoRepository.Get(input.ClientTeamInfo_Id.Value);
                        data.ItemSuitBMId = data.ClientTeamInfo.ItemSuit_Id;
                        data.ItemSuitName = data.ClientTeamInfo.ItemSuitName;
                        if (data.ClientTeamInfo.ClientReg != null)
                        {
                            data.ClientInfo = data.ClientTeamInfo.ClientReg.ClientInfo;
                            data.ClientType = data.ClientTeamInfo.ClientReg.ClientSate.ToString();
                            if (data.ClientInfo != null)
                            {
                                data.ClientInfoId = data.ClientInfo.Id;
                            }
                        }
                    }
                    else
                    {
                        if (input.ClientRegId.HasValue)
                        {
                            data.ClientReg = _clientRegRepository.Get(input.ClientRegId.Value);
                            if (data.ClientReg != null)
                            {
                                data.ClientRegId = data.ClientRegId;
                                data.ClientInfo = data.ClientInfo;
                                if (data.ClientInfo != null)
                                {
                                    data.ClientInfoId = data.ClientInfo.Id;
                                    data.ClientType = data.ClientInfo.ClientSate;
                                }
                                else
                                    data.ClientType = ((int)ClientSate.Normal).ToString();
                            }
                        }
                    }
                    data.CustomerItemGroup = null;
                    data = _customerRegRepository.Insert(data);
                    ret = data;
                }
                else
                {
                    var regEntity = _customerRegRepository.Get(input.Id);
                    oldgroplist = regEntity.CustomerItemGroup?.ToList();
                    //input.CostState = regEntity.CostState;
                    if (regEntity.RequestState.HasValue)
                    {
                        input.RequestState = regEntity.RequestState;
                    }

                    if (regEntity.ReviewSate.HasValue)
                    {
                        input.ReviewSate = regEntity.ReviewSate;
                    }

                    if (regEntity.SendToConfirm.HasValue)
                    {
                        input.SendToConfirm = regEntity.SendToConfirm;
                    }

                    if (regEntity.SummLocked.HasValue)
                    {
                        input.SummLocked = regEntity.SummLocked;
                    }

                    if (regEntity.SummSate.HasValue)
                    {
                        input.SummSate = regEntity.SummSate;
                    }

                    if (regEntity.CheckSate.HasValue)
                    {
                        input.CheckSate = regEntity.CheckSate;
                    }

                    if (regEntity.BarState.HasValue)
                    {
                        input.BarState = regEntity.BarState;
                    }

                    var retReg = input.MapTo(regEntity);
                    retReg.Customer = retCus;
                    retReg.CustomerId = retCus.Id;
                    if (retReg.ItemSuitBMId.HasValue)
                    {
                        retReg.ItemSuitBM = _itemSuitRepository.Get(regEntity.ItemSuitBMId.Value);
                    }

                    //修改需要需要根据参数修改是否还有分组信息
                    if (input.ClientTeamInfo_Id.HasValue)
                    {
                        retReg.ClientTeamInfo = _clientTeamInfoRepository.Get(input.ClientTeamInfo_Id.Value);
                        retReg.ItemSuitBMId = retReg.ClientTeamInfo.ItemSuit_Id;
                        retReg.ItemSuitName = retReg.ClientTeamInfo.ItemSuitName;
                        ret.ItemSuitBM = retReg.ClientTeamInfo.ItemSuit;
                        retReg.ClientTeamInfoId = input.ClientTeamInfo_Id;
                        retReg.ClientReg = retReg.ClientTeamInfo.ClientReg;
                        retReg.ClientRegId = retReg.ClientReg.Id;
                        if (retReg.ClientTeamInfo.ClientReg != null)
                        {
                            retReg.ClientInfo = retReg.ClientTeamInfo.ClientReg.ClientInfo;
                            if (retReg.ClientInfo != null)
                            {
                                retReg.ClientInfoId = retReg.ClientInfo.Id;
                                retReg.ClientType = retReg.ClientReg.ClientSate.ToString();
                            }
                            else
                                retReg.ClientType = ((int)ClientSate.Normal).ToString();
                        }
                    }
                    else
                    {
                        retReg.ClientInfo = null;
                        retReg.ClientInfoId = null;
                        retReg.ClientTeamInfo = null;
                        retReg.ClientReg = null;
                        retReg.ClientRegId = null;
                        if (input.ClientRegId.HasValue)
                        {
                            var clientreg = _clientRegRepository.Get(input.ClientRegId.Value);
                            if (clientreg != null)
                            {
                                retReg.ClientRegId = clientreg.Id;
                                retReg.ClientReg = clientreg;
                                retReg.ClientInfo = clientreg.ClientInfo;
                                if (clientreg.ClientInfo != null)
                                {
                                    retReg.ClientInfoId = clientreg.ClientInfo.Id;
                                    retReg.ClientType = clientreg.ClientSate.ToString();
                                }
                                else
                                    retReg.ClientType = ((int)ClientSate.Normal).ToString();
                            }
                        }
                    }

                    retReg.CustomerItemGroup = null;
                    regEntity = _customerRegRepository.Update(retReg);
                    ret = regEntity;
                }
                #endregion 再修改客户登记信息 结束



                #region 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 开始
                if (oldgroplist.Count > 0)
                {
                    foreach (var old in oldgroplist)
                        if (!input.CustomerItemGroup.Any(o => o.Id == old.Id))
                        {
                            var olditems = old.CustomerRegItem?.ToList();
                            if (olditems != null)
                            {
                                foreach (var item in olditems)
                                    _customerRegItemRepository.Delete(item);
                            }

                            _customerItemGroupRepository.Delete(old);
                        }
                }
                #endregion 查询当前已有的分组，修改登记信息这个方法只能切换套餐，切换则将套餐下组合项目都删除 结束

                #region 组合处理
                string sql = "";
                if (input.CustomerItemGroup != null)
                {


                }
                #endregion 组合处理
                #endregion  遍历体检人 结束

                result.Add(ret.MapTo<QueryCustomerRegDto>());

            }
            //DateTime dateTime1 = DateTime.Now;

            return result;
        }

        /// <summary>
        /// 取消登记
        /// </summary>
        public void CancelReg(CustomerRegViewDto dto)
        {
            var reg = _customerRegRepository.Get(dto.Id);
            if (reg != null)
            {
                reg.RegisterState = (int)RegisterState.No;
                reg.rgst_no = "";                 
                reg.LoginDate = null;
                _customerRegRepository.Update(reg);
            }
        }

        /// <summary>
        /// 登记窗体展示 查询登记数
        /// </summary>
        public ViewQueryRegNumbersDto QueryRegNumbers()
        {
            var result = new ViewQueryRegNumbersDto() { datas = new List<RegNumberData>() };
            var start = DateTime.Now.Date.ToShortDateString();//new DateTime(2018, 12, 27);//
            var end = DateTime.Now.Date.AddDays(1).ToShortDateString();

            string strsql = "select ISNULL( ClientName,'个人')ClientName,SUM(1)Sum, sum(case Sex WHEN 2 THEN 1 ELSE 0 END ) Famale, " +
         " sum(case Sex WHEN 1 THEN 1 ELSE 0 END )  Male from TjlCustomerRegs " +
        "left join TjlClientRegs on TjlCustomerRegs.ClientRegId = TjlClientRegs.Id " +
        "left join TjlClientInfoes on TjlClientRegs.ClientInfoId = TjlClientInfoes.Id " +
        "inner join TjlCustomers on TjlCustomerRegs.CustomerId = TjlCustomers.Id " +
        "where RegisterState =@RegisterState and LoginDate> @LoginDateStart and LoginDate<@LoginDateEnd GROUP BY  ISNULL( ClientName,'个人')";
            SqlParameter[] parameters = {
                        new SqlParameter("@RegisterState",(int)RegisterState.Yes),
                        new SqlParameter("@LoginDateStart",start),
                        new SqlParameter("@LoginDateEnd",end),
                        };

            List<RegNumberData> lstregNumberDatas = _sqlExecutor.SqlQuery<RegNumberData>
                (strsql, parameters).ToList();
            result.datas = lstregNumberDatas;
            int isumreg = 0;
            foreach (var item in lstregNumberDatas)
            {
                isumreg += item.Sum;
            }
            result.SumReg = isumreg;
            //var rows = _customerRegRepository.GetAll().
            //    Where(o => o.RegisterState == (int)RegisterState.Yes && o.LoginDate >= start && o.LoginDate < end).Select(o => new { o.ClientReg, o.ClientInfo, o.Customer.Sex }).ToList();
            //result.SumReg = rows.Count();
            //var geren = rows.Where(o => o.ClientReg == null);
            //result.datas.Add(new RegNumberData()
            //{
            //    ClientName = "个人",
            //    Sum = geren.Count(),
            //    Famale = geren.Where(o => o.Sex == (int)Sex.Woman).Count(),
            //    Male = geren.Where(o => o.Sex == (int)Sex.Man).Count(),
            //    //NoSex = geren.Where(o => o.Sex == (int)Sex.GenderNotSpecified).Count()
            //});
            //foreach (var client in rows.Where(o => o.ClientReg != null).GroupBy(o => o.ClientReg.Id).Select(o => o.First()))
            //{
            //    var clientRows = rows.Where(o => o.ClientReg != null && o.ClientReg.Id == client.ClientReg.Id);
            //    if (clientRows.Count() > 0)
            //    {
            //        result.datas.Add(new RegNumberData()
            //        {
            //            ClientName = clientRows.First().ClientReg.ClientInfo.ClientName,
            //            Sum = clientRows.Count(),
            //            Famale = clientRows.Where(o => o.Sex == (int)Sex.Woman).Count(),
            //            Male = clientRows.Where(o => o.Sex == (int)Sex.Man).Count(),
            //            //NoSex = clientRows.Where(o => o.Sex == (int)Sex.Unknown).Count()
            //        });
            //    }
            //}
            return result;

            //var datas = _customerRegRepository.GetAll()
            //    .Where(o => (o.RegisterState == (int)RegisterState.No || !o.RegisterState.HasValue) &&
            //                o.BookingDate >= start && o.BookingDate < end).Select(o => new { o.Customer.Sex }).ToList();
            //result.NotReg = datas.Count();
            //result.MaleNotReg = datas.Count(o => o.Sex == (int)Sex.Man);
            //result.FemaleNotReg = datas.Count(o => o.Sex == (int)Sex.Woman);

            //var rows = _customerRegRepository.GetAll().Where(o =>
            //        o.RegisterState == (int)RegisterState.Yes && o.LoginDate.Value >= start && o.LoginDate < end)
            //    .AsNoTracking();
            //rows = rows.Include(o => o.Customer);
            //var list = rows.ToList();
            //var regs = rows.Select(g => new { g.CheckSate, g.Customer.Sex, g.SummSate }).ToList();
            //if (regs != null)
            //{
            //    result.SumReg = regs.Count();
            //    result.MaleReg = regs.Count(o => o.Sex == (int)Sex.Man);
            //    result.FemaleReg = regs.Count(o => o.Sex == (int)Sex.Woman);
            //    result.Tijianzhong = regs.Count(o => o.CheckSate == (int)PhysicalEState.Process);
            //    result.TijianzhongMale = regs.Count(o =>
            //        o.CheckSate == (int)(int)PhysicalEState.Process && o.Sex == (int)Sex.Man);
            //    result.TijianzhongFemale = regs.Count(o =>
            //        o.CheckSate == (int)(int)PhysicalEState.Process && o.Sex == (int)Sex.Woman);
            //    result.WanchengTijian = regs.Count(o => o.CheckSate == (int)PhysicalEState.Complete);
            //    result.WanchengTijianMale = regs.Count(o =>
            //        o.CheckSate == (int)PhysicalEState.Complete && o.Sex == (int)Sex.Man);
            //    result.WanchengTijianFemale = regs.Count(o =>
            //        o.CheckSate == (int)PhysicalEState.Complete && o.Sex == (int)Sex.Woman);
            //    result.Yizongjian = regs.Count(o => o.SummSate != (int)SummSate.NotAlwaysCheck && o.SummSate.HasValue);
            //    result.YizongjianMale = regs.Count(o =>
            //        o.SummSate != (int)SummSate.NotAlwaysCheck && o.SummSate.HasValue && o.Sex == (int)Sex.Man);
            //    result.YizongjianFemale = regs.Count(o =>
            //          o.SummSate != (int)SummSate.NotAlwaysCheck && o.SummSate.HasValue && o.Sex == (int)Sex.Woman);
            //    result.Yishenhe =
            //        regs.Count(o => o.SummSate == (int)SummSate.Audited);
            //    result.YishenheMale = regs.Count(o =>
            //         o.SummSate == (int)SummSate.Audited && o.Sex == (int)Sex.Man);
            //    result.YishenheFemale = regs.Count(o =>
            //        o.SummSate == (int)SummSate.Audited && o.Sex == (int)Sex.Woman);
            //}

            //return result;
        }

        public List<CountDto> getupdate(ChargeBM input)
        {
            ChargeBM chargeBM = new ChargeBM();
            string sql = @"select count( a.Id) as ct from TjlCustomerItemGroups a inner join TbmDepartments b on a.DepartmentId=b.Id where b.Duty!='处置项目' 
and a.IsDeleted=0 and a.IsAddMinus !=3 and CustomerRegBMId='" + input.Id + "' and ISNULL(ApplicationNo,'')=''";

            var dtcloseSY = _sqlExecutor.SqlQuery<CountDto>(sql).ToList();
            return dtcloseSY;

        }

        public List<CustomerRegViewDto> getRepeatCus(ChargeBM input)
        {
            var cusinfo = _customerRegRepository.Get(input.Id);
            var star =DateTime.Parse( System.DateTime.Now.ToShortDateString());
            var end = DateTime.Parse(System.DateTime.Now.AddDays(1).ToShortDateString());
            var query = _customerRegRepository.GetAll().Where(p => p.LoginDate!=null &&
            p.LoginDate >= star
             && p.LoginDate < end && p.Customer.Name == cusinfo.Customer.Name &&
             p.Customer.Sex == cusinfo.Customer.Sex && p.CustomerBM != cusinfo.CustomerBM);
            var reglist= query.MapTo<List<CustomerRegViewDto>>();
            return reglist;

        }
        /// <summary>
        /// 修改登记信息的状态
        /// </summary>
        public void EditRegInfoState(EditCustomerRegStatesDto dto)
        {
            var regInfo = _customerRegRepository.Get(dto.CustomerRegId);
            if (regInfo != null)
            {
                if (dto.GuidanceSate)
                {
                    regInfo.GuidanceSate = dto.StateValue;
                    foreach (var group in regInfo.CustomerItemGroup)
                        group.GuidanceSate = dto.StateValue;
                }

                _customerRegRepository.Update(regInfo);
            }
        }

        public CustomerRegEssentialInfoViewDto GetCustomerRegEssentialInfoViewDto(CusNameInput input)
        {
            var query = _customerRegRepository.FirstOrDefault(m => m.Id == input.Id);
            return query.MapTo<CustomerRegEssentialInfoViewDto>();
        }

        public CustomerRegViewDto GetCustomerRegViewDto(CusNameInput input)
        {
            var query = _customerRegRepository.GetAll().Where(m => m.CustomerBM == input.Theme).FirstOrDefault();
            return query.MapTo<CustomerRegViewDto>();
        }

        /// <summary>
        /// 获取体检人检查组合基本信息
        /// </summary>
        /// <param name="cusNameInput"></param>
        /// <returns></returns>
        public List<CustomerItemGroupBarItemDto> GetLstCustomerItemGroupBarItemDto(CusNameInput cusNameInput)
        {
            var lstCustomerItemGroupBarItemDto =
                _customerItemGroupRepository.GetAllList(p => p.CustomerRegBMId == cusNameInput.Id && p.IsAddMinus != (int)AddMinusType.Minus);
            return lstCustomerItemGroupBarItemDto.MapTo<List<CustomerItemGroupBarItemDto>>();
        }
         
        public List<CustomerRegRosterDto> GetAll(QueryCustomerRegDto input)
        {
            var TjlCustomerRegList = new List<TjlCustomerReg>();
            var query = _customerRegRepository.GetAll();
            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Id);
                }
                else
                {
                    //体检号
                    if (!string.IsNullOrEmpty(input.CustomerBM))
                    {
                        query = query.Where(m => m.CustomerBM == input.CustomerBM);
                    }

                    //体检人姓名
                    if (!string.IsNullOrEmpty(input.Customer.Name))
                    {
                        query = query.Where(m => m.Customer.Name.Contains(input.Customer.Name));
                    }
                    //检查类型
                    if (!string.IsNullOrEmpty(input.PostState))
                    {
                        query = query.Where(m => m.PostState== input.PostState);
                    }
                    //工种
                    if (!string.IsNullOrEmpty(input.TypeWork))
                    {
                        query = query.Where(m => m.TypeWork == input.TypeWork);
                    }

                    //性别
                    if (input.Customer.Sex != null)
                    {
                        query = query.Where(m => m.Customer.Sex == input.Customer.Sex);
                    }
                    //组合
                    if (input.GroupID.HasValue)
                    {
                        query = query.Where(m => m.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == input.GroupID));
                    }
                    //介绍人
                    if (input.Introducer != null)
                    {
                        query = query.Where(m => m.Introducer.Contains(input.Introducer) || m.ClientReg.linkMan.Contains(input.Introducer));
                    }
                   
                    //单位
                    if (input.ClientRegId != null)
                    {
                        if (input.ClientRegId != Guid.Empty)
                        {
                            query = query.Where(o => o.ClientTeamInfo.ClientReg.Id == input.ClientRegId);
                        }
                    }

                    if (input.CompanysT != null)
                    {
                        if (input.CompanysT.Count != 0)
                        {
                            query = query.Where(m => input.CompanysT.Contains(m.ClientRegId.Value));
                        }
                    }

                    //套餐名称
                    if (!string.IsNullOrWhiteSpace(input.ItemSuitName))
                    {
                        query = query.Where(m => m.ItemSuitName == input.ItemSuitName);
                    }

                    //联系电话/固定电话
                    if (!string.IsNullOrWhiteSpace(input.Customer.Mobile))
                    {
                        query = query.Where(m =>
                            m.Customer.Mobile == input.Customer.Mobile ||
                            m.Customer.Telephone == input.Customer.Mobile);
                    }

                    //身份证号
                    if (!string.IsNullOrEmpty(input.Customer.IDCardNo))
                    {
                        query = query.Where(m => m.Customer.IDCardNo == input.Customer.IDCardNo);
                    }

                    //团体/个人
                    if (!string.IsNullOrEmpty(input.Customer.personalOrGroup))
                    {
                        if (input.Customer.personalOrGroup == "0")
                        {
                            query = query.Where(m => m.ClientReg != null);
                        }
                        if (input.Customer.personalOrGroup == "1")
                        {
                            query = query.Where(m => m.ClientReg == null);
                        }
                    }

                    //预约日期
                    if (input.LoginDateStartTime != null && input.LoginDateEndTime != null)
                    {
                        input.LoginDateStartTime = new DateTime(input.LoginDateStartTime.Value.Year,
                            input.LoginDateStartTime.Value.Month, input.LoginDateStartTime.Value.Day, 00,
                            00, 00);
                        input.LoginDateEndTime = new DateTime(input.LoginDateEndTime.Value.Year,
                            input.LoginDateEndTime.Value.Month, input.LoginDateEndTime.Value.Day, 23, 59,
                            59);
                        //query = query.Where(o =>
                        //    o.CreationTime >= Input.LoginDateStartTime &&
                        //    o.CreationTime < Input.LoginDateEndTime);
                        query = query.Where(o =>
                            o.BookingDate >= input.LoginDateStartTime &&
                            o.BookingDate < input.LoginDateEndTime);
                        var liso = query.ToList();
                    }

                    //登记日期
                    if (input.BookingDateStartTime != null && input.BookingDateEndTime != null)
                    {
                        input.BookingDateStartTime = new DateTime(input.BookingDateStartTime.Value.Year,
                            input.BookingDateStartTime.Value.Month, input.BookingDateStartTime.Value.Day, 00,
                            00, 00);
                        input.BookingDateEndTime = new DateTime(input.BookingDateEndTime.Value.Year,
                            input.BookingDateEndTime.Value.Month, input.BookingDateEndTime.Value.Day, 23, 59,
                            59);
                        query = query.Where(o =>
                              o.LoginDate >= input.BookingDateStartTime &&
                              o.LoginDate < input.BookingDateEndTime);
                        var list = query.ToList();
                    }
                    //交表日期
                    if (input.SendDateStartTime != null && input.SendDateEndTime != null)
                    {
                        input.SendDateStartTime = new DateTime(input.SendDateStartTime.Value.Year,
                            input.SendDateStartTime.Value.Month, input.SendDateStartTime.Value.Day, 00,
                            00, 00);
                        input.SendDateEndTime = new DateTime(input.SendDateEndTime.Value.Year,
                            input.SendDateEndTime.Value.Month, input.SendDateEndTime.Value.Day, 23, 59,
                            59);
                        query = query.Where(o =>
                              o.SendToConfirmDate >= input.SendDateStartTime &&
                              o.SendToConfirmDate < input.SendDateEndTime);
                        var list = query.ToList();
                    }
                    if (input.RegisterState != null)
                    {
                        query = query.Where(o => o.RegisterState == input.RegisterState);
                    }
                    if (input.SendToConfrim != null)
                    {
                        if (input.SendToConfrim == (int)SendToConfirm.No)
                        {
                            query = query.Where(a => a.SendToConfirm == input.SendToConfrim || !a.SendToConfirm.HasValue);
                        }
                        else
                            query = query.Where(a => a.SendToConfirm == input.SendToConfrim);
                    }

                    //if (input.BookingDate != DBNull.Value)
                    //    query = query.Where(m => m.Notice.Contains(input.ItemGroup.Notice));
                    //检查状态
                    if (input.CheckSate != null)
                    {
                        if (input.CheckSate != 0)
                        {
                            query = query.Where(m => m.CheckSate == input.CheckSate);
                        }
                    }
                    //年龄
                    if (input.AgeStart != null && input.AgeEnd != null)
                    {
                        if (input.AgeStart != 0 && input.AgeEnd != 0)
                        {
                            query = query.Where(o =>
                                o.Customer.Age >= input.AgeStart && o.Customer.Age <= input.AgeEnd);
                        }

                        if (input.AgeStart != 0 && input.AgeEnd == 0)
                        {
                            query = query.Where(o => o.Customer.Age >= input.AgeStart);
                        }

                        if (input.AgeStart == 0 && input.AgeEnd != 0)
                        {
                            query = query.Where(o => o.Customer.Age <= input.AgeEnd);
                        }
                    }

                    if (input.PhysicalType != null)
                        query = query.Where(q => q.PhysicalType == input.PhysicalType);

                    //体检类别
                    if (!string.IsNullOrWhiteSpace(input.ClientType))
                    {
                        query = query.Where(m => m.ClientType == input.ClientType);
                    }

                    //是否免费
                    //if (Input.IsFree != null)
                    //{
                    //    if (Input.IsFree == false)
                    //    {
                    //        query = query.Where(m => m.IsFree != true);
                    //    }
                    //}
                    //if (Input.PersonnelCategoryId.HasValue)
                    //{
                    //    query = query.Where(o => o.PersonnelCategoryId == Input.PersonnelCategoryId);
                    //}
                    //部门


                    //套餐
                    if (input.SetMealChoiceT != null)
                    {
                        if (input.SetMealChoiceT.Count != 0)
                        {
                            query = query.Where(m => input.SetMealChoiceT.Contains(m.ItemSuitBMId.Value));
                        }
                    }

                    if (input.PersonnelCategoryIdL != null && input.PersonnelCategoryIdL.Count != 0)
                    {
                        query = query.Where(m => input.PersonnelCategoryIdL.Contains(m.PersonnelCategoryId.Value));
                    }


                }
                query = query.OrderByDescending(o => o.BookingDate);
            }
            return query.MapTo<List<CustomerRegRosterDto>>();
        }

        public int UpdateTimes(GuideUpdateCustomerRegDto input)
        {
            //input.Id = Guid.Parse("88beb0D0-661E-42BB-95C1-0021144D2184");
            //input.TjlClientReg_Id = Guid.Parse("5b850eb7-549b-4f40-91b6-7ff5e2fa6bc8");
            var inum = 1;
            var dept = _customerRegRepository.FirstOrDefault(o => o.Id == input.Id);
            //团体预约
            if (input.ClientRegId.HasValue && input.ClientRegId != Guid.Empty)
            {
                if (dept.GuidanceNum == null)
                {
                    var clientcus = _customerRegRepository.GetAll().Where(o => o.ClientRegId == input.ClientRegId)
                                        .Max(o => o.GuidanceNum) ?? 0;
                    dept.GuidanceNum = clientcus + 1;
                }
                else
                {
                    dept.GuidanceNum = dept.GuidanceNum;
                }

                inum = dept.GuidanceNum.Value;
                dept.GuidanceSate = (int)PrintSate.Print;
                _customerRegRepository.Update(dept);
                //更新组合打印状态
                var GuidSate= (int)PrintSate.Print;
                _customerItemGroupRepository.GetAll().Where(p => p.CustomerRegBMId == input.Id).Update(p=> new TjlCustomerItemGroup
                {
                     GuidanceSate= GuidSate
                });

                //foreach (var item in dept.CustomerItemGroup)
                //{
                //    item.GuidanceSate = (int)PrintSate.Print;

                //    _customerItemGroupRepository.Update(item);
                //}
            }
            else
            {
                if (dept.GuidanceNum == null)
                {
                    dept.GuidanceNum = 1;
                }
                else
                {
                    var datestart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
                    var dateend = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                    var depts = _customerRegRepository.GetAll()
                                    .Where(o => o.CustomerId == null && o.CreationTime <= dateend &&
                                                o.CreationTime >= datestart).Max(o => o.GuidancePrintNum) ?? 0;

                    dept.GuidancePrintNum = depts + 1;
                }

                inum = dept.GuidancePrintNum ?? 1;
                dept.GuidanceSate = (int)PrintSate.Print;
                _customerRegRepository.Update(dept);
                //更新组合打印状态
                var GuidSate = (int)PrintSate.Print;
                _customerItemGroupRepository.GetAll().Where(p => p.CustomerRegBMId == input.Id).Update(p => new TjlCustomerItemGroup
                {
                    GuidanceSate = GuidSate
                });
                //foreach (var item in dept.CustomerItemGroup)
                //{
                //    item.GuidanceSate = (int)PrintSate.Print;

                //    _customerItemGroupRepository.Update(item);
                //}
            }

            return inum;
        }

        /// <summary>
        /// 组合下用户数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetNumber(EntityDto<Guid> input)
        {
            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    var query = _customerRegRepository.GetAll();
                    query = query.Where(m => m.ClientTeamInfoId == input.Id);
                    return query.Count();
                }

                return 0;
            }

            return 0;
        }

        public AbpUsersDto abpUsersDto()
        {
            var query = _userRepository.GetAll();
            return query.MapTo<AbpUsersDto>();
        }

        private PageResultDto<CustomerRegRosterDto> BuildQuery(PageInputDto<QueryCustomerRegDto> input)
        {
            var TjlCustomerRegList = new List<TjlCustomerReg>();
            var query = _customerRegRepository.GetAll();          
            if (input.Input != null)
            {
               
                if (input.Input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Input.Id);
                }
                else
                {
                    //体检号
                    if (!string.IsNullOrEmpty(input.Input.CustomerBM))
                    {
                        query = query.Where(m => m.CustomerBM == input.Input.CustomerBM);
                    }
                    //体检号
                    if (input.Input.LoginUserId.HasValue)
                    {
                        query = query.Where(m => m.CreatorUserId == input.Input.LoginUserId);
                    }
                    //单位分组信息
                    if (input.Input.ClientTeamInfoId != null)
                    {
                        query = query.Where(m => m.ClientTeamInfoId == input.Input.ClientTeamInfoId);
                    }

                    //体检人姓名
                    if (!string.IsNullOrEmpty(input.Input.Customer.Name))
                    {
                        query = query.Where(m => m.Customer.Name.Contains(input.Input.Customer.Name));
                    }
                    //开票名称
                    if (!string.IsNullOrEmpty(input.Input.FPNo))
                    {
                        query = query.Where(m => m.FPNo.Contains(input.Input.FPNo));
                    }
                    //检查类型
                    if (!string.IsNullOrEmpty(input.Input.PostState))
                    {
                        query = query.Where(m => m.PostState == input.Input.PostState);
                    }
                    //工种
                    if (!string.IsNullOrEmpty(input.Input.TypeWork))
                    {
                        query = query.Where(m => m.TypeWork == input.Input.TypeWork);
                    }
                    //性别
                    if (input.Input.Customer.Sex != null)
                    {
                        query = query.Where(m => m.Customer.Sex == input.Input.Customer.Sex);
                    }
                    //介绍人
                    if (input.Input.Introducer != null)
                    {
                        query = query.Where(m => m.Introducer.Contains(input.Input.Introducer) || m.ClientReg.linkMan.Contains(input.Input.Introducer));
                    }
                    if (input.Input.GroupID.HasValue)
                    {
                        query = query.Where(m => m.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == input.Input.GroupID));
                    }
                    //单位
                    if (input.Input.ClientRegId != null)
                    {
                        if (input.Input.ClientRegId != Guid.Empty)
                        {
                            query = query.Where(o => o.ClientReg.Id == input.Input.ClientRegId);
                        }
                    }

                    if (input.Input.CompanysT != null)
                    {
                        if (input.Input.CompanysT.Count != 0)
                        {
                            foreach (var StrId in input.Input.CompanysT)
                                TjlCustomerRegList.AddRange(query.Where(o => o.ClientTeamInfo.ClientReg.Id == StrId)
                                    .ToList());
                            return Screen(TjlCustomerRegList, input);
                        }
                    }

                    //套餐名称
                    if (!string.IsNullOrWhiteSpace(input.Input.ItemSuitName))
                    {
                        query = query.Where(m => m.ItemSuitName == input.Input.ItemSuitName);
                    }

                    //联系电话/固定电话
                    if (!string.IsNullOrWhiteSpace(input.Input.Customer.Mobile))
                    {
                        query = query.Where(m =>
                            m.Customer.Mobile == input.Input.Customer.Mobile ||
                            m.Customer.Telephone == input.Input.Customer.Mobile);
                    }

                    //身份证号
                    if (!string.IsNullOrEmpty(input.Input.Customer.IDCardNo))
                    {
                        query = query.Where(m => m.Customer.IDCardNo == input.Input.Customer.IDCardNo);
                    }

                    //团体/个人
                    if (!string.IsNullOrEmpty(input.Input.Customer.personalOrGroup))
                    {
                        if (input.Input.Customer.personalOrGroup == "0")
                        {
                            query = query.Where(m => m.ClientReg != null);
                        }
                        if (input.Input.Customer.personalOrGroup == "1")
                        {
                            query = query.Where(m => m.ClientReg == null);
                        }
                    }

                    //预约日期
                    if (input.Input.LoginDateStartTime != null && input.Input.LoginDateEndTime != null)
                    {
                        if (input.Input.Remarks != "时分秒")
                        {
                            input.Input.LoginDateStartTime = new DateTime(input.Input.LoginDateStartTime.Value.Year,
                            input.Input.LoginDateStartTime.Value.Month, input.Input.LoginDateStartTime.Value.Day, 00,
                            00, 00);
                        input.Input.LoginDateEndTime = new DateTime(input.Input.LoginDateEndTime.Value.Year,
                            input.Input.LoginDateEndTime.Value.Month, input.Input.LoginDateEndTime.Value.Day, 23, 59,
                            59);
                        }
                        //query = query.Where(o =>
                        //    o.CreationTime >= input.Input.LoginDateStartTime &&
                        //    o.CreationTime < input.Input.LoginDateEndTime);
                        query = query.Where(o =>
                            o.BookingDate >= input.Input.LoginDateStartTime &&
                            o.BookingDate < input.Input.LoginDateEndTime);
                        var liso = query.ToList();
                       
                    }

                    //登记日期
                    if (input.Input.BookingDateStartTime != null && input.Input.BookingDateEndTime != null)
                    {
                        if (input.Input.Remarks != "时分秒")
                        {
                            input.Input.BookingDateStartTime = new DateTime(input.Input.BookingDateStartTime.Value.Year,
                                input.Input.BookingDateStartTime.Value.Month, input.Input.BookingDateStartTime.Value.Day, 00,
                                00, 00);
                            input.Input.BookingDateEndTime = new DateTime(input.Input.BookingDateEndTime.Value.Year,
                                input.Input.BookingDateEndTime.Value.Month, input.Input.BookingDateEndTime.Value.Day, 23, 59,
                                59);
                        }
                       
                        query = query.Where(o =>
                              o.LoginDate >= input.Input.BookingDateStartTime &&
                              o.LoginDate < input.Input.BookingDateEndTime);
                        var list = query.ToList();
                    }
                    //交表日期
                    if (input.Input.SendDateStartTime != null && input.Input.SendDateEndTime != null)
                    {
                        input.Input.SendDateStartTime = new DateTime(input.Input.SendDateStartTime.Value.Year,
                            input.Input.SendDateStartTime.Value.Month, input.Input.SendDateStartTime.Value.Day, 00,
                            00, 00);
                        input.Input.SendDateEndTime = new DateTime(input.Input.SendDateEndTime.Value.Year,
                            input.Input.SendDateEndTime.Value.Month, input.Input.SendDateEndTime.Value.Day, 23, 59,
                            59);
                        query = query.Where(o =>
                              o.SendToConfirmDate >= input.Input.SendDateStartTime &&
                              o.SendToConfirmDate < input.Input.SendDateEndTime);
                        var list = query.ToList();
                    }
                    if (input.Input.RegisterState != null)
                    {
                        query = query.Where(o => o.RegisterState == input.Input.RegisterState);
                    }
                    if (input.Input.SendToConfrim != null)
                    {
                        if (input.Input.SendToConfrim == (int)SendToConfirm.No)
                        {
                            query = query.Where(a => a.SendToConfirm == input.Input.SendToConfrim || !a.SendToConfirm.HasValue);
                        }
                        else
                            query = query.Where(a => a.SendToConfirm == input.Input.SendToConfrim);
                    }

                    //if (input.BookingDate != DBNull.Value)
                    //    query = query.Where(m => m.Notice.Contains(input.ItemGroup.Notice));
                    //检查状态
                    if (input.Input.CheckSate != null)
                    {
                        if (input.Input.CheckSate != 0)
                        {
                            query = query.Where(m => m.CheckSate == input.Input.CheckSate);
                        }
                    }
                    //年龄
                    if (input.Input.AgeStart != null && input.Input.AgeEnd != null)
                    {
                        if (input.Input.AgeStart != 0 && input.Input.AgeEnd != 0)
                        {
                            query = query.Where(o =>
                                o.Customer.Age >= input.Input.AgeStart && o.RegAge <= input.Input.AgeEnd);
                        }

                        if (input.Input.AgeStart != 0 && input.Input.AgeEnd == 0)
                        {
                            query = query.Where(o => o.Customer.Age >= input.Input.AgeStart);
                        }

                        if (input.Input.AgeStart == 0 && input.Input.AgeEnd != 0)
                        {
                            query = query.Where(o => o.Customer.Age <= input.Input.AgeEnd);
                        }
                    }

                    if (input.Input.PhysicalType != null)
                        query = query.Where(q => q.PhysicalType == input.Input.PhysicalType);

                    //体检类别
                    if (!string.IsNullOrWhiteSpace(input.Input.ClientType))
                    {
                        query = query.Where(m => m.ClientType == input.Input.ClientType);
                    }
                    
                    //是否免费
                    //if (input.Input.IsFree != null)
                    //{
                    //    if (input.Input.IsFree == false)
                    //    {
                    //        query = query.Where(m => m.IsFree != true);
                    //    }
                    //}
                    //if (input.Input.PersonnelCategoryId.HasValue)
                    //{
                    //    query = query.Where(o => o.PersonnelCategoryId == input.Input.PersonnelCategoryId);
                    //}

                    //套餐
                    if (input.Input.SetMealChoiceT != null)
                    {
                        if (input.Input.SetMealChoiceT.Count != 0)
                        {
                            //foreach (var StrId in input.Input.SetMealChoiceT)
                            //    TjlCustomerRegList.AddRange(query.Where(o => o.ItemSuitBM.Id == StrId).ToList());
                            //return Screen(TjlCustomerRegList, input);
                            query = query.Where(o => input.Input.SetMealChoiceT.Contains(o.ItemSuitBMId.Value));
                        }
                    }

                    var userBM = _userRepository.Get(AbpSession.UserId.Value);
                    if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
                    {
                        query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                        || p.HospitalArea == null);
                    }

                    //部门
                    query = query.OrderByDescending(o => o.BookingDate);
                    


                    if (input.Input.PersonnelCategoryIdL != null && input.Input.PersonnelCategoryIdL.Count != 0)
                    {
                        foreach (var PersonnelCategoryId in input.Input.PersonnelCategoryIdL)
                            TjlCustomerRegList.AddRange(query.Where(m => m.PersonnelCategoryId == PersonnelCategoryId).ToList());
                        if (TjlCustomerRegList != null)
                        {
                            return PersonnelCategory(TjlCustomerRegList, input);
                        }
                        var result = new PageResultDto<CustomerRegRosterDto>();
                        result.CurrentPage = 1;
                        result.Calculate(1);
                        result.Result = null;
                        return result;
                    }
                }
            }

            if (query.Count() != 0)
            {
                query = query.OrderByDescending(o => o.BookingDate);
                var Total = query.Count();
                var sex = Convert.ToInt32(Sex.Man);
                var MaleTotal = query.Where(o => o.Customer.Sex == sex).Count();
                sex = Convert.ToInt32(Sex.Woman);
                var FemaleTotal = query.Where(o => o.Customer.Sex == sex).Count();
                sex = Convert.ToInt32(Sex.GenderNotSpecified);
                var Unknown = query.Where(o => o.Customer.Sex == sex).Count();
                var state = Convert.ToInt32(ExaminationState.Alr);
                var NoTotal = query.Where(o => o.CheckSate == state).Count();
                state = Convert.ToInt32(ExaminationState.Unchecked);
                var ConductTotal = query.Where(o => o.CheckSate == state).Count();
                state = Convert.ToInt32(ExaminationState.OK);
                var AlreadyTotal = query.Where(o => o.CheckSate == state).Count();
                var IsFreeTotal = query.Where(o => o.PersonnelCategory.IsFree == true).Count();
                var result = new PageResultDto<CustomerRegRosterDto>();
                result.CurrentPage = input.CurentPage;
                result.Calculate(query.Count());
                query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);

                result.Result = query.MapTo<List<CustomerRegRosterDto>>();
                foreach (var item in result.Result)
                {
                    item.AmountChecked = item.CustomerItemGroup.Where(r => r.CheckState == (int)ProjectIState.Complete).Sum(o => o.PriceAfterDis);
                }
                result.Result.FirstOrDefault().Total = Total;
                result.Result.FirstOrDefault().MaleTotal = MaleTotal;
                result.Result.FirstOrDefault().FemaleTotal = FemaleTotal;
                result.Result.FirstOrDefault().Unknown = Unknown;
                result.Result.FirstOrDefault().NoTotal = NoTotal;
                result.Result.FirstOrDefault().ConductTotal = ConductTotal;
                result.Result.FirstOrDefault().AlreadyTotal = AlreadyTotal;
                result.Result.FirstOrDefault().IsFreeTotal = IsFreeTotal;
                int a=0;
                foreach(var s in result.Result)
                {
                     a = s.CreatorUserId;
                    s.UserNames = _userRepository.FirstOrDefault(o => o.Id == a)?.UserName;
                }
                return result;
            }
            else
            {
                var result = new PageResultDto<CustomerRegRosterDto>();
                result.CurrentPage = 1;
                result.Calculate(1);
                result.Result = null;
                return result;
            }
        }

        public PageResultDto<CustomerRegRosterDto> PersonnelCategory(List<TjlCustomerReg> query, PageInputDto<QueryCustomerRegDto> input)
        {
            if (query.Count() != 0)
            {
                query = query.OrderByDescending(m => m.BookingDate).ToList();
                var Total = query.Count();
                var sex = Convert.ToInt32(Sex.Man);
                var MaleTotal = query.Where(o => o.Customer.Sex == sex).Count();
                sex = Convert.ToInt32(Sex.Woman);
                var FemaleTotal = query.Where(o => o.Customer.Sex == sex).Count();
                sex = Convert.ToInt32(Sex.GenderNotSpecified);
                var Unknown = query.Where(o => o.Customer.Sex == sex).Count();
                var state = Convert.ToInt32(ExaminationState.Alr);
                var NoTotal = query.Where(o => o.CheckSate == state).Count();
                state = Convert.ToInt32(ExaminationState.Unchecked);
                var ConductTotal = query.Where(o => o.CheckSate == state).Count();
                state = Convert.ToInt32(ExaminationState.OK);
                var AlreadyTotal = query.Where(o => o.CheckSate == state).Count();
                var IsFreeTotal = query.Where(o => o.PersonnelCategory.IsFree == true).Count();
                var result = new PageResultDto<CustomerRegRosterDto>();
                result.CurrentPage = input.CurentPage;
                result.Calculate(query.Count());
                query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize).ToList();
                result.Result = query.MapTo<List<CustomerRegRosterDto>>();
                foreach (var item in result.Result)
                {
                    item.AmountChecked = item.CustomerItemGroup.Where(r => r.CheckState == (int)ProjectIState.Complete).Sum(o => o.PriceAfterDis);
                }
                result.Result.FirstOrDefault().Total = Total;
                result.Result.FirstOrDefault().MaleTotal = MaleTotal;
                result.Result.FirstOrDefault().FemaleTotal = FemaleTotal;
                result.Result.FirstOrDefault().Unknown = Unknown;
                result.Result.FirstOrDefault().NoTotal = NoTotal;
                result.Result.FirstOrDefault().ConductTotal = ConductTotal;
                result.Result.FirstOrDefault().AlreadyTotal = AlreadyTotal;
                result.Result.FirstOrDefault().IsFreeTotal = IsFreeTotal;
                return result;
            }
            var resultT = new PageResultDto<CustomerRegRosterDto>();
            resultT.CurrentPage = 1;
            resultT.Calculate(1);
            resultT.Result = null;
            return resultT;
        }
        public PageResultDto<CustomerRegRosterDto> Screen(List<TjlCustomerReg> query, PageInputDto<QueryCustomerRegDto> input)
        {
            if (input.Input.PersonnelCategoryIdL != null && input.Input.PersonnelCategoryIdL.Count != 0)
            {
                foreach (var PersonnelCategoryId in input.Input.PersonnelCategoryIdL)
                    query.AddRange(query.Where(m => m.PersonnelCategoryId == PersonnelCategoryId).ToList());

            }
            if (query.Count() != 0)
            {
                query = query.OrderByDescending(m => m.BookingDate).ToList();
                var result = new PageResultDto<CustomerRegRosterDto>();
                result.CurrentPage = input.CurentPage;
                result.Calculate(query.Count());
                query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize).ToList();
                result.Result = query.MapTo<List<CustomerRegRosterDto>>();
                foreach (var item in result.Result)
                {
                    item.AmountChecked = item.CustomerItemGroup.Where(r => r.CheckState == (int)ProjectIState.Complete).Sum(o => o.PriceAfterDis);
                }
                return result;
            }
            var resultT = new PageResultDto<CustomerRegRosterDto>();
            resultT.CurrentPage = 1;
            resultT.Calculate(1);
            resultT.Result = null;
            return resultT;
        }

        public List<CustomerRegDto> DCScreen(List<TjlCustomerReg> query, QueryCustomerRegDto input)
        {
            //套餐
            if (input.SetMealChoiceT != null && input.CompanysT != null)
            {
                if (input.SetMealChoiceT.Count != 0)
                {
                    foreach (var StrId in input.SetMealChoiceT)
                        query.AddRange(query.Where(o => o.ItemSuitBM.Id == StrId).ToList());
                }
            }

            //套餐名称
            if (!string.IsNullOrWhiteSpace(input.ItemSuitName))
            {
                query = query.Where(m => m.ItemSuitName == input.ItemSuitName).ToList();
            }

            //体检号
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                query = query.Where(m => m.CustomerBM == input.CustomerBM).ToList();
            }

            //体检人姓名
            if (!string.IsNullOrEmpty(input.Customer.Name))
            {
                query = query.Where(m => m.Customer.Name == input.Customer.Name).ToList();
            }

            //性别
            if (input.Customer.Sex != null)
            {
                query = query.Where(m => m.Customer.Sex == input.Customer.Sex).ToList();
            }

            //联系电话/固定电话
            if (!string.IsNullOrWhiteSpace(input.Customer.Mobile))
            {
                query = query.Where(m =>
                    m.Customer.Mobile == input.Customer.Mobile ||
                    m.Customer.Telephone == input.Customer.Mobile).ToList();
            }

            //身份证号
            if (!string.IsNullOrEmpty(input.Customer.IDCardNo))
            {
                query = query.Where(m => m.Customer.IDCardNo == input.Customer.IDCardNo).ToList();
            }

            //医保卡号
            if (!string.IsNullOrEmpty(input.Customer.MedicalCard))
            {
                query = query.Where(m => m.Customer.MedicalCard == input.Customer.MedicalCard).ToList();
            }

            //体检日期
            if (input.NavigationStartTime != null && input.NavigationEndTime != null)
            {
                input.NavigationStartTime = new DateTime(input.NavigationStartTime.Value.Year,
                    input.NavigationStartTime.Value.Month, input.NavigationStartTime.Value.Day, 00, 00, 00);
                input.NavigationEndTime = new DateTime(input.NavigationEndTime.Value.Year,
                    input.NavigationEndTime.Value.Month, input.NavigationEndTime.Value.Day, 23, 59, 59);
                //query = query.Where(o =>
                //        o.BookingDate > input.NavigationStartTime &&
                //        o.BookingDate < input.NavigationEndTime)
                //    .ToList();
                query = query.Where(o =>
                       o.LoginDate > input.NavigationStartTime &&
                       o.LoginDate < input.NavigationEndTime)
                   .ToList();
            }

            //if (input.BookingDate != DBNull.Value)
            //    query = query.Where(m => m.Notice.Contains(input.ItemGroup.Notice));
            //检查状态
            if (input.CheckSate != null)
            {
                if (input.CheckSate != 0)
                {
                    query = query.Where(m => m.CheckSate == input.CheckSate).ToList();
                }
            }

            //年龄
            if (input.AgeStart != null && input.AgeEnd != null)
            {
                if (input.AgeStart != 0 && input.AgeEnd != 0)
                {
                    query = query.Where(o => o.RegAge >= input.AgeStart && o.RegAge <= input.AgeEnd)
                        .ToList();
                }

                if (input.AgeStart != 0 && input.AgeEnd == 0)
                {
                    query = query.Where(o => o.RegAge >= input.AgeStart).ToList();
                }

                if (input.AgeStart == 0 && input.AgeEnd != 0)
                {
                    query = query.Where(o => o.RegAge <= input.AgeEnd).ToList();
                }
            }

            //体检类别
            if (!string.IsNullOrWhiteSpace(input.ClientType))
            {
                query = query.Where(m => m.ClientType == input.ClientType).ToList();
            }

            query = query.OrderByDescending(m => m.CreationTime).ToList();
            return query.MapTo<List<CustomerRegDto>>();
        }

        /// <summary>
        /// 人数统计用查询
        /// </summary>
        /// <param name="input"></param>
        public List<QueryAllForNumberDto> QueryAllForNumber(QueryAllForNumberInput input)
        {
            QueryAllForNumberDto a = new QueryAllForNumberDto();
            var query = _customerRegRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.CheckType))
                query = query.Where(q => q.ClientType == input.CheckType);
            if (input.CompanyId != null) 

                query = query.Where(q => q.ClientRegId == input.CompanyId);
            if (input.DateStart != null)
            {
                input.DateStart = new DateTime(input.DateStart.Value.Year, input.DateStart.Value.Month, input.DateStart.Value.Day, 00, 00, 00);
                if (input.DateType.HasValue && input.DateType == 1)
                {
                    query = query.Where(q => q.BookingDate >= input.DateStart && q.CheckSate !=  (int)PhysicalEState.Not);
                }
                else
                {
                    query = query.Where(q => q.LoginDate >= input.DateStart);
                }
            }

            if (input.DateEnd != null)
            {
                input.DateEnd = new DateTime(input.DateEnd.Value.Year, input.DateEnd.Value.Month, input.DateEnd.Value.Day, 23, 59, 59);
                if (input.DateType.HasValue && input.DateType == 1)
                {
                    query = query.Where(q => q.BookingDate <= input.DateEnd && q.CheckSate != (int)PhysicalEState.Not
                    );
                }
                else
                {
                    query = query.Where(q => q.LoginDate <= input.DateEnd);
                }
            }

            if (input.PersonalTypeId != null)
                query = query.Where(q => q.PersonnelCategoryId == input.PersonalTypeId);

            if (input.Sex != null)
                query = query.Where(q => q.Customer.Sex == input.Sex);
            if (input.CheckState != null)
                query = query.Where(q => q.CheckSate != input.CheckState);

            if (input.RegisterState != null)
            {
                query = query.Where(q => q.RegisterState == input.RegisterState);
            }
            if (!String.IsNullOrWhiteSpace(input.Introducer))
                query = query.Where(q => q.Introducer.Contains(input.Introducer) || q.ClientReg.linkMan.Contains(input.Introducer));
            var result= query.MapTo<List<QueryAllForNumberDto>>();
            return result;
        }

        public List<QueryAllForPersonDto> QueryAllForPerson(QueryAllForNumberInput input)
       {
            var query = _customerRegRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.CheckType))
                query = query.Where(q => q.ClientType == input.CheckType);
            if (input.CompanyId != null)
                query = query.Where(q => q.ClientRegId == input.CompanyId);
            if (input.DateStart != null)
            {
                input.DateStart = new DateTime(input.DateStart.Value.Year, input.DateStart.Value.Month, input.DateStart.Value.Day, 00, 00, 00);
                if (input.DateType.HasValue && input.DateType == 1)
                {
                    query = query.Where(q => q.BookingDate >= input.DateStart);
                }
                else
                {
                    query = query.Where(q => q.LoginDate >= input.DateStart);
                }
            }
            if (input.DateEnd != null)
            {
                input.DateEnd = new DateTime(input.DateEnd.Value.Year, input.DateEnd.Value.Month, input.DateEnd.Value.Day, 23, 59, 59);
                if (input.DateType.HasValue && input.DateType == 1)
                { query = query.Where(q => q.BookingDate <= input.DateEnd); }
                else
                {
                    query = query.Where(q => q.LoginDate <= input.DateEnd);
                }
            }
            if (input.Sex != null)
                query = query.Where(q => q.Customer.Sex == input.Sex);
            if (input.CheckState != null)
                query = query.Where(q => q.CheckSate != input.CheckState);
            if (input.SelectType != null)
            {
                //公司/个人查询
                if (input.SelectType == 1)
                {
                    if (input.ClientRegId != null)
                    {
                        if (input.ClientRegId == Guid.Empty)
                            query = query.Where(q => q.ClientRegId != null);
                        else
                            query = query.Where(q => q.ClientRegId == input.ClientRegId);
                    }
                    else
                        query = query.Where(q => q.ClientRegId == null);
                }
            }
            if (!String.IsNullOrWhiteSpace(input.Introducer))
                query = query.Where(q => q.Introducer.Contains(input.Introducer) || q.ClientReg.linkMan.Contains(input.Introducer));
            if (input.PersonalTypeId != null)
                query = query.Where(q => q.PersonnelCategoryId == input.PersonalTypeId);

            if (input.RegisterState != null)
            {
                query = query.Where(q => q.RegisterState == input.RegisterState);
            }

            return query.OrderByDescending(o => o.BookingDate).ThenBy(o => o.ClientInfo.ClientName).MapTo<List<QueryAllForPersonDto>>();
        }
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public List<PersonTypeDto> GetAllPersonTypes()
        {
            var data = _personalCategoryRepository.GetAll();
            return data.MapTo<List<PersonTypeDto>>();
        }
        /// <summary>
        /// 获取1+X问卷
        /// </summary>
        /// <returns></returns>
        public List<OneAddQuestionsDto> getOneAddXQuestionnaires()
        {
            var oneaddque = _OneAddXQuestionnaire.GetAll().MapTo<List<OneAddQuestionsDto>>();
            return oneaddque;
        }
        /// <summary>
        /// 获取个人问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public List<CustomerQuestionDto> getCustomerQuestionDtos(EntityDto<Guid> input)
        {
            var customerquestion = _CustomerQuestion.GetAll().Where(o => o.CustomerRegId == input.Id).MapTo<List<CustomerQuestionDto>>();
            return customerquestion;
        }
        /// <summary>
        /// 获取个人加项包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CustomerAddPackageDto> getCustomerAddPackage(EntityDto<Guid> input)
        {
            var customeraddpackage = _CustomerAddPackage.GetAll().Where(o => o.CustomerRegId == input.Id).MapTo<List<CustomerAddPackageDto>>();
            return customeraddpackage;
        }
        /// <summary>
        /// 获取个人加项包项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CustomerAddPackageItemDto> getCustomerAddPackageItem(EntityDto<Guid> input)
        {
            var customeraddpackageItem = _CustomerAddPackageItem.GetAll().Where(o => o.CustomerRegId == input.Id).Distinct().MapTo<List<CustomerAddPackageItemDto>>();
            var sd = customeraddpackageItem.GroupBy(p => new { p.ItemGroup.ItemGroupName, p.ItemGroup.ItemGroupExplain, p.ItemPrice, p.PriceAfterDis }).Select(g => g.First()).ToList();

            return sd;
        }
        public List<SimpleItemSuitDto> getSearchItemSuitDto(EntityDto<Guid> input)
        {
            var customeraddpackageItem = _CustomerAddPackage.GetAll().Where(o => o.CustomerRegId == input.Id);
            return customeraddpackageItem.Select(o => o.ItemSuit).Distinct().MapTo<List<SimpleItemSuitDto>>();
        }
        /// <summary>
        /// 查询科室登记人员
        /// </summary>
        public List<DepartMentCustomerRegOutPut> QueryDepartMentCustomerReg(QueryDepartCustomerRegDto input)
        {
            var result = new List<DepartMentCustomerRegOutPut>();
            var customerReg = _customerRegRepository.GetAll().Where(o => o.RegisterState == (int)RegisterState.Yes).AsNoTracking();
            customerReg = customerReg.Include(o => o.CustomerItemGroup);
            customerReg = customerReg.Include(o => o.Customer);
            customerReg = customerReg.Include(o => o.ClientInfo);
            if (input.StartTime.HasValue)
                customerReg = customerReg.Where(o => o.LoginDate > input.StartTime);
            if (input.EndTime.HasValue)
                customerReg = customerReg.Where(o => o.LoginDate < input.EndTime);
            if (input.DepartMentIds != null)
            {
                if (input.DepartMentIds.Count > 0)
                {
                    customerReg = customerReg.Where(o => 
                    o.CustomerItemGroup.Any(p => input.DepartMentIds.Contains(p.DepartmentId) && p.IsAddMinus!=3));
                }
            }
            if (input.ItemGroupId.HasValue)
            {
                if (input.GroupState.HasValue && input.GroupState != 0)
                {
                    //部分检查和已检都算已检查
                    if (input.GroupState == (int)ProjectIState.Complete)
                    {
                        customerReg = customerReg.Where(o => o.CustomerItemGroup.Any(p => p.ItemGroupBM_Id == input.ItemGroupId && ( p.CheckState == (int)ProjectIState.Complete || p.CheckState == (int)ProjectIState.Part)));
                    }
                    else
                    {
                        customerReg = customerReg.Where(o => o.CustomerItemGroup.Any(p => p.ItemGroupBM_Id == input.ItemGroupId && p.CheckState == input.GroupState));
                    }
                }
                else
                {
                    customerReg = customerReg.Where(o => o.CustomerItemGroup.Any(p => p.ItemGroupBM_Id == input.ItemGroupId));
                }
            }         
            if (!string.IsNullOrWhiteSpace(input.Introducer))
            {
                customerReg = customerReg.Where(o => o.Introducer == input.Introducer);
            }
            if (input.ClientRegIds !=null  && input.ClientRegIds.Count > 0)
            {
                customerReg = customerReg.Where(o => input.ClientRegIds.Contains(o.ClientRegId.Value));
            }
            var     customerRegls = customerReg.ToList();
            foreach (var reg in customerRegls)
            {
                var item = reg.CustomerItemGroup.Where(p=>p.IsAddMinus!=3).GroupBy(o => o.DepartmentId);
                if (input.DepartMentIds != null)
                {
                    if (input.DepartMentIds.Count > 0)
                    {
                        item = item.Where(o => input.DepartMentIds.Contains(o.Key));
                    }
                }
                if (input.ItemGroupId.HasValue)
                {
                    item = item.Where(o => o.Any(p=>p.ItemGroupBM_Id == input.ItemGroupId));
                }
                foreach (var o in item)
                {
                    var data = new DepartMentCustomerRegOutPut
                    {
                        DepartmentOrder = o.First().DepartmentBM.OrderNum,
                        DepartmentId = o.First().DepartmentId,
                        DepartMentName = o.First().DepartmentName?.Replace(" ", ""),
                        CustomerName = o.First().CustomerRegBM.Customer.Name,
                        LoginDate = o.First().CustomerRegBM.LoginDate,
                        BookingDate = o.First().CustomerRegBM.BookingDate,
                        Sex = o.First().CustomerRegBM.Customer.Sex,
                        CustomerBM = o.First().CustomerRegBM.CustomerBM,
                        Age = o.First().CustomerRegBM.Customer.Age,
                        ClientName = o.First().CustomerRegBM.ClientInfo == null ? "个人" : o.First().CustomerRegBM.ClientInfo.ClientName,
                        ItemGroups = input.ItemGroupId.HasValue? reg.CustomerItemGroup.Where(p => p.ItemGroupBM_Id== input.ItemGroupId.Value && p.DepartmentId == o.First().DepartmentId && p.IsAddMinus != (int)AddMinusType.Minus).MapTo<List<JLItemGroupDto>>() 
                        : reg.CustomerItemGroup.Where(p => p.DepartmentId == o.First().DepartmentId && p.IsAddMinus != (int)AddMinusType.Minus).MapTo<List<JLItemGroupDto>>(),
                        SummSate = reg.SummSate,
                        Introducer = reg.Introducer,
                         Department=reg.Customer.Department,
                        Remarks=reg.Remarks
                    };
                    //计算科室内体检状态
                    if (data.ItemGroups.Any(p => p.CheckState != (int)ProjectIState.GiveUp))
                    {
                        var list = data.ItemGroups.Where(p => p.CheckState != (int)ProjectIState.GiveUp).ToList();
                        if (data.ItemGroups.Any(p => p.CheckState == (int)ProjectIState.Stay))
                        {
                            data.CheckState = (int)ProjectIState.Not;
                            list = data.ItemGroups.Where(p => p.CheckState != (int)ProjectIState.Stay).ToList();
                        }
                        if (list.Any(p => p.CheckState != (int)ProjectIState.Not))
                        {
                            if (list.Any(p => p.CheckState != (int)ProjectIState.Complete))
                            {
                                data.CheckState = (int)PhysicalEState.Process;
                            }
                            else
                                data.CheckState = (int)PhysicalEState.Complete;
                        }
                        else
                            data.CheckState = (int)PhysicalEState.Not;
                    }
                    else
                    {
                        data.CheckState = (int)PhysicalEState.Complete;
                    }

                    result.Add(data);
                }

                //result.AddRange(
                //item.Select(o =>
                //new DepartMentCustomerRegOutPut
                //{
                //    DepartmentId = o.DepartmentId,
                //    DepartMentName = o.DepartmentName,
                //    CustomerName = o.CustomerRegBM.Customer.Name,
                //    CheckState = reg.CheckSate,
                //    LoginDate = o.CustomerRegBM.LoginDate,
                //    BookingDate = o.CustomerRegBM.BookingDate,
                //    Sex = o.CustomerRegBM.Customer.Sex,
                //    CustomerBM = o.CustomerRegBM.CustomerBM,
                //    Age = o.CustomerRegBM.Customer.Age,
                //    ClientName = o.CustomerRegBM.ClientInfo?.ClientName,
                //    ItemGroups = reg.CustomerItemGroup.Where(p=>p.DepartmentId==o.DepartmentId).MapTo<List<JLItemGroupDto>>()
                //}));
            }
            return result.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.CheckState).ToList();
        }
        
        /// <summary>
        /// 根据身份证获取体检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<QueryCustomerRegDto> GetCustomerIDCard(SearchCustomerDto input)
        {
            //if (input.NotCheckState == 1)
            //{
            //    var query = _customerRegRepository.GetAll().AsNoTracking();
            //    if (!string.IsNullOrWhiteSpace(input.ArchivesNum))
            //    {
            //        query = query.Where(o => o.Customer.ArchivesNum == input.ArchivesNum);
            //    }
            //    if (!string.IsNullOrWhiteSpace(input.Name))
            //    {
            //        query = query.Where(o => o.Customer.Name == input.Name);
            //    }
            //    if (!string.IsNullOrWhiteSpace(input.IDCardNo))
            //    {
            //        query = query.Where(o => o.Customer.IDCardNo == input.IDCardNo);
            //    }
            //    if (!string.IsNullOrWhiteSpace(input.CustomerBM))
            //    {
            //        query = query.Where(o => o.CustomerBM == input.CustomerBM);
            //    }
            //    var result = query.OrderByDescending(o => o.CreationTime);
            //    var retdatas = new List<QueryCustomerRegDto>();//result.MapTo<List<QueryCustomerRegDto>>();
            //    if (result?.ToList().Count > 0)
            //    {
            //        foreach (var r in result)
            //        {
            //            var data = r.MapTo<QueryCustomerRegDto>();
            //            data.CustomerItemGroup = data.CustomerItemGroup.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder)?.ToList();
            //            if (r.ClientInfo != null)
            //                data.DanweiMingcheng = r.ClientInfo.ClientName;
            //            if (r.LastModifierUserId.HasValue)
            //            {
            //                var user = _userRepository.FirstOrDefault(o => o.Id == r.LastModifierUserId);
            //                if (user != null)
            //                {

            //                    data.KaidanYisheng = user.Name;
            //                }
            //            }
            //            else
            //            {
            //                var user = _userRepository.FirstOrDefault(o => o.Id == r.CreatorUserId);
            //                data.KaidanYisheng = user.Name;
            //            }
            //            retdatas.Add(data);
            //        }                   
            //    }
            //    return retdatas;
            //}
            //else
            //{
            //体检状态都为完成的时候就只能查询个人的信息 查不到登记信息了
            var customers = _customerRegRepository.GetAll();
            //if (!string.IsNullOrWhiteSpace(input.ArchivesNum)||!string.IsNullOrWhiteSpace(input.CustomerBM))
            //    customers = customers.Where(o => o.ArchivesNum == input.ArchivesNum||o.ArchivesNum==input.CustomerBM);
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                customers = customers.Where(o => o.Customer.Name == input.Name);
            }
            if (!string.IsNullOrWhiteSpace(input.IDCardNo))
            {
                customers = customers.Where(o => o.Customer.IDCardNo == input.IDCardNo);
            }

            if (string.IsNullOrWhiteSpace(input.ArchivesNum) && string.IsNullOrWhiteSpace(input.Name) &&
                string.IsNullOrWhiteSpace(input.IDCardNo))
            {
                return null;
            }
            var customer = customers.OrderByDescending(o => o.LoginDate).FirstOrDefault();
              
            if (customer != null)
            {
                var CusReg = customer.MapTo<QueryCustomerRegDto>();
                var CusReglist = new List<QueryCustomerRegDto>();
                CusReglist.Add(CusReg);
                return CusReglist;
            }
            else
            {
                return null;
            }
        }
        public List<DisageSumDTO> GetCustomerDisageSum(QueryCustomerRegDto input)
        {
            List<DisageSumDTO> disageSumDTOs = new List<DisageSumDTO>();
            string strcus = "SELECT CustomerBM,  Sex ,Age,SummarizeName,Name,IDCardNo FROM TjlCustomers " +
             "INNER JOIN TjlCustomerRegs on CustomerId = TjlCustomers.Id " +
             "INNER JOIN TjlCustomerSummarizeBMs on TjlCustomerRegs.Id = CustomerRegID where 1=1";
            if (input == null)
            {
                return disageSumDTOs;
            }
            if (input.sex != null)
            {
                strcus += " and Sex='" + input.sex.Value + "' ";
            }
            if (input.AgeStart != null && input.AgeEnd != null)
            {
                strcus += " and Age BETWEEN '" + input.AgeStart.Value + "' and  '" + input.AgeEnd.Value + "'";
            }
            if (input.ClientInfoId != null)
            {
                strcus += " and ClientInfoId='" + input.ClientInfoId + "' ";
            }
            if (input.LoginDateStartTime != null && input.LoginDateEndTime != null)
            {
                strcus += " and LoginDate BETWEEN '" + input.LoginDateStartTime.Value.ToString("yyyy-MM-dd")
                    + " 00:00:00' and  '" + input.LoginDateEndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59'";
            }
            if (input.BookingDateStartTime != null && input.BookingDateEndTime != null)
            {
                strcus += " and TjlCustomerRegs.CreationTime BETWEEN '" + input.BookingDateStartTime.Value + "' and  '" + input.BookingDateEndTime.Value + "'";
            }
            SqlParameter[] parameters = {
                        new SqlParameter("@id",new Guid()),
                        };
            disageSumDTOs = _sqlExecutor.SqlQuery<DisageSumDTO>
                           (strcus, parameters).ToList();
            return disageSumDTOs;
        }
        public HisCusInfoDto geHisvard(InCarNumDto input)
        { 
            // 获取接口数据
            var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

            var search = new InCarNum();
            search.CardNum = input.CardNum;
            search.HISName = input.HISName;
          
          
            var interfaceResult = hisInterfaceDriver.GetCusInfoByNum(search);

            return interfaceResult.MapTo<HisCusInfoDto>();
        }
        /// <summary>
        /// 挂号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InCarNumDto SaveHisInfo(InCarNumDto input)
        {
            // 获取接口数据
            InCarNumDto inCarNum = new InCarNumDto();
                var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

            var search = input.MapTo<InCarNum>();
            var interfaceResult = hisInterfaceDriver.SaveHisInfo(search);
            if (interfaceResult.Code == "0")
            {
                inCarNum.CardNum = interfaceResult.CardNum;
            }
            else
            {
                inCarNum.CardNum = "";
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                //添加操作日志  
                var sp = input.CardNum.Split('|');
                createOpLogDto.LogBM = sp[1];
                createOpLogDto.LogName = sp[1];
                createOpLogDto.LogText = "HIS挂号失败："+ interfaceResult.Code;
                createOpLogDto.LogType = (int)LogsTypes.InterId;
                createOpLogDto.LogDetail = input.CardNum;
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            return inCarNum;
        }

        //宜兴his获取物价同步
        public List<HisPriceDtos> GetYXHis(InCarNumPriceDto input)
        {
            //List<HisPriceDtos> hisPriceDtos = new List<HisPriceDtos>();
            // 获取接口数据
            var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

            var search = input.MapTo<InCarNumPrice>();

            var interfaceResults = hisInterfaceDriver.GetHisPrice(search);


            var returns = interfaceResults.MapTo<List<HisPriceDtos>>();
            //hisPriceDtos.Add(returns);

            return returns;

        }
        


        /// <summary>
        /// 插入医嘱项目表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<HisPriceDtos> InsertYXHis(List<HisPriceDtos> input)
        {
            // 获取接口数据
            //var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

            //var search = input.MapTo<List<HisPriceDto>>();

            //var interfaceResults = hisInterfaceDriver.InsertYXCharg(search);

            //var search = input.MapTo<List<TbmPriceSynchronize>>();
            var pricels = new List<TbmPriceSynchronize>();
            var upricels = new List<TbmPriceSynchronize>();
            input = input.Where(o=>o.chkit_costn !=null).ToList();
            foreach (var tbmPrice in input)
            {
             // var tbmprice=  _TbmPriceSynchronize.FirstOrDefault(o=>o.chkit_id== tbmPrice.chkit_id);
                var tbmpricelist = _TbmPriceSynchronize.GetAll().Where(o => o.chkit_id == tbmPrice.chkit_id).ToList();
                if (tbmpricelist.Count == 0)
                {
                    var search = tbmPrice.MapTo<TbmPriceSynchronize>();
                    search.Id = Guid.NewGuid();
                    pricels.Add(search);

                }
                foreach (var tbmprice in tbmpricelist)
                {
                    if (tbmprice == null)
                    {
                        var search = tbmPrice.MapTo<TbmPriceSynchronize>();
                        search.Id = Guid.NewGuid();
                        pricels.Add(search);
                    }
                    else
                    {

                        //tbmPrice.MapTo(tbmprice);
                        //同步医嘱项目
                        var grouppris = _TbmGroupRePriceSynchronizes.GetAll().Where(o => o.PriceSynchronizeId == tbmprice.Id && o.chkit_costn != (o.Count * tbmPrice.chkit_costn));
                        if (tbmPrice.chkit_Type != tbmprice.chkit_Type || tbmPrice.chkit_name != tbmprice.chkit_name || tbmPrice.chkit_costn != tbmprice.chkit_costn || grouppris.Count() > 0)
                        {
                            _TbmPriceSynchronize.Update(tbmprice.Id, e =>
                            {
                                e.chkit_costn = tbmPrice.chkit_costn;
                                e.chkit_name = tbmPrice.chkit_name;
                                e.chkit_Type = tbmPrice.chkit_Type;
                            });
                            if (grouppris.Count() > 0)
                            {
                                var groupPrid = grouppris.Update(o => new TbmGroupRePriceSynchronizes
                                {
                                    chkit_costn = o.Count * tbmPrice.chkit_costn
                                });
                            }
                            //同步组合，套餐价格
                            var Itemgroups = _itemGroupRepository.GetAll().Where(o => o.GroupRePriceSynchronizes.Any(r => r.PriceSynchronizeId == tbmprice.Id)).ToList();

                            foreach (var itemgroup in Itemgroups)
                            {

                                var upgroups = _itemGroupRepository.Update(itemgroup.Id, e => { e.Price = e.GroupRePriceSynchronizes.Sum(o => o.chkit_costn); });
                                var suitgroups = _ItemSuitItemGroupContrast.GetAll().Where(o => o.ItemGroupId == itemgroup.Id).ToList();
                                foreach (var suitrgrp in suitgroups)
                                {
                                    _ItemSuitItemGroupContrast.Update(suitrgrp.Id, e => { e.ItemPrice = upgroups.Price; });

                                }
                            }

                        }
                         

                    }
                }
                
            }
            _sqlExecutor.DbContext.Set<TbmPriceSynchronize>().AddRange(pricels);

            pricels.AddRange(upricels);

            try
            {
                CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            var returns = pricels.MapTo<List<HisPriceDtos>>();

            return returns;
        }


        /// <summary>
        /// 插入申请单 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public OutApplicationDto InsertSFCharg(TJSQDto input)
        {
            OutApplicationDto chargeBM = new OutApplicationDto();
            TjlApplicationForm appliy = new TjlApplicationForm();          
            TjlCustomerReg cusreg = new TjlCustomerReg();
            string blh = "";
            string mzh = "";
            string ksbm = "";
            
            string kdys = "";
            //门诊病历号
            string cardNo = "";
            if (!string.IsNullOrEmpty( input.BRH))
            {
                appliy = _TjlApplicationForm.FirstOrDefault(o => o.CustomerReg.CustomerBM == input.BRH.ToString() && o.SQSTATUS == 1);
                cusreg = _customerRegRepository.Get(input.CustomerRegId.Value);
                blh= cusreg.Customer.ArchivesNum;                
                mzh= cusreg.HisSectionNum;               
                ksbm= cusreg.NucleicAcidType.ToString();
                cardNo = cusreg.Customer.SectionNum;
                if (cusreg.OrderUserId.HasValue)
                {
                    kdys = cusreg.OrderUser.EmployeeNum;
                }
                else
                {
                    var userBM = _userRepository.Get(AbpSession.UserId.Value);
                    kdys = userBM.EmployeeNum;
                }
                input.FYZK = cusreg.McusPayMoney.PersonalMoney - cusreg.McusPayMoney.PersonalPayMoney;

                if (input.FYZK == 0)
                {
                    chargeBM.ApplicationNum = "";
                    return chargeBM;
                }
               
            }
            else
            {
                // appliy = _TjlApplicationForm.FirstOrDefault(o => o.ClientReg.ClientInfo.ClientName == input.DWMC.ToString() && o.SQSTATUS == 1);
                //修改为单位每次都新建申请
                appliy = null;
                 var clientreg = _clientRegRepository.Get(input.ClientRegId.Value);
                blh = clientreg.ClientRegBM;
                mzh = clientreg.Remark;
                ksbm = "1071";
                //科室医生
                var userBM = _userRepository.Get(AbpSession.UserId.Value);
                    kdys = userBM.EmployeeNum;               
                var kdysdt = _BasicDictionaryy.GetAll().FirstOrDefault(o => o.Type == "ForegroundFunctionControl" && o.Value == 150);
                if (kdysdt!=null && kdysdt.Remarks !="")
                {
                    var sbi = kdysdt.Remarks.Split('|');
                    if (sbi.Length > 0)
                    {
                        var ttusename = sbi[0];
                        var userNmae = _userRepository.GetAll().FirstOrDefault(o => o.Name == ttusename);
                        if (userNmae != null)
                        {
                            kdys = userNmae.EmployeeNum;
                        }
                    }
                }
                
            }
            if (appliy != null)
            {
                chargeBM.ApplicationNum= appliy.SQDH;
                if (input.HISName == "南京飓风" || input.HISName == "世轩")
                {
                    var hisInterfaceDriver1 = DriverFactory.GetDriver<IHisInterfaceDriver>();
                    InCarNum inCarNum = new InCarNum();
                    inCarNum.HISName = input.HISName;
                    inCarNum.CardNum = "TJ" + appliy.SQDH;
                    var ret = hisInterfaceDriver1.getHisState(inCarNum);
                    if (ret != "")
                    {
                        chargeBM.ApplicationNum = "插入HIS失败，该申请单HIS已收费";
                        return chargeBM;
                    }
                }
                input.SQDH = appliy.SQDH;
                input.Id = appliy.Id;
                input.SQSTATUS = appliy.SQSTATUS;
                input.DJKSDM = "登记科室代码";
                input.ZKLX = "2";
                input.REFUNDABLE = "N";
                var remark = "";
                if (!string.IsNullOrEmpty(appliy.Remark))
                {
                     remark = appliy.Remark.ToString();
                }
                input.MapTo(appliy);
                appliy.Remark = remark;
                _TjlApplicationForm.Update(appliy);

            }
            else
            {
                appliy = new TjlApplicationForm();
                input.SQDH = _IDNumberAppService.CreateApplicationBM();
                chargeBM.ApplicationNum = appliy.SQDH;
                input.SQSTATUS = 1;
                input.DJKSDM = "登记科室代码";
                input.ZKLX = "2";
                input.REFUNDABLE = "N";
                input.HCZT = 0;
               // input
                input.MapTo(appliy);
                _TjlApplicationForm.Insert(appliy);

            }
            CurrentUnitOfWork.SaveChanges();
            chargeBM.ZHMoney = appliy.FYZK;
            chargeBM.ApplicationNum = appliy.SQDH;
            //获取接口数据
            var interfaceResult = "";
           var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();
            if (input.HISName == "南京飓风" || input.HISName == "世轩")
            {

                MZ_YZML_MASTER mZ_YZML_MASTER = new MZ_YZML_MASTER();
                mZ_YZML_MASTER.yzid = "TJ" + appliy.SQDH;
                mZ_YZML_MASTER.sqdh = appliy.SQDH;
                mZ_YZML_MASTER.blh = blh;
                mZ_YZML_MASTER.mzh = mzh;
                mZ_YZML_MASTER.ksbm = ksbm;


                mZ_YZML_MASTER.ysbm = kdys;
                mZ_YZML_MASTER.rq = System.DateTime.Now;
                mZ_YZML_MASTER.xmlbbm = "80";
                mZ_YZML_MASTER.zxksbm = mZ_YZML_MASTER.ksbm;
                mZ_YZML_MASTER.zdbm01 = "";
                mZ_YZML_MASTER.zdmc01 = "";
                mZ_YZML_MASTER.ysyy = "";
                mZ_YZML_MASTER.bz = "";
                mZ_YZML_MASTER.lb = "M";
                mZ_YZML_MASTER.cflx = "体检";
                mZ_YZML_MASTER.yzxh = 0;
                mZ_YZML_MASTER.fybz = "0";
                mZ_YZML_MASTER.fyczy = "";
                mZ_YZML_MASTER.detail = new List<MZ_YZML_FY>();
                //健康管理科传明细(只有个人存在)
                if (mZ_YZML_MASTER.ksbm == "1096")
                {
                    mZ_YZML_MASTER.zdbm01 = "z200.001";
                    mZ_YZML_MASTER.zdmc01 = "健康查体";
                    var zdmc = _BasicDictionaryy.GetAll().FirstOrDefault(o => o.Type == "HisInterface" && o.Value == 3);
                    if (zdmc != null && !string.IsNullOrEmpty(zdmc.Remarks))
                    {
                        var sp = zdmc.Remarks.Split('|');
                        if (sp.Length >= 2)
                        {
                            mZ_YZML_MASTER.zdbm01 = sp[0];
                            mZ_YZML_MASTER.zdmc01 = sp[1];
                        }

                    }

                    var group = _customerItemGroupRepository.GetAll().Where(o =>
                     o.CustomerRegBMId == input.CustomerRegId.Value && o.IsAddMinus != (int)AddMinusType.Minus && o.PriceAfterDis != 0
                     && o.MReceiptInfoPersonalId == null && o.MReceiptInfoClientlId == null && o.PayerCat == (int)PayerCatType.NoCharge);
                    var HISDetail = group.SelectMany(o => o.ItemGroupBM.GroupRePriceSynchronizes).ToList();
                    var HISMoney = HISDetail.Sum(o => o.chkit_costn);
                    if (HISMoney != input.FYZK)
                    {
                        chargeBM.ApplicationNum = "插入HIS失败，金额不符！";
                        return chargeBM;
                    }
                    int num = 1;
                    foreach (var groupls in HISDetail)
                    {
                        var fy = mZ_YZML_MASTER.detail.FirstOrDefault(o => o.yzid == groupls.PriceSynchronize.chkit_id + appliy.SQDH);
                        if (fy != null)
                        {
                            //fy.jg += groupls.PriceSynchronize.chkit_costn.Value;
                            fy.sl += decimal.Parse(groupls.Count.ToString());
                        }
                        else
                        {
                            MZ_YZML_FY mZ_YZML_FY = new MZ_YZML_FY();
                            mZ_YZML_FY.yzid = groupls.PriceSynchronize.chkit_id + appliy.SQDH;
                            mZ_YZML_FY.mzh = mZ_YZML_MASTER.mzh;
                            mZ_YZML_FY.ksbm = mZ_YZML_MASTER.ksbm;
                            mZ_YZML_FY.ysbm = mZ_YZML_MASTER.ysbm;
                            mZ_YZML_FY.rq = mZ_YZML_MASTER.rq;
                            mZ_YZML_FY.blh = mZ_YZML_MASTER.blh;
                            mZ_YZML_FY.cfh = mZ_YZML_MASTER.yzid;
                            mZ_YZML_FY.xh = num;
                            mZ_YZML_FY.xh_id = 0;
                            mZ_YZML_FY.yzbm = groupls.PriceSynchronize.chkit_id;
                            mZ_YZML_FY.yzmc = groupls.PriceSynchronize.chkit_name;
                            mZ_YZML_FY.gg = "";
                            mZ_YZML_FY.ypcd = "";
                            mZ_YZML_FY.dw = groupls.PriceSynchronize.aut_name;
                            mZ_YZML_FY.sl = decimal.Parse(groupls.Count.ToString());
                            mZ_YZML_FY.jg = groupls.PriceSynchronize.chkit_costn.Value;
                            mZ_YZML_FY.dcsl = 1;
                            mZ_YZML_FY.zbbz = "0";
                            mZ_YZML_FY.sfbz = "0";
                            mZ_YZML_FY.ts = 1;
                            mZ_YZML_FY.jl = 1;
                            mZ_YZML_FY.cs = 1;
                            mZ_YZML_FY.yzzh = "-1";
                            mZ_YZML_FY.psbz = "0";
                            mZ_YZML_FY.fybz = "0";
                            mZ_YZML_FY.pjlsh = "";
                            mZ_YZML_FY.sfxmbm = "";
                            mZ_YZML_FY.tjxmbm = "";
                            mZ_YZML_FY.xmlbbm = "80";
                            mZ_YZML_FY.djbz = "0";
                            mZ_YZML_FY.zxksbm = "";
                            mZ_YZML_FY.tfbz = "0";
                            mZ_YZML_FY.dfbz = "0";
                            mZ_YZML_FY.sjly = "8";
                            mZ_YZML_FY.sfdh = mZ_YZML_MASTER.yzid;

                            mZ_YZML_MASTER.detail.Add(mZ_YZML_FY);
                            num = num + 1;
                        }
                    }
                }
                else
                {
                    MZ_YZML_FY mZ_YZML_FY = new MZ_YZML_FY();
                    mZ_YZML_FY.yzid = "m0092" + appliy.SQDH;
                    mZ_YZML_FY.mzh = mZ_YZML_MASTER.mzh;
                    mZ_YZML_FY.ksbm = mZ_YZML_MASTER.ksbm;
                    mZ_YZML_FY.ysbm = mZ_YZML_MASTER.ysbm;
                    mZ_YZML_FY.rq = mZ_YZML_MASTER.rq;
                    mZ_YZML_FY.blh = mZ_YZML_MASTER.blh;
                    mZ_YZML_FY.cfh = mZ_YZML_MASTER.yzid;
                    mZ_YZML_FY.xh = 1;
                    mZ_YZML_FY.xh_id = 0;
                    mZ_YZML_FY.yzbm = "m0092";
                    mZ_YZML_FY.yzmc = "体检费";
                    mZ_YZML_FY.gg = "";
                    mZ_YZML_FY.ypcd = "";
                    mZ_YZML_FY.dw = "次";
                    mZ_YZML_FY.sl = 1;
                    mZ_YZML_FY.jg = appliy.FYZK;
                    mZ_YZML_FY.dcsl = 1;
                    mZ_YZML_FY.zbbz = "0";
                    mZ_YZML_FY.sfbz = "0";
                    mZ_YZML_FY.ts = 1;
                    mZ_YZML_FY.jl = 1;
                    mZ_YZML_FY.cs = 1;
                    mZ_YZML_FY.yzzh = "-1";
                    mZ_YZML_FY.psbz = "0";
                    mZ_YZML_FY.fybz = "0";
                    mZ_YZML_FY.pjlsh = "";
                    mZ_YZML_FY.sfxmbm = "";
                    mZ_YZML_FY.tjxmbm = "";
                    mZ_YZML_FY.xmlbbm = "80";
                    mZ_YZML_FY.djbz = "0";
                    mZ_YZML_FY.zxksbm = "";
                    mZ_YZML_FY.tfbz = "0";
                    mZ_YZML_FY.dfbz = "0";
                    mZ_YZML_FY.sjly = "8";
                    mZ_YZML_FY.sfdh = mZ_YZML_MASTER.yzid;
                    mZ_YZML_MASTER.detail.Add(mZ_YZML_FY);
                }

                interfaceResult = hisInterfaceDriver.InsertHisSFCharg(mZ_YZML_MASTER);
            }
            //else if (input.HISName == "东软")
            //{
            //    var search = input.MapTo<TJSQ>();
            //    search.BRH = cardNo;
            //    search.BRKH = mzh;
            //    chargeBM.FPName = cardNo;//字段借用
            //    chargeBM.Remark= mzh;//字段借用
            //    interfaceResult = hisInterfaceDriver.InsertSFCharg(search);
            //}
            else  
            {
                if (input.HISName == "东软")
                {
                    chargeBM.FPName = cardNo;//字段借用
                    chargeBM.Remark= mzh;//字段借用
                }
                else
                { 
                    var search = input.MapTo<TJSQ>();
                    interfaceResult = hisInterfaceDriver.InsertSFCharg(search);
                }
            }
          
           return chargeBM;
            
        }

        /// <summary>
        /// 删除申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public OutErrDto DelApply(TJSQDto input)
        {
            OutErrDto outErrDto = new OutErrDto();           
           
            var appliy = _TjlApplicationForm.FirstOrDefault(o => o.SQDH== input.SQDH );
            if (appliy == null)
            {
                outErrDto.code = "0";
                outErrDto.err = "无该申请单";
                return outErrDto;
            }

            else if (appliy.SQSTATUS == 2)
            {
                outErrDto.code = "0";
                outErrDto.err = "申请单已收费不能取消,不能删除该申请单！";
                return outErrDto;
            }
            else if (appliy.SQSTATUS == 3)
            {
                outErrDto.code = "0";
                outErrDto.err = "申请单已作废,不能删除该申请单！";
                return outErrDto;
            }
            else if (appliy.SQSTATUS == 1)
            {
                _TjlApplicationForm.Delete(appliy);
                outErrDto.code = "1";
                outErrDto.err = "删除成功！";
                return outErrDto;
            }
            else
            {
                outErrDto.code = "0";
                outErrDto.err = "申请单状态异常，不能删除！";
                return outErrDto;
            }


        }

        /// <summary>
        /// 查询申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutApplicationDto> getapplication(TJSQDto input)
        {
            List<OutApplicationDto> applicationls = new List<OutApplicationDto>();
            var auer = _TjlApplicationForm.GetAll().AsNoTracking();
            if (input.ClientRegId.HasValue)
            {
                auer = auer.Where(o => o.ClientRegId == input.ClientRegId);
            }
            if (input.CustomerRegId.HasValue)
            {
                auer = auer.Where(o => o.CustomerRegId == input.CustomerRegId);
            }
          return auer.Select(o => new OutApplicationDto { ApplicationNum=o.SQDH, ZHMoney=o.FYZK, SQSTATUS=o.SQSTATUS,  CreatTime=o.CreationTime , FPName=o.FPName, Remark=o.Remark}).ToList();
        }
        #region 浉河区公卫接口
        [UnitOfWork(false)]
        /// <summary>
        /// 上传体检数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutResult OutTJData(GsearchCustomerDto input)
        {
            try
            {
                OutResult outResult = new OutResult();
                outResult.Code = 1;
                var que = _customerRegRepository.GetAllIncluding(o => o.Customer, o => o.CustomerRegItems);
                var list = que.ToList();
                if (!string.IsNullOrEmpty(input.Name))
                {
                    que = que.Where(o => o.Customer.Name == input.Name);
                    list = que.ToList();
                }
                if (input.Sex.HasValue)
                {
                    que = que.Where(o => o.Customer.Sex == input.Sex);
                }
                if (!string.IsNullOrEmpty(input.IdCardNo))
                {
                    que = que.Where(o => o.Customer.IDCardNo == input.IdCardNo);
                    list = que.ToList();
                }
                if (!string.IsNullOrEmpty(input.CustomerBM))
                {
                    que = que.Where(o => o.CustomerBM == input.CustomerBM);
                }
                if (input.StartDate.HasValue && input.EndtDate.HasValue)
                {
                    que = que.Where(o => o.LoginDate >= input.StartDate && o.LoginDate <= input.EndtDate);
                }
                var customerreg = que.ToList();

                foreach (TjlCustomerReg cusreg in customerreg)
                {
                    if (cusreg.CustomerRegItems.Count == 0)
                    {
                        continue;
                    }
                    getAuthKeyGetDto getAuthKeyGetDto = new getAuthKeyGetDto();
                    doctordata doctordata = new doctordata();

                    doctordata.xm = input.DoctorName;
                    doctordata.jgdm =input.DoctorJgdm /*"411502019"*/;
                    getAuthKeyGetDto.doctor = doctordata;
                    string jsonstr = JsonConvert.SerializeObject(getAuthKeyGetDto).Replace("NULL", "").Replace("null", "");
                    // Logger.Debug(jsonstr);

                    var GWURL = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "浉河区公卫接口地址")?.Remarks.ToString();
                    var UserURL = GWURL + "getAuthKey";
                    var ret = PostReturnFunction(jsonstr, UserURL);
                    
                    getJkdaGrJbxxIGetDto getJkdaGrJbxxIGetDto = new getJkdaGrJbxxIGetDto();
                    doctorJkdaGrJbxxIGetDto doctors = new doctorJkdaGrJbxxIGetDto();
                    PaientJkdaGrJbxxIGetDto paients = new PaientJkdaGrJbxxIGetDto();
                    doctors.jgdm =input.DoctorJgdm /*//"411502019"*/;
                    doctors.token= ret.data.token;
                    doctors.loginId = ret.data.loginId;                   
                    paients.sfzh= cusreg.Customer.IDCardNo;
                    getJkdaGrJbxxIGetDto.doctor = doctors;
                    getJkdaGrJbxxIGetDto.patient = paients;
                    string jsonstrs = JsonConvert.SerializeObject(getJkdaGrJbxxIGetDto).Replace("NULL", "").Replace("null", "");
                    // Logger.Debug(jsonstr);

                    var PersonURL = GWURL + "getJkdaGrJbxxInfo";
                    var rets = GetkdaGrJbxxIReturnFunction(jsonstrs, PersonURL);

                    if (rets.data.jbxxXm != null)
                    {

                        //判断：等于4已上传，走修改
                        if (cusreg.BespeakState == 4)
                        {
                            #region 赋值

                            updateJkdaTjBg JbxxIGetDto = new updateJkdaTjBg();

                            doctorJkdaGrJbxxIGetDto doctorss = new doctorJkdaGrJbxxIGetDto();
                            PaientJkdaGrJbxxIGetDto paientss = new PaientJkdaGrJbxxIGetDto();
                            doctorss.jgdm = input.DoctorJgdm /*"411502019"*/;//
                            doctorss.token = ret.data.token;
                            doctorss.loginId = ret.data.loginId;
                            paientss.sfzh = cusreg.Customer.IDCardNo;
                            JbxxIGetDto.doctor = doctorss;


                            PaientInfor paientInfor = new PaientInfor();
                            paientInfor.ytjid = cusreg.CustomerBM.ToString();
                            paientInfor.sfzh = cusreg.Customer.IDCardNo;
                            if (cusreg.LoginDate != null)
                            {
                                paientInfor.tjsj = cusreg.LoginDate.Value.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                paientInfor.tjsj = "";
                            }


                            //体检医生
                            if (cusreg.CSEmployeeBM == null)
                            {
                                var Username = _userRepository.FirstOrDefault(o => o.Id == cusreg.CreatorUserId).UserName;
                                paientInfor.tjys = Username;
                            }
                            else
                            {
                                paientInfor.tjys = cusreg.CSEmployeeBM.Name;
                            }

                            paientInfor.yljgdm = input.DoctorJgdm;
                            //1无症状 2头痛 3头晕 4心悸 5胸闷 6胸痛 7慢性咳嗽 8咳痰 9呼吸困难 10多饮 11多尿  12体重下降  13乏力 14关节肿痛15视力模糊16手脚麻木 17尿急18尿痛 19便秘 20腹泻21恶心呕吐22眼花 23耳鸣 24乳房胀痛 25其他
                            var zz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zz")?.ItemResultChar;
                            int tixingbm = 0;
                            switch (zz)
                            {
                                case "无症状":
                                    tixingbm = 1;
                                    break;
                                case "头痛":
                                    tixingbm = 2;
                                    break;
                                case "头晕":
                                    tixingbm = 3;
                                    break;
                                case "心悸":
                                    tixingbm = 4;
                                    break;
                                case "胸闷":
                                    tixingbm = 5;
                                    break;
                                case "胸痛":
                                    tixingbm = 6;
                                    break;
                                case "慢性咳嗽":
                                    tixingbm = 7;
                                    break;
                                case "咳痰":
                                    tixingbm = 8;
                                    break;
                                case "呼吸困难":
                                    tixingbm = 9;
                                    break;
                                case "多饮":
                                    tixingbm = 10;
                                    break;
                                case "多尿":
                                    tixingbm = 11;
                                    break;
                                case "体重下降":
                                    tixingbm = 12;
                                    break;
                                case "乏力":
                                    tixingbm = 13;
                                    break;
                                case "关节肿痛":
                                    tixingbm = 14;
                                    break;
                                case "视力模糊":
                                    tixingbm = 15;
                                    break;
                                case "手脚麻木":
                                    tixingbm = 16;
                                    break;
                                case "尿急":
                                    tixingbm = 17;
                                    break;
                                case "尿痛":
                                    tixingbm = 18;
                                    break;
                                case "便秘":
                                    tixingbm = 19;
                                    break;
                                case "腹泻":
                                    tixingbm = 20;
                                    break;
                                case "恶心呕吐":
                                    tixingbm = 21;
                                    break;
                                case "眼花":
                                    tixingbm = 22;
                                    break;
                                case "耳鸣":
                                    tixingbm = 23;
                                    break;
                                case "乳房胀痛":
                                    tixingbm = 24;
                                    break;
                                case "其他":
                                    tixingbm = 25;
                                    break;


                            }
                            paientInfor.zz = tixingbm.ToString();
                            paientInfor.tw = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tw")?.ItemResultChar;
                            paientInfor.ml = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ml")?.ItemResultChar;
                            paientInfor.hxpl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "hxpl")?.ItemResultChar;
                            paientInfor.zcssy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zcssy")?.ItemResultChar;
                            paientInfor.zcszy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zcszy")?.ItemResultChar;
                            paientInfor.ycssy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ycssy")?.ItemResultChar;
                            paientInfor.ycszy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ycszy")?.ItemResultChar;
                            paientInfor.tz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tz")?.ItemResultChar;
                            paientInfor.sg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sg")?.ItemResultChar;
                            paientInfor.yw = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yw")?.ItemResultChar;
                            paientInfor.tzzs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tzzs")?.ItemResultChar;
                            //老年人健康状态自我评估，1满意  2基本满意  3说不清楚  4不太满意  5不满意
                            var lnrjkztzwpg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrjkztzwpg")?.ItemResultChar;
                            int lnrjkz = 0;
                            switch (lnrjkztzwpg)
                            {
                                case "满意":
                                    lnrjkz = 1;
                                    break;
                                case "基本满意":
                                    lnrjkz = 2;
                                    break;
                                case "说不清楚":
                                    lnrjkz = 3;
                                    break;
                                case "不太满意":
                                    lnrjkz = 4;
                                    break;
                                case "不满意":
                                    lnrjkz = 5;
                                    break;
                            }
                            paientInfor.lnrjkztzwpg = lnrjkz.ToString();
                            //老年人生活自理能力自我评估，1 可自理（0～3分）2轻度依赖（4～8分）3 中度依赖（9～18分) 4 不能自理（≥19分）
                            var lnrshzlnlpg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrshzlnlpg")?.ItemResultChar;
                            int lnrshzln = 0;
                            switch (lnrshzlnlpg)
                            {
                                case "可自理（0～3分）":
                                    lnrshzln = 1;
                                    break;
                                case "轻度依赖（4～8分）":
                                    lnrshzln = 2;
                                    break;
                                case "中度依赖（9～18分)":
                                    lnrshzln = 3;
                                    break;
                                case "不能自理（≥19分）":
                                    lnrshzln = 4;
                                    break;
                            }
                            paientInfor.lnrshzlnlpg = lnrshzln.ToString();
                            //老年人认知功能，1粗筛阴性 2粗筛阳性
                            var lnrrzgn = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrrzgn")?.ItemResultChar;
                            int lnrr = 0;
                            switch (lnrrzgn)
                            {
                                case "粗筛阴性":
                                    lnrr = 1;
                                    break;
                                case "粗筛阳性":
                                    lnrr = 2;
                                    break;

                            }
                            paientInfor.lnrrzgn = lnrr.ToString();
                            paientInfor.lnrrzgnpf = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrrzgnpf")?.ItemResultChar;
                            //老年人情感状态初筛结果，1粗筛阴性2粗筛阳性
                            var lnrqgztpg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrqgztpg")?.ItemResultChar;
                            int lnrqgz = 0;
                            switch (lnrqgztpg)
                            {
                                case "粗筛阴性":
                                    lnrqgz = 1;
                                    break;
                                case "粗筛阳性":
                                    lnrqgz = 2;
                                    break;

                            }
                            paientInfor.lnrqgztpg = lnrqgz.ToString();
                            paientInfor.lnrqgztpf = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrqgztpf")?.ItemResultChar;
                            //体育锻炼 锻炼频率，1每天  2每周一次以上  3偶尔  4不锻炼
                            var tydl_dlpl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_dlpl")?.ItemResultChar;
                            int tydl = 0;
                            switch (tydl_dlpl)
                            {
                                case "每天":
                                    tydl = 1;
                                    break;
                                case "每周一次以上":
                                    tydl = 2;
                                    break;
                                case "偶尔 ":
                                    tydl = 3;
                                    break;
                                case "不锻炼":
                                    tydl = 4;
                                    break;

                            }
                            paientInfor.tydl_dlpl = lnrqgz.ToString();
                            paientInfor.tydl_jcdlsj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_jcdlsj")?.ItemResultChar;
                            paientInfor.tydl_mcdlsj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_mcdlsj")?.ItemResultChar;
                            paientInfor.tydl_dlfs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_dlfs")?.ItemResultChar;
                            //饮食习惯,1荤素均衡 2荤食为主 3素食为主 4嗜盐 5嗜油 6嗜糖
                            var ysxg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ysxg")?.ItemResultChar;
                            int ys = 0;
                            switch (ysxg)
                            {
                                case "荤素均衡":
                                    ys = 1;
                                    break;
                                case "荤食为主":
                                    ys = 2;
                                    break;
                                case "素食为主 ":
                                    ys = 3;
                                    break;
                                case "嗜盐":
                                    ys = 4;
                                    break;
                                case "嗜油":
                                    ys = 5;
                                    break;
                                case "嗜糖":
                                    ys = 6;
                                    break;

                            }
                            paientInfor.ysxg = ys.ToString();
                            //吸烟情况 吸烟状况，1从不吸烟　　　2已戒烟　　　 3吸烟 
                            var xyqk_xyzk = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_xyzk")?.ItemResultChar;
                            int xyqk = 0;
                            switch (xyqk_xyzk)
                            {
                                case "从不吸烟":
                                    xyqk = 1;
                                    break;
                                case "已戒烟":
                                    xyqk = 2;
                                    break;
                                case "吸烟 ":
                                    xyqk = 3;
                                    break;

                            }
                            paientInfor.xyqk_xyzk = xyqk.ToString();
                            paientInfor.xyqk_rxyl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_rxyl")?.ItemResultChar;
                            paientInfor.xyqk_ksxynl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_ksxynl")?.ItemResultChar;
                            paientInfor.xyqk_jynl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_jynl")?.ItemResultChar;
                            //饮酒频率，1从不  2偶尔  3经常  4每天
                            var yjqk_yjpl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_yjpl")?.ItemResultChar;
                            int yjqk = 0;
                            switch (yjqk_yjpl)
                            {
                                case "从不":
                                    yjqk = 1;
                                    break;
                                case "偶尔":
                                    yjqk = 2;
                                    break;
                                case "经常":
                                    yjqk = 3;
                                    break;
                                case "每天":
                                    yjqk = 4;
                                    break;

                            }
                            paientInfor.yjqk_yjpl = yjqk.ToString();
                            paientInfor.yjqk_ryjl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_ryjl")?.ItemResultChar;
                            //近一年内是否曾醉酒，1是  2否
                            var yjqk_sfzj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_sfzj")?.ItemResultChar;
                            int sfzj = 0;
                            switch (yjqk_sfzj)
                            {
                                case "是":
                                    sfzj = 1;
                                    break;
                                case "否":
                                    sfzj = 2;
                                    break;

                            }
                            paientInfor.yjqk_sfzj = sfzj.ToString();
                            //是否戒酒，1未戒酒  2已戒酒
                            var yjqk_sfjj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_sfjj")?.ItemResultChar;
                            int sfjj = 0;
                            switch (yjqk_sfzj)
                            {
                                case "未戒酒":
                                    sfjj = 1;
                                    break;
                                case "已戒酒":
                                    sfjj = 2;
                                    break;

                            }
                            paientInfor.yjqk_sfjj = sfjj.ToString();
                            //饮酒种类，1白酒2啤酒3红酒 4黄酒 9其他
                            var yjqk_yjzl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_yjzl")?.ItemResultChar;
                            int yjzl = 0;
                            switch (yjqk_yjzl)
                            {
                                case "白酒":
                                    yjzl = 1;
                                    break;
                                case "啤酒":
                                    yjzl = 2;
                                    break;
                                case "红酒":
                                    yjzl = 3;
                                    break;
                                case "黄酒":
                                    yjzl = 4;
                                    break;
                                case "其他":
                                    yjzl = 9;
                                    break;


                            }
                            paientInfor.yjqk_yjzl = yjzl.ToString();
                            paientInfor.yjqk_ksyjnl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_ksyjnl")?.ItemResultChar;
                            paientInfor.yjqk_jjnl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_jjnl")?.ItemResultChar;
                            //职业健康危害因素接触史，2无 1有
                            var zybwxys_yw = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_yw")?.ItemResultChar;
                            int _yw = 0;
                            switch (zybwxys_yw)
                            {
                                case "有":
                                    _yw = 1;
                                    break;
                                case "无":
                                    _yw = 2;
                                    break;

                            }
                            paientInfor.zybwxys_yw = _yw.ToString();
                            paientInfor.zybwxys_gz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_gz")?.ItemResultChar;
                            paientInfor.zybwxys_cysj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_cysj")?.ItemResultChar;
                            paientInfor.zybwxys_fc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fc")?.ItemResultChar;
                            //粉尘防护措施描述，2无 1有
                            var zybwxys_fcfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fcfhcsms")?.ItemResultChar;
                            int fcfhcsms = 0;
                            switch (zybwxys_fcfhcsms)
                            {
                                case "有":
                                    fcfhcsms = 1;
                                    break;
                                case "无":
                                    fcfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_fcfhcsms = fcfhcsms.ToString();
                            paientInfor.zybwxys_fswz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fswz")?.ItemResultChar;
                            paientInfor.zybwxys_fswzfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fswzfhcs")?.ItemResultChar;
                            //放射物质防护措施描述，2无 1有
                            var zybwxys_fswzfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fswzfhcsms")?.ItemResultChar;
                            int fswzfhcsms = 0;
                            switch (zybwxys_fswzfhcsms)
                            {
                                case "有":
                                    fswzfhcsms = 1;
                                    break;
                                case "无":
                                    fswzfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_fswzfhcsms = fswzfhcsms.ToString();
                            paientInfor.zybwxys_wlys = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_wlys")?.ItemResultChar;
                            paientInfor.zybwxys_wlysfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_wlysfhcs")?.ItemResultChar;
                            //物理因素防护措施描述，2无 1有
                            var zybwxys_wlysfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_wlysfhcsms")?.ItemResultChar;
                            int wlysfhcsms = 0;
                            switch (zybwxys_wlysfhcsms)
                            {
                                case "有":
                                    wlysfhcsms = 1;
                                    break;
                                case "无":
                                    wlysfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_wlysfhcsms = wlysfhcsms.ToString();
                            paientInfor.zybwxys_hxwz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_hxwz")?.ItemResultChar;
                            paientInfor.zybwxys_hxwzfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_hxwzfhcs")?.ItemResultChar;
                            //化学物质防护措施描述，2无 1有
                            var zybwxys_hxwzfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_hxwzfhcsms")?.ItemResultChar;
                            int _hxwzfhcsms = 0;
                            switch (zybwxys_hxwzfhcsms)
                            {
                                case "有":
                                    _hxwzfhcsms = 1;
                                    break;
                                case "无":
                                    _hxwzfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_hxwzfhcsms = _hxwzfhcsms.ToString();
                            paientInfor.zybwxys_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_qt")?.ItemResultChar;
                            paientInfor.zybwxys_qtfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_qtfhcs")?.ItemResultChar;
                            //其他防护措施描述，2无 1有
                            var zybwxys_qtfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_qtfhcsms")?.ItemResultChar;
                            int qtfhcsms = 0;
                            switch (zybwxys_hxwzfhcsms)
                            {
                                case "有":
                                    qtfhcsms = 1;
                                    break;
                                case "无":
                                    qtfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_qtfhcsms = qtfhcsms.ToString();
                            //口腔 口唇外观类别，1红润 2苍白 3发绀 4皲裂 5疱疹
                            var kq_kcwglb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kq_kcwglb")?.ItemResultChar;
                            int kcwglb = 0;
                            switch (kq_kcwglb)
                            {
                                case "红润":
                                    kcwglb = 1;
                                    break;
                                case "苍白":
                                    kcwglb = 2;
                                    break;
                                case "发绀":
                                    kcwglb = 3;
                                    break;
                                case "皲裂":
                                    kcwglb = 4;
                                    break;
                                case "疱疹":
                                    kcwglb = 5;
                                    break;


                            }
                            paientInfor.kq_kcwglb = kcwglb.ToString();
                            //齿列类别，1正常 2缺齿  3龋齿   4义齿(假牙)
                            var kq_cllb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kq_cllb")?.ItemResultChar;
                            int cllb = 0;
                            switch (kq_cllb)
                            {
                                case "正常":
                                    cllb = 1;
                                    break;
                                case "缺齿":
                                    cllb = 2;
                                    break;
                                case "龋齿 ":
                                    cllb = 3;
                                    break;
                                case "义齿(假牙)":
                                    cllb = 4;
                                    break;

                            }
                            paientInfor.kq_cllb = cllb.ToString();
                            //咽部检查结果，1无充血 2充血 3淋巴滤泡增生
                            var kq_ybjcjg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kq_ybjcjg")?.ItemResultChar;
                            int _ybjcjg = 0;
                            switch (kq_ybjcjg)
                            {
                                case "无充血":
                                    _ybjcjg = 1;
                                    break;
                                case "充血":
                                    _ybjcjg = 2;
                                    break;
                                case "淋巴滤泡增生 ":
                                    _ybjcjg = 3;
                                    break;

                            }
                            paientInfor.kq_ybjcjg = _ybjcjg.ToString();
                            paientInfor.sl_zyly = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_zyly")?.ItemResultChar;
                            paientInfor.sl_yyly = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_yyly")?.ItemResultChar;
                            paientInfor.sl_zyjz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_zyjz")?.ItemResultChar;
                            paientInfor.sl_yyjz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_yyjz")?.ItemResultChar;
                            //听力检测结果，1听见        2听不清或无法听见
                            var tljcjg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tljcjg")?.ItemResultChar;
                            int ljcjg = 0;
                            switch (tljcjg)
                            {
                                case "听见":
                                    ljcjg = 1;
                                    break;
                                case "听不清或无法听见":
                                    ljcjg = 2;
                                    break;

                            }
                            paientInfor.tljcjg = ljcjg.ToString();
                            //运动功能状态，1可顺利完成  2无法独立完成任何一个动作
                            var ydgnzt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ydgnzt")?.ItemResultChar;
                            int dgnzt = 0;
                            switch (ydgnzt)
                            {
                                case "可顺利完成":
                                    dgnzt = 1;
                                    break;
                                case "无法独立完成任何一个动作":
                                    dgnzt = 2;
                                    break;

                            }
                            paientInfor.ydgnzt = dgnzt.ToString();
                            //眼底，1正常2异常  3未检
                            var yd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yd")?.ItemResultChar;
                            int _yd = 0;
                            switch (yd)
                            {
                                case "正常":
                                    _yd = 1;
                                    break;
                                case "异常 ":
                                    _yd = 2;
                                    break;
                                case "未检 ":
                                    _yd = 3;
                                    break;
                            }
                            paientInfor.yd = _yd.ToString();
                            paientInfor.ydycms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ydycms")?.ItemResultChar;
                            //皮肤，01未见异常  02潮红 03苍白 04发绀 05黄染  06色素沉着 99其他 
                            var pf = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "pf")?.ItemResultChar;
                            int _pf = 0;
                            switch (pf)
                            {
                                case "未见异常 ":
                                    _pf = 01;
                                    break;
                                case "潮红 ":
                                    _pf = 02;
                                    break;
                                case "苍白 ":
                                    _pf = 03;
                                    break;
                                case "发绀":
                                    _pf = 04;
                                    break;
                                case "黄染":
                                    _pf = 05;
                                    break;
                                case "色素沉着":
                                    _pf = 06;
                                    break;
                                case "其他":
                                    _pf = 99;
                                    break;
                            }
                            paientInfor.pf = _pf.ToString();
                            paientInfor.pfqtms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "pfqtms")?.ItemResultChar;
                            //巩膜，1正常  2 黄染 3充血 9其他
                            var gm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gm")?.ItemResultChar;
                            int _gm = 0;
                            switch (gm)
                            {
                                case "正常":
                                    _gm = 1;
                                    break;
                                case "黄染":
                                    _gm = 2;
                                    break;
                                case "充血":
                                    _gm = 3;

                                    break;
                                case "其他":
                                    _gm = 9;
                                    break;

                            }
                            paientInfor.gm = _gm.ToString();
                            paientInfor.gmqtms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gmqtms")?.ItemResultChar;
                            //淋巴结检查，1未触及   2锁骨上   3腋窝   9其他 
                            var lbjjc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lbjjc")?.ItemResultChar;
                            int jjc = 0;
                            switch (lbjjc)
                            {
                                case "未触及 ":
                                    jjc = 1;
                                    break;
                                case "锁骨上":
                                    jjc = 2;
                                    break;
                                case "腋窝":
                                    jjc = 3;

                                    break;
                                case "其他":
                                    jjc = 9;
                                    break;

                            }
                            paientInfor.lbjjc = jjc.ToString();
                            paientInfor.lbjjcqtms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lbjjcqtms")?.ItemResultChar;
                            //肺桶状胸，1否　　2是
                            var f_tzx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_tzx")?.ItemResultChar;
                            int _tzx = 0;
                            switch (f_tzx)
                            {
                                case "否":
                                    _tzx = 1;
                                    break;
                                case "是":
                                    _tzx = 2;
                                    break;

                            }
                            paientInfor.f_tzx = _tzx.ToString();
                            //肺呼吸音，1正常  2异常
                            var f_hxy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_hxy")?.ItemResultChar;
                            int _hxy = 0;
                            switch (f_hxy)
                            {
                                case "正常":
                                    _hxy = 1;
                                    break;
                                case "异常":
                                    _hxy = 2;
                                    break;

                            }
                            paientInfor.f_hxy = _hxy.ToString();
                            paientInfor.f_hxyyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_hxyyc")?.ItemResultChar;
                            //肺啰音，1无 　 2干罗音  3湿罗音 4其他 
                            var f_ly = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_ly")?.ItemResultChar;
                            int _ly = 0;
                            switch (f_ly)
                            {
                                case "无":
                                    _ly = 1;
                                    break;
                                case "干罗音":
                                    _ly = 2;
                                    break;
                                case "湿罗音":
                                    _ly = 3;
                                    break;
                                case "其他":
                                    _ly = 4;
                                    break;

                            }
                            paientInfor.f_ly = _ly.ToString();
                            paientInfor.f_lyqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_lyqt")?.ItemResultChar;
                            paientInfor.xz_xl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xl")?.ItemResultChar;
                            //心脏心律类别，1齐  2不齐  3绝对不齐
                            var xz_xllb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xllb")?.ItemResultChar;
                            int _xllb = 0;
                            switch (xz_xllb)
                            {
                                case "齐":
                                    _xllb = 1;
                                    break;
                                case "不齐":
                                    _xllb = 2;
                                    break;
                                case "绝对不齐":
                                    _xllb = 3;
                                    break;
                            }
                            paientInfor.xz_xllb = _xllb.ToString();
                            //心脏杂音，1有  2无 
                            var xz_zy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zy")?.ItemResultChar;
                            int z_zy = 0;
                            switch (xz_zy)
                            {
                                case "有":
                                    z_zy = 1;
                                    break;
                                case "无":
                                    z_zy = 2;
                                    break;

                            }
                            paientInfor.xz_zy = z_zy.ToString();
                            paientInfor.xz_zyms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zyms")?.ItemResultChar;
                            //腹部压痛，1有  2无 
                            var fb_yt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_yt")?.ItemResultChar;
                            int b_yt = 0;
                            switch (fb_yt)
                            {
                                case "有":
                                    b_yt = 1;
                                    break;
                                case "无":
                                    b_yt = 2;
                                    break;

                            }
                            paientInfor.fb_yt = b_yt.ToString();
                            paientInfor.fb_ytms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_ytms")?.ItemResultChar;
                            //腹部包块，1有  2无 
                            var fb_bk = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_bk")?.ItemResultChar;
                            int b_bk = 0;
                            switch (fb_bk)
                            {
                                case "有":
                                    b_bk = 1;
                                    break;
                                case "无":
                                    b_bk = 2;
                                    break;

                            }
                            paientInfor.fb_bk = b_bk.ToString();
                            paientInfor.fb_bkms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_bkms")?.ItemResultChar;
                            //腹部肝大，1有  2无
                            var fb_gd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_gd")?.ItemResultChar;
                            int b_gd = 0;
                            switch (fb_gd)
                            {
                                case "有":
                                    b_gd = 1;
                                    break;
                                case "无":
                                    b_gd = 2;
                                    break;

                            }
                            paientInfor.fb_gd = b_gd.ToString();
                            paientInfor.fb_pdms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_pdms")?.ItemResultChar;
                            //腹部移动性浊音，1有  2无 
                            var fb_ydxzy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_ydxzy")?.ItemResultChar;
                            int _ydxzy = 0;
                            switch (fb_ydxzy)
                            {
                                case "有":
                                    _ydxzy = 1;
                                    break;
                                case "无":
                                    _ydxzy = 2;
                                    break;

                            }
                            paientInfor.fb_ydxzy = _ydxzy.ToString();
                            paientInfor.fb_ydxzyms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_ydxzyms")?.ItemResultChar;
                            //下肢水肿检查，1无   2单侧   3双侧不对称   4双侧对称
                            var xzszjc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xzszjc")?.ItemResultChar;
                            int _szjc = 0;
                            switch (xzszjc)
                            {
                                case "无":
                                    _szjc = 1;
                                    break;
                                case "单侧":
                                    _szjc = 2;
                                    break;
                                case "双侧不对称":
                                    _szjc = 3;
                                    break;
                                case "双侧对称":
                                    _szjc = 4;
                                    break;

                            }
                            paientInfor.xzszjc = _szjc.ToString();
                            //肛门指诊，1未及异常 2 触痛　  3包块  4前列腺异常 9其他
                            var gmzz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gmzz")?.ItemResultChar;
                            int _gmzz = 0;
                            switch (gmzz)
                            {
                                case "未及异常":
                                    _gmzz = 1;
                                    break;
                                case "触痛":
                                    _gmzz = 2;
                                    break;
                                case "包块":
                                    _gmzz = 3;
                                    break;
                                case "前列腺异常":
                                    _gmzz = 4;
                                    break;
                                case "其他":
                                    _gmzz = 9;
                                    break;

                            }
                            paientInfor.gmzz = _gmzz.ToString();
                            paientInfor.gmzzqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gmzzqt")?.ItemResultChar;
                            //足背动脉搏动，1未触及2触及双侧对称3触及左侧弱或消失4触及右侧弱或消失
                            var zbdmbd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zbdmbd")?.ItemResultChar;
                            int _zbdmbd = 0;
                            switch (zbdmbd)
                            {
                                case "未触及":
                                    _zbdmbd = 1;
                                    break;
                                case "触及双侧对称":
                                    _zbdmbd = 2;
                                    break;
                                case "触及左侧弱或消失":
                                    _zbdmbd = 3;
                                    break;
                                case "触及右侧弱或消失":
                                    _zbdmbd = 4;
                                    break;

                            }
                            paientInfor.zbdmbd = _zbdmbd.ToString();
                            //乳腺，1未见异常 2乳房切除 3异常泌乳4乳腺包块 9其他 
                            var rx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "rx")?.ItemResultChar;
                            int _rx = 0;
                            switch (rx)
                            {
                                case "未见异常":
                                    _rx = 1;
                                    break;
                                case "乳房切除":
                                    _rx = 2;
                                    break;
                                case "异常泌乳":
                                    _rx = 3;
                                    break;
                                case "乳腺包块":
                                    _rx = 4;
                                    break;
                                case "其他":
                                    _rx = 9;
                                    break;

                            }
                            paientInfor.rx = _rx.ToString();
                            paientInfor.rxqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "rxqt")?.ItemResultChar;
                            //妇科检查 外阴，1未见异常   2异常    3拒检
                            var fkjc_wy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_wy")?.ItemResultChar;
                            int _wy = 0;
                            switch (fkjc_wy)
                            {
                                case "未见异常":
                                    _wy = 1;
                                    break;
                                case "异常":
                                    _wy = 2;
                                    break;
                                case "拒检":
                                    _wy = 3;
                                    break;

                            }
                            paientInfor.fkjc_wy = _wy.ToString();
                            paientInfor.fkjc_wyyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_wyyc")?.ItemResultChar;
                            //妇科检查 宫颈，1未见异常   2异常    3拒检
                            var fkjc_gj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gj")?.ItemResultChar;
                            int _gj = 0;
                            switch (fkjc_gj)
                            {

                                case "未见异常":
                                    _gj = 1;
                                    break;
                                case "异常":
                                    _gj = 2;
                                    break;
                                case "拒检":
                                    _gj = 3;
                                    break;

                            }
                            paientInfor.fkjc_gj = _gj.ToString();
                            paientInfor.fkjc_gjyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gjyc")?.ItemResultChar;
                            //妇科检查 宫体，1未见异常   2异常    3拒检
                            var fkjc_gt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gj")?.ItemResultChar;
                            int _gt = 0;
                            switch (fkjc_gt)
                            {
                                case "未见异常":
                                    _gt = 1;
                                    break;
                                case "异常":
                                    _gt = 2;
                                    break;
                                case "拒检":
                                    _gt = 3;
                                    break;

                            }
                            paientInfor.fkjc_gt = _gt.ToString();
                            paientInfor.fkjc_gtyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gtyc")?.ItemResultChar;
                            //妇科检查 子宫附件，1未见异常   2异常    3拒检
                            var fkjc_zgfj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_zgfj")?.ItemResultChar;
                            int _zgfj = 0;
                            switch (fkjc_zgfj)
                            {
                                case "未见异常":
                                    _zgfj = 1;
                                    break;
                                case "异常":
                                    _zgfj = 2;
                                    break;
                                case "拒检":
                                    _zgfj = 3;
                                    break;

                            }
                            paientInfor.fkjc_zgfj = _zgfj.ToString();
                            paientInfor.fkjc_zgfjyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_zgfjyc")?.ItemResultChar;
                            paientInfor.ct_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ct_qt")?.ItemResultChar;
                            //血常规，1已检   2未检    3拒检
                            var xcg_ndbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xhdb")?.ItemId;
                            var xcg_nttid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_bxb")?.ItemId;
                            var xcg_qtid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xxb")?.ItemId;
                            var xcg_nxxid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_qt")?.ItemId;

                            var xcg_ndbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_ndbid).FirstOrDefault()))?.CheckState;
                            var xcg_nttGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_nttid).FirstOrDefault()))?.CheckState;
                            var xcg_qtGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_qtid).FirstOrDefault()))?.CheckState;
                            var xcg_nxxGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_nxxid).FirstOrDefault()))?.CheckState;
            
                            if (xcg_ndbGroups == 1 || xcg_nttGroups == 1 || xcg_qtGroups == 1 || xcg_nxxGroups == 1)
                            {
                                paientInfor.xcg = "2";
                            }
                            if (xcg_ndbGroups == 2 || xcg_nttGroups == 2 || xcg_qtGroups == 2 || xcg_nxxGroups == 2)
                            {
                                paientInfor.xcg = "1";
                            }
                            if (xcg_ndbGroups == 3 || xcg_nttGroups == 3 || xcg_qtGroups == 3 || xcg_nxxGroups == 3)
                            {
                                paientInfor.xcg = "1";
                            }
                            if (xcg_ndbGroups == 4 || xcg_nttGroups == 4 || xcg_qtGroups == 4 || xcg_nxxGroups == 4)
                            {
                                paientInfor.xcg = "3";
                            }
                            paientInfor.xcg_xhdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xhdb")?.ItemResultChar;
                            paientInfor.xcg_bxb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_bxb")?.ItemResultChar;
                            paientInfor.xcg_xxb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xxb")?.ItemResultChar;
                            paientInfor.xcg_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_qt")?.ItemResultChar;

                            ////尿常规，1已检   2未检    3拒检
                            var ncg_ndbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ndb")?.ItemId;
                            var ncg_nttid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ntt")?.ItemId;
                            var ncg_qtid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_qt")?.ItemId;
                            var ncg_nxxid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nxx")?.ItemId;
                            var ncg_ntid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nt")?.ItemId;

                            var ncg_ntGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_ntid).FirstOrDefault()))?.CheckState;
                            var ncg_ndbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_ndbid).FirstOrDefault()))?.CheckState;
                            var ncg_nttGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_nttid).FirstOrDefault()))?.CheckState;
                            var ncg_qtGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_qtid).FirstOrDefault()))?.CheckState;
                            var ncg_nxxGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_nxxid).FirstOrDefault()))?.CheckState;
                            if (ncg_ndbGroups == 1 || ncg_nttGroups == 1 || ncg_qtGroups == 1 || ncg_nxxGroups == 1 || ncg_ntGroups==1)
                            {
                                paientInfor.ncg = "2";
                            }
                            if (ncg_ndbGroups == 2 || ncg_nttGroups == 2 || ncg_qtGroups == 2 || ncg_nxxGroups == 2 || ncg_ntGroups == 2)
                            {
                                paientInfor.ncg = "1";
                            }
                            if (ncg_ndbGroups == 3 || ncg_nttGroups == 3 || ncg_qtGroups == 3 || ncg_nxxGroups == 3 || ncg_ntGroups == 3)
                            {
                                paientInfor.ncg = "1";
                            }
                            if (ncg_ndbGroups == 4 || ncg_nttGroups == 4 || ncg_qtGroups == 4|| ncg_nxxGroups == 4 || ncg_ntGroups == 4)
                            {
                                paientInfor.ncg = "3";
                            }
                            paientInfor.ncg_ndb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ndb")?.ItemResultChar;
                            paientInfor.ncg_nt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nt")?.ItemResultChar;
                            paientInfor.ncg_ntt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ntt")?.ItemResultChar;
                            paientInfor.ncg_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_qt")?.ItemResultChar;
                            paientInfor.ncg_nxx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nxx")?.ItemResultChar;
                            paientInfor.ncg_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_qt")?.ItemResultChar;
                            paientInfor.nwlbdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "nwlbdb")?.ItemResultChar;
                            paientInfor.kfxt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kfxt")?.ItemResultChar;
                            paientInfor.thxhdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "thxhdb")?.ItemResultChar;
                            //大便潜血，1阴性  2阳性 3未检
                            var dbqx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "dbqx")?.ItemResultChar;
                            int _dbqx = 0;
                            switch (dbqx)
                            {
                                case "阴性":
                                    _dbqx = 1;
                                    break;
                                case "阳性":
                                    _dbqx = 2;
                                    break;
                                case "未检":
                                    _dbqx = 3;
                                    break;

                            }
                            paientInfor.dbqx = _dbqx.ToString();
                            //乙肝表面抗原(HBsAg)，1阴性  2阳性 3未检
                            var ygbmky = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ygbmky")?.ItemResultChar;
                            int _ygbmky = 0;
                            switch (ygbmky)
                            {
                                case "阴性":
                                    _ygbmky = 1;
                                    break;
                                case "阳性":
                                    _ygbmky = 2;
                                    break;
                                case "未检":
                                    _ygbmky = 3;
                                    break;

                            }
                            paientInfor.ygbmky = _ygbmky.ToString();
                            //肝功能，1已检   2未检    3拒检
                            var ggn_xqgbzamid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgbzam")?.ItemId;
                            var ggn_xqgczamid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgczam")?.ItemId;
                            var ggn_bdbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_bdb")?.ItemId;
                            var ggn_zdhsid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_zdhs")?.ItemId;
                            var ggn_jhdhsid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_jhdhs")?.ItemId;

                            var ggn_xqgbzamGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_xqgbzamid).FirstOrDefault()))?.CheckState;
                            var ggn_xqgczamGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_xqgczamid).FirstOrDefault()))?.CheckState;
                            var ggn_bdbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_bdbid).FirstOrDefault()))?.CheckState;
                            var ggn_zdhsGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_zdhsid).FirstOrDefault()))?.CheckState;
                            var ggn_jhdhsGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_jhdhsid).FirstOrDefault()))?.CheckState;
                            if (ggn_xqgbzamGroups == 1 || ggn_xqgczamGroups == 1 || ggn_bdbGroups == 1 || ggn_zdhsGroups == 1 || ggn_jhdhsGroups == 1)
                            {
                                paientInfor.ggn = "2";
                            }
                            if (ggn_xqgbzamGroups == 2 || ggn_xqgczamGroups == 2 || ggn_bdbGroups == 2 || ggn_zdhsGroups == 2 || ggn_jhdhsGroups == 2)
                            {
                                paientInfor.ggn = "1";
                            }
                            if (ggn_xqgbzamGroups == 3 || ggn_xqgczamGroups == 3 || ggn_bdbGroups == 3 || ggn_zdhsGroups == 3 || ggn_jhdhsGroups == 3)
                            {
                                paientInfor.ggn = "1";
                            }
                            if (ggn_xqgbzamGroups == 4 || ggn_xqgczamGroups == 4 || ggn_bdbGroups == 4 || ggn_zdhsGroups == 4 || ggn_jhdhsGroups == 4)
                            {
                                paientInfor.ggn = "3";
                            }


                            paientInfor.ggn_xqgbzam = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgbzam")?.ItemResultChar;
                            paientInfor.ggn_xqgczam = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgczam")?.ItemResultChar;
                            paientInfor.ggn_bdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_bdb")?.ItemResultChar;
                            paientInfor.ggn_zdhs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_zdhs")?.ItemResultChar;
                            paientInfor.ggn_jhdhs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_jhdhs")?.ItemResultChar;
                            //肾功能，1已检   2未检    3拒检
                            var sgn_xqjgid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xqjg")?.ItemId;
                            var sgn_xnsdid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnsd")?.ItemId;
                            var sgn_xjndid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xjnd")?.ItemId;
                            var sgn_xnndid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnnd")?.ItemId;

                            var sgn_xqjgGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xqjgid).FirstOrDefault()))?.CheckState;
                            var sgn_xnsdGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xnsdid).FirstOrDefault()))?.CheckState;
                            var sgn_xjndGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xjndid).FirstOrDefault()))?.CheckState;
                            var sgn_xnndGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xnndid).FirstOrDefault()))?.CheckState;
                            if (sgn_xqjgGroups == 1 || sgn_xnsdGroups == 1 || sgn_xjndGroups == 1 || sgn_xnndGroups == 1)
                            {
                                paientInfor.sgn = "2";
                            }
                            if (sgn_xqjgGroups == 2 || sgn_xnsdGroups == 2 || sgn_xjndGroups == 2 || sgn_xnndGroups == 2)
                            {
                                paientInfor.sgn = "1";
                            }
                            if (sgn_xqjgGroups == 3 || sgn_xnsdGroups == 3 || sgn_xjndGroups == 3 || sgn_xnndGroups == 3)
                            {
                                paientInfor.sgn = "1";
                            }
                            if (sgn_xqjgGroups == 4 || sgn_xnsdGroups == 4 || sgn_xjndGroups == 4 || sgn_xnndGroups == 4)
                            {
                                paientInfor.sgn = "3";
                            }
                            paientInfor.sgn_xqjg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xqjg")?.ItemResultChar;
                            paientInfor.sgn_xnsd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnsd")?.ItemResultChar;
                            paientInfor.sgn_xjnd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xjnd")?.ItemResultChar;
                            paientInfor.sgn_xnnd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnnd")?.ItemResultChar;
                            //血脂，1已检   2未检    3拒检
                            var xz_zdgczid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zdgcz")?.ItemId;
                            var xz_gyszid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_gysz")?.ItemId;
                            var xz_xqdmdzdbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqdmdzdb")?.ItemId;
                            var xz_xqgmdzdbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqgmdzdb")?.ItemId;

                            var xz_zdgczGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_zdgczid).FirstOrDefault()))?.CheckState;
                            var xz_gyszGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_gyszid).FirstOrDefault()))?.CheckState;
                            var xz_xqdmdzdbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_xqdmdzdbid).FirstOrDefault()))?.CheckState;
                            var xz_xqgmdzdbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_xqgmdzdbid).FirstOrDefault()))?.CheckState;
                            if (xz_zdgczGroups == 1 || xz_gyszGroups == 1 || xz_xqdmdzdbGroups == 1 || xz_xqgmdzdbGroups == 1)
                            {
                                paientInfor.xz = "2";
                            }
                            if (xz_zdgczGroups == 2 || xz_gyszGroups == 2 || xz_xqdmdzdbGroups == 2 || xz_xqgmdzdbGroups == 2)
                            {
                                paientInfor.xz = "1";
                            }
                            if (xz_zdgczGroups == 3 || xz_gyszGroups == 3 || xz_xqdmdzdbGroups == 3 || xz_xqgmdzdbGroups == 3)
                            {
                                paientInfor.xz = "1";
                            }
                            if (xz_zdgczGroups == 4 || xz_gyszGroups == 4 || xz_xqdmdzdbGroups == 4 || xz_xqgmdzdbGroups == 4)
                            {
                                paientInfor.xz = "3";
                            }
                            paientInfor.xz_zdgcz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zdgcz")?.ItemResultChar;
                            paientInfor.xz_gysz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_gysz")?.ItemResultChar;
                            paientInfor.xz_xqdmdzdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqdmdzdb")?.ItemResultChar;
                            paientInfor.xz_xqgmdzdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqgmdzdb")?.ItemResultChar;
                            //心电图，1正常  2异常  3未检
                            var xdtycid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xdtyc")?.ItemId;
                            var xdtycGroup = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xdtycid).FirstOrDefault()))?.CheckState;
                            var xdtycGroups = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xdtyc")?.Symbol;

                            if (xdtycGroups == "P" )
                            {
                                paientInfor.xdt = "2";
                            }
                            if (xdtycGroups != "P" )
                            {
                                paientInfor.xdt = "1";
                            }
                            if (xdtycGroup == 1 )
                            {
                                paientInfor.xdt = "3";
                            }
                        
                            paientInfor.xdtyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xdtyc")?.ItemResultChar;
                            //X射线，1正常  2异常  3未检
                            var xsxycid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xsxyc")?.ItemId;
                            var xsxycGroup = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xsxycid).FirstOrDefault()))?.CheckState;
                            var xsxycGroups = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xsxyc")?.Symbol;

                            if (xsxycGroups == "M" )
                            {
                                paientInfor.xsx = "1";
                            }
                            if (xsxycGroups == "H" || xsxycGroups == "HH" || xsxycGroups == "L" || xsxycGroups == "LL" || xsxycGroups == "P")
                            {
                                paientInfor.xsx = "2";
                            }
                            if (xsxycGroup == 1)
                            {
                                paientInfor.xsx = "3";
                            }
                            paientInfor.xsxyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xsxyc")?.ItemResultChar;
                            //B超，1正常  2异常  3未检
                            var bchaoycid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "bchaoyc")?.ItemId;
                            var bchaoycGroup = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == bchaoycid).FirstOrDefault()))?.CheckState;
                            var bchaoycGroups = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "bchaoyc")?.Symbol;

                            if (bchaoycGroups == "M")
                            {
                                paientInfor.bchao = "1";
                            }
                            if (bchaoycGroups == "H" || bchaoycGroups == "HH" || bchaoycGroups == "L" || bchaoycGroups == "LL" || bchaoycGroups == "P")
                            {
                                paientInfor.bchao = "2";
                            }
                            if (bchaoycGroup == 1)
                            {
                                paientInfor.bchao = "3";
                            }
                            paientInfor.bchaoyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "bchaoyc")?.ItemResultChar;
                            //宫颈涂片，1正常  2异常  3未检
                            var gjtp = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gjtp")?.ItemResultChar;
                            int _gjtp = 0;
                            switch (gjtp)
                            {
                                case "正常":
                                    _gjtp = 1;
                                    break;
                                case "异常 ":
                                    _gjtp = 2;
                                    break;
                                case "未检":
                                    _gjtp = 3;
                                    break;
                            }
                            paientInfor.gjtp = _gjtp.ToString();
                            paientInfor.gjtpyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gjtpyc")?.ItemResultChar;
                            paientInfor.fzjc_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fzjc_qt")?.ItemResultChar;
                            //脑血管疾病，1未发现  2缺血性卒中  3脑出血 4蛛网膜下腔出血  5短暂性脑缺血发作  6其他
                            var nxgjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "nxgjb")?.ItemResultChar;
                            int _nxgjb = 0;
                            switch (nxgjb)
                            {
                                case "未发现":
                                    _nxgjb = 1;
                                    break;
                                case "缺血性卒中":
                                    _nxgjb = 2;
                                    break;
                                case "脑出血":
                                    _nxgjb = 3;
                                    break;
                                case "蛛网膜下腔出血":
                                    _nxgjb = 3;
                                    break;
                                case "短暂性脑缺血发作":
                                    _nxgjb = 3;
                                    break;
                                case "其他":
                                    _nxgjb = 3;
                                    break;
                            }
                            paientInfor.nxgjb = _nxgjb.ToString();
                            paientInfor.nxgjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "nxgjbqt")?.ItemResultChar;
                            //肾脏疾病，1未发现  2糖尿病肾病  3肾功能衰竭  4急性肾炎  5慢性肾炎   6其他
                            var szjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "szjb")?.ItemResultChar;
                            int _szjb = 0;
                            switch (szjb)
                            {
                                case "未发现":
                                    _szjb = 1;
                                    break;
                                case "糖尿病肾病":
                                    _szjb = 2;
                                    break;
                                case "肾功能衰竭":
                                    _szjb = 3;
                                    break;
                                case "急性肾炎":
                                    _szjb = 4;
                                    break;
                                case "慢性肾炎":
                                    _szjb = 5;
                                    break;
                                case "其他":
                                    _szjb = 6;
                                    break;
                            }
                            paientInfor.szjb = _szjb.ToString();
                            paientInfor.szjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "szjbqt")?.ItemResultChar;
                            //心脏疾病，1未发现  2心肌梗死  3心绞痛  4冠状动脉血运重建 5充血性心力衰竭 6 心前区疼痛  7其他 
                            var xzjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xzjb")?.ItemResultChar;
                            int _xzjb = 0;
                            switch (xzjb)
                            {
                                case "未发现":
                                    _xzjb = 1;
                                    break;
                                case "心肌梗死":
                                    _xzjb = 2;
                                    break;
                                case "心绞痛":
                                    _xzjb = 3;
                                    break;
                                case "冠状动脉血运重建":
                                    _xzjb = 4;
                                    break;
                                case "充血性心力衰竭":
                                    _xzjb = 5;
                                    break;
                                case "心前区疼痛":
                                    _xzjb = 6;
                                    break;
                                case "其他":
                                    _xzjb = 7;
                                    break;
                            }
                            paientInfor.xzjb = _xzjb.ToString();
                            paientInfor.xzjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xzjbqt")?.ItemResultChar;
                            //血管疾病，1未发现 2夹层动脉瘤  3动脉闭塞性疾病 4其他 
                            var xgjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xgjb")?.ItemResultChar;
                            int _xgjb = 0;
                            switch (xgjb)
                            {
                                case "未发现":
                                    _xgjb = 1;
                                    break;
                                case "夹层动脉瘤":
                                    _xgjb = 2;
                                    break;
                                case "动脉闭塞性疾病":
                                    _xgjb = 3;
                                    break;
                                case "其他":
                                    _xgjb = 4;
                                    break;
         
                            }
                            paientInfor.xgjb = _xgjb.ToString();
                            paientInfor.xgjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xgjbqt")?.ItemResultChar;
                            //眼部疾病，1未发现 2视网膜出血或渗出 3视乳头水肿 4白内障 5其他
                            var ybjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ybjb")?.ItemResultChar;
                            int _ybjb = 0;
                            switch (ybjb)
                            {
                                case "未发现":
                                    _ybjb = 1;
                                    break;
                                case "视网膜出血或渗出":
                                    _ybjb = 2;
                                    break;
                                case "视乳头水肿":
                                    _ybjb = 3;
                                    break;
                                case "白内障":
                                    _ybjb = 4;
                                    break;
                                case "其他":
                                    _ybjb = 5;
                                    break;

                            }
                            paientInfor.ybjb = _ybjb.ToString();
                            paientInfor.ybjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ybjbqt")?.ItemResultChar;
                            //神经系统疾病，1未发现 2有    
                            var sjxtjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sjxtjb")?.ItemResultChar;
                            int _sjxtjb = 0;
                            switch (sjxtjb)
                            {
                                case "未发现":
                                    _sjxtjb = 1;
                                    break;
                                case "有":
                                    _sjxtjb = 2;
                                    break;
                            }
                            paientInfor.sjxtjb = _sjxtjb.ToString();
                            paientInfor.sjxtjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sjxtjbqt")?.ItemResultChar;
                            //其他系统疾病，1未发现 2有     
                            var qtxtjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "qtxtjb")?.ItemResultChar;
                            int _qtxtjb = 0;
                            switch (qtxtjb)
                            {
                                case "未发现":
                                    _qtxtjb = 1;
                                    break;
                                case "有":
                                    _qtxtjb = 2;
                                    break;
                            }
                            paientInfor.qtxtjb = _qtxtjb.ToString();
                            paientInfor.qtxtjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "qtxtjbqt")?.ItemResultChar;
                            //健康评价是否有异常，1体检无异常 　2有异常     
                            var jtywycbm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jtywycbm")?.ItemResultChar;
                            int _jtywycbm = 0;
                            switch (jtywycbm)
                            {
                                case "体检无异常":
                                    _jtywycbm = 1;
                                    break;
                                case "有异常":
                                    _jtywycbm = 2;
                                    break;
                            }
                            paientInfor.jtywycbm = _jtywycbm.ToString();
                            paientInfor.yc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yc")?.ItemResultChar;
                            paientInfor.jkzdbm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jkzdbm")?.ItemResultChar;
                            //危险因素控制，1戒烟  2健康饮酒 3饮食 4锻炼 5减体重 6建议接种疫苗 9其他 
                            var wxyskzbm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "wxyskzbm")?.ItemResultChar;
                            int _wxyskzbm = 0;
                            switch (wxyskzbm)
                            {
                                case "戒烟":
                                    _wxyskzbm = 1;
                                    break;
                                case "健康饮酒":
                                    _wxyskzbm = 2;
                                    break;
                                case "饮食":
                                    _wxyskzbm = 3;
                                    break;
                                case "锻炼":
                                    _wxyskzbm = 4;
                                    break;
                                case "减体重":
                                    _wxyskzbm = 5;
                                    break;
                                case "建议接种疫苗":
                                    _wxyskzbm = 6;
                                    break;
                                case "其他":
                                    _wxyskzbm = 9;
                                    break;
                            }
                            paientInfor.wxyskzbm = _wxyskzbm.ToString();
                            paientInfor.jytz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jytz")?.ItemResultChar;
                            paientInfor.jyjzym = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jyjzym")?.ItemResultChar;
                            paientInfor.qtjy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "qtjy")?.ItemResultChar;
                            paientInfor.lnrShzlnlpgzb = "";
                            paientInfor.yyqk = "";
                            paientInfor.zyqk = "";
                            paientInfor.jtbsqk = "";
                            paientInfor.fmyqk = "";
                            JbxxIGetDto.patient = paientInfor;
                            string jsonstrss = JsonConvert.SerializeObject(JbxxIGetDto).Replace("NULL", "").Replace("null", "");
                            // Logger.Debug(jsonstr);

                            var GWURLs = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "浉河区公卫数据上传地址")?.Remarks.ToString();
                            var UpdateURL = GWURL + "updateJkdaTjBgjlInfo";
                            var retss = PostUPTReturnFunction(jsonstrs, UpdateURL);
                            if (retss.success == "false")
                            {
                                outResult.Code = 0;
                                outResult.ErrInfo += retss.message + ",";
                            }
                            
                            #endregion
                        }
                        else
                        {
                            //新增
                            #region 赋值
                          

                            //addJkdaTjBgjlInfo addJkdaTjBgjlInfo = new addJkdaTjBgjlInfo();

                            getJkdaGrJbxxIGetAddDto JbxxIGetDto = new getJkdaGrJbxxIGetAddDto();

                            doctorJkdaGrJbxxIGetDto doctorss = new doctorJkdaGrJbxxIGetDto();
                            PaientJkdaGrJbxxIGetDto paientss = new PaientJkdaGrJbxxIGetDto();
                            doctorss.jgdm = input.DoctorJgdm /*"411502019"*/;//input.DoctorJgdm
                            doctorss.token = ret.data.token;
                            doctorss.loginId = ret.data.loginId;
                            paientss.sfzh = cusreg.Customer.IDCardNo;
                            JbxxIGetDto.doctor = doctorss;
                           
                            
                            PaientAdd paientInfor = new PaientAdd();
                            paientInfor.ytjid = cusreg.CustomerBM.ToString();
                            paientInfor.sfzh = cusreg.Customer.IDCardNo;
                            if (cusreg.LoginDate != null)
                            {
                                paientInfor.tjsj = cusreg.LoginDate.Value.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                paientInfor.tjsj = "";
                            }


                            //体检医生
                            if (cusreg.CSEmployeeBM == null)
                            {
                                var Username = _userRepository.FirstOrDefault(o => o.Id == cusreg.CreatorUserId).UserName;
                                paientInfor.tjys = Username;
                            }
                            else
                            {
                                paientInfor.tjys = cusreg.CSEmployeeBM.Name;
                            }

                            paientInfor.yljgdm = input.DoctorJgdm;
                            //1无症状 2头痛 3头晕 4心悸 5胸闷 6胸痛 7慢性咳嗽 8咳痰 9呼吸困难 10多饮 11多尿  12体重下降  13乏力 14关节肿痛15视力模糊16手脚麻木 17尿急18尿痛 19便秘 20腹泻21恶心呕吐22眼花 23耳鸣 24乳房胀痛 25其他
                            var zz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zz")?.ItemResultChar;
                            int tixingbm = 0;
                            switch (zz)
                            {
                                case "无症状":
                                    tixingbm = 1;
                                    break;
                                case "头痛":
                                    tixingbm = 2;
                                    break;
                                case "头晕":
                                    tixingbm = 3;
                                    break;
                                case "心悸":
                                    tixingbm = 4;
                                    break;
                                case "胸闷":
                                    tixingbm = 5;
                                    break;
                                case "胸痛":
                                    tixingbm = 6;
                                    break;
                                case "慢性咳嗽":
                                    tixingbm = 7;
                                    break;
                                case "咳痰":
                                    tixingbm = 8;
                                    break;
                                case "呼吸困难":
                                    tixingbm = 9;
                                    break;
                                case "多饮":
                                    tixingbm = 10;
                                    break;
                                case "多尿":
                                    tixingbm = 11;
                                    break;
                                case "体重下降":
                                    tixingbm = 12;
                                    break;
                                case "乏力":
                                    tixingbm = 13;
                                    break;
                                case "关节肿痛":
                                    tixingbm = 14;
                                    break;
                                case "视力模糊":
                                    tixingbm = 15;
                                    break;
                                case "手脚麻木":
                                    tixingbm = 16;
                                    break;
                                case "尿急":
                                    tixingbm = 17;
                                    break;
                                case "尿痛":
                                    tixingbm = 18;
                                    break;
                                case "便秘":
                                    tixingbm = 19;
                                    break;
                                case "腹泻":
                                    tixingbm = 20;
                                    break;
                                case "恶心呕吐":
                                    tixingbm = 21;
                                    break;
                                case "眼花":
                                    tixingbm = 22;
                                    break;
                                case "耳鸣":
                                    tixingbm = 23;
                                    break;
                                case "乳房胀痛":
                                    tixingbm = 24;
                                    break;
                                case "其他":
                                    tixingbm = 25;
                                    break;


                            }
                            paientInfor.zz = tixingbm.ToString();
                            paientInfor.tw = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tw")?.ItemResultChar;
                            paientInfor.ml = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ml")?.ItemResultChar;
                            paientInfor.hxpl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "hxpl")?.ItemResultChar;
                            paientInfor.zcssy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zcssy")?.ItemResultChar;
                            paientInfor.zcszy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zcszy")?.ItemResultChar;
                            paientInfor.ycssy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ycssy")?.ItemResultChar;
                            paientInfor.ycszy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ycszy")?.ItemResultChar;
                            paientInfor.tz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tz")?.ItemResultChar;
                            paientInfor.sg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sg")?.ItemResultChar;
                            paientInfor.yw = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yw")?.ItemResultChar;
                            paientInfor.tzzs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tzzs")?.ItemResultChar;
                            //老年人健康状态自我评估，1满意  2基本满意  3说不清楚  4不太满意  5不满意
                            var lnrjkztzwpg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrjkztzwpg")?.ItemResultChar;
                            int lnrjkz = 0;
                            switch (lnrjkztzwpg)
                            {
                                case "满意":
                                    lnrjkz = 1;
                                    break;
                                case "基本满意":
                                    lnrjkz = 2;
                                    break;
                                case "说不清楚":
                                    lnrjkz = 3;
                                    break;
                                case "不太满意":
                                    lnrjkz = 4;
                                    break;
                                case "不满意":
                                    lnrjkz = 5;
                                    break;
                            }
                            paientInfor.lnrjkztzwpg = lnrjkz.ToString();
                            //老年人生活自理能力自我评估，1 可自理（0～3分）2轻度依赖（4～8分）3 中度依赖（9～18分) 4 不能自理（≥19分）
                            var lnrshzlnlpg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrshzlnlpg")?.ItemResultChar;
                            int lnrshzln = 0;
                            switch (lnrshzlnlpg)
                            {
                                case "可自理（0～3分）":
                                    lnrshzln = 1;
                                    break;
                                case "轻度依赖（4～8分）":
                                    lnrshzln = 2;
                                    break;
                                case "中度依赖（9～18分)":
                                    lnrshzln = 3;
                                    break;
                                case "不能自理（≥19分）":
                                    lnrshzln = 4;
                                    break;
                            }
                            paientInfor.lnrshzlnlpg = lnrshzln.ToString();
                            //老年人认知功能，1粗筛阴性 2粗筛阳性
                            var lnrrzgn = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrrzgn")?.ItemResultChar;
                            int lnrr = 0;
                            switch (lnrrzgn)
                            {
                                case "粗筛阴性":
                                    lnrr = 1;
                                    break;
                                case "粗筛阳性":
                                    lnrr = 2;
                                    break;

                            }
                            paientInfor.lnrrzgn = lnrr.ToString();
                            paientInfor.lnrrzgnpf = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrrzgnpf")?.ItemResultChar;
                            //老年人情感状态初筛结果，1粗筛阴性2粗筛阳性
                            var lnrqgztpg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrqgztpg")?.ItemResultChar;
                            int lnrqgz = 0;
                            switch (lnrqgztpg)
                            {
                                case "粗筛阴性":
                                    lnrqgz = 1;
                                    break;
                                case "粗筛阳性":
                                    lnrqgz = 2;
                                    break;

                            }
                            paientInfor.lnrqgztpg = lnrqgz.ToString();
                            paientInfor.lnrqgztpf = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lnrqgztpf")?.ItemResultChar;
                            //体育锻炼 锻炼频率，1每天  2每周一次以上  3偶尔  4不锻炼
                            var tydl_dlpl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_dlpl")?.ItemResultChar;
                            int tydl = 0;
                            switch (tydl_dlpl)
                            {
                                case "每天":
                                    tydl = 1;
                                    break;
                                case "每周一次以上":
                                    tydl = 2;
                                    break;
                                case "偶尔 ":
                                    tydl = 3;
                                    break;
                                case "不锻炼":
                                    tydl = 4;
                                    break;

                            }
                            paientInfor.tydl_dlpl = lnrqgz.ToString();
                            paientInfor.tydl_jcdlsj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_jcdlsj")?.ItemResultChar;
                            paientInfor.tydl_mcdlsj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_mcdlsj")?.ItemResultChar;
                            paientInfor.tydl_dlfs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tydl_dlfs")?.ItemResultChar;
                            //饮食习惯,1荤素均衡 2荤食为主 3素食为主 4嗜盐 5嗜油 6嗜糖
                            var ysxg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ysxg")?.ItemResultChar;
                            int ys = 0;
                            switch (ysxg)
                            {
                                case "荤素均衡":
                                    ys = 1;
                                    break;
                                case "荤食为主":
                                    ys = 2;
                                    break;
                                case "素食为主 ":
                                    ys = 3;
                                    break;
                                case "嗜盐":
                                    ys = 4;
                                    break;
                                case "嗜油":
                                    ys = 5;
                                    break;
                                case "嗜糖":
                                    ys = 6;
                                    break;

                            }
                            paientInfor.ysxg = ys.ToString();
                            //吸烟情况 吸烟状况，1从不吸烟　　　2已戒烟　　　 3吸烟 
                            var xyqk_xyzk = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_xyzk")?.ItemResultChar;
                            int xyqk = 0;
                            switch (xyqk_xyzk)
                            {
                                case "从不吸烟":
                                    xyqk = 1;
                                    break;
                                case "已戒烟":
                                    xyqk = 2;
                                    break;
                                case "吸烟 ":
                                    xyqk = 3;
                                    break;

                            }
                            paientInfor.xyqk_xyzk = xyqk.ToString();
                            paientInfor.xyqk_rxyl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_rxyl")?.ItemResultChar;
                            paientInfor.xyqk_ksxynl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_ksxynl")?.ItemResultChar;
                            paientInfor.xyqk_jynl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xyqk_jynl")?.ItemResultChar;
                            //饮酒频率，1从不  2偶尔  3经常  4每天
                            var yjqk_yjpl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_yjpl")?.ItemResultChar;
                            int yjqk = 0;
                            switch (yjqk_yjpl)
                            {
                                case "从不":
                                    yjqk = 1;
                                    break;
                                case "偶尔":
                                    yjqk = 2;
                                    break;
                                case "经常":
                                    yjqk = 3;
                                    break;
                                case "每天":
                                    yjqk = 4;
                                    break;

                            }
                            paientInfor.yjqk_yjpl = yjqk.ToString();
                            paientInfor.yjqk_ryjl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_ryjl")?.ItemResultChar;
                            //近一年内是否曾醉酒，1是  2否
                            var yjqk_sfzj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_sfzj")?.ItemResultChar;
                            int sfzj = 0;
                            switch (yjqk_sfzj)
                            {
                                case "是":
                                    sfzj = 1;
                                    break;
                                case "否":
                                    sfzj = 2;
                                    break;

                            }
                            paientInfor.yjqk_sfzj = sfzj.ToString();
                            //是否戒酒，1未戒酒  2已戒酒
                            var yjqk_sfjj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_sfjj")?.ItemResultChar;
                            int sfjj = 0;
                            switch (yjqk_sfzj)
                            {
                                case "未戒酒":
                                    sfjj = 1;
                                    break;
                                case "已戒酒":
                                    sfjj = 2;
                                    break;

                            }
                            paientInfor.yjqk_sfjj = sfjj.ToString();
                            //饮酒种类，1白酒2啤酒3红酒 4黄酒 9其他
                            var yjqk_yjzl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_yjzl")?.ItemResultChar;
                            int yjzl = 0;
                            switch (yjqk_yjzl)
                            {
                                case "白酒":
                                    yjzl = 1;
                                    break;
                                case "啤酒":
                                    yjzl = 2;
                                    break;
                                case "红酒":
                                    yjzl = 3;
                                    break;
                                case "黄酒":
                                    yjzl = 4;
                                    break;
                                case "其他":
                                    yjzl = 9;
                                    break;


                            }
                            paientInfor.yjqk_yjzl = yjzl.ToString();
                            paientInfor.yjqk_ksyjnl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_ksyjnl")?.ItemResultChar;
                            paientInfor.yjqk_jjnl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yjqk_jjnl")?.ItemResultChar;
                            //职业健康危害因素接触史，2无 1有
                            var zybwxys_yw = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_yw")?.ItemResultChar;
                            int _yw = 0;
                            switch (zybwxys_yw)
                            {
                                case "有":
                                    _yw = 1;
                                    break;
                                case "无":
                                    _yw = 2;
                                    break;

                            }
                            paientInfor.zybwxys_yw = _yw.ToString();
                            paientInfor.zybwxys_gz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_gz")?.ItemResultChar;
                            paientInfor.zybwxys_cysj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_cysj")?.ItemResultChar;
                            paientInfor.zybwxys_fc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fc")?.ItemResultChar;
                            //粉尘防护措施描述，2无 1有
                            var zybwxys_fcfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fcfhcsms")?.ItemResultChar;
                            int fcfhcsms = 0;
                            switch (zybwxys_fcfhcsms)
                            {
                                case "有":
                                    fcfhcsms = 1;
                                    break;
                                case "无":
                                    fcfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_fcfhcsms = fcfhcsms.ToString();
                            paientInfor.zybwxys_fswz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fswz")?.ItemResultChar;
                            paientInfor.zybwxys_fswzfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fswzfhcs")?.ItemResultChar;
                            //放射物质防护措施描述，2无 1有
                            var zybwxys_fswzfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_fswzfhcsms")?.ItemResultChar;
                            int fswzfhcsms = 0;
                            switch (zybwxys_fswzfhcsms)
                            {
                                case "有":
                                    fswzfhcsms = 1;
                                    break;
                                case "无":
                                    fswzfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_fswzfhcsms = fswzfhcsms.ToString();
                            paientInfor.zybwxys_wlys = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_wlys")?.ItemResultChar;
                            paientInfor.zybwxys_wlysfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_wlysfhcs")?.ItemResultChar;
                            //物理因素防护措施描述，2无 1有
                            var zybwxys_wlysfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_wlysfhcsms")?.ItemResultChar;
                            int wlysfhcsms = 0;
                            switch (zybwxys_wlysfhcsms)
                            {
                                case "有":
                                    wlysfhcsms = 1;
                                    break;
                                case "无":
                                    wlysfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_wlysfhcsms = wlysfhcsms.ToString();
                            paientInfor.zybwxys_hxwz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_hxwz")?.ItemResultChar;
                            paientInfor.zybwxys_hxwzfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_hxwzfhcs")?.ItemResultChar;
                            //化学物质防护措施描述，2无 1有
                            var zybwxys_hxwzfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_hxwzfhcsms")?.ItemResultChar;
                            int _hxwzfhcsms = 0;
                            switch (zybwxys_hxwzfhcsms)
                            {
                                case "有":
                                    _hxwzfhcsms = 1;
                                    break;
                                case "无":
                                    _hxwzfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_hxwzfhcsms = _hxwzfhcsms.ToString();
                            paientInfor.zybwxys_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_qt")?.ItemResultChar;
                            paientInfor.zybwxys_qtfhcs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_qtfhcs")?.ItemResultChar;
                            //其他防护措施描述，2无 1有
                            var zybwxys_qtfhcsms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zybwxys_qtfhcsms")?.ItemResultChar;
                            int qtfhcsms = 0;
                            switch (zybwxys_hxwzfhcsms)
                            {
                                case "有":
                                    qtfhcsms = 1;
                                    break;
                                case "无":
                                    qtfhcsms = 2;
                                    break;

                            }
                            paientInfor.zybwxys_qtfhcsms = qtfhcsms.ToString();
                            //口腔 口唇外观类别，1红润 2苍白 3发绀 4皲裂 5疱疹
                            var kq_kcwglb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kq_kcwglb")?.ItemResultChar;
                            int kcwglb = 0;
                            switch (kq_kcwglb)
                            {
                                case "红润":
                                    kcwglb = 1;
                                    break;
                                case "苍白":
                                    kcwglb = 2;
                                    break;
                                case "发绀":
                                    kcwglb = 3;
                                    break;
                                case "皲裂":
                                    kcwglb = 4;
                                    break;
                                case "疱疹":
                                    kcwglb = 5;
                                    break;


                            }
                            paientInfor.kq_kcwglb = kcwglb.ToString();
                            //齿列类别，1正常 2缺齿  3龋齿   4义齿(假牙)
                            var kq_cllb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kq_cllb")?.ItemResultChar;
                            int cllb = 0;
                            switch (kq_cllb)
                            {
                                case "正常":
                                    cllb = 1;
                                    break;
                                case "缺齿":
                                    cllb = 2;
                                    break;
                                case "龋齿 ":
                                    cllb = 3;
                                    break;
                                case "义齿(假牙)":
                                    cllb = 4;
                                    break;

                            }
                            paientInfor.kq_cllb = cllb.ToString();
                            //咽部检查结果，1无充血 2充血 3淋巴滤泡增生
                            var kq_ybjcjg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kq_ybjcjg")?.ItemResultChar;
                            int _ybjcjg = 0;
                            switch (kq_ybjcjg)
                            {
                                case "无充血":
                                    _ybjcjg = 1;
                                    break;
                                case "充血":
                                    _ybjcjg = 2;
                                    break;
                                case "淋巴滤泡增生 ":
                                    _ybjcjg = 3;
                                    break;

                            }
                            paientInfor.kq_ybjcjg = _ybjcjg.ToString();
                            paientInfor.sl_zyly = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_zyly")?.ItemResultChar;
                            paientInfor.sl_yyly = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_yyly")?.ItemResultChar;
                            paientInfor.sl_zyjz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_zyjz")?.ItemResultChar;
                            paientInfor.sl_yyjz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sl_yyjz")?.ItemResultChar;
                            //听力检测结果，1听见        2听不清或无法听见
                            var tljcjg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "tljcjg")?.ItemResultChar;
                            int ljcjg = 0;
                            switch (tljcjg)
                            {
                                case "听见":
                                    ljcjg = 1;
                                    break;
                                case "听不清或无法听见":
                                    ljcjg = 2;
                                    break;

                            }
                            paientInfor.tljcjg = ljcjg.ToString();
                            //运动功能状态，1可顺利完成  2无法独立完成任何一个动作
                            var ydgnzt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ydgnzt")?.ItemResultChar;
                            int dgnzt = 0;
                            switch (ydgnzt)
                            {
                                case "可顺利完成":
                                    dgnzt = 1;
                                    break;
                                case "无法独立完成任何一个动作":
                                    dgnzt = 2;
                                    break;

                            }
                            paientInfor.ydgnzt = dgnzt.ToString();
                            //眼底，1正常2异常  3未检
                            var yd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yd")?.ItemResultChar;
                            int _yd = 0;
                            switch (yd)
                            {
                                case "正常":
                                    _yd = 1;
                                    break;
                                case "异常 ":
                                    _yd = 2;
                                    break;
                                case "未检 ":
                                    _yd = 3;
                                    break;
                            }
                            paientInfor.yd = _yd.ToString();
                            paientInfor.ydycms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ydycms")?.ItemResultChar;
                            //皮肤，01未见异常  02潮红 03苍白 04发绀 05黄染  06色素沉着 99其他 
                            var pf = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "pf")?.ItemResultChar;
                            int _pf = 0;
                            switch (pf)
                            {
                                case "未见异常 ":
                                    _pf = 01;
                                    break;
                                case "潮红 ":
                                    _pf = 02;
                                    break;
                                case "苍白 ":
                                    _pf = 03;
                                    break;
                                case "发绀":
                                    _pf = 04;
                                    break;
                                case "黄染":
                                    _pf = 05;
                                    break;
                                case "色素沉着":
                                    _pf = 06;
                                    break;
                                case "其他":
                                    _pf = 99;
                                    break;
                            }
                            paientInfor.pf = _pf.ToString();
                            paientInfor.pfqtms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "pfqtms")?.ItemResultChar;
                            //巩膜，1正常  2 黄染 3充血 9其他
                            var gm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gm")?.ItemResultChar;
                            int _gm = 0;
                            switch (gm)
                            {
                                case "正常":
                                    _gm = 1;
                                    break;
                                case "黄染":
                                    _gm = 2;
                                    break;
                                case "充血":
                                    _gm = 3;

                                    break;
                                case "其他":
                                    _gm = 9;
                                    break;

                            }
                            paientInfor.gm = _gm.ToString();
                            paientInfor.gmqtms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gmqtms")?.ItemResultChar;
                            //淋巴结检查，1未触及   2锁骨上   3腋窝   9其他 
                            var lbjjc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lbjjc")?.ItemResultChar;
                            int jjc = 0;
                            switch (lbjjc)
                            {
                                case "未触及 ":
                                    jjc = 1;
                                    break;
                                case "锁骨上":
                                    jjc = 2;
                                    break;
                                case "腋窝":
                                    jjc = 3;

                                    break;
                                case "其他":
                                    jjc = 9;
                                    break;

                            }
                            paientInfor.lbjjc = jjc.ToString();
                            paientInfor.lbjjcqtms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "lbjjcqtms")?.ItemResultChar;
                            //肺桶状胸，1否　　2是
                            var f_tzx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_tzx")?.ItemResultChar;
                            int _tzx = 0;
                            switch (f_tzx)
                            {
                                case "否":
                                    _tzx = 1;
                                    break;
                                case "是":
                                    _tzx = 2;
                                    break;

                            }
                            paientInfor.f_tzx = _tzx.ToString();
                            //肺呼吸音，1正常  2异常
                            var f_hxy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_hxy")?.ItemResultChar;
                            int _hxy = 0;
                            switch (f_hxy)
                            {
                                case "正常":
                                    _hxy = 1;
                                    break;
                                case "异常":
                                    _hxy = 2;
                                    break;

                            }
                            paientInfor.f_hxy = _hxy.ToString();
                            paientInfor.f_hxyyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_hxyyc")?.ItemResultChar;
                            //肺啰音，1无 　 2干罗音  3湿罗音 4其他 
                            var f_ly = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_ly")?.ItemResultChar;
                            int _ly = 0;
                            switch (f_ly)
                            {
                                case "无":
                                    _ly = 1;
                                    break;
                                case "干罗音":
                                    _ly = 2;
                                    break;
                                case "湿罗音":
                                    _ly = 3;
                                    break;
                                case "其他":
                                    _ly = 4;
                                    break;

                            }
                            paientInfor.f_ly = _ly.ToString();
                            paientInfor.f_lyqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "f_lyqt")?.ItemResultChar;
                            paientInfor.xz_xl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xl")?.ItemResultChar;
                            //心脏心律类别，1齐  2不齐  3绝对不齐
                            var xz_xllb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xllb")?.ItemResultChar;
                            int _xllb = 0;
                            switch (xz_xllb)
                            {
                                case "齐":
                                    _xllb = 1;
                                    break;
                                case "不齐":
                                    _xllb = 2;
                                    break;
                                case "绝对不齐":
                                    _xllb = 3;
                                    break;
                            }
                            paientInfor.xz_xllb = _xllb.ToString();
                            //心脏杂音，1有  2无 
                            var xz_zy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zy")?.ItemResultChar;
                            int z_zy = 0;
                            switch (xz_zy)
                            {
                                case "有":
                                    z_zy = 1;
                                    break;
                                case "无":
                                    z_zy = 2;
                                    break;

                            }
                            paientInfor.xz_zy = z_zy.ToString();
                            paientInfor.xz_zyms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zyms")?.ItemResultChar;
                            //腹部压痛，1有  2无 
                            var fb_yt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_yt")?.ItemResultChar;
                            int b_yt = 0;
                            switch (fb_yt)
                            {
                                case "有":
                                    b_yt = 1;
                                    break;
                                case "无":
                                    b_yt = 2;
                                    break;

                            }
                            paientInfor.fb_yt = b_yt.ToString();
                            paientInfor.fb_ytms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_ytms")?.ItemResultChar;
                            //腹部包块，1有  2无 
                            var fb_bk = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_bk")?.ItemResultChar;
                            int b_bk = 0;
                            switch (fb_bk)
                            {
                                case "有":
                                    b_bk = 1;
                                    break;
                                case "无":
                                    b_bk = 2;
                                    break;

                            }
                            paientInfor.fb_bk = b_bk.ToString();
                            paientInfor.fb_bkms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_bkms")?.ItemResultChar;
                            //腹部肝大，1有  2无
                            var fb_gd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_gd")?.ItemResultChar;
                            int b_gd = 0;
                            switch (fb_gd)
                            {
                                case "有":
                                    b_gd = 1;
                                    break;
                                case "无":
                                    b_gd = 2;
                                    break;

                            }
                            paientInfor.fb_gd = b_gd.ToString();
                            paientInfor.fb_pdms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_pdms")?.ItemResultChar;
                            //腹部移动性浊音，1有  2无 
                            var fb_ydxzy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_ydxzy")?.ItemResultChar;
                            int _ydxzy = 0;
                            switch (fb_ydxzy)
                            {
                                case "有":
                                    _ydxzy = 1;
                                    break;
                                case "无":
                                    _ydxzy = 2;
                                    break;

                            }
                            paientInfor.fb_ydxzy = _ydxzy.ToString();
                            paientInfor.fb_ydxzyms = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fb_ydxzyms")?.ItemResultChar;
                            //下肢水肿检查，1无   2单侧   3双侧不对称   4双侧对称
                            var xzszjc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xzszjc")?.ItemResultChar;
                            int _szjc = 0;
                            switch (xzszjc)
                            {
                                case "无":
                                    _szjc = 1;
                                    break;
                                case "单侧":
                                    _szjc = 2;
                                    break;
                                case "双侧不对称":
                                    _szjc = 3;
                                    break;
                                case "双侧对称":
                                    _szjc = 4;
                                    break;

                            }
                            paientInfor.xzszjc = _szjc.ToString();
                            //肛门指诊，1未及异常 2 触痛　  3包块  4前列腺异常 9其他
                            var gmzz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gmzz")?.ItemResultChar;
                            int _gmzz = 0;
                            switch (gmzz)
                            {
                                case "未及异常":
                                    _gmzz = 1;
                                    break;
                                case "触痛":
                                    _gmzz = 2;
                                    break;
                                case "包块":
                                    _gmzz = 3;
                                    break;
                                case "前列腺异常":
                                    _gmzz = 4;
                                    break;
                                case "其他":
                                    _gmzz = 9;
                                    break;

                            }
                            paientInfor.gmzz = _gmzz.ToString();
                            paientInfor.gmzzqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gmzzqt")?.ItemResultChar;
                            //足背动脉搏动，1未触及2触及双侧对称3触及左侧弱或消失4触及右侧弱或消失
                            var zbdmbd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "zbdmbd")?.ItemResultChar;
                            int _zbdmbd = 0;
                            switch (zbdmbd)
                            {
                                case "未触及":
                                    _zbdmbd = 1;
                                    break;
                                case "触及双侧对称":
                                    _zbdmbd = 2;
                                    break;
                                case "触及左侧弱或消失":
                                    _zbdmbd = 3;
                                    break;
                                case "触及右侧弱或消失":
                                    _zbdmbd = 4;
                                    break;

                            }
                            paientInfor.zbdmbd = _zbdmbd.ToString();
                            //乳腺，1未见异常 2乳房切除 3异常泌乳4乳腺包块 9其他 
                            var rx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "rx")?.ItemResultChar;
                            int _rx = 0;
                            switch (rx)
                            {
                                case "未见异常":
                                    _rx = 1;
                                    break;
                                case "乳房切除":
                                    _rx = 2;
                                    break;
                                case "异常泌乳":
                                    _rx = 3;
                                    break;
                                case "乳腺包块":
                                    _rx = 4;
                                    break;
                                case "其他":
                                    _rx = 9;
                                    break;

                            }
                            paientInfor.rx = _rx.ToString();
                            paientInfor.rxqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "rxqt")?.ItemResultChar;
                            //妇科检查 外阴，1未见异常   2异常    3拒检
                            var fkjc_wy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_wy")?.ItemResultChar;
                            int _wy = 0;
                            switch (fkjc_wy)
                            {
                                case "未见异常":
                                    _wy = 1;
                                    break;
                                case "异常":
                                    _wy = 2;
                                    break;
                                case "拒检":
                                    _wy = 3;
                                    break;

                            }
                            paientInfor.fkjc_wy = _wy.ToString();
                            paientInfor.fkjc_wyyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_wyyc")?.ItemResultChar;
                            //妇科检查 宫颈，1未见异常   2异常    3拒检
                            var fkjc_gj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gj")?.ItemResultChar;
                            int _gj = 0;
                            switch (fkjc_gj)
                            {
                                case "未见异常":
                                    _gj = 1;
                                    break;
                                case "异常":
                                    _gj = 2;
                                    break;
                                case "拒检":
                                    _gj = 3;
                                    break;

                            }
                            paientInfor.fkjc_gj = _gj.ToString();
                            paientInfor.fkjc_gjyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gjyc")?.ItemResultChar;
                            //妇科检查 宫体，1未见异常   2异常    3拒检
                            var fkjc_gt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gj")?.ItemResultChar;
                            int _gt = 0;
                            switch (fkjc_gt)
                            {
                                case "未见异常":
                                    _gt = 1;
                                    break;
                                case "异常":
                                    _gt = 2;
                                    break;
                                case "拒检":
                                    _gt = 3;
                                    break;

                            }
                            paientInfor.fkjc_gt = _gt.ToString();
                            paientInfor.fkjc_gtyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_gtyc")?.ItemResultChar;
                            //妇科检查 子宫附件，1未见异常   2异常    3拒检
                            var fkjc_zgfj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_zgfj")?.ItemResultChar;
                            int _zgfj = 0;
                            switch (fkjc_zgfj)
                            {
                                case "未见异常":
                                    _zgfj = 1;
                                    break;
                                case "异常":
                                    _zgfj = 2;
                                    break;
                                case "拒检":
                                    _zgfj = 3;
                                    break;

                            }
                            paientInfor.fkjc_zgfj = _zgfj.ToString();
                            paientInfor.fkjc_zgfjyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fkjc_zgfjyc")?.ItemResultChar;
                            paientInfor.ct_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ct_qt")?.ItemResultChar;
                            //血常规，1已检   2未检    3拒检
                            //血常规，1已检   2未检    3拒检
                            var xcg_ndbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xhdb")?.ItemId;
                            var xcg_nttid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_bxb")?.ItemId;
                            var xcg_qtid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xxb")?.ItemId;
                            var xcg_nxxid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_qt")?.ItemId;

                            var xcg_ndbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_ndbid).FirstOrDefault()))?.CheckState;
                            var xcg_nttGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_nttid).FirstOrDefault()))?.CheckState;
                            var xcg_qtGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_qtid).FirstOrDefault()))?.CheckState;
                            var xcg_nxxGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xcg_nxxid).FirstOrDefault()))?.CheckState;
                           
                            if (xcg_ndbGroups == 1 || xcg_nttGroups == 1 || xcg_qtGroups == 1 || xcg_nxxGroups == 1)
                            {
                                paientInfor.xcg = "2";
                            }
                            if (xcg_ndbGroups == 2 || xcg_nttGroups == 2 || xcg_qtGroups == 2 || xcg_nxxGroups == 2)
                            {
                                paientInfor.xcg = "1";
                            }
                            if (xcg_ndbGroups == 3 || xcg_nttGroups == 3 || xcg_qtGroups == 3 || xcg_nxxGroups == 3)
                            {
                                paientInfor.xcg = "1";
                            }
                            if (xcg_ndbGroups == 4 || xcg_nttGroups == 4 || xcg_qtGroups == 4 || xcg_nxxGroups == 4)
                            {
                                paientInfor.xcg = "3";
                            }
                            paientInfor.xcg_xhdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xhdb")?.ItemResultChar;
                            paientInfor.xcg_bxb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_bxb")?.ItemResultChar;
                            paientInfor.xcg_xxb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_xxb")?.ItemResultChar;
                            paientInfor.xcg_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xcg_qt")?.ItemResultChar;

                            ////尿常规，1已检   2未检    3拒检
                            var ncg_ndbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ndb")?.ItemId;
                            var ncg_nttid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ntt")?.ItemId;
                            var ncg_qtid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_qt")?.ItemId;
                            var ncg_nxxid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nxx")?.ItemId;
                            var ncg_ntid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nt")?.ItemId;

                            var ncg_ntGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_ntid).FirstOrDefault()))?.CheckState;
                            var ncg_ndbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_ndbid).FirstOrDefault()))?.CheckState;
                            var ncg_nttGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_nttid).FirstOrDefault()))?.CheckState;
                            var ncg_qtGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_qtid).FirstOrDefault()))?.CheckState;
                            var ncg_nxxGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ncg_nxxid).FirstOrDefault()))?.CheckState;
                            if (ncg_ndbGroups == 1 || ncg_nttGroups == 1 || ncg_qtGroups == 1 || ncg_nxxGroups == 1 || ncg_ntGroups == 1)
                            {
                                paientInfor.ncg = "2";
                            }
                            if (ncg_ndbGroups == 2 || ncg_nttGroups == 2 || ncg_qtGroups == 2 || ncg_nxxGroups == 2 || ncg_ntGroups == 2)
                            {
                                paientInfor.ncg = "1";
                            }
                            if (ncg_ndbGroups == 3 || ncg_nttGroups == 3 || ncg_qtGroups == 3 || ncg_nxxGroups == 3 || ncg_ntGroups == 3)
                            {
                                paientInfor.ncg = "1";
                            }
                            if (ncg_ndbGroups == 4 || ncg_nttGroups == 4 || ncg_qtGroups == 4 || ncg_nxxGroups == 4 || ncg_ntGroups == 4)
                            {
                                paientInfor.ncg = "3";
                            }
                            paientInfor.ncg_ndb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ndb")?.ItemResultChar;
                            paientInfor.ncg_nt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nt")?.ItemResultChar;
                            paientInfor.ncg_ntt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_ntt")?.ItemResultChar;
                            paientInfor.ncg_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_qt")?.ItemResultChar;
                            paientInfor.ncg_nxx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_nxx")?.ItemResultChar;
                            paientInfor.ncg_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ncg_qt")?.ItemResultChar;
                            paientInfor.nwlbdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "nwlbdb")?.ItemResultChar;
                            paientInfor.kfxt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "kfxt")?.ItemResultChar;
                            paientInfor.thxhdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "thxhdb")?.ItemResultChar;
                            //大便潜血，1阴性  2阳性 3未检
                            var dbqx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "dbqx")?.ItemResultChar;
                            int _dbqx = 0;
                            switch (dbqx)
                            {
                                case "阴性":
                                    _dbqx = 1;
                                    break;
                                case "阳性":
                                    _dbqx = 2;
                                    break;
                                case "未检":
                                    _dbqx = 3;
                                    break;

                            }
                            paientInfor.dbqx = _dbqx.ToString();
                            //乙肝表面抗原(HBsAg)，1阴性  2阳性 3未检
                            var ygbmky = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ygbmky")?.ItemResultChar;
                            int _ygbmky = 0;
                            switch (ygbmky)
                            {
                                case "阴性":
                                    _ygbmky = 1;
                                    break;
                                case "阳性":
                                    _ygbmky = 2;
                                    break;
                                case "未检":
                                    _ygbmky = 3;
                                    break;

                            }
                            paientInfor.ygbmky = _ygbmky.ToString();
                            //肝功能，1已检   2未检    3拒检
                            var ggn_xqgbzamid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgbzam")?.ItemId;
                            var ggn_xqgczamid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgczam")?.ItemId;
                            var ggn_bdbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_bdb")?.ItemId;
                            var ggn_zdhsid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_zdhs")?.ItemId;
                            var ggn_jhdhsid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_jhdhs")?.ItemId;

                            var ggn_xqgbzamGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_xqgbzamid).FirstOrDefault()))?.CheckState;
                            var ggn_xqgczamGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_xqgczamid).FirstOrDefault()))?.CheckState;
                            var ggn_bdbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_bdbid).FirstOrDefault()))?.CheckState;
                            var ggn_zdhsGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_zdhsid).FirstOrDefault()))?.CheckState;
                            var ggn_jhdhsGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == ggn_jhdhsid).FirstOrDefault()))?.CheckState;
                            if (ggn_xqgbzamGroups == 1 || ggn_xqgczamGroups == 1 || ggn_bdbGroups == 1 || ggn_zdhsGroups == 1 || ggn_jhdhsGroups == 1)
                            {
                                paientInfor.ggn = "2";
                            }
                            if (ggn_xqgbzamGroups == 2 || ggn_xqgczamGroups == 2 || ggn_bdbGroups == 2 || ggn_zdhsGroups == 2 || ggn_jhdhsGroups == 2)
                            {
                                paientInfor.ggn = "1";
                            }
                            if (ggn_xqgbzamGroups == 3 || ggn_xqgczamGroups == 3 || ggn_bdbGroups == 3 || ggn_zdhsGroups == 3 || ggn_jhdhsGroups == 3)
                            {
                                paientInfor.ggn = "1";
                            }
                            if (ggn_xqgbzamGroups == 4 || ggn_xqgczamGroups == 4 || ggn_bdbGroups == 4 || ggn_zdhsGroups == 4 || ggn_jhdhsGroups == 4)
                            {
                                paientInfor.ggn = "3";
                            }


                            paientInfor.ggn_xqgbzam = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgbzam")?.ItemResultChar;
                            paientInfor.ggn_xqgczam = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_xqgczam")?.ItemResultChar;
                            paientInfor.ggn_bdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_bdb")?.ItemResultChar;
                            paientInfor.ggn_zdhs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_zdhs")?.ItemResultChar;
                            paientInfor.ggn_jhdhs = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ggn_jhdhs")?.ItemResultChar;
                            //肾功能，1已检   2未检    3拒检
                            var sgn_xqjgid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xqjg")?.ItemId;
                            var sgn_xnsdid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnsd")?.ItemId;
                            var sgn_xjndid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xjnd")?.ItemId;
                            var sgn_xnndid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnnd")?.ItemId;

                            var sgn_xqjgGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xqjgid).FirstOrDefault()))?.CheckState;
                            var sgn_xnsdGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xnsdid).FirstOrDefault()))?.CheckState;
                            var sgn_xjndGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xjndid).FirstOrDefault()))?.CheckState;
                            var sgn_xnndGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == sgn_xnndid).FirstOrDefault()))?.CheckState;
                            if (sgn_xqjgGroups == 1 || sgn_xnsdGroups == 1 || sgn_xjndGroups == 1 || sgn_xnndGroups == 1)
                            {
                                paientInfor.sgn = "2";
                            }
                            if (sgn_xqjgGroups == 2 || sgn_xnsdGroups == 2 || sgn_xjndGroups == 2 || sgn_xnndGroups == 2)
                            {
                                paientInfor.sgn = "1";
                            }
                            if (sgn_xqjgGroups == 3 || sgn_xnsdGroups == 3 || sgn_xjndGroups == 3 || sgn_xnndGroups == 3)
                            {
                                paientInfor.sgn = "1";
                            }
                            if (sgn_xqjgGroups == 4 || sgn_xnsdGroups == 4 || sgn_xjndGroups == 4 || sgn_xnndGroups == 4)
                            {
                                paientInfor.sgn = "3";
                            }
                            paientInfor.sgn_xqjg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xqjg")?.ItemResultChar;
                            paientInfor.sgn_xnsd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnsd")?.ItemResultChar;
                            paientInfor.sgn_xjnd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xjnd")?.ItemResultChar;
                            paientInfor.sgn_xnnd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sgn_xnnd")?.ItemResultChar;
                            //血脂，1已检   2未检    3拒检
                            var xz_zdgczid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zdgcz")?.ItemId;
                            var xz_gyszid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_gysz")?.ItemId;
                            var xz_xqdmdzdbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqdmdzdb")?.ItemId;
                            var xz_xqgmdzdbid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqgmdzdb")?.ItemId;

                            var xz_zdgczGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_zdgczid).FirstOrDefault()))?.CheckState;
                            var xz_gyszGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_gyszid).FirstOrDefault()))?.CheckState;
                            var xz_xqdmdzdbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_xqdmdzdbid).FirstOrDefault()))?.CheckState;
                            var xz_xqgmdzdbGroups = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xz_xqgmdzdbid).FirstOrDefault()))?.CheckState;
                            if (xz_zdgczGroups == 1 || xz_gyszGroups == 1 || xz_xqdmdzdbGroups == 1 || xz_xqgmdzdbGroups == 1)
                            {
                                paientInfor.xz = "2";
                            }
                            if (xz_zdgczGroups == 2 || xz_gyszGroups == 2 || xz_xqdmdzdbGroups == 2 || xz_xqgmdzdbGroups == 2)
                            {
                                paientInfor.xz = "1";
                            }
                            if (xz_zdgczGroups == 3 || xz_gyszGroups == 3 || xz_xqdmdzdbGroups == 3 || xz_xqgmdzdbGroups == 3)
                            {
                                paientInfor.xz = "1";
                            }
                            if (xz_zdgczGroups == 4 || xz_gyszGroups == 4 || xz_xqdmdzdbGroups == 4 || xz_xqgmdzdbGroups == 4)
                            {
                                paientInfor.xz = "3";
                            }
                            paientInfor.xz_zdgcz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_zdgcz")?.ItemResultChar;
                            paientInfor.xz_gysz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_gysz")?.ItemResultChar;
                            paientInfor.xz_xqdmdzdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqdmdzdb")?.ItemResultChar;
                            paientInfor.xz_xqgmdzdb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xz_xqgmdzdb")?.ItemResultChar;
                            //心电图，1正常  2异常  3未检
                            var xdtycid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xdtyc")?.ItemId;
                            var xdtycGroup = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xdtycid).FirstOrDefault()))?.CheckState;
                            var xdtycGroups = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xdtyc")?.Symbol;
                            if (xdtycGroups == "P")
                            {
                                paientInfor.xdt = "2";
                            }
                            if (xdtycGroups != "P")
                            {
                                paientInfor.xdt = "1";
                            }
                            if (xdtycGroup == 1)
                            {
                                paientInfor.xdt = "3";
                            }

                            paientInfor.xdtyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xdtyc")?.ItemResultChar;
                            //X射线，1正常  2异常  3未检
                            var xsxycid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xsxyc")?.ItemId;
                            var xsxycGroup = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == xsxycid).FirstOrDefault()))?.CheckState;
                            var xsxycGroups = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xsxyc")?.Symbol;

                            if (xsxycGroups == "M")
                            {
                                paientInfor.xsx = "1";
                            }
                            if (xsxycGroups == "H" || xsxycGroups == "HH" || xsxycGroups == "L" || xsxycGroups == "LL" || xsxycGroups == "P")
                            {
                                paientInfor.xsx = "2";
                            }
                            if (xsxycGroup == 1)
                            {
                                paientInfor.xsx = "3";
                            }
                            paientInfor.xsxyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xsxyc")?.ItemResultChar;
                            //B超，1正常  2异常  3未检
                            var bchaoycid = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "bchaoyc")?.ItemId;
                            var bchaoycGroup = cusreg.CustomerItemGroup.FirstOrDefault(o => o.CustomerRegItem.Contains(o.CustomerRegItem.Where(x => x.ItemId == bchaoycid).FirstOrDefault()))?.CheckState;
                            var bchaoycGroups = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "bchaoyc")?.Symbol;

                            if (bchaoycGroups == "M")
                            {
                                paientInfor.bchao = "1";
                            }
                            if (bchaoycGroups == "H" || bchaoycGroups == "HH" || bchaoycGroups == "L" || bchaoycGroups == "LL" || bchaoycGroups == "P")
                            {
                                paientInfor.bchao = "2";
                            }
                            if (bchaoycGroup == 1)
                            {
                                paientInfor.bchao = "3";
                            }
                            paientInfor.bchaoyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "bchaoyc")?.ItemResultChar;
                            //宫颈涂片，1正常  2异常  3未检
                            var gjtp = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gjtp")?.ItemResultChar;
                            int _gjtp = 0;
                            switch (gjtp)
                            {
                                case "正常":
                                    _gjtp = 1;
                                    break;
                                case "异常 ":
                                    _gjtp = 2;
                                    break;
                                case "未检":
                                    _gjtp = 3;
                                    break;
                            }
                            paientInfor.gjtp = _gjtp.ToString();
                            paientInfor.gjtpyc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "gjtpyc")?.ItemResultChar;
                            paientInfor.fzjc_qt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "fzjc_qt")?.ItemResultChar;
                            //脑血管疾病，1未发现  2缺血性卒中  3脑出血 4蛛网膜下腔出血  5短暂性脑缺血发作  6其他
                            var nxgjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "nxgjb")?.ItemResultChar;
                            int _nxgjb = 0;
                            switch (nxgjb)
                            {
                                case "未发现":
                                    _nxgjb = 1;
                                    break;
                                case "缺血性卒中":
                                    _nxgjb = 2;
                                    break;
                                case "脑出血":
                                    _nxgjb = 3;
                                    break;
                                case "蛛网膜下腔出血":
                                    _nxgjb = 3;
                                    break;
                                case "短暂性脑缺血发作":
                                    _nxgjb = 3;
                                    break;
                                case "其他":
                                    _nxgjb = 3;
                                    break;
                            }
                            paientInfor.nxgjb = _nxgjb.ToString();
                            paientInfor.nxgjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "nxgjbqt")?.ItemResultChar;
                            //肾脏疾病，1未发现  2糖尿病肾病  3肾功能衰竭  4急性肾炎  5慢性肾炎   6其他
                            var szjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "szjb")?.ItemResultChar;
                            int _szjb = 0;
                            switch (szjb)
                            {
                                case "未发现":
                                    _szjb = 1;
                                    break;
                                case "糖尿病肾病":
                                    _szjb = 2;
                                    break;
                                case "肾功能衰竭":
                                    _szjb = 3;
                                    break;
                                case "急性肾炎":
                                    _szjb = 4;
                                    break;
                                case "慢性肾炎":
                                    _szjb = 5;
                                    break;
                                case "其他":
                                    _szjb = 6;
                                    break;
                            }
                            paientInfor.szjb = _szjb.ToString();
                            paientInfor.szjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "szjbqt")?.ItemResultChar;
                            //心脏疾病，1未发现  2心肌梗死  3心绞痛  4冠状动脉血运重建 5充血性心力衰竭 6 心前区疼痛  7其他 
                            var xzjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xzjb")?.ItemResultChar;
                            int _xzjb = 0;
                            switch (xzjb)
                            {
                                case "未发现":
                                    _xzjb = 1;
                                    break;
                                case "心肌梗死":
                                    _xzjb = 2;
                                    break;
                                case "心绞痛":
                                    _xzjb = 3;
                                    break;
                                case "冠状动脉血运重建":
                                    _xzjb = 4;
                                    break;
                                case "充血性心力衰竭":
                                    _xzjb = 5;
                                    break;
                                case "心前区疼痛":
                                    _xzjb = 6;
                                    break;
                                case "其他":
                                    _xzjb = 7;
                                    break;
                            }
                            paientInfor.xzjb = _xzjb.ToString();
                            paientInfor.xzjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xzjbqt")?.ItemResultChar;
                            //血管疾病，1未发现 2夹层动脉瘤  3动脉闭塞性疾病 4其他 
                            var xgjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xgjb")?.ItemResultChar;
                            int _xgjb = 0;
                            switch (xgjb)
                            {
                                case "未发现":
                                    _xgjb = 1;
                                    break;
                                case "夹层动脉瘤":
                                    _xgjb = 2;
                                    break;
                                case "动脉闭塞性疾病":
                                    _xgjb = 3;
                                    break;
                                case "其他":
                                    _xgjb = 4;
                                    break;

                            }
                            paientInfor.xgjb = _xgjb.ToString();
                            paientInfor.xgjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "xgjbqt")?.ItemResultChar;
                            //眼部疾病，1未发现 2视网膜出血或渗出 3视乳头水肿 4白内障 5其他
                            var ybjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ybjb")?.ItemResultChar;
                            int _ybjb = 0;
                            switch (ybjb)
                            {
                                case "未发现":
                                    _ybjb = 1;
                                    break;
                                case "视网膜出血或渗出":
                                    _ybjb = 2;
                                    break;
                                case "视乳头水肿":
                                    _ybjb = 3;
                                    break;
                                case "白内障":
                                    _ybjb = 4;
                                    break;
                                case "其他":
                                    _ybjb = 5;
                                    break;

                            }
                            paientInfor.ybjb = _ybjb.ToString();
                            paientInfor.ybjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "ybjbqt")?.ItemResultChar;
                            //神经系统疾病，1未发现 2有    
                            var sjxtjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sjxtjb")?.ItemResultChar;
                            int _sjxtjb = 0;
                            switch (sjxtjb)
                            {
                                case "未发现":
                                    _sjxtjb = 1;
                                    break;
                                case "有":
                                    _sjxtjb = 2;
                                    break;
                            }
                            paientInfor.sjxtjb = _sjxtjb.ToString();
                            paientInfor.sjxtjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "sjxtjbqt")?.ItemResultChar;
                            //其他系统疾病，1未发现 2有     
                            var qtxtjb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "qtxtjb")?.ItemResultChar;
                            int _qtxtjb = 0;
                            switch (qtxtjb)
                            {
                                case "未发现":
                                    _qtxtjb = 1;
                                    break;
                                case "有":
                                    _qtxtjb = 2;
                                    break;
                            }
                            paientInfor.qtxtjb = _qtxtjb.ToString();
                            paientInfor.qtxtjbqt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "qtxtjbqt")?.ItemResultChar;
                            //健康评价是否有异常，1体检无异常 　2有异常     
                            var jtywycbm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jtywycbm")?.ItemResultChar;
                            int _jtywycbm = 0;
                            switch (jtywycbm)
                            {
                                case "体检无异常":
                                    _jtywycbm = 1;
                                    break;
                                case "有异常":
                                    _jtywycbm = 2;
                                    break;
                            }
                            paientInfor.jtywycbm = _jtywycbm.ToString();
                            paientInfor.yc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "yc")?.ItemResultChar;
                            paientInfor.jkzdbm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jkzdbm")?.ItemResultChar;
                            //危险因素控制，1戒烟  2健康饮酒 3饮食 4锻炼 5减体重 6建议接种疫苗 9其他 
                            var wxyskzbm = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "wxyskzbm")?.ItemResultChar;
                            int _wxyskzbm = 0;
                            switch (wxyskzbm)
                            {
                                case "戒烟":
                                    _wxyskzbm = 1;
                                    break;
                                case "健康饮酒":
                                    _wxyskzbm = 2;
                                    break;
                                case "饮食":
                                    _wxyskzbm = 3;
                                    break;
                                case "锻炼":
                                    _wxyskzbm = 4;
                                    break;
                                case "减体重":
                                    _wxyskzbm = 5;
                                    break;
                                case "建议接种疫苗":
                                    _wxyskzbm = 6;
                                    break;
                                case "其他":
                                    _wxyskzbm = 9;
                                    break;
                            }
                            paientInfor.wxyskzbm = _wxyskzbm.ToString();
                            paientInfor.jytz = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jytz")?.ItemResultChar;
                            paientInfor.jyjzym = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "jyjzym")?.ItemResultChar;
                            paientInfor.qtjy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "qtjy")?.ItemResultChar;
                            Oldjson oldjson = new Oldjson();
                            
                            oldjson.q1 = ""; oldjson.q2 = ""; oldjson.q3 = "";
                            oldjson.q4 = ""; oldjson.q5 = "";
                            paientInfor.lnrShzlnlpgzb = oldjson;
                                                       
                            Drugjson drugjson = new Drugjson();
                            drugjson.fyycx = ""; drugjson.ywyf = ""; drugjson.ywyl = "";
                            drugjson.ywzwmc = ""; drugjson.yysj = "";
                            paientInfor.yyqk = drugjson;
                            Personjson personjson = new Personjson();
                            personjson.bah = ""; personjson.ccsj = ""; personjson.jcsj = "";
                            personjson.zyyljgmc = ""; personjson.zyyy = "";
                            paientInfor.zyqk = personjson;
                            Familyjson  familyjson = new Familyjson();
                            familyjson.zyyy = ""; familyjson.zyyljgmc = ""; familyjson.jcsj = "";
                            familyjson.ccsj = ""; familyjson.bah = "";
                            paientInfor.jtbsqk = familyjson;
                            immunityjson immunityjson = new immunityjson();
                            immunityjson.jzjg = ""; immunityjson.jzmc = ""; immunityjson.jzsj = "";
                            paientInfor.fmyqk = immunityjson;
                            JbxxIGetDto.patient = paientInfor;
                            string jsonstrssadd = JsonConvert.SerializeObject(JbxxIGetDto).Replace("NULL", "").Replace("null", "");
                            // Logger.Debug(jsonstr);

                            var GWURLAdd = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "浉河区公卫数据上传地址")?.Remarks.ToString();
                            var UpdateURL = GWURL + "addJkdaTjBgjlInfo";
                            var retsss = PostAddReturnFunction(jsonstrssadd, UpdateURL);
                            if (retsss.code == "0")
                            {
                                var cus = cusreg.MapTo<TjlCustomerReg>();
                                cusreg.BespeakState = 4;
                                cus = _customerRegRepository.Update(cusreg);

                            }
                            if (retsss.success == "false")
                            {
                                outResult.Code = 0;
                                outResult.ErrInfo += retsss.message + ",";
                            }
                            #endregion
                        }

                    }
                    else
                    {

                        outResult.Code = 0;
                        outResult.ErrInfo = "没有档案！";                       
                    }
                    if (outResult.ErrInfo == null)
                    {
                        outResult.Code = 1;
                        outResult.ErrInfo = "成功";
                    }
                }
                return outResult;
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }
        #region 调用        
        public getAuthKeyReturnDto PostReturnFunction(string Json, string url)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(Json);
            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Expect = "application/json";

            request.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //StreamReader reader = new StreamReader(resStream, System.Text.Encoding.UTF8);
            var retString = reader.ReadToEnd();
            //int str = retString.IndexOf("{");
            //int end = retString.IndexOf("}");
            //retString = retString.Substring(retString.IndexOf("{"), retString.LastIndexOf("}")).Replace("\\", "");
            ////解析josn
            var jo = JsonConvert.DeserializeObject<getAuthKeyReturnDto>(retString);
            return jo;


        }
        #region xiugai
        public updateJkdaTjBgReturn PostUPTReturnFunction(string Json, string url)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(Json);
            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Expect = "application/json";

            request.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //StreamReader reader = new StreamReader(resStream, System.Text.Encoding.UTF8);
            var retString = reader.ReadToEnd();
            //int str = retString.IndexOf("{");
            //int end = retString.IndexOf("}");
            //retString = retString.Substring(retString.IndexOf("{"), retString.LastIndexOf("}")).Replace("\\", "");
            ////解析josn
            var jo = JsonConvert.DeserializeObject<updateJkdaTjBgReturn>(retString);
            return jo;


        }
        #endregion
        #region tianjia
        public AddJkdaTjBgReturn PostAddReturnFunction(string Json, string url)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(Json);
            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Expect = "application/json";

            request.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //StreamReader reader = new StreamReader(resStream, System.Text.Encoding.UTF8);
            var retString = reader.ReadToEnd();
            //int str = retString.IndexOf("{");
            //int end = retString.IndexOf("}");
            //retString = retString.Substring(retString.IndexOf("{"), retString.LastIndexOf("}")).Replace("\\", "");
            ////解析josn
            var jo = JsonConvert.DeserializeObject<AddJkdaTjBgReturn>(retString);
            return jo;


        }
        #endregion


        /// <summary>
        /// 上传老年人一体机数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public OutResult OutDataSouce(GsearchCustomerDto input)
        {

            try
            {
                OutResult outResult = new OutResult();
                outResult.Code = 1;
                var que = _customerRegRepository.GetAllIncluding(o => o.Customer, o => o.CustomerRegItems);
                var list = que.ToList();
                if (!string.IsNullOrEmpty(input.Name))
                {
                    que = que.Where(o => o.Customer.Name == input.Name);
                    list = que.ToList();
                }
                if (input.Sex.HasValue)
                {
                    que = que.Where(o => o.Customer.Sex == input.Sex);
                }
                if (!string.IsNullOrEmpty(input.IdCardNo))
                {
                    que = que.Where(o => o.Customer.IDCardNo == input.IdCardNo);
                    list = que.ToList();
                }
                if (!string.IsNullOrEmpty(input.CustomerBM))
                {
                    que = que.Where(o => o.CustomerBM == input.CustomerBM);
                }
                if (input.StartDate.HasValue && input.EndtDate.HasValue)
                {
                    que = que.Where(o => o.LoginDate >= input.StartDate && o.LoginDate <= input.EndtDate);
                }               
                var customerreg = que.ToList();

                foreach (TjlCustomerReg cusreg in customerreg)
                {
                    if (cusreg.CustomerRegItems.Count == 0)
                    {
                        continue;
                    }
                    GCustomerResultDto customerresult = new GCustomerResultDto();
                    var tenant = _Tenant.FirstOrDefault(o => o.Id == AbpSession.TenantId);
                    customerresult.UnitNo = tenant.TenancyName.Substring(1, tenant.TenancyName.Length - 1);
                    customerresult.UnitName = tenant.Name;
                    customerresult.DoctorId = "";//待处理
                    customerresult.DoctorName = "";
                    customerresult.RecordNo = cusreg.CustomerBM;
                    customerresult.MeasureTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//改为当前时间
                    GMemberDto gMember = new GMemberDto();
                    gMember.Name = cusreg.Customer.Name;
                    gMember.Mobile = cusreg.Customer.Mobile;
                    gMember.IdCode = cusreg.Customer.IDCardNo;
                    gMember.Age = cusreg.Customer.Age.ToString();
                    var cusSex = cusreg.Customer.Sex.ToString();//性别待处理
                    if (cusSex == "2")
                    {
                        cusSex = "0";
                    }
                    gMember.Sex = cusSex;
                    customerresult.Member = gMember;
                    GHeightDto gHeight = new GHeightDto();
                    gHeight.MacId = "";
                    gHeight.Height = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Height_Height")?.ItemResultChar ?? "";
                    gHeight.Weight = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Height_Weight")?.ItemResultChar ?? "";
                    gHeight.BMI = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Height_BMI")?.ItemResultChar;
                    customerresult.Height = gHeight;
                    GMinFatDto gMinFat = new GMinFatDto();
                    gMinFat.MacId = "";
                    gMinFat.FatMass = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_FatMass")?.ItemResultChar;
                    gMinFat.Water = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_Water")?.ItemResultChar;
                    gMinFat.WaterRate = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_WaterRate")?.ItemResultChar;
                    gMinFat.Height = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_Height")?.ItemResultChar;
                    gMinFat.Weight = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_Weight")?.ItemResultChar;
                    gMinFat.Bmi = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_Bmi")?.ItemResultChar;
                    gMinFat.BasicMetabolism = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_BasicMetabolism")?.ItemResultChar;
                    //1偏低 2标准 3 偏高   4 高
                    var tizhi = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_TiZhiResult")?.Symbol;
                    string tizhizt = "2";
                    if (tizhi == "L")
                    { tizhizt = "1"; }
                    else if (tizhi == "H")
                    { tizhizt = "3"; }
                    else if (tizhi == "HH")
                    { tizhizt = "4"; }
                    gMinFat.TiZhiResult = tizhizt;
                    //1消瘦 2标准 3 隐藏性肥胖 4 肌肉性肥胖/健壮 5肥胖
                    var tixing = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MinFat_TiXingResult")?.ItemResultChar;
                    int tixingbm = 0;
                    switch (tixing)
                    {
                        case "消瘦":
                            tixingbm = 1;
                            break;
                        case "标准":
                            tixingbm = 2;
                            break;
                        case "隐藏性肥胖":
                            tixingbm = 3;
                            break;
                        case "肌肉性肥胖":
                            tixingbm = 4;
                            break;
                        case "健壮":
                            tixingbm = 4;
                            break;
                        case "肥胖":
                            tixingbm = 5;
                            break;
                    }
                    gMinFat.TiXingResult = tixingbm.ToString();
                    customerresult.MinFat = gMinFat;
                    GBloodPressureDto gBloodPressure = new GBloodPressureDto();
                    gBloodPressure.MacId = "";
                    gBloodPressure.RateValue = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodPressure_RateValue")?.ItemResultChar;
                    gBloodPressure.HighPressure = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodPressure_HighPressure")?.ItemResultChar;
                    gBloodPressure.LowPressure = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodPressure_LowPressure")?.ItemResultChar;
                    customerresult.BloodPressure = gBloodPressure;
                    GBoDto gbo = new GBoDto();
                    gbo.MacId = "";
                    gbo.Value = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Bo_Value")?.ItemResultChar;
                    gbo.PulseValue = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Bo_PulseValue")?.ItemResultChar;
                    customerresult.Bo = gbo;
                    GPEEcgDto gpee = new GPEEcgDto();
                    gpee.MacId = "";
                    gpee.RR = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_RR")?.ItemResultChar;
                    gpee.PR = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_PR")?.ItemResultChar;
                    gpee.QRS = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_QRS")?.ItemResultChar;
                    gpee.QT = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_QT")?.ItemResultChar;
                    gpee.QTc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_QTc")?.ItemResultChar;
                    gpee.P_A = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_P_A")?.ItemResultChar;
                    gpee.T_A = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_T_A")?.ItemResultChar;
                    gpee.RV5 = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_RV5")?.ItemResultChar;
                    gpee.SV1 = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_SV1")?.ItemResultChar;
                    gpee.RV5_SV1 = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_RV5_SV1")?.ItemResultChar;
                    gpee.Hr = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_Hr")?.ItemResultChar;
                    gpee.Analysis = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_Analysis")?.ItemResultChar;
                    gpee.EcgImg = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "PEEcg_EcgImg")?.ItemResultChar;
                    customerresult.PEEcg = gpee;
                    GTemperatureDto gtemperature = new GTemperatureDto();
                    gtemperature.MacId = "";
                    gtemperature.Value = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Temperature_Value")?.ItemResultChar;
                    customerresult.Temperature = gtemperature;
                    GWhrDto gWhr = new GWhrDto();
                    gWhr.MacId = "";
                    gWhr.Yao = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Whr_Yao")?.ItemResultChar;
                    gWhr.Tun = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Whr_Tun")?.ItemResultChar;
                    gWhr.YaoTunBi = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Whr_YaoTunBi")?.ItemResultChar;
                    customerresult.Whr = gWhr;
                    GBloodSugarDto bloodSugarDto = new GBloodSugarDto();
                    bloodSugarDto.MacId = "";
                    bloodSugarDto.Value = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodSugar_Value")?.ItemResultChar;
                    bloodSugarDto.BloodsugarType = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodSugar_BloodsugarType")?.ItemResultChar;
                    customerresult.BloodSugar = bloodSugarDto;
                    GUaDto gUa = new GUaDto();
                    gUa.MacId = "";
                    gUa.Value = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Ua_Value")?.ItemResultChar;
                    customerresult.Ua = gUa;
                    GCholDto gChol = new GCholDto();
                    gChol.MacId = "";
                    gChol.Value = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Chol_Value")?.ItemResultChar;
                    customerresult.Chol = gChol;
                    GBloodFatDto gBloodFat = new GBloodFatDto();
                    gBloodFat.MacId = "";
                    gBloodFat.HdlChol = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodFat_HdlChol")?.ItemResultChar;
                    gBloodFat.Trig = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodFat_Trig")?.ItemResultChar;
                    gBloodFat.CalcLdl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "BloodFat_CalcLdl")?.ItemResultChar;
                    customerresult.BloodFat = gBloodFat;
                    GHbDto gHb = new GHbDto();
                    gHb.MacId = "";
                    gHb.Hb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Hb_Hb")?.ItemResultChar;
                    customerresult.Hb = gHb;
                    GUrinalysisDto gUrinalysis = new GUrinalysisDto();
                    gUrinalysis.MacId = "";
                    gUrinalysis.URO = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_URO")?.ItemResultChar;
                    gUrinalysis.BLD = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_BLD")?.ItemResultChar;
                    gUrinalysis.BIL = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_BIL")?.ItemResultChar;
                    gUrinalysis.KET = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_KET")?.ItemResultChar;
                    gUrinalysis.LEU = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_LEU")?.ItemResultChar;
                    gUrinalysis.GLU = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_GLU")?.ItemResultChar;
                    gUrinalysis.PRO = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_PRO")?.ItemResultChar;
                    gUrinalysis.PH = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_PH")?.ItemResultChar;
                    gUrinalysis.NIT = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_NIT")?.ItemResultChar;
                    gUrinalysis.SG = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_SG")?.ItemResultChar;
                    gUrinalysis.MAL = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_MAL")?.ItemResultChar;
                    gUrinalysis.Result = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "Urinalysis_Result")?.ItemResultChar;
                    customerresult.Urinalysis = gUrinalysis;

                    GMedicalInfoDto gMedicalInfo = new GMedicalInfoDto();


                    var fbgd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_HEPATOMEGALY")?.ItemResultChar;
                    int fbgdbm = 0;
                    if (fbgd != "无")
                    {
                        fbgdbm = 1;
                        gMedicalInfo.ABDO_HEPATOMEGALY_OTH = fbgd;
                        //  cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_HEPATOMEGALY_OTH")?.ItemResultChar;
                    }
                    gMedicalInfo.ABDO_HEPATOMEGALY = fbgdbm.ToString();


                    var xbbk = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_MASSES")?.ItemResultChar;
                    int xbbkbm = 0;
                    if (xbbk != "无")
                    {
                        xbbkbm = 1;
                        gMedicalInfo.ABDO_MASSES_OTHERS = xbbk;
                        //cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_MASSES_OTHERS")?.ItemResultChar;

                    }
                    gMedicalInfo.ABDO_MASSES = xbbkbm.ToString();

                    //腹部-压痛0 无 1有
                    var xbyt = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_PRESS_PAIN")?.ItemResultChar;
                    int xbytbm = 0;
                    if (xbyt != "无")
                    {
                        xbytbm = 1;
                        gMedicalInfo.ABDO_PRESS_PAIN_OTH = xbyt;
                        //  cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_PRESS_PAIN_OTH")?.ItemResultChar;

                    }
                    gMedicalInfo.ABDO_PRESS_PAIN = xbytbm.ToString();

                    var fbydzc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_SHIFTING_DULL")?.ItemResultChar;
                    int fbydzcbm = 0;
                    if (fbydzc != "无")
                    {
                        fbydzcbm = 1;
                        gMedicalInfo.ABDO_SHIFTING_DULL_OTH = fbydzc;
                        // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_SHIFTING_DULL_OTH")?.ItemResultChar;
                    }
                    gMedicalInfo.ABDO_SHIFTING_DULL = fbydzcbm.ToString();


                    var fbpd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_SPLENOMEGALY")?.ItemResultChar;
                    int fbpdbm = 0;
                    if (fbpd != "无")
                    {
                        fbpdbm = 1;
                        gMedicalInfo.ABDO_SPLENOMEGALY_OTH = fbpd;
                        // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ABDO_SPLENOMEGALY_OTH")?.ItemResultChar;
                    }
                    gMedicalInfo.ABDO_SPLENOMEGALY = fbpdbm.ToString();

                    //1听见 2听不清或无法听见               
                    var tl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_AUDITION")?.ItemResultChar;
                    int tlbm = 1;
                    if (tl != "听见")
                    {
                        tlbm = 2;
                    }
                    gMedicalInfo.AUDITION = tlbm.ToString();

                    gMedicalInfo.HEART_RATE = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_HEART_RATE")?.ItemResultChar;

                    //心脏-心律1 齐 2不齐3绝对不齐 
                    var xl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_CARDIAC_RHYTHM")?.ItemResultChar;
                    int xlBM = 0;
                    switch (xl)
                    {
                        case "齐":
                            xlBM = 1;
                            break;
                        case "不齐":
                            xlBM = 2;
                            break;
                        case "绝对不齐":
                            xlBM = 3;
                            break;
                    }
                    gMedicalInfo.CARDIAC_RHYTHM = xlBM.ToString();
                    //心脏-杂音0 无 1 有
                    var xzzy = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_CARDIAC_SOUFFLE")?.ItemResultChar;
                    int xzzyBM = 0;
                    if (xzzy != "无")
                    {
                        xzzyBM = 1;
                        gMedicalInfo.CARDIAC_SOUFFLE_OTHERS = xzzy;
                        // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_CARDIAC_SOUFFLE_OTHERS")?.ItemResultChar;
                    }
                    gMedicalInfo.CARDIAC_SOUFFLE = xzzyBM.ToString();


                    // gMedicalInfo.DENTAL_CARY = 
                    //  cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTAL_CARY")?.ItemResultChar;
                    gMedicalInfo.DENTAL_CARY_1 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTAL_CARY_1")?.ItemResultChar;
                    gMedicalInfo.DENTAL_CARY_2 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTAL_CARY_2")?.ItemResultChar;
                    gMedicalInfo.DENTAL_CARY_3 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTAL_CARY_3")?.ItemResultChar;
                    gMedicalInfo.DENTAL_CARY_4 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTAL_CARY_4")?.ItemResultChar;
                    var car1 = gMedicalInfo.DENTAL_CARY_1 ?? "";
                    var car2 = gMedicalInfo.DENTAL_CARY_2 ?? "";
                    var car3 = gMedicalInfo.DENTAL_CARY_3 ?? "";
                    var car4 = gMedicalInfo.DENTAL_CARY_4 ?? "";

                    if (car1.Replace("null", "") == "" && car2.Replace("null", "") == "" && car3.Replace("null", "") == "" && car4.Replace("null", "") == "")
                    {
                        gMedicalInfo.DENTAL_CARY = "0";
                    }
                    else
                    {
                        gMedicalInfo.DENTAL_CARY = "1";
                    }

                    //口腔-齿列正常1是 0否
                    var kqlc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTITIONDENTURE")?.ItemResultChar;
                    int kqlcbm = 0;
                    if (kqlc == "是")
                    {
                        kqlcbm = 1;

                    }
                    gMedicalInfo.DENTITIONDENTURE = kqlcbm.ToString();

                    // gMedicalInfo.DENTURE = 
                    //    cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTURE")?.ItemResultChar;
                    gMedicalInfo.DENTURE_1 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTURE_1")?.ItemResultChar;
                    gMedicalInfo.DENTURE_2 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTURE_2")?.ItemResultChar;
                    gMedicalInfo.DENTURE_3 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTURE_3")?.ItemResultChar;
                    gMedicalInfo.DENTURE_4 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_DENTURE_4")?.ItemResultChar;
                    var dentue1 = gMedicalInfo.DENTURE_1 ?? "";
                    var dentue2 = gMedicalInfo.DENTURE_2 ?? "";
                    var dentue3 = gMedicalInfo.DENTURE_3 ?? "";
                    var dentue4 = gMedicalInfo.DENTURE_4 ?? "";
                    if (dentue1.Replace("null", "") == "" && dentue2.Replace("null", "") == "" && dentue3.Replace("null", "") == "" && dentue4.Replace("null", "") == "")
                    {
                        gMedicalInfo.DENTURE = "0";
                    }
                    else
                    {
                        gMedicalInfo.DENTURE = "1";
                    }
                    //心电1 正常 2异常

                    var xd = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ELECTROCARDIOGRAM")?.ItemResultChar;
                    int xdbm = 1;
                    if (xd != "正常")
                    {
                        xdbm = 2;
                        gMedicalInfo.ELECTROCARDIOGRAM_EXCEP = xd;
                        // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_ELECTROCARDIOGRAM_EXCEP")?.ItemResultChar;

                    }
                    gMedicalInfo.ELECTROCARDIOGRAM = xdbm.ToString();
                    //口腔-口唇1红润 2苍白3发绀4皲裂5疱疹

                    var kqch = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LIPS")?.ItemResultChar;
                    int kqchbm = 0;
                    switch (kqch)
                    {
                        case "红润":
                            kqchbm = 1;
                            break;
                        case "苍白":
                            kqchbm = 2;
                            break;
                        case "发绀":
                            kqchbm = 3;
                            break;
                        case "皲裂":
                            kqchbm = 4;
                            break;
                        case "疱疹":
                            kqchbm = 5;
                            break;
                    }
                    gMedicalInfo.LIPS = kqchbm.ToString();
                    var yzx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LUNG_BARREL_CHEST")?.ItemResultChar;
                    int zyxbm = 0;
                    if (yzx == "是")
                    {
                        zyxbm = 1;
                    }
                    gMedicalInfo.LUNG_BARREL_CHEST = zyxbm.ToString();


                    var fhx = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LUNG_BREATH_SOUND")?.ItemResultChar;
                    int fhxbm = 1;
                    if (fhx != "正常")
                    {
                        fhxbm = 2;
                        gMedicalInfo.LUNG_BREATH_SOUND_EXCEP = fhx;
                        //  cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LUNG_BREATH_SOUND_EXCEP")?.ItemResultChar;
                    }
                    gMedicalInfo.LUNG_BREATH_SOUND = fhxbm.ToString();

                    //1无 2干罗音 3湿罗音4其他
                    var fly = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LUNG_RHONCHUS")?.ItemResultChar;
                    int flybm = 4;
                    switch (fly)
                    {
                        case "无":
                            flybm = 1;
                            break;
                        case "干罗音":
                            flybm = 2;
                            break;
                        case "湿罗音":
                            flybm = 3;
                            break;
                    }
                    gMedicalInfo.LUNG_RHONCHUS = flybm.ToString();
                    if (flybm == 4)
                    {
                        gMedicalInfo.LUNG_RHONCHUS_EXCEPTION = fly;
                        // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LUNG_RHONCHUS_EXCEPTION")?.ItemResultChar;

                    }
                    //淋巴结 1未触及2锁骨上3腋窝 4其他 
                    var lbj = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LYMPH_NODE")?.ItemResultChar;
                    int lbjBM = 4;
                    switch (lbj)
                    {
                        case "未触及":
                            lbjBM = 1;
                            break;
                        case "锁骨上":
                            lbjBM = 2;
                            break;
                        case "腋窝":
                            lbjBM = 3;
                            break;
                    }
                    gMedicalInfo.LYMPH_NODE = lbjBM.ToString();
                    if (lbjBM == 4)
                    {
                        gMedicalInfo.LYMPH_NODE_OTHERS = lbj;
                        // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_LYMPH_NODE_OTHERS")?.ItemResultChar;
                    }
                    // gMedicalInfo.MISS_TEETH = 
                    // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_MISS_TEETH")?.ItemResultChar;
                    gMedicalInfo.MISS_TEETH_1 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_MISS_TEETH_1")?.ItemResultChar;
                    gMedicalInfo.MISS_TEETH_2 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_MISS_TEETH_2")?.ItemResultChar;
                    gMedicalInfo.MISS_TEETH_3 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_MISS_TEETH_3")?.ItemResultChar;
                    gMedicalInfo.MISS_TEETH_4 =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_MISS_TEETH_4")?.ItemResultChar;
                    var mistee1 = gMedicalInfo.MISS_TEETH_1 ?? "";
                    var mistee2 = gMedicalInfo.MISS_TEETH_2 ?? "";
                    var mistee3 = gMedicalInfo.MISS_TEETH_3 ?? "";
                    var mistee4 = gMedicalInfo.MISS_TEETH_4 ?? "";
                    if (mistee1.Replace("null", "") == "" && mistee2.Replace("null", "") == "" && mistee3.Replace("null", "") == "" && mistee4.Replace("null", "") == "")
                    {
                        gMedicalInfo.MISS_TEETH = "0";
                    }
                    else
                    {
                        gMedicalInfo.MISS_TEETH = "1";
                    }
                    //运动功能1可顺利完成2无法独立完成其中任何一个动作

                    var ydnl = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_MOTOR_FUNCTION")?.ItemResultChar;
                    int ydnlbm = 1;
                    if (ydnl != "可顺利完成")
                    {
                        ydnlbm = 2;

                    }
                    gMedicalInfo.MOTOR_FUNCTION = ydnlbm.ToString();

                    //口腔-咽部1无充血 2充血3淋巴滤泡增生

                    var kqyb = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_PHARYNGEALPORTION")?.ItemResultChar;
                    var kqybbm = 0;
                    switch (kqyb)
                    {
                        case "无充血":
                            kqybbm = 1;
                            break;
                        case "充血":
                            kqybbm = 2;
                            break;
                        case "淋巴滤泡增生":
                            kqybbm = 3;
                            break;

                    }
                    gMedicalInfo.PHARYNGEALPORTION = kqybbm.ToString();
                    gMedicalInfo.PULSE_FREQUENCY =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_PULSE_FREQUENCY")?.ItemResultChar;
                    gMedicalInfo.RESPIRATORY_RATE =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_RESPIRATORY_RATE")?.ItemResultChar;
                    //1正常 2潮红3苍白 4发绀5黄染 6色素沉着 7 其他
                    var pf = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_SKIN")?.ItemResultChar;
                    int pfBM = 7;
                    switch (pf)
                    {
                        case "正常":
                            pfBM = 1;
                            break;
                        case "潮红":
                            pfBM = 2;
                            break;
                        case "苍白":
                            pfBM = 3;
                            break;
                        case "发绀":
                            pfBM = 4;
                            break;
                        case "黄染":
                            pfBM = 5;
                            break;
                        case "色素沉着":
                            pfBM = 6;
                            break;
                    }
                    gMedicalInfo.SKIN = pfBM.ToString();
                    if (pfBM == 7)
                    {
                        gMedicalInfo.SKIN_OTHERS = pf;
                        //cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_SKIN_OTHERS")?.ItemResultChar;
                    }
                    gMedicalInfo.STRAIGHTEN_VISION_LEFT_EYE =
                    cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_STRAIGHTEN_VISION_LEFT_EYE")?.ItemResultChar;

                    gMedicalInfo.STRAIGHTEN_VISION_RIGHT_EYE =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_STRAIGHTEN_VISION_RIGHT_EYE")?.ItemResultChar;

                    //腹部B超1 正常 2异常
                    var fbbc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_TYPE_B_ULTRASONIC")?.ItemDiagnosis;
                    int fbbcbm = 1;
                    if (fbbc != "正常")
                    {
                        fbbcbm = 2;
                        gMedicalInfo.TYPE_B_ULTRASONIC_EXCEP = fbbc;
                        // cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_TYPE_B_ULTRASONIC_EXCEP")?.ItemResultChar;
                    }
                    gMedicalInfo.TYPE_B_ULTRASONIC = fbbcbm.ToString();


                    var qtbc = cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_TYPE_B_ULTRASONIC_QT")?.ItemDiagnosis;
                    int qtbcbm = 1;
                    if (qtbc != "正常")
                    {
                        qtbcbm = 2;
                        gMedicalInfo.TYPE_B_ULTRASONIC_EXCEP_QT = qtbc;
                        //cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_TYPE_B_ULTRASONIC_EXCEP_QT")?.ItemResultChar;

                    }
                    gMedicalInfo.TYPE_B_ULTRASONIC_QT = qtbcbm.ToString();

                    gMedicalInfo.VISION_LEFT_EYE =
                         cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_VISION_LEFT_EYE")?.ItemResultChar;
                    gMedicalInfo.VISION_RIGHT_EYE =
                        cusreg.CustomerRegItems.FirstOrDefault(o => o.ItemBM.NameEngAbr == "MedicalInfo_VISION_RIGHT_EYE")?.ItemResultChar;

                    customerresult.MedicalInfo = gMedicalInfo;

                    string jsonstr = JsonConvert.SerializeObject(customerresult).Replace("NULL", "").Replace("null", "");
                   // Logger.Debug(jsonstr);

                    var GWURL = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "公卫数据上传地址")?.Remarks.ToString();
                    // JObject ret =  PostFunction(jsonstr, GWURL);
                    string strlog = jsonstr;
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    path = path + "Log";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path += "\\" + input.CustomerBM + "一体机.txt";
                    System.IO.File.WriteAllText(path, strlog);


                    var ret = PostFunction(jsonstr, GWURL);
                    if (ret.success == "false")
                    {
                        outResult.Code = 0;
                        outResult.ErrInfo += ret.message + ",";
                    }
                }
                if (outResult.ErrInfo == "")
                {
                    outResult.Code = 1;
                    outResult.ErrInfo = "成功";
                }
                return outResult;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取LIS申请单及项目
        /// </summary>
        /// <param name="searchApplyList"></param>
        /// <returns></returns>
        public OutResult GetItemsListApply(SearchApplyListDto searchApplyList)
        {
            //var GWURL = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "公卫Lis接口地址")?.Remarks.ToString();
            //var cliss = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "公卫Lis接口类")?.Remarks.ToString();
            //object[] objs = new object[3];
            // GCustomerResultDto customerresult = new GCustomerResultDto();
            //var tenant = _Tenant.FirstOrDefault(o => o.Id == AbpSession.TenantId);
            //objs[0] = 102;
            //objs[1] = "4";
            string datestar = "";
            string dateend = "";
            if (searchApplyList.APPLY_TIME_START.HasValue && searchApplyList.APPLY_TIME_END.HasValue)
            {
                datestar = searchApplyList.APPLY_TIME_START.Value.ToString("yyy-MM-dd");
                dateend = searchApplyList.APPLY_TIME_END.Value.ToString("yyy-MM-dd");
            }
            string xml = string.Format(@"
<ROOT>
<APPLY_NO>{0}</APPLY_NO>
<ARCHIVE_ID>{1}</ARCHIVE_ID>
<APPLY_TIME_START>{2}</APPLY_TIME_START>
<APPLY_TIME_END>{3}</APPLY_TIME_END>
<SICK_NAME>{4}</SICK_NAME>
<ARCHIVE_CODE>{5}</ARCHIVE_CODE>
<IDENTITY_CARD_NO>{6}</IDENTITY_CARD_NO>
<SICK_AGE_START>{7}</SICK_AGE_START>
<SICK_AGE_END>{8}</SICK_AGE_END>
<SICK_SEX>{9}</SICK_SEX>
</ROOT>", searchApplyList.APPLY_NO, searchApplyList.ARCHIVE_ID, datestar,
dateend, searchApplyList.SICK_NAME, searchApplyList.ARCHIVE_CODE,
searchApplyList.IDENTITY_CARD_NO, searchApplyList.SICK_AGE_START, searchApplyList.SICK_AGE_END, searchApplyList.SICK_SEX);
            //string xml = XmlConvertor.ObjectToXml(searchApplyList);
            //objs[2] = xml;
            //object ab = WebServiceHelper.InvokeWebService(GWURL, cliss, "GetApplyList", objs);
            //WebGW 
            //            string csxml = string.Format(@"
            //<ROOT>
            //<ARCHIVE_ID>123</ARCHIVE_ID>
            //<APPLY_NO>456</APPLY_NO>
            //<ITEM_NO>1001</ITEM_NO>
            //<ITEM_NAME>身高</ITEM_NAME>
            //<SICK_NAME>姓名</SICK_NAME>
            //<SICK_ADDRESS>地址</SICK_ADDRESS>
            //<SICK_SEX>性别</SICK_SEX>
            //<SICK_AGE>年龄</SICK_AGE>
            //<APPLY_TIME>2019-1-24</APPLY_TIME>
            //<APPLY_OPERATOR_NAME>张三</APPLY_OPERATOR_NAME>
            //<APPLY_DPT_NAME>检验科</APPLY_DPT_NAME>
            //<STATUS>1</STATUS>
            //<STATUS_NAME>已申请</STATUS_NAME>
            //<IDENTITY_CARD_NO>120224195903210730</IDENTITY_CARD_NO>
            //<ZONE_NAME>居委会</ZONE_NAME>
            //<ARCHIVE_CODE>333</ARCHIVE_CODE>
            //<REMARK>备注</REMAsRK>
            //</ROOT>");
            var tenant = _Tenant.FirstOrDefault(o => o.Id == AbpSession.TenantId);
            int UnitNo = int.Parse(tenant.TenancyName.Substring(1, tenant.TenancyName.Length - 1));

            Webwglis.LisService ser = new Webwglis.LisService();
            string ab = ser.GetApplyList(UnitNo, "4", xml);
           // ser.GetApplyList()
            OutResult outResult = new OutResult();
            outResult.ErrInfo = ab.ToString();
            var ds1 = XmlConvertor.CXmlToDataSet(ab.ToString());

            string applyData = "";
            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                for (int n = 0; n < ds1.Tables[0].Rows.Count; n++)
                {
                    if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[n]["IDENTITY_CARD_NO"].ToString()))
                    {
                        string cardid = ds1.Tables[0].Rows[n]["IDENTITY_CARD_NO"].ToString();

                        TjlCustomer tjlCustomer = _customerRepository.FirstOrDefault(o => o.IDCardNo == cardid);
                        if (tjlCustomer == null)
                        {
                            continue;
                        }
                        int sumsate = (int)SummSate.NotAlwaysCheck;
                        var cusreg = tjlCustomer.CustomerReg.Where(o => o.SummSate == sumsate).OrderByDescending(o => o.LoginDate);
                        if (cusreg.Count() == 0)
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[n]["ARCHIVE_ID"].ToString()))
                            tjlCustomer.WorkNumber = ds1.Tables[0].Rows[n]["ARCHIVE_ID"].ToString();
                        if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[n]["ARCHIVE_CODE"].ToString()))
                            tjlCustomer.SectionNum = ds1.Tables[0].Rows[n]["ARCHIVE_CODE"].ToString();
                        _customerRepository.Update(tjlCustomer);

                        if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[n]["APPLY_NO"].ToString()))
                        {
                            string APPLY_NO = ds1.Tables[0].Rows[n]["APPLY_NO"].ToString();
                            string ITEM_NO = ds1.Tables[0].Rows[n]["ITEM_NO"].ToString();
                            TjlLisApply tjlLisApply = _TjlLisApply.FirstOrDefault(o => o.ApplyNO == APPLY_NO && o.ItemGroup.OrderNum == int.Parse(ITEM_NO));
                            if (tjlLisApply == null)
                            {
                                tjlLisApply = new TjlLisApply();
                                tjlLisApply.Id = Guid.NewGuid();
                                tjlLisApply.ApplyNO = ds1.Tables[0].Rows[n]["APPLY_NO"].ToString();
                                int hisid = int.Parse(ds1.Tables[0].Rows[n]["ITEM_NO"].ToString());
                                TbmItemGroup tbmItemGroup = _itemGroupRepository.FirstOrDefault(o => o.OrderNum == hisid);
                                if (tbmItemGroup == null)
                                {
                                    continue;
                                }
                                tjlLisApply.ItemGroupID = tbmItemGroup.Id;
                                tjlLisApply.ItemGroupName = tbmItemGroup.ItemGroupName;
                                tjlLisApply.ItemGroup = tbmItemGroup;
                                tjlLisApply.CustomerRegBMId = cusreg.First().Id;
                                tjlLisApply.APPLY_OPERATOR_NAME = ds1.Tables[0].Rows[n]["APPLY_OPERATOR_NAME"].ToString();
                                tjlLisApply.APPLY_TIME = DateTime.Parse(ds1.Tables[0].Rows[n]["APPLY_TIME"].ToString());
                                tjlLisApply.STATUS = ds1.Tables[0].Rows[n]["STATUS"].ToString();
                                tjlLisApply.STATUSNAME = ds1.Tables[0].Rows[n]["STATUS_NAME"].ToString();
                                tjlLisApply.REMARK = ds1.Tables[0].Rows[n]["REMARK"].ToString();
                                _TjlLisApply.Insert(tjlLisApply);
                                if (!applyData.Contains(ds1.Tables[0].Rows[n]["APPLY_NO"].ToString()))
                                {
                                    applyData += "<APPLY_NO>" + ds1.Tables[0].Rows[n]["APPLY_NO"].ToString() + "</APPLY_NO>";
                                }

                            }
                        }
                    }
                }
                if (applyData != "")
                {
                    string applyDataxml = "<ROOT>" + applyData + "</ROOT>";
                    UpdateLisStatus(2, applyDataxml);
                }

            }
            return outResult;
        }


        /// <summary>
        /// 上传LIS报告
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutResult UpdateLisReport(GsearchCustomerDto input)
        {
            try
            {
                //var GWURL = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "公卫Lis接口地址")?.Remarks.ToString();
                //var cliss = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "公卫Lis接口类")?.Remarks.ToString();
                GCustomerResultDto customerresult = new GCustomerResultDto();
                OutResult outResult = new OutResult();
                outResult.Code = 1;
                outResult.ErrInfo = "";
                var que = _customerRegRepository.GetAllIncluding(o => o.Customer, o => o.CustomerRegItems);
                if (!string.IsNullOrEmpty(input.Name))
                {
                    que = que.Where(o => o.Customer.Name == input.Name);
                }
                if (input.Sex.HasValue)
                {
                    que = que.Where(o => o.Customer.Sex == input.Sex);
                }
                if (!string.IsNullOrEmpty(input.IdCardNo))
                {
                    que = que.Where(o => o.Customer.IDCardNo == input.IdCardNo);
                }
                if (!string.IsNullOrEmpty(input.CustomerBM))
                {
                    que = que.Where(o => o.CustomerBM == input.CustomerBM);
                }
                if (input.StartDate.HasValue && input.EndtDate.HasValue)
                {
                    que = que.Where(o => o.LoginDate >= input.StartDate && o.LoginDate <= input.EndtDate);
                }
                var customerreg = que.ToList();
                var tenant = _Tenant.FirstOrDefault(o => o.Id == AbpSession.TenantId);
                int UnitNo = int.Parse(tenant.TenancyName.Substring(1, tenant.TenancyName.Length - 1));
                Webwglis.LisService ser = new Webwglis.LisService();
                foreach (TjlCustomerReg cusreg in customerreg)
                {
                    var cusitems = cusreg.CustomerRegItems.Where(o => o.ItemResultChar != null && o.ItemResultChar != "").ToList();
                    if (cusitems.Count == 0)
                    {
                        continue;
                    }
                    var cusApplylist = _TjlLisApply.GetAll().Where(o => o.CustomerRegBMId == cusreg.Id).GroupBy(o => o.ApplyNO).ToList();

                    List<TjlLisApply> listapplysave = new List<TjlLisApply>();
                    //插入申请单
                    if (cusApplylist.Count() == 0)
                    {
                        string barset = "";
                        var barprintls = _TjlCustomerBarPrintInfo.GetAll().Where(o => o.CustomerReg_Id == cusreg.Id).OrderByDescending(o => o.CreationTime).ToList();
                        string resulItem = "";
                        string empName = "admin";
                        foreach (var cusbar in barprintls)
                        {
                            List<TjlLisApply> listapplyls = new List<TjlLisApply>();
                            if (!barset.Contains(cusbar.BarSettingsId.ToString()))
                            {
                                resulItem = "";
                                barset += cusbar.BarSettingsId.ToString() + "|";
                                foreach (var item in cusbar.CustomerBarPrintInfo)
                                {
                                    //如果条码是体检号不上传
                                    if (cusbar.BarNumBM == cusreg.CustomerBM)
                                    {
                                        continue;
                                    }
                                    var cusItem = cusreg.CustomerItemGroup.FirstOrDefault(o => o.ItemGroupBM_Id == item.ItemGroup.Id);
                                    if (cusItem != null)
                                    {

                                        resulItem += string.Format(@"	<PROJECT>
				<APPLY_NO>{0}</APPLY_NO>
				<ITEM_NO>{1}</ITEM_NO>
				<REVIEW_FLAG>{2}</REVIEW_FLAG>
				<REMARK></REMARK>
			</PROJECT>", UnitNo + cusbar.BarNumBM, cusItem.ItemGroupBM.HISID, 0);


                                        TjlLisApply listapply = new TjlLisApply();
                                        listapply.ApplyNO = UnitNo + cusbar.BarNumBM;
                                        listapply.APPLY_OPERATOR_NAME = empName;
                                        listapply.APPLY_TIME = DateTime.Now;
                                        listapply.CustomerRegBM = null;
                                        listapply.CustomerRegBMId = cusreg.Id;
                                        listapply.Id = Guid.NewGuid();
                                        listapply.ItemGroup = null;
                                        listapply.ItemGroupID = cusItem.ItemGroupBM_Id;
                                        listapply.ItemGroupName = cusItem.ItemGroupName;
                                        listapply.REMARK = "";
                                        listapply.STATUS = "1";
                                        listapply.STATUSNAME = "已提交";
                                        listapplyls.Add(listapply);
                                    }
                                }
                                if (resulItem != "")
                                {
                                    string strxml = string.Format(@"<ROOT><REPORT_INFO>
				<APPLY_NO>{0}</APPLY_NO>
				<APPLY_TIME>{1}</APPLY_TIME>
				<APPLY_OPERATOR_NAME>{2}</APPLY_OPERATOR_NAME>
				<APPLY_DPT_NAME>检验科</APPLY_DPT_NAME>
		</REPORT_INFO>
		<REPORT_DETAIL>", UnitNo + cusbar.BarNumBM, cusreg.LoginDate.Value.ToString("yyyy/MM/dd HH:mm:ss"), empName);
                                    strxml += resulItem + "</REPORT_DETAIL></ROOT>";

                                    if (strxml != "")
                                    {
                                        string path = AppDomain.CurrentDomain.BaseDirectory;
                                        if (!string.IsNullOrEmpty(path))
                                        {
                                            path = AppDomain.CurrentDomain.BaseDirectory + "Log";
                                            if (!Directory.Exists(path))
                                            {
                                                Directory.CreateDirectory(path);
                                            }
                                            path = path + "\\" + input.CustomerBM + "Lis申请单.txt";



                                            StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                                            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "-->" + UnitNo + "-->" + cusreg.Customer.IDCardNo + "" + "-->" + strxml + "");

                                            sw.Close();

                                        }
                                        int result = ser.UpdateLisApply(UnitNo, cusreg.Customer.IDCardNo, strxml);
                                        if (result == 1)
                                        {
                                            foreach (var apy in listapplyls)
                                            {
                                                var inlistapply = _TjlLisApply.Insert(apy);
                                                listapplysave.Add(inlistapply);
                                            }
                                            cusApplylist = listapplysave.GroupBy(o => o.ApplyNO).ToList();
                                        }
                                        else
                                        {
                                            outResult.Code = 0;
                                            outResult.ErrInfo += "提交LIS申请单：" + cusreg.CustomerBM + "-" + cusreg.Customer.Name + "失败:" + result.ToString();
                                        }

                                    }
                                }

                            }
                        }


                    }
                    //不同申请编码
                    foreach (var cusapply in cusApplylist)
                    {
                        string empname = "";
                        string shname = "";
                        DateTime reportTime = new DateTime();
                        string applyitemxml = "";
                        //申请单下不同组合
                        foreach (var group in cusapply)
                        {
                            //组合下项目
                            var cusitemlis = cusreg.CustomerRegItems.Where(o => o.ItemGroupBMId == group.ItemGroupID && o.ItemResultChar != null && o.ItemResultChar != "");
                            foreach (var cusitem in cusitemlis)
                            {
                                empname = cusitem.InspectEmployeeBM?.Name ?? "";
                                shname = cusitem.CheckEmployeeBM?.Name ?? "";
                                reportTime = DateTime.Now;//当前时间
                                string itemxml = string.Format(@"<PROJECT>
				<KPI_NO>{0}</KPI_NO>
				<KPI_NAME>{1}</KPI_NAME>
				<KPI_RESULT>{2}</KPI_RESULT>
				<KPI_ABBREVIATION>{3}</KPI_ABBREVIATION>
				<KPI_TIP>{4}</KPI_TIP>
				<REFERENCE_VALUE>{5}</REFERENCE_VALUE>
				<UNIT>{6}</UNIT>
				<SUB_ID>{7}</SUB_ID>
			</PROJECT>", cusitem.ItemBM.StandardCode, cusitem.ItemName, cusitem.ItemResultChar, cusitem.ItemBM.NamePM,
                cusitem.Symbol, cusitem.Stand, cusitem.Unit, cusitem.ItemOrder);
                                applyitemxml += itemxml;
                            }
                        }
                        string prt = string.Format(@"
<REPORT_INFO>
	<APPLY_NO>{0}</APPLY_NO>
    <COLLECT_DATE>{1}</COLLECT_DATE>
    <SAMPLE_TYPE></SAMPLE_TYPE>
	<BAR_CODE></BAR_CODE>
	<BED_NUMBER></BED_NUMBER>
	<SICK_FLAG>{2}</SICK_FLAG>
    <SAMPLE_STATUS>3</SAMPLE_STATUS>
    <CLINICAL_IMPRESSION></CLINICAL_IMPRESSION>
    <SEND_CHECK_DEPT>检验科</SEND_CHECK_DEPT>
	<SEND_CHECK_DOCTOR></SEND_CHECK_DOCTOR>
	<REPORT_DATE>{3}</REPORT_DATE>
	<INSPECTION_DOCTOR>{4}</INSPECTION_DOCTOR>
	<AUDIT_DOCTOR>{5}</AUDIT_DOCTOR>
	<SERIAL_NUMBER></SERIAL_NUMBER>
	<INSPECTION_ORG></INSPECTION_ORG>
</REPORT_INFO>
", cusapply.First().ApplyNO, cusapply.First().CreationTime.ToString("yyyy-MM-dd"), cusreg.Customer.ArchivesNum,
     reportTime.ToString("yyyy-MM-dd"), empname, shname);
                        string appList = "<ROOT>" + prt + "<REPORT_DETAIL>" + applyitemxml + "</REPORT_DETAIL></ROOT>";

                        //上传公卫
                        //object[] objs = new object[3];
                        // objs[0] = tenant.TenancyName.Substring(1, tenant.TenancyName.Length - 1);                   
                        // objs[1] = cusreg.CustomerBM;
                        // objs[2] = appList;
                        // object ab = WebServiceHelper.InvokeWebService(GWURL, cliss, "UpdateLisReport", objs);
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        if (!string.IsNullOrEmpty(path))
                        {
                            path = AppDomain.CurrentDomain.BaseDirectory + "Log";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            path = path + "\\" + input.CustomerBM + "Lis.txt";



                            StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                            //sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "-->" + UnitNo + "-->" + "" + "-->");
                            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "-->" + UnitNo + "-->" + cusapply.First().ApplyNO ?? "" + "-->" + appList);

                            sw.Close();

                        }
                        int ab = ser.UpdateLisReport(UnitNo, cusapply.First().ApplyNO, appList);
                        if (ab.ToString() == "0")
                        {
                            outResult.Code = 0;
                            outResult.ErrInfo += "申请单：" + cusapply.First().ApplyNO + "失败";
                        }

                    }
                }
                if (outResult.ErrInfo == "")
                {
                    outResult.Code = 1;
                    outResult.ErrInfo = "成功";
                }
                return outResult;
            }
            catch
            {
                throw;
            }
        }


        #region 调用        
        public ReJSON PostFunction(string Json, string url)
        {
            Encoding encoding = Encoding.UTF8;    
            byte[] data = encoding.GetBytes(Json);
            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";          
            request.ContentLength = data.Length;
            request.Expect = "application/json";

            request.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();        
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //StreamReader reader = new StreamReader(resStream, System.Text.Encoding.UTF8);
            var retString = reader.ReadToEnd();
            int str = retString.IndexOf("{");
            int end = retString.IndexOf("}");
            retString = retString.Substring(retString.IndexOf("{"), retString.LastIndexOf("}")).Replace("\\", "");
            //解析josn
            var jo = JsonConvert.DeserializeObject<ReJSON>(retString);
            return jo;
        }
 
        /// <summary>
        /// 回传Lis状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="applyData"></param>
        /// <returns></returns>
        public OutResult UpdateLisStatus(int status, string applyData)
        {
            //var GWURL = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "公卫Lis接口地址")?.Remarks.ToString();
            //var cliss = _BasicDictionaryy.FirstOrDefault(o => o.Type == "PublicHealthURL" && o.Text == "公卫Lis接口类")?.Remarks.ToString();
            //object[] objs = new object[2];
            //objs[0] = 2;
            //objs[1] = applyData;
            //object ab = WebServiceHelper.InvokeWebService(GWURL, cliss, "UpdateLisStatus", objs);
            Webwglis.LisService ser = new Webwglis.LisService();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + "Log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!System.IO.File.Exists(path))
                {
                    FileStream fs = System.IO.File.Create(path);
                    fs.Close();
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "-->" + status + "-->" + applyData + "-->");
                    sw.Close();
                }
            }
            int ab = ser.UpdateLisStatus(2, applyData);

            return new OutResult();
        }


        #endregion
        /// <summary>
        /// 用户实体类
        /// </summary>
        public class ReJSON
        {
            //是否成功true false
            public string success { get; set; }

            //异常信息
            public string message { get; set; }
        }

        #endregion
        #endregion

        public GetkdaGrJbxxIReturnData GetkdaGrJbxxIReturnFunction(string Json, string url)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(Json);
            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Expect = "application/json";
            request.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //StreamReader reader = new StreamReader(resStream, System.Text.Encoding.UTF8);
            var retString = reader.ReadToEnd();
            //int str = retString.IndexOf("{");
            //int end = retString.IndexOf("}");
            //retString = retString.Substring(retString.IndexOf("{"), retString.LastIndexOf("}")).Replace("\\", "");
            ////解析josn
            var jo = JsonConvert.DeserializeObject<GetkdaGrJbxxIReturnData>(retString);
            return jo;


        }
        /// <summary>
        /// 根据卡号性别匹配套餐 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CardTypeNameDto getSuitbyCardNum(CusCardDto input)
        {
            CardTypeNameDto cardTypeNameDto = new CardTypeNameDto();
              var que = _TbmCard.GetAll().FirstOrDefault(o => o.CardNo == input.CardNo);
            if (que != null)
            {
                if (que.Available!=1)
                {
                    cardTypeNameDto.Code = 0;
                    cardTypeNameDto.Err = "该卡已禁用！";
                    return cardTypeNameDto;
                }
                if (que.HasUse == 1)
                {
                    cardTypeNameDto.Code = 0;
                    cardTypeNameDto.Err = "该卡已使用！";
                    return cardTypeNameDto;
                }
                if (que.CardType?.CardType != "体检卡" &&
                    !que.CardCategory.Contains("单位"))
                {
                    cardTypeNameDto.Code = 0;
                    cardTypeNameDto.Err = "只能使用体检卡或单位卡！";
                    return cardTypeNameDto;
                }
                if (que.CardType?.Term==1 && 
                    que.EndTime.HasValue && que.EndTime.Value  <System.DateTime.Now)
                {
                    cardTypeNameDto.Code = 0;
                    cardTypeNameDto.Err = "体检卡已过期不能使用！";
                    return cardTypeNameDto;
                }
                if (que.CardType != null)
                {
                    var suit = que.CardType.ItemSuits.Where(r =>
                       (r.Sex == (int)Sex.GenderNotSpecified || r.Sex == input.Sex) &&
                       r.MaxAge >= input.Age && r.MinAge <= input.Age).ToList();
                    if (suit.Count == 0)
                    {
                        cardTypeNameDto.Code = 0;
                        cardTypeNameDto.Err = "没有匹配的套餐！";
                        return cardTypeNameDto;
                    }
                    cardTypeNameDto.ItemSuits = suit.MapTo<List<CardToSuitNameDto>>();
                }
                cardTypeNameDto.DiscountRate = que.DiscountRate;
                cardTypeNameDto.CardName = que.CardType?.CardName;
                cardTypeNameDto.SellName = que.SellCardUser.Name;
                cardTypeNameDto.ClientRegId = que.ClientRegId;
                cardTypeNameDto.CardCategory = que.CardCategory;
                cardTypeNameDto.ClientTeamInfoId = que.ClientTeamInfoId;
                cardTypeNameDto.Code = 1;
                cardTypeNameDto.FaceValue = que.FaceValue;
                cardTypeNameDto.Err = "";
                
                return cardTypeNameDto;
            }
            else
            {
                cardTypeNameDto.Code = 0;
                cardTypeNameDto.Err = "没有此卡信息！";
                return cardTypeNameDto;
            }
        }

        /// <summary>
        /// 体检人数统计用查询
        /// </summary>
        /// <param name="input"></param>
        public List<CustomerRegDto> QueryAllCount(QueryAllForNumberInput input)
        {
            var query = _customerRegRepository.GetAll();
            if (input.DateStart != null)
            {
                query = query.Where(o => o.LoginDate >= input.DateStart);
            }
            if (input.DateEnd != null)
            {
                query = query.Where(o => o.LoginDate <= input.DateEnd);
            }     
            if (input.DateStart != null)
            {
                query = query.Where(o => o.BookingDate >= input.DateStart);
            }
            if (input.DateEnd != null)
            {
                query = query.Where(o => o.BookingDate <= input.DateEnd);
            }
            var result= query.MapTo<List<CustomerRegDto>>();
            return result;
        }
        /// <summary>
        /// 体检人数环比统计图
        /// </summary>
        /// <param name="input"></param>
        public List<QueryAllForNumberDto> QueryAllForNumbers(QueryAllForNumberInput input)
        {          
            var query = _customerRegRepository.GetAll();
            //if (input.DateStart != null)
            //{
            //    input.DateStart = new DateTime(input.DateStart.Value.Year, input.DateStart.Value.Month, input.DateStart.Value.Day, 00, 00, 00);
            //    query = query.Where(q => q.LoginDate >= input.DateStart);
            //}
            //if (input.DateEnd != null)
            //{
            //    input.DateEnd = new DateTime(input.DateEnd.Value.Year, input.DateEnd.Value.Month, input.DateEnd.Value.Day, 23, 59, 59);
            //    query = query.Where(q => q.LoginDate <= input.DateEnd);
            //}
            if (input.DateStart != null)
            {
                input.DateStart = new DateTime(input.DateStart.Value.Year, input.DateStart.Value.Month, input.DateStart.Value.Day, 00, 00, 00);
                query = query.Where(q => q.BookingDate >= input.DateStart);
            }
            if (input.DateEnd != null)
            {
                input.DateEnd = new DateTime(input.DateEnd.Value.Year, input.DateEnd.Value.Month, input.DateEnd.Value.Day, 23, 59, 59);
                query = query.Where(q => q.BookingDate <= input.DateEnd);
            }
            if (input.SelectType == 1)
            {
                query = query.Where(o => o.ClientReg != null);
            }
            if(input.SelectType == 2)
            {
                query = query.Where(o => o.ClientReg == null);
            }
            List<CacheDtos> DQCacheList;
            if (input.WeekQuery) //周返回结果
            {
                DQCacheList = query.Select(r => new CacheDtos
                {
                    CreatTime = r.BookingDate.Value.Month.ToString() + "-" +
                                r.BookingDate.Value.Day.ToString(),
                    GroupOrPersonal = r.ClientReg == null ? "个人" : "单位"
                }).ToList();
            }
            else //月、周返回结果
            {
                DQCacheList = query.Select(r => new CacheDtos
                {
                    CreatTime = r.BookingDate.Value.Month.ToString() + "月",
                    GroupOrPersonal = r.ClientReg == null ? "个人" : "单位"
                }).ToList();
            }

            var DQqueryList = DQCacheList.GroupBy(r => new { r.CreatTime })
                .Select(r => new QueryAllForNumberDto
                {
                    Type = r.Key.CreatTime,
                    RenCount = r.Count(),
                    CurrentData =r.Where(o=>o.GroupOrPersonal=="个人").Count(),
                    CurrentDatas=r.Where(o => o.GroupOrPersonal == "单位").Count(),
                }).ToList();

            //数据合并显示结果
            var result = from a in DQqueryList                        
                         select new QueryAllForNumberDto
                         {
                             Type = a.Type,
                             RenCount=a.RenCount,
                             CurrentData = a.CurrentData,
                             CurrentDatas=a.CurrentDatas
                         };
            return result.OrderBy(r=>r.Type).ToList();
        
        }
        
        public StartupAlertCusRegDto GetStartupData(StartupAlertGetDto input)
        {
            var list = _customerRegRepository.GetAll();
            if (input.IDCardNo != null)
            {
                list = list.Where(o => o.Customer.IDCardNo == input.IDCardNo);
            }
            var returnData = list.MapTo< List<StartupAlertCusRegDto>>();
            if (returnData.Count > 0)
            {
                return returnData[0];
            }
            else
            {
                return new StartupAlertCusRegDto();

            }
        }
        public List<ChargeBM> NoPriceGroup(List<ChargeBM> input)
        {
            var idlsit = input.Select(o=>o.Id).ToList();
            var groupids = _TbmGroupRePriceSynchronizes.GetAll().Select(o => o.ItemGroupId).Distinct().ToList();
            var group = input.Where(o => !groupids.Contains(o.Id)).ToList();
            return group;

        }
        /// <summary>
        /// 医保价格不匹配组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ChargeBM> ErrPriceGroup(List<GroupPriceDto> input)
        {
            List<ChargeBM> chargeBMs = new List<ChargeBM>();

                   var groupIds = input.Select(o => o.Id).ToList();
            var groups = _itemGroupRepository.GetAll().Where(o => groupIds.Contains(o.Id) && o.Price != o.GroupRePriceSynchronizes.Sum(n => n.chkit_costn)).Select(
                o => new { o.ItemGroupName, o.Price, synPres = o.GroupRePriceSynchronizes.Sum(n => n.chkit_costn),o.Id }).ToList();
            if (groups.Count > 0)
            {
                chargeBMs = groups.Select(n => new ChargeBM { Id = n.Id, Name = (n.ItemGroupName + "(" + n.Price + ")" + "医保价格：" + n.synPres) }).ToList();
            }
            //var groupser = _itemGroupRepository.GetAll().Where(o => input.Any(n=>n.Id==o.Id && n.ItemPrice!=o.Price)).Select(
            //   o => new { o.ItemGroupName, o.Price, synPres = o.GroupRePriceSynchronizes.Sum(n => n.chkit_costn), o.Id }).ToList();
            var groupser= _itemGroupRepository.GetAll().Where(o=> groupIds.Contains(o.Id)).Select(
               o => new { o.ItemGroupName, o.Price, o.Id }).ToList();
            foreach (var grou in groupser)
            {
                var errgroup = input.FirstOrDefault(o => o.Id == grou.Id && o.ItemPrice != grou.Price);
                if (errgroup !=null)
                {
                  
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Id = grou.Id;
                    chargeBM.Name = (grou.ItemGroupName + "(" + errgroup.ItemPrice + ")" + "真实原价：" + grou.Price);
                    chargeBMs.Add(chargeBM);
                }
            }

            return chargeBMs;
        }

        public List<AbpTenantsDto> GetAbpTenants()
        {
            List<AbpTenantsDto> dtos = new List<AbpTenantsDto>();
            var s = "select Id, Name as Names from AbpTenants";
            SqlParameter[] parameters = {
                        new SqlParameter("@Id",new Guid()),
                        };
            var dto = _sqlExecutor.SqlQuery<AbpTenantsDto>
                           (s, parameters).ToList();

            return dto;
        }
        /// <summary>
        /// 获取村
        /// </summary>
        /// <returns></returns>
        public List<SimBasicDictionariesDto> GetCZ(AbpTenantsDto input)
        {
           
            var s = "select Value,Text,Remarks  from TbmBasicDictionaries where  Type='NucleicAcidtType' and IsDeleted=0   ";
            if (input.Id != 1)
            {
                s += "   and TenantId="+ input.Id + "";
            }
            SqlParameter[] parameters = {
                        new SqlParameter("@Id",new Guid()),
                        };
            var dto = _sqlExecutor.SqlQuery<SimBasicDictionariesDto>
                           (s, parameters).ToList();

            return dto;
        }

        public List<SickNessDto> GetDiseaseAppServices(SickNessDto input)
        {
            List<SickNessDto> dto = new List<SickNessDto>();
            var query = @"select AbpTenants.Name,ItemName,TbmBasicDictionaries.Text as NucleicAcidType,Count(case TjlCustomers.sex when 1 then 1 end)ManSex,
Count(case TjlCustomers.sex when 2 then 2 end)WoManSex,
cast(COUNT(TjlCustomers.Name) as nvarchar(500)) RCount,
sum(case ISNULL( Symbol,'') when '' then 0 when 'M' then 0 else 1 end )Symbol,
COUNT(CASE WHEN s1.SummarizeName = '高血压' then '高血压' end)GxyCount,
COUNT(CASE WHEN s2.SummarizeName = '糖尿病' then '糖尿病' end)TnbCount,
COUNT(CASE WHEN Age > 65 and s1.SummarizeName = '高血压' THEN '高血压' END)OldSummarize,
CAST(CONVERT(DECIMAL(18, 2), sum(case ISNULL( Symbol,'') when '' then 0 when 'M' then 0 else 1 end ) / CAST(ISNULL(NULLIF(COUNT(ProcessState), 0), 1) AS FLOAT) * 100)AS VARCHAR(10)) + '%' AS SignPer,
CAST(CONVERT(DECIMAL(18, 2), COUNT(CASE WHEN s1.SummarizeName = '高血压' then '高血压' END) / CAST(ISNULL(NULLIF(COUNT(TjlCustomers.Name), 0), 1) AS FLOAT) * 100)AS VARCHAR(10))+'%' AS JclGxyCount,
CAST(CONVERT(DECIMAL(18, 2), COUNT(CASE WHEN  s2.SummarizeName = '糖尿病' then '糖尿病' END) / CAST(ISNULL(NULLIF(COUNT(TjlCustomers.Name), 0), 1) AS FLOAT) * 100)AS VARCHAR(10))+'%' AS JclTnbCount, 
CAST(CONVERT(DECIMAL(18, 2), COUNT(CASE WHEN Age > 65 and s1.SummarizeName = '高血压' THEN '高血压' END) / CAST(ISNULL(NULLIF(COUNT(TjlCustomers.Name), 0), 1) AS FLOAT) * 100)AS VARCHAR(10))+'%' AS OldGxyCount 
 from TjlCustomerRegItems inner join TjlCustomerRegs on TjlCustomerRegItems.CustomerRegId = TjlCustomerRegs.Id 
inner join TjlCustomers on CustomerId = TjlCustomers.Id INNER JOIN AbpTenants ON TjlCustomers.TenantId = AbpTenants.Id 
left join TjlCustomerSummarizeBMs s1 on s1.CustomerRegID = TjlCustomerRegs.Id   and s1.SummarizeName='高血压'
left join TjlCustomerSummarizeBMs s2 on s2.CustomerRegID = TjlCustomerRegs.Id   and s2.SummarizeName='糖尿病'" +
                "left join TjlClientTeamInfoes on TjlClientTeamInfoes.Id = TjlCustomerRegs.ClientTeamInfoId " +
                "left join TbmBasicDictionaries on TbmBasicDictionaries.Value=NucleicAcidType and TbmBasicDictionaries.Type='NucleicAcidtType' " +
                "where TjlCustomerRegs.CheckSate != 1 and TjlCustomerRegs.RegisterState !=1  and TjlCustomerRegs.SummSate>=3 ";
            if (input == null)
            {
                return dto;
            }
            if (input.SatrtDateTime != null && input.EndDateTime != null)
            {
                query += " and TjlCustomerRegs.LoginDate BETWEEN '" + input.SatrtDateTime.Value.ToString("yyyy-MM-dd")
                    + " 00:00:00' and  '" + input.EndDateTime.Value.ToString("yyyy-MM-dd") + " 23:59:59'";
            }
            if (input.Name != "" && input.Name!="1")
            {
                query += " and AbpTenants.Id = " + input.Name + "";
            }
            if (input.ItemName != "")
            {
                query += " and TjlCustomerRegItems.ItemName like '%" + input.ItemName + "%'";
            }
            if (!string.IsNullOrWhiteSpace( input.NucleicAcidType) && input.NucleicAcidType!="null")
            {
                query += " and TjlCustomerRegs.NucleicAcidType like '%" + input.NucleicAcidType + "%'";
            }
            query += " GROUP BY AbpTenants.Name,TjlCustomerRegItems.ItemName,TbmBasicDictionaries.Text";
            SqlParameter[] parameters = {
                        new SqlParameter("@Id",new Guid()),
                        };
            var dt = _sqlExecutor.SqlQuery<SickNessDto>
                           (query, parameters).ToList();
            return dt;

        }
        /// <summary>
        /// 获取单位名单核收名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  List<ReportHSCusDto> getClientCusHS(EntityDto<Guid> input)
        {
            var que = _customerRegRepository.GetAll().Where(o=>o.ClientRegId==input.Id);
            var cuslist = que.Select(o => new ReportHSCusDto
            {
                Age = o.Customer.Age,
                CheckSate = o.CheckSate,
                Department = o.Customer.Department,
                MarriageStatus = o.Customer.MarriageStatus==null?90: o.Customer.MarriageStatus,
                Name = o.Customer.Name,
                Sex = o.Customer.Sex,
                WorkNumber = o.Customer.WorkNumber,
                BookingDate=o.BookingDate,
                 CustomerBM=o.CustomerBM,
                 ClientCusBM=o.ClientRegNum,
                  HasCheck=o.CheckSate!=1? "●" : "",
                   NoCheck= o.CheckSate == 1 ? "▲" : ""
            }).OrderBy(o=>o.ClientCusBM).ThenBy(o=>o.CustomerBM).ToList();

            return cuslist;
        }
        /// <summary>
        /// 获取导入名单数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ImportDataDto> GetImportDatas(SearchImportData input)
        {
            var que = _customerRegRepository.GetAll();
            //if (input.Name != null)
            //{
            //    que = que.Where(o => o.Customer.Name == input.Name);
            //}
            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                que = que.Where(o => o.CustomerBM == input.CustomerBM || o.Customer.Name == input.Name);
            }
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(o => o.ClientRegId == input.ClientRegId);
            }
            if (input.StartDateTime.HasValue)
            {
                if (input.TimeType == 2)
                {
                    que = que.Where(o => o.CustomerRegItems.Any(p=>p.StandFrom==3 && p.CustomerItemGroupBM.FirstDateTime >= input.StartDateTime));
                }
                else
                {
                    que = que.Where(o => o.LoginDate >= input.StartDateTime);
                }
            }
            if (input.EndDateTime.HasValue)
            {
                if (input.TimeType == 2)
                {
                    que = que.Where(o => o.CustomerRegItems.Any(p => p.StandFrom == 3 && p.CustomerItemGroupBM.FirstDateTime < input.EndDateTime));
                }
                else
                {
                    que = que.Where(o => o.LoginDate < input.EndDateTime);
                }
            }
            if (input.SuitId.HasValue)
            {
                que = que.Where(o => o.ItemSuitBMId ==input.SuitId );
            }
            if (input.CheckSate.HasValue)
            {
                que = que.Where(o => o.CheckSate == input.CheckSate);
            }
            if (input.SummSate.HasValue)
            {
                que = que.Where(o => o.SummSate == input.SummSate);
            }
            if (input.DepartType.HasValue)
            {
                que = que.Where(o => o.NucleicAcidType == input.DepartType);
            }
            var cuslist = que.Select(o => new ImportDataDto
            {
                Age = o.Customer.Age,
                Name = o.Customer.Name,
                Sex = o.Customer.Sex,
                CustomerBM = o.CustomerBM,
                ClientName=o.ClientInfo.ClientName,
                LoginDate=o.LoginDate,
                ImportData=o.CustomerRegItems.Where(p=>p.StandFrom==3).Select(s=>s.CustomerItemGroupBM.FirstDateTime).FirstOrDefault(),
                Operator=o.CustomerItemGroup.Select(i=>i.BillingEmployeeBM.UserName).FirstOrDefault(),
                 Id=o.Id,
                  CheckSate=o.CheckSate,
                   DepartType=o.NucleicAcidType,
                    SuitId=o.ItemSuitBMId,
                     SummSate=o.SummSate
            }).ToList();
            return cuslist;
        }
        public List<BdSatticDto> GetBdCz()
        {
            var query = _tbmBasicDictionary.GetAll().Where(o => o.Type == "NucleicAcidtType");
            var re = query.ToList();
            return re.MapTo<List<BdSatticDto>>();
        }
        /// <summary>
        /// 获取体检人信息体检人上次体检日期
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOldRegDto QueryOldCustomer(SearchOldRegDto input)
        {
            if (string.IsNullOrEmpty(input.ArchivesNum))
            {
                var query = _customerRegRepository.GetAll().Where(p => p.Customer.ArchivesNum == input.ArchivesNum
                && p.CustomerBM != input.CustomerBM).OrderByDescending(p => p.LoginDate)?.First();
                return query.MapTo<OutOldRegDto>();
            }
            else if (string.IsNullOrEmpty(input.IDCardNo))
            {
                var query = _customerRegRepository.GetAll().Where(p => p.Customer.IDCardNo == input.IDCardNo
               && p.CustomerBM != input.CustomerBM).OrderByDescending(p => p.LoginDate)?.First();
                return query.MapTo<OutOldRegDto>();
            }
            else
            {
                return null;
            }
            

          
        }
   /// <summary>
        /// 获取体检档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public QueryCustomerDto GetCusInfoByIDCard(SearchOldRegDto input)
        {
            var que = _customerRepository.GetAll().FirstOrDefault(p => p.IDCardNo == input.IDCardNo);
            return ObjectMapper.Map<QueryCustomerDto>(que);

        }
        /// <summary>
        /// 修改上传状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void UpCusUploadState(EntityDto<Guid> input)
        {
            var que = _customerRegRepository.Get(input.Id);
            que.UploadState = 2;
            _customerRegRepository.Update(que);

        }
       /// <summary>
       /// 商务进展
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        public List<BusiCusRegDto> SearchBusiCus(InSearchBusiCusDto input)
        {
            var que = _customerRegRepository.GetAll();
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(p=>p.ClientRegId== input.ClientRegId);
            }
            if (input.StarDate.HasValue)
            {
                que = que.Where(p => p.LoginDate >= input.StarDate);
            }
            if (input.EndDate.HasValue)
            {
                que = que.Where(p => p.LoginDate < input.EndDate);
            }
            if ( input.UserId.HasValue)
            {
                que = que.Where(p=> p.ClientReg.UserId== input.UserId);
            }
            var result = que.Select(p=>new BusiCusRegDto {
                 Age=p.Customer.Age,
                  BookingDate=p.BookingDate,
                   CheckSate=p.CheckSate,
                    ClientName=p.ClientInfo.ClientName,
                     ClientRegBM=p.ClientReg.ClientRegBM,
                      CustomerBM=p.CustomerBM,
                       LoginDate=p.LoginDate,
                        Name=p.Customer.Name,
                         PrintSate=p.PrintSate,
                          RegisterState=p.RegisterState,
                           Sex=p.Customer.Sex,
                            SummSate=p.SummSate,
                             linkMan=p.ClientReg.user.Name

            }).OrderBy(p=>p.LoginDate).ToList();
            return result;
        }

        /// <summary>
        /// 商务订单团检
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<BusiRSDto> SearchBusiCount(InSearchBusiCusDto input)
        {
            var que = _customerRegRepository.GetAll().Where(p=>p.ClientRegId!=null);
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(p => p.ClientRegId == input.ClientRegId);
            }
            if (input.StarDate.HasValue)
            {
                que = que.Where(p => p.LoginDate >= input.StarDate);
            }
            if (input.EndDate.HasValue)
            {
                que = que.Where(p => p.LoginDate < input.EndDate);
            }
            if (input.UserId.HasValue)
            {
                que = que.Where(p => p.ClientReg.UserId == input.UserId);
            }
            var result = que.GroupBy(p => new { p.ClientRegId, p.ClientTeamInfoId }).Select(p => new BusiRSDto
            {

                ClientName = p.FirstOrDefault().ClientInfo.ClientName,
                ClientRegBM = p.FirstOrDefault().ClientReg.ClientRegBM,

                linkMan = p.FirstOrDefault().ClientReg.user.Name,
                BusiMoney = p.FirstOrDefault().ClientTeamInfo.SWjg,
                TeamName = p.FirstOrDefault().ClientTeamInfo.TeamName,
                RegCount = p.Count(),
                 teamMoney=p.Count()* p.FirstOrDefault().ClientTeamInfo.SWjg


            }).OrderBy(p => p.ClientName).ToList();
            return result;
        }

        /// <summary>
        /// 商务订单个人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<BusiCusGRDtocs> GRBusiCount(InSearchBusiCusDto input)
        {
            var que = _customerRegRepository.GetAll().Where(p => p.ClientRegId == null);
           
            if (input.StarDate.HasValue)
            {
                que = que.Where(p => p.LoginDate >= input.StarDate);
            }
            if (input.EndDate.HasValue)
            {
                que = que.Where(p => p.LoginDate < input.EndDate);
            }
            if (input.UserId.HasValue)
            {
                que = que.Where(p => p.ClientReg.UserId == input.UserId);
            }
            //var pay = que.SelectMany(p => p.MReceiptInfo);
            var result = que.Select(p => new BusiCusGRDtocs
            {
                ClientRegBM = p.OrderNum,
                 Age=p.Customer.Age,
                  CustomerBM=p.CustomerBM,
                   Introducer=p.Introducer,
                    MayMoney=p.McusPayMoney.PersonalMoney,
                     Name=p.Customer.Name,
                      Sex=p.Customer.Sex,
                       LoginDate=p.LoginDate,
                        hasMoney = p.McusPayMoney.PersonalPayMoney,
                         payment=p.MReceiptInfo.SelectMany(n=>n.MPayment).Select(o=>o.MChargeTypename + ":" +o.Actualmoney).ToList()



            }).OrderBy(p => p.LoginDate).ToList();
            return result;
        }
        /// <summary>
        /// 保存补检预约 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public QueryCustomerRegDto  SaveSupplementary(EntityDto<Guid> input)
        {
         
            var cusreg = _customerRegRepository.GetAll().FirstOrDefault(p => p.SupplementaryRegID == input.Id);
            #region 个人预约
            var currReg = _customerRegRepository.Get(input.Id);
            QueryCustomerRegDto data = new QueryCustomerRegDto();           
            if (cusreg != null && cusreg.RegisterState == (int)RegisterState.No)
            {
                data = cusreg.MapTo<QueryCustomerRegDto>();               
                data.Remarks = "补检";
                data.CustomerItemGroup = null;
                data.CustomerItemGroup = new List<CusReg.Dto.TjlCustomerItemGroupDto>();
            }
            else
            {

                //检索是否有预约
                //本次体检
                data.ClientInfoId = currReg.ClientInfoId;
                data.ClientRegId = currReg.ClientRegId;
                data.ClientTeamInfoId = currReg.ClientTeamInfoId;
                data.SupplementaryRegID = input.Id;                
                data.BarState = 1;
                data.BlindSate = 1;
                data.BookingDate = currReg.TjlCusGiveUps.FirstOrDefault().stayDate;
                data.Remarks = "补检";
                data.CheckSate = 1;
                data.ClientType = "";//体检类别
                data.CostState = 1;
                data.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                data.CustomerType = currReg.CustomerType;
                data.EmailReport = currReg.EmailReport;
                data.ExamPlace = currReg.ExamPlace;
                data.FamilyState = currReg.FamilyState;
                data.GuidanceSate = 1;
                data.HaveBreakfast = 1;
                data.InfoSource = currReg.InfoSource;

                data.IsFree = false;
                data.MailingReport = currReg.MailingReport;
                data.MarriageStatus = currReg.MarriageStatus;
                data.Message = 2;
                data.PhysicalType = currReg.PhysicalType;
                data.PrintSate = 1;
                data.ReceiveSate = 1;
                data.RegAge = currReg.RegAge;
                data.RegisterState = 1;
                data.ReplaceSate = 1;
                data.ReportBySelf = 1;
                data.RequestState = 1;
                data.SendToConfirm = 1;
                data.SummLocked = 2;
                data.SummSate = 1;
                data.UrgentState = 1;
                data.OrderNum = "";
                //data. 生成查询码
                data.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();

                //体检人基本信息
                data.Customer = currReg.Customer.MapTo<QueryCustomerDto>();


                data.CustomerItemGroup = new List<CusReg.Dto.TjlCustomerItemGroupDto>();
            }
            var Staygroup = currReg.CustomerItemGroup.Where(p=>p.CheckState== (int)ProjectIState.Stay).ToList();
            //组合信息
            foreach (var cusGroup in Staygroup)
            {

                CusReg.Dto.TjlCustomerItemGroupDto tjlCustomerItemGroup = new CusReg.Dto.TjlCustomerItemGroupDto();
                tjlCustomerItemGroup.BarState = (int)PrintSate.NotToPrint;
                tjlCustomerItemGroup.CheckState = (int)ProjectIState.Not;           

                tjlCustomerItemGroup.DepartmentId = cusGroup.DepartmentId;               
                tjlCustomerItemGroup.DepartmentName = cusGroup.DepartmentName;
                tjlCustomerItemGroup.DepartmentOrder = cusGroup.DepartmentOrder;
                tjlCustomerItemGroup.DiscountRate = 1;
                tjlCustomerItemGroup.DrawSate = 1;
                tjlCustomerItemGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
                tjlCustomerItemGroup.GRmoney = tjlCustomerItemGroup.GRmoney;
                tjlCustomerItemGroup.GuidanceSate = (int)PrintSate.NotToPrint;
                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目

                tjlCustomerItemGroup.ItemGroupBM_Id = cusGroup.ItemGroupBM_Id.Value;
                tjlCustomerItemGroup.ItemGroupName = cusGroup.ItemGroupName;
                tjlCustomerItemGroup.ItemGroupOrder = cusGroup.ItemGroupOrder;
                tjlCustomerItemGroup.ItemGroupCodeBM = cusGroup.ItemGroupCodeBM;
                tjlCustomerItemGroup.ItemPrice = cusGroup.ItemPrice ;

                tjlCustomerItemGroup.ItemSuitName = data.ItemSuitName;
                tjlCustomerItemGroup.PayerCat = cusGroup.PayerCat;
                tjlCustomerItemGroup.PriceAfterDis = cusGroup.PriceAfterDis;
                tjlCustomerItemGroup.RefundState = (int)PayerCatType.NotRefund;
                tjlCustomerItemGroup.RequestState = (int)PrintSate.NotToPrint;
                tjlCustomerItemGroup.SFType = cusGroup.SFType;
                tjlCustomerItemGroup.SummBackSate = (int)SummSate.NotAlwaysCheck;
                tjlCustomerItemGroup.SuspendState = 1;
                tjlCustomerItemGroup.TTmoney = 0;
                data.CustomerItemGroup.Add(tjlCustomerItemGroup);
                //修改原补检状态

                cusGroup.CheckState = (int)ProjectIState.GiveUp;
                _customerItemGroupRepository.Update(cusGroup);
            }

            QueryCustomerRegDto curCustomRegInfo = RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
            return curCustomRegInfo;
            #endregion

        }
        /// <summary>
        /// 获取问诊扫描信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccQueCusDto> GetOccQueCus(SearchOccQueCus input)
        {
            var que = _customerRegRepository.GetAll();
            if (input.RegId.HasValue && input.RegId != Guid.Empty)
            {
                que = que.Where(p => p.Id == input.RegId);
            }
            else
            {
                if (!string.IsNullOrEmpty(input.CusRegBM))
                {
                    que = que.Where(p => p.CustomerBM == input.CusRegBM);
                }
                if (input.StarDate.HasValue)
                {
                    que = que.Where(p => p.LoginDate >= input.StarDate);
                }
                if (input.EndDate.HasValue)
                {
                    que = que.Where(p => p.LoginDate < input.EndDate);
                }
                if (input.OccQuesSate.HasValue )
                {
                    if (input.OccQuesSate == 1)
                    {
                        que = que.Where(p => p.OccQuesSate == input.OccQuesSate);
                    }
                    else
                    {
                        que = que.Where(p => p.OccQuesSate !=1);

                    }
                }
            }
            //只显示职业体检
            var biselist = _tbmBasicDictionary.GetAll().Where(p => p.Type == "ExaminationType" 
            && p.Text.Contains("职业")).Select(p=>p.Value).ToList();

            que = que.Where(p=> p.PhysicalType!=null && biselist.Contains( p.PhysicalType.Value));
          return  que.OrderBy(p=>p.LoginDate).MapTo<List<OccQueCusDto>>();
        }
        /// <summary>
        /// 保存职业史扫描
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  void  SaveOccQueCus(OccQueCusDto input)
        {
            var que = _customerRegRepository.Get(input.Id);

            que.OccQuesSate = 1;
            que.OccQuesDate = System.DateTime.Now;
            que.OccQuesPhotoId = input.OccQuesPhotoId;
            que.OccQuesUserId = input.OccQuesUserId;
            _customerRegRepository.Update(que);
        }

        /// <summary>
        /// 清楚职业史扫描
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void ClearOccQueCus(EntityDto<Guid> input)
        {
            var que = _customerRegRepository.Get(input.Id);

            que.OccQuesSate = 0;
            que.OccQuesDate =null;
            que.OccQuesPhotoId = null;
            que.OccQuesUserId = null;
            _customerRegRepository.Update(que);
        }
    }

}