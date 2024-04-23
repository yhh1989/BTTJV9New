using Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;

namespace Sw.Hospital.HealthExaminationSystem.Crisis
{
    public partial class EditCallBackList : UserBaseForm
    {
        public Guid CrisisSetId;
        private readonly ICrisisAppService _crisisAppService;
        private Guid dataId;
        public EditCallBackList()
        {
            InitializeComponent();
            _crisisAppService = new CrisisAppService();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var data = gvList.GetFocusedRow() as CallBackDto;
            if (data != null)
            {
                txtCallBackState.EditValue = data.CallBackState;
                txtCallBackType.EditValue = data.CallBackType;
                dateCallBackDate.EditValue = data.CallBacKDate;
                memoEdit.EditValue = data.CallBacKContent;
                dataId = data.Id;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCallBackState.EditValue = (int)CallBackState.Complete;
            txtCallBackType.EditValue = (int)CallBackType.Wechat;
            dateCallBackDate.EditValue = DateTime.Now;
            memoEdit.EditValue = null;
            dataId = Guid.Empty;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var data = new CallBackDto()
            {
                Id = dataId,
                TjlCrisisSetId=CrisisSetId
            };
            data.CallBackState = (int)txtCallBackState.EditValue;
            data.CallBackType = (int)txtCallBackType.EditValue;
            data.CallBacKDate = dateCallBackDate.DateTime;
            data.CallBacKContent = memoEdit.EditValue?.ToString();
            AutoLoading(() =>
            {
                _crisisAppService.SaveCallBack(data);
                ShowMessageBoxInformation("保存成功");
                LoadData(); 
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditCallBackList_Load(object sender, EventArgs e)
        {
            txtCallBackState.Properties.DataSource = CallBackEnumHelper.GetCallBackState();
            txtCallBackType.Properties.DataSource = CallBackEnumHelper.GetCallBackType();
            LoadData();
        }
        private void LoadData()
        {
            txtCallBackState.EditValue = (int)CallBackState.Complete;
            txtCallBackType.EditValue = (int)CallBackType.Wechat;
            dateCallBackDate.EditValue = DateTime.Now;
            dataId = Guid.Empty;
            if (CrisisSetId != Guid.Empty)
            {
                gcList.DataSource = _crisisAppService.QueryCallBackList(new Abp.Application.Services.Dto.EntityDto<Guid>() { Id = CrisisSetId });
            }
        }

        private void gvList_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.Name == gConState.Name)
                e.DisplayText = EnumHelper.GetEnumDesc((CallBackState)e.Value);
            if (e.Column.Name == gConType.Name)
                e.DisplayText = EnumHelper.GetEnumDesc((CallBackType)e.Value);
        }
    }
}