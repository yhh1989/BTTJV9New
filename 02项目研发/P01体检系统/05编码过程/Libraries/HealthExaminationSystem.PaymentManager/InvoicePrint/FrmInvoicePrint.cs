using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using DevExpress.XtraLayout.Utils;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MRise;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.MRise;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

using System.Text.RegularExpressions;
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Invoice;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using gregn6Lib;

namespace Sw.Hospital.HealthExaminationSystem.PaymentManager.InvoicePrint
{
    public partial class FrmInvoicePrint : UserBaseForm
    {
        private IMRiseAppService _mRiseAppService;
        private MReceiptInfoDto SettlementInfo;
        private IChargeAppService chargeAppService;
        private InvoiceManagementAppService invocieService;
        private List<MRiseDto> rises;
        GridppReport Report = new GridppReport();
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        public FrmInvoicePrint(Guid ReceiptID)
        {
            InitializeComponent();

            _mRiseAppService = new MRiseAppService();
            chargeAppService = new ChargeAppService();
            invocieService = new InvoiceManagementAppService();
            SettlementInfo = chargeAppService.GetInvalidReceiptById(new EntityDto<Guid> { Id = ReceiptID });
            //打印机设置 
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 50)?.Remarks;
            if (!string.IsNullOrEmpty(printName))
            {
                Report.Printer.PrinterName = printName;
            }
            Report.PrintEnd += PrintSuccess;
            Report.PrintAborted += PrintFailed;
        }
        /// <summary>
        /// 自定义初始化
        /// </summary>
        private void Intinal()
        {
            rises = _mRiseAppService.GetAllMRise();
            searchLookUpEditRise.Properties.DataSource = rises;
        }

        private void FrmInvoicePrint_Load(object sender, EventArgs e)
        {
            Intinal();
            //个人发票
            if (SettlementInfo.CustomerReg != null)
            {
                this.Text = "个人发票打印";
                layoutControlItemName.Text = "姓名：";
                textEditName.Text = SettlementInfo.CustomerReg.Customer.Name;
                layoutControlItemDanganhao.Visibility = LayoutVisibility.Always;
                textEditDanganhao.Text = SettlementInfo.CustomerReg.CustomerBM;
                textEditPrintName.Text = "体检费";
                layoutControlItemTotal.Visibility = LayoutVisibility.Never;
               // spinEditPrintMoney.ReadOnly = true;
                spinEditPrintMoney.Value = SettlementInfo.Shouldmoney;
                //List<SettlementInfoViewDto> list = new List<SettlementInfoViewDto>();
                //list.Add(new SettlementInfoViewDto
                //{
                //    Id = SettlementInfo.Id,
                //    Total = SettlementInfo.Shouldmoney,
                //    Already = 0,
                //    Surplus = SettlementInfo.Shouldmoney
                //});
                //gridControlSettlementInfo.DataSource = list;
                SettlementDataBind(SettlementInfo.Id);
                if (rises.Any(r => r.Name.Contains("个人")))
                {
                    searchLookUpEditRise.EditValue = rises.FirstOrDefault(r => r.Name.Contains("个人")).Id;
                }
                simpleButtonPrint.PerformClick();
                DialogResult = DialogResult.OK;
                this.Close();

            }
            //团体发票
            else
            {
                this.Text = "团体发票打印";
                layoutControlItemName.Text = "团体名称：";
                textEditName.Text = SettlementInfo.ClientReg.ClientInfo.ClientName;
                textEditDanganhao.Text = SettlementInfo.ClientReg.ClientRegBM;
               layoutControlItemDanganhao.Visibility = LayoutVisibility.Never;
                layoutControlItemTotal.Visibility = LayoutVisibility.Always;
                textEditPrintName.Text = "体检费";
                spinEditPrintMoney.ReadOnly = false;
                SettlementDataBind(SettlementInfo.Id);
            }
        }

        private void SettlementDataBind(Guid id)
        {
            var settlements = chargeAppService.GetCompenyInfo(new EntityDto<Guid> { Id = id });
            gridControlSettlementInfo.DataSource = settlements;
        }

