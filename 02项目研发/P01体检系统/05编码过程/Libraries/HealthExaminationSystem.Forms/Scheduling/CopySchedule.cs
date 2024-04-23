using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    public partial class CopySchedule : UserBaseForm
    {
        /// <summary>
        /// 公共应用服务
        /// </summary>
        private readonly ICommonAppService _commonAppService;

        /// <summary>
        /// 排期应用服务
        /// </summary>
        private readonly ISchedulingAppService _schedulingAppService;

        private DateTime _date;

        public CopySchedule(DateTime date)
        {
            InitializeComponent();

            _commonAppService = new CommonAppService();
            _schedulingAppService = new SchedulingAppService();
            _date = date;
        }

        private void CopySchedule_Load(object sender, EventArgs e)
        {
            dateEditMonth.DateTime = _date;
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            var month = dateEditMonth.DateTime;
            var isTeam = (bool)radioGroupIsTeam.EditValue;
            AutoLoading(() =>
            {
                var input = new SearchSchedulingNewDtoForMonth { Date = month, IsTeam = isTeam };
                var data = _schedulingAppService.GetSchedulingByMonth(input);
                gridControl.DataSource = data;
            });
        }

        private void radioGroupIsTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isTeam = (bool)radioGroupIsTeam.EditValue;
            if (isTeam)
            {
                gridColumnSchedulingClientInfo.Visible = true;
                gridColumnSchedulingClientInfo.VisibleIndex = 0;
                gridColumnSchedulingPersonalName.Visible = false;
            }
            else
            {
                gridColumnSchedulingPersonalName.Visible = true;
                gridColumnSchedulingPersonalName.VisibleIndex = 0;
                gridColumnSchedulingClientInfo.Visible = false;
            }
            simpleButtonQuery.PerformClick();
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            var row = gridViewScheduling.GetFocusedRow() as SchedulingNewDto;
            if (row == null)
            {
                ShowMessageBoxWarning("请先选择数据！");
                return;
            }

            var name = row.IsTeam ? row.ClientInfo.ClientName : row.PersonalName;
            var count = row.ItemGroups?.Count ?? 0;
            if (XtraMessageBox.Show(this, $"已选择【{name}】数据进行复制，{Environment.NewLine}包含{count}条项目，{Environment.NewLine}点击确定继续。", "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                SelectSchedulingComplete?.Invoke(this, new ScheduleEventArgs(row));
                DialogResult = DialogResult.OK;
            }
        }

        public event EventHandler<ScheduleEventArgs> SelectSchedulingComplete;

        private void dateEditMonth_DateTimeChanged(object sender, EventArgs e)
        {
            simpleButtonQuery.PerformClick();
        }
    }
}