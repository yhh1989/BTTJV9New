using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.UI;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.SchedulingSecondEdition
{
    /// <summary>
    /// 自定义预约编辑器
    /// </summary>
    public partial class FormAppointmentEditor : UserBaseForm
    {
        /// <summary>
        /// 排期
        /// </summary>
        private readonly SchedulerControl _schedulerControl;

        /// <summary>
        /// 排期存储
        /// </summary>
        private readonly SchedulerStorage _schedulerStorage;

        /// <summary>
        /// 预约
        /// </summary>
        private readonly Appointment _appointment;

        /// <summary>
        /// 控制器
        /// </summary>
        private readonly AppointmentFormController _controller;

        /// <summary>
        /// 当前标识
        /// </summary>
        private Guid _currentId;

        /// <summary>
        /// 初始化“自定义预约编辑器”
        /// </summary>
        private FormAppointmentEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化“自定义预约编辑器”
        /// </summary>
        /// <param name="schedulerControl">排期控件</param>
        /// <param name="schedulerStorage">排期存储</param>
        /// <param name="appointment">预约</param>
        public FormAppointmentEditor(SchedulerControl schedulerControl, SchedulerStorage schedulerStorage, Appointment appointment) : this()
        {
            Guard.ArgumentNotNull(schedulerControl, "control");
            Guard.ArgumentNotNull(schedulerStorage, "control.DataStorage");
            Guard.ArgumentNotNull(appointment, "apt");

            _schedulerControl = schedulerControl;
            _schedulerStorage = schedulerStorage;
            _appointment = appointment;
            _controller = CreateController(schedulerControl, appointment);
        }

        /// <summary>
        /// 创建控制器
        /// </summary>
        /// <param name="control"></param>
        /// <param name="apt"></param>
        /// <returns></returns>
        private AppointmentFormController CreateController(SchedulerControl control, Appointment apt)
        {
            return new AppointmentFormController(control, apt);
        }

        /// <summary>
        /// “保存”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonBC保存_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();

            if (searchLookUpEditGS公司.EditValue is Guid companyId)
            {

            }
            else if (string.IsNullOrWhiteSpace(textEditMC名称.Text))
            {
                dxErrorProvider.SetError(textEditMC名称, "请填写名称或选择一个单位。");
            }

            if (spinEditRS人数.Value <= 0)
            {
                dxErrorProvider.SetError(spinEditRS人数, "请填写体检人数。");
            }

            if (dxErrorProvider.HasErrors)
            {
                return;
            }

            ManualSchedulingDtoNo1 result;
            if (_controller.IsNewAppointment)
            {
                var data = GetData();

                result = DefinedCacheHelper.DefinedApiProxy.ManualSchedulingAppService.InsertManualScheduling(data).Result;
            }
            else
            {
                var data = GetData();
                data.Id = _currentId;
                result = DefinedCacheHelper.DefinedApiProxy.ManualSchedulingAppService.UpdateManualScheduling(data).Result;
            }

            _controller.Subject = result.GetName();
            _controller.Location = $"{result.NumberOfCustomer} 人";
            _controller.LabelKey = 2;
            _controller.StatusKey = 2;
            _controller.Description = result.GetDescription();
            _controller.EditedAppointmentCopy.CustomFields["Obj"] = result;
            _controller.ApplyChanges();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 窗体正在加载方法重写
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_controller == null)
                return;
            this.DataBindings.Add("Text", _controller, "Caption");
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAppointmentEditor_Shown(object sender, EventArgs e)
        {
            if (_controller.EditedAppointmentCopy.CustomFields["Obj"] is ManualSchedulingDtoNo1 row)
            {
                _currentId = row.Id;
                if (row.CompanyId.HasValue)
                {
                    searchLookUpEditGS公司.EditValue = row.CompanyId.Value;
                }
                else
                {
                    textEditMC名称.Text = row.Name;
                }

                dateEditRQ日期.DateTime = row.SchedulingDate;
                spinEditRS人数.Value = row.NumberOfCustomer;
                var ids = row.DepartmentCollection.Select(r => r.Id).ToList();
                if (checkedListBoxControlKSLB科室列表.DataSource is List<TbmDepartmentDto> rows)
                {
                    foreach (var item in rows)
                    {
                        if (ids.Contains(item.Id))
                        {
                            var index = checkedListBoxControlKSLB科室列表.FindItem(item);
                            checkedListBoxControlKSLB科室列表.SetItemChecked(index, true);
                        }
                    }
                }
            }
            else
            {
                dateEditRQ日期.DateTime = _controller.Start;
            }
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAppointmentEditor_Load(object sender, EventArgs e)
        {
            checkedListBoxControlKSLB科室列表.DataSource = DefinedCacheHelper.GetDepartments();
            searchLookUpEditGS公司.Properties.DataSource =
                DefinedCacheHelper.DefinedApiProxy.ClientInfoesAppService.GetAll().ToList();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private ManualSchedulingDtoNo2 GetData()
        {
            var data = new ManualSchedulingDtoNo2();
            if (!string.IsNullOrWhiteSpace(textEditMC名称.Text))
            {
                data.Name = textEditMC名称.Text;
            }

            if (searchLookUpEditGS公司.EditValue is Guid companyIdNo1)
            {
                data.CompanyId = companyIdNo1;
            }

            data.SchedulingDate = _controller.Start;
            data.NumberOfCustomer = (int)spinEditRS人数.Value;
            foreach (var selectedItem in checkedListBoxControlKSLB科室列表.CheckedItems)
            {
                if (selectedItem is TbmDepartmentDto dept)
                {
                    if (data.DepartmentIdList == null)
                    {
                        data.DepartmentIdList = new List<Guid>();
                    }
                    data.DepartmentIdList.Add(dept.Id);
                }
            }

            return data;
        }

        private void searchLookUpEditGS公司_Popup(object sender, EventArgs e)
        {
            searchLookUpEditGS公司View.BestFitColumns();
        }
    }
}
