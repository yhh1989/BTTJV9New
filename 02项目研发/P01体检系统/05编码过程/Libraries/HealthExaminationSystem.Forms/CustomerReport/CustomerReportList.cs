using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class CustomerReportList : UserBaseForm
    {
        private ICustomerReportAppService _service = new CustomerReportAppService();

        public CustomerReportList()
        {
            InitializeComponent();
            lueCustomerReportState.SetClearButton();

            gridView.Columns[gridColumnState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView.Columns[gridColumnState.FieldName].DisplayFormat.Format = new CustomFormatter((obj) =>
            {
                int.TryParse(obj?.ToString(), out int val);
                switch (val)
                {
                    case 1:
                        return "已交接";
                    case 2:
                        return "已发放";
                }
                return "";
            });
        }

        #region 事件

        private void CustomerReportList_Load(object sender, EventArgs e)
        {
            LoadControlData();
        }

        private void sbQuery_Click(object sender, EventArgs e)
        {
            Reload();
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
        /// 加载控件数据
        /// </summary>
        private void LoadControlData()
        {
            lueCustomerReportState.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(CustomerReportState));
            lueDateType.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(CustomerReportDateType));
            lueDateType.EditValue = CustomerReportDateType.Register;
            lueMemberType.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(CustomerReportMemberType));
            lueMemberType.EditValue = CustomerReportMemberType.Receiver;
            deStart.EditValue = DateTime.Now;
            deEnd.EditValue = DateTime.Now;
            sleDW.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }
        /// <summary>
        /// 重新加载数据
        /// </summary>
        private void Reload()
        {
            gridControl.DataSource = null;
            var input = new CustomerReportQuery
            {
                QueryText = teQueryText.Text.Trim(),
                
            };
            if (lueCustomerReportState.EditValue != null)
            {
                var report = (CustomerReportState)lueCustomerReportState.EditValue;
                switch (report)
                {
                    case CustomerReportState.NotHandover:
                        input.CustomerReportState = 0;
                        break;
                    case CustomerReportState.Handover:
                        input.CustomerReportState = 1;
                        break;
                    case CustomerReportState.Send:
                        input.CustomerReportState = 2;
                        break;
                    default:
                        break;
                }
            }
            if (lueDateType.EditValue != null)
            {
                var dType = (CustomerReportDateType)lueDateType.EditValue;
                switch (dType)
                {
                    case CustomerReportDateType.Register:
                        input.StartRegisterDate = deStart.DateTime.Date;
                        input.EndRegisterDate = deEnd.DateTime.Date;
                        break;
                    case CustomerReportDateType.Handover:
                        input.StartHandoverDate = deStart.DateTime.Date;
                        input.EndHandoverDate = deEnd.DateTime.Date;
                        break;
                    case CustomerReportDateType.Send:
                        input.StartSendDate = deStart.DateTime.Date;
                        input.EndSendDate = deEnd.DateTime.Date;
                        break;
                    default:
                        break;
                }
            }
            if (lueMemberType.EditValue != null)
            {
                var mType = (CustomerReportMemberType)lueMemberType.EditValue;
                switch (mType)
                {
                    case CustomerReportMemberType.Handover:
                        input.Handover = teMember.Text.Trim();
                        break;
                    case CustomerReportMemberType.Receiptor:
                        input.Receiptor = teMember.Text.Trim();
                        break;
                    case CustomerReportMemberType.Sender:
                        input.Sender = teMember.Text.Trim();
                        break;
                    case CustomerReportMemberType.Receiver:
                        input.Receiver = teMember.Text.Trim();
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(sleDW.EditValue?.ToString()))
            {
                input.ClientRegId = (Guid)sleDW.EditValue;
            }
            AutoLoading(() =>
            {
                var output = _service.Query(input);
                gridControl.DataSource = output;
            });
        }

        #endregion

        #region 内部
        /// <summary>
        /// 报告状态
        /// </summary>
        private enum CustomerReportState
        {
            /// <summary>
            /// 未交接
            /// </summary>
            [Description("未交接")]
            NotHandover,
            /// <summary>
            /// 已交接
            /// </summary>
            [Description("已交接")]
            Handover,
            /// <summary>
            /// 已发放
            /// </summary>
            [Description("已发放")]
            Send
        }
        /// <summary>
        /// 时间类别
        /// </summary>
        private enum CustomerReportDateType
        {
            /// <summary>
            /// 登记时间
            /// </summary>
            [Description("登记时间")]
            Register,
            /// <summary>
            /// 交接时间
            /// </summary>
            [Description("交接时间")]
            Handover,
            /// <summary>
            /// 发放时间
            /// </summary>
            [Description("发放时间")]
            Send,
        }
        /// <summary>
        /// 人员类别
        /// </summary>
        private enum CustomerReportMemberType
        {
            /// <summary>
            /// 交接人
            /// </summary>
            [Description("交接人")]
            Handover,
            /// <summary>
            /// 接收人
            /// </summary>
            [Description("接收人")]
            Receiptor,
            /// <summary>
            /// 发单人
            /// </summary>
            [Description("发单人")]
            Sender,
            /// <summary>
            /// 领单人
            /// </summary>
            [Description("领单人")]
            Receiver,
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ExcelHelper.GridViewToExcel(gridView, "报告单状态", "报告单状态");

        }

        private void sleDW_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
