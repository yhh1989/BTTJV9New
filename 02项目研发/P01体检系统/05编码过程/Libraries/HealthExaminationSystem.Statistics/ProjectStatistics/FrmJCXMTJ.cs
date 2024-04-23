using System;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.ProjectStatistics
{
    /// <summary>
    /// 检查项目统计
    /// </summary>
    public partial class FrmJCXMTJ : UserBaseForm
    {
        private IUserAppService _Userservice;

        private readonly IClientInfoesAppService _ClientInfoesAppService;

        private readonly DoctorStationAppService app = new DoctorStationAppService();

        private readonly DepartmentAppService DepartmentApp = new DepartmentAppService();

        private readonly CommonAppService dtApp = new CommonAppService();

        private readonly ItemGroupAppService ItemGroupApp = new ItemGroupAppService();



        public FrmJCXMTJ()
        {
            InitializeComponent();
            _ClientInfoesAppService = new ClientInfoesAppService();
        }

        public IUserAppService Userservice
        {
            get
            {
                if (_Userservice == null) _Userservice = new UserAppService();
                return _Userservice;
            }
        }

        private void FrmJCXMTJ_Load(object sender, EventArgs e)
        {
            //BindUser();
            BindDepartment();
            BindItemGroups();
            dgv.IndicatorWidth = 50;
            dt_Starte.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd") +" 00:00:00";
            dt_End.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd") + " 23:59:59";
            //sechClientName.Properties.DataSource = _ClientInfoesAppService.Query(new ClientInfoesListInput());
            sechClientName.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();

            //登记状态
            lUpDJState.Properties.DataSource = RegisterStateHelper.GetSelectList();
            lUpDJState.ItemIndex = 1;

          var checkstate=  ProjectIStateHelper.GetProjectIStateModels();
            lookChecksdate.Properties.DataSource = checkstate;
        }

        /// <summary>
        /// 用户加载
        /// </summary>
        public void BindUser()
        {
            var UsersListOutputDto = Userservice.GetUsers();
            if (UsersListOutputDto != null && UsersListOutputDto.Count > 0)
            {
                cbo_ysxm.Properties.DataSource = UsersListOutputDto;
                cbo_ysxm.Properties.ValueMember = "Name";
                cbo_ysxm.Properties.DisplayMember = "Name";
            }
        }

        /// <summary>
        /// 科室加载
        /// </summary>
        public void BindDepartment()
        {
            cbo_ksmc.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            cbo_ksmc.Properties.ValueMember = "Name";
            cbo_ksmc.Properties.DisplayMember = "Name";
        }

        /// <summary>
        /// 项目组合加载
        /// </summary>
        public void BindItemGroups()
        {
            cbo_xmzh.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
            cbo_xmzh.Properties.ValueMember = "ItemGroupName";
            cbo_xmzh.Properties.DisplayMember = "ItemGroupName";
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }

            try
            {
                var QC = new StatisticalClass();

                //医生
                //if (!string.IsNullOrWhiteSpace(cbo_ysxm.EditValue.ToString()))
                //{
                //    var str = cbo_ysxm.EditValue?.ToString()?.Split(',');
                //    if (str != null)
                //    {
                //        var list = new List<string>();
                //        foreach (var item in str)
                //            list.Add(item.Trim());

                //        QC.BillingEmployeeBM = list;
                //    }
                //}

                //科室

                var clientName = sechClientName.EditValue?.ToString();
                if (!string.IsNullOrWhiteSpace(clientName))
                    QC.ClientInfoReg_Id = Guid.Parse(clientName);

                if (!string.IsNullOrWhiteSpace(cbo_ksmc.EditValue?.ToString()))
                {
                    var str = cbo_ksmc.EditValue?.ToString()?.Split(',');
                    if (str != null)
                    {
                        var list = new List<string>();
                        foreach (var item in str)
                            list.Add(item.Trim());

                        QC.DepartmentName = list;
                    }
                }

                //项目组合
                if (!string.IsNullOrWhiteSpace(cbo_xmzh.EditValue?.ToString()))
                {
                    var str = cbo_xmzh.EditValue?.ToString()?.Split(',');
                    if (str != null)
                    {
                        var list = new List<string>();
                        foreach (var item in str)
                            list.Add(item.Trim());

                        QC.ItemGroupName = list;
                    }
                }
                if (comboBoxEdit1.Text.Contains("检查日期"))
                { QC.TimeState = 1; }
                else
                { QC.TimeState = 0; }

                //开始时间
                if (!string.IsNullOrWhiteSpace(dt_Starte.Text))
                {
                    QC.StartTime = dt_Starte.DateTime;
                }
                  

                //结束时间
                if (!string.IsNullOrWhiteSpace(dt_End.Text))
                {
                   // QC.EndTime = dt_End.DateTime.AddDays(1);
                    QC.EndTime = dt_End.DateTime;
                }
                if (lookChecksdate.EditValue != null)
                {
                    QC.CheckState = (int)lookChecksdate.EditValue;

                }
                if (lUpDJState.EditValue != null && !lUpDJState.Text.Contains("全部"))
                {
                    QC.RegistState = (int)lUpDJState.EditValue;
                }
                var result = app.GettheCheckItemStatistics(QC);
                dgc.DataSource = result;
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
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            dgv.CustomExport("检查项目统计");
        }

        private void dgv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void lookChecksdate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lookChecksdate.EditValue = null;
            }
        }
    }
}