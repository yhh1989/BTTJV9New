using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
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
using WindowsFormsApp1.Model;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class HisSelect : UserBaseForm
    {
       public List<OutQueryPatientInfoBodyNode> cusList = new List<OutQueryPatientInfoBodyNode>();
        public OutQueryPatientInfoBodyNode NowCusInfo = new OutQueryPatientInfoBodyNode();
        public HisSelect()
        {
            InitializeComponent();
        }

        private void HisSelect_Load(object sender, EventArgs e)
        {   //挂号科室
            var ghks = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
            repositoryItemLookUpEdit2.DataSource = ghks;

            gridControl1.DataSource = cusList;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var data = gridView1.GetFocusedRow() as OutQueryPatientInfoBodyNode;
            if (data != null)
            {
                NowCusInfo = data;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请选择挂号信息");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
