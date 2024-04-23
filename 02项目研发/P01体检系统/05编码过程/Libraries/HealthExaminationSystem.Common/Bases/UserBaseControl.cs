using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.BaseModule;
using Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Common.Bases
{
    public partial class UserBaseControl : XtraUserControl
    {
        public UserBaseControl()
        {
            InitializeComponent();
        }

        protected UserViewDto CurrentUser => Variables.User;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                if (permissionManager.Enabled && Variables.PermissionEnabled)
                {
                    //var formModuleAppService = new FormModuleAppService();
                    var formModules = DefinedCacheHelper.GetFormModules();
                    foreach (DictionaryEntry property in permissionManager.HashtableProperties)
                    {
                        var provide = (ProvidedProperties)property.Value;
                        if (provide.Enabled)
                            if (!string.IsNullOrWhiteSpace(provide.Id))
                                try
                                {
                                    //var result = formModuleAppService.GetByName(new NameDto { Name = provide.Id });
                                    var result = formModules.Find(r => r.Name == provide.Id);
                                    IPermission permission = null;
                                    foreach (var role in result.FormRoles)
                                        if (permission == null)
                                            permission = new PrincipalPermission(CurrentUser.Id.ToString(), role.Name);
                                        else
                                            permission =
                                                permission.Union(new PrincipalPermission(CurrentUser.Id.ToString(),
                                                    role.Name));

                                    if (permission != null)
                                    {
                                        try
                                        {
                                            permission.Demand();
                                        }
                                        catch (SecurityException)
                                        {
                                            if (property.Key is Control control)
                                            {
                                                control.Enabled = false;
                                                //control.Visible = provide.IsVisable;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (property.Key is Control control)
                                            control.Enabled = false;
                                    }
                                }
                                catch (UserFriendlyException exception)
                                {
                                    Console.WriteLine(exception);
                                }
                    }
                }
        }
    }
}