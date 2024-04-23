using Abp.Application.Services.Dto;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
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
    public partial class frmLqQR : UserBaseForm
    {
        private readonly ICustomerReportAppService customerReportAppService;
        private TbmCabinetDto tbmCabinetDto;
        public List<string> conNamelis = new List<string>();
        public string title = "";
        public List<Guid> LQlsit = new List<Guid>();
        public CusCabitLQDto dto;
        public frmLqQR()
        {
            customerReportAppService = new CustomerReportAppService();
            InitializeComponent();
        }

        private void frmLqQR_Load(object sender, EventArgs e)
        {
            lab1.Text = title;
            lab2.Text ="柜子编号：" +  string.Join(",", conNamelis);
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
                        if (conNamelis.Contains(rowName + conname))
                        {
                            row[conname] = "<bold><font  color=blue>" + rowName + conname + "</font></bold></br>";
                           
                        }
                        else
                        {
                            row[conname] = rowName + conname;
                        }

                    }
                    table.Rows.Add(row);

                }
                gridControl1.DataSource = table;
               // gridView1.SelectCell(gridView1.c ;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string allts = "";
            AutoLoading(() =>
            {
            foreach (var lqId in LQlsit)
            {
                
                var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 13);
                    if (ZjFormatd != null && !string.IsNullOrEmpty(ZjFormatd.Remarks) && ZjFormatd.Remarks=="1")
                    {
                        EntityDto<Guid> intput = new EntityDto<Guid>();
                        intput.Id = lqId;
                        var ts = customerReportAppService.IsSH(intput);
                        if (!string.IsNullOrEmpty(ts.Name))
                        {
                            allts += ts.Name + "\r\n";
                            continue;
                        }
                    }
                    CusCabitLQDto dto1 = new CusCabitLQDto();
                    dto1.Id = lqId;
                    dto1.Receiver = textEdit1.EditValue?.ToString();
                    dto1.Remarks = "";
                    dto1.GetState = 2;
                    dto = customerReportAppService.SaveTjlCabinetLQ(dto1);
             
            }
            });
            if (allts != "")
            {
                MessageBox.Show(allts);
            }
            DialogResult = DialogResult.OK;

        }
    }
}
