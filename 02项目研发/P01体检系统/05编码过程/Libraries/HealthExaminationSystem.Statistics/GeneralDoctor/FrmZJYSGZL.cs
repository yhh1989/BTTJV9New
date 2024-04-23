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
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.GeneralDoctor
{
    public partial class FrmZJYSGZL : UserBaseForm
    {
        public FrmZJYSGZL()
        {
            InitializeComponent();
        }
        #region 全局变量
        /// <summary>
        /// 总检统计
        /// </summary>
        InspectionTotalAppService _proxy = new InspectionTotalAppService();
        /// <summary>
        /// 
        /// </summary>
        InvoiceManagementAppService _invocieService = new InvoiceManagementAppService();
        /// <summary>
        /// 获取时间
        /// </summary>
        CommonAppService _commonProxy = new CommonAppService();
        #endregion


        /// <summary>
        /// 统计查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            try
            {
                //查询
                TjlCustomerQuery query = new TjlCustomerQuery();
                if (dtpStart.EditValue != null)
                {
                    //query.BeginDate = Convert.ToDateTime(dtpStart.EditValue.ToString());
                    query.BeginDate = dtpStart.DateTime.Date;
                }

                if (dtpEnd.EditValue != null)
                {
                    //query.EndDate = Convert.ToDateTime(dtpEnd.EditValue.ToString());
                    query.EndDate = dtpEnd.DateTime.Date.AddDays(1);
                }

                //用户Ids
                string[] arrIds = cbo_yonghu.Properties.GetCheckedItems().ToString().Split(',');
                if (arrIds.Length > 0 && string.IsNullOrEmpty(arrIds[0]) == false)
                    query.arrEmployeeName_Id = Array.ConvertAll<string, long>(arrIds, long.Parse);
                if (!string.IsNullOrEmpty(comEmployeeNameType.Text?.ToString()) && comEmployeeNameType.Text.ToString().Contains("总检"))
                {
                    query.EmployeeNameType = 1;
                }


                List<InspectionTotalStatisticsDto> gzlList = _proxy.InspectionTotalStatistics(query);
                gc.DataSource = gzlList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmZJYSGZL_Load(object sender, EventArgs e)
        {
            //var userList = _invocieService.GetUser();
            var userList = DefinedCacheHelper.GetComboUsers();
            cbo_yonghu.Properties.DisplayMember = "Name";
            cbo_yonghu.Properties.ValueMember = "Id";
            cbo_yonghu.Properties.DataSource = userList;
            cbo_yonghu.SetEditValue(CurrentUser.Id);
            dtpStart.DateTime = _commonProxy.GetDateTimeNow().Now;
            dtpEnd.DateTime = _commonProxy.GetDateTimeNow().Now;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            gv.CustomExport("总检医生工作量统计结果");
        }

        private void cbo_yonghu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                cbo_yonghu.EditValue = null;
                cbo_yonghu.RefreshEditValue();
            }
        }
    }
}