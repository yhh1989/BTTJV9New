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
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class FrmTTJZD : UserBaseForm
    {
        private IClientRegAppService _clientRegAppService;
        /// <summary>
        /// 团体结账单
        /// </summary>
        public FrmTTJZD()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var id = searchClinet.EditValue?.ToString();
            if (string.IsNullOrWhiteSpace(id))
            {
                ShowMessageBoxWarning("请选择单位。");
                return;
            }
            AutoLoading(() => {

                if (Tabs.SelectedTabPage.Name == departmentTabPage.Name)
                {
                    var data = _clientRegAppService.StatisticsDepartmentTTJZD(new EntityDto<Guid>() { Id = Guid.Parse(id) });
                    gridControlDepartment.DataSource = data.OrderBy(o => o.Category).ThenBy(o => o.Department);

                }
                else if (Tabs.SelectedTabPage.Name == teamTabPage.Name)
                {
                    var data = _clientRegAppService.StatisticsTeamTTJZD(new EntityDto<Guid>() { Id = Guid.Parse(id) });
                    gridControlTeam.DataSource = data.TeamTTJZDs;
                    lab.Text = "加项总金额：" + data.isAddMoney + ",加项折后总金额：" + data.isDisAddMoney + ",加项已检总金额" + data.isCheckedAddMoney;

                }
                else if (Tabs.SelectedTabPage.Name == personalTabPage.Name)
                {
                    var data = _clientRegAppService.StatisticsPersonalTTJZD(new EntityDto<Guid>() { Id = Guid.Parse(id) });
                    gridControlPersonal.DataSource = data.PayPersons;
                    gridControlPersonFree.DataSource = data.FreePersons;
                }
            });
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            var gvControl = new DevExpress.XtraGrid.GridControl();
            if (Tabs.SelectedTabPage.Name == departmentTabPage.Name)
            {
                gvControl = gridControlDepartment;
                if (gridControlDepartment.DataSource == null)
                    return;
            }
            else if (Tabs.SelectedTabPage.Name == teamTabPage.Name)
            {
                gvControl = gridControlTeam;
                if (gridControlTeam.DataSource == null)
                    return;
            }
            else if (Tabs.SelectedTabPage.Name == personalTabPage.Name)
            {
                var fileName1 = saveFileDialog.FileName;
                ExcelHelper.ExportToExcel(fileName1, gridControlPersonal);
                var fileName2 = saveFileDialog.FileName;
                ExcelHelper.ExportToExcel(fileName1,gridControlPersonFree);
                return;
            }
            var fileName = saveFileDialog.FileName;
            ExcelHelper.ExportToExcel(fileName, gvControl);
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void FrmTTJZD_Load(object sender, EventArgs e)
        {
            _clientRegAppService = new ClientRegAppService();
            searchClinet.Properties.DataSource = new CustomerAppService().QuereyClientRegInfos(new FullClientRegDto() { SDState = (int)SDState.Unlocked });//加载单位分组数据

        }

        private void Tabs_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            btnSearch.PerformClick();
        }
    }
}