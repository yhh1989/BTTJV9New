using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Users
{
    public partial class UserPassUpdate : UserBaseForm
    {
        private readonly IUserAppService _userAppService;
        public UserPassUpdate()
        {
            InitializeComponent();
            _userAppService = new UserAppService();
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            if (textEditOldPass.Text.Trim() == string.Empty)
            {
                dxErrorProvider.SetError(textEditOldPass, string.Format(Variables.MandatoryTips, "原密码"));
                textEditOldPass.Focus();
                return;
            }
            if (textEditNewPass.Text.Trim().Length > 100)
            {
                dxErrorProvider.SetError(textEditNewPass, "新密码位数超出最大长度100。");
                textEditNewPass.Focus();
                return;
            }
            if (textEditNewPass.Text.Trim() == string.Empty)
            {
                dxErrorProvider.SetError(textEditNewPass, string.Format(Variables.MandatoryTips, "新密码"));
                textEditNewPass.Focus();
                return;
            }
            if (textEditNewPass2.Text.Trim() == string.Empty)
            {
                dxErrorProvider.SetError(textEditNewPass2, string.Format(Variables.MandatoryTips, "确认新密码"));
                textEditNewPass.Focus();
                return;
            }
            if (textEditNewPass.Text.Trim() == textEditNewPass2.Text.Trim())
            {
                try
                {
                    //修改密码
                    _userAppService.UpdatePassword(new UpdatePwdDto { UserId = CurrentUser.Id, Password = textEditOldPass.Text.Trim(), NewPassword = textEditNewPass.Text.Trim(), ConfirmPassword = textEditNewPass2.Text.Trim() });
                    ShowMessageSucceed("修改成功");
                    this.DialogResult = DialogResult.OK;
                }
                catch (UserFriendlyException exception)
                {
                    ShowMessageBox(exception);
                }

            }
            else
            {
                dxErrorProvider.SetError(textEditNewPass, "两次新密码不一致。");
                textEditNewPass.Focus();
                return;
            }
        }
    }
}
