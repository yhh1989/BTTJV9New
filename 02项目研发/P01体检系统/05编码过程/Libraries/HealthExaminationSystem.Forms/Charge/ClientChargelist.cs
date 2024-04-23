using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.ChecklistReport;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.InvoicePrint;
using WindowsFormsApp1;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class ClientChargelist : UserBaseForm
    {
        private readonly IChargeAppService ChargeAppService;

        private List<ChargeGroupsDto> ChargeGroupLis;

        public CommonAppService CommonAppSrv;

       // private List<CreatePaymentDto> CreatePaymentList;//多种支付方式

        public List<EntityDto<Guid>> CusList;

        public Guid GuidClientRegID;
        public string  gtClientName="";
        List<CreatePaymentDto> CreatePaymentList;
        private IIDNumberAppService iIDNumberAppService;
        public ClientChargelist()
        {
            InitializeComponent();
            ChargeAppService = new ChargeAppService();
            CommonAppSrv = new CommonAppService();
            iIDNumberAppService = new IDNumberAppService();
        }

        private void ClientChargelist_Load(object sender, EventArgs e)
        {
            if (GuidClientRegID != null)
            {
                gridViewInvoice.Columns[ReceiptSate.FieldName].DisplayFormat.Format =
                    new CustomFormatter(FormatReceiptSate);
                gridViewInvoice.Columns[MPayment.FieldName].DisplayFormat.Format = new CustomFormatter(FormatMPayment);

                //加载支付方式
                var ChargeType = new List<ChargeTypeDto>();
                var type = 2;
                var chargeBM = new ChargeBM();
                chargeBM.Name = type.ToString();
                ChargeType = ChargeAppService.ChargeType(chargeBM);
                comPaymentMethod.Properties.DataSource = ChargeType;
                if (ChargeType.Count <= 0)
                {
                    MessageBox.Show("请至少维护一种支付方式！");
                    return;
                }
                comPaymentMethod.EditValue = ChargeType[0];

                //加载收费记录
                var charge = new EntityDto<Guid>();
                charge.Id = GuidClientRegID;
                var MReceiptClient = ChargeAppService.MInvoiceRecorView(charge);
                gridInvo.DataSource = MReceiptClient;
                if (CusList.Count > 0)
                {
                    ChargeGroupLis = ChargeAppService.getChargeGroups(CusList);
                    txtpayMoney.EditValue = ChargeGroupLis
                        .Where(r => r.IsAddMinus != 3 && r.MReceiptInfoClientlId == null && r.TTmoney > 0)
                        .Sum(r => r.TTmoney);
                    txtpayMoney.Enabled = false;
                    groupPay.Text += "<Color=Red>本次结算共：" + CusList.Count + "人</Color>";
                }
            }
        }

        private string FormatReceiptSate(object arg)
        {
            try
            {
                return InvoiceStatusHelper.PayerCatInvoiceStatus(arg);
            }
            catch
            {
                return "";
            }
        }

        private string FormatMPayment(object arg)
        {
            try
            {
                var payNames = "";
                var createPayments = (ICollection<CreatePaymentDto>)arg;
                foreach (var createPayment in createPayments)
                    payNames += createPayment.MChargeTypename + ":" + createPayment.Actualmoney + ",";
                return payNames.TrimEnd(',');
            }
            catch
            {
                return "";
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                simpleButton1.Visible = false;
                dxErrorProvider.ClearErrors();
                if (string.IsNullOrWhiteSpace(txtpayMoney.Text))
                {
                    XtraMessageBox.Show("请输入金额！");
                    return;
                }
                if (decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) <= 0)
                {
                    XtraMessageBox.Show("收费金额不能小于0！");
                    return;
                }              
                var input = new CreateReceiptInfoDto();
                input.ClientRegid = GuidClientRegID; //关联已有对象
                input.Actualmoney = decimal.Parse(txtpayMoney.EditValue.ToString());
                if (CommonAppSrv == null)
                    CommonAppSrv = new CommonAppService();
                input.ChargeDate = CommonAppSrv.GetDateTimeNow().Now;
                int chargestate = (int)InvoiceStatus.NormalCharge;
                input.ChargeState = chargestate;
                input.Discontmoney = 0;
                input.DiscontReason = "";
                input.Discount = 1;
                int receiptstate = (int)InvoiceStatus.Valid;
                input.ReceiptSate = receiptstate; 
                input.Remarks = txtRemarks.Text;
                int settlementsate = (int)ReceiptState.UnSettled;
                input.SettlementSate = settlementsate;
                input.Shouldmoney = decimal.Parse(txtpayMoney.EditValue.ToString());
                input.Summoney = decimal.Parse(txtpayMoney.EditValue.ToString());
                int tjtype = (int)ChargeApply.Company;
                input.TJType = tjtype;
                input.Userid = CurrentUser.Id;

                //支付方式记录集合 
                var CreatePayments = new List<CreatePaymentDto>();
                if (CreatePaymentList != null && CreatePaymentList.Count >= 0 && comPaymentMethod.Text == "")
                {
                    CreatePayments = CreatePaymentList;
                }
                else
                {
                    if (comPaymentMethod.Text == "")
                    {
                        MessageBox.Show("请选择支付方式");
                        return;
                    }

                    var CreatePayment = new CreatePaymentDto();
                    CreatePayment.Actualmoney = decimal.Parse(txtpayMoney.EditValue.ToString());
                    CreatePayment.CardNum = txtCardNum.Text.Trim(); //暂时不支持会员卡
                    CreatePayment.Discount = 1;
                    CreatePayment.MChargeTypeId = ((ChargeTypeDto)comPaymentMethod.EditValue).Id;
                    CreatePayment.Shouldmoney = decimal.Parse(txtpayMoney.EditValue.ToString());
                    CreatePayments.Add(CreatePayment);
                }

                input.MPaymentr = CreatePayments;
                

                //收费细目
                if (CusList.Count > 0)
                {
                    var CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();
                   
                    var ChargeGroups = ChargeGroupLis
                        .Where(r => r.IsAddMinus != 3 && r.MReceiptInfoClientlId == null && r.MReceiptInfoClientlId ==null && r.GRmoney <= 0);

                    var ChargeGrouplis = ChargeGroups.GroupBy(o=>o.ItemGroupName);
                    foreach (var ChargeGroup in ChargeGrouplis)
                    {
                        var CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                        //List<ChargeGroupsDto> chargeGroups = (List<ChargeGroupsDto>)ChargeGroup;
                        CreateMReceiptInfoDetailed.GroupsMoney = ChargeGroup.Sum(o=>o.ItemPrice);
                        CreateMReceiptInfoDetailed.GroupsDiscountMoney = ChargeGroup.Sum(o=>o.TTmoney);
                        if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                        {
                            CreateMReceiptInfoDetailed.Discount =
                                CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                        }
                        else
                        { CreateMReceiptInfoDetailed.Discount = 0; }
                        if (ChargeGroup.First().SFType.HasValue)
                            CreateMReceiptInfoDetailed.ReceiptTypeName = DefinedCacheHelper
                                .GetBasicDictionaryByValue(BasicDictionaryType.ChargeCategory,
                                    ChargeGroup.First().SFType.Value).Text;
                        else
                            CreateMReceiptInfoDetailed.ReceiptTypeName = "";
                        CreateMReceiptInfoDetailed.ItemGroupId = ChargeGroup.First().Id;
                        CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
                    }

                    input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
                }

                //保存收费记录
                #region 优康会员卡接口 
                if (comPaymentMethod.Text.Trim() == "会员卡")
                {

                    decimal YFJE = decimal.Parse(txtpayMoney.EditValue.ToString());
                    if (txtCardNum.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("使用会员卡时，卡号不能为空！");
                        return;
                    }
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Name = txtCardNum.Text.Trim();
                    var strhtm = ChargeAppService.GetNYHCardByNum(chargeBM);
                    if (strhtm.code == 0)
                    {
                        MessageBox.Show(strhtm.err);
                        return;
                    }

                    string ja1a = strhtm.CardNo;
                    decimal hyzk = strhtm.Amount;
                    string lb = strhtm.CategoryName;
                    try
                    {

                        simpleLabelItem2.Text = "卡信息：  卡号：" + ja1a + ",类别：" + lb + ",卡余额：" + hyzk + "";

                        if (hyzk < YFJE)
                        {
                            MessageBox.Show("会员卡余额不足，请充值！");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("该会员卡不存在，请核实！");
                        return;
                    }

                    try
                    {

                        ChargCardDto cusinput = new ChargCardDto();
                        cusinput.Amount = YFJE;
                        cusinput.CardNo = txtCardNum.Text.Trim();
                        cusinput.CheckItemCount = 0;
                        cusinput.ArchivesNum = "";
                        cusinput.SuitName = "";
                        var ykstrh = ChargeAppService.NYHChargeCard(cusinput);
                        if (ykstrh.code == 0)
                        {
                            MessageBox.Show(ykstrh.Mess);
                            return;
                        }
                        simpleLabelItem2.Text = "卡信息：  卡号：" + ja1a + ",类别：" + lb + ",卡余额：" + ykstrh.Amount + "";

                    }
                    catch (Exception ex)
                    {
                        ;
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                #endregion

                #region 团体收费推送

                var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 4)?.Remarks;
                var DJTSCJ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 2)?.Remarks;
                if (!string.IsNullOrEmpty(DJTS) && DJTS == "1" && !string.IsNullOrEmpty(DJTSCJ))
                {
           
                    var regIdls = CusList.Select(p => p.Id).ToList();
                    string cusIdStr = string.Join("|", regIdls);

                    input.pay_order_id = iIDNumberAppService.CreateApplicationBM();
                    XYNeInterface neInterface = new XYNeInterface();
                    //接口名称&单位预约ID&申请单号&金额&发票名称
                    var inStr = DJTSCJ + "&" + input.ClientRegid + "&" + input.pay_order_id + 
                        "&" + input.Actualmoney + "&" + input.Remarks + "&" +  CurrentUser.Name;
                    var outMess = neInterface.SaveTTPay(inStr, cusIdStr);
                    if (outMess.Code != "0")
                    {
                        MessageBox.Show(outMess.ReSult);
                        return;
                    }

                }

                #endregion
                #region 团体收费申请
                var receiptInfoDtoID = ChargeAppService.InsertReceiptInfoDto(input);
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2)?.Remarks;
                    if (HISName != null && HISName == "江苏鑫亿")
                    {
                        //MessageBox.Show("调用接口");
                        XYNeInterface neInterface = new XYNeInterface();
                        var outMess = neInterface.SaveTTPay(receiptInfoDtoID.ToString(), CurrentUser.EmployeeNum);
                        if (outMess.Code != "0")
                        {
                            MessageBox.Show(outMess.ReSult);
                           
                        }
                    }
                } 
                #endregion
                ShowMessageSucceed("收费成功！");
                //更新体检人收费状态及组合结算状态
                if (CusList.Count > 0)
                {
                    var updateChargeStateDto = new UpdateChargeStateDto();
                    var cusGroups = ChargeGroupLis.Where(r =>
                        r.IsAddMinus != 3 && r.MReceiptInfoClientlId == null && r.TTmoney > 0);
                    var CusGroupsIds = cusGroups.Select(r => r.Id);
                    var CusRegs = cusGroups.Where(r => r.CustomerRegBMId.HasValue).Select(r => r.CustomerRegBMId);
                    updateChargeStateDto.CusGroupids = CusGroupsIds.ToList();
                    updateChargeStateDto.CusRegids = CusRegs.Distinct().Cast<Guid>().ToList();
                    updateChargeStateDto.ReceiptID = receiptInfoDtoID;
                    updateChargeStateDto.CusType = 2;
                    ChargeAppService.updateClientChargeState(updateChargeStateDto);
                }

                //if (CreatePaymentList != null)
                //    CreatePaymentList.Clear();
                CusList.Clear();
                txtpayMoney.Text = "0.00";
                var charge = new EntityDto<Guid>();
                charge.Id = GuidClientRegID;
                var InvoiceRecord = ChargeAppService.MInvoiceRecorView(charge);
                gridInvo.DataSource = InvoiceRecord;
                simpleButton1.Visible = true;
                if (CreatePaymentList != null)
                {
                    CreatePaymentList.Clear();
                    labinedPayment.Text = "";
                }
              
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                simpleButton1.Visible = true;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var dto = gridInvo.GetFocusedRowDto<MReceiptClientDto>();
            if (dto == null)
                return;
            FrmInvoicePrint printFrom = new FrmInvoicePrint(dto.Id);
            printFrom.ShowDialog();
        }

        private void simpleButtonDetailedList_Click(object sender, EventArgs e)
        {
            //var customerRegId = Guid.Parse(gridViewInvoice.GetRowCellValue(gridViewInvoice.FocusedRowHandle, CustomerRegId).ToString());
            var dto = gridInvo.GetSelectedRowDtos<MReceiptClientDto>().FirstOrDefault();
            using (var frm = new ChecklistReport(dto.Id, ""))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    return;
            }
        }

        private void butCombinedPayment_Click(object sender, EventArgs e)
        {
            try
            {
                List<ChargeTypeDto> ChargeType = new List<ChargeTypeDto>();
                int type = 2;
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = type.ToString();
                ChargeType = ChargeAppService.ChargeType(chargeBM);

                 var Payment = new PaymentMethod();
                if (CusList.Count > 0 )
                {
                    decimal paymoney = decimal.Parse(txtpayMoney.EditValue.ToString());
                    if (paymoney > 0)
                    {
                        Payment = new PaymentMethod(paymoney);
                    }
                    else
                    {
                        MessageBox.Show("应收金额为0，不支持组合收费");
                        return;
                    }
                }

                    Payment.ChargeType = ChargeType;
                Payment.ShowDialog();
                if (Payment.DialogResult == DialogResult.OK)
                {
                    CreatePaymentList = Payment.CreatePaymentList;
                    labinedPayment.Text = "";
                    decimal payMoney = 0;
                    foreach (CreatePaymentDto payment in CreatePaymentList)
                    {
                        labinedPayment.Text += payment.MChargeTypename + ":" + payment.Actualmoney + "元，";
                        payMoney += payment.Actualmoney;
                    }
                    labinedPayment.Text = labinedPayment.Text.TrimEnd(',');
                    labinedPayment.ForeColor = Color.Red;
                    txtpayMoney.Text = payMoney.ToString("0.00");
                    if (CreatePaymentList.Count > 0)
                    {
                        comPaymentMethod.SelectedText = "";
                        comPaymentMethod.EditValue = "";
                    }

                    // 收费成功
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            #region HIS接口
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
            if (HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                //if (HISName == "北仑" || HISName == "南京飓风")
                //{
                    simpleButton1.Visible = false;
                    dxErrorProvider.ClearErrors();
                    if (string.IsNullOrWhiteSpace(txtpayMoney.Text))
                    {
                        XtraMessageBox.Show("请输入金额！");
                        return;
                    }
                    if (decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) <= 0)
                    {
                        XtraMessageBox.Show("收费金额不能小于0！");
                        return;
                    }
                    ICustomerAppService _customerSvr = new CustomerAppService();
                    TJSQDto input = new TJSQDto();                 
                    input.DWMC = gtClientName;
                    input.HISName = HISName;
                    input.UserID = Variables.User.Id;
                    input.FYZK = decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥'));
                    input.ClientRegId = GuidClientRegID;
                    var appliy = _customerSvr.InsertSFCharg(input);
                    if (appliy != null)
                    {
                        string AppliyNum = appliy.ApplicationNum;
                    }
                //}
            }
            #endregion
        }

        private void txtCardNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && txtCardNum.Text.Trim() != "")
            {
                #region 读会员卡
                if (comPaymentMethod.Text.Trim() == "会员卡")
                {
                    decimal YFJE = decimal.Parse(txtpayMoney.EditValue.ToString());
                    if (txtCardNum.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("使用会员卡时，卡号不能为空！");
                        return;
                    }
                    //InCarNumDto inCarNumDto = new InCarNumDto();
                    // inCarNumDto.CardNum = txtCardNum.Text.Trim();
                    //var strhtm = ChargeAppService.geYKInfor(inCarNumDto);
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Name = txtCardNum.Text.Trim();
                    var strhtm = ChargeAppService.GetNYHCardByNum(chargeBM);
                    if (strhtm.code == 0)
                    {
                        MessageBox.Show(strhtm.err);
                        return;
                    }
                    try
                    {


                        string ja1a = strhtm.CardNo;
                        decimal hyzk = strhtm.Amount;
                        string lb = strhtm.CategoryName;
                        simpleLabelItem2.Text = "卡信息： 卡号：" + ja1a + ",类别：" + lb + ",卡余额：" + hyzk + "";

                        if (hyzk < YFJE)
                        {
                            MessageBox.Show("会员卡余额不足，请充值！");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("该会员卡不存在，请核实！");
                        return;
                    }
                }
                #endregion
            }
        }
    }
}