        private void simpleButtonAddMRise_Click(object sender, EventArgs e)
        {
            FrmAddMRise from = new FrmAddMRise();
            if (from.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var result = _mRiseAppService.AddMRise(from.dto);
                    rises.Add(result);
                    searchLookUpEditRise.Properties.DataSource = rises;
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
        }

        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            // 尽量不要用 Object 和 String 类型直接比较
            //if (searchLookUpEditRise.EditValue == null|| searchLookUpEditRise.EditValue=="")
            //if (searchLookUpEditRise.EditValue == null || searchLookUpEditRise.EditValue.Equals(string.Empty))
            //{
            //    dxErrorProvider.SetError(searchLookUpEditRise, string.Format(Variables.MandatoryTips, "发票抬头"));
            //    return;
            //}
            var dto = gridControlSettlementInfo.GetFocusedRowDto<SettlementInfoViewDto>();
            if (dto == null)
                return;
            var printMoney = spinEditPrintMoney.Value;
            if (printMoney <= 0)
            {
                ShowMessageBoxInformation("剩余金额为0,不能打印发票");
                return;
            }
            
            if (dto.Surplus < printMoney)
            {
                dxErrorProvider.SetError(spinEditPrintMoney, string.Format(Variables.GreaterThanTips, "发票金额", "剩余金额"));
                return;
            }
            if (SettlementInfo.CustomerReg == null)
            {

                if (string.IsNullOrEmpty( SettlementInfo.ClientReg?.ClientInfo?.SocialCredit) &&
                    string.IsNullOrEmpty(searchLookUpEditRise.EditValue?.ToString()))
                {
                    MessageBox.Show("请在单位设置中维护单位统一信用代码，或者选择发票抬头！");
                    return;
                }
            }
            var rep = GridppHelper.GetTemplate("发票.grf");
            Report.LoadFromURL(rep);
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportBind);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportBind);

            try
            {
                var invoiceList = invocieService.GetUserInvoice().Where(i => i.State == (int)InvoiceState.Enable && i.StratCard < i.EndCard)
                .OrderBy(i => i.SerialNumber);

                if (invoiceList.Count() < 1)
                {
                    if (XtraMessageBox.Show("当前用户暂无发票,是否前往发票设置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FrmInvoiceManagement showFrom = new FrmInvoiceManagement();
                        showFrom.ShowDialog();
                    }
                }
                else
                {
                    try
                    {

                       
                        Report.Print(false);
                        //日志
                        CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                        createOpLogDto.LogBM = textEditDanganhao.Text;
                        createOpLogDto.LogName = textEditName.Text;
                        createOpLogDto.LogText = "打印发票";
                        createOpLogDto.LogDetail = "";
                        createOpLogDto.LogType = (int)LogsTypes.PrintId;
                        _commonAppService.SaveOpLog(createOpLogDto);
                    }
                    catch (Exception ex)
                    {
                        ShowMessageBoxError(ex.ToString());
                    }

                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        private void PrintSuccess()
        {
            if (!string.IsNullOrEmpty(searchLookUpEditRise.EditValue?.ToString()))
            {
                MInvoiceRecordDto input = new MInvoiceRecordDto();
                input.State = ((int)InvoiceStatus.Valid).ToString();
                input.InvoiceMoney = spinEditPrintMoney.Value;
                input.MReceiptInfo = SettlementInfo;
                input.MRise = new MRiseDto { Id = (Guid)searchLookUpEditRise.EditValue };
                var result = chargeAppService.PrintInvoice(input);
                if (result != null)
                    DialogResult = DialogResult.Yes;
            }
        }
        private void PrintFailed()
        {
            ShowMessageBoxError("打印失败!");
        }
        private void ReportBind()
        {
            Report.DetailGrid.Recordset.Append();
            string FPTT = "";
            string FPSH = "";
            if (SettlementInfo.CustomerReg != null)
            {
                Report.FieldByName("个人姓名").AsString = SettlementInfo.CustomerReg.Customer.Name;
                FPTT= SettlementInfo.CustomerReg.Customer.Name;

                if (Report.FieldByName("体检号") != null)
                {
                    Report.FieldByName("体检号").AsString = SettlementInfo.CustomerReg.CustomerBM;
                }
                try
                {
                    if (Report.FieldByName("性别") != null)
                    {
                        string sexform = "女";
                        if(SettlementInfo.CustomerReg.Customer.Sex==1)
                        {
                            sexform = "男";
                        }
                        Report.FieldByName("性别").AsString = sexform;
                    }

                }
                catch
                { }
               
            }
            else
            {
                // Report.FieldByName("个人姓名").AsString = SettlementInfo.ClientName;
                try
                {
                    if (Report.FieldByName("单位名称") != null)
                    {
                        
                        Report.FieldByName("单位名称").AsString = SettlementInfo.ClientName;
                        FPTT = SettlementInfo.ClientName;
                    }
                    Report.FieldByName("统一社会信用代码").AsString = SettlementInfo.ClientReg.ClientInfo.SocialCredit;
                    FPSH = SettlementInfo.ClientReg.ClientInfo.SocialCredit;
                }
                catch
                { }
            }
       
            if (Report.FieldByName("原价") != null)
            {
                Report.FieldByName("原价").AsString = SettlementInfo.Summoney.ToString();
            }
            if (Report.FieldByName("优惠") != null)
            {
                Report.FieldByName("优惠").AsString = (SettlementInfo.Summoney- SettlementInfo.Actualmoney).ToString();
            }
            if (Report.FieldByName("HISID") != null)
            {
                Report.FieldByName("HISID").AsString = SettlementInfo.pay_order_id;
            }
            Report.FieldByName("金额").AsString = SettlementInfo.Actualmoney.ToString();
            Report.FieldByName("支付方式").AsString = SettlementInfo.MPayment.FirstOrDefault().MChargeTypename;
            //Report.FieldByName("金额大写").AsString = ConvertToChinese(Convert.ToDouble(SettlementInfo.Actualmoney));
            Report.FieldByName("金额大写").AsString = CommonHelper.ConvertToChinese(Convert.ToDouble(spinEditPrintMoney.Value));
            Report.FieldByName("金额小写").AsString = spinEditPrintMoney.Value.ToString();
            Report.FieldByName("收款员").AsString = CurrentUser.Name;
            if (!string.IsNullOrEmpty(searchLookUpEditRise.EditValue?.ToString()))
            {
                 FPTT = searchLookUpEditRise.Text;

                if (Guid.TryParse(searchLookUpEditRise.EditValue.ToString(), out Guid Gid))
                {
                    if (Report.FieldByName("税号") != null)
                    {
                        var sh = rises.FirstOrDefault(r => r.Id == Gid).Duty;
                        FPSH = sh;
                        //Report.FieldByName("税号").AsString = sh;
                    }
                }
            }
            Report.FieldByName("发票抬头").AsString = FPTT.Replace("个人","");
            Report.FieldByName("税号").AsString = FPSH;
            Report.DetailGrid.Recordset.Post();

        }


        //private string ConvertToChinese(double dou)
        //{
        //    // 大写数字数组
        //    string[] num = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        //    // 数量单位数组，个位数为空
        //    string[] unit = { "", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆" };
        //    string d = dou.ToString();
        //    string zs = string.Empty;// 整数
        //    string xs = string.Empty;// 小数
        //    int i = d.IndexOf(".");
        //    string str = string.Empty;
        //    if (i > -1)
        //    {
        //        // 仅考虑两位小数
        //        zs = d.Substring(0, i);
        //        xs = d.Substring(i + 1, d.Length - i - 1);
        //        str = "元";
        //        if (xs.Length == 1)
        //            str = str + xs + "角";
        //        else if (xs.Length == 2)
        //            str = str + xs.Substring(0, 1) + "角" + xs.Substring(1, 1) + "分";
        //    }
        //    else
        //    {
        //        zs = d;
        //        str = "元整";
        //    }
        //    // 处理整数部分
        //    if (!string.IsNullOrEmpty(zs))
        //    {
        //        i = 0;
        //        // 从整数部分个位数起逐一添加单位
        //        foreach (char s in zs.Reverse())
        //        {
        //            str = s.ToString() + unit[i] + str;
        //            i++;
        //        }
        //    }
        //    // 将阿拉伯数字替换成中文大写数字
        //    for (int m = 0; m < 10; m++)
        //    {
        //        str = str.Replace(m.ToString(), num[m]);
        //    }
        //    // 替换零佰、零仟、零拾之类的字符
        //    str = Regex.Replace(str, "[零]+仟", "零");
        //    str = Regex.Replace(str, "[零]+佰", "零");
        //    str = Regex.Replace(str, "[零]+拾", "零");
        //    str = Regex.Replace(str, "[零]+亿", "亿");
        //    str = Regex.Replace(str, "[零]+万", "万");
        //    str = Regex.Replace(str, "[零]+", "零");
        //    str = Regex.Replace(str, "亿[万|仟|佰|拾]+", "亿");
        //    str = Regex.Replace(str, "万[仟|佰|拾]+", "万");
        //    str = Regex.Replace(str, "仟[佰|拾]+", "仟");
        //    str = Regex.Replace(str, "佰拾", "佰");
        //    str = Regex.Replace(str, "[零]+元整", "元整");
        //    return str;
        //}

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void gridViewMInoiceRecord_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dto = gridControlSettlementInfo.GetFocusedRowDto<SettlementInfoViewDto>();
            if (dto == null)
                return;
            spinEditTotal.Value = dto.Surplus;
            spinEditPrintMoney.Value = dto.Surplus;
        }

        private void FrmInvoicePrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.OnShown(e);
            if (!DesignMode)
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.SetWaitFormDescription(Variables.LoadingForEnd);
                    splashScreenManager.CloseWaitForm();
                }
        }
    }
}