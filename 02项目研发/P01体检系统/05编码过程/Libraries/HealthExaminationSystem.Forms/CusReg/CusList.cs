using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering.Templates;
using Newtonsoft.Json;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.WeChat;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.WeChat;
using Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class CusList : UserBaseForm
    {
        private readonly PhysicalExaminationAppService _PhysicalAppService;
        public string curCustomerBM;//当前选择的体检号
        private IWeChatAppService weChatAppService;
        public CusList()
        {
            InitializeComponent();
            _PhysicalAppService = new PhysicalExaminationAppService();
             weChatAppService = new WeChatAppService();
            InitForm();
        }

        private void cusList_Load(object sender, EventArgs e)
        {
            gridView1.IndicatorWidth = 40;
        }
        #region 处理

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void InitForm()
        {
            //体检状态
            teTJZT.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            teTJZT.ItemIndex = 0;
            //登记状态
            teDJZT.Properties.DataSource = RegisterStateHelper.GetSelectList();
            teDJZT.ItemIndex = 0;
            //总检状态
            teZJZT.Properties.DataSource = SummSateHelper.GetSelectList();
            teZJZT.ItemIndex = 0;
            //单位
            var clientreglist = _PhysicalAppService.QueryCompany();
            TeDW.Properties.DataSource = clientreglist;
        }

        #endregion

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CX();
        }
        public void CX()
        {
            dxErrorProvider.ClearErrors();
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gridControl1.DataSource = null;


            CustomerRegPhysicalDto dto = new CustomerRegPhysicalDto();
            CustomerPhysicalDto customer = new CustomerPhysicalDto();
            try
            {

                if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
                {
                    customer.SerchInput = teTJH.Text.Trim();
                }
                ////体检号
                //if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
                //    dto.CustomerBM = teTJH.Text.Trim();
                ////姓名

                //if (!string.IsNullOrWhiteSpace(teXM.Text.Trim()))
                //    customer.Name = teXM.Text.Trim();
                ////证件号
                //if (!string.IsNullOrWhiteSpace(teZJH.Text.Trim()))
                //    customer.IDCardNo = teZJH.Text.Trim();
                //体检状态
                if (teTJZT.EditValue != null)
                {
                    dto.CheckSate = int.TryParse(teTJZT.EditValue.ToString(), out int TJZT) ? (int?)TJZT : null;
                }
                if (teZJZT.EditValue != null)
                {
                    dto.SummSate = int.TryParse(teZJZT.EditValue.ToString(), out int ZJZT) ? (int?)ZJZT : null;
                    if (dto.SummSate == 0)
                        dto.SummSate = null;
                }
                if (teDJZT.EditValue != null)
                {
                    dto.RegisterState = int.TryParse(teDJZT.EditValue.ToString(), out int DJZT) ? (int?)DJZT : null;
                    if (dto.RegisterState == 0)
                        dto.RegisterState = null;
                }
                //是否个人
                dto.IsPersonal = chkEditPersonal.Checked;
                //单位
                if (TeDW.EditValue != null)
                {
                    if (!string.IsNullOrWhiteSpace(TeDW.EditValue.ToString()))
                    {
                        dto.TjlClientReg_Id = Guid.Parse(TeDW.EditValue.ToString());
                    }
                }

                if (ceJJN.Checked)
                {
                    DateTime time = new DateTime(DateTime.Now.Year, 01, 01);
                    DateTime times = new DateTime(DateTime.Now.Year, 12, 31);
                    dto.BookingDateStart = Convert.ToDateTime(time);
                    dto.BookingDateEnd = Convert.ToDateTime(times);
                }
                else
                {

                    //时间
                    if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                    {
                        if (Convert.ToDateTime(teSJQ.Text) > Convert.ToDateTime(daSJZ.Text))
                        {
                            dxErrorProvider.SetError(daSJZ, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                            daSJZ.Focus();
                            return;
                        }

                        dto.BookingDateStart = Convert.ToDateTime(teSJQ.Text);
                        dto.BookingDateEnd = Convert.ToDateTime(daSJZ.Text);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                            dto.BookingDateStart = Convert.ToDateTime(teSJQ.Text);
                        if (string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                            dto.BookingDateEnd = Convert.ToDateTime(daSJZ.Text);
                    }
                }
                //clientReg.ClientInfo = clientInfo;
                dto.Customer = customer;
                //dto.ClientReg = clientReg;




                var output = _PhysicalAppService.PersonalInformationQuery(new PageInputDto<CustomerRegPhysicalDto> { TotalPages = TotalPages, CurentPage = CurrentPage, Input = dto });


                gridControl1.DataSource = output;

                if (output != null)
                {
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    gridControl1.DataSource = output.Result;
                }

                InitialNavigator(dataNavigator1);


            }
            catch (ApiProxy.UserFriendlyException ex)
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



            gridView1.Columns[Sex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[Sex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

            gridView1.Columns[CheckSate.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[CheckSate.FieldName].DisplayFormat.Format = new CustomFormatter(CheckSateHelper.PhysicalEStateFormatter);

            gridView1.Columns[MarriageStatus.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[MarriageStatus.FieldName].DisplayFormat.Format = new CustomFormatter(MarrySateHelper.CustomMarrySateFormatter);

            gridView1.Columns[CostState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[CostState.FieldName].DisplayFormat.Format = new CustomFormatter(CostStateHelper.CostStateFormatter);


            gridView1.Columns[ReadyPregnancybirth.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[ReadyPregnancybirth.FieldName].DisplayFormat.Format = new CustomFormatter(BreedStateHelper.BreedStateFormatter);

            gridView1.Columns[SendToConfirm.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[SendToConfirm.FieldName].DisplayFormat.Format = new CustomFormatter(SendToConfirmHelper.SendToConfirmFormatter);
        }
        public void CXyy(string yy)
        {
            dxErrorProvider.ClearErrors();
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gridControl1.DataSource = null;


            CustomerRegPhysicalDto dto = new CustomerRegPhysicalDto();
            CustomerPhysicalDto customer = new CustomerPhysicalDto();
            try
            {


                if (ceJJN.Checked)
                {
                    DateTime time = new DateTime(DateTime.Now.Year, 01, 01);
                    DateTime times = new DateTime(DateTime.Now.Year, 12, 31);
                    dto.BookingDateStart = Convert.ToDateTime(time);
                    dto.BookingDateEnd = Convert.ToDateTime(times);
                }
                else
                {

                    //时间
                    if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                    {
                        if (Convert.ToDateTime(teSJQ.Text) > Convert.ToDateTime(daSJZ.Text))
                        {
                            dxErrorProvider.SetError(daSJZ, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                            daSJZ.Focus();
                            return;
                        }

                        dto.BookingDateStart = Convert.ToDateTime(teSJQ.Text);
                        dto.BookingDateEnd = Convert.ToDateTime(daSJZ.Text);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                            dto.BookingDateStart = Convert.ToDateTime(teSJQ.Text);
                        if (string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                            dto.BookingDateEnd = Convert.ToDateTime(daSJZ.Text);
                    }
                }
                //clientReg.ClientInfo = clientInfo;
                dto.Customer = customer;
                dto.OrderNum = yy;
                //dto.ClientReg = clientReg;




                var output = _PhysicalAppService.PersonalInformationQuery(new PageInputDto<CustomerRegPhysicalDto> { TotalPages = TotalPages, CurentPage = CurrentPage, Input = dto });


                gridControl1.DataSource = output;

                if (output != null)
                {
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    gridControl1.DataSource = output.Result;
                }

                InitialNavigator(dataNavigator1);


            }
            catch (ApiProxy.UserFriendlyException ex)
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



            gridView1.Columns[Sex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[Sex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

            gridView1.Columns[CheckSate.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[CheckSate.FieldName].DisplayFormat.Format = new CustomFormatter(CheckSateHelper.PhysicalEStateFormatter);

            gridView1.Columns[MarriageStatus.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[MarriageStatus.FieldName].DisplayFormat.Format = new CustomFormatter(MarrySateHelper.CustomMarrySateFormatter);

            gridView1.Columns[CostState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[CostState.FieldName].DisplayFormat.Format = new CustomFormatter(CostStateHelper.CostStateFormatter);


            gridView1.Columns[ReadyPregnancybirth.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[ReadyPregnancybirth.FieldName].DisplayFormat.Format = new CustomFormatter(BreedStateHelper.BreedStateFormatter);

            gridView1.Columns[SendToConfirm.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[SendToConfirm.FieldName].DisplayFormat.Format = new CustomFormatter(SendToConfirmHelper.SendToConfirmFormatter);
        }
        private void dataNavigator1_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            simpleButton2_Click(sender, e);

        }
        /// <summary>
        /// 表格双击事件 将体检号赋值给公开变量 给登记窗体读取
        /// </summary>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            if (e.Clicks == 2)
            {
                var row = gridView1.GetFocusedRow();
                if (row != null)
                {
                    var data = row as CustomerRegPhysicalDto;
                    if (data.ClientReg != null)
                    {
                        if (data.ClientReg.FZState == (int)FZState.Already)
                        {
                            ShowMessageBoxInformation("该人员的单位已封账，不能在登记处查看详情。");
                            return;
                        }
                    }
                    curCustomerBM = data.CustomerBM;
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            teTJH.Text = null;
            teTJZT.ItemIndex = 0;
            TeDW.EditValue = null;
            teSJQ.EditValue = null;
            daSJZ.EditValue = null;
            ceJJN.Checked = false;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == DrawCard.FieldName)
            {
                if (!string.IsNullOrWhiteSpace(e.Value?.ToString()))
                    e.DisplayText = "已抽血";
            }
            if (e.Column.FieldName == colRegisterState.FieldName)
            {
                if (!string.IsNullOrWhiteSpace(e.Value?.ToString()))
                {
                    e.DisplayText = EnumHelper.GetEnumDesc((RegisterState)e.Value);
                }
            }
            if (e.Column.FieldName == colSummSate.FieldName)
            {
                if (!string.IsNullOrWhiteSpace(e.Value?.ToString()))
                {
                    e.DisplayText = EnumHelper.GetEnumDesc((SummSate)e.Value);
                }
            }
        }

        private void teTJH_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
                if (e.KeyCode == Keys.Enter)
                    CX();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                //SearchRegDateDto searchReg1 = new SearchRegDateDto();
                //searchReg1.CustomerBM = "tj202210110000000031896";
                //var err1 = weChatAppService.DelCusReg(searchReg1);

                DateTime star = new DateTime();
                DateTime end = new DateTime();
                if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                {
                    if (Convert.ToDateTime(teSJQ.Text) > Convert.ToDateTime(daSJZ.Text))
                    {
                        dxErrorProvider.SetError(daSJZ, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        daSJZ.Focus();
                        return;
                    }

                    star = Convert.ToDateTime(teSJQ.Text);
                    end = Convert.ToDateTime(daSJZ.Text);
                    NeusoftInterface.HealthInWuHuInterface healthInWuHuInterface = new NeusoftInterface.HealthInWuHuInterface();
                    var outlist = healthInWuHuInterface.makeAsync(star.ToString(), end.ToString());
                    string ouerr = "";
                    int Cancelcout = 0;
                    string  cuslist ="";
                    foreach (var cusreg in outlist)
                    {
                       // var jo = (InCusInfoDto)JsonConvert.DeserializeObject(cusreg);
                        var jo = JsonConvert.DeserializeObject<NInCusInfoDto>(cusreg);
                        //取消登记
                        if (jo.isWxPay == "-1")
                        {
                            SearchRegDateDto searchReg = new SearchRegDateDto();
                            searchReg.CustomerBM = jo.OrderId;
                           var err= weChatAppService.DelCusReg(searchReg);
                            if (err.code == 1)
                            { Cancelcout = Cancelcout + 1; }
                            else
                            { ouerr += "取消预约失败：" + err.Mess; }
                        }
                        else
                        {
                            var Outerr = weChatAppService.RegCus(jo);
                            ouerr += Outerr.ErrInfo;
                            cuslist += jo.OrderId + "|";
                           
                        }
                      

                    }
                    if (outlist.Count > 0)
                    {
                        if (ouerr != "")
                        {
                            XtraMessageBox.Show("预约异常：" + ouerr);
                            //simpleButton2.PerformClick();
                        }
                        else
                        {
                            XtraMessageBox.Show("成功下载：" + outlist.Count + "条数据,其中取消预约：" + Cancelcout+"条数据");
                            //simpleButton2.PerformClick();
                        }
                        CXyy(cuslist.Trim('|'));
                    }
                    else
                    { XtraMessageBox.Show("无预约数据"); }

                }
                else
                {
                    XtraMessageBox.Show("请选择起止日期！");
                }

            }
            catch (Exception )
            {

                throw;
            }
           
       
        }       
    }
}
