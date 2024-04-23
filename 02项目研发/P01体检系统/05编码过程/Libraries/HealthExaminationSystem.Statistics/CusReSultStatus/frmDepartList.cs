using DevExpress.XtraTreeList.Nodes;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusReSultStatus
{
    public partial class frmDepartList : UserBaseForm
    {
        public string DepartNames = "";       
        public List<Guid> GetGuids;
        List<TbmDepartmentDto> curItemls = new List<TbmDepartmentDto>();

        public frmDepartList()
        {
            InitializeComponent();
        }

        private void frmDepartList_Load(object sender, EventArgs e)
        {
            var departlis = DefinedCacheHelper.GetDepartments();

            curItemls = departlis.ToList();
            treeListDepartments.DataSource = departlis.OrderBy(n => n.Name).ToList();

            foreach (TreeListNode node in treeListDepartments.Nodes)
            {
                var id = (Guid)node.GetValue(treeListColumnDepartmentId);
                if (DepartNames.Contains(id.ToString()))
                {
                    treeListDepartments.SetNodeCheckState(node, System.Windows.Forms.CheckState.Checked);
                }
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var departments = new List<Guid>();
            foreach (TreeListNode node in treeListDepartments.Nodes)
            {
                if (node.Checked)
                {
                    var id = (Guid)node.GetValue(treeListColumnDepartmentId);
                    departments.Add(id);
                }
            }
           
            GetGuids = departments;
            this.DialogResult = DialogResult.OK;
            //this.Close();
            

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
       {
            if (searchControl1.Text != "")
            {
                var strup = searchControl1.Text.ToUpper();
                var output = new List<TbmDepartmentDto>();
                output = curItemls.Where(o => o.Name.Contains(searchControl1.Text) || o.HelpChar.Contains(strup)).ToList();

                treeListDepartments.DataSource = output;
            }
            else
            {
                treeListDepartments.DataSource = curItemls;
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            SelectTreeListAll(treeListDepartments, checkEdit1.Checked);
        }
        /// <summary>
        /// 全选树
        /// </summary>
        /// <param name="tree">树控件</param>
        /// <param name="nodes">节点集合</param>
        public virtual void SelectTreeListAll(DevExpress.XtraTreeList.TreeList tree, bool ISChek)
        {

            foreach (TreeListNode item in tree.Nodes)
            {
                item.Checked = ISChek;

            }
            tree.Refresh();
        }
    }
}
