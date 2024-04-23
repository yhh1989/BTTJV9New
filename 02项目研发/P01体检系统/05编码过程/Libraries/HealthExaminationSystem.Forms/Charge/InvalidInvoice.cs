using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using gregn6Lib;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.ChecklistReport;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.InvoicePrint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class InvalidInvoice : UserBaseForm
    {
        private readonly List<ProjectIStateModel> _ProjectIState;
        public CommonAppService CommonAppSrv;
        private IChargeAppService ChargeAppService;
        GridppReport Report = new GridppReport();
        public InvalidInvoice()
        {
            InitializeComponent();
            ChargeAppService = new ChargeAppService();
            CommonAppSrv = new CommonAppService();
            _ProjectIState = ProjectIStateHelper.GetProjectIStateModels();
        }
        #region  事件
        private void InvalidInvoice_Load(object sender, EventArgs e)
        {
           // GridViewInvice.Columns[CustomerName.FieldName].DisplayFormat.Format = new CustomFormatter(FormatCheckName);
           // GridViewInvice.Columns[MPayment.FieldName].DisplayFormat.Format = new CustomFormatter(FormatMPayment);
            GridViewInvice.Columns[ReceiptSate.FieldName].DisplayFormat.Format = new CustomFormatter(FormatReceiptSate);
           // GridViewInvice.Columns[MInvoiceRecord.FieldName].DisplayFormat.Format = new CustomFormatter(FormatMInvoiceRecord);
            
            GridViewInvice.OptionsSelection.MultiSelect = true;
            GridViewInvice.OptionsView.ShowIndicator = false;//不显示指示器
            GridViewInvice.OptionsBehavior.ReadOnly = false;
            GridViewInvice.OptionsBehavior.Editable = false;
            txtStarDate.DateTime = CommonAppSrv.GetDateTimeNow().Now.Date;
            txtEndDate.DateTime = CommonAppSrv.GetDateTimeNow().Now.Date;
            // comSFType.Properties.DataSource = InvoiceStatusHelper.GetMarrySateModels();

            gridGroupLis.Columns[IsAddMinus.FieldName].DisplayFormat.Format = new CustomFormatter(FormatIsAddMinus);
           // gridGroupLis.Columns[CheckState.FieldName].DisplayFormat.Format = new CustomFormatter(FormatCheckState);
            gridGroupLis.Columns[PayerCat.FieldName].DisplayFormat.Format = new CustomFormatter(FormatRefundState);

        }

        private void butSearCh_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            try
            {
                List<MReceiptInfoDto> ReceiptInfo = new List<MReceiptInfoDto>();
                SearchInvoiceDto searchInvoice = new SearchInvoiceDto();
                if (!string.IsNullOrWhiteSpace(searchNames.Text.Trim()))
                    searchInvoice.SearchName = searchNames.Text.Trim();
                //时间
                if (txtStarDate.EditValue != null && txtEndDate.EditValue != null)
                {
                    searchInvoice.StarDate = DateTime.Parse(txtStarDate.EditValue.ToString());
                    searchInvoice.EndDate = DateTime.Parse(txtEndDate.EditValue.ToString());
                }
                if (checkCurrentUser.Checked==true)
                {
                    searchInvoice.UserType = 1;
                }
                else
                {
                    searchInvoice.UserType = 2;
                }
                if (comSFType.Text.Trim() == "收费")
                {
                    int receoptState = (int)InvoiceStatus.Valid;
                    searchInvoice.ReceiptSate = receoptState;
                }
                else if (comSFType.Text.Trim() == "作废")
                {
                    int receoptState = (int)InvoiceStatus.Cancellation;
                    searchInvoice.ReceiptSate = receoptState;
                }
                ReceiptInfo = ChargeAppService.GetInvalidReceipt(searchInvoice);
                gridInvice.DataSource = ReceiptInfo;
            
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }

        }
        private void GridViewInvice_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                var dto = gridInvice.GetFocusedRowDto<MReceiptInfoDto>();
                if (dto == null)
                    return;
                EntityDto<Guid> receiptID = new EntityDto<Guid>();
                receiptID.Id = dto.Id;
                MReceiptInfoDto mReceiptInfoDto = ChargeAppService.GetInvalidReceiptById(receiptID);
                if (mReceiptInfoDto.ReceiptSate != 1)
                {
                    butInvalid.Enabled = false;
                }
                else
                {
                    butInvalid.Enabled = true;
                }
                List<MReceiptInfoDetailedViewDto> ReceiptInfoDetailedView = new List<MReceiptInfoDetailedViewDto>();
                    EntityDto<Guid> input = new EntityDto<Guid>();
                    input.Id = dto.Id;                   
                    ReceiptInfoDetailedView = ChargeAppService.getReceiptInfoDetaileds(input);
                   // var sfGroups = ReceiptInfoDetailedView.GroupBy(o=>o.ReceiptTypeName);

                    gridGroups.DataSource = ReceiptInfoDetailedView;
                    gridControl1.DataSource= ReceiptInfoDetailedView;

                if (dto.ClientReg == null && dto.CustomerReg!=null)
                {

                    //获取该体检人组合信息
                    ChargeBM BM = new ChargeBM();
                    BM.Name = dto.CustomerReg?.CustomerBM;
                    getGroups(BM);
                    labelControl2.Text = "体检号：" + dto.CustomerReg?.CustomerBM + ",姓名：" + dto.CustomerReg.Customer.Name + ",性别" + dto.CustomerReg.Customer.Sex + ",年龄：" + dto.CustomerReg.Customer.Sex + ",单位：" + dto.ClientName;
                }
                //List<ChargeGroupsDto> chargeGroupsDtos = new List<ChargeGroupsDto>();
                //ChargeBM chargeBM = new ChargeBM();
                //chargeBM.Id = dto.Id;
                //if (dto.ClientReg != null)
                //{
                //    chargeBM.Name = "单位";
                //}
                //else
                //{
                //    chargeBM.Name = "个人";
                //    //获取该体检人组合信息
                //    ChargeBM BM = new ChargeBM();
                //    chargeBM.Name = dto.CustomerReg.CustomerBM;
                //    getGroups(chargeBM);
                //    labelControl2.Text = "体检号："+ dto.CustomerReg.CustomerBM + ",姓名："+ dto.CustomerReg.Customer.Name + ",性别"+ dto.CustomerReg.Customer.Sex + ",年龄："+ dto.CustomerReg.Customer.Sex + ",单位：" + dto.ClientName;
                //}
                //chargeGroupsDtos = ChargeAppService.getReceiptInfoGroups(chargeBM);
                //gridControl1.DataSource = chargeGroupsDtos;



            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        private void butInvalid_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = gridInvice.GetFocusedRowDto<MReceiptInfoDto>();
                if (dto == null)
                    return;
                //int index = GridViewInvice.FocusedRowHandle;
                DialogResult dr = XtraMessageBox.Show("是否确认作废？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                //总检完成不可作废
                if (dto.CustomerReg?.SummSate >= 3)
                {
                    XtraMessageBox.Show("该体检人已总检，不能作废！");
                    return;
                }

                EntityDto<Guid> receiptID = new EntityDto<Guid>();
                receiptID.Id = dto.Id;
                bool ischek = ChargeAppService.SFGroupCheck(receiptID);
                if (ischek == true)
                {
                    DialogResult drzf = XtraMessageBox.Show("此收费项目已检查，是否确认作废？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drzf == DialogResult.No)
                    {
                        return;
                    }

                }
                
                MReceiptInfoDto mReceiptInfoDto = ChargeAppService.GetInvalid(receiptID);

                int receiptstate = (int)InvoiceStatus.Cancellation;
                if (mReceiptInfoDto != null &&  mReceiptInfoDto.ReceiptSate == receiptstate)
                {
                    XtraMessageBox.Show("此记录已作废，不能重复作废！");
                    return;
                }
                #region 判断是否原路返回
                frmSFTS frmSFTS = new frmSFTS();
                frmSFTS.ShowDialog();


                GuIdDto input = new GuIdDto();
                //判断是否原路返回
                if (frmSFTS.isYLFH == false)
                {
                    frmPayMent frmPay = new frmPayMent();
                    frmPay.ShowDialog();
                    if (frmPay.DialogResult == DialogResult.OK)
                    {
                        input.PaymentId = frmPay.PaymentId.Id;
                    }

                } 
                #endregion

                #region 团体收费推送

                var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 4)?.Remarks;
                var DJTSCJ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 2)?.Remarks;
                if (!string.IsNullOrEmpty(DJTS) && DJTS == "1" && !string.IsNullOrEmpty(DJTSCJ))
                {

                    XYNeInterface neInterface = new XYNeInterface();
                    //接口名称&单位预约ID&申请单号&金额&发票名称
                    var inStr = DJTSCJ + "&" + dto.pay_order_id;
                    var outMess = neInterface.CancelTTPay(inStr, "");
                    if (outMess.Code != "0")
                    {
                        MessageBox.Show(outMess.ReSult);
                        return;
                    }

                }

                #endregion
                input.Id = dto.Id;
                MReceiptInfoDto MReceiptInfo = ChargeAppService.InsertInvalidReceiptInfoDto(input);
                //XtraMessageBox.Show( MReceiptInfo.code+":" + MReceiptInfo.err);
                if (MReceiptInfo.code != null && MReceiptInfo.code == "0")
                {
                    XtraMessageBox.Show("线上退费失败：" + MReceiptInfo.err);
                    return;
                }
           
                //MessageBox.Show(dto.Id.ToString());
                //MessageBox.Show(dto.pay_order_id?.ToString());
                if (!string.IsNullOrEmpty(dto.pay_order_id))
                {
                     
                    var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                    if (HISjk == "1")
                    {
                        var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2)?.Remarks;
                        if (HISName != null && HISName == "江苏鑫亿")
                        {
                            //MessageBox.Show("调用接口");
                            XYNeInterface neInterface = new XYNeInterface();
                            var outMess = neInterface.CancelTTPay(dto.pay_order_id, CurrentUser.EmployeeNum);
                            if (outMess.Code != "0")
                            {
                                MessageBox.Show(outMess.ReSult);
                            }
                        }
                    }
                }
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                if (dto.CustomerReg != null)
                {
                    createOpLogDto.LogBM = dto.CustomerReg.CustomerBM;
                    createOpLogDto.LogName = dto.CustomerReg.Customer.Name;
                }
                else if (dto.ClientReg != null)
                {
                    createOpLogDto.LogBM = dto.ClientReg.ClientRegBM;
                    createOpLogDto.LogName = dto.ClientReg.ClientInfo.ClientName;
                }
                
                createOpLogDto.LogText = "作废收费："+ dto.Actualmoney;                 
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                CommonAppSrv.SaveOpLog(createOpLogDto);
                XtraMessageBox.Show("作废发票成功！");
                List<MReceiptInfoDto> MReceiptlis = (List<MReceiptInfoDto>)gridInvice.DataSource;
                MReceiptlis.Add(MReceiptInfo);
                gridInvice.DataSource = MReceiptlis;
                gridInvice.Refresh();
                GridViewInvice.RefreshData();
                this.GridViewInvice.SelectRow(this.GridViewInvice.DataRowCount - 1);
                this.GridViewInvice.FocusedRowHandle = this.GridViewInvice.DataRowCount - 1;//焦点转移到最后一行
                DevExpress.Data.SelectionChangedEventArgs en = new DevExpress.Data.SelectionChangedEventArgs();
                GridViewInvice_SelectionChanged(sender, en);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

        }

        private void butCancelinvalid_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region 方法
        private string FormatCheckName(object arg)
        {
            try
            {
                ReceiptInfoViewDto ReceiptInfoView = (ReceiptInfoViewDto)arg;
                if (ReceiptInfoView.Customer != null)
                {
                    return ReceiptInfoView.Customer.Name;
                }
                else
                {
                    return ReceiptInfoView.ClientName;
                }


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
                ICollection<CreatePaymentDto> MPayments = (ICollection<CreatePaymentDto>)arg;
                string PayInfos = "";
                foreach (CreatePaymentDto PayMent in MPayments)
                {
                    PayInfos += PayMent.MChargeTypename + ":" + PayMent.Actualmoney + ",";
                }
                return PayInfos.TrimEnd(',');
            }
            catch
            {
                return "";
            }
        }
        private string FormatMInvoiceRecord(object arg)
        {
            try
            {
                ICollection<CreateInvoiceRecordDto> InvoiceRecords = (ICollection<CreateInvoiceRecordDto>)arg;
                string PayInfos = "";
                foreach (CreateInvoiceRecordDto InvoiceRecord in InvoiceRecords)
                {
                    PayInfos += InvoiceRecord.InvoiceNum +",";
                }
                return PayInfos.TrimEnd(',');
            }
            catch
            {
                return "";
            }
        }
        //FormatMInvoiceRecord
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



        #endregion

        private void searchNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r' && !string.IsNullOrWhiteSpace(searchNames.Text))
                {

                    List<MReceiptInfoDto> ReceiptInfo = new List<MReceiptInfoDto>();
                    SearchInvoiceDto searchInvoice = new SearchInvoiceDto();
                    if (!string.IsNullOrWhiteSpace(searchNames.Text.Trim()))
                        searchInvoice.SearchName = searchNames.Text.Trim();                                  
                    ReceiptInfo = ChargeAppService.GetInvalidReceipt(searchInvoice);
                    gridInvice.DataSource = ReceiptInfo;
                    //ChargeBM chargeBM = new ChargeBM();
                    //chargeBM.Name = searchNames.Text.Trim();
                    //getGroups(chargeBM);
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
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
                if (arg.ToString() == "1")
                {
                    return "自费";
                }
                else
                {
                    return GroupPayerCatHelper.GroupPayerCatHelperFormatter(arg);
                }
            }
            catch
            {
                return "";
            }
        }

        private void tabNavigationPage3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridViewItemGround_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void schItemGroup_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            AddGroups();
        }
        private void gridViewCheckGroups_DoubleClick(object sender, EventArgs e)
        {
            AddGroups();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            DelGroups();
        }

        private void gridViewReturnGroups_DoubleClick(object sender, EventArgs e)
        {
            DelGroups();

        }
        #region 添加组合
        public void AddGroups()
        {
            //获取选择dt
            var lstOptionalItemGroup = gridCheckGroups.GetSelectedRowDtos<ChargeGroupsDto>();

            if (lstOptionalItemGroup.Count > 0)
            {              
                foreach (ChargeGroupsDto ChargeGroup in lstOptionalItemGroup)
                {
                    var depart = DefinedCacheHelper.GetDepartments().FirstOrDefault(p=>p.Id== ChargeGroup.DepartmentId);
                    if (ChargeGroup.CheckState != (int)ProjectIState.Not && depart!=null &&
                        depart.Category!= "耗材")
                    {
                        MessageBox.Show("组合："+ChargeGroup.ItemGroupName +"已检查，不能退费！");
                        return;
                    }

                }

                RemoveGroup(lstOptionalItemGroup.ToList());
                List<ChargeGroupsDto> lstgroup = gridReturnGroups.GetDtoListDataSource<ChargeGroupsDto>();
                lstgroup.AddRange(lstOptionalItemGroup);
                gridReturnGroups.DataSource = lstgroup;
                gridReturnGroups.RefreshDataSource();
                //grdvSelectItemGroup.SelectRow(0);
            }          

        }
        #endregion 添加组合    
        #region 删除组合
        public void DelGroups()
        {
            //获取选择dt
            var lstOptionalItemGroup = gridReturnGroups.GetSelectedRowDtos<ChargeGroupsDto>();

            if (lstOptionalItemGroup.Count > 0)
            {
               

                #region 移除组合
                int iindex = gridViewReturnGroups.GetFocusedDataSourceRowIndex();
                var lstReturnGroups = gridReturnGroups.GetDtoListDataSource<ChargeGroupsDto>();
                foreach (ChargeGroupsDto item in lstOptionalItemGroup)
                {
                    if (lstReturnGroups.Contains(item))
                    {
                        gridReturnGroups.RemoveDtoListItem(item);
                    }
                }
                if (lstOptionalItemGroup.Count < iindex)
                {
                    gridViewReturnGroups.SelectRow(iindex);
                }
                else
                {
                    gridViewReturnGroups.SelectRow(lstOptionalItemGroup.Count - lstReturnGroups.Count);
                }
                #endregion
                //添加组合
                List<ChargeGroupsDto> lstgroup = gridCheckGroups.GetDtoListDataSource<ChargeGroupsDto>();
                lstgroup.AddRange(lstOptionalItemGroup);
                gridCheckGroups.DataSource = lstgroup;
                gridCheckGroups.RefreshDataSource();
                //grdvSelectItemGroup.SelectRow(0);
            }

        }
        #endregion

        #region 移除当前行
        private void RemoveGroup(List<ChargeGroupsDto> lstSimpleItemGroupDto)
        {
            //获取当前选择行
            int iindex = gridViewCheckGroups.GetFocusedDataSourceRowIndex();
            var lstOptionalItemGroup = gridCheckGroups.GetDtoListDataSource<ChargeGroupsDto>();
            foreach (ChargeGroupsDto item in lstSimpleItemGroupDto)
            {
                if (lstOptionalItemGroup.Contains(item))
                {
                    gridCheckGroups.RemoveDtoListItem(item);
                }
            }
            if (lstOptionalItemGroup.Count < iindex)
            {
                gridViewCheckGroups.SelectRow(iindex);
            }
            else
            {
                gridViewCheckGroups.SelectRow(lstOptionalItemGroup.Count - lstSimpleItemGroupDto.Count);
            }

        }
        #endregion
        #region 获取组合信息
        private void getGroups(ChargeBM chargeBM)
        {
            List<ChargeGroupsDto> chargeGroupsDtos= ChargeAppService.GetCusGroups(chargeBM);
            ///收费信息
            gridCheckGroups.DataSource = chargeGroupsDtos?.Where(r => r.MReceiptInfoPersonalId != null && r.IsAddMinus !=3 ).OrderBy(r => r.IsAddMinus).ToList();
            //int notrefund = (int)PayerCatType.NotRefund;
            int refund = (int)PayerCatType.Refund;
            int stayrefund = (int)PayerCatType.StayRefund;
            gridReturnGroups.DataSource= chargeGroupsDtos?.Where(r => r.RefundState == stayrefund && r.MReceiptInfoPersonalId !=null || r.RefundState== refund).OrderBy(r => r.IsAddMinus).ToList();
        }
        #endregion

        private void gridViewCheckGroups_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {           
                if (e.Column.FieldName != cnCheckState.FieldName)
                    return;
                if (e.Value == null)
                    return;
                e.DisplayText = EnumHelper.GetEnumDesc((ProjectIState)e.Value);            
        }

        private void gridViewReturnGroups_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != RefundState.FieldName)
                return;
            if (e.Value == null)
                return;
            e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
        }

        private void gridViewCheckGroups_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            if (e.Column.Name == RefundState.Name)
            {
                var isadd = gridViewCheckGroups.GetRowCellValue(e.RowHandle, RefundState);
                if (isadd == null)
                    return;
                if (Convert.ToInt32(isadd) != (int)ProjectIState.Not)
                {
                    e.Appearance.ForeColor = Color.Gray;                    
                }               
            }
        }
        private void gridViewReturnGroups_RowCountChanged(object sender, EventArgs e)
        {
            List<ChargeGroupsDto> lstgroup = gridReturnGroups.GetDtoListDataSource<ChargeGroupsDto>();
            int refundState = (int)PayerCatType.Refund;
            txtGroupMoney.Text = lstgroup.Where(o => o.RefundState != refundState).Sum(o => o.ItemPrice).ToString("0.00");
            txtBackMoney.Text = lstgroup.Where(o => o.RefundState != refundState).Sum(o => o.GRmoney).ToString("0.00");
        }

        private void butBackMoney_Click(object sender, EventArgs e)
        {
            var dtocus = gridInvice.GetFocusedRowDto<MReceiptInfoDto>();
            if (dtocus == null)
                return;
            //if (decimal.Parse(txtBackMoney.EditValue.ToString()) <= 0)
            //{
            //    MessageBox.Show("退费金额不能小于0！");
            //    return;
            //}
            int refundState = (int)PayerCatType.Refund;
            List<ChargeGroupsDto> lstgroup = gridReturnGroups.GetDtoListDataSource<ChargeGroupsDto>();
            lstgroup = lstgroup.Where(o => o.RefundState != refundState).ToList();
            var guidlis=   lstgroup.Where(o=>o.RefundState != refundState).Select(o => o.MReceiptInfoPersonalId).Distinct();
            #region 判断是否原路返回            
            frmSFTS frmSFTS = new frmSFTS();
            frmSFTS.ShowDialog();


            Guid? PaymentId = null;
            //判断是否原路返回
            if (frmSFTS.isYLFH == false)
            {
                frmPayMent frmPay = new frmPayMent();
                frmPay.ShowDialog();
                if (frmPay.DialogResult == DialogResult.OK)
                {
                    PaymentId = frmPay.PaymentId.Id;
                }

            }
             
            #endregion


            //作废原有发票
            foreach (Guid guid in guidlis)
            {
                GuIdDto input = new GuIdDto();
                input.Id = guid;
                input.Remoney = decimal.Parse(txtBackMoney.EditValue.ToString());
                if (PaymentId != null && PaymentId != Guid.NewGuid())
                {
                    input.PaymentId = PaymentId;
                }
                MReceiptInfoDto MReceiptInfo = ChargeAppService.InsertInvalidReceiptInfoDto(input);
                if (MReceiptInfo.code != null && MReceiptInfo.code == "0")
                {
                    XtraMessageBox.Show("线上退费失败：" + MReceiptInfo.err);
                    return;
                }
            }
            if (PaymentId != null && PaymentId != Guid.NewGuid() &&
                lstgroup.FirstOrDefault()!=null)
            {
                lstgroup.FirstOrDefault().PaymentId = PaymentId;


            }

            //重新收费
            List<MReceiptInfoDto> mReceiptInfos = ChargeAppService.GetReceiptInfoBack(lstgroup);
            //日志
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            if (dtocus.CustomerReg != null)
            {
                createOpLogDto.LogBM = dtocus.CustomerReg.CustomerBM;
                createOpLogDto.LogName = dtocus.CustomerReg.Customer.Name;
            }
            else if (dtocus.ClientReg != null)
            {
                createOpLogDto.LogBM = dtocus.ClientReg.ClientRegBM;
                createOpLogDto.LogName = dtocus.ClientReg.ClientInfo.ClientName;
            }

            createOpLogDto.LogText = "退费：" + decimal.Parse(txtBackMoney.EditValue.ToString());
            var lst = lstgroup.Select(o=>o.ItemGroupName+"("+ o.PriceAfterDis+")").ToList();
            createOpLogDto.LogDetail = string.Join(",", lst);
            createOpLogDto.LogType = (int)LogsTypes.ResId;
            CommonAppSrv.SaveOpLog(createOpLogDto);
            //打印体检清单或发票
            if (checkPrint.Checked == true)
            {
                foreach (MReceiptInfoDto rec in mReceiptInfos)
                {
                    EntityDto<Guid> entity = new EntityDto<Guid>();
                    entity.Id = rec.MPayment.First().MChargeTypeId;
                    ChargeTypeDto chargeType = ChargeAppService.ChargeTypeByID(entity);
                    if (chargeType.PrintName == 2)
                    {
                        PrinChekListByID(rec.Id);
                    }
                    else
                    {
                        bool isOK = PrininvoiceByID(rec.Id);                       
                    }
                }
            }

            //获取该体检人组合信息
            var dto = gridInvice.GetFocusedRowDto<MReceiptInfoDto>();
            if (dto == null)
                return;
            ChargeBM BM = new ChargeBM();
            BM.Name = dto.CustomerReg.CustomerBM;
            getGroups(BM);
            labelControl2.Text = "体检号：" + dto.CustomerReg.CustomerBM + ",姓名：" + dto.CustomerReg.Customer.Name + ",性别" + dto.CustomerReg.Customer.Sex + ",年龄：" + dto.CustomerReg.Customer.Sex + ",单位：" + dto.ClientName;

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
        string fpnumis = "";
        Guid repID = Guid.Empty;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fpnumis = "";
            repID= Guid.Empty;
            var id = GridViewInvice.GetRowCellValue(GridViewInvice.FocusedRowHandle, MInvoiceRecord);
            if (id == null)
            {
                
                // ShowMessageBoxInformation("尚未选定任何发票记录！");
                var receipID = (Guid)GridViewInvice.GetRowCellValue(GridViewInvice.FocusedRowHandle, conreceipID);
                repID = receipID;
                var fpH = GridViewInvice.GetRowCellValue(GridViewInvice.FocusedRowHandle, ReceiptSate);
                if (fpH.ToString() == "2")
                {
                    MessageBox.Show("作废不能打发票");
                }
                else
                { 
                    bool isOK = PrininvoiceByID(receipID);
                    butSearCh.PerformClick();
                }
            }
            else if (id.ToString()=="")
            {
                // ShowMessageBoxInformation("尚未选定任何发票记录！");
                var receipID = (Guid)GridViewInvice.GetRowCellValue(GridViewInvice.FocusedRowHandle, conreceipID);
                repID = receipID;
                var fpH = GridViewInvice.GetRowCellValue(GridViewInvice.FocusedRowHandle, ReceiptSate);
                if (fpH.ToString() == "2")
                {
                    MessageBox.Show("作废不能打发票");
                }
                else
                {
                    bool isOK = PrininvoiceByID(receipID);
                    butSearCh.PerformClick();
                }

            }
            else
            {
                var receipID = (Guid)GridViewInvice.GetRowCellValue(GridViewInvice.FocusedRowHandle, conreceipID);
                repID = receipID;
                string[] fpls = id.ToString().Split(',');
                foreach (string pf in fpls)
                {
                    if (pf != "")
                    {
                        fpnumis = pf;
                        var rep = GridppHelper.GetTemplate("发票.grf");
                        Report.LoadFromURL(rep);
                        Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportBind);
                        Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportBind);
                        //打印机设置
                        var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 50)?.Remarks;
                        if (!string.IsNullOrEmpty(printName))
                        {
                            Report.Printer.PrinterName = printName;
                        }
                        // ReportBind(id.ToString());
                        try
                        {
                            Report.Print(false);
                        }
                        catch (Exception ex)
                        {
                            ShowMessageBoxError(ex.ToString());
                        }
                    }
                }
            }
                
        }
        private void ReportBind()
        {
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = fpnumis;
            chargeBM.Id = repID;
            // var chargels = ChargeAppService.PrintInvoiceNum(chargeBM);

            var chargels = ChargeAppService.PrintInvoiceId(chargeBM);
            Report.DetailGrid.Recordset.Append();
            if (chargels.MReceiptInfo.CustomerReg != null)
            {
                Report.FieldByName("个人姓名").AsString = chargels.MReceiptInfo.CustomerReg.Customer.Name;
            }
            else
            {
                Report.FieldByName("个人姓名").AsString = chargels.MReceiptInfo.ClientName;
            }
            Report.FieldByName("金额").AsString = chargels.InvoiceMoney.ToString();
            Report.FieldByName("支付方式").AsString = chargels.MReceiptInfo.FormatMPayment;


            Report.FieldByName("金额大写").AsString = CommonHelper.ConvertToChinese(Convert.ToDouble(chargels.InvoiceMoney.ToString()));
            Report.FieldByName("金额小写").AsString = chargels.InvoiceMoney.ToString();
            Report.FieldByName("收款员").AsString = CurrentUser.Name;
            Report.FieldByName("发票抬头").AsString = chargels.MRise.Name;
            Report.DetailGrid.Recordset.Post();
        }       
    }
}
