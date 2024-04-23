using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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
using HealthExaminationSystem.Enumerations.Helpers;
using static Sw.Hospital.HealthExaminationSystem.Statistics.Charge.FinancialStatement;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class DailyList : UserBaseForm
    {
        private readonly IChargeAppService _chargeAppService;
        private readonly ICommonAppService _commonAppService;
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }
        
        List<ChargeTypeDto> ChargeTypelst = new List<ChargeTypeDto>();
        public DailyList()
        {
            _chargeAppService = new ChargeAppService();
            _commonAppService = new CommonAppService();
            CustomColumns = new Dictionary<string, CustomColumnValue>();
            InitializeComponent();
            gridViewCus.Columns[conSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCus.Columns[conSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void DailyList_Load(object sender, EventArgs e)
        {
           
            //单位
            var companies = DefinedCacheHelper.GetClientRegNameComDto();
            TeDW.Properties.DataSource = companies;

            //绑定医生        
            SfEdit.Properties.DataSource = DefinedCacheHelper.GetComboUsers();

            // 初始化时间框        
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEnd.DateTime = date;
            dateStar.DateTime = date;
        
            var  type = 2;
            ChargeBM chargeBM = new ChargeBM();
              chargeBM.Name = type.ToString();
            ChargeTypelst = _chargeAppService.ChargeType(chargeBM);
          //  var list = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ChargeCategory);
            foreach (var ChargeType in ChargeTypelst)
            {
                var column = new GridColumn();
                column.FieldName = $"gridColumnCustom{ChargeType.ChargeName}";
                column.Name = $"gridColumnCustom{ChargeType.ChargeName}";
                column.Caption = ChargeType.ChargeName;
                var customColumn = new CustomColumnValue { Text = column.Caption };
                CustomColumns.Add(column.Name, customColumn);
                column.Visible = true;
                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                column.SummaryItem.FieldName = "allMoney";
                column.SummaryItem.DisplayFormat = "{0:c}";
                column.SummaryItem.Tag = column.Name;
                gridViewCus.Columns.Add(column);
            }
            var column1 = new GridColumn();
            column1.FieldName = "单位挂账";
            column1.Name = $"gridColumnCustom1单位挂账";
            column1.Caption = "单位挂账";
            // var customColumn = new CustomColumnValue { Text = column1.Caption };
            //CustomColumns.Add(column.Name, customColumn);
            column1.Visible = true;
            column1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            column1.SummaryItem.FieldName = "单位挂账";
            column1.SummaryItem.DisplayFormat = "{0:c}";
            column1.SummaryItem.Tag = column1.Name;
            gridView2.Columns.Add(column1);
            foreach (var ChargeType in ChargeTypelst)
            {
                var column = new GridColumn();
                column.FieldName = ChargeType.ChargeName;
                column.Name = $"gridColumnCustom1{ChargeType.ChargeName}";
                column.Caption = ChargeType.ChargeName;
                var customColumn = new CustomColumnValue { Text = column.Caption };
                //CustomColumns.Add(column.Name, customColumn);
                column.Visible = true;
                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                column.SummaryItem.FieldName = ChargeType.ChargeName;
                column.SummaryItem.DisplayFormat = "{0:c}";
                column.SummaryItem.Tag = column.Name;
                gridView2.Columns.Add(column);
            }
           
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                gridColumn5.GroupIndex = 0;
            }
            else
            { gridColumn5.GroupIndex = -1; }
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

          
            var input = new SearchChagelistDto
            {
                SearchName = name,
                ClientRegID = clientRegId,
                LinkName = userid,
                UserType = isfree
            };
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
            if (SfEdit.EditValue != null)
            {
                input.UserID = long.Parse(SfEdit.EditValue.ToString());
            }
            var cus = _chargeAppService.getDailyList(input);

            gridControlCus.DataSource = cus;

           // var payment= _chargeAppService.getPayDailyList(input);

            DataTable dt = new DataTable();
            dt.Columns.Add("单位");
            dt.Columns.Add("总人数");
            dt.Columns.Add("总应收");
            dt.Columns.Add("总已收");
            foreach (var ChargeType in ChargeTypelst)
            {  
                dt.Columns.Add(ChargeType.ChargeName);
            
            }
            dt.Columns.Add("单位挂账");
            var paygroup = cus.Select(p => p.ClientName).Distinct().ToList();
            foreach (var client in paygroup)
            {
                DataRow dr = dt.NewRow();
                dr["单位"] = client;
                List<CusPayListDto> clientrs = new List<CusPayListDto>();
                if (!string.IsNullOrEmpty(client))
                {
                      clientrs = cus.Where(p => p.ClientName == client).ToList();
                 
                }
                else
                {
                    dr["单位"] = "个人";
                    clientrs = cus.Where(p => p.ClientName == "" || p.ClientName == null).ToList();
                }
                dr["总人数"] = clientrs.Count;
                dr["总应收"] = clientrs.Sum(p => p.allMoney);
                dr["总已收"] = clientrs.Sum(p => p.PayMoney);
                foreach (var ChargeType in ChargeTypelst)
                {
                    var paymoney = clientrs.SelectMany(p => p.cusPayments).ToList();
                    dr[ChargeType.ChargeName] = paymoney.Where(p => p.MChargeTypename ==
                    ChargeType.ChargeName).Sum(p => p.Actualmoney);
                }
                dr["单位挂账"] = clientrs.Sum(p => p.TTMoney);
               dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
        }

        private void gridViewCus_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (gridControlCus.DataSource is List<CusPayListDto> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {
                        var sum = rows[e.ListSourceRowIndex].cusPayments
                            ?.Where(r => r.MChargeTypename == CustomColumns[e.Column.Name].Text)
                            .Sum(r => r.Actualmoney);
                        e.DisplayText = sum?.ToString();
                    }
                }
            }
        }

        private void butOut_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "日报明细";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            gridControlCus.ExportToXls(saveFileDialog.FileName);
        }

        private void gridViewCus_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
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
                                    if (e.Row is CusPayListDto row)
                                    {
                                        var sum = row.cusPayments
                                            ?.Where(r => r.MChargeTypename == CustomColumns[tag].Text)
                                            .Sum(r => r.Actualmoney);
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
    }
}
