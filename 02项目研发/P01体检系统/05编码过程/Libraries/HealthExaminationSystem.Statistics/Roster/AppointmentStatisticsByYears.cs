using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Roster
{
    public partial class AppointmentStatisticsByYears : UserBaseForm
    {
        IClientInfoesAppService clientInfoesAppService = new ClientInfoesAppService();
        IClientRegAppService clientRegAppService = new ClientRegAppService();
        public AppointmentStatisticsByYears()
        {
            InitializeComponent();
            lueClient.SetClearButton();
        }

        private void AppointmentStatisticsByYears_Load(object sender, EventArgs e)
        {
            ReloadClient();
            deStartYear.DateTime = DateTime.Now.AddYears(-1);
            deEndYear.DateTime = DateTime.Now;
        }

        private void lueClient_Enter(object sender, EventArgs e)
        {
            if (lueClient.EditValue == null)
                ReloadClient();
        }
        private void sbReload_Click(object sender, EventArgs e)
        {
            Reload();
        }
        
        private void ReloadClient()
        {
            var list = clientInfoesAppService.Query(new ClientInfoesListInput());
            lueClient.Properties.DataSource = list;
        }

        private void Reload()
        {
            dxErrorProvider.ClearErrors();
            if (lueClient.EditValue == null)
            {
                dxErrorProvider.SetError(lueClient, string.Format(Variables.MandatoryTips, "单位"));
                lueClient.Focus();
                return;
            }

            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gridControl.DataSource = null;
            try
            {
                var input = new SearchClientRegStatisticsOverTheYearsDto
                {
                    ClientId = (Guid)lueClient.EditValue,
                    StartYear = deStartYear.DateTime.Year,
                    EndYear = deEndYear.DateTime.Year,
                };
                var output = clientRegAppService.QueryClientRegStatisticsOverTheYears(input);
                gridControl.DataSource = output;
                //gridView.BestFitColumns();
            }
            catch (ApiProxy.UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }
        }
    }
}
