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
using ZXing;
using ZXing.Common;

namespace Sw.Hospital.HealthExaminationSystem.Test
{
    public partial class XtraFormBarCode : UserBaseForm
    {
        public XtraFormBarCode()
        {
            InitializeComponent();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            openFileDialog.ShowDialog(this);
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            buttonEdit1.Text = openFileDialog.FileName;
            pictureEdit1.Image = Image.FromFile(buttonEdit1.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var decodeOption = new DecodingOptions
            {
                PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.AZTEC,
                    BarcodeFormat.CODABAR,
                    BarcodeFormat.CODE_39,
                    BarcodeFormat.CODE_93,
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.DATA_MATRIX,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.ITF,
                    BarcodeFormat.MAXICODE,
                    BarcodeFormat.PDF_417,
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.RSS_14,
                    BarcodeFormat.RSS_EXPANDED,
                    BarcodeFormat.UPC_A,
                    BarcodeFormat.UPC_E,
                    BarcodeFormat.All_1D,
                    BarcodeFormat.UPC_EAN_EXTENSION,
                    BarcodeFormat.MSI,
                    BarcodeFormat.PLESSEY,
                    BarcodeFormat.IMB
                },
                TryHarder = true
            };
            var br = new BarcodeReader {Options = decodeOption};
            var bit = new Bitmap(openFileDialog.FileName);
            var rs = br.Decode(bit);
            if (rs == null)
            {
                XtraMessageBox.Show(this, "读取失败！");
            }
            else
            {
                textEdit1.Text = rs.Text;
            }
        }
    }
}