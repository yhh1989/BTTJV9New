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
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Common.CommonFormat;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System.IO;
using DevExpress.XtraPrinting;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using DevExpress.XtraEditors.Controls;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.GeneralDoctor
{
    public partial class FrmTCTJ : UserBaseForm
    {
        public FrmTCTJ()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 套餐字典
        /// </summary>
        ItemSuitAppService _itemProxy = new ItemSuitAppService();
        CommonAppService _commonProxy = new CommonAppService();
        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            //套餐类别单元格
            gvItemSuit.Columns[colTaoCanLeiBie.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemSuit.Columns[colTaoCanLeiBie.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.ItemSuitTypeFormatter);
            //套餐类别下拉框
            lueItemSuitType.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ItemSuitType));
            lueItemSuitType.Properties.DisplayMember = "Value";
            lueItemSuitType.Properties.ValueMember = "Key";

            //套餐
            //chkcmbItemSuit.Properties.DataSource = _itemProxy.
            //    QueryFulls(new SearchItemSuitDto());
            chkcmbItemSuit.Properties.DataSource = Common.UserCache.DefinedCacheHelper.GetItemSuit();
            chkcmbItemSuit.Properties.DisplayMember = "ItemSuitName";
            chkcmbItemSuit.Properties.ValueMember = "Id";
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                QueryClass query = new QueryClass();
                if (lueItemSuitType.EditValue == null)
                {
                    query.ItemSuitType = null;
                }
                else
                {
                    query.ItemSuitType = (int)(lueItemSuitType.EditValue);
                }
                if (dt_Starte.EditValue != null)
                    query.LastModificationTimeBign = Convert.ToDateTime(dt_Starte.EditValue.ToString());
                if (dt_End.EditValue != null)
                    query.LastModificationTimeEnd = Convert.ToDateTime(dt_End.EditValue.ToString());


                List<Guid> chkList = new List<Guid>();
                string[] arrIds = chkcmbItemSuit.Properties.GetCheckedItems().ToString().Split(',');
                if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
                {
                    foreach (string item in arrIds)
                    {
                        chkList.Add(new Guid(item));
                    }
                }
                query.ItemSuitBMId = chkList;

                DoctorStationAppService _app = new DoctorStationAppService();
                List<SearchItemSuitStatisticsDto> tjList = _app.GetItemSuitStatistics(query);
                dgc.DataSource = tjList;
            }
            catch (Exception ex)
            {
                ShowMessageBoxInformation(ex.Message);
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTCTJ_Load(object sender, EventArgs e)
        {
            init();
            dt_Starte.DateTime = _commonProxy.GetDateTimeNow().Now;
            dt_End.DateTime = _commonProxy.GetDateTimeNow().Now;
        }

        /// <summary>
        /// 生产行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvItemSuit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            Export();
        }
        public void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "检查项目统计";
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)   //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
                return;
            XlsExportOptions options = new XlsExportOptions();
            var FileName = saveFileDialog.FileName;
            try
            {
                if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
                {
                    XlsxExportOptions xoptions = new XlsxExportOptions(TextExportMode.Text);
                    dgc.ExportToXlsx(FileName, xoptions);
                }
                else
                {
                    dgc.ExportToXls(FileName, options);
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
        /// 套餐类别选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lueItemSuitType_EditValueChanged(object sender, EventArgs e)
        {
            int? ItemSuitType;
            if (lueItemSuitType.EditValue == null)
            {
                ItemSuitType = null;
                var list = Common.UserCache.DefinedCacheHelper.GetItemSuit();
                chkcmbItemSuit.Properties.DataSource = list;
            }
            else
            {
                ItemSuitType = (int)lueItemSuitType.EditValue;
                var list = Common.UserCache.DefinedCacheHelper.GetItemSuit().Where(o => o.ItemSuitType == ItemSuitType);
                chkcmbItemSuit.Properties.DataSource = list;
            }


        }

        private void chkcmbItemSuit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                chkcmbItemSuit.EditValue = null;
            }
        }

        private void lueItemSuitType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lueItemSuitType.EditValue = null;
                chkcmbItemSuit.Properties.DataSource = Common.UserCache.DefinedCacheHelper.GetItemSuit();
            }
        }
    }
}