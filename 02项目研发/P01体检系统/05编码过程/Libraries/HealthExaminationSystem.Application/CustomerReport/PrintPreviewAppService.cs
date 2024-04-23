using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport
{
    [AbpAuthorize]
    public class PrintPreviewAppService : MyProjectAppServiceBase, IPrintPreviewAppService
    {
        private readonly IRepository<TjlClientInfo, Guid> _clientInfoRepository;

        private readonly IRepository<TjlClientReg, Guid> _clientRegRepository;

        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository;

        private readonly IRepository<TjlCustomerReportCollection, Guid> _customerReportCollectionRepository;

        private readonly IRepository<TjlCustomerReportPrintInfo, Guid> _customerReportPrintInfoRepository;

        private readonly IRepository<TjlCustomer, Guid> _customerRepository;

        private readonly IRepository<TjlCustomerSummarize, Guid> _customerSummarizeRepository;

        private readonly IRepository<TbmItemSuit, Guid> _itemSuitRepository;

        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<TjlOccQuestionnaire, Guid> _TjlOccQuestionnaire;

        private readonly IRepository<TbmOccDictionary, Guid> _TbmOccDictionary;
        private readonly IRepository<TjlOccQuesSymptom, Guid> _TjlOccQuesSymptom;
        private readonly IRepository<TjlCustomerReportPrintInfo, Guid> _TjlCustomerReportPrintInfo;
        private readonly IRepository<TbmBasicDictionary, Guid> _BasicDictionary;
        private readonly IIDNumberAppService _idNumberAppService;
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private readonly ISqlExecutor _sqlExecutor;
        public PrintPreviewAppService(IRepository<TjlClientInfo, Guid> clientInfoRepository
            , IRepository<TjlCustomerReportCollection, Guid> customerReportCollectionRepository
            , IRepository<TjlCustomerReportPrintInfo, Guid> customerReportPrintInfoRepository
            , IRepository<TjlCustomerReg, Guid> customerRegRepository
            , IRepository<TjlCustomer, Guid> customerRepository
            , IRepository<TbmItemSuit, Guid> itemSuitRepository
            , IRepository<TjlCustomerSummarize, Guid> customerSummarizeRepository
            , IRepository<User, long> userRepository, IRepository<TjlClientReg, Guid> clientRegRepository
            , ISqlExecutor sqlExecutor,
            IRepository<TjlOccQuestionnaire, Guid> TjlOccQuestionnaire,
            IRepository<TbmOccDictionary, Guid> TbmOccDictionary,
            IRepository<TjlOccQuesSymptom, Guid> TjlOccQuesSymptom,
            IRepository<TjlCustomerReportPrintInfo, Guid> TjlCustomerReportPrintInfo,
            IRepository<TbmBasicDictionary, Guid> BasicDictionary,
            IIDNumberAppService idNumberAppService)
        {
            _clientInfoRepository = clientInfoRepository;
            _customerReportCollectionRepository = customerReportCollectionRepository;
            _customerReportPrintInfoRepository = customerReportPrintInfoRepository;
            _customerRegRepository = customerRegRepository;
            _customerRepository = customerRepository;
            _itemSuitRepository = itemSuitRepository;
            _customerSummarizeRepository = customerSummarizeRepository;
            _userRepository = userRepository;
            _clientRegRepository = clientRegRepository;
            _sqlExecutor = sqlExecutor;
            _TjlOccQuestionnaire = TjlOccQuestionnaire;
            _TbmOccDictionary = TbmOccDictionary;
            _TjlOccQuesSymptom = TjlOccQuesSymptom;
            _TjlCustomerReportPrintInfo = TjlCustomerReportPrintInfo;
            _BasicDictionary = BasicDictionary;
            _idNumberAppService = idNumberAppService;
        }

        public List<ClientInfoesDto> OldGetClientInfos()
        {
            return _clientInfoRepository.GetAll().MapTo<List<ClientInfoesDto>>();
        }

        public List<CustomerPrintInfoOutput> GetPrintInfo()
        {
            //统计详情
            var details = _customerReportPrintInfoRepository.GetAllIncluding(g => g.CustomerRegBM)
                .GroupBy(g => g.CustomerRegBM.Id)
                .Select(g => new
                {
                    CustomerRegBMId = g.FirstOrDefault().CustomerRegBM.Id,
                    PrintTimes = g.Count(),
                    FirstPrintTime = g.Min(ga => ga.CreationTime),
                    LastPrintTime = g.Max(ga => ga.CreationTime)
                });
            //打印医生
            var printUsers = _customerReportPrintInfoRepository.GetAll().Join(_userRepository.GetAll(),
                    a => a.CreatorUserId, b => b.Id,
                    (a, b) => new { a.CreationTime, CustomerRegBMId = a.CustomerRegBmId, b.Name })
                .OrderBy(a => a.CreationTime);
            //初检医生（第一个打印的医生）
            var firstPrintUser = printUsers.Take(1);
            //终检医生（最后一个打印的医生）
            var lastPrintUser = printUsers.Skip(printUsers.Any() ? printUsers.Count() - 1 : printUsers.Count()).Take(1);
            //信息汇总
            var collections = _customerReportCollectionRepository.GetAll();
            var printInfos = collections.Any()
                ? collections
                    .Join(_customerRegRepository.GetAll(), a => a.CustomerRegId, b => b.Id,
                        (a, b) => new
                        {
                            a.CustomerRegId,
                            a.Receiver,
                            ExaminationDate = a.CreationTime,
                            b.ClientInfoId,
                            b.Id,
                            b.CustomerId,
                            b.SummSate,
                            b.PrintSate,
                            b.ItemSuitBMId
                        })
                    .Join(_customerRepository.GetAll(), b => b.CustomerId, c => c.Id,
                        (b, c) => new
                        { b.Id, b.CustomerRegId, b.ClientInfoId, b.ItemSuitBMId, c.Name, c.Sex, c.IDCardNo })
                    .Join(_clientInfoRepository.GetAll(), c => c.ClientInfoId, d => d.Id,
                        (c, d) => new { c.Id, c.CustomerRegId, c.ItemSuitBMId, d.ClientName })
                    .Join(_itemSuitRepository.GetAll(), d => d.ItemSuitBMId, e => e.Id,
                        (d, e) => new { d.Id, d.CustomerRegId, e.ItemSuitName })
                    .Join(_customerSummarizeRepository.GetAll(), e => e.Id, f => f.CustomerRegID,
                        (e, f) => new { e.CustomerRegId, f.EmployeeBMId, f.ShEmployeeBMId })
                    .Join(details, f => f.CustomerRegId, g => g.CustomerRegBMId,
                        (f, g) => new { f.CustomerRegId, g.PrintTimes, g.FirstPrintTime, g.LastPrintTime })
                    .Join(firstPrintUser, a => a.CustomerRegId, h => h.CustomerRegBMId,
                        (a, h) => new { a.CustomerRegId, FirstPrintUser = h.Name })
                    .Join(lastPrintUser, a => a.CustomerRegId, j => j.CustomerRegBMId,
                        (a, j) => new { a.CustomerRegId, LastPrintUser = j.Name })
                    .MapTo<List<CustomerPrintInfoOutput>>()
                : new List<CustomerPrintInfoOutput>();
            return printInfos;
        }

        public List<ClientInfoOfPrintPreviewDto> GetClientInfos()
        {
            string strsql = "select id,ClientName,HelpCode from TjlClientInfoes   where IsDeleted=0 ";
            SqlParameter[] parameters = {
                        new SqlParameter("@id",new Guid()),
                        };
            List<ClientInfoOfPrintPreviewDto> disageSumDTOs = _sqlExecutor.SqlQuery<ClientInfoOfPrintPreviewDto>
                           (strsql, parameters).ToList();
            return disageSumDTOs;
            //var query = _clientInfoRepository.GetAll().AsNoTracking();
            //var result = query.Select(r => new
            //{
            //    r.Id,
            //    r.ClientName,
            //    r.HelpCode
            //});
            //return result.MapTo<List<ClientInfoOfPrintPreviewDto>>();
        }
         
        public List<ClientRegForPrintPreviewDto> GetClientRegs(EntityDto<Guid> input)
        {
            var query = _clientRegRepository.GetAll();
            query = query.Where(r => r.ClientInfoId == input.Id);
            //var result = query.Select(r => new { r.Id, r.ClientInfo, r.StartCheckDate, r.Remark });
            //result = result.OrderByDescending(r => r.StartCheckDate);
           var result=  query.MapTo<List<ClientRegForPrintPreviewDto>>();
            return result.OrderByDescending(r => r.StartCheckDate).ToList();
        }

        public List<CustomerRegForPrintPreviewDto> GetCustomerRegs(SearchCustomerRegForPrintPreviewDto input)
        {
            var query = _customerRegRepository.GetAllIncluding(r => r.Customer, r => r.ClientInfo).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                //  query = query.Where(o => o.Customer.Name.Contains(input.Name));
                query = query.Where(o => o.Customer.Name.Contains(input.Name) || o.Customer.NameAB.ToUpper().Contains(input.Name.ToUpper()));

            }

            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                query = query.Where(o => o.CustomerBM == input.CustomerBM);
            }

            if (!string.IsNullOrWhiteSpace(input.IdCardNo))
            {
                query = query.Where(r => r.Customer.IDCardNo == input.IdCardNo);
            }
            if (!string.IsNullOrWhiteSpace(input.FPName))
            {
                query = query.Where(r => r.FPNo.Contains(input.FPName));
            }
            if (input.Sex.HasValue && input.Sex.Value != (int)Sex.GenderNotSpecified)
            {
                query = query.Where(o => o.Customer.Sex == input.Sex);
            }
            if (input.ClientId.HasValue)
            {
                query = query.Where(o => o.ClientInfoId == input.ClientId);
            }
            if (input.ClientRegId.HasValue)
            {
                query = query.Where(o => o.ClientRegId == input.ClientRegId);
            }
            if (input.ClientTeamId.HasValue)
            {
                query = query.Where(o => o.ClientTeamInfoId == input.ClientTeamId);

            }
            if (input.SuitId.HasValue)
            {
                query = query.Where(o => o.ItemSuitBMId == input.SuitId);

            }
            if (input.PrintSate.HasValue)
            {
                if (input.ISZY.HasValue && input.ISZY == 1)
                {
                    if (input.PrintSate.Value == (int)PrintSate.Print)
                    {
                        query = query.Where(o => o.OccPrintSate == (int)PrintSate.Print);
                    }
                    else if (input.PrintSate.Value == (int)PrintSate.NotToPrint)
                    {
                        query = query.Where(o => o.OccPrintSate != (int)PrintSate.Print);
                    }
                }
                else
                {
                    if (input.PrintSate.Value == (int)PrintSate.Print)
                    {
                        query = query.Where(o => o.PrintSate == (int)PrintSate.Print);
                    }
                    else if (input.PrintSate.Value == (int)PrintSate.NotToPrint)
                    {
                        query = query.Where(o => o.PrintSate != (int)PrintSate.Print);
                    }
                }
            }

            if (input.CheckSate.HasValue && input.CheckSate != (int)ExaminationState.Whole)
            {
                query = query.Where(o => o.CheckSate == input.CheckSate);
            }
            if (input.CustomerType.HasValue && input.CustomerType != (int)ExaminationState.Whole)
            {
                query = query.Where(o => o.CustomerType == input.CustomerType);
            }
            if (input.PhysicalType.HasValue)
            {
                query = query.Where(o => o.PhysicalType == input.PhysicalType);
            }
            if (input.SummSate.HasValue && input.SummSate != 0)
            {
                query = query.Where(o => o.SummSate == input.SummSate);
            }

            if (input.StartDate.HasValue && input.EndtDate.HasValue)
            {
                var start = input.StartDate.Value.Date;
                var end = input.EndtDate.Value.Date.AddDays(1);
                if (input.DateType.HasValue && input.DateType == 2)
                {

                    var CusIDs = _customerSummarizeRepository.GetAll().Where(o => o.ConclusionDate >= start
                     && o.ConclusionDate < end).Select(o => o.CustomerRegID).ToList();
                    query = query.Where(o => o.SummSate == (int)SummSate.Audited && CusIDs.Contains(o.Id));
                }
                else if (input.DateType.HasValue && input.DateType == 3)
                {
                    query = query.Where(o => o.BookingDate >= start && o.LoginDate < end);
                }
                else if (input.DateType.HasValue && input.DateType == 4)
                {
                    query = query.Where(o => o.PrintTime >= start && o.PrintTime < end);
                }
                else
                {
                    query = query.Where(o => o.LoginDate >= start && o.LoginDate < end);
                }
            }
            if (!string.IsNullOrEmpty(input.CusCabitBM))
            {
                query = query.Where(o => o.CusCabitBM == input.CusCabitBM);
            }
            if (input.StarCabitTime.HasValue && input.EndCabitTime.HasValue)
            {
                var start = input.StarCabitTime.Value.Date;
                var end = input.EndCabitTime.Value.Date.AddDays(1);
                query = query.Where(o => o.CusCabitTime >= start && o.LoginDate < end);
            }
            if (input.CusCabitState == 1)
            {
                query = query.Where(o => o.CusCabitState == 1);
            }
            else if (input.CusCabitState == 0)
            {
                query = query.Where(o => o.CusCabitState != 1 || o.CusCabitState == null);
            }
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            if (input.ReviewSate.HasValue)
            {
                if (input.ReviewSate == 2)
                {
                    query = query.Where(o => o.ReviewSate == input.ReviewSate);
                }
                else if (input.ReviewSate == 1)
                {
                    query = query.Where(o => o.ReviewSate != 2);
                }
            }
            if (input.isYQ.HasValue && input.isYQ == 1)
            {
                var datenow =DateTime.Parse( System.DateTime.Now.ToShortDateString());
                query = query.Where(p => (p.SummSate == 1 || p.SummSate == 2) && p.ReportDate.HasValue &&
                 p.ReportDate < datenow);
            }
            else if (input.isYQ.HasValue && input.isYQ == 2)
            {
                var datenow = DateTime.Parse(System.DateTime.Now.ToShortDateString());
                query = query.Where(p =>  !p.ReportDate.HasValue  || (
                 p.ReportDate >= datenow) || p.SummSate==3 || p.SummSate==4);
            }
            query = query.Where(o=>o.RegisterState== (int)RegisterState.Yes);
            query = query.OrderBy(r => r.LoginDate);
            var Departlist = _BasicDictionary.GetAll().FirstOrDefault(p => p.Type ==
             BasicDictionaryType.PresentationSet.ToString() && p.Value == 400);
            if (Departlist != null && !string.IsNullOrEmpty(Departlist.Remarks))
            {
                var grouplist = Departlist.Remarks.Replace("，", "|").Replace(",", "|").Split('|').ToList();
                return query.Select(p => new CustomerRegForPrintPreviewDto
                {
                    BookingDate = p.BookingDate,
                    CheckSate = p.CheckSate,
                    ClientInfo = p.ClientInfo==null? null: new ClientInfoOfPrintPreviewDto
                    {
                        ClientName = p.ClientInfo.ClientName,
                        HelpCode = p.ClientInfo.HelpCode,
                        Id = p.ClientInfo.Id
                    },
                    CusCabitBM = p.CusCabitBM,
                    CusCabitState = p.CusCabitState,
                    CusCabitTime = p.CusCabitTime,
                    Customer =p.Customer==null?null: new CustomerForPrintPreviewDto {
                     Age= p.Customer.Age,
                      ArchivesNum= p.Customer.ArchivesNum,
                       Birthday=p.Customer.Birthday,
                        Id=p.Customer.Id,
                         IDCardNo=p.Customer.IDCardNo,
                          Mobile=p.Customer.Mobile,
                           Name=p.Customer.Name,
                            Sex=p.Customer.Sex
                    },
                    CustomerBM = p.CustomerBM,
                    ExportSate = p.ExportSate,
                    Id = p.Id,
                    ItemSuitName = p.ItemSuitName,
                    LoginDate = p.LoginDate,
                    PhysicalType = p.PhysicalType,
                    PrintCount = p.PrintCount,
                    PrintSate = p.PrintSate,
                    PrintTime = p.PrintTime,
                    ReportMessageState = p.ReportMessageState,
                    ReviewSate = p.ReviewSate,
                    SummSate = p.SummSate,
                     HaveBreakfast=p.HaveBreakfast,
                      ReceiveSate=p.ReceiveSate
                    //JP = p.CustomerItemGroup.Any(o => o.IsDeleted == false && o.IsAddMinus != 3
                   //  && grouplist.Contains(o.ItemGroupName)) == true ? "是" : "否"

                }).ToList();
            }
            else
            {
                return query.MapTo<List<CustomerRegForPrintPreviewDto>>();
            }
        }

        public void UpdateCustomerRegisterPrintState(ChargeBM input)
        {
            var row = _customerRegRepository.Get(input.Id);
            if (row.PrintCount.HasValue)
            { row.PrintCount = row.PrintCount + 1; }
            else
            { row.PrintCount = 1; }
            row.PrintTime = System.DateTime.Now;
            if (!string.IsNullOrEmpty(input.Name) && input.Name.Contains("职业"))
            {
                row.OccPrintSate = (int)PrintSate.Print;
            }
            else
            {
                row.PrintSate = (int)PrintSate.Print;
            }
            //#region 合格证号
            //var tjh = _BasicDictionary.GetAll().FirstOrDefault(p=>p.Value== row.PhysicalType);
            //if(string.IsNullOrEmpty(row.HGZBH)  &&  tjh != null && (tjh.Text.Contains("从业") || tjh.Text.Contains("健康证")))
            //{
            //    row.HGZBH = _idNumberAppService.CreateJKZBM();


            //}
            //#endregion
            _customerRegRepository.Update(row);
            //增加打印记录
            TjlCustomerReportPrintInfo tjlCustomerReportPrintInfo = new TjlCustomerReportPrintInfo();
            tjlCustomerReportPrintInfo.Id = Guid.NewGuid();
            tjlCustomerReportPrintInfo.CauseRepair = "";
            tjlCustomerReportPrintInfo.CustomerRegBmId = row.Id;
            tjlCustomerReportPrintInfo.EmployeeBmId = AbpSession.UserId;
            _TjlCustomerReportPrintInfo.Insert(tjlCustomerReportPrintInfo);
        }

        public ChargeBM UpdateCustomerRegisterHGZState(EntityDto<Guid> input)
        {
            var row = _customerRegRepository.Get(input.Id);
            #region 合格证号
            var tjh = _BasicDictionary.GetAll().FirstOrDefault(p =>  p.Type == "ExaminationType" && 
            p.Value == row.PhysicalType);
            if (string.IsNullOrEmpty(row.HGZBH) && tjh != null && (tjh.Text.Contains("从业") || tjh.Text.Contains("健康证")))
            {
                row.HGZBH = _idNumberAppService.CreatHGZBM();
            }
            #endregion
            _customerRegRepository.Update(row);
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name= row.HGZBH;
            return chargeBM;
        }
        /// <summary>
        /// 修改打印通知单状态
        /// </summary>
        /// <param name="input"></param>
        public void UpdateNoticePrintState(EntityDto<Guid> input)
        {
            var row = _customerRegRepository.Get(input.Id);             
            row.TZDPrintSate = (int)PrintSate.Print;
            _customerRegRepository.Update(row);
            //增加打印记录
            TjlCustomerReportPrintInfo tjlCustomerReportPrintInfo = new TjlCustomerReportPrintInfo();
            tjlCustomerReportPrintInfo.Id = Guid.NewGuid();
            tjlCustomerReportPrintInfo.CauseRepair = "";
            tjlCustomerReportPrintInfo.ReportType = 2;
            tjlCustomerReportPrintInfo.CustomerRegBmId = row.Id;
            tjlCustomerReportPrintInfo.EmployeeBmId = AbpSession.UserId;
            _TjlCustomerReportPrintInfo.Insert(tjlCustomerReportPrintInfo);
        }
        public void UpdateCustomerSumPrintState(ChargeBM input)
        {
            var row = _customerRegRepository.Get(input.Id);
            if (input.Name.Contains("职业"))
            { row.occExportSate = (int)ExportSate.Export; }
            else
            {
                row.ExportSate = (int)ExportSate.Export;
            }
            _customerRegRepository.Update(row);
        }
        /// <summary>
        /// 根据预约Id获取问卷相关
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ReportOccQuesDto getOccQue(EntityDto<Guid> input)
        {
            ReportOccQuesDto outdto = new ReportOccQuesDto();
              var que = _TjlOccQuestionnaire.FirstOrDefault(o => o.CustomerRegBMId == input.Id);
            if (que != null)
            {
                
                
                outdto.ReportOccQueAll = que.MapTo<ReportOccQueAllDto>();
                outdto.OccCareerHistory = que.OccCareerHistory.Select(o => new ReportQuesCareerHistoryDto
                {
                    CustomerRegBMId = o.CustomerRegBMId,
                    EndTime = o.EndTime,
                    HisHazards = string.Join("、", o.OccHisHazardFactors.Select(r => r.Text).ToList()).TrimEnd('、'),
                    HisProtectives = string.Join("、", o.OccHisProtectives.Select(r => r.Text).ToList()).TrimEnd('、'),
                    StarTime = o.StarTime,
                    WorkClient = o.WorkClient,
                    WorkName = o.WorkName,
                    WorkType = o.WorkType,
                    WorkYears = o.WorkYears,
                    UnitAge = o.UnitAge,
                     StrWorkYears=o.StrWorkYears
                }).ToList();
                outdto.OccRadioactiveCareerHistory = que.RadioactiveCareerHistory.MapTo<List<OccReportQuesRadioactiveCareerHistoryDto>>();
                outdto.OccQuesMerriyHistory= que.OccQuesMerriyHistory.MapTo<List<OccReportQuesMerriyHistoryDto>>();
                outdto.OccFamilyHistory= que.OccFamilyHistory.MapTo<List<ReportOccQuesFamilyHistoryDto>>();
                outdto.OccPastHistory=que.OccPastHistory.MapTo<List<ReportOccQuesPastHistoryDto>>();
                outdto.OccQuesSymptom = que.OccQuesSymptom.MapTo<List<ReportOccQuesSymNameDto>>();
                return outdto;
            }
            else
            return outdto;

        }
        /// <summary>
        /// 获取症状
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ReportOccQuesSymptomDto>getOccQuesSymptoms(EntityDto<Guid> input)
        {
            List<ReportOccQuesSymptomDto> outresult = new List<ReportOccQuesSymptomDto>();
              var zgls = _TbmOccDictionary.GetAll().Where(o => o.Type == ZYBBasicDictionaryType.Symptom.ToString()).OrderBy(o=>o.OrderNum).ToList();
            var cuszgls = _TjlOccQuesSymptom.GetAll().Where(o=>o.CustomerRegBMId== input.Id).ToList();
            var que = _TjlOccQuestionnaire.GetAll().Where(o => o.CustomerRegBMId == input.Id).ToList();
            var UserName = "";
            if (que.Count > 0)
            {
                UserName = _userRepository.Get(que.First().CreatorUserId.Value).Name;
            }
            if (zgls != null && zgls.Count > 0 )
            {
                foreach (var zg in zgls)
                {
                    ReportOccQuesSymptomDto reportOccQuesSymptomDto = new ReportOccQuesSymptomDto();
                    reportOccQuesSymptomDto.BM = zg.OrderNum.ToString();
                    reportOccQuesSymptomDto.Name = zg.Text;
                    var cuszg = cuszgls.FirstOrDefault(o=>o.Name== zg.Text);
                    if (cuszg != null)
                    {
                        reportOccQuesSymptomDto.Degree = cuszg.Degree;
                        reportOccQuesSymptomDto.StarTime = cuszg.StarTime;
                    }
                    else
                    {
                        reportOccQuesSymptomDto.Degree = "-";
                    }
                    reportOccQuesSymptomDto.QuesUser = UserName;
                  
                    outresult.Add(reportOccQuesSymptomDto);
                }
                
            }
            return outresult;

        }


    }
}