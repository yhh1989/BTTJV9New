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
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using static DevExpress.XtraEditors.BaseCheckedListBoxControl;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Statistics.Department;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraLayout;
using DevExpress.Utils.Win;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using DevExpress.XtraCharts;
using System.Globalization;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.InspectionProject
{
    public partial class FrmKSGZLTJ : UserBaseForm
    {
        public FrmKSGZLTJ()
        {
            InitializeComponent();
        }
        #region  全局变量
        DoctorStationAppService _proxy = new DoctorStationAppService();
        /// <summary>
        /// 科室查询类
        /// </summary>
        DepartmentAppService _departProxy = new DepartmentAppService();

        private ICustomerAppService customerSvr;//体检预约
        private List<ClientRegNameComDto> clientRegs;//单位及分组字典
        private List<BasicDictionaryDto> customType;//客户类别字典
        private List<SexModel> sexList;//性别字典
        /// <summary>
        /// 获取时间
        /// </summary>
        CommonAppService _commonProxy = new CommonAppService();
        ClientRegAppService _clientregSvr = new ClientRegAppService();


        ///// <summary>
        ///// 记录当前科室Id  防止重复点击
        ///// </summary>
        //private Guid _currentDepartmentId;
        #endregion

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Guid> departmentIds = new List<Guid>();
            string[] arrIds = chkcmbDepartment.Properties.GetCheckedItems().ToString().Split(',');
            if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
            {
                foreach (string item in arrIds)
                {
                    departmentIds.Add(new Guid(item));
                }
            }
            else
            {
                XtraMessageBox.Show("查询科室不能为空");
                return;
            }
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                btnSearch.Enabled = false; //防止重复点击加载卡死现象
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {

                QueryClass query = new QueryClass();
                //科室
                query.DepartmentBMList = departmentIds;

                if (txtClientRegID.Text != null && txtClientRegID.Text != string.Empty)
                {
                    ////单位
                    //List<Guid?> clientRegs = new List<Guid?>();
                    //string[] arrClientIds = txtClientRegID.Properties.GetCheckedItems().ToString().Split(',');
                    //if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
                    //{
                    //    foreach (string item in arrClientIds)
                    //    {
                    //        clientRegs.Add(new Guid(item));
                    //    }
                    //}
                    //Guid clientInfoId = new Guid(txtClientRegID.EditValue.ToString());
                    //单位信息
                    var value = txtClientRegID.EditValue as List<Guid>;
                    query.ClientInfoRegId = new List<Guid?>();
                    value.ForEach(v=> {
                        query.ClientInfoRegId.Add(v);
                    });
                }
                //医生
                if (!string.IsNullOrWhiteSpace(txtDoctor.Text))
                {
                    query.DoctorName = txtDoctor.Text;
                }

                //时间

                if (dtpStart.EditValue != null)
                {
                    query.LastModificationTimeBign = dtpStart.DateTime.Date;
                    //query.LastModificationTimeBign = Convert.ToDateTime(dtpStart.EditValue.ToString());
                }

                if (dtpEnd.EditValue != null)
                {
                    //query.LastModificationTimeEnd = Convert.ToDateTime(dtpEnd.EditValue.ToString());
                    query.LastModificationTimeEnd = dtpEnd.DateTime.Date.AddDays(1);
                }
                if (!string.IsNullOrWhiteSpace(comDateType.EditValue?.ToString()) && comDateType.EditValue?.ToString() == "体检日期")
                {
                    query.DateType = 1;
                }
                if (grpCustomType.EditValue != null)
                    query.CustomerType = (int)grpCustomType.EditValue;
                if (gridLookUpSex.EditValue != null)
                {
                    query.Sex = (int)gridLookUpSex.EditValue;
                }
                //检查医生还是审核医生
                if (radioGroup1.EditValue != null)
                {
                    query.EmpType = int.Parse( radioGroup1.EditValue.ToString());
                }
               
                gcItem.DataSource = _proxy.KSGZLStatistics(query);
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
                    btnSearch.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmKSGZL_Load(object sender, EventArgs e)
        {
           
            customerSvr = new CustomerAppService();
            Init();
            dtpStart.DateTime = _commonProxy.GetDateTimeNow().Now;
            dtpEnd.DateTime = _commonProxy.GetDateTimeNow().Now;
            dateStar.Text = _commonProxy.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            dateEnd.Text = _commonProxy.GetDateTimeNow().Now.ToString("yyyy-MM-dd");

            dateEditStar.EditValue= _commonProxy.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            dateEditEnd.EditValue = _commonProxy.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            sexList = SexHelper.GetSexForPerson();// SexHelper.GetSexModelsForItemInfo();
            gridLookUpSex.Properties.DataSource = sexList;//性别
        }
        /// <summary>
        /// 初始化窗体控件数据
        /// </summary>
        private void Init()
        {
            checkedComboBoxEditCharName.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ChargeCategory);
            checkedComboBoxEditCharName.Properties.DisplayMember = "Text";
            checkedComboBoxEditCharName.Properties.ValueMember = "Value";
            //List<TbmDepartmentDto> departmentList = _departProxy.GetAll();
            chkcmbDepartment.Properties.DataSource = CurrentUser.TbmDepartments?.OrderBy(o => o.OrderNum)?.ToList(); //departmentList;
            chkcmbDepartment.Properties.DisplayMember = "Name";
            chkcmbDepartment.Properties.ValueMember = "Id";
            comboBoxEditDepartmentType1.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.LargeDepatType);
            comboBoxEditDepartmentType1.Properties.DisplayMember = "Text";
            comboBoxEditDepartmentType1.Properties.ValueMember = "Value";

            repositoryItemLookUpEdit1.DataSource= DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.LargeDepatType);

            searchLookUser.Properties.DataSource = DefinedCacheHelper.GetComboUsers().ToList();

            txt_Department.Properties.DataSource = CurrentUser.TbmDepartments?.OrderBy(o => o.OrderNum)?.ToList(); //departmentList;;
            txt_Department.Properties.ValueMember = "Id";
            txt_Department.Properties.DisplayMember = "Name";

            // clientRegs = customerSvr.QuereyClientRegInfos(new FullClientRegDto());//加载单位分组数据
            // clientRegs =_clientregSvr.getClientRegNameCom();
            clientRegs = DefinedCacheHelper.GetClientRegNameComDto();
            txtClientRegID.Properties.DataSource = clientRegs;
            txtClientRegID.Properties.DisplayMember = "ClientName";
            txtClientRegID.Properties.ValueMember = "Id";

            searchLookUpEditClient.Properties.DataSource = clientRegs;
            searchLookUpEditClient.Properties.DisplayMember = "ClientName";
            searchLookUpEditClient.Properties.ValueMember = "Id";
            //客户类别
            customType = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList();// Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CustomerType);
            grpCustomType.Properties.DataSource = customType;
            ////性别
            //customSex = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList();// Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CustomerType);
            //grpCustomType.Properties.DataSource = customType;
        }
        /*

        /// <summary>
        /// 左侧查询dgv行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvResult_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {
                SearchKSGZLStatisticsDto focuseRow = gvResult.GetFocusedRow() as SearchKSGZLStatisticsDto;
                
                if (focuseRow != null)
                {
                    //防止重复点击处理
                    if (focuseRow.Department_Id == _currentDepartmentId) return;

                    QueryClass query = new QueryClass();
                    if (dtpStart.EditValue != null)
                        query.LastModificationTimeBign = Convert.ToDateTime(dtpStart.EditValue.ToString());
                    if (dtpEnd.EditValue != null)
                        query.LastModificationTimeEnd = Convert.ToDateTime(dtpEnd.EditValue.ToString());

                    List<Guid> departmentIds = new List<Guid>();//科室id
                    departmentIds.Add((focuseRow.Department_Id));
                    query.DepartmentBMList = departmentIds;
                    gcItem.DataSource = _proxy.KSGZLItemStatistics(query);

                    //防止重复点击处理
                    _currentDepartmentId = focuseRow.Department_Id;
                }
            }
            catch (UserFriendlyException ex)
            {
                XtraMessageBox.Show(ex.Message, ex.Description, ex.Buttons, ex.Icon);
            }
            finally
            {
                if (closeWait)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }
        }*/
        /// <summary>
        /// 项目工作量结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportItemResult_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportToExcel("科室工作量", gcItem);
            //gvItem.CustomExport("科室工作量");
        }
        /// <summary>
        /// <summary>        /// 科室工作量统计图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DuiBiKeShiGongZuoLiang_Click(object sender, EventArgs e)
        {
            FrmKSGZLTJTX fm = new FrmKSGZLTJTX();
            fm.ShowDialog();
        }
        /// <summary>
        /// 汇总颜色标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvItem_RowStyle(object sender, RowStyleEventArgs e)
        {
            //科室汇总一行高亮色
            if (e.RowHandle > 0 && gvItem.GetRowCellValue(e.RowHandle, "InspectEmployeeName")?.ToString() == string.Empty)
            {
                e.Appearance.BackColor = Color.Gray;
            }
        }
        /// <summary>
        /// 其他压力导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYLExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportToExcel("其他统计", gcOther);
        }

        private void chkcmbDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                chkcmbDepartment.EditValue = null;
                chkcmbDepartment.RefreshEditValue();
            }
        }

        private void grpCustomType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                grpCustomType.EditValue = null;
            }
        }

        private void txtClientRegID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                searchLookUpEdit1View.ClearSelection();
                txtClientRegID.ToolTip = "";
                txtClientRegID.EditValue = null;
                txtClientRegID.RefreshEditValue();
            }

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYLSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtClientRegID_Closed(object sender, ClosedEventArgs e)
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
                txtClientRegID.ToolTip = str;
                txtClientRegID.EditValue = values;
            }
        }

        private void txtClientRegID_Popup(object sender, EventArgs e)
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
            searchLookUpEdit1View.ClearSelection();
            txtClientRegID.EditValue = null;
            txtClientRegID.ToolTip = "";
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            txtClientRegID.ClosePopup();
        }
        private void txtClientRegID_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (txtClientRegID.EditValue != null)
                e.DisplayText = txtClientRegID.ToolTip;
            else
                e.DisplayText = "";
        }

        private void chkcmbDepartment_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                chkcmbDepartment.EditValue = string.Empty;
                chkcmbDepartment.RefreshEditValue();
            }
            
        }

        private void grpCustomType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                grpCustomType.EditValue = null;
        }

        private void 其他统计_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
           
        }
        #region 统计图
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            dgc.DataSource = null;
            DataTable dt = null;
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(this.txt_Department.EditValue?.ToString()))
                {
                    ShowMessageBoxWarning("查询科室不能为空");
                    return;
                }
                HBQueryClass query = new HBQueryClass();
                DateTime QueryStartTime = Convert.ToDateTime(dateStar.EditValue.ToString());
                DateTime QueryEndTime = Convert.ToDateTime(dateEnd.EditValue.ToString());
                if (QueryStartTime > QueryEndTime || QueryStartTime.Year != QueryEndTime.Year || dateStar.EditValue == null || dateEnd.EditValue == null)
                {
                    ShowMessageBoxWarning("日期格式不正确，请选择正确的时间范围");
                    return;
                }
                //类型
                //if (radioGroup2.SelectedIndex != null && radioGroup2.SelectedIndex==0)
                //{
                //    query.CkeckedCount = radioGroup2.SelectedIndex;
                //}
                //if (radioGroup2.SelectedIndex != null && radioGroup2.SelectedIndex == 1)
                //{
                //    query.CheckedPerson = radioGroup2.SelectedIndex;
                //}
                //时间
                if (dateStar.EditValue != null)
                {
                    query.DQStartTime = QueryStartTime;
                }
                if (dateEnd.EditValue != null)
                {
                    query.DQEndTime = QueryEndTime;
                }
                if (rdo_Period.SelectedIndex == 0)
                {
                    query.WeekQuery = true;
                }
                else
                    query.WeekQuery = false;
                List<Guid> departmentIds = new List<Guid>();//科室id

                if (!string.IsNullOrWhiteSpace(txt_Department.EditValue.ToString()))
                {
                    string[] str = txt_Department.EditValue?.ToString()?.Split(',');
                    if (str != null)
                    {
                        List<System.Guid> list = new List<System.Guid>();
                        foreach (var item in str)
                        {
                            list.Add(new Guid(item));
                        }
                        departmentIds = list;
                    }
                }
                query.DepartmentBMList = departmentIds;
                dt = _proxy.KSGZLSTStatistics(query);
                if (dt.Rows.Count <= 0)
                {
                    splashScreenManager.CloseWaitForm();
                    return;
                }
                if (rdo_Period.SelectedIndex == 0)
                {
                    GetPeriod(QueryStartTime, QueryEndTime);
                    DataTable quarter = new DataTable();
                    quarter = dt.Clone();
                    var list = YearList;
                    foreach (var groupitem in list)
                    {
                        var dr = quarter.NewRow();
                        for (int j = 1; j < quarter.Columns.Count; j++)
                        {
                            string columName = quarter.Columns[j].ColumnName;
                            int tola = dt.AsEnumerable().Where(r => Convert.ToDateTime(r["周期"].ToString())
                            > groupitem.STtime && Convert.ToDateTime(r["周期"].ToString())
                            < groupitem.EDtime).Sum(n => Convert.ToInt32(n.IsNull(columName) ? 0 : n[columName]));
                            dr[columName] = tola;
                        }
                        dr["周期"] = groupitem.Type;
                        quarter.Rows.Add(dr);
                    }
                    dt = quarter;
                }
                if (rdo_Period.SelectedIndex == 2)//季度
                {
                    DataTable quarter = new DataTable();
                    quarter = dt.Clone();
                    var list = QueryList();
                    foreach (var groupitem in list)
                    {
                        var dr = quarter.NewRow();
                        for (int i = 1; i < quarter.Columns.Count; i++)
                        {
                            string columName = quarter.Columns[i].ColumnName;
                            int tola = dt.AsEnumerable().Where(r => groupitem.Query.Contains(r["周期"].ToString())).Sum(n => Convert.ToInt32(n.IsNull(columName) ? 0 : n[columName]));
                            dr[columName] = tola;
                        }
                        dr["周期"] = groupitem.Type;
                        quarter.Rows.Add(dr);
                    }
                    dt = quarter;
                }
                ToLineGragh(dt);
                dgv.Columns.Clear();
                DataTable NewDt = dt.Copy();
                var total = NewDt.NewRow();
                for (int j = 1; j < NewDt.Columns.Count; j++)
                {
                    int Count = 0;
                    for (int i = 0; i < NewDt.Rows.Count; i++)
                    {
                        Count += Int32.Parse(NewDt.Rows[i][NewDt.Columns[j].ColumnName].ToString());
                    }
                    total[NewDt.Columns[j].ColumnName] = Count;
                    total["周期"] = "汇总";
                }
                NewDt.Rows.Add(total);
                dgc.DataSource = NewDt;
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

        #region 日期获取
        public class Statistic
        {
            public string Type { get; set; }
            public DateTime STtime { get; set; }
            public DateTime EDtime { get; set; }
            public List<string> Query { get; set; }
        }
        public class JiDuUtil
        {
            public string JiDu { get; set; }
            public string YueFen { get; set; }
        }
        /// <summary>
        /// 季度
        /// </summary>
        /// <returns></returns>
        public List<Statistic> QueryList()
        {
            List<Statistic> list = new List<Statistic>();
            list.Add(new Statistic() { Type = "第一季度", Query = new List<string>() { "1月", "2月", "3月" } });
            list.Add(new Statistic() { Type = "第二季度", Query = new List<string>() { "4月", "5月", "6月" } });
            list.Add(new Statistic() { Type = "第三季度", Query = new List<string>() { "7月", "8月", "9月" } });
            list.Add(new Statistic() { Type = "第四季度", Query = new List<string>() { "10月", "11月", "12月" } });
            return list;
        }

        /// <summary>
        /// 获取指定期间的起止日期
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        List<Statistic> YearList;
        public void GetPeriod(DateTime beginDate, DateTime endDate)
        {
            YearList = new List<Statistic>();
            int Syear = beginDate.Year;
            beginDate = beginDate.AddDays(1 - Convert.ToInt32(beginDate.DayOfWeek.ToString("d")));  //本周周一
            int weeks = GetWeekOfYear(beginDate) - 1;//本年第几周
            GetWeek(Syear - 1, weeks + 1);
            //去年第几周的开始时间和结束时间
            //本年查询周数
            for (DateTime dt = beginDate.Date; dt < Convert.ToDateTime(dtpEnd.EditValue.ToString()); dt = dt.AddDays(6))
            {
                weeks = weeks + 1;
                Statistic result = new Statistic();
                result.Type = "第" + weeks + "周";
                result.STtime = dt;
                result.EDtime = dt.AddDays(7);
                YearList.Add(result);
            }
        }

        /// <summary>
        /// 一年中的周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }
        /// <summary>
        /// 用年份和第几周来获得一周开始和结束的时间,这里用星期一做开始。
        /// </summary>
        DateTime dtweekStart;
        DateTime dtweekeEnd;
        public void GetWeek(int year, int weekNum)
        {
            var dateTime = new DateTime(year, 1, 1);
            dateTime = dateTime.AddDays(7 * weekNum);
            dtweekStart = dateTime.AddDays(-(int)dateTime.DayOfWeek + (int)DayOfWeek.Monday);
            dtweekeEnd = dateTime.AddDays((int)DayOfWeek.Saturday - (int)dateTime.DayOfWeek + 1);
        }
        #endregion
        class RecordData
        {
            public string name { get; set; }
            public int value { get; set; }
        }
        #region 图形
        /// <summary>
        /// 把接收到的数据转换额外柱状图表所需要的数据, 绘制图表
        /// </summary>
        private void ToLineGragh(DataTable data)
        {
            string xBindName = "周期";
            //string seriesName = "科室";
            ViewType seriesType = ViewType.Bar;
            ChartControl.Series.Clear();
            if (cob_LeiXing.Text == "饼图")
            {
                seriesType = ViewType.Pie;
                string[] ValueDataMembers = new string[data.Columns.Count - 1];

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    //  var dts = data.Rows.AsEnumerable().Select(r => new { name = r["周期"], value = "" }).ToList();

                    string yBindName = data.Rows[i]["周期"].ToString();
                    //DataTable drTable = data.Rows[i].Table.Clone();
                    //drTable.Rows.Add(data.Rows[i].ItemArray);
                    List<RecordData> list = new List<RecordData>();
                    for (int j = 1; j < data.Columns.Count; j++)
                    {
                        string columnName = data.Columns[j].ColumnName;

                        list.Add(new RecordData() { name = columnName, value = Convert.ToInt32(string.IsNullOrEmpty(data.Rows[i][columnName].ToString()) ? "0" : data.Rows[i][columnName].ToString()) });
                    }
                    CreateSeries(ChartControl, yBindName, seriesType, list, "name", new string[] { "value" }, null);
                }
            }
            else
            {
                if (cob_LeiXing.Text == "折线图")
                    seriesType = ViewType.Line;
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    string yBindName = data.Columns[i].ColumnName;
                    CreateSeries(ChartControl, yBindName, seriesType, data, xBindName, new string[] { yBindName }, null);
                }
            }
        }
        /// <summary>
        /// 绘制图标
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="seriesName"></param>
        /// <param name="seriesType"></param>
        /// <param name="dataSource"></param>
        /// <param name="xBindName"></param>
        /// <param name="yBindName"></param>
        /// <param name="createSeriesRule"></param>
        public void CreateSeries(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string[] ValueDataMembers,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (ValueDataMembers == null)
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            //  _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(ValueDataMembers);

            _series.ShowInLegend = true;
            //_series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
            _series.Label.TextPattern = "{A}//{V}";
            //{A}表示argument   {V}表示数据值      {S}表示series
            //  series.Label.TextPattern = "{A}//{V}//{S}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);

        }
        #endregion

        private void btnExprt_Click(object sender, EventArgs e)
        {
            dgv.CustomExport("科室工作量统计");
        }
        #endregion

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            List<string> departmentIds = new List<string>();
            string[] arrIds = checkedComboBoxEditCharName.Properties.GetCheckedItems().ToString().Split(',');
            if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
            {
                foreach (string item in arrIds)
                {
                    departmentIds.Add(item.Replace(" ", ""));
                }
            }
            //else
            //{
            //    XtraMessageBox.Show("查询科室不能为空");
            //    return;
            //}
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                btnSearch.Enabled = false; //防止重复点击加载卡死现象
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {

                QueryClass query = new QueryClass();
                //科室
                query.BasicDictionaryType = departmentIds;

                if (searchLookUpEditClient.Text != null && searchLookUpEditClient.Text != string.Empty)
                {
                    ////单位
                    //List<Guid?> clientRegs = new List<Guid?>();
                    //string[] arrClientIds = txtClientRegID.Properties.GetCheckedItems().ToString().Split(',');
                    //if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
                    //{
                    //    foreach (string item in arrClientIds)
                    //    {
                    //        clientRegs.Add(new Guid(item));
                    //    }
                    //}
                    //Guid clientInfoId = new Guid(txtClientRegID.EditValue.ToString());
                    //单位信息
                    var value = (Guid)searchLookUpEditClient.EditValue ;
                    query.ClientInfoRegId = new List<Guid?>();
                  
                    query.ClientInfoRegId.Add(value);
                     
                }
               
                //时间

                if (dateEditStar.EditValue != null)
                {
                    query.LastModificationTimeBign = dateEditStar.DateTime.Date;
                    //query.LastModificationTimeBign = Convert.ToDateTime(dtpStart.EditValue.ToString());
                }

                if (dateEditEnd.EditValue != null)
                {
                    //query.LastModificationTimeEnd = Convert.ToDateTime(dtpEnd.EditValue.ToString());
                    query.LastModificationTimeEnd = dateEditEnd.DateTime.Date.AddDays(1);
                }
                if (!string.IsNullOrWhiteSpace(comDateType.EditValue?.ToString()) && comDateType.EditValue?.ToString() == "体检日期")
                {
                    query.DateType = 1;
                }

                gridControlCharName.DataSource = _proxy.DKSGZLStatistics(query);
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
                    btnSearch.Enabled = true;
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportToExcel("大科室检查人次", gridControlCharName);
        }

        private void checkedComboBoxEditCharName_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
              

            }
        }

        private void searchLookUpEditClient_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;


            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            List<int> departmentIds = new List<int>();
            string[] arrIds = comboBoxEditDepartmentType1.Properties.GetCheckedItems().ToString().Split(',');
            if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
            {
                foreach (string item in arrIds)
                {
                    var largedepart = int.Parse(item);
                    departmentIds.Add(largedepart);
                }
            }
            //else
            //{
            //    XtraMessageBox.Show("查询科室不能为空");
            //    return;
            //}
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                btnSearch.Enabled = false; //防止重复点击加载卡死现象
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {

                QueryClass query = new QueryClass();
                //科室
                query.LargeDepartmentBMList = departmentIds;

                if (searchLookUpEditclient1.Text != null && searchLookUpEditclient1.Text != string.Empty)
                {
                    ////单位
                    //List<Guid?> clientRegs = new List<Guid?>();
                    //string[] arrClientIds = txtClientRegID.Properties.GetCheckedItems().ToString().Split(',');
                    //if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
                    //{
                    //    foreach (string item in arrClientIds)
                    //    {
                    //        clientRegs.Add(new Guid(item));
                    //    }
                    //}
                    //Guid clientInfoId = new Guid(txtClientRegID.EditValue.ToString());
                    //单位信息
                    var value = searchLookUpEditclient1.EditValue as List<Guid>;
                    query.ClientInfoRegId = new List<Guid?>();
                    value.ForEach(v => {
                        query.ClientInfoRegId.Add(v);
                    });
                }
                //医生
                if (!string.IsNullOrWhiteSpace(searchLookUser.Text))
                {
                    query.DoctorName = searchLookUser.Text;
                }

                //时间

                if (dtpStart.EditValue != null)
                {
                    query.LastModificationTimeBign = dateEdit1.DateTime.Date;
                    //query.LastModificationTimeBign = Convert.ToDateTime(dtpStart.EditValue.ToString());
                }

                if (dtpEnd.EditValue != null)
                {
                    //query.LastModificationTimeEnd = Convert.ToDateTime(dtpEnd.EditValue.ToString());
                    query.LastModificationTimeEnd = dateEdit2.DateTime.Date.AddDays(1);
                }
                if (!string.IsNullOrWhiteSpace(comboBoxEdit2.EditValue?.ToString()) && comboBoxEdit2.EditValue?.ToString() == "体检日期")
                {
                    query.DateType = 1;
                }
                if (gridLookUpEdittype.EditValue != null)
                    query.CustomerType = (int)gridLookUpEdittype.EditValue;
                if (gridLookUpEditSex.EditValue != null)
                {
                    query.Sex = (int)gridLookUpEditSex.EditValue;
                }
                //检查医生还是审核医生
                if (radioGroup2.EditValue != null)
                {
                    query.EmpType = int.Parse(radioGroup2.EditValue.ToString());
                }

                gridControl1.DataSource = _proxy.BKSGZLStatistics(query);
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
                    btnSearch.Enabled = true;
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportToExcel("大科室工作量", gridControl1);
        }
    }
}