using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
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
using static Sw.Hospital.HealthExaminationSystem.CustomerReport.PrintPreview;

namespace Sw.Hospital.HealthExaminationSystem.GroupReport
{
    public partial class GroupReporList : UserBaseForm
    {
        private readonly GroupReportAppService _GroupReportAppService;
        private readonly ICommonAppService _commonAppService;
        private List<ClientInfosNameDto> clientList;
        /// <summary>
        /// 单位服务
        /// </summary>
        private IClientInfoesAppService ClientInfoservice = null;
        public IClientInfoesAppService _ClientInfoService
        {
            get
            {
                if (ClientInfoservice == null) ClientInfoservice = new ClientInfoesAppService();
                return ClientInfoservice;
            }
        }
        public GroupReporList()
        {
            InitializeComponent();
            _GroupReportAppService = new GroupReportAppService();
            //_ItemGroupAppService = new SeleteItemGroupAppService();
            _commonAppService = new CommonAppService();          
            InitForm();
        }

        private void GroupReporList_Load(object sender, EventArgs e)
        {
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.DateTime = date;
            dateEditStart.DateTime = date.AddYears(-1);            
            gridView3.AddNewRow();
            gridView3.SetRowCellValue(0, Presentation,"健康体检团报");
            if (gridView3.RowCount == 1)
            {
                gridView3.SelectRows(0,0);
            }
            query();
        }
        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void InitForm()
        {

            try
            {
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = "已预约";
                 clientList = _ClientInfoService.QueryClientName(chargeBM);              
                textEditCompany.Properties.DataSource = clientList;
                var GroupReportSet = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.GroupReportSet);
             
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }

        }
        //单位回车事件
        private void seDw_KeyDown(object sender, KeyEventArgs e)
        {
            query();
        }
        //单位双击事件
        private void gridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            query();
        }
        //查询
        private void query()
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gridControlClientInfo.DataSource = null;

            try
            {
                //List<GroupClientInfoDto> output = new List<GroupClientInfoDto>();
                List<ClientInfosNameDto> Select = new List<ClientInfosNameDto>();
                Select = clientList;
                if (textEditCompany.EditValue != null)
                {
                    var clientId = Guid.Parse(textEditCompany.EditValue.ToString());
                    Select = Select.Where(o => o.Id == clientId).ToList();
                }
                //时间
                if (dateEditStart.Text.Trim() != null && dateEditStart.Text.Trim() != "" && dateEditEnd.Text.Trim() != null && dateEditEnd.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(dateEditStart.Text) > Convert.ToDateTime(dateEditEnd.Text))
                    {
                        dxErrorProvider.SetError(dateEditEnd, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        dateEditEnd.Focus();
                        return;
                    }

                    var ClientRegStart = Convert.ToDateTime(dateEditStart.Text);
                    var ClientRegEnd = Convert.ToDateTime(dateEditEnd.Text);
                    Select = Select.Where(o => o.CreationTime >= ClientRegStart && o.CreationTime < ClientRegEnd.AddDays(1)).ToList();
                }

                gridControlClientInfo.DataSource = Select;
            }
            catch (ApiProxy.UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }

        private void gridView2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //int columnscount = gridView2.DataRowCount;

            int[] selectRows = gridViewClientReg.GetSelectedRows();
            DataRow row = this.gridViewClientReg.GetDataRow(selectRows[0]);


            foreach (int i in selectRows)
            {

                gridView1.SetRowCellValue(i, gridViewClientTeamInfo.Columns["selected"], true);
            }


            //var DtoInfo = gridControl2.GetFocusedRowDto<ItemInfoGroupDto>();

        }




        private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int[] selectRows = gridViewClientReg.GetSelectedRows();

            //DataRow row = this.gridView2.GetDataRow(selectRows[0]);

            //gridView2.level
            if (selectRows.Length!=0)
            {
                var view = gridViewClientReg.GetDetailView(selectRows[0], 0);
                GridView dView = gridViewClientReg.GetDetailView(selectRows[0], 0) as GridView;
                if (dView != null)
                {
                    dView.SelectAll();
                    //for (int i = 0; i < dView.DataRowCount; i++)
                    //{
                    //    // 设置 子DataGrid的CheckBox值
                    //    dView.SetRowCellValue(i, dView.Columns["selected"], true);
                    //    dView.SelectRow(i);
                    //}
                }
            }
            


        }

        private void simpleButtonPreview_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridViewClientReg.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                //if ((string)gridView3.GetRowCellValue(selectIndexes[0], Presentation) == "健康体检团报")
                //{
                    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var printReport = new PrintClientReport();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                   // printReport.Print(true, input);
                //}
            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridViewClientReg.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                //if ((string)gridView3.GetRowCellValue(selectIndexes[0], Presentation) == "健康体检团报")
                //{
                //    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                //    var printReport = new PrintClientReport();
                //    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                //    printReport.Print(false, input);
                //}
                string path = "";
                if (Shell.BrowseForFolder("请选择文件夹！", out path) != DialogResult.OK)
                    return;
                string pathold = path;
                
              var clientname= gridView.GetRowCellValue(selectIndexes[0], ClientNames);
                string strnewpath = path + "\\" + clientname;
                var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                var printReport = new PrintClientReport();
                EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
               // printReport.Print(false , input, strnewpath);
            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }

        private void gridView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            List<EntityDto<Guid>> idls = new List<EntityDto<Guid>>();
            int[] selectRows = gridView.GetSelectedRows();
            if (selectRows.Length != 0)
            {

                var ClientName = gridView.GetRowCellValue(selectRows[0], "ClientName").ToString();
                textEditGroupNewspaper.Text = ClientName;

                foreach (int num in selectRows)
                {
                    EntityDto<Guid> entityDto = new EntityDto<Guid>();
                    entityDto.Id = Guid.Parse(gridView.GetRowCellValue(num, "Id").ToString());
                    idls.Add(entityDto);
                }
            }
            else
            {
               // XtraMessageBox.Show("请对单位进行勾选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // return;
            }

            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gridControl2.DataSource = null;

            try
            {
                // var output = _GroupReportAppService.QueryGroup(Dto);
                var output = _GroupReportAppService.getreglist(idls);                
                gridControl2.DataSource = output;

            }
            catch (ApiProxy.UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }
    }
}
