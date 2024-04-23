using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Common;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.CompanyReport
{
    /// <summary>
    /// 父公司预约报告打印器
    /// </summary>
    public partial class XtraFormParentCompanyRegisterReportPrinter : UserBaseForm
    {
        /// <summary>
        /// 初始化“父公司预约报告打印器”
        /// </summary>
        public XtraFormParentCompanyRegisterReportPrinter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormParentCompanyRegisterReportPrinter_Load(object sender, EventArgs e)
        {
            var ids = DefinedCacheHelper.GetAllClientInfoCache().Where(r => r.ParentId != null).Select(r => r.ParentId)
                .Distinct();
            searchLookUpEditCompany.Properties.DataSource = DefinedCacheHelper.GetAllClientInfoCache().Where(r => ids.Contains(r.Id)).ToList();
        }

        /// <summary>
        /// “公司检索框”弹出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchLookUpEditCompany_Popup(object sender, EventArgs e)
        {
            searchLookUpEditCompanyView.BestFitColumns();
        }

        /// <summary>
        /// “查询”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            ButtonEnabledPropertySynchronize(sender, () =>
             {
                 if (searchLookUpEditCompany.EditValue is Guid id)
                 {
                     AutoLoading(() =>
                              {
                                  var data = DefinedCacheHelper.DefinedApiProxy.CompanyRelatedAppService
                                      .CompanyInformationListByParentId(new EntityDto<Guid>(id));
                                  data.Wait();

                                  gridControlCompanyInformation.DataSource = data.Result;
                                  gridViewCompanyInformation.BestFitColumns();

                                  gridViewCompanyInformation.BeginUpdate();
                                  try
                                  {
                                      for (var i = 0; i < gridViewCompanyInformation.RowCount; i++)
                                      {
                                          gridViewCompanyInformation.SetMasterRowExpanded(i, true);
                                      }
                                  }
                                  finally
                                  {
                                      gridViewCompanyInformation.EndUpdate();
                                  }
                              });
                 }
             });
        }

        /// <summary>
        /// “单位信息表格”行展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewCompanyInformation_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            if (gridViewCompanyInformation.GetDetailView(e.RowHandle, e.RelationIndex) is GridView gridView)
            {
                gridView.BestFitColumns();
            }
        }

        /// <summary>
        /// “单位信息表格”行正在关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewCompanyInformation_MasterRowCollapsing(object sender, MasterRowCanExpandEventArgs e)
        {
            e.Allow = false;
        }

        /// <summary>
        /// “预览”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPreview_Click(object sender, EventArgs e)
        {
            ButtonEnabledPropertySynchronize(sender, () =>
            {
                GenerateReport(true);
            });
        }

        /// <summary>
        /// “打印”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            ButtonEnabledPropertySynchronize(sender, () =>
            {
                GenerateReport();
            });
        }

        /// <summary>
        /// 生成报告
        /// </summary>
        /// <param name="preview"></param>
        private void GenerateReport(bool preview = false)
        {
            if (searchLookUpEditCompany.EditValue is Guid parentCompanyId)
            {
                var rows = new List<CompanyRegisterDtoNo1>();
                for (int i = 0; i < gridViewCompanyInformation.RowCount; i++)
                {
                    if (gridViewCompanyInformation.GetVisibleDetailView(i) is GridView view)
                    {
                        foreach (var selectedRow in view.GetSelectedRows())
                        {
                            if (view.GetRow(selectedRow) is CompanyRegisterDtoNo1 row)
                            {
                                rows.Add(row);
                            }
                        }
                    }
                }

                if (rows.Count == 0)
                {
                    ShowMessageBoxWarning("请先选择要打印的单位预约信息。");
                }
                else
                {
                    try
                    {
                        var printer = new PrintParentCompanyReport();
                        printer.LoadData(parentCompanyId, rows).Print(preview);
                    }
                    catch (Exception e)
                    {
                        ShowMessageBoxError(e.Message);
                    }
                }
            }
            else
            {
                ShowMessageBoxWarning("请先选择单位进行查询。");
            }
        }

        /// <summary>
        /// “选择最后一次预约”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropDownButtonXZZHYCYY选择最后一次预约_Click(object sender, EventArgs e)
        {
            foreach (BaseView view in gridControlCompanyInformation.Views)
            {
                if (view != gridControlCompanyInformation.MainView)
                {
                    if (view is GridView gridView)
                    {
                        if (gridView.RowCount != 0)
                        {
                            var rowIndexArray = gridView.GetSelectedRows();
                            if (rowIndexArray.Length != 0)
                            {
                                foreach (var rowHandle in rowIndexArray)
                                {
                                    gridView.UnselectRow(rowHandle); 
                                }
                            }
                            gridView.SelectRow(0);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// “选择所有预约”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemXZSYYY选择所有预约_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (BaseView view in gridControlCompanyInformation.Views)
            {
                if (view != gridControlCompanyInformation.MainView)
                {
                    if (view is GridView gridView)
                    {
                        if (gridView.RowCount != 0)
                        {
                            gridView.SelectAll();
                        }
                    }
                }
            }
        }
    }
}