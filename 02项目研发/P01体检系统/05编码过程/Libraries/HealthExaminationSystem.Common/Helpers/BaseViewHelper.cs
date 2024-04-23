using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public static  class BaseViewHelper
    {
        public static void CustomExport(this BaseView  dgv,string FN)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = FN;
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)   //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
                return;
            var FileName = saveFileDialog.FileName;
            if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
            {
                XlsxExportOptions xoptions = new XlsxExportOptions(TextExportMode.Text);
                dgv.ExportToXlsx(FileName, xoptions);
            }
            else
            {
                XlsExportOptions options = new XlsExportOptions(TextExportMode.Text);
                dgv.ExportToXls(FileName, options);
            }
            if (DevExpress.XtraEditors.XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                System.Diagnostics.Process.Start(FileName);//打开指定路径下的文件
        }
        //public static void CustomExport(this BaseView dgv, ExportTarget exportType= ExportTarget.Xlsx)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.FileName = "检查项目统计";
        //    saveFileDialog.DefaultExt = "xls";
        //    saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
        //    if (saveFileDialog.ShowDialog() != DialogResult.OK)   //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
        //        return;
        //    XlsExportOptions options = new XlsExportOptions();
        //    var FileName = saveFileDialog.FileName;
        //    try
        //    {
        //        dgv.Export(exportType, FileName, xoptions);
        //        if (DevExpress.XtraEditors.XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //            System.Diagnostics.Process.Start(FileName);//打开指定路径下的文件
        //    }
        //    catch (Exception ex)
        //    {
        //        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        //    }
        //}
    }
}
