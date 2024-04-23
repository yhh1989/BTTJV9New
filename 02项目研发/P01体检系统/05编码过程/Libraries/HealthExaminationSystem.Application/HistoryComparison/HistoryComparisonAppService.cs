using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExamination.Drivers;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison
{
    public class HistoryComparisonAppService : MyProjectAppServiceBase, IHistoryComparisonAppService
    {
        private readonly IRepository<TjlCustomerReg, Guid> _basicCustomerRegRepository; //体检信息
        private readonly IRepository<TjlCustomerRegItem, Guid> _basicCustomerRegItemRepository; //体检信息
        private readonly IRepository<TbmBasicDictionary, Guid> _BasicDictionaryy;
        private readonly IRepository<TbmItemInfo, Guid> _TbmItemInfo;
        private readonly IRepository<TjlCustomerDepSummary, Guid> _TjlCustomerDepSummary;
        private readonly IRepository<TjlCustomerSummarize, Guid> _TjlCustomerSummarize;

        public HistoryComparisonAppService
            (
                     IRepository<TjlCustomerReg, Guid> basicCustomerRegRepository,
                     IRepository<TjlCustomerRegItem, Guid> basicCustomerRegItemRepository,
                     IRepository<TbmBasicDictionary, Guid> BasicDictionaryy,
                     IRepository<TbmItemInfo, Guid> TbmItemInfo,
                     IRepository<TjlCustomerItemGroup, Guid> TjlCustomerItemGroup,
                     IRepository<TjlCustomerDepSummary, Guid> TjlCustomerDepSummary,
                     IRepository<TjlCustomerSummarize, Guid> TjlCustomerSummarize
            )
        {
            _basicCustomerRegRepository = basicCustomerRegRepository;
            _basicCustomerRegItemRepository = basicCustomerRegItemRepository;
            _BasicDictionaryy = BasicDictionaryy;
            _TbmItemInfo = TbmItemInfo;
            _TjlCustomerDepSummary = TjlCustomerDepSummary;
            _TjlCustomerSummarize = TjlCustomerSummarize;
        }
        /// <summary>
        /// 查询体检人登记表（综合查询）
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public List<SearchCustomerRegDto> GetCustomerRegList(SearchClass Search)
        {
            var CustomerReg = _basicCustomerRegRepository.GetAll().Where(o => o.CustomerId == Search.CustomerId);
            //var a = CustomerReg.MapTo<List<SearchCustomerRegDto>>();
            return CustomerReg.MapTo<List<SearchCustomerRegDto>>();
        }
        /// <summary>
        /// 查询体检人登记表（综合查询） 
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        /// <returns></returns>
        public List<HistoryResultMainDto> GetHistoryResultList(SearchClass Search)
        {
            var CustomerRegItem = _basicCustomerRegItemRepository.GetAll().Where(o => o.CustomerRegBM.CustomerId == Search.CustomerId);
            if (Search.DepartId.HasValue)
            {
                CustomerRegItem = CustomerRegItem.Where(p=>p.DepartmentId== Search.DepartId);
            }
            if (Search.GroupId.HasValue)
            {
                CustomerRegItem = CustomerRegItem.Where(p => p.ItemGroupBMId == Search.GroupId);
            }
            if (Search.ItemId.HasValue)
            { CustomerRegItem = CustomerRegItem.Where(p => p.ItemId == Search.ItemId); }
            var outHisItem = CustomerRegItem.Select(p => new HistoryResultDto {
             CheckDate=p.CustomerRegBM.LoginDate,
             CustomerBM=p.CustomerRegBM.CustomerBM,
             DepartId=p.DepartmentId,
             DepartName=p.DepartmentBM.Name,
             GroupId=p.ItemGroupBMId,
             GroupName=p.ItemGroupBM.ItemGroupName,
             ItemId=p.ItemId,
             ItemName=p.ItemName,
             ItemValue=p.ItemResultChar,
             Stand=p.Stand,
             Symbol=p.Symbol,
             DepartOrder=p.DepartmentBM.OrderNum,
             GroupOrder=p.ItemGroupBM.OrderNum,
             ItemOrder=p.ItemBM.OrderNum,

                ItemBM = p.ItemBM.ItemBM}).OrderByDescending(p=>p.CheckDate).ToList();

            var Itemresult = outHisItem.GroupBy(p => new { p.GroupName, p.DepartName, p.ItemName }).Select(p => new HistoryResultMainDto
            {
                DepartName = p.Key.DepartName,
                GroupName = p.Key.GroupName,
                ItemName = p.Key.ItemName,
                Stand = p.First().Stand,
                 DepartOrder=p.First().DepartOrder,
                  GroupOrder=p.First().GroupOrder,
                   ItemOrder=p.First().ItemOrder,
                    ItemBM=p.First().ItemBM,
                historyResultDetailDtos = p.Select(n => new HistoryResultDetailDto
                {
                    CheckDate = n.CheckDate,
                    CustomerBM = n.CustomerBM,
                    ItemValue = n.ItemValue,
                    Symbol = n.Symbol
                }).ToList()


            }).OrderBy(p=>p.DepartOrder).ThenBy(p=>p.GroupOrder).ToList();
           
            return Itemresult;
        }
        /// <summary>
        /// 获历史结果数据 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<OutHisValuesDto> getHisValue(InSerchHIsDto input)
        {
            List<OutHisValuesDto> outHisValuesDtos = new List<OutHisValuesDto>();
            //var itemList = _TbmItemInfo.GetAll().Where(p=> input.ItemIds.Contains(p.Id)).ToList();
            //先找本地库
            var cusItemValues = _basicCustomerRegItemRepository.GetAll().Where(
                p=>p.CustomerRegBM.CustomerId==input.CustomerId && p.CustomerRegId!= input.CustomerRegId 
                && 
                input.ItemIds.Contains(p.ItemId) && p.ProcessState== (int)ProjectIState.Complete 
                && p.ItemResultChar!=""  && p.ItemResultChar != null).OrderByDescending(p=>p.CustomerRegBM.LoginDate).ToList();
            List<Guid> NoItemIds = new List<Guid>();
            NoItemIds = input.ItemIds.ToList();
            if (cusItemValues.Count > 0)
            {
                foreach (var guid in input.ItemIds)
                {
                    var cusITem = cusItemValues.FirstOrDefault(p => p.ItemId == guid);
                    if (cusITem != null)
                    {
                        OutHisValuesDto outHis = new OutHisValuesDto();
                        outHis.ItemId = guid;
                        outHis.ItemValue = cusITem.ItemResultChar;
                        outHisValuesDtos.Add(outHis);
                        NoItemIds.Remove(guid);
                    }
                    

                }
            }
            //开启获取第三方库
            var oderHisITem = _BasicDictionaryy.GetAll().FirstOrDefault(p => p.Type ==
                 BasicDictionaryType.PresentationSet.ToString() && p.Value == 102);
            if (oderHisITem != null && oderHisITem.Remarks == "Y")
            {
                //本地库没匹配到的去历史库找
                if (NoItemIds.Count > 0 && !string.IsNullOrEmpty(input.IDCardNo))
                {

                    // 获取接口数据
                    var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();
                    Sw.Hospital.HealthExamination.Drivers.Models.HisInterface.SearchHisClassDto
                        search = new HealthExamination.Drivers.Models.HisInterface.SearchHisClassDto();
                    search.IDCardNo = input.IDCardNo;
                    var interfaceResult = hisInterfaceDriver.GetHistoryResult(search).OrderByDescending(
                        p => p.CheckDate).ToList();

                    foreach (var guid in NoItemIds)
                    {
                        var iteminfo = _TbmItemInfo.Get(guid);
                        var cusITem = interfaceResult.FirstOrDefault(p => p.ItemBM == iteminfo.ItemBM);
                        if (cusITem != null)
                        {
                            OutHisValuesDto outHis = new OutHisValuesDto();
                            outHis.ItemId = guid;
                            outHis.ItemValue = cusITem.ItemValue;
                            outHisValuesDtos.Add(outHis);
                        }

                    }

                }

            }
            return outHisValuesDtos;
        }

        /// <summary>
        /// 获取第三方历史数据 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<HistoryResultMainDto> geHisvard(SearchHisClassDto input)
        {
            // 获取接口数据
            var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

            var search = input.MapTo<Sw.Hospital.HealthExamination.Drivers.Models.HisInterface.SearchHisClassDto>();
            var interfaceResult = hisInterfaceDriver.GetHistoryResult(search);

            var Itemresult = interfaceResult.GroupBy(p => new { p.GroupName, p.DepartName, p.ItemName }).Select(p => new HistoryResultMainDto
            {
                DepartName = p.Key.DepartName,
                GroupName = p.Key.GroupName,
                ItemName = p.Key.ItemName,
                Stand = p.First().Stand,
                DepartOrder = p.First().DepartOrder,
                GroupOrder = p.First().GroupOrder,
                ItemOrder = p.First().ItemOrder,
                 ItemBM = p.First().ItemBM,
                historyResultDetailDtos = p.Select(n => new HistoryResultDetailDto
                {
                    CheckDate = n.CheckDate,
                    CustomerBM = n.CustomerBM,
                    ItemValue = n.ItemValue,
                    Symbol = n.Symbol
                }).OrderBy(r=>r.CheckDate).ToList()


            }).OrderBy(p => p.DepartOrder).ThenBy(p => p.GroupOrder).ToList();

            return Itemresult;
        }

        /// <summary>
        /// 获取第三方历史数据个人报告
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public  HisDbDto geHisvardReport(SearchHisClassDto input)
        {
            HisDbDto hisDbDto = new HisDbDto();
            List<HistoryItemValueDto> historyResultDtos = new List<HistoryItemValueDto>();

            var cusreg = _basicCustomerRegRepository.Get(input.CustomerRegId.Value);

            //获取三次对比去掉补检和复查
            var HistcusRegBMlist = _basicCustomerRegRepository.GetAll().Where(p => p.Customer.IDCardNo ==
             input.IDCardNo && p.LoginDate!=null &&
             p.LoginDate < cusreg.LoginDate && p.CustomerBM != cusreg.CustomerBM
             && p.SummSate == (int)SummSate.Audited && p.ReviewSate !=2 && p.Remarks!="补检").OrderByDescending(p=>p.LoginDate).
             Take(2).Select(p=>p.CustomerBM).ToList();
            //获取第二次体检号
            var sencondBM = "";
            if (HistcusRegBMlist.Count > 0)
            {
                sencondBM = HistcusRegBMlist.First();
            }           
            //获取三次体检号
            List<string> RegID = new List<string>();
            RegID.Add(cusreg.CustomerBM);
            RegID.AddRange(HistcusRegBMlist);

            
            //获取三次体检结果
            historyResultDtos = _basicCustomerRegItemRepository.GetAll().Where(p =>
              RegID.Contains(p.CustomerRegBM.CustomerBM) 
              && p.ProcessState == (int)ProjectIState.Complete ).
            Select(p => new HistoryItemValueDto
            {
                CheckDate = p.CustomerRegBM.LoginDate,
                CustomerBM = p.CustomerRegBM.CustomerBM,
                DepartName = p.DepartmentBM.Name,
                DepartOrder = p.DepartmentBM.OrderNum,
                GroupName = p.ItemGroupBM.ItemGroupName,
                GroupOrder = p.ItemGroupBM.OrderNum,
                ItemBM = p.ItemBM.ItemBM,
                ItemName = p.ItemName,
                ItemOrder = p.ItemOrder,
                ItemValue = p.ItemBM.moneyType==3?( p.ItemDiagnosis!= null ? (p.ItemDiagnosis != "" ? p.ItemDiagnosis: p.ItemResultChar) : p.ItemResultChar):p.ItemResultChar,
                Stand = p.Stand,
                Symbol = p.Symbol,
                 CustomerRegId=p.CustomerRegId
            }
                ).ToList();

            //获取本次体检项目
            var bcItems = historyResultDtos.Where(p => p.CustomerBM == cusreg.CustomerBM).Select(p=>p.ItemName).ToList();
            var bcItemBMs = historyResultDtos.Where(p => p.CustomerBM == cusreg.CustomerBM).Select(p => p.ItemBM).ToList();

            List<HisDepartSumDto> OldhisDepartSumDto = new List<HisDepartSumDto>();
            List<HisSumDto> OldhisSumDto = new List<HisSumDto>();


            #region 获取历史库
            //开启获取第三方库
            var oderHisITem = _BasicDictionaryy.GetAll().FirstOrDefault(p => p.Type ==
                 BasicDictionaryType.PresentationSet.ToString() && p.Value == 102);
            if (oderHisITem != null && oderHisITem.Remarks == "Y" &&  
                !string.IsNullOrEmpty(input.IDCardNo))
            {
                //如果本库小于3次体检则在历史库获取对比数据
                if (RegID.Count <= 2)
                {
                    int num = 3 - RegID.Count;

                    // 获取接口数据
                    var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

                    var search = input.MapTo<Sw.Hospital.HealthExamination.Drivers.Models.HisInterface.SearchHisClassDto>();

                    var interfaceResult = hisInterfaceDriver.GetHistoryResult(search);
                    var chetimelsit = interfaceResult.Select(p => p.CheckDate).Distinct().OrderByDescending(
                        p => p.Value).Take(num).ToList();
                    var Itemresult = interfaceResult.Select(p => new HistoryItemValueDto
                    {
                        CheckDate = p.CheckDate,
                        CustomerBM = p.CustomerBM,
                        DepartName = p.DepartName,
                        DepartOrder = p.DepartOrder,
                        GroupName = p.GroupName,
                        GroupOrder = p.GroupOrder,
                        ItemBM = p.ItemBM,
                        ItemName = p.ItemName,
                        ItemOrder = p.ItemOrder,
                        ItemValue = p.ItemValue,
                        Stand = p.Stand,
                        Symbol = p.Symbol
                    }).Where(p => chetimelsit.Contains(p.CheckDate)).
                    OrderByDescending(p => p.CheckDate).ToList();

                    if (Itemresult.Count > 0)
                    {
                        //加上历史数据
                       
                        //historyResultDtos.AddRange(Itemresult);
                        #region 处理项目名称和历史库不一致的问题
                        foreach (var oldcusjg in Itemresult)
                        {
                            if (bcItems.Contains(oldcusjg.ItemName) || bcItemBMs.Contains(oldcusjg.ItemBM))
                            {
                                var bc=historyResultDtos.FirstOrDefault(p => p.ItemBM == oldcusjg.ItemBM
                                 || p.ItemName == oldcusjg.ItemName);
                                if (bc != null)
                                {
                                    oldcusjg.ItemName = bc.ItemName;
                                    oldcusjg.DepartBM = bc.DepartBM;
                                    oldcusjg.DepartName = bc.DepartName;
                                    oldcusjg.GroupName = bc.GroupName;
                                    oldcusjg.GroupOrder = bc.GroupOrder;
                                    oldcusjg.ItemOrder = bc.ItemOrder;
                                    historyResultDtos.Add(oldcusjg);
                                }
                            }

                        }
                        #endregion
                        if (sencondBM == "")
                        {
                            sencondBM = Itemresult.First().CustomerBM;
                        }
                        //科室小结
                        OldhisDepartSumDto = interfaceResult.
                            Where(p => chetimelsit.Contains(p.CheckDate)).GroupBy(
                            p=> new  { p.CustomerBM,p.DepartName}).
                            Select(p => new
                        HisDepartSumDto
                        {
                            CheckDate = p.FirstOrDefault().CheckDate,
                            CustomerBM = p.Key.CustomerBM,
                            DepartBM = p.FirstOrDefault().DepartBM,
                            DepartName = p.Key.DepartName,
                            DepartOrder = p.FirstOrDefault().DepartOrder,
                            DepartSum = p.FirstOrDefault().DepartSum
                        }
                        ).Distinct().ToList();

                        //总检结论
                        OldhisSumDto = interfaceResult.Where
                            (p => chetimelsit.Contains(p.CheckDate)).GroupBy(
                            p=>p.CustomerBM).
                            Select(p => new
                       HisSumDto
                        {
                            CheckDate = p.FirstOrDefault().CheckDate,
                            CustomerBM = p.Key,
                            Sum = p.FirstOrDefault().Sum,
                             ChekDateYear=p.FirstOrDefault().CheckDate.ToString(),
                              ChekDateYear1 = p.FirstOrDefault().CheckDate,
                               diagnosis= p.FirstOrDefault().Sum,
                        }
                       ).Distinct().ToList();
                    }

                }
            }

            #endregion
            //如果只有一次直接返回
            var dbcs = historyResultDtos.Select(p => p.CustomerBM).Distinct().Count();
            if (dbcs <= 1)
            {
                return new HisDbDto();
            }
          
            #region 两次数值结果趋势对比
            //获取两次项目结果对比
            if (sencondBM != "")
            {
                //需要图标对比的科室
                var HisITem = _BasicDictionaryy.GetAll().FirstOrDefault(p => p.Type ==
                 BasicDictionaryType.PresentationSet.ToString() && p.Value == 101);
                if (HisITem != null && !string.IsNullOrEmpty(HisITem.Remarks))
                {

                    var CharItem = HisITem.Remarks.Split(',').ToList();

                    var charitems = historyResultDtos.Where(p => CharItem.Contains(p.ItemName)).ToList();
                    hisDbDto.HistoryItemChar = charitems.OrderBy(p => p.ItemBM).ThenBy(p => p.CheckDate).ToList();
                }
                //本次项目结果
                var cusItem = _basicCustomerRegItemRepository.GetAll().Where(p => p.CustomerRegId ==
           input.CustomerRegId.Value && p.ProcessState == (int)ProjectIState.Complete
           && p.ItemBM.moneyType != (int)ItemType.Explain).Select(p => new HistoryItemValueDto
           {
               CheckDate = p.CustomerRegBM.LoginDate,
               CustomerBM = p.CustomerRegBM.CustomerBM,
               DepartName = p.DepartmentBM.Name,
               DepartOrder = p.DepartmentBM.OrderNum,
               GroupName = p.ItemGroupBM.ItemGroupName,
               GroupOrder = p.ItemGroupBM.OrderNum,
               ItemBM = p.ItemBM.ItemBM,
               ItemName = p.ItemName,
               ItemOrder = p.ItemOrder,
               ItemValue = p.ItemResultChar,
               Stand = p.Stand,
               Symbol = p.Symbol,
               CustomerRegId = p.CustomerRegId
           }).OrderBy(p => p.DepartOrder).ThenBy(p => p.GroupOrder).
                    ThenBy(p => p.ItemOrder).ToList();
                List<HistoryItemDBDto> HisItems = new List<HistoryItemDBDto>();
                if (cusItem != null && cusItem.Count > 0)
                {
                    var cusBm = cusItem.FirstOrDefault().CustomerBM;
                    var Date1 = historyResultDtos.Where(p =>
                    p.CustomerBM == sencondBM).FirstOrDefault()?.CheckDate;

                    foreach (var item in cusItem)
                    {
                        HistoryItemDBDto historyItemValueDto = new HistoryItemDBDto();
                        historyItemValueDto = item.MapTo<HistoryItemDBDto>();
                        var oldItem = historyResultDtos.FirstOrDefault(p => p.CustomerBM ==
                          sencondBM && p.ItemBM == item.ItemBM);
                        historyItemValueDto.CheckDate1 = Date1;
                        if (oldItem != null)
                        {
                            historyItemValueDto.ItemValue1 = oldItem.ItemValue;
                            historyItemValueDto.CheckDate1 = oldItem.CheckDate;
                            historyItemValueDto.Symbol1 = oldItem.Symbol;
                            if (decimal.TryParse(historyItemValueDto.ItemValue1, out decimal v1)
                                && decimal.TryParse(historyItemValueDto.ItemValue, out decimal v2))
                            {
                                if (v2 > v1)
                                {
                                    historyItemValueDto.qs = "↑";
                                }
                                else if (v2 < v1)
                                {
                                    historyItemValueDto.qs = "↓";
                                }
                                else
                                { historyItemValueDto.qs = "--"; }
                            }
                            else
                            {
                                historyItemValueDto.qs = "";
                            }

                        }
                        HisItems.Add(historyItemValueDto);
                    }
                    hisDbDto.HistoryItem = HisItems.ToList();
                }
            }
            #endregion

            #region 3次项目对比
            //3次项目对比和本次体检项目对比 
            var cusITems = historyResultDtos.GroupBy(p => p.ItemName).Select(
                p => new {itemName= p.Key, sl = p.Count() });
            var cusItemIDs = cusITems.Where(p => p.sl > 1).Select(p => p.itemName).Distinct().ToList();
            hisDbDto.HistoryItemDb = historyResultDtos.Where(p=> cusItemIDs.Contains(p.ItemName)).Where(p =>
            bcItemBMs.Contains(p.ItemBM) ||
            bcItems.Contains(p.ItemName)).OrderBy(p => p.DepartOrder).ThenBy(p => p.DepartName).ThenBy(p => p.GroupOrder).ThenBy(p => p.GroupName).ThenBy
                (p => p.ItemOrder).ThenBy(p => p.ItemName).ThenBy(p => p.CheckDate).ToList();

            #endregion
            #region 3次科室小结对应
            //获取三次科室小节对比
            var HisCusDepartSum = _TjlCustomerDepSummary.GetAll().Where(p => RegID.Contains(p.CustomerReg.CustomerBM) && p.CharacterSummary != ""
            && p.CharacterSummary != null).Select(p => new HisDepartSumDto
            {
                regId = p.CustomerReg.Id,
                CheckDate = p.CustomerReg.LoginDate,
                CustomerBM = p.CustomerReg.CustomerBM,
                DepartName = p.DepartmentName,
                DepartOrder = p.DepartmentOrder,
                DepartSum = p.DagnosisSummary == "" ? p.CharacterSummary : p.DagnosisSummary == null ? p.CharacterSummary : p.DagnosisSummary
            }).OrderBy(p => p.DepartName).ThenBy(p => p.CheckDate).ToList();
            if (OldhisDepartSumDto.Count > 0)
            {
                HisCusDepartSum.AddRange(OldhisDepartSumDto);
            }
            hisDbDto.HisDepartSum = HisCusDepartSum.OrderBy(p => p.DepartName).ThenBy(p => p.CheckDate).ToList();

            #endregion
            //获取三次总检对比
            #region 三次总检对比
            var HisCusSum = _TjlCustomerSummarize.GetAll().Where(p => RegID.Contains(p.CustomerReg.CustomerBM) && p.CharacterSummary != ""
            && p.CharacterSummary != null).Select(p => new HisSumDto
            {
              
                CheckDate = p.CustomerReg.LoginDate,
                CustomerBM = p.CustomerReg.CustomerBM,
                 Sum=p.CharacterSummary ,
                  ChekDateYear = p.CustomerReg.LoginDate.ToString(),
                   ChekDateYear1= p.CustomerReg.LoginDate,
                    diagnosis= p.CharacterSummary

            }).OrderBy(p => p.CheckDate).ToList();
            if (OldhisSumDto.Count > 0)
            {
                HisCusSum.AddRange(OldhisSumDto);
            }
            hisDbDto.HisSum = HisCusSum.OrderBy(p => p.CheckDate).ToList();

           #endregion
            return hisDbDto;
        }
    }
}
