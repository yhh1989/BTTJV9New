using Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Foreground
{
    public partial class frmBloodQue : UserBaseForm
    {
        System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(BloodWorkstation));

        public frmBloodQue()
        {
            InitializeComponent();
        }

        private void frmBloodQue_Load(object sender, EventArgs e)
        {
            repositoryItemSearchLookUpEdit1.DataSource = DefinedCacheHelper.GetComboUsers();
            dateEditStar.DateTime = DateTime.Now;
            dateEditEnd.DateTime= DateTime.Now;
            searchLookUpEdit抽血人检索.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            //searchLookUpEdit抽血人检索.EditValue = CurrentUser.Id;
        }

        private void simpleButton查询按钮_Click(object sender, EventArgs e)
        {
            var input = new CustomerRegisterBarCodePrintInformationConditionInput();
          
            if (dateEditStar.EditValue != null)
            {
                input.StarBloodTime = dateEditStar.DateTime;
            }
            if (dateEditEnd.EditValue != null)
            {
                input.EndBloodTime = dateEditEnd.DateTime;
            }
            input.BloodUserId = searchLookUpEdit抽血人检索.EditValue as long?;
            input.AutoBlood = false;

               AutoLoading(() =>
            {
                var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                    .QueryBlood(input).Result;
                gridView2.FocusInvalidRow();

                gridControl1.DataSource = result;

                gridView2.FocusedRowHandle = 0;
            });
        }
    }
}
