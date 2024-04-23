using gregn6Lib;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.OutInspects
{
    public partial class ListGuide : UserBaseForm
    {
        private IWorkbook _workbook;
        private   IIDNumberAppService _idNumberAppService=new IDNumberAppService();
        private readonly ICommonAppService _commonAppService;
        private IItemSuitAppService itemSuitAppSvr;//套餐
        private ICustomerAppService customerSvr;//体检预约
        
        public ListGuide()
        {
            customerSvr = new CustomerAppService();
            itemSuitAppSvr = new ItemSuitAppService();
            _commonAppService = new CommonAppService();
            InitializeComponent();
        }

        private void ListGuide_Load(object sender, EventArgs e)
        {
            var ghks = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
            concz.Properties.DataSource = ghks;
            if (ghks != null && ghks.Count > 0)
            {
                concz.EditValue = ghks[0].Value;
            }
            comSuit.Properties.DataSource = DefinedCacheHelper.GetItemSuit().Where(o => o.Available == 1).ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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
                if (!dt.Columns.Contains("姓名"))
                { Err = "模板中缺少'姓名'列'\r\n"; }
                if (!dt.Columns.Contains("性别"))
                { Err += "模板中缺少'性别'列'\r\n"; }
                if (!dt.Columns.Contains("年龄"))
                { Err += "模板中缺少'年龄'列'\r\n"; }
                if (!dt.Columns.Contains("移动电话"))
                { Err += "模板中缺少'移动电话'列'\r\n"; }
                if (!dt.Columns.Contains("身份证号"))
                { Err += "模板中缺少'身份证号'列'\r\n"; }                
                if (Err != "")
                {
                    MessageBox.Show(Err);
                }
                else
                {
                    gridControl1.DataSource = dt;
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
            _workbook = new XSSFWorkbook(_fs);
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
                    if (row.GetCell(1) == null)
                    {
                        MessageBox.Show("姓名不能为空！");
                        return new DataTable();
                    }
                    if (row.GetCell(2) == null)
                    {
                        MessageBox.Show("性别不能为空！");
                        return new DataTable();
                    }
                    if (row.GetCell(3) == null)
                    {
                        MessageBox.Show("年龄不能为空！");
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dtDate.EditValue?.ToString()))
            {
                MessageBox.Show("请选择体检日期！");
                return;
            }
            if (string.IsNullOrWhiteSpace(concz.EditValue?.ToString()))
            {
                MessageBox.Show("请选择村庄！");
                return;
            }
            if (string.IsNullOrWhiteSpace(comSuit.EditValue?.ToString()))
            {
                MessageBox.Show("请选择套餐！");
                return;
            }
            string Err = "";
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;
                var dt = gridControl1.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    progressBarControl1.Properties.Maximum = dt.Rows.Count;
                    progressBarControl1.Properties.Step = 1;
                    progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBarControl1.Position = 0;
                    progressBarControl1.Properties.ShowTitle = true;
                    progressBarControl1.Properties.PercentView = true;
                    progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                    System.Windows.Forms.Application.DoEvents();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region 个人登记
                        QueryCustomerRegDto data = new QueryCustomerRegDto();

                        data.BarState = 1;
                        data.BlindSate = 1;
                        data.BookingDate = dtDate.DateTime;
                        data.LoginDate = dtDate.DateTime;
                        data.CheckSate = 1;
                        data.ClientType = "";//体检类别
                        data.CostState = 1;
                        data.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                        data.CustomerType = 1;
                        data.EmailReport = 2;
                        data.ExamPlace = 1;
                        data.FamilyState = 1;
                        data.GuidanceSate = 1;
                        data.HaveBreakfast = 1;
                        data.InfoSource = 2;
                        if (!string.IsNullOrWhiteSpace(concz.EditValue?.ToString()))
                        {
                            data.NucleicAcidType = (int)concz.EditValue;
                        }

                        data.IsFree = false;
                        if (!string.IsNullOrWhiteSpace(comSuit.EditValue?.ToString()))
                        {
                            data.ItemSuitBMId = (Guid)comSuit.EditValue;
                            data.ItemSuitName = comSuit.Text;
                        }
                        // User user = _jlUsrs.Get(AbpSession.UserId.Value);
                        data.KaidanYisheng = CurrentUser.UserName;
                        data.MailingReport = 1;
                        data.MarriageStatus = (int)MarrySate.Unstated;
                        data.Message = 2;
                        data.PhysicalType = 1;
                        data.PrintSate = 1;
                        data.ReceiveSate = 1;
                        data.RegAge = int.Parse(dt.Rows[i]["年龄"].ToString());
                        data.RegisterState = 2;
                        data.ReplaceSate = 1;
                        data.ReportBySelf = 1;
                        data.RequestState = 1;
                        data.ReviewSate = 1;
                        data.SendToConfirm = 1;
                        data.SummLocked = 2;
                        data.SummSate = 1;
                        data.UrgentState = 1;
                        if (!string.IsNullOrWhiteSpace(dt.Rows[i]["登记序号"]?.ToString()) && int.TryParse(dt.Rows[i]["登记序号"].ToString(), out int regNum))
                        {
                            data.CustomerRegNum = regNum;
                        }
                        //data. 生成查询码
                        data.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();
                        //体检人基本信息
                        data.Customer = new QueryCustomerDto();
                        //身份证验证
                        var IDdata = VerificationHelper.GetByIdCard(dt.Rows[i]["身份证号"].ToString());
                        if (IDdata != null)
                        {

                            data.Customer.Age = IDdata.Age;
                            data.Customer.Sex = (int)IDdata.Sex;
                            data.Customer.Birthday = IDdata.Birthday;
                            data.Customer.IDCardNo = dt.Rows[i]["身份证号"].ToString();
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(dt.Rows[i]["身份证号"].ToString()))
                            {
                                Err += $"登记序号：{dt.Rows[i]["登记序号"]?.ToString()}，姓名：{dt.Rows[i]["姓名"].ToString()}，身份证号：" +
                                $"{dt.Rows[i]["身份证号"].ToString()}错误！，身份证号保存失败\r\n";
                            }
                            data.Customer.Age = int.Parse(dt.Rows[i]["年龄"].ToString());
                            int sex = 1;
                            if (dt.Rows[i]["性别"].ToString().Contains("女"))
                            {
                                sex = 2;
                            }

                            data.Customer.Sex = sex;
                            
                        }
                        //无档案号的情况赋值
                        data.Customer.ArchivesNum = data.CustomerBM;
                        data.Customer.AgeUnit = "岁";
                        data.Customer.CardNumber = "";
                        data.Customer.CustomerType = 1;
                        data.Customer.Duty = "";
                        data.Customer.Email = "";
                        data.Customer.GuoJi = "";
                        data.Customer.HospitalNum = "";
                        // data.Customer.IDCardNo = input.IDCardNo;
                        data.Customer.IDCardType = 1;//证件类型
                        data.Customer.MarriageStatus = (int)MarrySate.Unstated;
                        data.Customer.MedicalCard = "";
                        data.Customer.Mobile = dt.Rows[i]["移动电话"].ToString();
                        data.Customer.Name = dt.Rows[i]["姓名"].ToString();
                        //生成简拼
                        var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = dt.Rows[i]["姓名"].ToString() });
                        data.Customer.NameAB = result.Brief;//姓名简写;

                        data.Customer.Telephone = "";
                        data.Customer.WbCode = "";//五笔
                        data.Customer.WorkNumber = "";
                        data.Customer.Qq = "";
                        data.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                        var suitID = (Guid)comSuit.EditValue;
                        var ret = itemSuitAppSvr.QueryFulls(new SearchItemSuitDto() { Id = suitID });
                        //组合信息
                        foreach (var cusGroup in ret.First().ItemSuitItemGroups)
                        {

                            TjlCustomerItemGroupDto tjlCustomerItemGroup = new TjlCustomerItemGroupDto();
                            tjlCustomerItemGroup.BarState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.CheckState = (int)ProjectIState.Not;



                            //TbmItemGroup tbmItemGroup = _bmItemGroup.Get(cusGroup.ItemGroupBM_Id.Value);
                            //if (tbmItemGroup == null)
                            //{
                            //    outResult.ErrInfo = "未找到项目" + cusGroup.ItemGroupName + "的项目ID，请核实";
                            //    return outResult;
                            //}
                            //if (tbmItemGroup.Price != cusGroup.ItemPrice)
                            //{
                            //    outResult.ErrInfo = "项目" + cusGroup.ItemGroupName + "金额：" + cusGroup.ItemPrice + ",和体检系统金额：" + tbmItemGroup.Price + "，金额不符请核实！";
                            //    return outResult;
                            //}

                            tjlCustomerItemGroup.DepartmentId = cusGroup.ItemGroup.DepartmentId;
                            //TbmDepartment tbmDepartment = _bmDepartment.Get(tbmItemGroup.DepartmentId);
                            tjlCustomerItemGroup.DepartmentName = cusGroup.ItemGroup.Department.Name;
                            tjlCustomerItemGroup.DepartmentOrder = cusGroup.ItemGroup.Department.OrderNum;
                            tjlCustomerItemGroup.DiscountRate = 1;
                            tjlCustomerItemGroup.DrawSate = 1;
                            tjlCustomerItemGroup.BillingEmployeeBMId = CurrentUser.Id;
                            tjlCustomerItemGroup.GRmoney = cusGroup.ItemGroup.Price.Value;
                            tjlCustomerItemGroup.GuidanceSate = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目


                            tjlCustomerItemGroup.ItemGroupBM_Id = cusGroup.ItemGroup.Id;
                            tjlCustomerItemGroup.ItemGroupName = cusGroup.ItemGroup.ItemGroupName;
                            tjlCustomerItemGroup.ItemGroupOrder = cusGroup.ItemGroup.OrderNum;
                            tjlCustomerItemGroup.ItemPrice = cusGroup.ItemGroup.Price.Value;
                            tjlCustomerItemGroup.ItemSuitId = suitID;

                            tjlCustomerItemGroup.ItemSuitName = data.ItemSuitName;
                            tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                            tjlCustomerItemGroup.PriceAfterDis = cusGroup.ItemGroup.Price.Value;
                            tjlCustomerItemGroup.RefundState = (int)PayerCatType.NotRefund;
                            tjlCustomerItemGroup.RequestState = (int)PrintSate.NotToPrint;
                            tjlCustomerItemGroup.SFType = Convert.ToInt32(cusGroup.ItemGroup.ChartCode);
                            tjlCustomerItemGroup.SummBackSate = (int)SummSate.NotAlwaysCheck;
                            tjlCustomerItemGroup.SuspendState = 1;
                            tjlCustomerItemGroup.TTmoney = 0;
                            data.CustomerItemGroup.Add(tjlCustomerItemGroup);
                        }
                        var IsChage = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.IsCharge, 10);
                        if (IsChage != null && IsChage.Text == "0")
                        {
                            data = NoChargeState(data);
                        }
                        QueryCustomerRegDto curCustomRegInfo = customerSvr.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
                        //执行步长
                        progressBarControl1.PerformStep();
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                        #endregion
                    }
                   
                }
            });
            if (Err != "")
            {
                MessageBox.Show(Err);
            }
        }
        /// <summary>
        /// 未关联收费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private QueryCustomerRegDto NoChargeState(QueryCustomerRegDto input)
        {


            foreach (TjlCustomerItemGroupDto cusgroup in input.CustomerItemGroup)
            {
                var ocusgroup = input.CustomerItemGroup.FirstOrDefault(p => p.ItemGroupName == cusgroup.ItemGroupName);
                if (ocusgroup.PayerCat == (int)PayerCatType.NoCharge)
                {
                    cusgroup.PayerCat = (int)PayerCatType.Charge;
                }
            }
            return input;


        }
    }
}
