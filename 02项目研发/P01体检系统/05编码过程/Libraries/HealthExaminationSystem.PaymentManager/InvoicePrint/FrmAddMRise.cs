using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Application.MRise;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.MRise;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.PaymentManager.InvoicePrint
{
    public partial class FrmAddMRise : UserBaseForm
    {
        private IMRiseAppService _mRiseAppService;
        private CommonAppService commonAppService;
        public MRiseDto dto = null;
        public FrmAddMRise()
        {
            InitializeComponent();
            _mRiseAppService = new MRiseAppService();
            commonAppService = new CommonAppService();
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var shuihao = textEditShuihao.Text;
            var name = textEditName.Text;
            if (string.IsNullOrWhiteSpace(shuihao))
            {
                dxErrorProvider.SetError(textEditShuihao, string.Format(Variables.MandatoryTips, "税号"));
                return;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                dxErrorProvider.SetError(textEditName, string.Format(Variables.MandatoryTips, "抬头名称"));
                return;
            }
            dto = new MRiseDto();
            dto.Name = name;
            dto.Duty = shuihao;
            dto.HelpChar = textEditHelpChar.Text;
            dto.WBCode = textEditWubima.Text;
            DialogResult = DialogResult.OK;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textEditName_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textEditName.Text.Trim()))
            {
                textEditHelpChar.Text = string.Empty;
                return;
            }
            ChineseDto input = new ChineseDto();
            input.Hans = textEditName.Text.Trim();
            textEditHelpChar.Text = commonAppService.GetHansBrief(input).Brief;
        }
    }
}