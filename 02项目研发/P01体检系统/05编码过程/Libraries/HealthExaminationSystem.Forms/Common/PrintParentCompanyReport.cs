using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using gregn6Lib;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Common
{
    /// <summary>
    /// 打印父类公司报告
    /// </summary>
    public class PrintParentCompanyReport
    {
        /// <summary>
        /// 主报表对象
        /// </summary>
        private GridppReport _gridppReportMain;

        /// <summary>
        /// 父类公司
        /// </summary>
        private ClientInfoCacheDto _parentCompany;

        /// <summary>
        /// 模板地址
        /// </summary>
        private string _templateUrl = null;

        /// <summary>
        /// 可以执行打印操作
        /// </summary>
        private bool _canPrint = false;

        /// <summary>
        /// 公司预约列表
        /// </summary>
        private List<CompanyRegisterDtoNo1> _companyRegisterList;

        /// <summary>
        /// 体检人预约列表
        /// </summary>
        private List<CustomerRegisterDtoNo3> _customerRegisterList;

        /// <summary>
        /// 体检人预约总检建议列表
        /// </summary>
        private List<CustomerRegisterSummarizeAdviceDtoNo1> _customerRegisterSummarizeAdviceList;

        /// <summary>
        /// 体检人预约的项目组合列表
        /// </summary>
        private List<CustomerRegisterItemGroupDtoNo4> _customerRegisterItemGroupList;

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="preview"></param>
        /// <param name="printDialog"></param>
        public void Print(bool preview = false, bool printDialog = false)
        {
            if (!_canPrint)
            {
                throw new ApplicationException(@"打印报告需要先执行加载数据方法，再执行打印方法。请联系管理员。");
            }

            _gridppReportMain = new GridppReport();
            try
            {
                _gridppReportMain.LoadFromURL(GridppHelper.GetTemplate(_templateUrl));

                _gridppReportMain.Initialize -= _gridppReportMain_Initialize;
                _gridppReportMain.Initialize += _gridppReportMain_Initialize;

                var localVariable子报表参检人员年龄分布控件 = _gridppReportMain.ControlByName("子报表参检人员年龄分布");
                if (localVariable子报表参检人员年龄分布控件 != null && localVariable子报表参检人员年龄分布控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表参检人员年龄分布 = localVariable子报表参检人员年龄分布控件.AsSubReport;
                    if (localVariable子报表参检人员年龄分布 != null)
                    {
                        localVariable子报表参检人员年龄分布.Report.Initialize -= localVariable子报表参检人员年龄分布_Report_Initialize;
                        localVariable子报表参检人员年龄分布.Report.Initialize += localVariable子报表参检人员年龄分布_Report_Initialize;

                        localVariable子报表参检人员年龄分布.Report.FetchRecord -= localVariable子报表参检人员年龄分布_Report_FetchRecord;
                        localVariable子报表参检人员年龄分布.Report.FetchRecord += localVariable子报表参检人员年龄分布_Report_FetchRecord;

                        localVariable子报表参检人员年龄分布.Report.ChartRequestData -= localVariable子报表参检人员年龄分布_Report_ChartRequestData;
                        localVariable子报表参检人员年龄分布.Report.ChartRequestData += localVariable子报表参检人员年龄分布_Report_ChartRequestData;
                    }
                }

                var localVariable子报表本次体检异常结果检出统计控件 = _gridppReportMain.ControlByName("子报表本次体检异常结果检出统计");
                if (localVariable子报表本次体检异常结果检出统计控件 != null && localVariable子报表本次体检异常结果检出统计控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表本次体检异常结果检出统计 = localVariable子报表本次体检异常结果检出统计控件.AsSubReport;
                    if (localVariable子报表本次体检异常结果检出统计 != null)
                    {
                        localVariable子报表本次体检异常结果检出统计.Report.FetchRecord -= localVariable子报表本次体检异常结果检出统计_Report_FetchRecord;
                        localVariable子报表本次体检异常结果检出统计.Report.FetchRecord += localVariable子报表本次体检异常结果检出统计_Report_FetchRecord;
                    }
                }

                var localVariable子报表本次体检异常结果检出统计前十图表控件 = _gridppReportMain.ControlByName("子报表本次体检异常结果检出统计前十图表");
                if (localVariable子报表本次体检异常结果检出统计前十图表控件 != null &&
                    localVariable子报表本次体检异常结果检出统计前十图表控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表本次体检异常结果检出统计前十图表 = localVariable子报表本次体检异常结果检出统计前十图表控件.AsSubReport;
                    if (localVariable子报表本次体检异常结果检出统计前十图表 != null)
                    {
                        localVariable子报表本次体检异常结果检出统计前十图表.Report.ChartRequestData -= localVariable子报表本次体检异常结果检出统计前十图表_Report_ChartRequestData;
                        localVariable子报表本次体检异常结果检出统计前十图表.Report.ChartRequestData += localVariable子报表本次体检异常结果检出统计前十图表_Report_ChartRequestData;
                    }
                }

                var localVariable子报表全体员工健康问题分析及保健建议前十控件 = _gridppReportMain.ControlByName("子报表全体员工健康问题分析及保健建议前十");
                if (localVariable子报表全体员工健康问题分析及保健建议前十控件 != null &&
                    localVariable子报表全体员工健康问题分析及保健建议前十控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表全体员工健康问题分析及保健建议前十 = localVariable子报表全体员工健康问题分析及保健建议前十控件.AsSubReport;
                    if (localVariable子报表全体员工健康问题分析及保健建议前十 != null)
                    {
                        localVariable子报表全体员工健康问题分析及保健建议前十.Report.FetchRecord -= localVariable子报表全体员工健康问题分析及保健建议前十_Report_FetchRecord;
                        localVariable子报表全体员工健康问题分析及保健建议前十.Report.FetchRecord += localVariable子报表全体员工健康问题分析及保健建议前十_Report_FetchRecord;
                    }
                }

                var localVariable子报表男性员工健康问题分析及保健建议前十控件 = _gridppReportMain.ControlByName("子报表男性员工健康问题分析及保健建议前十");
                if (localVariable子报表男性员工健康问题分析及保健建议前十控件 != null &&
                    localVariable子报表男性员工健康问题分析及保健建议前十控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表男性员工健康问题分析及保健建议前十 = localVariable子报表男性员工健康问题分析及保健建议前十控件.AsSubReport;
                    if (localVariable子报表男性员工健康问题分析及保健建议前十 != null)
                    {
                        localVariable子报表男性员工健康问题分析及保健建议前十.Report.FetchRecord -= localVariable子报表男性员工健康问题分析及保健建议前十_Report_FetchRecord;
                        localVariable子报表男性员工健康问题分析及保健建议前十.Report.FetchRecord += localVariable子报表男性员工健康问题分析及保健建议前十_Report_FetchRecord;
                    }
                }

                var localVariable子报表女性员工健康问题分析及保健建议前十控件 = _gridppReportMain.ControlByName("子报表女性员工健康问题分析及保健建议前十");
                if (localVariable子报表女性员工健康问题分析及保健建议前十控件 != null &&
                    localVariable子报表女性员工健康问题分析及保健建议前十控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表女性员工健康问题分析及保健建议前十 = localVariable子报表女性员工健康问题分析及保健建议前十控件.AsSubReport;
                    if (localVariable子报表女性员工健康问题分析及保健建议前十 != null)
                    {
                        localVariable子报表女性员工健康问题分析及保健建议前十.Report.FetchRecord -= localVariable子报表女性员工健康问题分析及保健建议前十_Report_FetchRecord;
                        localVariable子报表女性员工健康问题分析及保健建议前十.Report.FetchRecord += localVariable子报表女性员工健康问题分析及保健建议前十_Report_FetchRecord;
                    }
                }

                var localVariable子报表未到检人员名单控件 = _gridppReportMain.ControlByName("子报表未到检人员名单");
                if (localVariable子报表未到检人员名单控件 != null &&
                    localVariable子报表未到检人员名单控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表未到检人员名单 = localVariable子报表未到检人员名单控件.AsSubReport;
                    if (localVariable子报表未到检人员名单 != null)
                    {
                        localVariable子报表未到检人员名单.Report.FetchRecord -= localVariable子报表未到检人员名单_Report_FetchRecord;
                        localVariable子报表未到检人员名单.Report.FetchRecord += localVariable子报表未到检人员名单_Report_FetchRecord;
                    }
                }

                var localVariable子报表未检项目汇总控件 = _gridppReportMain.ControlByName("子报表未检项目汇总");
                if (localVariable子报表未检项目汇总控件 != null &&
                    localVariable子报表未检项目汇总控件.ControlType == GRControlType.grctSubReport)
                {
                    var localVariable子报表未检项目汇总 = localVariable子报表未检项目汇总控件.AsSubReport;
                    if (localVariable子报表未检项目汇总 != null)
                    {
                        localVariable子报表未检项目汇总.Report.FetchRecord -= localVariable子报表未检项目汇总_Report_FetchRecord;
                        localVariable子报表未检项目汇总.Report.FetchRecord += localVariable子报表未检项目汇总_Report_FetchRecord;
                    }
                }

                /*
                 * 增加本地打印机配置使用
                 */
                var printerName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 40)?.Remarks;
                if (!string.IsNullOrWhiteSpace(printerName))
                {
                    _gridppReportMain.Printer.PrinterName = printerName;
                }

                if (preview)
                {
                    _gridppReportMain.PrintPreview(true);
                }
                else
                {
                    _gridppReportMain.Print(printDialog);
                }
            }
            finally
            {
                Marshal.ReleaseComObject(_gridppReportMain);
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="parentCompanyId"></param>
        /// <param name="companyRegisterList"></param>
        public PrintParentCompanyReport LoadData(Guid parentCompanyId, List<CompanyRegisterDtoNo1> companyRegisterList)
        {
            if (parentCompanyId == Guid.Empty)
            {
                throw new ArgumentException(@"父类公司标识无效。", nameof(parentCompanyId));
            }

            _parentCompany = DefinedCacheHelper.GetAllClientInfoCache().Find(r => r.Id == parentCompanyId);
            if (_parentCompany == null)
            {
                throw new ArgumentException(@"使用给定的父类公司标识查找公司不存在。", nameof(parentCompanyId));
            }

            if (companyRegisterList == null || companyRegisterList.Count == 0)
            {
                throw new ArgumentException(@"缺少公司预约记录。", nameof(companyRegisterList));
            }

            _templateUrl = "父级汇总团体报告-健康体检.grf";

            _companyRegisterList = companyRegisterList;

            var companyRegisterIds = companyRegisterList.Select(r => r.Id).ToList();
            _customerRegisterList = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterAppService.CustomerRegisterList(companyRegisterIds).Result;
            _customerRegisterSummarizeAdviceList =
                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterSummarizeAdviceAppService
                    .CustomerRegisterSummarizeAdviceListNo1(companyRegisterIds).Result;

            var customerRegisterIds = _customerRegisterList.Select(r => r.Id).ToList();
            _customerRegisterItemGroupList =
                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemGroupAppService.CustomerRegisterItemGroupList(
                    customerRegisterIds).Result;

            _canPrint = true;
            return this;
        }

        /// <summary>
        /// 主报表初始化功能
        /// </summary>
        private void _gridppReportMain_Initialize()
        {
            _gridppReportMain.Title = $"{_parentCompany.ClientName}-{_gridppReportMain.Title}";
            var localVariable参数父级公司名称 = _gridppReportMain.ParameterByName("参数父级公司名称");
            if (localVariable参数父级公司名称 != null)
            {
                localVariable参数父级公司名称.Value = _parentCompany.ClientName;
            }
            var localVariable参数预约体检开始时间 = _gridppReportMain.ParameterByName("参数预约体检开始时间");
            if (localVariable参数预约体检开始时间 != null)
            {
                localVariable参数预约体检开始时间.Value = _companyRegisterList.Min(r => r.StartCheckDate);
            }
            var localVariable参数预约体检结束时间 = _gridppReportMain.ParameterByName("参数预约体检结束时间");
            if (localVariable参数预约体检结束时间 != null)
            {
                localVariable参数预约体检结束时间.Value = _companyRegisterList.Max(r => r.EndCheckDate);
            }
            var localVariable参数全体员工疾病列表前十 = _gridppReportMain.ParameterByName("参数全体员工疾病列表前十");
            if (localVariable参数全体员工疾病列表前十 != null)
            {
                var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                    .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                    .Take(10);
                var stringBuilder参数全体员工疾病列表前十 = new StringBuilder();
                var stringBuilder参数全体员工疾病列表前十标号 = new StringBuilder();
                var index = 1;
                foreach (var customerRegisterSummarizeAdvice in customerRegisterSummarizeAdviceGroupByAdviceId)
                {
                    stringBuilder参数全体员工疾病列表前十.Append(customerRegisterSummarizeAdvice.First().SummarizeName.Trim());
                    stringBuilder参数全体员工疾病列表前十.AppendLine(new string('-', 70));
                    stringBuilder参数全体员工疾病列表前十标号.AppendLine($"{index++}.");
                }
                localVariable参数全体员工疾病列表前十.Value = stringBuilder参数全体员工疾病列表前十.ToString().Trim();
                var localVariable参数全体员工疾病列表前十标号 = _gridppReportMain.ParameterByName("参数全体员工疾病列表前十标号");
                if (localVariable参数全体员工疾病列表前十标号 != null)
                {
                    localVariable参数全体员工疾病列表前十标号.Value = stringBuilder参数全体员工疾病列表前十标号.ToString().Trim();
                }
            }
            var localVariable参数男性员工疾病列表前十 = _gridppReportMain.ParameterByName("参数男性员工疾病列表前十");
            if (localVariable参数男性员工疾病列表前十 != null)
            {
                var customerRegisterIdsMan =
                    _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Man).Select(r => r.Id).ToList();
                var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                    .Where(r => customerRegisterIdsMan.Contains(r.CustomerRegID))
                    .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                    .Take(10);
                var stringBuilder参数男性员工疾病列表前十 = new StringBuilder();
                var stringBuilder参数男性员工疾病列表前十标号 = new StringBuilder();
                var index = 1;
                foreach (var customerRegisterSummarizeAdvice in customerRegisterSummarizeAdviceGroupByAdviceId)
                {
                    stringBuilder参数男性员工疾病列表前十.Append(customerRegisterSummarizeAdvice.First().SummarizeName.Trim());
                    stringBuilder参数男性员工疾病列表前十.AppendLine(new string('-', 70));
                    stringBuilder参数男性员工疾病列表前十标号.AppendLine($"{index++}.");
                }
                localVariable参数男性员工疾病列表前十.Value = stringBuilder参数男性员工疾病列表前十.ToString().Trim();
                var localVariable参数男性员工疾病列表前十标号 = _gridppReportMain.ParameterByName("参数男性员工疾病列表前十标号");
                if (localVariable参数男性员工疾病列表前十标号 != null)
                {
                    localVariable参数男性员工疾病列表前十标号.Value = stringBuilder参数男性员工疾病列表前十标号.ToString().Trim();
                }
            }
            var localVariable参数女性员工疾病列表前十 = _gridppReportMain.ParameterByName("参数女性员工疾病列表前十");
            if (localVariable参数女性员工疾病列表前十 != null)
            {
                var customerRegisterIdsWoman =
                    _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Woman).Select(r => r.Id).ToList();
                var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                    .Where(r => customerRegisterIdsWoman.Contains(r.CustomerRegID))
                    .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                    .Take(10);
                var stringBuilder参数女性员工疾病列表前十 = new StringBuilder();
                var stringBuilder参数女性员工疾病列表前十标号 = new StringBuilder();
                var index = 1;
                foreach (var customerRegisterSummarizeAdvice in customerRegisterSummarizeAdviceGroupByAdviceId)
                {
                    stringBuilder参数女性员工疾病列表前十.Append(customerRegisterSummarizeAdvice.First().SummarizeName.Trim());
                    stringBuilder参数女性员工疾病列表前十.AppendLine(new string('-', 70));
                    stringBuilder参数女性员工疾病列表前十标号.AppendLine($"{index++}.");
                }
                localVariable参数女性员工疾病列表前十.Value = stringBuilder参数女性员工疾病列表前十.ToString().Trim();
                var localVariable参数女性员工疾病列表前十标号 = _gridppReportMain.ParameterByName("参数女性员工疾病列表前十标号");
                if (localVariable参数女性员工疾病列表前十标号 != null)
                {
                    localVariable参数女性员工疾病列表前十标号.Value = stringBuilder参数女性员工疾病列表前十标号.ToString().Trim();
                }
            }
        }

        /// <summary>
        /// “参检人员年龄分布”子报表初始化功能
        /// </summary>
        private void localVariable子报表参检人员年龄分布_Report_Initialize()
        {
            var localVariable子报表参检人员年龄分布控件 = _gridppReportMain.ControlByName("子报表参检人员年龄分布");
            if (localVariable子报表参检人员年龄分布控件 != null && localVariable子报表参检人员年龄分布控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表参检人员年龄分布 = localVariable子报表参检人员年龄分布控件.AsSubReport;
                if (localVariable子报表参检人员年龄分布 != null)
                {
                    var localVariable参数总人数 = localVariable子报表参检人员年龄分布.Report.ParameterByName("参数总人数");
                    if (localVariable参数总人数 != null)
                    {
                        localVariable参数总人数.Value = _customerRegisterList.Count;
                    }
                    var localVariable参数男性总人数 = localVariable子报表参检人员年龄分布.Report.ParameterByName("参数男性总人数");
                    if (localVariable参数男性总人数 != null)
                    {
                        localVariable参数男性总人数.Value = _customerRegisterList.Count(r => r.Customer.Sex == (int)Sex.Man);
                    }
                    var localVariable参数女性总人数 = localVariable子报表参检人员年龄分布.Report.ParameterByName("参数女性总人数");
                    if (localVariable参数女性总人数 != null)
                    {
                        localVariable参数女性总人数.Value = _customerRegisterList.Count(r => r.Customer.Sex == (int)Sex.Woman);
                    }
                }
            }
        }

        /// <summary>
        /// “参检人员年龄分布”子报表请求数据事件
        /// </summary>
        private void localVariable子报表参检人员年龄分布_Report_FetchRecord()
        {
            var localVariable子报表参检人员年龄分布控件 = _gridppReportMain.ControlByName("子报表参检人员年龄分布");
            if (localVariable子报表参检人员年龄分布控件 != null && localVariable子报表参检人员年龄分布控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表参检人员年龄分布 = localVariable子报表参检人员年龄分布控件.AsSubReport;
                if (localVariable子报表参检人员年龄分布 != null)
                {
                    var localVariableDetailGrid = localVariable子报表参检人员年龄分布.Report.DetailGrid;
                    if (localVariableDetailGrid != null)
                    {
                        // 年龄段：
                        // 24岁以下
                        // 25-34岁
                        // 35-44岁
                        // 45-54岁
                        // 55-64岁
                        // 65岁以上
                        var count = _customerRegisterList.Count;
                        var countMan = _customerRegisterList.Count(r => r.Customer.Sex == (int)Sex.Man);
                        var countWoman = _customerRegisterList.Count(r => r.Customer.Sex == (int)Sex.Woman);

                        {
                            var count24Less = _customerRegisterList.Count(r => r.GetCustomerAge() <= 24);
                            var count24LessMan = _customerRegisterList.Count(r => r.GetCustomerAge() <= 24 && r.Customer.Sex == (int)Sex.Man);
                            var count24LessWoman = _customerRegisterList.Count(r => r.GetCustomerAge() <= 24 && r.Customer.Sex == (int)Sex.Woman);

                            localVariableDetailGrid.Recordset.Append();
                            var localVariable字段年龄段 = localVariableDetailGrid.Recordset.FieldByName("字段年龄段");
                            if (localVariable字段年龄段 != null)
                            {
                                localVariable字段年龄段.Value = "24岁以下";
                            }
                            var localVariable字段男性人数 = localVariableDetailGrid.Recordset.FieldByName("字段男性人数");
                            if (localVariable字段男性人数 != null)
                            {
                                localVariable字段男性人数.Value = count24LessMan;
                            }
                            var localVariable字段女性人数 = localVariableDetailGrid.Recordset.FieldByName("字段女性人数");
                            if (localVariable字段女性人数 != null)
                            {
                                localVariable字段女性人数.Value = count24LessWoman;
                            }
                            var localVariable字段总人数 = localVariableDetailGrid.Recordset.FieldByName("字段总人数");
                            if (localVariable字段总人数 != null)
                            {
                                localVariable字段总人数.Value = count24Less;
                            }
                            var localVariable字段男性人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段男性人数比例");
                            if (localVariable字段男性人数比例 != null)
                            {
                                localVariable字段男性人数比例.Value = (countMan == 0 ? 0 : (decimal)count24LessMan / countMan).ToString("P");
                            }
                            var localVariable字段女性人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段女性人数比例");
                            if (localVariable字段女性人数比例 != null)
                            {
                                localVariable字段女性人数比例.Value = (countWoman == 0 ? 0 : (decimal)count24LessWoman / countWoman).ToString("P");
                            }
                            var localVariable字段总人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段总人数比例");
                            if (localVariable字段总人数比例 != null)
                            {
                                localVariable字段总人数比例.Value = (count == 0 ? 0 : (decimal)count24Less / count).ToString("P");
                            }
                            localVariableDetailGrid.Recordset.Post();
                        }

                        var minAge = 25;
                        var maxAge = 34;
                        var formatAgeRange = "{0}-{1}岁";
                        while (maxAge <= 64)
                        {
                            var countLess = _customerRegisterList.Count(r => r.GetCustomerAge() >= minAge && r.GetCustomerAge() <= maxAge);
                            var countLessMan = _customerRegisterList.Count(r => r.GetCustomerAge() >= minAge && r.GetCustomerAge() <= maxAge && r.Customer.Sex == (int)Sex.Man);
                            var countLessWoman = _customerRegisterList.Count(r => r.GetCustomerAge() >= minAge && r.GetCustomerAge() <= maxAge && r.Customer.Sex == (int)Sex.Woman);

                            localVariableDetailGrid.Recordset.Append();
                            var localVariable字段年龄段 = localVariableDetailGrid.Recordset.FieldByName("字段年龄段");
                            if (localVariable字段年龄段 != null)
                            {
                                localVariable字段年龄段.Value = string.Format(formatAgeRange, minAge, maxAge);
                            }
                            var localVariable字段男性人数 = localVariableDetailGrid.Recordset.FieldByName("字段男性人数");
                            if (localVariable字段男性人数 != null)
                            {
                                localVariable字段男性人数.Value = countLessMan;
                            }
                            var localVariable字段女性人数 = localVariableDetailGrid.Recordset.FieldByName("字段女性人数");
                            if (localVariable字段女性人数 != null)
                            {
                                localVariable字段女性人数.Value = countLessWoman;
                            }
                            var localVariable字段总人数 = localVariableDetailGrid.Recordset.FieldByName("字段总人数");
                            if (localVariable字段总人数 != null)
                            {
                                localVariable字段总人数.Value = countLess;
                            }
                            var localVariable字段男性人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段男性人数比例");
                            if (localVariable字段男性人数比例 != null)
                            {
                                localVariable字段男性人数比例.Value = (countMan == 0 ? 0 : (decimal)countLessMan / countMan).ToString("P");
                            }
                            var localVariable字段女性人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段女性人数比例");
                            if (localVariable字段女性人数比例 != null)
                            {
                                localVariable字段女性人数比例.Value = (countWoman == 0 ? 0 : (decimal)countLessWoman / countWoman).ToString("P");
                            }
                            var localVariable字段总人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段总人数比例");
                            if (localVariable字段总人数比例 != null)
                            {
                                localVariable字段总人数比例.Value = (count == 0 ? 0 : (decimal)countLess / count).ToString("P");
                            }
                            localVariableDetailGrid.Recordset.Post();

                            minAge += 10;
                            maxAge += 10;
                        }

                        {
                            var count65Greater = _customerRegisterList.Count(r => r.GetCustomerAge() >= 65);
                            var count65GreaterMan = _customerRegisterList.Count(r => r.GetCustomerAge() >= 65 && r.Customer.Sex == (int)Sex.Man);
                            var count65GreaterWoman = _customerRegisterList.Count(r => r.GetCustomerAge() >= 65 && r.Customer.Sex == (int)Sex.Woman);

                            localVariableDetailGrid.Recordset.Append();
                            var localVariable字段年龄段 = localVariableDetailGrid.Recordset.FieldByName("字段年龄段");
                            if (localVariable字段年龄段 != null)
                            {
                                localVariable字段年龄段.Value = "65岁以上";
                            }
                            var localVariable字段男性人数 = localVariableDetailGrid.Recordset.FieldByName("字段男性人数");
                            if (localVariable字段男性人数 != null)
                            {
                                localVariable字段男性人数.Value = count65GreaterMan;
                            }
                            var localVariable字段女性人数 = localVariableDetailGrid.Recordset.FieldByName("字段女性人数");
                            if (localVariable字段女性人数 != null)
                            {
                                localVariable字段女性人数.Value = count65GreaterWoman;
                            }
                            var localVariable字段总人数 = localVariableDetailGrid.Recordset.FieldByName("字段总人数");
                            if (localVariable字段总人数 != null)
                            {
                                localVariable字段总人数.Value = count65Greater;
                            }
                            var localVariable字段男性人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段男性人数比例");
                            if (localVariable字段男性人数比例 != null)
                            {
                                localVariable字段男性人数比例.Value = (countMan == 0 ? 0 : (decimal)count65GreaterMan / countMan).ToString("P");
                            }
                            var localVariable字段女性人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段女性人数比例");
                            if (localVariable字段女性人数比例 != null)
                            {
                                localVariable字段女性人数比例.Value = (countWoman == 0 ? 0 : (decimal)count65GreaterWoman / countWoman).ToString("P");
                            }
                            var localVariable字段总人数比例 = localVariableDetailGrid.Recordset.FieldByName("字段总人数比例");
                            if (localVariable字段总人数比例 != null)
                            {
                                localVariable字段总人数比例.Value = (count == 0 ? 0 : (decimal)count65Greater / count).ToString("P");
                            }
                            localVariableDetailGrid.Recordset.Post();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// “参检人员年龄分布”子报表加载图表数据事件
        /// </summary>
        /// <param name="pSender"></param>
        private void localVariable子报表参检人员年龄分布_Report_ChartRequestData(IGRChart pSender)
        {
            if (pSender.Name == "图表参检人员年龄分布")
            {
                {
                    var count24LessMan = _customerRegisterList.Count(r => r.GetCustomerAge() <= 24 && r.Customer.Sex == (int)Sex.Man);
                    var count24LessWoman = _customerRegisterList.Count(r => r.GetCustomerAge() <= 24 && r.Customer.Sex == (int)Sex.Woman);

                    {
                        pSender.Recordset.Append();
                        var localVariable字段年龄段 = pSender.Recordset.FieldByName("字段年龄段");
                        if (localVariable字段年龄段 != null)
                        {
                            localVariable字段年龄段.Value = "24岁以下";
                        }
                        var localVariable字段人员性别 = pSender.Recordset.FieldByName("字段人员性别");
                        if (localVariable字段人员性别 != null)
                        {
                            localVariable字段人员性别.Value = "男性";
                        }
                        var localVariable字段人数 = pSender.Recordset.FieldByName("字段人数");
                        if (localVariable字段人数 != null)
                        {
                            localVariable字段人数.Value = count24LessMan;
                        }
                        pSender.Recordset.Post();
                    }

                    {
                        pSender.Recordset.Append();
                        var localVariable字段年龄段 = pSender.Recordset.FieldByName("字段年龄段");
                        if (localVariable字段年龄段 != null)
                        {
                            localVariable字段年龄段.Value = "24岁以下";
                        }
                        var localVariable字段人员性别 = pSender.Recordset.FieldByName("字段人员性别");
                        if (localVariable字段人员性别 != null)
                        {
                            localVariable字段人员性别.Value = "女性";
                        }
                        var localVariable字段人数 = pSender.Recordset.FieldByName("字段人数");
                        if (localVariable字段人数 != null)
                        {
                            localVariable字段人数.Value = count24LessWoman;
                        }
                        pSender.Recordset.Post();
                    }
                }

                var minAge = 25;
                var maxAge = 34;
                var formatAgeRange = "{0}-{1}岁";
                while (maxAge <= 64)
                {
                    var countLessMan = _customerRegisterList.Count(r => r.GetCustomerAge() >= minAge && r.GetCustomerAge() <= maxAge && r.Customer.Sex == (int)Sex.Man);
                    var countLessWoman = _customerRegisterList.Count(r => r.GetCustomerAge() >= minAge && r.GetCustomerAge() <= maxAge && r.Customer.Sex == (int)Sex.Woman);

                    {
                        pSender.Recordset.Append();
                        var localVariable字段年龄段 = pSender.Recordset.FieldByName("字段年龄段");
                        if (localVariable字段年龄段 != null)
                        {
                            localVariable字段年龄段.Value = string.Format(formatAgeRange, minAge, maxAge);
                        }
                        var localVariable字段人员性别 = pSender.Recordset.FieldByName("字段人员性别");
                        if (localVariable字段人员性别 != null)
                        {
                            localVariable字段人员性别.Value = "男性";
                        }
                        var localVariable字段人数 = pSender.Recordset.FieldByName("字段人数");
                        if (localVariable字段人数 != null)
                        {
                            localVariable字段人数.Value = countLessMan;
                        }
                        pSender.Recordset.Post();
                    }

                    {
                        pSender.Recordset.Append();
                        var localVariable字段年龄段 = pSender.Recordset.FieldByName("字段年龄段");
                        if (localVariable字段年龄段 != null)
                        {
                            localVariable字段年龄段.Value = string.Format(formatAgeRange, minAge, maxAge);
                        }
                        var localVariable字段人员性别 = pSender.Recordset.FieldByName("字段人员性别");
                        if (localVariable字段人员性别 != null)
                        {
                            localVariable字段人员性别.Value = "女性";
                        }
                        var localVariable字段人数 = pSender.Recordset.FieldByName("字段人数");
                        if (localVariable字段人数 != null)
                        {
                            localVariable字段人数.Value = countLessWoman;
                        }
                        pSender.Recordset.Post();
                    }

                    minAge += 10;
                    maxAge += 10;
                }

                {
                    var count65GreaterMan = _customerRegisterList.Count(r => r.GetCustomerAge() >= 65 && r.Customer.Sex == (int)Sex.Man);
                    var count65GreaterWoman = _customerRegisterList.Count(r => r.GetCustomerAge() >= 65 && r.Customer.Sex == (int)Sex.Woman);

                    {
                        pSender.Recordset.Append();
                        var localVariable字段年龄段 = pSender.Recordset.FieldByName("字段年龄段");
                        if (localVariable字段年龄段 != null)
                        {
                            localVariable字段年龄段.Value = "65岁以上";
                        }
                        var localVariable字段人员性别 = pSender.Recordset.FieldByName("字段人员性别");
                        if (localVariable字段人员性别 != null)
                        {
                            localVariable字段人员性别.Value = "男性";
                        }
                        var localVariable字段人数 = pSender.Recordset.FieldByName("字段人数");
                        if (localVariable字段人数 != null)
                        {
                            localVariable字段人数.Value = count65GreaterMan;
                        }
                        pSender.Recordset.Post();
                    }

                    {
                        pSender.Recordset.Append();
                        var localVariable字段年龄段 = pSender.Recordset.FieldByName("字段年龄段");
                        if (localVariable字段年龄段 != null)
                        {
                            localVariable字段年龄段.Value = "65岁以上";
                        }
                        var localVariable字段人员性别 = pSender.Recordset.FieldByName("字段人员性别");
                        if (localVariable字段人员性别 != null)
                        {
                            localVariable字段人员性别.Value = "女性";
                        }
                        var localVariable字段人数 = pSender.Recordset.FieldByName("字段人数");
                        if (localVariable字段人数 != null)
                        {
                            localVariable字段人数.Value = count65GreaterWoman;
                        }
                        pSender.Recordset.Post();
                    }
                }
            }
        }

        /// <summary>
        /// “本次体检异常结果检出统计”子报表请求数据事件
        /// </summary>
        private void localVariable子报表本次体检异常结果检出统计_Report_FetchRecord()
        {
            var localVariable子报表本次体检异常结果检出统计控件 = _gridppReportMain.ControlByName("子报表本次体检异常结果检出统计");
            if (localVariable子报表本次体检异常结果检出统计控件 != null && localVariable子报表本次体检异常结果检出统计控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表本次体检异常结果检出统计 = localVariable子报表本次体检异常结果检出统计控件.AsSubReport;
                if (localVariable子报表本次体检异常结果检出统计 != null)
                {
                    var detailGrid = localVariable子报表本次体检异常结果检出统计.Report.DetailGrid;
                    if (detailGrid != null)
                    {
                        var countPerson = _customerRegisterList.Count;
                        var countManPerson = _customerRegisterList.Count(r => r.Customer.Sex == (int)Sex.Man);
                        var countWomanPerson = _customerRegisterList.Count(r => r.Customer.Sex == (int)Sex.Woman);

                        var customerRegisterSummarizeAdviceGroupBySummarizeAdviceId = _customerRegisterSummarizeAdviceList.GroupBy(r => r.SummarizeName.Trim());
                        foreach (var customerRegisterSummarizeAdviceGroupList in customerRegisterSummarizeAdviceGroupBySummarizeAdviceId)
                        {
                            var customerRegisterIds =
                                    customerRegisterSummarizeAdviceGroupList.Select(r => r.CustomerRegID).ToList();

                            var countMan = _customerRegisterList.Count(r =>
                                customerRegisterIds.Contains(r.Id) && r.Customer.Sex == (int)Sex.Man);
                            var countWoman = _customerRegisterList.Count(r =>
                                customerRegisterIds.Contains(r.Id) && r.Customer.Sex == (int)Sex.Woman);
                            var count = countMan + countWoman;

                            detailGrid.Recordset.Append();
                            var localVariable字段体检结论 = detailGrid.Recordset.FieldByName("字段体检结论");
                            if (localVariable字段体检结论 != null)
                            {
                                localVariable字段体检结论.Value =
                                    customerRegisterSummarizeAdviceGroupList.First().SummarizeName.Trim();
                            }
                            var localVariable字段男性人数 = detailGrid.Recordset.FieldByName("字段男性人数");
                            if (localVariable字段男性人数 != null)
                            {
                                localVariable字段男性人数.Value = countMan;
                            }
                            var localVariable字段女性人数 = detailGrid.Recordset.FieldByName("字段女性人数");
                            if (localVariable字段女性人数 != null)
                            {
                                localVariable字段女性人数.Value = countWoman;
                            }
                            var localVariable字段总人数 = detailGrid.Recordset.FieldByName("字段总人数");
                            if (localVariable字段总人数 != null)
                            {
                                localVariable字段总人数.Value = count;
                            }
                            var localVariable字段男性人数比例 = detailGrid.Recordset.FieldByName("字段男性人数比例");
                            if (localVariable字段男性人数比例 != null)
                            {
                                localVariable字段男性人数比例.Value = (countManPerson == 0 ? 0 : (decimal)countMan / countManPerson).ToString("P");
                            }
                            var localVariable字段女性人数比例 = detailGrid.Recordset.FieldByName("字段女性人数比例");
                            if (localVariable字段女性人数比例 != null)
                            {
                                localVariable字段女性人数比例.Value = (countWomanPerson == 0 ? 0 : (decimal)countWoman / countWomanPerson).ToString("P");
                            }
                            var localVariable字段总人数比例 = detailGrid.Recordset.FieldByName("字段总人数比例");
                            if (localVariable字段总人数比例 != null)
                            {
                                localVariable字段总人数比例.Value = (countPerson == 0 ? 0 : (decimal)count / countPerson).ToString("P");
                            }
                            detailGrid.Recordset.Post();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// “本次体检异常结果检出统计前十图表”子报表加载图表数据事件
        /// </summary>
        /// <param name="pSender"></param>
        private void localVariable子报表本次体检异常结果检出统计前十图表_Report_ChartRequestData(IGRChart pSender)
        {
            if (pSender.Name == "图表女性员工疾病检出前十")
            {
                var customerRegisterIdsWoman =
                    _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Woman).Select(r => r.Id);
                var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                    .Where(r => customerRegisterIdsWoman.Contains(r.CustomerRegID))
                    .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                    .Take(10);
                foreach (var customerRegisterSummarizeGroup in customerRegisterSummarizeAdviceGroupByAdviceId.OrderBy(r => r.Key))
                {
                    pSender.Recordset.Append();
                    var field字段疾病名称 = pSender.Recordset.FieldByName("字段疾病名称");
                    if (field字段疾病名称 != null)
                    {
                        field字段疾病名称.Value = customerRegisterSummarizeGroup.First().SummarizeName.Trim();
                    }
                    var field字段性别 = pSender.Recordset.FieldByName("字段性别");
                    if (field字段性别 != null)
                    {
                        field字段性别.Value = "女性";
                    }
                    var field字段人数 = pSender.Recordset.FieldByName("字段人数");
                    if (field字段人数 != null)
                    {
                        field字段人数.Value = customerRegisterSummarizeGroup.Count();
                    }
                    pSender.Recordset.Post();
                }
            }
            else if (pSender.Name == "图表男性员工疾病检出前十")
            {
                var customerRegisterIdsMan =
                    _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Man).Select(r => r.Id);
                var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                    .Where(r => customerRegisterIdsMan.Contains(r.CustomerRegID))
                    .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                    .Take(10);
                foreach (var customerRegisterSummarizeGroup in customerRegisterSummarizeAdviceGroupByAdviceId.OrderBy(r => r.Key))
                {
                    pSender.Recordset.Append();
                    var field字段疾病名称 = pSender.Recordset.FieldByName("字段疾病名称");
                    if (field字段疾病名称 != null)
                    {
                        field字段疾病名称.Value = customerRegisterSummarizeGroup.First().SummarizeName.Trim();
                    }
                    var field字段性别 = pSender.Recordset.FieldByName("字段性别");
                    if (field字段性别 != null)
                    {
                        field字段性别.Value = "男性";
                    }
                    var field字段人数 = pSender.Recordset.FieldByName("字段人数");
                    if (field字段人数 != null)
                    {
                        field字段人数.Value = customerRegisterSummarizeGroup.Count();
                    }
                    pSender.Recordset.Post();
                }
            }
            else if (pSender.Name == "图表员工疾病检出前十")
            {
                var customerRegisterIdsMan =
                    _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Man).Select(r => r.Id);
                var customerRegisterIdsWoman =
                    _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Woman).Select(r => r.Id);
                var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                    .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                    .Take(10);
                foreach (var customerRegisterSummarizeGroup in customerRegisterSummarizeAdviceGroupByAdviceId.OrderBy(r => r.Key))
                {
                    pSender.Recordset.Append();
                    var field字段疾病名称 = pSender.Recordset.FieldByName("字段疾病名称");
                    if (field字段疾病名称 != null)
                    {
                        field字段疾病名称.Value = customerRegisterSummarizeGroup.First().SummarizeName.Trim();
                    }
                    var field字段性别 = pSender.Recordset.FieldByName("字段性别");
                    if (field字段性别 != null)
                    {
                        field字段性别.Value = "男性";
                    }
                    var field字段人数 = pSender.Recordset.FieldByName("字段人数");
                    if (field字段人数 != null)
                    {
                        field字段人数.Value = customerRegisterSummarizeGroup.Count(r => customerRegisterIdsMan.Contains(r.CustomerRegID));
                    }
                    pSender.Recordset.Post();

                    pSender.Recordset.Append();
                    var field字段疾病名称No1 = pSender.Recordset.FieldByName("字段疾病名称");
                    if (field字段疾病名称No1 != null)
                    {
                        field字段疾病名称No1.Value = customerRegisterSummarizeGroup.First().SummarizeName.Trim();
                    }
                    var field字段性别No1 = pSender.Recordset.FieldByName("字段性别");
                    if (field字段性别No1 != null)
                    {
                        field字段性别No1.Value = "女性";
                    }
                    var field字段人数No1 = pSender.Recordset.FieldByName("字段人数");
                    if (field字段人数No1 != null)
                    {
                        field字段人数No1.Value = customerRegisterSummarizeGroup.Count(r => customerRegisterIdsWoman.Contains(r.CustomerRegID));
                    }
                    pSender.Recordset.Post();
                }
            }
        }

        /// <summary>
        /// “全体员工健康问题分析及保健建议前十”子报表请求数据事件
        /// </summary>
        private void localVariable子报表全体员工健康问题分析及保健建议前十_Report_FetchRecord()
        {
            var localVariable子报表全体员工健康问题分析及保健建议前十控件 = _gridppReportMain.ControlByName("子报表全体员工健康问题分析及保健建议前十");
            if (localVariable子报表全体员工健康问题分析及保健建议前十控件 != null &&
                localVariable子报表全体员工健康问题分析及保健建议前十控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表全体员工健康问题分析及保健建议前十 = localVariable子报表全体员工健康问题分析及保健建议前十控件.AsSubReport;
                if (localVariable子报表全体员工健康问题分析及保健建议前十 != null)
                {
                    var detailGrid = localVariable子报表全体员工健康问题分析及保健建议前十.Report.DetailGrid;
                    if (detailGrid != null)
                    {
                        var customerRegisterIdsMan =
                            _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Man).Select(r => r.Id).ToList();
                        var customerRegisterIdsWoman =
                            _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Woman).Select(r => r.Id).ToList();
                        var customerRegisterListCount = _customerRegisterList.Count;
                        var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                            .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                            .Take(10);
                        foreach (var customerRegisterSummarizeAdvice in customerRegisterSummarizeAdviceGroupByAdviceId)
                        {
                            var groupManCustomerRegisterListCount =
                                customerRegisterSummarizeAdvice.Count(r =>
                                    customerRegisterIdsMan.Contains(r.CustomerRegID));
                            var groupWomanCustomerRegisterListCount =
                                customerRegisterSummarizeAdvice.Count(r =>
                                    customerRegisterIdsWoman.Contains(r.CustomerRegID));
                            var groupCount = customerRegisterSummarizeAdvice.Count();
                            foreach (var customerRegisterSummarize in customerRegisterSummarizeAdvice)
                            {
                                var currentCustomerRegister = _customerRegisterList.Find(r => r.Id == customerRegisterSummarize.CustomerRegID);
                                if (currentCustomerRegister != null)
                                {
                                    detailGrid.Recordset.Append();

                                    var field字段疾病名称 = detailGrid.Recordset.FieldByName("字段疾病名称");
                                    if (field字段疾病名称 != null)
                                    {
                                        field字段疾病名称.Value = customerRegisterSummarize.SummarizeName.Trim();
                                    }
                                    var field字段体检人姓名 = detailGrid.Recordset.FieldByName("字段体检人姓名");
                                    if (field字段体检人姓名 != null)
                                    {
                                        field字段体检人姓名.Value = currentCustomerRegister.Customer.Name;
                                    }
                                    var field字段体检人性别 = detailGrid.Recordset.FieldByName("字段体检人性别");
                                    if (field字段体检人性别 != null)
                                    {
                                        field字段体检人性别.Value = SexHelper.CustomSexFormatter(currentCustomerRegister.Customer.Sex);
                                    }
                                    var field字段体检人年龄 = detailGrid.Recordset.FieldByName("字段体检人年龄");
                                    if (field字段体检人年龄 != null)
                                    {
                                        field字段体检人年龄.Value = currentCustomerRegister.GetCustomerAge();
                                    }
                                    var field字段健康建议 = detailGrid.Recordset.FieldByName("字段健康建议");
                                    if (field字段健康建议 != null)
                                    {
                                        field字段健康建议.Value = customerRegisterSummarize.Advice;
                                    }
                                    var field字段体检号 = detailGrid.Recordset.FieldByName("字段体检号");
                                    if (field字段体检号 != null)
                                    {
                                        field字段体检号.Value = currentCustomerRegister.CustomerRegisterCode;
                                    }
                                    var field字段男性总人数 = detailGrid.Recordset.FieldByName("字段男性总人数");
                                    if (field字段男性总人数 != null)
                                    {
                                        field字段男性总人数.Value = customerRegisterIdsMan.Count;
                                    }
                                    var field字段女性总人数 = detailGrid.Recordset.FieldByName("字段女性总人数");
                                    if (field字段女性总人数 != null)
                                    {
                                        field字段女性总人数.Value = customerRegisterIdsWoman.Count;
                                    }
                                    var field字段总人数 = detailGrid.Recordset.FieldByName("字段总人数");
                                    if (field字段总人数 != null)
                                    {
                                        field字段总人数.Value = customerRegisterListCount;
                                    }
                                    var field字段分组男性人数 = detailGrid.Recordset.FieldByName("字段分组男性人数");
                                    if (field字段分组男性人数 != null)
                                    {
                                        field字段分组男性人数.Value = groupManCustomerRegisterListCount;
                                    }
                                    var field字段分组女性人数 = detailGrid.Recordset.FieldByName("字段分组女性人数");
                                    if (field字段分组女性人数 != null)
                                    {
                                        field字段分组女性人数.Value = groupWomanCustomerRegisterListCount;
                                    }
                                    var field字段男性分组人数比例 = detailGrid.Recordset.FieldByName("字段男性分组人数比例");
                                    if (field字段男性分组人数比例 != null)
                                    {
                                        field字段男性分组人数比例.Value = (customerRegisterIdsMan.Count == 0 ? 0 : (decimal)groupManCustomerRegisterListCount / customerRegisterIdsMan.Count).ToString("P");
                                    }
                                    var field字段女性分组人数比例 = detailGrid.Recordset.FieldByName("字段女性分组人数比例");
                                    if (field字段女性分组人数比例 != null)
                                    {
                                        field字段女性分组人数比例.Value = (customerRegisterIdsWoman.Count == 0 ? 0 : (decimal)groupWomanCustomerRegisterListCount / customerRegisterIdsWoman.Count).ToString("P");
                                    }
                                    var field字段分组人数比例 = detailGrid.Recordset.FieldByName("字段分组人数比例");
                                    if (field字段分组人数比例 != null)
                                    {
                                        field字段分组人数比例.Value = (customerRegisterListCount == 0 ? 0 : (decimal)groupCount / customerRegisterListCount).ToString("P");
                                    }
                                    var field字段分组人数 = detailGrid.Recordset.FieldByName("字段分组人数");
                                    if (field字段分组人数 != null)
                                    {
                                        field字段分组人数.Value = groupCount;
                                    }

                                    detailGrid.Recordset.Post();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// “男性员工健康问题分析及保健建议前十”子报表请求数据事件
        /// </summary>
        private void localVariable子报表男性员工健康问题分析及保健建议前十_Report_FetchRecord()
        {
            var localVariable子报表男性员工健康问题分析及保健建议前十控件 = _gridppReportMain.ControlByName("子报表男性员工健康问题分析及保健建议前十");
            if (localVariable子报表男性员工健康问题分析及保健建议前十控件 != null &&
                localVariable子报表男性员工健康问题分析及保健建议前十控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表男性员工健康问题分析及保健建议前十 = localVariable子报表男性员工健康问题分析及保健建议前十控件.AsSubReport;
                if (localVariable子报表男性员工健康问题分析及保健建议前十 != null)
                {
                    var detailGrid = localVariable子报表男性员工健康问题分析及保健建议前十.Report.DetailGrid;
                    if (detailGrid != null)
                    {
                        var customerRegisterIdsMan =
                            _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Man).Select(r => r.Id).ToList();
                        var customerRegisterListCount = _customerRegisterList.Count;
                        var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                            .Where(r => customerRegisterIdsMan.Contains(r.CustomerRegID))
                            .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                            .Take(10);
                        foreach (var customerRegisterSummarizeAdvice in customerRegisterSummarizeAdviceGroupByAdviceId)
                        {
                            var groupCount = customerRegisterSummarizeAdvice.Count();
                            foreach (var customerRegisterSummarize in customerRegisterSummarizeAdvice)
                            {
                                var currentCustomerRegister = _customerRegisterList.Find(r => r.Id == customerRegisterSummarize.CustomerRegID);
                                if (currentCustomerRegister != null)
                                {
                                    detailGrid.Recordset.Append();

                                    var field字段疾病名称 = detailGrid.Recordset.FieldByName("字段疾病名称");
                                    if (field字段疾病名称 != null)
                                    {
                                        field字段疾病名称.Value = customerRegisterSummarize.SummarizeName.Trim();
                                    }
                                    var field字段体检人姓名 = detailGrid.Recordset.FieldByName("字段体检人姓名");
                                    if (field字段体检人姓名 != null)
                                    {
                                        field字段体检人姓名.Value = currentCustomerRegister.Customer.Name;
                                    }
                                    var field字段体检人性别 = detailGrid.Recordset.FieldByName("字段体检人性别");
                                    if (field字段体检人性别 != null)
                                    {
                                        field字段体检人性别.Value = SexHelper.CustomSexFormatter(currentCustomerRegister.Customer.Sex);
                                    }
                                    var field字段体检人年龄 = detailGrid.Recordset.FieldByName("字段体检人年龄");
                                    if (field字段体检人年龄 != null)
                                    {
                                        field字段体检人年龄.Value = currentCustomerRegister.GetCustomerAge();
                                    }
                                    var field字段健康建议 = detailGrid.Recordset.FieldByName("字段健康建议");
                                    if (field字段健康建议 != null)
                                    {
                                        field字段健康建议.Value = customerRegisterSummarize.Advice;
                                    }
                                    var field字段体检号 = detailGrid.Recordset.FieldByName("字段体检号");
                                    if (field字段体检号 != null)
                                    {
                                        field字段体检号.Value = currentCustomerRegister.CustomerRegisterCode;
                                    }
                                    var field字段男性总人数 = detailGrid.Recordset.FieldByName("字段男性总人数");
                                    if (field字段男性总人数 != null)
                                    {
                                        field字段男性总人数.Value = customerRegisterIdsMan.Count;
                                    }
                                    var field字段总人数 = detailGrid.Recordset.FieldByName("字段总人数");
                                    if (field字段总人数 != null)
                                    {
                                        field字段总人数.Value = customerRegisterListCount;
                                    }
                                    var field字段分组男性人数 = detailGrid.Recordset.FieldByName("字段分组男性人数");
                                    if (field字段分组男性人数 != null)
                                    {
                                        field字段分组男性人数.Value = groupCount;
                                    }
                                    var field字段男性分组人数比例 = detailGrid.Recordset.FieldByName("字段男性分组人数比例");
                                    if (field字段男性分组人数比例 != null)
                                    {
                                        field字段男性分组人数比例.Value = (customerRegisterIdsMan.Count == 0 ? 0 : (decimal)groupCount / customerRegisterIdsMan.Count).ToString("P");
                                    }
                                    var field字段分组人数比例 = detailGrid.Recordset.FieldByName("字段分组人数比例");
                                    if (field字段分组人数比例 != null)
                                    {
                                        field字段分组人数比例.Value = (customerRegisterListCount == 0 ? 0 : (decimal)groupCount / customerRegisterListCount).ToString("P");
                                    }
                                    var field字段分组人数 = detailGrid.Recordset.FieldByName("字段分组人数");
                                    if (field字段分组人数 != null)
                                    {
                                        field字段分组人数.Value = groupCount;
                                    }

                                    detailGrid.Recordset.Post();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// “女性员工健康问题分析及保健建议前十”子报表请求数据事件
        /// </summary>
        private void localVariable子报表女性员工健康问题分析及保健建议前十_Report_FetchRecord()
        {
            var localVariable子报表女性员工健康问题分析及保健建议前十控件 = _gridppReportMain.ControlByName("子报表女性员工健康问题分析及保健建议前十");
            if (localVariable子报表女性员工健康问题分析及保健建议前十控件 != null &&
                localVariable子报表女性员工健康问题分析及保健建议前十控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表女性员工健康问题分析及保健建议前十 = localVariable子报表女性员工健康问题分析及保健建议前十控件.AsSubReport;
                if (localVariable子报表女性员工健康问题分析及保健建议前十 != null)
                {
                    var detailGrid = localVariable子报表女性员工健康问题分析及保健建议前十.Report.DetailGrid;
                    if (detailGrid != null)
                    {
                        var customerRegisterIdsWoman =
                            _customerRegisterList.Where(r => r.Customer.Sex == (int)Sex.Woman).Select(r => r.Id).ToList();
                        var customerRegisterListCount = _customerRegisterList.Count;
                        var customerRegisterSummarizeAdviceGroupByAdviceId = _customerRegisterSummarizeAdviceList
                            .Where(r => customerRegisterIdsWoman.Contains(r.CustomerRegID))
                            .GroupBy(r => r.SummarizeName.Trim()).OrderByDescending(r => r.Count())
                            .Take(10);
                        foreach (var customerRegisterSummarizeAdvice in customerRegisterSummarizeAdviceGroupByAdviceId)
                        {
                            var groupCount = customerRegisterSummarizeAdvice.Count();
                            foreach (var customerRegisterSummarize in customerRegisterSummarizeAdvice)
                            {
                                var currentCustomerRegister = _customerRegisterList.Find(r => r.Id == customerRegisterSummarize.CustomerRegID);
                                if (currentCustomerRegister != null)
                                {
                                    detailGrid.Recordset.Append();

                                    var field字段疾病名称 = detailGrid.Recordset.FieldByName("字段疾病名称");
                                    if (field字段疾病名称 != null)
                                    {
                                        field字段疾病名称.Value = customerRegisterSummarize.SummarizeName.Trim();
                                    }
                                    var field字段体检人姓名 = detailGrid.Recordset.FieldByName("字段体检人姓名");
                                    if (field字段体检人姓名 != null)
                                    {
                                        field字段体检人姓名.Value = currentCustomerRegister.Customer.Name;
                                    }
                                    var field字段体检人性别 = detailGrid.Recordset.FieldByName("字段体检人性别");
                                    if (field字段体检人性别 != null)
                                    {
                                        field字段体检人性别.Value = SexHelper.CustomSexFormatter(currentCustomerRegister.Customer.Sex);
                                    }
                                    var field字段体检人年龄 = detailGrid.Recordset.FieldByName("字段体检人年龄");
                                    if (field字段体检人年龄 != null)
                                    {
                                        field字段体检人年龄.Value = currentCustomerRegister.GetCustomerAge();
                                    }
                                    var field字段健康建议 = detailGrid.Recordset.FieldByName("字段健康建议");
                                    if (field字段健康建议 != null)
                                    {
                                        field字段健康建议.Value = customerRegisterSummarize.Advice;
                                    }
                                    var field字段体检号 = detailGrid.Recordset.FieldByName("字段体检号");
                                    if (field字段体检号 != null)
                                    {
                                        field字段体检号.Value = currentCustomerRegister.CustomerRegisterCode;
                                    }
                                    var field字段女性总人数 = detailGrid.Recordset.FieldByName("字段女性总人数");
                                    if (field字段女性总人数 != null)
                                    {
                                        field字段女性总人数.Value = customerRegisterIdsWoman.Count;
                                    }
                                    var field字段总人数 = detailGrid.Recordset.FieldByName("字段总人数");
                                    if (field字段总人数 != null)
                                    {
                                        field字段总人数.Value = customerRegisterListCount;
                                    }
                                    var field字段分组女性人数 = detailGrid.Recordset.FieldByName("字段分组女性人数");
                                    if (field字段分组女性人数 != null)
                                    {
                                        field字段分组女性人数.Value = groupCount;
                                    }
                                    var field字段女性分组人数比例 = detailGrid.Recordset.FieldByName("字段女性分组人数比例");
                                    if (field字段女性分组人数比例 != null)
                                    {
                                        field字段女性分组人数比例.Value = (customerRegisterIdsWoman.Count == 0 ? 0 : (decimal)groupCount / customerRegisterIdsWoman.Count).ToString("P");
                                    }
                                    var field字段分组人数比例 = detailGrid.Recordset.FieldByName("字段分组人数比例");
                                    if (field字段分组人数比例 != null)
                                    {
                                        field字段分组人数比例.Value = (customerRegisterListCount == 0 ? 0 : (decimal)groupCount / customerRegisterListCount).ToString("P");
                                    }
                                    var field字段分组人数 = detailGrid.Recordset.FieldByName("字段分组人数");
                                    if (field字段分组人数 != null)
                                    {
                                        field字段分组人数.Value = groupCount;
                                    }

                                    detailGrid.Recordset.Post();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// “未到检人员名单”子报表请求数据事件
        /// </summary>
        private void localVariable子报表未到检人员名单_Report_FetchRecord()
        {
            var localVariable子报表未到检人员名单控件 = _gridppReportMain.ControlByName("子报表未到检人员名单");
            if (localVariable子报表未到检人员名单控件 != null &&
                localVariable子报表未到检人员名单控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表未到检人员名单 = localVariable子报表未到检人员名单控件.AsSubReport;
                if (localVariable子报表未到检人员名单 != null)
                {
                    var detailGrid = localVariable子报表未到检人员名单.Report.DetailGrid;
                    if (detailGrid != null)
                    {
                        var customerRegisterListNo1 = _customerRegisterList.Where(r => r.CheckSate == null || r.CheckSate == (int)PhysicalEState.Not).ToList();
                        foreach (var customerRegister in customerRegisterListNo1)
                        {
                            detailGrid.Recordset.Append();
                            var field字段体检号 = detailGrid.Recordset.FieldByName("字段体检号");
                            if (field字段体检号 != null)
                            {
                                field字段体检号.Value = customerRegister.CustomerRegisterCode;
                            }
                            var field字段姓名 = detailGrid.Recordset.FieldByName("字段姓名");
                            if (field字段姓名 != null)
                            {
                                field字段姓名.Value = customerRegister.Customer.Name;
                            }
                            var field字段性别 = detailGrid.Recordset.FieldByName("字段性别");
                            if (field字段性别 != null)
                            {
                                field字段性别.Value = SexHelper.CustomSexFormatter(customerRegister.Customer.Sex);
                            }
                            var field字段年龄 = detailGrid.Recordset.FieldByName("字段年龄");
                            if (field字段年龄 != null)
                            {
                                field字段年龄.Value = customerRegister.GetCustomerAge();
                            }
                            var field字段电话 = detailGrid.Recordset.FieldByName("字段电话");
                            if (field字段电话 != null)
                            {
                                field字段电话.Value = customerRegister.Customer.Mobile;
                            }
                            detailGrid.Recordset.Post();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// “未检项目汇总”子报表请求数据事件
        /// </summary>
        private void localVariable子报表未检项目汇总_Report_FetchRecord()
        {
            var localVariable子报表未检项目汇总控件 = _gridppReportMain.ControlByName("子报表未检项目汇总");
            if (localVariable子报表未检项目汇总控件 != null &&
                localVariable子报表未检项目汇总控件.ControlType == GRControlType.grctSubReport)
            {
                var localVariable子报表未检项目汇总 = localVariable子报表未检项目汇总控件.AsSubReport;
                if (localVariable子报表未检项目汇总 != null)
                {
                    var detailGrid = localVariable子报表未检项目汇总.Report.DetailGrid;
                    if (detailGrid != null)
                    {
                        var customerRegisterIds = _customerRegisterList.Where(r =>
                            r.CheckSate != null && r.CheckSate != (int)PhysicalEState.Not).Select(r => r.Id).ToList();
                        var itemGroupGroupByCustomerRegisterList = _customerRegisterItemGroupList.Where(r =>
                            r.CustomerRegBMId != null && customerRegisterIds.Contains(r.CustomerRegBMId.Value) &&
                            r.CheckState != (int)ProjectIState.Complete &&
                            r.CheckState != (int)ProjectIState.Part).GroupBy(r => r.CustomerRegBMId);
                        foreach (var customerRegisterItemGroupGroupByCustomerRegister in itemGroupGroupByCustomerRegisterList)
                        {
                            var customerRegister = _customerRegisterList.Find(r =>
                                r.Id == customerRegisterItemGroupGroupByCustomerRegister.Key);
                            detailGrid.Recordset.Append();
                            var field字段体检号 = detailGrid.Recordset.FieldByName("字段体检号");
                            if (field字段体检号 != null)
                            {
                                field字段体检号.Value = customerRegister.CustomerRegisterCode;
                            }
                            var field字段姓名 = detailGrid.Recordset.FieldByName("字段姓名");
                            if (field字段姓名 != null)
                            {
                                field字段姓名.Value = customerRegister.Customer.Name;
                            }
                            var field字段性别 = detailGrid.Recordset.FieldByName("字段性别");
                            if (field字段性别 != null)
                            {
                                field字段性别.Value = SexHelper.CustomSexFormatter(customerRegister.Customer.Sex);
                            }
                            var field字段年龄 = detailGrid.Recordset.FieldByName("字段年龄");
                            if (field字段年龄 != null)
                            {
                                field字段年龄.Value = customerRegister.GetCustomerAge();
                            }
                            var field字段未检项目 = detailGrid.Recordset.FieldByName("字段未检项目");
                            if (field字段未检项目 != null)
                            {
                                field字段未检项目.Value = string.Join("；",
                                    customerRegisterItemGroupGroupByCustomerRegister.Select(r => r.ItemGroupName));
                            }
                            detailGrid.Recordset.Post();
                        }
                    }
                }
            }
        }
    }
}