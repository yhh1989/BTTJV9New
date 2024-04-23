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
    public partial class OccQuesPastHistoryEdit : UserBaseForm
    {
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService = new OccDisProposalNewAppService();
        private readonly IOccdiseaseConsulitationAppService _OccdiseaseConsulitationAppService=new OccdiseaseConsulitationAppService();
        private readonly Guid _id;
        public OccQuesPastHistoryDto occpast = new OccQuesPastHistoryDto();

        public OccQuesPastHistoryEdit()
        {
            InitializeComponent();
        }
        //private readonly Guid? Cusid;
        //public OccQuesPastHistoryEdit(Guid id) : this()
        //{
        //    Cusid = OccdieaseConsultation.Cusid;
        //}
        
        private void simpleButton6_Click(object sender, EventArgs e)

        {
         
            dxErrorProvider.ClearErrors();
            var cusids = OccdieaseConsultation.Cusid;
            ///OccQuestionPastAddrucan dto = new OccQuestionPastAddrucan();
            occpast = new OccQuesPastHistoryDto();
            occpast.CustomerRegBMId = cusids;
            occpast.DiagnTime = date.DateTime;            
            occpast.IllName = lookUpEdit4.Text.ToString();
            occpast.DiagnosisClient = textEdit1.Text;
            occpast.Treatment = this.method.Text.Trim();
            occpast.Iscured = this.ishave.SelectedIndex;
            occpast.DiagnosticCode = textEditDiagnosticCode.Text;
            if (_id == Guid.Empty)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
      

        private void isover_Load(object sender, EventArgs e)
        {
            var OccGets = new OccdieaseHistoryRucan();
            OccGets.Id = OccdieaseConsultation.Cusid;
            //var id = OccdieaseConsultation.;
            //var results = _OccdiseaseConsulitationAppService.GetAllOccupationHistory(OccGets);
            //治疗方式
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Treatment.ToString();
            var dll = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            method.Properties.DataSource = dll;

            //疾病
            ChargeBM chargeBMs = new ChargeBM();
            chargeBMs.Name = ZYBBasicDictionaryType.MedicalHistory.ToString();
            var dlls = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBMs);
             
            foreach (var yj in dlls)
            {
                lookUpEdit4.Items.Add(yj.Text);
            }
        }
       
    }
}
