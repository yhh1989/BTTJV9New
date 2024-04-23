using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;

namespace Sw.Hospital.HealthExaminationSystem.Permission
{
    public partial class RoleEditor : UserBaseForm
    {
        private readonly IFormRoleAppService _formRoleAppService;

        private readonly Guid _id;

        public RoleEditor()
        {
            InitializeComponent();

            _formRoleAppService = new FormRoleAppService();
        }

        public RoleEditor(Guid id) : this()
        {
            _id = id;
        }

        private void RoleEditor_Load(object sender, System.EventArgs e)
        {
            LoadData();
            if (_id != Guid.Empty)
            {
                InitialUpdate();
            }
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
                var result = _formRoleAppService.GetAllFormModules();
                if (Variables.ISZYB != "1" && Variables.ISZYB != "2")
                {
                    result = result.Where(o=>!o.Nickname.Contains("职业健康")).ToList();
                }
                splashScreenManager.SetWaitFormDescription(Variables.LoadingForForm);
                layoutControlGroupBase.BeginUpdate();
                foreach (var formModuleViewDto in result)
                {
                    if (formModuleViewDto.TypeName == Variables.MenuType)
                    {
                       
                        var checkEdit = new CheckEdit
                        {
                            Text = formModuleViewDto.Nickname
                        };
                        var item = layoutControlGroupMenus.AddItem("", checkEdit);
                        item.Name = formModuleViewDto.Id.ToString("N");
                        item.TextVisible = false;
                        item.Move(emptySpaceItemMenus, InsertType.Top);
                    }
                    else if (formModuleViewDto.TypeName == Variables.ButtonType)
                    {
                        var checkEdit = new CheckEdit
                        {
                            Text = formModuleViewDto.Nickname
                        };
                        var item = layoutControlGroupButtons.AddItem("", checkEdit);
                        item.Name = formModuleViewDto.Id.ToString("N");
                        item.TextVisible = false;
                        item.Move(emptySpaceItemButtons, InsertType.Top);
                    }
                }

                layoutControlGroupBase.EndUpdate();
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

        private void InitialUpdate()
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
                var result = _formRoleAppService.GetById(new EntityDto<Guid> { Id = _id });
                splashScreenManager.SetWaitFormDescription(Variables.LoadingEditor);
                var modules = result.FormModules;
                foreach (var module in modules)
                {
                    if (module.TypeName == Variables.MenuType)
                    {
                        try
                        {
                            var name = module.Id.ToString("N");
                            var item = layoutControlGroupMenus.Items.FindByName(name);
                            var layoutControlItem = (LayoutControlItem)item;
                            if (layoutControlItem != null)
                            {
                                ((CheckEdit)layoutControlItem.Control).Checked = true;
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                    else if (module.TypeName == Variables.ButtonType)
                    {
                        var name = module.Id.ToString("N");
                        var item = layoutControlGroupButtons.Items.FindByName(name);
                        var layoutControlItem = (LayoutControlItem)item;
                        ((CheckEdit)layoutControlItem.Control).Checked = true;
                    }
                }

                textEditRoleName.Text = result.Name;
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

        private void simpleButtonSave_Click(object sender, System.EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingReadyUpdate);
            try
            {
                dxErrorProvider.ClearErrors();
                var name = textEditRoleName.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    dxErrorProvider.SetError(textEditRoleName, string.Format(Variables.MandatoryTips, "角色名称"));
                    return;
                }

                var modules = new List<Guid>();
                foreach (BaseLayoutItem item in layoutControlGroupMenus.Items)
                {
                    if (item is EmptySpaceItem)
                    {
                        continue;
                    }
                    if (item is LayoutControlItem layoutControlItem)
                    {
                        if (((CheckEdit)layoutControlItem.Control).Checked)
                        {
                            modules.Add(new Guid(layoutControlItem.Name));
                        }
                    }
                }
                foreach (BaseLayoutItem item in layoutControlGroupButtons.Items)
                {
                    if (item is EmptySpaceItem)
                    {
                        continue;
                    }
                    if (item is LayoutControlItem layoutControlItem)
                    {
                        if (((CheckEdit)layoutControlItem.Control).Checked)
                        {
                            modules.Add(new Guid(layoutControlItem.Name));
                        }
                    }
                }

                if (_id == Guid.Empty)
                {
                    splashScreenManager.SetWaitFormDescription(Variables.LoadingSaveing);
                    var create = new CreateFormRoleDto
                    {
                        Name = name,
                        Modules = modules
                    };
                    _formRoleAppService.Create(create);
                }
                else
                {
                    splashScreenManager.SetWaitFormDescription(Variables.LoadingUpdate);
                    var update = new UpdateFormRoleDto
                    {
                        Name = name,
                        Modules = modules,
                        Id = _id
                    };
                    _formRoleAppService.Update(update);
                }

                DialogResult = DialogResult.OK;
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
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
    }
}