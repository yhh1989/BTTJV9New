using DevExpress.Utils;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.OccCusInfoOut
{
    public partial class frmOccQuePic : UserBaseForm
    {
        private CameraHelper cameraHelper;
        private PictureController _pictureController;
        private CustomerAppService _customerAppService;
        private readonly ICommonAppService _commonAppService;
        public frmOccQuePic()
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
            pictureCus.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;


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
            if (cameraHelper == null || cameraHelper.IsDisposed)
            {
                cameraHelper = new CameraHelper();
                cameraHelper.TakeSnapshotComplete += (ss, ee) =>
                {
                    var camer = ss as CameraHelper;
                    //textEditTijianhao.Text = camer.BarCode;
                    var customerIMG = camer.ImageName;


                    //上传
                    var result = _pictureController.Uploading(customerIMG, "Test");
                    var GuidancePhotoId = result.Id;
                    OccQueCusDto occQueCusDto = new OccQueCusDto();
                    occQueCusDto.Id = cusInfo.Id;
                    occQueCusDto.OccQuesPhotoId = GuidancePhotoId;
                    occQueCusDto.OccQuesUserId = CurrentUser.Id;
                    _customerAppService.SaveOccQueCus(occQueCusDto);
                    SearchOccQueCus input = new SearchOccQueCus();
                    input.RegId = cusInfo.Id;
                    var cuslist = _customerAppService.GetOccQueCus(input);

                    if (cuslist.Count > 0)
                    {
                        var cusInfonew = cuslist.FirstOrDefault();
                        ModelHelper.CustomMapTo(cusInfonew, cusInfo);
                        if (cusInfo.OccQuesPhotoId.HasValue && cusInfo.OccQuesPhotoId.Value != Guid.Empty)
                        {
                            var url = _pictureController.GetUrl(cusInfo.OccQuesPhotoId.Value);
                            pictureCus.LoadAsync(url.RelativePath);

                        }
                        gridViewcus.RefreshData();
                        gridCus.RefreshDataSource();
                        gridCus.Refresh();
                    }


                };
                cameraHelper.Show(this);
            
            }

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
            ;
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
                if (cusInfo.OccQuesPhotoId.HasValue && cusInfo.OccQuesPhotoId.Value != Guid.Empty)
                {
                    var url = _pictureController.GetUrl(cusInfo.OccQuesPhotoId.Value);
                    pictureCus.LoadAsync(url.RelativePath);

                }
                else
                {
                    pictureCus.Image = null;
                }

                textCusRegBM.Text = cusInfo.CustomerBM;
                textName.Text = cusInfo.Customer.Name;
                textSex.Text = cusInfo.Customer.FormatSex;
                textAge.Text = cusInfo.Customer.Age.ToString();


            }
        }
    }
}
