using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Native;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.SchedulingSecondEdition
{
    /// <summary>
    /// 预约日历
    /// </summary>
    public partial class FormAppointmentCalendar : UserBaseForm
    {
        /// <summary>
        /// 初始化“预约日历”
        /// </summary>
        public FormAppointmentCalendar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// “预约日历”编辑预约窗体显示事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void schedulerControlYYRL预约日历_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            //SchedulerControl scheduler = ((SchedulerControl)(sender));
            //CustomAppointmentForm form = new CustomAppointmentForm(scheduler, e.Appointment, e.OpenRecurrenceForm);
            //try
            //{
            //    e.DialogResult = form.ShowDialog();
            //    e.Handled = true;
            //}
            //finally
            //{
            //    form.Dispose();
            //}
            if (schedulerStorageYYRL预约日历.Appointments.Contains(e.Appointment) && e.Appointment.CustomFields["Obj"] == null)
            {
                e.Handled = true;
                return;
            }
            using (var frm = new FormAppointmentEditor(schedulerControlYYRL预约日历, schedulerStorageYYRL预约日历, e.Appointment))
            {
                e.DialogResult = frm.ShowDialog(this);
                e.Handled = true;
            }
        }

        /// <summary>
        /// “新建预约”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonXJYY新建预约_Click(object sender, EventArgs e)
        {
            schedulerControlYYRL预约日历.CreateNewAppointment();
        }

        /// <summary>
        /// “日或月视图”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRHYST日或月视图_Click(object sender, EventArgs e)
        {
            if (schedulerControlYYRL预约日历.ActiveViewType == SchedulerViewType.Month)
            {
                schedulerControlYYRL预约日历.ActiveViewType = SchedulerViewType.Day;
            }
            else
            {
                schedulerControlYYRL预约日历.ActiveViewType = SchedulerViewType.Month;
            }
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAppointmentCalendar_Load(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            schedulerControlYYRL预约日历.LimitInterval.Start = today;
            schedulerControlYYRL预约日历.Start = today;
            dateNavigatorYYRL预约日历.DateTime = today;
            schedulerControlYYRL预约日历.ActiveViewType = SchedulerViewType.Month;
        }

        /// <summary>
        /// “预约日历”右键菜单显示之前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void schedulerControlYYRL预约日历_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            // 取消右键菜单
            e.Menu.Items.Clear();
        }

        /// <summary>
        /// “编辑预约”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonBJYY编辑预约_Click(object sender, EventArgs e)
        {
            if (schedulerControlYYRL预约日历.SelectedAppointments.Count != 0)
            {
                var apt = schedulerControlYYRL预约日历.SelectedAppointments[0];
                schedulerControlYYRL预约日历.ShowEditAppointmentForm(apt);
            }
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAppointmentCalendar_Shown(object sender, EventArgs e)
        {
            LoadScheduler();
        }

        /// <summary>
        /// 加载行程
        /// </summary>
        private void LoadScheduler()
        {
            AutoLoading(() =>
            {
                schedulerControlYYRL预约日历.BeginUpdate();
                foreach (CheckedListBoxItem item in checkedListBoxControlRLLB日历列表.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (item.Value.Equals(1) && item.Tag == null)
                        {
                            LoadManualScheduling();
                        }
                        else if (item.Value.Equals(2) && item.Tag == null)
                        {
                            LoadCompanyScheduling();
                        }
                        else if (item.Value.Equals(3) && item.Tag == null)
                        {
                            LoadPersonScheduling();
                        }
                        item.Tag = 1;
                    }
                    else
                    {
                        item.Tag = null;
                        if (item.Value.Equals(1))
                        {
                            ClearManualScheduling();
                        }
                        else if (item.Value.Equals(2))
                        {
                            ClearCompanyScheduling();
                        }
                        else if (item.Value.Equals(3))
                        {
                            ClearPersonScheduling();
                        }
                    }
                }
                schedulerControlYYRL预约日历.EndUpdate();
            });
        }

        /// <summary>
        /// “日历列表”项目点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxControlRLLB日历列表_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            LoadScheduler();
        }

        /// <summary>
        /// 加载人工行程安排
        /// </summary>
        private void LoadManualScheduling()
        {
            var data = DefinedCacheHelper.DefinedApiProxy.ManualSchedulingAppService.QueryManualSchedulingList().Result;
            foreach (var manualScheduling in data)
            {
                var appointment = schedulerStorageYYRL预约日历.CreateAppointment(AppointmentType.Normal);
                appointment.Subject = manualScheduling.GetName();
                appointment.Start = manualScheduling.SchedulingDate;
                appointment.End = manualScheduling.SchedulingDate.AddDays(1);
                appointment.Location = $"{manualScheduling.NumberOfCustomer} 人";
                appointment.LabelKey = 2;
                appointment.StatusKey = 2;
                appointment.Description = manualScheduling.GetDescription();
                appointment.CustomFields["Obj"] = manualScheduling;
                schedulerStorageYYRL预约日历.Appointments.Add(appointment);
            }
        }

        /// <summary>
        /// 清空人工行程安排
        /// </summary>
        private void ClearManualScheduling()
        {
            var list = new List<Appointment>();
            for (var i = 0; i < schedulerStorageYYRL预约日历.Appointments.Count; i++)
            {
                var appointment = schedulerStorageYYRL预约日历.Appointments[i];
                if (appointment.LabelKey.Equals(2))
                {
                    list.Add(appointment);
                }
            }
            foreach (var appointment in list)
            {
                schedulerStorageYYRL预约日历.Appointments.Remove(appointment);
            }
        }

        /// <summary>
        /// 加载个人行程安排
        /// </summary>
        private void LoadPersonScheduling()
        {
            var data = DefinedCacheHelper.DefinedApiProxy.ManualSchedulingAppService.QueryPersonSchedulingList().Result;
            foreach (var manualScheduling in data)
            {
                var appointment = schedulerStorageYYRL预约日历.CreateAppointment(AppointmentType.Normal);
                appointment.Subject = manualScheduling.GetName();
                appointment.Start = manualScheduling.SchedulingDate;
                appointment.End = manualScheduling.SchedulingDate.AddDays(1);
                appointment.Location = $"{manualScheduling.NumberOfCustomer} 人";
                appointment.LabelKey = 3;
                appointment.StatusKey = 3;
                appointment.Description = manualScheduling.GetDescription();
                schedulerStorageYYRL预约日历.Appointments.Add(appointment);
            }
        }

        /// <summary>
        /// 清空个人行程安排
        /// </summary>
        private void ClearPersonScheduling()
        {
            var list = new List<Appointment>();
            for (var i = 0; i < schedulerStorageYYRL预约日历.Appointments.Count; i++)
            {
                var appointment = schedulerStorageYYRL预约日历.Appointments[i];
                if (appointment.LabelKey.Equals(3))
                {
                    list.Add(appointment);
                }
            }
            foreach (var appointment in list)
            {
                schedulerStorageYYRL预约日历.Appointments.Remove(appointment);
            }
        }

        /// <summary>
        /// 加载公司行程安排
        /// </summary>
        private void LoadCompanyScheduling()
        {
            var data = DefinedCacheHelper.DefinedApiProxy.ManualSchedulingAppService.QueryCompanySchedulingList().Result;
            foreach (var manualScheduling in data)
            {
                var appointment = schedulerStorageYYRL预约日历.CreateAppointment(AppointmentType.Normal);
                appointment.Subject = manualScheduling.GetName();
                appointment.Start = manualScheduling.SchedulingDate;
                appointment.End = manualScheduling.SchedulingDate.AddDays(1);
                appointment.Location = $"{manualScheduling.NumberOfCustomer} 人";
                appointment.LabelKey = 4;
                appointment.StatusKey = 4;
                appointment.Description = manualScheduling.GetDescription();
                schedulerStorageYYRL预约日历.Appointments.Add(appointment);
            }
        }

        /// <summary>
        /// 清空公司行程安排
        /// </summary>
        private void ClearCompanyScheduling()
        {
            var list = new List<Appointment>();
            for (var i = 0; i < schedulerStorageYYRL预约日历.Appointments.Count; i++)
            {
                var appointment = schedulerStorageYYRL预约日历.Appointments[i];
                if (appointment.LabelKey.Equals(4))
                {
                    list.Add(appointment);
                }
            }
            foreach (var appointment in list)
            {
                schedulerStorageYYRL预约日历.Appointments.Remove(appointment);
            }
        }

        private void toolTipControllerYYRL预约日历_BeforeShow(object sender, DevExpress.Utils.ToolTipControllerShowEventArgs e)
        {
            if (e.SelectedObject is AppointmentViewInfo avi)
            {
                e.SuperTip.Items.Clear();
                e.SuperTip.Items.AddTitle(avi.Appointment.Subject);
                e.SuperTip.Items.Add(avi.Appointment.Location);
                if (!string.IsNullOrWhiteSpace(avi.Appointment.Description))
                {
                    var descriptionList = avi.Appointment.Description.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in descriptionList)
                    {
                        if (item.StartsWith("Title"))
                        {
                            e.SuperTip.Items.AddSeparator();
                            e.SuperTip.Items.AddTitle(item.Remove(0, 5));
                        }
                        else
                        {
                            e.SuperTip.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void schedulerControlYYRL预约日历_VisibleIntervalChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSX刷新_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                schedulerControlYYRL预约日历.BeginUpdate();
                foreach (CheckedListBoxItem item in checkedListBoxControlRLLB日历列表.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (item.Value.Equals(1))
                        {
                            ClearManualScheduling();
                            LoadManualScheduling();
                        }
                        else if (item.Value.Equals(2))
                        {
                            ClearCompanyScheduling();
                            LoadCompanyScheduling();
                        }
                        else if (item.Value.Equals(3))
                        {
                            ClearPersonScheduling();
                            LoadPersonScheduling();
                        }
                    }
                }
                schedulerControlYYRL预约日历.EndUpdate();
            });
        }

        private void dateNavigatorYYRL预约日历_DateTimeChanged(object sender, EventArgs e)
        {
            
        }

        private void dateNavigatorYYRL预约日历_DateTimeCommit(object sender, EventArgs e)
        {

        }
    }
}
