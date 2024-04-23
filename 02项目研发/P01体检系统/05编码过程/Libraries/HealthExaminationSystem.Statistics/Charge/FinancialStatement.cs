using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using CacheHelper = Sw.Hospital.HealthExaminationSystem.Common.Helpers.CacheHelper;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class FinancialStatement : UserBaseForm
    {
        private readonly IPhysicalExaminationAppService _physicalExaminationAppService;

        /// <summary>
        /// 所有科室用户
        /// </summary>
        private List<UserForComboDto> _currentUsers;

        private readonly IChargeAppService _chargeAppService;
        private readonly ICommonAppService _commonAppService;

        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }

        public FinancialStatement()
        {
            InitializeComponent();
            _chargeAppService = new ChargeAppService();
            _physicalExaminationAppService = new PhysicalExaminationAppService();
            CustomColumns = new Dictionary<string, CustomColumnValue>();
            _commonAppService = new CommonAppService();

            gridViewCus.Columns[Sex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCus.Columns[Sex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void FinancialStatement_Load(object sender, EventArgs e)
        {
            gridViewCus.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "CustomerReg.CustomerBM", CustomerBM, "{0:N3}");
            gridViewCus.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Count", gridcount, "合计：{0:N0}人");
            gridViewCus.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "cusPayMoney", Actualmoney, "{0:c}");
            //单位
            // var companies = _physicalExaminationAppService.QueryCompany();
            var comreg = DefinedCacheHelper.GetClientRegNameComDto();
            TeDW.Properties.DataSource = comreg;

            //绑定医生
            //_currentUsers = DefinedCacheHelper.GetComboUsers();
            //lookUpEmp.Properties.DataSource = _currentUsers;

            // 初始化时间框        
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEnd.DateTime = date;
            dateStar.DateTime = date;
            gridViewCus.OptionsBehavior.AutoExpandAllGroups = true;

        }
        private void getcon(int type)
        {
            CustomColumns.Clear();
            List<GridColumn> deparlist = new List<GridColumn>();
            for (int con=0; con< gridViewCus.Columns.Count;con++)
            {
                var listname = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ChargeCategory).Select(p=>p.Text).ToList();

                if (listname.Contains(gridViewCus.Columns[con].Caption))
                {
                    deparlist.Add(gridViewCus.Columns[con]);
                   // gridViewCus.Columns.Remove(gridViewCus.Columns[con]);
                }
                var listname2 = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.LargeDepatType).Select(p => p.Text).ToList();
                if (listname2.Contains(gridViewCus.Columns[con].Caption))
                {
                    deparlist.Add(gridViewCus.Columns[con]);
                    // gridViewCus.Columns.Remove(gridViewCus.Columns[con]);
                }
            }
            for (int con = 0; con < deparlist.Count; con++)
            {
                gridViewCus.Columns.Remove(deparlist[con]);

            }
                var list = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ChargeCategory).OrderBy(P=>P.OrderNum).ToList();

            if (type == 1)
            {
                list= DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.LargeDepatType).OrderBy(P => P.OrderNum).ToList();
            }
            
            for (var num = 0; num < list.Count; num++)
            {
                var column = new GridColumn();
                column.FieldName = $"gridColumnCustom{list[num].Value}";
                column.Name = $"gridColumnCustom{list[num].Value}";
                column.Caption = list[num].Text;              
                var customColumn = new CustomColumnValue { Text = column.Caption };

                // column.DisplayFormat.FormatType= FormatType.Numeric;
                column.DisplayFormat.FormatType = FormatType.Numeric;
                column.DisplayFormat.FormatString = "#,###.00";
                CustomColumns.Add(column.Name, customColumn);
                column.Visible = true;
                column.VisibleIndex = gridViewCus.Columns.Count + 1;
                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                column.SummaryItem.FieldName = "CustomerReg.CustomerBM";
                column.SummaryItem.DisplayFormat = "{0:c}";
                column.SummaryItem.Tag = column.Name;
                gridViewCus.Columns.Add(column);
                GridGroupSummaryItem gridGroupSummaryItem = new GridGroupSummaryItem();
                gridGroupSummaryItem.SummaryType= DevExpress.Data.SummaryItemType.Custom;
                gridGroupSummaryItem.FieldName = "CustomerReg.CustomerBM";
                gridGroupSummaryItem.ShowInGroupColumnFooter = column;
                gridGroupSummaryItem.DisplayFormat = "{0:c}";
                gridGroupSummaryItem.Tag= column.Name;
                gridViewCus.GroupSummary.Add(gridGroupSummaryItem);
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            labts.Text = "";
            var name = searchName.Text.Trim();
            var clientRegId = TeDW.EditValue as Guid?;
            var maxmoney = decimal.Parse(textMaxMoney.EditValue.ToString());
            var minmoney = decimal.Parse(textMinMoney.EditValue.ToString());
            var userid = lookUpEmp.Text.Trim();          
            int isfree = 0;
            //if (checkIsFree.Checked==true)
            //{
            //    isfree = 1;
            //}

            if (radioGroupIsfree.EditValue.ToString() == "0")
            {
                isfree = 1;
            }
            else if (radioGroupIsfree.EditValue.ToString() == "1")
            {
                isfree = 2;
            }

                var input = new SearchSFTypeDto
                {
                    SearchName = name,
                    ClientRegID = clientRegId,
                    LinkName = userid,
                    UserType = isfree
                };
            if (checkEdit1.Checked == true)
            {
                input.TTMoney = 1;
            }
            if (!string.IsNullOrWhiteSpace(dateStar.Text.Trim()) && !string.IsNullOrWhiteSpace(dateEnd.Text.Trim()))
            {
                input.StarDate = dateStar.DateTime;
                input.EndDate = dateEnd.DateTime;
            }
            if ((maxmoney != 0 || minmoney != 0) && maxmoney >= minmoney)
            {
                input.MaxMoney = maxmoney;
                input.MinMoney = minmoney;
            }
            if (checkDepat.Checked == true)
            {
                input.DepatType = 1;
                getcon(1);
            }
            else
            { getcon(0); }
            if (radioGroup1.EditValue?.ToString() == "0")
            {
              
                CustomerBM.Visible = true;            
                CustomerName.Visible = true;             
                ClientName.Visible = true;                
                Sex.Visible = true;
                Age.Visible = true;
                Mobile.Visible = true;
                gridClient.Visible = false;
                gridcount.Visible = false;

                CustomerBM.VisibleIndex = 0;
                CustomerName.VisibleIndex = 1;
                ClientName.VisibleIndex = 2;
                Sex.VisibleIndex = 3;
                Age.VisibleIndex = 4;
                Mobile.VisibleIndex = 5;


            }
            else
            {

                CustomerBM.Visible = false;
                CustomerName.Visible = false;
                ClientName.Visible = false;
                Sex.Visible = false;
                Age.Visible = false;
                Mobile.Visible = false;
                gridClient.Visible = true;
                gridcount.Visible = true;
                gridClient.VisibleIndex = 0;
                gridcount.VisibleIndex = 1;
            }
            input.SeachType =int.Parse( radioGroup1.EditValue?.ToString());
            var cus = _chargeAppService.getCusStType(input);

            gridControlCus.DataSource = cus;
            //原价
            var  dwyj = cus.Where(p => p.ISclient == "团体").
                SelectMany(p=>p.MReceiptDetailses).Sum(p=>p.GroupsMoney);
            var gryj = cus.Where(p => p.ISclient == "个人").
               SelectMany(p => p.MReceiptDetailses).Sum(p => p.GroupsMoney);
            //折后价格
            var dwzh = cus.Where(p => p.ISclient == "团体").Sum(p => p.cusPayMoney);
               
            var grzh = cus.Where(p => p.ISclient == "个人").Sum(p => p.cusPayMoney);

            labts.Text = "总工作量合计金额约为(单位+个人):<Color=Red>"+ dwyj  +"+" + gryj +"=" +(dwyj+ gryj) + "</Color>;";
            labts.Text += "总工作量折后合计金额约为(单位+个人):<Color=Red>" + dwzh + "+" + grzh + "=" + (dwzh + grzh) + "</Color>;";
        }

        private void gridViewCus_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (gridControlCus.DataSource is List<CustomerSFTypeDto> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {
                        var sum = rows[e.ListSourceRowIndex].MReceiptDetailses
                            ?.Where(r => r.ReceiptTypeName == CustomColumns[e.Column.Name].Text)
                            .Sum(r => r.GroupsDiscountMoney);
                          e.DisplayText = sum?.ToString();
                       


                    }
                }
            }
        }
        private void ResetItemGroupSummary()
        {


            gridViewCus.GroupSummary.BeginUpdate();

        

          

            gridViewCus.GroupSummary.EndUpdate();
        }
        private void gridViewCus_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            ResetItemGroupSummary();
            if (e.IsTotalSummary  )
            {
                 
                if (e.Item is GridColumnSummaryItem gridColumnSummaryItem)
                {
                    if (gridColumnSummaryItem.Tag is string tag)
                    {
                        if (CustomColumns.ContainsKey(tag))
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    CustomColumns[tag].Sum = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.Row is CustomerSFTypeDto row)
                                    {
                                        var sum = row.MReceiptDetailses
                                            ?.Where(r => r.ReceiptTypeName == CustomColumns[tag].Text)
                                            .Sum(r => r.GroupsDiscountMoney);
                                        CustomColumns[tag].Sum = CustomColumns[tag].Sum + sum ?? 0;
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = CustomColumns[tag].Sum;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }


            }
            else if (e.IsGroupSummary)
            {
                if (e.Item is GridGroupSummaryItem gridColumnSummaryItem)
                {
                    if (gridColumnSummaryItem.Tag is string tag)
                    {
                        if (CustomColumns.ContainsKey(tag))
                        {
                            switch (e.SummaryProcess)
                            {
                                case CustomSummaryProcess.Start:
                                    CustomColumns[tag].Sum = 0;
                                    break;
                                case CustomSummaryProcess.Calculate:
                                    if (e.Row is CustomerSFTypeDto row)
                                    {
                                        var sum = row.MReceiptDetailses
                                            ?.Where(r => r.ReceiptTypeName == CustomColumns[tag].Text)
                                            .Sum(r => r.GroupsDiscountMoney);
                                        CustomColumns[tag].Sum = CustomColumns[tag].Sum + sum ?? 0;
                                    }
                                    break;
                                case CustomSummaryProcess.Finalize:
                                    e.TotalValue = CustomColumns[tag].Sum;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }

           
        }

        private void searchName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    var name = searchName.Text.Trim();

                    var input = new SearchSFTypeDto
                    {
                        SearchName = name,
                    };
                    var cus = _chargeAppService.getCusStType(input);

                    gridControlCus.DataSource = cus;
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        public class CustomColumnValue
        {
            public string Text { get; set; }

            public decimal Sum { get; set; }
        }

        private void lookUpEmp_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    var name = lookUpEmp.Text.Trim();

                    var input = new SearchSFTypeDto
                    {
                        LinkName = name,
                    };
                    var cus = _chargeAppService.getCusStType(input);

                    gridControlCus.DataSource = cus;
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void butOut_Click(object sender, EventArgs e)
        {
            //var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //saveFileDialog.FileName = "个人统计";
            //saveFileDialog.Title = "导出Excel";
            //saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            //saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            //var dialogResult = saveFileDialog.ShowDialog();
            //if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
            //    return;
            //gridControlCus.ExportToXls(saveFileDialog.FileName);
            ExcelHelper.GridViewToExcel(gridViewCus, "科室分成", "科室分成");
        }


        private void lookUpEmp_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}