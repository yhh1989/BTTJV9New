using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisRequisition
{
    public partial class OccDisRequisitionList : UserBaseForm
    {
        private readonly IOccDisRequisitionAppService _OccDisRequisitionAppService;

        public OccDisRequisitionList()
        {
            InitializeComponent();
            _OccDisRequisitionAppService = new OccDisRequisitionAppService();
        }

        private void OccDisRequisitionList_Load(object sender, EventArgs e)
        {
            var list = new List<EnumModel>();
            list.Add(new EnumModel { Id = 1, Name = "是" });
            list.Add(new EnumModel { Id = 0, Name = "否" });
            editActive.Properties.DataSource = list;

            var lists = new List<EnumModel>();
            lists.Add(new EnumModel { Id = 2020, Name = "2020" });
            lists.Add(new EnumModel { Id = 2019, Name = "2019" });
            lists.Add(new EnumModel { Id = 2018, Name = "2018" });
            lists.Add(new EnumModel { Id = 2017, Name = "2017" });
            comboBoxEdit1.Properties.DataSource = lists;
            //OutOccCustomerSumDto show = new OutOccCustomerSumDto();
            //var data = _OccDisRequisitionAppService.GetOccDisRequisition(show);
            //gridControl1.DataSource = data;
            comClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();

            gridView1.Columns[conTZDPrintSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conTZDPrintSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(PrintSateHelper.PrintSateFormatter);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                foreach (var Index in selectIndexes)
                {
                    var id = (Guid)gridView1.GetRowCellValue(Index, gridColumn8);
                    var printReport = new GroupReportConsomer();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    printReport.Print(false, input);
                }


            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OutOccCustomerSumDto show = new OutOccCustomerSumDto();
            if (!string.IsNullOrWhiteSpace(comClient.EditValue?.ToString()))
            {
                show.ClientRegID = (Guid)comClient.EditValue;
            }
            if (dateEditStartTime.EditValue != null)
                show.StartCheckDate = dateEditStartTime.DateTime;

            if (dateEditEndTime.EditValue != null)
                show.EndCheckDate = dateEditEndTime.DateTime;

            if (show.StartCheckDate > show.EndCheckDate)
            {
                dxErrorProvider.SetError(dateEditStartTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                dxErrorProvider.SetError(dateEditEndTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                return;
            }
            if (!string.IsNullOrWhiteSpace(searchName.Text.Trim()))
            {
                show.CustomerBM = searchName.Text.Trim();
            }
            if (comboBoxEdit1.EditValue != null)
                show.YearTime = comboBoxEdit1.Text.Trim();
            var data = _OccDisRequisitionAppService.GetOccDisRequisition(show);
            if (!string.IsNullOrEmpty(editActive.EditValue?.ToString()))
            {
                if (editActive.EditValue?.ToString() == "1")
                {
                    data = data.Where(p => p.IsReview == "是").ToList();
                }
                else if (editActive.EditValue?.ToString() == "0")
                {
                    data = data.Where(p => p.IsReview == "否").ToList();
                }
            }
            //var dataShow = data.Select(p => new
            //{
            //    p.Age,
            //    p.CheckDate,
            //    p.ClientName,
            //    p.ClientRegID,
            //    p.Conclusion,
            //    p.CustomerBM,
            //    p.CustomerRegBMId,
            //    p.CustomerRegNum,
            //    p.EndCheckDate,              
            //    p.IDCardNo,
            //    p.InjuryAge,
            //    p.IsReview,
            //    p.Name,
            //    p.PostState,
            //    p.ReviewContent,
            //    p.ReviewContentDate,
            //    p.Sex,
            //    p.StartCheckDate,
            //    p.TotalWorkAge,
            //    p.TypeWork,
            //    p.TZDPrintSate,
            //    p.WorkName,
            //    p.YearTime,
            //    p.ZYRiskName,
            //    p.ZYTreatmentAdvice
            //}).Distinct().ToList(); 
            gridControl1.DataSource = data;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                foreach (var Index in selectIndexes)
                {
                    var id = (Guid)gridView1.GetRowCellValue(Index, gridColumn8);
                    var printReport = new GroupReportConsomer();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    printReport.Prints(false, input);
                }


            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }
       
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                string path = "";
                if (Shell.BrowseForFolder("请选择文件夹！", out path) != DialogResult.OK)
                    return;
                string pathold = path;
                foreach (var Index in selectIndexes)
                {

                   //var dr= gridView1.GetDataRow(Index) as OutOccCustomerSumDto;

                    var dr = gridView1.GetRowCellValue(Index, conIsReview);
                    var clientname = gridView1.GetRowCellValue(Index, gridColumn3);
                    string strnewpath = path + "\\" + clientname;
                    var id = (Guid)gridView1.GetRowCellValue(Index, gridColumn8);
                    var printReport = new GroupReportConsomer();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    printReport.Print(false, input, strnewpath);

                    //复查通知单
                    if (dr.ToString() == "是")
                    {
                        printReport.Prints(false, input, strnewpath);
                    }
                }
            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        public class Shell
        {
            private class Win32
            {
                public const int _MAX_PATH = 260;
                public const uint BIF_EDITBOX = 0x0010;
                public const uint BIF_NEWDIALOGSTYLE = 0x0040;

                public delegate int BFFCALLBACK(IntPtr/*HWND*/   hwnd, uint/*UINT*/   uMsg, IntPtr/*LPARAM*/   lParam, IntPtr/*LPARAM*/   lpData);

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct BROWSEINFO
                {
                    public IntPtr/*HWND*/   _hwndOwner;
                    public IntPtr/*LPCITEMIDLIST*/   _pidlRoot;
                    [MarshalAs(UnmanagedType.LPStr)]
                    public string/*LPTSTR*/   _szDirectory;
                    [MarshalAs(UnmanagedType.LPStr)]
                    public string/*LPCTSTR*/   _lpszTitle;
                    public uint/*UINT*/   _ulFlags;
                    [MarshalAs(UnmanagedType.FunctionPtr)]
                    public BFFCALLBACK/*BFFCALLBACK*/   _lpfn;
                    public IntPtr/*LPARAM*/   _lParam;
                    public int/*int*/   _iImage;

                    public BROWSEINFO(IntPtr parent, string title)
                    {
                        _hwndOwner = parent;
                        _pidlRoot = (IntPtr)0;
                        _szDirectory = null;
                        _lpszTitle = title;
                        _ulFlags = BIF_EDITBOX | BIF_NEWDIALOGSTYLE;
                        _lpfn = (BFFCALLBACK)null;
                        _lParam = (IntPtr)0;
                        _iImage = 0;
                    }
                }

                [ComImport]
                [Guid("00000002-0000-0000-C000-000000000046")]
                [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
                internal interface IMalloc
                {
                    [PreserveSig]
                    IntPtr/*void   *   */   Alloc(ulong/*ULONG*/   cb);
                    [PreserveSig]
                    IntPtr/*void   *   */   Realloc(IntPtr/*void   *   */   pv, ulong/*ULONG*/   cb);
                    [PreserveSig]
                    void/*void*/   Free(IntPtr/*void   *   */   pv);
                    [PreserveSig]
                    ulong/*ULONG*/   GetSize(IntPtr/*void   *   */   pv);
                    [PreserveSig]
                    int/*int*/   DidAlloc(IntPtr/*void   *   */   pv);
                    [PreserveSig]
                    void/*void*/   HeapMinimize();
                }

                [DllImport("shell32.dll")]
                public static extern IntPtr/*LPITEMIDLIST*/   SHBrowseForFolder(ref BROWSEINFO/*LPBROWSEINFO*/   lpbi);

                [DllImport("shell32.dll")]
                public static extern bool/*BOOL*/   SHGetPathFromIDList(IntPtr/*LPCITEMIDLIST*/   pidl, StringBuilder/*LPTSTR*/   pszPath);

                [DllImport("Shell32.dll")]
                public static extern int/*HRESULT*/   SHGetMalloc([MarshalAs(UnmanagedType.IUnknown)]   out object   /*LPMALLOC   *   */   ppMalloc);
            }

            ///   <summary>   
            ///   Browse   for   a   shell   folder.   
            ///   </summary>   
            ///   <param   name="title">Title   to   display   in   dialogue   box</param>   
            ///   <param   name="path">Return   path</param>   
            ///   <returns>DialogResult.OK   if   successful</returns>   
            public static DialogResult BrowseForFolder(string title, out string path)
            {
                return BrowseForFolder((IntPtr)0, title, out path);
            }

            public static DialogResult BrowseForFolder(IntPtr parent, string title, out string path)
            {
                path = null;
                Win32.BROWSEINFO browseInfo = new Win32.BROWSEINFO(parent, title);
                IntPtr pidl = Win32.SHBrowseForFolder(ref browseInfo);
                if (pidl == IntPtr.Zero) { return DialogResult.Cancel; }
                try
                {
                    StringBuilder stringBuilder = new StringBuilder(Win32._MAX_PATH);
                    if (Win32.SHGetPathFromIDList(pidl, stringBuilder))
                    {
                        path = stringBuilder.ToString();
                        return DialogResult.OK;
                    }
                    else
                    {
                        return DialogResult.Cancel;
                    }
                }
                finally
                {
                    //   Free   memory   allocated   by   shell.   
                    object pMalloc = null;
                    //   Get   the   IMalloc   interface   and   free   the   block   of   memory.   
                    if (Win32.SHGetMalloc(out pMalloc) == 0/*NOERROR*/)
                    {
                        ((Win32.IMalloc)pMalloc).Free(pidl);
                    }
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void comClient_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
                

            }
        }

        private void editActive_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;


            }
        }
    }
}
