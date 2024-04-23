using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Win.Properties;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.WebApi.Api.Models;
using AppDomain = System.AppDomain;

namespace HealthExaminationSystem.Win
{
    public partial class Login : UserBaseForm
    {
        private readonly AccountController _account;

        private readonly UserAppService _userAppService;

        private readonly FormRoleAppService _formRoleAppService;

        public Login()
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
            

            SetBackground();
            checkEditRememberPwd.Checked = Settings.Default.RememberPwd;
            textEditUserName.Text = Settings.Default.UserName;
            textEditUserPwd.Text = Settings.Default.Password;
        }

        private void SetBackground()
        {
            Variables.Company = "体检系统";
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var logo = Path.Combine(baseDirectory, "Logo");
            var logoImg = Path.Combine(logo, "Logo.png");
            //var x = 25;
            //var y = 30;
            var x = 50;
            var y = 90;
            var fontSize = 25;
            if (File.Exists(logoImg))
            {
                using (var logoImage = Image.FromFile(logoImg))
                {
                    layoutControlGroupBase.BackgroundImage = DrawBackgroundImage(logoImage, x, y);
                }
            }
            var logoModel = Path.Combine(logo, "Logo.json");
            if (File.Exists(logoModel))
            {
                var logoModelJson = File.ReadAllText(logoModel, Encoding.UTF8);
                var logoM = JsonConvert.DeserializeObject<Logo.Logo>(logoModelJson);
                if (!string.IsNullOrWhiteSpace(logoM.Name))
                {
                    Variables.Company = logoM.Name;
                    x = x + 75 + 6;
                    y = y + 75 / 2 - fontSize;
                    layoutControlGroupBase.BackgroundImage = DrawBackgroundImage(logoM.Name, fontSize, x, y);
                }

                if (!string.IsNullOrWhiteSpace(logoM.EnName))
                {
                    y = y + fontSize * 2 - 8;
                    layoutControlGroupBase.BackgroundImage = DrawBackgroundImage(logoM.EnName, fontSize / 2, x, y);
                }
            }
        }

        private Image DrawBackgroundImage(Image logo, int x, int y)
        {
            var bitmap = new Bitmap(layoutControlGroupBase.BackgroundImage, layoutControlGroupBase.BackgroundImage.Width, layoutControlGroupBase.BackgroundImage.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var bit = new Bitmap(logo))
                {
                    //bit.MakeTransparent(Color.Transparent);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(bit, x, y, 75, 75);
                }
            }
            return bitmap;
        }

        private Image DrawBackgroundImage(string name, int fontSize, int x, int y)
        {
            var bitmap = new Bitmap(layoutControlGroupBase.BackgroundImage, layoutControlGroupBase.BackgroundImage.Width, layoutControlGroupBase.BackgroundImage.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var rectX = x;
                var rectY = y;
                var rectWidth = name.Length * fontSize * 2;
                var rectHeight = fontSize * 2;
                var textArea = new RectangleF(rectX, rectY, rectWidth, rectHeight);
                using (var font = new Font(AppearanceObject.DefaultFont.Name, fontSize, FontStyle.Bold))
                {
                    using (var brush = new SolidBrush(Color.White))
                    {
                        graphics.DrawString(name, font, brush, textArea);
                    }
                }
            }
            return bitmap;
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
                    dxErrorProvider.SetError(textEditUserPwd, string.Format(Variables.MandatoryTips, "密码"));
                    textEditUserPwd.Focus();
                    return;
                }
                _account.Authenticate(new LoginModel
                {
                    TenancyName = Settings.Default.TenantName,
                    UsernameOrEmailAddress = name,
                    Password = pwd
                });
                Variables.User = _userAppService.GetUser(new EntityDto<long> { Id = -1 });
                var genericIdentity = new GenericIdentity(Variables.User.Id.ToString());
                var roles = new List<string>();
                splashScreenManager.SetWaitFormDescription(Variables.LoadingForPermission);
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
                if (checkEditRememberPwd.Checked)
                {
                    Settings.Default.RememberPwd = true;
                    Settings.Default.Password = pwd;
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default.RememberPwd = false;
                    //Settings.Default.UserName = string.Empty;
                    Settings.Default.Password = string.Empty;
                    Settings.Default.Save();
                }
                DialogResult = DialogResult.OK;
            });
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            Activate();
            textEditUserName.Focus();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}