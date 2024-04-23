using Abp.Authorization;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Common;

using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Sw.Hospital.HealthExaminationSystem.UserException.Others;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Abp.Domain.Uow;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using AutoMapper;
using Abp.Application.Services.Dto;
using Z.EntityFramework.Plus;
using HealthExaminationSystem.Enumerations.Helpers;
using System.IO;
using Sw.Hospital.HealthExaminationSystem.Application.Picture;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using System.Drawing;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat
{
    [AbpAuthorize]
    public class WeChatAppService : MyProjectAppServiceBase, IWeChatAppService
    {
        #region 接口和引用
        private readonly ICommonAppService common ;
        private readonly IRepository<TjlCustomer, Guid> _jlCustomer; //用户表

        private readonly IRepository<TjlCustomerReg, Guid> _jlCustomerReg; //预约表
        private readonly IRepository<TbmBasicDictionary, Guid> _jTbmBasicDictionary; //字典
        private readonly IRepository<TjlCustomerSummarize, Guid> _jlCustomerSummarize;//总检建议
        private readonly IRepository<TjlCustomerDepSummary, Guid> _jlCustomerDepSummary; //
        private readonly IRepository<TjlCustomerItemPic, Guid> _jlPicture; //图片

        private readonly IRepository<TjlCustomerItemGroup, Guid> _jlCustomerItemGroup; //预约组合表
        private readonly IRepository<TjlClientInfo, Guid> _tjlClientInfo; //单位表

        private readonly IRepository<TjlClientTeamInfo, Guid> _jlClientTeamInfo; //单位分组表

        private readonly IRepository<TjlClientReg, Guid> _jlClientReg; //单位预约信息

        private readonly IRepository<TjlCustomerRegItem, Guid> _jlCustomerRegItem; //项目预约表

        private readonly IRepository<TbmItemGroup, Guid> _bmItemGroup; //项目组合编码表

        private readonly IRepository<TjlMcusPayMoney, Guid> _jlMcusPayMoney; //个人应收已收表

        private readonly IRepository<TbmItemSuit, Guid> _bmItemSuit; //套餐编码表
        private readonly IRepository<User, long> _jlUsrs;
        private readonly IRepository<TjlClientTeamRegitem, Guid> _Regitem;//单位分组登记项目
        private readonly IRepository<TbmDepartment, Guid> _bmDepartment;//科室编码表
        private readonly IRepository<TjlCustomerAddPackage, Guid> _jlCusAddpackages;//加项包记录
        private readonly IRepository<TjlCustomerAddPackageItem, Guid> _jlCusAddPackItems;//加项包管理项目记录
        private readonly IRepository<TjlCustomerQuestion, Guid> _jlCusQuestion;//问卷记录

        private readonly ICustomerAppService _customerAppService;

        private readonly IIDNumberAppService _idNumberAppService;

        private readonly IChargeAppService _ChargeAppService;
        private IItemSuitAppService _itemSuitAppSvr;
        //private readonly ICommonAppService _commonAppService;

        private PictureAppService _pictureController;

        private readonly IRepository<TbmOneAddXQuestionnaire, Guid> _OneAddXQuestionnaire;
        private readonly IRepository<TbmItemSuitItemGroupContrast, Guid> _itemSuitItemGroupRepository;
        private readonly IRepository<TbmItemInfo, Guid> _tbmItemInfo;//项目编码表
        private readonly IRepository<TbmSummarizeAdvice, Guid> _tbmSummarizeAdvice;//建议编码表

        private readonly IRepository<TbmMChargeType, Guid> _tbmMChargeType;//支付方式
        private readonly IRepository<TjlCusReview, Guid> _tjlCusReview;//支付方式       

        private readonly IRepository<TjlCustomerSummarizeBM, Guid> _CustomerSummarizeBM;//总检建议表    
        private readonly IRepository<TjlCustomerItemPic, Guid> _CustomerItemPic;//总检建议表   

        private readonly IRepository<TbmGroupRePriceSynchronizes, Guid> _GroupRePriceSynchronizes;//组合关联医嘱项目 
        private readonly IRepository<TbmPriceSynchronize, Guid> _TbmPriceSynchronize;//医嘱项目 
        private readonly IRepository<TjlMReceiptInfo, Guid> _TjlMReceiptInfo;//收费表 
        private readonly IRepository<TjlCusQuestion, Guid> _TjlCusQuestion;//问卷 
        private readonly IRepository<TjlQuestionBom, Guid> _TjlQuestionBom;//答卷题目详情 
        private readonly IRepository<TjlQuestiontem, Guid> _TjlQuestiontem;//选项题备选项 
        private readonly IRepository<TjlCusGiveUp, Guid> _TjlCusGiveUp;//补检 

     

        public WeChatAppService(
            IRepository<TjlCustomer, Guid> jlCustomer,
            IRepository<TjlCustomerReg, Guid> jlCustomerReg,
            IRepository<TjlCustomerItemGroup, Guid> jlCustomerItemGroup,
            IRepository<TjlClientTeamInfo, Guid> jlClientTeamInfo,
            IRepository<TjlClientReg, Guid> jlClientReg,
            IRepository<TbmItemGroup, Guid> bmItemGroup,
            IRepository<TjlCustomerRegItem, Guid> jlCustomerRegItem,
            IRepository<TjlMcusPayMoney, Guid> jlMcusPayMoney,
            IRepository<TbmItemSuit, Guid> bmItemSuit,
            IRepository<User, long> jlUsrs,
            IRepository<TjlClientTeamRegitem, Guid> Regitem,
            IRepository<TbmDepartment, Guid> bmDepartment,
            IRepository<TjlCustomerAddPackage, Guid> jlCusAddpackages,
            IRepository<TjlCustomerAddPackageItem, Guid> jlCusAddPackItems,
            IRepository<TjlCustomerQuestion, Guid> jlCusQuestion,
            IRepository<TbmOneAddXQuestionnaire, Guid> OneAddXQuestionnaire,
            ICustomerAppService customerAppService,
            IIDNumberAppService idNumberAppService,
            ICommonAppService commonAppService,
            IRepository<TbmItemSuitItemGroupContrast, Guid> itemSuitItemGroupRepository,
            IRepository<TjlCustomerDepSummary, Guid> jlCustomerDepSummary,
            IRepository<TjlCustomerSummarize, Guid> jlCustomerSummarize,
            IRepository<TbmBasicDictionary, Guid> jTbmBasicDictionary,
            IRepository<TbmItemInfo, Guid> tbmItemInfo,
            IRepository<TbmSummarizeAdvice, Guid> tbmSummarizeAdvice,
            IRepository<TbmMChargeType, Guid> tbmMChargeType,
            IChargeAppService ChargeAppService,
            IRepository<TjlCusReview, Guid> tjlCusReview,
             IRepository<TjlClientInfo, Guid> tjlClientInfo,
             IRepository<TjlCustomerSummarizeBM, Guid> CustomerSummarizeBM,
             IRepository<TjlCustomerItemPic, Guid> CustomerItemPic,
             IRepository<TbmGroupRePriceSynchronizes, Guid> GroupRePriceSynchronizes,
             IRepository<TbmPriceSynchronize, Guid> TbmPriceSynchronize,
             IRepository<TjlMReceiptInfo, Guid> TjlMReceiptInfo,
             IRepository<TjlCusQuestion, Guid> TjlCusQuestion,
             IRepository<TjlQuestionBom, Guid> TjlQuestionBom,
             IRepository<TjlQuestiontem, Guid> TjlQuestiontem,
              IRepository<TjlCusGiveUp, Guid> TjlCusGiveUp,
              ICommonAppService _common,
              PictureAppService pictureController,
              IItemSuitAppService itemSuitAppSvr
        )
        {
            _itemSuitAppSvr = itemSuitAppSvr;
               _ChargeAppService = ChargeAppService;
            _jlCustomer = jlCustomer;
            _jlCustomerReg = jlCustomerReg;
            _jlCustomerItemGroup = jlCustomerItemGroup;
            _jlClientTeamInfo = jlClientTeamInfo;
            _jlClientReg = jlClientReg;
            _jlCustomerRegItem = jlCustomerRegItem;
            _bmItemGroup = bmItemGroup;
            _jlMcusPayMoney = jlMcusPayMoney;
            _bmItemSuit = bmItemSuit;
            _jlUsrs = jlUsrs;
            _Regitem = Regitem;
            _bmDepartment = bmDepartment;
            _jlCusAddpackages = jlCusAddpackages;
            _jlCusAddPackItems = jlCusAddPackItems;
            _jlCusQuestion = jlCusQuestion;
            _customerAppService = customerAppService;
            _idNumberAppService = idNumberAppService;
           // _commonAppService = commonAppService;
            _OneAddXQuestionnaire = OneAddXQuestionnaire;
            _itemSuitItemGroupRepository = itemSuitItemGroupRepository;
            _jlCustomerDepSummary = jlCustomerDepSummary;
            _jlCustomerSummarize = jlCustomerSummarize;
            _jTbmBasicDictionary = jTbmBasicDictionary;
            _tbmItemInfo = tbmItemInfo;
            _tbmSummarizeAdvice = tbmSummarizeAdvice;
            _tbmMChargeType = tbmMChargeType;
            _tjlCusReview = tjlCusReview;
            _tjlClientInfo = tjlClientInfo;
            _CustomerSummarizeBM = CustomerSummarizeBM;
            _CustomerItemPic = CustomerItemPic;
            _GroupRePriceSynchronizes = GroupRePriceSynchronizes;
            _TbmPriceSynchronize = TbmPriceSynchronize;
            _TjlMReceiptInfo = TjlMReceiptInfo;
            _TjlCusQuestion = TjlCusQuestion;
            _TjlQuestionBom = TjlQuestionBom;
            _TjlQuestiontem = TjlQuestiontem;
            _TjlCusGiveUp = TjlCusGiveUp;
            common = _common;
            _pictureController = pictureController;

        }
        #endregion

        /// <summary>
        /// 预约接口提交订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutResult SubmitOrder(InCusInfoDto input)
        {
            try
            {
                //ICustomerAppService customerSvr = new CustomerAppService();
                OutResult outResult = new OutResult();
                Guid customerregid = Guid.Empty;
                Guid clientregID = Guid.Empty;
                if (input.IsTT == 0)
                {
                    #region 个人预约
                    QueryCustomerRegDto data = new QueryCustomerRegDto();

                    data.BarState = 1;
                    data.BlindSate = 1;
                    data.BookingDate = input.BookingDate;
                    data.CheckSate = 1;
                    data.ClientType = "";//体检类别
                    data.CostState = 1;
                    data.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                    data.CustomerType = 1;
                    data.EmailReport = 2;
                    data.ExamPlace = 1;
                    data.FamilyState = 1;
                    data.GuidanceSate = 1;
                    data.HaveBreakfast = 1;
                    data.InfoSource = 2;
                    data.IsFree = false;
                    if (input.ItemSuitBMId.HasValue)
                    {
                        data.ItemSuitBMId = input.ItemSuitBMId;
                        var tbmItemSuit = _bmItemSuit.Get(input.ItemSuitBMId.Value);
                        data.ItemSuitName = tbmItemSuit.ItemSuitName;
                    }
                    User user = _jlUsrs.Get(AbpSession.UserId.Value);
                    data.KaidanYisheng = user.UserName;
                    data.MailingReport = 1;
                    data.MarriageStatus = input.MarriageStatus;
                    data.Message = 2;
                    data.PhysicalType = 1;
                    data.PrintSate = 1;
                    data.ReceiveSate = 1;
                    data.RegAge = input.Age;
                    data.RegisterState = 1;
                    data.ReplaceSate = 1;
                    data.ReportBySelf = 1;
                    data.RequestState = 1;
                    data.ReviewSate = 1;
                    data.SendToConfirm = 1;
                    data.SummLocked = 2;
                    data.SummSate = 1;
                    data.UrgentState = 1;

                    //data. 生成查询码
                    data.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();
                    //体检人基本信息
                    data.Customer = new QueryCustomerDto();
                    //身份证验证
                    var IDdata = VerificationHelper.GetByIdCard(input.IDCardNo);
                    if (IDdata != null)
                    {

                        data.Customer.Age = IDdata.Age;
                        data.Customer.Sex = (int)IDdata.Sex;
                        data.Customer.Birthday = IDdata.Birthday;
                        data.Customer.IDCardNo = input.IDCardNo;
                        //验证是否有该体检人档案
                        TjlCustomer tjlCustomer = _jlCustomer.FirstOrDefault(o => o.IDCardNo == input.IDCardNo);
                        if (tjlCustomer != null)
                        {
                            data.Customer.Id = tjlCustomer.Id;
                            data.Customer.ArchivesNum = tjlCustomer.ArchivesNum;

                        }
                        else
                        {
                            data.Customer.ArchivesNum = data.CustomerBM;
                        }
                    }
                    else
                    {
                        if (input.Birthday.HasValue)
                        {
                            data.Customer.Birthday = input.Birthday;
                            data.Customer.Age = input.Age;
                            var age = DateTime.Now.Year - input.Birthday.Value.Year;
                            if (input.Age != age)
                                data.Customer.Age = age;
                        }
                        else
                        {
                            data.Customer.Age = input.Age;
                        }
                        data.Customer.Sex = input.Sex;
                        data.Customer.ArchivesNum = data.CustomerBM;
                    }
                    //无档案号的情况赋值
                    data.Customer.AgeUnit = "岁";
                    data.Customer.CardNumber = "";
                    data.Customer.CustomerType = 1;
                    data.Customer.Duty = "";
                    data.Customer.Email = "";
                    data.Customer.GuoJi = "";
                    data.Customer.HospitalNum = "";
                    // data.Customer.IDCardNo = input.IDCardNo;
                    data.Customer.IDCardType = 1;//证件类型
                    data.Customer.MarriageStatus = input.MarriageStatus;
                    data.Customer.MedicalCard = "";
                    data.Customer.Mobile = input.Mobile;
                    data.Customer.Name = input.Name;
                    //生成简拼
                    var result = common.GetHansBrief(new ChineseDto { Hans = input.Name });
                    data.Customer.NameAB = result.Brief;//姓名简写;

                    data.Customer.Telephone = input.Telephone;
                    data.Customer.WbCode = "";//五笔
                    data.Customer.WorkNumber = "";
                    data.Customer.Qq = input.WeixinH;
                    data.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                    //组合信息
                    foreach (InCusGroupsDto cusGroup in input.CustomerItemGroup)
                    {

                        TjlCustomerItemGroupDto tjlCustomerItemGroup = new TjlCustomerItemGroupDto();
                        tjlCustomerItemGroup.BarState = (int)PrintSate.NotToPrint;
                        tjlCustomerItemGroup.CheckState = (int)ProjectIState.Not;

                        //传来的是bm  ItemGroupBM_Id
                        TbmItemGroup tbmItemGroup = _bmItemGroup.Get(cusGroup.ItemGroupBM_Id.Value);
                        if (tbmItemGroup == null)
                        {
                            outResult.ErrInfo = "未找到项目" + cusGroup.ItemGroupName + "的项目ID，请核实";
                            return outResult;
                        }
                        //if (tbmItemGroup.Price != cusGroup.ItemPrice)
                        //{
                        //    outResult.ErrInfo = "项目" + cusGroup.ItemGroupName + "金额：" + cusGroup.ItemPrice + ",和体检系统金额：" + tbmItemGroup.Price + "，金额不符请核实！";
                        //    return outResult;
                        //}
                        tjlCustomerItemGroup.DepartmentId = tbmItemGroup.DepartmentId;
                        TbmDepartment tbmDepartment = _bmDepartment.Get(tbmItemGroup.DepartmentId);
                        tjlCustomerItemGroup.DepartmentName = tbmDepartment.Name;
                        tjlCustomerItemGroup.DepartmentOrder = tbmDepartment.OrderNum;
                        tjlCustomerItemGroup.DiscountRate = cusGroup.DiscountRate;
                        tjlCustomerItemGroup.DrawSate = 1;
                        tjlCustomerItemGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
                        tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis;
                        tjlCustomerItemGroup.GuidanceSate = (int)PrintSate.NotToPrint;
                        if (cusGroup.ItemSuitId.HasValue)
                        {
                            tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目
                        }
                        else
                        {
                            tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Add;//是否加减项 正常项目
                        }
                        tjlCustomerItemGroup.ItemGroupBM_Id = tbmItemGroup.Id;
                        tjlCustomerItemGroup.ItemGroupName = tbmItemGroup.ItemGroupName;
                        tjlCustomerItemGroup.ItemGroupOrder = tbmItemGroup.OrderNum;
                        tjlCustomerItemGroup.ItemPrice = cusGroup.ItemPrice;
                        //if (cusGroup.ItemSuitId.HasValue)
                        //{
                        tjlCustomerItemGroup.ItemSuitId = cusGroup.ItemSuitId;
                        //}
                        tjlCustomerItemGroup.ItemSuitName = data.ItemSuitName;
                        tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                        tjlCustomerItemGroup.PriceAfterDis = cusGroup.PriceAfterDis;
                        tjlCustomerItemGroup.RefundState = (int)PayerCatType.NotRefund;
                        tjlCustomerItemGroup.RequestState = (int)PrintSate.NotToPrint;
                        tjlCustomerItemGroup.SFType = Convert.ToInt32(tbmItemGroup.ChartCode);
                        tjlCustomerItemGroup.SummBackSate = (int)SummSate.NotAlwaysCheck;
                        tjlCustomerItemGroup.SuspendState = 1;
                        tjlCustomerItemGroup.TTmoney = cusGroup.TTmoney;
                        data.CustomerItemGroup.Add(tjlCustomerItemGroup);
                    }
                    QueryCustomerRegDto curCustomRegInfo = _customerAppService.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
                    customerregid = curCustomRegInfo.Id;
                    outResult.CustomerBM = curCustomRegInfo.CustomerBM;
                    outResult.OrderId = input.OrderId;
                    #endregion
                }
                else
                {
                    #region 单位预约
                    if (string.IsNullOrEmpty(input.CustomerBM))
                    {
                        outResult.ErrInfo = "团体人员预约，体检号不能为空！";
                        return outResult;
                    }
                    TjlCustomerReg tjlCustomerReg = _jlCustomerReg.FirstOrDefault(o => o.CustomerBM == input.CustomerBM);
                    if (tjlCustomerReg == null)
                    {
                        outResult.ErrInfo = "体检系统中无此人信息，请核实！";
                        return outResult;
                    }
                    if (!tjlCustomerReg.ClientInfoId.HasValue || !tjlCustomerReg.ClientTeamInfoId.HasValue)
                    {
                        outResult.ErrInfo = "该体检人不属于任何单位，请核实！";
                        return outResult;
                    }
                    QueryCustomerRegDto data = new QueryCustomerRegDto();
                    data = tjlCustomerReg.MapTo<QueryCustomerRegDto>();
                    data.BookingDate = input.BookingDate;
                    if (input.ItemSuitBMId.HasValue)
                    {
                        data.ItemSuitBMId = input.ItemSuitBMId;
                        TbmItemSuit tbmItemSuit = _bmItemSuit.Get(input.ItemSuitBMId.Value);
                        data.ItemSuitName = tbmItemSuit.ItemSuitName;
                    }
                    User user = _jlUsrs.Get(AbpSession.UserId.Value);
                    data.KaidanYisheng = user.UserName;
                    data.MarriageStatus = input.MarriageStatus;
                    data.RegAge = input.Age;
                    //data.
                    data.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();
                    //体检人基本信息
                    data.Customer = new QueryCustomerDto();
                    //身份证验证
                    var IDdata = VerificationHelper.GetByIdCard(input.IDCardNo);
                    if (IDdata != null)
                    {
                        data.Customer.Age = IDdata.Age;
                        data.Customer.Sex = (int)IDdata.Sex;
                        data.Customer.Birthday = IDdata.Birthday;
                        //验证是否有该体检人档案                   
                    }
                    else
                    {
                        if (input.Birthday.HasValue)
                        {
                            data.Customer.Birthday = input.Birthday;
                            data.Customer.Age = input.Age;
                            var age = DateTime.Now.Year - input.Birthday.Value.Year;
                            if (input.Age != age)
                                data.Customer.Age = age;
                        }
                        else
                        {
                            data.Customer.Age = input.Age;
                        }
                        data.Customer.Sex = input.Sex;
                    }
                    data.Customer.IDCardNo = input.IDCardNo;
                    data.Customer.IDCardType = 1;//证件类型
                    data.Customer.MarriageStatus = input.MarriageStatus;
                    data.Customer.Mobile = input.Mobile;
                    data.Customer.Name = input.Name;
                    var result = common.GetHansBrief(new ChineseDto { Hans = input.Name });
                    data.Customer.NameAB = result.Brief;//姓名简写;
                    data.Customer.Telephone = input.Telephone;
                    // data.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                    TjlClientTeamInfo tjlClientTeamInfo = _jlClientTeamInfo.Get(data.ClientTeamInfo_Id.Value);
                    decimal oldMoney = 0;
                    decimal cjMoney = 0;
                    //判断 个人付款Gr  团队付款是
                    if (tjlClientTeamInfo.CostType == (int)PayerCatType.FixedAmount && tjlClientTeamInfo.TeamMoney.HasValue)
                    {
                        oldMoney = tjlClientTeamInfo.TeamMoney.Value;
                        decimal cusOldMoney = data.CustomerItemGroup.Sum(o => o.PriceAfterDis);
                        cjMoney = oldMoney - cusOldMoney;
                    }
                    //判断组合信息 如果没有传 只修改预约时间或者个人信息mmm
                    //组合信息
                    foreach (InCusGroupsDto cusGroup in input.CustomerItemGroup)
                    {
                        if (!data.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == cusGroup.ItemGroupBM_Id))
                        {
                            TjlCustomerItemGroupDto tjlCustomerItemGroup = new TjlCustomerItemGroupDto();
                            tjlCustomerItemGroup.BarState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.CheckState = (int)ProjectIState.Not;
                            TbmItemGroup tbmItemGroup = _bmItemGroup.Get(cusGroup.ItemGroupBM_Id.Value);
                            if (tbmItemGroup == null)
                            {
                                outResult.ErrInfo = "未找到项目" + cusGroup.ItemGroupName + "的项目ID，请核实";
                                return outResult;
                            }
                            tjlCustomerItemGroup.DepartmentId = tbmItemGroup.DepartmentId;
                            TbmDepartment tbmDepartment = _bmDepartment.Get(tbmItemGroup.DepartmentId);
                            tjlCustomerItemGroup.DepartmentName = tbmDepartment.Name;
                            tjlCustomerItemGroup.DepartmentOrder = tbmDepartment.OrderNum;
                            tjlCustomerItemGroup.DiscountRate = cusGroup.DiscountRate;
                            tjlCustomerItemGroup.DrawSate = 1;
                            tjlCustomerItemGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
                            tjlCustomerItemGroup.GuidanceSate = (int)PrintSate.NotToPrint;
                            if (cusGroup.ItemSuitId.HasValue)
                            {
                                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目
                            }
                            else
                            {
                                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Add;//是否加减项 正常项目
                            }
                            tjlCustomerItemGroup.ItemGroupBM_Id = tbmItemGroup.Id;
                            tjlCustomerItemGroup.ItemGroupName = tbmItemGroup.ItemGroupName;
                            tjlCustomerItemGroup.ItemGroupOrder = tbmItemGroup.OrderNum;
                            tjlCustomerItemGroup.ItemSuitId = cusGroup.ItemSuitId;
                            tjlCustomerItemGroup.ItemSuitName = data.ItemSuitName;

                            tjlCustomerItemGroup.RefundState = (int)PayerCatType.NotRefund;
                            tjlCustomerItemGroup.RequestState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.SFType = Convert.ToInt32(tbmItemGroup.ChartCode);
                            tjlCustomerItemGroup.SummBackSate = (int)SummSate.NotAlwaysCheck;
                            tjlCustomerItemGroup.SuspendState = 1;
                            tjlCustomerItemGroup.ItemPrice = cusGroup.ItemPrice;
                            tjlCustomerItemGroup.PriceAfterDis = cusGroup.PriceAfterDis;
                            //单位支付
                            if (tjlClientTeamInfo.JxType == (int)PayerCatType.ClientCharge)
                            {
                                tjlCustomerItemGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                tjlCustomerItemGroup.GRmoney = 0;
                                tjlCustomerItemGroup.TTmoney = cusGroup.PriceAfterDis;
                            }
                            //个人支付
                            else if (tjlClientTeamInfo.JxType == (int)PayerCatType.PersonalCharge)
                            {
                                tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                                tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis;
                                tjlCustomerItemGroup.TTmoney = 0;
                            }
                            //固定金额
                            if (tjlClientTeamInfo.CostType == (int)PayerCatType.FixedAmount)
                            {
                                if (cjMoney > 0)
                                {
                                    if (cusGroup.PriceAfterDis > cjMoney)
                                    {
                                        tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                                        tjlCustomerItemGroup.TTmoney = cjMoney;
                                        tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis - cjMoney;
                                        cjMoney = 0;
                                    }
                                    else
                                    {
                                        tjlCustomerItemGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                        tjlCustomerItemGroup.TTmoney = cusGroup.PriceAfterDis;
                                        tjlCustomerItemGroup.GRmoney = 0;
                                        cjMoney = cjMoney - cusGroup.PriceAfterDis;
                                    }
                                }
                                else
                                {
                                    tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                                    tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis;
                                    tjlCustomerItemGroup.TTmoney = 0;

                                }

                            }

                            data.CustomerItemGroup.Add(tjlCustomerItemGroup);
                        }
                    }
                    QueryCustomerRegDto curCustomRegInfo = _customerAppService.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
                    customerregid = curCustomRegInfo.Id;
                    clientregID = curCustomRegInfo.ClientRegId.Value;
                    outResult.CustomerBM = curCustomRegInfo.CustomerBM;
                    outResult.OrderId = input.OrderId;

                    #endregion
                }
                #region 保存问卷及加项包信息            
                if (input.Questions != null)
                {
                    foreach (InCusQusTionDto cusqus in input.Questions)
                    {
                        TjlCustomerQuestion tjlCusQuestion = new TjlCustomerQuestion();
                        tjlCusQuestion.Id = Guid.NewGuid();
                        tjlCusQuestion.OutQuestionID = cusqus.OutQuestionID;

                        tjlCusQuestion.CustomerRegId = customerregid;
                        TbmOneAddXQuestionnaire tbmOneAddXQuestionnaire = _OneAddXQuestionnaire.FirstOrDefault(o => o.ExternalCode == cusqus.OutQuestionID);
                        if (tbmOneAddXQuestionnaire == null)
                        {
                            outResult.ErrInfo = "问卷ID" + cusqus.OutQuestionID + "没有找到，请核实";
                            return outResult;
                        }
                        tjlCusQuestion.OneAddXQuestionnaireid = tbmOneAddXQuestionnaire.Id;
                        if (clientregID != Guid.Empty)
                        {
                            tjlCusQuestion.ClientRegId = clientregID;
                        }
                        tjlCusQuestion.QuestionName = tbmOneAddXQuestionnaire.Name;
                        tjlCusQuestion.QuestionType = tbmOneAddXQuestionnaire.Category;
                        _jlCusQuestion.Insert(tjlCusQuestion);
                    }
                }
                if (input.TjlCusAddpackages != null)
                {
                    foreach (InCusAddPacksDto cuspack in input.TjlCusAddpackages)
                    {
                        TjlCustomerAddPackage tjlCusAddpackages = new TjlCustomerAddPackage();
                        tjlCusAddpackages.CustomerRegId = customerregid;
                        if (clientregID != Guid.Empty)
                        {
                            tjlCusAddpackages.ClientRegId = clientregID;
                        }
                        tjlCusAddpackages.Id = Guid.NewGuid();
                        tjlCusAddpackages.ItemSuitID = cuspack.ItemSuitID;
                        TjlCustomerAddPackage tjlCusAddpackagesNew = _jlCusAddpackages.Insert(tjlCusAddpackages);
                        foreach (INCusAddPackItems cuspackItem in input.TjlCusAddPackItems)
                        {
                            if (cuspackItem.ItemSuitID == cuspack.ItemSuitID)
                            {
                                TjlCustomerAddPackageItem tjlCusAddPackItems = new TjlCustomerAddPackageItem();
                                tjlCusAddPackItems.CheckState = cuspackItem.CheckState;
                                if (clientregID != Guid.Empty)
                                {
                                    tjlCusAddPackItems.ClientRegId = clientregID;
                                }
                                tjlCusAddPackItems.CustomerRegId = customerregid;
                                tjlCusAddPackItems.Id = Guid.NewGuid();
                                tjlCusAddPackItems.ItemGroupID = cuspackItem.ItemGroupID;
                                tjlCusAddPackItems.ItemGroupName = cuspackItem.ItemGroupName;
                                tjlCusAddPackItems.ItemSuitID = cuspackItem.ItemSuitID;
                                tjlCusAddPackItems.TjlCusAddpackagesID = tjlCusAddpackagesNew.Id;
                                var groupmoney = input.CustomerItemGroup.FirstOrDefault(o => o.ItemGroupBM_Id == cuspackItem.ItemGroupID);
                                if (groupmoney != null)
                                {
                                    tjlCusAddPackItems.ItemPrice = groupmoney.ItemPrice;
                                    tjlCusAddPackItems.PriceAfterDis = groupmoney.PriceAfterDis;
                                    tjlCusAddPackItems.Suitgrouprate = groupmoney.DiscountRate;
                                }
                                else
                                {
                                    var suitgroup = _itemSuitItemGroupRepository.FirstOrDefault(o => o.ItemSuitId == cuspackItem.ItemSuitID && cuspackItem.ItemGroupID == cuspackItem.ItemGroupID);
                                    if (suitgroup != null)
                                    {
                                        tjlCusAddPackItems.ItemPrice = suitgroup.ItemPrice;
                                        tjlCusAddPackItems.PriceAfterDis = suitgroup.PriceAfterDis;
                                        tjlCusAddPackItems.Suitgrouprate = suitgroup.Suitgrouprate;

                                    }

                                }
                                _jlCusAddPackItems.Insert(tjlCusAddPackItems);
                            }
                        }
                    }
                }
                #endregion
                return outResult;
            }
            catch (EntityNotFoundException e)
            {
                throw new IdNotFoundExecption(e.Message, "预约异常！");
            }
        }
        /// <summary>
        /// 体检状态接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusStateDto> GetCusState(InCusBMDto input)
        {
            var result = new List<CusStateDto>();
            var cus = _jlCustomerReg.GetAll().Where(p=>p.OrderNum!=null && 
            p.OrderNum !="");
            if (!string.IsNullOrWhiteSpace(input.CustomerRegBM))
            {
                cus = cus.Where(o => o.CustomerBM == input.CustomerRegBM);
            }
            if (input.StartTime.HasValue )
            {
                cus = cus.Where(o => o.LastModificationTime!=null && 
                o.LastModificationTime >= input.StartTime );

            }
            if (input.EndTime.HasValue)
            {
                cus = cus.Where(o => o.LastModificationTime != null &&
                o.LastModificationTime <= input.EndTime);

            }
            var resultq = from b in cus
                          join a in _jlCustomerSummarize.GetAll() on b.Id equals a.CustomerRegID into ab
                          from subA in ab.DefaultIfEmpty()
                          select new CusStateDto
                          {
                              hospitalno = input.hospitalno,
                              isfinishcheck = b.SummSate == 4 ? 1 : 0,
                              isregiser = b.RegisterState == 1 ? "0" : "1",
                              orderId = b.OrderNum,
                              regisertime = b.LoginDate.HasValue? b.LoginDate:null,
                              finishchecktime = (b.SummSate==4 && subA.ConclusionDate.HasValue)? subA.ConclusionDate : null, // A表的字段1，使用条件运算符避免空引用异常  

                          };
            result = resultq.ToList();
            return result;
            

        }
        public List<RepCustomerRegDto> GetReport(InCusBMDto input)
        {
            var result = new List<RepCustomerRegDto>();
            var cus = _jlCustomerReg.GetAll();
            if (!string.IsNullOrWhiteSpace(input.CustomerRegBM))
            {
                cus = cus.Where(o => o.CustomerBM == input.CustomerRegBM);
            }
            if (input.StartTime.HasValue && input.EndTime.HasValue)
            {
                cus = cus.Where(o => o.CreationTime == input.StartTime && o.DeletionTime == input.EndTime);

            }

            result = cus.MapTo<List<RepCustomerRegDto>>();

            return result;
            //var cusinfo = _jlCustomerReg.GetAll().FirstOrDefault(o => o.CustomerBM == input.CustomerRegBM);
            //if(cusinfo.)
            //if (cusinfo.SummSate != 4)
            //{
            //    result.ReState = 0;
            //    result.StateMa = "未总检";
            //    return result;
            //}

            //else
            //{
            //    //已总检可以查报告  总检状态
            //    //基本信息
            //     result.cusInfo = cusinfo.Customer.MapTo<ReportCusDto>();
            //    //检查结果
            //     result.cusTem= _jlCustomerRegItem.GetAll().Where(o => o.CustomerRegId == cusinfo.Id).MapTo<List<ReportCusITemDto>>();
            //    //总检建议
            //     result.cusSummarize = _jlCustomerSummarize.GetAll().Where(o => o.CustomerRegID == cusinfo.Id).MapTo<CustomerSummarizeDto>();

            //    //图片
            //    result.pictureDtos = _jlPicture.GetAll().Where(o=>o.TjlCustomerRegID==cusinfo.Id).Select(w=>w.pictures).MapTo<List<PictureDto>>();

            //     result.ReState = 1;
            //     result.StateMa = "已总检";
            //     return result;
            //}


        }

        #region 体检人状态接口

        #endregion
        #region 体检人信息接口
        /// <summary>
        /// 获取更新体检号
        /// </summary>
        /// <param name="cosPrice"></param>
        /// <returns></returns>     
        public List<ClientRegBMDto> CustomerBM(ReceiveCos cosPrice)
        {
            var cusBM = new List<ClientRegBMDto>();
            var custlist = _jlCustomer.GetAll();
            var resuitl = new List<CustomerJson>();
            if (!string.IsNullOrWhiteSpace(cosPrice.ArchivesNum))
            {
                custlist = custlist.Where(o => o.ArchivesNum == cosPrice.ArchivesNum);
            }
            if (cosPrice.starTime.HasValue)
            {
                custlist = custlist.Where(o => (o.LastModificationTime != null && o.LastModificationTime > cosPrice.starTime) || o.CreationTime > cosPrice.starTime);
            }
            if (cosPrice.EndTime.HasValue)
            {
                custlist = custlist.Where(o => (o.LastModificationTime != null && o.LastModificationTime <= cosPrice.EndTime) || o.CreationTime <= cosPrice.EndTime);
            }
            if (!cosPrice.starTime.HasValue)
            {
                var st = System.DateTime.Now.AddYears(-3);
                custlist = custlist.Where(o => (o.LastModificationTime != null && o.LastModificationTime > st) || o.CreationTime > st);
            }

            cusBM = custlist.Select(o => new ClientRegBMDto
            {
                ClientRegBM = o.ArchivesNum,
                LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
            }).OrderBy(o => o.LastTime).ThenBy(o => o.ClientRegBM).ToList();

            if (cosPrice.ErrCustomer != null && cosPrice.ErrCustomer.Count > 0)
            {
                var errlist = _jlCustomer.GetAll().Where(o => cosPrice.ErrCustomer.Contains(o.ArchivesNum)).Select(o => new ClientRegBMDto
                {
                    ClientRegBM = o.ArchivesNum,
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
                }).OrderBy(o => o.LastTime).ThenBy(o => o.ClientRegBM).ToList();
                cusBM.AddRange(errlist);
            }

            return cusBM;
        }
        /// <summary>
        /// 体检人信息接口
        /// </summary>
        /// <param name="cosPrice"></param>
        /// <returns></returns>    
        public List<CustomerJson> Customerlist(InCusRegBMDto cosPrice)
        {
            var custlist = _jlCustomer.GetAll().Where(o => o.ArchivesNum == cosPrice.CustomerBM);
            var resuitl = new List<CustomerJson>();
            custlist = custlist.OrderBy(o => o.CreationTime);
            var resultlist = custlist.MapTo<List<CustomerJson>>();
            return resultlist;

        }
        #endregion

        #region 体检报告接口
        /// <summary>
        /// 获取更新体检号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>       
        public List<ClientRegBMDto> getCusReBM(SeachRepot input)
        {
            var result = new List<ClientRegBMDto>();

            var que = _jlCustomerSummarize.GetAll().Where(o => o.CustomerReg.SummSate == (int)SummSate.Audited);
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                que = que.Where(o => o.CustomerReg.CustomerBM == input.CustomerBM);
            }
            if (!string.IsNullOrEmpty(input.IdCard))
            {
                que = que.Where(o => o.CustomerReg.Customer.IDCardNo == input.IdCard);
            }
            if (input.starTime.HasValue)
            {
                que = que.Where(o => o.ConclusionDate > input.starTime.Value);
            }
            if (input.EndTime.HasValue)
            {
                que = que.Where(o => o.ConclusionDate <= input.EndTime.Value);
            }
            if (!input.starTime.HasValue)
            {
                var startime = System.DateTime.Now.AddYears(-3);
                que = que.Where(o => o.ConclusionDate > startime);
            }
            result = que.Select(o => new ClientRegBMDto
            {
                ClientRegBM = o.CustomerReg.CustomerBM,
                LastTime = o.ConclusionDate,
                PhysicalType = o.CustomerReg.PhysicalType
            }).ToList();
            //如果有错误列表,附加上错误列表的数据
            if (input.ErrCustomerBMs != null && input.ErrCustomerBMs.Count > 0)
            {//已审核的异常数据重新上传
                var relst = _jlCustomerSummarize.GetAll().Where(o => o.CustomerReg.SummSate == 4 && input.ErrCustomerBMs.Contains(o.CustomerReg.CustomerBM)).
                    Select(o => new ClientRegBMDto
                    {
                        ClientRegBM = o.CustomerReg.CustomerBM,
                        LastTime = o.ConclusionDate,
                        PhysicalType = o.CustomerReg.PhysicalType
                    }).OrderBy(o => o.LastTime).ToList();

                result.AddRange(relst);
            }
            return result;
        }
        /// <summary>
        /// 获取报告
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>       
        public List<reportjson> RegportList(InCusRegBMDto input)
        {
            //获取报告设置
            var bgsz = _jTbmBasicDictionary.GetAll().
                Where(o => o.Type == "PresentationSet" && o.Value >= 40).ToList();
            //一般检查子报表
            var ybjc = bgsz.
               FirstOrDefault(o => o.Value == 40).Remarks;
            //检查子报表
            var jc = bgsz.FirstOrDefault(o => o.Value == 50).Remarks;
            //检验子报表
            var jy = bgsz.
               FirstOrDefault(o => o.Value == 60).Remarks;

            //影像及图像，一张图（目前不处理图片，先把图片科室放入影像子报表）
            //图像
            
            var yxjtx = bgsz.
               FirstOrDefault(o => o.Value == 80).Remarks;
            //一张图
            yxjtx += bgsz.
               FirstOrDefault(o => o.Value == 90).Remarks;
            var yzt= bgsz.
               FirstOrDefault(o => o.Value == 90).Remarks;
            //影像
            yxjtx += bgsz.
           FirstOrDefault(o => o.Value == 70).Remarks;

            var atrAdviceType = bgsz.
           FirstOrDefault(o => o.Value == 160)?.Remarks ?? "";
            var atrAdviceNameType = bgsz.
           FirstOrDefault(o => o.Value == 161)?.Remarks ?? "";
            var resuitls = new List<reportjson>();
            try
            {
                var cusregls = _jlCustomerSummarize.GetAll().FirstOrDefault(o => o.CustomerReg.CustomerBM == input.CustomerBM);
                //var cuspic = _CustomerItemPic.GetAll().Where(o=>o.CustomerRegBM.CustomerBM== input.CustomerBM && o.ItemBMID!=null).ToList();
                //没有数据直接返回
                if (cusregls == null)
                {
                    var result = new reportjson();
                    result.code = 0;
                    result.Mess = "没有该体检人，请核实！";
                    resuitls.Add(result);
                    return resuitls;
                }
                //每个人一个json，完整的报告信息
                //var regid = cusregls.Id;
                try
                {
                    var resuit = new reportjson();
                    //报告首页及页面页脚数据（体检人基本信息）
                    var infor = cusregls.CustomerReg;
                    var reportInfo = new ReportCusDto();
                    reportInfo.Name = infor.Customer.Name;
                    reportInfo.IDCardNo = infor.Customer.IDCardNo;
                    reportInfo.Mobile = infor.Customer.Mobile;
                    reportInfo.Sex = infor.Customer.Sex;
                    reportInfo.Age = infor.Customer.Age;
                    reportInfo.customerBm = infor.CustomerBM;
                    reportInfo.ArchivesNum = infor.Customer.ArchivesNum;
                    reportInfo.Nation = infor.Customer.Nation;
                    reportInfo.LoginDate = infor.LoginDate;
                    reportInfo.ClientName = infor.ClientInfo?.ClientName;
                    resuit.reportitems2 = reportInfo;
                    resuit.CustomerBM = infor.CustomerBM;
                    resuit.ArchivesNum = infor.Customer.ArchivesNum;
                    resuit.dingdan = infor.OrderNum;
                    //总检结论 健康建议
                    var suggest = cusregls;// _jlCustomerSummarize.GetAll().FirstOrDefault(o => o.CustomerRegID == regid);
                    var cussum = new ReportSuggestBear();
                    cussum.EmployeeBM = suggest.EmployeeBM?.Name;
                    cussum.ShEmployeeBM = suggest.ShEmployeeBM?.Name;

                    cussum.ConclusionDate = suggest.ConclusionDate;
                    //设置诊断建议样式
                    cussum.CharacterSummary = suggest.CharacterSummary.Replace("\r\n", "<br/>");
                    var adlis = FormatAdvice(atrAdviceType, atrAdviceNameType, cusregls.CustomerReg.CustomerSummarizeBM.ToList());
                    cussum.Advice = adlis.adviceContent;
                    cussum.DagnosisSummary = adlis.adviceName;
                    resuit.reportitems3 = cussum;
                    resuit.time = suggest.ConclusionDate;
                    //体检项目结果
                    var cusitemOld = _jlCustomerRegItem.GetAll().
                        OrderBy(o => o.DepartmentBM.OrderNum).
                        ThenBy(o => o.ItemGroupBM.OrderNum).
                        ThenBy(o => o.ItemGroupBM.Id).
                        ThenBy(o => o.ItemOrder).
                        Where(o => o.CustomerRegId == infor.Id && o.CustomerItemGroupBM.CheckState != 1 && o.CustomerItemGroupBM.IsAddMinus != (int)AddMinusType.Minus).Select(
                        o => new reportitemInfoOld
                        {
                            Id = o.Id,
                            ItemId = o.ItemId,
                            DeptName = o.DepartmentBM.Name ?? "",
                            ItemGroupName = o.ItemGroupBM.ItemGroupName ?? "",
                            ItemName = o.ItemName ?? "",
                            ItemResultChar = o.ItemResultChar ?? "",
                            Symbol = o.Symbol ?? "",
                            Unit = o.Unit ?? "",
                            Stand = o.Stand ?? "",
                            //检查医生
                            CheckDoctor = o.InspectEmployeeBM.Name ?? "",
                            //审核医生
                            CheckEmployeDoctor = o.CheckEmployeeBM.Name ?? "",
                            ProcessState = o.ProcessState ?? 1,
                            DeptEmp = "",
                            DeptSum = ""
                      

                        }).ToList();
                    //同一个项目多次记录
                    var itemIDs = cusitemOld.Where(o => yxjtx.Contains(o.DeptName)).GroupBy(o => o.ItemId).Where(o => o.Count() > 1 ).Select(o => o.Key).ToList();
                    var yztitem = cusitemOld.Where(o => o.DeptName.Contains("彩超") && yzt.Contains(o.DeptName) && 
                    !itemIDs.Contains(o.ItemId)).Select(o=>o.ItemResultChar).Distinct().ToList();                 


                    #region 多部位一报告显示一遍
                    //北仑多部位一报告处理
                    if (yztitem.Count > 0)
                    {
                        foreach (var itemValue in yztitem)
                        {
                            var yztitemIds = cusitemOld.Where(o => o.DeptName.Contains("彩超") && yzt.Contains(o.DeptName) && o.ItemResultChar == itemValue).Select(
                                o=>o.ItemId).ToList();
                            var cusItems = cusitemOld.Where(o => yztitemIds.Contains(o.ItemId));
                            var groupName = string.Join(",", cusItems.Select(o => o.ItemGroupName).ToList());
                            string isll = "P";
                            var isllcout = cusItems.Where(o => o.Symbol == "P").ToList();
                            if (isllcout.Count == 0)
                            {
                                isll = "M";
                            }
                            foreach (var cusitem in cusItems)
                            {
                                // var newitemvalue = cusitemOld.FirstOrDefault(o=>o.Id==cusitem.Id);
                                cusitem.ItemGroupName = groupName;
                                cusitem.ItemName = "彩超所见";
                                cusitem.Symbol = isll;
                            }
                        }                        
                    }
                    //多部位报告显示一遍 
                    if (itemIDs.Count > 0)
                    {
                        foreach (var itemid in itemIDs)
                        {
                            var cusItems = cusitemOld.Where(o => o.ItemId == itemid);
                            var groupName = string.Join(",", cusItems.Select(o => o.ItemGroupName).ToList());
                            foreach (var cusitem in cusItems)
                            {
                                cusitem.ItemGroupName = groupName;
                            }
                        }                      
                    }
                    #endregion
                  
                    var cusItemls = cusitemOld.GroupBy(o=>new {
                         o.DeptName,
                         o.ItemGroupName,
                         o.ItemName,
                         o.ItemResultChar,
                         o.Symbol,
                        o.Unit,
                         o.Stand,
                        //检查医生
                         o.CheckDoctor,
                        //审核医生
                         o.CheckEmployeDoctor,
                         o.ProcessState
                    }).Select(
                       o => new reportitemInfo
                       {
                           DeptName = o.Key.DeptName,
                           ItemGroupName = o.Key.ItemGroupName,
                           ItemName = o.Key.ItemName,
                           ItemResultChar = o.Key.ItemResultChar,
                           Symbol = o.Key.Symbol,
                           Unit = o.Key.Unit,
                           Stand = o.Key.Stand,
                            //检查医生
                            CheckDoctor = o.Key.CheckDoctor,
                            //审核医生
                            CheckEmployeDoctor = o.Key.CheckEmployeDoctor,
                           ProcessState = o.Key.ProcessState

                       }).ToList();

                    #region 绑定组合小结科室小结 固定检查模板为科室小结，其他为组合小结
                    //去重复组合列表

                    //获取除检查模板的组合小结
                    var cusgrouplist = _jlCustomerItemGroup.GetAll().Where(o => o.CustomerRegBMId == infor.Id && !jc.Contains(o.DepartmentBM.Name)).ToList();
                    var groupNamels = cusItemls.Where(o=> !jc.Contains(o.DeptName)).Select(o => o.ItemGroupName).Distinct().ToList();
                           //循环科室给第一个组合小结赋值
                    foreach (var groupname in groupNamels)
                    {
                        //获取该组合小结
                        var groupsum = cusgrouplist.FirstOrDefault(o => o.ItemGroupBM.ItemGroupName == groupname);
                        if (groupsum == null)
                        {
                             groupsum = cusgrouplist.FirstOrDefault(o => groupname.Contains(o.ItemGroupBM.ItemGroupName ) && o.ItemGroupDiagnosis !="");
                            if (groupsum == null)
                            {
                                groupsum = cusgrouplist.FirstOrDefault(o => groupname.Contains(o.ItemGroupBM.ItemGroupName) );
                                if (groupsum == null)
                                {
                                    continue;
                                }
                            }
                        }
                        //获取第一条该组合项目结果 
                        var cusItem = cusItemls.FirstOrDefault(o => o.ItemGroupName == groupname);
                        if (string.IsNullOrEmpty(groupsum?.ItemGroupDiagnosis))
                        {
                            
                              cusItem.DeptSum = groupsum?.ItemGroupSum ?? "";
                            


                        }
                        else
                        {
                            cusItem.DeptSum = groupsum?.ItemGroupDiagnosis ?? "";

                        }
                        if (groupsum.CheckState != 1 && cusItem.DeptSum == "")
                        {
                            cusItem.DeptSum = "未见明显异常";
                        }
                        cusItem.DeptEmp = groupsum?.CheckEmployeeBM?.Name;
                        cusItem.CheckEmployeDoctor = groupsum?.InspectEmployeeBM?.Name;
                        //增加一个时间
                    }

                    //去重复检查类科室列表
                    var departNamels = cusItemls.Where(o => jc.Contains(o.DeptName)).Select(o => o.DeptName).Distinct().ToList();
                    //获取检查类科室小结
                    var cusDepartlist = _jlCustomerDepSummary.GetAll().Where(o => o.CustomerRegId == infor.Id && jc.Contains(o.DepartmentName)).ToList();
                    //循环科室给第一个项目科室小结赋值
                    foreach (var departneme in departNamels)
                    {
                        //获取该科室小结
                        var depatsum = cusDepartlist.FirstOrDefault(o => o.DepartmentBM.Name == departneme);
                        //获取第一条该科室项目结果
                        var cusItem = cusItemls.FirstOrDefault(o => o.DeptName == departneme);
                        if (string.IsNullOrEmpty(depatsum?.DagnosisSummary))
                        {
                            cusItem.DeptSum = depatsum?.CharacterSummary;
                        }
                        else
                        {
                            cusItem.DeptSum = depatsum?.DagnosisSummary;
                        }
                        cusItem.DeptEmp = depatsum?.ExamineEmployeeBM?.Name;
                        cusItem.CheckEmployeDoctor = depatsum?.ExamineEmployeeBM?.Name;
                        //增加一个时间
                    } 
                    #endregion

                    //一般检查
                    resuit.reportitem1 = cusItemls.Where(o => ybjc.Contains(o.DeptName)).ToList();
                  
                    //检查
                    resuit.reportitem2 = cusItemls.Where(o => jc.Contains(o.DeptName)).ToList();
                    //检验
                    resuit.reportitem3 = cusItemls.Where(o => jy.Contains(o.DeptName)).ToList();
                    //影像及图像
                    resuit.reportitem4 = cusItemls.Where(o => yxjtx.Contains(o.DeptName)).ToList();
                    resuit.Template = "常规体检报告.grf";
                    resuit.code = 1;
                    resuit.Mess = "成功查询该报告";
                    resuitls.Add(resuit);
                }
                catch (EntityNotFoundException e)
                {
                    var resuit = new reportjson();
                    resuit.code = 0;
                    resuit.Mess = e.Message;
                    resuitls.Add(resuit);
                }

                return resuitls;
            }
            catch (EntityNotFoundException e)
            {
                var resuit = new reportjson();
                resuit.code = 0;
                resuit.Mess = e.Message;
                resuitls.Add(resuit);
                return resuitls;
            }

        }
        #region 格式化总检建议
        private adviceNames FormatAdvice(string atrAdviceType, string atrAdviceNameType, List<TjlCustomerSummarizeBM> cusSumBMs)
        {
            adviceNames adviceNames = new adviceNames();
            string strAdvice = "";
            //建议格式

            string stradviceitem = "";
            //诊断格式
            string stradviceNames = "";

            string stradviceName = "";

            if (cusSumBMs.Count > 0)
            {
                foreach (var item in cusSumBMs.Where(o => o.ParentAdviceId == Guid.Empty || o.ParentAdviceId == null).OrderBy(o => o.SummarizeOrderNum))
                {  //建议内容
                    stradviceitem = atrAdviceType.Replace("【序号】",
                      item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                      item.SummarizeName.Trim()).Replace("【建议内容】", item.Advice.Trim()).Replace("【空格】", " ").Replace("【换行】", "<br/>");
                    strAdvice += stradviceitem;
                    //诊断格式
                    stradviceName = atrAdviceNameType.Replace("【序号】",
                    item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                    item.SummarizeName.Trim()).Replace("【空格】", " ").Replace("【换行】", "<br/>");
                    stradviceNames += stradviceName;

                    var Children = cusSumBMs.Where(o => o.ParentAdviceId == item.Id)?.OrderBy(o => o.SummarizeOrderNum).ToList();
                    if (Children != null && Children.Count() > 0)
                    {
                        foreach (var itemChildren in Children)
                        {
                            //建议格式
                            stradviceitem = atrAdviceNameType.Replace("【序号】",
                             "").Replace("【建议名称】",
                             itemChildren.SummarizeName.Trim()).Replace("【建议内容】", itemChildren.Advice.Trim()).Replace("【空格】", " ").Replace("【换行】", "<br/>");
                            strAdvice += stradviceitem;

                            //诊断格式
                            stradviceName = atrAdviceType.Replace("【序号】",
                            item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                            item.SummarizeName.Trim()).Replace("【空格】", " ").Replace("【换行】", "<br/>");
                            stradviceNames += stradviceName;
                        }
                    }
                }

            }
            adviceNames.adviceContent = strAdvice;
            adviceNames.adviceName = stradviceNames;
            if (adviceNames.adviceName == "")
            {
                var adls = cusSumBMs.OrderBy(r => r.SummarizeOrderNum).Select(o => (o.SummarizeOrderNum + "、" + o.SummarizeName));
                adviceNames.adviceName = string.Join("<br/>", adls);
            }
            return adviceNames;
        }
        #endregion
        #endregion
        #region 体检结果信息
        /// <summary>
        /// 体检人检查结果信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>       
        public List<WCusRegDto> getCusResult(InCusRegBMDto input)
        {
            var result = new List<WCusRegDto>();

            var que = _jlCustomerSummarize.GetAll().Where(o => o.CustomerReg.SummSate == (int)SummSate.Audited);

            que = que.Where(o => o.CustomerReg.CustomerBM == input.CustomerBM);

            result = que.OrderBy(o => o.ConclusionDate).Take(10).Select(o => new WCusRegDto
            {
                ArchivesNum = o.CustomerReg.Customer.ArchivesNum,
                OrderId = o.CustomerReg.OrderNum,
                BookingDate = o.CustomerReg.BookingDate,
                CheckSate = o.CustomerReg.CheckSate,
                ClientRegBM = o.CustomerReg.ClientReg.ClientRegBM,
                ClientTeamInfoBM = o.CustomerReg.ClientTeamInfo.TeamBM,
                CostState = o.CustomerReg.CostState,
                CustomerBM = o.CustomerReg.CustomerBM,
                CustomerDepSummary = o.CustomerReg.CustomerDepSummary.Select(r => new WCusDepSumDto
                {
                    CharacterSummary = r.CharacterSummary,
                    CheckDate = r.CheckDate,
                    DagnosisSummary = r.DagnosisSummary,
                    DepartmentBM = r.DepartmentBM.DepartmentBM,
                    EmpName = r.ExamineEmployeeBM.Name
                }).ToList(),
                CustomerSummarize = new WCusSumDto
                {
                    Advice = o.Advice,
                    CharacterSummary = o.CharacterSummary,
                    ConclusionDate = o.ConclusionDate,
                    DagnosisSummary = o.DagnosisSummary,
                    EmployeeName = o.EmployeeBM.Name,
                    ExamineDate = o.ExamineDate,
                    ShEmployeeName = o.ShEmployeeBM.Name
                },
                CustomerItemGroup = o.CustomerReg.CustomerItemGroup.Select(n => new WXCusGroupDto
                {
                    CheckState = n.CheckState,
                    DiscountRate = n.DiscountRate,
                    GRmoney = n.GRmoney,
                    IsAddMinus = n.IsAddMinus,
                    ItemGroupCodeBM = n.ItemGroupBM.ItemGroupBM,
                    ItemGroupName = n.ItemGroupName,
                    ItemPrice = n.ItemPrice,
                    ItemSuitBM = n.ItemSuit.ItemSuitID,
                    PayerCat = n.PayerCat,
                    PriceAfterDis = n.PriceAfterDis,
                    TTmoney = n.TTmoney,
                    CheckEmployeeName = n.CheckEmployeeBM.Name,
                    InspectEmployeeName = n.InspectEmployeeBM.Name
                }).ToList(),
                CustomerRegItems = o.CustomerReg.CustomerRegItems.Select(q => new WCusRegItemDto
                {
                    CheckEmployeeName = q.CheckEmployeeBM.Name,
                    CrisisSate = q.CrisisSate,
                    IllnessLevel = q.IllnessLevel,
                    IllnessSate = q.IllnessSate,
                    InspectEmployeeName = q.InspectEmployeeBM.Name,
                    ItemBM = q.ItemBM.ItemBM,
                    ItemDiagnosis = q.ItemDiagnosis,
                    ItemGroupBM = q.ItemGroupBM.ItemGroupBM,
                    ItemName = q.ItemName,
                    ItemResultChar = q.ItemResultChar,
                    ItemSum = q.ItemSum,
                    PositiveSate = q.PositiveSate,
                    ProcessState = q.ProcessState,
                    Stand = q.Stand,
                    Symbol = q.Symbol,
                    Unit = q.Unit
                }).ToList(),
                CustomerRegNum = o.CustomerReg.CustomerRegNum,
                CustomerType = o.CustomerReg.CustomerType,
                ItemSuitBM = o.CustomerReg.ItemSuitBM.ItemSuitID,
                LastTime = o.ConclusionDate,
                LoginDate = o.CustomerReg.LoginDate,
                MailingReport = o.CustomerReg.MailingReport,
                MarriageStatus = o.CustomerReg.MarriageStatus,
                PhysicalType = o.CustomerReg.PhysicalType,
                PrintSate = o.CustomerReg.PrintSate,
                RegAge = o.CustomerReg.RegAge,
                RegisterState = o.CustomerReg.RegisterState,
                SummSate = o.CustomerReg.SummSate,
                WebQueryCode = o.CustomerReg.WebQueryCode
            }).ToList();
            return result;


        }
        #endregion
        #region 基本信息接口
        /// <summary>
        /// 科室信息
        /// </summary>
        /// <returns></returns>     
        public List<ReportDepartDto> getDeparts(SearchBiseDto dto)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                //按修改时间获取更新的科室信息
                List<ReportDepartDto> list = new List<ReportDepartDto>();
                var serch = _bmDepartment.GetAll();
                if (dto.starTime.HasValue)
                {
                    serch = serch.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
                }
                list = serch.Select(o => new ReportDepartDto
                {
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                    Address = o.Address,
                    Category = o.Category,
                    DepartmentBM = o.DepartmentBM,
                    Duty = o.Duty,
                    HelpChar = o.HelpChar,
                    MaxCheckDay = o.MaxCheckDay,
                    MenAddress = o.MenAddress,
                    Name = o.Name,
                    OrderNum = o.OrderNum,
                    Remarks = o.Remarks,
                    Sex = o.Sex,
                    WBCode = o.WBCode,
                    WomenAddress = o.WomenAddress,
                    State = 1
                }).ToList();
                //上传失败的数据信息
                if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
                {
                    var Errserch = _bmDepartment.GetAll().Where(o => dto.ErrIDBMs.Contains(o.DepartmentBM));
                    var erlist = Errserch.Select(o => new ReportDepartDto
                    {
                        LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                        Address = o.Address,
                        Category = o.Category,
                        DepartmentBM = o.DepartmentBM,
                        Duty = o.Duty,
                        HelpChar = o.HelpChar,
                        MaxCheckDay = o.MaxCheckDay,
                        MenAddress = o.MenAddress,
                        Name = o.Name,
                        OrderNum = o.OrderNum,
                        Remarks = o.Remarks,
                        Sex = o.Sex,
                        WBCode = o.WBCode,
                        WomenAddress = o.WomenAddress,
                        State = 1
                    }).ToList();
                    list.AddRange(erlist);
                }
                List<ReportDepartDto> listretrn = new List<ReportDepartDto>();
                if (list.Count > 500)
                {
                    var endtime = list.OrderBy(o => o.LastTime).Take(500).LastOrDefault().LastTime;

                    listretrn = list.OrderBy(o => o.LastTime).Where(o => o.LastTime <= endtime).Select(o => new ReportDepartDto
                    {
                        LastTime = o.LastTime,
                        Address = o.Address,
                        Category = o.Category,
                        DepartmentBM = o.DepartmentBM,
                        Duty = o.Duty,
                        HelpChar = o.HelpChar,
                        MaxCheckDay = o.MaxCheckDay,
                        MenAddress = o.MenAddress,
                        Name = o.Name,
                        OrderNum = o.OrderNum,
                        Remarks = o.Remarks,
                        Sex = o.Sex,
                        WBCode = o.WBCode,
                        WomenAddress = o.WomenAddress,
                        State = 0
                    }).OrderBy(o => o.LastTime).ToList();
                }
                else
                {
                    listretrn = list.OrderBy(o => o.LastTime).ToList();
                }
                return listretrn;
            //}
        }
        /// <summary>
        /// 组合信息
        /// </summary>
        /// <returns></returns>       
        public List<ReportItemGroupDto> getItemGroups(SearchBiseDto dto)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                List<ReportItemGroupDto> list = new List<ReportItemGroupDto>();
                var serch = _bmItemGroup.GetAll().Where(p=>p.IsActive==true);
                if (dto.starTime.HasValue)
                {
                    serch = serch.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
                }
                list = serch.Select(o => new ReportItemGroupDto
                {
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                    CostPrice = o.CostPrice,
                    DepartmentBM = o.Department.DepartmentBM,
                    DrawState = o.DrawState,
                    GroupItems = o.ItemInfos.Select(r => new ReportGroupItems
                    {
                        ItemBM = r.ItemBM
                    }).ToList(),
                    HelpChar = o.HelpChar,
                    HISID = o.HISID,
                    HISName = o.HISName,
                    IsActive = o.IsActive,
                    ItemGroupBM = o.ItemGroupBM,
                    ItemGroupExplain = o.ItemGroupExplain,
                    ItemGroupName = o.ItemGroupName,
                    MaxAge = o.MaxAge,
                    MaxDiscount = o.MaxDiscount,
                    MealState = o.MealState,
                    MinAge = o.MinAge,
                    Notice = o.Notice,
                    OrderNum = o.OrderNum,
                    OutgoingState = o.OutgoingState,
                    Price = o.Price,
                    PrivacyState = o.PrivacyState,
                    Remarks = o.Remarks,
                    Sex = o.Sex,
                    Taboo = o.Taboo,
                    WBCode = o.WBCode,
                    WomenState = o.WomenState,
                    State = 1

                }).ToList();
                if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
                {
                    var Errserch = _bmItemGroup.GetAll().Where(o => dto.ErrIDBMs.Contains(o.ItemGroupBM));
                    var erlist = Errserch.Select(o => new ReportItemGroupDto
                    {
                        LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                        CostPrice = o.CostPrice,
                        DepartmentBM = o.Department.DepartmentBM,
                        DrawState = o.DrawState,
                        GroupItems = o.ItemInfos.Select(r => new ReportGroupItems
                        {
                            ItemBM = r.ItemBM
                        }).ToList(),
                        HelpChar = o.HelpChar,
                        HISID = o.HISID,
                        HISName = o.HISName,
                        IsActive = o.IsActive,
                        ItemGroupBM = o.ItemGroupBM,
                        ItemGroupExplain = o.ItemGroupExplain,
                        ItemGroupName = o.ItemGroupName,
                        MaxAge = o.MaxAge,
                        MaxDiscount = o.MaxDiscount,
                        MealState = o.MealState,
                        MinAge = o.MinAge,
                        Notice = o.Notice,
                        OrderNum = o.OrderNum,
                        OutgoingState = o.OutgoingState,
                        Price = o.Price,
                        PrivacyState = o.PrivacyState,
                        Remarks = o.Remarks,
                        Sex = o.Sex,
                        Taboo = o.Taboo,
                        WBCode = o.WBCode,
                        WomenState = o.WomenState,
                        State = 1
                    }).ToList();
                    list.AddRange(erlist);
                }
                List<ReportItemGroupDto> listretrn = new List<ReportItemGroupDto>();
                if (list.Count > 500)
                {
                    var endtime = list.OrderBy(o => o.LastTime).Take(500).LastOrDefault().LastTime;

                    listretrn = list.OrderBy(o => o.LastTime).Where(o => o.LastTime <= endtime).Select(o => new ReportItemGroupDto
                    {
                        LastTime = o.LastTime,
                        CostPrice = o.CostPrice,
                        DepartmentBM = o.DepartmentBM,
                        DrawState = o.DrawState,
                        GroupItems = o.GroupItems,
                        HelpChar = o.HelpChar,
                        HISID = o.HISID,
                        HISName = o.HISName,
                        IsActive = o.IsActive,
                        ItemGroupBM = o.ItemGroupBM,
                        ItemGroupExplain = o.ItemGroupExplain,
                        ItemGroupName = o.ItemGroupName,
                        MaxAge = o.MaxAge,
                        MaxDiscount = o.MaxDiscount,
                        MealState = o.MealState,
                        MinAge = o.MinAge,
                        Notice = o.Notice,
                        OrderNum = o.OrderNum,
                        OutgoingState = o.OutgoingState,
                        Price = o.Price,
                        PrivacyState = o.PrivacyState,
                        Remarks = o.Remarks,
                        Sex = o.Sex,
                        Taboo = o.Taboo,
                        WBCode = o.WBCode,
                        WomenState = o.WomenState,
                        State = 0
                    }).OrderBy(o => o.LastTime).ToList();
                }
                else
                {
                    listretrn = list.OrderBy(o => o.LastTime).ToList();
                }
                return listretrn;

            //}
        }
        /// <summary>
        /// 项目信息
        /// </summary>
        /// <returns></returns>        
        public List<ReportItemInfoDto> getItemInfos(SearchBiseDto dto)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                List<ReportItemInfoDto> list = new List<ReportItemInfoDto>();
                var serch = _tbmItemInfo.GetAll().Where(p=>p.IsActive==1);
                if (dto.starTime.HasValue)
                {
                    serch = serch.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
                }
                list = serch.Select(o => new ReportItemInfoDto
                {
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                    Ckisrpot = o.Ckisrpot,
                    DepartmentBM = o.Department.DepartmentBM,
                    HelpChar = o.HelpChar,
                    HISCode = o.HISCode,
                    ItemBM = o.ItemBM,
                    Lclxid = o.Lclxid,
                    MaxAge = o.MaxAge,
                    MinAge = o.MinAge,
                    moneyType = o.moneyType,
                    Name = o.Name,
                    NameEngAbr = o.NameEngAbr,
                    NamePM = o.NamePM,
                    Notice = o.Notice,
                    OrderNum = o.OrderNum,
                    Remark = o.Remark,
                    ReportCode = o.ReportCode,
                    Sex = o.Sex,
                    StandardCode = o.StandardCode,
                    Unit = o.Unit,
                    WBCode = o.WBCode,
                    State = 1
                }).ToList();
                if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
                {
                    var Errserch = _tbmItemInfo.GetAll().Where(o => dto.ErrIDBMs.Contains(o.ItemBM));
                    var erlist = Errserch.Select(o => new ReportItemInfoDto
                    {
                        LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                        Ckisrpot = o.Ckisrpot,
                        DepartmentBM = o.Department.DepartmentBM,
                        HelpChar = o.HelpChar,
                        HISCode = o.HISCode,
                        ItemBM = o.ItemBM,
                        Lclxid = o.Lclxid,
                        MaxAge = o.MaxAge,
                        MinAge = o.MinAge,
                        moneyType = o.moneyType,
                        Name = o.Name,
                        NameEngAbr = o.NameEngAbr,
                        NamePM = o.NamePM,
                        Notice = o.Notice,
                        OrderNum = o.OrderNum,
                        Remark = o.Remark,
                        ReportCode = o.ReportCode,
                        Sex = o.Sex,
                        StandardCode = o.StandardCode,
                        Unit = o.Unit,
                        WBCode = o.WBCode,
                        State = 1
                    }).ToList();
                    list.AddRange(erlist);
                }
                List<ReportItemInfoDto> listretrn = new List<ReportItemInfoDto>();
                if (list.Count > 500)
                {
                    var endtime = list.OrderBy(o => o.LastTime).Take(500).LastOrDefault().LastTime;

                    listretrn = list.OrderBy(o => o.LastTime).Where(o => o.LastTime <= endtime).Select(o => new ReportItemInfoDto
                    {
                        LastTime = o.LastTime,
                        Ckisrpot = o.Ckisrpot,
                        DepartmentBM = o.DepartmentBM,
                        HelpChar = o.HelpChar,
                        HISCode = o.HISCode,
                        ItemBM = o.ItemBM,
                        Lclxid = o.Lclxid,
                        MaxAge = o.MaxAge,
                        MinAge = o.MinAge,
                        moneyType = o.moneyType,
                        Name = o.Name,
                        NameEngAbr = o.NameEngAbr,
                        NamePM = o.NamePM,
                        Notice = o.Notice,
                        OrderNum = o.OrderNum,
                        Remark = o.Remark,
                        ReportCode = o.ReportCode,
                        Sex = o.Sex,
                        StandardCode = o.StandardCode,
                        Unit = o.Unit,
                        WBCode = o.WBCode,
                        State = 0
                    }).OrderBy(o => o.LastTime).ToList();
                }
                else
                {
                    listretrn = list.OrderBy(o => o.LastTime).ToList();
                }
                return listretrn;

            //}
        }
        /// <summary>
        /// 套餐信息
        /// </summary>
        /// <returns></returns>       
        public List<ReportItemSuitDto> getItemSuits(SearchBiseDto dto)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                List<ReportItemSuitDto> list = new List<ReportItemSuitDto>();
                var serch = _bmItemSuit.GetAll().Where(p=>p.Available==1);
                if (dto.starTime.HasValue)
                {
                    serch = serch.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
                }
                list = serch.Select(o => new ReportItemSuitDto
                {
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                    Available = o.Available,
                    CjPrice = o.CjPrice,
                    ConceiveStatus = o.ConceiveStatus,
                    CostPrice = o.CostPrice,
                    ExaminationType = o.ExaminationType,
                    HelpChar = o.HelpChar,
                    ItemSuitID = o.ItemSuitID,
                    ItemSuitItemGroups = o.ItemSuitItemGroups.Select(r => new ReportItemSuitItemGroupDto
                    {
                        ItemGroupBM = r.ItemGroup.ItemGroupBM,
                        ItemPrice = r.ItemPrice,
                        PriceAfterDis = r.PriceAfterDis,
                        Suitgrouprate = r.Suitgrouprate
                    }).ToList(),
                    ItemSuitName = o.ItemSuitName,
                    ItemSuitType = o.ItemSuitType,
                    MaritalStatus = o.MaritalStatus,
                    MaxAge = o.MaxAge,
                    MinAge = o.MinAge,
                    Notice = o.Notice,
                    OrderNum = o.OrderNum,
                    Price = o.Price,
                    Remarks = o.Remarks,
                    RiskName = o.RiskName,
                    Sex = o.Sex,
                    WBCode = o.WBCode,
                    State = 1

                }).ToList();
                if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
                {
                    var Errserch = _bmItemSuit.GetAll().Where(o => dto.ErrIDBMs.Contains(o.ItemSuitID));
                    var erlist = Errserch.Select(o => new ReportItemSuitDto
                    {
                        LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                        Available = o.Available,
                        CjPrice = o.CjPrice,
                        ConceiveStatus = o.ConceiveStatus,
                        CostPrice = o.CostPrice,
                        ExaminationType = o.ExaminationType,
                        HelpChar = o.HelpChar,
                        ItemSuitID = o.ItemSuitID,
                        ItemSuitItemGroups = o.ItemSuitItemGroups.Select(r => new ReportItemSuitItemGroupDto
                        {
                            ItemGroupBM = r.ItemGroup.ItemGroupBM,
                            ItemPrice = r.ItemPrice,
                            PriceAfterDis = r.PriceAfterDis,
                            Suitgrouprate = r.Suitgrouprate
                        }).ToList(),
                        ItemSuitName = o.ItemSuitName,
                        ItemSuitType = o.ItemSuitType,
                        MaritalStatus = o.MaritalStatus,
                        MaxAge = o.MaxAge,
                        MinAge = o.MinAge,
                        Notice = o.Notice,
                        OrderNum = o.OrderNum,
                        Price = o.Price,
                        Remarks = o.Remarks,
                        RiskName = o.RiskName,
                        Sex = o.Sex,
                        WBCode = o.WBCode,
                        State = 1
                    }).ToList();
                    list.AddRange(erlist);
                }
                List<ReportItemSuitDto> listretrn = new List<ReportItemSuitDto>();
                if (list.Count > 500)
                {
                    var endtime = list.OrderBy(o => o.LastTime).Take(500).LastOrDefault().LastTime;

                    listretrn = list.OrderBy(o => o.LastTime).Where(o => o.LastTime <= endtime).Select(o => new ReportItemSuitDto
                    {
                        LastTime = o.LastTime,
                        Available = o.Available,
                        CjPrice = o.CjPrice,
                        ConceiveStatus = o.ConceiveStatus,
                        CostPrice = o.CostPrice,
                        ExaminationType = o.ExaminationType,
                        HelpChar = o.HelpChar,
                        ItemSuitID = o.ItemSuitID,
                        ItemSuitItemGroups = o.ItemSuitItemGroups,
                        ItemSuitName = o.ItemSuitName,
                        ItemSuitType = o.ItemSuitType,
                        MaritalStatus = o.MaritalStatus,
                        MaxAge = o.MaxAge,
                        MinAge = o.MinAge,
                        Notice = o.Notice,
                        OrderNum = o.OrderNum,
                        Price = o.Price,
                        Remarks = o.Remarks,
                        RiskName = o.RiskName,
                        Sex = o.Sex,
                        WBCode = o.WBCode,
                        State = 0

                    }).OrderBy(o => o.LastTime).ToList();
                }
                else
                {
                    listretrn = list.OrderBy(o => o.LastTime).ToList();
                }
                return listretrn;
            //}
        }
        /// <summary>
        /// 获取建议编码
        /// </summary>
        /// <returns></returns>        
        public List<ClientRegBMDto> getISummarizeBM(SearchBiseDto dto)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                List<ClientRegBMDto> list = new List<ClientRegBMDto>();
                var serch = _tbmSummarizeAdvice.GetAll();
                if (dto.starTime.HasValue)
                {
                    serch = serch.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
                }
                list = serch.Select(o => new ClientRegBMDto
                {
                    ClientRegBM = o.Uid,
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
                }).ToList();
                if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
                {
                    var Errserch = _tbmSummarizeAdvice.GetAll().Where(o => dto.ErrIDBMs.Contains(o.Uid));
                    var Errlist = serch.Select(o => new ClientRegBMDto
                    {
                        ClientRegBM = o.Uid,
                        LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
                    }).ToList();
                    list.AddRange(Errlist);
                }
                return list.OrderBy(o => o.LastTime).ToList();
            //}
        }
        /// <summary>
        /// 获取建议库
        /// </summary>
        /// <returns></returns>      
        public List<ReportSummarizeAdviceDto> getISummarizeAdvices(InCusRegBMDto dto)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                List<ReportSummarizeAdviceDto> list = new List<ReportSummarizeAdviceDto>();
                var serch = _tbmSummarizeAdvice.GetAll().Where(o => o.Uid == dto.CustomerBM);
                list = serch.Select(o => new ReportSummarizeAdviceDto
                {
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                    AdviceName = o.AdviceName,
                    Advicevalue = o.Advicevalue,
                    ClientAdvice = o.ClientAdvice,
                    CrisisSate = o.CrisisSate,
                    DepartmentAdvice = o.DepartmentAdvice,
                    DepartmentBM = o.Department.DepartmentBM,
                    DiagnosisAType = o.DiagnosisAType,
                    DiagnosisExpain = o.DiagnosisExpain,
                    DiagnosisSate = o.DiagnosisSate,
                    DietGuide = o.DietGuide,
                    HealthcareAdvice = o.HealthcareAdvice,
                    HelpChar = o.HelpChar,
                    HideOnGroupReport = o.HideOnGroupReport,
                    Knowledge = o.Knowledge,
                    MarrySate = o.MarrySate,
                    MaxAge = o.MaxAge,
                    MinAge = o.MinAge,
                    OrderNum = o.OrderNum,
                    SexState = o.SexState,
                    SportGuide = o.SportGuide,
                    SummAdvice = o.SummAdvice,
                    SummState = o.SummState,
                    Uid = o.Uid

                }).ToList();
                return list;
            //}
        }
        #endregion
        #region 团体预约
        /// <summary>
        /// 获取更新的预约编码
        /// </summary>
        /// <returns></returns>        
        public List<ClientRegBMDto> getClientRegBM(SearchBiseDto dto)
        {
            List<ClientRegBMDto> list = new List<ClientRegBMDto>();
            var que = _jlClientReg.GetAll().Where(p=>p.ControlDate==1);
            if (dto.starTime.HasValue)
            {
                que = que.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime) || o.CustomerReg.Any(r => r.CreationTime > dto.starTime));
            }
            list = que.Select(o => new ClientRegBMDto
            {
                ClientRegBM = o.ClientRegBM,
                LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
            }).ToList();
            if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
            {
                var erlist = _jlClientReg.GetAll().Where(o => dto.ErrIDBMs.Contains(o.ClientRegBM)).
                    Select(o => new ClientRegBMDto
                    {
                        ClientRegBM = o.ClientRegBM,
                         ControlDate=o.ControlDate,
                        LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
                    }).ToList();
                list.AddRange(erlist);
            }
            return list.OrderBy(o => o.LastTime).ToList();
        }
        /// <summary>
        /// 根据编码获取单位预约信息
        /// </summary>
        /// <returns></returns>        
        public List<WXClientRegDto> getClientRegInfo(SearchClientCusDto dto)
        {
            List<WXClientRegDto> list = new List<WXClientRegDto>();
            var que = _jlClientReg.GetAll().Where(o => o.ClientRegBM == dto.ClientRegBM);
            list = que.MapTo<List<WXClientRegDto>>();
            return list;
        }
        /// <summary>
        /// 单位根据编码更新时间节点获取更新的人员编码
        /// </summary>
        /// <returns></returns>       
        public List<ClientRegBMDto> getClientRegCusBM(SearchClientCusDto dto)
        {
            List<ClientRegBMDto> list = new List<ClientRegBMDto>();
            var que = _jlCustomerReg.GetAll().Where(o => o.ClientReg.ClientRegBM == dto.ClientRegBM);

            if (dto.starTime.HasValue && string.IsNullOrEmpty(dto.ClientRegBM))
            {
                // dto.starTime = dto.starTime.Value.AddHours(-2);
                que = que.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
            }
            if (string.IsNullOrEmpty(dto.ClientRegBM) && !dto.starTime.HasValue)
            {
                que = que.Take(10);
            }
            list = que.Select(o => new ClientRegBMDto
            {
                ClientRegBM = o.CustomerBM,
                LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime

            }).ToList();
            if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
            {
                var Erque = _jlCustomerReg.GetAll().Where(o => o.ClientReg.ClientRegBM == dto.ClientRegBM
                && dto.ErrIDBMs.Contains(o.CustomerBM));

                var Erlist = que.Select(o => new ClientRegBMDto
                {
                    ClientRegBM = o.CustomerBM,
                    LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
                }).ToList();
                list.AddRange(Erlist);
            }
            return list.OrderBy(o => o.LastTime).ToList();
        }
        /// <summary>
        /// 单位根据编码更新时间节点获取更新的人员信息
        /// </summary>
        /// <returns></returns>       
        public List<WXClientCusInfoDto> getClientRegCus(InCusRegBMDto dto)
        {
            List<WXClientCusInfoDto> list = new List<WXClientCusInfoDto>();
            var que = _jlCustomerReg.GetAll().Where(o => o.CustomerBM == dto.CustomerBM);


            list = que.Select(o => new WXClientCusInfoDto
            {
                ArchivesNum = o.Customer.ArchivesNum,
                BookingDate = o.BookingDate,
                CheckSate = o.CheckSate,
                ClientRegBM = o.ClientReg.ClientRegBM,
                ClientTeamInfoBM = o.ClientTeamInfo.TeamBM,
                CustomerBM = o.CustomerBM,
                CustomerItemGroup = o.CustomerItemGroup.Select(r => new WXCusGroupDto
                {
                    CheckState = r.CheckState,
                    DiscountRate = r.DiscountRate,
                    GRmoney = r.GRmoney,
                    IsAddMinus = r.IsAddMinus,
                    ItemGroupCodeBM = r.ItemGroupBM.ItemGroupBM,
                    ItemGroupName = r.ItemGroupName,
                    ItemPrice = r.ItemPrice,
                    ItemSuitBM = r.ItemSuit.ItemSuitID,
                    PayerCat = r.PayerCat,
                    PriceAfterDis = r.PriceAfterDis,
                    TTmoney = r.TTmoney
                }).ToList(),
                CustomerRegNum = o.CustomerRegNum,
                ItemSuitBM = o.ItemSuitBM.ItemSuitID,
                LoginDate = o.LoginDate,
                MarriageStatus = o.MarriageStatus,
                RegAge = o.RegAge,
                RegisterState = o.RegisterState,
                SummSate = o.SummSate,
                WebQueryCode = o.WebQueryCode,
                 LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
            }).ToList();
            return list.OrderBy(o => o.LoginDate).ToList();
        }
        /// <summary>
        /// 团体预约（只修改日期）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>       
        public ResultDto RegClientCusDate(SearchRegDateDto dto)
        {
            ResultDto resuit = new ResultDto();
            try
            {

                var cusreginfo = _jlCustomerReg.FirstOrDefault(o => o.CustomerBM == dto.CustomerBM);
                if (cusreginfo == null)
                {
                    resuit.code = 0;
                    resuit.Mess = "没有该体检人信息";
                    return resuit;
                }
                if (dto.RegTime != null)
                {
                    cusreginfo.BookingDate = dto.RegTime;
                    cusreginfo.InfoSource =2;
                    cusreginfo.BookingStatus = 1;
                    cusreginfo.AppointmentTime = dto.RegTime;
                    var isup = _jlCustomerReg.Update(cusreginfo);
                    resuit.code = 1;
                    resuit.Mess = "更新成功！";
                }
                else
                {
                    resuit.code = 0;
                    resuit.Mess = "时间错误！";
                }
                return resuit;

            }
            catch (EntityNotFoundException e)
            {

                resuit.code = 0;
                resuit.Mess = e.Message;
                return resuit;

            }
        }

        #endregion
        /// <summary>
        /// 将byte数组转换为文件并保存到指定地址
        /// </summary>
        /// <param name="buff">byte数组</param>
        /// <param name="savepath">保存地址</param>
        public static void Bytes2File(byte[] buff, string savepath)
        {
            if (System.IO.File.Exists(savepath))
            {
                System.IO.File.Delete(savepath);
            }


            FileStream fs = new FileStream(savepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(buff, 0, buff.Length);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="belong">文件归属于</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Picture.Dto.PictureDto Uploading(string inPicbase64, string belong, Guid? id = null)
        {
            //HttpPostedFile
            //HttpPostedFileBase
            //HttpPostedFileWrapper
            //Directory

            #region 照片

            var baseDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            var fileDirectory = Path.Combine(baseDirectory, "Upload");
            var pictureDirectory = Path.Combine(fileDirectory, "Picture");
            var belongDirectory = Path.Combine(pictureDirectory, belong);
            var date = DateTime.Now.ToString("yyyyMM");
            var dateDirectory = Path.Combine(belongDirectory, date);
            if (!Directory.Exists(dateDirectory))
                Directory.CreateDirectory(dateDirectory);
            var createPictureDto = new CreateOrUpdatePictureDto
            {
                Id = Guid.NewGuid(),
                Belong = belong
            };
            var extension = ".jpg";
            var fileName = Path.Combine(dateDirectory, $"{createPictureDto.Id}{extension}");
            // var jpgfile = pdfDire + "\\" + data.Customer.ArchivesNum + ".jpg";
            //二进制转pdf
            byte[] byteArray = Convert.FromBase64String(inPicbase64);
            Bytes2File(byteArray, fileName);  
            
            #endregion  
           
            if (id != null && id != Guid.Empty)
            {
                createPictureDto.Id = id.Value;
            }
           
            
            GC.Collect();
            createPictureDto.RelativePath = fileName.Replace(baseDirectory, "");
            var thumbnail = Path.Combine(dateDirectory, "Thumbnail");
            if (!Directory.Exists(thumbnail))
                Directory.CreateDirectory(thumbnail);
            var thumbnailFileName = Path.Combine(thumbnail, $"{createPictureDto.Id}{extension}");
            using (var img = Image.FromFile(fileName))
            {
                if (img.Height <= 300 && img.Width <= 300)
                {
                    img.Save(thumbnailFileName);
                    createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                }
                else
                {
                    var width = 300;
                    var height = 300;
                    if (img.Height > img.Width)
                        width = (int)(300d / img.Height * img.Width);
                    else
                        height = (int)(300d / img.Width * img.Height);

                    using (var img1 = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
                    {
                        img1.Save(thumbnailFileName);
                        createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                    }
                }
            }

            try
            {
                var result = _pictureController.Create(createPictureDto);
                return result;
            }
            catch (Exception)
            {
                System.IO.File.Delete(fileName);
                System.IO.File.Delete(thumbnailFileName);
            }

            return new Picture.Dto.PictureDto();
        }
        #region 预约
        /// <summary>
        /// 预约
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        public OutResult RegCus(NInCusInfoDto input)
        {
            try
            {
                //ICustomerAppService customerSvr = new CustomerAppService();
                OutResult outResult = new OutResult();
                Guid customerregid = Guid.Empty;
                Guid clientregID = Guid.Empty;
                var TjlCusAddPackItems = new List<TjlCustomerItemGroupDto>();
                if (input.IsTT == 0)
                {
                    #region 个人预约
                    if (!string.IsNullOrEmpty(input.OrderId))
                    {
                        var oldcus = _jlCustomerReg.GetAll().FirstOrDefault(p=>p.OrderNum== input.OrderId);
                        if (oldcus != null)
                        {
                            outResult.Code = 0;
                            outResult.ErrInfo = input.Name+"_"+ input.OrderId + "已预约，无需重复预约";

                            return outResult;
                        }
                    }
                    QueryCustomerRegDto data = new QueryCustomerRegDto();

                    data.BarState = 1;
                    data.BlindSate = 1;
                    data.BookingDate = input.BookingDate;
                    data.AppointmentTime = input.BookingDate;
                    data.CheckSate = 1;
                    data.ClientType = "";//体检类别
                    data.CostState = 1;
                    data.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                    data.CustomerType = 1;
                    data.EmailReport = 2;
                    data.ExamPlace = 1;
                    data.FamilyState = 1;
                    data.GuidanceSate = 1;
                    data.HaveBreakfast = 1;
                    data.InfoSource = 2;
                    
                    data.IsFree = false;
                    var tbmItemSuit = new TbmItemSuit();
                    if (!string.IsNullOrEmpty(input.ItemSuitBM))
                    {
                        tbmItemSuit = _bmItemSuit.FirstOrDefault(o => o.ItemSuitID == input.ItemSuitBM);
                        if (tbmItemSuit != null)
                        {
                            data.ItemSuitBMId = tbmItemSuit.Id;
                            data.ItemSuitName = tbmItemSuit.ItemSuitName;
                        }
                    }
                    // User user = _jlUsrs.Get(AbpSession.UserId.Value);
                    //data.KaidanYisheng = user.UserName;
                    data.MailingReport = 1;
                    data.MarriageStatus = input.MarriageStatus;
                    data.Message = 2;
                    data.PhysicalType = 1;
                    data.PrintSate = 1;
                    data.ReceiveSate = 1;
                    data.RegAge = input.Age;
                    data.RegisterState = 1;
                    data.ReplaceSate = 1;
                    data.ReportBySelf = 1;
                    data.RequestState = 1;
                    data.ReviewSate = 1;
                    data.SendToConfirm = 1;
                    data.SummLocked = 2;
                    data.SummSate = 1;
                    data.UrgentState = 1;
                    data.OrderNum = input.OrderId;
                    //data. 生成查询码
                    data.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();
                    //开单医生和挂号科室
                    if (input.NucleicAcidType.HasValue)
                    {
                        data.NucleicAcidType = input.NucleicAcidType.Value;
                    }
                    if (!string.IsNullOrEmpty(input.OrderUserId))
                    {
                        var userkd = _jlUsrs.GetAll().FirstOrDefault(o => o.EmployeeNum == input.OrderUserId);
                        if (userkd != null)
                        {
                            data.OrderUserId = userkd.Id;
                        }
                    }
                    //体检人基本信息
                    data.Customer = new QueryCustomerDto();
                    //身份证验证
                    var IDdata = VerificationHelper.GetByIdCard(input.IDCardNo);
                    if (IDdata != null)
                    {

                        data.Customer.Age = IDdata.Age;
                        data.Customer.Sex = (int)IDdata.Sex;
                        data.Customer.Birthday = IDdata.Birthday;
                        data.Customer.IDCardNo = input.IDCardNo;
                        //验证是否有该体检人档案
                        TjlCustomer tjlCustomer = _jlCustomer.FirstOrDefault(o => o.IDCardNo == input.IDCardNo);
                        if (tjlCustomer != null)
                        {
                            data.Customer.Id = tjlCustomer.Id;
                            data.Customer.ArchivesNum = tjlCustomer.ArchivesNum;

                        }
                        else
                        {
                            data.Customer.ArchivesNum = data.CustomerBM;
                        }
                    }
                    else
                    {
                        if (input.Birthday.HasValue)
                        {
                            data.Customer.Birthday = input.Birthday;
                            data.Customer.Age = input.Age;
                            var age = DateTime.Now.Year - input.Birthday.Value.Year;
                            if (input.Age != age)
                                data.Customer.Age = age;
                        }
                        else
                        {
                            data.Customer.Age = input.Age;
                        }
                        data.Customer.Sex = input.Sex;
                        data.Customer.ArchivesNum = data.CustomerBM;
                    }
                    //无档案号的情况赋值
                    data.Customer.AgeUnit = "岁";
                    data.Customer.CardNumber = "";
                    data.Customer.CustomerType = 1;
                    data.Customer.Duty = "";
                    data.Customer.Email = "";
                    data.Customer.GuoJi = "";
                    data.Customer.HospitalNum = "";
                    // data.Customer.IDCardNo = input.IDCardNo;
                    data.Customer.IDCardType = 1;//证件类型
                    data.Customer.MarriageStatus = input.MarriageStatus;
                    data.Customer.MedicalCard = "";
                    data.Customer.Mobile = input.Mobile;
                    data.Customer.Name = input.Name;
                    //生成简拼
                    var result = common.GetHansBrief(new ChineseDto { Hans = input.Name });
                    data.Customer.NameAB = result.Brief;//姓名简写;

                    data.Customer.Telephone = input.Telephone;
                    data.Customer.WbCode = "";//五笔
                    data.Customer.WorkNumber = "";
                    data.Customer.Qq = input.WeixinH;
                    data.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                    #region 照片
                    if (!string.IsNullOrEmpty(input.photofile))
                    {


                        try
                        {
                            var picDto = Uploading(input.photofile, "CusPhotoBm");

                            data.PhotoBmId = picDto.Id;
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                    }
                    #endregion
                    if (input.CustomerItemGroup.Count == 0 && data.ItemSuitBMId.HasValue)
                    {
                        var ret = _itemSuitAppSvr.QueryFulls(new SearchItemSuitDto() { Id = data.ItemSuitBMId.Value });
                        if (ret != null)
                        {
                            if (ret.Count > 0)
                            {

                                var sutGroups = ret.First().ItemSuitItemGroups.OrderBy(o => o.ItemGroup?.Department?.OrderNum).ThenBy(o => o.ItemGroup?.OrderNum).ToList();
                                if (data.CustomerItemGroup == null)
                                    data.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();


                                foreach (var item in sutGroups)
                                {
                                    if (item.ItemGroup == null)
                                    {
                                        continue;
                                    }
                                    var group = new TjlCustomerItemGroupDto();
                                    group.ItemGroupBM_Id = item.ItemGroupId;
                                    group.ItemPrice = item.ItemPrice == null ? 0.00M : item.ItemPrice.Value;
                                    group.PriceAfterDis = item.PriceAfterDis == null ? 0.00M : item.PriceAfterDis.Value;
                                    group.ItemGroupName = item.ItemGroup?.ItemGroupName;
                                    group.DiscountRate = item.Suitgrouprate == null ? 0.00M : item.Suitgrouprate.Value;
                                    group.GRmoney = item.PriceAfterDis == null ? 0.00M : item.PriceAfterDis.Value;

                                    group.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目
                                    group.ItemGroupOrder = item.ItemGroup?.OrderNum;
                                    group.PayerCat = (int)PayerCatType.NoCharge;
                                    group.TTmoney = 0.00M;
                                    group.ItemSuitId = data.ItemSuitBMId;
                                    group.ItemSuitName = data.ItemSuitName;
                                    group.SFType = Convert.ToInt32(item.ItemGroup?.ChartCode);

                                    TbmDepartment depart = _bmDepartment.Get(item.ItemGroup.DepartmentId);
                                    if (depart != null)
                                    {
                                        group.DepartmentId = depart.Id;
                                        group.DepartmentName = depart.Name;
                                        group.DepartmentOrder = depart.OrderNum;

                                    }
                                    group.CheckState = (int)ProjectIState.Not;
                                    group.RefundState = (int)PayerCatType.NotRefund;
                                    group.GuidanceSate = (int)PrintSate.NotToPrint;
                                    group.BarState = (int)PrintSate.NotToPrint;
                                    group.RequestState = (int)PrintSate.NotToPrint;
                                    group.RefundState = (int)PayerCatType.NotRefund;

                                    group.SummBackSate = (int)SummSate.NotAlwaysCheck;
                                    if (!data.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == group.ItemGroupBM_Id))
                                    {
                                        data.CustomerItemGroup.Add(group);
                                    }


                                }
                            }
                        }
                    }
                    else
                    {
                        //组合信息
                        foreach (NCusGroupsDto cusGroup in input.CustomerItemGroup)
                        {

                            TjlCustomerItemGroupDto tjlCustomerItemGroup = new TjlCustomerItemGroupDto();
                            tjlCustomerItemGroup.BarState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.CheckState = (int)ProjectIState.Not;

                            //传来的是bm  ItemGroupBM_Id
                            TbmItemGroup tbmItemGroup = _bmItemGroup.FirstOrDefault(o => o.ItemGroupBM == cusGroup.ItemGroupBM);
                            if (tbmItemGroup == null)
                            {
                                outResult.Code = 0;
                                outResult.ErrInfo = "未找到项目" + cusGroup.ItemGroupBM + "的项目ID，请核实";

                                return outResult;
                            }
                            tjlCustomerItemGroup.DepartmentId = tbmItemGroup.DepartmentId;
                            TbmDepartment tbmDepartment = _bmDepartment.Get(tbmItemGroup.DepartmentId);
                            tjlCustomerItemGroup.DepartmentName = tbmDepartment.Name;
                            tjlCustomerItemGroup.DepartmentOrder = tbmDepartment.OrderNum;
                            tjlCustomerItemGroup.DiscountRate = cusGroup.DiscountRate;
                            tjlCustomerItemGroup.DrawSate = 1;
                            tjlCustomerItemGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
                            tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis;
                            tjlCustomerItemGroup.GuidanceSate = (int)PrintSate.NotToPrint;
                            if (!string.IsNullOrEmpty(cusGroup.ItemSuitBM))
                            {
                                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目
                            }
                            else
                            {
                                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Add;//是否加减项 正常项目
                            }
                            tjlCustomerItemGroup.ItemGroupBM_Id = tbmItemGroup.Id;
                            tjlCustomerItemGroup.ItemGroupName = tbmItemGroup.ItemGroupName;
                            tjlCustomerItemGroup.ItemGroupOrder = tbmItemGroup.OrderNum;
                            tjlCustomerItemGroup.ItemGroupCodeBM = tbmItemGroup.ItemGroupBM;
                            tjlCustomerItemGroup.ItemPrice = tbmItemGroup.Price.Value;

                            tjlCustomerItemGroup.ItemSuitName = data.ItemSuitName;
                            tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                            tjlCustomerItemGroup.PriceAfterDis = cusGroup.PriceAfterDis;
                            tjlCustomerItemGroup.RefundState = (int)PayerCatType.NotRefund;
                            tjlCustomerItemGroup.RequestState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.SFType = Convert.ToInt32(tbmItemGroup.ChartCode);
                            tjlCustomerItemGroup.SummBackSate = (int)SummSate.NotAlwaysCheck;
                            tjlCustomerItemGroup.SuspendState = 1;
                            tjlCustomerItemGroup.TTmoney = 0;
                            if (!string.IsNullOrEmpty(cusGroup.ItemSuitBM))
                            {
                                if (cusGroup.ItemSuitBM == input.ItemSuitBM)
                                {
                                    tjlCustomerItemGroup.ItemSuitId = tbmItemSuit?.Id;
                                }
                                else
                                {
                                    TjlCusAddPackItems.Add(tjlCustomerItemGroup);
                                }
                            }
                            data.CustomerItemGroup.Add(tjlCustomerItemGroup);
                        }
                    }
                    QueryCustomerRegDto curCustomRegInfo = _customerAppService.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
                    customerregid = curCustomRegInfo.Id;
                    outResult.CustomerBM = curCustomRegInfo.CustomerBM;
                    outResult.OrderId = input.OrderId;
                   
                    #region 支付,在线已支付
                    if (input.isWxPay !=null && input.isWxPay=="1")
                    {
                        CurrentUnitOfWork.SaveChanges();
                        InCusPayDto inCusPayDto = new InCusPayDto();
                        inCusPayDto.CustomerBM= curCustomRegInfo.CustomerBM;
                        inCusPayDto.PayTime = System.DateTime.Now;
                        inCusPayDto.PayMoney = curCustomRegInfo.CustomerItemGroup.
                            Where(p => p.PayerCat == (int)PayerCatType.NoCharge).Sum(p => p.PriceAfterDis);
                        inCusPayDto.PayGroups = curCustomRegInfo.CustomerItemGroup.Select(p => new PayGroupDto
                        {
                            ItemGroupBM = p.ItemGroupCodeBM,
                            PriceAfterDis = p.PriceAfterDis
                        }).ToList();

                        Payment(inCusPayDto);
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    #region 单位预约
                    if (string.IsNullOrEmpty(input.CustomerBM))
                    {
                        outResult.Code = 0;
                        outResult.ErrInfo = "团体人员预约，体检号不能为空！";
                        return outResult;
                    }
                    TjlCustomerReg tjlCustomerReg = _jlCustomerReg.FirstOrDefault(o => o.CustomerBM == input.CustomerBM);
                    if (tjlCustomerReg == null)
                    {
                        outResult.Code = 0;
                        outResult.ErrInfo = "体检系统中无此人信息，请核实！";
                        return outResult;
                    }
                    if (!tjlCustomerReg.ClientInfoId.HasValue || !tjlCustomerReg.ClientTeamInfoId.HasValue)
                    {
                        outResult.Code = 0;
                        outResult.ErrInfo = "该体检人不属于任何单位，或分组，请核实！";
                        return outResult;
                    }
                    QueryCustomerRegDto data = new QueryCustomerRegDto();
                    data = tjlCustomerReg.MapTo<QueryCustomerRegDto>();
                    data.BookingDate = input.BookingDate;
                    data.OrderNum = input.OrderId;
                    var tbmItemSuit = new TbmItemSuit();
                    if (!string.IsNullOrEmpty(input.ItemSuitBM))
                    {
                        tbmItemSuit = _bmItemSuit.FirstOrDefault(o => o.ItemSuitID == input.ItemSuitBM);
                        if (tbmItemSuit != null)
                        {
                            data.ItemSuitBMId = tbmItemSuit.Id;
                            data.ItemSuitName = tbmItemSuit.ItemSuitName;
                        }
                    }
                    data.MarriageStatus = input.MarriageStatus;
                    data.RegAge = input.Age;
                    //data.
                    data.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();
                    //开单医生和挂号科室
                    if (input.NucleicAcidType.HasValue)
                    {
                        data.NucleicAcidType = input.NucleicAcidType.Value;
                    }
                    if (!string.IsNullOrEmpty(input.OrderUserId))
                    {
                        var userkd = _jlUsrs.GetAll().FirstOrDefault(o => o.EmployeeNum == input.OrderUserId);
                        if (userkd != null)
                        {
                            data.OrderUserId = userkd.Id;
                        }
                    }
                    //体检人基本信息
                    data.Customer = new QueryCustomerDto();
                    //身份证验证
                    var IDdata = VerificationHelper.GetByIdCard(input.IDCardNo);
                    if (IDdata != null)
                    {
                        data.Customer.Age = IDdata.Age;
                        data.Customer.Sex = (int)IDdata.Sex;
                        data.Customer.Birthday = IDdata.Birthday;
                        //验证是否有该体检人档案                   
                    }
                    else
                    {
                        if (input.Birthday.HasValue)
                        {
                            data.Customer.Birthday = input.Birthday;
                            data.Customer.Age = input.Age;
                            var age = DateTime.Now.Year - input.Birthday.Value.Year;
                            if (input.Age != age)
                                data.Customer.Age = age;
                        }
                        else
                        {
                            data.Customer.Age = input.Age;
                        }
                        data.Customer.Sex = input.Sex;
                    }
                    data.Customer.IDCardNo = input.IDCardNo;
                    data.Customer.IDCardType = 1;//证件类型
                    data.Customer.MarriageStatus = input.MarriageStatus;
                    data.Customer.Mobile = input.Mobile;
                    data.Customer.Name = input.Name;
                    var result = common.GetHansBrief(new ChineseDto { Hans = input.Name });
                    data.Customer.NameAB = result.Brief;//姓名简写;
                    data.Customer.Telephone = input.Telephone;
                    // data.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                    TjlClientTeamInfo tjlClientTeamInfo = _jlClientTeamInfo.Get(data.ClientTeamInfo_Id.Value);
                    decimal oldMoney = 0;
                    decimal cjMoney = 0;
                    //判断 个人付款Gr  团队付款是
                    if (tjlClientTeamInfo.CostType == (int)PayerCatType.FixedAmount && tjlClientTeamInfo.TeamMoney.HasValue)
                    {
                        oldMoney = tjlClientTeamInfo.TeamMoney.Value;
                        decimal cusOldMoney = data.CustomerItemGroup.Sum(o => o.PriceAfterDis);
                        cjMoney = oldMoney - cusOldMoney;
                    }
                    //判断组合信息 如果没有传 只修改预约时间或者个人信息mmm
                    //组合信息
                    foreach (NCusGroupsDto cusGroup in input.CustomerItemGroup)
                    {
                        if (!data.CustomerItemGroup.Any(o => o.ItemGroupCodeBM == cusGroup.ItemGroupBM))
                        {
                            TjlCustomerItemGroupDto tjlCustomerItemGroup = new TjlCustomerItemGroupDto();
                            tjlCustomerItemGroup.BarState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.CheckState = (int)ProjectIState.Not;
                            TbmItemGroup tbmItemGroup = _bmItemGroup.FirstOrDefault(o => o.ItemGroupBM == cusGroup.ItemGroupBM);
                            if (tbmItemGroup == null)
                            {
                                outResult.Code = 0;
                                outResult.ErrInfo = "未找到项目" + cusGroup.ItemGroupBM + "的项目ID，请核实";
                                return outResult;
                            }
                            tjlCustomerItemGroup.DepartmentId = tbmItemGroup.DepartmentId;
                            TbmDepartment tbmDepartment = _bmDepartment.Get(tbmItemGroup.DepartmentId);
                            tjlCustomerItemGroup.DepartmentName = tbmDepartment.Name;
                            tjlCustomerItemGroup.DepartmentOrder = tbmDepartment.OrderNum;
                            tjlCustomerItemGroup.DiscountRate = cusGroup.DiscountRate;
                            tjlCustomerItemGroup.DrawSate = 1;
                            tjlCustomerItemGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
                            tjlCustomerItemGroup.GuidanceSate = (int)PrintSate.NotToPrint;
                            if (!string.IsNullOrEmpty(cusGroup.ItemSuitBM))
                            {
                                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目
                            }
                            else
                            {
                                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Add;//是否加减项 正常项目
                            }
                            tjlCustomerItemGroup.ItemGroupBM_Id = tbmItemGroup.Id;
                            tjlCustomerItemGroup.ItemGroupName = tbmItemGroup.ItemGroupName;
                            tjlCustomerItemGroup.ItemGroupOrder = tbmItemGroup.OrderNum;
                            //tjlCustomerItemGroup.ItemSuitId = cusGroup.ItemSuitId;                          
                            tjlCustomerItemGroup.ItemSuitName = data.ItemSuitName;

                            tjlCustomerItemGroup.RefundState = (int)PayerCatType.NotRefund;
                            tjlCustomerItemGroup.RequestState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.SFType = Convert.ToInt32(tbmItemGroup.ChartCode);
                            tjlCustomerItemGroup.SummBackSate = (int)SummSate.NotAlwaysCheck;
                            tjlCustomerItemGroup.SuspendState = 1;
                            tjlCustomerItemGroup.ItemPrice = tbmItemGroup.Price.Value;
                            tjlCustomerItemGroup.PriceAfterDis = cusGroup.PriceAfterDis;
                            //单位支付
                            if (tjlClientTeamInfo.JxType == (int)PayerCatType.ClientCharge)
                            {
                                tjlCustomerItemGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                tjlCustomerItemGroup.GRmoney = 0;
                                tjlCustomerItemGroup.TTmoney = cusGroup.PriceAfterDis;
                            }
                            //个人支付
                            else if (tjlClientTeamInfo.JxType == (int)PayerCatType.PersonalCharge)
                            {
                                tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                                tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis;
                                tjlCustomerItemGroup.TTmoney = 0;
                            }
                            //固定金额
                            if (tjlClientTeamInfo.CostType == (int)PayerCatType.FixedAmount)
                            {
                                if (cjMoney > 0)
                                {
                                    if (cusGroup.PriceAfterDis > cjMoney)
                                    {
                                        tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                                        tjlCustomerItemGroup.TTmoney = cjMoney;
                                        tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis - cjMoney;
                                        cjMoney = 0;
                                    }
                                    else
                                    {
                                        tjlCustomerItemGroup.PayerCat = (int)PayerCatType.ClientCharge;
                                        tjlCustomerItemGroup.TTmoney = cusGroup.PriceAfterDis;
                                        tjlCustomerItemGroup.GRmoney = 0;
                                        cjMoney = cjMoney - cusGroup.PriceAfterDis;
                                    }
                                }
                                else
                                {
                                    tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                                    tjlCustomerItemGroup.GRmoney = cusGroup.PriceAfterDis;
                                    tjlCustomerItemGroup.TTmoney = 0;

                                }

                            }
                            if (!string.IsNullOrEmpty(cusGroup.ItemSuitBM))
                            {
                                if (cusGroup.ItemSuitBM == input.ItemSuitBM)
                                {
                                    tjlCustomerItemGroup.ItemSuitId = tbmItemSuit?.Id;
                                }
                                else
                                {
                                    TjlCusAddPackItems.Add(tjlCustomerItemGroup);
                                }
                            }
                            data.CustomerItemGroup.Add(tjlCustomerItemGroup);
                        }
                    }
                    QueryCustomerRegDto curCustomRegInfo = _customerAppService.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
                    customerregid = curCustomRegInfo.Id;
                    clientregID = curCustomRegInfo.ClientRegId.Value;
                    outResult.CustomerBM = curCustomRegInfo.CustomerBM;
                    outResult.OrderId = input.OrderId;
                    #region 支付,在先已支付
                    if (input.isWxPay != null && input.isWxPay == "1")
                    {
                        CurrentUnitOfWork.SaveChanges();
                        InCusPayDto inCusPayDto = new InCusPayDto();
                        inCusPayDto.CustomerBM = curCustomRegInfo.CustomerBM;
                        inCusPayDto.PayTime = System.DateTime.Now;
                        inCusPayDto.PayMoney = curCustomRegInfo.CustomerItemGroup.
                            Where(p => p.PayerCat == (int)PayerCatType.NoCharge).Sum(p => p.GRmoney);
                        inCusPayDto.PayGroups = curCustomRegInfo.CustomerItemGroup.Where(p => p.PayerCat == (int)PayerCatType.NoCharge).
                            Select(p => new PayGroupDto
                        {
                            ItemGroupBM = p.ItemGroupCodeBM,
                            PriceAfterDis = p.GRmoney
                            }).ToList();
                        Payment(inCusPayDto);

                    }
                    #endregion

                    #endregion
                }
                //修改预约状态
                var cusReg = _jlCustomerReg.GetAll().FirstOrDefault(o => o.CustomerBM == outResult.CustomerBM);
                if (cusReg != null)
                {
                    cusReg.BookingStatus = 1;
                    _jlCustomerReg.Update(cusReg);
                }
                #region 保存问卷及加项包信息            
                if (input.Questions != null)
                {
                    foreach (InCusQusTionDto cusqus in input.Questions)
                    {
                        TjlCustomerQuestion tjlCusQuestion = new TjlCustomerQuestion();
                        tjlCusQuestion.Id = Guid.NewGuid();
                        tjlCusQuestion.OutQuestionID = cusqus.OutQuestionID;

                        tjlCusQuestion.CustomerRegId = customerregid;
                        TbmOneAddXQuestionnaire tbmOneAddXQuestionnaire = _OneAddXQuestionnaire.FirstOrDefault(o => o.ExternalCode == cusqus.OutQuestionID);
                        if (tbmOneAddXQuestionnaire == null)
                        {
                            outResult.Code = 0;
                            outResult.ErrInfo = "问卷ID" + cusqus.OutQuestionID + "没有找到，请核实";
                            return outResult;
                        }
                        tjlCusQuestion.OneAddXQuestionnaireid = tbmOneAddXQuestionnaire.Id;
                        if (clientregID != Guid.Empty)
                        {
                            tjlCusQuestion.ClientRegId = clientregID;
                        }
                        tjlCusQuestion.QuestionName = tbmOneAddXQuestionnaire.Name;
                        tjlCusQuestion.QuestionType = tbmOneAddXQuestionnaire.Category;
                        _jlCusQuestion.Insert(tjlCusQuestion);
                    }
                }
                if (input.TjlCusAddpackages != null)
                {
                    foreach (NCusAddPacksDto cuspack in input.TjlCusAddpackages)
                    {
                        TjlCustomerAddPackage tjlCusAddpackages = new TjlCustomerAddPackage();
                        tjlCusAddpackages.CustomerRegId = customerregid;
                        if (clientregID != Guid.Empty)
                        {
                            tjlCusAddpackages.ClientRegId = clientregID;
                        }
                        tjlCusAddpackages.Id = Guid.NewGuid();
                        var itemsuit = _bmItemSuit.FirstOrDefault(o => o.ItemSuitID == cuspack.ItemSuitBM);
                        if (itemsuit != null)
                        {
                            tjlCusAddpackages.ItemSuitID = itemsuit.Id;
                            TjlCustomerAddPackage tjlCusAddpackagesNew = _jlCusAddpackages.Insert(tjlCusAddpackages);
                            var cuspacGroups = TjlCusAddPackItems.Where(o => o.ItemSuitId == itemsuit.Id).ToList();
                            foreach (var cuspackItem in cuspacGroups)
                            {

                                TjlCustomerAddPackageItem tjlCusAddPackItems = new TjlCustomerAddPackageItem();
                                tjlCusAddPackItems.CheckState = 1;
                                if (clientregID != Guid.Empty)
                                {
                                    tjlCusAddPackItems.ClientRegId = clientregID;
                                }
                                tjlCusAddPackItems.CustomerRegId = customerregid;
                                tjlCusAddPackItems.Id = Guid.NewGuid();
                                tjlCusAddPackItems.ItemGroupID = cuspackItem.ItemGroupBM_Id;
                                tjlCusAddPackItems.ItemGroupName = cuspackItem.ItemGroupName;
                                tjlCusAddPackItems.ItemSuitID = itemsuit.Id;
                                tjlCusAddPackItems.TjlCusAddpackagesID = tjlCusAddpackagesNew.Id;
                                tjlCusAddPackItems.ItemPrice = cuspackItem.ItemPrice;
                                tjlCusAddPackItems.PriceAfterDis = cuspackItem.PriceAfterDis;
                                tjlCusAddPackItems.Suitgrouprate = cuspackItem.DiscountRate;
                                _jlCusAddPackItems.Insert(tjlCusAddPackItems);

                            }
                        }


                    }
                }

                #endregion
                outResult.Code = 1;
             
                return outResult;
            }
            catch (EntityNotFoundException e)
            {
                throw new IdNotFoundExecption(e.Message, "预约异常！");
            }
        }
        /// <summary>
        /// 保存优康云问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        public OutResult SaveYKYQuestion(WJMainDto input)
        {
            OutResult outResult = new OutResult();
            var questionBomlistDto  = input.Answerjson;     
            string  checkNo=   input.checkNo ;
            //questionBomlistDto转化为对象AnswerjsonDto 

            List<AnswerjsonDto> questionBomlist = input.Answerjson;

            if (questionBomlist == null)
            {
                outResult.Code = 0;
                outResult.ErrInfo =  "没有问卷题目详情";
                return outResult;

            }   
            string idcard = "";
            var jbxx= questionBomlist.FirstOrDefault(p=>p.groupname.Contains("基本信息"));
            if(jbxx == null)
            {
                outResult.Code = 0;
                outResult.ErrInfo = "没有找到基本信息";
                return outResult;
            }
            else
            {
                 var idcardinfo = jbxx.questionlist.FirstOrDefault(p => p.questionname.Contains("身份证号"));
                if( idcardinfo!=null)
                {
                    idcard = idcardinfo.answer;
                }
            }
            //根据问卷IP删除问卷
            if (!string.IsNullOrEmpty(checkNo))
            {
                var que = _TjlCusQuestion.GetAll().FirstOrDefault(p => p.checkNo == checkNo);
                if (que != null && que.CustomerReg.SummSate != (int)SummSate.NotAlwaysCheck)
                {
                    outResult.Code = 0;
                    outResult.ErrInfo = checkNo + "，" + que.CustomerReg.Customer.Name + ",已审核，不能修改问卷";
                    return outResult;
                }
                if (que != null)
                {
                    //删除旧问卷
                    _TjlQuestiontem.GetAll().Where(p => p.CusQuestionId == que.Id).Delete();
                    _TjlQuestionBom.GetAll().Where(p => p.CusQuestionId == que.Id).Delete();
                    _TjlCusQuestion.Delete(que);
               
                 
                }
            }        
           
            var tjlque = new TjlCusQuestion();
            var cusreg = new TjlCustomerReg();
            if (!string.IsNullOrEmpty(checkNo))
            {
                 cusreg = _jlCustomerReg.GetAll().FirstOrDefault(p => p.OrderNum == checkNo);
            }
            if (!string.IsNullOrEmpty(idcard))
            {
               
                    //最新的登记信息
                cusreg = _jlCustomerReg.GetAll().Where(p => p.Customer.IDCardNo == idcard && p.SummSate!=4).OrderByDescending(p=>p.LoginDate).FirstOrDefault();
               
            }
            if (cusreg == null)
            {
                outResult.Code = -1;
                outResult.ErrInfo = input.checkNo + "，"     + ",没有该体检人";
                return outResult;
            }
            else if (cusreg != null && (cusreg.SummSate == (int)SummSate.Audited || cusreg.SummSate == (int)SummSate.HasInitialReview))
            {
                outResult.Code = 0;
                outResult.ErrInfo = input.checkNo + "，"   + ",已总检，不能修改问卷";
                return outResult;
            }
            else  
            {

                //删除旧问卷
                _TjlQuestiontem.GetAll().Where(p => p.CustomerRegId == cusreg.Id).Delete();
                _TjlQuestionBom.GetAll().Where(p => p.CustomerRegId == cusreg.Id).Delete();
               
                _TjlCusQuestion.GetAll().Where(p => p.CustomerRegId == cusreg.Id).Delete();
               
                 
                
            }
            //tjlque.CustomerRegId = cusreg.Id;
            //tjlque.questionBomList = null;
            //tjlque.Id = Guid.NewGuid();            
            // tjlque.Type = 1;
            tjlque = new TjlCusQuestion
            {
                checkNo = cusreg.CustomerBM,
                CustomerRegId = cusreg.Id,
                Id = Guid.NewGuid(),
                Type = 1,
                evaluateResult = "",
                hasReport = 0,
                lastTime = DateTime.Now.ToString(),
                personName = cusreg.Customer.Name,
                mobile = cusreg.Customer.Mobile,
                orderNo = checkNo,
                tempPersonCheckOrderno = checkNo


            };
            _TjlCusQuestion.Insert(tjlque);
            int orderNum = 1;
            foreach (var questionBom in questionBomlist)
            {
                foreach(var item in questionBom.questionlist)
                {
                    var questionbomEnty = new TjlQuestionBom();
                    questionbomEnty.itemList = null;

                    questionbomEnty.Id = Guid.NewGuid();
                    questionbomEnty.CustomerRegId = cusreg.Id;
                    questionbomEnty.CusQuestionId = tjlque.Id;
                    questionbomEnty.answerContent = item.answer;
                    questionbomEnty.bomItemName= item.questionname;
                    questionbomEnty.OrderNum = orderNum;
                    questionbomEnty.bomItemType = 1;
                    questionbomEnty.Title= questionBom.groupname;
                    _TjlQuestionBom.Insert(questionbomEnty);
                    orderNum = orderNum + 1;

                }

            }
            outResult.Code = 1;
            outResult.CustomerBM = cusreg.CustomerBM;
            outResult.ErrInfo = "保存问卷成功！";

            return outResult;
        }
        /// <summary>
        /// 保存微信问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        public OutResult SaveCusQuestion(TjlCusQuestionDto input)
        {
            OutResult outResult = new OutResult();
            var questionBomlist = input.questionBomList;
            if (questionBomlist == null)
            {
                outResult.Code = 0;
                outResult.ErrInfo = input.checkNo + "，" + input.personName + ",没有问卷题目详情";
                return outResult;

            }
            //根据问卷IP删除问卷
            if (!string.IsNullOrEmpty(input.checkNo))
            {
                var que = _TjlCusQuestion.GetAll().FirstOrDefault(p => p.checkNo == input.checkNo);
                if (que != null && que.CustomerReg.SummSate != (int)SummSate.NotAlwaysCheck)
                {
                    outResult.Code = 0;
                    outResult.ErrInfo = input.checkNo + "，" + input.personName + ",已审核，不能修改问卷";
                    return outResult;
                }
                if (que != null)
                {
                    //删除旧问卷
                    _TjlQuestiontem.GetAll().Where(p => p.CusQuestionId == que.Id).Delete();
                    _TjlQuestionBom.GetAll().Where(p => p.CusQuestionId == que.Id).Delete();
                    _TjlCusQuestion.Delete(que);


                }
            }

            var tjlque = ObjectMapper.Map<TjlCusQuestion>(input);
            var cusreg = new TjlCustomerReg();
            if (!string.IsNullOrEmpty(input.orderNo))
            {
                cusreg = _jlCustomerReg.GetAll().FirstOrDefault(p => p.OrderNum == input.orderNo);
            }
            if (string.IsNullOrEmpty(input.orderNo) || (cusreg == null && !string.IsNullOrEmpty(input.IDCardNo)))
            {

                //最新的登记信息
                cusreg = _jlCustomerReg.GetAll().Where(p => p.Customer.IDCardNo == input.IDCardNo && p.SummSate != 4).OrderByDescending(p => p.LoginDate).FirstOrDefault();

            }
            if (cusreg == null)
            {
                outResult.Code = -1;
                outResult.ErrInfo = input.checkNo + "，" + input.personName + ",没有该体检人";
                return outResult;
            }
            else if (cusreg != null && (cusreg.SummSate == (int)SummSate.Audited || cusreg.SummSate == (int)SummSate.HasInitialReview))
            {
                outResult.Code = 0;
                outResult.ErrInfo = input.checkNo + "，" + input.personName + ",已总检，不能修改问卷";
                return outResult;
            }
            else
            {

                //删除旧问卷
                _TjlQuestiontem.GetAll().Where(p => p.CustomerRegId == cusreg.Id).Delete();
                _TjlQuestionBom.GetAll().Where(p => p.CustomerRegId == cusreg.Id).Delete();

                _TjlCusQuestion.GetAll().Where(p => p.CustomerRegId == cusreg.Id).Delete();



            }
            tjlque.CustomerRegId = cusreg.Id;
            tjlque.questionBomList = null;
            tjlque.Id = Guid.NewGuid();
            if (tjlque.hasReport.HasValue)
            {
                tjlque.Type = 2;

            }
            else
            {
                tjlque.Type = 1;
            }
            _TjlCusQuestion.Insert(tjlque);
            foreach (var questionBom in questionBomlist)
            {

                var questionbomEnty = ObjectMapper.Map<TjlQuestionBom>(questionBom);
                questionbomEnty.itemList = null;

                questionbomEnty.Id = Guid.NewGuid();
                questionbomEnty.CustomerRegId = cusreg.Id;
                questionbomEnty.CusQuestionId = tjlque.Id;
                _TjlQuestionBom.Insert(questionbomEnty);
                var itemlist = questionBom.itemList;
                if (itemlist != null)
                {
                    foreach (var item in itemlist)
                    {
                        var itemEnty = ObjectMapper.Map<TjlQuestiontem>(item);
                        itemEnty.CustomerRegId = cusreg.Id;
                        itemEnty.CusQuestionId = tjlque.Id;
                        itemEnty.QuestionBomId = questionbomEnty.Id;

                        _TjlQuestiontem.Insert(itemEnty);

                    }
                }
            }
            outResult.Code = 1;
            outResult.CustomerBM = input.checkNo;
            outResult.ErrInfo = "保存问卷成功！";

            return outResult;
        }


        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>       
        public ResultDto DelCusReg(SearchRegDateDto dto)
        {
            ResultDto resuit = new ResultDto();
            try
            {

                var cusreginfo = _jlCustomerReg.FirstOrDefault(o => o.CustomerBM == dto.CustomerBM);
                if (cusreginfo == null)
                {
                    //增加订单号
                    cusreginfo = _jlCustomerReg.GetAll().FirstOrDefault(p => p.OrderNum == dto.CustomerBM);
                    if (cusreginfo == null)
                    {
                        resuit.code = 0;
                        resuit.Mess = dto.CustomerBM +"没有该体检人信息";
                        return resuit;
                    }
                }
                //EntityDto<Guid> cusName = new EntityDto<Guid>();
                //cusName.Id = cusreginfo.Id;
                //CustomerRegCostDto customerReg = _ChargeAppService.GetsfState(cusName);
                //if (customerReg.CostState != (int)PayerCatType.NoCharge)
                //{
                   
                //    resuit.code = 0;
                //    resuit.Mess = "该体检人已收费不能删除！";
                //    return resuit;
                //}               
                if (cusreginfo.CheckSate != (int)ExaminationState.Alr)
                {
                   
                    resuit.code = 0;
                    resuit.Mess = "已开始体检，无法删除！";
                    return resuit;
                }


                if (cusreginfo.RegisterState == (int)RegisterState.Yes)
                {
                    resuit.code = 0;
                    resuit.Mess = "该体检人已登记，不能取消预约";
                    return resuit;
                }

                cusreginfo.BookingStatus = -1;
                var isup = _jlCustomerReg.Update(cusreginfo);
                //个人删除
                if (!cusreginfo.ClientRegId.HasValue)
                {
                    // _jlCustomerReg.Delete(isup);

                    
                    if (_jlCustomerReg.GetAll().Any(o => o.Id == cusreginfo.Id))
                    {
                        //体检人项目组合
                        var customerItemGroups = _jlCustomerItemGroup.GetAll().Where(o => o.CustomerRegBMId == cusreginfo.Id);
                        if (customerItemGroups != null)
                            foreach (var cusItemGroup in customerItemGroups)
                                _jlCustomerItemGroup.Delete(cusItemGroup);

                        //个人应收已收
                        var McusPayMoneys = _jlMcusPayMoney.GetAll().Where(o => o.Id == cusreginfo.Id);

                        if (McusPayMoneys != null && McusPayMoneys.Count() > 0)
                            foreach (var mpm in McusPayMoneys)
                                _jlMcusPayMoney.Delete(mpm);

                        //体检人检查项目结果表
                        var CustomerRegItems = _jlCustomerRegItem.GetAll().Where(o => o.CustomerRegId == cusreginfo.Id);
                        if (CustomerRegItems != null)
                            foreach (var cri in CustomerRegItems)
                                _jlCustomerRegItem.Delete(cri);
                        _jlCustomerReg.DeleteAsync(cusreginfo);
                    }
                }
                resuit.code = 1;
                resuit.Mess = "取消预约成功！";

                return resuit;

            }
            catch (EntityNotFoundException e)
            {

                resuit.code = 0;
                resuit.Mess = e.Message;
                return resuit;

            }
        }
        #endregion
        #region 微信支付
        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>     
        public OutResult Payment(InCusPayDto dto)
        {
            OutResult result = new OutResult();

            var cusreg = _jlCustomerReg.GetAll().FirstOrDefault(o => o.CustomerBM == dto.CustomerBM);
            if (cusreg == null)
            {
                result.Code = 0;
                result.ErrInfo = "没有该体检人，请核实！";
                return result;

            }
            var summoney = dto.PayGroups.Sum(o => o.PriceAfterDis);
            if (summoney != dto.PayMoney)
            {
                result.Code = 0;
                result.ErrInfo = "支付金额，和明细金额不符";
                return result;
            }
            CreateReceiptInfoDto input = new CreateReceiptInfoDto();
            decimal ReturnMoney = 0;     //抹零金额    
            input.Discontmoney = 0;

            input.Actualmoney = dto.PayMoney.Value;

            input.ChargeDate = dto.PayTime.Value;
            input.ChargeState = (int)InvoiceStatus.NormalCharge;

            input.CustomerRegid = cusreg.Id;//关联已有对象                

            input.DiscontReason = "";
            input.Discount = 1;
            input.ReceiptSate = (int)InvoiceStatus.Valid;
            input.Remarks = "微信线上支付";
            input.SettlementSate = (int)ReceiptState.UnSettled;
            input.Shouldmoney = dto.PayMoney.Value;
            input.Summoney = dto.PayMoney.Value;
            int tjtype = 2;
            input.TJType = tjtype;
            input.Userid = AbpSession.UserId.Value;

            //支付方式
            List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
            var payment = _tbmMChargeType.FirstOrDefault(o => o.ChargeName.Contains("微信"));
            //没有微信支付则新建
            if (payment == null)
            {
                payment = new TbmMChargeType();
                payment.Id = Guid.NewGuid();
                payment.AccountingState = 2;
                payment.ChargeApply = 3;
                payment.ChargeCode = 100;
                payment.ChargeName = "微信支付";
                payment.HelpChar = "WX";
                payment.OrderNum = 100;
                payment.PrintName = 3;
                payment.Remarks = "";
                payment = _tbmMChargeType.Insert(payment);

            }
            CreatePaymentDto CreatePayment = new CreatePaymentDto();
            CreatePayment.Actualmoney = dto.PayMoney.Value;
            CreatePayment.CardNum = "";//卡号
            CreatePayment.Discount = 1;
            CreatePayment.MChargeTypeId = payment.Id;
            CreatePayment.Shouldmoney = dto.PayMoney.Value;
            CreatePayment.MChargeTypename = payment.ChargeName;
            CreatePayments.Add(CreatePayment);

            input.MPaymentr = CreatePayments;
            //未收费项目集合
            List<CreateMReceiptInfoDetailedDto> CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();

            var cusPerGroups = dto.PayGroups;
            //var ChargeGroups = cusPerGroups?.GroupBy(r => r.SFType);
            foreach (var ChargeGroup in cusPerGroups)
            {
                var itemgroup = _jlCustomerItemGroup.FirstOrDefault(o => o.ItemGroupBM.ItemGroupBM == ChargeGroup.ItemGroupBM && o.CustomerRegBMId == cusreg.Id);
                // var itemgroup = _bmItemGroup.FirstOrDefault(o=>o.ItemGroupBM== ChargeGroup.ItemGroupBM);
                if (itemgroup == null)
                {
                    result.Code = 0;
                    result.ErrInfo = "没找到该项目组合：" + ChargeGroup.ItemGroupBM;
                    return result;
                }
                CreateMReceiptInfoDetailedDto CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                CreateMReceiptInfoDetailed.GroupsMoney = itemgroup.ItemPrice;
                CreateMReceiptInfoDetailed.GroupsDiscountMoney = ChargeGroup.PriceAfterDis;
                if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                {
                    CreateMReceiptInfoDetailed.Discount = CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                }
                else
                {
                    CreateMReceiptInfoDetailed.Discount = 1;
                }
                CreateMReceiptInfoDetailed.ReceiptTypeName = itemgroup.ItemGroupBM.ChartName;
                //组合
                CreateMReceiptInfoDetailed.ItemGroupId = itemgroup.Id;
                CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
            }

            input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
            //保存收费记录
            //  Guid receiptInfoDtoID = ChargeAppService.InsertReceiptInfoDto(input);
            OutErrDto outErrDto = _ChargeAppService.InsertReceiptState(input);

            result.Code = int.Parse(outErrDto.code);
            result.ErrInfo = outErrDto.err;
            if (result.Code == 1)
            {
                result.ErrInfo = "收费成功！";
                result.ReceiptId = outErrDto.Id;
            }
            result.CustomerBM = cusreg.CustomerBM;
            result.OrderId = cusreg.OrderNum;
            return result;

        }

        /// <summary>
        /// 微信取消支付
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>     
        public OutResult CancelPayment(ReceiptIdDto ReceiptId)
        {
            OutResult outResult = new OutResult();

            var dto = _TjlMReceiptInfo.FirstOrDefault(o => o.Id == ReceiptId.ReceiptId);
            if (dto == null)
            {
                outResult.Code = 0;
                outResult.ErrInfo = "没有该退费记录！";
                return outResult;
            }
            //总检完成不可作废
            if (dto.CustomerReg?.SummSate >= 3)
            {

                outResult.Code = 0;
                outResult.ErrInfo = "该体检人已总检，不能作废！";
                return outResult;
            }
                EntityDto<Guid> receiptID = new EntityDto<Guid>();
                receiptID.Id = dto.Id;
                bool ischek = _ChargeAppService.SFGroupCheck(receiptID);
            if (ischek == true)
            {


                outResult.Code = 0;
                outResult.ErrInfo = "项目已检查，不能作废！";
                return outResult;
            }

            MReceiptInfoDto mReceiptInfoDto = _ChargeAppService.GetInvalid(receiptID);
                int receiptstate = (int)InvoiceStatus.Cancellation;
            if (mReceiptInfoDto != null && mReceiptInfoDto.ReceiptSate == receiptstate)
            {


                outResult.Code = 0;
                outResult.ErrInfo = "此记录已作废，不能重复作废！";
                return outResult;
            }
                GuIdDto input = new GuIdDto();
                input.Id = dto.Id;
                MReceiptInfoDto MReceiptInfo = _ChargeAppService.InsertInvalidReceiptInfoDto(input);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                if (dto.CustomerReg != null)
                {
                    createOpLogDto.LogBM = dto.CustomerReg.CustomerBM;
                    createOpLogDto.LogName = dto.CustomerReg.Customer.Name;
                }
                else if (dto.ClientReg != null)
                {
                    createOpLogDto.LogBM = dto.ClientReg.ClientRegBM;
                    createOpLogDto.LogName = dto.ClientReg.ClientInfo.ClientName;
                }

                createOpLogDto.LogText = "线上作废收费：" + dto.Actualmoney;
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ResId;
            common.SaveOpLog(createOpLogDto);
            outResult.Code = 1;
            outResult.ReceiptId = MReceiptInfo.Id;
            outResult.CustomerBM = dto.CustomerReg.CustomerBM;
            outResult.OrderId = dto.CustomerReg.OrderNum;
            outResult.ErrInfo = "退费成功！";
            return outResult;




        }
        #endregion
        #region HIS收费回传
        public OutResult SaveHis(fee_chargeDto feeinput  )
        {
            #region 收费
            OutResult result = new OutResult();
            if (feeinput.fee_info.Count ==0)
            {
                result.Code = 0;
                result.ErrInfo = "没有申请单号不能收费！";
                return result;

            }
            var fee_no = feeinput.fee_info.FirstOrDefault().receipt_no;
            var cusreg = _jlCustomerItemGroup.GetAll().FirstOrDefault(p => p.CustomerRegBM.Customer.pid == feeinput.pid && p.fee_no == fee_no && p.PayerCat== (int)PayerCatType.NoCharge)?.CustomerRegBM;
            if (cusreg == null)
            {
                result.Code = 0;
                result.ErrInfo = "没有该体检人，或该体检人已收费请核实！";
                result.CustomerBM = feeinput.pid;
                result.OrderId = feeinput.sett_id;
                return result;

            }

            var summoney = feeinput.fee_info.Sum(o => o.actual_money);
            //if (summoney != feeinput.total_money)
            //{
            //    result.Code = 0;
            //    result.ErrInfo = "支付金额，和明细金额不符";
            //    result.CustomerBM = feeinput.pid;
            //    result.OrderId = feeinput.sett_id;
            //    return result;
            //}
            foreach (var sqd in feeinput.fee_info)
            {
                CreateReceiptInfoDto input = new CreateReceiptInfoDto();
                decimal ReturnMoney = 0;     //抹零金额    
                input.Discontmoney = 0;

                input.Actualmoney = feeinput.total_money.Value;

                input.ChargeDate = DateTime.Parse(feeinput.pay_time);
                input.ChargeState = (int)InvoiceStatus.NormalCharge;

                input.CustomerRegid = cusreg.Id;//关联已有对象                

                input.DiscontReason = "";
                input.Discount = 1;
                input.ReceiptSate = (int)InvoiceStatus.Valid;
                input.Remarks = feeinput.sett_id;
                input.SettlementSate = (int)ReceiptState.UnSettled;
                input.Shouldmoney = feeinput.total_money.Value;
                input.Summoney = feeinput.total_money.Value;
                int tjtype = 2;
                input.TJType = tjtype;
                input.Userid = AbpSession.UserId.Value;
                input.pay_order_id = sqd.receipt_no;
                //支付方式
                List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
                foreach (var HISpayment in feeinput.sett_info)
                {

                    var payment = _tbmMChargeType.FirstOrDefault(o => o.ChargeName.Contains(HISpayment.pay_type));
                    //没有微信支付则新建
                    if (payment == null)
                    {
                        payment = new TbmMChargeType();
                        payment.Id = Guid.NewGuid();
                        payment.AccountingState = 2;
                        payment.ChargeApply = 3;
                        payment.ChargeCode = 100;
                        payment.ChargeName = HISpayment.pay_type;
                        var zjm = common.GetHansBrief(new ChineseDto { Hans = HISpayment.pay_type });
                        payment.HelpChar = zjm.Brief;
                        payment.OrderNum = 100;
                        payment.PrintName = 3;
                        payment.Remarks = "";
                        payment = _tbmMChargeType.Insert(payment);
                        CurrentUnitOfWork.SaveChanges();

                    }
                    CreatePaymentDto CreatePayment = new CreatePaymentDto();
                    CreatePayment.Actualmoney = HISpayment.sett_price.Value;
                    CreatePayment.CardNum = "";//卡号
                    CreatePayment.Discount = 1;
                    CreatePayment.MChargeTypeId = payment.Id;
                    CreatePayment.Shouldmoney = HISpayment.sett_price.Value;
                    CreatePayment.MChargeTypename = payment.ChargeName;
                    CreatePayments.Add(CreatePayment);
                }
                input.MPaymentr = CreatePayments;
                //未收费项目集合
                List<CreateMReceiptInfoDetailedDto> CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();

                //var cusPerGroups = feeinput.total_money.Value;
                //var ChargeGroups = cusPerGroups?.GroupBy(r => r.SFType);
                var receipt_no = sqd.receipt_no;
                var cusPerGroups = _jlCustomerItemGroup.GetAll().Where(p=>p.CustomerRegBMId== cusreg.Id && p.fee_no== receipt_no);
                foreach (var itemgroup in cusPerGroups)
                {
                    //var itemgroup = _jlCustomerItemGroup.FirstOrDefault(o => o.ItemGroupBM.ItemGroupBM == ChargeGroup.ItemGroupBM && o.CustomerRegBMId == cusreg.Id);
                    // var itemgroup = _bmItemGroup.FirstOrDefault(o=>o.ItemGroupBM== ChargeGroup.ItemGroupBM);
                   
                    CreateMReceiptInfoDetailedDto CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                    CreateMReceiptInfoDetailed.GroupsMoney = itemgroup.ItemPrice;
                    CreateMReceiptInfoDetailed.GroupsDiscountMoney = itemgroup.PriceAfterDis;
                    if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                    {
                        CreateMReceiptInfoDetailed.Discount = CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                    }
                    else
                    {
                        CreateMReceiptInfoDetailed.Discount = 1;
                    }
                    CreateMReceiptInfoDetailed.ReceiptTypeName = itemgroup.ItemGroupBM.ChartName;
                    //组合
                    CreateMReceiptInfoDetailed.ItemGroupId = itemgroup.Id;
                    CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
                }

                input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
                //保存收费记录
                //  Guid receiptInfoDtoID = ChargeAppService.InsertReceiptInfoDto(input);
                OutErrDto outErrDto = _ChargeAppService.InsertReceiptState(input);
                result.Code = int.Parse(outErrDto.code);
                result.ErrInfo = outErrDto.err;
            }
           
            if (result.Code == 1)
            {
                result.ErrInfo = "收费成功！";
               // result.ReceiptId = feeinput.sett_id;
            }
            result.CustomerBM = feeinput.pid;
            result.OrderId = feeinput.sett_id;
            return result;
            #endregion             
        }


        /// <summary>
        /// His取消支付
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>     
        public OutResult CancelHis(Refee_chargeDto feeinput)
        {
            OutResult outResult = new OutResult();
            foreach (var fee_in in  feeinput.fee_info)
            {
                #region 退费
                var dto = _TjlMReceiptInfo.FirstOrDefault(o => o.pay_order_id == fee_in.receipt_no);
                if (dto == null)
                {
                    outResult.Code = 0;
                    outResult.ErrInfo = "没有该退费记录！";
                    return outResult;
                }
                //总检完成不可作废
                if (dto.CustomerReg?.SummSate >= 3)
                {

                    outResult.Code = 0;
                    outResult.ErrInfo = "该体检人已总检，不能作废！";
                    return outResult;
                }
                EntityDto<Guid> receiptID = new EntityDto<Guid>();
                receiptID.Id = dto.Id;
                bool ischek = _ChargeAppService.SFGroupCheck(receiptID);
                if (ischek == true)
                {


                    outResult.Code = 0;
                    outResult.ErrInfo = "项目已检查，不能作废！";
                    return outResult;
                }

                MReceiptInfoDto mReceiptInfoDto = _ChargeAppService.GetInvalid(receiptID);
                int receiptstate = (int)InvoiceStatus.Cancellation;
                if (mReceiptInfoDto != null && mReceiptInfoDto.ReceiptSate == receiptstate)
                {


                    outResult.Code = 0;
                    outResult.ErrInfo = "此记录已作废，不能重复作废！";
                    return outResult;
                }
                GuIdDto input = new GuIdDto();
                input.Id = dto.Id;
                MReceiptInfoDto MReceiptInfo = _ChargeAppService.InsertInvalidReceiptInfoDto(input);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                if (dto.CustomerReg != null)
                {
                    createOpLogDto.LogBM = dto.CustomerReg.CustomerBM;
                    createOpLogDto.LogName = dto.CustomerReg.Customer.Name;
                }
                else if (dto.ClientReg != null)
                {
                    createOpLogDto.LogBM = dto.ClientReg.ClientRegBM;
                    createOpLogDto.LogName = dto.ClientReg.ClientInfo.ClientName;
                }

                createOpLogDto.LogText = "HIS作废收费：" + dto.Actualmoney;
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                common.SaveOpLog(createOpLogDto);
                outResult.Code = 1;
                outResult.ReceiptId = MReceiptInfo.Id;
                outResult.CustomerBM = dto.CustomerReg.Customer.pid;
                outResult.OrderId = dto.Remarks;
                outResult.ErrInfo = "退费成功！";
                #endregion
            }
            return outResult;




        }

        #endregion
        /// <summary>
        /// 复查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WxCusReview> getWxCusReview(SeachRepot input)
        {
            List<WxCusReview> wxCusReviews = new List<WxCusReview>();
            var cusreview = _tjlCusReview.GetAll().Where(o => o.SummarizeAdvice.Uid != null && o.SummarizeAdvice.Uid != "");
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                cusreview = cusreview.Where(o => o.CustomerReg.CustomerBM == input.CustomerBM);
            }
            if (input.starTime.HasValue)
            {
                cusreview = cusreview.Where(o => o.CreationTime > input.starTime || o.LastModificationTime > input.starTime);
            }
            if (input.EndTime.HasValue)
            {
                cusreview = cusreview.Where(o => o.CreationTime <= input.EndTime || o.LastModificationTime <= input.EndTime);
            }
            wxCusReviews = cusreview.Select(o => new WxCusReview
            {
                ArchivesNum = o.CustomerReg.Customer.ArchivesNum,
                CustomerBM = o.CustomerReg.CustomerBM,
                adviceBM = o.SummarizeAdvice.Uid,
                ReviewDate = o.ReviewDate,
                IllName = o.SummarizeAdvice.AdviceName,
                Remark = o.Remart,
                LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                ItemGroups = o.ItemGroup.Select(r => new WxReviewItemGroup { ItemGroupBM = r.ItemGroupBM }).ToList()
            }).OrderBy(n => n.LastDate).ToList();
            //错误信息
            if (input.ErrCustomerBMs != null && input.ErrCustomerBMs.Count > 0)
            {
                var cusreviewerr = _tjlCusReview.GetAll().Where(o => o.SummarizeAdvice.Uid != null && o.SummarizeAdvice.Uid != "" && input.ErrCustomerBMs.Contains(o.CustomerReg.CustomerBM));
                var wxCusReviewserr = cusreviewerr.Select(o => new WxCusReview
                {
                    ArchivesNum = o.CustomerReg.Customer.ArchivesNum,
                    CustomerBM = o.CustomerReg.CustomerBM,
                    adviceBM = o.SummarizeAdvice.Uid,
                    ReviewDate = o.ReviewDate,
                    IllName = o.SummarizeAdvice.AdviceName,
                    Remark = o.Remart,
                    LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                    ItemGroups = o.ItemGroup.Select(r => new WxReviewItemGroup { ItemGroupBM = r.ItemGroupBM }).ToList()
                }).OrderBy(n => n.LastDate).ToList();
                wxCusReviews.AddRange(wxCusReviewserr);
            }
            return wxCusReviews;
        }
        /// <summary>
        /// 复查消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WxCusReviewMessDto> getWxCusReviewMess(SeachRepot input)
        {
           // var bis = _jTbmBasicDictionary.GetAll().Where(p => p.Type == "Disease").Select(o => o.Text).ToList();

            List<WxCusReviewMessDto> wxCusReviews = new List<WxCusReviewMessDto>();
            var cusreview = _tjlCusReview.GetAll();
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                cusreview = cusreview.Where(o => o.CustomerReg.CustomerBM == input.CustomerBM);
            }
            if (input.starTime.HasValue)
            {
                cusreview = cusreview.Where(o => o.CreationTime > input.starTime || o.LastModificationTime > input.starTime);
            }
            if (input.EndTime.HasValue)
            {
                cusreview = cusreview.Where(o => o.CreationTime <= input.EndTime || o.LastModificationTime <= input.EndTime);
            }
            var CusReviews = cusreview.ToList().GroupBy(P => P.CustomerReg.CustomerBM).ToList();
            //错误信息
            foreach (var revie in CusReviews)
            {
                WxCusReviewMessDto wxCus = new WxCusReviewMessDto();
                wxCus.archivesnum = revie.FirstOrDefault().CustomerReg.Customer.ArchivesNum;
                wxCus.CustomerBM = revie.FirstOrDefault().CustomerReg.CustomerBM;
                wxCus.OrderNum = revie.FirstOrDefault().CustomerReg.OrderNum;
                var groupNames = revie.SelectMany(p => p.ItemGroup).Select(p => p.ItemGroupName).ToList();
                wxCus.keyword1 = string.Join(",", groupNames);
                wxCus.keyword2 = revie.FirstOrDefault().ReviewDate.ToString();
                wxCus.LastDate = revie.Min(p => p.CreationTime);
                wxCusReviews.Add(wxCus);
            }

            //错误信息
            if (input.ErrCustomerBMs != null && input.ErrCustomerBMs.Count > 0)
            {
                var cusreviewerrs = _tjlCusReview.GetAll().Where(o => o.CustomerReg!=null && o.SummarizeAdvice.Uid != null && o.SummarizeAdvice.Uid != ""
                && input.ErrCustomerBMs.Contains(o.CustomerReg.CustomerBM)).GroupBy(P=>P.CustomerReg.CustomerBM).ToList();
              
                foreach(var revie in cusreviewerrs)
                {
                    WxCusReviewMessDto wxCus = new WxCusReviewMessDto();
                    wxCus.archivesnum = revie.FirstOrDefault().CustomerReg.Customer.ArchivesNum;
                    wxCus.CustomerBM = revie.FirstOrDefault().CustomerReg.CustomerBM;
                    wxCus.OrderNum = revie.FirstOrDefault().CustomerReg.OrderNum;
                    var groupNames = revie.SelectMany(p=>p.ItemGroup).Select(p=>p.ItemGroupName).ToList();
                    wxCus.keyword1 = string.Join(",", groupNames);
                    wxCus.keyword2 = revie.FirstOrDefault().ReviewDay.ToString();
                    wxCus.LastDate = revie.Min(p => p.CreationTime);
                    wxCusReviews.Add(wxCus);
                }
                //var wxCusReviewserr = cusreviewerr.Select(o => new WxCusReview
                //{
                //    ArchivesNum = o.CustomerReg.Customer.ArchivesNum,
                //    CustomerBM = o.CustomerReg.CustomerBM,
                //    adviceBM = o.SummarizeAdvice.Uid,
                //    ReviewDate = o.ReviewDate,
                //    IllName = o.SummarizeAdvice.AdviceName,
                //    Remark = o.Remart,
                //    LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                //    ItemGroups = o.ItemGroup.Select(r => new WxReviewItemGroup { ItemGroupBM = r.ItemGroupBM }).ToList()
                //}).OrderBy(n => n.LastDate).ToList();
                //wxCusReviews.AddRange(wxCusReviewserr);
            }
            return wxCusReviews.OrderBy(p=>p.LastDate).ToList();
        }
        /// <summary>
        /// 补检消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WxCusReviewMessDto> getCusGiveUp(SeachRepot input)
        {
           // var bis = _jTbmBasicDictionary.GetAll().Where(p => p.Type == "Disease").Select(o => o.Text).ToList();

            List<WxCusReviewMessDto> wxCusReviews = new List<WxCusReviewMessDto>();
            var que = _TjlCusGiveUp.GetAll();
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                que = que.Where(o => o.CustomerReg.CustomerBM == input.CustomerBM);
            }
            if (input.starTime.HasValue)
            {
                que = que.Where(o => o.CreationTime > input.starTime || o.LastModificationTime > input.starTime);
            }
            if (input.EndTime.HasValue)
            {
                que = que.Where(o => o.CreationTime <= input.EndTime || o.LastModificationTime <= input.EndTime);
            }
            que = que.Where(p=>p.stayType!=1);
            var CusReviews = que.Where(p=>p.CustomerReg!=null).ToList().GroupBy(P => P.CustomerReg.CustomerBM).ToList();
            //错误信息
            foreach (var revie in CusReviews)
            {
                WxCusReviewMessDto wxCus = new WxCusReviewMessDto();
                wxCus.archivesnum = revie.FirstOrDefault().Customer.ArchivesNum;
                wxCus.CustomerBM = revie.FirstOrDefault().CustomerReg.CustomerBM;
                wxCus.OrderNum = revie.FirstOrDefault().CustomerReg.OrderNum;
                var groupNames = revie.Select(p => p.CustomerItemGroup.ItemGroupName).ToList();
                wxCus.keyword1 = string.Join(",", groupNames);
                wxCus.keyword2 = revie.FirstOrDefault().stayDate.ToString();
                wxCus.LastDate = revie.Min(p => p.CreationTime);
                wxCusReviews.Add(wxCus);
            }

            //错误信息
            if (input.ErrCustomerBMs != null && input.ErrCustomerBMs.Count > 0)
            {
                var cusreviewerrs = _TjlCusGiveUp.GetAll().Where(o => o.CustomerReg != null 
                && input.ErrCustomerBMs.Contains(o.CustomerReg.CustomerBM)).GroupBy(P => P.CustomerReg.CustomerBM).ToList();

                foreach (var revie in cusreviewerrs)
                {
                    WxCusReviewMessDto wxCus = new WxCusReviewMessDto();
                    wxCus.archivesnum = revie.FirstOrDefault().CustomerReg.Customer.ArchivesNum;
                    wxCus.CustomerBM = revie.FirstOrDefault().CustomerReg.CustomerBM;
                    wxCus.OrderNum = revie.FirstOrDefault().CustomerReg.OrderNum;
                    var groupNames = revie.Select(p => p.CustomerItemGroup.ItemGroupName).ToList();
                    wxCus.keyword1 = string.Join(",", groupNames);
                    wxCus.keyword2 = revie.FirstOrDefault().stayDate.ToString();
                    wxCus.LastDate = revie.Min(p => p.CreationTime);
                    wxCusReviews.Add(wxCus);
                }
                //var wxCusReviewserr = cusreviewerr.Select(o => new WxCusReview
                //{
                //    ArchivesNum = o.CustomerReg.Customer.ArchivesNum,
                //    CustomerBM = o.CustomerReg.CustomerBM,
                //    adviceBM = o.SummarizeAdvice.Uid,
                //    ReviewDate = o.ReviewDate,
                //    IllName = o.SummarizeAdvice.AdviceName,
                //    Remark = o.Remart,
                //    LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                //    ItemGroups = o.ItemGroup.Select(r => new WxReviewItemGroup { ItemGroupBM = r.ItemGroupBM }).ToList()
                //}).OrderBy(n => n.LastDate).ToList();
                //wxCusReviews.AddRange(wxCusReviewserr);
            }
            return wxCusReviews.OrderBy(p => p.LastDate).ToList();
        }

        /// <summary>
        /// 危急值消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WxCusMessDto> getCusCrisis(SeachRepot input)
        {
            // var bis = _jTbmBasicDictionary.GetAll().Where(p => p.Type == "Disease").Select(o => o.Text).ToList();

            List<WxCusMessDto> wxCusReviews = new List<WxCusMessDto>();
            var que = _jlCustomerRegItem.GetAll().Where(p => p.CrisisSate == (int)CrisisSate.Abnormal);
         
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                que = que.Where(o => o.CustomerRegBM.CustomerBM == input.CustomerBM);
            }
            if (input.starTime.HasValue)
            {
                que = que.Where(o => o.CreationTime > input.starTime || o.LastModificationTime > input.starTime);
            }
            if (input.EndTime.HasValue)
            {
                que = que.Where(o => o.CreationTime <= input.EndTime || o.LastModificationTime <= input.EndTime);
            }         
            var CusReviews = que.ToList().GroupBy(P => P.CustomerRegBM.CustomerBM).ToList();
            //错误信息
            foreach (var revie in CusReviews)
            {
                WxCusMessDto wxCus = new WxCusMessDto();
                wxCus.archivesnum = revie.FirstOrDefault().CustomerRegBM.Customer.ArchivesNum;
                wxCus.CustomerBM = revie.FirstOrDefault().CustomerRegBM.CustomerBM;
                wxCus.OrderNum = revie.FirstOrDefault().CustomerRegBM.OrderNum;
                wxCus.Name = revie.FirstOrDefault().CustomerRegBM.Customer.Name;
                var DepartName = revie.Where(p=>p.DepartmentBM!=null).Select(p =>  p.DepartmentBM.Name).Distinct().ToList();
                var departNames = string.Join(",", DepartName);
                var ItemName = revie.Where(p => p.DepartmentBM != null).Select(p => p.ItemName).Distinct().ToList();
                var ItemNames = string.Join(",", ItemName);
                var Result = revie.Select(p =>"项目名称：" +p.ItemName+"：" +p.ItemResultChar).ToList();
                var Results = string.Join(";", Result);
                wxCus.keyword1 = departNames;
                wxCus.keyword2 = ItemNames;
                wxCus.keyword3 = Results;
                wxCus.LastDate = revie.Min(p => p.CreationTime);
                wxCusReviews.Add(wxCus);
            }

            //错误信息
            if (input.ErrCustomerBMs != null && input.ErrCustomerBMs.Count > 0)
            {
                var cusreviewerrs = _TjlCusGiveUp.GetAll().Where(o => o.CustomerReg != null
                && input.ErrCustomerBMs.Contains(o.CustomerReg.CustomerBM)).GroupBy(P => P.CustomerReg.CustomerBM).ToList();

                foreach (var revie in cusreviewerrs)
                {
                    WxCusMessDto wxCus = new WxCusMessDto();
                    wxCus.archivesnum = revie.FirstOrDefault().CustomerReg.Customer.ArchivesNum;
                    wxCus.CustomerBM = revie.FirstOrDefault().CustomerReg.CustomerBM;
                    wxCus.OrderNum = revie.FirstOrDefault().CustomerReg.OrderNum;
                    var groupNames = revie.Select(p => p.CustomerItemGroup.ItemGroupName).ToList();
                    wxCus.keyword1 = string.Join(",", groupNames);
                    wxCus.keyword2 = revie.FirstOrDefault().stayDate.ToString();
                    wxCus.LastDate = revie.Min(p => p.CreationTime);
                    wxCusReviews.Add(wxCus);
                }
                //var wxCusReviewserr = cusreviewerr.Select(o => new WxCusReview
                //{
                //    ArchivesNum = o.CustomerReg.Customer.ArchivesNum,
                //    CustomerBM = o.CustomerReg.CustomerBM,
                //    adviceBM = o.SummarizeAdvice.Uid,
                //    ReviewDate = o.ReviewDate,
                //    IllName = o.SummarizeAdvice.AdviceName,
                //    Remark = o.Remart,
                //    LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                //    ItemGroups = o.ItemGroup.Select(r => new WxReviewItemGroup { ItemGroupBM = r.ItemGroupBM }).ToList()
                //}).OrderBy(n => n.LastDate).ToList();
                //wxCusReviews.AddRange(wxCusReviewserr);
            }
            return wxCusReviews.OrderBy(p => p.LastDate).ToList();
        }
        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WXClientInfoDto> getWXClientInfo(SearchBiseDto input)
        {
            List<WXClientInfoDto> outlist = new List<WXClientInfoDto>();
            var que = _tjlClientInfo.GetAll();
            if (input.starTime.HasValue)
            {
                que = que.Where(o => o.CreationTime > input.starTime || o.LastModificationTime > input.starTime);
            }
            outlist = que.Select(o => new WXClientInfoDto
            {
                Address = o.Address,
                ClientAbbreviation = o.ClientAbbreviation,
                ClientAccount = o.ClientAccount,
                ClientBank = o.ClientBank,
                ClientBM = o.ClientBM,
                ClientEmail = o.ClientEmail,
                Clientlndutry = o.Clientlndutry,
                ClientName = o.ClientName,
                ClientQQ = o.ClientQQ,
                Fax = o.Fax,
                HelpCode = o.HelpCode,
                LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                LinkMan = o.LinkMan,
                Mobile = o.Mobile,
                OrganizationCode = o.OrganizationCode,
                PostCode = o.PostCode,
                Telephone = o.Telephone,
                WubiCode = o.WubiCode
            }).ToList();
            if (input.ErrIDBMs != null && input.ErrIDBMs.Count > 0)
            {
                var queErr = _tjlClientInfo.GetAll().Where(o => input.ErrIDBMs.Contains(o.ClientBM)).Select(o => new WXClientInfoDto
                {
                    Address = o.Address,
                    ClientAbbreviation = o.ClientAbbreviation,
                    ClientAccount = o.ClientAccount,
                    ClientBank = o.ClientBank,
                    ClientBM = o.ClientBM,
                    ClientEmail = o.ClientEmail,
                    Clientlndutry = o.Clientlndutry,
                    ClientName = o.ClientName,
                    ClientQQ = o.ClientQQ,
                    Fax = o.Fax,
                    HelpCode = o.HelpCode,
                    LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                    LinkMan = o.LinkMan,
                    Mobile = o.Mobile,
                    OrganizationCode = o.OrganizationCode,
                    PostCode = o.PostCode,
                    Telephone = o.Telephone,
                    WubiCode = o.WubiCode
                }).ToList();
                if (queErr != null && queErr.Count > 0)
                {
                    outlist.AddRange(queErr.ToList());
                }
            }
            return outlist;


        }
        /// <summary>
        /// 获取有人员的单位预约编码
        /// </summary>
        /// <returns></returns>        
        public List<ClientRegBMDto> getCheckClientRegBM(SearchBiseDto dto)
        {
            List<ClientRegBMDto> list = new List<ClientRegBMDto>();
            var que = _jlCustomerReg.GetAll().Where(o => o.ClientInfoId != null).Select(o => new
            {
                o.ClientReg.ClientRegBM,
                LastDate = o.ClientReg.LastModificationTime == null ? o.ClientReg.CreationTime : o.ClientReg.LastModificationTime
            }).Distinct();
            if (dto.starTime.HasValue)
            {
                que = que.Where(o => o.LastDate > dto.starTime);
            }
            var clientlist = que.ToList();
            list = clientlist.Select(o => new ClientRegBMDto
            {
                ClientRegBM = o.ClientRegBM,
                LastTime = o.LastDate
            }).ToList();
            if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
            {
                var erlist = _jlClientReg.GetAll().Where(o => dto.ErrIDBMs.Contains(o.ClientRegBM)).
                    Select(o => new ClientRegBMDto
                    {
                        ClientRegBM = o.ClientRegBM,
                        LastTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime
                    }).ToList();
                list.AddRange(erlist);
            }
            var sl = list.OrderBy(o => o.LastTime).Distinct().ToList();
            return sl;
        }
        /// <summary>
        /// 单位预约及人员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public WXClientCusListDto wXClientCusListDto(SearchClientCusDto dto)
        {
            WXClientCusListDto list = new WXClientCusListDto();
            var que = _jlClientReg.GetAll().FirstOrDefault(o => o.ClientRegBM == dto.ClientRegBM);
            if (que != null)
            {
                list.ClientBM = que.ClientInfo.ClientBM;
                list.ClientRegBM = que.ClientRegBM;
                list.EndCheckDate = que.EndCheckDate;              
                list.StartCheckDate = que.StartCheckDate;
                list.LastTime = que.LastModificationTime == null ? que.CreationTime : que.LastModificationTime;
                var cusl = _jlCustomerReg.GetAll().Where(o => o.ClientRegId == que.Id);
                if (dto.starTime.HasValue)
                {
                    cusl = cusl.Where(o=>o.CreationTime>dto.starTime && o.LastModificationTime>dto.starTime);
                }
                list.clientcuslist = cusl.Select(o => new WXclientCusDto
                {
                    ArchivesNum = o.Customer.ArchivesNum,
                    CustomerBM = o.CustomerBM,
                    ItemSuitBM = o.ItemSuitBM.ItemSuitID??"",
                    LoginDate = o.LoginDate,
                    WebQueryCode = o.WebQueryCode,
                    LastTime= o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime

                }).ToList();
            }
            return list;

        }
        /// <summary>
        /// 体检预约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WXCusReglistDto> wXCusReglistDtos(SeachRepot input)
        {
            List<WXCusReglistDto> lis = new List<WXCusReglistDto>();
            var que = _jlCustomerReg.GetAll().Where(o=>o.ClientRegId == null);
            if (input.starTime.HasValue)
            {
                que = que.Where(o => o.CreationTime > input.starTime || (o.LastModificationTime != null &&
                o.LastModificationTime > input.starTime));
            }
            var quels = que.Select(o => new WXCusReglistDto
            {
                ArchivesNum = o.Customer.ArchivesNum,
                CustomerBM = o.CustomerBM,
                ItemSuitBM = o.ItemSuitBM.ItemSuitID,
                LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                LoginDate = o.LoginDate,
                State = 1,
                WebQueryCode = o.WebQueryCode
            }).ToList();
            if (quels.Count > 100)
            {
                var endtime = quels.OrderBy(o => o.LastDate).Take(100).LastOrDefault().LastDate;

                lis = quels.OrderBy(o => o.LastDate).Where(o => o.LastDate <= endtime).Select(o => new WXCusReglistDto
                {
                    ArchivesNum = o.ArchivesNum,
                    CustomerBM = o.CustomerBM,
                    ItemSuitBM = o.ItemSuitBM,
                    LastDate = o.LastDate,
                    LoginDate = o.LoginDate,
                    State = 0,
                    WebQueryCode = o.WebQueryCode
                }).ToList();

            }
            else
            {
                lis = quels.OrderBy(o => o.LastDate).ToList();
            }
            return lis;
        }
        /// <summary>
        /// 体检人诊断
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusSumBMLisDto> cusSumBMLisDtos(SeachRepot input)
        {
            List<CusSumBMLisDto> list = new List<CusSumBMLisDto>();
            var que = _CustomerSummarizeBM.GetAll().Where(p=>p.CustomerReg.SummSate==(int)SummSate.Audited);
            if (input.starTime.HasValue)
            {
                que = que.Where(o=>o.CreationTime>input.starTime || o.LastModificationTime>input.starTime);
            }
            var sumlist = que.Select(o => new CusSumBMLisDto
            {
                CustomerBM = o.CustomerReg.CustomerBM,
                SummarizeName = o.SummarizeName,
                Uid = o.SummarizeAdvice.Uid,
                LastDate= o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                State=1,
                ExamineDate=o.CreationTime,
                LoginDate=o.CustomerReg.LoginDate

            }).ToList();
            var bis = _jTbmBasicDictionary.GetAll().Where(p => p.Type == "Disease").Select(o => o.Text).ToList();
            if (bis.Count == 0)
            {
                bis.Add("高血压");
                bis.Add("糖尿病");
                bis.Add("低血压");
            }
            sumlist = sumlist.Where(p => bis.Contains(p.SummarizeName)).ToList();

            if (sumlist.Count > 50)
            {
                var endtime = sumlist.OrderBy(o => o.LastDate).Take(50).LastOrDefault().LastDate;

                list = sumlist.OrderBy(o => o.LastDate).Where(o => o.LastDate <= endtime).Select(o => new CusSumBMLisDto
                {
                    CustomerBM = o.CustomerBM,
                    SummarizeName = o.SummarizeName,
                    Uid = o.Uid,
                    LastDate = o.LastDate,
                    State = 0,
                    ExamineDate = o.ExamineDate,
                    LoginDate = o.LoginDate
                }).ToList();

            }
            else
            {
                list = sumlist.OrderBy(o => o.LastDate).ToList();
            }
            return list;
        }

        /// <summary>
        /// 体检人慢病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusMBSumBMDtocs> cusMBSumDtos(SeachRepot input)
        {
            var bis = _jTbmBasicDictionary.GetAll().Where(p=>p.Type== "Disease").Select(o=>o.Text).ToList();
            if (bis.Count == 0)
            {
                bis.Add("高血压");
                bis.Add("糖尿病");
                bis.Add("低血压");
            }
            List<CusMBSumBMDtocs> list = new List<CusMBSumBMDtocs>();
            var que = _jlCustomerReg.GetAll().Where(o =>o.SummSate==(int)SummSate.Audited && o.PrintSate == (int)PrintSate.Print 
            && o.CustomerSummarizeBM.Any(p=>bis.Contains(p.SummarizeName)));
            if (input.starTime.HasValue)
            {
                que = que.Where(o => o.CustomerSummarizeBM.Any(p=>p.CreationTime> input.starTime || p.LastModificationTime > input.starTime));
            }
            var sumlist = que.Select(o => new CusMBSumBMDtocs
            {
                CustomerBM = o.CustomerBM,
                CustomerSummarizeBM = o.CustomerSummarizeBM.Where(p=> bis.Contains(p.SummarizeName)).Select(p=>new SumBMDto {
                 SummarizeName=p.SummarizeName,
                 Uid=p.SummarizeAdvice.Uid}).ToList(),                
                LastDate = o.CustomerSummarizeBM.FirstOrDefault(p => bis.Contains(p.SummarizeName)).CreationTime,
                State = 1,
                ExamineDate = o.CustomerSummarizeBM.FirstOrDefault(p => bis.Contains(p.SummarizeName)).CreationTime,
                LoginDate = o.LoginDate

            }).ToList();
            if (sumlist.Count > 50)
            {
                var endtime = sumlist.OrderBy(o => o.LastDate).Take(50).LastOrDefault().LastDate;

                list = sumlist.OrderBy(o => o.LastDate).Where(o => o.LastDate <= endtime).Select(o => new CusMBSumBMDtocs
                {
                    CustomerBM = o.CustomerBM,
                    CustomerSummarizeBM = o.CustomerSummarizeBM,                  
                    LastDate = o.LastDate,
                    State = 0,
                    ExamineDate = o.ExamineDate,
                    LoginDate = o.LoginDate
                }).ToList();

            }
            else
            {
                list = sumlist.OrderBy(o => o.LastDate).ToList();
            }
            return list;
        }
        /// <summary>
        /// 组合关联医嘱项目
        /// </summary>
        /// <returns></returns>
        public List<WXGroupPriceSynchronizesDto> GetWXGroupPriceSynchronizes(SearchBiseDto dto)
        {
            var GroupRePrice = new List<WXGroupPriceSynchronizesDto>();
               var serch = _GroupRePriceSynchronizes.GetAll().Where(o => o.ItemGroup != null);
            if (dto.starTime.HasValue)
            {
                serch = serch.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
            }

            GroupRePrice = serch.Select(o => new WXGroupPriceSynchronizesDto
            {
                ItemGroupBM = o.ItemGroup.ItemGroupBM,
                ItemGroupName = o.ItemGroup.ItemGroupName,
                chkit_costn = o.chkit_costn,
                chkit_id = o.PriceSynchronize.chkit_id,
                chkit_name = o.PriceSynchronize.chkit_name,
                Count = o.Count,
                LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
            }).ToList();
            if (dto.ErrIDBMs != null && dto.ErrIDBMs.Count > 0)
            {
                var Errserch = _GroupRePriceSynchronizes.GetAll().Where(o => o.ItemGroup != null && dto.ErrIDBMs.Contains(o.ItemGroup.ItemGroupBM));
                var erlist = Errserch.Select(o => new WXGroupPriceSynchronizesDto
                {
                    ItemGroupBM = o.ItemGroup.ItemGroupBM,
                    ItemGroupName = o.ItemGroup.ItemGroupName,
                    chkit_costn = o.chkit_costn,
                    chkit_id = o.PriceSynchronize.chkit_id,
                    chkit_name = o.PriceSynchronize.chkit_name,
                    Count = o.Count,
                    LastDate = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                }).ToList();
                GroupRePrice.AddRange(erlist);
            }
            return GroupRePrice.ToList();
        }
        /// <summary>
        /// 医嘱项目
        /// </summary>
        /// <returns></returns>
        
        public async Task<List<WXPriceSynchronizeDto>> GetWXPriceSynchronize(SearchBiseDto dto)
        {
            var GroupRePrice = new List<WXGroupPriceSynchronizesDto>();
            var serch = _TbmPriceSynchronize.GetAll();
            if (dto.starTime.HasValue)
            {
                serch = serch.Where(o => o.CreationTime > dto.starTime || (o.LastModificationTime != null && o.LastModificationTime > dto.starTime));
            }
            return await serch.AsNoTracking().
                ProjectToListAsync<WXPriceSynchronizeDto>(GetConfigurationProvider<Core.Coding.TbmPriceSynchronize, WXPriceSynchronizeDto>());

            
        }
          



    }

    public class adviceNames
    {
        public string adviceName { get; set; }

        public string adviceContent { get; set; }


    }
    public class reportitemInfoOld
    {
        public virtual Guid Id { get; set; }
        public virtual Guid ItemId { get; set; }

        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemResultChar { get; set; }

        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>
        [StringLength(16)]
        public virtual string Symbol { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }



        /// <summary>
        /// 项目组合名称
        /// </summary>      
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>      
        [StringLength(32)]
        public virtual string ItemName { get; set; }
        /// <summary>
        /// 科室小结
        /// </summary>      
        [StringLength(32)]
        public virtual string DeptSum { get; set; }
        /// <summary>
        /// 小结医生
        /// </summary>      
        [StringLength(32)]
        public virtual string DeptEmp { get; set; }

        /// <summary>
        /// 检查医生（检查者）
        /// </summary>
        public virtual string CheckDoctor { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        public virtual string CheckEmployeDoctor { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>      
        [StringLength(32)]
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 项目状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? ProcessState { get; set; }

    }
}
