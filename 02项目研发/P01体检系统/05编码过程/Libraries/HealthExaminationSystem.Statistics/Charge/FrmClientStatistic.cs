using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
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

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class FrmClientStatistic : UserBaseForm
    {
        private IUserAppService _Userservice = null;
        private readonly PhysicalExaminationAppService _PhysicalAppService;
        private IChargeAppService ChargeAppService;
        private readonly ICommonAppService _commonAppService;
        public FrmClientStatistic()
        {
            InitializeComponent();
            _Userservice = new UserAppService();
            ChargeAppService = new ChargeAppService();
            _PhysicalAppService = new PhysicalExaminationAppService();
            _commonAppService = new CommonAppService();
        }

        private void FrmClientStatistic_Load(object sender, EventArgs e)
        {
            gridView1.OptionsSelection.MultiSelect = true;
            //gridViewCus.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            gridView1.OptionsView.ShowIndicator = false;//不显示指示器
            gridView1.OptionsBehavior.ReadOnly = false;
            gridView1.OptionsBehavior.Editable = false;
            //客服用户信息                  
            List<UserForComboDto> _currentUserdtoSys = new List<UserForComboDto>();
            _currentUserdtoSys = DefinedCacheHelper.GetComboUsers();
            gluClientDegree.Properties.DataSource = _currentUserdtoSys;
            //单位
            var clientreglist = _PhysicalAppService.QueryCompany();
            TeDW.Properties.DataSource = clientreglist;

            // 初始化时间框        
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEnd.DateTime = date;
            dateStar.DateTime = date;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            var user = txtUser.Text.Trim();
            var clientRegId = TeDW.EditValue as Guid?;            
            var userid = gluClientDegree.EditValue as long?;
            var name = searchLookUpEdit2.EditValue?.ToString();
            var input = new SearchSFTypeDto
            {
                SearchName = name,
                ClientRegID = clientRegId,
                UserID = userid,
                LinkName = user

            };
            if (!string.IsNullOrWhiteSpace(dateStar.Text.Trim()) && !string.IsNullOrWhiteSpace(dateEnd.Text.Trim()))
            {
                input.StarDate = dateStar.DateTime as DateTime?;
                input.EndDate = dateEnd.DateTime as DateTime?;

            }           
            List<ClientPaymentDto> clientPaymentDtos= ChargeAppService.getClientPayment(input);
            gridControlClient.DataSource = clientPaymentDtos;
        }

        private void butOut_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "单位收费信息列表";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            ExportToExcel(saveFileDialog, gridControlClient);
            saveFileDialog.FileName = saveFileDialog.FileName.Replace("人员信息", "项目信息");

            //ExportToExcel(saveFileDialog, gcItem);
            //gcItem.ExportToXls(saveFileDialog.FileName);
            XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static void ExportToExcel(System.Windows.Forms.SaveFileDialog saveFileDialog, params DevExpress.XtraPrinting.IPrintable[] panels)
        {

            string FileName = saveFileDialog.FileName;
            var ps = new DevExpress.XtraPrinting.PrintingSystem();
            var link = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
            ps.Links.Add(link);
            foreach (var panel in panels)
            {
                link.Links.Add(CreatePrintableLink(panel));
            }
            link.Landscape = true;//横向
                                  //判断是否有标题，有则设置
                                  //link.CreateDocument(); //建立文档
            int count = 1;
            //在重复名称后加（序号）
            while (System.IO.File.Exists(FileName))
            {
                if (FileName.Contains(")."))
                {
                    int start = FileName.LastIndexOf("(");
                    int end = FileName.LastIndexOf(").") - FileName.LastIndexOf("(") + 2;
                    FileName = FileName.Replace(FileName.Substring(start, end), string.Format("({0}).", count));
                }
                else
                {
                    FileName = FileName.Replace(".", string.Format("({0}).", count));
                }
                count++;
            }
            if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
            {
                var options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXlsx(FileName, options);
            }
            else
            {
                var options = new DevExpress.XtraPrinting.XlsExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXls(FileName, options);
            }
        }
        /// <summary>
        /// 创建打印Componet
        /// </summary>
        /// <param name="printable"></param>
        /// <returns></returns>
        public static DevExpress.XtraPrinting.PrintableComponentLink CreatePrintableLink(DevExpress.XtraPrinting.IPrintable printable)
        {
            var chart = printable as DevExpress.XtraCharts.ChartControl;
            if (chart != null)
                chart.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Stretch;
            var printableLink = new DevExpress.XtraPrinting.PrintableComponentLink() { Component = printable };
            return printableLink;
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var dto = gridControlClient.GetFocusedRowDto<ClientPaymentDto>();
            if (dto == null)
                return;
            if (dto.Id !=null)
            {
                //项目列表  
                QueryClass query = new QueryClass();
                List<Guid?> clientRegs = new List<Guid?>();
                clientRegs.Add(dto.Id);
                query.ClientInfoId = clientRegs;
                gcItem.DataSource = ChargeAppService.KSGZLStatistics(query);
            }
        }

        private void gvItem_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.Name == cnSFType.Name && e.Value.ToString() != "")
            {
                e.DisplayText = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ChargeCategory, (int)e.Value).Text.ToString();
            }
        }

        private void TeDW_EditValueChanged(object sender, EventArgs e)
        {
            butSearch.Focus();
        }
    }
}
