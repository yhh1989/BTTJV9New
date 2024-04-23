using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusioncontraindication;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusioncontraindication;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusioncontraindication.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisRequisition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Occupationalcontraindication
{
    public partial class Occupationalcontraindications : UserBaseForm
    {
        private readonly IOccupationalcontraindicationAppService _OccupationalcontraindicationAppService;
        private readonly IClientInfoesAppService _IClientInfoesAppService;

        public Occupationalcontraindications()
        {
            InitializeComponent();
            _OccupationalcontraindicationAppService = new OccupationalcontraindicationAppService();
            _IClientInfoesAppService = new ClientInfoesAppService();
        }

        private void Occupationalcontraindications_Load(object sender, EventArgs e)
        {
            var occ = new OccContraindicationGet();

            var lists = new List<EnumModel>();
            lists.Add(new EnumModel { Id = 2020, Name = "2020" });
            lists.Add(new EnumModel { Id = 2019, Name = "2019" });
            lists.Add(new EnumModel { Id = 2018, Name = "2018" });
            lists.Add(new EnumModel { Id = 2017, Name = "2017" });

            comboBoxEdit1.Properties.DataSource = lists;
            var results = _OccupationalcontraindicationAppService.GetOccConclusionSuspected(occ);
            gridControl1.DataSource = results;

            ClientInfoesListInput s = new ClientInfoesListInput();
            var result = _IClientInfoesAppService.Query(s);
            txtClientName.Properties.DataSource = result;
        }

        void LoadData()
        {
            try
            {

                var occ = new OccContraindicationGet();
                if (!string.IsNullOrWhiteSpace(txtClientName.Text.Trim()))
                {
                    occ.ClientName = txtClientName.Text.Trim();
                }
                if (Startdate.EditValue != null)
                    occ.StartCheckDate = Startdate.DateTime;

                if (Enddate.EditValue != null)
                    occ.EndCheckDate = Enddate.DateTime;

                if (occ.StartCheckDate > occ.EndCheckDate)
                {
                    dxErrorProvider.SetError(Startdate, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                    dxErrorProvider.SetError(Enddate, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                    return;
                }
                if (comboBoxEdit1.EditValue != null)
                    occ.YearTime = comboBoxEdit1.Text.Trim();
                var data = _OccupationalcontraindicationAppService.GetOccConclusionSuspected(occ);
                gridControl1.DataSource = data;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }

        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var result = gridControl1.DataSource as List<OccConclusioncontraindicationShowDto>;
            if (result != null && result.Count > 0)
            {
                var frm = new ContraindicationsChar(result);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
           
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                string path = "";
                if (Shell.BrowseForFolder("请选择文件夹！", out path) != DialogResult.OK)
                    return;
                string pathold = path;
                var clientname = gridView1.GetRowCellValue(selectIndexes[0], gridColumn3);
                string strnewpath = path + "\\" + clientname;
                var id = (Guid)gridView1.GetRowCellValue(selectIndexes[0], gridColumn8);
                var printReport = new GroupReportConsomer();
                EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                printReport.Print(false, input, strnewpath);
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
    }
}
