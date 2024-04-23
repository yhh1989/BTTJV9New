using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Users
{
    public partial class UserList : UserBaseForm
    {
        private readonly IFormRoleAppService _formRoleAppService;

        private readonly IUserAppService _userAppService;

        public UserList()
        {
            InitializeComponent();

            _userAppService = new UserAppService();
            _formRoleAppService = new FormRoleAppService();
            
            gridViewUsers.DataController.AllowIEnumerableDetails = true;
        }

        //新建
        private void simpleButtonNewUser_Click(object sender, EventArgs e)
        {
            using (var frm = new UserEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        //修改
        private void simpleButtonUpdate_Click(object sender, EventArgs e)
        {
            var userId = gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, gridColumnUserId);
            using (var frm = new UserEditor((long)userId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var result = gridControl.DataSource as List<UserFormDto>;
                    var curdata = gridViewUsers.GetFocusedRow() as UserFormDto;
                }
                   // LoadData();
            }
           
        }

        //删除
        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            var lId = gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, gridColumnUserId);
            try
            {
                var question = XtraMessageBox.Show("是否删除？", "询问",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (question != DialogResult.Yes)
                    return;


                var result = gridControl.DataSource as List<UserFormDto>;
                var curdata = gridViewUsers.GetFocusedRow() as UserFormDto;
                
                //AutoLoading(() =>
                //{
                    _userAppService.DeleteUser(new EntityDto<long> { Id = (long)lId });
                    
                //}, Variables.LoadingDelete);
                //LoadData();
                result.Remove(curdata);
                gridViewUsers.RefreshData();
                gridControl.RefreshDataSource();
            }
            catch (UserFriendlyException ex)
            {
                XtraMessageBox.Show("用户已使用不能删除！");
               //ShowMessageBox("用户已使用不能删除！");
                //ShowMessageBox(ex);
            }
        }

        private void simpleButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            var searchUserDto = new SearchUserDto();
            if (!string.IsNullOrWhiteSpace(tegh.Text.Trim()))
                searchUserDto.NameOrEmployeeNum = tegh.Text.Trim();
            //if (!string.IsNullOrWhiteSpace(teName.Text.Trim()))
            //    searchUserDto.Name = teName.Text.Trim();
            if (lookUpEditFormRole.EditValue != null)
                searchUserDto.FormRole = (Guid)lookUpEditFormRole.EditValue;
            LoadData(searchUserDto);
        }

        private void UserSetting_Load(object sender, EventArgs e)
        {
            tegh.Text = "";
            //teName.Text = "";
            lookUpEditFormRole.EditValue = null;
            LoadData();
        }

        private void lookUpEditFormRole_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookUpEditFormRole.EditValue = null;
                lookUpEditFormRole.ItemIndex = -1;
            }
        }

        private void LoadData()
        {
            try
            {
                var result = _userAppService.GetUsers();
                gridControl.DataSource = result;
                var formRoles = _formRoleAppService.GetAll();
                lookUpEditFormRole.Properties.DataSource = formRoles;
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }

        private void LoadData(SearchUserDto input)
        {
            try
            {
                var result = _userAppService.GetUsersByCondition(input);
                gridControl.DataSource = result;
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }

        private void tegh_KeyDown(object sender, KeyEventArgs e)
        {
            //!string.IsNullOrWhiteSpace(tegh.Text.Trim())
            if (e.KeyCode == Keys.Enter)
                simpleButtonQuery_Click(sender, e);
        }

        private void teName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(teName.Text.Trim()))
            if (e.KeyCode == Keys.Enter)
                simpleButtonQuery_Click(sender, e);
        }

        private void lookUpEditFormRole_KeyDown(object sender, KeyEventArgs e)
        {
            //if (lookUpEditFormRole.EditValue != null)
            if (e.KeyCode == Keys.Enter)
                simpleButtonQuery_Click(sender, e);
        }

        private void gridViewUsers_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridViewUsers.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    var userId = gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, gridColumnUserId);
                    using (var frm = new UserEditor((long)userId))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                            LoadData();
                    }
                }
            }
        }
    }
}