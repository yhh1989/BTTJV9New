using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Suggest
{
    public partial class frmSumConflictList : UserBaseForm
    {
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;

        public frmSumConflictList()
        {
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            InitializeComponent();
        }

        private void frmSumConflictList_Load(object sender, EventArgs e)
        {
            gridView1.Columns[conSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            ChargeBM chargeBM = new ChargeBM();
            if (!string.IsNullOrEmpty(textEditWord.EditValue?.ToString()))
            {
                chargeBM.Name = textEditWord.EditValue?.ToString();
            }
            var sumconlist = _summarizeAdviceAppService.SearchSumConflict(chargeBM);
            gridControl1.DataSource = sumconlist;


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmSumConflict frmSumConflict = new frmSumConflict();
            frmSumConflict.ShowDialog();
            butSearch.PerformClick();

        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            //var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, conId);
            var id = gridView1.GetFocusedRowCellValue(conId);
             var dto = gridView1.GetFocusedRow() as  SumConflictDto;
            using (var frm = new frmSumConflict((Guid)id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //  simpleButtonQuery.PerformClick();
                    ModelHelper.CustomMapTo(frm._Model, dto);
                    gridControl1.RefreshDataSource();
                }
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, conId);
            if (id != null)
            {
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Id = (Guid)id;
                _summarizeAdviceAppService.DelSumConflict(chargeBM);
            }
        }
    }
}
