using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using gregn6Lib;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.InvoicePrint;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
 

namespace Sw.Hospital.HealthExaminationSystem.PaymentManager.ChecklistReport
{
    public partial class ChecklistReport : UserBaseForm
    {
        GridppReport Report = new GridppReport();
        private readonly ICustomerAppService _customerAppService;
        private readonly IChargeAppService _chargeAppService;
        private readonly IUserAppService _userAppService;
        private CustomerRegViewDto regViewDto { get; set; }
        public Guid Id { get; set; }
        public string CustomerBM { get; set; }
        private readonly ICommonAppService _commonAppService;
        public ChecklistReport()
        {
            _commonAppService = new CommonAppService();
            InitializeComponent();

            _customerAppService = new CustomerAppService();
            _chargeAppService = new ChargeAppService();
            _userAppService = new UserAppService();
        }

        public ChecklistReport(Guid id, string CustomerBM) : this()
        {
            Id = id;
            this.CustomerBM = CustomerBM;
        }
        private void ChecklistReport_Load(object sender, EventArgs e)
        {
            #region 作废
            //string ReportFile = System.Windows.Forms.Application.StartupPath + @"\Reports\体检清单.grf";
            //if (!System.IO.File.Exists(ReportFile))
            //{
            //    XtraMessageBox.Show("打印文件没有找到！");
            //    return;
            //}
            //Report.LoadFromFile(ReportFile);
            #endregion

            var rep = GridppHelper.GetTemplate("体检清单.grf");
            Report.LoadFromURL(rep);
            //Report.LoadFromFile(ReportFile);
            if (!string.IsNullOrWhiteSpace(CustomerBM))
            {
                textEditTJH.Text = CustomerBM;
                keydown();
            }
            Seach();
        }
        //查询加载gridview数据
        private void btnSearch_Click(object sender, EventArgs e)
        {
            keydown();
        }
        public void keydown()
        {
            if (!string.IsNullOrWhiteSpace(textEditTJH.Text.Trim()))
            {
                regViewDto = _customerAppService.GetCustomerRegViewDto(new CusNameInput { Theme = textEditTJH.Text.Trim() });
                if (regViewDto == null)
                {
                    gridControl1.DataSource = null;
                    rptDaily.Stop();
                    rptDaily.Report = Report;
                    rptDaily.Start();
                    return;
                }
                var charge = _chargeAppService.GetReceiptOrCustomer(new EntityDto<Guid> { Id = regViewDto.Id });
                gridControl1.DataSource = charge;
                rptDaily.Stop();
                rptDaily.Report = Report;
                rptDaily.Start();
            }
        }
        private void Seach()
        {
            rptDaily.Stop();
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            rptDaily.Report = Report;
            rptDaily.Start();
            if (Id != Guid.Empty)
            {
                if (regViewDto != null)
                {
                    Report.Print(false);
                }
                //rptDaily.Report.Print(true);
                this.Close();
            }
        }
        private void ReportFetchRecord()
        {
            if (Id != Guid.Empty)
            {
                var MReceiptOrCustomerViewDto = _chargeAppService.GetReceiptViewDto(new EntityDto<Guid> { Id = Id });
                if (MReceiptOrCustomerViewDto.CustomerReg != null)
                {
                    regViewDto = MReceiptOrCustomerViewDto.CustomerReg; regViewDto.Id = MReceiptOrCustomerViewDto.Id;
                }
            }
            if (regViewDto != null)
            {
                if (regViewDto.Id != Guid.Empty)
                {
                    var ReceiptDetailed = _chargeAppService.GetReceiptDetailed(new EntityDto<Guid> { Id = regViewDto.Id });
                    if (ReceiptDetailed == null)
                    {
                        return;
                    }
                    var charge = _chargeAppService.GetCustomerItemGroupList(new EntityDto<Guid> { Id = regViewDto.Id });
                    var users = _userAppService.GetUser(new EntityDto<long> { Id = ReceiptDetailed.Userid });
                    if (charge.Count == 0)
                    {
                        Report.DetailGrid.Recordset.Append();
                        Report.FieldByName("档案号").AsString = regViewDto.CustomerBM;
                        Report.FieldByName("姓名").AsString = regViewDto.Customer.Name;
                        if (regViewDto.Customer.Sex != 0)
                        {
                            Report.FieldByName("性别").AsString = regViewDto.Customer.Sex == 1 ? "男" : "女";
                        }
                        if (regViewDto.ClientReg != null)
                            Report.FieldByName("单位").AsString = regViewDto.ClientReg.ClientInfo.ClientName;
                        Report.FieldByName("时间").AsString = ReceiptDetailed.ChargeDate.ToLongDateString();
                        Report.FieldByName("金额").AsString = ReceiptDetailed.Actualmoney.ToString();
                        if (users != null)
                            Report.FieldByName("收费人").AsString = users.Name;
                        Report.FieldByName("开票人").AsString = regViewDto.Customer.Name;
                        if (Report.FieldByName("二维码") != null && !string.IsNullOrEmpty(ReceiptDetailed.ewm ))
                        {
                             //01,10,发票代码，发票号码，未税金额，开票日期，校验码，AAFF
                            QRCode qRCode = new QRCode();
                            var ewm = string.Format("01,10,{0},{1},{2},{3},{4},AAFF,",
                             ReceiptDetailed.fpdm, ReceiptDetailed.fphm, ReceiptDetailed.hjje,
                             ReceiptDetailed.ChargeDate.ToString("yyyyMMdd"),
                             ReceiptDetailed.jym);
                            //MessageBox.Show(ReceiptDetailed.fpdm);
                           // MessageBox.Show(ewm);
                            var ss = qRCode.er(ewm);                         
                            Report.FieldByName("二维码").AsString = ss;
                            if (Report.FieldByName("发票代码") != null)
                            { Report.FieldByName("发票代码").AsString = ReceiptDetailed.fpdm; }
                            if (Report.FieldByName("发票号") != null)
                            { Report.FieldByName("发票号").AsString = ReceiptDetailed.fphm; }

                        }
                        if (Report.FieldByName("支付方式") != null)
                        {
                            var Mpay = ReceiptDetailed.MPayment.Select(p => p.MChargeTypename).ToList();
                            Report.FieldByName("支付方式").AsString = string.Join("、", Mpay);
 
                        }
                            Report.DetailGrid.Recordset.Post();
                        return;
                    }
                    if (charge != null)
                    {
                        if (charge != null)
                        {
                            foreach (var item in charge)
                            {
                                Report.DetailGrid.Recordset.Append();
                                Report.FieldByName("档案号").AsString = regViewDto.CustomerBM;
                                Report.FieldByName("姓名").AsString = regViewDto.Customer.Name;
                                if (regViewDto.Customer.Sex != 0)
                                {
                                    Report.FieldByName("性别").AsString = regViewDto.Customer.Sex == 1 ? "男" : "女";
                                }
                                if (regViewDto.ClientReg != null)
                                    Report.FieldByName("单位").AsString = regViewDto.ClientReg.ClientInfo.ClientName;
                                Report.FieldByName("时间").AsString = ReceiptDetailed.ChargeDate.ToLongDateString();
                                Report.FieldByName("金额").AsString = ReceiptDetailed.Actualmoney.ToString();
                                Report.FieldByName("项目").AsString = item.ItemGroupName;
                                Report.FieldByName("原价").AsString = item.ItemPrice.ToString();
                                Report.FieldByName("折扣率").AsString = (item.DiscountRate * 100).ToString();
                                Report.FieldByName("实际价格").AsString = item.PriceAfterDis.ToString();
                                if (users != null)
                                    Report.FieldByName("收费人").AsString = users.Name;

                                Report.FieldByName("开票人").AsString = CurrentUser.Name;
                                //MessageBox.Show(ReceiptDetailed.ewm);
                                if (Report.FieldByName("二维码") != null && !string.IsNullOrEmpty(ReceiptDetailed.ewm))
                                {
                                    //MessageBox.Show("二维码");
                                    var ewm = string.Format("01,10,{0},{1},{2},{3},{4},AAFF,",
                            ReceiptDetailed.fpdm, ReceiptDetailed.fphm, ReceiptDetailed.hjje,
                            ReceiptDetailed.ChargeDate.ToString("yyyyMMdd"),
                            ReceiptDetailed.jym);
                                    //MessageBox.Show(ReceiptDetailed.fpdm);
                                    //MessageBox.Show(ewm);
                                    QRCode qRCode = new QRCode();
                                    var ss= qRCode.er(ewm);
                                    Report.FieldByName("二维码").AsString = ss;
                                    if (Report.FieldByName("发票代码") != null)
                                    { Report.FieldByName("发票代码").AsString = ReceiptDetailed.fpdm; }
                                    if (Report.FieldByName("发票号") != null)
                                    { Report.FieldByName("发票号").AsString = ReceiptDetailed.fphm; }
                                    //MessageBox.Show(ss);
                                }
                                if (Report.FieldByName("支付方式") != null)
                                {
                                    var Mpay = ReceiptDetailed.MPayment.Select(p => p.MChargeTypename).ToList();
                                    Report.FieldByName("支付方式").AsString = string.Join("、", Mpay);
                                }
                                Report.DetailGrid.Recordset.Post();
                            }
                        }

                    }
                }
            }

        }
        //打印预览
        private void simpleButtonDY_Click(object sender, EventArgs e)
        {
            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 60)?.Remarks;
            if (!string.IsNullOrEmpty(printName))
            {
                Report.Printer.PrinterName = printName;
            }
            Report.Print(false);
            //日志
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            createOpLogDto.LogBM = regViewDto.CustomerBM;
            createOpLogDto.LogName = regViewDto.Customer.Name;
            createOpLogDto.LogText = "打印收费清单";
            createOpLogDto.LogDetail = "";
            createOpLogDto.LogType = (int)LogsTypes.PrintId;
            _commonAppService.SaveOpLog(createOpLogDto);
            //rptDaily.Report.PrintPreview(true);
        }


        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dtos = gridControl1.GetSelectedRowDtos<MReceiptInfoPerViewDto>();
            regViewDto.Id = dtos.FirstOrDefault().Id;
            Seach();
        }

        private void textEditTJH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                keydown();
        }
    }
}
