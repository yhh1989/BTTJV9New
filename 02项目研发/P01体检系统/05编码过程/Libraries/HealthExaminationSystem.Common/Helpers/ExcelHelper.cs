using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers

{
    public class ExcelHelper : IDisposable
    {
        #region DataTable 导出 excel

        private readonly string _fileName; //文件名

        private FileStream _fs;

        private IWorkbook _workbook;

        public ExcelHelper(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// 将 DataTable 数据导入到 Excel 中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable 的列名是否要导入</param>
        /// <param name="sheetName">要导入的 Excel 的 Sheet 的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            using (_fs = new FileStream(_fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                if (_fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                {
                    // 2007版本
                    _workbook = new XSSFWorkbook();
                }
                else if (_fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
                {
                    // 2003版本
                    _workbook = new HSSFWorkbook();
                }

                try
                {
                    ISheet sheet;
                    if (_workbook != null)
                    {
                        sheet = _workbook.CreateSheet(sheetName);
                    }
                    else
                    {
                        return -1;
                    }

                    int j;
                    int count;
                    if (isColumnWritten)
                    {
                        // 写入 DataTable 的列名
                        var row = sheet.CreateRow(0);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    int i;
                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        var row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }
                        ++count;
                    }
                    //设置自适应列宽
                    for (j = 0; j < data.Columns.Count; j++)
                    {
                        sheet.AutoSizeColumn(j);
                    }
                    // 写入到 Excel
                    _workbook.Write(_fs);

                    return count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Exception: " + ex.Message);
                    return -1;
                }
                finally
                {
                    if (_fs != null)
                    {
                        if (_fs.CanWrite)
                            _fs.Flush();
                        _fs.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 将 DataTable 数据导入到 Excel 中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable 的列名是否要导入</param>
        /// <param name="sheetName">要导入的 Excel 的 Sheet 的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataSet ds, string sheetName, bool isColumnWritten)
        {
            int count=0;
            using (_fs = new FileStream(_fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                if (_fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                {
                    // 2007版本
                    _workbook = new XSSFWorkbook();
                }
                else if (_fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
                {
                    // 2003版本
                    _workbook = new HSSFWorkbook();
                }

                try
                {
                    int si = 1;
                    foreach (DataTable data in ds.Tables)
                    {
                        ISheet sheet;
                        if (_workbook != null)
                        {
                            sheet = _workbook.CreateSheet(sheetName + si.ToString());
                            si = si + 1;
                        }
                        else
                        {
                            return -1;
                        }
                        int j;
                        //int count;
                        if (isColumnWritten)
                        {
                            // 写入 DataTable 的列名
                            var row = sheet.CreateRow(0);
                            for (j = 0; j < data.Columns.Count; ++j)
                            {
                                row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            }
                            count = 1;
                        }
                        else
                        {
                            count = 0;
                        }

                        int i;
                        for (i = 0; i < data.Rows.Count; ++i)
                        {
                            var row = sheet.CreateRow(count);
                            for (j = 0; j < data.Columns.Count; ++j)
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                            }
                            ++count;
                        }
                        //设置自适应列宽
                        for (j = 0; j < data.Columns.Count; j++)
                        {
                            sheet.AutoSizeColumn(j);
                        }
                        // 写入到 Excel
                        _workbook.Write(_fs);

                    }
                    return count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Exception: " + ex.Message);
                    return -1;
                }
                finally
                {
                    if (_fs != null)
                    {
                        if (_fs.CanWrite)
                            _fs.Flush();
                        _fs.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 将 Excel 中的数据导入到 DataTable 中
        /// </summary>
        /// <param name="sheetName">Excel 工作薄 Sheet 的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是 DataTable 的列名</param>
        /// <returns>返回的 DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            var data = new DataTable();
            try
            {
                _fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
                if (_fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                {
                    // 2007版本
                    _workbook = new XSSFWorkbook(_fs);
                }
                else if (_fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
                {
                    // 2003版本
                    _workbook = new HSSFWorkbook(_fs);
                }

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
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            var cell = firstRow.GetCell(i);
                            var cellValue = cell?.StringCellValue;

                            if (cellValue != null)
                            {
                                var column = new DataColumn(cellValue);
                                data.Columns.Add(column);
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

                        var dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是 NULL
                            if (row.GetCell(j) != null)
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }

                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Exception: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            if (_fs != null)
                _fs.Dispose();
        }

        #endregion

        #region Dev 导出 excel

        /// <summary>
        /// DevExpress通用导出Excel,支持多个控件同时导出在同一个Sheet表
        /// eg:ExportToXlsx("",gridControl1,gridControl2);
        /// 将gridControl1和gridControl2的数据一同导出到同一张工作表
        /// </summary>
        /// <param name="title">文件名</param>
        /// <param name="panels">控件集</param>
        public static void ExportToExcel(string title, params DevExpress.XtraPrinting.IPrintable[] panels)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = title;
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            //          while (System.IO.File.Exists(saveFileDialog.FileName) &&
            //DevExpress.XtraEditors.XtraMessageBox.Show("该文件名已存在，是否覆盖？",
            //"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //          {
            //              if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //                  saveFileDialog.OverwritePrompt = false;
            //          }
            string FileName = saveFileDialog.FileName;
            var ps = new DevExpress.XtraPrinting.PrintingSystem();
            var link = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
            ps.Links.Add(link);
            foreach (var panel in panels)
            {
                link.Links.Add(CreatePrintableLink(panel));
            }
            link.Landscape = true;//横向
                                  //判断是否有标题，有则设置
                                  //link.CreateDocument(); //建立文档
            int count = 1;
            //在重复名称后加（序号）
            while (System.IO.File.Exists(FileName))
            {
                if (FileName.Contains(")."))
                {
                    int start = FileName.LastIndexOf("(");
                    int end = FileName.LastIndexOf(").") - FileName.LastIndexOf("(") + 2;
                    FileName = FileName.Replace(FileName.Substring(start, end), string.Format("({0}).", count));
                }
                else
                {
                    FileName = FileName.Replace(".", string.Format("({0}).", count));
                }
                count++;
            }
            if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
            {
                var options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXlsx(FileName, options);
            }
            else
            {
                var options = new DevExpress.XtraPrinting.XlsExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXls(FileName, options);
            }
            var question = DevExpress.XtraEditors.XtraMessageBox.Show("保存成功，是否打开文件？", "提示",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question);
            if (question == System.Windows.Forms.DialogResult.Yes)
                System.Diagnostics.Process.Start(FileName);//打开指定路径下的文件
        }

        /// <summary>
        /// 创建打印Componet
        /// </summary>
        /// <param name="printable"></param>
        /// <returns></returns>
        public static DevExpress.XtraPrinting.PrintableComponentLink CreatePrintableLink(DevExpress.XtraPrinting.IPrintable printable)
        {
            var chart = printable as DevExpress.XtraCharts.ChartControl;
            if (chart != null)
                chart.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Stretch;
            var printableLink = new DevExpress.XtraPrinting.PrintableComponentLink() { Component = printable };
            return printableLink;
        }
        #region 导出到excel多sheet
        /// <summary>
        /// 将 DataTable 数据导入到 Excel 中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable 的列名是否要导入</param>
        /// <param name="sheetName">要导入的 Excel 的 Sheet 的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public void DataSetToExcel(DataSet ds, bool isColumnWritten)
        {
            using (_fs = new FileStream(_fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                if (_fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                {
                    // 2007版本
                    _workbook = new XSSFWorkbook();
                }
                else if (_fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
                {
                    // 2003版本
                    _workbook = new HSSFWorkbook();
                }
                foreach (DataTable data in ds.Tables)
                {
                    try
                    {
                        ISheet sheet;
                        if (_workbook != null)
                        {
                            sheet = _workbook.CreateSheet(data.TableName);
                        }
                        else
                        {
                            return ;
                        }

                        int j;
                        int count;
                        if (isColumnWritten)
                        {
                            // 写入 DataTable 的列名
                            var row = sheet.CreateRow(0);
                            for (j = 0; j < data.Columns.Count; ++j)
                            {
                                row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            }
                            count = 1;
                        }
                        else
                        {
                            count = 0;
                        }

                        int i;
                        for (i = 0; i < data.Rows.Count; ++i)
                        {
                            var row = sheet.CreateRow(count);
                            for (j = 0; j < data.Columns.Count; ++j)
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                            }
                            ++count;
                        }
                        //设置自适应列宽
                        for (j = 0; j < data.Columns.Count; j++)
                        {
                            sheet.AutoSizeColumn(j);
                        }
                        // 写入到 Excel
                        _workbook.Write(_fs);
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Exception: " + ex.Message);
                        return ;
                    }
                    finally
                    {
                        if (_fs != null)
                        {
                            if (_fs.CanWrite)
                                _fs.Flush();
                            _fs.Close();
                        }
                    }
                }
            }
        }


        #endregion
        /// <summary>
        /// DevExpress控件通用导出Excel,支持多个控件同时导出在同一个Sheet表或者分不同工作薄
        /// eg:ExportToXlsx("test",true,"控件",gridControl1,gridControl2);
        /// 将gridControl1和gridControl2的数据一同导出到同一个文件不同的工作薄
        /// eg:ExportToXlsx("test",false,"",gridControl1,gridControl2);
        /// 将gridControl1和gridControl2的数据一同导出到同一个文件同一个的工作薄
        /// <param name="title">文件名</param>
        /// <param name="isPageForEachLink">多个打印控件是否分多个工作薄显示</param>
        /// <param name="sheetName">工作薄名称</param>
        /// <param name="printables">控件集 eg:GridControl,PivotGridControl,TreeList,ChartControl...</param>
        public static void ExportToExcel(string title, bool isPageForEachLink, string sheetName, params DevExpress.XtraPrinting.IPrintable[] printables)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = title,
                Title = "导出Excel",
                Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls"
            };
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.Cancel)
                return;
            string FileName = saveFileDialog.FileName;
            DevExpress.XtraPrintingLinks.CompositeLink link = new DevExpress.XtraPrintingLinks.CompositeLink(new DevExpress.XtraPrinting.PrintingSystem());
            foreach (var item in printables)
            {
                var plink = new DevExpress.XtraPrinting.PrintableComponentLink() { Component = item };
                link.Links.Add(plink);
            }
            if (isPageForEachLink)//15.1 的Xls不支持这个功能，15.2未知
                link.CreatePageForEachLink();
            if (string.IsNullOrEmpty(sheetName)) sheetName = "Sheet";//默认工作薄名称
            try
            {
                int count = 1;
                //在重复名称后加（序号）
                while (System.IO.File.Exists(FileName))
                {
                    if (FileName.Contains(")."))
                    {
                        int start = FileName.LastIndexOf("(");
                        int end = FileName.LastIndexOf(").") - FileName.LastIndexOf("(") + 2;
                        FileName = FileName.Replace(FileName.Substring(start, end), string.Format("({0}).", count));
                    }
                    else
                    {
                        FileName = FileName.Replace(".", string.Format("({0}).", count));
                    }
                    count++;
                }
                if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
                {
                    DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = sheetName };
                    if (isPageForEachLink)
                        options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage;
                    link.ExportToXlsx(FileName, options);
                }
                else
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions() { SheetName = sheetName };
                    if (isPageForEachLink) //15.Xls没有这个属性，15.2未知
                        options.ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFilePageByPage;
                    link.ExportToXls(FileName, options);
                }
                if (DevExpress.XtraEditors.XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    System.Diagnostics.Process.Start(FileName);//打开指定路径下的文件
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// GridView导出为EXCEL
        /// </summary>
        /// <param name="gv">以表格形式显示数据的视图</param>
        /// <param name="FileName">文件名</param>
        /// <param name="SheetName">工作表名</param>
        public static string GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView gv, string FileName, string SheetName = "")
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel 工作簿(*.xlsx)|*.xlsx";
            if (FileName.Trim() != "")
                fileDialog.FileName = FileName;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsxExportOptionsEx op = new DevExpress.XtraPrinting.XlsxExportOptionsEx();

                op.ExportType = DevExpress.Export.ExportType.WYSIWYG;//所见即所得
                op.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;//指定XLSX导出模式-->单一文件
                op.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;//指定是否在导出的XLS(或XLSX)文档中使用绑定数据集中的数据字段的格式-->使用与原始文档中相同的格式
                if (SheetName.Trim() != "")
                    op.SheetName = SheetName;//工作簿名称

                gv.OptionsPrint.PrintHeader = true;//是否打印行头
                gv.OptionsPrint.AutoWidth = false;//获取或设置输出/导出输出中的列的宽度是否会自动改变，以便视图与页面宽度相匹配。
                gv.OptionsPrint.AllowCancelPrintExport = true;  //获取或设置打印/导出进度窗口是否包含一个取消按钮，这允许终端用户取消当前的打印/导出操作。
                gv.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 10);
                gv.AppearancePrint.Row.Font = new System.Drawing.Font("微软雅黑", 10);
                gv.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("微软雅黑", 10);
                //gv.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;//设置偶数行居中，必须设置gv.OptionsPrint.EnableAppearanceEvenRow = true;
                //gv.OptionsPrint.EnableAppearanceEvenRow = true;
                try
                {
                    gv.ExportToXlsx(fileDialog.FileName, op);
                   
                    DevExpress.XtraEditors.XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show("导出失败,原因：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            return fileDialog.FileName;
        }


        
        #endregion


    }

}