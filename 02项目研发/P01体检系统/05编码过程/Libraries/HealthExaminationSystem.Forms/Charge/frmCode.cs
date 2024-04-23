using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class frmCode : UserBaseForm
    {
        public string AuCode = "";
        public frmCode()
        {
            InitializeComponent();
        }

        private void frmCode_Load(object sender, EventArgs e)
        {
            textEditCode.Focus();
        }

        private void frmCode_Click(object sender, EventArgs e)
        {
            textEditCode.Focus();
        }

        private void textEditCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textEditCode.Text))
            {
                AuCode = textEditCode.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
