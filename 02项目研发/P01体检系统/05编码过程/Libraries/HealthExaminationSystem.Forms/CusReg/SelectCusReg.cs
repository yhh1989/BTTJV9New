using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class SelectCusReg : UserBaseForm
    {
        public List<QueryCustomerRegDto> Datas;
        public event Action<QueryCustomerRegDto> SelectData;
        public SelectCusReg()
        {
            InitializeComponent();
        }

        private void SelectCusReg_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetDepartments();         
            gridControl1.DataSource = Datas;
             
            gridView1.Columns[gridConReviewSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[gridConReviewSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(ReviewSateTypeHelper.ReviewFormatter);

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var data = gridView1.GetFocusedRow() as QueryCustomerRegDto;
            SelectData?.Invoke(data);
            DialogResult = DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.FieldName == colSex.FieldName)
            {
                e.DisplayText= EnumHelper.GetEnumDesc((Sex)e.Value);
            }
        }
    }
}