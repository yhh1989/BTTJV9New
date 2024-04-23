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
    public partial class frmSFTS : UserBaseForm
    {
        public bool isYLFH = false;

        public frmSFTS()
        {
            InitializeComponent();
        }

        private void frmSFTS_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            isYLFH = true;
            this.DialogResult = DialogResult.OK;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            isYLFH = false;
            this.DialogResult = DialogResult.OK;
        }
    }
}
