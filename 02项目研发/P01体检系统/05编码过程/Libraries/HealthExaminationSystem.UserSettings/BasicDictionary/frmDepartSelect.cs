using DevExpress.XtraTreeList.Nodes;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary
{
    public partial class frmDepartSelect : UserBaseForm
    {
        public string  guids = "";
        public frmDepartSelect()
        {
            InitializeComponent();
        }
        public frmDepartSelect(string deptIds)
        {
            InitializeComponent();
            guids = deptIds;
        }

        private void frmDepartSelect_Load(object sender, EventArgs e)
        {
            treeListDepartments.DataSource = DefinedCacheHelper.GetDepartments();

            if (guids!="")
            {               

                foreach (TreeListNode node in treeListDepartments.Nodes)
                {
                    var deptname = node.GetValue(treeListColumnDepartmentName).ToString();
                    if (guids.Contains(deptname))
                    {
                        treeListDepartments.SetNodeCheckState(node, System.Windows.Forms.CheckState.Checked);
                    }
                }
            }


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string departnames = "";
            foreach (TreeListNode node in treeListDepartments.Nodes)
            {
                if (node.Checked)
                {
                    var deptname= (Guid)node.GetValue(treeListColumnDepartmentName);
                    departnames += deptname + ",";
                }
            }
            guids = departnames;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
