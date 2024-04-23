using Sw.Hospital.HealthExaminationSystem.ApiProxy.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OAApprovalSet
{
    public partial class FrmOAZKSet : UserBaseForm
    {

        private OAApprovalAppService oAApprovalAppService = new OAApprovalAppService();
        public CreateClientXKSetDto createClientXKSetDto = new CreateClientXKSetDto();
        public FrmOAZKSet()
        {
            InitializeComponent();
        }

        private void FrmOAZKSet_Load(object sender, EventArgs e)
        {
           

            AutoLoading(() =>
            {
                var OAApp = oAApprovalAppService.getCreateClientXKSet();
                if (OAApp != null)
                {
                    createClientXKSetDto = OAApp;
                    if (OAApp.ZKType.ToString() == "1")
                    {
                        radioGroup1.SelectedIndex = 0;
                    }
                    else
                    { radioGroup1.SelectedIndex = 1; }
                    textZK.EditValue = OAApp.DiscountRate;

                }
            });
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {


            if (createClientXKSetDto == null)
            {
                createClientXKSetDto = new CreateClientXKSetDto();
            }
            if (radioGroup1.EditValue == null)
            {
                MessageBox.Show("请选择折扣类型！");
                return;
            }          

            AutoLoading(() =>
            {
                createClientXKSetDto.ZKType = int.Parse(radioGroup1.EditValue.ToString());
                createClientXKSetDto.DiscountRate =  decimal.Parse(textZK.EditValue.ToString().Replace("%",""));
                oAApprovalAppService.creatCreateClientXKSet(createClientXKSetDto);
            });
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
