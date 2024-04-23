using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class CabinetSelection : UserBaseForm
    {
        private readonly ICustomerReportAppService customerReportAppService;
        private TbmCabinetDto tbmCabinetDto;
        private List<TjlCusCabitDto> CusCabitdto;


        public CabinetSelection()
        {
            InitializeComponent();
            customerReportAppService = new CustomerReportAppService();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CabinetSet frm = new CabinetSet();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {

            }
        }

        private void CabinetSelection_Load(object sender, EventArgs e)
        {
            gridView1.IndicatorWidth = 30;
            CusCabitdto = customerReportAppService.getTjlCabinet();
            upDate();
            

        }
        private void upDate()
        {
            int ord = 0;
            tbmCabinetDto = customerReportAppService.getTbmCabinet();
            if (tbmCabinetDto != null)
            {
                DataTable table = new DataTable();
                for (int con = 0; con < tbmCabinetDto.HCont; con++)
                {
                    string Name = "";
                    if (tbmCabinetDto.HCont == (int)CabinetSate.YWfomat)
                    {
                        int A = 65;
                        Name = ((char)(A + con)).ToString();
                    }
                    else
                    {
                        Name = (con + 1).ToString();
                    }


                    var Columns = new DevExpress.XtraGrid.Columns.GridColumn();                                 

                    RepositoryItemRichTextEdit repositoryItemCalcEdit_je = new RepositoryItemRichTextEdit();
                    repositoryItemCalcEdit_je.DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat.Html;
                    repositoryItemCalcEdit_je.Name = Name;
                    repositoryItemCalcEdit_je.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;

                    Columns.ColumnEdit = repositoryItemCalcEdit_je;
                    Columns.ColumnEdit.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
                    
                    Columns.Name = Name;
                    Columns.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    Columns.FieldName = Name;
                    Columns.Caption = Name;
                    Columns.VisibleIndex = ord;
                    Columns.MaxWidth = 200;
                    Columns.MinWidth = 100;        
                    gridView1.Columns.Add(Columns);
                    table.Columns.Add(Name);
                    ord++;
                }

                table.Columns.Add("n1");

                for (int i = 0; i < tbmCabinetDto.WCont; i++)
                {
                    DataRow row = table.NewRow();
                    string rowName = (i + 1).ToString();
                    if (tbmCabinetDto.WType == (int)CabinetSate.YWfomat)
                    {
                        int A = 65;
                        rowName = ((char)(A + i)).ToString();
                    }

                    DataRowView rowAdd = (DataRowView)gridView1.GetFocusedRow();
                    for (int con = 0; con < gridView1.Columns.Count; con++)
                    {
                        string conname = gridView1.Columns[con].Name;
                        string catName = rowName + conname;
                        int grcont = CusCabitdto.Where(o => o.CabitName == catName && o.GetState == 1 && o.ReportState == 1).Count();
                        int ttcont = CusCabitdto.Where(o => o.CabitName == catName && o.GetState == 1 && o.ReportState == 2).Count();
                        string GSName = "";
                        string HeardName1 = "<bold><font  size=5  color=blue>" + rowName + conname + "</font></bold></br>";
                        if (grcont > 0)
                        {
                            GSName += "<bold> <font size=2 color=gray>个报个数：</font><font size=2 color=green>" + grcont + "</font></bold></br>";
                        }
                        if (ttcont > 0)
                        {
                            GSName += " <bold><font size=2 color=gray>团报个数:</font><font size=2 color=green>" + ttcont + "</font></bold>";
                        }
                        if (GSName != "")
                        {
                            row[conname] = HeardName1 + GSName;
                        }
                        else
                        {
                            row[conname] = rowName + conname;
                        }
                    }
                    table.Rows.Add(row);

                }
                gridControl1.DataSource = table;
            }
        }
        private class Connames
        {
            public virtual string ConName { get; set; }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (tbmCabinetDto != null)
            {
                if (e.Info.IsRowIndicator && e.RowHandle > -1)
                {
                    if (tbmCabinetDto.WType == (int)CabinetSate.YWfomat)
                    {
                        int A = 65;
                        e.Info.DisplayText =  ((char)(A + e.RowHandle)).ToString();
                    }
                    else
                    {
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                }
            }
        }

        private void richEditControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
