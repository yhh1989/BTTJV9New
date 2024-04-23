using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OConDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.RiskFactor;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    public partial class DictionariesSet : UserBaseForm
    {
        private IOConDictionaryAppService _conDictionaryAppService { get; set; }
        public ZYBTypeDto zybTypeDto;
        private readonly ICommonAppService _commonAppService;
        public DictionariesSet()
        {
            InitializeComponent();
            _conDictionaryAppService = new OConDictionaryAppService();
            _commonAppService = new CommonAppService();
        }

        private void DictionariesSet_Load(object sender, EventArgs e)
        {
            if (zybTypeDto != null)
            {
                textEditName.EditValue = zybTypeDto.Name; 
                textEditWorkNamejp.EditValue = zybTypeDto.HelpChar;
                comtype.EditValue= zybTypeDto.TypeName;            
            }
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            var name = textEditName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                dxErrorProvider.SetError(textEditName, "名称为必填项！");
                textEditName.Focus();
                return;
            }
            var nameJp = textEditWorkNamejp.Text.Trim();
            if (zybTypeDto == null)
            {
                zybTypeDto = new ZYBTypeDto();
            }
            zybTypeDto.Name = name;
            zybTypeDto.TypeName = comtype.Text;
            zybTypeDto.HelpChar = nameJp;
            _conDictionaryAppService.ZYBTypeInsert(zybTypeDto);
            //AutoLoading(() =>
            //{

            //});
            DialogResult = DialogResult.OK;
           
        }

        private void textEditName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textEditName.Text))
            {
                var input = new ChineseDto();
                input.Hans = textEditName.Text.Trim();
                textEditWorkNamejp.Text = _commonAppService.GetHansBrief(input).Brief;
            }
        }

        private void simpleButtonOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
