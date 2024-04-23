using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class FrmHistoryYearComparison : UserBaseForm
    {
        /// <summary>
        /// 体检人Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }
        /// <summary>
        /// 体检人预约信息
        /// </summary>
        private List<SearchCustomerRegDto> CustomerRegListSys;
        /// <summary>
        /// 体检人组合记录
        /// </summary>
        private List<CustomerItemGroupDto> CustomerItemGroupSys;
        /// <summary>
        /// 体检人项目记录
        /// </summary>
        private List<SearchCustomerRegItemDto> CustomerRegItemSys;
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly HistoryComparisonAppService _calendarYearComparison;
        /// <summary>
        /// 记录已生成列名（列名是时间）
        /// </summary>
        private Dictionary<int, string> DictDatetime;
        /// <summary>
        /// 展示类
        /// </summary>
        private DataTable ExhibitionTable;
        /// <summary>
        /// 下拉框绑定科室
        /// </summary>
        private List<DistinctDepartment> DepartmentListSys;
        /// <summary>
        /// 下拉框绑定组合
        /// </summary>
        private List<DistinctItemGroup> ItemGroupListSys;
        /// <summary>
        /// 下拉框绑定项目
        /// </summary>
        private List<DistinctItem> ItemListSys;
        public FrmHistoryYearComparison()
        {
            InitializeComponent();
            _calendarYearComparison = new HistoryComparisonAppService();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCalendarYearComparison_Load(object sender, EventArgs e)
        {

            CustomerRegListSys = new List<SearchCustomerRegDto>();
            CustomerItemGroupSys = new List<CustomerItemGroupDto>();
            CustomerRegItemSys = new List<SearchCustomerRegItemDto>();
            DictDatetime = new Dictionary<int, string>();
            CustomerRegListSys = _calendarYearComparison.GetCustomerRegList(new SearchClass() { CustomerId = CustomerGuid });
            foreach (var itemReg in CustomerRegListSys)
            {
                var GroupList = itemReg.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus && o.PayerCat != (int)PayerCatType.NoCharge);
                CustomerItemGroupSys.AddRange(GroupList);
                foreach (var itemGroup in GroupList)
                {
                    CustomerRegItemSys.AddRange(itemGroup.CustomerRegItem);
                }
            }
            DepartmentListSys = CustomerItemGroupSys.Select(r => new DistinctDepartment { Id = r.DepartmentId, Name = r.DepartmentName, OrderNum = r.DepartmentOrder }).Distinct().ToList();
            ItemGroupListSys = CustomerItemGroupSys.Select(r => new DistinctItemGroup { Id = (Guid)r.ItemGroupBM_Id, Name = r.ItemGroupName, OrderNum = r.ItemGroupOrder, DepartmentId = r.DepartmentId }).Distinct().ToList();
            ItemListSys = CustomerRegItemSys.Select(r => new DistinctItem { Id = r.ItemId, Name = r.ItemName, OrderNum = r.ItemOrder, DepartmentId = r.DepartmentId, ItemGroupId = r.ItemGroupBMId }).Distinct().ToList();
            checkedComboBoxEditDepartment.Properties.DataSource = DepartmentListSys.OrderBy(o => o.OrderNum);
            checkedComboBoxEditItemGroup.Properties.DataSource = ItemGroupListSys.OrderBy(o => o.OrderNum);
            checkedComboBoxEditItem.Properties.DataSource = ItemListSys.OrderBy(o => o.OrderNum);
            int ord = 4;
            ExhibitionTable = new DataTable();
            ExhibitionTable.Columns.Add("DepartmentId", typeof(String));
            ExhibitionTable.Columns.Add("DepartmentName", typeof(String));
            ExhibitionTable.Columns.Add("ItemGroupId", typeof(String));
            ExhibitionTable.Columns.Add("ItemGroupName", typeof(String));
            ExhibitionTable.Columns.Add("ItemId", typeof(String));
            ExhibitionTable.Columns.Add("ItemName", typeof(String));
            ExhibitionTable.Columns.Add("Stand", typeof(String));

            foreach (var item in CustomerRegListSys.OrderByDescending(o => o.LoginDate))
            {
                var Columns = new DevExpress.XtraGrid.Columns.GridColumn();
                Columns.Name = DateTime.Parse(item.LoginDate.ToString()).ToString("yyyy-MM-dd");
                Columns.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                Columns.FieldName = DateTime.Parse(item.LoginDate.ToString()).ToString("yyyy-MM-dd");
                Columns.Caption = DateTime.Parse(item.LoginDate.ToString()).ToString("yyyy-MM-dd");
                Columns.VisibleIndex = ord;
                Columns.MaxWidth = 200;
                Columns.MinWidth = 100;
                Columns.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                gridViewContrast.Columns.Add(Columns);
                ExhibitionTable.Columns.Add(Columns.Caption, typeof(String));
                DictDatetime.Add(ord, Columns.Name);
                ord++;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSelect_Click(object sender, EventArgs e)
        {
            ExhibitionTable.Rows.Clear();
            //科室，组合，项目所有勾选的科室记录
            List<DistinctDepartment> dictDepartment = new List<DistinctDepartment>();
            //科室，组合，项目所有勾选的组合记录
            List<DistinctItemGroup> dictItemGroup = new List<DistinctItemGroup>();
            //科室，组合，项目所有勾选的项目记录
            List<DistinctItem> dictItem = new List<DistinctItem>();
            //查询包含的时间列，不包含的会隐藏
            List<string> LoginDateList = new List<string>();
            //判断是否勾选了科室下拉框
            if (!string.IsNullOrWhiteSpace(checkedComboBoxEditDepartment.EditValue.ToString()))
            {
                //获取勾选的科室数组
                var dictDepartmentList = checkedComboBoxEditDepartment.EditValue.ToString().Replace(" ","").Split(',').ToList();
                //记录勾选的科室信息
                dictDepartment.AddRange(DepartmentListSys.Where(o => dictDepartmentList.Contains(o.Id.ToString())));
                //记录勾选的组合信息
                dictItemGroup.AddRange(ItemGroupListSys.Where(o => dictDepartmentList.Contains(o.DepartmentId.ToString())));
                //记录勾选的项目信息
                dictItem.AddRange(ItemListSys.Where(o => dictDepartmentList.Contains(o.DepartmentId.ToString())));
            }
            //判断是否勾选了组合下拉框
            if (!string.IsNullOrWhiteSpace(checkedComboBoxEditItemGroup.EditValue.ToString()))
            {
                //获取勾选的组合数组
                var dictItemGroupList = checkedComboBoxEditItemGroup.EditValue.ToString().Replace(" ", "").Split(',').ToList();
                //循环勾选的组合数据
                foreach (var ItemGroup in ItemGroupListSys.Where(o => dictItemGroupList.Contains(o.Id.ToString())))
                {
                    //获取此组合的信息
                    var dictItemGroupFirst = ItemGroupListSys.FirstOrDefault(o => o.Id == ItemGroup.Id);
                    //判断此组合的科室是否存在于科室记录中，若不存在则科室
                    if (dictDepartment.FirstOrDefault(o => o.Id == ItemGroup.DepartmentId) == null)
                    {
                        dictDepartment.Add(DepartmentListSys.FirstOrDefault(o => o.Id == ItemGroup.DepartmentId));
                    }
                    //判断此组合是否存在于组合记录中，若不存在则增加组合和项目
                    if (!dictItemGroup.Contains(dictItemGroupFirst))
                    {
                        dictItemGroup.Add(ItemGroup);
                        dictItem.AddRange(ItemListSys.Where(o => o.ItemGroupId == ItemGroup.Id));
                    }
                }
            }
            //判断是否勾选了项目下拉框
            if (!string.IsNullOrWhiteSpace(checkedComboBoxEditItem.EditValue.ToString()))
            {
                //获取勾选的项目数组
                var dictItemList = checkedComboBoxEditItem.EditValue.ToString().Replace(" ", "").Split(',').ToList();
                //循环勾选的项目数据
                foreach (var item in ItemListSys.Where(o => dictItemList.Contains(o.Id.ToString())))
                {
                    //判断此项目科室记录是否存在，若不存在则增加科室信息
                    if (dictDepartment.FirstOrDefault(o => o.Id == item.DepartmentId) == null)
                    {
                        dictDepartment.Add(DepartmentListSys.FirstOrDefault(o => o.Id == item.DepartmentId));
                    }
                    //判断此项目组合记录是否存在，若不存在则增加组合信息
                    if (dictItemGroup.FirstOrDefault(o => o.Id == item.ItemGroupId) == null)
                    {
                        dictItemGroup.Add(ItemGroupListSys.FirstOrDefault(o => o.Id == item.ItemGroupId));
                    }
                    //判断此项目是否存在项目记录，若不存在则增加项目信息
                    if (!dictItem.Contains(item))
                    {
                        dictItem.Add(item);
                    }
                }
            }
            //判断科室记录中是否存在数据，若没有则表示都没有勾选
            if (dictDepartment.Count != 0)
            {
                //循环勾选科室
                foreach (var itemDepartment in dictDepartment)
                {
                    var Deparment = CustomerItemGroupSys.Where(o => o.DepartmentId == itemDepartment.Id).FirstOrDefault();
                    //循环科室对应组合
                    foreach (var itemGroup in dictItemGroup.Where(o => o.DepartmentId == itemDepartment.Id))
                    {
                        var CustomerId = CustomerRegListSys.OrderByDescending(o => o.LoginDate).FirstOrDefault()?.Id;
                        //循环组合对应项目   
                        foreach (var item in dictItem.Where(o => o.ItemGroupId == itemGroup.Id))
                        {
                            //添加项目
                            DataRow row = ExhibitionTable.NewRow();
                            row["DepartmentId"] = itemGroup.DepartmentId;
                            row["DepartmentName"] = itemDepartment.Name;
                            row["ItemGroupId"] = itemGroup.Id;
                            row["ItemGroupName"] = itemGroup.Name.Trim();
                            row["ItemId"] = item.Id;
                            row["ItemName"] = item.Name;
                            row["Stand"] = CustomerRegItemSys.FirstOrDefault(o => o.ItemId == item.Id && o.CustomerRegId == CustomerId).Stand;
                            foreach (var itemList in CustomerRegItemSys.Where(o => o.ItemId == item.Id))
                            {
                                var Login = CustomerRegListSys.FirstOrDefault(o => o.Id == itemList.CustomerRegId);
                                if (Login != null && Login.LoginDate != null)
                                {
                                    var LoginDate = DateTime.Parse(Login.LoginDate.ToString()).ToString("yyyy-MM-dd");
                                    row[LoginDate] = itemList.ItemResultChar;
                                    if (!LoginDateList.Contains(LoginDate))
                                    {
                                        LoginDateList.Add(LoginDate);
                                    }
                                }
                            }
                            ExhibitionTable.Rows.Add(row);
                        }
                        //添加组合诊断
                        DataRow rowGroupSum = ExhibitionTable.NewRow();
                        rowGroupSum["DepartmentId"] = itemGroup.DepartmentId;
                        rowGroupSum["DepartmentName"] = itemDepartment.Name;
                        rowGroupSum["ItemGroupId"] = itemGroup.Id;
                        rowGroupSum["ItemGroupName"] = itemGroup.Name.Trim();
                        rowGroupSum["ItemName"] = "组合诊断";
                        foreach (var item in CustomerItemGroupSys.Where(o => o.ItemGroupBM_Id == itemGroup.Id))
                        {
                            var Login = CustomerRegListSys.FirstOrDefault(o => o.Id == item.CustomerRegBMId);
                            if (Login != null && Login.LoginDate != null)
                            {
                                var LoginDate = DateTime.Parse(Login.LoginDate.ToString()).ToString("yyyy-MM-dd");
                                rowGroupSum[LoginDate] = item.ItemGroupDiagnosis;
                            }
                        }
                        ExhibitionTable.Rows.Add(rowGroupSum);
                    }
                    //添加科室诊断
                    DataRow rowDeparmentSum = ExhibitionTable.NewRow();
                    rowDeparmentSum["DepartmentId"] = itemDepartment.Id;

                    rowDeparmentSum["DepartmentName"] = itemDepartment.Name;
                    rowDeparmentSum["ItemName"] = "科室诊断";
                    foreach (var itemCustomer in CustomerRegListSys)
                    {
                        var LoginDate = DateTime.Parse(itemCustomer.LoginDate.ToString()).ToString("yyyy-MM-dd");
                        rowDeparmentSum[LoginDate] = itemCustomer.CustomerDepSummary.FirstOrDefault(o => o.DepartmentBMId == itemDepartment.Id)?.DagnosisSummary;
                    }
                    ExhibitionTable.Rows.Add(rowDeparmentSum);
                }
            }
            //显示隐藏列
            foreach (var item in DictDatetime)
            {
                if (LoginDateList.Contains(item.Value)==false)
                {
                    gridViewContrast.Columns[item.Value].Visible = false;
                }
                else
                {
                    gridViewContrast.Columns[item.Value].Visible = true;
                    gridViewContrast.Columns[item.Value].VisibleIndex = item.Key;
                }
            }
            gridControlContrast.DataSource = ExhibitionTable;


        }

        private void simpleButtonQuxianTuShi_Click(object sender, EventArgs e)
        {

        }
    }
}
