using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class frmTS : UserBaseForm
    {
        public string cusreginfo;
        public int selectindex=0;
        public frmTS()
        {
            InitializeComponent();

        }


        private void frmTS_Load(object sender, EventArgs e)
        {
            richInfo.Text = cusreginfo;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            selectindex = 2;
            DialogResult = DialogResult.OK;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            selectindex = 1;
            DialogResult = DialogResult.OK;


        }

        private void butClose_Click(object sender, EventArgs e)
        {
            selectindex = 0;
            this.Close();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            selectindex = 2;
            DialogResult = DialogResult.OK;
        }
    }
}
