using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class CustomerReportHandover : UserBaseForm
    {
        private ICustomerReportAppService _service = new CustomerReportAppService();

        public CustomerReportHandover()
        {
            InitializeComponent();
            
            gridView.Columns[gridColumnState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView.Columns[gridColumnState.FieldName].DisplayFormat.Format = new CustomFormatter(StateFormatter);
        }
        private string StateFormatter(object obj)
        {
            int.TryParse(obj?.ToString(), out int val);
            switch (val)
            {
                case 0:
                    return "无";
                case 1:
                    return "已交接";
                case 2:
                    return "已发放";
            }
            return "";
        }

        #region 事件

        private void CustomerReportHandover_Load(object sender, EventArgs e)
        {
            var userDto = DefinedCacheHelper.GetComboUsers();
            teHanderover.Properties.DataSource = userDto;

            ceIsScanCodeAutoHandover.Checked = true;
            reReceiptor.Text = CurrentUser.Name;
            var input = new CustomerReportQuery
            {
                CustomerReportState = 1,
            };
            input.StartHandoverDate = DateTime.Now.Date;
            input.EndHandoverDate = DateTime.Now.AddDays(1).AddDays(1);
            var output = _service.Query(input);
            gridControl.DataSource=output;

        }

        private void CustomerReportHandover_Shown(object sender, EventArgs e)
        {
            teNumber.Focus();
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            Ok();
            teNumber.Text = string.Empty;
            teNumber.Focus();
        }
        private void sbCancel_Click(object sender, EventArgs e)
        {
            teNumber.Text = "";
            teNumber.Focus();
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            Save();
            teNumber.Focus();
        }

        private void ceIsScanCodeAutoHandover_CheckedChanged(object sender, EventArgs e)
        {
            sbSave.Enabled = !ceIsScanCodeAutoHandover.Checked;
            teNumber.Focus();
        }
        private void CustomerReportHandover_FormClosing(object sender, FormClosingEventArgs e)
        {
            var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            if (list != null && list.Any(m => m.State == 0))
            {
                var res = XtraMessageBox.Show("列表中还有未交接的报告，是否确认不再交接？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == nameof(CustomerReportFullDto.State))
            {
                if (int.TryParse(e.CellValue?.ToString(), out int val))
                {
                    switch (val)
                    {
                        //case 1://测试要求去掉背景色
                        //    {
                        //        e.Appearance.BackColor = Color.Yellow;
                        //        e.Appearance.ForeColor = Color.Red;
                        //    }
                        //    break;
                        case 2:
                            {
                                //e.Appearance.BackColor = Color.Lime;
                                e.Appearance.ForeColor = Color.ForestGreen;
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region 处理

        /// <summary>
        /// 确认流水号
        /// </summary>
        private void Ok()
        {
            dxErrorProvider.ClearErrors();
            string handerover = teHanderover.Text.Trim();
            if (string.IsNullOrEmpty(handerover))
            {
                dxErrorProvider.SetError(teHanderover, string.Format(Variables.MandatoryTips, "交接人"));
                teHanderover.Focus();
                return;
            }
            string number = teNumber.Text.Trim();
            if (string.IsNullOrEmpty(number))
            {
                dxErrorProvider.SetError(teNumber, string.Format(Variables.MandatoryTips, "流水号"));
                teNumber.Focus();
                return;
            }

            var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            if(list !=null && list.Count>0 && list.Any(m=>m.CustomerReg.CustomerBM == number))
            {
                var dto = list.FirstOrDefault(m => m.CustomerReg.CustomerBM == number);
                ShowMessageBoxWarning($"体检号{number}已被读取，当前状态：{StateFormatter(dto.State)}。");
                return;
            }

            AutoLoading(() =>
            {
                var input = new CustomerReportHandoverInput
                {
                    Number = number,
                    Handover = handerover,
                    Receiptor = CurrentUser.Name,
                    Complete = ceIsScanCodeAutoHandover.Checked
                };
                var dto = _service.Handover(input);
                if (dto != null)
                {
                   
                    var cuslist= gridControl.DataSource as  List<CustomerReportFullDto>;
                    cuslist.Insert(0,dto);
                    gridControl.DataSource = cuslist;
                    gridControl.Refresh();
                    gridControl.RefreshDataSource();
                    //gridControl.AddDtoListItem(dto);
                }
            }, Variables.LoadingSaveing);
        }
        /// <summary>
        /// 确认流水号
        /// </summary>
        private void Cancel(CustomerReportFullDto InDto)
        {
            dxErrorProvider.ClearErrors();      
            string number = InDto.CustomerReg.CustomerBM;
            AutoLoading(() =>
            {
                var input = new CustomerReportHandoverInput
                {
                    Number = number,                   
                    Receiptor = CurrentUser.Name,
                    Complete = ceIsScanCodeAutoHandover.Checked
                };
                
                var dto = _service.CancelHandover(input);
                if (dto != null)
                {
                    var cuslist = gridControl.DataSource as List<CustomerReportFullDto>;
                    cuslist.Remove(InDto);
                    gridControl.DataSource = cuslist;
                    gridControl.RefreshDataSource();
                    gridControl.Refresh();
                    //gridControl.AddDtoListItem(dto);
                }
            }, Variables.LoadingSaveing);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private void Save()
        {
            //var selectIndexes = gridControl.GetSelectedRows();
            var list = gridControl.GetSelectedRowDtos<CustomerReportFullDto>();

            //var list = gridControl.GetDtoListDataSource<CustomerReportFullDto>();
            list = list.Where(m => m.State == 0).ToList();
            if (list.Count > 0)
            {
                List<CustomerReportHandoverInput> input = new List<CustomerReportHandoverInput>();
                foreach (var item in list)
                {
                    input.Add(new CustomerReportHandoverInput
                    {
                        Number = item.CustomerReg.CustomerBM,
                        Handover = item.Handover,
                        Receiptor = item.Receiptor,
                    });
                }

                AutoLoading(() =>
                {
                    _service.BatchHandover(input);
                    foreach (var item in list)
                    {
                        item.State = 1;
                    }
                    gridControl.RefreshDataSource();
                }, Variables.LoadingSaveing);
            }
        }

        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CustomerReportQuery show = new CustomerReportQuery();
            if (dateEditStartTime.EditValue != null)
                show.StartHandoverDate = dateEditStartTime.DateTime;

            if (dateEditEndTime.EditValue != null)
                show.EndHandoverDate = dateEditEndTime.DateTime;

            if (show.StartHandoverDate > show.EndHandoverDate)
            {
                dxErrorProvider.SetError(dateEditStartTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                dxErrorProvider.SetError(dateEditEndTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                return;
            }
            if (checkOwer.Checked == true)
            {

                show.Receiptor = CurrentUser.Name.ToString();
            }
            var data = _service.Query(show);
            gridControl.DataSource = data;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "体检交接表";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            //var ds = gridControl.DataSource;
            //gridColumn1.Visible = true;
            //gridColumn3.Visible = true;
            //gridColumn4.Visible = true;
            //gridColumn5.Visible = true;
            //gridColumn6.Visible = true;
            //gridColumnHandover.Visible = false;
            //gridColumnReceiptor.Visible = false;
            //gridColumnHandoverTime.Visible = false;
            //gridColumnState.Visible = false;
            //gridControl.DataSource = ds;
            gridControl.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var list = gridControl.GetSelectedRowDtos<CustomerReportFullDto>();
            foreach (var item in list)
            {
                Cancel(item);
            }
        }
    }
}
