using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.WebApi.Api.Models;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;


namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ClearData
{
    public partial class frmClearData : UserBaseForm
    {
        private readonly AccountController _account;
        private readonly UserAppService _userAppService;


        public frmClearData()
        {
            _userAppService = new UserAppService();
            InitializeComponent();
        }

        private void frmClearData_Load(object sender, EventArgs e)
        {
            this.textEditUserName.Text = "admin";
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {

                var pwd = textEditUserPwd.Text;
                if (string.IsNullOrWhiteSpace(pwd))
                {
                    dxErrorProvider.SetError(textEditUserPwd, string.Format(Variables.MandatoryTips, "密码"));
                    textEditUserPwd.Focus();
                    return;
                }
                VerificationUserDto dto = new VerificationUserDto();
                dto.UserName = "admin";
                dto.Password = pwd;
               var result=   _userAppService.VerificationUser(dto);
                if (result == true)
                {
                    frmCheck deleteData = new frmCheck();
                    this.Hide();
                    deleteData.ShowDialog();
                    
                }
                else {
                    MessageBox.Show("密码不正确");
                }
            });
        }
    }
}
