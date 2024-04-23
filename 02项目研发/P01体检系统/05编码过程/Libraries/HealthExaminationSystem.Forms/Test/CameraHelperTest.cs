using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.CusReg;

namespace Sw.Hospital.HealthExaminationSystem.Test
{
    public partial class CameraHelperTest : UserBaseForm
    {
        public CameraHelperTest()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (var frm = new CameraHelper())
            {
                frm.TakeSnapshotComplete += (s, ee) =>
                {
                    if (s is CameraHelper cameraHelper)
                    {
                        textEdit1.Text = cameraHelper.ImageName;
                        textEdit2.Text = cameraHelper.BarCode;
                        pictureEdit1.Image = cameraHelper.Image;
                    }
                };
                frm.ShowDialog(this);
            }
        }
    }
}