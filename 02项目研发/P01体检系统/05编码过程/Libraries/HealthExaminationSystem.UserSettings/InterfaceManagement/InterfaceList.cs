using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.InterfaceItemComparison;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.InterfaceManagement
{
    public partial class InterfaceList : UserBaseForm
    {
        public IInterfaceItemAppService interfaceItemAppService;
        public List<int> indexls = new List<int>();
        public List<int> indexgroupls = new List<int>();
        public List<int> indexempls = new List<int>();
        DepartmentAppService _departProxy = new DepartmentAppService();
        ItemGroupAppService _itemGroupAppService = new ItemGroupAppService();
        ItemInfoAppService _itemInfoAppService = new ItemInfoAppService();
        public InterfaceList()
        {
            InitializeComponent();
        }

        private void InterfaceList_Load(object sender, EventArgs e)
        {
            try
            {
                //绑定科室
                interfaceItemAppService = new InterfaceItemAppService();
                //List<TbmDepartmentDto> departmentList = _departProxy.GetAll();
                ;
                chkcmbDepartment.Properties.DataSource = DefinedCacheHelper.GetDepartments();                
                chkcmbDepartment.Properties.DisplayMember = "Name";
                chkcmbDepartment.Properties.ValueMember = "Id";
                //绑定组合
                //SearchItemGroupDto groupinput = new SearchItemGroupDto();
                //List<SimpleItemGroupDto> grouplis = _itemGroupAppService.QuerySimples(groupinput);
                checkItemGroup.Properties.DataSource = HealthExaminationSystem.Common.Helpers.CacheHelper.GetItemGroups();
                checkItemGroup.Properties.DisplayMember = "ItemGroupName";
                checkItemGroup.Properties.ValueMember = "Id";
                //绑定项目
              //  List<ItemInfoViewDto> itemlis = _itemInfoAppService.GetAll().OrderBy(o => o.Department.Name).ThenBy(o => o.OrderNum).ToList();
                txtItemName.Properties.DataSource = DefinedCacheHelper.GetItemInfos();
                txtItemName.Properties.DisplayMember = "Name";
                txtItemName.Properties.ValueMember = "Id";

                var _currentUserdtoSys = new List<UserForComboDto>();
                _currentUserdtoSys = DefinedCacheHelper.GetComboUsers();
                //绑定检查医生
                searchEmp.Properties.DataSource = _currentUserdtoSys;

                searchEmp.Properties.DisplayMember = "Name";
                searchEmp.Properties.ValueMember = "Id";


                var closeWait = false;
                if (!splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.ShowWaitForm();
                    closeWait = true;
                }
                try
                {
                    //interfaceItemAppService = new InterfaceItemAppService();
                    //SearchInterIFaceItemComparisonDto input = new SearchInterIFaceItemComparisonDto();
                    //ShowInterfaceItems(input);
                   // butSearch_Click(sender, e);
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
                finally
                {
                    if (closeWait)
                    {
                        if (splashScreenManager.IsSplashFormVisible)
                            splashScreenManager.CloseWaitForm();
                    }
                }

            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

        }
        private void butSearch_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            try
            {

                List<string> depttype = new List<string>();
                string[] depttypes = checkedDeptType.Properties.GetCheckedItems().ToString().Split(',');
                if (depttypes.Length > 0 && !string.IsNullOrEmpty(depttypes[0]))
                {
                    foreach (string item in depttypes)
                    {
                        depttype.Add(item);
                    }
                }
                SearchInterIFaceItemComparisonDto input = new SearchInterIFaceItemComparisonDto();
                if (chkcmbDepartment.EditValue != null)
                {
                    input.departmentId = (Guid)chkcmbDepartment.EditValue;
                }
                if (checkItemGroup.EditValue != null)
                {
                    input.ItemGroupId = (Guid)checkItemGroup.EditValue;
                }
                if (txtItemName.EditValue != null)
                {
                    input.ItemID = (Guid)txtItemName.EditValue;
                }
                if (searchEmp.EditValue != null && !searchEmp.EditValue.Equals(string.Empty))
                {
                    input.EmpID = (long)searchEmp.EditValue;
                }
                input.DeptTypes = depttype;
                input.CheckType = comCheckType.Text;
                ShowInterfaceItems(input);
                indexgroupls.Clear();
                indexls.Clear();
                indexempls.Clear();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }

        private void gridviewInterfaceItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!indexls.Contains(e.RowHandle))
            {
                indexls.Add(e.RowHandle);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                foreach (int row in indexls)
                {
                    int interfaceItemsDto = gridviewInterfaceItems.GetDataSourceRowIndex(row);
                    var itemdata = (List<InterfaceItemsDto>)gridControl1.DataSource;

                    InterfaceItemsDto interfaceitem = itemdata[interfaceItemsDto];
                    if (interfaceitem.ObverseItemId != null)
                    {
                        InsertInterfaceItemDto insertInterfaceItemDto = new InsertInterfaceItemDto();                       
                        insertInterfaceItemDto.ItemInfoId = interfaceitem.ItemInfoId;
                        insertInterfaceItemDto.ItemName = interfaceitem.ItemName;
                        insertInterfaceItemDto.ObverseItemId = interfaceitem.ObverseItemId;
                        insertInterfaceItemDto.ObverseItemName = interfaceitem.ObverseItemName;
                        insertInterfaceItemDto.InstrumentModelNumber = interfaceitem.InstrumentModelNumber;
                        insertInterfaceItemDto.Remarks = interfaceitem.Remarks;
                        insertInterfaceItemDto.Id = interfaceitem.Id;
                        interfaceItemAppService.SaveInterfaceItems(insertInterfaceItemDto);
                    }

                }
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {
                foreach (int row in indexgroupls)
                {
                    int interfaceItemgroupsDto = gridViewItemGroup.GetDataSourceRowIndex(row);
                    var itemdata = (List<InterfaceItemGroupsDto>)gridControl2.DataSource;

                    InterfaceItemGroupsDto interfaceitem = itemdata[interfaceItemgroupsDto];
                    if (interfaceitem.ObverseItemId != null)
                    {
                        InsertInterfaceItemGroupDto insertInterfaceItemGroupDto = new InsertInterfaceItemGroupDto();
                        insertInterfaceItemGroupDto.DepartmentId = interfaceitem.DepartmentId;
                        insertInterfaceItemGroupDto.ItemGroupId = interfaceitem.ItemGroupId;
                        insertInterfaceItemGroupDto.ObverseItemId = interfaceitem.ObverseItemId;
                        insertInterfaceItemGroupDto.ObverseItemName = interfaceitem.ObverseItemName;
                        insertInterfaceItemGroupDto.InstrumentModelNumber = interfaceitem.InstrumentModelNumber;
                        insertInterfaceItemGroupDto.Remarks = interfaceitem.Remarks;
                        insertInterfaceItemGroupDto.Id = interfaceitem.Id;
                        interfaceItemAppService.SaveInterfaceItemGroups(insertInterfaceItemGroupDto);
                    }
                }

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
                foreach (int row in indexempls)
                {
                    int interfaceItemgroupsDto = gridViewEmp.GetDataSourceRowIndex(row);
                    var itemdata = (List<InterfaceUserDto>)gridControl3.DataSource;
                    InterfaceUserDto interfaceitem = itemdata[interfaceItemgroupsDto];
                    if (interfaceitem.ObverseEmpId != null)
                    {
                        InsertInterfaceEmpDto insertInterfaceItemGroupDto = new InsertInterfaceEmpDto();
                        insertInterfaceItemGroupDto.EmployeeId = interfaceitem.EmployeeId;
                        insertInterfaceItemGroupDto.EmployeeName = interfaceitem.EmployeeName;
                        insertInterfaceItemGroupDto.ObverseEmpId = interfaceitem.ObverseEmpId;
                        insertInterfaceItemGroupDto.ObverseEmpName = interfaceitem.ObverseEmpName;
                        insertInterfaceItemGroupDto.Id = interfaceitem.Id;
                        interfaceItemAppService.SaveInterfaceUser(insertInterfaceItemGroupDto);
                    }
                }
            }
            indexgroupls.Clear();
            indexls.Clear();
            indexempls.Clear();
            butSearch_Click(sender, e);
        }
        /// <summary>
        /// 查询项目对应
        /// </summary>
        /// <param name="input"></param>
        private void ShowInterfaceItems(SearchInterIFaceItemComparisonDto input)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                List<InterfaceItemsDto> interfaceItemsDtos = interfaceItemAppService.GetInterfaceItemComparison(input);
                gridControl1.DataSource = interfaceItemsDtos;
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {
                List<InterfaceItemGroupsDto> interfaceItemGrousDtos = interfaceItemAppService.GetInterfaceItemGroupComparison(input);
                gridControl2.DataSource = interfaceItemGrousDtos;
            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
                List<InterfaceUserDto> interfaceItemGrousDtos = interfaceItemAppService.getInterfaceUser(input);
                gridControl3.DataSource = interfaceItemGrousDtos;
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (tabPane1.SelectedPageIndex == 0)
                {
                    int RowIndex = gridviewInterfaceItems.GetFocusedDataSourceRowIndex();
                    var table = (List<InterfaceItemsDto>)gridControl1.DataSource;
                    var result = gridviewInterfaceItems.GetFocusedRow() as InterfaceItemsDto;                
                    var Iinterfaceitems = new InterfaceItemsDto();
                    Iinterfaceitems.DepartmentId = result.DepartmentId;
                    Iinterfaceitems.DepartmentName = result.DepartmentName;
                    Iinterfaceitems.ItemGroupId = result.ItemGroupId;
                    Iinterfaceitems.ItemGroupName = result.ItemGroupName;
                    Iinterfaceitems.ItemBM = result.ItemBM;
                    Iinterfaceitems.ItemInfoId = result.ItemInfoId;
                    Iinterfaceitems.ItemName = result.ItemName;             
                    table.Insert(RowIndex, Iinterfaceitems);
                    gridControl1.DataSource = table;
                    gridControl1.Refresh();
                    gridControl1.RefreshDataSource();
                    gridviewInterfaceItems.RefreshData();
                }
                else if (tabPane1.SelectedPageIndex == 1)
                {

                    var RowName = gridViewItemGroup.GetFocusedDataRow();
                    int RowIndex = gridViewItemGroup.GetFocusedDataSourceRowIndex();
                    var table = (List<InterfaceItemGroupsDto>)gridControl2.DataSource;
                    var Iinterfaceitems = new InterfaceItemGroupsDto();
                    Iinterfaceitems.DepartmentId = table[RowIndex].DepartmentId;
                    Iinterfaceitems.Department = table[RowIndex].Department;
                    Iinterfaceitems.ItemGroupId = table[RowIndex].ItemGroupId;
                    Iinterfaceitems.ItemGroup = table[RowIndex].ItemGroup;
                    Iinterfaceitems.ItemGroupName = table[RowIndex].ItemGroupName;
                    table.Insert(RowIndex, Iinterfaceitems);
                    gridControl2.DataSource = table;
                    gridControl2.Refresh();
                    gridControl2.RefreshDataSource();
                    gridViewItemGroup.RefreshData();
                }
                else if (tabPane1.SelectedPageIndex == 2)
                {

                    var RowName = gridViewEmp.GetFocusedDataRow();
                    int RowIndex = gridViewEmp.GetFocusedDataSourceRowIndex();
                    var table = (List<InterfaceUserDto>)gridControl3.DataSource;
                    var Iinterfaceitems = new InterfaceUserDto();
                    Iinterfaceitems.EmployeeId = table[RowIndex].EmployeeId;
                    Iinterfaceitems.EmployeeName = table[RowIndex].EmployeeName;
                    Iinterfaceitems.EmployeeId = table[RowIndex].EmployeeId;
                    table.Insert(RowIndex, Iinterfaceitems);
                    gridControl3.DataSource = table;
                    gridControl3.Refresh();
                    gridControl3.RefreshDataSource();
                    gridViewEmp.RefreshData();
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            //switch (tabPane1.SelectedPageIndex)
            //{
            //    case 0:
            //        if (gridControl1.DataSource == null)
            //        {
            //            butSearch_Click(sender, e);
            //        }
            //        break;
            //    case 1:
            //        if (gridControl2.DataSource == null)
            //        {
            //            butSearch_Click(sender, e);
            //        }
            //        break;
            //    case 2:
            //        if (gridControl3.DataSource == null)
            //        {
            //            butSearch_Click(sender, e);
            //        }
            //        break;

            //}
        }

        private void gridViewItemGroup_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!indexgroupls.Contains(e.RowHandle))
            {
                indexgroupls.Add(e.RowHandle);
            }
        }

        private void gridViewEmp_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!indexempls.Contains(e.RowHandle))
            {
                indexempls.Add(e.RowHandle);
            }
        }

        private void butOutExcel_Click(object sender, EventArgs e)
        {



            switch (tabPane1.SelectedPageIndex)
            {
                case 0:
                    if (gridControl1.DataSource != null)
                    {
                        var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                        saveFileDialog.FileName = "项目对应";
                        saveFileDialog.Title = "导出Excel";
                        saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                        saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                        var dialogResult = saveFileDialog.ShowDialog();
                        if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return;

                        gridviewInterfaceItems.OptionsView.AllowCellMerge = false;
                        gridControl1.ExportToXls(saveFileDialog.FileName);
                        gridviewInterfaceItems.OptionsView.AllowCellMerge = true;
                    }
                    break;
                case 1:
                    if (gridControl2.DataSource != null)
                    {
                        var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                        saveFileDialog.FileName = "组合对应";
                        saveFileDialog.Title = "导出Excel";
                        saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                        saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                        var dialogResult = saveFileDialog.ShowDialog();
                        if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return;
                        gridViewItemGroup.OptionsView.AllowCellMerge = false;
                        //ExcelHelper.ExportToExcel("组合对应", gridControl2);
                        gridControl2.ExportToXls(saveFileDialog.FileName);
                        gridViewItemGroup.OptionsView.AllowCellMerge = true;
                    }
                    break;
                case 2:
                    if (gridControl3.DataSource != null)
                    {
                        var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                        saveFileDialog.FileName = "医生对应";
                        saveFileDialog.Title = "导出Excel";
                        saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                        saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                        var dialogResult = saveFileDialog.ShowDialog();
                        if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return;
                        gridViewEmp.OptionsView.AllowCellMerge = false;
                        // ExcelHelper.ExportToExcel("医生对应", gridControl3);
                        gridControl3.ExportToXls(saveFileDialog.FileName);
                        gridViewEmp.OptionsView.AllowCellMerge = true;
                    }
                    break;

            }
        }

        private void butInExcel_Click(object sender, EventArgs e)
        {
            try
            {
                InterfaceImportExcel interfaceImportExcel = new InterfaceImportExcel();
                interfaceImportExcel.ShowDialog();
                butSearch_Click(sender, e);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "删除")
            {
                DialogResult dr = XtraMessageBox.Show("是删除该记录？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {

                    int interfaceItemsDto = gridviewInterfaceItems.GetFocusedDataSourceRowIndex();
                    var itemdata = (List<InterfaceItemsDto>)gridControl1.DataSource;

                    if (itemdata[interfaceItemsDto].Id != Guid.Empty)
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = itemdata[interfaceItemsDto].Id;
                        chargeBM.Name = "项目对应";
                        interfaceItemAppService.delInterface(chargeBM);
                    }
                    itemdata.Remove(itemdata[interfaceItemsDto]);
                    gridviewInterfaceItems.RefreshData();
                }


            }
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "删除")
            {
                DialogResult dr = XtraMessageBox.Show("是删除该记录？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {

                    int interfaceItemsDto = gridViewItemGroup.GetFocusedDataSourceRowIndex();
                    var itemdata = (List<InterfaceItemGroupsDto>)gridControl2.DataSource;

                    if (itemdata[interfaceItemsDto].Id != Guid.Empty)
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = itemdata[interfaceItemsDto].Id;
                        chargeBM.Name = "组合对应";
                        interfaceItemAppService.delInterface(chargeBM);
                    }
                    itemdata.Remove(itemdata[interfaceItemsDto]);
                    gridViewItemGroup.RefreshData();
                }


            }

        }

        private void repositoryItemButtonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "删除")
            {
                DialogResult dr = XtraMessageBox.Show("是删除该记录？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {

                    int interfaceItemsDto = gridViewEmp.GetFocusedDataSourceRowIndex();
                    var itemdata = (List<InterfaceUserDto>)gridControl3.DataSource;

                    if (itemdata[interfaceItemsDto].Id != Guid.Empty)
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = itemdata[interfaceItemsDto].Id;
                        chargeBM.Name = "医生对应";
                        interfaceItemAppService.delInterface(chargeBM);
                    }
                    itemdata.Remove(itemdata[interfaceItemsDto]);
                    gridViewEmp.RefreshData();
                }


            }
        }

        private void delte_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }
    }
}
