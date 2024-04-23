using DevExpress.ExpressApp;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class frmSM : UserBaseForm
    {
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly IDoctorStationAppService _doctorStation;
        public string CatName = "";
        private readonly ICustomerReportAppService customerReportAppService;
        private readonly ICommonAppService _commonAppService;
        public frmSM()
        {
            _commonAppService = new CommonAppService();
            _doctorStation = new DoctorStationAppService();
            customerReportAppService = new CustomerReportAppService();
            InitializeComponent();
        }

        private void frmSM_Load(object sender, EventArgs e)
        {
            simpleLabelItem1.Text = CatName;
            textEdit1.Focus();
            textEdit1.SelectAll();
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var _currentInputSys = new ATjlCustomerRegDto();
                if (!string.IsNullOrEmpty(textEdit1.EditValue.ToString()))
                {
                    AutoLoading(() =>
                    {
                        var Query = new QueryClass();
                        Query.CustomerBM = textEdit1.EditValue.ToString();
                        Query.RegisterState = (int)RegisterState.Yes;
                        _currentInputSys = _doctorStation.GetCustomerRegList(Query).FirstOrDefault(); //获取客户信息
                        if (_currentInputSys != null)
                        {
                            string sexformat = "";
                            if (_currentInputSys.Customer.Sex != null)
                                sexformat = SexHelper.CustomSexFormatter(_currentInputSys.Customer.Sex);
                            labelControl1.Text = string.Format(@"体检号：{0}，姓名：{1},性别：{2}，年龄：{3}",
                               _currentInputSys.CustomerBM, _currentInputSys.Customer.Name, sexformat, _currentInputSys.Customer.Age);  
                                TjlCusCabitDto dto = new TjlCusCabitDto();
                                dto.CustomerRegId = _currentInputSys.Id;
                                dto.CabitName = CatName;
                                dto.GetState = 1;
                                dto.ReportState = 1;
                                customerReportAppService.SaveTjlCabinet(dto);
                                CustomerUpCatDto catDto = new CustomerUpCatDto();
                                catDto.Id = _currentInputSys.Id;
                                catDto.CusCabitBM = CatName;
                                catDto.CusCabitState = 1;
                                catDto.CusCabitTime = _commonAppService.GetDateTimeNow().Now;
                                var result = customerReportAppService.UpCustomerUpCat(catDto);
                                labelControl1.Text += ", 存入成功！";
                        }
                        else
                        {
                            MessageBox.Show("没有该体检人");
                        }


                    });
                    
                    textEdit1.Focus();
                    textEdit1.SelectAll();
                }
            }
        }
    }
}
