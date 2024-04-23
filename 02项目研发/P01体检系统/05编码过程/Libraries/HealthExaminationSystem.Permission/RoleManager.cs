using System;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;

namespace Sw.Hospital.HealthExaminationSystem.Permission
{
    public partial class RoleManager : UserBaseForm
    {
        private readonly IFormRoleAppService _formRoleAppService;

        public RoleManager()
        {
            InitializeComponent();

            _formRoleAppService = new FormRoleAppService();
        }

        private void RoleManager_Load(object sender, System.EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {
                var result = _formRoleAppService.GetAllForView();
                gridControl.DataSource = result;
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }

        private void simpleButtonCreate_Click(object sender, System.EventArgs e)
        {
            using (var frm = new RoleEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void simpleButtonUpdate_Click(object sender, System.EventArgs e)
        {
            var id = gridViewFormRole.GetRowCellValue(gridViewFormRole.FocusedRowHandle, gridColumnFormRoleId);
            if (id == null)
            {
                ShowMessageBoxInformation("请先选择要修改的数据行!");
                return;
            }
            using (var frm = new RoleEditor((Guid)id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            var id = gridViewFormRole.GetRowCellValue(gridViewFormRole.FocusedRowHandle, gridColumnFormRoleId);
            var name = gridViewFormRole.GetRowCellValue(gridViewFormRole.FocusedRowHandle, gridColumnFormRoleName);
            if (id == null)
            {
                ShowMessageBoxInformation("请先选择要修改的数据行!");
                return;
            }

            if (XtraMessageBox.Show($"确定要删除 {name} 角色吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _formRoleAppService.Delete(new EntityDto<Guid> { Id = (Guid)id });
                LoadData();
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridViewFormRole_DoubleClick(object sender, EventArgs e)
        {
            simpleButtonUpdate.PerformClick();
        }
    }
}