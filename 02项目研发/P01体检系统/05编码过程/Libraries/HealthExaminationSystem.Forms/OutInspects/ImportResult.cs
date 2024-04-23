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
    public partial class ImportResult : UserBaseForm
    {
        public ImportResult()
        {
            InitializeComponent();
        }

        private void ImportResult_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExportTemplates s = new ExportTemplates();
            s.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ExportList i = new ExportList();
            i.ShowDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DataImport a = new DataImport();
            a.ShowDialog();
        }
    }
}
