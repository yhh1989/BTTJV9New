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
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using DevExpress.XtraLayout.Utils;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class FrmGiveUpMessage : UserBaseForm
    {
        private int state;

        private CustomerItemGroupDto ItemGroup=null;
        public DateTime? NextDate { get; set; }
        public string Remarks { get; set; }

        private ICrossTableAppService crossTableAppService;
        public FrmGiveUpMessage(int State)
        {
            state = State;
            InitializeComponent();
        }

        public FrmGiveUpMessage(int State, CustomerItemGroupDto itemGroup)
        {
            state = State;
            ItemGroup = itemGroup;
            crossTableAppService = new CrossTableAppService();
            InitializeComponent();
        }


        private void FrmGiveUpMessage_Load(object sender, EventArgs e)
        {
            if (state == (int)ProjectIState.Stay)
            {
                this.Text = "项目组待检报备";
                layoutControlItemDatetime.Visibility = LayoutVisibility.Always;
                dateEditNextTime.Properties.MinValue = DateTime.Now.AddDays(1);
            }
            else
            {
                this.Text = "项目组放弃报备";
                layoutControlItemDatetime.Visibility= LayoutVisibility.Never;
            }

            if (ItemGroup != null)
            {
                var result= crossTableAppService.QueryGiveUpInfo(new QueryGiveUpDto {
                    CustomerItemGroupId=ItemGroup.Id,
                    CustomerRegId=(Guid)ItemGroup.CustomerRegBMId
                });
                if (result != null)
                {
                    richTextBoxBeizhu.Text = result.remart;
                    dateEditNextTime.EditValue = result.stayDate;
                }
            }
        }

        private void simpleButtonQueren_Click(object sender, EventArgs e)
        {
            if(dateEditNextTime.EditValue!=null)
                NextDate = (DateTime)dateEditNextTime.EditValue;
            Remarks = richTextBoxBeizhu.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}