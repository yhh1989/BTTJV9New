using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class FrmSumHide : UserBaseForm
    {
        public string category = "";
        public string categoryInt = "";
        public string sum = "";
        public int IsNormal = 0;
        public FrmSumHide()
        {
            InitializeComponent();
        }

        private void FrmSumHide_Load(object sender, EventArgs e)
        {
            Labcategory.Text = category;
            simpleButton2.Text = category;
            labSum.Text = sum;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            categoryInt = "";            
            DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
