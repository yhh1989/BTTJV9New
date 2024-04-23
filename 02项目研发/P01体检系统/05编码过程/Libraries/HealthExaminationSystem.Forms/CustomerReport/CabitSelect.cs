using DevExpress.XtraEditors.Repository;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
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
    public partial class CabitSelect : UserBaseForm
    {
        private readonly ICustomerReportAppService customerReportAppService;
        private TbmCabinetDto tbmCabinetDto;
        private List<TjlCusCabitDto> CusCabitdto;
        public string conName = "";
        public CabitSelect()
        {
            customerReportAppService = new CustomerReportAppService();
            InitializeComponent();
        }

        private void CabitSelect_Load(object sender, EventArgs e)
        {
            gridView1.IndicatorWidth = 30;
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
                        
                         row[conname] = rowName + conname;
                        
                    }
                    table.Rows.Add(row);

                }
                gridControl1.DataSource = table;
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            conName = gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString();
            DialogResult = DialogResult.OK;

        }
    }
}
