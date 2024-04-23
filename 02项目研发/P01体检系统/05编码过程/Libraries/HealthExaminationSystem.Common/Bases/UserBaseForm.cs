using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Common.Bases
{
    /// <summary>
    /// 体检系统用户窗体基类
    /// </summary>
    public partial class UserBaseForm : XtraForm
    {
        /// <summary>
        /// 初始化用户窗体
        /// </summary>
        public UserBaseForm()
        {
            InitializeComponent();

            TotalPages = 0;

            CurrentPage = 1;

            // 确定设计模式与运行模式
            // DesignMode 在构造函数里不起作用
            if (GetService(typeof(IDesignerHost)) != null || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                // 设计模式
            }
            else
            {
                // 运行模式
                splashScreenManager.ShowWaitForm();
            }

            // 简单判断设计模式与运行模式
            //if (!System.Windows.Forms.Application.ExecutablePath.EndsWith("devenv.exe"))
            //{
            //    splashScreenManager.ShowWaitForm();
            //}
        }

        /// <summary>
        /// 总页数
        /// </summary>
        protected int TotalPages { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        protected int CurrentPage { get; set; }

        /// <summary>
        /// 当前登陆用户信息
        /// </summary>
        protected UserViewDto CurrentUser => Variables.User;

        // 重写窗体加载
        protected override void OnLoad(EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                if (!DesignMode)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.SetWaitFormDescription(Variables.LoadingForForm);
                }

                base.OnLoad(e);
                stopwatch.Stop();
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("****************************分 割 线****************************");
                stringBuilder.AppendLine($"窗体 Type：{GetType().FullName}");
                stringBuilder.AppendLine($"窗体 名称：{Text}");
                stringBuilder.AppendLine($"窗体 Load 事件耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
                try
                {
                    File.AppendAllText(
                        Path.Combine(Variables.LogDirectory, $"FormTakesLog-{DateTime.Now:yyyyMMdd}.txt"),
                        stringBuilder.ToString());
                }
                catch
                {
                    // ignored
                }
            }
            catch (UserFriendlyException userFriendlyException)
            {
                ShowMessageBox(userFriendlyException);
            }
            catch (Exception exception)
            {
                // 如果调用子窗体的 Load 事件出现异常
                // 确保可以关闭 Loading 层
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.SetWaitFormDescription(Variables.LoadingForError);
                    splashScreenManager.CloseWaitForm();
                }
                Console.WriteLine(exception);
                throw;
            }

            if (!DesignMode)
            {
                if (splashScreenManager.IsSplashFormVisible)
                    splashScreenManager.SetWaitFormDescription(Variables.LoadingForPermission);
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
                                    {
                                        if (permission == null)
                                            permission = new PrincipalPermission(CurrentUser.Id.ToString(), role.Name);
                                        else
                                            permission =
                                                permission.Union(new PrincipalPermission(CurrentUser.Id.ToString(),
                                                    role.Name));
                                    }

                                    if (permission != null)
                                    {
                                        try
                                        {
                                            permission.Demand();
                                        }
                                        catch (SecurityException)
                                        {
                                            if (property.Key is Control control)
                                                control.Enabled = false;
                                            else if (property.Key is LayoutControlItem pcn)
                                            {
                                                //var pcn = (LayoutControlItem)control;
                                                pcn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                                            }
                                        }
                                    }
                                    else
                                     {
                                        if (property.Key is Control control)
                                        {
                                            control.Enabled = false;
                                           
                                        }
                                        else if (property.Key is LayoutControlItem pcn)
                                        {
                                            //var pcn = (LayoutControlItem)control;
                                            pcn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                                        }
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

      

        /// <summary>
        /// 显示<see cref="UserFriendlyException" />弹窗
        /// </summary>
        /// <param name="e">弹窗信息</param>
        protected void ShowMessageBox(UserFriendlyException e)
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
            XtraMessageBox.Show(this, e.Description, e.Code.ToString(), e.Buttons, e.Icon);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="message">需要提示的内容</param>
        protected void ShowMessageBoxInformation(string message)
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
            XtraMessageBox.Show(this, message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">需要提示的内容</param>
        protected void ShowMessageBoxError(string message)
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
            XtraMessageBox.Show(this, message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">需要提示的内容</param>
        protected void ShowMessageBoxWarning(string message)
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
            XtraMessageBox.Show(this, message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 成功消息
        /// </summary>
        /// <param name="message">需要提示的内容</param>
        protected void ShowMessageSucceed(string message)
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
            XtraMessageBox.Show(this, message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 重写窗体第一次显示
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!DesignMode)
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.SetWaitFormDescription(Variables.LoadingForEnd);
                    splashScreenManager.CloseWaitForm();
                }
        }

        /// <summary>
        /// 首页
        /// </summary>
        protected void First(DataNavigator navigator)
        {
            CurrentPage = 1;
        }

        /// <summary>
        /// 尾页
        /// </summary>
        protected void Last(DataNavigator navigator)
        {
            CurrentPage = TotalPages;
        }

        /// <summary>
        /// 下一页
        /// </summary>
        protected void NextPage(DataNavigator navigator)
        {
            CurrentPage = CurrentPage + 1;
        }

        /// <summary>
        /// 上一页
        /// </summary>
        protected void PrevPage(DataNavigator navigator)
        {
            CurrentPage = CurrentPage - 1;
        }

        /// <summary>
        /// 初始化导航
        /// </summary>
        /// <param name="navigator"></param>
        protected void InitialNavigator(DataNavigator navigator)
        {
            if (navigator.DataSource == null)
            {
                var binding = new BindingList<string> { "1", "2", "3" };
                navigator.DataSource = binding;
                navigator.Position = 1;
            }

            if (TotalPages == 0 || TotalPages == 1)
            {
                navigator.Buttons.First.Enabled = false;
                navigator.Buttons.PrevPage.Enabled = false;
                navigator.Buttons.NextPage.Enabled = false;
                navigator.Buttons.Last.Enabled = false;
            }
            else
            {
                if (CurrentPage == 1)
                {
                    navigator.Buttons.First.Enabled = false;
                    navigator.Buttons.PrevPage.Enabled = false;
                    navigator.Buttons.NextPage.Enabled = true;
                    navigator.Buttons.Last.Enabled = true;
                }
                else if (CurrentPage == TotalPages)
                {
                    navigator.Buttons.First.Enabled = true;
                    navigator.Buttons.PrevPage.Enabled = true;
                    navigator.Buttons.NextPage.Enabled = false;
                    navigator.Buttons.Last.Enabled = false;
                }
                else
                {
                    navigator.Buttons.First.Enabled = true;
                    navigator.Buttons.PrevPage.Enabled = true;
                    navigator.Buttons.NextPage.Enabled = true;
                    navigator.Buttons.Last.Enabled = true;
                }
            }

            navigator.TextStringFormat = string.Format(Variables.PageFormat, CurrentPage, TotalPages);
        }

        protected void DataNavigatorButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            e.Handled = true;
            var navigator = (DataNavigator)sender;
            if (e.Button.ButtonType == NavigatorButtonType.First)
                First(navigator);
            else if (e.Button.ButtonType == NavigatorButtonType.Last)
                Last(navigator);
            else if (e.Button.ButtonType == NavigatorButtonType.NextPage)
                NextPage(navigator);
            else if (e.Button.ButtonType == NavigatorButtonType.PrevPage)
                PrevPage(navigator);
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

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.ExStyle |= 0x02000000;
                }
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014)
                return;
            base.WndProc(ref m);
        }

        private void UserBaseForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 处理按钮点击时先禁用再启用
        /// </summary>
        /// <param name="sender">
        /// 已支持 Button
        /// <para><see cref="SimpleButton"/></para>
        /// <para><see cref="EditorButton"/></para>
        /// <para><see cref="BarButtonItem"/></para>
        /// </param>
        /// <param name="method"></param>
        protected void ButtonEnabledPropertySynchronize(object sender, Action method)
        {
            if (sender is SimpleButton button)
            {
                try
                {
                    button.Enabled = false;
                    method.Invoke();
                }
                finally
                {
                    button.Enabled = true;
                }
            }
            else if (sender is EditorButton button1)
            {
                try
                {
                    button1.Enabled = false;
                    method.Invoke();
                }
                finally
                {
                    button1.Enabled = true;
                }
            }
            else if (sender is BarButtonItem button2)
            {
                try
                {
                    button2.Enabled = false;
                    method.Invoke();
                }
                finally
                {
                    button2.Enabled = true;
                }
            }
            else
            {
                method.Invoke();
            }
        }
    }
}