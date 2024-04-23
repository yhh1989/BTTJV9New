using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sw.Hospital.HealthExaminationSystem.Statistics.Charge.FinancialStatement;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.DiseaseStatistics
{
    public partial class frmIllState : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;
        private  readonly DiagnosisAppService _diagnosisAppService;
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }

        private List<GridColumn> OldColumns = new List<GridColumn>();
        public frmIllState()
        {
            _commonAppService = new CommonAppService();
            _diagnosisAppService = new DiagnosisAppService();
            CustomColumns = new Dictionary<string, CustomColumnValue>();
            InitializeComponent();
        }

        private void frmIllState_Load(object sender, EventArgs e)
        {
          
            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now;
            dtEnd.DateTime = date;
            dtStar.DateTime = date;

            
            checkedItem.Properties.DataSource = DefinedCacheHelper.GetItemInfos();
            checkedItem.Properties.DisplayMember = "Name";
            checkedItem.Properties.ValueMember = "Id";
            sleDW.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            InitTreeFunction( "");
        }
        #region 初始化树
        /// <summary>
        /// 为了提高速度，第一次需要构建功能树节点
        /// </summary>
        private void InitTreeFunction(string strname)
        {


            this.treeFunction.Nodes.Clear();
            treeFunction.CheckBoxes = true;

            //初始化全部功能树
            TreeNode parentNode = this.treeFunction.Nodes.Add("", "项目名称");
           // var itemlist = DefinedCacheHelper.GetItemInfos();

            var depr = DefinedCacheHelper.GetItemInfos().Where(p=>p.Department !=null).Select(p => p.Department.Id).Distinct().ToList();
            foreach (var typeInfo in depr)
            {
                var deparName = DefinedCacheHelper.GetItemInfos().Where(p => p.Department != null).FirstOrDefault(p => p.Department.Id == typeInfo);
                TreeNode ptNode = new TreeNode(deparName.Department.Name);
                parentNode.Nodes.Add(ptNode);
                var itemslist= DefinedCacheHelper.GetItemInfos().Where(p => p.Department != null).Where(p => p.Department.Id== typeInfo).ToList();
                AddFunctionNode(ptNode, itemslist, strname);
            }
            treeFunction.Nodes[0].Expand();
            treeFunction.SelectedNode = treeFunction.Nodes[0];
            treeFunction.Focus();

            this.treeFunction.EndUpdate();
        }
        #endregion

        #region 初始化功能树
        /// <summary>
        /// 初始化功能树
        /// </summary>
        private void AddFunctionNode(TreeNode node, List<ItemInfoSimpleDto> drs, string strname)
        {
         
            foreach (var  info in drs)
            {
                if (info.Name.Contains(strname) || strname == "")
                {
                    TreeNode subNode = new TreeNode(info.Name.ToString());
                    subNode.Tag = info.Id.ToString();
                    node.Nodes.Add(subNode);
                }

            }
        }
        #endregion
        private void butSearch_Click(object sender, EventArgs e)
        {
            if (CustomColumns != null && CustomColumns.Count > 0)
            {
                foreach (var con in OldColumns)
                {
                    if (gridView1.Columns.Contains(con))
                    {
                        gridView1.Columns.Remove(con);
                    }
                }
                OldColumns.Clear();
            }
            SearchItem searchItem = new SearchItem();

            if (!string.IsNullOrEmpty(dtStar.EditValue?.ToString()))
            {
                searchItem.StartDateTime = dtStar.DateTime;
            }
            if (!string.IsNullOrEmpty(dtEnd.EditValue?.ToString()))
            {
                searchItem.EndDateTime = dtEnd.DateTime;
            }
            if (!string.IsNullOrEmpty(sleDW.EditValue?.ToString()))
            {
                searchItem.ClientRegId = (Guid)sleDW.EditValue;
            }
            //if (!string.IsNullOrEmpty(checkedItem.EditValue?.ToString()))
            //{
            //    var ItemStr = checkedItem.EditValue?.ToString();
            //    var itemIdlist = ItemStr.Split(',').ToList();
            //    List<Guid> ItmIDlist = new List<Guid>();
            //    foreach (var ItmID in itemIdlist)
            //    {
            //        if (!string.IsNullOrEmpty(ItmID))
            //        {
            //            ItmIDlist.Add(Guid.Parse(ItmID));
            //        }
            //    }
            //    searchItem.ItemID = ItmIDlist;
            //}
            List<Guid> ItmIDlist = new List<Guid>();
            foreach (TreeNode item in this.treeFunction.Nodes[0].Nodes)
            {
                if (item.Nodes.Count > 0)
                {

                    foreach (TreeNode itemnode in item.Nodes)
                    {
                        if (itemnode.Checked == true && itemnode.Tag!=null)
                        {
                            var itemnodels =Guid.Parse(itemnode.Tag.ToString());
                            ItmIDlist.Add(itemnodels);
                        }
                    }
                }
            }
            searchItem.ItemID = ItmIDlist;

            if ((!string.IsNullOrEmpty(temin.EditValue?.ToString()) && temin.Value!=0 )
                || ( !string.IsNullOrEmpty(teMax.EditValue?.ToString()) && teMax.Value!=0))
            {
                searchItem.MinValue = temin.Value;
                searchItem.MaxValue = teMax.Value;
            }
            //if (!string.IsNullOrEmpty(teMax.EditValue?.ToString()))
            //{
            //    searchItem.MaxValue = teMax.Value;
            //}
            if (!string.IsNullOrEmpty(searchIllNmame.EditValue?.ToString()))
            {
                searchItem.IllStr = searchIllNmame.EditValue?.ToString();
            }
            if (checkEdit1.Checked==true)
            {
                searchItem.ISIll = checkEdit1.Checked;
            }
            

            var resultdt = _diagnosisAppService.getIllCount(searchItem);
            if (resultdt.Count > 0)
            {
                var newCusItems = resultdt.GroupBy(p => new
                { p.Age, p.ClientName, p.CustomerBM, p.LoginDate, p.Name, p.Sex }).Select(p => new
                       CusRegInfoMainDto
                {
                    Age = p.Key.Age,
                    ClientName = p.Key.ClientName,
                    Sex = p.Key.Sex,
                    Name = p.Key.Name,
                    LoginDate = p.Key.LoginDate,
                    CustomerBM = p.Key.CustomerBM,
                    CusRegInfoDetail = p.Select(n => new CusRegInfoDetailDto
                    {
                        ItemDiag = n.ItemDiag,
                        ItemName = n.ItemName,
                        ItemValue = n.ItemValue,
                        Stand = n.Stand,
                        Symbol = n.Symbol
                    }).ToList()
                }).ToList();
                var itemname = resultdt.OrderBy(p=>p.DepartOrderNum).
                    ThenBy(p=>p.GrouptOrderNum).ThenBy(p=>p.ItemOrderNum).
                    Select(p => p.ItemName).Distinct().ToList();
                foreach (var depar in itemname)
                {
                    var column = new GridColumn();
                    column.FieldName = $"gridColumnCustom{depar}";
                    column.Name = $"gridColumnCustom{depar}";
                    column.Caption = depar;
                    if (!CustomColumns.Any(o => o.Key == column.Name))
                    {
                        var customColumn = new CustomColumnValue { Text = column.Caption };
                        CustomColumns.Add(column.Name, customColumn);
                    }
                    column.Visible = true;
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    column.ColumnEdit = memoEdit;
                    //column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                    column.SummaryItem.FieldName = "Name";
                    column.SummaryItem.DisplayFormat = "{0:c}";
                    column.SummaryItem.Tag = column.Name;
                    column.Width = 100;

                    gridView1.Columns.Add(column);
                    OldColumns.Add(column);
                }
                gridControl1.DataSource = newCusItems;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelHelper.GridViewToExcel(gridView1, "阳性统计", "阳性统计");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        List<string> lsname = new List<string>();
        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            // List<string> lstr = getAdviceNode();
            InitTreeFunction(searchControl1.Text);
            /// List<string> lstr2 = getAdviceNode();
            foreach (TreeNode item in this.treeFunction.Nodes[0].Nodes)
            {
                foreach (TreeNode itemnode in item.Nodes)
                {
                    string strs = itemnode.Text;
                    if (lsname.Contains(itemnode.Text))
                    {
                        itemnode.Checked = true;
                    }
                }
            }
        }
        public List<string> getAdviceNode()
        {
            List<string> lstr = new List<string>();
            foreach (TreeNode item in this.treeFunction.Nodes[0].Nodes)
            {
                if (item.Nodes.Count > 0)
                {

                    foreach (TreeNode itemnode in item.Nodes)
                    {
                        if (itemnode.Checked)
                        {
                            if (lstr.Count == 0)
                            {
                                lsname.Add(itemnode.Text);
                                lstr.Add(itemnode.Tag.ToString() + "," + itemnode.Text);
                            }
                            else
                            {
                                if (!lstr.Contains(itemnode.Text))
                                {
                                    lsname.Add(itemnode.Text);
                                    lstr.Add(itemnode.Tag.ToString() + "," + itemnode.Text);
                                }
                            }
                        }
                    }
                }
            }
            return lstr;
        }

        private void treeFunction_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CheckSelect(e.Node, e.Node.Checked);
        }
        #region 子树选择
        /// <summary>
        /// 子树选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="selectAll"></param>
        private void CheckSelect(TreeNode node, bool selectAll)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                subNode.Checked = selectAll;

                CheckSelect(subNode, selectAll);
            }
        }
        #endregion

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (gridControl1.DataSource is List<CusRegInfoMainDto> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {                       
                        var sum2 = rows[e.ListSourceRowIndex].CusRegInfoDetail
                            ?.Where(r => r.ItemName == CustomColumns[e.Column.Name].Text)
                            .Select(r => r.ItemValue).ToList();
                        foreach (var i in sum2)
                        {
                            if (i != null)
                            {
                                e.DisplayText = i.ToString();
                            }

                        }                      

                    }
                }
            }
        }

        private void treeFunction_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckSelect(e.Node, e.Node.Checked);
        }
    }
}
