using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.OutInspects
{
    public partial class ExportList : UserBaseForm
    {
        public ExportList()
        {
            InitializeComponent();
        }

        private void ExportList_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OptGroup o = new OptGroup();
            o.ShowDialog();
        }
    }
}
