using DevExpress.XtraEditors.Camera;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class CusCamera : UserBaseForm
    {
        private readonly string _cameraSetting;

        private readonly string _camera;
        private readonly IBasicDictionaryAppService _basicDictionaryAppService;
        string ptsize = "";
        public CusCamera()
        {
            InitializeComponent();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _camera = Path.Combine(baseDirectory, "Camera");
            _cameraSetting = Path.Combine(baseDirectory, "CameraSetting.xml");
            _basicDictionaryAppService = new BasicDictionaryAppService();
        }

        private void CusCamera_Load(object sender, EventArgs e)
        {
            if (File.Exists(_cameraSetting))
            {
                cameraControl1.RestoreSettingsFromXml(_cameraSetting);
                cameraControl1.Start();
            }
            else
            {
                cameraControl1.Start(CameraControl.GetDefaultDevice());
            }
            BasicDictionaryInput basicDictionary = new BasicDictionaryInput();
            basicDictionary.Type = BasicDictionaryType.PhotoSize.ToString();
         var baselist=   _basicDictionaryAppService.Query(basicDictionary);
            ptsize = baselist.FirstOrDefault(p=>p.Value==1)?.Remarks;
           
            if (!string.IsNullOrEmpty(ptsize)   )
            {
                var prlist = ptsize.Split('*');
                if (prlist.Length == 4)
                {
                    spX.Value = Convert.ToInt32(prlist[0]);
                    spY.Value = Convert.ToInt32(prlist[1]);
                    spW.Value = Convert.ToInt32(prlist[2]);
                    spH.Value = Convert.ToInt32(prlist[3]);
                }
            }
        }
        /// 采集图片
        /// </summary>
        public Image CameraImage { get; set; }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.CameraImage = this.cameraControl1.TakeSnapshot();
            System.Drawing.Image img = this.CameraImage;          

           //设置拍照尺寸
            if (  spW.Value > 0 && spH.Value > 0)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.X = Convert.ToInt32(spX.Value);
                rectangle.Y = Convert.ToInt32(spY.Value);
                rectangle.Width = Convert.ToInt32(spW.Value);
                rectangle.Height = Convert.ToInt32(spH.Value);
                System.Drawing.Image imgnew = CropImage(img, rectangle);
                this.CameraImage = imgnew;
            }
            this.pictureEdit1.Image = this.CameraImage;

          



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
            this.cameraControl1.Dispose();
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

            if (spH.Value > 0 && spW.Value > 0)
            {
                var zb = spX.Value + "*" + spY.Value + "*" + spW.Value + "*" + spH.Value;

                if (ptsize != null)
                {
                    ptsize.Remarks = zb;
                      var input = new UpdateBasicDictionaryDto
                       {
                           Id= ptsize.Id,                         
                           Remarks = zb,
                            Code= ptsize.Code,
                             Text= ptsize.Text,
                              Type= ptsize.Type,
                               Value= ptsize.Value

                      };
                    _basicDictionaryAppService.Edit(input); }
                else
                {
                    var input = new CreateBasicDictionaryDto
                    {
                        Value = 1,
                        Text = "照片坐标",
                        Remarks = zb,
                        Type = BasicDictionaryType.PhotoSize.ToString(),
                        Code = ""
                    };
                    _basicDictionaryAppService.Add(input);
                }
               
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("照片宽度高度，都要大于0！");
            }
        }

        private void cameraControl1_MouseMove(object sender, MouseEventArgs e)
        {
             
          var ss=  string.Format("X:{0}  Y:{1}", e.X, e.Y);
            labelzb.Text = "当前坐标：" + ss;
            labelzb.Refresh();
        }
    }
}
