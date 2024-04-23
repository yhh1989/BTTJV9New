using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class frmpdfShow : UserBaseForm
    {
        public frmpdfShow()
        {
            InitializeComponent();
        }
              public string strpdfPath = "";        private void frmpdfShow_Load(object sender, EventArgs e)        {            if (!string.IsNullOrEmpty(strpdfPath))            {                pdfViewer1.LoadDocument(strpdfPath);            }        }
    }
}
