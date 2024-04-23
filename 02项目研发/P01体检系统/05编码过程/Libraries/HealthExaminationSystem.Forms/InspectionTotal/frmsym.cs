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

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class frmsym : UserBaseForm
    {
        public string sym;
        public frmsym()
        {
            InitializeComponent();
        }

        private void frmsym_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sym = "H";
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            sym = "L";
            this.DialogResult = DialogResult.OK;
        }
    }
}
