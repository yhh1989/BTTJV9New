using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;

namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    public partial class ScheduleOfGrid : UserBaseForm
    {
        /// <summary>
        /// 排期应用服务
        /// </summary>
        private readonly ISchedulingAppService _schedulingAppService;

        /// <summary>
        /// 排期编辑器
        /// </summary>
        private ScheduleEditorOfGrid _scheduleEditorOfGrid;

        /// <summary>
        /// 公共应用服务
        /// </summary>
        private readonly ICommonAppService _commonAppService;

        public ScheduleOfGrid()
        {
            InitializeComponent();

            _schedulingAppService = new SchedulingAppService();
            _commonAppService = new CommonAppService();
        }

        private void ScheduleOfGrid_Load(object sender, EventArgs e)
        {
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditOrientation.DateTime = date;
            dateEditOrientation.Properties.MinValue = date.Date;
            InitialData();
        }

        private void InitialData()
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);

            var data = _schedulingAppService.GetAllListScheduling();
            gridControl.DataSource = data;
        }

        private void SetMemoEditSynthesize(DateTime date)
        {
            AutoLoading(() =>
            {
                var data = _schedulingAppService.GetSchedulingByDate(new SearchSchedulingNewDto { Date = date });
                var sb = new StringBuilder();
                sb.AppendLine(date.ToString("D"));
                if (data.Count != 0)
                {
                    sb.AppendLine($"总共{data.Sum(r => r.TotalNumber)}人");
                    var am = data.Where(r => r.TimeFrame != "下午").ToList();
                    if (am.Count != 0)
                    {
                        sb.AppendLine("==================");
                        //sb.AppendLine("上午：");
                        var clients = am.Where(r => r.IsTeam).ToList();
                        if (clients.Count != 0)
                        {
                            //sb.AppendLine("-------------------------");
                            sb.AppendLine("单位：");
                            foreach (var client in clients)
                            {
                                sb.AppendLine($"{client.ClientInfo?.ClientName}：{client.TotalNumber}人");
                            }
                        }
                        var personals = am.Where(r => !r.IsTeam).ToList();
                        if (personals.Count != 0)
                        {
                            sb.AppendLine("-------------------------");
                            sb.AppendLine("个人：");
                            foreach (var personal in personals)
                            {
                                sb.AppendLine($"{personal.PersonalName}：{personal.TotalNumber}人");
                            }
                        }
                    }
                    var pm = data.Where(r => r.TimeFrame == "下午").ToList();
                    if (pm.Count != 0)
                    {
                        sb.AppendLine("==================");
                        sb.AppendLine("下午：");
                        var clients = pm.Where(r => r.IsTeam).ToList();
                        if (clients.Count != 0)
                        {
                            sb.AppendLine("-------------------------");
                            sb.AppendLine("单位：");
                            foreach (var client in clients)
                            {
                                sb.AppendLine($"{client.ClientInfo?.ClientName}：{client.TotalNumber}人");
                            }
                        }
                        var personals = pm.Where(r => !r.IsTeam).ToList();
                        if (personals.Count != 0)
                        {
                            sb.AppendLine("-------------------------");
                            sb.AppendLine("个人：");
                            foreach (var personal in personals)
                            {
                                sb.AppendLine($"{personal.PersonalName}：{personal.TotalNumber}人");
                            }
                        }
                    }


                    var itemGroupAndPersonals = new List<ItemGroupAndPersonal>();
                    foreach (var dto in data)
                    {
                        foreach (var itemGroup in dto.ItemGroups)
                        {
                            var item = itemGroupAndPersonals.Find(r => r.ItemGroup.Id == itemGroup.Id);
                            if (item == null)
                            {
                                var itemGroupAndPersonal = new ItemGroupAndPersonal
                                {
                                    ItemGroup = itemGroup,
                                    Personal = dto.TotalNumber
                                };
                                itemGroupAndPersonals.Add(itemGroupAndPersonal);
                            }
                            else
                            {
                                item.Personal += dto.TotalNumber;
                            }
                        }
                    }

                    if (itemGroupAndPersonals.Count != 0)
                    {
                        sb.AppendLine("==================");
                        sb.AppendLine("项目：");
                        foreach (var itemGroupAndPersonal in itemGroupAndPersonals)
                        {
                            sb.AppendLine($"{itemGroupAndPersonal.ItemGroup.ItemGroupName}：{itemGroupAndPersonal.Personal}人");
                        }
                    }
                }
                memoEditSynthesize.Text = sb.ToString();
            });
        }

        private void layoutViewScheduling_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (layoutViewScheduling.GetRow(e.FocusedRowHandle) is SchedulingNewDto scheduling)
            {
                gridControlItemGroup.DataSource = scheduling.ItemGroups;
                SetMemoEditSynthesize(scheduling.ScheduleDate);
                dateEditOrientation.DateTime = scheduling.ScheduleDate;
            }
        }

        private void simpleButtonOrientation_Click(object sender, EventArgs e)
        {
            var date = dateEditOrientation.DateTime.Date;
            if (gridControl.DataSource is List<SchedulingNewDto> data)
            {
                var index = data.FindIndex(r => r.ScheduleDate.Date == date);
                if (index == -1)
                {
                    alertControl.Show(this, "提示", $"定位当天没有任何排期！{Environment.NewLine}点击新建按钮添加排期！");
                }
                else
                {
                    layoutViewScheduling.FocusedRowHandle = index;
                }
            }
        }

        private void simpleButtonNew_Click(object sender, EventArgs e)
        {
            if (_scheduleEditorOfGrid != null && !_scheduleEditorOfGrid.IsDisposed)
            {
                alertControl.Show(this, "提示", "已经有打开的编辑器，请先关闭！");
                _scheduleEditorOfGrid.WindowState = FormWindowState.Normal;
                _scheduleEditorOfGrid.Activate();
                return;
            }

            var date = _commonAppService.GetDateTimeNow().Now;
            if (date.Date > dateEditOrientation.DateTime.Date)
            {
                alertControl.Show(this, "提示", "只能在今天之后的日期进行排期！");
                return;
            }

            var date1 = dateEditOrientation.DateTime;
            if (date1.DayOfWeek == DayOfWeek.Saturday || date1.DayOfWeek == DayOfWeek.Sunday)
            {
                if (XtraMessageBox.Show(this, "确定要在星期天排期吗？", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    return;
                }
            }
            GC.Collect();
            _scheduleEditorOfGrid = new ScheduleEditorOfGrid(dateEditOrientation.DateTime.Date);
            _scheduleEditorOfGrid.ScheduleSaved -= ScheduleEditorOfGridScheduleSaved;
            _scheduleEditorOfGrid.ScheduleSaved += ScheduleEditorOfGridScheduleSaved;
            _scheduleEditorOfGrid.ScheduleUpdateComplete -= ScheduleEditorOfGridScheduleSaved;
            _scheduleEditorOfGrid.ScheduleUpdateComplete += ScheduleEditorOfGridScheduleSaved;
            _scheduleEditorOfGrid.Show(this);
        }

        private void ScheduleEditorOfGridScheduleSaved(object sender, ScheduleEventArgs e)
        {
            InitialData();
            if (gridControl.DataSource is List<SchedulingNewDto> data)
            {
                var index = data.FindIndex(r => r.Id == e.Data.Id);
                if (layoutViewScheduling.FocusedRowHandle == index)
                {
                    SetMemoEditSynthesize(e.Data.ScheduleDate);
                }
                else
                {
                    layoutViewScheduling.FocusedRowHandle = index;
                }
            }
        }

        private void simpleButtonEdit_Click(object sender, EventArgs e)
        {
            if (layoutViewScheduling.GetFocusedRow() is SchedulingNewDto scheduling)
            {
                if (_scheduleEditorOfGrid != null && !_scheduleEditorOfGrid.IsDisposed)
                {
                    alertControl.Show(this, "提示", "已经有打开的编辑器，请先关闭！");
                    _scheduleEditorOfGrid.WindowState = FormWindowState.Normal;
                    _scheduleEditorOfGrid.Activate();
                    return;
                }

                GC.Collect();
                _scheduleEditorOfGrid = new ScheduleEditorOfGrid(scheduling.ScheduleDate, scheduling.Id);
                _scheduleEditorOfGrid.ScheduleSaved -= ScheduleEditorOfGridScheduleSaved;
                _scheduleEditorOfGrid.ScheduleSaved += ScheduleEditorOfGridScheduleSaved;
                _scheduleEditorOfGrid.ScheduleUpdateComplete -= ScheduleEditorOfGridScheduleSaved;
                _scheduleEditorOfGrid.ScheduleUpdateComplete += ScheduleEditorOfGridScheduleSaved;
                _scheduleEditorOfGrid.Show(this);
            }
        }

        private void layoutViewScheduling_CardClick(object sender, DevExpress.XtraGrid.Views.Layout.Events.CardClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                simpleButtonEdit.PerformClick();
            }
        }

        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            if (layoutViewScheduling.GetFocusedRow() is SchedulingNewDto scheduling)
            {
                var name = scheduling.IsTeam ? scheduling.ClientInfo.ClientName : scheduling.PersonalName;
                var count = scheduling.ItemGroups.Count;
                var number = scheduling.TotalNumber;
                if (XtraMessageBox.Show(this, $"已选择【{name}】数据进行删除，{Environment.NewLine}日期：{scheduling.ScheduleDate:D}，{Environment.NewLine}包含{count}条项目，{Environment.NewLine}共{number}个人员，{Environment.NewLine}点击确定继续。", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _schedulingAppService.DeleteScheduling(new EntityDto<Guid> { Id = scheduling.Id });
                    memoEditSynthesize.ResetText();
                    InitialData();
                    dateEditOrientation.DateTime = scheduling.ScheduleDate;
                    simpleButtonOrientation.PerformClick();
                }
            }
        }

        private void simpleButtonLast_Click(object sender, EventArgs e)
        {
            var date = dateEditOrientation.DateTime.Date.AddDays(-1);
            if (gridControl.DataSource is List<SchedulingNewDto> data)
            {
                if (data.Count == 0)
                {
                    alertControl.Show(this, "提示", "没有任何排期数据！");
                    return;
                }
                var index = data.FindIndex(r => r.ScheduleDate.Date == date);
                if (index == -1)
                {
                    var lastDate = date.AddDays(-1);
                    var minDate = data.Min(r => r.ScheduleDate);
                    if (lastDate < minDate.Date)
                    {
                        alertControl.Show(this, "提示", "已经找到第一个排期了！");
                        return;
                    }

                    dateEditOrientation.DateTime = date;
                    simpleButtonLast.PerformClick();
                }
                else
                {
                    layoutViewScheduling.FocusedRowHandle = index;
                }
            }
        }

        private void simpleButtonNext_Click(object sender, EventArgs e)
        {
            var date = dateEditOrientation.DateTime.Date.AddDays(1);
            if (gridControl.DataSource is List<SchedulingNewDto> data)
            {
                if (data.Count == 0)
                {
                    alertControl.Show(this, "提示", "没有任何排期数据！");
                    return;
                }
                var index = data.FindIndex(r => r.ScheduleDate.Date == date);
                if (index == -1)
                {
                    var nextDate = date.AddDays(1);
                    var maxDate = data.Max(r => r.ScheduleDate);
                    if (nextDate > maxDate.Date)
                    {
                        alertControl.Show(this, "提示", "已经找到最后一个排期了！");
                        return;
                    }

                    dateEditOrientation.DateTime = date;
                    simpleButtonNext.PerformClick();
                }
                else
                {
                    layoutViewScheduling.FocusedRowHandle = index;
                }
            }
        }
    }
}