using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.ChecklistReport;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.InvoicePrint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{ 
    public partial class PersonCharge : UserBaseForm
    {
        private IChargeAppService ChargeAppService;
        private IDoctorStationAppService doctorStationAppService ;
        private readonly List<SexModel> _sexModels;

        private readonly List<ProjectIStateModel> _ProjectIState;
        public ChargeInfoDto ChargeInfoPerSion;
        public CustomerRegDto CustomerReg;
        public CommonAppService CommonAppSrv;
        List<CreatePaymentDto> CreatePaymentList;
        private List<BasicDictionaryDto> tijianType;//体检类型字典
        private InvoiceManagementAppService invocieService;

        private ICustomerAppService customerSvr;//体检预约
        private string _customerBm;

        public int normal = 0;
        public int Addition = 0;
        public PersonCharge()
        {
            InitializeComponent();
            doctorStationAppService = new DoctorStationAppService();
            CommonAppSrv = new CommonAppService();
            ChargeAppService = new ChargeAppService();
            ChargeInfoPerSion = new ChargeInfoDto();
            _sexModels = SexHelper.GetSexModelsForItemInfo();
            _ProjectIState = ProjectIStateHelper.GetProjectIStateModels();

            gridViewCus.Columns[Sex.FieldName].DisplayFormat.Format = new CustomFormatter(FormatSexs);
            gridViewCus.Columns[ClientType.FieldName].DisplayFormat.Format = new CustomFormatter(FormatClientType);


            gridViewGroups.Columns[IsAddMinus.FieldName].DisplayFormat.Format = new CustomFormatter(FormatIsAddMinus);
            gridViewGroups.Columns[CheckState.FieldName].DisplayFormat.Format = new CustomFormatter(FormatCheckState);
            gridViewGroups.Columns[PayerCat.FieldName].DisplayFormat.Format = new CustomFormatter(FormatRefundState);

        }

        public PersonCharge(string customerBm) : this()
        {
            _customerBm = customerBm;
        }

        #region 事件
        private void PersonCharge_Load(object sender, EventArgs e)
        {
            try
            {
                customerSvr = new CustomerAppService();
                invocieService = new InvoiceManagementAppService();
                txtStarDate.DateTime = CommonAppSrv.GetDateTimeNow().Now.Date;
                txtEndDate.DateTime = CommonAppSrv.GetDateTimeNow().Now.Date;
                tijianType = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ExaminationType);
              
                tijianType = DefinedCacheHelper.GetBasicDictionary().Where(o=>o.Type== "ExaminationType").ToList();
                List<ChargeTypeDto> ChargeType = new List<ChargeTypeDto>();
                int type = 2;
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = type.ToString();
                ChargeType = ChargeAppService.ChargeType(chargeBM);
                comPaymentMethod.Properties.DataSource = ChargeType;
                //登记状态
                teDJZT.Properties.DataSource = RegisterStateHelper.GetSelectList();
             
                if (ChargeType.Count <= 0)
                {
                    MessageBox.Show("请维护支付方式！");
                    return;
                }
                comPaymentMethod.EditValue = ChargeType[0];
                // comPaymentMet
                //comPaymentMethod.sel

                gridViewCus.OptionsSelection.MultiSelect = true;
                //gridViewCus.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

                gridViewCus.OptionsView.ShowIndicator = false;//不显示指示器
                gridViewCus.OptionsBehavior.ReadOnly = false;
                gridViewCus.OptionsBehavior.Editable = false;
                if (_customerBm != "" && _customerBm != null)
                {
                    //splitContainerControl1.Panel1.Visible = false;
                    splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                    txtName.Text = _customerBm;
                    System.Windows.Forms.KeyPressEventArgs ee = new KeyPressEventArgs('\r');
                    txtName_KeyPress(sender, ee);

                }
                else
                {
                    QueryData();
                    if (gridViewCus.DataRowCount > 0)
                    {
                        gridViewCus.FocusedRowHandle = 0;
                        gridViewCus.SelectRows(0, 0);

                    }
                }

            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

        }
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    gridGroups.DataSource = null;
                    ChargQueryCusDto QueryCustomerReg = new ChargQueryCusDto();
                    QueryCustomerReg.CustomerBM = txtName.Text;
                    ICollection<ChargeCusInfoDto> ChargeCusInfo = ChargeAppService.ChargeCusInfo(QueryCustomerReg);
                    gridCus.DataSource = ChargeCusInfo;

                    if (gridViewCus.DataRowCount > 0)
                    {
                        gridViewCus.FocusedRowHandle = 0;
                        gridViewCus.SelectRows(0, 0);

                    }
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {

                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForForm);
            try
            {
                //ChargQueryCusDto QueryCustomerReg = new ChargQueryCusDto();
                //QueryCustomerReg.CustomerBM = txtName.Text;
                //if (txtStarDate.EditValue != null && txtEndDate.EditValue != null)
                //{
                //    QueryCustomerReg.NavigationStartTime = DateTime.Parse(txtStarDate.EditValue.ToString());
                //    QueryCustomerReg.NavigationEndTime = DateTime.Parse(txtEndDate.EditValue.ToString());
                //}
                //if (checkUncharge.Checked == false)
                //{
                //    QueryCustomerReg.CostState = 2;
                //}
                //ICollection<ChargeCusInfoDto> ChargeCusInfo = ChargeAppService.ChargeCusInfo(QueryCustomerReg);
                //gridCus.DataSource = ChargeCusInfo;
                QueryData();

                if (gridViewCus.DataRowCount > 0)
                {
                    gridViewCus.FocusedRowHandle = 0;
                    gridViewCus.SelectRows(0, 0);

                }
                else
                {
                    if (closeWait)
                    {
                        splashScreenManager.CloseWaitForm();
                        closeWait = false;
                    }
                    MessageBox.Show("没有收费信息");
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }

        }
        private void QueryData()
        {
            ChargQueryCusDto QueryCustomerReg = new ChargQueryCusDto();
            QueryCustomerReg.CustomerBM = txtName.Text;

            if (txtStarDate.EditValue != null && txtEndDate.EditValue != null)
            {
                QueryCustomerReg.NavigationStartTime = DateTime.Parse(txtStarDate.EditValue.ToString());
                QueryCustomerReg.NavigationEndTime = DateTime.Parse(txtEndDate.EditValue.ToString());
            }
            if (checkUncharge.Checked == false)
            {
                QueryCustomerReg.CostState = 2;
            }
            else
            {
                QueryCustomerReg.CostState = 1;

            }
            if (teDJZT.EditValue != null)
            {
                QueryCustomerReg.RegisterState = int.TryParse(teDJZT.EditValue.ToString(), out int DJZT) ? (int?)DJZT : null;
                if (QueryCustomerReg.RegisterState == 0)
                    QueryCustomerReg.RegisterState = null;
            }
            gridGroups.DataSource = null;
            ICollection<ChargeCusInfoDto> ChargeCusInfo = ChargeAppService.ChargeCusInfo(QueryCustomerReg);
            gridCus.DataSource = ChargeCusInfo;
        }
        private void gridViewCus_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }
        private void gridViewCus_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {

                var dto = gridCus.GetFocusedRowDto<ChargeCusInfoDto>();
                if (dto == null)
                    return;
                if (!string.IsNullOrEmpty(dto.CustomerBM))
                {
                    var invoiceList = invocieService.GetUserInvoice().Where(i => i.State == (int)InvoiceState.Enable && i.StratCard < i.EndCard)
              .OrderBy(i => i.SerialNumber);
                    string ss = "";
                    if (invoiceList.Count() > 0)
                    {
                        ss = invoiceList.First().NowCard.ToString();
                    }
                    CheckChange.Checked = false;
                    string customerBM = dto.CustomerBM;
                    // MessageBox.Show(customerBM);
                    ChargeBM Bm = new ChargeBM();
                    Bm.Name = customerBM;
                    labSuitname.Text = "当前发票号：" + ss + ",套餐：" + dto.ItemSuitName?.ToString();
                    labCusInfo.Text = "体检号：" + dto.CustomerBM + ",姓名：" + dto.Customer.Name + ",性别：" + SexHelper.CustomSexFormatter(dto.Customer.Sex) + ",年龄:" + dto.Customer.Age + ",单位:" + dto.ClientInfo?.ClientName;
                    ChargeInfoDto ChargeInfo = ChargeAppService.Get(Bm);
                    if (ChargeInfo != null)
                    {
                        var datas = ChargeInfo.ChargeGroups.Where(r => r.PayerCat != 3).OrderBy(r => r.IsAddMinus)
                            .ToList();
                        // 收费信息
                        gridGroups.DataSource = datas;
                        gridViewGroups.BestFitColumns();
                        if (ChargeInfo.ChargeGroups != null)
                        {
                            foreach (var item in ChargeInfo.ChargeGroups)
                            {
                                if (item.IsAddMinus == 2)
                                    Addition++;
                                else
                                    normal++;
                            }
                        }
                        txtAddMoney.Text = ChargeInfo.AddGroupMoney.ToString("0.00");
                        txtAllMoney.Text = ChargeInfo.ReceivableMoney.ToString("0.00");
                        txtGroupMoney.Text = ChargeInfo.GroupsMoney.ToString("0.00");
                        txtSubtractMoney.Text = ChargeInfo.SubtractMoney.ToString("0.00");

                        if (ChargeInfo.ItemSuit != null)
                        {
                            txtSuitMoney.Text = ChargeInfo.ItemSuit.Price.ToString();
                        }
                        else
                        {
                            txtSuitMoney.Text = "0";
                        }
                        txtAdjustmentMoney.Text = ChargeInfo.AdjustmentMoney.ToString("0.00");
                        txtSurplusMoney.Text = ChargeInfo.SurplusMoney.ToString("0.00");
                        txtReceiveMoney.Text = ChargeInfo.CollectedMoney.ToString("0.00");

                        //支付信息
                        if (dto.PersonnelCategoryId.HasValue && dto.PersonnelCategory.IsFree == true)
                        {
                            txtReceivable.Text = "0.00";
                            txtpayMoney.Text = "0.00";
                            groupPayInfo.Text = "支付信息<Color=Red>(免费)</Color>";
                            butCombinedPayment.Enabled = false;
                        }
                        else
                        {
                            txtReceivable.Text = ChargeInfo.SurplusMoney <= 0 ? "0.00" : ChargeInfo.SurplusMoney.ToString("0.00");
                            txtpayMoney.Text = ChargeInfo.SurplusMoney <= 0 ? "0.00" : ChargeInfo.SurplusMoney.ToString("0.00");
                            groupPayInfo.Text = "支付信息";
                            butCombinedPayment.Enabled = true;
                        }
                        txtRemarks.Text = "";
                        ChargeInfoPerSion = ChargeInfo;
                        if (CreatePaymentList != null)
                        {
                            CreatePaymentList.Clear();
                            labinedPayment.Text = "";
                        }
                        //控制已收费不能操作收费
                        int nocharge = (int)PayerCatType.NoCharge;
                        var groupmoney = datas.Where(o => o.PayerCat == nocharge).ToList();
                        if (ChargeInfo.SurplusMoney == 0 && groupmoney.Count == 0)
                        {
                            butCharge.Enabled = false;
                        }
                        else
                        {
                            butCharge.Enabled = true;
                        }
                    }
                    label1.Text = "共" + (normal + Addition) + "项，加项" + Addition + "";
                    normal = 0; Addition = 0;
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        private void butCharge_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.ClearErrors();
                if (gridViewCus.SelectedRowsCount <= 0 || string.IsNullOrWhiteSpace(ChargeInfoPerSion.Id.ToString()))
                {
                    XtraMessageBox.Show("请选择体检人！");
                    return;
                }
                if (decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) == 0)
                {
                    DialogResult dr = XtraMessageBox.Show("没有可收费项目，是否继续收费？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                }
                if (decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) < 0)
                {
                    XtraMessageBox.Show("收费金额不能小于0！");
                    return;
                }
                //有待退费项目 需先退费
                QueryClass queryClass = new QueryClass();
                var dto = gridCus.GetFocusedRowDto<ChargeCusInfoDto>();
                queryClass.CustomerRegId = dto.Id;
                List<ATjlCustomerItemGroupPrintGuidanceDto> lstaTjlCustomerItemGroupDtos = doctorStationAppService.GetATjlCustomerItemGroupPrintGuidanceDto(queryClass);
                var vitem = from c in lstaTjlCustomerItemGroupDtos where c.RefundState == (int)PayerCatType.StayRefund select c;
                if (vitem.Count() > 0)
                {
                    XtraMessageBox.Show("有待退项目,请先退费后再收费！");
                    return;
                }
                if (!string.IsNullOrEmpty(txtCardNum.Text) || comPaymentMethod.Text.Contains("单位储值卡"))
                {
                    if (!getJJK())
                    {
                        return;
                    }
                }
                CreateReceiptInfoDto input = new CreateReceiptInfoDto();
                #region 线上支付接口
                var webOpen = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.WebPay, 1);
                if (webOpen != null && webOpen.Remarks == "1")
                {
                    if (comPaymentMethod.Text.Contains("微信")
                        || comPaymentMethod.Text.Contains("支付宝")
                         || comPaymentMethod.Text.Contains("线上支付"))
                    {
                        frmCode frmCode = new frmCode();
                        frmCode.ShowDialog();
                        if (frmCode.DialogResult == DialogResult.OK &&
                         !string.IsNullOrEmpty(frmCode.AuCode))
                        {
                            input.auth_code = frmCode.AuCode;
                        }
                        else
                        {
                            XtraMessageBox.Show("无客户支付码信息，未执行收费操作");
                            return;
                        }
                    }
                }
                #endregion
                decimal ReturnMoney = 0;     //抹零金额      
                if (CheckChange.Checked == true)
                {
                    input.Discontmoney = decimal.Parse(labChange.Text.ToString());
                }
                else
                {
                    input.Discontmoney = 0;
                    if (decimal.Parse(labChange.Text.ToString()) < 0)
                    {
                        ReturnMoney = -decimal.Parse(labChange.Text.ToString());
                    }
                }
                input.Actualmoney = decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) - ReturnMoney;
                if (CommonAppSrv == null)
                {
                    CommonAppSrv = new CommonAppService();
                }
                input.ChargeDate = CommonAppSrv.GetDateTimeNow().Now;
                if (ChargeInfoPerSion.PersonnelCategoryId.HasValue && ChargeInfoPerSion.PersonnelCategory.IsFree == true)
                {
                    input.ChargeState = (int)InvoiceStatus.FreeAdmission;
                }
                else
                {
                    input.ChargeState = (int)InvoiceStatus.NormalCharge;
                }
                input.CustomerRegid = ChargeInfoPerSion.Id;//关联已有对象                

                input.DiscontReason = "";
                input.Discount = 1;
                input.ReceiptSate = (int)InvoiceStatus.Valid;
                input.Remarks = txtRemarks.Text;
                input.SettlementSate = (int)ReceiptState.UnSettled;
                input.Shouldmoney = decimal.Parse(txtReceivable.EditValue.ToString().Trim('¥'));
                input.Summoney = decimal.Parse(txtAllMoney.EditValue.ToString().Trim('¥'));
                int tjtype = (int)ChargeApply.Personal;
                input.TJType = tjtype;
                input.Userid = CurrentUser.Id;

                //支付方式
                List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
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
                  
                    CreatePaymentDto CreatePayment = new CreatePaymentDto();
                    CreatePayment.Actualmoney = decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) - ReturnMoney;
                    CreatePayment.CardNum = txtCardNum.Text.Trim();//卡号
                    CreatePayment.Discount = 1;
                    CreatePayment.MChargeTypeId = ((ChargeTypeDto)comPaymentMethod.EditValue).Id;
                    CreatePayment.Shouldmoney = decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) - ReturnMoney;
                    CreatePayment.MChargeTypename = ((ChargeTypeDto)comPaymentMethod.EditValue).ChargeName;
                    CreatePayments.Add(CreatePayment);
                }
                input.MPaymentr = CreatePayments;
                //未收费项目集合
                List<CreateMReceiptInfoDetailedDto> CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();

                var cusPerGroups = ChargeInfoPerSion.ChargeGroups?.Where(r => r.IsAddMinus != 3 && r.PayerCat == 1 && r.TTmoney <= 0);
                //var ChargeGroups = cusPerGroups?.GroupBy(r => r.SFType);
                foreach (var ChargeGroup in cusPerGroups)
                {
                    CreateMReceiptInfoDetailedDto CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                    CreateMReceiptInfoDetailed.GroupsMoney = ChargeGroup.ItemPrice;
                    CreateMReceiptInfoDetailed.GroupsDiscountMoney = ChargeGroup.GRmoney;
                    if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                    {
                        CreateMReceiptInfoDetailed.Discount = CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                    }
                    else
                    {
                        CreateMReceiptInfoDetailed.Discount = 1;
                    }
                    if (ChargeGroup.SFType.HasValue)
                        CreateMReceiptInfoDetailed.ReceiptTypeName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ChargeCategory, ChargeGroup.SFType.Value)?.Text;
                    else
                        CreateMReceiptInfoDetailed.ReceiptTypeName = "";
                    //组合
                    CreateMReceiptInfoDetailed.ItemGroupId = ChargeGroup.Id;
                    CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
                }

                input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
                //修改原价
                if (CreateMReceiptInfoDetaileds.Count>0)
                { input.Summoney = CreateMReceiptInfoDetaileds.Sum(p => p.GroupsMoney); }
                //保存收费记录
                //  Guid receiptInfoDtoID = ChargeAppService.InsertReceiptInfoDto(input);
                //保存前验证收费金额是否发生变化
                EntityDto<Guid> cusPay = new EntityDto<Guid>();
                cusPay.Id = input.CustomerRegid.Value;
               var cuspaymoney= ChargeAppService.GetCusMoney(cusPay);
                if ((cuspaymoney.PersonalMoney- cuspaymoney.PersonalPayMoney) != decimal.Parse(txtReceivable.EditValue.ToString().Trim('¥')))
                {
                    MessageBox.Show("该体检人收费信息发生变化，请刷新后再操作！");
                    return;
                }
                OutErrDto outErrDto =   ChargeAppService.InsertReceiptState(input);
                             
                if (outErrDto.code == "0")
                {
                    groupPayInfo.Text = "支付信息 <Color=Red> " + outErrDto.cardInfo + "</Color>";
                    ShowMessageSucceed(outErrDto.cardInfo);
                    return;
                }
                else
                {
                    if (outErrDto.cardInfo != "")
                    {
                        groupPayInfo.Text = "支付信息 <Color=Red> " + outErrDto.cardInfo + "</Color>";
                    }

                }

                //更新体检人收费状态及组合结算状态

                //UpdateChargeStateDto updateChargeStateDto = new UpdateChargeStateDto();
                //var cusGroups = cusPerGroups;
                //var CusGroupsIds = cusGroups.Select(r => r.Id);
                //var CusRegs = cusGroups.Where(r => r.CustomerRegBMId.HasValue).Select(r => r.CustomerRegBMId);
                //updateChargeStateDto.CusGroupids = CusGroupsIds.ToList();
                //updateChargeStateDto.CusRegids = CusRegs.Distinct().Cast<Guid>().ToList();
                //updateChargeStateDto.ReceiptID = receiptInfoDtoID;
                //updateChargeStateDto.CusType = 1;
                //ChargeAppService.updateClientChargeState(updateChargeStateDto);
                //插入抹零项目
                //if (CheckChange.Checked == true)
                //{
                //    SearchMLGroupDto searchMLGroupDto = new SearchMLGroupDto();
                //    searchMLGroupDto.CustomerRegID = ChargeInfoPerSion.Id;
                //    searchMLGroupDto.MReceiptInfoPersonalId = receiptInfoDtoID;
                //    searchMLGroupDto.MLMoney = -decimal.Parse(labChange.Text);
                //    TjlCustomerItemGroupDto tjlCustomerItemGroupDto = ChargeAppService.InsertMLGroup(searchMLGroupDto);
                //}
                //更新费用表
                //SearchPayMoneyDto inputcs = new SearchPayMoneyDto();
                //inputcs.Id = ChargeInfoPerSion.Id;
                //inputcs.PayMoney = decimal.Parse(txtpayMoney.EditValue.ToString()) - ReturnMoney;
                //if (CheckChange.Checked == true)
                //{
                //    inputcs.DistMoney = -(decimal.Parse(labChange.Text));
                //}
                //else
                //{
                //    inputcs.DistMoney = 0;
                //}
                //CusPayMoneyViewDto cusPayMoneyViewDto = ChargeAppService.UpCusMoney(inputcs);
                //打印体检清单或发票
                #region 电子发票
                var bis = DefinedCacheHelper.GetBasicDictionary().Where(p => p.Type == "DZFP").ToList();
                var fpisop = bis.FirstOrDefault(p => p.Value == 1)?.Remarks;
                if (fpisop == "1")
                {
                    //MessageBox.Show("开始电子发票接口");
                    DZFPInputDto dZFPInputDto = new DZFPInputDto();
                    dZFPInputDto.TjlMReceiptId = outErrDto.Id;
                    dZFPInputDto.Actualmoney = input.Actualmoney;
                    dZFPInputDto.ChargeDate = input.ChargeDate;
                    dZFPInputDto.Mobile = "";
                    dZFPInputDto.Name = dto.Customer.Name;
                    dZFPInputDto.UserName = CurrentUser.Name;
                    ChargeAppService.DZPF(dZFPInputDto);
                }
                #endregion
                if (checkPrint.Checked == true)
                {
                    EntityDto<Guid> entity = new EntityDto<Guid>();
                    entity.Id = CreatePayments[0].MChargeTypeId;
                    ChargeTypeDto chargeType = ChargeAppService.ChargeTypeByID(entity);
                    if (chargeType.PrintName == 2)
                    {
                        // PrininvoiceByID(receiptInfoDtoID);
                        PrinChekListByID(outErrDto.Id);
                    }
                    else
                    {
                        bool isOK = PrininvoiceByID(outErrDto.Id);
                        var invoiceList = invocieService.GetUserInvoice().Where(i => i.State == (int)InvoiceState.Enable && i.StratCard < i.EndCard)
              .OrderBy(i => i.SerialNumber);
                        if (invoiceList.Count() > 0)
                        {
                            labSuitname.Text = "当前票据号：" + invoiceList.First().NowCard.ToString();
                        }
                    }
                }
                //收费成功
                //var dto = gridCus.GetFocusedRowDto<ChargeCusInfoDto>();
                //if (dto.RegisterState.HasValue && dto.RegisterState.Value == 1)
                //{
                //    DialogResult drts = XtraMessageBox.Show("收费成功，是否今天体检？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (drts == DialogResult.Yes)
                //    {

                //    }
                //}
                //txtReceiveMoney.Text = (decimal.Parse(txtReceiveMoney.EditValue.ToString()) + decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥'))).ToString("0.00");

                if (CreatePaymentList != null)
                {
                    CreatePaymentList.Clear();
                }
                RefreshData();
                var em = new DevExpress.Data.SelectionChangedEventArgs();
                gridViewCus_SelectionChanged(sender, em);

                // 收费成功
                ShowMessageSucceed("收费成功！");
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

        }

        #region 刷新收费项数据
        private void RefreshData()
        {

            var vcusinfo = gridCus.GetFocusedRowDto<ChargeCusInfoDto>();
            ChargQueryCusDto QueryCustomerReg = new ChargQueryCusDto();
            QueryCustomerReg.CustomerBM = vcusinfo.CustomerBM;
            ICollection<ChargeCusInfoDto> ChargeCusInfo = ChargeAppService.ChargeCusInfo(QueryCustomerReg);
            vcusinfo = ChargeCusInfo.First();
            ICollection<ChargeCusInfoDto> dataOld = (ICollection<ChargeCusInfoDto>)gridCus.DataSource;
            var columnView = (ColumnView)gridCus.FocusedView;
            int focusedhandle = columnView.FocusedRowHandle;
            gridViewCus.SetRowCellValue(focusedhandle, CostState, vcusinfo.CostState);//已收
                                                                                      // gridViewCus.SetRowCellValue(focusedhandle, cusName, vcusinfo.CostStateDisplay);//已收
                                                                                      // gridViewCus.SetRowCellValue(focusedhandle, ItemSuitName, "dd");//已收
            decimal ysmoney = vcusinfo.McusPayMoney.PersonalPayMoney;
            gridViewCus.SetRowCellValue(gridViewCus.FocusedRowHandle, CustomerItemGroup, vcusinfo.McusPayMoney.PersonalMoney);//应收
            gridViewCus.SetRowCellValue(gridViewCus.FocusedRowHandle, MReceiptInfo, ysmoney);//已收
                                                                                             //消息推送                                                                               //消息推送
                                                                                             // PushMassage(vcusinfo.CustomerBM);

            //gridViewCus.SetFocusedValue(vcusinfo);
            //gridViewCus.RefreshRow(gridViewCus.FocusedRowHandle);
            //gridCus.RefreshDataSource();
        }
        #endregion

        private void butCombinedPayment_Click(object sender, EventArgs e)
        {
            try
            {
                //List<ChargeTypeDto> ChargeType = new List<ChargeTypeDto>();
                //ChargeType = (List<ChargeTypeDto>)comPaymentMethod.Properties.DataSource;

                List<ChargeTypeDto> ChargeType = new List<ChargeTypeDto>();
                int type = 2;
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = type.ToString();
                ChargeType = ChargeAppService.ChargeType(chargeBM);

                var Payment = new PaymentMethod();
                Payment.ChargeType = ChargeType;
                Payment.Receivable = decimal.Parse(txtReceivable.EditValue.ToString().Trim('¥'));
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
        #endregion
        #region 方法
        private string FormatSexs(object arg)
        {
            try
            {

                return _sexModels.Find(r => r.Id == (int)arg).Display;
            }
            catch
            {
                return _sexModels.Find(r => r.Id == (int)global::HealthExaminationSystem.Enumerations.Sex.GenderNotSpecified).Display;
            }
        }

        private string FormatClientType(object arg)
        {
            try
            {
                //var name = from m in tijianType where m.Value == (int)arg select m.Text;
                return tijianType.Find(r => r.Value == int.Parse(arg.ToString())).Text;

            }
            catch
            {
                return "";
            }
        }

        private string FormatIsAddMinus(object arg)
        {
            try
            {

                // return _AddMinusTypeModel.Find(r => r.Id == (int)arg).Display;
                return AddMinusTypeHelper.AddMinusTypeHelperFormatter(arg);
            }
            catch
            {
                return "";
            }
        }
        private string FormatCheckState(object arg)
        {
            try
            {

                return _ProjectIState.Find(r => r.Id == (int)arg).Display;
            }
            catch
            {
                return "";
            }
        }
        private string FormatRefundState(object arg)
        {
            try
            {

                // return _GroupPayerCatModels.Find(r => r.Id == (int)arg).Display;
                //if (arg.ToString() == "1")
                //{
                //    return "自费";
                //}
                //else
                //{
                    return GroupPayerCatHelper.GroupPayerCatHelperFormatter(arg);
                //}
            }
            catch
            {
                return "";
            }
        }
        private string SumMReceiptInfo(object arg)
        {
            string Money = "";
            ICollection<MReceiptInfoPerDto> ReceiptInfo = (ICollection<MReceiptInfoPerDto>)arg;
            Money = ReceiptInfo.Sum(r => r.Actualmoney).ToString("0.00");
            return Money;
        }
        /// <summary>
        /// 获取应收金额
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string SumGroupMoney(object arg)
        {
            string Money = "";
            ICollection<ChargeGroupsDto> ChargeGroups = (ICollection<ChargeGroupsDto>)arg;
            Money = ChargeGroups.Where(r => r.IsAddMinus != 3).Sum(r => r.PriceAfterDis).ToString("0.00");
            return Money;
        }
        private void getSyMoney()
        {
            if (txtAllMoney.Text != "" && txtReceiveMoney.Text != "")
            {
                txtSurplusMoney.Text = (decimal.Parse(txtAllMoney.EditValue.ToString()) - decimal.Parse(txtReceiveMoney.EditValue.ToString())).ToString("0.00");
                txtReceivable.Text = txtSurplusMoney.Text;
                txtpayMoney.Text = txtSurplusMoney.Text;
            }
        }



        #endregion

        private void txtAllMoney_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtAllMoney_TextChanged(object sender, EventArgs e)
        {
            getSyMoney();
        }

        private void txtReceiveMoney_TextChanged(object sender, EventArgs e)
        {
            getSyMoney();
        }

        private void butDetailed_Click(object sender, EventArgs e)
        {
            var dto = gridCus.GetFocusedRowDto<ChargeCusInfoDto>();
            if (dto == null)
            {
                XtraMessageBox.Show("请选择体检人！");
                return;
            }
            string customerBM = dto.CustomerBM;//体检号
            Guid CusomerregID = dto.Id;//预约ID
            //下面打开体检清单界面根据体检号，或预约ID找到这个人的所有收费记录
            using (var frm = new ChecklistReport(new Guid { }, customerBM))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    return;
            }
        }
        /// <summary>
        /// 根据结算ID打印体检清单
        /// </summary>
        /// <param name="ReceiptID"></param>
        /// <returns></returns>
        private bool PrinChekListByID(Guid ReceiptID)
        {
            using (var frm = new ChecklistReport(ReceiptID, ""))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    return true;
                else
                    return false;
            }

        }
        /// <summary>
        /// 根据结算ID打印发票
        /// </summary>
        /// <param name="ReceiptID"></param>
        /// <returns></returns>
        private bool PrininvoiceByID(Guid ReceiptID)
        {
            FrmInvoicePrint printForm = new FrmInvoicePrint(ReceiptID);
            if (printForm.ShowDialog() == DialogResult.Yes)
                return true;
            else
                return false;
        }

        private void butRecord_Click(object sender, EventArgs e)
        {
            var dto = gridCus.GetFocusedRowDto<ChargeCusInfoDto>();
            if (dto == null)
            {
                XtraMessageBox.Show("请选择体检人！");
                return;
            }
            var clientCharge = new ChargeList(dto.CustomerBM);
            clientCharge.ShowDialog();

        }

        private void txtpayMoney_TextChanged(object sender, EventArgs e)
        {
            decimal ResultNum;
            if (decimal.TryParse(txtpayMoney.EditValue.ToString(), out ResultNum))
            {
                if (txtpayMoney.EditValue.ToString().Trim().Equals(string.Empty) || txtReceivable.EditValue.ToString().Trim().Equals(string.Empty) || txtpayMoney.EditValue.ToString().Trim().Equals("."))
                {
                    labChange.Text = "0.00";
                    return;
                }

                decimal rePay = Convert.ToDecimal(txtReceivable.EditValue.ToString()) - Convert.ToDecimal(txtpayMoney.EditValue.ToString());
                labChange.Text = decimal.Parse(rePay.ToString()).ToString("0.00");
                if (rePay <= decimal.Zero)
                {
                    labChange.Appearance.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    labChange.Appearance.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void butCharge_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridViewCus_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "CostState")
            {
                e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
            }
        }
        /// <summary>
        /// 导出人员项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDaoChuXiangMu_Click(object sender, EventArgs e)
        {
            var rows = gridViewCus.GetSelectedRows();
            if (rows.Count() > 0)
            {
                if (gridGroups.DataSource != null)
                {
                    //List<ChargeGroupsDto> List = gridGroups.DataSource as List<ChargeGroupsDto>;
                    //DataTable table = new DataTable();
                    //table.Columns.Add("科室名称");
                    //table.Columns.Add("组合名称");
                    //table.Columns.Add("原价");
                    //table.Columns.Add("折后价");
                    //table.Columns.Add("收费");
                    //table.Columns.Add("个付");
                    //table.Columns.Add("团付");
                    //table.Columns.Add("加项");
                    //table.Columns.Add("检查");
                    //table.Columns.Add("收费状态");
                    //foreach (var item in List)
                    //{
                    //    var nRow = table.NewRow();
                    //    nRow["科室名称"] = item.DepartmentName;
                    //    nRow["组合名称"] = item.ItemGroupName;
                    //    nRow["原价"] = item.ItemPrice;
                    //    nRow["折后价"] = item.PriceAfterDis;
                    //    nRow["收费"] = item.PayerCat;
                    //    nRow["个付"] = item.GRmoney;
                    //    nRow["团付"] = item.TTmoney;
                    //    nRow["加项"] = item.IsAddMinus;
                    //    nRow["检查"] = item.CheckState;
                    //    nRow["收费状态"] = item.ChargeState;
                    //    table.Rows.Add(nRow);

                    //}
                    //var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    //saveFileDialog.FileName = "个人收费信息";
                    //saveFileDialog.Title = "导出Excel";
                    //saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                    //saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                    //var dialogResult = saveFileDialog.ShowDialog();
                    //if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                    //    return;
                    //ExcelHelper ex = new ExcelHelper(saveFileDialog.FileName);
                    //ex.DataTableToExcel(table, "项目信息", true);
                    //XtraMessageBox.Show("导出成功！");
                    ExcelHelper.ExportToExcel("项目信息", gridGroups);
                }
                else
                {
                    XtraMessageBox.Show("该患者没有体检项目！");
                    return;
                }
            }
            else
            {
                XtraMessageBox.Show("请选择患者！");
                return;
            }

        }
        /// <summary>
        /// 导出人员列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var rows = gridViewCus.GetSelectedRows();
            if (rows.Count() > 0)
            {
                if (gridGroups.DataSource != null)
                {
                    ExcelHelper.ExportToExcel("人员列表", gridCus);
                    //List<ChargeCusInfoDto> List = gridCus.DataSource as List<ChargeCusInfoDto>;
                    //DataTable table = new DataTable();
                    //table.Columns.Add("体检号");
                    //table.Columns.Add("姓名");
                    //table.Columns.Add("性别");
                    //table.Columns.Add("年龄");
                    //table.Columns.Add("单位");
                    //table.Columns.Add("套餐");
                    //table.Columns.Add("应收金额");
                    //table.Columns.Add("已收金额");
                    //table.Columns.Add("剩余金额");
                    //table.Columns.Add("体检类别");
                    //table.Columns.Add("体检日期");
                    //table.Columns.Add("收费状态");
                    //foreach (var item in List)
                    //{
                    //    var nRow = table.NewRow();
                    //    nRow["体检号"] = item.CustomerBM;
                    //    nRow["姓名"] = item.Customer.Name;
                    //    nRow["性别"] = item.Customer.Sex;
                    //    nRow["年龄"] = item.Customer.Age;
                    //    if (item.ClientInfo != null)
                    //    {
                    //        nRow["单位"] = item.ClientInfo.ClientName;
                    //    }
                    //    nRow["套餐"] = item.ItemSuitName;
                    //    if (item.McusPayMoney != null)
                    //    {
                    //        nRow["应收金额"] = item.McusPayMoney.PersonalMoney;
                    //        nRow["已收金额"] = item.McusPayMoney.PersonalPayMoney;
                    //    }
                    //    //nRow["剩余金额"] = item.Surplus;
                    //    nRow["体检类别"] = item.ClientType;
                    //    nRow["体检日期"] = item.LoginDate;
                    //    nRow["收费状态"] = item.CostState;
                    //    table.Rows.Add(nRow);

                    //}
                    //var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    //saveFileDialog.FileName = "人员列表";
                    //saveFileDialog.Title = "导出Excel";
                    //saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                    //saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                    //var dialogResult = saveFileDialog.ShowDialog();
                    //if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                    //    return;
                    //ExcelHelper ex = new ExcelHelper(saveFileDialog.FileName);
                    //ex.DataTableToExcel(table, "人员列表", true);
                    //XtraMessageBox.Show("导出成功！");
                }
                else
                {
                    XtraMessageBox.Show("该患者没有体检项目！");
                    return;
                }
            }
            else
            {
                XtraMessageBox.Show("请选择患者！");
                return;
            }
        }

        private void gridViewGroups_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.Name == cnSFType.Name && e.Value.ToString() != "")
            {
                e.DisplayText = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ChargeCategory, (int)e.Value)?.Text.ToString();
            }
        }

        private void gridViewGroups_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle < 0)
            //    return;
            //if (e.Column.Name == IsAddMinus.Name)
            //{
            //    //加项颜色
            //    var isadd = gridViewGroups.GetRowCellValue(e.RowHandle, IsAddMinus);
            //    if (isadd == null)
            //        return;
            //if (Convert.ToInt32(isadd) == (int)AddMinusType.Add)
            //    {

            //        e.Appearance.ForeColor = Color.Red;

            //    }
            //    else if (Convert.ToInt32(isadd) == (int)AddMinusType.Minus)
            //    {
            //        e.Appearance.ForeColor = Color.Green;
            //        e.Appearance.FontStyleDelta = FontStyle.Strikeout;
            //    }
            //    else
            //    {
            //        e.Appearance.ForeColor = Color.Black;
            //        Appearance.FontStyleDelta = FontStyle.Regular;
            //    }
            //}
            ////收费颜色
            //if (e.Column.Name == gridColumn3.Name)
            //{
            //    var isChage = gridViewGroups.GetRowCellValue(e.RowHandle, gridColumn3);
            //    if (isChage == null)
            //        return;
            //    if (isChage.ToString() == "未收费")
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        e.Appearance.ForeColor = Color.Black;
            //    }
            //}
        }

        private decimal summaryGrMoney;

        private decimal summaryPriceAfterDis;

        private decimal summaryItemPrice;

        private decimal summaryGrMoney1;

        private decimal summaryPriceAfterDis1;

        private decimal summaryItemPrice1;

        private void gridViewGroups_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsGroupSummary)
            {
                if (e.Item is GridGroupSummaryItem gridGroupSummaryItem)
                {
                    if (gridGroupSummaryItem.Tag is int tag)
                    {
                        if (tag == 1)
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    summaryGrMoney = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.FieldValue != null)
                                    {
                                        var row = (ChargeGroupsDto)e.Row;
                                        if (row.IsAddMinus != (int)AddMinusType.Minus)
                                        {
                                            summaryGrMoney = summaryGrMoney + (decimal)e.FieldValue;
                                        }
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = summaryGrMoney;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else if (tag == 2)
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    summaryPriceAfterDis = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.FieldValue != null)
                                    {
                                        var row = (ChargeGroupsDto)e.Row;
                                        if (row.IsAddMinus != (int)AddMinusType.Minus)
                                        {
                                            summaryPriceAfterDis = summaryPriceAfterDis + (decimal)e.FieldValue;
                                        }
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = summaryPriceAfterDis;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else if (tag == 3)
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    summaryItemPrice = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.FieldValue != null)
                                    {
                                        var row = (ChargeGroupsDto)e.Row;
                                        if (row.IsAddMinus != (int)AddMinusType.Minus)
                                        {
                                            summaryItemPrice = summaryItemPrice + (decimal)e.FieldValue;
                                        }
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = summaryItemPrice;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }
            else if (e.IsTotalSummary)
            {
                if (e.Item is GridColumnSummaryItem gridColumnSummaryItem)
                {
                    if (gridColumnSummaryItem.Tag is int tag)
                    {
                        if (tag == 1)
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    summaryGrMoney1 = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.FieldValue != null)
                                    {
                                        var row = (ChargeGroupsDto)e.Row;
                                        if (row.IsAddMinus != (int)AddMinusType.Minus)
                                        {
                                            summaryGrMoney1 = summaryGrMoney1 + (decimal)e.FieldValue;
                                        }
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = summaryGrMoney1;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else if (tag == 2)
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    summaryPriceAfterDis1 = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.FieldValue != null)
                                    {
                                        var row = (ChargeGroupsDto)e.Row;
                                        if (row.IsAddMinus != (int)AddMinusType.Minus)
                                        {
                                            summaryPriceAfterDis1 = summaryPriceAfterDis1 + (decimal)e.FieldValue;
                                        }
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = summaryPriceAfterDis1;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else if (tag == 3)
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    summaryItemPrice1 = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.FieldValue != null)
                                    {
                                        var row = (ChargeGroupsDto)e.Row;
                                        if (row.IsAddMinus != (int)AddMinusType.Minus)
                                        {
                                            summaryItemPrice1 = summaryItemPrice1 + (decimal)e.FieldValue;
                                        }
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = summaryItemPrice1;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }
        }

        private void PersonCharge_Shown(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = layoutControlGroup4.Width;
        }

        private void txtCardNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && txtCardNum.Text.Trim() !="")
            {
                #region 读会员卡
                if (comPaymentMethod.Text.Trim() == "会员卡")
                {
                    decimal YFJE = decimal.Parse(txtpayMoney.EditValue.ToString()) - decimal.Parse(labChange.Text);
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
                        groupPayInfo.Text = "支付信息 <Color=Red> 卡号：" + ja1a + ",类别：" + lb + ",卡余额：" + hyzk + "</Color>";

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
                else if (comPaymentMethod.Text.Trim() == "单位储值卡")
                {
                    getJJK();
                }
                #endregion
            }
        }
        private bool getJJK()
        {
            // decimal YFJE = decimal.Parse(txtpayMoney.EditValue.ToString()) - decimal.Parse(labChange.Text);
            if (txtCardNum.Text.Trim() == "")
            {
                XtraMessageBox.Show("使用单位储值卡时，卡号不能为空！");
                return false ;
            }
            CusCardDto cusCardDto = new CusCardDto();

            cusCardDto.CardNo = txtCardNum.Text;

            var carinfo = customerSvr.getSuitbyCardNum(cusCardDto);
            if (carinfo.Code == 0)
            {
                XtraMessageBox.Show(carinfo.Err);
                return false;
            }
            else
            {
                try
                {
                    decimal YFJE = decimal.Parse(txtpayMoney.EditValue.ToString()) - decimal.Parse(labChange.Text);

                    if (!carinfo.CardCategory.Contains("储值卡"))
                    {
                        XtraMessageBox.Show("非单位储值卡,不能用于支付!");
                        return false ;
                    }
                    groupPayInfo.Text = "支付信息 <Color=Red> 卡号：" + cusCardDto.CardNo + ",卡余额：" + carinfo.FaceValue + "</Color>";

                    if (carinfo.FaceValue < YFJE)
                    {
                        MessageBox.Show("单位储值卡余额不足，请充值！");
                        return false;
                    }
                    return true;
                }
                catch
                {
                    MessageBox.Show("该会员卡不存在，请核实！");
                    return false;
                }

            }
        }
    }
}
