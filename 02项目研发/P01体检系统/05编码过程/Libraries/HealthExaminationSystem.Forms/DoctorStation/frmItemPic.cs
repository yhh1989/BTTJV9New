using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class frmItemPic : UserBaseForm
    {
        private PictureController _pictureController;
        private readonly IDoctorStationAppService _doctorStation;
        public string DepartName = "";
        public Guid CusGroupId = new Guid();
        public Guid CusRegId = new Guid();
        public Guid CusItemID = new Guid();
        /// <summary>
        /// 当前科室的图片
        /// </summary>
        public List<CustomerItemPicDto> customerItemPicSys;

        public frmItemPic()
        {
            _doctorStation = new DoctorStationAppService();
            InitializeComponent();
        }
        public frmItemPic(Guid _CusRegId, Guid _CusGroupId, Guid _CusItemID,
            List<CustomerItemPicDto> _customerItemPicSys) :this()
        {
            CusRegId = _CusRegId;
            CusGroupId = _CusGroupId;
            CusItemID = _CusItemID;
            customerItemPicSys = _customerItemPicSys;
        }
        private void frmItemPic_Load(object sender, EventArgs e)
        {
            _pictureController = new PictureController();
            labeDepartName.Text = DepartName;
            gridControlPic.DataSource = customerItemPicSys;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Excel(*.jpg)|*.jpg|Excel(*.png)|*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var dt = gridControlPic.DataSource as List<CustomerItemPicDto>;
                if (dt == null)
                {
                    dt = new List<CustomerItemPicDto>();
                }
                pictureEditItem.Image = Image.FromFile(openFileDialog.FileName);

                var CustomerItemPicDto = new CustomerItemPicDto();
                CustomerItemPicDto.CustomerItemGroupID = CusGroupId;
                CustomerItemPicDto.ItemBMID = CusItemID;
                CustomerItemPicDto.TjlCustomerRegID = CusRegId;
             var picOut=   _pictureController.Uploading(openFileDialog.FileName,"");
                CustomerItemPicDto.PictureBM = picOut.Id;
                dt.Add(CustomerItemPicDto);
                gridControlPic.DataSource = dt;
                gridControlPic.RefreshDataSource();
                gridControlPic.Refresh();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var dto = gridControlPic.GetFocusedRowDto<CustomerItemPicDto>();
            if (dto == null)
            {
                ShowMessageBoxWarning("请选中行！");
            }
            if (dto.PictureBM.HasValue)
            {
                //_pictureController.Delete(dto.PictureBM.Value);
                var dt = gridControlPic.DataSource as List<CustomerItemPicDto>;
                dt.Remove(dto);
                gridControlPic.DataSource = dt;
                gridControlPic.RefreshDataSource();
                gridControlPic.Refresh();
            }
            else
            {
                ShowMessageBoxWarning("图片未保存无需删除！");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var dt = gridControlPic.DataSource as List<CustomerItemPicDto>;
            if (dt !=null)
            {
                if (dt.Count == 0)
                {
                    CustomerItemPicDto customerItemPic = new CustomerItemPicDto();
                    customerItemPic.CustomerItemGroupID = CusGroupId;
                    customerItemPic.ItemBMID = CusItemID;
                    customerItemPic.TjlCustomerRegID = CusRegId;
                    dt.Add(customerItemPic);
                }
                _doctorStation.SaveItemPic(dt);
                customerItemPicSys = dt.ToList();
                this.DialogResult = DialogResult.OK;

            }
            else
            {
                ShowMessageBoxWarning("无数据保存！");
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var dto = gridControlPic.GetFocusedRowDto<CustomerItemPicDto>();
            if (dto == null)
                return;
            var url = _pictureController.GetUrl(dto.PictureBM.Value);

            

            using (var stream = ImageHelper.GetUriImageStream(new Uri(url.RelativePath)))
            {

                pictureEditItem.Image = Image.FromStream(stream);
            }

        }
    }
}
