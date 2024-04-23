using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccDayStatic;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic
{
    public partial class OccDayStaticList : UserBaseForm
    {
        private readonly IOccDayStaticAppService _OccDayStaticAppService;     
        private List<GridColumn> gridColumns = new List<GridColumn>();
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }
        public OccDayStaticList()
        {
            InitializeComponent();
            _OccDayStaticAppService = new OccDayStaticAppService();
        }

        private void OccDayStaticList_Load(object sender, EventArgs e)
        {
            CustomColumns = new Dictionary<string, CustomColumnValue>();                   
            ToYearStyle(dtTime);
          
            textEditClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }      
        private void GetConNames()
        {
            foreach (var con in gridColumns)
            {
                gridView1.Columns.Remove(con);
            }
            gridColumns.Clear();
            CustomColumns.Clear();
            int ConCount = 0;
            string unit = "";
            //选择月加载天数
            if (!string.IsNullOrWhiteSpace(monthEdit1.EditValue?.ToString()))
            {
                var mouth = int.Parse(monthEdit1.EditValue?.ToString());
                var year = dtTime.DateTime.Year;
                ConCount = DateTime.DaysInMonth(year, mouth);

            }
            //选择年加载月份
            else
            {
               
                ConCount = 12;
                unit = "月";
            }

           // var ss = gridView1.Columns.Count;
            for (int i = 1; i <= ConCount; i++)
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

        /// <summary>
        /// 图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var result = gridControl1.DataSource as List<outresut>;
            if (result != null && result.Count > 0)
            {            

                var frm = new frmOccDayStatic(result, gridColumns.Count);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //OutOccDayDto show = new OutOccDayDto();
            if (string.IsNullOrWhiteSpace(dtTime.EditValue?.ToString()))
            {
                MessageBox.Show("请选择年份！");
                return;
            }
            //if (!string.IsNullOrWhiteSpace(textEditClient.Text.Trim()))
            //{
            //    show.ClientName = textEditClient.Text.Trim();
            //}
            //if (dtTime.EditValue != null)
            //    show.YearTime = dtTime.Text.Trim();

            //var data = _OccDayStaticAppService.GetOutOccDays(show);
            //gridControl1.DataSource = data;
            GetConNames();
            INOccMothDto iNOccMothDto = new INOccMothDto();
            if(!string.IsNullOrWhiteSpace( textEditClient.EditValue?.ToString()))
            {
                iNOccMothDto.ClienRegId = (Guid)textEditClient.EditValue;
            }
            iNOccMothDto.YearTime = (int)dtTime.DateTime.Year;
            if (!string.IsNullOrWhiteSpace(monthEdit1.EditValue?.ToString()))
            {
                iNOccMothDto.MothTime = (int)monthEdit1.EditValue;
            }

          var mothlist=  _OccDayStaticAppService.GetOutOccMothDays(iNOccMothDto);
            var grouplist = mothlist.GroupBy(o =>new { o.ClientName }).Select(p=>new outresut {
             ClientName=p.Key.ClientName,
              outOccMothDtos=p.Select(n=> new OutOccMothDto {
                   ClientName=n.ClientName,
               ConName=n.ConName,
               ConCount=n.ConCount}).ToList()
             }).ToList();

            gridControl1.DataSource = grouplist;
        }       
        /// <summary>
        /// 显示列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        public class CustomColumnValue
        {
            public string Text { get; set; }

            public decimal Sum { get; set; }
        }

        private void monthEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;                

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ExcelHelper.GridViewToExcel(gridView1, "职业健康月统计", "职业健康月统计");
        }
    }
    public class outresut
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }

        public List<OutOccMothDto> outOccMothDtos { get; set; }
    }

   
}
