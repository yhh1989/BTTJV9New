using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReSultStatus;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static Sw.Hospital.HealthExaminationSystem.CustomerReport.PrintPreview;
using static Sw.Hospital.HealthExaminationSystem.Statistics.Charge.FinancialStatement;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusReSultStatus
{
    public partial class ReSultStstis : UserBaseForm
    {
        ICusReSultStatusAppService _cusReSultStatusApp = new CusReSultStatusAppService();
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }

        private List<GridColumn> OldColumns = new List<GridColumn>();
        private readonly ICommonAppService _commonAppService;
        private List<string> connamels = new List<string>();
        private List<ReSultSetDto> reSultSetDtos = new List<ReSultSetDto>();
        public ReSultStstis()
        {
            InitializeComponent();
            CustomColumns = new Dictionary<string, CustomColumnValue>();
            _commonAppService = new CommonAppService();
            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
        }

        private void ReSultSet_Load(object sender, EventArgs e)
        {
            var dataDto = _commonAppService.GetDateTimeNow();
            dateEditStartTime.DateTime = dataDto.Now.Date.AddDays(-1);
            dateEditEndTime.DateTime = dataDto.Now.Date;

            var ClientNames = DefinedCacheHelper.GetClientRegNameComDto();
            sleDW.Properties.DataSource = ClientNames;
            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();
            lookUpEditSumStatus.EditValue = (int)SummSate.NotAlwaysCheck;
            // 加载体检类别数据
            var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            BasicDictionaryDto all = new BasicDictionaryDto();
            //登记状态
            lUpDJState.Properties.DataSource = RegisterStateHelper.GetSelectList();
            //lUpDJState.ItemIndex = 1;
            // 加载体检状态数据
            lookUpEditExaminationStatus.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            lookUpEditExaminationStatus.EditValue = (int)ExaminationState.Whole;
            all.Value = -1;
            all.Text = "全部";
            Examination.Add(all);
            lookUpEditExaminationCategories.Properties.DataSource = Examination;
           // Reload();
            gridView2.OptionsView.RowAutoHeight = true;
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Reload(List<ReSultCusInfoDto> input)
        {
            AutoLoading(() =>
            {
                //var hasDepatIds = input.SelectMany(p=>p.ReSultCusDeparts).Select(o=>o.DepartId).Distinct().ToList();
                //var hasItemGroupIds = input.SelectMany(p => p.ReSultCusGrous).Select(o => o.ItemGroupId).Distinct().ToList();
                //var hasItemIds = input.SelectMany(p => p.ReSultCusItems).Select(o => o.ItemInfoId).Distinct().ToList();
                //删除列
                if (CustomColumns!=null && CustomColumns.Count>0)
                {
                    foreach (var con in OldColumns)
                    {
                        if (gridView2.Columns.Contains(con))
                        {
                            gridView2.Columns.Remove(con);
                        }
                    }
                    OldColumns.Clear();
                }
                var list = _cusReSultStatusApp.GetReSultDepart();
              
                foreach (var tr in list)
                {
                    if (tr.isAdVice == true)
                    {
                        this.gridView2.Columns["ReSultCusSums"].Visible = true;
                    }
                    else
                    {
                        this.gridView2.Columns["ReSultCusSums"].Visible = false;
                    }
                    if (tr.isOccupational == true)
                    {
                        this.gridView2.Columns["ReSultCusOccSum"].Visible = true;
                    }
                    else
                    {
                        this.gridView2.Columns["ReSultCusOccSum"].Visible = false;
                    }
                }
                //var de = DefinedCacheHelper.GetDepartments();
                //var depatnamel = list.SelectMany(o => o.Departs).Select(o => o.DepartmentId).Distinct().ToList();
                //depatnamel = depatnamel.Where(p => hasDepatIds.Contains(p)).ToList();
                //List<string> sa = new List<string>();
                //foreach (var s in depatnamel)
                //{
                //    var ssa = de.Where(o => o.Id == s).Select(i => i.Name).ToList();
                //    sa.AddRange(ssa);
                //}

                var sa = input.SelectMany(p => p.ReSultCusDeparts).Select(o =>new { o.DepartName,o.DepartOrder}).OrderBy(o=>o.DepartOrder).Distinct().Select(p=>p.DepartName).ToList();
                var itemgroup = input.SelectMany(p => p.ReSultCusGrous).Select(o =>new { o.GroupName, o.DepartOrder, o.GroupOrder }).OrderBy(o=>o.DepartOrder).ThenBy(p=>p.GroupOrder)
                .Distinct().Select(p=>p.GroupName).ToList();
                var itemlist = input.SelectMany(p => p.ReSultCusItems).Select(o => new { o.ItemName, o.DepartOrder, o.GroupOrder, o.ItemOrder }).OrderBy(o => o.DepartOrder).ThenBy(p => p.GroupOrder)
                .ThenBy(p => p.ItemOrder).Distinct().ToList();
                var itemname = itemlist.Select(p=>p.ItemName).ToList();

                foreach (var depar in sa)
                {
                    var column = new GridColumn();
                    
                    column.FieldName = $"gridColumnCustom{depar}";
                    column.Name = $"gridColumnCustom{depar}";
                    column.Caption = depar;
                 
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    column.ColumnEdit = memoEdit;
                    if (!CustomColumns.Any(o => o.Key == column.Name))
                    {
                        var customColumn = new CustomColumnValue { Text = column.Caption };
                        CustomColumns.Add(column.Name, customColumn);
                    }
                    column.Visible = true;
                    //column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                    column.SummaryItem.FieldName = "Name";
                    column.SummaryItem.DisplayFormat = "{0:c}";
                    column.SummaryItem.Tag = column.Name;
                   // column.Width = 100; 
                    gridView2.Columns.Add(column);
                    OldColumns.Add(column);
                }
                //var it = DefinedCacheHelper.GetItemGroups();
                //var depatnamel1 = list.SelectMany(o => o.Groups).Select(o => o.ItemGroupId).Distinct().ToList();
                //depatnamel1 = depatnamel1.Where(p=>hasItemGroupIds.Contains(p)).ToList();
                //List<string> itemgroup = new List<string>();
                //foreach (var s in depatnamel1)
                //{
                //    var itemgroupname = it.Where(o => o.Id == s).Select(i => i.ItemGroupName).ToList();
                //    itemgroup.AddRange(itemgroupname);
                //}
                foreach (var depar in itemgroup)
                {
                    var column = new GridColumn();
                    column.FieldName = $"gridColumnCustom{depar}";
                    column.Name = $"gridColumnCustom{depar}";
                    column.Caption = depar;
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    column.ColumnEdit = memoEdit;

                    if (!CustomColumns.Any(o => o.Key == column.Name))
                    {
                        var customColumn = new CustomColumnValue { Text = column.Caption };
                        CustomColumns.Add(column.Name, customColumn);
                    }
                    column.Visible = true;
                    //column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                    column.SummaryItem.FieldName = "Name";
                    column.SummaryItem.DisplayFormat = "{0:c}";
                    column.SummaryItem.Tag = column.Name;
                    column.Width = 100;
                   
                    gridView2.Columns.Add(column);
                    OldColumns.Add(column);
                }
                //var item = DefinedCacheHelper.GetItemInfos();
                //var depatname = list.SelectMany(o => o.Items).Select(o => o.ItemInfoId).Distinct().ToList();
                //depatname = depatname.Where(p=>hasItemIds.Contains(p)).ToList();
                //List<string> itemname = new List<string>();
                //foreach (var s in depatname)
                //{
                //    var itemna = item.Where(o => o.Id == s).Select(i => i.Name).ToList();
                //    itemname.AddRange(itemna);
                //}

                foreach (var depar in itemname)
                {
                    var column = new GridColumn();
                    column.FieldName = $"gridColumnCustom{depar}";
                    column.Name = $"gridColumnCustom{depar}";
                    column.Caption = depar;
                    if (!CustomColumns.Any(o => o.Key == column.Name))
                    {
                        var customColumn = new CustomColumnValue { Text = column.Caption };
                        CustomColumns.Add(column.Name, customColumn);
                    }
                    column.Visible = true;
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    column.ColumnEdit = memoEdit;
                    //column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                    column.SummaryItem.FieldName = "Name";
                    column.SummaryItem.DisplayFormat = "{0:c}";
                    column.SummaryItem.Tag = column.Name;
                    column.Width = 100;
                    
                    gridView2.Columns.Add(column);
                    OldColumns.Add(column);
                }

                //foreach (var tr in list)
                //{
                //    if (tr.isAdVice == true)
                //    {
                //        List<string> advice = new List<string>();
                //        advice.Add("总检建议");

                //        foreach (var depar in advice)
                //        {
                //            var column = new GridColumn();
                //            column.FieldName = $"gridColumnCustom{depar}";
                //            column.Name = $"gridColumnCustom{depar}";
                //            column.Caption = depar;
                //            var customColumn = new CustomColumnValue { Text = column.Caption };
                //            CustomColumns.Add(column.Name, customColumn);

                //            column.Visible = true;
                //            column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                //            column.SummaryItem.FieldName = "Name";
                //            column.SummaryItem.DisplayFormat = "{0:c}";
                //            column.SummaryItem.Tag = column.Name;
                //            gridView2.Columns.Add(column);
                //        }
                //    }
                //}
                //foreach (var tclou in list)
                //{
                //    if (tclou.isOccupational == true)
                //    {
                //        List<string> occupation = new List<string>();
                //        occupation.Add("职业健康结论");

                //        foreach (var depar in occupation)
                //        {
                //            var column = new GridColumn();
                //            column.FieldName = $"gridColumnCustom{depar}";
                //            column.Name = $"gridColumnCustom{depar}";
                //            column.Caption = depar;
                //            var customColumn = new CustomColumnValue { Text = column.Caption };
                //            CustomColumns.Add(column.Name, customColumn);

                //            column.Visible = true;
                //            column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                //            column.SummaryItem.FieldName = "Name";
                //            column.SummaryItem.DisplayFormat = "{0:c}";
                //            column.SummaryItem.Tag = column.Name;
                //            gridView2.Columns.Add(column);
                //        }
                //    }
                //}
            });

        }
       
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            var ReSultStatus = _cusReSultStatusApp.GetReSultDepart();
            if (ReSultStatus.Count > 0)
            {
                var reDepart = ReSultStatus.First();
                if (reDepart.isOccupational == true)
                {
                    conWorkName.Visible = true;
                    conTypeWork.Visible = true;
                    conPostState.Visible = true;
                    conRiskS.Visible = true;
                    conInjuryAge.Visible = true;

                }
                else
                {
                    conWorkName.Visible = false;
                    conTypeWork.Visible = false;
                    conPostState.Visible = false;
                    conRiskS.Visible = false;
                    conInjuryAge.Visible = false;
                }
            }
            ShowResultSetDto show = new ShowResultSetDto();
            if (sleDW.EditValue != null)
            {
                show.ClientrRegID = (Guid)sleDW.EditValue;
            }
            if (comTimeType.Text.Contains("登记"))
            {
                show.TimeType = 1;
            }
            else
            {
                show.TimeType = 2;
            }
            if (dateEditStartTime.EditValue != null)
                show.StartDataTime = dateEditStartTime.DateTime;
            if (dateEditEndTime.EditValue != null)
                show.EndDataTime = dateEditEndTime.DateTime.AddDays(1);
            if (show.StartDataTime > show.EndDataTime)
            {
                ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                return;
            }
            var examinationCategory = lookUpEditExaminationCategories.EditValue as int?;
            if (examinationCategory.HasValue && examinationCategory != -1)
            {
                show.PhysicalType = examinationCategory;
            }
            if (lookUpEditSumStatus.EditValue != null && lookUpEditSumStatus.Text != "全部")
            {
                show.SummSate = (int)lookUpEditSumStatus.EditValue;
            }
            if (lookUpEditExaminationStatus.EditValue != null && lookUpEditExaminationStatus.Text != "全部")
            {
                show.CheckSate = (int)lookUpEditExaminationStatus.EditValue;
            }
            if (lUpDJState.EditValue != null && lUpDJState.Text != "全部")
            {
                show.RegisterState = (int)lUpDJState.EditValue;
            }
            var result = _cusReSultStatusApp.getCusResult(show);
            Reload(result);
            gridControl.DataSource = result;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (var frm = new ReSultSet())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                   // Reload();
                    gridView2.OptionsView.RowAutoHeight = true;
                    gridView2.BestFitColumns();
                    gridControl.RefreshDataSource();
                    gridControl.Refresh();
                }
            }
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (gridControl.DataSource is List<ReSultCusInfoDto> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {
                        var sum = rows[e.ListSourceRowIndex].ReSultCusDeparts
                            ?.Where(r => r.DepartName == CustomColumns[e.Column.Name].Text)
                            .Select(r => r.DepartSum).ToList();
                        foreach (var d in sum)
                        {
                            if (d != null)
                            {
                                e.DisplayText = d.ToString();
                            }
                        }
                        var sum1 = rows[e.ListSourceRowIndex].ReSultCusGrous
                            ?.Where(r => r.GroupName == CustomColumns[e.Column.Name].Text)
                            .Select(r => r.GroupSum).ToList();
                        foreach (var g in sum1)
                        {
                            if (g != null)
                            {
                                e.DisplayText = g.ToString();
                            }
                        }
                        var sum2 = rows[e.ListSourceRowIndex].ReSultCusItems
                            ?.Where(r => r.ItemName == CustomColumns[e.Column.Name].Text)
                            .Select(r => r.ItemResultChar).ToList();
                        foreach (var i in sum2)
                        {
                            if (i != null)
                            {
                                e.DisplayText = i.ToString();
                            }

                        }
                        //if (rows[e.ListSourceRowIndex].ReSultCusSums != null)
                        //{
                        //    var sum3 = rows[e.ListSourceRowIndex].ReSultCusSums.ToString();
                        //    e.DisplayText = sum3.ToString();
                        //}
                        //if (rows[e.ListSourceRowIndex]?.ReSultCusOccSum != null)
                        //{
                        //    var sum4 = rows[e.ListSourceRowIndex].ReSultCusOccSum.ToString();
                        //    e.DisplayText = sum4.ToString();
                        //}

                    }
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           ExcelHelper.GridViewToExcel(gridView2, "体检人结果", "体检人结果");

            //DataTable data = new DataTable();
            //for (int i=0; i<gridView2.Columns.Count;i++)
            //{if (!data.Columns.Contains(gridView2.Columns[i].FieldName))
            //    {
            //        DataColumn dataColumn = new DataColumn();
            //        dataColumn.ColumnName = gridView2.Columns[i].FieldName;
            //        data.Columns.Add(dataColumn);
            //    }
            //}
            //for (int i = 0; i < gridView2.RowCount; i++)
            //{
            //    DataRow dataRow = data.NewRow();
            //    for (int n= 0; n < gridView2.Columns.Count; n++)
            //    {
            //        var ss = "";
            //        if (gridView2.Columns[n].FieldName == "PhysicalType")
            //        { ss = gridView2.GetRowCellValue(i, gridView2.Columns[n].FieldName)?.ToString(); }
            //        else
            //        {
            //             ss = gridView2.GetRowCellDisplayText(i, gridView2.Columns[n].FieldName)?.ToString();
            //        }
            //        dataRow[gridView2.Columns[n].FieldName] = ss;
            //    }
            //    data.Rows.Add(dataRow);
            //}


            //var saveFileDialog = new SaveFileDialog();
            //saveFileDialog.FileName = "体检人结果";
            //saveFileDialog.DefaultExt = "xls";
            //saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            //if (saveFileDialog.ShowDialog() != DialogResult.OK
            //) //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
            //    return;
            //var FileName = saveFileDialog.FileName;
            //gridControl.DataSource = data;
            //gridControl.ExportToXlsx(FileName);


            // gridControl.ExportToXls(FileName);

            //ExcelHelper excle = new ExcelHelper(saveFileDialog.FileName);
            //excle.DataTableToExcel(data, "体检人结果", true);
        }
    
        private void sleDW_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            sleDW.EditValue = null;
        }

        private void lookUpEditExaminationStatus_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            lookUpEditExaminationStatus.EditValue = null;
        }

        private void lookUpEditSumStatus_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            lookUpEditSumStatus.EditValue = null;
        }
    }
}
