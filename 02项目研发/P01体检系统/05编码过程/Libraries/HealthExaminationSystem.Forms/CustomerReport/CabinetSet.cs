using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class CabinetSet : UserBaseForm
    {
        private readonly ICustomerReportAppService customerReportAppService;
        private TbmCabinetDto tbmCabinetDto;
        public CabinetSet()
        {
            InitializeComponent();
            customerReportAppService = new CustomerReportAppService();
        }
        
        private void CabinetSet_Load(object sender, EventArgs e)
        {
            conHType.Properties.DataSource = CabinetHelper.GetIfTypeModels();
            conWType.Properties.DataSource= CabinetHelper.GetIfTypeModels();
            tbmCabinetDto = customerReportAppService.getTbmCabinet();
            if (tbmCabinetDto != null)
            {
                conHType.EditValue = tbmCabinetDto.HType;
                conWType.EditValue = tbmCabinetDto.WType;
                txtCount.EditValue = tbmCabinetDto.GCont;
                txtHNum.EditValue = tbmCabinetDto.HCont;
                txtWNum.EditValue = tbmCabinetDto.WCont;
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (tbmCabinetDto == null)
            {
                tbmCabinetDto = new TbmCabinetDto();
            }
           
            if (!string.IsNullOrWhiteSpace(conHType.EditValue?.ToString()))
            {
              
                tbmCabinetDto.HType = int.Parse(conHType.EditValue.ToString());
            }
            else
            {
                dxErrorProvider.SetError(conHType, string.Format(Variables.MandatoryTips, "标记类型"));
                conHType.Focus();
                return ;
            }
            if (!string.IsNullOrWhiteSpace(conWType.EditValue?.ToString()))
            {
                tbmCabinetDto.WType = (int)conWType.EditValue;
            }
            else
            {
                dxErrorProvider.SetError(conWType, string.Format(Variables.MandatoryTips, "标记类型"));
                conWType.Focus();
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtHNum.EditValue?.ToString()))
            {
                //var  ss= (int)txtHNum.EditValue;
                tbmCabinetDto.HCont = int.Parse( txtHNum.EditValue.ToString());
            }
            else
            {
                dxErrorProvider.SetError(txtHNum, string.Format(Variables.MandatoryTips, "行数"));
                txtHNum.Focus();
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtWNum.EditValue?.ToString()))
            {
                tbmCabinetDto.WCont = int.Parse(txtWNum.EditValue.ToString()); 
            }
            else
            {
                dxErrorProvider.SetError(txtWNum, string.Format(Variables.MandatoryTips, "列数"));
                txtWNum.Focus();
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtCount.EditValue?.ToString()))
            {
                tbmCabinetDto.GCont = int.Parse(txtCount.EditValue.ToString()); 
            }
            else
            {
                dxErrorProvider.SetError(txtCount, string.Format(Variables.MandatoryTips, "数量/格"));
                txtCount.Focus();
                return;
            }
           
            AutoLoading(() =>
            {
                var result = customerReportAppService.SaveTbmCabinet(tbmCabinetDto);
            });

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
