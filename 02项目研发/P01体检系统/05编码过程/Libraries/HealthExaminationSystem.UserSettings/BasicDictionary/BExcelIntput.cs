using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary
{
    public partial class BExcelIntput : UserBaseForm
    {
        private IWorkbook _workbook;
        private readonly IBasicDictionaryAppService _OccDisProposalNewAppService;
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        private string isWZ = "";
       public string parentId = "";
        public string tName = "";
        public string key = "";
        public BExcelIntput()
        {
            InitializeComponent();

            _OccDisProposalNewAppService = new BasicDictionaryAppService();
        }
        public BExcelIntput(string _parentId, string _Name, string _key
            ) :this()
        {
            parentId = _parentId;
            tName = _Name;
            key = _key;
        }
        public string type = "";
        private void ExcelIntput_Load(object sender, EventArgs e)
        {

        }
 
        /// <summary>
        /// 选择文件导入
        /// </summary>
        public void ImportNew()
        {
            openFileDialog.Filter = "Excel(*.xls)|*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();
                AutoLoading(() =>
                {
                    dt = ExcelToDataTable(openFileDialog.FileName, "Sheet", true);
                });
                string Err = "";
                if (!dt.Columns.Contains("名称"))
                { Err = "模板中缺少'名称'列'\r\n"; }
                //if (!dt.Columns.Contains("分类"))
                //{ Err += "模板中缺少'分类'列'\r\n"; }
                if (!dt.Columns.Contains("编码"))
                { Err += "模板中缺少'平台编码'列'\r\n"; }

                if (Err != "")
                {
                    MessageBox.Show(Err);
                }
                else
                {
                    gridControlImportExcel.DataSource = dt;
                }

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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ImportNew();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            
            var data = gridControlImportExcel.DataSource as DataTable;
            if (data == null || data.Rows.Count == 0)
            {
                ShowMessageBoxInformation("没有名单可导入！");
                return;
            }
            int i = 0;
            List<CreateBasicDictionaryDto> OccDictionarylist = new List<CreateBasicDictionaryDto>();
            //操作数据库
            AutoLoading(() =>
            {
                foreach (DataRow dr in data.Rows)
            {
                if (string.IsNullOrEmpty(dr["名称"].ToString()))
                {
                    continue;
                }
                    #region MyRegion
                    CreateBasicDictionaryDto dto = new CreateBasicDictionaryDto();
                dto.Text = dr["名称"].ToString();  
                if (data.Columns.Contains("备注"))
                {
                    dto.Remarks = dr["备注"].ToString();

                }              
                dto.OrderNum = i;
                dto.Code = dr["编码"].ToString();                
                dto.Type = key;                   
                OccDictionarylist.Add(dto);
                #endregion
            }
            _OccDisProposalNewAppService.InExcel(OccDictionarylist);
            });
            MessageBox.Show("导入成功！");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var strList = new List<string>() {
                "编码",
                "名称",         
                "备注",
            };
            JArray mb_jarray = new JArray();
            List<int> cellIndexs = new List<int>();
            GridControlHelper.DownloadTemplate(strList, "字典导入模板", mb_jarray, cellIndexs, "yyyy-MM-dd");

        }
    }
}
