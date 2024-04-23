using Abp.Application.Services.Dto;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class GridControlHelper
    {
        /// <summary>
        /// 获取 List_Dto 数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        public static List<T> GetDtoListDataSource<T>(this GridControl gridControl)
            where T : class
        {
            return gridControl.DataSource == null ? null : (List<T>)gridControl.DataSource;
        }
        /// <summary>
        /// 移除 List_Dto 数据源中的项目，并刷新数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        public static void RemoveDtoListItem<T>(this GridControl gridControl, T item)
            where T : EntityDto<Guid>
        {
            gridControl.GetDtoListDataSource<T>()?.Remove(item);
            gridControl.RefreshDataSource();
        }
        /// <summary>
        /// 移除 List_Dto 数据源中的项目，并刷新数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        public static void RemoveDtoListItem<T>(this GridControl gridControl, List<T> item)
            where T : EntityDto<Guid>
        {
            gridControl.GetDtoListDataSource<T>()?.RemoveAll(m => item.Any(i => i.Id == m.Id));
            gridControl.RefreshDataSource();
        }
        /// <summary>
        /// 添加 List_Dto 数据源中的项目，并刷新数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <param name="item"></param>
        /// <param name="unique">唯一，已有则返回false</param>
        /// <returns></returns>
        public static bool AddDtoListItem<T>(this GridControl gridControl, T item, bool unique = false)
            where T : EntityDto<Guid>
        {
            var list = gridControl.GetDtoListDataSource<T>();
            if (list == null)
            {
                list = new List<T>();
                gridControl.DataSource = list;
            }
            if (unique && list.Any(m => m.Id == item.Id))
            {
                return false;
            }
            list.Add(item);
            //list.Insert(0, item);
            gridControl.RefreshDataSource();
            return true;
        }
        /// <summary>
        /// 添加 List_Dto 数据源中的项目，并刷新数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <param name="item"></param>
        /// <param name="unique">唯一，已有则不添加</param>
        /// <returns></returns>
        public static void AddDtoListItem<T>(this GridControl gridControl, List<T> items, bool unique = false)
            where T : EntityDto<Guid>
        {
            var list = gridControl.GetDtoListDataSource<T>();
            if (list == null)
            {
                list = new List<T>();
                gridControl.DataSource = list;
            }
            items.RemoveAll(m => list.Any(s => s.Id == m.Id));
            list.AddRange(items);
            gridControl.RefreshDataSource();
        }
        /// <summary>
        /// 添加 List_Dto 数据源中的项目，并刷新数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <param name="item"></param>
        /// <param name="unique">可重复添加</param>
        /// <returns></returns>
        public static void AddAllDtoListItem<T>(this GridControl gridControl, List<T> items, bool unique = false)
            where T : EntityDto<Guid>
        {
            var list = gridControl.GetDtoListDataSource<T>();
            if (list == null)
            {
                list = new List<T>();
                gridControl.DataSource = list;
            }           
            list.AddRange(items);
            gridControl.RefreshDataSource();
        }
        /// <summary>
        /// 获取当前行Dto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        public static T GetFocusedRowDto<T>(this GridControl gridControl)
            where T : EntityDto<Guid>
        {
            var gridView = gridControl.FocusedView as GridView;
            if (gridView == null)
            {
                return null;
            }

            if (gridView.FocusedRowHandle < 0)
            {
                var index = gridView.GetDataRowHandleByGroupRowHandle(gridView.FocusedRowHandle);
                if (index < 0)
                {
                    return null;
                }
                return gridView.GetRow(index) as T;
            }
            return gridView.GetFocusedRow() as T;
        }
        /// <summary>
        /// 获取当前选择的行的Dto列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        public static List<T> GetSelectedRowDtos<T>(this GridControl gridControl)
            where T : EntityDto<Guid>
        {
            List<T> list = new List<T>();
            var gridView = gridControl.FocusedView as GridView;
            foreach (var i in gridView.GetSelectedRows())
            {
                var dto = (T)gridView.GetRow(i);
                list.Add(dto);
            }
            return list;
        }


        /// <summary>
        /// 导出Excel模板（只含标头）
        /// </summary>
        /// <param name="strList">标题名称</param>
        /// <param name="fileName">文件名称</param>
        public static void ExportByGridControl(List<string> strList, string fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)   //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
                return;
            //XlsExportOptions options = new XlsExportOptions();
            var FileName = saveFileDialog.FileName;
            GridControl grid = new GridControl();
            grid.BindingContext = new BindingContext();//绑定内容实例，否则需要在界面显示才会自动实例
            GridView view = new GridView(grid);
            grid.MainView = view;
            //grid.DataSource = dt;
            if (strList != null)
            {
                int i = 0;
                foreach (var item in strList)
                {
                    view.Columns.Add(new GridColumn() { Name = item, Caption = item, VisibleIndex = i });

                    i++;
                }
            }
            grid.ForceInitialize();//强制初始化
            //XlsxExportOptions xoptions = new XlsxExportOptions();
            grid.ExportToXlsx(FileName);
            if (DevExpress.XtraEditors.XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                System.Diagnostics.Process.Start(FileName);//打开指定路径下的文件
        }
        /// <summary>
        /// 导出名单下拉模板
        /// </summary>
        /// <param name="celNameList"></param>
        /// <param name="mb_jarray"></param>
        /// <param name="cellIndexs"></param>
        /// <param name="timeFormat"></param>
        public static void DownloadTemplate(List<string> celNameList,string fileName, JArray mb_jarray, List<int> cellIndexs, string timeFormat)
        {
            // 创建新的Excel 工作簿
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            string saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消 
            bool isXSSF = true;
            IWorkbook workbook = null;
            try
            {
                workbook = new XSSFWorkbook();
            }
            catch (Exception ex)
            {
                workbook = new HSSFWorkbook();
                isXSSF = false;
            }
            try
            {
                if (workbook != null)
                {
                    ISheet sheet = workbook.CreateSheet("人员名单");
                    try
                    {
                        XSSFCellStyle lo_Style = (XSSFCellStyle)workbook.CreateCellStyle();
                        lo_Style.DataFormat = HSSFDataFormat.GetBuiltinFormat("text");
                        var cell = sheet.GetColumnStyle(8);
                        cell.DataFormat = HSSFDataFormat.GetBuiltinFormat("text");
                        
                    }
                    catch (Exception ex)
                    {
                        HSSFCellStyle lo_Style = (HSSFCellStyle)workbook.CreateCellStyle();
                        lo_Style.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                    }
                   

                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < celNameList.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(celNameList[i]);
                    }
                    ISheet sheet1 = workbook.GetSheetAt(0);//获得第一个工作表 
                    if (isXSSF)
                    {
                        XSSFDataValidationHelper helper = new XSSFDataValidationHelper((XSSFSheet)sheet1);//获得一个数据验证Helper  
                        for (int i = 0; i < mb_jarray.Count; i++)
                        {
                            try
                            {
                               
                                JArray array = (JArray)JsonConvert.DeserializeObject(mb_jarray[i]["vlaue"].ToString());
                               
                                 CellRangeAddressList regions = new CellRangeAddressList(1, 65535, int.Parse(mb_jarray[i]["cel"].ToString()), int.Parse(mb_jarray[i]["cel"].ToString()));
                                IDataValidation validation = helper.CreateValidation(helper.CreateExplicitListConstraint(array.ToObject<List<string>>().ToArray()), regions);//创建约束
                                validation.CreateErrorBox("错误", "请按右侧下拉箭头选择!");//不符合约束时的提示  
                                validation.ShowErrorBox = true;//显示上面提示 = True  
                                sheet1.AddValidationData(validation);//添加进去 
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        HSSFDataValidationHelper helper = new HSSFDataValidationHelper((HSSFSheet)sheet1);//获得一个数据验证Helper  

                        for (int i = 0; i < mb_jarray.Count; i++)
                        {
                            JArray array = (JArray)JsonConvert.DeserializeObject(mb_jarray[i]["vlaue"].ToString());

                            CellRangeAddressList regions = new CellRangeAddressList(1, 65535, int.Parse(mb_jarray[i]["cel"].ToString()), int.Parse(mb_jarray[i]["cel"].ToString()));
                            IDataValidation validation = helper.CreateValidation(helper.CreateExplicitListConstraint(array.ToObject<List<string>>().ToArray()), regions);//创建约束
                            validation.CreateErrorBox("错误", "请按右侧下拉箭头选择!");//不符合约束时的提示  
                            validation.ShowErrorBox = true;//显示上面提示 = True  
                            sheet1.AddValidationData(validation);//添加进去 
                        }
                    }
                 
                    sheet1.ForceFormulaRecalculation = true;
                    if (cellIndexs.Count > 0)
                    {
                        ICellStyle style0 = workbook.CreateCellStyle();
                        IDataFormat dataformat = workbook.CreateDataFormat();
                        style0.DataFormat = dataformat.GetFormat(timeFormat);
                        for (int i = 0; i < cellIndexs.Count; i++)
                        {
                            row.GetCell(cellIndexs[i]).CellStyle = style0;
                        }
                    }

                    if (!File.Exists(saveFileName))
                    {
                        FileStream fs = File.Create(saveFileName);
                        fs.Close();
                    }
                    FileStream outputStream = new FileStream(saveFileName, FileMode.Open, FileAccess.Write);
                    workbook.Write(outputStream);                   
                    outputStream.Close();
                    sheet = null;
                    row = null;
                    workbook = null;
                    MessageBox.Show("数据导出", "提示");
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        /// <summary>
        /// 重写Npoi方法
        /// </summary>
        public class NpoiMemoryStream : MemoryStream
        {
            public NpoiMemoryStream()
            {
                AllowClose = true;
            }

            public bool AllowClose { get; set; }

            public override void Close()
            {
                if (AllowClose)
                    base.Close();
            }
        }

    }
}
