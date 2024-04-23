using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.FrmMakeCollect;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Data;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusRegStatis
{
    public partial class FrmMakeCollect : UserBaseForm
    {
        private readonly IFrmMakeCollectAppService _ifrmmakecollectappservice;
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }
        public FrmMakeCollect()
        {
            InitializeComponent();
            _ifrmmakecollectappservice = new FrmMakeCollectAppService();
            CustomColumns = new Dictionary<string, CustomColumnValue>();
        }

        private void FrmMakeCollect_Load(object sender, EventArgs e)
        {
            //var list = DefinedCacheHelper.GetDepartments();
            ShowMakeCollectDto show1 = new ShowMakeCollectDto();
            var list = _ifrmmakecollectappservice.GetShowMakeCollects(show1);
            var depatnamels = list.SelectMany(o => o.departlist).Select(o => o.departname).Distinct().ToList();
            int num = 0;
            foreach (var depar in depatnamels)
            {
                var column = new GridColumn();
                column.FieldName = $"gridColumnCustom{depar}";
                column.Name = $"gridColumnCustom{depar}";
                column.Caption = depar;
                var customColumn = new CustomColumnValue { Text = column.Caption };
                CustomColumns.Add(column.Name, customColumn);

                column.Visible = true;
                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                column.SummaryItem.FieldName = "BookingDate";
                column.SummaryItem.DisplayFormat = "{0:c}";
                column.SummaryItem.Tag = column.Name;
                //gridView1.Columns.Add(column);
                gridView1.Columns.Insert(num, column);
               
                num = num + 1;
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (gridControl1.DataSource is List<ShowMakeCollectDto> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {
                        var sum = rows[e.ListSourceRowIndex].departlist
                            ?.Where(r => r.departname == CustomColumns[e.Column.Name].Text)
                            .Sum(r => r.count);
                        e.DisplayText = sum?.ToString();
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ShowMakeCollectDto show1 = new ShowMakeCollectDto();
            if (dateEditStartTime.EditValue != null)
            {
                show1.StartDataTime = (DateTime)dateEditStartTime.EditValue;
            }
            if (dateEditEndTime.EditValue != null)
            {
                show1.EndDataTime = (DateTime)dateEditEndTime.EditValue;
            }
            var list = _ifrmmakecollectappservice.GetShowMakeCollects(show1);

            
            gridControl1.DataSource = list;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "预约汇总统计";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            var ds = gridControl1.DataSource;
            gridControl1.DataSource = ds;
            gridControl1.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
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
                                    CustomColumns[tag].count = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.Row is ShowMakeCollectDto row)
                                    {
                                        var sum = row.departlist
                                            ?.Where(r => r.departname == CustomColumns[tag].Text)
                                            .Sum(r => r.count);
                                        CustomColumns[tag].count = CustomColumns[tag].count + sum ?? 0;
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = CustomColumns[tag].count;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var id = gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "BookingDate").ToString();
            if (id != "")
            {
                frmCusRegStatiscs ss = new frmCusRegStatiscs();

                ss.olde = DateTime.Parse(id);
                ss.ShowDialog();
            }

        }
    }
    public class CustomColumnValue
    {
        public string Text { get; set; }

        public decimal count { get; set; }
    }
}
