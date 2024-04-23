using DevExpress.Utils;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.CusReg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.OccCusInfoOut
{
    public partial class frmOccMQuePic : UserBaseForm
    {
        private CameraHelper cameraHelper;
        private PictureController _pictureController;
        private CustomerAppService _customerAppService;
        private readonly ICommonAppService _commonAppService;
        public frmOccMQuePic()
        {
            _commonAppService = new CommonAppService();
            InitializeComponent();
        }

        private void frmOccQuePic_Load(object sender, EventArgs e)
        {
            _pictureController = new PictureController();
            _customerAppService = new CustomerAppService();
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEnd.DateTime = date;
            dateStar.EditValue = date;
          //  pictureCus.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;


            gridViewcus.Columns[conOccQuesSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewcus.Columns[conOccQuesSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(QueStateHelper.QueStateFormatter);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           var cusInfo= gridViewcus.GetFocusedRow() as OccQueCusDto;
            if (cusInfo == null)
            {
                MessageBox.Show("请选择体检人！");
                return;
            }
            #region 单图拍照
            //if (cameraHelper == null || cameraHelper.IsDisposed)
            //{
            //    cameraHelper = new CameraHelper();
            //    cameraHelper.TakeSnapshotComplete += (ss, ee) =>
            //    {
            //        var camer = ss as CameraHelper;
            //        //textEditTijianhao.Text = camer.BarCode;
            //        var customerIMG = camer.ImageName;


            //        //上传
            //        var result = _pictureController.Uploading(customerIMG, "Test");
            //        var GuidancePhotoId = result.Id;
            //        OccQueCusDto occQueCusDto = new OccQueCusDto();
            //        occQueCusDto.Id = cusInfo.Id;
            //        occQueCusDto.OccQuesPhotoId = GuidancePhotoId;
            //        occQueCusDto.OccQuesUserId = CurrentUser.Id;
            //        _customerAppService.SaveOccQueCus(occQueCusDto);
            //        SearchOccQueCus input = new SearchOccQueCus();
            //        input.RegId = cusInfo.Id;
            //        var cuslist = _customerAppService.GetOccQueCus(input);

            //        if (cuslist.Count > 0)
            //        {
            //            var cusInfonew = cuslist.FirstOrDefault();
            //            ModelHelper.CustomMapTo(cusInfonew, cusInfo);
            //            if (cusInfo.OccQuesPhotoId.HasValue && cusInfo.OccQuesPhotoId.Value != Guid.Empty)
            //            {
            //                var url = _pictureController.GetUrl(cusInfo.OccQuesPhotoId.Value);
            //                pictureCus.LoadAsync(url.RelativePath);

            //            }
            //            gridViewcus.RefreshData();
            //            gridCus.RefreshDataSource();
            //            gridCus.Refresh();
            //        }


            //    };
            //    cameraHelper.Show(this);

            //}

            #endregion

            #region 多图片照相
            try
            {
               
                //字段判断拍照采集头像
                #region MyRegion
                var cameraHelper = new ZYCusCamera();

                if (cameraHelper.ShowDialog() == DialogResult.OK)
                {
                    pictureCus.Images.Clear();
                    var cuimgaelist = cameraHelper.imageIDList;
                      
                    if (cuimgaelist != null && cuimgaelist.Count>0)
                    {
                        List<OccQueCusDto> OccQueCuslist = new List<OccQueCusDto>();
                        foreach (var iamge in cuimgaelist)
                        {
                           
                            var customerIMG = iamge;

                            //上传                             
                            var GuidancePhotoId = iamge;
                            OccQueCusDto occQueCusDto = new OccQueCusDto();
                            occQueCusDto.Id = cusInfo.Id;
                            occQueCusDto.OccQuesPhotoId = GuidancePhotoId;
                            occQueCusDto.OccQuesUserId = CurrentUser.Id;
                            OccQueCuslist.Add(occQueCusDto);

                        }
                        if (OccQueCuslist.Count > 0)
                        {
                            _customerAppService.SaveOccQueCusList(OccQueCuslist);
                            OccQueCusDto input = new OccQueCusDto();
                            input.Id = cusInfo.Id;
                            var cuslist = _customerAppService.getOccQueCusList(input);
                            if (OccQueCuslist.Count > 0)
                            {
                                foreach (var cusImage in cuslist)
                                {

                                    var url = _pictureController.GetUrl(cusImage.PictureBM.Value);
                                    WebClient wc = new WebClient();
                                    byte[] bytes = wc.DownloadData(url.RelativePath);
                                    MemoryStream ms = new MemoryStream(bytes);
                                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                    pictureCus.Images.Add(img);

                                }
                                labCount.Text = "共：" + pictureCus.Images.Count +"张图片";
                                cusInfo.OccQuesDate = System.DateTime.Now;
                                cusInfo.OccQuesSate = 1;
                                cusInfo.OccQuesUserId = OccQueCuslist.FirstOrDefault().OccQuesUserId;
                                gridViewcus.RefreshData();
                                gridCus.RefreshDataSource();
                                gridCus.Refresh();
                            }
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
               // alertInfo.Show(this, "提示!", ex.Message.ToString());

            }
            #endregion
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchOccQueCus SearchOccQue = new SearchOccQueCus();
            if (!string.IsNullOrEmpty(textBM.Text))
            {
                SearchOccQue.CusRegBM = textBM.Text;
            }
            if (!string.IsNullOrEmpty(dateStar.EditValue?.ToString()))
            {
                SearchOccQue.StarDate =DateTime.Parse( dateStar.DateTime.ToShortDateString());
            }
            if (!string.IsNullOrEmpty(dateEnd.EditValue?.ToString()))
            {
                SearchOccQue.EndDate = DateTime.Parse(dateEnd.DateTime.ToShortDateString()).AddDays(1);
            }
            if (!string.IsNullOrEmpty(radioGroup1.EditValue?.ToString()))
            {
                if (radioGroup1.EditValue.ToString() != "2")
                {
                    SearchOccQue.OccQuesSate=(int)radioGroup1.EditValue;
                }
            }

            //occQueCusDto.Id = cusInfo.Id;
            //occQueCusDto.OccQuesPhotoId = GuidancePhotoId;
            //occQueCusDto.OccQuesUserId = CurrentUser.Id;
            var cuslist= _customerAppService.GetOccQueCus(SearchOccQue);
            gridCus.DataSource = cuslist;
        }

        private void gridViewcus_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && e.RowHandle >= 0)
            {
                var cusInfo = gridViewcus.GetFocusedRow() as OccQueCusDto;
                if (cusInfo == null)
                {
                    MessageBox.Show("请选择体检人！");
                    return;
                }
                SearchOccQueCus input = new SearchOccQueCus();
                input.RegId = cusInfo.Id;
                var cuslist = _customerAppService.GetOccQueCus(input);

                if (cuslist.Count > 0)
                {
                    cusInfo = cuslist.FirstOrDefault();
                    //gridViewcus.RefreshData();
                }
                //if (cusInfo.OccQuesPhotoId.HasValue && cusInfo.OccQuesPhotoId.Value != Guid.Empty)
                //{
                //    var url = _pictureController.GetUrl(cusInfo.OccQuesPhotoId.Value);
                //    pictureCus.LoadAsync(url.RelativePath);

                //}
                //else
                //{
                //    pictureCus.Image = null;
                //}
                #region 获取图片     
                pictureCus.Images.Clear();
                OccQueCusDto inputpic = new OccQueCusDto();
                inputpic.Id = cusInfo.Id;
                var cuspiclist = _customerAppService.getOccQueCusList(inputpic);
                
                    foreach (var cusImage in cuspiclist)
                    {

                        var url = _pictureController.GetUrl(cusImage.PictureBM.Value);
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(url.RelativePath);
                        MemoryStream ms = new MemoryStream(bytes);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                        pictureCus.Images.Add(img);

                    }
                #endregion
                labCount.Text = "共：" + pictureCus.Images.Count + "张图片";
                textCusRegBM.Text = cusInfo.CustomerBM;
                textName.Text = cusInfo.Customer.Name;
                textSex.Text = cusInfo.Customer.FormatSex;
                textAge.Text = cusInfo.Customer.Age.ToString();


            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var cusInfo = gridViewcus.GetFocusedRow() as OccQueCusDto;
            if (cusInfo == null)
            {
                MessageBox.Show("请选择体检人！");
                return;
            }
            if (cameraHelper == null || cameraHelper.IsDisposed)
            {
                cameraHelper = new CameraHelper();
                cameraHelper.TakeSnapshotComplete += (ss, ee) =>
                {
                    var camer = ss as CameraHelper;
                    //textEditTijianhao.Text = camer.BarCode;
                    var customerIMG = camer.ImageName;


                   
                  


                };
                cameraHelper.Show(this);

            }

        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show("是否删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {

                var cusInfo = gridViewcus.GetFocusedRow() as OccQueCusDto;
                if (cusInfo == null)
                {
                    MessageBox.Show("请选择体检人！");
                    return;
                }
                //删除这个人的照片
                OccQueCusDto inputpic = new OccQueCusDto();
                inputpic.Id = cusInfo.Id;
                _customerAppService.deletOccQue(inputpic);
                #region 获取图片     
                //刷新图片
                pictureCus.Images.Clear();

                var cuspiclist = _customerAppService.getOccQueCusList(inputpic);

                foreach (var cusImage in cuspiclist)
                {

                    var url = _pictureController.GetUrl(cusImage.PictureBM.Value);
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(url.RelativePath);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    pictureCus.Images.Add(img);

                }
                #endregion
            }
        }
    }
}
