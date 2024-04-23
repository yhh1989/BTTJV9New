using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Win.Properties;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.WebApi.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HealthExaminationSystem.Win
{
    public partial class LoginNew : XtraForm
    {


        private readonly AccountController _account;

        private readonly UserAppService _userAppService;

        private readonly FormRoleAppService _formRoleAppService;



        private const int WM_NCLBUTTONDOWN = 0XA1;   //.定义鼠標左鍵按下
        private const int HTCAPTION = 2;
        public LoginNew()
        {
            InitializeComponent();
            _account = new AccountController();

            _userAppService = new UserAppService();

            _formRoleAppService = new FormRoleAppService();
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private const int WmSyscommand = 0x0112;

        private const int ScMove = 0xF010;

        private const int HtCaption = 0x0002;

        private void emptySpaceItem_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WmSyscommand, ScMove + HtCaption, 0);
        }

        private void Login_Load(object sender, EventArgs e)
        {

            //checkEditRememberPwd.Checked = Settings.Default.RememberPwd;
            textEditUserName.Text = Settings.Default.UserName;
            textEditUserPwd.Text = Settings.Default.Password;
            string fp = System.Windows.Forms.Application.StartupPath + "\\Logo\\logonew.png";
            if (File.Exists(fp))
            {
                // 使用Image.FromFile方法从文件路径加载图片  
                Image image = Image.FromFile(fp);
               
                // 记住在使用完Image对象后释放其占用的资源  
               
                labelLogo.Appearance.Image = image;
                //image.Dispose();
            }
          

        }
        

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {

            AutoLoading(() =>
            {
                dxErrorProvider.ClearErrors();
                var name = textEditUserName.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    dxErrorProvider.SetError(textEditUserName, string.Format(Variables.MandatoryTips, "用户名"));
                    textEditUserName.Focus();
                    return;
                }
                var pwd = textEditUserPwd.Text;
                if (string.IsNullOrWhiteSpace(pwd))
                {
                    //dxErrorProvider.SetError(textEditUserPwd, string.Format(Variables.MandatoryTips, "密码"));
                    //textEditUserPwd.Focus();
                    //return;
                    pwd = "&&&&&&";
                }
                Authenticate(name, pwd);
            });
        }

        public void Authenticate(string name, string pwd)
        {
            try
            {
                _account.Authenticate(new LoginModel
                {
                    TenancyName = Settings.Default.TenantName,
                    UsernameOrEmailAddress = name,
                    Password = pwd
                });
                Variables.User = _userAppService.GetUser(new EntityDto<long> { Id = -1 });
                var genericIdentity = new GenericIdentity(Variables.User.Id.ToString());
                var roles = new List<string>();
                if (pwd != null)
                {
                    splashScreenManager.SetWaitFormDescription(Variables.LoadingForPermission);
                }
                if (Variables.User.UserName == "admin")
                {
                    Variables.PermissionEnabled = false;
                    var allRoles = _formRoleAppService.GetAll();
                    foreach (var role in allRoles)
                    {
                        roles.Add(role.Name);
                    }
                }
                else
                {
                    Variables.PermissionEnabled = true;
                    foreach (var role in Variables.User.FormRoles)
                    {
                        roles.Add(role.Name);
                    }
                }
                var genericPrincipal = new GenericPrincipal(genericIdentity, roles.ToArray());
                Thread.CurrentPrincipal = genericPrincipal;
                Settings.Default.UserName = name;
                //if (checkEditRememberPwd.Checked)
                //{
                //    Settings.Default.RememberPwd = true;
                //    Settings.Default.Password = pwd;
                //    Settings.Default.Save();
                //}
                //else
                //{
                Settings.Default.RememberPwd = false;
                //Settings.Default.UserName = string.Empty;
                Settings.Default.Password = string.Empty;
                if (Debugger.IsAttached && Dns.GetHostName() == "DESKTOP-GKQ70O9")
                {
                    // 在调试状态时，并且是某台指定的计算机，则记住用户密码
                    Settings.Default.Password = pwd;
                }
                Settings.Default.Save();
                //}


                DialogResult = DialogResult.OK;
            }
            catch (UserFriendlyException ex)
            {
                
                MessageBox.Show(ex.Message +ex.Description);
            }
        }

        /// <summary>
        /// 自动加载 Loading 层
        /// </summary>
        /// <param name="method">Loading 层执行过程</param>
        /// <param name="tips">开始执行时的描述 </param>
        protected void AutoLoading(Action method, string tips = Variables.LoadingForCloud)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }

            System.Windows.Forms.Application.DoEvents();
            splashScreenManager.SetWaitFormDescription(tips);
            try
            {
                method.Invoke();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
            }
        }
        /// <summary>
        /// 显示<see cref="UserFriendlyException" />弹窗
        /// </summary>
        /// <param name="e">弹窗信息</param>
        protected void ShowMessageBox(UserFriendlyException e)
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
            //XtraMessageBox.Show(this, e.Description, e.Code.ToString(), e.Buttons, e.Icon);
            XtraMessageBox.Show(this, e.Description, "提示", e.Buttons, e.Icon);
        }


        private void Login_Shown(object sender, EventArgs e)
        {
            Activate();
            textEditUserName.Focus();
        }

        private void LoginNew_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WmSyscommand, ScMove + HtCaption, 0);
        }

        private void labelControl5_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void labelControl5_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void labelControl5_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEditUserName_Leave(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(textEditUserName.EditValue.ToString().Trim()))
            //{
            //    SearchUserDto userDto = new SearchUserDto();
            //    userDto.EmployeeNum = textEditUserName.EditValue.ToString().Trim();
            //    userDto.TenantName = Settings.Default.TenantName;
            //    var result = _userAppService.GetUsersByName(userDto);
            //    if (result.Count > 0)
            //    {
            //        textEditUserName.EditValue = result[0].UserName;
            //    }
            //}
        }

        private void textEditUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textEditUserName.Text))
            {
                textEditUserPwd.Focus();
            }
        }

        private void textEditUserPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//&& !string.IsNullOrWhiteSpace(textEditUserPwd.Text)
            {
                simpleButtonOk.Focus();
                simpleButtonOk.PerformClick();
            }
        }
    }
}
