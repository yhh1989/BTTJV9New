using DevExpress.ExpressApp;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class frmPictrueDesk : UserBaseForm
    {
        private Guid? departId;
        /// <summary>
        /// 当前体检人
        /// </summary>
        private ATjlCustomerRegDto _currentInputSys;
        /// <summary>
        /// 所有科室的图片
        /// </summary>
        private List<CustomerItemPicDto> _customerItemPicAllSys;
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly IDoctorStationAppService _doctorStation;
        /// <summary>
        /// 当前体检人体检项目
        /// </summary>
        private List<ATjlCustomerItemGroupDto> _currentCustomerItemGroupSys;
        /// <summary>
        /// 当前登录人拥有科室id
        /// </summary>
        private readonly List<Guid> _departmentGuidSys;
        /// <summary>
        /// 彩图科室
        /// </summary>
        private readonly List<String> _picDepart;
        /// <summary>
        /// 获取图片
        /// </summary>
        private readonly PictureController _pictureController;
        public frmPictrueDesk()
        {
            _pictureController = new PictureController();
            _departmentGuidSys = new List<Guid>();
            _departmentGuidSys = CurrentUser.TbmDepartments.Select(r => r.Id).ToList();
            _picDepart = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString()).Select(o=>o.Text)?.ToList();
            _doctorStation = new DoctorStationAppService();
            InitializeComponent();
        }
        public frmPictrueDesk(ATjlCustomerRegDto aTjlCustomerRegDto , List<CustomerItemPicDto> customerItemPicDtos, List<ATjlCustomerItemGroupDto> aTjlCustomerItemGroupDtos)
        {
            _pictureController = new PictureController();
            _departmentGuidSys = new List<Guid>();
            _departmentGuidSys = CurrentUser.TbmDepartments.Select(r => r.Id).ToList();
            _picDepart = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString()).Select(o => o.Text)?.ToList();
            _doctorStation = new DoctorStationAppService();
            _currentInputSys = aTjlCustomerRegDto;
            _customerItemPicAllSys = customerItemPicDtos;
            _currentCustomerItemGroupSys = aTjlCustomerItemGroupDtos;
            InitializeComponent();
        }
        private void frmPictrueDesk_Load(object sender, EventArgs e)
        {
            
           
        }
        //加载数据
        private void getcusresult()
        {

            AutoLoading(() =>
            {
                //图片信息
                var Query = new QueryClass();
                Query.CustomerRegId = _currentInputSys.Id;
                _customerItemPicAllSys = new List<CustomerItemPicDto>();
                _customerItemPicAllSys = _doctorStation.GetCustomerItemPicDtos(Query);

               
                //只显示本科室或异常科室
                _currentCustomerItemGroupSys = new List<ATjlCustomerItemGroupDto>();
                _currentCustomerItemGroupSys = _doctorStation.GetCustomerItemGroup(Query).Where(o => _departmentGuidSys.Contains(o.DepartmentId) ).ToList();

               
            });

        }
        //加载数据
        private void showCusInfor()
        {
            var itemss = _currentCustomerItemGroupSys.Where(o => _picDepart.Contains(o.DepartmentName)).SelectMany(o => o.CustomerRegItem).Distinct().ToList();
            gridItem.DataSource = itemss;
            //体检号
            txtCusRegBM.Text = _currentInputSys.CustomerBM;
            //姓名
            labName.Text = _currentInputSys.Customer.Name;
            //性别
            if (_currentInputSys.Customer.Sex != null)
                labSex.Text = SexHelper.CustomSexFormatter(_currentInputSys.Customer.Sex);

            //年龄
            labAge.Text = _currentInputSys.RegAge?.ToString();

        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

            var dto = gridItem.GetFocusedRowDto<ATjlCustomerRegItemDto>();
            if (dto == null)
                return;
            var cusItem = _currentCustomerItemGroupSys.SelectMany(o=>o.CustomerRegItem).FirstOrDefault(o=>o.Id==dto.Id);
            txtSum.Text = cusItem.ItemSum;
            txtDia.Text = cusItem.ItemDiagnosis;
            labDepart.Text = _currentCustomerItemGroupSys.FirstOrDefault(o => o.DepartmentId == cusItem.DepartmentId).DepartmentName;
            labTime.Text = _currentCustomerItemGroupSys.FirstOrDefault(o=>o.CustomerRegItem.Any(r=>r.Id==dto.Id)).FirstDateTime.ToString();
          var  _customerItemPicSys = new List<CustomerItemPicDto>();
          var  _customerPicSys = 1;
            _customerItemPicSys = _customerItemPicAllSys.Where(o => o.ItemBMID == cusItem.ItemId).ToList();
            if (_customerItemPicSys != null && _customerItemPicSys.Count > 0)
            {
                
                foreach (var pic in _customerItemPicSys)
                {
                    var Pic = Guid.Parse(_customerItemPicSys[_customerPicSys].PictureBM.ToString());
                    var result = _pictureController.GetUrl(Pic);
                    
                   
                    switch (_customerPicSys)
                    {
                        case 1:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic1.Image = Image.FromStream(stream);
                            }
                            break;
                        case 2:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic2.Image = Image.FromStream(stream);
                            }
                            break;
                        case 3:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic3.Image = Image.FromStream(stream);
                            }
                            break;
                        case 4:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic4.Image = Image.FromStream(stream);
                            }
                            break;
                        case 5:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic5.Image = Image.FromStream(stream);
                            }
                            break;
                        case 6:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic6.Image = Image.FromStream(stream);
                            }
                            break;
                        case 7:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic7.Image = Image.FromStream(stream);
                            }
                            break;
                        case 8:
                            using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                            {
                                pic8.Image = Image.FromStream(stream);
                            }
                            break;
                    }

                    GC.Collect();
                    picShow.Image = pic1.Image;
                    _customerPicSys = _customerPicSys + 1;
                }
            }
            
        }
    }
}
