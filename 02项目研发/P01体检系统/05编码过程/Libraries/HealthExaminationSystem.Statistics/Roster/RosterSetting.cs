using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Roster;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraLayout;
using DevExpress.Utils.Win;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Roster
{
    public partial class RosterSetting : UserBaseForm
    {
        /// <summary>
        /// 公共应用服务
        /// </summary>
        private readonly ICommonAppService _commonAppService;

        /// <summary>
        /// 体检人应用服务
        /// </summary>
        private readonly ICustomerAppService _customerAppServicep;
        
        private readonly IPrintPreviewAppService _printPreviewAppService;
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService;

        private readonly IInspectionTotalAppService _inspectionTotalService;
        public RosterSetting()
        {
            InitializeComponent();
            _printPreviewAppService = new PrintPreviewAppService();
            _customerAppServicep = new CustomerAppService();
            _commonAppService = new CommonAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
            _inspectionTotalService = new InspectionTotalAppService();
        }

        /// <summary>
        /// 已选套餐集合
        /// </summary>
        public List<SimpleItemSuitDto> SetMealChoice { get; set; }

        private List<CustomerRegDto> Dto { get; set; }
        private List<CustomerRegRosterDto> Dtos { get; set; }
        private int click { get; set; }
        private IPersonnelCategoryAppService _personnelCategoryAppService;
        private void RosterSetting_Load(object sender, EventArgs e)
        {
        
            System.Windows.Forms.Application.DoEvents();
            //txtPersonnelCategory.Properties.DataSource = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o=>o.IsActive==true)?.ToList();
            //检查时间
            var nowDate = _commonAppService.GetDateTimeNow().Now;
            string s1 = nowDate.ToShortDateString() + " 00:00:00";
            string e1 = nowDate.ToShortDateString() + " 23:59:59";
            deTJRQO.DateTime =DateTime.Parse(s1);//.AddDays(-1)
            deTJRQT.DateTime = DateTime.Parse(e1);

            deCJSJO.DateTime = nowDate;//.AddDays(-1)
            deCJSJT.DateTime = nowDate;

            dateSendStar.DateTime = nowDate;
            dateSendEnd.DateTime= nowDate;

            //customerreg.Columns[gridColumnZongjian.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            //customerreg.Columns[gridColumnZongjian.FieldName].DisplayFormat.Format =
            //    new CustomFormatter(CustomSummStateFormatter);
            //var data = _customerAppServicep.abpUsersDto();
            comboBoxEdit2.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            //下拉框赋值
            lueExaminationType.Properties.DataSource =
                DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ExaminationType);

            var sexlist = SexHelper.GetSexModelsForItemInfo();
            luesex.Properties.DataSource = sexlist;

            //单选框赋值
            lookUpEdit1.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            lookUpEdit1.Properties.NullText = "全部";

            //"单位"框赋值
            //var newlist = _customerAppServicep.QuereyClientRegInfos(new FullClientRegDto());
            //sleDW.Properties.DataSource = newlist.OrderByDescending(n => n.CreationTime).ToList();
            // sleDW.Properties.DataSource = _printPreviewAppService.GetClientInfos();
            sleDW.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            
             //人员列表
             _personnelCategoryAppService = new PersonnelCategoryAppService();
            txtPersonnelCategory.Properties.DataSource = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();
            txtPersonnelCategory.Properties.ValueMember = "Id";
            txtPersonnelCategory.Properties.DisplayMember = "Name";
            BindItemGroups();

            //登记状态
            lUpDJState.Properties.DataSource = RegisterStateHelper.GetSelectList();
            lUpDJState.ItemIndex = 1;
            //交表状态
            var sendToConfirmModels = SendToConfirmHelper.GetSendToConfirmModels();
            lookUpEditJiaobiaoZhuangtai.Properties.DataSource = sendToConfirmModels;
            //检查类型
            ChargeBM chargeBM = new ChargeBM();
            //工种
            chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
            var lis3 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            txtTypeWork.Properties.DataSource = lis3;


            chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
            var lis1 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            txtCheckType.Properties.DataSource = lis1;


            List<SupplierData> Supplierlist = new List<SupplierData> { new SupplierData { Id = "", Name = "全部" }, new SupplierData { Id = "0", Name = "团体" }, new SupplierData { Id = "1", Name = "个人" } };
            teYBKH.Properties.DataSource = Supplierlist;
            //teYBKH.Properties.ValueMember = "Id";
            //teYBKH.Properties.DisplayMember = "Name";
            //可复制
            //gridColumn1.OptionsColumn.AllowEdit = false;
            //gridColumn15.OptionsColumn.AllowEdit = false;

            CustomerSex.OptionsColumn.AllowEdit = false;
            CustomerAge.OptionsColumn.AllowEdit = false;
            gridColumn18.OptionsColumn.AllowEdit = false;
            gridColumn19.OptionsColumn.AllowEdit = false;
            gridColumn20.OptionsColumn.AllowEdit = false;
            gridColumn21.OptionsColumn.AllowEdit = false;
            gridColumn22.OptionsColumn.AllowEdit = false;
            gridColumn24.OptionsColumn.AllowEdit = false;
            gridColumnZongjian.OptionsColumn.AllowEdit = false;
            grTJZT.OptionsColumn.AllowEdit = false;
            drLQZT.OptionsColumn.AllowEdit = false;

            //gridColumnDAH.OptionsColumn.AllowEdit = false;

            try
            {
                var dto = new QueryCustomerRegDto();
                dto.LoginDateStartTime = deTJRQO.DateTime;
                dto.LoginDateEndTime = deTJRQT.DateTime;
                dto.BookingDateStartTime = deCJSJO.DateTime;
                dto.BookingDateEndTime = deCJSJT.DateTime;
                dto.RegisterState = (int)RegisterState.Yes;
                dto.Customer = new QueryCustomerDto { };
                var output = _customerAppServicep.QueryAll(new PageInputDto<QueryCustomerRegDto>
                { TotalPages = TotalPages, CurentPage = CurrentPage, Input = dto });
                if (output.Result == null)
                    return;

                TotalPages = output.TotalPages;
                if (click == 1)
                    CurrentPage = output.CurrentPage;
                else
                    CurrentPage = 1;
               // InitialNavigator(dataNavigator1);
                gridControl1.DataSource = output.Result;
                //customerreg.Columns[CustomerSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                //customerreg.Columns[CustomerSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);
                labelControl1.Text = "总人数：" + output.Result.FirstOrDefault().Total.ToString();
                labelControl6.Text = " 男：" + output.Result.FirstOrDefault().MaleTotal.ToString();
                labelControl8.Text = " 女：" + output.Result.FirstOrDefault().FemaleTotal.ToString();
                labelControl5.Text = " 未说明性别：" + output.Result.FirstOrDefault().Unknown.ToString();
                labelControl10.Text = " 未体检：" + output.Result.FirstOrDefault().NoTotal.ToString();
                labelControl12.Text = " 体检中：" + output.Result.FirstOrDefault().ConductTotal.ToString();
                labelControl14.Text = " 体检完成：" + output.Result.FirstOrDefault().AlreadyTotal.ToString();
                labelControl16.Text = " 免费：" + output.Result.FirstOrDefault().IsFreeTotal.ToString();


            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        /// <summary>
        /// 项目组合加载
        /// </summary>
        public void BindItemGroups()
        {
            cbo_xmzh.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
            cbo_xmzh.Properties.ValueMember = "Id";
            cbo_xmzh.Properties.DisplayMember = "ItemGroupName";
        }
        /// <summary>
        /// 自定义总检状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CustomSummStateFormatter(object obj)
        {
            var SummStates = SummSateHelper.GetSelectList();
            try
            {
                return SummStates.Find(r => r.Id == (int)obj).Display;
            }
            catch
            {
                return SummStates.Find(r => r.Id == (int)SummSate.NotAlwaysCheck).Display;
            }
        }



        private void sbQuery_Click(object sender, EventArgs e)
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
            try
            {
                var dto = new QueryCustomerRegDto();
                var queryCustomerDto = new QueryCustomerDto();

                //体检号
                if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
                    dto.CustomerBM = teTJH.Text.Trim();

                //身份证号
                if (!string.IsNullOrWhiteSpace(teSFZH.Text.Trim()))
                    queryCustomerDto.IDCardNo = teSFZH.Text.Trim();

                //团体/个人
                if (teYBKH.EditValue != null)
                    queryCustomerDto.personalOrGroup = teYBKH.EditValue.ToString();

                //登记状态
                if (lUpDJState.EditValue != null)
                {
                    dto.RegisterState = int.TryParse(lUpDJState.EditValue.ToString(), out var DJZT) ? (int?)DJZT : null;
                    dto.RegisterState = dto.RegisterState == 0 ? null : dto.RegisterState;
                }
                if (cbo_xmzh.EditValue != null)
                {
                    dto.GroupID =Guid.Parse(cbo_xmzh.EditValue.ToString());
                }

                //姓名
                if (!string.IsNullOrWhiteSpace(teName.Text.Trim()))
                    queryCustomerDto.Name = teName.Text.Trim();

                //性别
                if (luesex.EditValue != null)
                    queryCustomerDto.Sex = int.TryParse(luesex.EditValue.ToString(), out var sex) ? (int?)sex : null;

                //介绍人
                if (teJieShaoRen.EditValue != null)
                    dto.Introducer = teJieShaoRen.EditValue.ToString();
                //移动电话
                if (!string.IsNullOrWhiteSpace(tePhone.Text.Trim()))
                    queryCustomerDto.Mobile = tePhone.Text.Trim();

                

                //开票名称
                if (!string.IsNullOrWhiteSpace(textEditFPNo.Text.Trim()))
                    dto.FPNo= textEditFPNo.Text.Trim();

                //遍历控件 登记时间
                if (ceTJRQQ.Checked)
                {
                    if (Convert.ToDateTime(deTJRQO.Text) > Convert.ToDateTime(deTJRQT.Text))
                    {
                        dxErrorProvider.SetError(deTJRQT, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        deTJRQT.Focus();
                        return;
                    }

                    //体检时间(用导诊时间来传值)
                    dto.BookingDateStartTime = Convert.ToDateTime(deTJRQO.Text);
                    dto.BookingDateEndTime = Convert.ToDateTime(deTJRQT.Text);
                }
                //交表时间                
                if (checkSend.Checked)
                {
                    if (Convert.ToDateTime(dateSendStar.Text) > Convert.ToDateTime(dateSendEnd.Text))
                    {
                        dxErrorProvider.SetError(dateSendEnd, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        dateSendEnd.Focus();
                        return;
                    }

                    //体检时间(用导诊时间来传值)
                    dto.SendDateStartTime = Convert.ToDateTime(dateSendStar.Text);
                    dto.SendDateEndTime = Convert.ToDateTime(dateSendEnd.Text);
                }

                //遍历控件 创建时间
                if (CECJState.Checked)
                {
                    if (Convert.ToDateTime(deCJSJO.Text) > Convert.ToDateTime(deCJSJT.Text))
                    {
                        dxErrorProvider.SetError(deCJSJT, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        deTJRQT.Focus();
                        return;
                    }

                    //体检时间(用导诊时间来传值)
                    dto.LoginDateStartTime = Convert.ToDateTime(deCJSJO.Text);
                    dto.LoginDateEndTime = Convert.ToDateTime(deCJSJT.Text);
                }

                if (Convert.ToInt32(seO.Value) > Convert.ToInt32(seT.Value))
                {
                    dxErrorProvider.SetError(seT, string.Format(Variables.GreaterThanTips, "起始年龄", "结束年龄"));
                    seT.Focus();
                    return;
                }

                //年龄
                dto.AgeStart = Convert.ToInt32(seO.Value);
                dto.AgeEnd = Convert.ToInt32(seT.Value);

                //体检状态
                if (lookUpEdit1.EditValue != null)
                    dto.CheckSate = int.TryParse(lookUpEdit1.EditValue.ToString(), out var TJZT) ? (int?)TJZT : null;

                //登记状态
                if (lUpDJState.EditValue != null)
                {
                    dto.RegisterState = int.TryParse(lUpDJState.EditValue.ToString(), out var DJZT) ? (int?)DJZT : null;
                    dto.RegisterState = dto.RegisterState == 0 ? null : dto.RegisterState;
                }
               
                //交表状态
                if (lookUpEditJiaobiaoZhuangtai.EditValue != null)
                    dto.SendToConfrim = Convert.ToInt32(lookUpEditJiaobiaoZhuangtai.EditValue);
                //单位
                if (sleDW.EditValue != null)
                {
                    dto.ClientRegId = sleDW.EditValue as Guid?;
                }
                //登记人
                if (comboBoxEdit2.EditValue != null)
                {
                    dto.LoginUserId = Convert.ToInt32(comboBoxEdit2.EditValue);
                }
                //单位分组信息
                if (comboBoxEdit1.EditValue != null)
                {
                    dto.ClientTeamInfoId = (Guid?)comboBoxEdit1.EditValue;
                }
                if (!string.IsNullOrWhiteSpace(txtCheckType.EditValue?.ToString()))
                {
                    dto.PostState = txtCheckType.Text;
                }
                if (!string.IsNullOrWhiteSpace(txtTypeWork.EditValue?.ToString()))
                {
                    dto.TypeWork = txtTypeWork.Text;
                }
                //套餐
                if (SetMealChoice != null)
                    if (SetMealChoice.Count != 0)
                        dto.SetMealChoiceT = SetMealChoice.Select(o => o.Id).ToList();
                dto.Customer = queryCustomerDto;
                if (lueExaminationType.EditValue != null)
                    dto.PhysicalType = (int?)lueExaminationType.EditValue;
                //是否免费
                //if (checkEditIsFree.Checked)
                //    dto.IsFree = true;
                //else
                //    dto.IsFree = false;
                //if (!string.IsNullOrWhiteSpace(txtPersonnelCategory.Text))
                // {
                //dto.PersonnelCategoryId = Guid.Parse(txtPersonnelCategory.EditValue.ToString());
                // }
                dto.Remarks = "时分秒";
                dto.PersonnelCategoryIdL = txtPersonnelCategory.EditValue as List<Guid>;
                //var output = _customerAppServicep.QueryAll(new PageInputDto<QueryCustomerRegDto>
                //{ TotalPages = TotalPages, CurentPage = CurrentPage, Input = dto });

                var output = _customerAppServicep.GetAll(dto);
                if (output == null)
                {
                    TotalPages = 1;
                    CurrentPage = 1;
                   // InitialNavigator(dataNavigator1);
                    gridControl1.DataSource = output;
                    labelControl1.Text = "总人数：" + "0";
                    labelControl6.Text = " 男：" + "0";
                    labelControl8.Text = " 女：" + "0";
                    labelControl5.Text = " 未说明性别：" + "0";
                    labelControl10.Text = " 未体检：" + "0";
                    labelControl12.Text = " 体检中：" + "0";
                    labelControl14.Text = " 体检完成：" + "0";
                    labelControl16.Text = " 免费：" + "0";
                    labelControl3.Text="已审核：" + "0";
                    labelControl4.Text= "未审核：" + "0";
                    //labelSH.Text = "0";
                    //labelNSH.Text = "0";
                    return;
                }

                //customerreg.Columns[CustomerSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                //customerreg.Columns[CustomerSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);
                //TotalPages = output.TotalPages;
                //if (click == 1)
                //    CurrentPage = output.CurrentPage;
                //else
                //    CurrentPage = 1;
                //InitialNavigator(dataNavigator1);
                gridControl1.DataSource = output;
                //customerreg.Columns[CustomerSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                //customerreg.Columns[CustomerSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);
                labelControl1.Text = "总人数：" + output.Count.ToString();
                labelControl6.Text = " 男：" + output.Count(p=>p.Customer.Sex== (int)Sex.Man).ToString();
                labelControl8.Text = " 女：" + output.Count(p => p.Customer.Sex == (int)Sex.Woman).ToString();
                labelControl5.Text = " 未说明性别：" + output.Count(p => p.Customer.Sex == (int)Sex.GenderNotSpecified).ToString();
                labelControl10.Text = " 未体检：" + output.Count(p=>p.CheckSate== (int)ExaminationState.Alr).ToString();
                labelControl12.Text = " 体检中：" + output.Count(p => p.CheckSate == (int)ExaminationState.Unchecked).ToString();
                labelControl14.Text = " 体检完成：" + output.Count(p => p.CheckSate == (int)ExaminationState.OK).ToString();
                labelControl16.Text = " 免费：" + output.Count(p => p.IsFree == true).ToString();
                //labelSH.Text= output.Count(p => p.SummSate == (int)SummSate.Audited).ToString();
                //labelNSH.Text= output.Count(p => p.SummSate != (int)SummSate.Audited).ToString();
                labelControl3.Text = "已审核：" + output.Count(p => p.SummSate == (int)SummSate.Audited).ToString();
                labelControl4.Text = "未审核：" + output.Count(p => p.SummSate != (int)SummSate.Audited).ToString();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
            }
        }

        //套餐按钮事件
        private void sbXZTC_Click(object sender, EventArgs e)
        {
            using (var frm = new SetMealChoice(SetMealChoice))
            {
                labsuitname.Text = "";
                if (frm.ShowDialog() == DialogResult.OK)
                    SetMealChoice = frm.Dto;
                if (SetMealChoice != null && SetMealChoice.Count > 0)
                {
                    foreach (SimpleItemSuitDto st in SetMealChoice)
                    {
                        labsuitname.Text += st.ItemSuitName + ",";
                    }
                    labsuitname.Text = labsuitname.Text.TrimEnd(',');
                }
                else
                {
                    labsuitname.Text = "";
                }
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var CustomerReg = customerreg.GetFocusedRow() as CustomerRegRosterDto;
            //using (var frm = new FrmInspectionTotalShow(CustomerReg))
            //{
            //    if (frm.ShowDialog() == DialogResult.OK)
            //        return;
            //}

            #region 调用总检界面

            //var dto = gridControl.GetFocusedRowDto<InspectionTotalListDto>();

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
                    FrmInspectionTotal frmInspectionTotal = new FrmInspectionTotal(nowdate);
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

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            // Export();
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "体检档案";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            gridControl1.ExportToXls(saveFileDialog.FileName);
        }

        #region 导出信息
        public void Export()
        {
            var data = gridControl1.GetDtoListDataSource<CustomerRegDto>();
            DataTable table = new DataTable();
            table.Columns.Add("体检日期");
            table.Columns.Add("档案号");
            table.Columns.Add("姓名");
            table.Columns.Add("性别");
            table.Columns.Add("年龄");
            table.Columns.Add("手机号");
            table.Columns.Add("单位");
            table.Columns.Add("介绍人");
            table.Columns.Add("分组名称");
            table.Columns.Add("套餐名称");
            table.Columns.Add("实检金额");
            table.Columns.Add("金额");
            table.Columns.Add("加项");
            table.Columns.Add("减项");
            table.Columns.Add("实收金额");
            table.Columns.Add("折扣率");
            foreach (var item in data)
            {
                var row = table.NewRow();
                row["体检日期"] = item.LoginDate;
                row["档案号"] = item.Customer.ArchivesNum;
                row["姓名"] = item.Customer.Name;
                row["性别"] = SexHelper.CustomSexFormatter(item.Customer.Sex);
                row["年龄"] = item.Customer.Age;
                row["手机号"] = item.Customer.Mobile;
                row["单位"] = item.ClientReg == null ? "" : item.ClientReg.ClientInfo.ClientName;
                row["介绍人"] = item.Introducer;
                row["分组名称"] = item.ClientTeamInfo == null ? "" : item.ClientTeamInfo.TeamName;
                row["套餐名称"] = item.ItemSuitName;

                row["实检金额"] = item.AmountChecked;
                //加减项字符串
                string addItem = string.Empty;
                string minusItem = string.Empty;
                var groupItem = item.CustomerItemGroup;
                groupItem.ForEach(g =>
                {
                    if (g.IsAddMinus == (int)AddMinusType.Add)
                        addItem += g.ItemGroupName + ",";
                    else if (g.IsAddMinus == (int)AddMinusType.Minus)
                        minusItem += g.ItemGroupName + ",";
                });
                //实收应收折扣计算
                decimal Shouldmoney = 0.00m;
                decimal Actualmoney = 0.00m;
                if (item.MReceiptInfo != null)
                {
                    Shouldmoney = item.MReceiptInfo.Sum(m => m.Shouldmoney);
                    Actualmoney = item.MReceiptInfo.Sum(m => m.Actualmoney);
                }
                row["金额"] = Shouldmoney;
                row["实收金额"] = Actualmoney;
                row["折扣率"] = Shouldmoney == 0 ? 0 : (Shouldmoney - Actualmoney) / Shouldmoney;
                row["加项"] = addItem.Trim(',');
                row["减项"] = minusItem.Trim(',');
                table.Rows.Add(row);
            }
            //ExcelHelper.ExportToExcel("花名册", gridControl1);
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "花名册";
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK
            ) //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
                return;
            var FileName = saveFileDialog.FileName;
            ExcelHelper ex = new ExcelHelper(saveFileDialog.FileName);
            ex.DataTableToExcel(table, "花名册", true);
            ex.Dispose();
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
    DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }
        #endregion

        private void dataNavigator1_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            click = 1;
            sbQuery_Click(sender, e);
            click = 0;
        }

        private void RosterSetting_Shown(object sender, EventArgs e)
        {
        }

        private void sleDW_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                sleDW.EditValue = null;
                sleDW.Properties.NullText = null;
            }

            if (e.Button.Index == -1)
                sleDW.EditValue = null;
        }

        private void lueExaminationType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lueExaminationType.EditValue = null;
        }

        private void luesex_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                luesex.EditValue = null;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //体检号
            teTJH.Text = null;

            //身份证号
            teSFZH.Text = null;

            //医保卡号
            teYBKH.EditValue = null;

            //姓名
            teName.Text = null;

            //性别
            luesex.EditValue = null;

            //移动电话
            tePhone.Text = null;

            //介绍人
            teJieShaoRen.Text = null;

            //复选框控件
            ceTJRQQ.Checked = false;

            //体检时间(用导诊时间来传值)
            deTJRQO.Text = null;
            deTJRQT.Text = null;

            //年龄
            seO.Value = 0;
            seT.Value = 0;

            //体检类别
            lueExaminationType.EditValue = null;

            //体检状态
            // rgTJZT.EditValue = null;
            // lookUpEdit1.SelectedIndex = 0;
            //单位
            sleDW.EditValue = null;
            sleDW.Properties.NullText = null;
            //套餐
            SetMealChoice = null;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var question = XtraMessageBox.Show("确定导出全部？此操作会占用很长时间", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
                return;

            dxErrorProvider.ClearErrors();
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }

            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            //gridControl1.DataSource = null;
            AutoLoading(() =>
            {
                //var grid = gridControl1.GetDtoListDataSource<CustomerRegRosterDto>();
                //if (grid != null)
                    Dtos = gridControl1.GetDtoListDataSource<CustomerRegRosterDto>().ToList();
                //else
                    //Dto = null;

                //Dto = gridControl1.GetDtoListDataSource<CustomerRegDto>().ToList();
                var dto = new QueryCustomerRegDto();
                var queryCustomerDto = new QueryCustomerDto();

                //体检号
                if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
                    dto.CustomerBM = teTJH.Text.Trim();

                //身份证号
                if (!string.IsNullOrWhiteSpace(teSFZH.Text.Trim()))
                    queryCustomerDto.IDCardNo = teSFZH.Text.Trim();

                //团体/个人
                if (teYBKH.EditValue != null)
                    queryCustomerDto.personalOrGroup = teYBKH.EditValue.ToString();
                //组合
                if (cbo_xmzh.EditValue != null)
                {
                    dto.GroupID = Guid.Parse(cbo_xmzh.EditValue.ToString());
                }

                //登记状态
                if (lUpDJState.EditValue != null)
                {
                    dto.RegisterState = int.TryParse(lUpDJState.EditValue.ToString(), out var DJZT) ? (int?)DJZT : null;
                    dto.RegisterState = dto.RegisterState == 0 ? null : dto.RegisterState;
                }

                //姓名
                if (!string.IsNullOrWhiteSpace(teName.Text.Trim()))
                    queryCustomerDto.Name = teName.Text.Trim();

                //性别
                if (luesex.EditValue != null)
                    queryCustomerDto.Sex = int.TryParse(luesex.EditValue.ToString(), out var sex) ? (int?)sex : null;

                //介绍人
                if (teJieShaoRen.EditValue != null)
                    dto.Introducer = teJieShaoRen.EditValue.ToString();
                //移动电话
                if (!string.IsNullOrWhiteSpace(tePhone.Text.Trim()))
                    queryCustomerDto.Mobile = tePhone.Text.Trim();

               
                if (!string.IsNullOrWhiteSpace(txtCheckType.EditValue?.ToString()))
                {
                    dto.PostState = txtCheckType.Text;
                }
                if (!string.IsNullOrWhiteSpace(txtTypeWork.EditValue?.ToString()))
                {
                    dto.TypeWork = txtTypeWork.Text;
                }

                //遍历控件 登记时间
                if (ceTJRQQ.Checked)
                {
                    if (Convert.ToDateTime(deTJRQO.Text) > Convert.ToDateTime(deTJRQT.Text))
                    {
                        dxErrorProvider.SetError(deTJRQT, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        deTJRQT.Focus();
                        return;
                    }

                    //体检时间(用导诊时间来传值)
                    dto.BookingDateStartTime = Convert.ToDateTime(deTJRQO.Text);
                    dto.BookingDateEndTime = Convert.ToDateTime(deTJRQT.Text);
                }
                //交表时间                
                if (checkSend.Checked)
                {
                    if (Convert.ToDateTime(dateSendStar.Text) > Convert.ToDateTime(dateSendEnd.Text))
                    {
                        dxErrorProvider.SetError(dateSendEnd, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        dateSendEnd.Focus();
                        return;
                    }

                    //体检时间(用导诊时间来传值)
                    dto.SendDateStartTime = Convert.ToDateTime(dateSendStar.Text);
                    dto.SendDateEndTime = Convert.ToDateTime(dateSendEnd.Text);
                }

                //遍历控件 创建时间
                if (CECJState.Checked)
                {
                    if (Convert.ToDateTime(deCJSJO.Text) > Convert.ToDateTime(deCJSJT.Text))
                    {
                        dxErrorProvider.SetError(deCJSJT, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        deTJRQT.Focus();
                        return;
                    }

                    //体检时间(用导诊时间来传值)
                    dto.LoginDateStartTime = Convert.ToDateTime(deCJSJO.Text);
                    dto.LoginDateEndTime = Convert.ToDateTime(deCJSJT.Text);
                }

                if (Convert.ToInt32(seO.Value) > Convert.ToInt32(seT.Value))
                {
                    dxErrorProvider.SetError(seT, string.Format(Variables.GreaterThanTips, "起始年龄", "结束年龄"));
                    seT.Focus();
                    return;
                }

                //年龄
                dto.AgeStart = Convert.ToInt32(seO.Value);
                dto.AgeEnd = Convert.ToInt32(seT.Value);

                //体检状态
                if (lookUpEdit1.EditValue != null)
                    dto.CheckSate = int.TryParse(lookUpEdit1.EditValue.ToString(), out var TJZT) ? (int?)TJZT : null;

                //登记状态
                if (lUpDJState.EditValue != null)
                {
                    dto.RegisterState = int.TryParse(lUpDJState.EditValue.ToString(), out var DJZT) ? (int?)DJZT : null;
                    dto.RegisterState = dto.RegisterState == 0 ? null : dto.RegisterState;
                }

                //交表状态
                if (lookUpEditJiaobiaoZhuangtai.EditValue != null)
                    dto.SendToConfrim = Convert.ToInt32(lookUpEditJiaobiaoZhuangtai.EditValue);
                //单位
                if (sleDW.EditValue != null)
                {
                    dto.ClientRegId = sleDW.EditValue as Guid?;
                }

                //套餐
                if (SetMealChoice != null)
                    if (SetMealChoice.Count != 0)
                        dto.SetMealChoiceT = SetMealChoice.Select(o => o.Id).ToList();
                dto.Customer = queryCustomerDto;
                if (lueExaminationType.EditValue != null)
                    dto.ClientType = lueExaminationType.EditValue.ToString();
                //是否免费
                //if (checkEditIsFree.Checked)
                //    dto.IsFree = true;
                //else
                //    dto.IsFree = false;
                //if (!string.IsNullOrWhiteSpace(txtPersonnelCategory.Text))
                // {
                //dto.PersonnelCategoryId = Guid.Parse(txtPersonnelCategory.EditValue.ToString());
                // }
                dto.PersonnelCategoryIdL = txtPersonnelCategory.EditValue as List<Guid>;
                try
                {
                    dto.Remarks = "时分秒";
                    gridControl1.DataSource = _customerAppServicep.GetAll(dto);

                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
                finally
                {
                    if (closeWait)
                        if (splashScreenManager.IsSplashFormVisible)
                            splashScreenManager.CloseWaitForm();
                }
                //  Export();
                var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.FileName = "体检档案";
                saveFileDialog.Title = "导出Excel";
                saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                var dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                    return;
                gridControl1.ExportToXls(saveFileDialog.FileName);
                gridControl1.DataSource = Dtos;
            });
        }
    

        private void txtPersonnelCategory_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                txtPersonnelCategory.EditValue = null;
        }       

        private void txtPersonnelCategory_Popup(object sender, EventArgs e)
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
                //msgLCI.Control = searchResult;
                msgLCI.Move(a, DevExpress.XtraLayout.Utils.InsertType.Left);
                msgLCI.BestFitWeight = 100;
            }
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            //luValues.Clear();//将保存的数据清空
            gridView1.ClearSelection();
            txtPersonnelCategory.EditValue = null;
            txtPersonnelCategory.ToolTip = "";
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            txtPersonnelCategory.ClosePopup();
        }

        private void txtPersonnelCategory_Closed(object sender, ClosedEventArgs e)
        {
            var row = gridView1.GetSelectedRows();
            if (row.Count() > 0)
            {
                var str = "";
                var values = new List<Guid>();
                foreach (var r in row)
                {
                    var data = gridView1.GetRow(r) as Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto;
                    str += data.Name + "，";
                    values.Add(data.Id);
                }
                //txtClientRegId.Text = str;
                txtPersonnelCategory.ToolTip = str;
                txtPersonnelCategory.EditValue = values;
            }
        }

        private void txtPersonnelCategory_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (txtPersonnelCategory.EditValue != null)
                e.DisplayText = txtPersonnelCategory.ToolTip;
            else
                e.DisplayText = "";
        }

        private void txtPersonnelCategory_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtPersonnelCategory.EditValue = null;
                gridView1.ClearSelection();
                txtPersonnelCategory.ToolTip = "";
                txtPersonnelCategory.RefreshEditValue();
            }
        }

        private void ceTJRQQ_CheckedChanged(object sender, EventArgs e)
        {
            if (ceTJRQQ.Checked)
            {
                deTJRQO.ReadOnly = false;
                deTJRQT.ReadOnly = false;
                CECJState.Checked = false;
            }
            else
            {
                deTJRQO.ReadOnly = true;
                deTJRQT.ReadOnly = true;
            }
        }

        private void CECJState_CheckedChanged(object sender, EventArgs e)
        {
            if (CECJState.Checked)
            {
                deCJSJO.ReadOnly = false;
                deCJSJT.ReadOnly = false;
                ceTJRQQ.Checked = false;
            }
            else
            {
                deCJSJO.ReadOnly = true;
                deCJSJT.ReadOnly = true;
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            //var CustomerReg = customerreg.GetFocusedRow() as CustomerRegRosterDto;
            //if (CustomerReg == null)
            //    return;
            //using (var frm = new FrmInspectionTotalShow(CustomerReg))
            //{
            //    if (frm.ShowDialog() == DialogResult.OK)
            //        return;
            //}
        }

        private void lookUpEditJiaobiaoZhuangtai_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookUpEditJiaobiaoZhuangtai.EditValue = null;
            }
        }

        private void sleDW_EditValueChanged(object sender, EventArgs e)
        {
            ClientTeamInfoDto clientTeamInfoDto = new ClientTeamInfoDto();
            if ((Guid)sleDW.EditValue != null)
            {
                clientTeamInfoDto.Id = (Guid)sleDW.EditValue;
                var data = _customerAppServicep.QueryClientTeamInfoes(clientTeamInfoDto);
                comboBoxEdit1.Properties.DataSource = data;
            }
            
        }

        private void txtTypeWork_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtTypeWork.EditValue = null;
                
            }
        }

        private void txtCheckType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtCheckType.EditValue = null;

            }
        }
    }

}
/// <summary>
/// 团体/个人
/// </summary>
public class SupplierData
{
    public string Id { get; set; }

    public string Name { get; set; }//供应商名称
}