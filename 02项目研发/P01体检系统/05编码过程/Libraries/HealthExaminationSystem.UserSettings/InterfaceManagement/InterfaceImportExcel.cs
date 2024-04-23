using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.InterfaceItemComparison;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.InterfaceManagement
{
    public partial class InterfaceImportExcel : UserBaseForm
    {
        //private IWorkbook _workbook;
        public InterfaceImportExcel()
        {
            InitializeComponent();
        }
        OleDbConnection m_XLSConn;
        public IInterfaceItemAppService interfaceItemAppService;
        DepartmentAppService _departProxy = new DepartmentAppService();
        ItemGroupAppService _itemGroupAppService = new ItemGroupAppService();
        ItemInfoAppService _itemInfoAppService = new ItemInfoAppService();
        private void InterfaceImportExcel_Load(object sender, EventArgs e)
        {
            interfaceItemAppService = new InterfaceItemAppService();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //AutoLoading(() =>
            //{
                Import();
            //});

        }


        /// <summary>
        /// 选择文件导入
        /// </summary>
        public void Import()
        {
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Excel文件(*.xls)|*.xls";        
            openFileDialog.FileName = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (m_XLSConn != null)
                {
                    if (m_XLSConn.State == ConnectionState.Open)
                        m_XLSConn.Close();
                    m_XLSConn = null;
                }


                DataTable dt = null;
                AutoLoading(() =>
                {
                    dt = ExcelToDataTable(openFileDialog.FileName, true);
                });
            string names = "";
                switch (tabPane1.SelectedPageIndex)
                {
                    case 0:
                        names = @"科室名称,组合名称,项目名称,对应项目编码,对应项目名称,仪器代号,备注";

                        break;
                    case 1:
                        names = @"科室名称,组合名称,对应组合编码,对应组合名称,仪器代号,备注";

                        break;
                    case 2:
                        names = @"医生名称,医生工号,对应医生工号,对应医生名称";
                        break;                  
                }
               bool isOK = checkCus(names, dt);
                if (!isOK)
                {
                    MessageBox.Show("模板列数不匹配请核实");
                    return;
                }
                DataTable erdt = new DataTable();
                erdt = dt.Clone();
               // var closeWait = false;              
                try
                {
                    switch (tabPane1.SelectedPageIndex)
                    {
                        case 0:
                            if (gridControl1.DataSource == null)
                            {
                                List<InterfaceItemsDto> InterfaceItemslis = new List<InterfaceItemsDto>();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    SearchInterfaceItemDto searchInterfaceItemDto = new SearchInterfaceItemDto();
                                    searchInterfaceItemDto.DeptName = dt.Rows[i]["科室名称"].ToString();
                                    searchInterfaceItemDto.GroupName = dt.Rows[i]["组合名称"].ToString();
                                    searchInterfaceItemDto.ItemName = dt.Rows[i]["项目名称"].ToString();
                                    searchInterfaceItemDto.ObverseItemId = dt.Rows[i]["对应项目编码"].ToString();
                                    searchInterfaceItemDto.ObverseItemName = dt.Rows[i]["对应项目名称"].ToString();
                                    searchInterfaceItemDto.InstrumentModelNumber = dt.Rows[i]["仪器代号"].ToString();
                                    searchInterfaceItemDto.Remarks = dt.Rows[i]["备注"].ToString();
                                    InterfaceItemsDto interfaceItemsDto = interfaceItemAppService.getInterfaceItems(searchInterfaceItemDto);
                                    if (interfaceItemsDto != null)
                                    {
                                        InterfaceItemslis.Add(interfaceItemsDto);
                                    }
                                    else
                                    {
                                        if (searchInterfaceItemDto.ItemName.ToString() != "")
                                        {
                                            DataRow errow = erdt.NewRow();
                                            errow.ItemArray = dt.Rows[i].ItemArray;
                                            erdt.Rows.Add(errow);
                                        }
                                    }
                                }
                                gridControl1.DataSource = InterfaceItemslis;
                            }
                            break;
                        case 1:
                            if (gridControl2.DataSource == null)
                            {
                                List<InterfaceItemGroupsDto> InterfaceItemGrouslis = new List<InterfaceItemGroupsDto>();

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    SearchInterfaceItemDto searchInterfaceItemDto = new SearchInterfaceItemDto();
                                    searchInterfaceItemDto.DeptName = dt.Rows[i]["科室名称"].ToString();
                                    searchInterfaceItemDto.GroupName = dt.Rows[i]["组合名称"].ToString();
                                    searchInterfaceItemDto.ObverseItemId = dt.Rows[i]["对应组合编码"].ToString();

                                    searchInterfaceItemDto.ObverseItemName = dt.Rows[i]["对应组合名称"].ToString();
                                    searchInterfaceItemDto.InstrumentModelNumber = dt.Rows[i]["仪器代号"].ToString();
                                    searchInterfaceItemDto.Remarks = dt.Rows[i]["备注"].ToString();
                                    InterfaceItemGroupsDto interfaceItemGroupsDto = interfaceItemAppService.getInterfaceItemGroups(searchInterfaceItemDto);
                                    if (interfaceItemGroupsDto != null)
                                    {
                                        InterfaceItemGrouslis.Add(interfaceItemGroupsDto);
                                    }
                                    else
                                    {
                                        if (searchInterfaceItemDto.GroupName.ToString() != "")
                                        {
                                            DataRow errow = erdt.NewRow();
                                            errow.ItemArray = dt.Rows[i].ItemArray;
                                            erdt.Rows.Add(errow);
                                        }
                                    }
                                }
                                gridControl2.DataSource = InterfaceItemGrouslis;

                            }
                            break;
                        case 2:
                            if (gridControl3.DataSource == null)
                            {
                                List<InterfaceUserDto> InterfaceUserlis = new List<InterfaceUserDto>();

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    SearchInterfaceEmpDto searchInterfaceEmpDto = new SearchInterfaceEmpDto();
                                    searchInterfaceEmpDto.EmployeeName = dt.Rows[i]["医生名称"].ToString();
                                    searchInterfaceEmpDto.EmployeeNum = dt.Rows[i]["医生工号"].ToString();
                                    searchInterfaceEmpDto.ObverseEmpId = dt.Rows[i]["对应医生工号"].ToString();
                                    searchInterfaceEmpDto.ObverseEmpName = dt.Rows[i]["对应医生名称"].ToString();
                                    InterfaceUserDto insertInterfaceEmpDto = interfaceItemAppService.getInterfaceEmp(searchInterfaceEmpDto);
                                    if (insertInterfaceEmpDto != null)
                                    {
                                        InterfaceUserlis.Add(insertInterfaceEmpDto);
                                    }
                                    else
                                    {
                                        if (searchInterfaceEmpDto.EmployeeName != "")
                                        {
                                            DataRow errow = erdt.NewRow();
                                            errow.ItemArray = dt.Rows[i].ItemArray;
                                            erdt.Rows.Add(errow);
                                        }
                                    }
                                }
                                gridControl3.DataSource = InterfaceUserlis;
                            }
                            break;

                    }

                    if (erdt.Rows.Count > 0)
                    {
                        var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                        saveFileDialog.FileName = "不能导入列表";
                        saveFileDialog.Title = "导出Excel";
                        saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                        saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                        var dialogResult = saveFileDialog.ShowDialog();
                        if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return;
                        ExcelHelper ex = new ExcelHelper(saveFileDialog.FileName);
                        ex.DataTableToExcel(erdt, "不能导入列表", true);
                    }
                    gridControl3.Refresh();
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
                finally
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();

                }

                // gridControlImportExcel.DataSource = dt;
            }
        }



        //IWorkbook workbook = null;
        /// <summary>
        /// 将excel导入到datatable
        /// </summary>
        /// <param name="filePath">excel路径</param>
        /// <param name="isColumnName">第一行是否是列名</param>
        /// <returns>返回datatable</returns>
        public static DataTable ExcelToDataTable(string filePath, bool isColumnName)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本
                    if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行
                                int cellCount = firstRow.LastCellNum;//列数

                                //构建datatable的列
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);

                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Blean 4,Error = 5,)
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int oknum = 0;
            int errnum = 0;
            if (tabPane1.SelectedPageIndex == 0)
            {
                for (int row =0;row < gridviewInterfaceItems.RowCount;row ++)
                {
                    int interfaceItemsDto = gridviewInterfaceItems.GetDataSourceRowIndex(row);
                    var itemdata = (List<InterfaceItemsDto>)gridControl1.DataSource;

                    InterfaceItemsDto interfaceitem = itemdata[interfaceItemsDto];
                    if (interfaceitem.ObverseItemId != null)
                    {
                        InsertInterfaceItemDto insertInterfaceItemDto = new InsertInterfaceItemDto();
                        //insertInterfaceItemDto.DepartmentId = interfaceitem.DepartmentId;
                        //insertInterfaceItemDto.ItemGroupId = interfaceitem.ItemGroupId;
                        insertInterfaceItemDto.ItemInfoId = interfaceitem.ItemInfoId;
                        
                        insertInterfaceItemDto.ItemName = interfaceitem.ItemName;
                        insertInterfaceItemDto.ObverseItemId = interfaceitem.ObverseItemId;
                        insertInterfaceItemDto.ObverseItemName = interfaceitem.ObverseItemName;
                        insertInterfaceItemDto.InstrumentModelNumber = interfaceitem.InstrumentModelNumber;
                        insertInterfaceItemDto.Remarks = interfaceitem.Remarks;
                        insertInterfaceItemDto.Id = interfaceitem.Id;
                        var interfaceitemdto= interfaceItemAppService.SaveInterfaceItems(insertInterfaceItemDto);
                        if (interfaceitemdto.Id != Guid.NewGuid())
                        {
                            //  gridviewInterfaceItems.SetRowCellValue(row, CheckState, "导入成功");
                            // gridviewInterfaceItems.Set
                            // gridviewInterfaceItems.RefreshData();
                             interfaceitemdto.CheckState = "导入成功";
                             itemdata[interfaceItemsDto] = interfaceitemdto;
                            gridviewInterfaceItems.RefreshData();
                            oknum += 1;


                        }
                        else
                        {
                            interfaceitemdto.CheckState = "导入异常";
                            itemdata[interfaceItemsDto] = interfaceitemdto;
                            gridviewInterfaceItems.RefreshData();
                            errnum += 1;
                            //gridviewInterfaceItems.SetRowCellValue(row, CheckState, "导入异常");
                        }
                    }
                    else
                    {
                        gridviewInterfaceItems.SetRowCellValue(row, CheckState,"导入失败：没有对应ID");
                    }

                }
                gridControl1.Refresh();
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {
                for (int row=0;row < gridViewItemGroup.RowCount;row ++)
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
                        var savesult=  interfaceItemAppService.SaveInterfaceItemGroups(insertInterfaceItemGroupDto);
                        if (savesult.Id != Guid.NewGuid())
                        {
                            // gridviewInterfaceItems.SetRowCellValue(row, CheckState, "导入成功");
                            savesult.CheckState = "导入成功";
                            itemdata[interfaceItemgroupsDto] = savesult;
                            gridViewItemGroup.RefreshData();
                            oknum += 1;
                        }
                        else
                        {
                            // gridviewInterfaceItems.SetRowCellValue(row, CheckState, "导入异常");
                            savesult.CheckState = "导入异常";
                            itemdata[interfaceItemgroupsDto] = savesult;
                            gridViewItemGroup.RefreshData();
                            errnum += 1;

                        }
                      
                    }
                    else
                    {
                        gridViewItemGroup.SetRowCellValue(row, cCheckState, "导入失败：没有对应ID");
                    }
                }
                gridControl2.Refresh();
            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
                for (int row= 0; row < gridViewEmp.RowCount;row++)
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
                        var savesult = interfaceItemAppService.SaveInterfaceUser(insertInterfaceItemGroupDto);
                        if (savesult.Id != Guid.NewGuid())
                        {
                            //  gridviewInterfaceItems.SetRowCellValue(row, CheckState, "导入成功");
                            savesult.CheckState = "导入成功";
                            itemdata[interfaceItemgroupsDto] = savesult;
                            gridViewEmp.RefreshData();
                            oknum += 1;
                        }
                        else
                        {
                            // gridviewInterfaceItems.SetRowCellValue(row, CheckState, "导入异常");
                            savesult.CheckState = "导入异常";
                            itemdata[interfaceItemgroupsDto] = savesult;
                            gridViewEmp.RefreshData();
                            errnum += 1;
                        }
                    }
                    else
                    {
                        gridViewEmp.SetRowCellValue(row, conCheckState, "导入失败：没有对应ID");
                    }
                }
                gridControl3.Refresh();

            }
            if (errnum > 0 || oknum > 0)
            {
                MessageBox.Show("成功导入" + oknum +"条数据" + ",异常数据" + errnum+"条");
            }
        }
        /// <summary>
        /// 检查模板列是否正确
        /// </summary>
        /// <param name="names"></param>
        /// <param name="dtData"></param>
        /// <returns></returns>
        private bool checkCus(string names, DataTable dtData)
        {
            string[] ss = names.Split(',');
            bool isOK = true;
            foreach (string s in ss)
            {
                bool hscon = false;
                foreach (DataColumn con in dtData.Columns)
                {
                    if (s == con.ColumnName)
                    {
                        hscon = true;
                    }
                }
                if (hscon == false)
                {
                    isOK = false;
                    break;
                }
            }
            return isOK;

        }
    }
 
}

