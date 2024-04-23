using DevExpress.XtraEditors.Camera;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class ZYCusCamera : UserBaseForm
    {
        private readonly string _cameraSetting;
        private PictureController _pictureController;
        private readonly string _camera;
        private readonly IBasicDictionaryAppService _basicDictionaryAppService;
        public List<Image> imagelist = new List<Image>();
        public List<string> imagePathList = new List<string>();
        public List<Guid> imageIDList = new List<Guid>();
        public ZYCusCamera()
        {
            InitializeComponent();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _camera = Path.Combine(baseDirectory, "Camera");
            _cameraSetting = Path.Combine(baseDirectory, "CameraSetting.xml");
            _basicDictionaryAppService = new BasicDictionaryAppService();
        }

        private void CusCamera_Load(object sender, EventArgs e)
        {
            _pictureController = new PictureController();
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
        /// 采集图片
        /// </summary>
        public Image CameraImage { get; set; }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (imagelist != null && imagelist.Count > 0)
            {
                foreach (var iamge in imagelist)
                {
                    #region image转路径

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
                    var customerIMG = Path.Combine(_camera, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss fff") + ".jpg");

                    iamge.Save(customerIMG, ImageFormat.Jpeg);
                    #endregion
                    //上传
                    var result = _pictureController.Uploading(customerIMG, "Test");
                    var GuidancePhotoId = result.Id;
                    imageIDList.Add(GuidancePhotoId);
                }

            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.CameraImage = this.cameraControl.TakeSnapshot();
            System.Drawing.Image img = this.CameraImage;          

           

            imagelist.Add(this.CameraImage);
            imageSlider1.Images.Add(this.CameraImage);
            imageSlider1.CurrentImageIndex = imageSlider1.Images.Count - 1;
            labYS.Text = "共：" + imageSlider1.Images.Count +"张图片";
        }


        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="originImage">原图片</param>
        /// <param name="region">裁剪的方形区域</param>
        /// <returns>裁剪后图片</returns>
        public static Image CropImage(Image originImage, Rectangle region)
        {
            Bitmap result = new Bitmap(region.Width, region.Height);
            Graphics graphics = Graphics.FromImage(result);
          
            graphics.DrawImage(originImage,new Rectangle(0,0, region.Width, region.Height),  region, GraphicsUnit.Pixel);
            
            return result;
        }
        private void FrmCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.cameraControl.Dispose();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BasicDictionaryInput basicDictionary = new BasicDictionaryInput();
            basicDictionary.Type = BasicDictionaryType.PhotoSize.ToString();
            var baselist = _basicDictionaryAppService.Query(basicDictionary);
           var  ptsize = baselist.FirstOrDefault(p => p.Value == 1);
                        
        }
 

        private void butDel_Click(object sender, EventArgs e)
        {
            imagelist.Remove(imageSlider1.CurrentImage);
            
            imageSlider1.Images.Remove(imageSlider1.CurrentImage);
           
        }

        private void imageSlider1_ImageChanged(object sender, DevExpress.XtraEditors.Controls.ImageChangedEventArgs e)
        {
           // this.pictureEdit1.Image = imageSlider1.CurrentImage;
        }
    }
}
