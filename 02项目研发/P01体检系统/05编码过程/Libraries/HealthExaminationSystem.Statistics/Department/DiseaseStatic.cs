using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Department
{
    public partial class DiseaseStatic : UserBaseForm
    {
        private readonly ICustomerAppService _CustomerAppService = new CustomerAppService();

        public DiseaseStatic()
        {
            InitializeComponent();
        }

        private void DiseaseStatic_Load(object sender, EventArgs e)
        {
            var result = _CustomerAppService.GetAbpTenants();
            comboBoxEdit1.Properties.DataSource = result;
            ClientTeamInfoDto a = new ClientTeamInfoDto();
            var results = _CustomerAppService.QueryClientTeamInfos(a);
            comboBoxEdit2.Properties.DataSource = results;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SickNessDto s = new SickNessDto();
            if (!string.IsNullOrWhiteSpace(comboBoxEdit1.Text.Trim()))
            {
                s.Name = comboBoxEdit1.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(comboBoxEdit2.Text.Trim()))
            {
                s.TeamName = comboBoxEdit2.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(textEdit2.Text.Trim()))
            {
                s.ItemName = textEdit2.Text.Trim();
            }
            if (dtpStart.EditValue != null)
                s.SatrtDateTime = dtpStart.DateTime;

            if (dtpEnd.EditValue != null)
                s.EndDateTime = dtpEnd.DateTime;

            if (s.SatrtDateTime >s.EndDateTime)
            {
                dxErrorProvider.SetError(dtpStart, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                dxErrorProvider.SetError(dtpEnd, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                return;
            }
            var results = _CustomerAppService.GetDiseaseAppServices(s);
            gridControl1.DataSource = results;
        }
    }
}
