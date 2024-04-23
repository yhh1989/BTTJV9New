using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionYear;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic.OccDayStaticList;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using DevExpress.Data;
using DevExpress.XtraGrid;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionStatistics.OccConclusionYear;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionYear
{
    public partial class OccConclusionYear : UserBaseForm
    {
        private readonly IOccConclusionYearAppService _OccConclusionYearAppservice;
        private List<GridColumn> gridColumns = new List<GridColumn>();
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }
        public OccConclusionYear()
        {
            InitializeComponent();
            _OccConclusionYearAppservice = new OccConclusionYearAppService();
        }

        private void OccConclusionYear_Load(object sender, EventArgs e)
        {
            CustomColumns = new Dictionary<string, CustomColumnValue>();
            ToYearStyle(Startdate);
            ToYearStyle(Enddate);
            txtClientName.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            //var results = _OccConclusionYearAppservice.GetOccAbnormalResult(occYear);
            //gridControl1.DataSource = results;
        }
        private void GetConNames()
        {
            foreach (var con in gridColumns)
            {
                gridView1.Columns.Remove(con);
            }
            gridColumns.Clear();
            CustomColumns.Clear();           
            string unit = "";
            //选择月加载天数
            var starY = Startdate.DateTime.Year;
            var endY = Enddate.DateTime.Year;

            // var ss = gridView1.Columns.Count;
            for (int i = starY; i <= endY; i++)
            {
                var Columns = new DevExpress.XtraGrid.Columns.GridColumn();
                Columns.Name = i.ToString();
                Columns.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                Columns.Caption = i.ToString() + unit;
                Columns.FieldName = i.ToString();
                Columns.VisibleIndex = i;
                Columns.MaxWidth = 100;
                Columns.MinWidth = 100;
                Columns.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();

                var customColumn = new CustomColumnValue { Text = Columns.Name };
                CustomColumns.Add(Columns.Name, customColumn);
                gridColumns.Add(Columns);


                Columns.Visible = true;
                Columns.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                Columns.SummaryItem.FieldName = "ClientName";
                Columns.SummaryItem.DisplayFormat = "{0:c}";
                Columns.SummaryItem.Tag = Columns.Name;
                gridView1.Columns.Add(Columns);

            }
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Startdate.EditValue?.ToString()))
            {
                MessageBox.Show("请选择开始时间！");
                return;
            }
            if (string.IsNullOrWhiteSpace(Enddate.EditValue?.ToString()))
            {
                MessageBox.Show("请选择结束时间！");
                return;
            }
            GetConNames();
            OccConclusionYearShowDto iNOccMothDto = new OccConclusionYearShowDto();
            if (!string.IsNullOrWhiteSpace(txtClientName.EditValue?.ToString()))
            {
                iNOccMothDto.ClientregID = (Guid)txtClientName.EditValue;
            }
            if (!string.IsNullOrWhiteSpace(Startdate.EditValue?.ToString()))
            {
                iNOccMothDto.StartCheckDate = Startdate.DateTime.Year;
            }
            if (!string.IsNullOrWhiteSpace(Enddate.EditValue?.ToString()))
            {
                iNOccMothDto.EndCheckDate = Enddate.DateTime.Year;
            }

            var mothlist = _OccConclusionYearAppservice.GetOccAbnormalResult(iNOccMothDto);
            var grouplist = mothlist.GroupBy(o => new { o.ClientName }).Select(p => new outresut
            {
                ClientName = p.Key.ClientName,
                outOccMothDtos = p.Select(n => new OutOccMothDto
                {
                    ClientName = n.ClientName,
                    ConName = n.ConName,
                    ConCount = n.ConCount
                }).ToList()
            }).ToList();

            gridControl1.DataSource = grouplist;
        }
        void ToYearStyle(DevExpress.XtraEditors.DateEdit dateEdit, bool touchUI = false)
        {
            dateEdit.Properties.Mask.EditMask = "yyyy";
            if (touchUI)
            {
                dateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            }
            else
                dateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dateEdit.Properties.ShowToday = false;
            dateEdit.Properties.ShowMonthHeaders = false;
            dateEdit.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
            dateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;

            dateEdit.Properties.Mask.UseMaskAsDisplayFormat = false;

            dateEdit.Properties.DisplayFormat.FormatString = "yyyy";
            dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.EditFormat.FormatString = "yyyy";
            dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;


        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (gridControl1.DataSource is List<outresut> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {
                        var ss = CustomColumns[e.Column.Name].Text;
                        var convalue = rows[e.ListSourceRowIndex].outOccMothDtos
                            ?.FirstOrDefault(r => r.ConName.ToString() == CustomColumns[e.Column.Name].Text);
                        e.DisplayText = convalue?.ConCount?.ToString();
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var result = gridControl1.DataSource as List<outresut>;
            if (result != null && result.Count > 0)
            {

                var frm = new frmOccConclusionYearChart(result, gridColumns);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }

        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                if (e.Item is GridColumnSummaryItem gridColumnSummaryItem)
                {
                    if (gridColumnSummaryItem.Tag is string tag)
                    {
                        if (CustomColumns.ContainsKey(tag))
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    CustomColumns[tag].Sum = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.Row is outresut row)
                                    {
                                        var sum = row.outOccMothDtos
                                            ?.Where(r => r.ConName.ToString() == CustomColumns[tag].Text)
                                            .Sum(r => r.ConCount);
                                        CustomColumns[tag].Sum = CustomColumns[tag].Sum + sum ?? 0;
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = CustomColumns[tag].Sum;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
             ExcelHelper.GridViewToExcel(gridView1, "职业健康年统计", "职业健康年统计");
        }

        
    }
}
