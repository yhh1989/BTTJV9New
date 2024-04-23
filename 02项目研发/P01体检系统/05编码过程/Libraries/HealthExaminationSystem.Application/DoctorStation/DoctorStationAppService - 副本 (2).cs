using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Dicom;
using Dicom.Imaging;
using PdfiumViewer;
using Sw.Hospital.HealthExamination.Drivers;
using Sw.Hospital.HealthExamination.Drivers.Models.LisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceEmployeeComparison;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Picture;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Crisis;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Z.EntityFramework.Plus;
using Symbol = Sw.Hospital.HealthExaminationSystem.Common.Enums.Symbol;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation
{
    /// <summary>
    /// 医生站
    /// </summary>
    [AbpAuthorize]
    public class DoctorStationAppService : MyProjectAppServiceBase, IDoctorStationAppService
    {
        private readonly IRepository<TbmBasicDictionary, Guid> _basicDictionaryRepository; //系统字典

        private readonly IRepository<TjlCustomerDepSummary, Guid> _customerDepSummaryRepository; //科室小结

        private readonly IRepository<TjlCustomerItemGroup, Guid> _customerItemGroupRepository; //体检人组合项目

        private readonly IRepository<TjlCustomerItemPic, Guid> _customerItemPicRepository; //图片

        private readonly IRepository<TjlCustomerRegItem, Guid> _customerRegItemRepository; //体检人项目

        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository; //体检人预约登记信息表

        private readonly IRepository<TbmItemDictionary, Guid> _itemDictionaryRepository; //项目字典

        private readonly IRepository<TbmItemInfo, Guid> _itemInfoRepository; //项目设置

        private readonly IRepository<TbmItemStandard, Guid> _itemStandardRepository; //项目参考值设置

        private readonly IRepository<TjlMReceiptInfo, Guid> _receiptInfoRepository; //收费结算表

        private readonly IRepository<TbmSummarizeAdvice, Guid> _summarizeAdviceRepository; //健康建议

        private readonly IRepository<User, long> _userRepository; //用户信息

        //private readonly IRepository<TjlCustomerSummary, Guid> _customerSummary; //专科建议 

        private readonly IInterfaceItemComparisonAppService _InterfaceItemComparison; //项目对应 

        private readonly InterfaceEmployeeComparisonAppService _InterfaceEmployeeComparison; //医生对应 

        private readonly IRepository<TbmDepartment, Guid> _departmentRepository; //科室设置

        private readonly IRepository<TbmDiagnosis, Guid> _diagnosis; //复合判断设置

        private readonly IRepository<TbmDiagnosisData, Guid> _diagnosisData; //复合判断设置细目

        private readonly IRepository<TjlCrisisSet, Guid> _crisisSetRepository; //危急值记录

        private readonly IRepository<TjlCustomerBarPrintInfo, Guid> _customerBarPrintInfoRepository; //条码记录

        private readonly IRepository<TbmItemGroup, Guid> _bmItemGroup; //条码记录

        private readonly IPictureAppService _pictureAppService;//图片
        private readonly IUserAppService _userAppService;

        private readonly IRepository<TjlClientInfo, Guid> _clientInfo; //单位信息


        private readonly IRepository<TjlCustomerSummarize, Guid> _customerSummarizeRepository;//总检建议
        private readonly ISqlExecutor _sqlExecutor;
        private readonly IRepository<TjlCusGiveUp, Guid> _tjlCusGiveUpRepository;
        private readonly IRepository<TdbInterfaceItemComparison, Guid> _interfaceItemComparison;
        private readonly IRepository<TdbInterfaceEmployeeComparison, Guid> _interfaceEmployeeComparison;
        private readonly IRepository<TjlCustomerQuestion, Guid> _CusQuestion;//问卷记录
        private readonly IRepository<TbmOneAddXQuestionnaire, Guid> _OneAddXQuestionnaire;//问卷记录
        private readonly IRepository<TbmItemProcExpress, Guid> _ItemProcExpress; //计算型公式
        private readonly IRepository<TbmSumHide, Guid> _TbmSumHide;
        private readonly IRepository<TbmCriticalSet, Guid> _TbmCriticalSet;//危急值设置
        private readonly ICommonAppService _commonAppService;

        /// <summary>
        /// 实现接口
        /// </summary>
        /// <param name="customerRegRepository">体检人预约登记信息表</param>
        /// <param name="userRepository">用户信息</param>
        /// <param name="itemDictionaryRepository">项目字典</param>
        /// <param name="customerItemGroupRepository">体检人组合项目</param>
        /// <param name="customerRegItemRepository">体检人项目</param>
        /// <param name="itemStandardRepository">项目参考值设置</param>
        /// <param name="receiptInfoRepository">收费结算表</param>
        /// <param name="basicDictionaryRepository">系统字典</param>
        /// <param name="customerDepSummaryRepository">系统字典</param>
        /// <param name="customerItemPicRepository">系统字典</param>
        /// <param name="itemInfoRepository">项目设置</param>
        /// <param name="summarizeAdviceRepository">收费结算表</param>
        /// <param name="customerSummary">专科建议</param>
        /// <param name="InterfaceItemComparison">项目对应</param>
        /// <param name="InterfaceEmployeeComparison">医生对应</param>
        /// <param name="departmentRepository">科室设置</param>
        /// <param name="diagnosis">复合判断设置</param>
        /// <param name="diagnosisData">复合判断设置细目</param>
        /// <param name="crisisSetRepository">危急值记录</param>
        public DoctorStationAppService(
            IRepository<TjlCustomerReg, Guid> customerRegRepository,
            IRepository<User, long> userRepository,
            IRepository<TbmItemDictionary, Guid> itemDictionaryRepository,
            IRepository<TjlCustomerItemGroup, Guid> customerItemGroupRepository,
            IRepository<TjlCustomerRegItem, Guid> customerRegItemRepository,
            IRepository<TbmItemStandard, Guid> itemStandardRepository,
            IRepository<TjlMReceiptInfo, Guid> receiptInfoRepository,
            IRepository<TbmBasicDictionary, Guid> basicDictionaryRepository,
            IRepository<TjlCustomerDepSummary, Guid> customerDepSummaryRepository,
            IRepository<TjlCustomerItemPic, Guid> customerItemPicRepository,
            IRepository<TbmItemInfo, Guid> itemInfoRepository,
            IRepository<TbmSummarizeAdvice, Guid> summarizeAdviceRepository,
            IInterfaceItemComparisonAppService InterfaceItemComparison,
            InterfaceEmployeeComparisonAppService InterfaceEmployeeComparison,
             IUserAppService userAppService,
            IRepository<TbmDepartment, Guid> departmentRepository,
            IRepository<TbmDiagnosis, Guid> diagnosis,
            IRepository<TbmDiagnosisData, Guid> diagnosisData,
             IRepository<TjlCrisisSet, Guid> crisisSetRepository,
             IPictureAppService pictureAppService,
               IRepository<TjlCustomerBarPrintInfo, Guid> customerBarPrintInfoRepository,
               ISqlExecutor sqlExecutor,
                IRepository<TbmItemGroup, Guid> bmItemGroup,
                IRepository<TjlClientInfo, Guid> clientInfo,
                IRepository<TjlCusGiveUp, Guid> tjlCusGiveUpRepository,
                IRepository<TdbInterfaceItemComparison, Guid> interfaceItemComparison,
                IRepository<TdbInterfaceEmployeeComparison, Guid> interfaceEmployeeComparison,
                IRepository<TjlCustomerQuestion, Guid> jlCusQuestion,
                IRepository<TbmOneAddXQuestionnaire, Guid> jlOneAddXQuestionnaire,
                            IRepository<TjlCustomerSummarize, Guid> customerSummarizeRepository,
                            IRepository<TbmItemProcExpress, Guid> ItemProcExpress,
                            IRepository<TbmSumHide, Guid> TbmSumHide,
                            ICommonAppService CommonAppService,
                            IRepository<TbmCriticalSet, Guid> TbmCriticalSet)
        {
            _customerRegRepository = customerRegRepository;
            _userRepository = userRepository;
            _itemDictionaryRepository = itemDictionaryRepository;
            _customerItemGroupRepository = customerItemGroupRepository;
            _customerRegItemRepository = customerRegItemRepository;
            _itemStandardRepository = itemStandardRepository;
            _basicDictionaryRepository = basicDictionaryRepository;
            _receiptInfoRepository = receiptInfoRepository;
            _customerDepSummaryRepository = customerDepSummaryRepository;
            _customerItemPicRepository = customerItemPicRepository;
            _itemInfoRepository = itemInfoRepository;
            _summarizeAdviceRepository = summarizeAdviceRepository;
            _InterfaceItemComparison = InterfaceItemComparison;
            _InterfaceEmployeeComparison = InterfaceEmployeeComparison;
            _departmentRepository = departmentRepository;
            _diagnosis = diagnosis;
            _diagnosisData = diagnosisData;
            _crisisSetRepository = crisisSetRepository;
            _pictureAppService = pictureAppService;
            _userAppService = userAppService;
            _customerBarPrintInfoRepository = customerBarPrintInfoRepository;
            _sqlExecutor = sqlExecutor;
            _bmItemGroup = bmItemGroup;
            _tjlCusGiveUpRepository = tjlCusGiveUpRepository;
            _interfaceItemComparison = interfaceItemComparison;
            _interfaceEmployeeComparison = interfaceEmployeeComparison;
            _clientInfo = clientInfo;
            _CusQuestion = jlCusQuestion;
            _OneAddXQuestionnaire = jlOneAddXQuestionnaire;
            _customerSummarizeRepository = customerSummarizeRepository;
            _ItemProcExpress = ItemProcExpress;
            _TbmSumHide = TbmSumHide;
            _commonAppService = CommonAppService;
            _TbmCriticalSet = TbmCriticalSet;
        }

        /// <summary>
        /// 获取当前登录人科室人数统计
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>f
        public ReturnClass GetReturnClass(QueryClassTwo Query)
        {
            var returnClass = new ReturnClass();
            var query = _customerItemGroupRepository.GetAll().AsNoTracking();
            query = query.Where(o => o.CustomerRegBM.LoginDate >= Query.LastModificationTimeBign &&
                                     o.CustomerRegBM.LoginDate <= Query.LastModificationTimeEnd);
            query = query.Where(r => Query.DepartmentBMList.Contains(r.DepartmentId));

            var result = query.Select(r => new { r.CheckState, r.CustomerRegBMId }).ToList();
            var a = result.Select(r => r.CustomerRegBMId).Distinct().Count();

            returnClass.AlreadyInspect = result.Where(r => r.CheckState == (int)ProjectIState.Complete)
                .Select(r => r.CustomerRegBMId).Distinct().Count();

            returnClass.NotInspect = result.Where(r => r.CheckState == (int)ProjectIState.Not)
                .Select(r => r.CustomerRegBMId).Distinct().Count();

            returnClass.AlreadyRegister = result.Select(r => r.CustomerRegBMId).Distinct().Count();
            return returnClass;
        }

        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerRegDto> GetCustomerRegList(QueryClass query)
        {
            var queryCustomerRegisters = _customerRegRepository.GetAll();
            queryCustomerRegisters = queryCustomerRegisters.Include(r => r.Customer);
            queryCustomerRegisters = queryCustomerRegisters.Include(r => r.ClientInfo);

            //查询码
            if (!string.IsNullOrWhiteSpace(query.WebQueryCode))
                queryCustomerRegisters = queryCustomerRegisters.Where(o => o.WebQueryCode == query.WebQueryCode);

            //体检号
            if (!string.IsNullOrWhiteSpace(query.CustomerBM))
            {
                queryCustomerRegisters = queryCustomerRegisters.Where(o => o.CustomerBM == query.CustomerBM);
            }
            if (query.RegisterState != null)
            {
                queryCustomerRegisters = queryCustomerRegisters.Where(o => o.RegisterState == query.RegisterState);
            }
            if (query.LastModificationTimeBign.HasValue)
            {
                queryCustomerRegisters = queryCustomerRegisters.Where(
                    o => o.LoginDate >= query.LastModificationTimeBign);
            }
            if (query.LastModificationTimeEnd.HasValue)
            {
                queryCustomerRegisters = queryCustomerRegisters.Where(
                    o => o.LoginDate <= query.LastModificationTimeEnd);
            }
            if (query.SummSate.HasValue)
            {
                queryCustomerRegisters = queryCustomerRegisters.Where(p =>
                 p.SummSate == query.SummSate);
            }
            if (query.CustomerItemGroupId.HasValue)
            {
                queryCustomerRegisters = queryCustomerRegisters.Where(p =>
                 p.CustomerItemGroup.Any(n => n.ItemGroupBM_Id == query.CustomerItemGroupId));
            }
            if (!string.IsNullOrWhiteSpace(query.CustomerName))
                queryCustomerRegisters = queryCustomerRegisters.Where(o => o.Customer.Name == query.CustomerBM);
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                queryCustomerRegisters = queryCustomerRegisters.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            var result = queryCustomerRegisters.ToList();

            return result.MapTo<List<ATjlCustomerRegDto>>();
        }

        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetCustomerRegListForSearch(QueryClass Query)
        {
            var CustomerReg = _customerRegRepository.GetAll()
                .Where(o => o.CustomerBM == Query.CustomerBM || o.Customer.Name == Query.CustomerName);
            return CustomerReg.MapTo<List<TjlCustomerRegForInspectionTotalDto>>();
        }

        /// <summary>
        /// 查询体检人体检项目
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupDto> GetCustomerItemGroup(QueryClass Query)
        {
            var dto = _customerItemGroupRepository.GetAll().Where(o =>
                o.CustomerRegBMId == Query.CustomerRegId && o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus && o.DepartmentBM.Category != "耗材");
            return dto.MapTo<List<ATjlCustomerItemGroupDto>>();
        }
        /// <summary>
        /// 查询体检人体检项目
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupDto> GetCustomerItemGroupByBm(QueryClass Query)
        {
            var dto = _customerItemGroupRepository.GetAll().Where(o =>
                o.CustomerRegBM.CustomerBM == Query.CustomerBM && o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus && o.DepartmentBM.Category != "耗材");
            return dto.MapTo<List<ATjlCustomerItemGroupDto>>();
        }
        /// <summary>
        /// 查询体检人体检项目（包含未收费）
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupDto> GetCustomerAllItemGroup(QueryClass Query)
        {
            var dto = _customerItemGroupRepository.GetAll().Where(o =>
                o.CustomerRegBMId == Query.CustomerRegId && o.IsAddMinus != (int)AddMinusType.Minus
                && o.DepartmentBM.Category != "耗材");
            return dto.MapTo<List<ATjlCustomerItemGroupDto>>();
        }
        /// <summary>
        /// 查询体检人图片
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerItemPicDto> GetCustomerItemPicDtos(QueryClass query)
        {
            var dto = _customerItemPicRepository.GetAll().AsNoTracking().Where(o => o.TjlCustomerRegID == query.CustomerRegId);
            return dto.MapTo<List<CustomerItemPicDto>>();
        }

        /// <summary>
        /// 查询体检人小结
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerDepSummaryViewDto> GetCustomerDepSummaries(QueryClass query)
        {
            var dto = _customerDepSummaryRepository.GetAll().Where(o => o.CustomerRegId == query.CustomerRegId);
            return dto.MapTo<List<CustomerDepSummaryViewDto>>();
        }

        /// <summary>
        /// 获取组合小结
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerItemGroupPrintViewDto> GetCustomerItemGroupPrintViewDtos(QueryClass query)
        {
            var dto = _customerItemGroupRepository.GetAll().
                Where(o => o.CustomerRegBMId == query.CustomerRegId
                && o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus
                && o.DepartmentBM.Category != "耗材").OrderBy(p => p.CheckState).ThenBy(p => p.DepartmentBM.OrderNum).ThenBy(p => p.ItemGroupBM.OrderNum);
            return dto.MapTo<List<CustomerItemGroupPrintViewDto>>();
        }

        /// <summary>
        /// 查询体检人体检项目包含科室信息和组合信息不包含结果信息
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupPrintGuidanceDto> GetATjlCustomerItemGroupPrintGuidanceDto(QueryClass Query)
        {
            var dto = _customerItemGroupRepository.GetAll().Where(o => o.CustomerRegBMId == Query.CustomerRegId).OrderBy(o => o.DepartmentBM.OrderNum).ThenBy(o => o.ItemGroupBM.OrderNum);
            var rist = dto.MapTo<List<ATjlCustomerItemGroupPrintGuidanceDto>>();

            return rist;
        }

        /// <summary>
        /// 获取人员体检记录
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetTjlCustomerRegDto(QueryClassTwo Query)
        {
            var dto = _customerRegRepository.GetAll().Where(o => o.Customer.Id == Query.CustomerId);
            return dto.MapTo<List<TjlCustomerRegForInspectionTotalDto>>();
        }

        /// <summary>
        /// 返回当前科室未体检的人员
        /// </summary>
        /// <param name="input">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForDoctorDto> GetSameDayCusTomerReg(QueryClassTwo input)

        {
            if (input.DepartmentBMList != null)
            {
                List<TjlCustomerRegForDoctorDto> ListDto = new List<TjlCustomerRegForDoctorDto>();

                //var customerList = _customerRegRepository.GetAll().AsNoTracking().Where(o =>
                //o.LoginDate >= input.LastModificationTimeBign &&
                //                     o.LoginDate <= input.LastModificationTimeEnd).ToList();
                //List<Guid?> GuidList = new List<Guid?>();
                //foreach (var itemcustomer in customerList)
                //{
                //    GuidList.Add(itemcustomer.Id);
                //}
                // var itemGroupDto = _customerItemGroupRepository.GetAll().AsNoTracking().Where(o =>
                //input.DepartmentBMList.Contains(o.DepartmentId) && GuidList.Contains(o.CustomerRegBMId)).ToList();
                // var customerRegs = itemGroupDto.Select(r => r.CustomerRegBM).Distinct().ToList();
                var customerRegs = new List<TjlCustomerReg>();
                //芜湖去掉姓名查询也按时间2022/8/2
                //if (!string.IsNullOrEmpty( input.Name) )
                ////else if(input.LastModificationTimeBign ==  Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00") && input.LastModificationTimeEnd == Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"))
                //{
                //    customerRegs = _customerRegRepository.GetAll().AsNoTracking().
                //       Where(o => ( o.Customer.Name.Contains(input.Name) ||  o.Customer.NameAB.ToUpper().Contains(input.Name.ToUpper()))
                //       && o.CustomerItemGroup.Any(n => input.DepartmentBMList.Contains(n.DepartmentId))
                //       ).ToList();
                //}
                //else
                //{
                    //检查日期
                    if (input.isJC.HasValue && input.isJC.Value == 1)
                    {
                        customerRegs = _customerRegRepository.GetAll().AsNoTracking().
                          Where(o => o.CustomerItemGroup.Any(n => n.FirstDateTime >= input.LastModificationTimeBign &&
                                             n.FirstDateTime <= input.LastModificationTimeEnd && n.IsAddMinus != (int)AddMinusType.Minus && input.DepartmentBMList.Contains(n.DepartmentId))).ToList();
                    }
                    //体检日期
                   else if (input.isJC.HasValue && input.isJC.Value == 2)
                    {
                        customerRegs = _customerRegRepository.GetAll().AsNoTracking().
                            Where(o => o.BookingDate >= input.LastModificationTimeBign &&
                                              o.BookingDate <= input.LastModificationTimeEnd &&
                         o.CustomerItemGroup.Any(n => n.IsAddMinus != (int)AddMinusType.Minus && input.DepartmentBMList.Contains(n.DepartmentId))).ToList();
                    }
                    //登记日期
                    else
                    {
                        customerRegs = _customerRegRepository.GetAll().AsNoTracking().
                           Where(o => o.LoginDate >= input.LastModificationTimeBign &&
                                             o.LoginDate <= input.LastModificationTimeEnd &&
                        o.CustomerItemGroup.Any(n => n.IsAddMinus != (int)AddMinusType.Minus && input.DepartmentBMList.Contains(n.DepartmentId))).ToList();
                    }
                //}

                var userBM = _userRepository.Get(AbpSession.UserId.Value);
                if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
                {
                    customerRegs = customerRegs.Where(p => p.HospitalArea == userBM.HospitalArea
                    || p.HospitalArea == null).ToList();
                }

                var setint = (int)Sex.GenderNotSpecified;

                if (input.Sex.HasValue && input.Sex != setint)
                {

                    customerRegs = customerRegs.Where(o => o.Customer.Sex == input.Sex).ToList();
                }
                if (input.ClientRegID.HasValue)
                {
                    customerRegs = customerRegs.Where(o => o.ClientRegId == input.ClientRegID).ToList();
                }
                if (input.PhysicalType.HasValue)
                {
                    customerRegs = customerRegs.Where(o => o.PhysicalType == input.PhysicalType).ToList();
                }
                if (input.CheckState.HasValue)
                {
                    //customerRegs = customerRegs.Where(o => o.CheckSate == input.CheckState).ToList();
                    //修改已检包括部分检查
                    if (input.CheckState == (int)ProjectIState.Complete)
                    {
                        customerRegs = customerRegs.Where(o => o.CustomerItemGroup.Any(p => p.IsAddMinus != (int)AddMinusType.Minus && input.DepartmentBMList.Contains(p.DepartmentId) 
                        && p.IsAddMinus!= (int)AddMinusType.Minus &&
                       (p.CheckState == input.CheckState|| p.CheckState == (int)ProjectIState.Part))).ToList();
                    }
                    else
                    {
                        customerRegs = customerRegs.Where(o => o.CustomerItemGroup.Any(p => p.IsAddMinus != (int)AddMinusType.Minus && input.DepartmentBMList.Contains(p.DepartmentId)
                         && p.IsAddMinus != (int)AddMinusType.Minus &&
                        p.CheckState == input.CheckState)).ToList();
                    }
                }
                if (!string.IsNullOrWhiteSpace(input.Name))
                    customerRegs = customerRegs.Where(r => r.Customer.Name.Contains(input.Name)|| r.Customer.NameAB.ToUpper().Contains(input.Name.ToUpper())).ToList();
                var result = customerRegs.MapTo<List<TjlCustomerRegForDoctorDto>>();

                return result;
            }

            return new List<TjlCustomerRegForDoctorDto>();
        }
        /// <summary>
        /// 项目不包括放弃
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerRegItemReoprtDto> GetTjlCustomerRegItemReoprtDtos(QueryClass query)
        {
            var RegItemDto = _customerRegItemRepository.GetAll().
                Where(p => p.CustomerRegId == query.CustomerRegId && p.CustomerItemGroupBM.IsAddMinus != (int)AddMinusType.Minus
                && p.CustomerItemGroupBM.PayerCat != (int)PayerCatType.NoCharge
                && p.CustomerItemGroupBM.CheckState != (int)ProjectIState.GiveUp
                && p.CustomerItemGroupBM.CheckState != (int)ProjectIState.Not);
            RegItemDto = RegItemDto.OrderBy(
                p => p.DepartmentBM.OrderNum).OrderBy(p => p.ItemGroupBM.OrderNum).OrderBy(
                p => p.ItemOrder);
            return RegItemDto.MapTo<List<TjlCustomerRegItemReoprtDto>>();
        }
        /// <summary>
        /// 包含放弃的项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerRegItemReoprtDto> GetTjlCustomerRegAllItemReoprtDtos(QueryClass query)
        {
            var RegItemDto = _customerRegItemRepository.GetAll().
                Where(p => p.CustomerRegId == query.CustomerRegId && p.CustomerItemGroupBM.IsAddMinus != (int)AddMinusType.Minus
                && p.CustomerItemGroupBM.PayerCat != (int)PayerCatType.NoCharge
                && p.CustomerItemGroupBM.CheckState != (int)ProjectIState.Not && p.ItemBM.Ckisrpot != 2);
            RegItemDto = RegItemDto.OrderBy(
                p => p.DepartmentBM.OrderNum).OrderBy(p => p.ItemGroupBM.OrderNum).OrderBy(
                p => p.ItemOrder);
            return RegItemDto.MapTo<List<TjlCustomerRegItemReoprtDto>>();
        }
        /// <summary>
        /// 获取两次原复查信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerRegSimpleViewDto> GetTjlCustomerRegRevew(QueryClass query)
        {
            List<CustomerRegSimpleViewDto> outCusReg = new List<CustomerRegSimpleViewDto>();
            var regcus = _customerRegRepository.Get( query.CustomerRegId.Value);
            if (regcus.ReviewRegID.HasValue)
            {
                 
                 var reCus1= _customerRegRepository.Get(regcus.ReviewRegID.Value);
                var cus1 = reCus1.MapTo<CustomerRegSimpleViewDto>();
                outCusReg.Add(cus1);
                if (reCus1.ReviewRegID.HasValue)
                {
                    var reCus2 = _customerRegRepository.Get(reCus1.ReviewRegID.Value);
                    var cus2 = reCus2.MapTo<CustomerRegSimpleViewDto>();
                    outCusReg.Add(cus2);
                }

            }
            return outCusReg;
        }
        /// <summary>
        /// 获取未体检完成的含有危急值的患者列表
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetCriticalValue(QueryClassTwo Query)
        {
            if (Query.DepartmentBMList != null)
            {
                var RegItemDto = _customerRegItemRepository.GetAll().Where(o =>
                    Query.DepartmentBMList.Contains(o.DepartmentId) && o.CrisisSate == Query.CrisisSate).ToList();
                var RegItemDto1 = RegItemDto.Where(o => o.CustomerItemGroupBM.CheckState == Query.CheckState).ToList();
                var guid = new List<Guid>();
                foreach (var item in RegItemDto1)
                    guid.Add(item.CustomerRegBM.Id);
                var RegDto = _customerRegRepository.GetAll().Where(o => guid.Contains(o.Id));
                return RegDto.MapTo<List<TjlCustomerRegForInspectionTotalDto>>();
            }

            return new List<TjlCustomerRegForInspectionTotalDto>();
        }
        /// <summary>
        /// 获取未体检完成的含有危急值的患者列表
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<CriticalCusDto> GetCriticalCusList(QueryClassTwo Query)
        {
            if (Query.DepartmentBMList != null)
            {
                var RegItemDto = _customerRegItemRepository.GetAll().Where(o =>
                    Query.DepartmentBMList.Contains(o.DepartmentId) && o.CrisisSate == Query.CrisisSate
                    && o.ProcessState == (int)ProjectIState.Complete && o.CustomerRegBM.SummSate != (int)SummSate.Audited).ToList();
                //var RegItemDto1 = RegItemDto.Where(o => o.CustomerItemGroupBM.CheckState == Query.CheckState).ToList();

                var RegDto = RegItemDto.GroupBy(p => new {
                    p.CustomerRegBM.Id,
                    p.CustomerRegBM.CustomerBM,
                    p.CustomerRegBM.Customer.Name,
                    p.CustomerRegBM.LoginDate
                }).Select(p => new CriticalCusDto
                {
                    Id = p.Key.Id,
                    体检号 = p.Key.CustomerBM,
                    危急值结果 = p.Select(n => new CriticalItemDto
                    {
                        Id = n.Id,
                        ItemID = n.ItemId,
                        分类 = n.CrisisLever,
                        结果 = n.ItemResultChar,
                        项目名称 = n.ItemName
                    }).ToList(),
                    姓名 = p.Key.Name,
                    登记时间 = p.Key.LoginDate,
                    状态 = p.First().CrisisVisitSate == null ? 0 : p.First().CrisisVisitSate
                }).ToList();

                return RegDto;
            }

            return new List<CriticalCusDto>();
        }
        /// <summary>
        /// 体检人组合项目状态修改并返回组合信息
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        public ATjlCustomerItemGroupDto GiveUpCheckItemGroup(UpdateClass Update)
        {
            var Group = _customerItemGroupRepository.GetAll().FirstOrDefault(o => o.Id == Update.CustomerItemGroupId);
            if (Group != null && Group.Id != Guid.Empty)
            {
                Group.CheckState = Update.CheckState;
                foreach (var item in Group.CustomerRegItem)
                    item.ProcessState = Update.ProcessState;
                _customerItemGroupRepository.Update(Group);
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                return new ATjlCustomerItemGroupDto();
            }
            if (Update.CheckState == (int)ProjectIState.GiveUp)
            {
                if (!_customerItemGroupRepository.GetAll().Any(o => o.CustomerRegBMId == Group.CustomerRegBMId
                 && o.CheckState != (int)ProjectIState.Complete && o.CheckState != (int)ProjectIState.Part && o.CheckState != (int)ProjectIState.GiveUp &&
                         o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus))
                {
                    var customer = _customerRegRepository.FirstOrDefault(o => o.Id == Group.CustomerRegBMId);
                    customer.CheckSate = (int)PhysicalEState.Complete;
                    _customerRegRepository.Update(customer);

                }
                //else
                //{
                //    var customer = _customerRegRepository.FirstOrDefault(o => o.Id == Group.CustomerRegBMId);
                //    customer.CheckSate = (int)PhysicalEState.Process;
                //    _customerRegRepository.Update(customer);
                //}
            }

            return Group.MapTo<ATjlCustomerItemGroupDto>();
        }

        /// <summary>
        /// 单独项目修改状态并返回组合信息
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        public ATjlCustomerItemGroupDto GiveUpCheckItem(UpdateClass Update)
        {
            var item = _customerRegItemRepository.GetAll().FirstOrDefault(o => o.Id == Update.CustomerItemId);
            item.ProcessState = Update.ProcessState;
            _customerRegItemRepository.Update(item);
            var groupId = item.ItemGroupBM.Id;
            //var Group = _customerItemGroupRepository.GetAll().Where(o => o.Id == groupId);
            var Group = _customerItemGroupRepository.GetAll().FirstOrDefault(o => o.Id == item.CustomerItemGroupBMid);
            return Group.MapTo< ATjlCustomerItemGroupDto>();
        }

        /// <summary>
        /// 保存项目信息
        /// </summary>
        /// <param name="Input">组合集合</param>
        /// <returns></returns>
        public bool UpdateInspectionProject(List<UpdateCustomerItemGroupDto> Input)
        {
            //预约人id
            var CustomerRegId = Guid.Empty;
            List<Guid> HCGroupIDs = new List<Guid>();
            List<Guid> HCItemIDs = new List<Guid>();
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            List<string> logDept = new List<string>();
            var HCIITems = _itemInfoRepository.GetAll().Where(p => p.ItemInfos.Count > 0).ToList();

            foreach (var itemGroup in Input)
            {
                var GroupDt =
                    _customerItemGroupRepository.Get(itemGroup.Id); //.FirstOrDefault(o => o.Id == itemGroup.Id);
                GroupDt.CheckState = itemGroup.CheckState; //状态
                if (!logDept.Contains(GroupDt.DepartmentName))
                {
                    logDept.Add(GroupDt.DepartmentName);
                }
                if (itemGroup.BillingEmployeeBMId.HasValue)
                {
                    GroupDt.BillingEmployeeBM = null;
                       GroupDt.BillingEmployeeBMId = itemGroup.BillingEmployeeBMId; //开单医生
                    //GroupDt.BillingEmployeeBM = _userRepository.Get(itemGroup.BillingEmployeeBMId.Value);
                }
                if (itemGroup.InspectEmployeeBMId.HasValue)
                {
                    GroupDt.InspectEmployeeBM = null;
                    GroupDt.InspectEmployeeBMId = itemGroup.InspectEmployeeBMId; //检查人
                    //GroupDt.InspectEmployeeBM = _userRepository.Get(itemGroup.InspectEmployeeBMId.Value);
                }
                if (itemGroup.CheckEmployeeBMId.HasValue)
                {
                    GroupDt.CheckEmployeeBM = null;
                       GroupDt.CheckEmployeeBMId = itemGroup.CheckEmployeeBMId; //审核人
                    //GroupDt.CheckEmployeeBM = _userRepository.Get(itemGroup.CheckEmployeeBMId.Value);
                }
                //GroupDt.BillingEmployeeBMId = itemGroup.BillingEmployeeBMId; //开单医生
                //GroupDt.InspectEmployeeBMId = itemGroup.InspectEmployeeBMId; //检查人
                //GroupDt.CheckEmployeeBMId = itemGroup.CheckEmployeeBMId; //审核人

                if (!GroupDt.FirstDateTime.HasValue)
                    GroupDt.FirstDateTime = itemGroup.FirstDateTime; //第一次检查时间
                _customerItemGroupRepository.Update(GroupDt);
                CustomerRegId = Guid.Parse(GroupDt.CustomerRegBMId.ToString());
                //var GroupDto = _customerItemGroupRepository.FirstOrDefault(o => o.Id == itemGroup.Id);

                foreach (var item in itemGroup.CustomerRegItem)
                {
                    var ItemSe = _customerRegItemRepository.Get(item.Id); //.FirstOrDefault(o => o.Id == item.Id);
                    ItemSe.ItemResultChar = item.ItemResultChar; //结果
                    ItemSe.ItemDiagnosis = item.ItemDiagnosis;//增加诊断
                    if (item.InspectEmployeeBMId.HasValue)
                    {
                        ItemSe.InspectEmployeeBM = null;
                           ItemSe.InspectEmployeeBMId = item.InspectEmployeeBMId; //检查医生
                        //ItemSe.InspectEmployeeBM = _userRepository.Get(item.InspectEmployeeBMId.Value);
                    }
                    if (item.CheckEmployeeBMId.HasValue)
                    {
                        ItemSe.CheckEmployeeBM = null;
                           ItemSe.CheckEmployeeBMId = item.CheckEmployeeBMId; //审核人
                        //ItemSe.CheckEmployeeBM = _userRepository.Get(item.CheckEmployeeBMId.Value);
                    }
                    ItemSe.ProcessState = item.ProcessState; //状态
                    ItemSe.Stand = item.Stand;
                    ItemSe.Unit = item.Unit;
                    if (item.StandFrom.HasValue)
                    { ItemSe.StandFrom = item.StandFrom; }
                    _customerRegItemRepository.Update(ItemSe);
                    if (HCIITems != null && HCIITems.Count > 0)
                    {
                        var iteminfo = HCIITems.FirstOrDefault(p => p.Id == item.ItemId);
                        if (iteminfo != null)
                        {
                            var itemids = iteminfo.ItemInfos.Select(o => o.Id).ToList();
                            foreach (Guid itemid in itemids)
                            {
                                if (!HCItemIDs.Contains(itemid))
                                {
                                    HCItemIDs.Add(itemid);
                                }
                            }
                            if (!HCGroupIDs.Contains(GroupDt.ItemGroupBM_Id.Value))
                            {
                                HCGroupIDs.Add(GroupDt.ItemGroupBM_Id.Value);
                            }
                        }
                    }
                }

            }
            //var ss = _customerRegItemRepository.GetAll().Where(o => o.CustomerRegId == CustomerRegId && HCItemIDs.Contains(o.ItemId)).ToList();
            // _customerRegItemRepository.GetAll().Where(o => o.CustomerRegId == CustomerRegId && HCItemIDs.Contains(o.ItemId)).Update(o => new TjlCustomerRegItem { ProcessState = (int)ProjectIState.Complete });
            //互斥项目处理
            foreach (Guid imid in HCItemIDs)
            {
                var chitem = _customerRegItemRepository.FirstOrDefault(o => o.CustomerRegId == CustomerRegId && o.ItemId == imid);
                if (chitem != null)
                {
                    chitem.ProcessState = (int)ProjectIState.Complete;
                    _customerRegItemRepository.Update(chitem);

                }
            }
            CurrentUnitOfWork.SaveChanges();
            if (HCGroupIDs.Count > 0)
            {
                foreach (Guid id in HCGroupIDs)
                {
                    var lis = _customerRegItemRepository.GetAll().Where(o => o.CustomerRegId == CustomerRegId && o.ItemGroupBMId == id && o.ProcessState == (int)ProjectIState.Not).ToList();
                    if (lis.Count() == 0)
                    {
                        var GroupDt =
                   _customerItemGroupRepository.FirstOrDefault(o => o.CustomerRegBMId == CustomerRegId && o.ItemGroupBM_Id == id);
                        GroupDt.CheckState = (int)ProjectIState.Complete;
                        _customerItemGroupRepository.Update(GroupDt);

                    }
                }
            }

            var reg = _customerRegRepository.FirstOrDefault(o => o.Id == CustomerRegId);
            if (!_customerItemGroupRepository.GetAll().Any(o =>
                    o.CustomerRegBMId == reg.Id && o.CheckState != (int)ProjectIState.Complete
                    && o.CheckState != (int)ProjectIState.Part &&
                    o.CheckState != (int)ProjectIState.GiveUp &&
                    o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus
                    && o.DepartmentBM.Category != "耗材"))
            {
                //reg.CheckSate = (int)PhysicalEState.Complete;
                //参数控制部分检查不是体检完成

                var isBF = _basicDictionaryRepository.FirstOrDefault(o => o.Type == BasicDictionaryType.DepatSumSet.ToString() && o.Value == 3 )?.Remarks;
                if (!string.IsNullOrEmpty(isBF) && isBF == "Y" &&
                    _customerItemGroupRepository.GetAll().Any(o => o.CustomerRegBMId == reg.Id && o.CheckState == (int)ProjectIState.Part &&
                    o.IsAddMinus != (int)AddMinusType.Minus && o.DepartmentBM.Category != "耗材"))
                {

                    reg.CheckSate = (int)PhysicalEState.Process;
                }
                else
                {
                    reg.CheckSate = (int)PhysicalEState.Complete;
                }

                _customerRegRepository.Update(reg);

            }
            else
            {

                //修改体检人成为体检中状态
                //if (reg.CheckSate == (int)PhysicalEState.Not)
                //{
                reg.CheckSate = (int)PhysicalEState.Process;
                _customerRegRepository.Update(reg);
            }
            //}

            //添加操作日志          
            createOpLogDto.LogBM = reg.CustomerBM;
            createOpLogDto.LogName = reg.Customer.Name;
            string det = "";
            if (logDept.Count > 0)
            {
                det = string.Join(",", logDept).TrimEnd(',');
            }
            createOpLogDto.LogText = "保存：" + det + "结果";
            createOpLogDto.LogType = (int)LogsTypes.CheckId;
            createOpLogDto.LogDetail = "";
            _commonAppService.SaveOpLog(createOpLogDto);
            return true;
        }
        /// <summary>
        /// 保存项目结果
        /// </summary>
        /// <param name="Input">项目组合信息</param>
        public bool UpdateSectionSummary(List<ATjlCustomerItemGroupDto> Input)
        {

        
            foreach (var itemGroup in Input)
            {
                //var FirstGroup = _customerItemGroupRepository.FirstOrDefault(o => o.Id == itemGroup.Id);
                //FirstGroup.ItemGroupSum = itemGroup.ItemGroupSum;
                //_customerItemGroupRepository.Update(FirstGroup);

                foreach (var item in itemGroup.CustomerRegItem)
                {

                    var FirstItem = _customerRegItemRepository.FirstOrDefault(o => o.Id == item.Id);
                    if (FirstItem.CrisisVisitSate == 3)
                    {
                        continue;
                    }
                    FirstItem.Symbol = item.Symbol;
                    FirstItem.ItemSum = item.ItemSum;
                    FirstItem.CrisisSate = item.CrisisSate;
                    FirstItem.ItemDiagnosis = item.ItemDiagnosis;
                    FirstItem.PositiveSate = item.PositiveSate;
                    FirstItem.IllnessLevel = item.IllnessLevel;
                    if (FirstItem.CrisisSate == (int)CrisisSate.Normal)
                    {
                        FirstItem.CrisisLever = null;

                        FirstItem.CrisiChar = "";
                    }
                    if (item.CrisisSate == (int)CrisisSate.Abnormal)
                    {
                        TjlCrisisSet Crisis = new TjlCrisisSet();
                        Crisis.Id = Guid.NewGuid();
                        Crisis.TjlCustomerRegItemId = item.Id;
                        Crisis.MsgState = (int)CrisisMsgStatecs.Not;
                        Crisis.CrisisType = (int)CrisisType.SystemCreate;
                        Crisis.CallBackState = 0;
                        _crisisSetRepository.Insert(Crisis);
                    }
                    #region 危急值处理
                    var cricalset = _TbmCriticalSet.GetAll().Where(p => p.ItemInfoId == FirstItem.ItemId).ToList();
                    if (cricalset.Count > 0  )
                    {
                        bool isWJZ = false;
                        foreach (var cset in cricalset)
                        {
                            //性别过滤
                            if (cset.Sex.HasValue && cset.Sex != (int)Sex.GenderNotSpecified && cset.Sex != FirstItem.CustomerRegBM?.Customer?.Sex)
                            {
                                continue;
                            }
                            //往年结果
                            if (cset.Old.HasValue && cset.Old == 1)
                            {
                                var isOldOk = isOld(FirstItem.CustomerRegBM.Id, FirstItem.ItemId, FirstItem.ItemBM?.ItemBM, cset);
                                if (isOldOk == false)
                                {
                                    continue;
                                }
                            }
                            if (cset.CalculationType == (int)CalculationTypeState.Numerical && decimal.TryParse(FirstItem.ItemResultChar, out decimal ValueNum))
                            {
                                string strSymbol = FirstItem.Symbol;
                                switch (cset.Operator)
                                {
                                    case (int)OperatorState.Big:
                                        if (ValueNum > cset.ValueNum)
                                        {
                                            isWJZ = true;
                                            strSymbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);
                                        }
                                        break;
                                    case (int)OperatorState.BigEqual:
                                        if (ValueNum >= cset.ValueNum)
                                        {
                                            isWJZ = true;
                                            strSymbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);
                                        }
                                        break;
                                    case (int)OperatorState.Small:
                                        if (ValueNum < cset.ValueNum)
                                        {
                                            isWJZ = true;
                                            strSymbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);
                                        }
                                        break;
                                    case (int)OperatorState.SmallEqual:
                                        if (ValueNum <= cset.ValueNum)
                                        {
                                            isWJZ = true;
                                            strSymbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);
                                        }
                                        break;
                                    case (int)OperatorState.Equal:
                                        if (ValueNum == cset.ValueNum)
                                        {
                                            isWJZ = true;
                                            strSymbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);
                                        }
                                        break;
                                }
                                if (isWJZ == true)
                                {
                                    var isOK = true;
                                    var CriticalDetails = cset.CriticalDetails;
                                    if (CriticalDetails != null && CriticalDetails.Count > 0)
                                    {
                                        //并且所有项目结果都满足要求
                                         
                                          isOK = isZYYC(FirstItem.CustomerRegBM.Id, CriticalDetails.ToList());
                                    }
                                    if (isOK)
                                    {
                                        FirstItem.Symbol = strSymbol;
                                        //换双箭头
                                        if (strSymbol == SymbolHelper.SymbolFormatter(Symbol.Superhigh) && !FirstItem.ItemDiagnosis.Contains("↑↑"))
                                        {
                                            FirstItem.ItemSum = FirstItem.ItemSum.Replace("↑", "↑↑");
                                            FirstItem.ItemDiagnosis = FirstItem.ItemDiagnosis.Replace("↑", "↑↑");
                                        }
                                        else if (strSymbol == SymbolHelper.SymbolFormatter(Symbol.UltraLow) && !FirstItem.ItemDiagnosis.Contains("↓↓"))
                                        {
                                            FirstItem.ItemSum = FirstItem.ItemSum.Replace("↓", "↓↓");
                                            FirstItem.ItemDiagnosis = FirstItem.ItemDiagnosis.Replace("↓", "↓↓");
                                        }

                                        FirstItem.CrisisSate = (int)CrisisSate.Abnormal;
                                        FirstItem.CrisisLever = cset.CriticalType;
                                        FirstItem.CrisiChar = cset.ValueChar;
                                        FirstItem.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                                    }
                                }

                            }
                            else if (cset.CalculationType == (int)CalculationTypeState.Text)
                            {
                                if (FirstItem!=null && FirstItem.ItemResultChar!=null && FirstItem.ItemResultChar.Contains(cset.ValueChar))
                                {
                                    if (cset.ValueNum.HasValue && cset.ValueNum!=0)
                                    {
                                        #region 获取文本中的数值
                                        string str = FirstItem.ItemResultChar;
                                        Regex reg = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");
                                        MatchCollection mc = reg.Matches(str);//设定要查找的字符串
                                        List<string> strlist = new List<string>();
                                        foreach (Match m in mc)
                                        {
                                            string s = m.Groups[0].Value;
                                            strlist.Add(s);
                                            str = str.Replace(s, "");
                                        }


                                        Regex reg1 = new Regex(@"[0-9]+");//2秒后超时
                                        MatchCollection mc1 = reg1.Matches(str);//设定要查找的字符串
                                        foreach (Match m in mc1)
                                        {
                                            string s = m.Groups[0].Value;
                                            strlist.Add(s);
                                        }

                                        foreach (var charNum in strlist)
                                        {
                                            var trrNum = decimal.Parse(charNum);
                                            switch (cset.Operator)
                                            {
                                                case (int)OperatorState.Big:
                                                    if (trrNum > cset.ValueNum)
                                                    {
                                                        isWJZ = true;
                                                        FirstItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);
                                                    }
                                                    break;
                                                case (int)OperatorState.BigEqual:
                                                    if (trrNum >= cset.ValueNum)
                                                    {
                                                        isWJZ = true;
                                                        FirstItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);
                                                    }
                                                    break;
                                                case (int)OperatorState.Small:
                                                    if (trrNum < cset.ValueNum)
                                                    {
                                                        isWJZ = true;
                                                        FirstItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                                    }
                                                    break;
                                                case (int)OperatorState.SmallEqual:
                                                    if (trrNum <= cset.ValueNum)
                                                    {
                                                        isWJZ = true;
                                                        FirstItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                                    }
                                                    break;
                                                case (int)OperatorState.Equal:
                                                    if (trrNum == cset.ValueNum)
                                                    {
                                                        isWJZ = true;
                                                        FirstItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                                    }
                                                    break;
                                            }
                                            if (isWJZ == true)
                                            {  var isOK = true;
                                                var CriticalDetails = cset.CriticalDetails;
                                                if (CriticalDetails != null && CriticalDetails.Count > 0)
                                                {
                                                    //并且所有项目结果都满足要求
                                                    isOK = isZYYC(FirstItem.CustomerRegBM.Id, CriticalDetails.ToList());
                                                }
                                                if (isOK)
                                                {
                                                    FirstItem.CrisisSate = (int)CrisisSate.Abnormal;
                                                    FirstItem.CrisisLever = cset.CriticalType;
                                                    FirstItem.CrisiChar = cset.ValueChar;
                                                }
                                            }

                                        }
                                        #endregion



                                    }
                                    else
                                    {
                                        var isOK = true;
                                        var CriticalDetails = cset.CriticalDetails;
                                        if (CriticalDetails != null && CriticalDetails.Count > 0)
                                        {
                                            //并且所有项目结果都满足要求
                                            isOK = isZYYC(FirstItem.CustomerRegBM.Id, CriticalDetails.ToList());
                                        }
                                        if (isOK)
                                        {
                                            isWJZ = true;
                                            FirstItem.CrisisSate = (int)CrisisSate.Abnormal;
                                            FirstItem.CrisisLever = cset.CriticalType;
                                            FirstItem.CrisiChar = cset.ValueChar;
                                        }
                                    }
                                }
                            }
                            if (isWJZ == true)
                            {
                                break;

                            }
                        }
                    }
                    #endregion

                    _customerRegItemRepository.Update(FirstItem);
                }
            }

            return true;
        }
        private bool isOld(Guid regId,Guid ItemId,string ItemBM, TbmCriticalSet cset)
        {
            //获取本库历史数据
            var newreg = _customerRegRepository.Get(regId);
            var OldCusItemChar = _customerRegItemRepository.GetAll().Where(p=>p.CustomerRegId!= regId 
            && p.CustomerRegBM.CustomerId== newreg.CustomerId && p.ItemId== ItemId  
            && p.ProcessState==2)?.OrderByDescending(p=>p.CustomerRegBM.LoginDate)?.FirstOrDefault()?.ItemResultChar;
            if (OldCusItemChar == null)
            {
               

                //开启获取第三方库
                var oderHisITem = _basicDictionaryRepository.GetAll().FirstOrDefault(p => p.Type ==
                 BasicDictionaryType.PresentationSet.ToString() && p.Value == 102);
                if (oderHisITem != null && oderHisITem.Remarks == "Y" && !string.IsNullOrEmpty(newreg.Customer?.IDCardNo))
                {
                    // 获取接口数据
                    var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

                    var search =new   Sw.Hospital.HealthExamination.Drivers.Models.HisInterface.SearchHisClassDto();
                    search.IDCardNo = newreg.Customer?.IDCardNo;
                    search.ItemBM = ItemBM;
                    var interfaceResult = hisInterfaceDriver.GetHistoryResult(search);
                    var chetime = interfaceResult.FirstOrDefault();
                    if (chetime!=null)
                    {
                        OldCusItemChar = chetime.ItemValue;
                    }

                }
               
            }
            bool isWJZ = false;
            if (!string.IsNullOrEmpty(OldCusItemChar))
            {
                
                if (decimal.TryParse(OldCusItemChar, out decimal ValueNum))
                {

                    switch (cset.Operator)
                    {
                        case (int)OperatorState.Big:
                            if (ValueNum > cset.ValueNum)
                            {
                                isWJZ = true;

                            }
                            break;
                        case (int)OperatorState.BigEqual:
                            if (ValueNum >= cset.ValueNum)
                            {
                                isWJZ = true;

                            }
                            break;
                        case (int)OperatorState.Small:
                            if (ValueNum < cset.ValueNum)
                            {
                                isWJZ = true;

                            }
                            break;
                        case (int)OperatorState.SmallEqual:
                            if (ValueNum <= cset.ValueNum)
                            {
                                isWJZ = true;

                            }
                            break;
                        case (int)OperatorState.Equal:
                            if (ValueNum == cset.ValueNum)
                            {
                                isWJZ = true;

                            }
                            break;
                    }

                }
                else
                {
                    if (OldCusItemChar != null && OldCusItemChar.Contains(cset.ValueChar))
                    {
                        if (cset.ValueNum.HasValue && cset.ValueNum != 0)
                        {
                            #region 获取文本中的数值
                            string str = OldCusItemChar;
                            Regex reg = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");
                            MatchCollection mc = reg.Matches(str);//设定要查找的字符串
                            List<string> strlist = new List<string>();
                            foreach (Match m in mc)
                            {
                                string s = m.Groups[0].Value;
                                strlist.Add(s);
                                str = str.Replace(s, "");
                            }


                            Regex reg1 = new Regex(@"[0-9]+");//2秒后超时
                            MatchCollection mc1 = reg1.Matches(str);//设定要查找的字符串
                            foreach (Match m in mc1)
                            {
                                string s = m.Groups[0].Value;
                                strlist.Add(s);
                            }

                            foreach (var charNum in strlist)
                            {
                                var trrNum = decimal.Parse(charNum);
                                switch (cset.Operator)
                                {
                                    case (int)OperatorState.Big:
                                        if (trrNum > cset.ValueNum)
                                        {
                                            isWJZ = true;

                                        }
                                        break;
                                    case (int)OperatorState.BigEqual:
                                        if (trrNum >= cset.ValueNum)
                                        {
                                            isWJZ = true;

                                        }
                                        break;
                                    case (int)OperatorState.Small:
                                        if (trrNum < cset.ValueNum)
                                        {
                                            isWJZ = true;


                                        }
                                        break;
                                    case (int)OperatorState.SmallEqual:
                                        if (trrNum <= cset.ValueNum)
                                        {
                                            isWJZ = true;


                                        }
                                        break;
                                    case (int)OperatorState.Equal:
                                        if (trrNum == cset.ValueNum)
                                        {
                                            isWJZ = true;


                                        }
                                        break;
                                }


                            }
                            #endregion
                        }
                        else
                        { isWJZ = true; }

                    }
                }
            }
            return isWJZ;
        }
        private bool isZYYC(Guid regId, List<TbmCriticalDetail> CriticalDetails)
        {
            bool isWJZ = false;
            var cusItem = _customerRegItemRepository.GetAll().Where(p=>p.CustomerRegId== regId);
            foreach (var CriticalDetail in CriticalDetails)
            {
                #region MyRegion

                foreach (var item in cusItem)
                {
                    if (item.ItemId != CriticalDetail.ItemInfoId)
                    {
                        continue;
                    }
                    var FirstItem = _customerRegItemRepository.FirstOrDefault(o => o.Id == item.Id);
                    if (FirstItem.CrisisVisitSate == 3)
                    {
                        continue;
                    }
                    #region 危急值处理
                    foreach (var cset in CriticalDetails)
                    {
                        if (decimal.TryParse(FirstItem.ItemResultChar, out decimal ValueNum))
                        {
                            string strSymbol = FirstItem.Symbol;
                            switch (cset.Operator)
                            {
                                case (int)OperatorState.Big:
                                    if (ValueNum > cset.ValueNum)
                                    {
                                        isWJZ = true;

                                    }
                                    break;
                                case (int)OperatorState.BigEqual:
                                    if (ValueNum >= cset.ValueNum)
                                    {
                                        isWJZ = true;

                                    }
                                    break;
                                case (int)OperatorState.Small:
                                    if (ValueNum < cset.ValueNum)
                                    {
                                        isWJZ = true;

                                    }
                                    break;
                                case (int)OperatorState.SmallEqual:
                                    if (ValueNum <= cset.ValueNum)
                                    {
                                        isWJZ = true;

                                    }
                                    break;
                                case (int)OperatorState.Equal:
                                    if (ValueNum == cset.ValueNum)
                                    {
                                        isWJZ = true;

                                    }
                                    break;
                            }

                        }
                        else
                        {
                            if (FirstItem != null && FirstItem.ItemResultChar != null && FirstItem.ItemResultChar.Contains(cset.ValueChar))
                            {
                                if (cset.ValueNum.HasValue && cset.ValueNum != 0)
                                {
                                    #region 获取文本中的数值
                                    string str = FirstItem.ItemResultChar;
                                    Regex reg = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");
                                    MatchCollection mc = reg.Matches(str);//设定要查找的字符串
                                    List<string> strlist = new List<string>();
                                    foreach (Match m in mc)
                                    {
                                        string s = m.Groups[0].Value;
                                        strlist.Add(s);
                                        str = str.Replace(s, "");
                                    }


                                    Regex reg1 = new Regex(@"[0-9]+");//2秒后超时
                                    MatchCollection mc1 = reg1.Matches(str);//设定要查找的字符串
                                    foreach (Match m in mc1)
                                    {
                                        string s = m.Groups[0].Value;
                                        strlist.Add(s);
                                    }

                                    foreach (var charNum in strlist)
                                    {
                                        var trrNum = decimal.Parse(charNum);
                                        switch (cset.Operator)
                                        {
                                            case (int)OperatorState.Big:
                                                if (trrNum > cset.ValueNum)
                                                {
                                                    isWJZ = true;

                                                }
                                                break;
                                            case (int)OperatorState.BigEqual:
                                                if (trrNum >= cset.ValueNum)
                                                {
                                                    isWJZ = true;

                                                }
                                                break;
                                            case (int)OperatorState.Small:
                                                if (trrNum < cset.ValueNum)
                                                {
                                                    isWJZ = true;


                                                }
                                                break;
                                            case (int)OperatorState.SmallEqual:
                                                if (trrNum <= cset.ValueNum)
                                                {
                                                    isWJZ = true;


                                                }
                                                break;
                                            case (int)OperatorState.Equal:
                                                if (trrNum == cset.ValueNum)
                                                {
                                                    isWJZ = true;


                                                }
                                                break;
                                        }
                                        if (isWJZ == true)
                                        {

                                        }

                                    }
                                    #endregion



                                }
                                else
                                { isWJZ = true; }

                            }
                        }
                        if (isWJZ == false)
                        {
                            break;

                        }
                    }

                    #endregion


                }
                
                #endregion
            }
            return isWJZ;
        }
        /// <summary>
        /// 保存科室小结
        /// </summary>
        /// <param name="Input">科室小结</param>
        /// <returns></returns>
        public bool InsertCustomerDepSummary(CreateCustomerDepSummary Input)
        {
            var Dep = _customerDepSummaryRepository.FirstOrDefault(o =>
                o.CustomerRegId == Input.CustomerRegId && o.DepartmentBMId == Input.DepartmentBMId);
            if (Dep != null)
            {
                Input.Id = Dep.Id;
                var input = Input.MapTo(Dep);
                _customerDepSummaryRepository.Update(input);
            }
            else
            {
                var input = Input.MapTo<TjlCustomerDepSummary>();
                input.Id = Guid.NewGuid();
                _customerDepSummaryRepository.Insert(input);
            }
            return true;
        }
        /// <summary>
        /// 获取科室小结
        /// </summary>
        /// <param name="Input">科室小结</param>
        /// <returns></returns>
        public List<ATjlCustomerDepSummaryDto> GetCustomerDepSummary(QueryClass Input)
        {
            var Dto = _customerDepSummaryRepository.GetAll().Where(o => o.CustomerRegId == Input.CustomerRegId);
            return Dto.MapTo<List<ATjlCustomerDepSummaryDto>>();
        }
        ///// <summary>
        ///// 保存专科建议
        ///// </summary>
        ///// <param name="Input">专科建议</param>
        ///// <returns></returns>
        //public bool InsertCustomerSummary(SearchTjlCustomerSummaryDto Input)
        //{
        //    var Cus = _customerSummary.FirstOrDefault(o =>
        //    o.CustomerRegBMId == Input.CustomerRegBMId && o.DepartmentBMId == Input.DepartmentBMId);
        //    if (Cus != null)
        //    {
        //        Input.Id = Cus.Id;
        //        var input = Input.MapTo(Cus);
        //        _customerSummary.Update(input);
        //    }
        //    else
        //    {
        //        var input = Input.MapTo<TjlCustomerSummary>();
        //        input.Id = Guid.NewGuid();
        //        _customerSummary.Insert(input);
        //    }
        //    return true;
        //}
        ///// <summary>
        ///// 保存专科建议
        ///// </summary>
        ///// <param name="Input">专科建议</param>
        ///// <returns></returns>
        //public List<SearchTjlCustomerSummaryDto> GetCustomerSummary(QueryClass Input)
        //{
        //    var Dto = _customerSummary.GetAll().Where(o => o.CustomerRegBMId == Input.CustomerRegId);
        //    return Dto.MapTo<List<SearchTjlCustomerSummaryDto>>();
        //}
        /// <summary>
        /// 修改小结
        /// </summary>
        /// <param name="Update">修改类（可通用）</param>
        /// <returns></returns>
        public bool UpdateCustomerDepSummary(UpdateClass Update)
        {
            var DepSummary = _customerDepSummaryRepository.FirstOrDefault(o =>
                o.CustomerRegId == Update.CustomerRegId && o.DepartmentBMId == Update.DepartmentBM);
            if (DepSummary == null)
                throw new FieldVerifyException("当前还未生成小结！", "当前还未生成小结！");

            DepSummary.CharacterSummary = Update.CharacterSummary;
            DepSummary.DagnosisSummary = Update.DagnosisSummary;
            _customerDepSummaryRepository.Update(DepSummary);

            return true;
        }

        /// <summary>
        /// 清除当前用户科室下当前人员体检信息
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public bool DeleteCustomerInspectInformation(QueryClassTwo Query)
        {
            foreach (var itemDepartment in Query.DepartmentBMList)
            {
                //修改项目组合和项目结果
                var Group = _customerItemGroupRepository.GetAll()
                    .Where(o => o.CustomerRegBMId == Query.CustomerId && o.DepartmentId == itemDepartment).ToList();
                foreach (var itemGroup in Group)
                {
                    //组合状态
                    itemGroup.CheckState = Query.CheckState;

                    //开单医生
                    itemGroup.BillingEmployeeBMId = null;

                    //检查人
                    itemGroup.InspectEmployeeBMId = null;

                    //审核人
                    itemGroup.CheckEmployeeBMId = null;

                    //组合小结
                    itemGroup.ItemGroupSum = string.Empty;

                    //组合小结
                    itemGroup.ItemGroupDiagnosis = string.Empty;

                    itemGroup.ItemGroupOriginalDiag = string.Empty;

                    foreach (var item in itemGroup.CustomerRegItem)
                    {
                        //状态
                        item.ProcessState = Query.ProcessState;

                        //结果
                        item.ItemResultChar = string.Empty;

                        //项目标示
                        item.Symbol = string.Empty;

                        //阳性状态
                        item.PositiveSate = null;

                        //疾病状态
                        item.IllnessSate = null;

                        //危急值
                        item.CrisisSate = null;

                        //小结
                        item.ItemSum = null;
                        //单位
                        item.Stand = null;

                        //小结
                        item.ItemDiagnosis = null;
                        var itemPic = _customerItemPicRepository.FirstOrDefault(o => o.ItemBMID == item.Id);
                        if (itemPic != null)
                        {
                            _customerItemPicRepository.Delete(itemPic);
                        }
                    }

                    _customerItemGroupRepository.Update(itemGroup);
                }

                //科室小结
                var depSummary = _customerDepSummaryRepository.FirstOrDefault(o =>
                    o.CustomerRegId == Query.CustomerId && o.DepartmentBMId == itemDepartment);
                if (depSummary != null)
                {
                    depSummary.CharacterSummary = string.Empty;
                    depSummary.DagnosisSummary = string.Empty;
                    depSummary.SystemCharacter = string.Empty;
                    depSummary.OriginalDiag = string.Empty;
                    depSummary.ExamineEmployeeBMId = null;
                    _customerDepSummaryRepository.Update(depSummary);
                }
                ////专科建议
                //var cusSummary = _customerSummary.FirstOrDefault(o =>
                //o.CustomerRegBMId == Query.CustomerId && o.DepartmentBMId == itemDepartment);
                //if (cusSummary != null)
                //{
                //    cusSummary.CharacterSummary = string.Empty;
                //    _customerSummary.Update(cusSummary);
                //}
            }
            CurrentUnitOfWork.SaveChanges();
            if (Query.CustomerId != Guid.Empty)
            {
                var customerReg = _customerRegRepository.FirstOrDefault(o => o.Id == Query.CustomerId);
                var CheckGroupCount = _customerItemGroupRepository.GetAll()
                  .Where(o => o.CustomerRegBMId == Query.CustomerId && o.CheckState != (int)PhysicalEState.Not
                  && o.DepartmentBM.Category != "耗材").ToList();
                //if (customerReg.CheckSate == (int)PhysicalEState.Complete)
                //{

                if (CheckGroupCount.Count > 0)
                {
                    customerReg.CheckSate = (int)PhysicalEState.Process;
                    _customerRegRepository.Update(customerReg);
                }
                else
                {
                    customerReg.CheckSate = (int)PhysicalEState.Not;
                    _customerRegRepository.Update(customerReg);
                }
                //}
            }
            return true;
        }

        /// <summary>
        /// 获取所有医生
        /// </summary>
        /// <returns></returns>
        public List<UserViewDto> GetUserViewDto()
        {
            var userList = _userRepository.GetAllIncluding(r => r.FormRoles, r => r.TbmDepartments);
            var result = userList.ToList();
            return result.MapTo<List<UserViewDto>>();
        }

        /// <summary>
        /// 查询登陆科室项目字典
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public List<BTbmItemDictionaryDto> GetItemDictionarylist(QueryClassTwo query)
        {
            List<BTbmItemDictionaryDto> RTdata = new List<BTbmItemDictionaryDto>();
            var input = _itemDictionaryRepository.GetAll().AsNoTracking();
            foreach (var item in query.DepartmentBMList)
            {
                var data = input.Where(o => o.DepartmentBM.Id == item);
                RTdata.AddRange(data.MapTo<List<BTbmItemDictionaryDto>>());
            }
            return RTdata;
        }

        /// <summary>
        /// 获取所有项目结果参考
        /// </summary>
        /// <returns></returns>
        public List<SearchItemStandardDto> GetGenerateSummary()
        {
            var dto = _itemStandardRepository.GetAll().OrderBy(p => p.OrderNum);
            return dto.MapTo<List<SearchItemStandardDto>>();
        }

        /// <summary>
        /// 获取小结格式和项目信息
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<BasicDictionaryDto> GetDictionaryDto(QueryClass Query)
        {
            var dto = _basicDictionaryRepository.GetAll().
                Where(o => o.Type == BasicDictionaryType.DoctorStationDisplayColumn.ToString());
            return dto.MapTo<List<BasicDictionaryDto>>();
        }

        /// <summary>
        /// 查询科室项目
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<BTbmItemInfoDto> GetItemInfo(QueryClass Query)
        {
            var dto = _itemInfoRepository.GetAll().Where(o => o.DepartmentId == Query.DepartmentBM);
            return dto.MapTo<List<BTbmItemInfoDto>>();
        }

        /// <summary>
        /// 获取套餐统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SearchItemSuitStatisticsDto> GetItemSuitStatistics(QueryClass query)
        {
            var rows = _customerRegRepository.GetAll().Where(o => o.BookingDate > query.LastModificationTimeBign && o.BookingDate < query.LastModificationTimeEnd && o.ItemSuitBMId.HasValue);
            if (query.ItemSuitType.HasValue)
                rows = rows.Where(o => o.ItemSuitBM.ItemSuitType == query.ItemSuitType);
            if (query.ItemSuitBMId.Count > 0)
                rows = rows.Where(o => query.ItemSuitBMId.Contains(o.ItemSuitBMId.Value));
            rows = rows.Include(r => r.ItemSuitBM);
            rows = rows.Include(r => r.MReceiptInfo);
            var allData = rows.ToList();
            //var allData = _customerRegRepository.GetAllIncluding(r => r.ItemSuitBM, r => r.MReceiptInfo).ToList();
            var searchData = from item in allData
                             where item.MReceiptInfo.Count > 0 && item.ItemSuitBM != null
                             group item by new
                             {
                                 item.ItemSuitBMId,
                                 item.ItemSuitBM.ItemSuitType, //套餐类别
                                 item.ItemSuitBM.ItemSuitName,
                                 item.ItemSuitBM.CostPrice // 成本价
                             ,
                                 item.ItemSuitBM.Price   //单价
                             ,
                                 item.MReceiptInfo
                                 //    item.MReceiptInfo.First().Shouldmoney //应收金额
                                 //,item.MReceiptInfo.First().Actualmoney  //实收金额
                                 //,
                                 //    item.MReceiptInfo.First().Discount //折扣率
                             } into g
                             select new SearchItemSuitStatisticsDto
                             {
                                 ItemSuitType = g.Key.ItemSuitType,
                                 ItemSuitBMId = g.Key.ItemSuitBMId,
                                 ItemSuitName = g.Key.ItemSuitName,
                                 CostPrice = g.Key.CostPrice,
                                 Price = g.Key.Price,
                                 Discount = g.Key.MReceiptInfo.First().Discount,
                                 Shouldmoney = g.Key.MReceiptInfo.Sum(a => a.Shouldmoney),
                                 Actualmoney = g.Key.MReceiptInfo.Sum(a => a.Actualmoney),
                                 Count = g.Count()
                             };
            searchData = searchData.GroupBy(o => o.ItemSuitBMId)
                .Select(g => new SearchItemSuitStatisticsDto()
                {
                    ItemSuitBMId = g.First().ItemSuitBMId,
                    ItemSuitType = g.First().ItemSuitType,
                    ItemSuitName = g.First().ItemSuitName,
                    CostPrice = g.Sum(a => a.CostPrice),
                    Discount = g.Average(a => a.Discount),
                    Shouldmoney = g.Sum(a => a.Shouldmoney),
                    Actualmoney = g.Sum(a => a.Actualmoney),
                    Count = g.Count()
                });

            if (searchData != null)
                return searchData.ToList();//.MapTo<List<SearchItemSuitStatisticsDto>>();
            else
                return null;
        }

        /// <summary>
        /// 科室工作量项目细目统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SearchKSGZLItemStatisticsDto> KSGZLItemStatistics(QueryClass query)
        {
            var data = _customerRegItemRepository.GetAll()
                .Where(r => query.DepartmentBMList.Contains(r.DepartmentId)); //科室

            if (query.LastModificationTimeBign != null)
                data = data.Where(r => r.CustomerRegBM.LoginDate >= query.LastModificationTimeBign);
            if (query.LastModificationTimeEnd != null)
                data = data.Where(r => r.CustomerRegBM.LoginDate <= query.LastModificationTimeEnd);
            if (query.ClientInfoId != null) //单位Id
                data = data.Where(r => query.ClientInfoId.Contains(r.CustomerRegBM.ClientInfoId));

            //linq  case when + Sum..............(~_~)   r.DepartmentId,r.DepartmentBM.Name,r.InspectEmployeeBM.Surname, r.InspectEmployeeBM,
            var queryList = data.GroupBy(r => new { r.ItemName, r.ItemOrder })
                .Select(r => new SearchKSGZLItemStatisticsDto
                {
                    ItemName = r.Key.ItemName,
                    ItemOrder = r.Key.ItemOrder,
                    GiveUpNum = r.Sum(a => a.ProcessState == 1 ? 1 : 0), //项目状态 1放弃2已检3待查
                    CompleteNum = r.Sum(a => a.ProcessState == 2 ? 1 : 0),
                    AwaitNum = r.Sum(a => a.ProcessState == 3 ? 1 : 0)
                }).ToList();
            return queryList;
        }

        /// <summary>
        /// 查询科室健康建议
        /// </summary>
        /// <param name="input">查询类（可通用）</param>
        /// <returns></returns>
        public List<SearchTbmSummarizeAdviceDto> GetSummarizeAdvice(QueryClassTwo input)
        {
            List<SearchTbmSummarizeAdviceDto> Dto = new List<SearchTbmSummarizeAdviceDto>();
            var query = _summarizeAdviceRepository.GetAll().AsNoTracking();
            foreach (var item in input.DepartmentBMList)
            {
                query = query.Where(r => r.DepartmentId == item);
                Dto.AddRange(query.MapTo<List<SearchTbmSummarizeAdviceDto>>());
            }
            return Dto;
        }

        /// <summary>
        /// 检查项目统计
        /// </summary>
        /// <param name="Query">查询类（可通用）</param>
        /// <returns></returns>
        public List<ATjlCustomerItemGroupViewDto> GettheCheckItemStatistics(StatisticalClass Query)
        {
            var CustomerItemGroup = _customerItemGroupRepository.GetAll().Where(o => o.IsAddMinus != 3);
            if (Query.ClientInfo_Id != null && Query.ClientInfo_Id != Guid.Empty)
                CustomerItemGroup =
                   CustomerItemGroup.Where(o => o.CustomerRegBM.ClientInfoId == Query.ClientInfo_Id);
            if (Query.ClientInfoReg_Id != null && Query.ClientInfoReg_Id != Guid.Empty)
                CustomerItemGroup =
                   CustomerItemGroup.Where(o => o.CustomerRegBM.ClientRegId == Query.ClientInfoReg_Id);
            if (Query.BillingEmployeeBM != null && Query.BillingEmployeeBM.Count > 0)
                CustomerItemGroup =
                    CustomerItemGroup.Where(o => Query.BillingEmployeeBM.Contains(o.BillingEmployeeBM.Name));
            if (Query.DepartmentName != null && Query.DepartmentName.Count > 0)
                CustomerItemGroup =
                    CustomerItemGroup.Where(o => Query.DepartmentName.Contains(o.DepartmentName.Trim()));
            if (Query.ItemGroupName != null && Query.ItemGroupName.Count > 0)
                CustomerItemGroup = CustomerItemGroup.Where(o => Query.ItemGroupName.Contains(o.ItemGroupName.Trim()));
            if (Query.StartTime != null && Query.EndTime != null)
            {
                if (Query.TimeState.HasValue && Query.TimeState == 1)
                {
                    CustomerItemGroup = CustomerItemGroup.Where(o =>
                        o.FirstDateTime >= Query.StartTime && o.FirstDateTime < Query.EndTime);//原本是第一次检查时间zyl要求改为登记时间
                }
                else
                {
                    CustomerItemGroup = CustomerItemGroup.Where(o =>
                        o.CustomerRegBM.LoginDate >= Query.StartTime && o.CustomerRegBM.LoginDate < Query.EndTime);//原本是第一次检查时间zyl要求改为登记时间
                }
            }
            if (Query.CheckState.HasValue)
            {
                CustomerItemGroup = CustomerItemGroup.Where(o =>
                          o.CheckState == Query.CheckState.Value);//原本是第一次检查时间zyl要求改为登记时间

            }
            if (Query.RegistState.HasValue)
            {
                CustomerItemGroup = CustomerItemGroup.Where(o =>
                          o.CustomerRegBM.RegisterState == Query.RegistState.Value);//原本是第一次检查时间zyl要求改为登记时间

            }
            var result = CustomerItemGroup
                .GroupBy(p => new { p.DepartmentBM.DepartmentBM, p.DepartmentName, p.ItemGroupName, p.ItemPrice })
                .Select(g => new
                    ATjlCustomerItemGroupViewDto
                {
                    DepartmentCodeBM = g.Key.DepartmentBM,
                    DepartmentName = g.Key.DepartmentName,

                    // ItemGroupCodeBM,
                    ItemGroupName = g.Key.ItemGroupName,
                    ItemPrice = g.Key.ItemPrice,
                    OriginalPrice = g.Where(a => a.PayerCat != 5).Sum(a => a.ItemPrice),
                    PriceAfterDis = g.Where(a => a.PayerCat != 5).Sum(a => a.PriceAfterDis),
                    Number = g.Count(a => a.PayerCat != 5),
                    FreeToCheck = g.Count(a => a.PayerCat == 5)
                }).OrderBy(r => r.DepartmentName).ToList();


            return result;
        }

        /// <summary>
        /// 科室工作量统计
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        public List<SearchKSGZLStatisticsDto> KSGZLStatistics(QueryClass query)
        {
            SearchKSGZLStatisticsDto s = new SearchKSGZLStatisticsDto();
            var data = _customerItemGroupRepository.GetAllIncluding(r => r.CustomerRegBM, a => a.InspectEmployeeBM).Select(
               r => new
               {
                   r.DepartmentId,
                   r.CustomerRegBM.LoginDate,
                   r.PayerCat,
                   r.CustomerRegBM.CustomerType,
                   r.CheckState,
                   r.CustomerRegBM.ClientInfoId,
                   r.CustomerRegBM.ClientRegId,
                   EmployeeName = query.EmpType == 2 ? r.CheckEmployeeBM.Name : r.InspectEmployeeBM.Name,
                   r.DepartmentName,
                   InspectEmployeeBMId = query.EmpType == 2 ? r.CheckEmployeeBMId : r.InspectEmployeeBMId,
                   r.ItemGroupBM_Id,
                   r.ItemGroupName,
                   r.ItemPrice,
                   r.CustomerRegBM.RegisterState,
                   r.CustomerRegBM.Customer.Sex,
                   r.CustomerRegBM.Customer.Name,
                   r.CustomerRegBM.BookingDate,
                   r.CustomerRegBM.CheckSate

               });
            //科室 &&  已收费
            data = data.Where(r => query.DepartmentBMList.Contains(r.DepartmentId) && r.PayerCat != 1 && r.CheckState != 1);
            if (query.LastModificationTimeBign != null)
            {
                if (query.DateType.HasValue && query.DateType == 1)
                { data = data.Where(r => r.BookingDate >= query.LastModificationTimeBign && r.CheckSate != (int)PhysicalEState.Not); }
                else
                {
                    data = data.Where(r => r.LoginDate >= query.LastModificationTimeBign);
                }
            }
            if (query.LastModificationTimeEnd != null)
            {
                if (query.DateType.HasValue && query.DateType == 1)
                {
                    data = data.Where(r => r.BookingDate <= query.LastModificationTimeEnd && r.CheckSate != (int)PhysicalEState.Not);
                }
                else
                {
                    data = data.Where(r => r.LoginDate <= query.LastModificationTimeEnd);
                }
            }
            if (query.ClientInfoId != null) //单位Id
                data = data.Where(r => query.ClientInfoId.Contains(r.ClientInfoId));
            if (query.ClientInfoRegId != null) //单位预约Id
                data = data.Where(r => query.ClientInfoRegId.Contains(r.ClientRegId));
            if (query.CustomerType != null)
                data = data.Where(r => r.CustomerType == query.CustomerType);
            //检索医生
            if (!string.IsNullOrWhiteSpace(query.DoctorName))
                data = data.Where(r => r.EmployeeName.Contains(query.DoctorName));
            if (query.Sex.HasValue && query.Sex != (int)Sex.GenderNotSpecified)
            {
                data = data.Where(r => r.Sex == query.Sex);
            }
            //var cusgroup= data.Select(r=> new {
            //    r.DepartmentId,
            //    r.DepartmentName,
            //    r.InspectEmployeeBMId,
            //    r.Name,
            //    r.ItemGroupBM_Id,
            //    r.ItemGroupName,
            //    r.ItemPrice,
            //    r.RegisterState,
            //    r.CheckState
            //}).ToList();

            var suit = data.Where(c => c.EmployeeName != null && (c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part)).ToList();
            var ksgzllist = suit.GroupBy(r => new
            {
                r.DepartmentId,
                r.DepartmentName,
                r.InspectEmployeeBMId,
                r.EmployeeName,
                r.ItemGroupBM_Id,
                r.ItemGroupName,
                r.ItemPrice,
                //r.DiscountRate
            }).Select(r => new SearchKSGZLStatisticsDto
            {
                Department_Id = r.Key.DepartmentId,
                DepartmentName = r.Key.DepartmentName,
                InspectEmployeeName = r.Key.EmployeeName,
                ItemGroupName = r.Key.ItemGroupName,
                RegisterNum = r.Sum(a => a.RegisterState == 2 ? 1 : 0), //登记状态 1未登记 2已登记
                UnRegisterNum = r.Sum(a => a.RegisterState == 1 ? 1 : 0),
                UnCheckedNum = r.Where(c => c.CheckState == (int)ProjectIState.Not).Count(), //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
                CompleteNum = r.Where(c => c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part).Count(),
                CheckedCount = data.Where(o => o.DepartmentId == r.Key.DepartmentId).Select(o => o.Name).Count(),
                GiveUpNum = r.Where(c => c.CheckState == (int)ProjectIState.GiveUp).Count(),
                AwaitNum = r.Where(c => c.CheckState == (int)ProjectIState.Stay).Count(),
                ItemPrice = r.Key.ItemPrice, //单价
                Count = r.Where(c => c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part).Count(),
                //DiscountRate = r.Key.DiscountRate,
                ShouldMoney = r.Count() * r.Key.ItemPrice,
                ActualMoney = r.Key.ItemPrice * r.Where(c => c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part).Count()
            }).ToList();
            //变态的需求
            //List<SearchKSGZLStatisticsDto> huiZongData = ksgzllist.GroupBy(r => new { r.DepartmentName }).Select(r => new SearchKSGZLStatisticsDto
            //{
            //    DepartmentName = r.Key.DepartmentName + "汇总",
            //    InspectEmployeeName = "",
            //    RegisterNum = r.Sum(a => a.RegisterNum),
            //    UnRegisterNum = r.Sum(a => a.UnRegisterNum),
            //    CompleteNum = r.Sum(a => a.CompleteNum),
            //    UnCheckedNum = r.Sum(a => a.UnCheckedNum),
            //    AwaitNum = r.Sum(a => a.AwaitNum),
            //    GiveUpNum = r.Sum(a => a.GiveUpNum),
            //    ItemPrice = r.Sum(a => a.ItemPrice),
            //    Count = r.Sum(a => a.Count),
            //    ShouldMoney = r.Sum(a => a.ShouldMoney),
            //    ActualMoney = r.Sum(a => a.ActualMoney)
            //}).ToList();
            //ksgzllist.AddRange(huiZongData);

            var ss = ksgzllist.OrderBy(r => r.DepartmentName).ThenBy(r => r.InspectEmployeeName).ToList();
            return ss;

            #region  注释的代码
            //var data = _customerRegItemRepository.GetAllIncluding(r => r.CustomerRegBM, r => r.DepartmentBM)
            //    .Where(r => query.DepartmentBMList.Contains(r.DepartmentId));

            //if (query.LastModificationTimeBign != null)
            //    data = data.Where(r => r.CustomerRegBM.LoginDate >= query.LastModificationTimeBign);
            //if (query.LastModificationTimeEnd != null)
            //    data = data.Where(r => r.CustomerRegBM.LoginDate <= query.LastModificationTimeEnd);
            //if (query.ClientInfoId != null) //单位Id
            //    data = data.Where(r => r.CustomerRegBM.ClientInfoId == query.ClientInfoId);

            ////linq  case when + Sum..............(~_~)
            //var queryList = data.GroupBy(r => new { r.DepartmentId, r.DepartmentBM.Name })
            //    .Select(r => new SearchKSGZLStatisticsDto
            //    {
            //        Department_Id = r.Key.DepartmentId,
            //        DepartmentName = r.Key.Name,
            //        RegisterNum = r.Sum(a => a.CustomerRegBM.RegisterState == 2 ? 1 : 0), //登记状态 1未登记 2已登记
            //        UnRegisterNum = r.Sum(a => a.CustomerRegBM.RegisterState == 1 ? 1 : 0)
            //    }).ToList();
            //return queryList;
            #endregion
        }
        /// <summary>
        /// 大科室工作量统计
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        public List<SearchKSGZLStatisticsDto> BKSGZLStatistics(QueryClass query)
        {
            SearchKSGZLStatisticsDto s = new SearchKSGZLStatisticsDto();
            var data = _customerItemGroupRepository.GetAllIncluding(r => r.CustomerRegBM, a => a.InspectEmployeeBM).Select(
               r => new
               {
                   r.DepartmentId,
                   r.CustomerRegBM.LoginDate,
                   r.PayerCat,
                   r.CustomerRegBM.CustomerType,
                   r.CheckState,
                   r.CustomerRegBM.ClientInfoId,
                   r.CustomerRegBM.ClientRegId,
                   EmployeeName = query.EmpType == 2 ? r.CheckEmployeeBM.Name : r.InspectEmployeeBM.Name,
                   r.DepartmentName,
                   InspectEmployeeBMId = query.EmpType == 2 ? r.CheckEmployeeBMId : r.InspectEmployeeBMId,
                   r.ItemGroupBM_Id,
                   r.ItemGroupName,
                   r.ItemPrice,
                   r.CustomerRegBM.RegisterState,
                   r.CustomerRegBM.Customer.Sex,
                   r.CustomerRegBM.Customer.Name,
                   r.CustomerRegBM.BookingDate,
                   r.CustomerRegBM.CheckSate,
                   r.DepartmentBM.LargeDepart

               });
            //科室 &&  已收费
            if (query.LargeDepartmentBMList!=null && query.LargeDepartmentBMList.Count>0)
            {
                data = data.Where(r => query.LargeDepartmentBMList.Contains(r.LargeDepart.Value) && r.PayerCat != 1 && r.CheckState != 1);
            }
            if (query.LastModificationTimeBign != null)
            {
                if (query.DateType.HasValue && query.DateType == 1)
                { data = data.Where(r => r.BookingDate >= query.LastModificationTimeBign && r.CheckSate != (int)PhysicalEState.Not); }
                else
                {
                    data = data.Where(r => r.LoginDate >= query.LastModificationTimeBign);
                }
            }
            if (query.LastModificationTimeEnd != null)
            {
                if (query.DateType.HasValue && query.DateType == 1)
                {
                    data = data.Where(r => r.BookingDate <= query.LastModificationTimeEnd && r.CheckSate != (int)PhysicalEState.Not);
                }
                else
                {
                    data = data.Where(r => r.LoginDate <= query.LastModificationTimeEnd);
                }
            }
            if (query.ClientInfoId != null) //单位Id
                data = data.Where(r => query.ClientInfoId.Contains(r.ClientInfoId));
            if (query.ClientInfoRegId != null) //单位预约Id
                data = data.Where(r => query.ClientInfoRegId.Contains(r.ClientRegId));
            if (query.CustomerType != null)
                data = data.Where(r => r.CustomerType == query.CustomerType);
            //检索医生
            if (!string.IsNullOrWhiteSpace(query.DoctorName))
                data = data.Where(r => r.EmployeeName.Contains(query.DoctorName));
            if (query.Sex.HasValue && query.Sex != (int)Sex.GenderNotSpecified)
            {
                data = data.Where(r => r.Sex == query.Sex);
            }
            //var cusgroup= data.Select(r=> new {
            //    r.DepartmentId,
            //    r.DepartmentName,
            //    r.InspectEmployeeBMId,
            //    r.Name,
            //    r.ItemGroupBM_Id,
            //    r.ItemGroupName,
            //    r.ItemPrice,
            //    r.RegisterState,
            //    r.CheckState
            //}).ToList();

            var suit = data.Where(c => c.EmployeeName != null && (c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part)).ToList();
            var ksgzllist = suit.GroupBy(r => new
            {
                //r.DepartmentId,
                //r.DepartmentName,
                r.InspectEmployeeBMId,
                r.EmployeeName,
                r.ItemGroupBM_Id,
                r.ItemGroupName,
                r.ItemPrice,
                r.LargeDepart
                //r.DiscountRate
            }).Select(r => new SearchKSGZLStatisticsDto
            {
                 LargeDepart=r.Key.LargeDepart,
                //Department_Id = r.Key.DepartmentId,
                //DepartmentName = r.Key.DepartmentName,
                InspectEmployeeName = r.Key.EmployeeName,
                ItemGroupName = r.Key.ItemGroupName,
                RegisterNum = r.Sum(a => a.RegisterState == 2 ? 1 : 0), //登记状态 1未登记 2已登记
                UnRegisterNum = r.Sum(a => a.RegisterState == 1 ? 1 : 0),
                UnCheckedNum = r.Where(c => c.CheckState == (int)ProjectIState.Not).Count(), //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
                CompleteNum = r.Where(c => c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part).Count(),
                CheckedCount = data.Where(o => o.LargeDepart == r.Key.LargeDepart).Select(o => o.Name).Count(),
                GiveUpNum = r.Where(c => c.CheckState == (int)ProjectIState.GiveUp).Count(),
                AwaitNum = r.Where(c => c.CheckState == (int)ProjectIState.Stay).Count(),
                ItemPrice = r.Key.ItemPrice, //单价
                Count = r.Where(c => c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part).Count(),
                //DiscountRate = r.Key.DiscountRate,
                ShouldMoney = r.Count() * r.Key.ItemPrice,
                ActualMoney = r.Key.ItemPrice * r.Where(c => c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part).Count()
            }).ToList();
            //变态的需求
            //List<SearchKSGZLStatisticsDto> huiZongData = ksgzllist.GroupBy(r => new { r.DepartmentName }).Select(r => new SearchKSGZLStatisticsDto
            //{
            //    DepartmentName = r.Key.DepartmentName + "汇总",
            //    InspectEmployeeName = "",
            //    RegisterNum = r.Sum(a => a.RegisterNum),
            //    UnRegisterNum = r.Sum(a => a.UnRegisterNum),
            //    CompleteNum = r.Sum(a => a.CompleteNum),
            //    UnCheckedNum = r.Sum(a => a.UnCheckedNum),
            //    AwaitNum = r.Sum(a => a.AwaitNum),
            //    GiveUpNum = r.Sum(a => a.GiveUpNum),
            //    ItemPrice = r.Sum(a => a.ItemPrice),
            //    Count = r.Sum(a => a.Count),
            //    ShouldMoney = r.Sum(a => a.ShouldMoney),
            //    ActualMoney = r.Sum(a => a.ActualMoney)
            //}).ToList();
            //ksgzllist.AddRange(huiZongData);

            var ss = ksgzllist.OrderBy(r => r.LargeDepart).ThenBy(r => r.InspectEmployeeName).ToList();
            return ss;

            #region  注释的代码
            //var data = _customerRegItemRepository.GetAllIncluding(r => r.CustomerRegBM, r => r.DepartmentBM)
            //    .Where(r => query.DepartmentBMList.Contains(r.DepartmentId));

            //if (query.LastModificationTimeBign != null)
            //    data = data.Where(r => r.CustomerRegBM.LoginDate >= query.LastModificationTimeBign);
            //if (query.LastModificationTimeEnd != null)
            //    data = data.Where(r => r.CustomerRegBM.LoginDate <= query.LastModificationTimeEnd);
            //if (query.ClientInfoId != null) //单位Id
            //    data = data.Where(r => r.CustomerRegBM.ClientInfoId == query.ClientInfoId);

            ////linq  case when + Sum..............(~_~)
            //var queryList = data.GroupBy(r => new { r.DepartmentId, r.DepartmentBM.Name })
            //    .Select(r => new SearchKSGZLStatisticsDto
            //    {
            //        Department_Id = r.Key.DepartmentId,
            //        DepartmentName = r.Key.Name,
            //        RegisterNum = r.Sum(a => a.CustomerRegBM.RegisterState == 2 ? 1 : 0), //登记状态 1未登记 2已登记
            //        UnRegisterNum = r.Sum(a => a.CustomerRegBM.RegisterState == 1 ? 1 : 0)
            //    }).ToList();
            //return queryList;
            #endregion
        }


        /// <summary>
        /// 大科室工作量统计
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        public List<SearchKSGZLStatisticsDto> DKSGZLStatistics(QueryClass query)
        {
            SearchKSGZLStatisticsDto s = new SearchKSGZLStatisticsDto();
            var data = _customerItemGroupRepository.GetAllIncluding(r => r.CustomerRegBM).Select(
               r => new
               {
                   r.PayerCat,
                   r.CheckState,
                   r.CustomerRegBM.BookingDate,
                   r.CustomerRegBM.CheckSate,
                   r.ItemGroupBM.ChartName,
                   r.ItemGroupBM.ChartCode,
                   r.CustomerRegBM.LoginDate,
                   r.CustomerRegBM.ClientRegId,
                   r.CustomerRegBM.ClientInfoId

               });
            //科室 &&  已收费
            data = data.Where(r => r.PayerCat != 1 && r.CheckState != 1);
            if (query.BasicDictionaryType != null && query.BasicDictionaryType.Count > 0)
            {
                //var  que = data.Where(r => r.ChartCode != null && r.ChartCode != "").ToList();

                //var ss1 = que.Where(r=> query.BasicDictionaryType.Contains(r.ChartCode)).ToList();
               data = data.Where(r => r.ChartCode!=null && r.ChartCode!="" 
                && query.BasicDictionaryType.Contains(r.ChartCode));
            }
            if (query.LastModificationTimeBign != null)
            {
                if (query.DateType.HasValue && query.DateType == 1)
                { data = data.Where(r => r.BookingDate >= query.LastModificationTimeBign && r.CheckSate != (int)PhysicalEState.Not); }
                else
                {
                    data = data.Where(r => r.LoginDate >= query.LastModificationTimeBign);
                }
            }
            if (query.LastModificationTimeEnd != null)
            {
                if (query.DateType.HasValue && query.DateType == 1)
                {
                    data = data.Where(r => r.BookingDate <= query.LastModificationTimeEnd && r.CheckSate != (int)PhysicalEState.Not);
                }
                else
                {
                    data = data.Where(r => r.LoginDate <= query.LastModificationTimeEnd);
                }
            }
            if (query.ClientInfoId != null) //单位Id
                data = data.Where(r => query.ClientInfoId.Contains(r.ClientInfoId));
            if (query.ClientInfoRegId != null) //单位预约Id
                data = data.Where(r => query.ClientInfoRegId.Contains(r.ClientRegId));         
          

            var suit = data.Where(c => (c.CheckState == (int)ProjectIState.Complete || c.CheckState == (int)ProjectIState.Part)).ToList();
            var ksgzllist = suit.GroupBy(r => new
            {
                 r.ChartName           
                        
                
            }).Select(r => new SearchKSGZLStatisticsDto
            {
                
                DepartmentName = r.Key.ChartName ,  
                CheckedCount = r.Count(),
                
              
            }).ToList();
           
            var ss = ksgzllist.OrderBy(r => r.DepartmentName).ToList();
            return ss;
 
        }
        /// <summary>
        /// 科室压力
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SearchKSYLStatisticsDto> KSYLStatistics(QueryClass query)
        {
            var data = _customerItemGroupRepository.GetAllIncluding(r => r.CustomerRegBM, a => a.InspectEmployeeBM, a => a.ItemGroupBM);
            data = data.Where(r => query.DepartmentBMList.Contains(r.DepartmentId));

            if (query.LastModificationTimeBign != null)
                data = data.Where(r => r.CustomerRegBM.LoginDate >= query.LastModificationTimeBign);
            if (query.LastModificationTimeEnd != null)
                data = data.Where(r => r.CustomerRegBM.LoginDate <= query.LastModificationTimeEnd);

            var ksgzllist = data.GroupBy(r => new
            {
                r.DepartmentId,
                r.DepartmentName,
                r.InspectEmployeeBMId,
                r.InspectEmployeeBM.Name,
                r.ItemGroupCodeBM,
                r.ItemGroupName,
                r.ItemGroupBM.MaxCheckDay
            }).Select(r => new SearchKSYLStatisticsDto
            {
                Department_Id = r.Key.DepartmentId,
                DepartmentName = r.Key.DepartmentName,
                InspectEmployeeName = r.Key.Name,
                ItemGroupName = r.Key.ItemGroupName,
                MaxNum = r.Key.MaxCheckDay,
                //项目检查状态 1未检查 2已检查 3部分检查 4放弃 5待查 6暂存
                CheckingNum = r.Sum(a => a.CheckState == 3 ? 1 : 0), /*在检 3  **/
                UnCheckedNum = r.Sum(a => a.CheckState == 1 ? 1 : 0),/*未检  1  **/
                CheckedNum = r.Sum(a => a.CheckState == 2 ? 1 : 0),/*已检 2 **/

                TotalNum = r.Sum(a => a.CheckState == 3 || a.CheckState == 2 || a.CheckState == 1 ? 1 : 0),
                StartTime = r.Min(a => a.FirstDateTime),
                // EntityFunctions 已经停止使用了，不推荐继续使用
                // 请替换成 DbFunctions
                AvgTime = r.Sum(a => a.CheckState == 2 ? 1 : 0) == 0 ? 0 : DbFunctions.DiffMinutes(r.Min(a => a.FirstDateTime), query.LastModificationTimeEnd) / r.Sum(a => a.CheckState == 2 ? 1 : 0)
            }).ToList();
            return ksgzllist;
        }
        /// <summary>
        /// 已检、放弃、待查、未检统计
        /// </summary>
        /// <returns></returns>
        public List<PatientExaminationProjectStatisticsViewDto> GetATjlCustomerItemGrouplist(
        PatientExaminationCondition Condition)
        {
            var CustomerReg = _customerRegRepository.GetAllIncluding(r => r.CustomerItemGroup);
            CustomerReg = CustomerReg.Where(o => o.RegisterState == (int)RegisterState.Yes);
            //单位
            if (Condition.ClientName != null && Condition.ClientName.Count > 0)
                CustomerReg = CustomerReg.Where(r => Condition.ClientName.Contains(r.ClientInfo.ClientName));
            if (Condition.ClientRegId != null && Condition.ClientRegId.Count > 0)
                CustomerReg = CustomerReg.Where(r => Condition.ClientRegId.Contains(r.ClientRegId));


            //if (Condition.StartTime != null && Condition.EndTime != null)
            //    CustomerReg = CustomerReg.Where(r =>
            //        r.LastModificationTime >= Condition.StartTime && r.LastModificationTime < Condition.EndTime);
            if (!string.IsNullOrWhiteSpace(Condition.IsPersonal))
            {
                if (Condition.IsPersonal == "团体")
                    CustomerReg = CustomerReg.Where(o => o.ClientInfoId.HasValue || o.ClientTeamInfoId.HasValue);
                else
                    CustomerReg = CustomerReg.Where(o => !o.ClientInfoId.HasValue && !o.ClientTeamInfoId.HasValue);
            }

            //体检号
            //if (!string.IsNullOrEmpty(Condition.ArchivesNum))
            //CustomerReg = CustomerReg.Where(r => r.Customer.ArchivesNum.Contains(Condition.ArchivesNum));
            if (!string.IsNullOrEmpty(Condition.CustomerBM))
                CustomerReg = CustomerReg.Where(r => r.CustomerBM == Condition.CustomerBM);
            //姓名
            if (!string.IsNullOrEmpty(Condition.Name))
                CustomerReg = CustomerReg.Where(r => r.Customer.Name.Contains(Condition.Name));

            //总检状态 1未总检2已分诊3已初检4已审核
            if (Condition.SummSate != null && Condition.SummSate != 0)
                CustomerReg = CustomerReg.Where(r => r.SummSate == Condition.SummSate);

            //打印状态 1未打印2已打印
            if (Condition.PrintSate != null && Condition.PrintSate != 0)
                CustomerReg = CustomerReg.Where(r => r.PrintSate == Condition.PrintSate);

            //收费状态 1未收费2已收费3欠费
            if (Condition.CostState != null && Condition.CostState != 0)
                CustomerReg = CustomerReg.Where(r => r.CostState == Condition.CostState);
            //体检状态
            if (Condition.PersonlCheckState != 0 && Condition.PersonlCheckState.HasValue)
                CustomerReg = CustomerReg.Where(r => r.CheckSate == Condition.PersonlCheckState);
            if (!string.IsNullOrWhiteSpace(Condition.Introducer))
                CustomerReg = CustomerReg.Where(o => o.Introducer == Condition.Introducer || o.ClientInfo.LinkMan == Condition.Introducer);
            //加减状态 1为正常项目2为加项3为减项
            if (Condition.IsAddMinus != null)
            {
                CustomerReg = CustomerReg.Where(o => o.CustomerItemGroup.Select(n => n.IsAddMinus.Value == Condition.IsAddMinus.Value).Count() > 0);
                //foreach(var s in result)
                //{
                //    if(s.CustomerItemGroup.Select(o=>o.IsAddMinus == Condition.IsAddMinus))
                //    {

                //    }
                //}

            }
            //var results = result.MapTo<PatientExaminationProjectStatisticsViewDto>();
            //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
            //if (Condition.CheckState != null)
            //{
            //    result = result.(r => r.CustomerItemGroup.Equals(Condition.CheckState));
            //}
            //时间
            if (Condition.StartTime != null && Condition.EndTime != null)
            {
                Condition.StartTime = new DateTime(Condition.StartTime.Value.Year,
                    Condition.StartTime.Value.Month, Condition.StartTime.Value.Day, 00,
                    00, 00);
                Condition.EndTime = new DateTime(Condition.EndTime.Value.Year,
                    Condition.EndTime.Value.Month, Condition.EndTime.Value.Day, 23, 59,
                    59);
                if (Condition.DateType.HasValue && Condition.DateType == 1)
                {
                    CustomerReg = CustomerReg.Where(o =>
                       o.BookingDate >= Condition.StartTime &&
                       o.BookingDate < Condition.EndTime && o.CheckSate != (int)PhysicalEState.Not);
                }
                else
                {
                    CustomerReg = CustomerReg.Where(o =>
                        o.LoginDate >= Condition.StartTime &&
                        o.LoginDate < Condition.EndTime);
                }
            }
            //var result= CustomerReg.MapTo<List<PatientExaminationProjectStatisticsViewDto>>();
            //return result;
            var q = CustomerReg.Select(r => new PatientExaminationProjectStatisticsViewDto
            {
                TeamName = r.ClientInfo == null ? "" : r.ClientTeamInfo.TeamName,
                RegisterState = r.RegisterState,
                Remarks = r.Customer.Remarks,
                Department = r.Customer.Department,
                SendToConfrim = r.SendToConfirm,
                CusCheckSate = r.CheckSate,
                //流水号
                ArchivesNum = r.CustomerBM,

                //姓名
                Name = r.Customer.Name,
                Introducer = r.ClientInfo == null ? r.Introducer : r.ClientInfo.LinkMan,
                //性别
                Sex = r.Customer.Sex,

                //年龄
                Age = r.Customer.Age,
                IDCardNo = r.Customer.IDCardNo,
                Mobile = r.Customer.Mobile,
                RegRemarks = r.Remarks,

                //单位
                ClientName = r.ClientInfo == null ? "个人" : r.ClientInfo.ClientName,
                BookingDate = r.BookingDate,
                CheckSate = r.CheckSate,
                //第一次登记日期()
                LoginDate = r.LoginDate,

                //未检查组合数
                UnCheckNum = r.CustomerItemGroup.Count(a => a.CheckState != null && a.CheckState == 1),

                //已检查组合数
                CheckNum = r.CustomerItemGroup.Count(a => a.CheckState != null && a.CheckState == 2),

                //放弃检查组合数
                GiveupCheckNum = r.CustomerItemGroup.Count(a => a.CheckState != null && a.CheckState == 4),

                //待查检查数
                AwaitCheckNum = r.CustomerItemGroup.Count(a => a.CheckState != null && a.CheckState == 5),

                //总检状态 1未总检2已分诊3已初检4已审核
                SummSate = r.SummSate == null ? 1 : r.SummSate,

                //打印状态 1未打印2已打印
                PrintSate = r.PrintSate == null ? 1 : r.PrintSate,

                //收费状态 1未收费2已收费3欠费
                CostState = r.CostState == null ? 1 : r.CostState,

                //套餐名称
                ItemSuitName = r.ItemSuitName,

                //应收金额
                ItemPrice = r.McusPayMoney == null ? 0 : (r.McusPayMoney.PersonalMoney + r.McusPayMoney.ClientMoney),

                //实收金额
                PriceAfterDis = r.McusPayMoney == null ? 0 : r.McusPayMoney.PersonalPayMoney,

                //项目明细赋值
                CustomerItemGroup = r.CustomerItemGroup.Select(
                    n => new ATjlCustomerItemGroupDto
                    {
                        Id = n.Id,
                        //科室
                        DepartmentName = n.DepartmentName,

                        //组合
                        ItemGroupName = n.ItemGroupName,

                        //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
                        CheckState = n.CheckState == null ? 1 : n.CheckState,

                        //应收金额
                        ItemPrice = n.TTmoney + n.GRmoney,

                        //实收金额
                        PriceAfterDis = n.PriceAfterDis,

                        //加减状态 1为正常项目2为加项3为减项
                        IsAddMinus = n.IsAddMinus == null ? 1 : n.IsAddMinus,

                        //退费状态 1正常2带退费3退费 收费处退费后变为减项状态
                        RefundState = n.RefundState == null ? 1 : n.RefundState,

                        //组合小结
                        ItemGroupSum = n.ItemGroupSum,

                        //最后一次更新时间
                        Drawtime = n.LastModificationTime
                    }).OrderBy(n => n.CheckState).ToList()
            }).ToList();

            foreach (var item in q)
            {
                //未检查项目
                item.UnCheckItems = string.Join(",",
                    item.CustomerItemGroup.Where(a => a.IsAddMinus != (int)AddMinusType.Minus && (a.CheckState == null || a.CheckState == (int)ProjectIState.Not))
                        .Select(a => a.ItemGroupName).ToArray());

                //已检查项目
                item.CheckItems = string.Join(",",
                    item.CustomerItemGroup.Where(a => a.CheckState != null && a.CheckState == (int)ProjectIState.Complete)
                        .Select(a => a.ItemGroupName).ToArray());
                //部分检查
                item.PartItems = string.Join(",", item.CustomerItemGroup.Where(a => a.CheckState != null && a.CheckState == (int)ProjectIState.Part));
                //放弃检查项目
                //item.GiveupCheckItems = string.Join(",",
                //    item.CustomerItemGroup.Where(a => a.CheckState != null && a.CheckState == 4)
                //        .Select(a => a.ItemGroupName).ToArray());
                foreach (var group in item.CustomerItemGroup.Where(a => a.CheckState != null && a.CheckState == 4))
                {
                    if (string.IsNullOrWhiteSpace(item.GiveupCheckItems))
                        item.GiveupCheckItems = group.ItemGroupName;
                    else
                        item.GiveupCheckItems = string.Join(",", new List<string> { item.GiveupCheckItems, group.ItemGroupName });
                    var awititem = _tjlCusGiveUpRepository.FirstOrDefault(o => o.CustomerItemGroupId == group.Id);
                    if (awititem != null)
                    {
                        if (string.IsNullOrWhiteSpace(item.GiveupDate))
                            item.GiveupDate = awititem.CreationTime.ToString("yyyy年MM月dd日");
                        else
                            item.GiveupDate = string.Join(",", new List<string> { item.GiveupDate, awititem.CreationTime.ToString("yyyy年MM月dd日") });
                    }
                }
                //待查检查项目
                //item.AwaitCheckItems = string.Join(",",
                //    item.CustomerItemGroup.Where(a => a.CheckState != null && a.CheckState == 5)
                //        .Select(a => a.ItemGroupName).ToArray());
                foreach (var group in item.CustomerItemGroup.Where(a => a.CheckState != null && a.CheckState == 5))
                {
                    if (string.IsNullOrWhiteSpace(item.AwaitCheckItems))
                        item.AwaitCheckItems = group.ItemGroupName;
                    else
                        item.AwaitCheckItems = string.Join(",", new List<string> { item.AwaitCheckItems, group.ItemGroupName });
                    var awititem = _tjlCusGiveUpRepository.FirstOrDefault(o => o.CustomerItemGroupId == group.Id);
                    if (awititem != null)
                    {
                        if (awititem.stayDate.HasValue)
                        {
                            if (string.IsNullOrWhiteSpace(item.AwaitDate))
                            {
                                item.AwaitDate = awititem.stayDate.Value.ToString("yyyy年MM月dd日");
                            }
                            else
                                item.AwaitDate = string.Join(",", new List<string> { item.AwaitDate, awititem.stayDate.Value.ToString("yyyy年MM月dd日") });
                        }

                    }

                }
                //减项目
                item.AddCheckItems = string.Join(",",
                    item.CustomerItemGroup.Where(a => a.IsAddMinus != null && a.IsAddMinus == 2)
                        .Select(a => a.ItemGroupName).ToArray());

                //加项目
                item.MinusCheckItems = string.Join(",",
                    item.CustomerItemGroup.Where(a => a.IsAddMinus != null && a.IsAddMinus == 3)
                        .Select(a => a.ItemGroupName).ToArray());
            }

            //加减状态 1为正常项目2为加项3为减项
            if (Condition.IsAddMinus != null && Condition.IsAddMinus != 0)
            {
                if (Condition.IsAddMinus == 2) //加项
                    q = q.Where(r => !string.IsNullOrEmpty(r.AddCheckItems.Trim())).ToList();
                else if (Condition.IsAddMinus == 3)
                    q = q.Where(r => !string.IsNullOrEmpty(r.MinusCheckItems.Trim())).ToList();
            }

            //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
            if (Condition.CheckState != null && Condition.CheckState != 0)
            {
                if (Condition.CheckState == (int)ProjectIState.Not) //未检查
                    q = q.Where(r => !string.IsNullOrEmpty(r.UnCheckItems?.Trim())).ToList();
                else if (Condition.CheckState == (int)ProjectIState.Complete) //已检查
                    q = q.Where(r => !string.IsNullOrEmpty(r.CheckItems?.Trim())).ToList();
                else if (Condition.CheckState == (int)ProjectIState.Part)//部分检查
                    q = q.Where(r => !string.IsNullOrEmpty(r.PartItems?.Trim())).ToList();
                else if (Condition.CheckState == (int)ProjectIState.Stay) //待查
                    q = q.Where(r => !string.IsNullOrEmpty(r.AwaitCheckItems?.Trim())).ToList();
                else if (Condition.CheckState == (int)ProjectIState.GiveUp) //放弃
                    q = q.Where(r => !string.IsNullOrEmpty(r.GiveupCheckItems?.Trim())).ToList();
                else if (Condition.CheckState == (int)ProjectIState.Temporary)//目前没有设为暂存状态
                    q = new List<PatientExaminationProjectStatisticsViewDto>();
            }

            return q.OrderByDescending(r => r.LoginDate).ToList();
        }

        /// <summary>
        /// 科室环比统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<KSHBGZLStatisticsDto> KSHBGZLStatistics(HBQueryClass query)

        {
            //当前结果

            var data = _customerRegRepository.GetAllIncluding(r => r.CustomerRegItems)
                .Where(r => r.CustomerRegItems.Any(n => query.DepartmentBMList.Contains(n.DepartmentId) && n.ProcessState == 2));
            if (query.DQStartTime != null) //开始时间
                data = data.Where(r => r.LoginDate >= query.DQStartTime);
            if (query.DQEndTime != null) //结束时间
                data = data.Where(r => r.LoginDate <= query.DQEndTime);

            //历史结果
            var data2 = _customerRegRepository.GetAllIncluding(r => r.CustomerRegItems)
                .Where(r => r.CustomerRegItems.Any(n => query.DepartmentBMList.Contains(n.DepartmentId) && n.ProcessState == 2));

            if (query.LSStartTime != null)//开始时间
            {
                data2 = data2.Where(r => r.LoginDate >= query.LSStartTime);
            }
            if (query.LSEndTime != null)//结束时间
            {
                data2 = data2.Where(r => r.LoginDate <= query.LSEndTime);
            }

            List<CacheDto> DQCacheList;
            List<CacheDto> LSCacheList;
            if (query.WeekQuery) //周返回结果
            {
                DQCacheList = data.Select(r => new CacheDto
                {
                    CreatTime = r.LoginDate.Value.Month.ToString() + "-" +
                                r.LoginDate.Value.Day.ToString(),
                    Register = r.RegisterState
                }).ToList();

                LSCacheList = data2.Select(r => new CacheDto
                {
                    CreatTime = r.LoginDate.Value.Month.ToString() + "-" +
                                r.LoginDate.Value.Day.ToString(),
                    Register = r.RegisterState
                }).ToList();
            }
            else //月、周返回结果
            {
                DQCacheList = data.Select(r => new CacheDto
                {
                    CreatTime = r.LoginDate.Value.Month.ToString() + "月",
                    Register = r.RegisterState
                }).ToList();

                LSCacheList = data2.Select(r => new CacheDto
                {
                    CreatTime = r.LoginDate.Value.Month.ToString() + "月",
                    Register = r.RegisterState
                }).ToList();
            }

            var DQqueryList = DQCacheList.GroupBy(r => new { r.CreatTime })
                .Select(r => new KSHBGZLStatisticsDto
                {
                    Type = r.Key.CreatTime,
                    CurrentData = r.Count(a => a.Register == 2)
                }).ToList();

            var LSqueryList = LSCacheList.GroupBy(r => new { r.CreatTime })
                .Select(r => new KSHBGZLStatisticsDto
                {
                    Type = r.Key.CreatTime,
                    ComparativeData = r.Count(a => a.Register == 2)
                }).ToList();

            //数据合并显示结果
            var result = from a in DQqueryList
                         join b in LSqueryList
                             on a.Type equals b.Type
                              into subParent
                         from c in subParent.DefaultIfEmpty()
                         select new KSHBGZLStatisticsDto
                         {
                             Type = a.Type,
                             CurrentData = a.CurrentData,
                             ComparativeData = c?.ComparativeData
                         };
            return result.OrderBy(r => r.Type).ToList();
        }

        /// <summary>
        /// 科室工作量统计（图形）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable KSGZLSTStatistics(HBQueryClass query)
        {
            var data = _customerItemGroupRepository.GetAllIncluding(r => r.CustomerRegBM, a => a.InspectEmployeeBM).Select(
                o => new { o.CustomerRegBM.LoginDate, o.DepartmentId, o.DepartmentName });
            data = data.Where(r => query.DepartmentBMList.Contains(r.DepartmentId));

            if (query.DQStartTime != null)
                data = data.Where(r => r.LoginDate >= query.DQStartTime);
            if (query.DQEndTime != null)
                data = data.Where(r => r.LoginDate <= query.DQEndTime);
            DataTable dt = new DataTable();

            if (query.WeekQuery) //周返回结果
            {
                dt.Columns.Add("周期", typeof(string));
                foreach (var item in data.GroupBy(r => r.DepartmentName).ToList())
                {
                    dt.Columns.Add(item.Key, typeof(int));
                    dt.Columns[item.Key].DefaultValue = 0;
                }
                var DQqueryList = data.GroupBy(r => new
                {
                    CreatTime = r.LoginDate.Value.Month.ToString() + "-" +
                                r.LoginDate.Value.Day.ToString()
                }).ToList();
                foreach (var groupitem in DQqueryList)
                {
                    var dr = dt.NewRow();
                    foreach (var cellValue in groupitem.GroupBy(n => n.DepartmentName))
                    {
                        dr[cellValue.Key] = cellValue.Count();
                    }
                    dr["周期"] = groupitem.Key.CreatTime;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                dt.Columns.Add("周期", typeof(string));
                foreach (var item in data.GroupBy(r => r.DepartmentName).ToList())
                {
                    dt.Columns.Add(item.Key, typeof(int));
                    dt.Columns[item.Key].DefaultValue = 0;
                }
                var DQqueryList = data.GroupBy(r => new { CreatTime = r.LoginDate.Value.Month }).ToList();
                foreach (var groupitem in DQqueryList)
                {
                    var dr = dt.NewRow();
                    foreach (var cellValue in groupitem.GroupBy(n => n.DepartmentName))
                    {
                        dr[cellValue.Key] = cellValue.Count();
                    }
                    dr["周期"] = groupitem.Key.CreatTime + "月";
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public InterfaceCustomerRegDto GetInterfaceCustomerReg(TdbInterfaceDocWhere tdbInterfaceWhere)
        {
            var queryCustomerRegisters = _customerRegRepository.FirstOrDefault(o => o.CustomerBM == tdbInterfaceWhere.inactivenum);
            return queryCustomerRegisters.MapTo<InterfaceCustomerRegDto>();
        }
        /// <summary>
        /// 获取接口数据不用token验证根据获取
        /// 信息
        /// </summary>
        /// <param name="tdbInterfaceWhere"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        //[AbpAllowAnonymous]
        public InterfaceBack ConvertInterface(TdbInterfaceDocWhere tdbInterfaceWhere)
        {

            var interfaceBack = new InterfaceBack { StrBui = new StringBuilder(), JKStrBui = new StringBuilder() };
            //try
            //{
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant))
                {
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    var customerReg = _customerRegRepository.FirstOrDefault(r => r.CustomerBM == tdbInterfaceWhere.inactivenum);
                    if (customerReg == null)
                    {
                        interfaceBack.StrBui.Append("该档案号没有数据！");
                        interfaceBack.JKStrBui.Append("无该体人！");
                        //添加操作日志  
                        //createOpLogDto.LogBM = tdbInterfaceWhere.inactivenum;
                        //createOpLogDto.LogName = tdbInterfaceWhere.inactivenum;
                        //createOpLogDto.LogText = "接口失败";
                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                        //createOpLogDto.LogDetail = interfaceBack.StrBui.ToString();
                        //_commonAppService.SaveOpLog(createOpLogDto);
                        return interfaceBack;
                    }

                    //已经总检不再获取数据
                    if (customerReg.SummSate == (int)SummSate.Audited || customerReg.SummSate == (int)SummSate.HasInitialReview)
                    {
                        interfaceBack.StrBui.Append("该档案号已经总检！");
                        interfaceBack.JKStrBui.Append("该档案号已经总检,不能更新数据！");
                        //添加操作日志  
                        //createOpLogDto.LogBM = customerReg.CustomerBM;
                        //createOpLogDto.LogName = customerReg.Customer.Name;
                        //createOpLogDto.LogText = "接口失败";
                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                        //createOpLogDto.LogDetail = interfaceBack.StrBui.ToString();
                        //_commonAppService.SaveOpLog(createOpLogDto);
                        return interfaceBack;
                    }
                    //已经总检不再获取数据
                    if (customerReg.RegisterState == (int)RegisterState.No)
                    {
                        interfaceBack.StrBui.Append("该档案号未登记！");
                        interfaceBack.JKStrBui.Append("该档案号未登记,不能保存数据！");
                        //添加操作日志  
                        //createOpLogDto.LogBM = customerReg.CustomerBM;
                        //createOpLogDto.LogName = customerReg.Customer.Name;
                        //createOpLogDto.LogText = "接口失败";
                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                        //createOpLogDto.LogDetail = interfaceBack.StrBui.ToString();
                        //_commonAppService.SaveOpLog(createOpLogDto);
                        return interfaceBack;
                    }
                    //租户Id
                    int tandId = customerReg.TenantId;
                    int Age = 0;
                    if (customerReg.Customer.Age == null)
                    {
                        if (customerReg.Customer.Birthday.HasValue)
                        {
                            Age = DateTime.Parse(customerReg.LoginDate.ToString()).Year - DateTime.Parse(customerReg.Customer.Birthday.ToString()).Year;
                        }
                    }
                    else
                    {
                        Age = (int)customerReg.Customer.Age;
                    }
                    string sex = customerReg.Customer.Sex.ToString();
                    string SexGenderNotSpecified = ((int)Sex.GenderNotSpecified).ToString();
                    // 获取接口数据
                    var lisInterfaceDriver = DriverFactory.GetDriver<ILisInterfaceDriver>();
                    var interfaceWhere = new TdbInterfaceWhere { ActiveNum = tdbInterfaceWhere.inactivenum };
                    var interfaceResult = lisInterfaceDriver.Convert(interfaceWhere).OrderByDescending(o => o.resultdate).ToList();
                    if (interfaceResult == null)
                    {
                        interfaceBack.StrBui.Append("该档案号没有检查数据！或驱动程序配置不正确！");
                        //添加操作日志  
                        //createOpLogDto.LogBM = customerReg.CustomerBM;
                        //createOpLogDto.LogName = customerReg.Customer.Name;
                        //createOpLogDto.LogText = "接口失败";
                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                        //createOpLogDto.LogDetail = interfaceBack.StrBui.ToString();
                        //_commonAppService.SaveOpLog(createOpLogDto);
                        return interfaceBack;
                    }
                    if (interfaceResult.Count == 0)
                    {
                        interfaceBack.StrBui.Append("此人没有接口结果！");
                        //添加操作日志  
                        //createOpLogDto.LogBM = customerReg.CustomerBM;
                        //createOpLogDto.LogName = customerReg.Customer.Name;
                        //createOpLogDto.LogText = "接口失败";
                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                        //createOpLogDto.LogDetail = interfaceBack.StrBui.ToString();
                        //_commonAppService.SaveOpLog(createOpLogDto);
                        return interfaceBack;
                    }
                    //多部位报告合并
                    //肝胆胰脾双肾彩超A;输尿管,膀胱彩超A;前列腺彩超A
                    var interfaceResulthb = interfaceResult.Where(p => p.initemid.Contains(";")).ToList();
                    if (interfaceResulthb.Count > 0)
                    {
                        for (int num = 0; num < interfaceResulthb.Count; num++)
                        {
                            var interls = interfaceResulthb[num];
                            interfaceResult.Remove(interls);
                            var obIitems = interls.initemid.Split(';').ToList();
                            foreach (var ob in obIitems)
                            {
                                var obItemId = ob;
                                if (interls.initemid.Contains("ZD") && !ob.Contains("ZD"))
                                { obItemId = ob + "ZD"; }
                                //报告单号如果是空就用档案号加项目编码
                                var BarNum = "&" + interls.inactivenum + "_" + interls.initemid.Trim();
                                if (!string.IsNullOrEmpty(interls.barnum))
                                { BarNum = "&" + interls.barnum; }
                                interfaceResult.Add(new vw_TInterfaceresult
                                {
                                    checkdate = interls.checkdate,
                                    barnum = BarNum,
                                    inactivenum = interls.inactivenum,
                                    idnum = interls.idnum,
                                    indoctorid = interls.indoctorid,
                                    initemchar = interls.initemchar,
                                    initemid = obItemId,
                                    inPicDirs = interls.inPicDirs,
                                    inSHdoctorid = interls.inSHdoctorid,
                                    invalue = interls.invalue,
                                    inYQid = interls.inYQid,
                                    resultdate = interls.resultdate,
                                    resultstate = interls.resultstate,
                                    wjz = interls.wjz,
                                    xmbs = interls.xmbs,
                                    xmckz = interls.xmckz,
                                    xmdw = interls.xmdw

                                });
                                //添加操作日志  
                                //createOpLogDto.LogBM = customerReg.CustomerBM;
                                //createOpLogDto.LogName = customerReg.Customer.Name;
                                //createOpLogDto.LogText = "拆分接口：" + obItemId;
                                //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                //createOpLogDto.LogDetail = "项目" + interls.initemid + "拆分接口：" + obItemId;
                                //_commonAppService.SaveOpLog(createOpLogDto);
                            }
                            interfaceResult = interfaceResult.OrderByDescending(o => o.resultdate).ToList();

                        }

                    }                   
                    //纸张报告不判断
                    var pgroup = _basicDictionaryRepository.GetAll().Where(o => o.Type == "PayerGroup").ToList();
                    var customerRegItemQuery =
                            _customerRegItemRepository.GetAll().Where(r => r.CustomerRegId == customerReg.Id && r.ProcessState != (int)ProjectIState.GiveUp
                            && r.CustomerItemGroupBM.CheckState != (int)ProjectIState.GiveUp);
                    if (tdbInterfaceWhere.departmentID.HasValue)
                    {
                        customerRegItemQuery =
                            customerRegItemQuery.Where(r => r.DepartmentId == tdbInterfaceWhere.departmentID && r.ProcessState != (int)ProjectIState.GiveUp);
                    }
                    var customerRegItems = customerRegItemQuery.ToList();
                    List<Guid> bmItemGroupGuid = new List<Guid>();
                    List<Guid> HCGroupIDs = new List<Guid>();
                    List<Guid> HCItemIDs = new List<Guid>();
                    List<Guid> DepartIDs = new List<Guid>();//生成小结
                                                            //是否显示正常值
                    string iszcz = "";
                    string isXMJG = "";
                    var isShow = _basicDictionaryRepository.FirstOrDefault(o => o.Type == "CusSumSet" && o.Value == 4 && o.TenantId == tandId)?.Remarks;
                    if (!string.IsNullOrEmpty(isShow))
                    {
                        iszcz = isShow;
                    }
                    var isShowxmjg = _basicDictionaryRepository.FirstOrDefault(o => o.Type == "CusSumSet" && o.Value == 9 && o.TenantId == tandId)?.Remarks;
                    if (!string.IsNullOrEmpty(isShowxmjg))
                    {
                        isXMJG = isShowxmjg;
                    }
                    string groupErr = "";
                    
                    foreach (var customerRegItem in customerRegItems)
                    {
                        if (customerRegItem.CrisisVisitSate == 3)
                        {
                            interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 危急值已审核不能修改结果！");
                            //添加操作日志  
                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                            //createOpLogDto.LogName = customerReg.Customer.Name;
                            //createOpLogDto.LogText = "接口失败";
                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                            //createOpLogDto.LogDetail = $"项目：{customerRegItem.ItemName} 危急值已审核不能修改结果！";
                            //_commonAppService.SaveOpLog(createOpLogDto);
                            if (!groupErr.Contains(customerRegItem.ItemGroupBM?.ItemGroupName))
                            {
                                groupErr += customerRegItem.ItemGroupBM?.ItemGroupName;
                                interfaceBack.JKStrBui.Append(customerRegItem.ItemGroupBM?.ItemGroupName + "上传失败。（危急值已审核不能修改结果）；");

                            }
                            continue;
                        }

                        vw_TInterfaceresult interfaceResultItem = new vw_TInterfaceresult();
                        var contrastItemls = _interfaceItemComparison.GetAll().Where(r => r.ItemInfoId == customerRegItem.ItemId && r.TenantId == tandId 
                        && r.ObverseItemId !="" && r.ObverseItemId !=null).ToList();
                        if (contrastItemls == null || contrastItemls.Count == 0)
                        {
                            //新增走体检编码无需项目对应
                            interfaceResultItem = interfaceResult.FirstOrDefault(r =>
                                  r.initemid == customerRegItem.ItemBM?.ItemBM);
                            if (interfaceResultItem == null)
                            {
                                // 这样判断会多出一部分数据
                                // 因为本来就不是所有的项目都有对应
                                interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 缺少第三方系统对应！");
                                continue;
                            }
                        }
                        else
                        {
                            List<vw_TInterfaceresult> vw_TInterfaceresultlist = new List<vw_TInterfaceresult>();
                            foreach (var contrastItem in contrastItemls)
                            {
                                if (string.IsNullOrWhiteSpace(contrastItem.InstrumentModelNumber))
                                {
                                    
                                  var   interfaceResultItemls = interfaceResult.FirstOrDefault(r =>
                                    r.initemid == contrastItem.ObverseItemId);
                                    if (interfaceResultItemls != null)
                                    {
                                        vw_TInterfaceresultlist.Add(interfaceResultItemls);
                                    }
                                 //     interfaceResultItem = interfaceResult.FirstOrDefault(r =>
                                 //r.initemid == contrastItem.ObverseItemId);
                                    //if (interfaceResultItem != null && !string.IsNullOrEmpty(interfaceResultItem.inactivenum))
                                    //{
                                    //    break;
                                    //}
                                }
                                else
                                {
                                    var interfaceResultItemls = interfaceResult.FirstOrDefault(r =>
                                       r.initemid == contrastItem.ObverseItemId && r.inYQid == contrastItem.InstrumentModelNumber);
                                    if (interfaceResultItemls != null)
                                    {
                                        vw_TInterfaceresultlist.Add(interfaceResultItemls);
                                    }

                                    //interfaceResultItem = interfaceResult.FirstOrDefault(r =>
                                    //   r.initemid == contrastItem.ObverseItemId && r.inYQid == contrastItem.InstrumentModelNumber);
                                    //if (interfaceResultItem != null && !string.IsNullOrEmpty(interfaceResultItem.inactivenum))
                                    //{
                                    //    break;
                                    //}
                                }
                            }
                            if (vw_TInterfaceresultlist != null && vw_TInterfaceresultlist.Count > 0)
                            {
                                interfaceResultItem = vw_TInterfaceresultlist.OrderByDescending(p => p.resultdate).ToList().FirstOrDefault();
                            }
                        }
                        if (interfaceResultItem == null || (string.IsNullOrWhiteSpace(interfaceResultItem.invalue) && string.IsNullOrWhiteSpace(interfaceResultItem.initemchar)))
                        {
                            interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 第三方系统未返回数据！");
                            //添加操作日志  
                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                            //createOpLogDto.LogName = customerReg.Customer.Name;
                            //createOpLogDto.LogText = "接口失败";
                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                            //createOpLogDto.LogDetail = $"项目：{customerRegItem.ItemName} 第三方系统未返回数据！";
                            //_commonAppService.SaveOpLog(createOpLogDto);
                            if (!groupErr.Contains(customerRegItem.ItemGroupBM?.ItemGroupName))
                            {
                                groupErr += customerRegItem.ItemGroupBM?.ItemGroupName;
                                interfaceBack.JKStrBui.Append(customerRegItem.ItemGroupBM?.ItemGroupName + "上传失败。（第三方系统未返回数据）；");
                            }
                            continue;
                        }

                        var interfaceEmployee = _interfaceEmployeeComparison.FirstOrDefault(r =>
                            r.ObverseEmpId == interfaceResultItem.indoctorid && r.TenantId == tandId);

                        if (interfaceEmployee == null)
                        {
                            interfaceEmployee = new TdbInterfaceEmployeeComparison();
                            //interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 第三方系统没有对应检查医生！");
                            //yhh修改没有医生创建医生
                            var userName = _userRepository.FirstOrDefault(r => 
                            (r.Name == interfaceResultItem.indoctorid || r.EmployeeNum== interfaceResultItem.indoctorid 
                            || r.Name== interfaceResultItem.indoctorname)
                            && r.TenantId == tandId);
                            if (userName != null)
                            {
                                interfaceEmployee.EmployeeId = userName.Id;
                                interfaceEmployee.EmployeeName = userName.Name;
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(interfaceResultItem.indoctorid))
                                {
                                    interfaceBack.StrBui.Append($"项目：{customerRegItem.ItemName} 检查医生不能为空！");
                                    //添加操作日志  
                                    //createOpLogDto.LogBM = customerReg.CustomerBM;
                                    //createOpLogDto.LogName = customerReg.Customer.Name;
                                    //createOpLogDto.LogText = "接口失败";
                                    //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                    //createOpLogDto.LogDetail = $"项目：{customerRegItem.ItemName} 检查医生不能为空！";
                                    //_commonAppService.SaveOpLog(createOpLogDto);
                                    continue;
                                }
                                var empName = interfaceResultItem.indoctorid;
                                if (!string.IsNullOrEmpty( interfaceResultItem.indoctorname))
                                {
                                    empName = interfaceResultItem.indoctorname;
                                }
                                var createUserFormDto = new CreateUserFormTeandIDDto
                                {
                                    UserName = System.DateTime.Now.ToString("yyMMddHHmmss"),
                                    EmployeeNum = interfaceResultItem.indoctorid,
                                    Name = empName,
                                    Password = "123qwe",
                                    HelpChar = "",
                                    Sex = 1,
                                    EmailAddress = System.DateTime.Now.ToString("yyMMddHHmmss") + "@qq.com",
                                    State = 1,
                                    DomainName = "",
                                    Duty = "",
                                    IsActive = true,
                                    Address = "",
                                    Discount = "1",
                                    FormRoleIds = new List<Guid>(),
                                    DepartmentIds = new List<Guid>(),
                                    TenantId = tandId

                                };
                                _userAppService.CreateUserHasTeandID(createUserFormDto);
                                userName = _userRepository.FirstOrDefault(r => r.Name == interfaceResultItem.indoctorid);
                                interfaceEmployee.EmployeeId = userName.Id;
                                interfaceEmployee.EmployeeName = userName.Name;
                            }
                            // continue;
                        }

                        var interfaceEmployeeSh = _interfaceEmployeeComparison.FirstOrDefault(r =>
                            r.ObverseEmpId == interfaceResultItem.inSHdoctorid);
                        if (interfaceEmployeeSh == null)
                        {
                            // interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 第三方系统没有对应审核医生！");

                            interfaceEmployeeSh = new TdbInterfaceEmployeeComparison();
                            //yhh修改没有医生创建医生
                            var userName = _userRepository.FirstOrDefault(r => (r.Name == interfaceResultItem.inSHdoctorid 
                            || r.Name == interfaceResultItem.inSHdoctorname || r.EmployeeNum== interfaceResultItem.inSHdoctorid)
                            && r.TenantId == tandId);
                            if (userName != null)
                            {
                                interfaceEmployeeSh.EmployeeId = userName.Id;
                                interfaceEmployeeSh.EmployeeName = userName.Name;
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(interfaceResultItem.inSHdoctorid))
                                {
                                    interfaceBack.StrBui.Append($"项目：{customerRegItem.ItemName} 审核医生不能为空！");
                                    if (!groupErr.Contains(customerRegItem.ItemGroupBM?.ItemGroupName))
                                    {
                                        groupErr += customerRegItem.ItemGroupBM?.ItemGroupName;
                                        interfaceBack.JKStrBui.Append(customerRegItem.ItemGroupBM?.ItemGroupName + "上传失败。（审核医生不能为空）；");
                                    }
                                    //添加操作日志  
                                    //createOpLogDto.LogBM = customerReg.CustomerBM;
                                    //createOpLogDto.LogName = customerReg.Customer.Name;
                                    //createOpLogDto.LogText = "接口失败";
                                    //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                    //createOpLogDto.LogDetail = $"项目：{customerRegItem.ItemName} 审核医生不能为空！";
                                    //_commonAppService.SaveOpLog(createOpLogDto);
                                    continue;
                                }
                                var shEmpName = interfaceResultItem.inSHdoctorid;
                                if (!string.IsNullOrEmpty(interfaceResultItem.inSHdoctorname))
                                {
                                    shEmpName = interfaceResultItem.inSHdoctorname;
                                }
                                var createUserFormDto = new CreateUserFormTeandIDDto
                                {
                                    UserName = System.DateTime.Now.ToString("yyMMddHHmmss") + "1",
                                    EmployeeNum = interfaceResultItem.inSHdoctorid,
                                    Name = shEmpName,
                                    Password = "123qwe",
                                    HelpChar = "",
                                    Sex = 1,
                                    EmailAddress = System.DateTime.Now.ToString("yyMMddHHmmss") + "1@qq.com",
                                    State = 1,
                                    DomainName = "",
                                    Duty = "",
                                    IsActive = true,
                                    Address = "",
                                    Discount = "1",
                                    FormRoleIds = new List<Guid>(),
                                    DepartmentIds = new List<Guid>(),
                                    TenantId = tandId
                                };
                                _userAppService.CreateUserHasTeandID(createUserFormDto);
                                userName = _userRepository.FirstOrDefault(r => r.Name == interfaceResultItem.inSHdoctorid);
                                interfaceEmployeeSh.EmployeeId = userName.Id;
                                interfaceEmployeeSh.EmployeeName = userName.Name;
                            }
                            // continue;
                        }
                        string xmckz = "";
                        if (iszcz != "N")
                        {
                            if (!string.IsNullOrEmpty(interfaceResultItem.xmckz))
                            {
                                xmckz = $"（正常值：{interfaceResultItem.xmckz})";
                            }
                        }

                        //使用体检参考值 ,项目标识是空
                        if (string.IsNullOrEmpty(interfaceResultItem.xmbs) && interfaceResultItem.inYQid == "TJZX")
                        {
                            if (!string.IsNullOrEmpty(customerRegItem.ItemBM?.Unit) &&  
                                string.IsNullOrEmpty(interfaceResultItem.xmdw))
                            {
                                interfaceResultItem.xmdw = customerRegItem.ItemBM?.Unit;
                            }
                            if (customerRegItem.ItemBM?.moneyType == (int)ItemType.Explain || customerRegItem.ItemBM?.moneyType == (int)ItemType.YinYang)
                            {
                                interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 项目标示为空！");
                                var vitemstand = _itemStandardRepository.FirstOrDefault(r => r.PositiveSate == 2 && customerRegItem.ItemId == r.ItemId && r.TenantId == tandId);
                                if (vitemstand != null)
                                {
                                    try
                                    {
                                        interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 项目参考值:" + vitemstand.Summ
                                       + ";项目结果:" + interfaceResultItem.invalue + "！");
                                        if (vitemstand.Summ.ToString().Replace(" ", "").ToUpper() != interfaceResultItem.invalue.Replace(" ", "").ToUpper())
                                        {
                                            interfaceResultItem.xmbs = "P";
                                        }
                                        else
                                        {
                                            interfaceResultItem.xmbs = "M";
                                        }
                                        interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 提示:" + interfaceResultItem.xmbs);
                                    }
                                    catch (Exception ex)
                                    {

                                        interfaceBack.StrBui.AppendLine($"错误：{customerRegItem.ItemName} 提示:" + ex.Message + ex.StackTrace);
                                        if (!groupErr.Contains(customerRegItem.ItemGroupBM?.ItemGroupName))
                                        {
                                            groupErr += customerRegItem.ItemGroupBM?.ItemGroupName;
                                            interfaceBack.JKStrBui.Append(customerRegItem.ItemGroupBM?.ItemGroupName + "上传失败。（" + ex.Message + "）；");
                                        }
                                    }
                                    if (customerRegItem.ItemBM?.moneyType == (int)ItemType.YinYang)
                                    {
                                        interfaceResultItem.xmckz = vitemstand.Summ.ToString().Replace(" ", "");
                                    }
                                }
                                else
                                {
                                    interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 未获取到参考值！");
                                    interfaceResultItem.xmbs = "M";
                                }
                                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"AppServiceLog-{DateTime.Now:yyyyMMdd}.txt"), interfaceBack.ToString());
                            }
                            else
                            {
                                if (decimal.TryParse(interfaceResultItem.invalue, out decimal rtItemResultChar))
                                {
                                    interfaceBack.StrBui.AppendLine($"项目：{customerRegItem.ItemName} 已走体检参考值！" + ";项目结果:" + interfaceResultItem.invalue + "！");
                                    //var rtItemResultChar = Convert.ToDecimal(customerRegItem.ItemResultChar);
                                    var ItemStandardDto = _itemStandardRepository.FirstOrDefault(o => o.TenantId == tandId 
                                    && o.ItemId == customerRegItem.ItemId
                                    && o.MinAge <= Age && o.MaxAge >= Age && 
                                    (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar);
                                   
                                    if (ItemStandardDto != null && ItemStandardDto.PositiveSate == (int)PositiveSate.Abnormal)
                                    { //判断是否异常
                                        if (string.IsNullOrEmpty(xmckz) && ItemStandardDto.MaxValue.HasValue && ItemStandardDto.MaxValue.HasValue)
                                        {
                                            var NomarlStandard = _itemStandardRepository.FirstOrDefault(o => o.TenantId == tandId
                                   && o.ItemId == customerRegItem.ItemId
                                   && o.MinAge <= Age && o.MaxAge >= Age &&
                                   (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.IsNormal== (int)Symbol.Normal);

                                            if (NomarlStandard != null && NomarlStandard.MinValue.HasValue && NomarlStandard.MaxValue.HasValue)
                                            {
                                                xmckz = NomarlStandard.MinValue.Value.ToString("0.##") + "~" + NomarlStandard.MaxValue.Value.ToString("0.##");
                                                xmckz = $"（正常值：{xmckz})";
                                                if (string.IsNullOrEmpty(interfaceResultItem.xmckz))
                                                {
                                                    interfaceResultItem.xmckz = xmckz;
                                                }
                                                 //小结不显示参考值
                                                if (iszcz == "N")
                                                {
                                                    xmckz = "";
                                                }
                                            }
                                        }


                                        if (ItemStandardDto.IsNormal == (int)Symbol.High)
                                        {

                                            interfaceResultItem.xmbs = "↑";
                                        }
                                        else if (ItemStandardDto.IsNormal == (int)Symbol.Superhigh)
                                        {
                                            interfaceResultItem.xmbs = "↑↑";

                                        }
                                        else if (ItemStandardDto.IsNormal == (int)Symbol.Low)
                                        {

                                            interfaceResultItem.xmbs = "↓";
                                        }
                                        else if (ItemStandardDto.IsNormal == (int)Symbol.UltraLow)
                                        {

                                            interfaceResultItem.xmbs = "↓↓";
                                        }
                                        else
                                        {
                                            interfaceResultItem.xmbs = "P";
                                            if (string.IsNullOrEmpty(interfaceResultItem.initemchar))
                                            {
                                              
                                                if (!string.IsNullOrEmpty(ItemStandardDto.Summ))
                                                {
                        
                                                    interfaceResultItem.initemchar = ItemStandardDto.Summ + interfaceResultItem.invalue + interfaceResultItem.xmdw + xmckz; 

                                                }
                                                else
                                                {
                                                    interfaceResultItem.initemchar =  interfaceResultItem.invalue  +interfaceResultItem.xmdw + xmckz;
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        interfaceResultItem.xmbs = "M";
                                    }
                                    var normal = _itemStandardRepository.FirstOrDefault(o => o.TenantId == tandId && o.ItemId == customerRegItem.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.PositiveSate == (int)PositiveSate.Normal);
                                    if (normal != null)
                                    {
                                        interfaceResultItem.xmckz = normal.MinValue.Value.ToString("0.##") + "~" + normal.MaxValue.Value.ToString("0.##");
                                    }

                                }
                            }
                        }

                        customerRegItem.InspectEmployeeBMId = interfaceEmployee.EmployeeId;
                        customerRegItem.CheckEmployeeBMId = interfaceEmployeeSh.EmployeeId;
                        customerRegItem.ItemResultChar = interfaceResultItem.invalue;
                        customerRegItem.IllnessLevel = 0;
                        //string xmckz = "";
                        //if (iszcz != "N")
                        //{
                        //    xmckz = $"（正常值：{interfaceResultItem.xmckz})";
                        //}

                        if (interfaceResultItem.xmbs == "↓")
                        {
                            var sumsym = "偏低";
                            //重度等级
                            if (decimal.TryParse(customerRegItem.ItemResultChar, out decimal rtItemResultChar))
                            {
                                //var rtItemResultChar = Convert.ToDecimal(customerRegItem.ItemResultChar);
                                //使用体检结论
                                var ItemStandardDto = _itemStandardRepository.FirstOrDefault(o => o.TenantId == tandId && o.ItemId == customerRegItem.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar
                                && o.IsNormal== (int)Symbol.Low);

                                if (ItemStandardDto != null)
                                {
                                    customerRegItem.IllnessLevel = ItemStandardDto.IllnessLevel;
                                    if (!string.IsNullOrWhiteSpace(ItemStandardDto.Summ))
                                    {
                                        sumsym = ItemStandardDto.Summ;
                                    }
                                }
                            }

                            if (isXMJG == "N")
                            {
                                customerRegItem.ItemSum = $"{sumsym}{xmckz}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}{xmckz}";

                            }
                            else
                            {
                                customerRegItem.ItemSum =
                               $"{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↓{xmckz}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↓{xmckz}";
                            }
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Low);
                            customerRegItem.CrisisSate = (int)CrisisSate.Normal;
                            customerRegItem.IllnessSate = (int)IllnessSate.Abnormal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Abnormal;


                        }
                        else if (interfaceResultItem.xmbs == "↓↓")
                        {
                            //重度等级
                            var sumsym = "极低";
                            if (decimal.TryParse(customerRegItem.ItemResultChar, out decimal rtItemResultChar))
                            {
                                //var rtItemResultChar = Convert.ToDecimal(customerRegItem.ItemResultChar);
                                var ItemStandardDto = _itemStandardRepository.FirstOrDefault(o => o.TenantId == tandId && o.ItemId == customerRegItem.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar
                                && o.IsNormal == (int)Symbol.UltraLow);

                                if (ItemStandardDto != null)
                                {
                                    customerRegItem.IllnessLevel = ItemStandardDto.IllnessLevel;
                                    if (!string.IsNullOrWhiteSpace(ItemStandardDto.Summ))
                                    {
                                        sumsym = ItemStandardDto.Summ;
                                    }
                                }
                            }

                            if (isXMJG == "N")
                            {
                                customerRegItem.ItemSum = $"{sumsym}{xmckz}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}{xmckz}";

                            }
                            else
                            {
                                customerRegItem.ItemSum =
                                $"{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↓↓{xmckz}";

                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↓↓{xmckz}";
                            }
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);
                            customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                            customerRegItem.IllnessSate = (int)IllnessSate.Abnormal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Abnormal;
                        }
                        else if (interfaceResultItem.xmbs == "↑")
                        {
                            //重度等级
                            var sumsym = "偏高";
                            if (decimal.TryParse(customerRegItem.ItemResultChar, out decimal rtItemResultChar))
                            {
                                //var rtItemResultChar = Convert.ToDecimal(customerRegItem.ItemResultChar);
                                var ItemStandardDto = _itemStandardRepository.FirstOrDefault(o => o.TenantId == tandId && o.ItemId == customerRegItem.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar
                                     && o.IsNormal == (int)Symbol.High);
                                if (ItemStandardDto != null)
                                {
                                    customerRegItem.IllnessLevel = ItemStandardDto.IllnessLevel;
                                    if (!string.IsNullOrWhiteSpace(ItemStandardDto.Summ))
                                    {
                                        sumsym = ItemStandardDto.Summ;
                                    }
                                }
                            }
                            if (isXMJG == "N")
                            {
                                customerRegItem.ItemSum = $"{sumsym}{xmckz}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}{xmckz}";
                                if (customerRegItem.ItemTypeBM == (int)ItemType.YinYang)
                                {
                                    customerRegItem.ItemSum = $"阳性{xmckz}";
                                    customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}阳性{xmckz}";
                                }
                            }
                            else
                            {
                                customerRegItem.ItemSum =
                                    $"{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↑{xmckz}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↑{xmckz}";
                                if (customerRegItem.ItemTypeBM == (int)ItemType.YinYang)
                                {
                                    customerRegItem.ItemSum =
                                   $"阳性({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↑{xmckz}";
                                    customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}阳性({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↑{xmckz}";
                                }
                            }
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.High);
                            customerRegItem.CrisisSate = (int)CrisisSate.Normal;
                            customerRegItem.IllnessSate = (int)IllnessSate.Abnormal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Abnormal;
                        }
                        else if (interfaceResultItem.xmbs == "↑↑")
                        {
                            //重度等级
                            var sumsym = "极高";
                            if (decimal.TryParse(customerRegItem.ItemResultChar, out decimal rtItemResultChar))
                            {
                                // var rtItemResultChar = Convert.ToDecimal(customerRegItem.ItemResultChar);
                                var ItemStandardDto = _itemStandardRepository.FirstOrDefault(o => o.TenantId == tandId && o.ItemId == customerRegItem.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar
                                 && o.IsNormal == (int)Symbol.Superhigh);

                                if (ItemStandardDto != null)
                                {
                                    customerRegItem.IllnessLevel = ItemStandardDto.IllnessLevel;
                                    if (!string.IsNullOrWhiteSpace(ItemStandardDto.Summ))
                                    {
                                        sumsym = ItemStandardDto.Summ;
                                    }
                                }
                            }
                            if (isXMJG == "N")
                            {
                                customerRegItem.ItemSum = $"{sumsym}{xmckz}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}{xmckz}";

                            }
                            else
                            {
                                customerRegItem.ItemSum =
                                    $"{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↑↑{xmckz}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{sumsym}({interfaceResultItem.invalue}{interfaceResultItem.xmdw})↑↑{xmckz}";
                            }
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);
                            customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                            customerRegItem.IllnessSate = (int)IllnessSate.Abnormal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Abnormal;
                        }
                        else if (interfaceResultItem.xmbs == "阴性" || string.IsNullOrWhiteSpace(interfaceResultItem.xmbs))
                        {
                            customerRegItem.ItemSum =
                                $"{interfaceResultItem.invalue}";
                            customerRegItem.ItemDiagnosis = $"{interfaceResultItem.initemchar}";

                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                            customerRegItem.CrisisSate = (int)CrisisSate.Normal;
                            customerRegItem.IllnessSate = (int)IllnessSate.Abnormal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Normal;
                        }
                        else if (interfaceResultItem.xmbs == "阳性")
                        {
                            customerRegItem.ItemSum = $"{interfaceResultItem.invalue}";
                            customerRegItem.ItemDiagnosis = $"{interfaceResultItem.initemchar}";
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                            if (interfaceResultItem.wjz == "1")
                            {
                                customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                                customerRegItem.CrisisLever = 1;
                                customerRegItem.CrisiChar = "";
                                customerRegItem.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                            }
                            if (interfaceResultItem.wjz == "2")
                            {
                                customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                                customerRegItem.CrisisLever = 2;
                                customerRegItem.CrisiChar = "";
                                customerRegItem.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                            }
                            else
                            {
                                customerRegItem.CrisisSate = (int)CrisisSate.Normal;
                            }

                            customerRegItem.IllnessSate = (int)IllnessSate.Abnormal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Abnormal;
                        }
                        else if (interfaceResultItem.xmbs == "P")
                        {
                            customerRegItem.ItemSum = $"{interfaceResultItem.invalue}";
                            customerRegItem.ItemDiagnosis = $"{interfaceResultItem.initemchar}";
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                            if (interfaceResultItem.wjz == "1")
                            {
                                customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;

                               
                                customerRegItem.CrisisLever = 1;
                                customerRegItem.CrisiChar = "";
                                customerRegItem.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                            }
                            if (interfaceResultItem.wjz == "2")
                            {
                                customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                                customerRegItem.CrisisLever = 2;
                                customerRegItem.CrisiChar = "";
                                customerRegItem.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                            }
                            else
                            {
                                customerRegItem.CrisisSate = (int)CrisisSate.Normal;
                            }
                            customerRegItem.IllnessSate = (int)IllnessSate.Abnormal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Abnormal;
                        }
                        else if (interfaceResultItem.xmbs == "M")
                        {
                            // 正常都进入小结的科室
                            var strjcDepartment = _basicDictionaryRepository.FirstOrDefault(o => o.Type == "DepatSumSet" && o.Value == 2  )?.Remarks;
                            
                            if (strjcDepartment!=null && strjcDepartment.Contains(customerRegItem.DepartmentBM?.Name))
                            {
                                customerRegItem.ItemSum =
                                 $"{interfaceResultItem.invalue}";
                                customerRegItem.ItemDiagnosis = $"{interfaceResultItem.initemchar}";
                            }
                            else
                            {
                                customerRegItem.ItemSum = "";
                                customerRegItem.ItemDiagnosis = "";
                            }
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                            customerRegItem.CrisisSate = (int)CrisisSate.Normal;
                            customerRegItem.IllnessSate = (int)IllnessSate.Normal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Normal;
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(interfaceResultItem.initemchar))
                            {

                                if (string.IsNullOrWhiteSpace(interfaceResultItem.xmckz))
                                {
                                    customerRegItem.ItemSum = $"{interfaceResultItem.invalue}{interfaceResultItem.xmdw}";
                                }
                                else
                                {
                                    customerRegItem.ItemSum = $"{interfaceResultItem.invalue}{interfaceResultItem.xmdw}{xmckz}";
                                }
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{customerRegItem.ItemSum}";
                            }
                            else
                            {
                                customerRegItem.ItemSum = $"{interfaceResultItem.invalue}";
                                customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{interfaceResultItem.initemchar}";
                            }
                            customerRegItem.ItemSum = $"{interfaceResultItem.invalue}";
                            customerRegItem.ItemDiagnosis = $"{customerRegItem.ItemName}{interfaceResultItem.initemchar}";
                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                            customerRegItem.CrisisSate = (int)CrisisSate.Normal;
                            customerRegItem.PositiveSate = (int)PositiveSate.Abnormal;
                        }
                        customerRegItem.ProcessState = (int)ProjectIState.Complete;
                        if (interfaceResultItem.initemid != "3215")
                        {
                            customerRegItem.Stand = interfaceResultItem.xmckz;
                        }
                        else
                        {
                            customerRegItem.Stand = "阴性(-)";
                        }
                        customerRegItem.Stand = interfaceResultItem.xmckz;
                        customerRegItem.Unit = interfaceResultItem.xmdw;
                        if (!DepartIDs.Contains(customerRegItem.DepartmentId))
                        {
                            DepartIDs.Add(customerRegItem.DepartmentId);

                        }
                        #region 危急值处理

                        var cricalset = _TbmCriticalSet.GetAll().Where(p => p.ItemInfoId == customerRegItem.ItemId).ToList();
                        if (cricalset.Count > 0 && customerRegItem.CrisisVisitSate != 3)
                        {
                            bool isWJZ = false;
                            foreach (var cset in cricalset)
                            {
                                //性别过滤
                                if (cset.Sex.HasValue && cset.Sex != (int)Sex.GenderNotSpecified && cset.Sex != customerReg?.Customer?.Sex)
                                {
                                    continue;
                                }
                                //往年结果
                                if (cset.Old.HasValue && cset.Old == 1)
                                {
                                    var isOldOk = isOld(customerReg.Id, customerRegItem.ItemId, customerRegItem.ItemBM?.ItemBM, cset);
                                    if (isOldOk != false)
                                    {
                                        continue;
                                    }
                                }

                                if (cset.CalculationType == (int)CalculationTypeState.Numerical && decimal.TryParse(customerRegItem.ItemResultChar, out decimal ValueNum))
                                {
                                    switch (cset.Operator)
                                    {
                                        case (int)OperatorState.Big:
                                            if (ValueNum > cset.ValueNum)
                                            {
                                                isWJZ = true;
                                                customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);

                                            }
                                            break;
                                        case (int)OperatorState.BigEqual:
                                            if (ValueNum >= cset.ValueNum)
                                            {
                                                isWJZ = true;
                                                customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);

                                            }
                                            break;
                                        case (int)OperatorState.Small:
                                            if (ValueNum < cset.ValueNum)
                                            {
                                                isWJZ = true;
                                                customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                            }
                                            break;
                                        case (int)OperatorState.SmallEqual:
                                            if (ValueNum <= cset.ValueNum)
                                            {
                                                isWJZ = true;
                                                customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                            }
                                            break;
                                        case (int)OperatorState.Equal:
                                            if (ValueNum == cset.ValueNum)
                                            {
                                                isWJZ = true;
                                                customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                            }
                                            break;
                                    }
                                    if (isWJZ == true)
                                    {
                                        var isOK = true;
                                        var CriticalDetails = cset.CriticalDetails;
                                        if (CriticalDetails != null && CriticalDetails.Count > 0)
                                        {
                                            //并且所有项目结果都满足要求
                                            isOK = isZYYC(customerRegItem.CustomerRegId, CriticalDetails.ToList());
                                        }
                                        if (isOK)
                                        {
                                            //换双箭头
                                            if (customerRegItem.Symbol == SymbolHelper.SymbolFormatter(Symbol.Superhigh) && !customerRegItem.ItemDiagnosis.Contains("↑↑"))
                                            {
                                                customerRegItem.ItemSum = customerRegItem.ItemSum.Replace("↑", "↑↑");
                                                customerRegItem.ItemDiagnosis = customerRegItem.ItemDiagnosis.Replace("↑", "↑↑");

                                            }
                                            else if (customerRegItem.Symbol == SymbolHelper.SymbolFormatter(Symbol.UltraLow) && !customerRegItem.ItemDiagnosis.Contains("↓↓"))
                                            {
                                                customerRegItem.ItemSum = customerRegItem.ItemSum.Replace("↓", "↓↓");
                                                customerRegItem.ItemDiagnosis = customerRegItem.ItemDiagnosis.Replace("↓", "↓↓");
                                            }
                                            customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                                            customerRegItem.CrisisLever = cset.CriticalType;
                                            customerRegItem.CrisiChar = cset.ValueChar;
                                            customerRegItem.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                                        }
                                    }

                                }
                                else if (cset.CalculationType == (int)CalculationTypeState.Text)
                                {
                                    if (customerRegItem.ItemResultChar.Contains(cset.ValueChar))
                                    {
                                        if (cset.ValueNum.HasValue && cset.ValueNum>0)
                                        {
                                            #region 获取文本中的数值
                                            string str = customerRegItem.ItemResultChar;
                                            Regex reg = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");
                                            MatchCollection mc = reg.Matches(str);//设定要查找的字符串
                                            List<string> strlist = new List<string>();
                                            foreach (Match m in mc)
                                            {
                                                string s = m.Groups[0].Value;
                                                strlist.Add(s);
                                                str = str.Replace(s, "");
                                            }


                                            Regex reg1 = new Regex(@"[0-9]+");//2秒后超时
                                            MatchCollection mc1 = reg1.Matches(str);//设定要查找的字符串
                                            foreach (Match m in mc1)
                                            {
                                                string s = m.Groups[0].Value;
                                                strlist.Add(s);
                                            }

                                            foreach (var charNum in strlist)
                                            {
                                                var trrNum = decimal.Parse(charNum);
                                                switch (cset.Operator)
                                                {
                                                    case (int)OperatorState.Big:
                                                        if (trrNum > cset.ValueNum)
                                                        {
                                                            isWJZ = true;
                                                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);

                                                        }
                                                        break;
                                                    case (int)OperatorState.BigEqual:
                                                        if (trrNum >= cset.ValueNum)
                                                        {
                                                            isWJZ = true;
                                                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.Superhigh);

                                                        }
                                                        break;
                                                    case (int)OperatorState.Small:
                                                        if (trrNum < cset.ValueNum)
                                                        {
                                                            isWJZ = true;
                                                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                                        }
                                                        break;
                                                    case (int)OperatorState.SmallEqual:
                                                        if (trrNum <= cset.ValueNum)
                                                        {
                                                            isWJZ = true;
                                                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                                        }
                                                        break;
                                                    case (int)OperatorState.Equal:
                                                        if (trrNum == cset.ValueNum)
                                                        {
                                                            isWJZ = true;
                                                            customerRegItem.Symbol = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

                                                        }
                                                        break;
                                                }
                                                if (isWJZ == true)
                                                {
                                                    var isOK = true;
                                                    var CriticalDetails = cset.CriticalDetails;
                                                    if (CriticalDetails != null && CriticalDetails.Count > 0)
                                                    {
                                                        //并且所有项目结果都满足要求                                            
                                                        var customergroup = _customerItemGroupRepository.GetAll().Where(r => r.CustomerRegBMId == customerRegItem.CustomerRegId);
                                                        var dd = customergroup.MapTo<List<ATjlCustomerItemGroupDto>>();
                                                        isOK = isZYYC(customerRegItem.CustomerRegId, CriticalDetails.ToList());
                                                    }
                                                    if (isOK)
                                                    {
                                                        customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                                                        customerRegItem.CrisisLever = cset.CriticalType;
                                                        customerRegItem.CrisiChar = cset.ValueChar;
                                                    }
                                                }

                                            }
                                            #endregion



                                        }
                                        else
                                        {
                                            var isOK = true;
                                            var CriticalDetails = cset.CriticalDetails;
                                            if (CriticalDetails != null && CriticalDetails.Count > 0)
                                            {
                                                //并且所有项目结果都满足要求
                                                isOK = isZYYC(customerRegItem.CustomerRegId, CriticalDetails.ToList());
                                            }
                                            if (isOK)
                                            {
                                                customerRegItem.CrisisSate = (int)CrisisSate.Abnormal;
                                                customerRegItem.CrisisLever = cset.CriticalType;
                                                customerRegItem.CrisiChar = cset.ValueChar;
                                            }
                                        }
                                    }
                                }
                                if (isWJZ == true)
                                {
                                    break;

                                }
                            }
                        }
                        #endregion
                        if (customerRegItem.CrisisSate == (int)CrisisSate.Normal)
                        {
                            customerRegItem.CrisisLever = null;
                            customerRegItem.CrisiChar = "";
                        }
                        //修改报告单号
                        if (!string.IsNullOrEmpty(interfaceResultItem.barnum) && interfaceResultItem.barnum.Contains("&"))
                        {
                            customerRegItem.ReportBM = interfaceResultItem.barnum;
                            customerRegItem.CustomerItemGroupBM.ReportBM = interfaceResultItem.barnum;
                        }
                        //放射号
                        else if (!string.IsNullOrEmpty(interfaceResultItem.barnum))
                        {
                            //customerRegItem.ReportBM = interfaceResultItem.barnum;
                            customerRegItem.CustomerItemGroupBM.PacsBM = interfaceResultItem.barnum;
                            customerRegItem.CustomerRegBM.PacsBM = interfaceResultItem.barnum;
                        }
                        else
                        {
                            customerRegItem.ReportBM = "";
                            customerRegItem.CustomerItemGroupBM.ReportBM = "";
                        }
                        _customerRegItemRepository.Update(customerRegItem);
                         
                        //添加操作日志  
                        string value = customerRegItem.ItemResultChar;
                        if (customerRegItem.ItemResultChar!=null && customerRegItem.ItemResultChar.Length > 101)
                        {
                            value = customerRegItem.ItemResultChar.Substring(0, 100);
                        }
                        //createOpLogDto.LogBM = customerReg.CustomerBM;
                        //createOpLogDto.LogName = customerReg.Customer.Name;
                        //createOpLogDto.LogText = "保存接口成功";
                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                        //createOpLogDto.LogDetail = $"项目：{customerRegItem.ItemName} ：{value } ";
                        //_commonAppService.SaveOpLog(createOpLogDto);
                        //互斥项目
                        var iteminfo = _itemInfoRepository.Get(customerRegItem.ItemId);
                        if (iteminfo.ItemInfos != null && iteminfo.ItemInfos.Count > 0)
                        {
                            var itemids = iteminfo.ItemInfos.Select(o => o.Id).ToList();
                            foreach (Guid itemid in itemids)
                            {
                                if (!HCItemIDs.Contains(itemid))
                                {
                                    HCItemIDs.Add(itemid);
                                }
                            }
                            if (!HCGroupIDs.Contains(customerRegItem.ItemGroupBMId.Value))
                            {
                                HCGroupIDs.Add(customerRegItem.ItemGroupBMId.Value);
                            }
                        }
                        //增加组合医生信息
                        if (!bmItemGroupGuid.Contains(customerRegItem.ItemGroupBMId.Value))
                        {
                            var customerItemGroup = _customerItemGroupRepository.Get(customerRegItem.CustomerItemGroupBMid.Value);
                            customerItemGroup.InspectEmployeeBMId = interfaceEmployee.EmployeeId;
                            customerItemGroup.InspectEmployeeBM = interfaceEmployee.Employee;
                            customerItemGroup.CheckEmployeeBMId = interfaceEmployeeSh.EmployeeId;
                            customerItemGroup.CheckEmployeeBM = interfaceEmployeeSh.Employee;
                            customerItemGroup.FirstDateTime = interfaceResultItem.checkdate;//检查时间
                                                                                            //判断录入人是否存在
                            if (!string.IsNullOrWhiteSpace(interfaceResultItem.inLRdoctorid) || !string.IsNullOrWhiteSpace(interfaceResultItem.inLRdoctorname))
                            {
                                var interfaceEmployeeLR = _interfaceEmployeeComparison.FirstOrDefault(r =>
                                                            r.ObverseEmpId == interfaceResultItem.inLRdoctorid ||
                                                            r.ObverseEmpName == interfaceResultItem.inLRdoctorname);
                                if (interfaceEmployeeLR != null)
                                {
                                    customerItemGroup.BillingEmployeeBMId = interfaceEmployeeLR.EmployeeId;
                                    customerItemGroup.BillingEmployeeBM = interfaceEmployeeLR.Employee;
                                }
                            }
                            _customerItemGroupRepository.Update(customerItemGroup);
                            bmItemGroupGuid.Add(customerItemGroup.ItemGroupBM_Id.Value);
                        }
                        #region 图片处理
                        if (!string.IsNullOrWhiteSpace(interfaceResultItem.inPicDirs))
                        {
                            //interfaceBack.JKStrBui.Append("图片路径：" + interfaceResultItem.inPicDirs);
                            var customerItemPics = _customerItemPicRepository.GetAll()
                                    .Where(r => r.ItemBMID == customerRegItem.Id && r.TjlCustomerRegID == customerRegItem.CustomerRegId).ToList();
                            var itemImages = interfaceResultItem.inPicDirs.Split('|').ToList();
                            //添加操作日志  
                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                            //createOpLogDto.LogName = customerReg.Customer.Name;
                            //createOpLogDto.LogText = "存在图片";
                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                           // createOpLogDto.LogDetail = "存在图片:"+ interfaceResultItem.inPicDirs;

                            //var picpath = interfaceResultItem.inPicDirs;
                            //if (interfaceResultItem.inPicDirs.Length > 500)
                            //{
                            //    picpath = interfaceResultItem.inPicDirs.Substring(0, 250);
                            //}
                            //createOpLogDto.LogDetail = picpath;
                           //_commonAppService.SaveOpLog(createOpLogDto);

                            foreach (var itemDelete in customerItemPics)
                            {

                                _customerItemPicRepository.Delete(itemDelete);
                                //interfaceBack.JKStrBui.Append("先删除原有图片：" + itemDelete.Id);
                            }
                            //pdf报告转换
                            if (interfaceResultItem.inPicDirs.Contains(".pdf"))
                            {
                                interfaceBack.JKStrBui.Append("pdf图片路径：" + interfaceResultItem.inPicDirs);
                                var NitemImages = itemImages.ToList();

                                foreach (var nimage in NitemImages)
                                {
                                    #region pdf转jpg
                                    var imagenew = nimage;
                                    if (nimage.Contains(".pdf") && !nimage.Contains("ftp://"))
                                    {
                                        if (!File.Exists(nimage))
                                        {
                                            //添加操作日志  
                                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                                            //createOpLogDto.LogName = customerReg.Customer.Name;
                                            //createOpLogDto.LogText =  "无法访问，转换pdf报告失败";
                                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                            //createOpLogDto.LogDetail = nimage ;
                                            //_commonAppService.SaveOpLog(createOpLogDto);
                                        }
                                        else
                                        {

                                            try
                                            {
                                                var path = Path.GetDirectoryName(nimage);
                                                var filename = Path.GetFileNameWithoutExtension(nimage);
                                                var iamges = PDFConvertToJPG(nimage, path, 0);
                                                itemImages.Remove(nimage);
                                                itemImages.Add(iamges);
                                                //createOpLogDto.LogBM = customerReg.CustomerBM;
                                                //createOpLogDto.LogName = customerReg.Customer.Name;
                                                //createOpLogDto.LogText = "pdf转换成功" ;
                                                //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                                //createOpLogDto.LogDetail = string.Join(",", iamges);
                                                //_commonAppService.SaveOpLog(createOpLogDto);
                                            }
                                            catch (Exception ex)
                                            {

                                                //添加操作日志  
                                                //createOpLogDto.LogBM = customerReg.CustomerBM;
                                                //createOpLogDto.LogName = customerReg.Customer.Name;
                                                //createOpLogDto.LogText ="图片转换失败";
                                                //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                                //createOpLogDto.LogDetail = nimage + ex.Message;
                                                //_commonAppService.SaveOpLog(createOpLogDto);
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                            //ftp下载
                            if (interfaceResultItem.inPicDirs.Contains("ftp://"))
                            {
                                //interfaceBack.JKStrBui.Append("ftp图片路径：" + interfaceResultItem.inPicDirs);
                                var NitemImages = itemImages.ToList();

                                foreach (var nimage in NitemImages)
                                {
                                    #region pdf转jpg
                                    var imagenew = nimage;

                                    if (nimage.Contains("ftp://"))
                                    {
                                        //if (!File.Exists(imagenew))
                                        //{
                                        //    itemImages.Remove(imagenew);
                                        //}
                                        //else
                                        //{

                                        try
                                        {
                                            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                            var fileDirectory = Path.Combine(baseDirectory, "Upload");
                                            var pictureDirectory = Path.Combine(fileDirectory, "Picture");
                                            var belongDirectory = Path.Combine(pictureDirectory, "FTP");
                                            var date = DateTime.Now.ToString("yyyyMM");
                                            var dateDirectory = Path.Combine(belongDirectory, date);
                                            if (!Directory.Exists(dateDirectory))
                                                Directory.CreateDirectory(dateDirectory);
                                            int len = nimage.LastIndexOf('/');
                                            var finame = customerRegItem.ItemName.Replace("+","").Replace("_","").Replace("-","").Replace("&", "") 
                                                + System.DateTime.Now.ToString("yyMMddHHmmss")+ nimage.Substring(len + 1, nimage.Length - len - 1) ;
                                            var iamges = ftpDownload(nimage, Path.Combine(dateDirectory, finame));
                                            //添加操作日志  
                                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                                            //createOpLogDto.LogName = customerReg.Customer.Name;
                                            //createOpLogDto.LogText = "ftp下载成功";
                                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                            //createOpLogDto.LogDetail = string.Join(",", iamges.Trim());
                                            //_commonAppService.SaveOpLog(createOpLogDto);
                                            if (iamges.Contains(".pdf"))
                                            {
                                                var path = Path.GetDirectoryName(iamges);
                                                var filename = Path.GetFileNameWithoutExtension(iamges);
                                                var iamgest = PDFConvertToJPG(iamges, path , 0);
                                                itemImages.Remove(imagenew);
                                                itemImages.Add(iamgest);
                                            }
                                            //将docm图片转成jpg图片路径
                                            else if (iamges.Contains(".dcm") || iamges.Contains(".dic"))
                                            {
                                                var path = Path.GetDirectoryName(iamges);
                                                var filename = Path.GetFileNameWithoutExtension(iamges);

                                                var jpgiamge = path + "\\" + filename + ".jpg";

                                                var fullamge = Path.Combine(baseDirectory, jpgiamge);
                                                var docfile = Path.Combine(baseDirectory, iamges);
                                                //添加操作日志  
                                                //createOpLogDto.LogBM = customerReg.CustomerBM;
                                                //createOpLogDto.LogName = customerReg.Customer.Name;
                                                //createOpLogDto.LogText = "dcm转换";
                                                //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                                //createOpLogDto.LogDetail = docfile + "|" + fullamge;
                                                //_commonAppService.SaveOpLog(createOpLogDto);
                                                var isOk = DocmConvertToImagePath(docfile, fullamge, 1, customerReg.CustomerBM);
                                                if (isOk == true)
                                                {
                                                    itemImages.Remove(imagenew);
                                                    itemImages.Add(jpgiamge);
                                                    //添加操作日志  
                                                    //createOpLogDto.LogBM = customerReg.CustomerBM;
                                                    //createOpLogDto.LogName = customerReg.Customer.Name;
                                                    //createOpLogDto.LogText = "dcm转换成功";
                                                    //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                                    //createOpLogDto.LogDetail = string.Join(",", jpgiamge.Trim());
                                                    //_commonAppService.SaveOpLog(createOpLogDto);
                                                }
                                                else
                                                {  //添加操作日志  
                                                    createOpLogDto.LogBM = customerReg.CustomerBM;
                                                    createOpLogDto.LogName = customerReg.Customer.Name;
                                                    createOpLogDto.LogText = "dcm转换异常";
                                                    createOpLogDto.LogType = (int)LogsTypes.InterId;
                                                    createOpLogDto.LogDetail = string.Join(",", jpgiamge.Trim());
                                                    _commonAppService.SaveOpLog(createOpLogDto);
                                                }
                                            }
                                            else
                                            {
                                                itemImages.Remove(imagenew);
                                                itemImages.Add(iamges);
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                            //添加操作日志  
                                            createOpLogDto.LogBM = customerReg.CustomerBM;
                                            createOpLogDto.LogName = customerReg.Customer.Name;
                                            createOpLogDto.LogText = "ftp下载失败";
                                            createOpLogDto.LogType = (int)LogsTypes.InterId;
                                            createOpLogDto.LogDetail = nimage.Trim() + ex.Message;
                                            _commonAppService.SaveOpLog(createOpLogDto);
                                        }
                                        //}

                                    }
                                    #endregion
                                }
                            }
                            //http下载
                            #region 暂时不支持
                            if (interfaceResultItem.inPicDirs.Contains("http") && interfaceResultItem.inPicDirs.Contains(".pdf"))
                            {
                                var NitemImages = itemImages.ToList();

                                foreach (var nimage in NitemImages)
                                {
                                    var imagenew = nimage;
                                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                    var fileDirectory = Path.Combine(baseDirectory, "Upload");
                                    var pictureDirectory = Path.Combine(fileDirectory, "Picture");
                                    var belongDirectory = Path.Combine(pictureDirectory, "http");
                                    var date = DateTime.Now.ToString("yyyyMM");
                                    var dateDirectory = Path.Combine(belongDirectory, date);
                                    if (!Directory.Exists(dateDirectory))
                                        Directory.CreateDirectory(dateDirectory);
                                    int len = nimage.LastIndexOf('/');
                                    var finame = customerRegItem.ItemName.Replace("+", "").Replace("_", "").Replace("-", "").Replace("&", "")
                                        + System.DateTime.Now.ToString("yyMMddHHmmss") + nimage.Substring(len + 1, nimage.Length - len - 1);
                                    var iamges = HttpDownloadFile(nimage, Path.Combine(dateDirectory, finame));
                                    //添加操作日志  
                                    //createOpLogDto.LogBM = customerReg.CustomerBM;
                                    //createOpLogDto.LogName = customerReg.Customer.Name;
                                    //createOpLogDto.LogText = "http下载成功";
                                    //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                    //createOpLogDto.LogDetail = string.Join(",", iamges.Trim());
                                    //_commonAppService.SaveOpLog(createOpLogDto);
                                    if (iamges.Contains(".pdf"))
                                    {
                                        var path = Path.GetDirectoryName(iamges);
                                        var filename = Path.GetFileNameWithoutExtension(iamges);
                                        var iamgest = PDFConvertToJPG(iamges, path + "\\",  0);
                                        itemImages.Remove(imagenew);
                                        itemImages.Add(iamgest);
                                    }

                                    if (iamges.Contains(".dcm") || iamges.Contains(".dic"))
                                    {
                                        var path = Path.GetDirectoryName(iamges);
                                        var filename = Path.GetFileNameWithoutExtension(iamges);

                                        var jpgiamge = path + "\\" + filename + ".jpg";

                                        var fullamge = Path.Combine(baseDirectory, jpgiamge);
                                        var docfile = Path.Combine(baseDirectory, iamges);
                                        //添加操作日志  
                                        //createOpLogDto.LogBM = customerReg.CustomerBM;
                                        //createOpLogDto.LogName = customerReg.Customer.Name;
                                        //createOpLogDto.LogText = "dcm转换";
                                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                        //createOpLogDto.LogDetail = docfile + "|" + fullamge;
                                        //_commonAppService.SaveOpLog(createOpLogDto);
                                        var isOk = DocmConvertToImagePath(docfile, fullamge, 1, customerReg.CustomerBM);
                                        if (isOk == true)
                                        {
                                            itemImages.Remove(imagenew);
                                            itemImages.Add(jpgiamge);
                                            //添加操作日志  
                                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                                            //createOpLogDto.LogName = customerReg.Customer.Name;
                                            //createOpLogDto.LogText = "dcm转换成功";
                                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                            //createOpLogDto.LogDetail = string.Join(",", jpgiamge.Trim());
                                            //_commonAppService.SaveOpLog(createOpLogDto);
                                        }
                                        else
                                        {  //添加操作日志  
                                            createOpLogDto.LogBM = customerReg.CustomerBM;
                                            createOpLogDto.LogName = customerReg.Customer.Name;
                                            createOpLogDto.LogText = "dcm转换异常";
                                            createOpLogDto.LogType = (int)LogsTypes.InterId;
                                            createOpLogDto.LogDetail = string.Join(",", jpgiamge.Trim());
                                            _commonAppService.SaveOpLog(createOpLogDto);
                                        }
                                    }
                                }
                            }
                            #endregion
                            foreach (string img in itemImages)
                            {
                                if (Guid.TryParse(img, out var imgId))
                                {
                                    //interfaceBack.JKStrBui.Append("上传图片路径开始：" + imgId);
                                    TjlCustomerItemPic pic = new TjlCustomerItemPic();
                                    pic.Id = Guid.NewGuid();
                                    pic.TjlCustomerRegID = customerReg.Id;
                                    pic.CustomerItemGroupID = customerRegItem.CustomerItemGroupBMid;
                                    pic.ItemBMID = customerRegItem.Id;
                                    pic.PictureBM = imgId;
                                    pic.TenantId = tandId;
                                    _customerItemPicRepository.Insert(pic);
                                    // interfaceBack.JKStrBui.Append("上传图片路径结束：" + imgId);
                                }
                                else
                                {
                                    //interfaceBack.JKStrBui.Append("上传图片路径开始：" + imgId);
                                    foreach (var itemDelete in customerItemPics)
                                    {
                                        if (itemDelete.PictureBM.HasValue)
                                        {
                                            _pictureAppService.Delete(new EntityDto<Guid>(itemDelete.PictureBM.Value));
                                            //interfaceBack.JKStrBui.Append("删除图片：" + itemDelete.PictureBM.Value);
                                        }
                                    }
                                    var belong = "PACS";
                                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                    var fileDirectory = Path.Combine(baseDirectory, "Upload");
                                    var pictureDirectory = Path.Combine(fileDirectory, "Picture");
                                    var belongDirectory = Path.Combine(pictureDirectory, belong);
                                    var date = DateTime.Now.ToString("yyyyMM");
                                    var dateDirectory = Path.Combine(belongDirectory, date);
                                    if (!Directory.Exists(dateDirectory))
                                        Directory.CreateDirectory(dateDirectory);
                                    var createPictureDto = new CreateOrUpdatePictureTenIdDto
                                    {
                                        Id = Guid.NewGuid(),
                                        Belong = belong
                                    };
                                    var extension = Path.GetExtension(img);
                                    var fileName = Path.Combine(dateDirectory, $"{createPictureDto.Id}{extension}");

                                    var itemImgName = $"http:{img.Replace('\\', '/')}";
                                    if (img.Contains("http:"))
                                    {
                                        itemImgName = img;
                                    }
                                    if (img.Contains(baseDirectory))
                                    {
                                        itemImgName = img;
                                    }


                                    GC.Collect();
                                    createPictureDto.RelativePath = itemImgName.Replace(baseDirectory, "");

                                    createPictureDto.Thumbnail = createPictureDto.RelativePath;

                                    try
                                    {
                                        createPictureDto.TenantId = tandId;
                                        var result = _pictureAppService.CreateTenaId(createPictureDto);
                                        TjlCustomerItemPic pic = new TjlCustomerItemPic
                                        {
                                            Id = Guid.NewGuid(),
                                            TjlCustomerRegID = customerReg.Id,
                                            CustomerItemGroupID = customerRegItem.CustomerItemGroupBMid,
                                            ItemBMID = customerRegItem.Id,
                                            PictureBM = result.Id,
                                            TenantId = tandId
                                        };
                                        _customerItemPicRepository.Insert(pic);
                                        //添加操作日志  
                                        //createOpLogDto.LogBM = customerReg.CustomerBM;
                                        //createOpLogDto.LogName = customerReg.Customer.Name;
                                        //createOpLogDto.LogText = "保存图片";
                                        //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                        //var picpath1 = createPictureDto.RelativePath;
                                        //if (createPictureDto.RelativePath.Length > 500)
                                        //{
                                        //    picpath1 = createPictureDto.RelativePath.Substring(0, 250);
                                        //}
                                        //createOpLogDto.LogDetail = picpath1;
                                        //_commonAppService.SaveOpLog(createOpLogDto);
                                        //interfaceBack.JKStrBui.Append("上传图片：" + createPictureDto.RelativePath);
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    CurrentUnitOfWork.SaveChanges();
                    var customerRegItemGroupsQuery = _customerItemGroupRepository.GetAll()
                        .Where(r => r.CustomerRegBMId == customerReg.Id);
                    if (tdbInterfaceWhere.departmentID.HasValue)
                    {
                        customerRegItemGroupsQuery =
                            customerRegItemGroupsQuery.Where(r => r.DepartmentId == tdbInterfaceWhere.departmentID);
                    }

                    var customerRegItemGroups = customerRegItemGroupsQuery.ToList();
                    foreach (var customerRegItemGroup in customerRegItemGroups)
                    {
                        if (customerRegItemGroup.CustomerRegItem.Count != 0 && customerRegItemGroup.CustomerRegItem.All(r => r.ProcessState == (int)ProjectIState.Complete))
                        {
                            customerRegItemGroup.CheckState = (int)ProjectIState.Complete;
                        }
                        else if (customerRegItemGroup.CustomerRegItem.Any(r => r.ProcessState == (int)ProjectIState.Complete))
                        {
                            customerRegItemGroup.CheckState = (int)ProjectIState.Part;
                        }

                        var customerRegItem = customerRegItemGroup.CustomerRegItem?.FirstOrDefault(r =>
                            r.CheckEmployeeBMId != null && r.InspectEmployeeBMId != null);
                        if (customerRegItem != null)
                        {
                            customerRegItemGroup.InspectEmployeeBMId = customerRegItem.InspectEmployeeBMId;
                            customerRegItemGroup.InspectEmployeeBM = customerRegItem.InspectEmployeeBM;
                            customerRegItemGroup.CheckEmployeeBMId = customerRegItem.CheckEmployeeBMId;
                            customerRegItemGroup.CheckEmployeeBM = customerRegItem.CheckEmployeeBM;
                        }
                        //if (customerReg.LoginDate < customerRegItemGroup.FirstDateTime)
                        //{
                        //    if (!customerRegItemGroup.FirstDateTime.HasValue)
                        //    {

                        //        customerRegItemGroup.FirstDateTime = DateTime.Now;
                        //    }
                        //}
                        //else
                        //{
                        //    customerRegItemGroup.FirstDateTime = DateTime.Now;
                        //}
                        if (!customerRegItemGroup.FirstDateTime.HasValue)
                        {

                            customerRegItemGroup.FirstDateTime = DateTime.Now;
                        }

                        _customerItemGroupRepository.Update(customerRegItemGroup);
                    }
                    CurrentUnitOfWork.SaveChanges();
                    var customerReg1 = _customerRegRepository.FirstOrDefault(r => r.CustomerBM == tdbInterfaceWhere.inactivenum);
                    //互斥项目处理
                    foreach (Guid imid in HCItemIDs)
                    {
                        var chitem = _customerRegItemRepository.FirstOrDefault(o => o.CustomerRegId == customerReg1.Id && o.ItemId == imid);
                        if (chitem != null)
                        {
                            chitem.ProcessState = (int)ProjectIState.Complete;
                            _customerRegItemRepository.Update(chitem);
                        }
                    }
                    CurrentUnitOfWork.SaveChanges();

                    if (HCGroupIDs.Count > 0)
                    {
                        foreach (Guid id in HCGroupIDs)
                        {
                            var lis = _customerRegItemRepository.GetAll().Where(o => o.CustomerRegId == customerReg1.Id && o.ItemGroupBMId == id && o.ProcessState == (int)ProjectIState.Not).ToList();
                            if (lis.Count() == 0)
                            {
                                var GroupDt =
                           _customerItemGroupRepository.FirstOrDefault(o => o.CustomerRegBMId == customerReg1.Id && o.ItemGroupBM_Id == id);
                                if (GroupDt != null)
                                {
                                    GroupDt.CheckState = (int)ProjectIState.Complete;
                                    _customerItemGroupRepository.Update(GroupDt);
                                }
                            }
                        }
                    }
                    var group = customerReg1.CustomerItemGroup.Where(o => o.DepartmentBM.Category != "耗材").ToList();

                    //if (group.Count != 0 && group.
                    //    All(r => r.CheckState == (int)ProjectIState.Complete && r.CheckState != (int)ProjectIState.GiveUp
                    //    && r.PayerCat != (int)PayerCatType.NoCharge))
                    //{
                    //    customerReg1.CheckSate = (int)PhysicalEState.Complete;
                    //}
                    //else if (group.Any(r => r.CheckState == (int)ProjectIState.Complete || r.CheckState == (int)ProjectIState.Part))
                    //{
                    //    customerReg1.CheckSate = (int)PhysicalEState.Process;
                    //}
                    if (group.Count != 0 && !group.Any(o => o.CheckState != (int)ProjectIState.Complete
                && o.CheckState != (int)ProjectIState.Part &&
                o.CheckState != (int)ProjectIState.GiveUp &&
                o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus
                && o.DepartmentBM.Category != "耗材"))
                    {
                        //参数控制部分检查不是体检完成

                        var isBF = _basicDictionaryRepository.FirstOrDefault(o => o.Type == BasicDictionaryType.DepatSumSet.ToString()   && o.Value == 3 && o.TenantId == tandId)?.Remarks;
                        if (!string.IsNullOrEmpty(isBF) && isBF == "Y" &&
                            group.Any(o =>  o.CheckState == (int)ProjectIState.Part && 
                            o.IsAddMinus != (int)AddMinusType.Minus && o.DepartmentBM.Category != "耗材"))
                        {

                            customerReg1.CheckSate = (int)PhysicalEState.Process;
                        }
                        else
                        {
                            customerReg1.CheckSate = (int)PhysicalEState.Complete;
                        }

                    }
                    else
                    {

                        customerReg1.CheckSate = (int)PhysicalEState.Process;
                    }

                    _customerRegRepository.Update(customerReg1);
                    //生成小结
                    if (DepartIDs.Count > 0)
                    {
                        foreach (var departid in DepartIDs)
                        {
                            //参数控制未检查完也生成小结
                            var isOk = false;
                            var sumSet = _basicDictionaryRepository.GetAll().FirstOrDefault(p => p.Type == "DepatSumSet" && p.Value == 1);
                            if (sumSet != null && sumSet.Text == "Y")
                            {
                                isOk = true;
                            }
                            var cusgrops = customerReg1.CustomerItemGroup.Where(o => o.DepartmentId == departid && o.CheckState == (int)ProjectIState.Not && o.IsAddMinus != (int)AddMinusType.Minus).ToList();

                            //纸质报告判断
                            if (pgroup != null && pgroup.Count > 0)
                            {
                                var PgNames = pgroup.Select(o => o.Text).ToList();
                                cusgrops = cusgrops.Where(o => !PgNames.Contains(o.ItemGroupBM?.ItemGroupName)).ToList();
                            }
                            if (cusgrops.Count == 0 || isOk)
                            {

                                CreateConclusionDto input = new CreateConclusionDto();
                                input.CustomerBM = customerReg1.CustomerBM;
                                List<Guid> departl = new List<Guid>();
                                departl.Add(departid);
                                input.Department = departl;
                                CreateConclusion(input);
                            }
                        }
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Debug("获取接口错误:" + ex.Message + ";" + ex.Source);
            //    throw;

            //}
            return interfaceBack;

        }
        /// <summary>
        /// Http下载文件
        /// </summary>
        public static string HttpDownloadFile(string url, string path)

        {

            // 设置参数

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            //直到request.GetResponse()程序才开始向目标网页发送Post请求

            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流

            Stream stream = new FileStream(path, FileMode.Create);
            byte[] bArr = new byte[1024];

            int size = responseStream.Read(bArr, 0, (int)bArr.Length);

            while (size > 0)

            {

                stream.Write(bArr, 0, size);

                size = responseStream.Read(bArr, 0, (int)bArr.Length);

            }

            stream.Close();

            responseStream.Close();

            return path;

        }


        /// 下载服务器文件至客户端 
        /// </summary> 
        /// <param name="URL">被下载的文件地址，绝对路径</param> 
        /// <param name="Dir">另存放的目录</param> 

        public string ftpDownload(string URL, string Dir)

        {

            WebClient client = new WebClient();
            //client.Credentials = new NetworkCredential();
            string Path = Dir;   //另存为的绝对路径＋文件名 
            try
            {
                client.DownloadFile(new Uri(URL), Path);
            }

            catch (Exception ex)
            {
                //添加操作日志 
                throw;
            }

            finally
            {
                client.Dispose();
            }
            return Path;
        }


        /// <summary>
        /// 为图片生成缩略图
        /// </summary>
        /// <param name="image">原图片的路径</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        /// <returns></returns>
        private Image GetThumbnail(Image image, int width, int height)
        {
            using (image)
            {
                var bmp = new Bitmap(width, height);
                //从Bitmap创建一个System.Drawing.Graphics
                var gr = Graphics.FromImage(bmp);
                //设置 
                gr.SmoothingMode = SmoothingMode.HighQuality;
                //下面这个也设成高质量
                gr.CompositingQuality = CompositingQuality.HighQuality;
                //下面这个设成High
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //把原始图像绘制成上面所设置宽高的缩小图
                var rectDestination = new Rectangle(0, 0, width, height);

                gr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                return bmp;
            }
        }

        /// <summary>
        /// 生成科室小结
        /// </summary>
        /// <param name="Create">体检人信息，体检人项目，科室信息</param>
        public bool CreateConclusion(CreateConclusionDto Create)
        {
            bool isOK = true;
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            //患者信息
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant))
                {
                    var customerReg = _customerRegRepository.FirstOrDefault(o => o.CustomerBM == Create.CustomerBM);
                    int TenantId = customerReg.TenantId;
                    var sumHide = _TbmSumHide.GetAll().ToList();
                    if (customerReg == null)
                    {
                        return true;
                    }
                    //已经总检不再获取数据
                    if (customerReg.SummSate == (int)SummSate.Audited || customerReg.SummSate == (int)SummSate.HasInitialReview)
                    {
                        //添加操作日志  
                        createOpLogDto.LogBM = customerReg.CustomerBM;
                        createOpLogDto.LogName = customerReg.Customer.Name;
                        createOpLogDto.LogText = "生成小结失败，已总检";
                        createOpLogDto.LogType = (int)LogsTypes.InterId;
                        createOpLogDto.LogDetail = string.Join(",", Create.Department);
                        _commonAppService.SaveOpLog(createOpLogDto);
                        return true;
                    }


                    //图像科室,是否正常都进入小结
                     var strjcDepartment = _basicDictionaryRepository.FirstOrDefault(o => o.Type == "DepatSumSet" && o.Value == 2 && o.TenantId==TenantId)?.Remarks;
                    //var strjcDepartment = "";
                    if (string.IsNullOrEmpty(strjcDepartment))
                    {
                        strjcDepartment = "";

                    }
                    foreach (var departmentItem in Create.Department)
                    {
                        //CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                        string logtxt = "";
                        //设置编码记录诊断是否有结果只是全部被隐藏了
                        bool hsitemZD = false;
                        //科室信息
                        var currentDepartment = _departmentRepository.Get(departmentItem);
                        var sumformat = currentDepartment.SumFormat ?? "";
                        string[] groupsumls = new string[0];
                        if (sumformat != "")
                        {
                            groupsumls = new string[2];
                            if (sumformat.Contains("|"))
                            {
                                groupsumls = sumformat?.Split('|');
                            }
                            else
                            {
                                groupsumls[0] = "";
                                groupsumls[1] = sumformat ?? "";

                            }
                        }
                        //判断该体检人是否有该科室,没有登记该科室项目则不是生成失败
                        var cusdepart = customerReg.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus
                         && o.DepartmentId == currentDepartment.Id).ToList();
                        if (cusdepart.Count == 0)
                        { continue; }
                        //&& o.CheckState != (int)ProjectIState.Not ，o.CheckState != (int)ProjectIState.Stay && 
                        //var customerItemGroups = customerReg.CustomerItemGroup.Where(o =>
                        //    o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus !=
                        //    (int)AddMinusType.Minus && o.CheckState != (int)ProjectIState.GiveUp
                        //    && o.DepartmentId == currentDepartment.Id).OrderBy(r => r.ItemGroupOrder).ToList();
                        //去掉收费控制
                        var customerItemGroups = customerReg.CustomerItemGroup.Where(o => o.IsAddMinus !=
                            (int)AddMinusType.Minus && o.CheckState != (int)ProjectIState.GiveUp
                            && o.DepartmentId == currentDepartment.Id).OrderBy(r => r.ItemGroupOrder).ToList();

                        //纸张报告不判断
                        var pgroup = _basicDictionaryRepository.GetAll().Where(o => o.Type == "PayerGroup").ToList();
                        if (pgroup != null && pgroup.Count > 0)
                        {
                            var PgNames = pgroup.Select(o => o.Text).ToList();
                            customerItemGroups = customerItemGroups.Where(o => !PgNames.Contains(o.ItemGroupBM?.ItemGroupName)).ToList();
                        }

                        var sumSet = _basicDictionaryRepository.GetAll().FirstOrDefault(p => p.Type == "DepatSumSet" && p.Value == 1);
                        if (sumSet != null && sumSet.Text == "Y")
                        {
                            if (customerItemGroups.Count == 0)
                            {
                                // isOK = false;
                                //添加操作日志  
                                //createOpLogDto.LogBM = customerReg.CustomerBM;
                                //createOpLogDto.LogName = customerReg.Customer.Name;
                                //createOpLogDto.LogText = "生成小结失败";
                                //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                //createOpLogDto.LogDetail = currentDepartment.Name +"，存在未检项目或待查项目不能小结";
                                //_commonAppService.SaveOpLog(createOpLogDto);
                                continue;
                            }
                        }
                        else
                        {
                            if (customerItemGroups.Count == 0 ||
                                customerItemGroups.FirstOrDefault(o => o.CheckState == (int)ProjectIState.Not) != null ||
                                customerItemGroups.FirstOrDefault(o => o.CheckState == (int)ProjectIState.Stay) != null)
                            {
                                // isOK = false;
                                //添加操作日志  
                                //createOpLogDto.LogBM = customerReg.CustomerBM;
                                //createOpLogDto.LogName = customerReg.Customer.Name;
                                //createOpLogDto.LogText = "生成小结失败";
                                //createOpLogDto.LogType = (int)LogsTypes.InterId;
                                //createOpLogDto.LogDetail = currentDepartment.Name +"，存在未检项目或待查项目不能小结";
                                //_commonAppService.SaveOpLog(createOpLogDto);
                                continue;
                            }
                        }

                        var itemInfos1 = customerReg.CustomerRegItems.ToList();
                        var itemInfos = itemInfos1.Select(r => r.ItemBM).ToList();
                        if (itemInfos.Count == 0)
                        {
                            // isOK = false;

                            // isOK = false;
                            //添加操作日志  
                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                            //createOpLogDto.LogName = customerReg.Customer.Name;
                            //createOpLogDto.LogText = "生成小结失败";
                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                            //createOpLogDto.LogDetail = currentDepartment.Name +   "，不存在检查项目";
                            //_commonAppService.SaveOpLog(createOpLogDto);
                            continue;
                        }

                        //科室小结
                        var depSummary = _customerDepSummaryRepository.FirstOrDefault(o =>
                            o.CustomerRegId == customerReg.Id && o.DepartmentBMId == currentDepartment.Id);
                        if (depSummary == null)
                        {
                            depSummary = new TjlCustomerDepSummary();
                        }
                        else
                        {
                            depSummary.CharacterSummary = string.Empty;
                            depSummary.DagnosisSummary = string.Empty;
                            depSummary.PrivacyCharacterSummary = string.Empty;
                            depSummary.OriginalDiag = string.Empty;
                        }

                        depSummary.CustomerRegId = customerReg.Id;
                        depSummary.DepartmentBMId = currentDepartment.Id;
                        depSummary.DepartmentOrder = currentDepartment.OrderNum;
                        depSummary.DepartmentName = currentDepartment.Name;



                        var sexGenderNotSpecified = (int)Sex.GenderNotSpecified;
                        int zhxh = 1;
                        int zhxhzd = 1;
                        int xmxh = 1;
                        int xmxhzd = 1;
                        bool hsxh = false;
                        int zxh = 1;
                        //通一个项目只出一个小结
                        List<Guid?> itemlist = new List<Guid?>();
                        //相同报告单号只生成一次小结
                        List<string> reportBMList = new List<string>();
                        foreach (var itemGroup in customerItemGroups)
                        {
                            if (!string.IsNullOrEmpty(itemGroup.ReportBM) &&
                                reportBMList.Contains(itemGroup.ReportBM))
                            {
                                continue;
                            }
                           
                            var GroupSum = "";
                            //组合项目小结字符串
                            itemGroup.ItemGroupSum = string.Empty;
                            //清空原有组合结论
                            itemGroup.ItemGroupDiagnosis = string.Empty;
                            itemGroup.ItemGroupOriginalDiag = string.Empty;
                            //判断有没有异常项目
                            var isAbnormal = true;
                            //判断是不是第一个正常项目
                            var isNormal = true;
                            //循环当前分组项目
                            var customerRegItemsByGroup = itemGroup.CustomerRegItem.Where(p => p.ItemBM != null).OrderBy(o => o.ItemOrder).ToList();

                            int xh = 1;
                            int zdxh = 1;
                            //同一个项目只能复合判断一次
                            List<Guid?> fhitemls = new List<Guid?>();
                            bool HasGroup = false;
                            foreach (var item in customerRegItemsByGroup)
                            {

                                var checkItem = itemInfos.FirstOrDefault(o => o.Id == item.ItemId);
                                if (checkItem != null && checkItem.IsSummary == (int)Summary.NotToEnter)
                                {
                                    continue;
                                }
                                HasGroup = true;
                                //过滤异常结果
                                var ZD = item.ItemSum;
                                if (!string.IsNullOrWhiteSpace(item.ItemDiagnosis))
                                {
                                    ZD = item.ItemDiagnosis;
                                }
                                if (sumHide.Count > 0 && !string.IsNullOrWhiteSpace(ZD))
                                {
                                    // if(item)
                                    var sumh = sumHide.Where(o => ZD.Contains(o.SumWord) && (o.ClientType == "" || o.ClientType == customerReg.PhysicalType.ToString())).Select(o => o.SumWord).ToList();
                                    var sumY = sumHide.Where(o => ZD.Contains(o.SumWord) && o.IsNormal == 1 && (o.ClientType == "" || o.ClientType == customerReg.PhysicalType.ToString())).Select(o => o.SumWord).ToList();
                                    //数值型诊断没有项目名称，所以得加上项目名称进行判断
                                    if (item.ItemTypeBM == (int)ItemType.Calculation || item.ItemTypeBM == (int)ItemType.Number && !ZD.Contains(item.ItemName))
                                    {
                                        var zdls = item.ItemName + ZD;

                                        sumh = sumHide.Where(o => zdls.Contains(o.SumWord) && (o.ClientType == "" || o.ClientType == customerReg.PhysicalType.ToString())).Select(o => o.SumWord).ToList();
                                        sumY = sumHide.Where(o => zdls.Contains(o.SumWord) && o.IsNormal == 1 && (o.ClientType == "" || o.ClientType == customerReg.PhysicalType.ToString())).Select(o => o.SumWord).ToList();
                                    }
                                    //正常诊断修改状态
                                    if (sumY.Count > 0 && checkItem.moneyType == (int)ItemType.Explain)
                                    {
                                        foreach (var sums in sumY)
                                        {
                                            if (!string.IsNullOrWhiteSpace(sums))
                                            {
                                                var sum1 = sums + "；";
                                                var sum2 = sums + ";";
                                                var sum3 = sums + "。";
                                                var sum4 = sums + "，";
                                                var sum5 = sums + ",";
                                                ZD = ZD.Replace(sum1, "").Replace(sum2, "").Replace(sum3, "").Replace(sum4, "").Replace(sum5, "").Replace(sums, "");

                                            }
                                        }
                                        if (ZD.Trim().Replace("\r\n", "") == "")
                                        {
                                            ZD = "";
                                            item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);

                                        }
                                    }
                                    //过滤隐藏诊断
                                    if (sumh.Count > 0)
                                    {
                                        if (checkItem.moneyType == (int)ItemType.Number || checkItem.moneyType == (int)ItemType.Calculation)
                                        {
                                            ZD = "";
                                            //item.ItemDiagnosis = "";
                                        }
                                        else
                                        {
                                            foreach (var sums in sumh)
                                            {
                                                if (!string.IsNullOrWhiteSpace(sums))
                                                {
                                                    var sum1 = sums + "；";
                                                    var sum2 = sums + ";";
                                                    var sum3 = sums + "。";
                                                    var sum4 = sums + "，";
                                                    var sum5 = sums + ",";
                                                    ZD = ZD.Replace(sum1, "").Replace(sum2, "").Replace(sum3, "").Replace(sum4, "").Replace(sum5, "").Replace(sums, "");
                                                }
                                            }
                                        }
                                    }

                                }
                                //如果是图像科室则不管异常与否都进入结论
                                if (strjcDepartment != null && !strjcDepartment.Contains(itemGroup.DepartmentName))
                                {
                                    //复合判断                          
                                    if (item.DiagnSate == (int)DiagnSate.Judge)
                                    {
                                        //判断此项目是否有对应上的复合判断,默认没有
                                        var isFuHe = false;
                                        //已经复合复合判断的项目不在处理 
                                        if (fhitemls.Contains(item.ItemId))
                                        {
                                            isFuHe = true;
                                        }
                                        else
                                        {
                                            var diagnoses = _diagnosisData.GetAll().Where(o => o.ItemInfoId == item.ItemId && o.TenantId == TenantId).Select(o => o.Diagnosis).Distinct().ToList();
                                            foreach (var diagnosis in diagnoses)
                                            {

                                                if (diagnosis == null)
                                                {
                                                    continue;
                                                }
                                                //判断当前复合判断是否全部对应上,默认有对应上的
                                                var isZuHeFuHe = true;
                                                var itemDiagnoses = diagnosis.DiagnosisDatals.ToList();
                                                foreach (var diagnosisData in itemDiagnoses)
                                                {
                                                    int sex1 = (int)Sex.GenderNotSpecified;
                                                    int sex2 = (int)Sex.Unknown;

                                                    string cusage = customerReg.Customer.Sex.ToString();
                                                    //性别必须满足设置
                                                    if (diagnosisData.Sex != null && diagnosisData.Sex != ""
                                                            && diagnosisData.Sex != sex1.ToString() && diagnosisData.Sex != sex2.ToString() &&
                                                            diagnosisData.Sex != cusage)
                                                    {
                                                        isZuHeFuHe = false;
                                                        break;
                                                    }
                                                    //判断当前项目是否对应上
                                                    bool diag;
                                                    //数字和文字分开判断
                                                    var ItemfC = itemInfos1.FirstOrDefault(o => o.ItemId == diagnosisData.ItemInfo.Id);
                                                    if (ItemfC == null)
                                                    {
                                                        isZuHeFuHe = false;
                                                        break;
                                                    }
                                                    if (ItemfC.ItemTypeBM != (int)ItemType.Number && ItemfC.ItemTypeBM != (int)ItemType.Calculation)
                                                    {

                                                        diag = itemInfos1.Any(o => o.ItemId == diagnosisData.ItemInfo.Id && o.ItemResultChar == diagnosisData.ItemStandard);
                                                        //额外增加判断，阴阳型增加数值判断（乙肝定量为阴阳型，但是结果是数值）
                                                        if (!diag)
                                                        {
                                                            if (ItemfC.ItemTypeBM == (int)ItemType.YinYang)
                                                            {
                                                                var Itemchar = itemInfos1.FirstOrDefault(o => o.ItemId == diagnosisData.ItemInfo.Id && o.ItemResultChar != null && o.ItemResultChar != "");
                                                                if (Itemchar != null)
                                                                {
                                                                    var Itemcharvalue = Itemchar.ItemResultChar.Replace(">", "").Replace("<", "").Replace(">=", "").Replace("<=", "");
                                                                    if (decimal.TryParse(Itemcharvalue, out decimal itemvalue))
                                                                    {
                                                                        if (itemvalue >= diagnosisData.minValue && itemvalue <= diagnosisData.maxValue)
                                                                        {
                                                                            diag = true;
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            diag = itemInfos1.Any(o => o.ItemId == diagnosisData.ItemInfo.Id && o.ItemResultChar != null && o.ItemResultChar != "" && decimal.Parse(o.ItemResultChar) >= diagnosisData.minValue && decimal.Parse(o.ItemResultChar) <= diagnosisData.maxValue);
                                                        }
                                                        catch (Exception ex)
                                                        {

                                                            throw;
                                                        }
                                                    }
                                                    //如果碰到没对应上的直接跳出不再继续循环
                                                    if (!diag)
                                                    {
                                                        isZuHeFuHe = false;
                                                        break;
                                                    }
                                                }
                                                //如果所有细目都对应上则将判断赋值到组合结论中
                                                if (isZuHeFuHe)
                                                {
                                                    var jieLun = diagnosis.Conclusion;
                                                    if (!itemGroup.ItemGroupDiagnosis.Contains(jieLun))
                                                    {
                                                        //itemGroup.ItemGroupDiagnosis += jieLun + Environment.NewLine;
                                                        //增加
                                                        string groupsum = "(" + zdxh + ")" + jieLun + Environment.NewLine;
                                                        if (groupsumls.Length >= 2)
                                                        {
                                                            string xmxhbm = zdxh.ToString();
                                                            if (groupsumls[0] == "")
                                                            {
                                                                xmxhbm = xmxhzd.ToString();
                                                                xmxhzd = xmxhzd + 1;
                                                            }
                                                            groupsum = groupsumls[1];
                                                            if (groupsum.Contains("【序号】"))
                                                            {
                                                                hsxh = true;
                                                            }
                                                            groupsum = groupsum.Replace("【序号】", xmxhbm).Replace("【项目小结】",
                                                               jieLun).Replace("【空格】", " ").
                                                                Replace("【换行】", Environment.NewLine).ToString();
                                                        }
                                                        itemGroup.ItemGroupDiagnosis += groupsum;
                                                        itemGroup.ItemGroupOriginalDiag += "&" + jieLun + ";";
                                                        zdxh = zdxh + 1;
                                                        //记录满足复合条件的项目ID，下次不在判断
                                                        var hsfhitems = diagnosis.DiagnosisDatals.Select(o => o.ItemInfoId).ToList();
                                                        fhitemls.AddRange(hsfhitems);
                                                        isFuHe = true;
                                                        //一个项目满足一个复合判断，不在判断
                                                        #region 复合判断修改项目诊断
                                                     _customerRegItemRepository.GetAll().Where(p => p.CustomerRegId == customerReg.Id && fhitemls.Contains(p.ItemId)).
                                                           Update(o => new TjlCustomerRegItem
                                                           {
                                                               ItemDiagnosis= jieLun
                                                           }); ;

                                            #endregion
                                                        break;

                                                    }

                                                    isFuHe = true;
                                                }
                                            }
                                        }
                                        if (isFuHe)
                                        {
                                           
                                            string itemname = "";
                                            if (checkItem.IsSummaryName == 1)
                                            {

                                                if (checkItem.moneyType == (int)ItemType.Explain)
                                                {
                                                    itemname = item.ItemName + "：";
                                                }
                                                else
                                                {
                                                    itemname = item.ItemName;
                                                }
                                            }
                                            string groupsum = "(" + xh + ")" + itemname + item.ItemSum + Environment.NewLine;
                                            if (xh > zxh)
                                            {
                                                zxh = xh;

                                            }
                                            if (groupsumls.Length >= 2)
                                            {
                                                string xmxhbm = xh.ToString();
                                                if (groupsumls[0] == "")
                                                {
                                                    xmxhbm = xmxh.ToString();
                                                    xmxh = xmxh + 1;
                                                }
                                                groupsum = groupsumls[1];
                                                if (groupsum.Contains("【序号】"))
                                                {
                                                    hsxh = true;
                                                }
                                                groupsum = groupsum.Replace("【序号】", xmxhbm).Replace("【项目小结】",
                                                   itemname + item.ItemSum).Replace("【空格】", " ").
                                                    Replace("【换行】", Environment.NewLine).ToString();
                                            }
                                            string str = itemGroup.ItemGroupSum.Replace(" ", "").Replace("\r\n", "");
                                            if (str == "未见明显异常")
                                            {
                                                itemGroup.ItemGroupSum = groupsum;
                                            }
                                            else
                                            {
                                                itemGroup.ItemGroupSum += groupsum;
                                            }
                                            xh = xh + 1;
                                            isNormal = false;
                                            continue;
                                        }
                                    }
                                    //判断是否正常
                                    if (item.Symbol == SymbolHelper.SymbolFormatter((int)Symbol.Normal))
                                    {
                                        //第一个项目进，如果是正常。则之后不再进入
                                        if (isNormal && isAbnormal)
                                        {
                                            itemGroup.ItemGroupSum = "未见明显异常" + Environment.NewLine;
                                            isNormal = false;
                                        }
                                    }
                                    else
                                    {
                                        if (isAbnormal)
                                        {
                                            if (item.IllnessSate != (int)IllnessSate.Normal)
                                            {
                                                if (!string.IsNullOrWhiteSpace(item.ItemSum))
                                                {
                                                    string itemname = "";
                                                    if (checkItem.IsSummaryName == 1)
                                                    {

                                                        if (checkItem.moneyType == (int)ItemType.Explain)
                                                        {
                                                            itemname = item.ItemName + "：";
                                                        }
                                                        else
                                                        {
                                                            itemname = item.ItemName;
                                                        }
                                                    }

                                                    string groupsum = "(" + xh + ")" + itemname + item.ItemSum + Environment.NewLine;
                                                    if (xh > zxh)
                                                    {
                                                        zxh = xh;

                                                    }
                                                    if (groupsumls.Length >= 2)
                                                    {
                                                        string xmxhbm = xh.ToString();
                                                        if (groupsumls[0] == "")
                                                        {
                                                            xmxhbm = xmxh.ToString();
                                                            xmxh = xmxh + 1;
                                                        }
                                                        groupsum = groupsumls[1];
                                                        if (groupsum.Contains("【序号】"))
                                                        {
                                                            hsxh = true;
                                                        }
                                                        groupsum = groupsum.Replace("【序号】", xmxhbm).Replace("【项目小结】",
                                                           itemname + item.ItemSum).Replace("【空格】", " ").
                                                            Replace("【换行】", Environment.NewLine).ToString();
                                                    }
                                                    itemGroup.ItemGroupSum = groupsum;
                                                    GroupSum = "&" + itemname + item.ItemSum + ";";
                                                    xh = xh + 1;
                                                    isAbnormal = false;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            if (item.IllnessSate != (int)IllnessSate.Normal && item.ItemSum != null)
                                            {
                                                //重复项目不在处理
                                                if (!itemlist.Contains(item.ItemId))
                                                {



                                                    string itemname = "";
                                                    if (checkItem.IsSummaryName == 1)
                                                    {

                                                        if (checkItem.moneyType == (int)ItemType.Explain)
                                                        {
                                                            itemname = item.ItemName + "：";
                                                        }
                                                        else
                                                        {
                                                            itemname = item.ItemName;
                                                        }
                                                    }
                                                    string groupsum = "(" + xh + ")" + itemname + item.ItemSum + Environment.NewLine;
                                                    if (xh > zxh)
                                                    {
                                                        zxh = xh;

                                                    }
                                                    if (groupsumls.Length >= 2)
                                                    {
                                                        string xmxhbm = xh.ToString();
                                                        if (groupsumls[0] == "")
                                                        {
                                                            xmxhbm = xmxh.ToString();
                                                            xmxh = xmxh + 1;
                                                        }
                                                        groupsum = groupsumls[1];
                                                        if (groupsum.Contains("【序号】"))
                                                        {
                                                            hsxh = true;
                                                        }
                                                        groupsum = groupsum.Replace("【序号】", xmxhbm).Replace("【项目小结】",
                                                           itemname + item.ItemSum).Replace("【空格】", " ").
                                                            Replace("【换行】", Environment.NewLine).ToString();
                                                    }
                                                    itemGroup.ItemGroupSum += groupsum;
                                                    GroupSum += "&" + itemname + item.ItemSum + ";";
                                                    xh = xh + 1;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (item.IllnessSate != (int)IllnessSate.Normal)
                                    {
                                        //重复项目不在处理
                                        if (!itemlist.Contains(item.ItemId))
                                        {

                                            string itemname = "";
                                            if (checkItem.IsSummaryName == 1)
                                            {

                                                if (checkItem.moneyType == (int)ItemType.Explain)
                                                {
                                                    itemname = item.ItemName + "：";
                                                }
                                                else
                                                {
                                                    itemname = item.ItemName;
                                                }
                                            }
                                            string groupsum = "(" + xh + ")" + itemname + item.ItemSum + Environment.NewLine;
                                            if (xh > zxh)
                                            {
                                                zxh = xh;

                                            }
                                            if (groupsumls.Length >= 2)
                                            {
                                                string xmxhbm = xh.ToString();
                                                if (groupsumls[0] == "")
                                                {
                                                    xmxhbm = xmxh.ToString();
                                                    xmxh = xmxh + 1;
                                                }
                                                groupsum = groupsumls[1];
                                                if (groupsum.Contains("【序号】"))
                                                {
                                                    hsxh = true;
                                                }
                                                groupsum = groupsum.Replace("【序号】", xmxhbm).Replace("【项目小结】",
                                                   itemname + item.ItemSum).Replace("【空格】", " ").
                                                    Replace("【换行】", Environment.NewLine).ToString();
                                            }
                                            itemGroup.ItemGroupSum += groupsum;
                                            GroupSum += "&" + itemname + item.ItemSum + ";";
                                            xh = xh + 1;
                                        }
                                    }
                                }
                                //有诊断不为空这状态修改
                                if (!string.IsNullOrWhiteSpace(item.ItemDiagnosis) && string.IsNullOrWhiteSpace(ZD))
                                {
                                    hsitemZD = true;
                                }
                                if (!string.IsNullOrWhiteSpace(item.ItemDiagnosis) && !string.IsNullOrWhiteSpace(ZD))
                                {
                                    if (string.IsNullOrWhiteSpace(itemGroup.ItemGroupDiagnosis))
                                    {
                                        //重复项目不在处理
                                        if (!itemlist.Contains(item.ItemId))
                                        {

                                            string itemname = "";
                                            if (checkItem.IsSummaryName == 1)
                                            {

                                                if (checkItem.moneyType == (int)ItemType.Explain)
                                                {
                                                    if (!item.ItemDiagnosis.Contains(item.ItemName + "："))
                                                    {
                                                        itemname = item.ItemName + "：";
                                                    }
                                                }
                                                else
                                                {
                                                    if (!item.ItemDiagnosis.Contains(item.ItemName))
                                                    {
                                                        itemname = item.ItemName;
                                                    }

                                                }

                                            }
                                            string groupsum = "(" + zdxh + ")" + itemname + ZD + Environment.NewLine;
                                            if (groupsumls.Length >= 2)
                                            {
                                                string xmxhbm = zdxh.ToString();
                                                if (groupsumls[0] == "")
                                                {
                                                    xmxhbm = xmxhzd.ToString();
                                                    xmxhzd = xmxhzd + 1;
                                                }
                                                groupsum = groupsumls[1];
                                                if (groupsum.Contains("【序号】"))
                                                {
                                                    hsxh = true;
                                                }
                                                groupsum = groupsum.Replace("【序号】", xmxhbm.ToString()).Replace("【项目小结】",
                                                   itemname + ZD).Replace("【空格】", " ").
                                                    Replace("【换行】", Environment.NewLine).ToString();
                                            }
                                            itemGroup.ItemGroupDiagnosis = groupsum;
                                            itemGroup.ItemGroupOriginalDiag = "&" + itemname + ZD + ";";
                                            zdxh = zdxh + 1;
                                        }
                                    }
                                    else
                                    {
                                        //重复项目不在处理
                                        if (!itemlist.Contains(item.ItemId))
                                        {
                                            string itemname = "";
                                            if (checkItem.IsSummaryName == 1)
                                            {

                                                if (checkItem.moneyType == (int)ItemType.Explain)
                                                {
                                                    if (!item.ItemDiagnosis.Contains(item.ItemName+ "："))
                                                    {
                                                        itemname = item.ItemName + "：";
                                                    }
                                                }
                                                else
                                                {
                                                    if (!item.ItemDiagnosis.Contains(item.ItemName))
                                                    {
                                                        itemname = item.ItemName;
                                                    }
                                                }
                                            }
                                            string groupsum = "(" + zdxh + ")" + itemname + ZD + Environment.NewLine;
                                            if (groupsumls.Length >= 2)
                                            {
                                                string xmxhbm = zdxh.ToString();
                                                if (groupsumls[0] == "")
                                                {
                                                    xmxhbm = xmxhzd.ToString();
                                                    xmxhzd = xmxhzd + 1;
                                                }
                                                groupsum = groupsumls[1];
                                                if (groupsum.Contains("【序号】"))
                                                {
                                                    hsxh = true;
                                                }
                                                groupsum = groupsum.Replace("【序号】", xmxhbm).Replace("【项目小结】",
                                                   itemname + ZD).Replace("【空格】", " ").
                                                    Replace("【换行】", Environment.NewLine).ToString();
                                            }
                                            itemGroup.ItemGroupDiagnosis += groupsum;
                                            itemGroup.ItemGroupOriginalDiag += "&" + itemname + ZD + ";";
                                            zdxh = zdxh + 1;
                                        }
                                    }
                                }
                                itemlist.Add(item.ItemId);
                            }
                            //如果有诊断但是被全部隐藏则诊断为未见明显异常
                            //只有一条记录去掉序号
                            string str2 = itemGroup.ItemGroupOriginalDiag.Replace("&", "");
                            int count = itemGroup.ItemGroupOriginalDiag.Length - str2.Length;

                            string sumstr = GroupSum.Replace("&", "");
                            int count1 = GroupSum.Length - sumstr.Length;
                            if ((count == 1 || count1 == 1) && xmxhzd <= 2)
                            {
                                if (groupsumls.Length >= 1 && groupsumls[0] == "" && groupsumls[1].Contains("【序号】"))
                                {
                                    // 本科室只有一个异常才去掉
                                    var deptyc = customerItemGroups.Where(p => p.DepartmentId == itemGroup.DepartmentId).SelectMany(p => p.CustomerRegItem).
                                        Where(p => p.Symbol != null && p.Symbol != "" && p.Symbol != "M").Count();
                                    if (deptyc <= 1)
                                    {
                                        if (count1 == 1)
                                        {
                                            itemGroup.ItemGroupSum = sumstr.TrimEnd(';');
                                        }
                                        if (count == 1)
                                        {
                                            itemGroup.ItemGroupDiagnosis = str2.TrimEnd(';');
                                        }
                                        hsxh = false;
                                    }
                                }
                                else
                                {
                                    //暂时去掉可能会影响序号问题
                                    if (count1 == 1 && groupsumls.Length >= 2 && groupsumls[1].Contains("【序号】"))
                                    {
                                        itemGroup.ItemGroupSum = sumstr.TrimEnd(';');
                                    }
                                    if (count == 1 && groupsumls.Length >= 2 && groupsumls[1].Contains("【序号】"))
                                    {
                                        itemGroup.ItemGroupDiagnosis = str2.TrimEnd(';');
                                    }

                                }
                            }//GroupSum += "&" + itemname + item.ItemSum + ";";


                            _customerItemGroupRepository.Update(itemGroup);
                            //判断项目组合是不是已查或部分已查
                            if (itemGroup.CheckState == (int)ProjectIState.Complete || itemGroup.CheckState == (int)ProjectIState.Part)
                            {
                                if (!string.IsNullOrWhiteSpace(depSummary.CharacterSummary) && itemGroup.ItemGroupSum.Replace(" ", "").Replace(Environment.NewLine, "") != "未见明显异常")
                                {
                                    if (depSummary.CharacterSummary.Replace(" ", "").Replace(Environment.NewLine, "") == "未见明显异常")
                                    {
                                        if (groupsumls.Length >= 1)
                                        {
                                            depSummary.CharacterSummary = groupsumls[0].Replace("【组合名称】",
                                                itemGroup.ItemGroupName).Replace("【空格】", " ").Replace("【换行】", Environment.NewLine).Replace("【序号】", zhxh.ToString()) + itemGroup.ItemGroupSum;
                                            zhxh = zhxh + 1;
                                        }
                                        else
                                        {
                                            depSummary.CharacterSummary = itemGroup.ItemGroupName + Environment.NewLine + itemGroup.ItemGroupSum;
                                        }
                                    }
                                    else
                                    {
                                        // depSummary.CharacterSummary += itemGroup.ItemGroupName + Environment.NewLine + itemGroup.ItemGroupSum;
                                        if (groupsumls.Length >= 1)
                                        {
                                            depSummary.CharacterSummary += groupsumls[0].Replace("【组合名称】",
                                                itemGroup.ItemGroupName).Replace("【空格】", " ").Replace("【换行】", Environment.NewLine).Replace("【序号】", zhxh.ToString()) + itemGroup.ItemGroupSum;
                                            zhxh = zhxh + 1;
                                        }
                                        else
                                        {
                                            depSummary.CharacterSummary += itemGroup.ItemGroupName + Environment.NewLine + itemGroup.ItemGroupSum;
                                        }
                                    }

                                }
                                else if (string.IsNullOrWhiteSpace(depSummary.CharacterSummary))
                                {
                                    depSummary.CharacterSummary = itemGroup.ItemGroupSum;
                                    if (groupsumls.Length >= 1 && !itemGroup.ItemGroupSum.Contains("未见明显异常"))
                                    {
                                        depSummary.CharacterSummary = groupsumls[0].Replace("【组合名称】",
                                            itemGroup.ItemGroupName).Replace("【空格】", " ").Replace("【换行】", Environment.NewLine).Replace("【序号】", zhxh.ToString()) + itemGroup.ItemGroupSum;
                                        zhxh = zhxh + 1;
                                    }
                                    else
                                    {
                                        depSummary.CharacterSummary = itemGroup.ItemGroupSum;
                                    }
                                }
                                if (!string.IsNullOrWhiteSpace(itemGroup.ItemGroupDiagnosis))
                                {
                                    depSummary.OriginalDiag += "$【" + itemGroup.ItemGroupName + "】" + itemGroup.ItemGroupOriginalDiag;

                                    if (groupsumls.Length >= 1)
                                    {
                                        string xhnum = "";
                                        if (!itemGroup.ItemGroupDiagnosis.Contains("未见明显异常"))
                                        {
                                            xhnum = zhxhzd.ToString();
                                            zhxhzd = zhxhzd + 1;
                                        }
                                        //隐私小结单独存放
                                        if (itemGroup.ItemGroupBM.PrivacyState == 1)
                                        {
                                            depSummary.PrivacyCharacterSummary += groupsumls[0].Replace("【组合名称】",
                                            itemGroup.ItemGroupName).Replace("【空格】", " ").Replace("【换行】", Environment.NewLine).Replace("【序号】", xhnum.ToString()) + itemGroup.ItemGroupDiagnosis;
                                        }
                                        else
                                        {
                                            depSummary.DagnosisSummary += groupsumls[0].Replace("【组合名称】",
                                                itemGroup.ItemGroupName).Replace("【空格】", " ").Replace("【换行】", Environment.NewLine).Replace("【序号】", xhnum.ToString()) + itemGroup.ItemGroupDiagnosis;
                                        }


                                    }
                                    else
                                    {//隐私小结单独存放
                                        if (itemGroup.ItemGroupBM.PrivacyState == 1)
                                        {
                                            depSummary.PrivacyCharacterSummary += itemGroup.ItemGroupDiagnosis + Environment.NewLine;
                                        }
                                        else
                                        {
                                            depSummary.DagnosisSummary += itemGroup.ItemGroupDiagnosis + Environment.NewLine;
                                        }
                                    }
                                }
                                ////循环体检项目
                                //foreach (var item in itemGroup.CustomerRegItem)
                                //{
                                //    var itemJianYi = _summarizeAdviceRepository.GetAll().Where(r => r.DepartmentId == currentDepartment.Id).FirstOrDefault(o => o.MinAge <= customerReg.Customer.Age && o.MaxAge >= customerReg.Customer.Age && (o.SexState == customerReg.Customer.Sex || o.SexState == sexGenderNotSpecified) && o.AdviceName == item.ItemSum);
                                //    if (itemJianYi != null)
                                //    {
                                //        cusSummary.CharacterSummary += itemJianYi.DepartmentAdvice + Environment.NewLine;
                                //    }
                                //}
                            }
                            //加上已经保存的报告单号
                            if (HasGroup == true && !string.IsNullOrEmpty(itemGroup.ReportBM) &&
                              !reportBMList.Contains(itemGroup.ReportBM))
                            {                                 
                                    reportBMList.Add(itemGroup.ReportBM);                                 
                            }
                        }

                        depSummary.CheckDate = customerItemGroups.OrderBy(o => o.FirstDateTime).First().FirstDateTime;
                        var sumemp = customerItemGroups.Where(o => o.InspectEmployeeBMId != null).OrderBy(o => o.FirstDateTime).ToList();
                        if (sumemp.Count > 0)
                        {
                            depSummary.ExamineEmployeeBMId = sumemp.First()?.InspectEmployeeBMId;
                            depSummary.ExamineEmployeeBM = _userRepository.Get(depSummary.ExamineEmployeeBMId.Value);
                        }
                        //去掉收尾换行
                        if (depSummary.CharacterSummary != null && depSummary.CharacterSummary.Length > 2 && depSummary.CharacterSummary.Substring(0, 2) == Environment.NewLine)
                        {
                            depSummary.CharacterSummary = depSummary.CharacterSummary.Substring(2, depSummary.CharacterSummary.Length - 2);
                        }
                        if (depSummary.DagnosisSummary != null && depSummary.DagnosisSummary.Length > 2 && depSummary.DagnosisSummary.Substring(0, 2) == Environment.NewLine)
                        {
                            depSummary.DagnosisSummary = depSummary.DagnosisSummary.Substring(2, depSummary.DagnosisSummary.Length - 2);
                        }
                        depSummary.DagnosisSummary = depSummary.DagnosisSummary?.TrimEnd((char[])"\r\n".ToCharArray());
                        depSummary.CharacterSummary = depSummary.CharacterSummary?.TrimEnd((char[])"\r\n".ToCharArray());

                        if (hsitemZD == false && string.IsNullOrWhiteSpace(depSummary.DagnosisSummary))
                        {
                            depSummary.DagnosisSummary = "未见明显异常";
                        }
                        //包含序号且只有一条不显示序号
                        if (hsxh == true && xmxh == 2)
                        {

                            Regex reg = new Regex(@"^1.");
                            depSummary.CharacterSummary = reg.Replace(depSummary.CharacterSummary, "");
                            //Regex hsreg = new Regex(@"^(1)");
                            //depSummary.CharacterSummary = hsreg.Replace(depSummary.CharacterSummary, "");
                        }
                        if (hsxh == true && xmxhzd == 2)
                        {
                            Regex reg = new Regex(@"^1.");
                            depSummary.DagnosisSummary = reg.Replace(depSummary.DagnosisSummary, "");

                            //Regex hsreg = new Regex(@"^(1)");
                            //depSummary.DagnosisSummary = hsreg.Replace(depSummary.DagnosisSummary, "");
                        }
                        //if (zxh == 1)
                        //{
                        //     if(depSummary.CharacterSummary.Substring(0,3)=="(1)")
                        //    depSummary.CharacterSummary = depSummary.CharacterSummary.Substring(2, depSummary.CharacterSummary.Length-3);
                        //    if (depSummary.DagnosisSummary.Substring(0, 3) == "(1)")
                        //        depSummary.DagnosisSummary = depSummary.DagnosisSummary.Substring(2, depSummary.DagnosisSummary.Length - 3);
                        //}
                        if (depSummary.Id == Guid.Empty)
                        {
                            depSummary.Id = Guid.NewGuid();
                            depSummary.TenantId = TenantId;
                            _customerDepSummaryRepository.Insert(depSummary);
                            logtxt = "生成科室小结（增加），科室：" + depSummary.DepartmentName;
                            //添加操作日志  
                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                            //createOpLogDto.LogName = customerReg.Customer.Name;
                            //createOpLogDto.LogText = "生成小结成功";
                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                            //createOpLogDto.LogDetail = logtxt;
                            //_commonAppService.SaveOpLog(createOpLogDto);

                        }
                        else
                        {
                            _customerDepSummaryRepository.Update(depSummary);
                            //添加操作日志  
                            //createOpLogDto.LogBM = customerReg.CustomerBM;
                            //createOpLogDto.LogName = customerReg.Customer.Name;
                            //createOpLogDto.LogText = "生成小结成功";
                            //createOpLogDto.LogType = (int)LogsTypes.InterId;
                            //createOpLogDto.LogDetail = "生成科室小结（修改），科室：" + depSummary.DepartmentName; 
                            //_commonAppService.SaveOpLog(createOpLogDto);


                        }
                        //更新体检日期
                        var fistTime = customerReg.CustomerItemGroup.Where(o => o.FirstDateTime != null).OrderBy(o => o.FirstDateTime).FirstOrDefault();
                        if (fistTime != null)
                        {
                            var reg = _customerRegRepository.Get(customerReg.Id);
                            reg.BookingDate = fistTime.FirstDateTime;
                            _customerRegRepository.Update(reg);
                        }

                        //if (cusSummary.Id == Guid.Empty)
                        //{
                        //    cusSummary.Id = Guid.NewGuid();
                        //    _customerSummary.Insert(cusSummary);
                        //}
                        //else
                        //{
                        //    _customerSummary.Update(cusSummary);
                        //}
                    }
                }
            }
            return isOK;
        }
        /// <summary>
        /// 生成未小结科室小结
        /// </summary>
        /// <param name="regid"></param>
        /// <returns></returns>
        public bool CreateAllNoConclusion(EntityDto<Guid> regid)
        {
            var cusBM = _customerRegRepository.Get(regid.Id).CustomerBM;
            var alldepard = _customerItemGroupRepository.GetAll().Where(o =>
            o.CustomerRegBMId == regid.Id && o.IsAddMinus != (int)AddMinusType.Minus && o.CheckState != 1).Select(
                o => o.DepartmentId).Distinct().ToList();
            var hsconclus = _customerDepSummaryRepository.GetAll().Where(o => o.CustomerRegId == regid.Id).Select(o => o.DepartmentBMId).ToList();
            var nusumdepatls = alldepard.Where(o => !hsconclus.Contains(o)).ToList();
            if (nusumdepatls != null && nusumdepatls.Count > 0)
            {
                CreateConclusionDto createConclusionDto = new CreateConclusionDto();
                createConclusionDto.Department = nusumdepatls;
                createConclusionDto.CustomerBM = cusBM;
                return CreateConclusion(createConclusionDto);
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判断是否锁定
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public CusLockDto JudgeLocking(QueryClass Query)
        {
            CusLockDto cusLock = new CusLockDto();
        
            var isOk=   _customerRegRepository.GetAll().Any(o => o.Id == Query.CustomerRegId && o.SummLocked == (int)SDState.Locking);
            cusLock.IsLock = isOk;
            if (isOk)
            {
              var cusreg=  _customerRegRepository.GetAll().FirstOrDefault(o => o.Id == Query.CustomerRegId && o.SummLocked == (int)SDState.Locking);             
              cusLock.LockUser = cusreg.SummLockEmployeeBM?.Name;
            }
          

            return cusLock;
        }
        /// <summary>
        /// 根据条码号查询体检号
        /// </summary>
        /// <param name="query">条码号</param>
        /// <returns></returns>
        public string GetCusBarPrintInfo(QueryClass query)
        {
            var Info = _customerBarPrintInfoRepository.FirstOrDefault(o => o.BarNumBM == query.BarNumBM);
            if (Info != null && Info.CustomerReg_Id != Guid.Empty)
            {
                Guid id = Guid.Parse(Info.CustomerReg_Id.ToString());
                var CusInfo = _customerRegRepository.Get(id);
                if (CusInfo != null)
                {
                    return CusInfo.CustomerBM;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 修改危急值状态
        /// </summary>
        /// <param name="updateClass"></param>
        /// <returns></returns>
        public OutStateDto UpdateCrisisSate(UpdateClass updateClass)
        {
            OutStateDto outStateDto = new OutStateDto();
            var customerItem = _customerRegItemRepository.FirstOrDefault(o => o.Id == updateClass.CustomerItemId);
            if (customerItem.CrisisVisitSate == 3)
            {
                outStateDto.IsOK = false;
                outStateDto.ErrMess = "已审核不能取消危急值状态！";
                return outStateDto;
            }
            if (updateClass.CrisisSate == (int)CrisisSate.Abnormal)
            {
                customerItem.CrisisSate = (int)CrisisSate.Abnormal;
                customerItem.PositiveSate = (int)PositiveSate.Abnormal;

             
                customerItem.CrisisLever = updateClass.CrisisLever;
                customerItem.CrisiChar = updateClass.CrisiChar;
                customerItem.CrisisVisitSate = (int)CrisisVisitSate.Yes;

 

            }
            else
            {
                customerItem.CrisisSate = (int)CrisisSate.Normal;
                customerItem.PositiveSate = (int)PositiveSate.Normal;
                customerItem.CrisisVisitSate = null;

                customerItem.CrisisLever = null;
                customerItem.CrisiChar = "";
            }

            _customerRegItemRepository.Update(customerItem);
            outStateDto.IsOK = true;
            outStateDto.ErrMess = "成功！";
            return outStateDto;
        }



        #region 
        /// <summary>
        /// 批量单个项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ATjlCustomerRegDto BatchInsertDodyFat(QueryClass query)
        {
            // var renyuan = _customerRegRepository.GetAll();
            var xiangmuid = "8354E65E-0903-442D-8B32-3C168D1A88CB";
            var cusid = "6479D668-01BE-4ADD-942C-90B3EB40D191";
            System.Data.SqlClient.SqlParameter[] parameters = {
                    new SqlParameter("@itemid",xiangmuid),
                     new SqlParameter("@cusid",cusid)
            };
            int i = 0;
            int b = 0;
            List<TjlCustomerReg> reg = _sqlExecutor.SqlQuery<TjlCustomerReg>

                                ("select * from TjlCustomerRegs  where Id in(select CustomerRegBMId from TjlCustomerItemGroups where ItemGroupBM_Id ='6479D668-01BE-4ADD-942C-90B3EB40D191' and IsDeleted=0 and Id not in(select CustomerItemGroupBMid from TjlCustomerRegItems where ItemId='8354E65E-0903-442D-8B32-3C168D1A88CB'))  and IsDeleted =0 and CustomerBM in('0315049','0315053','1901150003','1901150001','1901150010','1901210014','1901210022','1901210020','1901220071','1901220068','1901220072','1901220070','1901220069','1901220073','1901240013','1901230006','1901240011','1901230005')", parameters).ToList();
            foreach (var itemrenyuan in reg)
            {
                var zuheid = Guid.Parse(cusid);
                var zuhe = _customerItemGroupRepository.GetAll().FirstOrDefault(o => o.CustomerRegBMId == itemrenyuan.Id
                  && o.ItemGroupBM_Id == zuheid);
                if (zuhe != null)
                {
                    var xiangmu = _customerRegItemRepository.GetAll().Where(o => o.CustomerItemGroupBMid == zuhe.Id).ToList();
                    if (xiangmu.Count() >= 0)
                    {
                        var xiangmuGuid = Guid.Parse(xiangmuid);
                        if (!xiangmu.Any(o => o.ItemId == xiangmuGuid))
                        {
                            var keshiGuid = Guid.Parse("B9B026C8-3875-4838-A84F-DA9B74B734B0");
                            var keshi = _departmentRepository.Get(keshiGuid);
                            var zuhebianma = _bmItemGroup.Get(zuheid);

                            var xiangmubianma = _itemInfoRepository.Get(xiangmuGuid);
                            //var shengao = 0.00;
                            //var tizhong = 0.00;
                            //var InspectEmployeeBMId = xiangmu.FirstOrDefault().InspectEmployeeBMId;
                            //var CheckEmployeeBMId = xiangmu.FirstOrDefault().CheckEmployeeBMId;
                            //var shengaoGuid = Guid.Parse("8AED1301-D972-4422-862A-C9995922A69F");
                            //if (xiangmu.Any(o => o.ItemId == shengaoGuid))
                            //{
                            //    if (!string.IsNullOrWhiteSpace(xiangmu.FirstOrDefault(o => o.ItemId == shengaoGuid).ItemResultChar))
                            //    {
                            //        shengao = double.Parse(xiangmu.FirstOrDefault(o => o.ItemId ==
                            //         shengaoGuid).ItemResultChar);
                            //    }
                            //}
                            //var tizhongGuid = Guid.Parse("E041EBEB-4DF9-4E07-8380-49ACB26F5558");
                            //if (xiangmu.Any(o => o.ItemId == tizhongGuid))
                            //{
                            //    if (!string.IsNullOrWhiteSpace(xiangmu.FirstOrDefault(o => o.ItemId == tizhongGuid).ItemResultChar))
                            //    {
                            //        tizhong = double.Parse(xiangmu.FirstOrDefault(o => o.ItemId == tizhongGuid
                            //        ).ItemResultChar);
                            //    }
                            //}
                            TjlCustomerRegItem insert = new TjlCustomerRegItem();
                            insert.Id = Guid.NewGuid();
                            //insert.CustomerRegBM = itemrenyuan;
                            insert.CustomerRegId = itemrenyuan.Id;
                            //insert.CustomerItemGroupBM = zuhe;
                            insert.CustomerItemGroupBMid = zuhe.Id;
                            insert.DepartmentId = zuhe.DepartmentId;
                            //insert.DepartmentBM = keshi;
                            //insert.ItemGroupBM = zuhebianma;
                            insert.ItemGroupBMId = zuhebianma.Id;
                            insert.ItemId = xiangmubianma.Id;
                            //insert.ItemBM = xiangmubianma;
                            //if (shengao != 0 && tizhong != 0)
                            //{
                            //    insert.ItemResultChar = (tizhong / (shengao * shengao) * 10000).ToString("0.00");
                            //    insert.InspectEmployeeBMId = InspectEmployeeBMId; //检查医生
                            //    insert.CheckEmployeeBMId = CheckEmployeeBMId; //审核人
                            //    insert.ProcessState = (int)ProjectIState.Complete; //状态

                            //}
                            insert.ItemName = xiangmubianma.Name;
                            insert.ItemTypeBM = xiangmubianma.moneyType;
                            insert.ItemOrder = xiangmubianma.OrderNum;
                            insert.Unit = xiangmubianma.Unit;
                            insert.ProcessState = (int)ProjectIState.Not;
                            insert.DiagnSate = xiangmubianma.DiagnosisComplexSate;
                            //insert.SummBackSate = (int)SDState.Unlocked;
                            _customerRegItemRepository.Insert(insert);
                            b++;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                {
                    i++;
                }
            }

            return new ATjlCustomerRegDto();
        }
        /// <summary>
        /// 计算体脂
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ATjlCustomerRegDto CalculationConstitution(QueryClass query)
        {
            // var renyuan = _customerRegRepository.GetAll();
            var xiangmuid = "E3B738C9-3DB6-4779-80B5-A4699E40CDD4";
            var zuHeGuid = Guid.Parse("1C7AA37B-A021-4EA5-9A14-FAAC648ABD90");
            var shenGaoGuid = Guid.Parse("8AED1301-D972-4422-862A-C9995922A69F");
            var tiZhongGuid = Guid.Parse("E041EBEB-4DF9-4E07-8380-49ACB26F5558");
            System.Data.SqlClient.SqlParameter[] parameters = {
                    new SqlParameter("@itemid",xiangmuid),
            };
            int i = 0;
            int b = 0;
            List<TjlCustomerReg> reg = _sqlExecutor.SqlQuery<TjlCustomerReg>
                ("select * from TjlCustomerRegs where Id in( select CustomerRegBMId from TjlCustomerItemGroups where IsDeleted =0 and Id in( select CustomerItemGroupBMid from TjlCustomerRegItems where ItemId =@itemid and (ItemResultChar ='' or ItemResultChar is null) and IsDeleted =0)) and IsDeleted =0 and LoginDate >'2018-12-16'", parameters).ToList();
            foreach (var itemrenyuan in reg)
            {
                var xiangMuGuid = Guid.Parse(xiangmuid);

                var xiangmu = _customerRegItemRepository.GetAll().Where(o => o.ItemGroupBMId == zuHeGuid && o.CustomerRegId == itemrenyuan.Id).ToList();
                if (xiangmu.Count() > 0)
                {
                    var shengao = 0.00;
                    var tizhong = 0.00;
                    var shengaos = xiangmu.FirstOrDefault(o => o.ItemId == shenGaoGuid);
                    if (shengaos != null && !string.IsNullOrWhiteSpace(shengaos.ItemResultChar))
                    {
                        shengao = double.Parse(shengaos.ItemResultChar);
                    }
                    var tizhongs = xiangmu.FirstOrDefault(o => o.ItemId == tiZhongGuid);
                    if (tizhongs != null && !string.IsNullOrWhiteSpace(tizhongs.ItemResultChar))
                    {
                        tizhong = double.Parse(tizhongs.ItemResultChar);
                    }
                    if (shengao != 0 && tizhong != 0)
                    {
                        var tiZhi = xiangmu.FirstOrDefault(o => o.ItemId == xiangMuGuid);
                        if (tiZhi != null && string.IsNullOrWhiteSpace(tiZhi.ItemResultChar))
                        {
                            var Items = _customerRegItemRepository.FirstOrDefault(o => o.Id == tiZhi.Id);
                            Items.ItemResultChar = (tizhong / (shengao * shengao) * 10000).ToString("0.00");
                            _customerRegItemRepository.Update(Items);
                            b++;
                            continue;
                        }
                    }
                    i++;

                }
                else
                {
                    i++;
                }

            }

            return new ATjlCustomerRegDto();
        }
        #endregion
        /// <summary>
        /// 保存问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool SaveCustomerQuestion(List<SaveCustomerQusTionDto> input)
        {
            Guid guid = input.First().CustomerRegId.Value;
            List<TjlCustomerQuestion> customerquestions = new List<TjlCustomerQuestion>();
            var que = _CusQuestion.GetAll();
            customerquestions = que.Where(o => o.CustomerRegId == guid).ToList();
            for (int n = 0; n < customerquestions.Count; n++)
            {
                _CusQuestion.Delete(customerquestions[n]);
            }
            //CurrentUnitOfWork.SaveChanges();
            foreach (SaveCustomerQusTionDto cusqus in input)
            {
                TjlCustomerQuestion tjlCusQuestion = new TjlCustomerQuestion();
                tjlCusQuestion.Id = Guid.NewGuid();
                tjlCusQuestion.OneAddXQuestionnaireid = cusqus.OneAddXQuestionnaireid;
                if (cusqus.ClientRegId != Guid.Empty)
                {
                    tjlCusQuestion.ClientRegId = cusqus.ClientRegId;
                }
                var Qusestion = _OneAddXQuestionnaire.Get(cusqus.OneAddXQuestionnaireid.Value);
                tjlCusQuestion.QuestionName = Qusestion.Name;
                tjlCusQuestion.QuestionType = Qusestion.Category;
                tjlCusQuestion.OutQuestionID = Qusestion.ExternalCode;
                tjlCusQuestion.CustomerRegId = cusqus.CustomerRegId;
                //tjlCusQuestion.
                _CusQuestion.Insert(tjlCusQuestion);
            }
            // CurrentUnitOfWork.SaveChanges();
            return true;
        }
        /// <summary>
        /// 获取所有单位信息
        /// </summary>
        /// <returns></returns>
        public List<SearchClientInfoDto> GetSearchClientInfoDto(DeparmentCustomerSearch Search)
        {
            var ClientInfo = _clientInfo.GetAll();
            return ClientInfo.MapTo<List<SearchClientInfoDto>>();
        }
        /// <summary>
        /// 科室人员查询
        /// </summary>
        /// <param name="Search"></param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetDeparmentCustomerRegs(DeparmentCustomerSearch Search)
        {
            var Get = _customerRegRepository.GetAll().Where(o => o.RegisterState == (int)RegisterState.Yes);

            //体检号/姓名
            if (!string.IsNullOrWhiteSpace(Search.CustomerBMOrName))
            {
                Get = Get.Where(o => o.CustomerBM.Contains(Search.CustomerBMOrName)
                 || o.Customer.Name.Contains(Search.CustomerBMOrName));
            }
            //登记时间
            if (Search.StartTime != null && Search.EndTime != null)
            {
                Get = Get.Where(o => o.LoginDate >= Search.StartTime && o.LoginDate <= Search.EndTime);
            }
            //单位
            if (Search.ClientInfoId != null && Search.ClientInfoId != Guid.Empty)
            {
                Get = Get.Where(o => o.ClientInfoId == Search.ClientInfoId);
            }
            //科室
            if (Search.DepartmentBM.Count > 0)
            {
                Get = Get.Where(o => o.CustomerItemGroup.Where(p => Search.DepartmentBM.Contains(p.DepartmentId)
                && p.IsAddMinus != (int)AddMinusType.Minus && p.PayerCat != (int)PayerCatType.NoCharge).Count() > 0);
            }
            //我的患者
            if (Search.IsOwn)
            {
                Get = Get.Where(o => o.CustomerItemGroup.Where(p => p.InspectEmployeeBMId == Search.OwnId).Count() > 0);
            }

            //体检状态
            if (Search.CheckSate != null)
            {
                Get = Get.Where(o => o.CheckSate == Search.CheckSate);
            }
            //总检状态
            if (Search.SummSate != null)
            {
                Get = Get.Where(o => o.SummSate == Search.SummSate);
            }
            //项目未完成
            if (Search.NotComplete)
            {
                Get = Get.Where(o => o.CustomerRegItems.Where(p => Search.DepartmentBM.Contains(p.DepartmentId) && p.ProcessState == (int)ProjectIState.Not).Count() > 0);
            }
            //未生成小结
            if (Search.IsOwnSumm)
            {
                var Customer = Get.ToList();
                var trCustomer = Customer.Where(o => o.CustomerDepSummary == null ||
                o.CustomerDepSummary.Where(p => p.CharacterSummary == "" && p.DagnosisSummary == ""
                && Search.DepartmentBM.Contains(p.DepartmentBMId.Value)).Count() > 0 ||
                o.CustomerDepSummary.Where(i => Search.DepartmentBM.Contains(i.DepartmentBMId.Value)).Count() == 0);
                return trCustomer.MapTo<List<TjlCustomerRegForInspectionTotalDto>>();
            }
            return Get.MapTo<List<TjlCustomerRegForInspectionTotalDto>>();
        }
        /// <summary>
        /// 重置患者状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool ResetCustomerChecked(QueryClass query)
        {
            var Customer = _customerRegRepository.FirstOrDefault(o => o.CustomerBM == query.CustomerBM);
            if (Customer != null)
            {
                //var Info = _customerSummarizeRepository.FirstOrDefault(o => o.CustomerRegID == Customer.Id);
                //if (Info == null)
                //    return false;
                //重置状态
                Customer.SummSate = (int)SummSate.NotAlwaysCheck;
                //Customer.FSEmployeeId = null;
                //Customer.CSEmployeeId = null;
                // Customer.CheckSate = (int)PhysicalEState.Process;
                Customer.SendToConfirm = (int)SendToConfirm.No;
                _customerRegRepository.Update(Customer);
                ////重置汇总 
                //Info.EmployeeBM = null;
                //Info.ShEmployeeBMId = null;
                //Info.CheckState = 1;
                //Info.EmployeeBMId = null;
                //var result = _customerSummarizeRepository.Update(Info);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 所有计算型公式
        /// </summary>
        /// <returns></returns>
        public List<ItemProcExpressDto> getItemProcExpress()
        {
            List<ItemProcExpressDto> itemProcExpresses = _ItemProcExpress.GetAll().MapTo<List<ItemProcExpressDto>>();
            return itemProcExpresses;
        }
        #region pdf转jpg
        //static void ExportImage(PdfDictionary image, ref int count)
        //{
        //    string filter = image.Elements.GetName("/Filter");
        //    switch (filter)
        //    {
        //        case "/DCTDecode":
        //            ExportJpegImage(image, ref count);
        //            break;

        //        case "/FlateDecode":
        //            ExportAsPngImage(image, ref count);
        //            break;
        //    }
        //}
        //static void ExportJpegImage(PdfDictionary image, ref int count)
        //{
        //    // Fortunately JPEG has native support in PDF and exporting an image is just writing the stream to a file.
        //    byte[] stream = image.Stream.Value;
        //    FileStream fs = new FileStream(String.Format("Image{0}.jpeg", count++), FileMode.Create, FileAccess.Write);
        //    BinaryWriter bw = new BinaryWriter(fs);
        //    bw.Write(stream);
        //    bw.Close();
        //}


        //static void ExportAsPngImage(PdfDictionary image, ref int count)
        //{
        //    int width = image.Elements.GetInteger(PdfImage.Keys.Width);
        //    int height = image.Elements.GetInteger(PdfImage.Keys.Height);
        //    int bitsPerComponent = image.Elements.GetInteger(PdfImage.Keys.BitsPerComponent);

        //    // TODO: You can put the code here that converts vom PDF internal image format to a Windows bitmap
        //    // and use GDI+ to save it in PNG format.
        //    // It is the work of a day or two for the most important formats. Take a look at the file
        //    // PdfSharp.Pdf.Advanced/PdfImage.cs to see how we create the PDF image formats.
        //    // We don't need that feature at the moment and therefore will not implement it.
        //    // If you write the code for exporting images I would be pleased to publish it in a future release
        //    // of PDFsharp.
        //}
        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }

        /// <summary>
        /// 将PDF文档转换为图片的方法
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="imageOutputPath">图片输出路径</param>
        /// <param name="imageName">生成图片的名字</param>
        /// <param name="startPageNum">从PDF文档的第几页开始转换</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>
        /// <param name="imageFormat">设置所需图片格式</param>
        /// <param name="definition">设置图片的清晰度，数字越大越清晰</param>
        //public static List<string> ConvertPDF2Image(string pdfInputPath, string imageOutputPath,
        //    string imageName, int startPageNum, int endPageNum, ImageFormat imageFormat, Definition definition)
        //{
        //    List<string> images = new List<string>();

        //    PDFFile pdfFile = PDFFile.Open(pdfInputPath);

        //    if (!Directory.Exists(imageOutputPath))
        //    {
        //        Directory.CreateDirectory(imageOutputPath);
        //    }

        //    // validate pageNum
        //    if (startPageNum <= 0)
        //    {
        //        startPageNum = 1;
        //    }

        //    if (endPageNum > pdfFile.PageCount)
        //    {
        //        endPageNum = pdfFile.PageCount;
        //    }

        //    if (startPageNum > endPageNum)
        //    {
        //        int tempPageNum = startPageNum;
        //        startPageNum = endPageNum;
        //        endPageNum = startPageNum;
        //    }
        //    // start to convert each page
        //    for (int i = startPageNum; i <= endPageNum; i++)
        //    {
        //        Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * (int)definition);
        //        pageImage.Save(imageOutputPath + imageName + i.ToString() + "." + imageFormat.ToString(), imageFormat);
        //        pageImage.Dispose();
        //        images.Add(imageOutputPath + imageName + i.ToString() + "." + imageFormat.ToString());
        //    }

        //    pdfFile.Dispose();
        //    return images;
        //}

        /// <summary>
        /// PDF转JPG(不丢失文字)
        /// </summary>
        /// <param name="inFilePath">输入物理路径（E:\\pdf\\test.pdf）</param>
        /// <param name="pagenum">打印页数（E:\\img\\test.jpg）</param>
        public static string PDFConvertToJPG(string inFilePath,string imageOutputPath, int pagenum)
        {
            // var path = Path.GetDirectoryName(inFilePath);//目录信息
            var path = imageOutputPath;
             var filename = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(inFilePath));//文件名
            using (var document = PdfDocument.Load(inFilePath))
            {
                var pageCount = document.PageCount;

                var dpi = 300;
                using (var image = document.Render(pagenum, dpi, dpi, PdfRenderFlags.CorrectFromDpi))
                {
                    var encoder = ImageCodecInfo.GetImageEncoders()
                        .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    var encParams = new EncoderParameters(1);
                    encParams.Param[0] = new EncoderParameter(
                        System.Drawing.Imaging.Encoder.Quality, 10L);

                    image.Save(path + @"\" + filename + pagenum.ToString()+ "." + "jpg", encoder, encParams);
                }

                return path + @"\" + filename+ pagenum.ToString() + "." + "jpg";
            }
        }
        #endregion
        #region 将docm图片转成jpg图片路径
        /// <summary>
        /// 将docm图片转成jpg图片路径
        /// </summary>
        /// <param name="docmpath">docm图片路径</param>
        /// <param name="strimagepath">存储jpg路径</param>
        /// <param name="inum">图片缩小倍数</param>
        /// <returns>是否转换成功</returns>
        public bool DocmConvertToImagePath(string docmpath, string strimagepath, int inum,string custBM)
        {

            bool br = true;
            try
            {
                string fileName = docmpath;

                string fullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", fileName);

                DicomFile file = DicomFile.Open(fullName);

                //生成设置要改为X64位
                //System.Reflection.Assembly.LoadFrom(Path.Combine(Application.StartupPath, "Dicom.Native.dll"));

                //file //= file.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian, new DicomJpegLsParams());
                string path = AppDomain.CurrentDomain.BaseDirectory;
                Dicom.Imaging.Codec.DesktopTranscoderManager.LoadCodecs(path, "Dicom.Native*.dll");

                var image = new DicomImage(file.Dataset);

                //image.NumberOfFrames 如果有多帧图片需要将每帧都转成jpeg 
                Bitmap bitmap = image.RenderImage().AsBitmap();

                //生成缩略图
                string imageName = strimagepath;
                Bitmap bit = new Bitmap(bitmap, bitmap.Width / inum, bitmap.Height / inum);
                bit.Save(strimagepath);
                //var images =  bitmap.GetThumbnailImage(bitmap.Width/2,bitmap.Height/2, null, IntPtr.Zero);
                //images.Save("test2.jpg");

                bit.Dispose();
                bitmap.Dispose();
                //bitmap.Save(imageName);
                //bitmap.Save(imageName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                //textBox2.Text = ex.Message;
                br = false;
                //添加操作日志  
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = custBM;
                createOpLogDto.LogName = custBM;
                createOpLogDto.LogText = "dcm转换异常";
                createOpLogDto.LogType = (int)LogsTypes.InterId;
                createOpLogDto.LogDetail = ex.Message;
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            return br;

        }

        #endregion
        public void UpCheckState(EntityDto<Guid> input)
        {
            var reg = _customerRegRepository.FirstOrDefault(o => o.Id == input.Id);
            //var regs = _customerRegItemRepository.FirstOrDefault(o => o.CustomerRegId == input.Id);
            if (!_customerItemGroupRepository.GetAll().Any(o =>
                    o.CustomerRegBMId == reg.Id && o.CheckState != (int)ProjectIState.Complete
                    && o.CheckState != (int)ProjectIState.Part &&
                    o.CheckState != (int)ProjectIState.GiveUp &&
                    o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus
                    && o.DepartmentBM.Category != "耗材"))
            {
                reg.CheckSate = (int)PhysicalEState.Complete;
                _customerRegRepository.Update(reg);
            }
            //else if (_customerItemGroupRepository.GetAll().Any(o =>
            //        o.CustomerRegBMId == reg.Id && o.CheckState != (int)ProjectIState.Complete
            //        && o.CheckState != (int)ProjectIState.Part && regs.ProcessState != (int)ProjectIState.Not
            //        && regs.ProcessState != (int)ProjectIState.Complete && regs.ProcessState != (int)ProjectIState.Stay 
            //        && o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus
            //        && o.DepartmentBM.Category != "耗材"))
            //{                
            //    regs.ProcessState =(int)ProjectIState.GiveUp;
            //    _customerRegItemRepository.Update(regs);
            //}
            else
            {
                //修改体检人成为体检中状态
                //if (reg.CheckSate == (int)PhysicalEState.Not)
                //{
                reg.CheckSate = (int)PhysicalEState.Process;
                _customerRegRepository.Update(reg);
            }
        }
        /// <summary>
        /// 到检信息查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CheckCusRegGroupDto> getCheckCusGroup(SearchCusGroupDto input)
        {
            var que = _customerItemGroupRepository.GetAll().Where(p=>p.IsAddMinus!=3 && p.CustomerRegBM!=null);
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(p=>p.CustomerRegBM.ClientRegId== input.ClientRegId);
            }
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                que = que.Where(p=>p.CustomerRegBM.CustomerBM==input.CustomerBM);
            }
            if (input.CheckSate.HasValue)
            {
                que = que.Where(p=>p.CustomerRegBM.CheckSate==input.CheckSate);
            }
            if (input.RegisterState.HasValue)
            {
                que = que.Where(p=>p.CustomerRegBM.RegisterState==input.RegisterState);
            }
            if (input.SummSate.HasValue)
            {
                que = que.Where(p=>p.CustomerRegBM.SummSate==input.SummSate);
            }
            if (input.StartDate.HasValue && input.EndtDate.HasValue)
            {
                //按登记时间
                if (input.TimeType == 1)
                {
                    que = que.Where(p => p.CustomerRegBM.LoginDate>=input.StartDate 
                    && p.CustomerRegBM.LoginDate<input.EndtDate);
                }
                else if (input.TimeType == 2)//体检时间
                {
                    que = que.Where(p => p.CustomerRegBM.BookingDate >= input.StartDate
                   && p.CustomerRegBM.BookingDate < input.EndtDate);
                }
                else
                {
                    que = que.Where(p=>p.FirstDateTime>=input.StartDate 
                    && p.FirstDateTime <input.EndtDate);
                }               
            }
            var outSult = que.Select(p => new CheckCusRegGroupDto
            {
                Age = p.CustomerRegBM.Customer.Age,
                BookingDate = p.CustomerRegBM.BookingDate,
                CheckSate = p.CustomerRegBM.CheckSate,
                ClientName = p.CustomerRegBM.ClientInfo == null ? "" : p.CustomerRegBM.ClientInfo.ClientName,
                CustomerBM = p.CustomerRegBM.CustomerBM,
                DepartmentName = p.DepartmentName,
                DiscountRate = p.DiscountRate,
                FirstDateTime = p.FirstDateTime,
                GroupCheckState = p.CheckState,
                ItemGroupName = p.ItemGroupName,
                ItemPrice = p.ItemPrice,
                LoginDate = p.CustomerRegBM.LoginDate,
                Name = p.CustomerRegBM.Customer.Name,
                PayerCat = p.PayerCat,
                Count = 1,
                Department = p.CustomerRegBM.Customer.Department,
                 MReceiptInfoClientlId=p.MReceiptInfoClientlId,
                  MReceiptInfoPersonalId=p.MReceiptInfoPersonalId,
                PriceAfterDis = p.PriceAfterDis,
                RegisterState = p.CustomerRegBM.RegisterState,
                Sex = p.CustomerRegBM.Customer.Sex,
                StartCheckDate = p.CustomerRegBM.ClientReg.StartCheckDate,
                SummSate = p.CustomerRegBM.SummSate,
                TeamName = p.CustomerRegBM.ClientTeamInfo == null ? "" : p.CustomerRegBM.ClientTeamInfo.TeamName
            }).OrderBy(p=>p.CustomerBM).ThenByDescending(p=>p.LoginDate).ToList();
            return outSult;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="itemImages"></param>
        public void SaveItemPic(List<CustomerItemPicDto> input)
        {
            //先删除
            if (input.Count > 0)
            {
                var cusItemId = input[0].ItemBMID;
                var CustomerRegID = input[0].TjlCustomerRegID;
                var customerItemPics = _customerItemPicRepository.GetAll()
                                       .Where(r => r.ItemBMID == cusItemId
                                       && r.TjlCustomerRegID == CustomerRegID).ToList();


                foreach (var itemDelete in customerItemPics)
                {

                    _customerItemPicRepository.Delete(itemDelete);
                    //interfaceBack.JKStrBui.Append("先删除原有图片：" + itemDelete.Id);
                }
            }
            var PicInput = input.Where(p => p.PictureBM != null).ToList();

            foreach (var picInfo in PicInput)
            {
               
                    //interfaceBack.JKStrBui.Append("上传图片路径开始：" + imgId);
                    
                var pic = picInfo.MapTo<TjlCustomerItemPic>();
                   pic.Id = Guid.NewGuid();
                pic.TjlCustomerRegID = picInfo.TjlCustomerRegID;
                pic.CustomerItemGroupID = picInfo.CustomerItemGroupID;
                pic.ItemBMID = picInfo.ItemBMID;
                pic.PictureBM = picInfo.PictureBM;
                _customerItemPicRepository.Insert(pic);
                // interfaceBack.JKStrBui.Append("上传图片路径结束：" + imgId);
                CurrentUnitOfWork.SaveChanges();

            }
        }

    }
}