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
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Data;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraLayout;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.GeneralDoctor
{
    public partial class FrmHZJCXMTJ : UserBaseForm
    {
        public FrmHZJCXMTJ()
        {
            InitializeComponent();
        }
        DoctorStationAppService app = new DoctorStationAppService();
        ClientInfoesAppService ClientInfoesApp = new ClientInfoesAppService();
        CommonAppService dtApp = new CommonAppService();
        private void FrmHZJCXMTJ_Load(object sender, EventArgs e)
        {
            BindClient();
            BindSource();
            dgv.IndicatorWidth = 40;
            dgvXM.IndicatorWidth = 30;
        }

        /// <summary>
        /// 科室加载
        /// </summary>
        public void BindClient()
        {
            //chkClientName.Properties.DataSource = ClientInfoesApp.GetAll();
            //chkClientName.Properties.ValueMember = "ClientName";
            //chkClientName.Properties.DisplayMember = "ClientName";

          var  clientRegs = DefinedCacheHelper.GetClientRegNameComDto();
            chkClientName.Properties.DataSource = clientRegs;
            chkClientName.Properties.DisplayMember = "ClientName";
            chkClientName.Properties.ValueMember = "Id";
        }
        /// <summary>
        /// 数据加载
        /// </summary>
        public void BindSource()
        {
            var list = new List<KeyValuePair<Enum, string>>();
            list.Add(new KeyValuePair<Enum, string>(PayerCatType.NoCharge, EnumHelper.GetEnumDesc(PayerCatType.NoCharge)));
            list.Add(new KeyValuePair<Enum, string>(PayerCatType.Charge, EnumHelper.GetEnumDesc(PayerCatType.Charge)));
            //收费状态
            cob_CostState.Properties.DataSource = list;
            //检查状态
            cob_CheckState.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ProjectIState));
            //总检状态
            cob_SummSate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(SummSate));
            //报告打印状态
            cob_PrintSate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(PrintSate));
            //人员体检状态
            cob_PersonalCheckState.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            cob_PersonalCheckState.ItemIndex = 0;
            //套餐
            cob_IsAddMinus.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(AddMinusType));
            dt_Stater.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            dt_End.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd");

            //登记状态
          //  repositoryItemLookUpEdit1.DataSource= EnumHelper.GetEnumDescs(typeof(RegisterState));
            repositoryItemLookUpEdit1.DataSource = RegisterStateHelper.GetSelectList();

            //repositoryItemLookUpEdit2.DataSource= EnumHelper.GetEnumDescs(typeof(ProjectIState));
            repositoryItemLookUpEdit2.DataSource = ProjectIStateHelper.GetProjectModels();

            //  repositoryItemLookUpEdit4.DataSource= EnumHelper.GetEnumDescs(typeof(SendToConfirm));

            repositoryItemLookUpEdit4.DataSource = SendToConfirmHelper.GetSendToConfirmModels();

        }
        private void edior_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //var editor = sender as PopupBaseEdit;
            //if (e.Button.Kind == ButtonPredefines.Delete)
            //{
            //    if (editor.EditValue == null)
            //        return;
            //    editor.EditValue = null;
            //}
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                var editor = sender as PopupBaseEdit;
                editor.EditValue = null;
                editor.EditValue = 0;
                if (editor.Name == EditPersonal.Name)
                    editor.EditValue = null;
               

            }
        }
        private void btn_Select_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }

            try
            {
                PatientExaminationCondition PEC = new PatientExaminationCondition();
                PEC.StartTime = Convert.ToDateTime(dt_Stater.Text);
                PEC.EndTime = Convert.ToDateTime(dt_End.Text).AddDays(1);
                if (!string.IsNullOrWhiteSpace(EditPersonal.Text))
                    PEC.IsPersonal = EditPersonal.Text;
                //单位
                if (!string.IsNullOrWhiteSpace(chkClientName.EditValue?.ToString()))
                {
                    //string[] str = chkClientName.EditValue?.ToString()?.Split(',');
                    //if (str != null)
                    //{
                    //    List<string> list = new List<string>();
                    //    foreach (var item in str)
                    //    {
                    //        list.Add(item.Trim());
                    //    }

                    //    PEC.ClientName = list;
                    //}
                    var value = chkClientName.EditValue as List<Guid>;
                    PEC.ClientRegId = new List<Guid?>();
                    value.ForEach(v => {
                        PEC.ClientRegId.Add(v);
                    });
                }
                PEC.Introducer = txtIntuducer.Text.Trim();
                //体检号
                PEC.ArchivesNum = txt_ArchivesNum.Text.Trim();
                PEC.CustomerBM = txt_ArchivesNum.Text.Trim();
                PEC.Name = txt_Name.Text.Trim();

                //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
                PEC.CheckState = Convert.ToInt32(cob_CheckState.EditValue);

                //总检状态 1未总检2已分诊3已初检4已审核
                PEC.SummSate = Convert.ToInt32(cob_SummSate.EditValue);

                //打印状态 1未打印2已打印
                PEC.PrintSate = Convert.ToInt32(cob_PrintSate.EditValue);

                //收费状态 1未收费2已收费3欠费
                PEC.CostState = Convert.ToInt32(cob_CostState.EditValue);

                //加减状态 1为正常项目2为加项3为减项
                //PEC.IsAddMinus = Convert.ToInt32(cob_IsAddMinus.EditValue);
                //if (!string.IsNullOrWhiteSpace(Convert.ToString(cob_IsAddMinus.EditValue)))
                //{
                    PEC.IsAddMinus = Convert.ToInt32(cob_IsAddMinus.EditValue);
               // }
                //体检状态
                PEC.PersonlCheckState = Convert.ToInt32(cob_PersonalCheckState.EditValue);
                //时间
                if (checkEdit.Checked)
                {
                    if (Convert.ToDateTime(dt_Stater.Text) > Convert.ToDateTime(dt_End.Text))
                    {
                        dxErrorProvider.SetError(dt_Stater, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        dt_Stater.Focus();
                        return;
                    }

                    //体检时间(用导诊时间来传值)
                    PEC.StartTime = Convert.ToDateTime(dt_Stater.Text);
                    PEC.EndTime = Convert.ToDateTime(dt_End.Text);
                    if (!string.IsNullOrWhiteSpace(comDateType.EditValue?.ToString()) && comDateType.EditValue?.ToString()=="体检日期")
                    {
                        PEC.DateType = 1;
                    }
                }
                else
                {
                    PEC.StartTime = null;
                    PEC.EndTime = null;
                }
                var result = app.GetATjlCustomerItemGrouplist(PEC);
                StatisticalResult(result);
                dgc.DataSource = result;

                //result[0].CustomerItemGroup.Add(new ATjlCustomerRegItemDto());
                dgc.RefreshDataSource();
                splashScreenManager.CloseWaitForm();
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

        /// <summary>
        /// 统计结果
        /// </summary>
        public void StatisticalResult(List<PatientExaminationProjectStatisticsViewDto> items)
        {
            string result = null;
            if (items.Count > 0)
            {
                result = "总人数：" + items.Count + "人； 已检：" + items.Where(r => r.CheckSate.HasValue&&r.CheckSate != (int)PhysicalEState.Not).ToList().Count
                    + "人； 放弃：" + items.Where(r => !string.IsNullOrEmpty(r.GiveupCheckItems)).ToList().Count
                    + "人； 待查：" + items.Where(r => !string.IsNullOrEmpty(r.AwaitCheckItems)).ToList().Count
                    + "人； 未检：" + items.Where(o=>o.CheckSate==null||o.CheckSate==(int)PhysicalEState.Not).ToList().Count
                    + "人； 加项：" + items.Where(r => !string.IsNullOrEmpty(r.AddCheckItems)).ToList().Count
                    + "人； 减项：" + items.Where(r => !string.IsNullOrEmpty(r.MinusCheckItems)).ToList().Count
                    + "人；应收金额：" + items.Sum(r => r.ItemPrice)
                    + "元；个人实收金额：" + items.Sum(r => r.PriceAfterDis)
                    + "元；操作时间:" + dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
            //总人数：已检、放弃、待查、未检、加项、减项 分别统计人数，应收金额、实收金额、操作时间。
            txt_Result.Text = result;
            dgv.GroupPanelText = result;
        }


        private void chkClientName_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                chkClientName.EditValue = null;
                //searchLookUpEdit1View.ClearSelection();
                chkClientName.ToolTip = "";
                chkClientName.RefreshEditValue();
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            //chkClientName.Properties.Items.Clear();
            txt_ArchivesNum.Text = string.Empty;
            txt_Name.Text = string.Empty;
            dt_Stater.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            dt_End.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            cob_CheckState.EditValue = null;
            cob_SummSate.EditValue = null;
            cob_PrintSate.EditValue = null;
            cob_CostState.EditValue = null;
            cob_IsAddMinus.EditValue = null;
            chkClientName.EditValue = null;
            chkClientName.RefreshEditValue();
            txtIntuducer.EditValue = null;
            EditPersonal.EditValue = null;
        }

        #region 状态格式化
        /// <summary>
        /// 状态格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //项目检查状态
            //if (e.Column.FieldName == "CheckState")
            //{
            //    switch (e.DisplayText)
            //    {
            //        case "1":
            //            e.DisplayText = "未检查";
            //            break;
            //        case "2":
            //            e.DisplayText = "已检查";
            //            break;
            //        case "3":
            //            e.DisplayText = "部分检查";
            //            break;
            //        case "4":
            //            e.DisplayText = "放弃";
            //            break;
            //        case "5":
            //            e.DisplayText = "待查";
            //            break;
            //    }
            //}
            //总检状态 1未总检2已分诊3已初检4已审核
            //if (e.Column.FieldName == "SummSate")
            //{
            //    switch (e.DisplayText)
            //    {
            //        case "1":
            //            e.DisplayText = "未总检";
            //            break;
            //        case "2":
            //            e.DisplayText = "已分诊";
            //            break;
            //        case "3":
            //            e.DisplayText = "已初检";
            //            break;
            //        case "4":
            //            e.DisplayText = "已审核";
            //            break;
            //    }
            //}
            //打印状态 1未打印2已打印
            //if (e.Column.FieldName == "PrintSate")
            //{
            //    switch (e.DisplayText)
            //    {
            //        case "1":
            //            e.DisplayText = "未打印";
            //            break;
            //        case "2":
            //            e.DisplayText = "已打印";
            //            break;
            //    }
            //}
            //收费状态 1未收费2已收费3欠费
            //if (e.Column.FieldName == "CostState")
            //{
            //    switch (e.DisplayText)
            //    {
            //        case "1":
            //            e.DisplayText = "未收费";
            //            break;
            //        case "2":
            //            e.DisplayText = "已收费";
            //            break;
            //        case "3":
            //            e.DisplayText = "欠费";
            //            break;
            //    }
            //}
            //加减状态 1为正常项目2为加项3为减项4调项减5调项加
            //if (e.Column.FieldName == "IsAddMinus")
            //{
            //    switch (e.DisplayText)
            //    {
            //        case "1":
            //            e.DisplayText = "正常";
            //            break;
            //        case "2":
            //            e.DisplayText = "加项";
            //            break;
            //        case "3":
            //            e.DisplayText = "减项";
            //            break;
            //        case "4":
            //            e.DisplayText = "调项减";
            //            break;
            //        case "5":
            //            e.DisplayText = "调项加";
            //            break;
            //    }
            //}
            //性别
            //if (e.Column.FieldName == "Sex") { 

            //    switch (e.DisplayText)
            //    {
            //        case "0":
            //            e.DisplayText = "未知的性别";
            //            break;
            //        case "1":
            //            e.DisplayText = "男";
            //            break;
            //        case "2":
            //            e.DisplayText = "女";
            //            break;

            // }
        }

        private void dgvXM_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            // 项目检查状态1未检查2已检查3部分检查4放弃5待查 6暂存
            if (e.Column.FieldName == "CheckState")
            {
                switch (e.DisplayText)
                {
                    case "1":
                        e.DisplayText = "未检查";
                        break;
                    case "2":
                        e.DisplayText = "已检查";
                        break;
                    case "3":
                        e.DisplayText = "部分检查";
                        break;
                    case "4":
                        e.DisplayText = "放弃";
                        break;
                    case "5":
                        e.DisplayText = "待查";
                        break;
                    case "6":
                        e.DisplayText = "暂存";
                        break;
                }
            }
            // 加减状态 1为正常项目2为加项3为减项
            if (e.Column.FieldName == "IsAddMinus")
            {
                switch (e.DisplayText)
                {
                    case "1":
                        e.DisplayText = "正常";
                        break;
                    case "2":
                        e.DisplayText = "加项";
                        break;
                    case "3":
                        e.DisplayText = "减项";
                        break;
                }
            }
            // 退费状态 1正常2带退费3退费 收费处退费后变为减项状态
            if (e.Column.FieldName == "RefundState")
            {
                switch (e.DisplayText)
                {
                    case "1":
                        e.DisplayText = "正常";
                        break;
                    case "2":
                        e.DisplayText = "待退费";
                        break;
                    case "3":
                        e.DisplayText = "退费";
                        break;
                }
            }
        }
        #endregion

        private void dgv_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void dgvXM_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            dgv.CustomExport("已检、放弃、待查、未检统计");
        }

        private void chkClientName_Popup(object sender, EventArgs e)
        {
            //得到当前SearchLookUpEdit弹出窗体
            PopupSearchLookUpEditForm form = (sender as IPopupControl).PopupWindow as PopupSearchLookUpEditForm;
            SearchEditLookUpPopup popup = form.Controls.OfType<SearchEditLookUpPopup>().FirstOrDefault();
            LayoutControl layout = popup.Controls.OfType<LayoutControl>().FirstOrDefault();
            //如果窗体内空间没有确认按钮，则自定义确认simplebutton，取消simplebutton，选中结果label
            if (layout.Controls.OfType<Control>().Where(ct => ct.Name == "btOK").FirstOrDefault() == null)
            {
                //得到空的空间
                EmptySpaceItem a = layout.Items.Where(it => it.TypeName == "EmptySpaceItem").FirstOrDefault() as EmptySpaceItem;

                //得到取消按钮，重写点击事件
                Control clearBtn = layout.Controls.OfType<Control>().Where(ct => ct.Name == "btClear").FirstOrDefault();
                LayoutControlItem clearLCI = (LayoutControlItem)layout.GetItemByControl(clearBtn);
                clearBtn.Click += clearBtn_Click;

                //添加一个simplebutton控件(确认按钮)
                LayoutControlItem myLCI = (LayoutControlItem)clearLCI.Owner.CreateLayoutItem(clearLCI.Parent);
                myLCI.TextVisible = false;
                SimpleButton btOK = new SimpleButton() { Name = "btOK", Text = "确定" };
                btOK.Click += btOK_Click;
                myLCI.Control = btOK;
                myLCI.SizeConstraintsType = SizeConstraintsType.Custom;//控件的大小设置为自定义
                myLCI.MaxSize = clearLCI.MaxSize;
                myLCI.MinSize = clearLCI.MinSize;
                myLCI.Move(clearLCI, DevExpress.XtraLayout.Utils.InsertType.Left);

                //添加一个label控件（选中结果显示）
                LayoutControlItem msgLCI = (LayoutControlItem)clearLCI.Owner.CreateLayoutItem(a.Parent);
                msgLCI.TextVisible = false;
                msgLCI.Move(a, DevExpress.XtraLayout.Utils.InsertType.Left);
                msgLCI.BestFitWeight = 100;
            }
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            //luValues.Clear();//将保存的数据清空
            //searchLookUpEdit1View.ClearSelection();
            chkClientName.EditValue = null;
            chkClientName.ToolTip = "";
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            chkClientName.ClosePopup();
        }
        private void chkClientName_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (chkClientName.EditValue != null)
                e.DisplayText = chkClientName.ToolTip;
            else
                e.DisplayText = "";
        }

        private void chkClientName_Closed(object sender, ClosedEventArgs e)
        {
            var row = searchLookUpEdit1View.GetSelectedRows();
            if (row.Count() > 0)
            {
                var str = "";
                var values = new List<Guid>();
                foreach (var r in row)
                {
                    var data = searchLookUpEdit1View.GetRow(r) as ClientRegNameComDto;
                    str += data.ClientName + "，";
                    values.Add(data.Id);
                }
                //txtClientRegId.Text = str;
                chkClientName.ToolTip = str;
                chkClientName.EditValue = values;
            }

        }

        private void FrmHZJCXMTJ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Select.PerformClick();
            }
        }
    }
}