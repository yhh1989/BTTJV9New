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
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Test
{
    public partial class MergePicture : UserBaseForm
    {
        public MergePicture()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                var images = new List<Image>
                {
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file)
                };
                var image = ImageHelper.DrawImage(1024, 768, images: images.ToArray());
                pictureEdit1.Image = image;
                GC.Collect();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                var images = new List<Image>
                {
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file)
                };
                var image = ImageHelper.DrawImage(1024, 768, images: images.ToArray());
                pictureEdit1.Image = image;
                GC.Collect();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                var images = new List<Image>
                {
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file),
                    Image.FromFile(file)
                };
                var image = ImageHelper.DrawImage(1024, 768, 3, images: images.ToArray());
                pictureEdit1.Image = image;
                GC.Collect();
            }
        }
    }
}