using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using NPOI.SS.UserModel;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemInfo
{
    public partial class ItemList : UserBaseForm
    {
        private readonly IItemInfoAppService _itemInfoAppService;

        private readonly IDepartmentAppService _departmentAppService;

        private List<SexModel> sexList;

        private List<PositiveStateModel> positiveList;

        private List<LabelingStateModel> labelLingList;

        private List<InvoiceStateModel> enableList;

        private List<IllnessSateModel> illnessList;
        public List<int> indexls = new List<int>();
        private List<IllnessLevelModel> illnessLevel;

        private IWorkbook _workbook;
        public ItemList()
        {
            InitializeComponent();

            _itemInfoAppService = new ItemInfoAppService();
            _departmentAppService = new DepartmentAppService();
            sexList = SexHelper.GetSexModelsForItemInfo();
            positiveList = PositiveStateHelper.GetPositiveStateModels();
            labelLingList = SymbolHelper.GetLabelingStateModels();
            enableList = InvoiceStateHelper.GetInvoiceStateModels();
            illnessList = IllnessSateHelp.GetIfTypeModels();
            illnessLevel = IllnessLevelHelp.GetIfTypeModels();
        }

        /// <summary>
        /// 项目信息加载
        /// </summary>
        private void ItemInfoLoad()
        {
            //try
            //{
            //    var departments = _itemInfoAppService.QueryDepartment(new TbmDepartmentDto()).OrderBy(d => d.OrderNum);
            //    var viewList = departments.Distinct().Select(d => new MainViewModel
            //    {
            //        Id = d.Id,
            //        Name = d.Name,
            //        ItemInfos = _itemInfoAppService.QueryItemInfo(new ItemInfoDto {DepartmentId = d.Id})
            //            .OrderBy(i => i.OrderNum).ToList()
            //    });
            //    gridControlItems.DataSource = viewList;
            //}
            //catch (UserFriendlyException ex)
            //{
            //    ShowMessageBox(ex);
            //}
        }

        private string FormatItemSex(object arg)
        {
            try
            {
                return sexList.Find(s => s.Id == (int)arg).Display;
            }
            catch
            {
                return sexList.Find(s => s.Id == (int)Sex.GenderNotSpecified).Display;
            }
        }

        private string FormatItemIllness(object arg)
        {
            try
            {
                return illnessList.Find(s => s.Id == Convert.ToInt16(arg)).Display;
            }
            catch
            {
                return "";
            }
        }

        private string gridColumnLevel(object arg)
        {
            try
            {
                return illnessLevel.Find(s => s.Id == Convert.ToInt16(arg)).Display;
            }
            catch
            {
                return "";
            }
        }
        private string FormatEnable(object arg)
        {
            try
            {
                if ((int)arg ==2)
                {
                    return "停用";
                }
                else
                {
                    return "启用";
                }
            }
            catch
            {
                return "启用";
            }
        }

        private string FormatPositive(object arg)
        {
            try
            {
                return positiveList.Find(p => p.Id == (int)arg).Display;
            }
            catch
            {
                return arg.ToString();
            }
        }

        public string CustomSexFormatter(object obj)
        {
            try
            {
                return sexList.Find(r => r.Id == Convert.ToInt16(obj)).Display;
            }
            catch
            {
                return sexList.Find(r => r.Id == (int)Sex.GenderNotSpecified).Display;
            }
        }
        private string FormatLabelLing(object arg)
        {
            try
            {
                return labelLingList.Find(p => p.Id == (int)arg).Display;
            }
            catch
            {
                return arg.ToString();
            }
        }

        /// <summary>
        /// 项目标准加载
        /// </summary>
        private void LoadStandardInfoData(Guid id)
        {
            try
            {
                var standards =
                    _itemInfoAppService.QueryItemStandardByItemId(new EntityDto<Guid>
                    {
                        Id = id
                    });
                gridControlItemStandard.DataSource = standards.OrderBy(s => s.OrderNum).ToList();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemList_Load(object sender, EventArgs e)
        {
            InitializeData();
            LoadData();
        }

        /// <summary>
        /// 项目添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonItemInfoCreate_Click(object sender, EventArgs e)
        {
            var data = gridViewItemInfo.GetFocusedRow() as ItemInfoViewDto;
            using (var frm = new ItemEditor())
            {
                if (data != null && data.Department!=null)
                {
                    frm.DepartmentId = data.Department.Id;
                }
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    simpleButtonQuery.PerformClick();
                }
            }
        }

        /// <summary>
        /// 项目编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleEditItemInfoEdit_Click(object sender, EventArgs e)
        {
            var id = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, gridColumnItemInfoId);
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！"); 
                return;
            }
            var dto = gridControlItems.GetFocusedRowDto<ItemInfoViewDto>();
            if (id != null)
                using (var frm = new ItemEditor((Guid)id))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //simpleButtonQuery.PerformClick();
                        ModelHelper.CustomMapTo(frm._Model, dto);
                        gridControlItems.RefreshDataSource();
                    }
                }
        }

        /// <summary>
        /// 项目删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleEditItemInfoDelete_Click(object sender, EventArgs e)
        {
            var id = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, gridColumnItemInfoId);
            if (id == null)
            {
                ShowMessageBoxError("尚未选定任何项目！");
                return;
            }
            var name = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, gridColumnItemInfoName);

            if (XtraMessageBox.Show($"确定要删除项目 {name} 吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {
                try
                {

                    // simpleButtonQuery.PerformClick();
                    var dto = gridControlItems.GetFocusedRowDto<ItemInfoViewDto>();
                    AutoLoading(() =>
                    {
                        _itemInfoAppService.DeleteItemInfo(new EntityDto<Guid>
                        {
                            Id = (Guid)id
                        });
                        gridControlItems.RemoveDtoListItem(dto);
                    }, Variables.LoadingDelete);
                }
                catch (UserFriendlyException exception)
                {
                    ShowMessageBoxError(exception.ToString());
                }
            }
        }

        /// <summary>
        /// 编辑标准事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonItemStandardEdit_Click(object sender, EventArgs e)
        {
            var dto = gridControlItemStandard.GetFocusedRowDto<ItemStandardDto>();
            if (dto != null)
            {
                var from = new ItemStandardEditor(dto);
                if (dto.Item != null)
                {
                    from.ItemId = dto.Item.Id;
                    from.ItemName = dto.Item.Name;
                }

                if (from.ShowDialog() == DialogResult.OK)
                    LoadStandardInfoData(dto.Item.Id);
            }
        }

        /// <summary>
        /// 添加标准事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonItemStandardCreate_Click(object sender, EventArgs e)
        {
            var dto = gridControlItems.GetFocusedRowDto<ItemInfoViewDto>();
            if (dto != null)
            {
                var from = new ItemStandardEditor();
                from.ItemId = dto.Id;
                from.ItemName = dto.Name;
                if (from.ShowDialog() == DialogResult.OK)
                    gridControlItemStandard.AddDtoListItem(from.editDto);
            }
        }

        /// <summary>
        /// 删除标准事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonItemStandardDelete_Click(object sender, EventArgs e)
        {
            var dto = gridControlItemStandard.GetFocusedRowDto<ItemStandardDto>();
            if (dto == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目标准！");
                return;
            }
            if (XtraMessageBox.Show("确定要删除该项目标准吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                return;
            if (dto != null)
            {
                _itemInfoAppService.DeleteItemStandard(new ItemStandardDto { Id = dto.Id });
                gridControlItemStandard.RemoveDtoListItem(dto);
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            var searchItemInfoDto = new SearchItemInfoDto();
            if (lookUpEditDepartments.EditValue != null)
                searchItemInfoDto.DepartmentId = (Guid)lookUpEditDepartments.EditValue;
            if (!string.IsNullOrWhiteSpace(textEditItemName.Text.Trim()))
                searchItemInfoDto.Name = textEditItemName.Text.Trim();
            if (editActive.EditValue != null)
                searchItemInfoDto.IsActive = Convert.ToInt16(editActive.EditValue);
            try
            {
                indexls.Clear();
                var result = _itemInfoAppService.QueryItemInfo(searchItemInfoDto);
                gridControlItems.DataSource = null;

                gridControlItems.DataSource = result;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        /// <summary>
        /// 项目列表模型
        /// </summary>
        private class MainViewModel : TbmDepartmentDto
        {
            public List<ItemInfoDto> ItemInfos { get; set; }
        }

        private void InitializeData()
        {
            try
            {
                var depts = _departmentAppService.GetAll();
                lookUpEditDepartments.Properties.DataSource = depts;
                //var list = new List<EnumModel>();
                //list.Add(new EnumModel { Id = 0, Name = "启用" });
                //list.Add(new EnumModel { Id = 1, Name = "停用" });
                //editActive.Properties.DataSource = list;

                var enableItems = InvoiceStateHelper.GetInvoiceStateModels();
                editActive.Properties.DataSource = enableItems;
                editActive.EditValue = (int)InvoiceState.Enable;
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
            gridViewItemInfo.Columns[gridColumnItemInfoSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItemInfo.Columns[gridColumnItemInfoSex.FieldName].DisplayFormat.Format = new CustomFormatter(FormatItemSex);
            gridViewItemstandard.Columns[gridColumnPositive.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItemstandard.Columns[gridColumnPositive.FieldName].DisplayFormat.Format = new CustomFormatter(FormatPositive);
            gridViewItemstandard.Columns[gridColumnLabelLing.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItemstandard.Columns[gridColumnLabelLing.FieldName].DisplayFormat.Format = new CustomFormatter(FormatLabelLing);
            gridViewItemstandard.Columns[gridColumnSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItemstandard.Columns[gridColumnSex.FieldName].DisplayFormat.Format = new CustomFormatter(CustomSexFormatter);
            gridViewItemstandard.Columns[gridColumnIllness.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItemstandard.Columns[gridColumnIllness.FieldName].DisplayFormat.Format = new CustomFormatter(FormatItemIllness);
            gridViewItemInfo.Columns[gridColumnEnable.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItemInfo.Columns[gridColumnEnable.FieldName].DisplayFormat.Format = new CustomFormatter(FormatEnable);
            gridViewItemstandard.Columns[gridColumnLevels.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItemstandard.Columns[gridColumnLevels.FieldName].DisplayFormat.Format = new CustomFormatter(gridColumnLevel);
            //体检类别
            var checktype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            if (Variables.ISZYB == "2")
            {
                checktype = checktype.Where(o => o.Text.Contains("职业")).ToList();
            }
            repositoryItemLookUpEdit1.DataSource = checktype;


        }

        private void LoadData()
        {
            try
            {
                var itemInfos = _itemInfoAppService.GetAll();
                gridControlItems.DataSource = itemInfos;
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
        }

        private void lookUpEditDepartments_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookUpEditDepartments.EditValue = null;
            }
        }

        private void gridViewItemInfo_RowClick(object sender, RowClickEventArgs e)
        {
            var id = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, gridColumnItemInfoId);
            if (id != null)
                LoadStandardInfoData((Guid)id);
        }

        private void butPX_Click(object sender, EventArgs e)
        {
            int num = 30 * (CurrentPage - 1);
            var grouplist = GetGridViewFilteredAndSortedData(gridViewItemInfo);
            var foceDto = gridViewItemInfo.GetFocusedRow() as ItemInfoViewDto; 


            for (var i = 0; i < grouplist.Count; i++)
            {
                
                ItemInfoViewDto gridRow = (ItemInfoViewDto)grouplist[i];
                if (foceDto.Department.Id == gridRow.Department.Id)
                {
                    if (gridRow.OrderNum == null)
                    {
                        gridRow.OrderNum = 0;
                    }
                    if (gridRow.OrderNum != i)
                    {
                        gridRow.OrderNum = num + i;
                        //记录更新的索引
                        for (int rr = 0; rr < gridViewItemInfo.RowCount; rr++)
                        {
                            ItemInfoViewDto fullItemGroupDto = gridViewItemInfo.GetRow(rr) as ItemInfoViewDto;
                            if (fullItemGroupDto != null && fullItemGroupDto.Id == gridRow.Id)
                            {
                                if (!indexls.Contains(rr))
                                {
                                    indexls.Add(rr);
                                }
                            }
                        }
                    }
                }
            }
            this.gridControlItems.RefreshDataSource();
        }

        private void simpleButtonUp_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int dsRowIndex = this.gridViewItemInfo.GetFocusedDataSourceRowIndex();
                if (this.gridViewItemInfo.IsFirstRow == true)
                {   //当前节点是第一个节点,不用上移
                    return;
                }
                var role = gridViewItemInfo.GetFocusedRow() as ItemInfoViewDto;
                if (role != null)
                {
                    //var roleList = GetGridViewFilteredAndSortedData(gridViewItemInfo);

                   var roleList = this.gridControlItems.DataSource as List<ItemInfoViewDto>;

                    int selectIndex = roleList.IndexOf(role);
                    if (selectIndex == 0)
                    {
                        return;
                    }
                    //判断如果下面的上面不是同一个科室则不能移动
                    if (roleList[selectIndex - 1].Department.Id != roleList[selectIndex].Department.Id)
                    {
                        return;
                    }
                    roleList[selectIndex] = roleList[selectIndex - 1];
                    roleList[selectIndex - 1] = role;
                    this.gridControlItems.DataSource = roleList;

                    if (roleList[selectIndex - 1].IsDeleted == 1)
                    {
                        ShowMessageBoxError("项目已禁用，无法移动！");
                        return;
                    }
                    this.gridControlItems.RefreshDataSource();
                    this.gridViewItemInfo.MovePrev();
                    butPX.PerformClick();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void simpleButtonDown_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int dsRowIndex = this.gridViewItemInfo.GetFocusedDataSourceRowIndex();
                if (this.gridViewItemInfo.IsLastRow == true)
                {   //当前节点是第一个节点,不用下移
                    return;
                }
                var role = gridViewItemInfo.GetFocusedRow() as ItemInfoViewDto;
                if (role != null)
                {

                    //int tIndex = dsRowIndex + 1;
                    //int tRowHandle = this.gridViewItemInfo.GetRowHandle(tIndex);
                    //TbmDepartmentDto t_role = this.gridViewItemInfo.GetRow(tRowHandle) as TbmDepartmentDto;

                    //显示
                    List<ItemInfoViewDto> roleList = this.gridControlItems.DataSource as List<ItemInfoViewDto>;
                    //var roleList = GetGridViewFilteredAndSortedData(gridViewItemInfo);

                    int selectIndex = roleList.IndexOf(role);
                    if (selectIndex == roleList.Count - 1)
                    {
                        return;
                    }
                    //判断如果下面的不是同一个科室则不能移动
                    if (roleList[selectIndex + 1].Department.Id != roleList[selectIndex].Department.Id)
                    {
                        return;
                    }
                    roleList[selectIndex] = roleList[selectIndex + 1];
                    roleList[selectIndex + 1] = role;
                    //
                    this.gridControlItems.DataSource = roleList;
                    this.gridControlItems.RefreshDataSource();
                    this.gridViewItemInfo.MoveNext();
                    butPX.PerformClick();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //
            }
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {
                foreach (int row in indexls)
                {
                    ItemInfoViewDto Department = this.gridViewItemInfo.GetRow(row) as ItemInfoViewDto;
                    if (Department != null)
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = Department.Id;
                        chargeBM.Name = Department.OrderNum.ToString();

                        _itemInfoAppService.UpdateOrder(chargeBM);
                    }
                }
                indexls.Clear();
                simpleButtonQuery.PerformClick();
            }
            catch (UserFriendlyException ex)
            {

                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
                ShowMessageBox(ex);
                return;
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
        public System.Collections.IList GetGridViewFilteredAndSortedData(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            return view.DataController.GetAllFilteredAndSortedRows();
        }

        private void gridViewItemInfo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!indexls.Contains(e.RowHandle))
            {
                indexls.Add(e.RowHandle);
            }
        }

        private void editActive_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                editActive.EditValue = null;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Import();
        }
        /// <summary>
        /// 选择文件导入
        /// </summary>
        public void Import()
        {
            openFileDialog.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();
                AutoLoading(() =>
                {
                    dt = ExcelToDataTable(openFileDialog.FileName, "Sheet", true);
                });
                string Err = "";
                if (!dt.Columns.Contains("平台编码"))
                { Err = "模板中缺少'平台编码'列'\r\n"; }
                if (!dt.Columns.Contains("单位"))
                { Err += "模板中缺少'单位'列'\r\n"; }
                if (!dt.Columns.Contains("单位编码"))
                { Err += "模板中缺少'单位编码'列'\r\n"; }
                for (int i=0; i< dt.Rows.Count;i++)
                {
                    var nowItemInfo = DefinedCacheHelper.GetItemInfos().FirstOrDefault(p=>p.StandardCode== dt.Rows[i]["平台编码"].ToString());
                    if (nowItemInfo != null)
                    {
                        UpItemUnit upItemUnit = new UpItemUnit();
                        upItemUnit.StandardCode = dt.Rows[i]["平台编码"].ToString();
                        upItemUnit.Unit = dt.Rows[i]["单位"].ToString();
                        upItemUnit.UnitBM= dt.Rows[i]["单位编码"].ToString();
                        _itemInfoAppService.InputUnit(upItemUnit);
                    }
                }
                MessageBox.Show("更新单位成功！");
                simpleButtonQuery.PerformClick();
            }
        }

        /// <summary>
        /// 将 Excel 中的数据导入到 DataTable 中
        /// </summary>
        /// <param name="sheetName">Excel 工作薄 Sheet 的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是 DataTable 的列名</param>
        /// <returns>返回的 DataTable</returns>
        public DataTable ExcelToDataTable(string _fileName, string sheetName, bool isFirstRowColumn)
        {
            var data = new DataTable();

            FileStream _fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            //_workbook = new HSSFWorkbook(_fs);      
            _workbook = WorkbookFactory.Create(_fs);//使用接口，自动识别excel2003/2007格式
            ISheet sheet;
            if (sheetName != null)
            {
                // 如果没有找到指定的 SheetName 对应的 Sheet，则尝试获取第一个 Sheet
                sheet = _workbook.GetSheet(sheetName) ?? _workbook.GetSheetAt(0);
            }
            else
            {
                sheet = _workbook.GetSheetAt(0);
            }

            if (sheet != null)
            {
                var firstRow = sheet.GetRow(0);
                // 一行最后一个 Cell 的编号，即总的列数
                int cellCount = firstRow.LastCellNum;
                int startRow;
                if (isFirstRowColumn)
                {
                    List<string> conName = new List<string>();

                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        var cell = firstRow.GetCell(i);
                        var cellValue = cell?.StringCellValue;

                        if (cellValue != null)
                        {

                            var column = new DataColumn(cellValue);
                            if (!conName.Contains(cellValue))
                            {
                                data.Columns.Add(column);
                                conName.Add(cellValue);
                            }
                            else
                            {
                                data.Columns.Add(column + i.ToString());
                                conName.Add(cellValue);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;
                }
                else
                {
                    startRow = sheet.FirstRowNum;
                }

                // 最后一列的标号
                var rowCount = sheet.LastRowNum;
                for (var i = startRow; i <= rowCount; ++i)
                {
                    var row = sheet.GetRow(i);

                    // 没有数据的行默认是 NULL       
                    if (row == null)
                        continue;
                    if (row.GetCell(0) == null)
                    {
                        MessageBox.Show("体检号不能为空！");
                        return new DataTable();
                    }
                    if (row.GetCell(0) == null)
                    {
                        MessageBox.Show("体检号不能为空！");
                        return new DataTable();
                    }
                    List<string> namels = new List<string>();
                    var dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        //同理，没有数据的单元格都默认是 NULL
                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                            //namels.Add();
                        }
                    }

                    //判断增加分组信息
                    //编码为空时判断                   
                    data.Rows.Add(dataRow);
                }


            }

            return data;

        }

    }
}