using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseConsulitation
{
    public partial class OccQuesFamilyHistory : UserBaseForm
    {
        private readonly IOccdiseaseConsulitationAppService _OccdiseaseConsulitationAppService=new OccdiseaseConsulitationAppService();
        public OccQuestionFamilyrucan occpast = new OccQuestionFamilyrucan();
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService=new OccDisProposalNewAppService();
        public OccQuesFamilyHistory()
        {
            InitializeComponent();
        }
        private readonly Guid _id;
        private readonly Guid Cusid;
       

        private void OccQuesFamilyHistory_Load(object sender, EventArgs e)
        {
            //家族史疾病
            ChargeBM chargeBMs = new ChargeBM();
            chargeBMs.Name = ZYBBasicDictionaryType.MedicalHistory.ToString();
            var dlls = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBMs);
            lookUpEdit4.Properties.DataSource = dlls;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
             occpast = new OccQuestionFamilyrucan();
            
            occpast.CustomerRegBMId = OccdieaseConsultation.Cusid;
            occpast.IllName = lookUpEdit4.EditValue.ToString();
            occpast.relatives = textEdit2.Text.Trim();

            if (_id == Guid.Empty) { 
           
                    DialogResult = System.Windows.Forms.DialogResult.OK;
               
            }
        }

        private void lookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
