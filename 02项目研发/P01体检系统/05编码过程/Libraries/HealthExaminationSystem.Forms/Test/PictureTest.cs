using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DialogResult = System.Windows.Forms.DialogResult;

namespace Sw.Hospital.HealthExaminationSystem.Test
{
    public partial class PictureTest : UserBaseForm
    {
        private PictureController _pictureController;

        public PictureTest()
        {
            InitializeComponent();

            _pictureController = new PictureController();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var result = _pictureController.Uploading(openFileDialog.FileName, "Test");
                memoEdit1.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var result = XtraInputBox.Show("请输入ID", "提示", string.Empty);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var result1 = _pictureController.GetUrl(new Guid(result));
                memoEdit2.Text = JsonConvert.SerializeObject(result1, Formatting.Indented);

                if (ImageHelper.TryGetUriImageStream(new Uri(result1.Thumbnail), out var stream))
                {
                    using (stream)
                    {
                        pictureEdit2.Image = Image.FromStream(stream);
                    }
                }

                if (ImageHelper.TryGetUriImageStream(new Uri(result1.RelativePath), out var stream1))
                {
                    using (stream1)
                    {
                        pictureEdit1.Image = Image.FromStream(stream1);
                    }
                }

                GC.Collect();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var result = XtraInputBox.Show("请输入ID", "提示", string.Empty);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var result1 = _pictureController.Update(openFileDialog.FileName, new Guid(result));
                    memoEdit1.Text = JsonConvert.SerializeObject(result1, Formatting.Indented);
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var result = XtraInputBox.Show("请输入ID", "提示", string.Empty);
            if (!string.IsNullOrWhiteSpace(result))
            {
                _pictureController.Delete(new Guid(result));
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }
    }
}