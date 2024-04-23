using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class frmCheckDatecs : UserBaseForm
    {
        public  DateTime Star;
        public DateTime End;
        public DateTime StarTime;
        public DateTime EndTime;
        public frmCheckDatecs()
        {
            InitializeComponent();
        }
        public frmCheckDatecs(DateTime starTime , DateTime endTime) :this()
        {
            dateEditStar.EditValue = starTime;
            dateEditEnd.EditValue = endTime;
            datetimesatr.EditValue = System.DateTime.Now.ToShortDateString();
            datetimesEnd.EditValue= System.DateTime.Now.ToShortDateString();
        }
        private void frmCheckDatecs_Load(object sender, EventArgs e)
        {
            this.datetimesatr.Properties.Mask.EditMask = "HH:mm";
            this.datetimesatr.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.datetimesEnd.Properties.Mask.EditMask = "HH:mm";
            this.datetimesEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Star = dateEditStar.DateTime;
            End = dateEditEnd.DateTime;

            StarTime = datetimesatr.DateTime;
            EndTime = datetimesEnd.DateTime;
            this.DialogResult= DialogResult.OK;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
