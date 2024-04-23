using BT_Word_JobLibrary;
using DevExpress.XtraEditors;
using HealthExamination.HardwareDrivers;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class CustomerReportSend : UserBaseForm
    {
        private ICustomerReportAppService _service = new CustomerReportAppService();
        private IIdCardReaderHardwareDriver driver;
        private readonly IPrintPreviewAppService _printPreviewAppService;

        public CustomerReportSend()
        {
            InitializeComponent();
            driver = DriverFactory.GetDriver<IIdCardReaderHardwareDriver>();
            _printPreviewAppService = new PrintPreviewAppService();
            gridView.Columns[gridColumnState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView.Columns[gridColumnState.FieldName].DisplayFormat.Format = new CustomFormatter(StateFormatter);
        }
        private string StateFormatter(object obj)
        {
            int.TryParse(obj?.ToString(), out int val);
            switch (val)
            {
                case 0:
                    return "无";
                case 1:
                    return "已交接";
                case 2:
                    return "已发放";
            }
            return "";
        }

        #region 事件

        private void CustomerReportReceive_Load(object sender, EventArgs e)
        {
            ceIsScanCodeAutoSend.Checked = true;
            teSender.Text = CurrentUser.Name;
            var input = new CustomerReportQuery
            {
                CustomerReportState = 2,
            };
            input.StartHandoverDate = DateTime.Now.Date;
            input.EndHandoverDate = DateTime.Now.AddDays(1).AddDays(1);
            var output = _service.Query(input);
            gridControl.DataSource = output;
            // 加载打印模板
            var MBNamels = DefinedCacheHelper
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
            listBoxControlTemplates.Properties.DataSource = list;

            if (list.Count > 0)
            {
                listBoxControlTemplates.EditValue = list[0];
            }
        }

        private void CustomerReportReceive_Shown(object sender, EventArgs e)
        {
            teNumber.Focus();
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIDCardNo.Text))
            {
                Ok(txtIDCardNo.Text);
            }
            else
            {
                Ok();
            }
            teNumber.Text = string.Empty;
            teNumber.Focus();
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            teNumber.Text = "";
            teNumber.Focus();
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            Save();
            teNumber.Focus();
        }

        private void ceScanCodeAutoRecevice_CheckedChanged(object sender, EventArgs e)
        {
            sbSave.Enabled = !ceIsScanCodeAutoSend.Checked;
            teNumber.Focus();
        }
        private void CustomerReportSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            if (list != null && list.Any(m => m.State == 1))
            {
                var res = XtraMessageBox.Show("列表中还有未发放的报告，是否确认不再发放？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == nameof(CustomerReportFullDto.State))
            {
                if (int.TryParse(e.CellValue?.ToString(), out int val))
                {
                    switch (val)
                    {
                        //case 1:
                        //    {
                        //        e.Appearance.BackColor = Color.Yellow;
                        //        e.Appearance.ForeColor = Color.Red;
                        //    }
                        //    break;
                        case 2:
                            {
                               // e.Appearance.BackColor = Color.Lime;
                                e.Appearance.ForeColor = Color.ForestGreen;
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region 处理

        /// <summary>
        /// 确认流水号
        /// </summary>
        private void Ok(string IDCar="")
        {
            dxErrorProvider.ClearErrors();
            string receiver = teReceiver.Text.Trim();
            //if (string.IsNullOrEmpty(receiver))
            //{
            //    dxErrorProvider.SetError(teReceiver, string.Format(Variables.MandatoryTips, "领取人"));
            //    teReceiver.Focus();
            //    return;
            //}
         
            string number = teNumber.Text.Trim();
            if (!string.IsNullOrEmpty(IDCar))
            {
                number = IDCar;
            }
            if (string.IsNullOrEmpty(number))
            {
                dxErrorProvider.SetError(teNumber, string.Format(Variables.MandatoryTips, "流水号"));
                teNumber.Focus();
                return;
            }

            var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            if (list != null && list.Count > 0 && 
                list.Any(m => m.CustomerReg.CustomerBM == number || 
                m.CustomerReg.Customer.IDCardNo== number))
            {
                var dto = list.FirstOrDefault(m => m.CustomerReg.CustomerBM == number
                || m.CustomerReg.Customer.IDCardNo == number);
                if (dto.State == 2)
                {
                    if (!string.IsNullOrEmpty(IDCar))
                    {
                        ShowMessageBoxWarning($"身份证号{number}已被领取，当前状态：{StateFormatter(dto.State)}。");

                    }
                    else
                        ShowMessageBoxWarning($"体检号{number}已被领取，当前状态：{StateFormatter(dto.State)}。");
                    return;
                }
            }

            AutoLoading(() =>
            {
                var input = new CustomerReportHandoverInput
                {
                    Number = number,
                    Handover = CurrentUser.Name,
                    Receiptor = receiver,
                    Complete = ceIsScanCodeAutoSend.Checked
                };
                var dto = _service.Send(input);
                if (dto != null)
                {
                    gridControl.AddDtoListItem(dto);
                }
                //if (ceIsScanCodeAutoSend.Checked)
                //{
                //    var input = new CustomerReportHandoverInput
                //    {
                //        Number = number,
                //        Handover = CurrentUser.Name,
                //        Receiptor = receiver,
                //        Complete = ceIsScanCodeAutoSend.Checked
                //    };
                //    var dto = _service.Send(input);
                //    if (dto != null)
                //    {
                //        gridControl.AddDtoListItem(dto);
                //    }
                //}
                //else
                //{
                //    var dto = _service.Get(new CustomerReportByNumber { CustomerBM = number });
                //    if (dto != null)
                //    {
                //        dto.Sender = CurrentUser.Name;
                //        dto.Receiver = receiver;
                //        gridControl.AddDtoListItem(dto);
                //    }
                //}
                gridControl.RefreshDataSource();
            }, Variables.LoadingSaveing);
        }
        /// <summary>
        /// 确认流水号
        /// </summary>
        private void Cancel()
        {
            dxErrorProvider.ClearErrors();
            string receiver = teReceiver.Text.Trim();
            //if (string.IsNullOrEmpty(receiver))
            //{
            //    dxErrorProvider.SetError(teReceiver, string.Format(Variables.MandatoryTips, "领取人"));
            //    teReceiver.Focus();
            //    return;
            //}
            string number = teNumber.Text.Trim();
            if (string.IsNullOrEmpty(number) 
                && string.IsNullOrEmpty(txtIDCardNo.Text.Trim()))
            {
                dxErrorProvider.SetError(teNumber, string.Format(Variables.MandatoryTips, "流水号"));
                teNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(number))
            {
                number = txtIDCardNo.Text.Trim();
            }
            var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            if (list != null && list.Count > 0 && list.Any(m => m.CustomerReg.CustomerBM == number) )
            {
                var dto = list.FirstOrDefault(m => 
                m.CustomerReg.CustomerBM == number ||
                 m.CustomerReg.Customer.IDCardNo== txtIDCardNo.Text.Trim());
                if (dto.State != 2)
                {
                    ShowMessageBoxWarning($"体检号{number}没发放报表，无需取消，当前状态：{StateFormatter(dto.State)}。");
                    return;
                }
                if (dto != null && string.IsNullOrEmpty(number))
                {
                    number = dto.CustomerReg.CustomerBM;
                }
            }

            AutoLoading(() =>
            {
                var input = new CustomerReportHandoverInput
                {
                    Number = number,
                    Handover = CurrentUser.Name,
                    Receiptor = receiver,
                    Complete = false
                };
                var dto = _service.Cancel(input);
                if (dto != null)
                {
                    //  gridControl.AddDtoListItem(dto);
                    list = list.Where(p => p.Id != dto.Id).ToList();
                    gridControl.DataSource = list;
                    gridControl.RefreshDataSource();
                    gridControl.Refresh();
                }
              
                gridControl.RefreshDataSource();
            }, Variables.LoadingSaveing);
            MessageBox.Show("取消成功！");
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private void Save()
        {
            var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            list = list.Where(m => m.State < 2).ToList();
            if (list.Count > 0)
            {
                List<CustomerReportHandoverInput> input = new List<CustomerReportHandoverInput>();
                foreach (var item in list)
                {
                    input.Add(new CustomerReportHandoverInput
                    {
                        Number = item.CustomerReg.CustomerBM,
                        Handover = item.Sender,
                        Receiptor = item.Receiver,
                    });
                }

                AutoLoading(() =>
                {
                    _service.BatchSend(input);
                    foreach (var item in list)
                    {
                        item.State = 2;
                    }
                    gridControl.RefreshDataSource();
                }, Variables.LoadingSaveing);
            }
        }

        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Cancel();
            teNumber.Text = string.Empty;
            teNumber.Focus();
            
        }

        private void txtIDCardNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CustomerReportSend_KeyDown(sender, new KeyEventArgs(Keys.F2));
        }

        private void CustomerReportSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (driver != null)
                {
                    var card = driver.ReadCardInfo();
                    Image photo = null;
                    if (card.Succeed)
                    {
                        txtIDCardNo.Text = card.Card.IdCardNo;

                        Ok(txtIDCardNo.Text);

                    }
                    else
                    {

                        ShowMessageBoxWarning(card.Explain);
                    }
                   
                }

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView.GetSelectedRows();
            string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 300)?.Remarks;
            var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            if (selectIndexes.Length != 0)
            {
                foreach (var index in selectIndexes)
                {
                    var CustomerBM = gridView.GetRowCellValue(index, gridColumnCustomerRegCustomerBM).ToString();
                    var currcus = list.FirstOrDefault(p => p.CustomerReg.CustomerBM == CustomerBM);

                    if (currcus == null)
                    {
                        continue;
                    }
                    var sumtstate = currcus.CustomerReg.SummSate;

                    if (!string.IsNullOrEmpty(strwjshow) && strwjshow == "Y")
                    {
                        if (sumtstate != (int)SummSate.Audited)
                        {
                            MessageBox.Show("未审核不能打印报告！");
                            continue;
                        }
                    }
                    //表格体检
                    if (listBoxControlTemplates.EditValue.ToString().Contains("表格"))
                    {
                        var printReport = new BGPrintReport();
                        var cusNameInput = new CusNameInput { Id = currcus.CustomerRegId };
                        printReport.cusNameInput = cusNameInput;
                        printReport.Print(false, listBoxControlTemplates.EditValue.ToString(), "");
                    }
                    else
                    {
                        var printReport = new PrintReportNew();
                        //if (!radioGroupPrintOption.EditValue.Equals(0))
                        //{
                        //    printReport.StrReportTemplateName = "000";
                        //}
                        var TempName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
                        var cusNameInput = new CusNameInput { Id = currcus.CustomerRegId };
                        printReport.cusNameInput = cusNameInput;
                        if (listBoxControlTemplates.EditValue.ToString() == TempName)
                        {
                            printReport.islndb = true;
                        }
                        if (listBoxControlTemplates.EditValue.ToString() == "根据体检类别匹配")
                        {
                          
                                printReport.Print(false, "", "");
                           
                        }
                        else
                        {
                            #region word报告
                            if (listBoxControlTemplates.EditValue.ToString().Contains(".rdlx") ||
                                listBoxControlTemplates.EditValue.ToString().Contains(".rpx"))
                            {
                                var customBM = currcus.CustomerReg.CustomerBM;

                                Commonprintword(customBM, "", currcus.CustomerRegId);
                            }
                            #endregion
                            #region word报告
                            else if (listBoxControlTemplates.EditValue.ToString().Contains(".doc") ||
                                 listBoxControlTemplates.EditValue.ToString().Contains(".doc"))
                            {
                                try
                                {
                                    var customBM = currcus.CustomerReg.CustomerBM;
                                    var Name = currcus.CustomerReg.Customer.Name;

                                    this.Cursor = Cursors.WaitCursor;
                                    Commonprintdocword(customBM, "", true, false);
                                    //更新打印状态
                                    string mb = "";
                                    _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM { Id = currcus.CustomerRegId, Name = mb });

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
                                
                                    printReport.Print(false, "", listBoxControlTemplates.EditValue.ToString());
                               
                            }
                        }
                    }

                }
               
            }
            else
            {
                ShowMessageBoxWarning("没有选择任何数据！");
            }
        }
        private async Task Commonprintdocword(string customBM, string path, bool bIsPrint, bool bIsView)
        {
            await Task.Run(async () =>
            {
                path = path + "\\" + customBM + ".doc";
                var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 30)?.Remarks;
                if (printName == null)
                {
                    printName = "";
                }
                var GrdPath = GridppHelper.GetTemplate(listBoxControlTemplates.EditValue.ToString());
                //string args = customBM + "|" + GrdPath + "|" + path + "|" + "True";
                WordHelper oHelper = new WordHelper();
                ReturnClass oRtn = await oHelper.ExcuteWordAsync(customBM, GrdPath, path, bIsPrint, bIsView, printName);
                if (oRtn.IsSucceeded())
                { }
                else
                {
                    MessageBox.Show(oRtn.message);
                }
            });
        }

        private async Task Commonprintword(string customBM, string path, Guid regID)
        {
            await Task.Run(() => printword(customBM, path, regID));
        }
        private void printword(string customBM, string path, Guid regId)
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

        private void simpleButtonPrintPreview_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {
                //XtraMessageBox.Show("试用版本，不能预览报告！");
                //return;
            }
            var selectIndexes = gridView.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                var cusReg =  gridView.GetFocusedRow() as CustomerReportFullDto;
                //表格体检
                if (listBoxControlTemplates.EditValue.ToString().Contains("表格"))
                {
                    var printReport = new BGPrintReport();
                    var cusNameInput = new CusNameInput { Id = cusReg.CustomerRegId };
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
                    int? ispr = 3;
                    var TempName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
                    var cusNameInput = new CusNameInput { Id = cusReg.CustomerRegId, PrivacyState = ispr };
                    //var cusNameInput = new CusNameInput { Id = id };
                    printReport.cusNameInput = cusNameInput;
                    if (listBoxControlTemplates.EditValue.ToString() == TempName)
                    {
                        printReport.islndb = true;
                    }
                    if (listBoxControlTemplates.EditValue.ToString() == "根据体检类别匹配")
                    {
                         
                            printReport.Print(true, "", "");
                       

                        
                    }
                    else
                    {
                        #region word报告
                        if (listBoxControlTemplates.EditValue.ToString().Contains(".doc") ||
                           listBoxControlTemplates.EditValue.ToString().Contains(".doc"))
                        {
                            try
                            {
                                var customBM = cusReg.CustomerReg.CustomerBM;

                                this.Cursor = Cursors.WaitCursor;
                                Commonprintdocword(customBM, "", false, true);

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
                             
                                printReport.Print(true, "", listBoxControlTemplates.EditValue.ToString());
                            
                        }
                    }

                }
            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }

        private void teNumber_Click(object sender, EventArgs e)
        {
            teNumber.SelectAll();
            
        }
    }
}
