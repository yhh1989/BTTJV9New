using DevExpress.XtraTreeList.Nodes;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
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
    public partial class frmItemGroupList : UserBaseForm
    {
        public string ItemGroupNames = "";
        public List<Guid> GetGuids;
        List<SimpleItemGroupDto> curItemls = new List<SimpleItemGroupDto>();
        public frmItemGroupList()
        {
            InitializeComponent();
        }

        private void frmItemGroupList_Load(object sender, EventArgs e)
        {
            var departlis = DefinedCacheHelper.GetItemGroups();
            curItemls = departlis.ToList();
            treeListDepartments.DataSource = departlis.OrderBy(n => n.ItemGroupName).ToList();

            foreach (TreeListNode node in treeListDepartments.Nodes)
            {
                var id = (Guid)node.GetValue(treeListColumnItemGroupId);
                if (ItemGroupNames.Contains(id.ToString()))
                {
                    treeListDepartments.SetNodeCheckState(node, System.Windows.Forms.CheckState.Checked);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var ItemGroups = new List<Guid>();
            foreach (TreeListNode node in treeListDepartments.Nodes)
            {
                if (node.Checked)
                {
                    var id = (Guid)node.GetValue(treeListColumnItemGroupId);
                    ItemGroups.Add(id);
                }
            }
           // ReSultSet reSultSet = new ReSultSet();
            GetGuids = ItemGroups;
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
                var output = new List<SimpleItemGroupDto>();
                output = curItemls.Where(o => o.ItemGroupName.Contains(searchControl1.Text) || o.HelpChar.Contains(strup)).ToList();
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
