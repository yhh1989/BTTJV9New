using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Internal.Implementations;
using NPOI.SS.Formula.Functions;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Company;

namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    public partial class ScheduleEditorOfGrid : UserBaseForm
    {
        private readonly BindingList<ItemGroupOfScheduleDto> _itemGroupOfScheduleDto;

        private readonly ISchedulingAppService _schedulingAppService;

        private readonly BindingList<ItemGroupOfScheduleDto> _selectItemGroupOfScheduleDto;

        private DateTime _date;

        private readonly Guid? _id;

        /// <summary>
        /// 公共应用服务
        /// </summary>
        private readonly ICommonAppService _commonAppService;

        public ScheduleEditorOfGrid(DateTime date, Guid? id = null)
        {
            InitializeComponent();

            _date = date;
            _id = id;

            _schedulingAppService = new SchedulingAppService();

            _selectItemGroupOfScheduleDto = new BindingList<ItemGroupOfScheduleDto>();
            _itemGroupOfScheduleDto = new BindingList<ItemGroupOfScheduleDto>();
            _commonAppService = new CommonAppService();
        }

        private void ScheduleEditor_Load(object sender, EventArgs e)
        {
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditScheduleDate.DateTime = date;
            dateEditScheduleDate.Properties.MinValue = date.Date;
            gridControlSelectItemGroupOfScheduleDto.DataSource = _selectItemGroupOfScheduleDto;
            gridControlItemGroupOfScheduleDto.DataSource = _itemGroupOfScheduleDto;
            InitialCompany();
            InitialItemGroup();
            InitialAppointment();
        }

        /// <summary>
        /// 绑定单位下拉框
        /// </summary>
        private void InitialCompany()
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.SetWaitFormDescription("正在加载单位数据...");
            searchLookUpEditClient.Properties.DataSource = _schedulingAppService.GetAllListOfClientInfo();
        }

        private void InitialItemGroup()
        {
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.SetWaitFormDescription("正在加载项目数据...");
            var data = _schedulingAppService.GetAllListOfItemGroup();
            data.ForEach(r =>
            {
                _itemGroupOfScheduleDto.Add(r);
            });
        }

        private void InitialAppointment()
        {
            if (_id.HasValue)
            {
                // 恢复已有数据到界面
                var row = _schedulingAppService.GetSchedulingById(new EntityDto<Guid> { Id = _id.Value });
                radioGroupType.EditValue = row.IsTeam;
                dateEditScheduleDate.DateTime = row.ScheduleDate;
                searchLookUpEditClient.EditValue = row.ClientInfoId;
                textEditPersonalName.Text = row.PersonalName;
                textEditTotalNumber.Text = row.TotalNumber.ToString();
                textEditIntroducer.Text = row.Introducer;
                radioGroupTimeFrame.EditValue = string.IsNullOrWhiteSpace(row.TimeFrame) ? "全天" : row.TimeFrame;
                row.ItemGroups.ForEach(r =>
                {
                    _selectItemGroupOfScheduleDto.Add(r);
                });
                memoEditRemarks.Text = row.Remarks;
            }
            else
            {
                // 初始化新的数据
                dateEditScheduleDate.DateTime = _date;
            }
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var date = dateEditScheduleDate.DateTime;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                if (XtraMessageBox.Show(this, "确定要在星期天排期吗？", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    return;
                }
            }

            if (SaveDate())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public event EventHandler<ScheduleEventArgs> ScheduleSaved;

        public event EventHandler<ScheduleEventArgs> ScheduleUpdateComplete;

        private void radioGroupType_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroupType.EditValue.Equals(true))
            {
                navigationFrameCompanyOrPersonal.SelectedPage = navigationPageCompany;
                searchLookUpEditClient.Enabled = true;
                textEditPersonalName.Enabled = false;
                layoutControlItemCompanyOrPersonal.Text = @"单位：";
            }
            else if (radioGroupType.EditValue.Equals(false))
            {
                navigationFrameCompanyOrPersonal.SelectedPage = navigationPagePersonal;
                searchLookUpEditClient.Enabled = false;
                textEditPersonalName.Enabled = true;
                layoutControlItemCompanyOrPersonal.Text = @"名称：";
            }
        }

        private void gridViewItemGroupOfScheduleDto_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var row = (ItemGroupOfScheduleDto)gridViewItemGroupOfScheduleDto.GetRow(e.RowHandle);
                if (_selectItemGroupOfScheduleDto.All(r => r.Id != row.Id))
                    _selectItemGroupOfScheduleDto.Add(row);
            }
        }

        private void gridViewSelectItemGroupOfScheduleDto_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var row = (ItemGroupOfScheduleDto)gridViewSelectItemGroupOfScheduleDto.GetRow(e.RowHandle);
                _selectItemGroupOfScheduleDto.Remove(row);
            }
        }

        private void simpleButtonSaveNext_Click(object sender, EventArgs e)
        {
            var date = dateEditScheduleDate.DateTime;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                if (XtraMessageBox.Show(this, "确定要在星期天排期吗？", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    return;
                }
            }
            SaveDate();
        }

        private bool SaveDate()
        {
            dxErrorProvider.ClearErrors();
            if (radioGroupType.EditValue.Equals(true))
            {
                if (searchLookUpEditClient.EditValue == null || searchLookUpEditClient.EditValue.Equals(string.Empty))
                {
                    dxErrorProvider.SetError(searchLookUpEditClient, string.Format(Variables.MandatoryTips, "单位"));
                    return false;
                }
            }
            else if (radioGroupType.EditValue.Equals(false))
            {
                if (string.IsNullOrWhiteSpace(textEditPersonalName.Text.Trim()))
                {
                    dxErrorProvider.SetError(textEditPersonalName, string.Format(Variables.MandatoryTips, "个人"));
                    return false;
                }
            }

            var scheduling = new SchedulingNewDto();
            if (radioGroupType.EditValue.Equals(true))
            {
                scheduling.IsTeam = true;
                scheduling.ClientInfoId = (Guid)searchLookUpEditClient.EditValue;
            }
            else if (radioGroupType.EditValue.Equals(false))
            {
                scheduling.IsTeam = false;
                scheduling.PersonalName = textEditPersonalName.Text.Trim();
            }

            scheduling.ScheduleDate = dateEditScheduleDate.DateTime;
            scheduling.TotalNumber = Convert.ToInt32(textEditTotalNumber.Text);
            scheduling.Introducer = textEditIntroducer.Text.Trim();
            scheduling.ItemGroups = _selectItemGroupOfScheduleDto.ToList();
            scheduling.Remarks = memoEditRemarks.Text.Trim();
            scheduling.TimeFrame = radioGroupTimeFrame.EditValue.ToString();
            if (_id.HasValue)
            {
                // 更新操作
                scheduling.Id = _id.Value;
                var result = _schedulingAppService.UpdateScheduling(scheduling);
                ScheduleUpdateComplete?.Invoke(this, new ScheduleEventArgs(result));
            }
            else
            {
                // 创建操作
                var result = _schedulingAppService.InsertScheduling(scheduling);
                ScheduleSaved?.Invoke(this, new ScheduleEventArgs(result));
            }

            ClearData();
            return true;
        }

        private void ClearData()
        {
            searchLookUpEditClient.EditValue = string.Empty;
            textEditPersonalName.Text = string.Empty;
            textEditTotalNumber.Text = @"0";
            textEditIntroducer.Text = string.Empty;
            _selectItemGroupOfScheduleDto.Clear();
            memoEditRemarks.Text = string.Empty;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButtonCopy_Click(object sender, EventArgs e)
        {
            using (var frm = new CopySchedule(dateEditScheduleDate.DateTime))
            {
                frm.SelectSchedulingComplete += CopyScheduleSelectSchedulingComplete;
                frm.ShowDialog(this);
            }
        }

        private void CopyScheduleSelectSchedulingComplete(object sender, ScheduleEventArgs e)
        {
            if (e.Data.ItemGroups != null)
            {
                foreach (var itemGroup in e.Data.ItemGroups)
                {
                    if (!_selectItemGroupOfScheduleDto.Contains(itemGroup))
                        _selectItemGroupOfScheduleDto.Add(itemGroup);
                }
            }
        }

        private void searchLookUpEditClient_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEditClient.EditValue != null && !searchLookUpEditClient.EditValue.Equals(string.Empty))
            {
                if (searchLookUpEditClient.GetSelectedDataRow() is ClientInfoRegDto company)
                {
                    textEditIntroducer.Text = company.LinkMan;
                }
            }
        }

        private void searchLookUpEditClient_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                using (var frm = new FrmEditTjlClientInfoes())
                {
                    frm.CreateCompanyComplete += id =>
                    {
                        InitialCompany();
                        searchLookUpEditClient.EditValue = id;
                    };
                    frm.ShowDialog(this);
                }
            }
        }
    }
}