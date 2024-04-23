using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Camera;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
//using ZXing;
//using ZXing.Common;
using AppDomain = System.AppDomain;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class CameraHelper : UserBaseForm
    {
        private readonly string _cameraSetting;

        private readonly string _camera;

        //private readonly DecodingOptions _decodingOptions;

        //private readonly BarcodeReader _barcodeReader;

        public Image Image { get; set; }

        public string ImageName { get; set; }

        public string BarCode { get; set; }

        public CameraHelper()
        {
            InitializeComponent();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _camera = Path.Combine(baseDirectory, "Camera");
            _cameraSetting = Path.Combine(baseDirectory, "CameraSetting.xml");
            //_decodingOptions = new DecodingOptions
            //{
            //    PossibleFormats = new List<BarcodeFormat>
            //    {
            //        //BarcodeFormat.AZTEC,
            //        //BarcodeFormat.CODABAR,
            //        BarcodeFormat.CODE_39,
            //        //BarcodeFormat.CODE_93,
            //        //BarcodeFormat.CODE_128,
            //        //BarcodeFormat.DATA_MATRIX,
            //        //BarcodeFormat.EAN_8,
            //        //BarcodeFormat.EAN_13,
            //        //BarcodeFormat.ITF,
            //        //BarcodeFormat.MAXICODE,
            //        //BarcodeFormat.PDF_417,
            //        //BarcodeFormat.QR_CODE,
            //        //BarcodeFormat.RSS_14,
            //        //BarcodeFormat.RSS_EXPANDED,
            //        //BarcodeFormat.UPC_A,
            //        //BarcodeFormat.UPC_E,
            //        //BarcodeFormat.All_1D,
            //        //BarcodeFormat.UPC_EAN_EXTENSION,
            //        //BarcodeFormat.MSI,
            //        //BarcodeFormat.PLESSEY,
            //        //BarcodeFormat.IMB
            //    },
            //    TryHarder = true
            //};
            //_barcodeReader = new BarcodeReader { Options = _decodingOptions };
        }

        private void cameraControl_DeviceChanged(object sender, DevExpress.XtraEditors.Camera.CameraDeviceChangedEventArgs e)
        {
            cameraControl.SaveSettingsToXml(_cameraSetting);
        }

        private void CameraHelper_Load(object sender, EventArgs e)
        {
            if (File.Exists(_cameraSetting))
            {
                cameraControl.RestoreSettingsFromXml(_cameraSetting);
                cameraControl.Start();
            }
            else
            {
                cameraControl.Start(CameraControl.GetDefaultDevice());
            }
        }

        private void simpleButtonSnapshot_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_camera))
            {
                var files = Directory.GetFiles(_camera);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.LastWriteTime < DateTime.Now.AddDays(-3))
                    {
                        fileInfo.Delete();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_camera);
            }
            GC.Collect();
            ImageName = Path.Combine(_camera, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss fff") + ".jpg");
            Image = cameraControl.TakeSnapshot();
            Image.Save(ImageName, ImageFormat.Jpeg);
            using (var bitmap = new Bitmap(ImageName))
            {
                //var result = _barcodeReader.Decode(bitmap);
                //BarCode = result?.Text;
                textEditCustomerBM.Text = BarCode;
            }
            TakeSnapshotComplete?.Invoke(this, EventArgs.Empty);
            GC.Collect();
           
        }

        public event EventHandler TakeSnapshotComplete;

        private void simpleButtonSnapshotAndClose_Click(object sender, EventArgs e)
        {
            simpleButtonSnapshot.PerformClick();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Snapshot()
        {
            simpleButtonSnapshot.PerformClick();
        }

        public void SnapshotAndClose()
        {
            simpleButtonSnapshotAndClose.PerformClick();
        }

        public void Cancel()
        {
            simpleButtonCancel.PerformClick();
        }

        private void textEditCustomerBM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(textEditCustomerBM.Text))
                {
                    BarCode = textEditCustomerBM.Text;
                    TakeSnapshotComplete?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}