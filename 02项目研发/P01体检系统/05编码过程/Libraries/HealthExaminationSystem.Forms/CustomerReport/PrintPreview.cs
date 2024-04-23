using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using BT_Word_JobLibrary;
using DevExpress.Diagram.Core.Native.Ribbon;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.UserSettings.DynamicColumnDirectory;

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class PrintPreview : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;

        private readonly IPrintPreviewAppService _printPreviewAppService;
        private readonly ICustomerReportAppService customerReportAppService;
        private readonly IInspectionTotalAppService _inspectionTotalService;
        /// <summary>
        /// 表格视图标识
        /// </summary>
        private const string CurrentGridViewId = "A81FF9AB-893C-45CE-81E3-2D330FCEAAF4";

        public PrintPreview()
        {
            _inspectionTotalService = new InspectionTotalAppService();
            _printPreviewAppService = new PrintPreviewAppService();
            _commonAppService = new CommonAppService();
            customerReportAppService = new CustomerReportAppService();
            InitializeComponent();

            gridViewCustomerReg.Columns[gridColumnCustomerRegSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCustomerReg.Columns[gridColumnCustomerRegSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);

            //repositoryItemLookUpEdit1.DataSource = PrintSateHelper.GetSexModels();
            repositoryItemLookUpEdit2.DataSource = PrintSateHelper.GetSexModels();
            repositoryItemLookUpEditPrintSate.DataSource = PrintSateHelper.GetSexModels();
            repositoryItemLookUpEditSummSate.DataSource = SummSateHelper.GetSelectList();
            repositoryItemLookUpEditPhysicalType.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
          
            repositoryItemLookUpEditCheckSate.DataSource = PhysicalEStateHelper.YYGetList();
        }    
        private void PrintPreview_Load(object sender, EventArgs e)
        {
            DynamicColumnConfigurationHelper.LoadGridViewDynamicColumnConfiguration(CurrentGridViewId, gridViewCustomerReg);

            //复查状态
            var reviewStates = ReviewSateTypeHelper.GetAllReviewStates();
            checkReview.Properties.DataSource = reviewStates;
            //隐私报告
            var list1 = new List<EnumModel>();
            list1.Add(new EnumModel { Id = 1, Name = "仅隐私报告" });
            list1.Add(new EnumModel { Id = 2, Name = "个人报告(不含隐私)" });
            list1.Add(new EnumModel { Id = 3, Name = "全部" });
            comboBoxEdit1.Properties.DataSource = list1;
            comboBoxEdit1.EditValue = 3;

            // 加载性别数据
            lookUpEditSex.Properties.DataSource = SexHelper.GetSexModelsForItemInfo();
            lookUpEditSex.EditValue = (int)Sex.GenderNotSpecified;

            // 加载打印状态数据
            lookUpEditPrintSate.Properties.DataSource = PrintSateHelper.GetSexModels();
            lookUpEditPrintSate.EditValue = (int)PrintSate.NotToPrint;

            // 加载单位数据
            searchLookUpEditCompany.Properties.DataSource = _printPreviewAppService.GetClientInfos();

            // 加载体检类别数据
            // lookUpEditExaminationCategories.Properties.DataSource = ExaminationCategoryHelper.GetExaminationCategories();
          var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            BasicDictionaryDto all = new BasicDictionaryDto();
            all.Value = -1;
            all.Text = "全部";
            Examination.Add(all);
            lookUpEditExaminationCategories.Properties.DataSource = Examination;
            // 加载体检状态数据
            lookUpEditExaminationStatus.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            //体检类别
            lookUpEditCustomerType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList();

            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();
            //存入状态
           concatstate.Properties.DataSource=  CabinetHelper.GetCRModels();

            // 加载打印模板
            var MBNamels= DefinedCacheHelper
                .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            string[] mbls = MBNamels.Split('|');
            List<string> list = new List<string>();
            list.Add("根据体检类别匹配");
            if (Variables.ISZYB != "2")
            {
                foreach (string mb in mbls)
                {
                    if (mb != "")
                    {
                        list.Add(mb);
                        // listBoxControlTemplates.Items.Add(mb);
                    }
                }
                // 加载对比报告模板
                var db = DefinedCacheHelper
                    .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1)?.Remarks;
                if (!string.IsNullOrEmpty(db))
                {
                    list.Add(db);

                }
            }
            // 加载职业健康报告模板
            var zybmb = DefinedCacheHelper
                .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2)?.Remarks;
            if (!string.IsNullOrEmpty(zybmb))
            {
                list.Add(zybmb);

            }
            // 加载打印模板
            //list.Add(DefinedCacheHelper
            //    .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks);
            listBoxControlTemplates.Properties.DataSource = list;

            if (list.Count > 0)
            {
                listBoxControlTemplates.EditValue = list[0];
            }
            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.DateTime = date;
            dateEditStart.DateTime = date;


            gridViewCustomerReg.Columns[conReportMessageState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridViewCustomerReg.Columns[conReportMessageState.FieldName].DisplayFormat.Format = new CustomFormatter(ShortMessageStateHelper.ShortMessageStateFormatter);


            gridViewCustomerReg.Columns[conReviewSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCustomerReg.Columns[conReviewSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(ReviewSateTypeHelper.ReviewFormatter);

        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var name = textEditName.Text.Trim();
                var customerBm = textEditCustomerBm.Text.Trim();
                var idCardNo = textEditIdCardNo.Text.Trim();
                var FPName = txtFp.Text.Trim();
                var sex = lookUpEditSex.EditValue as int?;
                var clientRegId = searchLookUpEditClientReg.EditValue as Guid?;
                var clientId= searchLookUpEditCompany.EditValue as Guid?;
                var printSate = lookUpEditPrintSate.EditValue as int?;
                
                var examinationCategory = lookUpEditExaminationCategories.EditValue as int?;
                if (examinationCategory.HasValue && examinationCategory == -1)
                {
                    examinationCategory = null;
                }
                var examinationStatus = lookUpEditExaminationStatus.EditValue as int?;
                var sumstatus = lookUpEditSumStatus.EditValue as int?;
                var input = new SearchCustomerRegForPrintPreviewDto
                {
                    Name = name,
                    CustomerBM = customerBm,
                    IdCardNo = idCardNo,
                    Sex = sex,
                    ClientRegId = clientRegId,
                    PrintSate = printSate,
                    CheckSate = examinationStatus,
                    PhysicalType = examinationCategory,
                    SummSate = sumstatus,
                     ClientId= clientId,
                      FPName= FPName


                };
                if (checkEditDisabledDate.Checked)
                {
                    input.StartDate = dateEditStart.DateTime;
                    input.EndtDate = dateEditEnd.DateTime;
                    input.DateType = 1;
                    if (comTimeType.Text.Contains("审核"))
                    {
                        input.DateType = 2;

                    }
                    if (comTimeType.Text.Contains("体检"))
                    {
                        input.DateType = 3;

                    }
                    if (comTimeType.Text.Contains("报告"))
                    {
                        input.DateType = 4;

                    }
                }
                if (checkZYB.Checked == true)
                {
                    input.ISZY = 1;
                }
                if (concatstate.EditValue != null && concatstate.EditValue.ToString() == "1")
                {
                    input.CusCabitState = 1;
                }
                if (concatstate.EditValue != null && concatstate.EditValue.ToString() == "0")
                {
                    input.CusCabitState = 0;
                }
                if (dtcatStar.EditValue != null && dtcatEnd.EditValue != null)
                {
                    input.EndCabitTime = dtcatStar.DateTime;
                    input.StarCabitTime = dtcatEnd.DateTime;
                }
                if (butcat.EditValue != null)
                {
                    input.CusCabitBM = butcat.EditValue.ToString();
                }
                if (!string.IsNullOrEmpty(lookUpEditCustomerType.EditValue?.ToString()))
                {
                    input.CustomerType = (int)lookUpEditCustomerType.EditValue;
                }
                if (!string.IsNullOrEmpty(checkReview.EditValue?.ToString()) && checkReview.EditValue?.ToString() != "0")
                {
                    input.ReviewSate = (int)checkReview.EditValue;
                }
                var data = _printPreviewAppService.GetCustomerRegs(input);
                if (checkAll.Checked == true)
                {
                    var cusreg = gridControlCustomerReg.DataSource as List<CustomerRegForPrintPreviewDto>;
                    if (cusreg != null)
                    {
                        var regid = cusreg.Select(p=>p.Id).ToList();
                        data = data.Where(p=> !regid.Contains(p.Id)).ToList();
                        cusreg.AddRange(data);
                        gridControlCustomerReg.DataSource = cusreg;
                        gridViewCustomerReg.BestFitColumns();
                    }
                    else
                    {
                        gridControlCustomerReg.DataSource = data;
                        gridViewCustomerReg.BestFitColumns();
                    }
                }
                else
                {
                    gridControlCustomerReg.DataSource = data;
                    gridViewCustomerReg.BestFitColumns();
                }
                if (checkAll.Checked == true)
                {
                    textEditCustomerBm.SelectAll();

                }
            });
        }

        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            textEditName.ResetText();
            textEditCustomerBm.ResetText();
            textEditIdCardNo.ResetText();
            lookUpEditSex.EditValue = (int)Sex.GenderNotSpecified;
            searchLookUpEditCompany.EditValue = null;
            lookUpEditPrintSate.EditValue = (int)ReportState.AllowPrint;
            lookUpEditExaminationCategories.EditValue = null;
            lookUpEditExaminationStatus.EditValue = null;
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.DateTime = date;
            dateEditStart.DateTime = date.AddDays(-30);
        }

        private void searchLookUpEditCompany_EditValueChanged(object sender, EventArgs e)
        {
            // 加载单位预约信息
            if (searchLookUpEditCompany.EditValue == null || searchLookUpEditCompany.EditValue.Equals(string.Empty))
            {
                searchLookUpEditClientReg.EditValue = null;
                searchLookUpEditClientReg.Properties.DataSource = null;
            }
            else
            {
                var id = (Guid)searchLookUpEditCompany.EditValue;
                var regs = _printPreviewAppService.GetClientRegs(new EntityDto<Guid> { Id = id });
                searchLookUpEditClientReg.Properties.DataSource = regs;
                if (regs.Count != 0)
                {
                    searchLookUpEditClientReg.EditValue = regs.First().Id;
                }
            }
        }

        private void simpleButtonPrintPreview_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {
                //XtraMessageBox.Show("试用版本，不能预览报告！");
                //return;
            }
            var selectIndexes = gridViewCustomerReg.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                var id = (Guid)gridViewCustomerReg.GetRowCellValue(selectIndexes[0], gridColumnCustomerRegId);
                //表格体检
                if (listBoxControlTemplates.EditValue.ToString().Contains("表格"))
                {
                    var printReport = new BGPrintReport();
                    var cusNameInput = new CusNameInput { Id = id };
                    printReport.cusNameInput = cusNameInput;
                    printReport.Print(true, listBoxControlTemplates.EditValue.ToString(), "");
                }
                else
                {
                    var printReport = new PrintReportNew();
                    
                         
                        //if (!radioGroupPrintOption.EditValue.Equals(0))
                        //{
                        //    printReport.StrReportTemplateName = "000";
                        //}
                        int? ispr = (int?)comboBoxEdit1.EditValue;
                        var TempName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
                        var cusNameInput = new CusNameInput { Id = id, PrivacyState = ispr };
                        //var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        if (listBoxControlTemplates.EditValue.ToString() == TempName)
                        {
                            printReport.islndb = true;
                        }
                    if (listBoxControlTemplates.EditValue.ToString() == "根据体检类别匹配")
                    {
                        if (checkEditReview.Checked == true)
                        { printReport.Print(true, "", "","0",0,false,true); }
                        else
                        {
                            printReport.Print(true, "", "");
                        }

                        //printReport.Print(true,  "","",  "0", 0, true);
                    }
                    else
                    {
                        #region word报告
                        if (listBoxControlTemplates.EditValue.ToString().Contains(".doc") ||
                           listBoxControlTemplates.EditValue.ToString().Contains(".doc"))
                        {
                            try
                            {
                                var customBM = gridViewCustomerReg.GetRowCellValue(selectIndexes[0], gridColumnCustomerRegCustomerBM)?.ToString();

                                this.Cursor = Cursors.WaitCursor;
                                Commonprintdocword(customBM, "", false , true);

                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                this.Cursor = Cursors.Default;
                            }


                        }
                        #endregion
                        else
                        {
                            if (checkEditReview.Checked == true)
                            { printReport.Print(true, "", listBoxControlTemplates.EditValue.ToString(), "0", 0, false, true); }
                            else
                            {
                                printReport.Print(true, "", listBoxControlTemplates.EditValue.ToString());
                            }
                        }
                    }
                    
                }
            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }

        private void simpleButtonPrintAll_Click(object sender, EventArgs e)
        {
            if (gridControlCustomerReg.DataSource is List<CustomerRegForPrintPreviewDto> data && data.Count != 0)
            {
                foreach (var customerRegForPrintPreviewDto in data)
                {
                    var printReport = new PrintReportNew();
                    //if (!radioGroupPrintOption.EditValue.Equals(0))
                    //{
                    //    printReport.StrReportTemplateName = "000";
                    //}
                    var cusNameInput = new CusNameInput { Id = customerRegForPrintPreviewDto.Id };
                    printReport.cusNameInput = cusNameInput;
                    printReport.Print(false);
                    //if (!radioGroupPrintOption.EditValue.Equals(1))
                    //{
                    //    string mb = "";
                    //    if (!string.IsNullOrEmpty(listBoxControlTemplates.EditValue?.ToString()) && listBoxControlTemplates.EditValue.ToString().Contains("职业"))
                    //    { mb = "职业"; }
                    //    _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM { Id = customerRegForPrintPreviewDto.Id, Name= mb });
                    //}
                }
                simpleButtonQuery.PerformClick();
            }
            else
            {
                ShowMessageBoxWarning("没有任何数据！");
            }
        }
        private void printword(string customBM,string path,Guid regId)
        {


            var GrdPath = GridppHelper.GetTemplate(listBoxControlTemplates.EditValue.ToString());
            string args = customBM + "|" + GrdPath + "|" + path + "|" + "True";
            var reportpath = AppDomain.CurrentDomain.BaseDirectory + "\\报告查询";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = reportpath + "\\SearchSelf.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.Start();

            string mb = "";
            //if (iszjj == true)
            //{ mb = "职业"; }
            _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM { Id = regId, Name = mb });

        }

        private async Task Commonprintword(string customBM, string path,Guid regID)
        {
            await Task.Run(() => printword(customBM, path, regID));
        }
        private async Task Commonprintdocword(string customBM, string path,bool bIsPrint, bool bIsView )
        {
            await Task.Run(async () =>
            {
                path = path + "\\" + customBM +".doc";
                var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 30)?.Remarks;
                if (printName == null)
                {
                    printName = "";
                }
                var GrdPath = GridppHelper.GetTemplate(listBoxControlTemplates.EditValue.ToString());
                //string args = customBM + "|" + GrdPath + "|" + path + "|" + "True";
                WordHelper oHelper = new WordHelper();
                ReturnClass oRtn =await  oHelper.ExcuteWordAsync(customBM, GrdPath, path, bIsPrint, bIsView, printName);
                if (oRtn.IsSucceeded())
                { }
                else
                {
                    MessageBox.Show(oRtn.message);
                }
            });
        }
       
        private void simpleButtonPrintSelects_Click(object sender, EventArgs e)
        {
            //    if (Variables.ISReg == "0")
            //    {
            //        XtraMessageBox.Show("试用版本，不能打印报告！");
            //        return;
            //    }
            var selectIndexes = gridViewCustomerReg.GetSelectedRows();
            string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 300)?.Remarks;
            if (selectIndexes.Length != 0)
            {
                foreach (var index in selectIndexes)
                {
                    var id = (Guid)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegId);
                    var printstate = gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegPrintSate)?.ToString();
                    if (printstate == "2")
                    {
                        DialogResult dr = XtraMessageBox.Show("已打印过报告是否继续打印？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.Cancel)
                        {
                            continue;
                        }
                    }                    
                    var sumtstate = gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegSummSate)?.ToString();

                    if (!string.IsNullOrEmpty(strwjshow) && strwjshow == "Y")
                    {
                        if (int.Parse(sumtstate) != (int)SummSate.Audited)
                        {
                            MessageBox.Show("未审核不能打印报告！");
                            continue;
                        }
                    }
                    //表格体检
                    if (listBoxControlTemplates.EditValue.ToString().Contains("表格"))
                    {
                        var printReport = new BGPrintReport();
                        var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        printReport.Print(false,  listBoxControlTemplates.EditValue.ToString(),"");
                    }
                    else
                    {
                        var printReport = new PrintReportNew();
                        //if (!radioGroupPrintOption.EditValue.Equals(0))
                        //{
                        //    printReport.StrReportTemplateName = "000";
                        //}
                        var TempName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
                        var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        if (listBoxControlTemplates.EditValue.ToString() == TempName)
                        {
                            printReport.islndb = true;
                        }
                        if (listBoxControlTemplates.EditValue.ToString() == "根据体检类别匹配")
                        {
                            if (checkEditReview.Checked == true)
                            {
                                printReport.Print(false, "", "", "0", 0, false, true);
                            }
                            else
                            {
                                printReport.Print(false, "", "");
                            }
                        }
                        else
                        {
                            #region word报告
                            if (listBoxControlTemplates.EditValue.ToString().Contains(".rdlx") ||
                                listBoxControlTemplates.EditValue.ToString().Contains(".rpx"))
                            {
                                var customBM = gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM)?.ToString();

                                Commonprintword(customBM, "",id);
                            }
                            #endregion
                            #region word报告
                            else if (listBoxControlTemplates.EditValue.ToString().Contains(".doc") ||
                                 listBoxControlTemplates.EditValue.ToString().Contains(".doc"))
                            {
                                try
                                {
                                    var customBM = gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM)?.ToString();
                                    var Name = gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM)?.ToString();

                                    this.Cursor = Cursors.WaitCursor;
                                    Commonprintdocword(customBM, "", true, false);
                                    //更新打印状态
                                    string mb = "";                                   
                                    _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM { Id = id, Name = mb });
                                    
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    this.Cursor = Cursors.Default;
                                }


                            }
                            #endregion
                            else
                            {
                                if (checkEditReview.Checked == true)
                                {
                                    printReport.Print(false, "", listBoxControlTemplates.EditValue.ToString(), "0", 0, false, true);
                                }
                                else
                                {
                                    printReport.Print(false, "", listBoxControlTemplates.EditValue.ToString());
                                }
                            }
                        }
                    }
                   
                }
                simpleButtonQuery.PerformClick();
            }
            else
            {
                ShowMessageBoxWarning("没有选择任何数据！");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {//
                //XtraMessageBox.Show("试用版本，不能导出报告！");
             //  return;
            }
            var selectIndexes = gridViewCustomerReg.GetSelectedRows();
            bool isok = false;
            if (selectIndexes.Length != 0)
            {
                string path = "";
                if (Shell.BrowseForFolder("请选择文件夹！", out path) != DialogResult.OK)
                    return;
                string pathold = path;
                foreach (var index in selectIndexes)
                {
                    var id = (Guid)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegId);
                    var cusBM = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM);
                    var Name = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegName);
                    var formatName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 202);
                    string FileName = "";
                    if (formatName != null && formatName.Text != "")
                    {

                        var cusinto = gridViewCustomerReg.GetRow(index) as CustomerRegForPrintPreviewDto;
                        string fsex = "女";
                        if (cusinto.Customer.Sex == 1)
                        {
                            fsex = "男";
                        }
                            FileName = formatName.Text.Replace("【档案号】", cusinto.Customer.ArchivesNum).Replace(
                            "【体检号】", cusBM).Replace("【年龄】",cusinto.Age.ToString()).Replace(
                            "【性别】", fsex).Replace("【姓名】", Name).Replace("【电话】", cusinto.Customer.Mobile).Replace
                            ("【身份证号】",cusinto.Customer.IDCardNo).Replace("【就诊号】", cusinto.Customer.VisitCard).
                            Replace("【登记日期】", cusinto.LoginDate?.ToString("yyyy-MM-dd")).
                            Replace("【体检日期】", cusinto.BookingDate?.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        FileName = Name + "-" + cusBM;
                    }
                    string strnewpath = path + "\\" + FileName;
                    //表格体检
                    if (listBoxControlTemplates.EditValue.ToString().Contains("表格"))
                    {
                        var printReport = new BGPrintReport();
                        var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        printReport.Print(false, listBoxControlTemplates.EditValue.ToString(), strnewpath);
                    }
                    else
                    {
                        #region word报告
                        if (listBoxControlTemplates.EditValue.ToString().Contains(".rdlx") ||
                            listBoxControlTemplates.EditValue.ToString().Contains(".rpx"))
                        {
                            var customBM = gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM)?.ToString();

                            Commonprintword(customBM, path,id);


                        }
                        #endregion
                        #region word报告
                        else if (listBoxControlTemplates.EditValue.ToString().Contains(".doc") ||
                             listBoxControlTemplates.EditValue.ToString().Contains(".doc"))
                        {
                            try
                            {
                                var customBM = gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM)?.ToString();

                                this.Cursor = Cursors.WaitCursor;
                                Commonprintdocword(customBM, path, false, false);

                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                this.Cursor = Cursors.Default;
                            }


                        }
                        #endregion
                        else
                        {

                            var printReport = new PrintReportNew();

                            //if (!radioGroupPrintOption.EditValue.Equals(0))
                            //{
                            //    printReport.StrReportTemplateName = "000";
                            //}
                            var TempName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
                            var cusNameInput = new CusNameInput { Id = id };
                            printReport.cusNameInput = cusNameInput;
                            if (listBoxControlTemplates.EditValue.ToString() == TempName)
                            {
                                printReport.islndb = true;
                            }

                            if (listBoxControlTemplates.EditValue.ToString() == "根据体检类别匹配")
                            {
                                if (checkEditReview.Checked == true)
                                {
                                    printReport.Print(false, strnewpath, "", "0", 0, false, true);
                                }
                                else
                                {
                                    printReport.Print(false, strnewpath, "");
                                }
                            }
                            else
                            {
                                if (checkEditReview.Checked == true)
                                {
                                    printReport.Print(false, strnewpath, listBoxControlTemplates.EditValue.ToString(), "0", 0, false, true);
                                }
                                else
                                {
                                    printReport.Print(false, strnewpath, listBoxControlTemplates.EditValue.ToString());
                                }
                            }
                        }
                        
                        
                    }
                    //if (!radioGroupPrintOption.EditValue.Equals(1))
                    //{
                        string mbname = "";
                        if (listBoxControlTemplates.EditValue.ToString().Contains("职业"))
                        {
                            mbname = "职业";
                        }
                        _printPreviewAppService.UpdateCustomerSumPrintState(new ChargeBM { Id = id, Name= mbname });
                        
                    //}
                    isok = true;
                }
                if (isok)
                {
                    MessageBox.Show("导出成功！");
                }
                simpleButtonQuery.PerformClick();
            }
            else
            {
                ShowMessageBoxWarning("没有选择任何数据！");
            }
        }
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        public class Shell
        {
            private class Win32
            {
                public const int _MAX_PATH = 260;
                public const uint BIF_EDITBOX = 0x0010;
                public const uint BIF_NEWDIALOGSTYLE = 0x0040;

                public delegate int BFFCALLBACK(IntPtr/*HWND*/   hwnd, uint/*UINT*/   uMsg, IntPtr/*LPARAM*/   lParam, IntPtr/*LPARAM*/   lpData);

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct BROWSEINFO
                {
                    public IntPtr/*HWND*/   _hwndOwner;
                    public IntPtr/*LPCITEMIDLIST*/   _pidlRoot;
                    [MarshalAs(UnmanagedType.LPStr)]
                    public string/*LPTSTR*/   _szDirectory;
                    [MarshalAs(UnmanagedType.LPStr)]
                    public string/*LPCTSTR*/   _lpszTitle;
                    public uint/*UINT*/   _ulFlags;
                    [MarshalAs(UnmanagedType.FunctionPtr)]
                    public BFFCALLBACK/*BFFCALLBACK*/   _lpfn;
                    public IntPtr/*LPARAM*/   _lParam;
                    public int/*int*/   _iImage;

                    public BROWSEINFO(IntPtr parent, string title)
                    {
                        _hwndOwner = parent;
                        _pidlRoot = (IntPtr)0;
                        _szDirectory = null;
                        _lpszTitle = title;
                        _ulFlags = BIF_EDITBOX | BIF_NEWDIALOGSTYLE;
                        _lpfn = (BFFCALLBACK)null;
                        _lParam = (IntPtr)0;
                        _iImage = 0;
                    }
                }

                [ComImport]
                [Guid("00000002-0000-0000-C000-000000000046")]
                [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
                internal interface IMalloc
                {
                    [PreserveSig]
                    IntPtr/*void   *   */   Alloc(ulong/*ULONG*/   cb);
                    [PreserveSig]
                    IntPtr/*void   *   */   Realloc(IntPtr/*void   *   */   pv, ulong/*ULONG*/   cb);
                    [PreserveSig]
                    void/*void*/   Free(IntPtr/*void   *   */   pv);
                    [PreserveSig]
                    ulong/*ULONG*/   GetSize(IntPtr/*void   *   */   pv);
                    [PreserveSig]
                    int/*int*/   DidAlloc(IntPtr/*void   *   */   pv);
                    [PreserveSig]
                    void/*void*/   HeapMinimize();
                }

                [DllImport("shell32.dll")]
                public static extern IntPtr/*LPITEMIDLIST*/   SHBrowseForFolder(ref BROWSEINFO/*LPBROWSEINFO*/   lpbi);

                [DllImport("shell32.dll")]
                public static extern bool/*BOOL*/   SHGetPathFromIDList(IntPtr/*LPCITEMIDLIST*/   pidl, StringBuilder/*LPTSTR*/   pszPath);

                [DllImport("Shell32.dll")]
                public static extern int/*HRESULT*/   SHGetMalloc([MarshalAs(UnmanagedType.IUnknown)]   out object   /*LPMALLOC   *   */   ppMalloc);
            }

            ///   <summary>   
            ///   Browse   for   a   shell   folder.   
            ///   </summary>   
            ///   <param   name="title">Title   to   display   in   dialogue   box</param>   
            ///   <param   name="path">Return   path</param>   
            ///   <returns>DialogResult.OK   if   successful</returns>   
            public static DialogResult BrowseForFolder(string title, out string path)
            {
                return BrowseForFolder((IntPtr)0, title, out path);
            }

            public static DialogResult BrowseForFolder(IntPtr parent, string title, out string path)
            {
                path = null;
                Win32.BROWSEINFO browseInfo = new Win32.BROWSEINFO(parent, title);
                IntPtr pidl = Win32.SHBrowseForFolder(ref browseInfo);
                if (pidl == IntPtr.Zero) { return DialogResult.Cancel; }
                try
                {
                    StringBuilder stringBuilder = new StringBuilder(Win32._MAX_PATH);
                    if (Win32.SHGetPathFromIDList(pidl, stringBuilder))
                    {
                        path = stringBuilder.ToString();
                        return DialogResult.OK;
                    }
                    else
                    {
                        return DialogResult.Cancel;
                    }
                }
                finally
                {
                    //   Free   memory   allocated   by   shell.   
                    object pMalloc = null;
                    //   Get   the   IMalloc   interface   and   free   the   block   of   memory.   
                    if (Win32.SHGetMalloc(out pMalloc) == 0/*NOERROR*/)
                    {
                        ((Win32.IMalloc)pMalloc).Free(pidl);
                    }
                }
            }
        }

        private void gridControlCustomerReg_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            
        }

        private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            CabitSelect cabitSelect = new CabitSelect();
            if (cabitSelect.ShowDialog() == DialogResult.OK)
            {
                butcat.EditValue = cabitSelect.conName;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridViewCustomerReg.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                CabitSelect cabitSelect = new CabitSelect();
                if (cabitSelect.ShowDialog() == DialogResult.OK)
                {

                    foreach (var index in selectIndexes)
                    {
                        var strid =  gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegId);
                        if (strid == null)
                        {
                            continue;
                        }
                        AutoLoading(() =>
                        {
                            var id = (Guid)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegId);
                            TjlCusCabitDto dto = new TjlCusCabitDto();
                            dto.CustomerRegId = id;
                            dto.CabitName = cabitSelect.conName;
                            dto.GetState = 1;
                            dto.ReportState = 1;
                            customerReportAppService.SaveTjlCabinet(dto);
                            CustomerUpCatDto catDto = new CustomerUpCatDto();
                            catDto.Id = id;
                            catDto.CusCabitBM = cabitSelect.conName;
                            catDto.CusCabitState = 1;
                            catDto.CusCabitTime = _commonAppService.GetDateTimeNow().Now;
                            var result = customerReportAppService.UpCustomerUpCat(catDto);                          
                        });
                        simpleButtonQuery.PerformClick();
                    }
                }
            }
        }

        private void repositoryItemHyperLinkEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
           
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
               
           
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (butcat.EditValue == null)
            {
                XtraMessageBox.Show("请选择存入柜子！");
                return;
            }
            frmSM frmSm = new frmSM();
            frmSm.CatName = butcat.EditValue.ToString();
            frmSm.ShowDialog();
        }

        private void gridViewCustomerReg_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "conCZCabitState")
            {
                //var selectIndexes = e.RowHandle;
                
                    var index = e.RowHandle;
                    var id = (Guid)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegId);
                var rowdata = gridControlCustomerReg.Views[0].GetRow(index) as CustomerRegForPrintPreviewDto;
                    var ckname = (string)gridViewCustomerReg.GetRowCellValue(index, conCZCabitState);
                    if (ckname == "存入")
                    {
                        CabitSelect cabitSelect = new CabitSelect();
                        if (cabitSelect.ShowDialog() == DialogResult.OK)
                        {


                            AutoLoading(() =>
                            {

                                TjlCusCabitDto dto = new TjlCusCabitDto();
                                dto.CustomerRegId = id;
                                dto.CabitName = cabitSelect.conName;
                                dto.GetState = 1;
                                dto.ReportState = 1;
                                customerReportAppService.SaveTjlCabinet(dto);
                                CustomerUpCatDto catDto = new CustomerUpCatDto();
                                catDto.Id = id;
                                catDto.CusCabitBM = cabitSelect.conName;
                                catDto.CusCabitState = 1;
                                catDto.CusCabitTime = _commonAppService.GetDateTimeNow().Now;
                                var result = customerReportAppService.UpCustomerUpCat(catDto);
                                rowdata.CusCabitBM = result.CusCabitBM;
                                rowdata.CusCabitState = result.CusCabitState;
                                rowdata.CusCabitTime = result.CusCabitTime;
                                gridViewCustomerReg.SetRowCellValue(index, conCusCabitBM, result.CusCabitBM);
                                gridViewCustomerReg.SetRowCellValue(index, conCusCabitTime, result.CusCabitTime);
                                gridViewCustomerReg.SetRowCellValue(index, conCusCabitState, "存入");
                                gridViewCustomerReg.SetRowCellValue(index, conCZCabitState, "取消存入");
                            });
                        gridViewCustomerReg.RefreshData();
                        gridControlCustomerReg.Refresh();

                        }
                    }
                    else if (ckname == "取消存入")
                    {
                        
                           
                                AutoLoading(() =>
                                {
                                    TjlCusCabitDto dto = new TjlCusCabitDto();
                                    dto.CustomerRegId = id;
                                    customerReportAppService.DelTjlCabinet(dto);
                                    CustomerUpCatDto catDto = new CustomerUpCatDto();
                                    catDto.Id = id;
                                    catDto.CusCabitBM = "";
                                    catDto.CusCabitState = 0;
                                    catDto.CusCabitTime = null;
                                    var result = customerReportAppService.UpCustomerUpCat(catDto);
                                    rowdata.CusCabitBM = result.CusCabitBM;
                                    rowdata.CusCabitState = result.CusCabitState;
                                    rowdata.CusCabitTime = result.CusCabitTime;
                                    gridViewCustomerReg.SetRowCellValue(index, conCusCabitBM, "");
                                    gridViewCustomerReg.SetRowCellValue(index, conCusCabitTime, "");
                                    gridViewCustomerReg.SetRowCellValue(index, conCusCabitState, "未存入");
                                    gridViewCustomerReg.SetRowCellValue(index, conCZCabitState, "存入");
                                });

                    gridViewCustomerReg.RefreshData();
                    gridControlCustomerReg.Refresh();
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {
                //XtraMessageBox.Show("试用版本，不能导出报告！");
                //return;
            }
            
            var ftpormatName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 201);
            if (ftpormatName == null)
            {
                MessageBox.Show("请在字典个人报告设置中，增加编号为201，备注为fpt地址的字典！");
                return;
            }
            if (ftpormatName.Remarks == "")
            {
                MessageBox.Show("请在字典个人报告设置中，编号为201的字段中维护ftp地址");
                return;
            }
           
            var selectIndexes = gridViewCustomerReg.GetSelectedRows();
            bool isok = false;
            if (selectIndexes.Length != 0)
            {
               
                string pathold = Directory.GetCurrentDirectory() +"\\PDF";
                // pathold = "ftp://zkcx-ftp:Huihui123@101.200.81.160/localuser/zkcx-ftp/";
                if (!Directory.Exists(pathold))
                {
                    Directory.CreateDirectory(pathold);
                }
                foreach (var index in selectIndexes)
                {
                    string ftppath = ftpormatName.Remarks;
                    var id = (Guid)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegId);
                    var cusBM = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM);
                    var Name = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegName);
                    //string strnewpath = pathold + Name + "-" + cusBM;
                    var formatName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 202);
                    string FileName = "";
                    if (formatName != null && formatName.Text != "")
                    {

                        var cusinto = gridViewCustomerReg.GetRow(index) as CustomerRegForPrintPreviewDto;
                        string fsex = "女";
                        if (cusinto.Customer.Sex == 1)
                        {
                            fsex = "男";
                        }
                        FileName = formatName.Text.Replace("【档案号】", cusinto.Customer.ArchivesNum).Replace(
                        "【体检号】", cusBM).Replace("【年龄】", cusinto.Age.ToString()).Replace(
                        "【性别】", fsex).Replace("【姓名】", Name).Replace("【电话】", cusinto.Customer.Mobile).Replace
                        ("【身份证号】", cusinto.Customer.IDCardNo);
                    }
                    else
                    {
                        FileName = Name + "-" + cusBM;
                    }
                    string strnewpath = pathold +"\\" +  FileName ;
                    ftppath +=  FileName + ".pdf";
                    //表格体检
                    if (listBoxControlTemplates.EditValue.ToString().Contains("表格"))
                    {
                        var printReport = new BGPrintReport();
                        var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        printReport.Print(false, listBoxControlTemplates.EditValue.ToString(), strnewpath);
                    }
                    else
                    {
                        var printReport = new PrintReportNew();

                        var TempName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
                        var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        if (listBoxControlTemplates.EditValue.ToString() == TempName)
                        {
                            printReport.islndb = true;
                        }
                        //printReport.Print(false, strnewpath, listBoxControlTemplates.EditValue.ToString(), "1");


                        #region MyRegion
                        if (listBoxControlTemplates.EditValue.ToString() == "根据体检类别匹配")
                        {
                            if (checkEditReview.Checked == true)
                            {
                                printReport.Print(false, strnewpath, "", "0", 0, false, true);
                            }
                            else
                            {
                                printReport.Print(false, strnewpath, "","0");
                            }
                        }
                        else
                        {
                            if (checkEditReview.Checked == true)
                            {
                                printReport.Print(false, strnewpath, listBoxControlTemplates.EditValue.ToString(), "0", 0, false, true);
                            }
                            else
                            {
                                printReport.Print(false, strnewpath, listBoxControlTemplates.EditValue.ToString(), "0");
                            }
                        }
                        #endregion


                    }
                    //if (!radioGroupPrintOption.EditValue.Equals(1))
                    //{
                        string mbname = "";
                        if (listBoxControlTemplates.EditValue.ToString().Contains("职业"))
                        {
                            mbname = "职业";
                        }
                        _printPreviewAppService.UpdateCustomerSumPrintState(new ChargeBM { Id = id, Name= mbname });
                    //}
                    //ftpDownload(ftppath.Replace(".pdf", ".tif"), strnewpath + ".tif");
                    ftpDownload(ftppath, strnewpath + ".pdf");
                    isok = true;
                }
                if (isok)
                {
                    MessageBox.Show("导出成功！");
                }
                simpleButtonQuery.PerformClick();
            }
            else
            {
                ShowMessageBoxWarning("没有选择任何数据！");
            }
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
                client.UploadFile(new Uri(URL), Path);
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

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            #region 发送短信
            IClientRegAppService _clientRegAppService = new ClientRegAppService(); // 预约仓储
            var MessageModle = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ShortModle.ToString() && o.Value == 2)?.Remarks;
            if (string.IsNullOrEmpty(MessageModle))
            {
                XtraMessageBox.Show("请在字典设置“短信模板”，编码为2中设置报告短信模板");
                return;
            }
            var rowList = gridViewCustomerReg.GetSelectedRows();
            foreach (var item in rowList)
            {
                if (gridViewCustomerReg.GetRow(item) is CustomerRegForPrintPreviewDto row)
                {
                    if (!string.IsNullOrEmpty(row.Customer.Mobile))
                    {
                        ShortMessageDto input = new ShortMessageDto();
                        input.Age = row.Customer.Age;
                        //input.CustomerId = row.CustomerId;
                        input.CustomerRegId = row.Id;
                        var shortMes = MessageModle.Replace("【姓名】", row.Customer.Name);
                        if (row.Customer.Sex == 1)
                        {
                            shortMes = shortMes.Replace("【性别】", "先生");
                        }
                        else if (row.Customer.Sex == 2)
                        {
                            shortMes = shortMes.Replace("【性别】", "女士");
                        }
                        shortMes = shortMes.Replace("【体检号】", row.CustomerBM);                       

                        input.Message = shortMes;
                        input.CustomerBM = row.CustomerBM;
                        input.MessageType = 2;
                        input.Mobile = row.Customer.Mobile;
                        input.Name = row.Customer.Name;
                        input.SendState = 0;
                        input.Sex = row.Customer.Sex;
                        _clientRegAppService.SaveMessage(input);
                    }
                }
            }
            #endregion
        }

        private void textEditCustomerBm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textEditCustomerBm.Text))
            {
              
                AutoLoading(() =>
                {

                    var customerBm = textEditCustomerBm.Text.Trim();                  
                    var input = new SearchCustomerRegForPrintPreviewDto
                    {                      
                        CustomerBM = customerBm,
                    };                   
                    var data = _printPreviewAppService.GetCustomerRegs(input) ;
                    if (checkAll.Checked == true)
                    {
                        var cusreg = gridControlCustomerReg.DataSource as List<CustomerRegForPrintPreviewDto>;
                        if (cusreg != null)
                        {
                            cusreg.AddRange(data);
                            gridControlCustomerReg.DataSource = cusreg;
                            gridViewCustomerReg.BestFitColumns();
                        }
                        else
                        {
                            gridControlCustomerReg.DataSource = data;
                            gridViewCustomerReg.BestFitColumns();
                        }
                    }
                    else
                    {
                        gridControlCustomerReg.DataSource = data;
                        gridViewCustomerReg.BestFitColumns();
                    }
                });
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
           // ExcelHelper.GridViewToExcel(gridViewCustomerReg, "人员列表", "人员列表");
            ExcelHelper.ExportToExcel("人员列表",gridControlCustomerReg);
        }

        private void lookUpEditCustomerType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {//
                //XtraMessageBox.Show("试用版本，不能导出报告！");
                //  return;
            }
            var selectIndexes = gridViewCustomerReg.GetSelectedRows();
            bool isok = false;
            if (selectIndexes.Length != 0)
            {
                string path = "";
                if (Shell.BrowseForFolder("请选择文件夹！", out path) != DialogResult.OK)
                    return;
                string pathold = path;
                foreach (var index in selectIndexes)
                {
                    var id = (Guid)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegId);
                    var cusBM = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegCustomerBM);
                    var Name = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnCustomerRegName);
                    var formatName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 202);
                    string FileName = "";
                    if (formatName != null && formatName.Text != "")
                    {

                        var cusinto = gridViewCustomerReg.GetRow(index) as CustomerRegForPrintPreviewDto;
                        string fsex = "女";
                        if (cusinto.Customer.Sex == 1)
                        {
                            fsex = "男";
                        }
                        FileName = formatName.Text.Replace("【档案号】", cusinto.Customer.ArchivesNum).Replace(
                        "【体检号】", cusBM).Replace("【年龄】", cusinto.Age.ToString()).Replace(
                        "【性别】", fsex).Replace("【姓名】", Name).Replace("【电话】", cusinto.Customer.Mobile).Replace
                        ("【身份证号】", cusinto.Customer.IDCardNo);
                    }
                    else
                    {
                        FileName = Name + "-" + cusBM;
                    }
                    string strnewpath = path + "\\" + FileName;
                    //表格体检
                    if (listBoxControlTemplates.EditValue.ToString().Contains("表格"))
                    {
                        var printReport = new BGPrintReport();
                        var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        printReport.Print(false, listBoxControlTemplates.EditValue.ToString(), strnewpath);
                    }
                    else
                    {
                        var printReport = new PrintReportNew();
                        //if (!radioGroupPrintOption.EditValue.Equals(0))
                        //{
                        //    printReport.StrReportTemplateName = "000";
                        //}
                        var TempName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
                        var cusNameInput = new CusNameInput { Id = id };
                        printReport.cusNameInput = cusNameInput;
                        if (listBoxControlTemplates.EditValue.ToString() == TempName)
                        {
                            printReport.islndb = true;
                        }
                        if (listBoxControlTemplates.EditValue.ToString() == "根据体检类别匹配")
                        {
                            printReport.Print(false, strnewpath, "");
                        }
                        else
                        {

                            printReport.Print(false, strnewpath, listBoxControlTemplates.EditValue.ToString());
                        }
                    }
                    //if (!radioGroupPrintOption.EditValue.Equals(1))
                    //{
                        string mbname = "";
                        if (listBoxControlTemplates.EditValue.ToString().Contains("职业"))
                        {
                            mbname = "职业";
                        }
                        _printPreviewAppService.UpdateCustomerSumPrintState(new ChargeBM { Id = id , Name= mbname });
                    //}
                    isok = true;
                }
                if (isok)
                {
                    MessageBox.Show("导出成功！");
                }
                simpleButtonQuery.PerformClick();
            }
            else
            {
                ShowMessageBoxWarning("没有选择任何数据！");
            }
        }

        private void gridControlCustomerReg_DoubleClick(object sender, EventArgs e)
        {
            
            var CustomerReg = gridViewCustomerReg.GetFocusedRow() as CustomerRegForPrintPreviewDto;
            if (CustomerReg == null)
                return;
            #region 调用总检界面            

            if (CustomerReg == null)
            {
                ShowMessageBoxWarning("请选中行！");
            }
            else
            {
                var input = new TjlCustomerQuery();
                input.CustomerRegID = CustomerReg.Id;

                var dto = _inspectionTotalService.GetCustomerRegList(input).FirstOrDefault();
                if (dto != null)
                {
                    var nowdate = _inspectionTotalService.Transformation(dto);
                    FrmInspectionTotal frmInspectionTotal = new FrmInspectionTotal(nowdate,true);
                    //{
                    //    frmInspectionTotal.isShow = true
                    //    //WindowState = FormWindowState.Maximized
                    //};
                    frmInspectionTotal.isShow = true;
                    frmInspectionTotal.Show();

                }
            }
            #endregion
        }

        private void simpleButton7_Click_1(object sender, EventArgs e)
        {
            WordHelper oHelper = new WordHelper();
            ReturnClass oRtn = oHelper.CreateNewAllMark();
            if (oRtn.IsSucceeded())
            {
                MessageBox.Show("已在路径【" + oRtn.data.ToString() + "】生成新文件。");
            }
            else
            {
                MessageBox.Show(oRtn.message);
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            using (var frm = new FormDynamicColumnConfiguration())
            {
                frm.CurrentGridViewId = CurrentGridViewId;
                frm.CurrentGridView = gridViewCustomerReg;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    DynamicColumnConfigurationHelper.LoadGridViewDynamicColumnConfiguration(CurrentGridViewId, gridViewCustomerReg);
                }
            }
        }
    }
}