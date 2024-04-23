using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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
    public partial class OccQuestionSymptom : UserBaseForm
    {
        private readonly IOccdiseaseConsulitationAppService _OccdiseaseConsulitationAppService=new OccdiseaseConsulitationAppService();
        public OccQustionSymptomrucan occpast = new OccQustionSymptomrucan();
        public OccQuestionSymptom()
        {
            InitializeComponent();
        }
        private readonly Guid _id;
        private readonly Guid Cusid;
        public OccQuestionSymptom(Guid id) : this()
        {
            Cusid = id;
        }

        private void OccQuestionSymptom_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            occpast = new OccQustionSymptomrucan();
            
            occpast.CustomerRegBMId = Cusid;
            occpast.Name = textEdit1.Text.Trim();
            occpast.Degree = textEdit2.Text.Trim();

            if (_id == Guid.Empty)
            {
                
                
               
                    DialogResult = System.Windows.Forms.DialogResult.OK;

               
            }
        }
    }
}
