using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrint
{
    [AbpAuthorize]
    public class BarPrintAppService : MyProjectAppServiceBase, IBarPrintAppService
    {

        #region 申明变量

        private readonly IRepository<TjlCustomerBarPrintInfo, Guid> _customerBarPrintInfoRepository;
        private readonly IRepository<TjlCustomerBarPrintInfoItemGroup, Guid> _CustomerBarPrintInfoItemGroup;

        private readonly IRepository<TbmBarSettings, Guid> _barSettingsRepository;
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository;

        private readonly IRepository<TjlCustomerItemGroup, Guid> _TjlCustomerItemGroup;
        private readonly IIDNumberAppService _idNumberAppService;
        private readonly ICommonAppService _commonAppService;

        private readonly IRepository<TbmBasicDictionary, Guid> _BasicDictionaryRepository;
        #endregion 申明变量
        #region 构造函数

        public BarPrintAppService(IRepository<TjlCustomerBarPrintInfo, Guid> customerBarPrintInfoRepository,
            IRepository<TbmBarSettings, Guid> barSettingsRepository, IRepository<TjlCustomerReg, Guid> CustomerRegRepository,
            IRepository<TjlCustomerBarPrintInfoItemGroup, Guid> CustomerBarPrintInfoItemGroup,
              IIDNumberAppService idNumberAppService, IRepository<TjlCustomerItemGroup, Guid> TjlCustomerItemGroup,
               ICommonAppService commonAppService,
               IRepository<TbmBasicDictionary, Guid> BasicDictionaryRepository)
        {
            _customerBarPrintInfoRepository = customerBarPrintInfoRepository;
            _barSettingsRepository = barSettingsRepository;
            _customerRegRepository = CustomerRegRepository;
            _CustomerBarPrintInfoItemGroup = CustomerBarPrintInfoItemGroup;
            _idNumberAppService = idNumberAppService;
            _TjlCustomerItemGroup = TjlCustomerItemGroup;
            _commonAppService = commonAppService;
            _BasicDictionaryRepository = BasicDictionaryRepository;
        }

        #endregion 构造函数

        /// <summary>
        /// 增加
        /// </summary>
        public CustomerBarPrintInfoDto AddBarPrintApp(CreateCustomerBarPrintInfoDto CustomerBarPrintInfoDto)
        {
            var entity = CustomerBarPrintInfoDto.MapTo<TjlCustomerBarPrintInfo>();
            //entity.Id = Guid.NewGuid();
            entity.BarSettings = _barSettingsRepository.Get(CustomerBarPrintInfoDto.BarSettingsId);
            entity.CustomerReg = _customerRegRepository.Get(CustomerBarPrintInfoDto.custtomerregid);
            var entityResult = _customerBarPrintInfoRepository.Insert(entity);
            var dto = entityResult.MapTo<CustomerBarPrintInfoDto>();
            return dto;
        }

        /// <summary>
        /// 批量增加
        /// </summary>
        public void AddBarPrintApp(List<CreateCustomerBarPrintInfoDto> CustomerBarPrintInfoDto)
        {
            foreach (var item in CustomerBarPrintInfoDto)
            {
                var entity = item.MapTo<TjlCustomerBarPrintInfo>();
                //entity.Id = Guid.NewGuid();
                entity.BarSettings = _barSettingsRepository.Get(item.BarSettingsId);
                entity.CustomerReg = _customerRegRepository.Get(item.custtomerregid);
                var entityResult = _customerBarPrintInfoRepository.Insert(entity);
                var dto = entityResult.MapTo<CustomerBarPrintInfoDto>();
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = entity.CustomerReg.CustomerBM;
                createOpLogDto.LogName = entity.CustomerReg.Customer.Name;
                createOpLogDto.LogText = "打印条码,第(" + entity.BarPrintCount + ")次打印" + entity.BarNumBM + "";
                createOpLogDto.LogDetail = entity.BarName;
                createOpLogDto.LogType = (int)LogsTypes.PrintId;
                _commonAppService.SaveOpLog(createOpLogDto);
            }
        }

        public bool DeleteBarPrintApp(CustomerBarPrintInfoDto CustomerBarPrintInfoDto)
        {
            var pa = _customerBarPrintInfoRepository.FirstOrDefault(o => o.Id == CustomerBarPrintInfoDto.Id);
            _customerBarPrintInfoRepository.Delete(pa);
            return true;
        }



        /// <summary>
        /// 查询已打印条码
        /// </summary>
        [UnitOfWork(isTransactional: false)]
        public List<CustomerBarPrintInfoDto> GetLstBarPrintApp(CusNameInput cusNameInput)
        {
            var query1 = _customerRegRepository.Get(cusNameInput.Id);
            var result = query1.CustomerBarPrintInfo.ToList();
            //result.Select(r => r.BarSettings).ToList();
            return result.MapTo<List<CustomerBarPrintInfoDto>>();

            //var query = _customerBarPrintInfoRepository.GetAllIncluding(r => r.BarSettings);
            //query = query.Where(p => p.CustomerReg_Id == cusNameInput.Id);
            //return query.MapTo<List<CustomerBarPrintInfoDto>>();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="CustomerBarPrintInfoDto"></param>
        /// <returns></returns>
        public bool UpdateBarPrintApp(CustomerBarPrintInfoDto CustomerBarPrintInfoDto)
        {
            var entity = _customerBarPrintInfoRepository.Get(CustomerBarPrintInfoDto.Id);
            CustomerBarPrintInfoDto.MapTo(entity); // 赋值
            entity = _customerBarPrintInfoRepository.Update(entity);
            var dto = entity.MapTo<CustomerBarPrintInfoDto>();
            return true;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="CustomerBarPrintInfoDto"></param>
        /// <returns></returns>
        public bool UpdateBarPrintApp(List<CustomerBarPrintInfoDto> CustomerBarPrintInfoDto)
        {
            foreach (var item in CustomerBarPrintInfoDto)
            {
                var entity = _customerBarPrintInfoRepository.Get(item.Id);
                item.MapTo(entity); // 赋值
                entity = _customerBarPrintInfoRepository.Update(entity);
                var dto = entity.MapTo<CustomerBarPrintInfoDto>();
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = entity.CustomerReg.CustomerBM;
                createOpLogDto.LogName = entity.CustomerReg.Customer.Name;
                createOpLogDto.LogText = "打印条码,第("+ entity.BarPrintCount + ")次打印"+ entity.BarNumBM+"";
                createOpLogDto.LogDetail = entity.BarName;
                createOpLogDto.LogType = (int)LogsTypes.PrintId;
                _commonAppService.SaveOpLog(createOpLogDto);
               
            }
            return true;
        }


        /// <summary>
        /// 查询所有打印条码
        /// </summary>
        [UnitOfWork(isTransactional: false)]
        public ReportJsonDto GetBarPrint(CusNameInput cusNameInput)
        {
            var result = new List<TjlCustomerBarPrintInfo>();
            var reportJsonForExamine = new ReportJsonDto();
            reportJsonForExamine.Detail = new List<DetailDto>();
            //foreach (var cusNameInput in cusNameInputls)
            //{
                var querycus = _customerRegRepository.Get(cusNameInput.Id);
            //现有组合
            var que = querycus.CustomerItemGroup.Where(p => p.IsAddMinus != (int)AddMinusType.Minus);
            #region 放弃是否显示控制
            var fqxs = _BasicDictionaryRepository.GetAll().FirstOrDefault(p => p.Type == "BarPrintSet"
            && p.Value == 77)?.Remarks;
            if (!string.IsNullOrEmpty(fqxs) && fqxs == "1")
            {
                que = que.Where(r => r.CheckState != (int)ProjectIState.GiveUp).ToList();
            }
            #endregion
            var cusgroups = que.Select(o=>o.ItemGroupBM_Id).ToList();
            
            List<Guid> barprintId = new List<Guid>();
               //已打条码
                if (querycus.CustomerItemGroup.Where(o => o.BarState == 2).ToList().Count > 0)
                {
                    result = querycus.CustomerBarPrintInfo.OrderBy(o=>o.BarSettings.OrderNum).ToList();
                foreach (var item in result)
                {
                    //作废项目发生改变条码
                    var bargrousID = item.CustomerBarPrintInfo.Select(o=>o.itemgroup_Id).ToList();
                    if (bargrousID.Any(p => !cusgroups.Contains(p.Value)))
                    {
                        barprintId.Add(item.Id);                       
                        continue;

                    }

                    if (item.BarSettings.IsRepeatItemBarcode == 1)
                    {
                        for (var i = 0; i < item.BarSettings.BarPage; i++)
                        {

                            var detail = new DetailDto();
                            detail.CustomerExaminationNumber = item.BarNumBM;
                            string strname = querycus.Customer.Name + " " +
                                SexHelper.CustomSexFormatter(querycus.Customer.Sex) + " " +
                                querycus.Customer.Age.ToString();
                            detail.CustomerName = strname;
                            detail.ItemGroupName = item.BarName;
                            if (item.BarSettings.testType.HasValue)
                            {
                                int tevalue = item.BarSettings.testType.Value;
                                string tex = "";
                                switch (tevalue)
                                {
                                    case 0:
                                        tex = "仪器";
                                        break;
                                    case 1:
                                        tex = "外送";
                                        break;
                                    case 2:
                                        tex = "内检";
                                        break;
                                }
                                detail.TestType = tex;
                            }
                            detail.CustomerBM = querycus.CustomerBM;
                            detail.Colour = item.BarSettings.TubeColor;
                            detail.ClientRegNum = querycus.ClientRegNum;
                            detail.CustomerRegNum = querycus.CustomerRegNum;
                            detail.PrimaryName = querycus.PrimaryName;
                            if (querycus.ClientInfo != null)
                            {
                                detail.ClientInfoName = querycus.ClientInfo.ClientName;
                            }
                            reportJsonForExamine.Detail.Add(detail);
                        }
                        item.BarPrintCount = item.BarPrintCount + 1;
                        _customerBarPrintInfoRepository.Update(item);
                    }
                    //CurrentUnitOfWork.SaveChanges();
                }
                }
            //删除项目发生变化条码
            #region 删除项目发生变化条码
            if (barprintId.Count > 0)
            {
                var Nogroup = _CustomerBarPrintInfoItemGroup.GetAll().Where(p => barprintId.Contains(p.BarPrintInfoId.Value)).Select(p=>p.itemgroup_Id).ToList();
                _CustomerBarPrintInfoItemGroup.GetAll().Where(p => barprintId.Contains(p.BarPrintInfoId.Value)).Delete();
                _customerBarPrintInfoRepository.GetAll().Where(p => barprintId.Contains(p.Id)).Delete();

                var cusgroupIds = _TjlCustomerItemGroup.GetAll().Where(o => o.CustomerRegBMId == querycus.Id && Nogroup.Contains( o.ItemGroupBM_Id)).Select(p=>p.Id).ToList();
                foreach (var cusgroup in cusgroupIds)
                {

                    _TjlCustomerItemGroup.Update(cusgroup, e => { e.BarState =1; });
                }                    
               

                CurrentUnitOfWork.SaveChanges();
            }
            #endregion
            //未打条码
            var cusbarlis = result.MapTo<List<CustomerBarPrintInfoDto>>();

            var quen = querycus.CustomerItemGroup.Where(o => o.BarState != 2 && o.IsAddMinus != (int)AddMinusType.Minus);
            #region 放弃是否显示控制        
            if (!string.IsNullOrEmpty(fqxs) && fqxs == "1")
            {
                quen = quen.Where(r => r.CheckState != (int)ProjectIState.GiveUp).ToList();
            }
            #endregion
            var cusgroupids = quen.Select(o => o.ItemGroupBM_Id).ToList();
                //var groupid = groups.Select(o => o.ItemGroupBM_Id).ToList();
                var barsetls = _barSettingsRepository.GetAllIncluding(o => o.Baritems).Where(o => o.Baritems.Any(n => cusgroupids.Contains(n.ItemGroupId))).OrderBy(o=>o.OrderNum).ToList();
            foreach (var barset in barsetls)
            {
                if (barset.IsRepeatItemBarcode == 2)
                { 
                    continue;
                }
                //条码号
                var barnum = "";
                switch (barset.BarNUM)
                {
                    case 1:
                        barnum = querycus.CustomerBM;
                        break;
                    case 3:
                        barnum = _idNumberAppService.CreateBarBM();
                        break;
                }
                barnum = barset.StrBar + barnum;
                //条码名称
                //string barname = "";
                var groupls = barset.Baritems.Where(o => cusgroupids.Contains(o.ItemGroupId)).Select(o => new { o.ItemGroupAlias, o.ItemGroupId }).ToList();
                var groupnamels = groupls.Select(o => o.ItemGroupAlias);
                string barname = string.Join("+", groupnamels);
           
                //条码类型
                string tex = "";
                if (barset.testType.HasValue)
                {
                    int tevalue = barset.testType.Value;

                    switch (tevalue)
                    {
                        case 0:
                            tex = "仪器";
                            break;
                        case 1:
                            tex = "外送";
                            break;
                        case 2:
                            tex = "内检";
                            break;
                    }

                }
                #region gruid++条码数据
                for (var i = 0; i < barset.BarPage; i++)
                {
                    var detail = new DetailDto();


                    detail.CustomerExaminationNumber = barnum;
                    string strname = querycus.Customer.Name + " " +
                        SexHelper.CustomSexFormatter(querycus.Customer.Sex) + " " +
                        querycus.Customer.Age.ToString();
                    detail.CustomerName = strname;
                    detail.ItemGroupName = barname;
                    detail.TestType = tex;
                    detail.Colour = barset.TubeColor;
                    detail.ClientRegNum = querycus.ClientRegNum;
                    detail.CustomerRegNum = querycus.CustomerRegNum;
                    detail.PrimaryName = querycus.PrimaryName;
                    detail.CustomerBM = querycus.CustomerBM;
                    if (querycus.ClientInfo != null)
                    {
                        detail.ClientInfoName = querycus.ClientInfo.ClientName;
                    }
                    reportJsonForExamine.Detail.Add(detail);
                }
                #endregion
                //增加条码记录
                TjlCustomerBarPrintInfo customerBarPrintInfo = new TjlCustomerBarPrintInfo();
                customerBarPrintInfo.BarName = barname;
                customerBarPrintInfo.BarNumBM = barnum;
                customerBarPrintInfo.BarPrintCount = customerBarPrintInfo.BarPrintCount;
                customerBarPrintInfo.BarPrintTime = System.DateTime.Now;

                customerBarPrintInfo.Id = Guid.NewGuid();
                customerBarPrintInfo.BarSettingsId = barset.Id;
                customerBarPrintInfo.CustomerReg_Id = querycus.Id;
                customerBarPrintInfo.Age = querycus.RegAge;
                customerBarPrintInfo.ArchivesNum = querycus.CustomerBM;
                _customerBarPrintInfoRepository.Insert(customerBarPrintInfo);
                //CurrentUnitOfWork.SaveChanges();
                //增加条码项目记录
                foreach (var bargroup in groupls)
                {
                    TjlCustomerBarPrintInfoItemGroup tjlCustomerBarPrintInfoItem = new TjlCustomerBarPrintInfoItemGroup();
                    tjlCustomerBarPrintInfoItem.Id = Guid.NewGuid();
                    tjlCustomerBarPrintInfoItem.BarPrintInfoId = customerBarPrintInfo.Id;
                    tjlCustomerBarPrintInfoItem.CollectionState = false;
                    tjlCustomerBarPrintInfoItem.itemgroup_Id = bargroup.ItemGroupId;
                    tjlCustomerBarPrintInfoItem.ItemGroupName = bargroup.ItemGroupAlias;
                    tjlCustomerBarPrintInfoItem.ItemGroupNameAlias = bargroup.ItemGroupAlias;
                    _CustomerBarPrintInfoItemGroup.Insert(tjlCustomerBarPrintInfoItem); ;

                    //CurrentUnitOfWork.SaveChanges();

                }
                var groupids = groupls.Select(o=>o.ItemGroupId).ToList();
                _TjlCustomerItemGroup.GetAll().Where(o=>o.CustomerRegBMId== querycus.Id && groupids.Contains(o.ItemGroupBM_Id.Value)).Update(o=>new TjlCustomerItemGroup { BarState=2});
            }

            //CurrentUnitOfWork.SaveChanges();
            //}
            return reportJsonForExamine;

        }

        /// <summary>
        /// 查询所有打印条码
        /// </summary>
        [UnitOfWork(isTransactional: false)]
        public  ReportJsonDto GetAllBarPrint(List<CusNameInput> cusNameInputls)
        {
            var result = new List<TjlCustomerBarPrintInfo>();
            var reportJsonForExamine = new ReportJsonDto();
            reportJsonForExamine.Detail = new List<DetailDto>();
            foreach (var cusNameInput in cusNameInputls)
            {
                var querycus = _customerRegRepository.Get(cusNameInput.Id);
            //现有组合
            var cusgroups = querycus.CustomerItemGroup.Where(p => p.IsAddMinus != (int)AddMinusType.Minus).Select(o => o.ItemGroupBM_Id).ToList();
            List<Guid> barprintId = new List<Guid>();
            //已打条码
            if (querycus.CustomerItemGroup.Where(o => o.BarState == 2).ToList().Count > 0)
            {
                result = querycus.CustomerBarPrintInfo.OrderBy(o => o.BarSettings.OrderNum).ToList();
                foreach (var item in result)
                {
                    //作废项目发生改变条码
                    var bargrousID = item.CustomerBarPrintInfo.Select(o => o.itemgroup_Id).ToList();
                    if (bargrousID.Any(p => !cusgroups.Contains(p.Value)))
                    {
                        barprintId.Add(item.Id);
                        continue;

                    }

                    if (item.BarSettings.IsRepeatItemBarcode == 1)
                    {
                        for (var i = 0; i < item.BarSettings.BarPage; i++)
                        {

                            var detail = new DetailDto();
                            detail.CustomerExaminationNumber = item.BarNumBM;
                            string strname = querycus.Customer.Name + " " +
                                SexHelper.CustomSexFormatter(querycus.Customer.Sex) + " " +
                                querycus.Customer.Age.ToString();
                            detail.CustomerName = strname;
                            detail.ItemGroupName = item.BarName;
                            if (item.BarSettings.testType.HasValue)
                            {
                                int tevalue = item.BarSettings.testType.Value;
                                string tex = "";
                                switch (tevalue)
                                {
                                    case 0:
                                        tex = "仪器";
                                        break;
                                    case 1:
                                        tex = "外送";
                                        break;
                                    case 2:
                                        tex = "内检";
                                        break;
                                }
                                detail.TestType = tex;
                            }
                            detail.Colour = item.BarSettings.TubeColor;
                            detail.ClientRegNum = querycus.ClientRegNum;
                            detail.CustomerRegNum = querycus.CustomerRegNum;
                            reportJsonForExamine.Detail.Add(detail);
                        }
                        item.BarPrintCount = item.BarPrintCount + 1;
                        _customerBarPrintInfoRepository.Update(item);
                    }
                    //CurrentUnitOfWork.SaveChanges();
                }
            }
            //删除项目发生变化条码
            #region 删除项目发生变化条码
            if (barprintId.Count > 0)
            {
                var Nogroup = _CustomerBarPrintInfoItemGroup.GetAll().Where(p => barprintId.Contains(p.BarPrintInfoId.Value)).Select(p => p.itemgroup_Id).ToList();
                _CustomerBarPrintInfoItemGroup.GetAll().Where(p => barprintId.Contains(p.BarPrintInfoId.Value)).Delete();
                _customerBarPrintInfoRepository.GetAll().Where(p => barprintId.Contains(p.Id)).Delete();

                var cusgroupIds = _TjlCustomerItemGroup.GetAll().Where(o => o.CustomerRegBMId == querycus.Id && Nogroup.Contains(o.ItemGroupBM_Id)).Select(p => p.Id).ToList();
                foreach (var cusgroup in cusgroupIds)
                {

                    _TjlCustomerItemGroup.Update(cusgroup, e => { e.BarState = 1; });
                }


                CurrentUnitOfWork.SaveChanges();
            }
            #endregion
            //未打条码
            var cusbarlis = result.MapTo<List<CustomerBarPrintInfoDto>>();
            var cusgroupids = querycus.CustomerItemGroup.Where(o => o.BarState != 2 && o.IsAddMinus != (int)AddMinusType.Minus).Select(o => o.ItemGroupBM_Id).ToList();
            //var groupid = groups.Select(o => o.ItemGroupBM_Id).ToList();
            var barsetls = _barSettingsRepository.GetAllIncluding(o => o.Baritems).Where(o => o.Baritems.Any(n => cusgroupids.Contains(n.ItemGroupId))).OrderBy(o => o.OrderNum).ToList();
            foreach (var barset in barsetls)
            {
                if (barset.IsRepeatItemBarcode == 2)
                {
                    continue;
                }
                //条码号
                var barnum = "";
                switch (barset.BarNUM)
                {
                    case 1:
                        barnum = querycus.CustomerBM;
                        break;
                    case 3:
                        barnum = _idNumberAppService.CreateBarBM();
                        break;
                }
                //条码名称
                //string barname = "";
                var groupls = barset.Baritems.Where(o => cusgroupids.Contains(o.ItemGroupId)).Select(o => new { o.ItemGroupAlias, o.ItemGroupId }).ToList();
                var groupnamels = groupls.Select(o => o.ItemGroupAlias);
                string barname = string.Join("+", groupnamels);

                //条码类型
                string tex = "";
                if (barset.testType.HasValue)
                {
                    int tevalue = barset.testType.Value;

                    switch (tevalue)
                    {
                        case 0:
                            tex = "仪器";
                            break;
                        case 1:
                            tex = "外送";
                            break;
                        case 2:
                            tex = "内检";
                            break;
                    }

                }
                #region gruid++条码数据
                for (var i = 0; i < barset.BarPage; i++)
                {
                    var detail = new DetailDto();


                    detail.CustomerExaminationNumber = barnum;
                    string strname = querycus.Customer.Name + " " +
                        SexHelper.CustomSexFormatter(querycus.Customer.Sex) + " " +
                        querycus.Customer.Age.ToString();
                    detail.CustomerName = strname;
                    detail.ItemGroupName = barname;
                    detail.TestType = tex;
                    detail.Colour = barset.TubeColor;
                    detail.ClientRegNum = querycus.ClientRegNum;
                    detail.CustomerRegNum = querycus.CustomerRegNum;
                    reportJsonForExamine.Detail.Add(detail);
                }
                #endregion
                //增加条码记录
                TjlCustomerBarPrintInfo customerBarPrintInfo = new TjlCustomerBarPrintInfo();
                customerBarPrintInfo.BarName = barname;
                customerBarPrintInfo.BarNumBM = barnum;
                customerBarPrintInfo.BarPrintCount = customerBarPrintInfo.BarPrintCount;
                customerBarPrintInfo.BarPrintTime = System.DateTime.Now;

                customerBarPrintInfo.Id = Guid.NewGuid();
                customerBarPrintInfo.BarSettingsId = barset.Id;
                customerBarPrintInfo.CustomerReg_Id = querycus.Id;
                customerBarPrintInfo.Age = querycus.RegAge;
                customerBarPrintInfo.ArchivesNum = querycus.CustomerBM;
                _customerBarPrintInfoRepository.Insert(customerBarPrintInfo);
                //CurrentUnitOfWork.SaveChanges();
                //增加条码项目记录
                foreach (var bargroup in groupls)
                {
                    TjlCustomerBarPrintInfoItemGroup tjlCustomerBarPrintInfoItem = new TjlCustomerBarPrintInfoItemGroup();
                    tjlCustomerBarPrintInfoItem.Id = Guid.NewGuid();
                    tjlCustomerBarPrintInfoItem.BarPrintInfoId = customerBarPrintInfo.Id;
                    tjlCustomerBarPrintInfoItem.CollectionState = false;
                    tjlCustomerBarPrintInfoItem.itemgroup_Id = bargroup.ItemGroupId;
                    tjlCustomerBarPrintInfoItem.ItemGroupName = bargroup.ItemGroupAlias;
                    tjlCustomerBarPrintInfoItem.ItemGroupNameAlias = bargroup.ItemGroupAlias;
                    _CustomerBarPrintInfoItemGroup.Insert(tjlCustomerBarPrintInfoItem); ;

                    //CurrentUnitOfWork.SaveChanges();

                }
                var groupids = groupls.Select(o => o.ItemGroupId).ToList();
                _TjlCustomerItemGroup.GetAll().Where(o => o.CustomerRegBMId == querycus.Id && groupids.Contains(o.ItemGroupBM_Id.Value)).Update(o => new TjlCustomerItemGroup { BarState = 2 });
            }

                //CurrentUnitOfWork.SaveChanges();
            }
            return reportJsonForExamine;

        }
        /// <summary>
        /// 更新条码，导引单打印状态
        /// </summary>
        /// <param name="input"></param>
        public void UpdateCustomerRegisterPrintState(ChargeBM input)
        {
            var row = _customerRegRepository.Get(input.Id);
            if (input.Name.Contains("条码"))
            {
                row.BarState = (int)PrintSate.Print;
            }
            else if (input.Name.Contains("导引单"))
            {
                row.GuidanceSate = (int)PrintSate.Print;
            }
            _customerRegRepository.Update(row);
        }
    }
}