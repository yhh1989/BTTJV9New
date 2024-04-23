using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic.OccDayStaticList;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class frmCusHis : UserBaseForm
    {
        public readonly IHistoryComparisonAppService _HistoryComparisonAppService;
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }
        private List<GridColumn> OldColumns = new List<GridColumn>();
        public Guid? departId;
        public Guid? cusId;
        public string IDcard;
        public frmCusHis()
        {
            InitializeComponent();
            _HistoryComparisonAppService = new HistoryComparisonAppService();
            CustomColumns = new Dictionary<string, CustomColumnValue>();


        }
        public frmCusHis(Guid _cusId, string _IDcard, Guid _departId) : this()
        {
            cusId = _cusId;
            IDcard = _IDcard;
            departId = _departId;
           
        }
        private void frmCusHis_Load(object sender, EventArgs e)
        {
            #region 绑定历史数据检索条件
            searchLookUpDepartMent.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            searchLookUpGroup.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
            searchLookUpItem.Properties.DataSource = DefinedCacheHelper.GetItemInfos();
            searchLookUpDepartMent.EditValue = departId;
            if (cusId.HasValue)
            {
                butsearchHis.PerformClick();
            }
            #endregion
        }

        private void butsearchHis_Click(object sender, EventArgs e)
        {
            SearchClass searchClass = new SearchClass();
            SearchHisClassDto searchHisClassDto = new SearchHisClassDto();
            searchHisClassDto.IDCardNo = IDcard;
            searchClass.CustomerId = cusId.Value;
            if (!string.IsNullOrEmpty(searchLookUpDepartMent.EditValue?.ToString()))
            {
                searchClass.DepartId = (Guid)searchLookUpDepartMent.EditValue;
                searchHisClassDto.DepartName = searchLookUpDepartMent.Text;
            }
            if (!string.IsNullOrEmpty(searchLookUpGroup.EditValue?.ToString()))
            {
                searchClass.GroupId = (Guid)searchLookUpGroup.EditValue;
                searchHisClassDto.GroupName = searchLookUpGroup.Text;
            }
            if (!string.IsNullOrEmpty(searchLookUpItem.EditValue?.ToString()))
            {
                searchClass.ItemId = (Guid)searchLookUpItem.EditValue;
                searchHisClassDto.ItemName = searchLookUpItem.Text;
            }
            var HisResult = _HistoryComparisonAppService.GetHistoryResultList(searchClass);

            //开启获取第三方库
             
            var oderHisITem = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(p => p.Type ==
                 BasicDictionaryType.PresentationSet.ToString() && p.Value == 102);
            if (oderHisITem != null && oderHisITem.Remarks == "Y")
            {
                var HisResult1 = _HistoryComparisonAppService.geHisvard(searchHisClassDto);

                HisResult.AddRange(HisResult1);
            }
            Reload(HisResult);
            gridControlHisResult.DataSource = HisResult;
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Reload(List<HistoryResultMainDto> input)
        {
            AutoLoading(() =>
            {

                //删除列
                if (CustomColumns != null && CustomColumns.Count > 0)
                {
                    foreach (var con in OldColumns)
                    {
                        if (gridView7.Columns.Contains(con))
                        {
                            gridView7.Columns.Remove(con);
                        }
                    }
                    OldColumns.Clear();
                    CustomColumns.Clear();
                }

                var itemname = input.SelectMany(p => p.historyResultDetailDtos).Select(p => new { p.CheckDate, p.CustomerBM }).Distinct().OrderBy(
                    p => p.CheckDate).ToList();

                foreach (var depar in itemname)
                {
                    var column = new GridColumn();
                    column.FieldName = $"gridColumnCustom{depar.CustomerBM}";
                    column.Name = $"{depar.CustomerBM}";
                    column.Caption = "(" + depar.CheckDate.Value.ToShortDateString() + ")";
                    if (!CustomColumns.Any(o => o.Key == column.Name))
                    {
                        var customColumn = new CustomColumnValue { Text = column.Name };
                        CustomColumns.Add(column.Name, customColumn);
                    }
                    column.Visible = true;
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    column.ColumnEdit = memoEdit;
                    //column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                    column.SummaryItem.FieldName = "Name";
                    column.SummaryItem.DisplayFormat = "{0:c}";
                    column.SummaryItem.Tag = column.Name;
                    column.Width = 100;

                    gridView7.Columns.Add(column);
                    OldColumns.Add(column);
                }
                gridColumn41.VisibleIndex = gridView7.Columns.Count + 1;



            });

        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (e.ListSourceRowIndex < 0)
                {
                    return;
                }
                if (gridControlHisResult.DataSource is List<HistoryResultMainDto> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {

                        var itemValue = rows[e.ListSourceRowIndex].historyResultDetailDtos
                            ?.Where(r => r.CustomerBM == CustomColumns[e.Column.Name].Text)
                            .Select(r => new { r.ItemValue, r.Symbol }).ToList();
                        foreach (var i in itemValue)
                        {
                            if (i != null && i.ItemValue != null)
                            {
                                string bs = "";
                                if (i.Symbol == "H")
                                {
                                    //string name=

                                    bs = "↑";
                                }
                                else if (i.Symbol == "L")
                                {

                                    bs = "↓";
                                }
                                else if (i.Symbol == "HH")
                                {

                                    bs = "↑↑";
                                }
                                else if (i.Symbol == "LL")
                                {

                                    bs = "↓↓";
                                }
                                else
                                {

                                    bs = "";


                                }
                                e.DisplayText = i.ItemValue.ToString() + bs;
                            }

                        }



                    }
                }
            }
        }

        private void gridView3_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (e.DisplayText.Contains("↑↑"))
                {

                    e.Appearance.BackColor = Color.Red;
                }
                else if (e.DisplayText.Contains("↓↓"))
                { e.Appearance.BackColor = Color.Blue; }
                else if (e.DisplayText.Contains("↑"))
                { e.Appearance.BackColor = Color.Salmon; }
                else if (e.DisplayText.Contains("↓"))
                { e.Appearance.BackColor = Color.SkyBlue; }
                else
                {
                    e.Appearance.BackColor = Color.Transparent;
                }
            }
        }
    }
}
