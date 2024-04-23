using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using Abp.Application.Services.Dto;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Company;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System.Drawing;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class ClientCharge : UserBaseForm
    {
        private List<ClientRegDto> clientRegs;//单位及分组字典
        private ICustomerAppService customerSvr;//体检预约
        private IChargeAppService ChargeAppService;
        private readonly List<SexModel> _sexModels;
        public CommonAppService CommonAppSrv;
        private List<ClientPayCusLisViewDto> PClientPayCusLis;
        //private List<ChargeTeamMoneyViewDto> PChargeTeamMoneys;
        private IClientRegAppService clientRegAppService;
        ICustomerAppService _customerSvr = new CustomerAppService();
        public ClientCharge()
        {
            InitializeComponent();
            CommonAppSrv = new CommonAppService();
            clientRegAppService = new ClientRegAppService();
            ChargeAppService = new ChargeAppService();
            customerSvr = new CustomerAppService();

            _sexModels = SexHelper.GetSexModelsForItemInfo();
            gridViewCusLis.Columns[cusSex.FieldName].DisplayFormat.Format = new CustomFormatter(FormatSexs);

            // gridViewCusLis.Columns[RegisterStates.FieldName].DisplayFormat.Format = new CustomFormatter(FormatCheckSate);


        }
        private void txtReceivable_TextChanged(object sender, EventArgs e)
        {
            SurplusMoney();
            PayProportion();
        }
        #region 事件
        private void ClientCharge_Load(object sender, EventArgs e)
        {
            try
            {
                //gridViewClientTeam.OptionsSelection.MultiSelect = true;
                //gridViewClientTeam.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                //gridViewClientTeam.OptionsView.ShowIndicator = false;//不显示指示器
                //gridViewClientTeam.OptionsBehavior.ReadOnly = false;
                //gridViewClientTeam.OptionsBehavior.Editable = false;
                gridViewCusLis.OptionsSelection.MultiSelect = true;
                gridViewCusLis.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                gridViewCusLis.OptionsView.ShowIndicator = false;//不显示指示器
                gridViewCusLis.OptionsBehavior.ReadOnly = false;
                gridViewCusLis.OptionsBehavior.Editable = false;
                FullClientRegDto dto = new FullClientRegDto();
                if (chekSeal.Checked == true)
                {
                    dto.FZState = 2;
                }
                clientRegs = customerSvr.QuereyClientRegInfos(dto).Where(o => o.FZState == 2).ToList();//加载单位分组数据
                txtClientRegID.Properties.DataSource = clientRegs;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return;
            }
        }

        private void butCharge_Click(object sender, EventArgs e)
        {

            if (txtClientRegID.EditValue != null && txtClientRegID.EditValue?.ToString() != "")
            {


                int[] selectRows = gridViewCusLis.GetSelectedRows();
                var chargeBmIds = new List<EntityDto<Guid>>();
                foreach (int num in selectRows)
                {
                    if (num < 0)
                        continue;
                    var id = (Guid)gridViewCusLis.GetRowCellValue(num, gridColumnCustomerRegId);
                    chargeBmIds.Add(new EntityDto<Guid> { Id = id });
                }

                ClientChargelist clientChargelist = new ClientChargelist();
                clientChargelist.GuidClientRegID = ((ClientRegDto)txtClientRegID.EditValue).Id;
                clientChargelist.CusList = chargeBmIds;
                clientChargelist.gtClientName = ((ClientRegDto)txtClientRegID.EditValue).ClientInfo.ClientName;
                var dr = clientChargelist.ShowDialog();
                if (dr == DialogResult.Cancel)
                    SBRefresh.PerformClick();
            }
            else
            {
                ShowMessageBoxInformation("没有要收费的项");
            }
        }
        /// <summary>
        /// 编辑控件中的按钮点击事件
        /// </summary>
        private void edior_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
                if (editor.Name == txtClientRegID.Name)
                    txtClientRegID.EditValue = null;
            }

        }
        private void txtClientRegID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtClientRegID.EditValue != null)
                {
                    EntityDto<Guid> charge = new EntityDto<Guid>();
                    charge.Id = ((ClientRegDto)txtClientRegID.EditValue).Id;
                    ClientRegDto clientRegDto = clientRegAppService.GetClientRegByID(charge);
                    string fzst = "";
                    if (clientRegDto.FZState == 1)
                    {
                        labFZState.Text = "<Color=Red>单位已封账</Color>";
                        butSeal.Text = "解账";
                        butCharge.Enabled = false;
                    }
                    else
                    {
                        labFZState.Text = "单位未封账";
                        butSeal.Text = "封账";
                        butCharge.Enabled = true;
                    }
                    int confirmstate = (int)ConfirmState.Confirmed;
                    if (clientRegDto.ConfirmState.HasValue && clientRegDto.ConfirmState.Value == confirmstate)
                    {
                        labConfirmState.Text = "<Color=Red>费用已确认</Color>";
                        butClientPay.Text = "取消审核";
                    }
                    else
                    {
                        labConfirmState.Text = "费用未确认";
                        butClientPay.Text = "审核";

                    }
                    labLinkMan.Text = "联系人：" + clientRegDto.linkMan?.ToString();

                    //获取分组列表
                    //List<ChargeTeamMoneyViewDto> ClientPayCusLi = ChargeAppService.GetClientTeamList(charge);
                    //gridControlTeam.DataSource = ClientPayCusLi;
                    //获取人员列表
                    List<ClientPayCusLisViewDto> ClientPayCusLisView = ChargeAppService.GetClientCusList(charge);
                    gridControlCusLis.DataSource = ClientPayCusLisView;
                    //gridViewCusLis.ViewCaption="单位信息："+ txtClientRegID.Text.Trim() + "，介绍人：" + clientRegDto.linkMan?.ToString() + ",时间："+CommonAppSrv.GetDateTimeNow().Now.Date.ToString("yyyy年MM月dd日");
                    gridViewCusLis.ViewCaption = $@"单位：{txtClientRegID.Text.Trim()}，介绍人：{clientRegDto.linkMan}，时间：{CommonAppSrv.GetDateTimeNow().Now:yyyy年MM月dd日}";
                    //项目列表  
                    QueryClass query = new QueryClass();
                    List<Guid?> clientRegs = new List<Guid?>();
                    clientRegs.Add(charge.Id);
                    query.ClientInfoId = clientRegs;
                    var itemlist = ChargeAppService.KSGZLStatistics(query);
                    //for (int nn = 0; nn < itemlist.Count; nn++)
                    //{
                    //    itemlist[nn].ClientName = txtClientRegID.Text ;
                    //    itemlist[nn].LinkMan = clientRegDto.linkMan?.ToString();
                    //}
                    gcItem.DataSource = itemlist;
                    //gvItem.ViewCaption= "单位信息：" + txtClientRegID.Text.Trim() + "，介绍人：" + clientRegDto.linkMan?.ToString() + "，时间：" + CommonAppSrv.GetDateTimeNow().Now.Date.ToString("yyyy年MM月dd日");
                    gvItem.ViewCaption =
                        $@"单位：{txtClientRegID.Text.Trim()}，介绍人：{clientRegDto.linkMan}，时间：{CommonAppSrv.GetDateTimeNow().Now:yyyy年MM月dd日}";

                    PClientPayCusLis = ClientPayCusLisView;
                    txtGroupMoney.Text = gridViewCusLis.Columns[TeamMoney.FieldName].SummaryItem.SummaryValue?.ToString();
                    txtAddMoney.Text = gridViewCusLis.Columns[ClientAddMoney.FieldName].SummaryItem.SummaryValue?.ToString();
                    txtSubtractMoney.Text = gridViewCusLis.Columns[ClientMinusMoney.FieldName].SummaryItem.SummaryValue?.ToString();
                    txtCheckMoney.Text = gridViewCusLis.Columns[ChekAllMoney.FieldName].SummaryItem.SummaryValue?.ToString();
                    if (clientRegDto.PersonnelCategoryId.HasValue && clientRegDto.PersonnelCategory.IsFree == true)
                    {
                        txtDiscountPrice.Text = "0.00";
                    }
                    else
                    {
                        txtDiscountPrice.Text = gridViewCusLis.Columns[ClientMoney.FieldName].SummaryItem.SummaryValue?.ToString();
                    }

                    List<MReceiptClientDto> MReceiptClient = ChargeAppService.MInvoiceRecorView(charge);
                    int receiptstte = (int)InvoiceStatus.Valid;
                    var sumMcusPayMoneys = MReceiptClient?.Where(r => r.ReceiptSate == receiptstte).Sum(r => r?.Actualmoney);
                    txtReceivable.Text = sumMcusPayMoneys.ToString();
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return;
            }
        }
        private void txtSubtractMoney_TextChanged(object sender, EventArgs e)
        {
            txtTxMoney();
        }

        private void txtAddMoney_TextChanged(object sender, EventArgs e)
        {
            txtTxMoney();
        }

        private void txtDiscountPrice_TextChanged(object sender, EventArgs e)
        {
            Discount();
            SurplusMoney();
            PayProportion();
        }
        #endregion
        #region 方法
        //FormatCheckSate
        private string FormatCheckSate(object arg)
        {
            if ((int)arg == 1)
            {
                return "未到检";
            }
            else
            {
                return "已到检";
            }
        }
        private string FormatSexs(object arg)
        {
            try
            {

                return _sexModels.Find(r => r.Id == (int)arg).Display;
            }
            catch
            {
                return _sexModels.Find(r => r.Id == (int)Sex.GenderNotSpecified).Display;
            }
        }
        private string SumTJRS(object arg)
        {

            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Count.ToString();
            return TJRS;

        }
        //SumSJ
        private string SumSJRS(object arg)
        {
            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Where(r => r.CheckSate != 1).Count().ToString();
            return TJRS;
        }
        private string SumSJMoney(object arg)
        {

            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        //SumJX
        private string SumAddMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumYSMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumJQX(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumTXAddMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAdjustAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumTXJXMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAdjustMinusMoney);
            string ss = sumMcusPayMoneys.Value.ToString();
            return sumMcusPayMoneys.Value.ToString();
        }
        private void txtTxMoney()
        {
            if (txtAddMoney.EditValue != null && txtSubtractMoney.EditValue != null && txtAddMoney.EditValue?.ToString() != "" && txtSubtractMoney.EditValue?.ToString() != "")
            {
                txtAdjustmentMoney.EditValue = decimal.Parse(txtAddMoney.EditValue.ToString()) - decimal.Parse(txtSubtractMoney.EditValue.ToString());
            }
            else
            {
                txtAdjustmentMoney.EditValue = "0.00";
            }
        }
        private void Discount()
        {
            if (txtGroupMoney.EditValue != null && txtDiscountPrice.EditValue != null && txtGroupMoney.EditValue?.ToString() != "" && txtDiscountPrice.EditValue?.ToString() != "" && decimal.Parse(txtGroupMoney.EditValue?.ToString()) != 0)
            {
                txtDiscount.EditValue = (decimal.Parse(txtDiscountPrice.EditValue.ToString()) / decimal.Parse(txtGroupMoney.EditValue.ToString())).ToString("0.00");
            }
            else
            {
                txtDiscount.EditValue = "0.00";
            }
        }
        private void SurplusMoney()
        {
            if (txtSurplusMoney.EditValue != null && txtDiscountPrice.EditValue != null && txtSurplusMoney.EditValue?.ToString() != "" && txtDiscountPrice.EditValue?.ToString() != "")
            {
                txtSurplusMoney.EditValue = decimal.Parse(txtDiscountPrice.EditValue.ToString()) - decimal.Parse(txtReceivable.EditValue.ToString());
            }
            else
            {
                txtSurplusMoney.EditValue = "0.00";
            }
        }
        private void PayProportion()
        {
            if (txtSurplusMoney.EditValue != null && txtDiscountPrice.EditValue != null && txtSurplusMoney.EditValue?.ToString() != "" && txtDiscountPrice.EditValue?.ToString() != "" && decimal.Parse(txtDiscountPrice.EditValue.ToString()) != 0)
            {
                txtPayProportion.EditValue = (decimal.Parse(txtReceivable.EditValue.ToString()) / decimal.Parse(txtDiscountPrice.EditValue.ToString())).ToString("0.00");
            }
            else
            {
                txtPayProportion.EditValue = "0.00";

            }
        }


        #endregion

        private void butAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClientRegID.EditValue != null)
                {
                    CreateReceiptInfoDto input = new CreateReceiptInfoDto();
                    input.Actualmoney = decimal.Parse("0");
                    if (CommonAppSrv == null)
                    {
                        CommonAppSrv = new CommonAppService();
                    }
                    input.ChargeDate = CommonAppSrv.GetDateTimeNow().Now;
                    input.ChargeState = 1;
                    input.ClientRegid = ((ClientRegDto)txtClientRegID.EditValue).Id;//关联已有对象               
                    input.Discontmoney = 0;

                    input.DiscontReason = "";
                    input.Discount = 1;
                    input.ReceiptSate = 1;//和上面重复了ChargeState
                    input.Remarks = "";
                    input.SettlementSate = 2;
                    input.Shouldmoney = decimal.Parse(txtReceivable.EditValue.ToString());
                    input.Summoney = decimal.Parse("0");
                    input.TJType = 2;
                    input.Userid = CurrentUser.Id;
                    ChargeAppService.InsertClientReceiptDto(input);
                    XtraMessageBox.Show("建账成功！");
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        private void groupCusList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "已到检")
            {
                List<ClientPayCusLisViewDto> ClientPayCusLisView = PClientPayCusLis.Where(r => r.CheckSate != 1).ToList();
                gridControlCusLis.DataSource = ClientPayCusLisView;

            }
        }

        private void groupCusList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            gridControlCusLis.DataSource = PClientPayCusLis;

        }

        private void chekSeal_CheckedChanged(object sender, EventArgs e)
        {
            FullClientRegDto dto = new FullClientRegDto();
            if (chekSeal.Checked == true)
            {
                dto.FZState = 2;
                labFZState.Text = "封账状态";
                labFZState.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                butSeal.Text = "封账";
                butCharge.Enabled = true;
            }
            else
            {
                dto.FZState = 1;
                labFZState.Text = "封账状态";
                labFZState.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                butSeal.Text = "解账";
                butCharge.Enabled = false;
            }
            //gridControlTeam.DataSource = null;
            gridControlCusLis.DataSource = null;
            txtGroupMoney.Text = "0";
            txtDiscount.Text = "";
            txtDiscountPrice.Text = "0";
            txtCheckMoney.Text = "0";
            txtAdjustmentMoney.Text = "0";
            txtAddMoney.Text = "0";
            txtSubtractMoney.Text = "0";
            txtChange.Text = "0";
            txtSurplusMoney.Text = "0";
            txtReceivable.Text = "0";
            txtPayProportion.Text = "";
            clientRegs = customerSvr.QuereyClientRegInfos(dto);//加载单位分组数据
            txtClientRegID.Properties.DataSource = clientRegs;
        }

        private void butSeal_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClientRegID.EditValue == null || txtClientRegID.EditValue?.ToString() == "")
                {
                    XtraMessageBox.Show("请选择单位");
                    return;
                }
              
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Id = ((ClientRegDto)txtClientRegID.EditValue).Id;

                if (butSeal.Text == "封账")
                {
                    chargeBM.Name = "1";
                    TJSQDto input = new TJSQDto();
                    input.DWMC = txtClientRegID.Text;
                    input.ClientRegId = chargeBM.Id;
                    var appls = _customerSvr.getapplication(input);
                    if (appls.Any(p => p.SQSTATUS == 1))
                    {
                        DialogResult dr = XtraMessageBox.Show("包含未收费的申请单是否确认封账？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.Cancel)
                        {
                            return;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("无解帐权限");
                    return;
                }
                ClientRegDto clientRegDto = ChargeAppService.UpZFState(chargeBM);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = clientRegDto.ClientRegBM;
                createOpLogDto.LogName = clientRegDto.ClientInfo.ClientName;
                if (clientRegDto.FZState == 1)
                {
                    createOpLogDto.LogText = "单位封账";
                }
                else if (clientRegDto.FZState == 2)
                {
                    createOpLogDto.LogText = "单位解账";
                }
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ChargId;
                CommonAppSrv.SaveOpLog(createOpLogDto);
                var index = 0;
                if (clientRegDto.FZState == 1)
                {
                    labFZState.Text = "单位已封账";
                    labFZState.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    butSeal.Text = "解账";
                    butCharge.Enabled = false;
                    for (int i = 0; i < clientRegs.Count; i++)
                    {
                        if (clientRegs[i].Id == clientRegDto.Id)
                            index = i;
                    }
                    clientRegs.RemoveAt(index);
                }
                else
                {
                    //MessageBox.Show("无解帐权限");
                    

                    //labFZState.Text = "单位未封账";
                    //labFZState.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
                    //butSeal.Text = "封账";
                    //butCharge.Enabled = true;
                    //for (int i = 0; i < clientRegs.Count; i++)
                    //{
                    //    if (clientRegs[i].Id == clientRegDto.Id)
                    //        index = i;
                    //}
                    ////txtClientRegID.Properties.DataSource = null;
                    //clientRegs.RemoveAt(index);
                    //txtClientRegID.Properties.DataSource = clientRegs;
                    //txtClientRegID.EditValue = clientRegDto;
                }

                XtraMessageBox.Show("修改成功！");
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void buTexport_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = txtClientRegID.Text + "-" + labLinkMan.Text.Replace("联系人：", "") + "-人员信息";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            ExportToExcel(saveFileDialog, gridControlCusLis);
            // gridControlCusLis.ExportToXls(saveFileDialog.FileName);
            saveFileDialog.FileName = saveFileDialog.FileName.Replace("人员信息", "项目信息");

            gridColumn14.Visible = false;
            gridColumn15.Visible = false;
            gridColumn2.Visible = false;
            ExportToExcel(saveFileDialog, gcItem);
            // gcItem.ExportToXls(saveFileDialog.FileName);
            gridColumn14.Visible = true;
            gridColumn15.Visible = true;
            gridColumn2.Visible = true;
            gridColumn14.VisibleIndex = 6;
            gridColumn15.VisibleIndex = 7;
            gridColumn2.VisibleIndex = 8;
            XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        public static void ExportToExcel(System.Windows.Forms.SaveFileDialog saveFileDialog, params DevExpress.XtraPrinting.IPrintable[] panels)
        {

            string FileName = saveFileDialog.FileName;
            var ps = new DevExpress.XtraPrinting.PrintingSystem();
            var link = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
            ps.Links.Add(link);
            foreach (var panel in panels)
            {
                link.Links.Add(CreatePrintableLink(panel));
            }
            link.Landscape = true;//横向
                                  //判断是否有标题，有则设置
                                  //link.CreateDocument(); //建立文档
            int count = 1;
            //在重复名称后加（序号）
            while (System.IO.File.Exists(FileName))
            {
                if (FileName.Contains(")."))
                {
                    int start = FileName.LastIndexOf("(");
                    int end = FileName.LastIndexOf(").") - FileName.LastIndexOf("(") + 2;
                    FileName = FileName.Replace(FileName.Substring(start, end), string.Format("({0}).", count));
                }
                else
                {
                    FileName = FileName.Replace(".", string.Format("({0}).", count));
                }
                count++;
            }
            if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
            {
                var options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXlsx(FileName, options);
            }
            else
            {
                var options = new DevExpress.XtraPrinting.XlsExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXls(FileName, options);
            }
        }
        /// <summary>
        /// 创建打印Componet
        /// </summary>
        /// <param name="printable"></param>
        /// <returns></returns>
        public static DevExpress.XtraPrinting.PrintableComponentLink CreatePrintableLink(DevExpress.XtraPrinting.IPrintable printable)
        {
            var chart = printable as DevExpress.XtraCharts.ChartControl;
            if (chart != null)
                chart.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Stretch;
            var printableLink = new DevExpress.XtraPrinting.PrintableComponentLink() { Component = printable };
            return printableLink;
        }
        private void SBRefresh_Click(object sender, EventArgs e)
        {
            if (txtClientRegID.EditValue == null || txtClientRegID.EditValue?.ToString() == "")
            {
                XtraMessageBox.Show("请选择单位");
                return;
            }
            txtClientRegID_EditValueChanged(sender, e);
        }

        private void xtraTabControl1_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            // e.Button.Caption
            if (e.Button.Caption == "已到检")
            {
                List<ClientPayCusLisViewDto> ClientPayCusLisView = PClientPayCusLis.Where(r => r.CheckSate != 1).ToList();
                gridControlCusLis.DataSource = ClientPayCusLisView;
                e.Button.Caption = "全部";
                gridViewCusLis.OptionsBehavior.AutoExpandAllGroups = true;
                return;

            }
            if (e.Button.Caption == "全部")
            {
                gridControlCusLis.DataSource = PClientPayCusLis;
                e.Button.Caption = "已到检";
                gridViewCusLis.OptionsBehavior.AutoExpandAllGroups = true;
                return;

            }
        }

        private void gvItem_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            try
            {
                //GridGroupRowInfo gridGroupRowInfo = e.Info as GridGroupRowInfo;
                //if (gridGroupRowInfo.IsGroupRow == true)
                //{
                //    if (gridGroupRowInfo.Column == gridColumn4)
                //    {


                //        GridView gridView = sender as GridView;
                //        int index = gridView.GetDataSourceRowIndex(e.RowHandle);
                //        List<SearchKSGZLStatisticsDto> searchKSGZLStatisticsDtos = (List<SearchKSGZLStatisticsDto>)gridView.DataSource;
                //        string depname = searchKSGZLStatisticsDtos[index].TeamName.ToString();
                //        //gridView.GetRowCellValue(e.RowHandle, "TeamName").ToString();

                //        //gridGroupRowInfo.AddColumnSummaryItem(gridColumn4, new GridGroupSummaryItem(SummaryItemType.Sum, gridColumn12.FieldName, gridColumn12, "d"), null);
                //        //gridGroupRowInfo.AddColumnSummaryItem(gridColumn12, new GridGroupSummaryItem(SummaryItemType.Sum, gridColumn12.FieldName, gridColumn12, "d"), null);
                //        //gridGroupRowInfo.GroupText = depname
                //        //    + "    总金额:￥" + searchKSGZLStatisticsDtos.Where(o => o.TeamName == depname).Sum(o => o.ActualMoney).ToString() + "元  " +
                //        //"应收金额:￥" + searchKSGZLStatisticsDtos.Where(o => o.TeamName == depname).Sum(o => o.ShouldMoney).ToString() + "元  " +
                //        //"已检金额:￥" + searchKSGZLStatisticsDtos.Where(o => o.TeamName == depname).Sum(o => o.CheckMoney).ToString() + "元";

                //    }
                //    else if (gridGroupRowInfo.Column == gridColumn3)
                //    {
                //        GridView gridView = sender as GridView;
                //        int index = gridView.GetDataSourceRowIndex(e.RowHandle);
                //        List<SearchKSGZLStatisticsDto> searchKSGZLStatisticsDtos = (List<SearchKSGZLStatisticsDto>)gridView.DataSource;
                //        string teamname = searchKSGZLStatisticsDtos[index].TeamName;
                //        string depname = searchKSGZLStatisticsDtos[index].DepartmentName;
                //        // gridGroupRowInfo.AddColumnSummaryItem(gridColumn4, new GridGroupSummaryItem(SummaryItemType.Sum, gridColumn12.FieldName, gridColumn12, "d"), null);
                //       // gridGroupRowInfo.AddColumnSummaryItem(gridColumn12, new GridGroupSummaryItem(SummaryItemType.Sum, gridColumn12.FieldName, gridColumn12, "d"), null);
                //        //gridGroupRowInfo.GroupText = depname
                //        //    + "    总金额:￥" + searchKSGZLStatisticsDtos.Where(o => o.TeamName == teamname && o.DepartmentName == depname).Sum(o => o.ActualMoney).ToString() + "元  " +
                //        //"应收金额:￥" + searchKSGZLStatisticsDtos.Where(o => o.TeamName == teamname && o.DepartmentName == depname).Sum(o => o.ShouldMoney).ToString() + "元  " +
                //        //"已检金额:￥" + searchKSGZLStatisticsDtos.Where(o => o.TeamName == teamname && o.DepartmentName == depname).Sum(o => o.CheckMoney).ToString() + "元";
                //    }
                //}
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void butClientPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClientRegID.EditValue == null || txtClientRegID.EditValue?.ToString() == "")
                {
                    XtraMessageBox.Show("请选择单位");
                    return;
                }
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Id = ((ClientRegDto)txtClientRegID.EditValue).Id;
                int que = 0;
                if (butClientPay.Text == "审核")
                {
                    que = (int)ConfirmState.Confirmed;
                    chargeBM.Name = que.ToString();
                }
                else
                {
                    que = (int)ConfirmState.Unconfirmed;
                    chargeBM.Name = que.ToString();
                }
                ClientRegDto clientRegDto = ChargeAppService.UpConfirmState(chargeBM);
                if (clientRegDto.ConfirmState == (int)ConfirmState.Confirmed)
                {
                    //  labFZState.Text = "<Color=Red>单位已封账</Color>";
                    labConfirmState.Text = "<Color=Red>费用已确认</Color>";
                    // labConfirmState.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    butClientPay.Text = "取消审核";
                    //  butCharge.Enabled = false;                   
                }
                else
                {
                    labConfirmState.Text = "费用未确认";
                    //labConfirmState.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
                    butClientPay.Text = "审核";
                    // butCharge.Enabled = true;                  
                }

                XtraMessageBox.Show("修改成功！");
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void butChargeSetting_Click(object sender, EventArgs e)
        {
            if (txtClientRegID.EditValue == null || txtClientRegID.EditValue?.ToString() == "")
            {
                XtraMessageBox.Show("请选择单位");
                return;
            }
            if (txtClientRegID.EditValue != null)
            {
                EntityDto<Guid> charge = new EntityDto<Guid>();
                charge.Id = ((ClientRegDto)txtClientRegID.EditValue).Id;
                ClientRegDto clientRegDto = clientRegAppService.GetClientRegByID(charge);
                if (clientRegDto.FZState == 1)
                {
                    MessageBox.Show("单位已封账,不能进行收费设置！");
                    return;
                }
                var datasource = ChargeAppService.GetClientTeamList(charge);
                List<Guid> list = new List<Guid>();
                if (datasource == null)
                {
                    ShowMessageBoxInformation("请添加分组！");
                    return;
                }
                foreach (var item in datasource)
                {
                    list.Add(item.Id);
                }
                using (var frm = new FrmClientTeamCharge(list, clientRegDto.ClientInfo.ClientName, clientRegDto.ClientRegBM, clientRegDto.ClientInfo?.Id))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        txtClientRegID_EditValueChanged(sender, e);
                        return;
                    }

                }
            }
            else
            {
                MessageBox.Show("请选择单位！");
            }
        }

        private void gvItem_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.Name == cnSFType.Name && e.Value.ToString() != "")
            {
                e.DisplayText = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ChargeCategory, (int)e.Value).Text.ToString();
            }
        }

        private void gvItem_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            if (e.Column.Name == gridColumn2.Name)
            {
                var isAdd = gvItem.GetRowCellValue(e.RowHandle, gridColumn2)?.ToString();
                if (isAdd == null)
                    return;
                if (isAdd == "加项")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (isAdd == "减项")
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.FontStyleDelta = FontStyle.Strikeout;
                }
            }
        }

        private void ClientCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
                butCharge.PerformClick();
        }

        private string SfType;

        private void gvItem_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
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
                                    SfType = string.Empty;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.FieldValue != null && string.IsNullOrWhiteSpace(SfType))
                                    {
                                        SfType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ChargeCategory, (int)e.FieldValue).Text;
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = $"{SfType}科室合计";
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtClientRegID.EditValue != null && txtClientRegID.EditValue?.ToString() != "")
            {      

                frmApplivation clientChargelist = new frmApplivation();
                clientChargelist.GuidClientRegID = ((ClientRegDto)txtClientRegID.EditValue).Id;            
                clientChargelist.gtClientName = ((ClientRegDto)txtClientRegID.EditValue).ClientInfo.ClientName;
                var clientDto = (ClientRegDto)txtClientRegID.EditValue;
                var dr = clientChargelist.ShowDialog();
                if (dr == DialogResult.Cancel)
                    SBRefresh.PerformClick();

            }
            else
            {
                ShowMessageBoxInformation("没有要收费的项");
            }
        }

        private void butCencelSeal_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClientRegID.EditValue == null || txtClientRegID.EditValue?.ToString() == "")
                {
                    XtraMessageBox.Show("请选择单位");
                    return;
                }

                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Id = ((ClientRegDto)txtClientRegID.EditValue).Id;

                chargeBM.Name = "2";
               
                ClientRegDto clientRegDto = ChargeAppService.UpZFState(chargeBM);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = clientRegDto.ClientRegBM;
                createOpLogDto.LogName = clientRegDto.ClientInfo.ClientName;
                if (clientRegDto.FZState == 1)
                {
                    createOpLogDto.LogText = "单位封账";
                }
                else if (clientRegDto.FZState == 2)
                {
                    createOpLogDto.LogText = "单位解账";
                }
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ChargId;
                CommonAppSrv.SaveOpLog(createOpLogDto);
                       

                XtraMessageBox.Show("修改成功！");
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
    }
}
