using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System.Data.OleDb;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Newtonsoft.Json;
using Abp.Application.Services.Dto;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using Newtonsoft.Json.Linq;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmIntReSult : UserBaseForm
    {
        #region 构造
        private PictureController _pictureController;
        /// <summary>
        /// 项目参考值
        /// </summary>
        private List<SearchItemStandardDto> _itemStandardSys;
        private readonly IClientRegAppService _service = new ClientRegAppService();
        private ICustomerAppService customerSvr = new CustomerAppService();//体检预约
        private List<TbmDepartmentDto> Departments;//科室数据

        public Guid ClientRegId;//单位预约ID
        private FrmClientRegCustomerList frmCliRegCustomer;
        //编码
        IIDNumberAppService _iIdNumber = new IDNumberAppService();
        private IWorkbook _workbook;

        public ClientTeamRegDto cteDto;  //单位团体预约Dto
        private IChargeAppService _chargeAppService { get; set; }

        private IIDNumberAppService iIDNumberAppService;

        private readonly IDoctorStationAppService _doctorStation;
        #endregion

        public FrmIntReSult()
        {
            InitializeComponent();
            excelDs.Fill();
            _chargeAppService = new ChargeAppService();
            iIDNumberAppService = new IDNumberAppService();
            _doctorStation = new DoctorStationAppService();


        }


        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string FormatDatetime(object arg)
        {
            try
            {
                return DateTime.FromOADate(Convert.ToInt32(arg)).ToString();

                //return _sexModels.Find(r => r.Id == (int)arg).Display;
            }
            catch (Exception)
            {
                return null;
                //return _sexModels.Find(r => r.Id == (int)Sex.GenderNotSpecified).Display;
            }
        }

        #region 系统事件

        /// <summary>
        /// 选择文件导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
           
            ImportNew();
        }

        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            
            if (ImportDataSouceNew())
            {
                DialogResult = DialogResult.OK;
                ShowMessageSucceed("导入成功！");
            }
        }


        /// <summary>
        /// grid样式展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gdvImportExcel_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;           
         
        }

        #endregion

        #region 实现方法

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
                if (!dt.Columns.Contains("体检号"))
                { Err = "模板中缺少'体检号'列'\r\n"; }
                if (!dt.Columns.Contains("年龄"))
                {  Err += "模板中缺少'年龄'列'\r\n"; }
                if (!dt.Columns.Contains("性别"))
                { Err += "模板中缺少'性别'列'\r\n"; }            
                foreach (var con in dt.Columns)
                {
                    if(con.ToString() != "体检号" && con.ToString() != "年龄" && con.ToString() != "性别" && con.ToString() != "姓名")
                    {
                        if (!DefinedCacheHelper.GetItemInfos().Any(o => o.Name == con.ToString()))
                        {
                            Err += "项目名称：" + con.ToString() + ",不存在不能导入系统！\r\n";
                        }
                    }
                }
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
                            if (!string.IsNullOrEmpty(dataRow["图片路径"]?.ToString()))
                            {
                              var PicPath =  Path.GetDirectoryName(_fileName);
                                if (Directory.Exists(PicPath))
                                {
                                    var file = PicPath +"\\" + dataRow["图片路径"]?.ToString() + ".jpg";
                                    var filepdf = PicPath + "\\"+ dataRow["图片路径"]?.ToString() + ".pdf";
                                    if (File.Exists(file))
                                    {
                                        dataRow[j] = file;
                                    }
                                    else if (File.Exists(filepdf))
                                    {
                                        dataRow[j] = filepdf;

                                    }
                                    else
                                    {
                                        dataRow[j] = "";
                                    }
                                }
                            }
                        }
                    }                   
                   
                    //判断增加分组信息
                    //编码为空时判断                   
                    data.Rows.Add(dataRow);
                }
                
            
            }

            return data;
            
        }

        /// <summary>
        /// 数据导入数据库
        /// </summary>
        public bool ImportDataSouce()
        {
            // 不判断 data 是否为空就直接筛选数据？
            // 你底下再判断 data 为空有什么用。
            // 如果 data 为空，上面就导致程序崩溃了。
            var data = gridControlImportExcel.DataSource as DataTable;
            if (data == null || data.Rows.Count == 0)
            {
                ShowMessageBoxInformation("没有名单可导入！");
                return false;
            }
            string iszcz = "";
            var isshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 4)?.Remarks;
            if (!string.IsNullOrEmpty(isshow))
            {
                iszcz = isshow;
            }
            string Err = "";
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.Properties.Maximum = data.Rows.Count;
                progressBarControl1.Properties.Step = 1;
                progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                progressBarControl1.Position = 0;
                progressBarControl1.Properties.ShowTitle = true;
                progressBarControl1.Properties.PercentView = true;
                progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                label1.Text = "共：" + data.Rows.Count+"条数据";

                System.Windows.Forms.Application.DoEvents();
                int num = 1;
               
                foreach (DataRow dr in data.Rows)
                {
                    label2.Text = "第：" + num + "条";
                    label2.Refresh();
                    var CustomerItemGroup = new List<UpdateCustomerItemGroupDto>();
                    var Query = new QueryClass();
                    Query.CustomerBM = dr["体检号"].ToString();
                    if (string.IsNullOrEmpty(Query.CustomerBM))
                    {
                        //执行步长
                        progressBarControl1.PerformStep();
                        num = num + 1;
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                        Err +="第"+ num + "行体检号为空不能导入 \r\n";
                        continue;
                    }
                    int Age=int.Parse( dr["年龄"].ToString());
                    int cusSex = 1;
                    if (dr["性别"].ToString().Contains("女"))
                    {
                        cusSex = 2;
                    }
                    //只显示本科室或异常科室
                    var _currentCustomerItemGroupSys = new List<ATjlCustomerItemGroupDto>();
                    _currentCustomerItemGroupSys = _doctorStation.GetCustomerItemGroupByBm(Query).ToList();
                    foreach (var itemGroup in _currentCustomerItemGroupSys)
                    {
                        if (itemGroup.CheckState == (int)ProjectIState.GiveUp)
                        {
                            Err += "体检号：" + Query.CustomerBM + "，组合名称："+ itemGroup.ItemGroupName + "为放弃状态不能导入 \r\n";
                            continue;
                        }
                        
                        var GroupDto = new UpdateCustomerItemGroupDto();
                        GroupDto.Id = itemGroup.Id; //id
                        GroupDto.CheckState = (int)ProjectIState.Complete; //状态
                       
                        GroupDto.BillingEmployeeBMId = CurrentUser.Id; //开单医生
                        GroupDto.InspectEmployeeBMId = CurrentUser.Id;
                        GroupDto.CheckEmployeeBMId = CurrentUser.Id;
                        GroupDto.FirstDateTime = DateTime.Now;
                        GroupDto.CustomerRegItem = new List<UpdateCustomerRegItemDto>();

                        //项目
                        foreach (var item in itemGroup.CustomerRegItem)
                        {
                            if (!data.Columns.Contains(item.ItemName))
                            {
                                //Err += "体检号：" + Query.CustomerBM + "，项目名称：" + item.ItemName + "，名称重复不能导入 \r\n";
                                continue;
                            }
                          
                            var ItemDto = new UpdateCustomerRegItemDto();
                            ItemDto.Id = item.Id; //id                            
                           
                            var SexGenderNotSpecified = ((int)Sex.GenderNotSpecified).ToString();
                            ItemDto.InspectEmployeeBMId = CurrentUser.Id;
                            ItemDto.StandFrom = (int)ExamPlace.GoOut;
                            ItemDto.CheckEmployeeBMId = CurrentUser.Id;
                            ItemDto.ItemResultChar = dr[item.ItemName].ToString(); //结果
                            var itemData = _itemStandardSys.Where(o =>
                                    o.ItemId == item.ItemId && o.PositiveSate == (int)PositiveSate.Normal &&
                                    o.MaxAge >=Age  && o.MinAge <= Age &&
                                    (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified))
                                .FirstOrDefault();
                            if (itemData != null)
                            {
                                
                                item.Stand = (itemData.MinValue + "-" + itemData.MaxValue).Replace(".00", "");
                            }
                            ItemDto.Stand = item.Stand;
                            ItemDto.Unit = item.Unit;
                            item.ItemResultChar = ItemDto.ItemResultChar;
                            item.ProcessState = ItemDto.ProcessState;
                            
                            #region 项目小结
                            //判断项目是否异常
                            var IsItemAbnormal = false;
                            //判断院内院外
                            //if (item.StandFrom == (int)ExamPlace.Hospital || item.StandFrom == null)
                            if (true)
                            {
                                //判断是不是已经完成检查
                                if (ItemDto.ProcessState == (int)ProjectIState.Complete)
                                {
                                    //数值型判断
                                    if ((item.ItemTypeBM == (int)ItemType.Number || item.ItemTypeBM == (int)ItemType.Calculation) && ItemDto.ItemResultChar != null && ItemDto.ItemResultChar != "")
                                    {
                                        //&& o.CheckType == (int)ResultJudgementState.BigOrSmall
                                        var rtItemResultChar = Convert.ToDecimal(item.ItemResultChar);//int.Parse(Item.ItemResultChar);
                                        var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar).FirstOrDefault();
                                        if (ItemStandardDto != null)
                                        {
                                            //重度等级
                                            item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                            
                                            //判断是否异常
                                            if (ItemStandardDto.PositiveSate == (int)PositiveSate.Abnormal)
                                            {
                                                if (ItemStandardDto.IsNormal == (int)Symbol.High)
                                                {
                                                    if (iszcz == "N")
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↑{2})", ItemStandardDto.Summ, item.ItemResultChar, item.Unit);
                                                        ItemDto.ItemSum = item.ItemSum  ;
                                                    }
                                                    else
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↑{2})（正常值：{3}）", ItemStandardDto.Summ, item.ItemResultChar, item.Unit, item.Stand);
                                                        ItemDto.ItemSum = item.ItemSum;
                                                    }
                                                    IsItemAbnormal = true;
                                                }
                                                else if (ItemStandardDto.IsNormal == (int)Symbol.Superhigh)
                                                {
                                                    if (iszcz == "N")
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↑↑{2})", ItemStandardDto.Summ, item.ItemResultChar, item.Unit);
                                                    }
                                                    else
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↑↑{2})（正常值：{3}）", ItemStandardDto.Summ, item.ItemResultChar, item.Unit, item.Stand);
                                                    }
                                                    IsItemAbnormal = true;
                                                    ItemDto.ItemSum = item.ItemSum;
                                                }
                                                else if (ItemStandardDto.IsNormal == (int)Symbol.Low)
                                                {
                                                    if (iszcz == "N")
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↓{2})", ItemStandardDto.Summ, item.ItemResultChar, item.Unit);
                                                    }
                                                    else
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↓{2})（正常值：{3}）", ItemStandardDto.Summ, item.ItemResultChar, item.Unit, item.Stand);
                                                    }
                                                    IsItemAbnormal = true;
                                                    ItemDto.ItemSum = item.ItemSum;
                                                }
                                                else if (ItemStandardDto.IsNormal == (int)Symbol.UltraLow)
                                                {
                                                    if (iszcz == "N")
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↓↓{2})", ItemStandardDto.Summ, item.ItemResultChar, item.Unit);
                                                    }
                                                    else
                                                    {
                                                        item.ItemSum = string.Format(@"{0}({1}↓↓{2})（正常值：{3}）", ItemStandardDto.Summ, item.ItemResultChar, item.Unit, item.Stand);
                                                    }
                                                    item.PositiveSate = (int)PositiveSate.Abnormal;
                                                    IsItemAbnormal = true;
                                                    ItemDto.ItemSum = item.ItemSum;
                                                    ItemDto.PositiveSate = item.PositiveSate;
                                                }
                                                //else if (ItemStandardDto.IsNormal == 4)
                                                //{
                                                //    Item.ItemSum = string.Format(@"{0}({1}↓↓{2})（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                //    IsItemAbnormal = true;
                                                //}
                                                else
                                                {
                                                    if (iszcz == "N")
                                                    {
                                                        item.ItemSum = string.Format(@"{0}", ItemStandardDto.Summ);
                                                    }
                                                    else
                                                    {
                                                        item.ItemSum = string.Format(@"{0}（正常值：{1}）", ItemStandardDto.Summ, item.Stand);
                                                    }
                                                    ItemDto.ItemSum = item.ItemSum;
                                                   
                                                }
                                            }
                                            else
                                            {
                                                if (iszcz == "N")
                                                {
                                                    item.ItemSum = ItemStandardDto.Summ;
                                                }
                                                else
                                                {
                                                    item.ItemSum = ItemStandardDto.Summ + "（正常值：" + item.Stand + "）";
                                                }
                                                ItemDto.ItemSum = item.ItemSum;
                                            }

                                            item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                            if (item.Symbol == SymbolHelper.SymbolFormatter((int)Symbol.Superhigh) || item.Symbol == SymbolHelper.SymbolFormatter((int)Symbol.UltraLow))
                                            {
                                                item.CrisisSate = (int)CrisisSate.Abnormal;
                                            }
                                            else
                                            {
                                                item.CrisisSate = (int)CrisisSate.Normal;
                                            }
                                            ItemDto.CrisisSate = item.CrisisSate;
                                        }
                                        else
                                        {

                                            item.ItemSum = "";
                                            item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                                            ItemDto.ItemSum = item.ItemSum;
                                       

                                        }
                                    }
                                    //说明形判断
                                    else if (item.ItemTypeBM == (int)ItemType.Explain)
                                    {
                                        var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified) && o.Summ == item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                                        if (ItemStandardDto != null)
                                        {
                                            //重度等级
                                            item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                            item.ItemSum = ItemStandardDto.Summ;
                                            item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                            if (ItemStandardDto.IsNormal != 4)
                                            {
                                                IsItemAbnormal = true;
                                            }
                                          
                                            ItemDto.ItemSum = item.ItemSum;
                                        }
                                        else
                                        {
                                            var ItemStandardDtoNull = _itemStandardSys.Where(o => o.ItemId == item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified) && item.ItemResultChar.Contains(o.Summ) && o.CheckType == (int)ResultJudgementState.ContainText).FirstOrDefault();
                                            if (ItemStandardDtoNull != null)
                                            {
                                                //重度等级
                                                item.IllnessLevel = ItemStandardDtoNull.IllnessLevel;
                                                item.ItemSum = item.ItemResultChar;
                                                item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDtoNull.IsNormal);
                                                if (ItemStandardDtoNull.IsNormal != 4)
                                                {
                                                    IsItemAbnormal = true;
                                                }
                                                ItemDto.ItemSum = item.ItemSum;
                                            
                                            }
                                            else
                                            {
                                                var ckz = _itemStandardSys.Where(o => o.ItemId == item.ItemId).FirstOrDefault();
                                                if (ckz != null)
                                                {   //重度等级
                                                    item.IllnessLevel = ckz.IllnessLevel;
                                                    item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                                    item.ItemSum = item.ItemResultChar;
                                                    IsItemAbnormal = true;
                                                }
                                                else
                                                {
                                                    item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                                                    item.ItemSum = "";
                                                }
                                                ItemDto.ItemSum = item.ItemSum;
                                               

                                            }

                                        }
                                    }
                                    //阴阳形判断
                                    else if (item.ItemTypeBM == (int)ItemType.YinYang)
                                    {
                                        var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified) && o.Summ == item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                                        if (ItemStandardDto != null)
                                        {
                                            //重度等级
                                            item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                            item.Symbol = ItemStandardDto.Summ;
                                            item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                            if (ItemStandardDto.IsNormal != 4)
                                            {
                                                IsItemAbnormal = true;
                                            }
                                            ItemDto.ItemSum = item.ItemSum;
                                           
                                        }
                                        else
                                        {
                                            item.ItemSum = item.ItemResultChar;
                                            item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                            IsItemAbnormal = true;
                                            ItemDto.ItemSum = item.ItemSum;
                                           
                                        }
                                    }
                                }
                            }
                            item.ItemDiagnosis = string.Empty;
                            if (IsItemAbnormal)
                            {
                                ////判断字典中是否搜索到                           
                                item.ItemDiagnosis = item.ItemSum.Trim();
                                ItemDto.ItemDiagnosis = item.ItemDiagnosis;
                             
                            }
                            #endregion

                            GroupDto.CustomerRegItem.Add(ItemDto);
                            CustomerItemGroup.Add(GroupDto);
                        }

                    }
                    if (CustomerItemGroup != null && CustomerItemGroup.Count() > 0)
                    {
                        _doctorStation.UpdateInspectionProject(CustomerItemGroup);
                        _doctorStation.UpdateSectionSummary(_currentCustomerItemGroupSys);

                        var cusdepart = _currentCustomerItemGroupSys.Select(o => o.DepartmentId).Distinct().ToList();
                        CreateConclusionDto conclusion = new CreateConclusionDto();
                        conclusion.CustomerBM = dr["体检号"].ToString();
                        conclusion.Department = cusdepart;
                        _doctorStation.CreateConclusion(conclusion);
                    }
                    //执行步长
                    progressBarControl1.PerformStep();
                    num = num + 1;
                    //处理当前消息队列中的所有windows消息,不然进度条会不同步
                    System.Windows.Forms.Application.DoEvents();
                }
            });
            if (Err != "")
            {
                MessageBox.Show(Err);
            }
            return true;
        }
        /// <summary>
        /// 生成项目组合小结
        /// </summary>
        public void CreateConclusion(List<ATjlCustomerItemGroupDto> _currentCustomerItemGroupSys,int Age, string cussex)
        {
            //是否显示正常值
            string iszcz = "";
            var isshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 4)?.Remarks;
            if (!string.IsNullOrEmpty(isshow))
            {
                iszcz = isshow;
            }


            //获取基本信息 限制条件
            string sex = "2";
            if (cussex.Contains("男"))
            {
                sex = "1";
            }
            
            string SexGenderNotSpecified = ((int)Sex.GenderNotSpecified).ToString();
            var KeShiZhenDuan = string.Empty;
            //循环科室组合
            foreach (var ItemGroup in _currentCustomerItemGroupSys)
            {
                //判断项目组合是不是已查或部分已查
                if (ItemGroup.CheckState == (int)ProjectIState.Complete || ItemGroup.CheckState == (int)ProjectIState.Part)
                {
                    //循环当前分组项目
                    foreach (var Item in ItemGroup.CustomerRegItem.OrderBy(o => o.ItemOrder))
                    {
                        //判断项目是否异常
                        var IsItemAbnormal = false;
                        //判断院内院外
                          //if (Item.StandFrom == (int)ExamPlace.Hospital || Item.StandFrom == null)
                        if (true)
                        {
                            //判断是不是已经完成检查
                            if (Item.ProcessState == (int)ProjectIState.Complete)
                            {
                                //数值型判断
                                if ((Item.ItemTypeBM == (int)ItemType.Number || Item.ItemTypeBM == (int)ItemType.Calculation) && Item.ItemResultChar != null && Item.ItemResultChar != "")
                                {
                                    //&& o.CheckType == (int)ResultJudgementState.BigOrSmall
                                    var rtItemResultChar = Convert.ToDecimal(Item.ItemResultChar);//int.Parse(Item.ItemResultChar);
                                    var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar).FirstOrDefault();
                                    if (ItemStandardDto != null)
                                    {
                                        //重度等级
                                        Item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                        //判断是否异常
                                        if (ItemStandardDto.PositiveSate == (int)PositiveSate.Abnormal)
                                        {
                                            if (ItemStandardDto.IsNormal == (int)Symbol.High)
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↑{2})", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↑{2})（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                IsItemAbnormal = true;
                                            }
                                            else if (ItemStandardDto.IsNormal == (int)Symbol.Superhigh)
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↑↑{2})", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↑↑{2})（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                IsItemAbnormal = true;
                                            }
                                            else if (ItemStandardDto.IsNormal == (int)Symbol.Low)
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↓{2})", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↓{2})（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                IsItemAbnormal = true;
                                            }
                                            else if (ItemStandardDto.IsNormal == (int)Symbol.UltraLow)
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↓↓{2})", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1}↓↓{2})（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                Item.PositiveSate = (int)PositiveSate.Abnormal;
                                                IsItemAbnormal = true;
                                            }
                                            //else if (ItemStandardDto.IsNormal == 4)
                                            //{
                                            //    Item.ItemSum = string.Format(@"{0}({1}↓↓{2})（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                            //    IsItemAbnormal = true;
                                            //}
                                            else
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}", ItemStandardDto.Summ);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}（正常值：{1}）", ItemStandardDto.Summ, Item.Stand);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (iszcz == "N")
                                            {
                                                Item.ItemSum = ItemStandardDto.Summ;
                                            }
                                            else
                                            {
                                                Item.ItemSum = ItemStandardDto.Summ + "（正常值：" + Item.Stand + "）";
                                            }
                                        }

                                        Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                        if (Item.Symbol == SymbolHelper.SymbolFormatter((int)Symbol.Superhigh) || Item.Symbol == SymbolHelper.SymbolFormatter((int)Symbol.UltraLow))
                                        {
                                            Item.CrisisSate = (int)CrisisSate.Abnormal;
                                        }
                                        else
                                        {
                                            Item.CrisisSate = (int)CrisisSate.Normal;
                                        }
                                    }
                                    else
                                    {
                                       
                                        Item.ItemSum = "";
                                        Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);

                                    }
                                }
                                //说明形判断
                                else if (Item.ItemTypeBM == (int)ItemType.Explain)
                                {
                                    var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.Summ == Item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                                    if (ItemStandardDto != null)
                                    {
                                        //重度等级
                                        Item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                        Item.ItemSum = ItemStandardDto.Summ;
                                        Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                        if (ItemStandardDto.IsNormal != 4)
                                        {
                                            IsItemAbnormal = true;
                                        }
                                    }
                                    else
                                    {
                                        var ItemStandardDtoNull = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && Item.ItemResultChar.Contains(o.Summ) && o.CheckType == (int)ResultJudgementState.ContainText).FirstOrDefault();
                                        if (ItemStandardDtoNull != null)
                                        {
                                            //重度等级
                                            Item.IllnessLevel = ItemStandardDtoNull.IllnessLevel;
                                            Item.ItemSum = Item.ItemResultChar;
                                            Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDtoNull.IsNormal);
                                            if (ItemStandardDtoNull.IsNormal != 4)
                                            {
                                                IsItemAbnormal = true;
                                            }
                                        }
                                        else
                                        {
                                            var ckz = _itemStandardSys.Where(o => o.ItemId == Item.ItemId).FirstOrDefault();
                                            if (ckz != null)
                                            {   //重度等级
                                                Item.IllnessLevel = ckz.IllnessLevel;
                                                Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                                Item.ItemSum = Item.ItemResultChar;
                                                IsItemAbnormal = true;
                                            }
                                            else
                                            {
                                                Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                                                Item.ItemSum = "";
                                            }

                                        }

                                    }
                                }
                                //阴阳形判断
                                else if (Item.ItemTypeBM == (int)ItemType.YinYang)
                                {
                                    var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.Summ == Item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                                    if (ItemStandardDto != null)
                                    {
                                        //重度等级
                                        Item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                        Item.Symbol = ItemStandardDto.Summ;
                                        Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                        if (ItemStandardDto.IsNormal != 4)
                                        {
                                            IsItemAbnormal = true;
                                        }
                                    }
                                    else
                                    {
                                        Item.ItemSum = Item.ItemResultChar;
                                        Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                        IsItemAbnormal = true;
                                    }
                                }
                            }
                        }
                        Item.ItemDiagnosis = string.Empty;
                        if (IsItemAbnormal)
                        {
                            ////判断字典中是否搜索到                           
                            Item.ItemDiagnosis = Item.ItemSum.Trim();
                           
                        }
                    }
                }
            }
            var currgrouplist = _currentCustomerItemGroupSys.ToList();
            _doctorStation.UpdateSectionSummary(currgrouplist);//保存项目组合和项目小结

        }

        private void FrmImportExcel_Load(object sender, EventArgs e)
        {
            _pictureController = new PictureController();
            _itemStandardSys = new List<SearchItemStandardDto>();
            _itemStandardSys = _doctorStation.GetGenerateSummary();
        }
        private bool getFZState()
        {
            bool isFZState = false;
            if (ClientRegId != Guid.Empty)
            {
                var FZSt = _chargeAppService.GetZFState(new EntityDto<Guid> { Id = ClientRegId });
                if (FZSt == 1)
                {
                    return true;
                }
            }
            return isFZState;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmOutModle frmOutModle = new frmOutModle();
            frmOutModle.ShowDialog();

        }
        /// <summary>
        /// 选择文件导入
        /// </summary>
        public void ImportNew()
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
                if (!dt.Columns.Contains("体检号"))
                { Err = "模板中缺少'体检号'列'\r\n"; }
                if (!dt.Columns.Contains("年龄"))
                { Err += "模板中缺少'年龄'列'\r\n"; }
                if (!dt.Columns.Contains("性别"))
                { Err += "模板中缺少'性别'列'\r\n"; }
                
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

        public bool ImportDataSouceNew()
        {

            //是否显示正常值
            string iszcz = "";
            var isshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 4)?.Remarks;
            if (!string.IsNullOrEmpty(isshow))
            {
                iszcz = isshow;
            }
            //是否显示结果
            string isXMJG = "";
            var isShowxmjg = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 9)?.Remarks;
            if (!string.IsNullOrEmpty(isShowxmjg))
            {
                isXMJG = isShowxmjg;
            }
            // 不判断 data 是否为空就直接筛选数据？
            // 你底下再判断 data 为空有什么用。
            // 如果 data 为空，上面就导致程序崩溃了。
            var data = gridControlImportExcel.DataSource as DataTable;
            if (data == null || data.Rows.Count == 0)
            {
                ShowMessageBoxInformation("没有名单可导入！");
                return false;
            }           
            string Err = "";
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.Properties.Maximum = data.Rows.Count;
                progressBarControl1.Properties.Step = 1;
                progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                progressBarControl1.Position = 0;
                progressBarControl1.Properties.ShowTitle = true;
                progressBarControl1.Properties.PercentView = true;
                progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                label1.Text = "共：" + data.Rows.Count + "条数据";

                System.Windows.Forms.Application.DoEvents();
                int num = 1;

                foreach (DataRow dr in data.Rows)
                {
                    label2.Text = "第：" + num + "条";
                    label2.Refresh();
                    var CustomerItemGroup = new List<UpdateCustomerItemGroupDto>();
                    var Query = new QueryClass();
                    Query.CustomerBM = dr["体检号"].ToString();
                    if (string.IsNullOrEmpty(Query.CustomerBM))
                    {
                        //执行步长
                        progressBarControl1.PerformStep();
                        num = num + 1;
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                        Err += "第" + num + "行体检号为空不能导入 \r\n";
                        continue;
                    }
                    int Age = int.Parse(dr["年龄"].ToString());
                    int cusSex = 1;
                    if (dr["性别"].ToString().Contains("女"))
                    {
                        cusSex = 2;
                    }
                    //只显示本科室或异常科室
                    var _currentCustomerItemGroupSys = new List<ATjlCustomerItemGroupDto>();
                    _currentCustomerItemGroupSys = _doctorStation.GetCustomerItemGroupByBm(Query).ToList();
                    if (_currentCustomerItemGroupSys.Count==0)
                    {
                        //执行步长
                        progressBarControl1.PerformStep();
                        num = num + 1;
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                        continue;
                    }

                    var itemGroup = _currentCustomerItemGroupSys.FirstOrDefault(p => 
                    p.CustomerRegItem.Any(n => n.ItemBM?.ItemBM ==
                      dr["项目代号"].ToString() || n.ItemName == dr["项目名称"].ToString()));
                    if (itemGroup == null)
                    {
                        //执行步长
                        progressBarControl1.PerformStep();
                        num = num + 1;
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                        continue;
                    }


                    var GroupDto = new UpdateCustomerItemGroupDto();
                   
                    GroupDto.Id = itemGroup.Id; //id
                    GroupDto.CheckState = (int)ProjectIState.Complete; //状态

                    GroupDto.BillingEmployeeBMId = CurrentUser.Id; //开单医生
                    GroupDto.InspectEmployeeBMId = CurrentUser.Id;
                    GroupDto.CheckEmployeeBMId = CurrentUser.Id;
                    GroupDto.FirstDateTime = DateTime.Now;
                    GroupDto.CustomerRegItem = new List<UpdateCustomerRegItemDto>();
                    //项目
                    var item = itemGroup.CustomerRegItem.FirstOrDefault(n => n.ItemBM?.ItemBM ==
                       dr["项目代号"].ToString() || n.ItemName == dr["项目名称"].ToString());
                   


                        var ItemDto = new UpdateCustomerRegItemDto();
                        ItemDto.Id = item.Id;
                        ItemDto.ProcessState = (int)ProjectIState.Complete; //状态
                      
                        var SexGenderNotSpecified = ((int)Sex.GenderNotSpecified).ToString();
                        ItemDto.InspectEmployeeBMId = CurrentUser.Id;
                        ItemDto.StandFrom = (int)ExamPlace.GoOut;
                        ItemDto.CheckEmployeeBMId = CurrentUser.Id;
                        ItemDto.ItemResultChar = dr["项目测定值"].ToString(); //结果

                        ItemDto.Stand = dr["参考值"].ToString();                       
                      
                        ItemDto.Unit = dr["单位"].ToString();
                        string xmbs = "";
                        //g 高， d  低   Z 正常。
                        if (dr["高低标记"].ToString().Trim().Contains("g"))
                        {
                            xmbs = "H";
                        }
                        else if (dr["高低标记"].ToString().Trim().Contains("d"))
                        {
                            xmbs = "L";
                        }
                    ItemDto.Symbol = xmbs;
                    ItemDto.ProcessState = (int)ProjectIState.Complete; //状态

                    
                    //item.InspectEmployeeBMId = ItemDto.InspectEmployeeBMId;
                    
                    
                    item.ItemResultChar = ItemDto.ItemResultChar; //结果
                    item.Stand = ItemDto.Stand;
                    item.Unit = ItemDto.Stand;

                    #region 项目小结
                    //判断项目是否异常
                    var IsItemAbnormal = false;

                    //数值型判断
                    if ((item.ItemTypeBM == (int)ItemType.Number || item.ItemTypeBM == (int)ItemType.Calculation) && ItemDto.ItemResultChar != null && ItemDto.ItemResultChar != "")
                    {
                        //&& o.CheckType == (int)ResultJudgementState.BigOrSmall

                        //重度等级
                        item.IllnessLevel = 1;

                        //判断是否异常

                        if (ItemDto.Symbol == "H")
                        {


                            if (iszcz == "N")
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↑", "偏高", ItemDto.ItemResultChar, ItemDto.Unit);
                            }
                            else
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↑（正常值：{3}）", "偏高", ItemDto.ItemResultChar, ItemDto.Unit, ItemDto.Stand);
                            }

                            IsItemAbnormal = true;
                            ItemDto.PositiveSate = (int)PositiveSate.Abnormal;
                        }
                        else if (ItemDto.Symbol == "HH")
                        {
                            //if (iszcz == "N")
                            //{
                            //    item.ItemSum = string.Format(@"{0}({1}↑↑ {2})", "极高", ItemDto.ItemResultChar, ItemDto.Unit);
                            //}
                            //else
                            //{
                            //    item.ItemSum = string.Format(@"{0}({1}↑↑ {2})（正常值：{3}）", "极高", ItemDto.ItemResultChar, ItemDto.Unit, ItemDto.Stand);
                            //}
                            IsItemAbnormal = true;
                            if (iszcz == "N")
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↑↑", "极高", ItemDto.ItemResultChar, ItemDto.Unit);
                            }
                            else
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↑↑（正常值：{3}）", "极高", ItemDto.ItemResultChar, ItemDto.Unit, ItemDto.Stand);
                            }
                            ItemDto.ItemSum = item.ItemSum;
                        }
                        else if (ItemDto.Symbol == "L")
                        {
                            //if (iszcz == "N")
                            //{
                            //    item.ItemSum = string.Format(@"{0}({1}↓ {2})", "偏低", ItemDto.ItemResultChar, ItemDto.Unit);
                            //}
                            //else
                            //{
                            //    item.ItemSum = string.Format(@"{0}({1}↓ {2})（正常值：{3}）", "偏低", ItemDto.ItemResultChar, ItemDto.Unit, ItemDto.Stand);
                            //}

                            if (iszcz == "N")
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↓", "偏低", ItemDto.ItemResultChar, ItemDto.Unit);
                            }
                            else
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↓（正常值：{3}）", "偏低", ItemDto.ItemResultChar, ItemDto.Unit, ItemDto.Stand);
                            }
                            IsItemAbnormal = true;
                            ItemDto.ItemSum = item.ItemSum;
                        }
                        else if (ItemDto.Symbol == "LL")
                        {
                            //if (iszcz == "N")
                            //{
                            //    item.ItemSum = string.Format(@"{0}({1}↓↓ {2})", "极低", ItemDto.ItemResultChar, ItemDto.Unit);
                            //}
                            //else
                            //{
                            //    item.ItemSum = string.Format(@"{0}({1}↓↓ {2})（正常值：{3}）", "极低", ItemDto.ItemResultChar, ItemDto.Unit, ItemDto.Stand);
                            //}

                            if (iszcz == "N")
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↓↓", "极低", ItemDto.ItemResultChar, ItemDto.Unit);
                            }
                            else
                            {
                                item.ItemSum = string.Format(@"{0}({1} {2})↓↓（正常值：{3}）", "极低", ItemDto.ItemResultChar, ItemDto.Unit, ItemDto.Stand);
                            }

                            item.PositiveSate = (int)PositiveSate.Abnormal;
                            IsItemAbnormal = true;
                            ItemDto.ItemSum = item.ItemSum;
                            ItemDto.PositiveSate = item.PositiveSate;
                        }


                        else
                        {
                            if (iszcz == "N")
                            {
                                item.ItemSum = "";
                            }
                            else
                            {
                                item.ItemSum = "";
                            }
                            ItemDto.ItemSum = item.ItemSum;
                        }


                        item.CrisisSate = (int)CrisisSate.Normal;
                        ItemDto.CrisisSate = item.CrisisSate;

                    }
                    //说明形判断
                    else if (item.ItemTypeBM == (int)ItemType.Explain)
                    {
                        var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified) && o.Summ == item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                        if (ItemStandardDto != null)
                        {
                            //重度等级
                            item.IllnessLevel = ItemStandardDto.IllnessLevel;
                            item.ItemSum = ItemStandardDto.Summ;
                            item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                            if (ItemStandardDto.IsNormal != 4)
                            {
                                IsItemAbnormal = true;
                            }

                            ItemDto.ItemSum = item.ItemSum;
                        }
                        else
                        {
                            var ItemStandardDtoNull = _itemStandardSys.Where(o => o.ItemId == item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified) && item.ItemResultChar.Contains(o.Summ) && o.CheckType == (int)ResultJudgementState.ContainText).FirstOrDefault();
                            if (ItemStandardDtoNull != null)
                            {
                                //重度等级
                                item.IllnessLevel = ItemStandardDtoNull.IllnessLevel;
                                item.ItemSum = item.ItemResultChar;
                                item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDtoNull.IsNormal);
                                if (ItemStandardDtoNull.IsNormal != 4)
                                {
                                    IsItemAbnormal = true;
                                }
                                ItemDto.ItemSum = item.ItemSum;

                            }
                            else
                            {
                                var ckz = _itemStandardSys.Where(o => o.ItemId == item.ItemId).FirstOrDefault();
                                if (ckz != null)
                                {   //重度等级
                                    item.IllnessLevel = ckz.IllnessLevel;
                                    item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                    item.ItemSum = item.ItemResultChar;
                                    IsItemAbnormal = true;
                                }
                                else
                                {
                                    item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                                    item.ItemSum = "";
                                }
                                ItemDto.ItemSum = item.ItemSum;


                            }

                        }
                    }
                    //阴阳形判断
                    else if (item.ItemTypeBM == (int)ItemType.YinYang)
                    {
                        var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == cusSex.ToString() || o.Sex == SexGenderNotSpecified) && o.Summ == item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                        if (ItemStandardDto != null)
                        {
                            //重度等级
                            item.IllnessLevel = ItemStandardDto.IllnessLevel;
                            item.Symbol = ItemStandardDto.Summ;
                            item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                            if (ItemStandardDto.IsNormal != 4)
                            {
                                IsItemAbnormal = true;
                            }
                            ItemDto.ItemSum = item.ItemSum;

                        }
                        else
                        {
                            item.ItemSum = item.ItemResultChar;
                            item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                            IsItemAbnormal = true;
                            ItemDto.ItemSum = item.ItemSum;

                        }
                    }
                         

                        item.ItemDiagnosis = string.Empty;
                        if (IsItemAbnormal)
                        {
                            ////判断字典中是否搜索到                           
                            item.ItemDiagnosis = item.ItemSum.Trim();
                            ItemDto.ItemDiagnosis = item.ItemDiagnosis;

                        }
                    #endregion
                     item.Symbol = ItemDto.Symbol;

                    GroupDto.CustomerRegItem.Add(ItemDto);
                        CustomerItemGroup.Add(GroupDto);
                    

                    
                    if (CustomerItemGroup != null && CustomerItemGroup.Count() > 0)
                    {
                        _doctorStation.UpdateInspectionProject(CustomerItemGroup);
                        _doctorStation.UpdateSectionSummary(_currentCustomerItemGroupSys);
                        #region 上传图片
                        if (!string.IsNullOrEmpty(dr["图片路径"].ToString()))
                        {
                            List<CustomerItemPicDto> dt = new List<CustomerItemPicDto>();
                            var CustomerItemPicDto = new CustomerItemPicDto();
                            CustomerItemPicDto.CustomerItemGroupID = CustomerItemGroup.FirstOrDefault()?.Id;
                            CustomerItemPicDto.ItemBMID = ItemDto.Id;
                            CustomerItemPicDto.TjlCustomerRegID = itemGroup?.CustomerRegBMId;
                            var picOut = _pictureController.Uploading(dr["图片路径"].ToString(), "");
                             CustomerItemPicDto.PictureBM = picOut.Id;
                            dt.Add(CustomerItemPicDto);
                            _doctorStation.SaveItemPic(dt);

                        } 
                        #endregion
                        var cusdepart = _currentCustomerItemGroupSys.Select(o => o.DepartmentId).Distinct().ToList();
                        CreateConclusionDto conclusion = new CreateConclusionDto();
                        conclusion.CustomerBM = dr["体检号"].ToString();
                        conclusion.Department = cusdepart;
                        _doctorStation.CreateConclusion(conclusion);
                    }
                    //执行步长
                    progressBarControl1.PerformStep();
                    num = num + 1;
                    //处理当前消息队列中的所有windows消息,不然进度条会不同步
                    System.Windows.Forms.Application.DoEvents();
                }
            });
            if (Err != "")
            {
                MessageBox.Show(Err);
            }
            return true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (ImportDataSouce())
            {
                DialogResult = DialogResult.OK;
                ShowMessageSucceed("导入成功！");
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var strList = new List<string>() {
                 "体检号",
                "年龄",
                "性别",
                "项目代号",
                "项目名称",
                "项目测定值",
                "参考值",
                "单位",
                "高低标记",
                "图片路径"
            };
            JArray mb_jarray = new JArray();
            List<int> cellIndexs = new List<int>();
            GridControlHelper.DownloadTemplate(strList, "结果导入", mb_jarray, cellIndexs, "yyyy-MM-dd");
        }
    }
    #endregion

   
}