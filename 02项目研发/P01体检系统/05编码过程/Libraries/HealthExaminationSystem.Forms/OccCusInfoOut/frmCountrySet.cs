using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccNationalDisease;
using Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.OccCusInfoOut
{
    public partial class frmCountrySet : UserBaseForm
    {
        private OccNationalDiseaseAppService occNationalDiseaseAppService = new OccNationalDiseaseAppService();
        public frmCountrySet()
        {
            InitializeComponent();
        }

        private void frmCountrySet_Load(object sender, EventArgs e)
        {
            

            var country = occNationalDiseaseAppService.GetCountry();
            if (country != null)
            {
                textLicense.Text = country.License;
                textReportZoneCode.Text = country.ReportZoneCode;
                textReportOrgCode.Text = country.ReportOrgCode;

                textReportPeson.Text = country.ReportPeson;
                textReportPesonTel.Text = country.ReportPesonTel;
                textReportUnit.Text = country.ReportUnit;

                textWritePeson.Text = country.WritePeson;
                textWritePesonTel.Text = country.WritePesonTel;
                textWriteUnit.Text = country.WriteUnit;

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(textLicense.Text))
            //{
            //    dxErrorProvider.SetError(textLicense, string.Format(Variables.MandatoryTips, "上报机构授权"));
            //    textLicense.Focus();
            //    return  ;
            //}
            if (string.IsNullOrWhiteSpace(textReportZoneCode.Text))
            {
                dxErrorProvider.SetError(textReportZoneCode, string.Format(Variables.MandatoryTips, "上报地区代码"));
                textReportZoneCode.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textReportOrgCode.Text))
            {
                dxErrorProvider.SetError(textReportOrgCode, string.Format(Variables.MandatoryTips, "上报机构代码"));
                textReportOrgCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textReportPeson.Text))
            {
                dxErrorProvider.SetError(textReportPeson, string.Format(Variables.MandatoryTips, "报告人姓名"));
                textReportPeson.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textReportPesonTel.Text))
            {
                dxErrorProvider.SetError(textReportPesonTel, string.Format(Variables.MandatoryTips, "报告人电话"));
                textReportPesonTel.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textReportUnit.Text))
            {
                dxErrorProvider.SetError(textReportUnit, string.Format(Variables.MandatoryTips, "报告人单位"));
                textReportUnit.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textWritePesonTel.Text))
            {
                dxErrorProvider.SetError(textWritePesonTel, string.Format(Variables.MandatoryTips, "填表人电话"));
                textWritePesonTel.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textWriteUnit.Text))
            {
                dxErrorProvider.SetError(textWriteUnit, string.Format(Variables.MandatoryTips, "填表人单位"));
                textWriteUnit.Focus();
                return;
            }
            CreateCountrySet createCountrySet = new CreateCountrySet();
            createCountrySet.License = textLicense.Text;
            createCountrySet.ReportOrgCode = textReportOrgCode.Text;
            createCountrySet.ReportPeson = textReportPeson.Text;
            createCountrySet.ReportPesonTel = textReportPesonTel.Text;
            createCountrySet.ReportUnit = textReportUnit.Text;
            createCountrySet.ReportZoneCode = textReportZoneCode.Text;
            createCountrySet.WritePeson = textWritePeson.Text;
            createCountrySet.WritePesonTel = textWritePesonTel.Text;
            createCountrySet.WriteUnit = textWriteUnit.Text;
            var country = occNationalDiseaseAppService.SaveCountry(createCountrySet);
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
