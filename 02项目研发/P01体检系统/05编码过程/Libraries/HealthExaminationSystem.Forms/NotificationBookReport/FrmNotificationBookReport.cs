using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.NotificationBookReport
{
    public partial class FrmNotificationBookReport : UserBaseForm
    {
        private readonly IGroupReportAppService _groupReportAppService;
        private readonly IInspectionTotalAppService _inspectionTotalAppService;
        public FrmNotificationBookReport()
        {
            InitializeComponent();
            _groupReportAppService = new GroupReportAppService();
            _inspectionTotalAppService = new InspectionTotalAppService();
        }

        private void FrmNotificationBookReport_Load(object sender, EventArgs e)
        {
            InitForm();
            var examinationTypes = DefinedCacheHelper.GetBasicDictionary()
                .Where(r => r.Type == BasicDictionaryType.ExaminationType.ToString()).ToList();
            repositoryItemLookUpEditExaminationType.DataSource = examinationTypes;
        }

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void InitForm()
        {
            // 加载所有单位
            var companies = DefinedCacheHelper.UpdateClientRegNameComDto();
            searchLookUpEditCompany.Properties.DataSource = companies;
        }

        private void Query()
        {
            if (searchLookUpEditCompany.EditValue == null && dateEditStart.Text == null && dateEditEnd.Text == null)
            {
                return;
            }

            if (searchLookUpEditCompany.EditValue is Guid id)
            {
                gridControlCompanyRegister.DataSource = null;
                AutoLoading(() =>
                {
                    var output =
                        _groupReportAppService.GetCompanyRegisterIncludeGroupAndPersonnel(new EntityDto<Guid>
                        { Id = id });

                    gridControlCompanyRegister.DataSource = output;
                });
            }
        }

        private void searchLookUpEditCompany_KeyDown(object sender, KeyEventArgs e)
        {
            Query();
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void searchLookUpEditCompany_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonQuery.PerformClick();
        }

        private void gridViewCompanyRegister_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var selectRows = gridViewCompanyRegister.GetSelectedRows();

            if (selectRows.Length != 0)
            {
                var view = gridViewCompanyRegister.GetDetailView(selectRows[0], 0);
                var dView = gridViewCompanyRegister.GetDetailView(selectRows[0], 0) as GridView;
                if (dView != null)
                {
                    dView.SelectAll();
                    for (var i = 0; i < dView.DataRowCount; i++)
                    {
                        // 设置 子DataGrid的CheckBox值
                        dView.SetRowCellValue(i, dView.Columns["selected"], true);
                        dView.SelectRow(i);
                    }
                }
            }
        }

        private void simpleButtonReport_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridViewCompanyRegister.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                var id = gridViewCompanyRegister.GetRowCellValue(selectIndexes[0], ConClientRegID).ToString();
                List<TjlCustomerSummarizeDto> Result = null;
                if (checkEditFuZhen.Checked)
                {
                    Sw.Hospital.HealthExaminationSystem.Common.NotificationBookReport.CheckReviewPrint(id, true);
                }
                if (checkEditJinJi.Checked)
                {
                    if (Result == null)
                        Result = _inspectionTotalAppService.GetCustomerSummary(new EntityDto<Guid>() { Id = Guid.Parse(id) });
                    var JinJiData = Result.Where(n => n.ZYConclusion == "208c3745-28f5-4599-8067-ddb609185122".ToLower());
                    if (JinJiData.Count() > 0)
                    {
                        foreach (var item in JinJiData)
                        {
                            Sw.Hospital.HealthExaminationSystem.Common.NotificationBookReport.ContraindicationNotificationPrint(item, true);
                        }
                    }
                    else
                    {
                        ShowMessageBoxWarning("此次预约中不包含禁忌证患者！");
                    }
                }
                if (checkEditYiSi.Checked)
                {
                    if (Result == null)
                        Result = _inspectionTotalAppService.GetCustomerSummary(new EntityDto<Guid>() { Id = Guid.Parse(id) });
                    var YiSiData = Result.Where(n => n.ZYConclusion == "318FDDF0-2842-4CBE-A324-5FF0D74E61B6".ToLower());
                    if (YiSiData.Count() > 0)
                    {
                        foreach (var item in YiSiData)
                        {
                            Sw.Hospital.HealthExaminationSystem.Common.NotificationBookReport.SuspectedOccupationalDiseasesPrint(item, true);
                        }
                    }
                    else
                    {
                        ShowMessageBoxWarning("此次预约中不包含疑似职业健康患者！");
                    }
                }
            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }
    }
}
