using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.WorkType;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;


namespace Sw.Hospital.HealthExaminationSystem.UserSettings.WorkType
{

    public partial class WorkTypeEditor : UserBaseForm
    {
        private readonly IWorkTypeAppService _workTypeAppService;
        private readonly ICommonAppService _commonAppService;
        public WorkTypeDto workTypeInfo;
        public WorkTypeEditor()
        {
            InitializeComponent();
            _commonAppService = new CommonAppService();
            _workTypeAppService = new WorkTypeAppService();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            var name = textEditName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                dxErrorProvider.SetError(textEditName, "名称为必填项！");
                textEditName.Focus();
                return;
            }
            if (radioGroupType.EditValue ==null )
            {
                dxErrorProvider.SetError(radioGroupType, "类别为必填项！");
                textEditName.Focus();
                return;
            }

            var state = comboBoxEditState.SelectedIndex;
            var type = int.Parse(radioGroupType.EditValue.ToString());
            var nameJp = textEditWorkNamejp.Text.Trim();
            var bm = txtBM.Text.ToString();
            var order =int.Parse(txtOrder.EditValue.ToString());
            var content = txtContent.Text.ToString();
            var input = new WorkTypeDto
            {
                Name = name,
                Category = type,
                HelpChar = nameJp,
                IsActive = state == 0 ? true : false,
                Num = bm,
                Order=order,
                Content=content
                

            };
            if (workTypeInfo != null)
            {
                if (workTypeInfo.Id != Guid.Empty)
                {
                    input.Id = workTypeInfo.Id;
                    try
                    {
                        _workTypeAppService.Edit(input);
                        DialogResult = DialogResult.OK;
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBox(ex);
                    }
                }
            }
            else
            {
                try
                {
                    _workTypeAppService.Add(input);
                    DialogResult = DialogResult.OK;
                }
                catch (UserFriendlyException exception)
                {
                    ShowMessageBox(exception);
                }
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkTypeEditor_Load(object sender, EventArgs e)
        {
            if (workTypeInfo != null)
            {
                textEditName.EditValue = workTypeInfo.Name;
                if (workTypeInfo.IsActive)
                {
                    comboBoxEditState.SelectedIndex = 0;
                }
                else
                {
                    comboBoxEditState.SelectedIndex = 1;
                }
                textEditWorkNamejp.EditValue = workTypeInfo.HelpChar;
                txtBM.EditValue = workTypeInfo.Num;
                txtContent.EditValue = workTypeInfo.Content;
                txtOrder.EditValue = workTypeInfo.Order;


                if (workTypeInfo.Category.HasValue)
                    radioGroupType.SelectedIndex = workTypeInfo.Category.Value;
            }
        }
        /// <summary>
        /// 自动获取简码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textEditName.Text))
            {
                var input = new ChineseDto();
                input.Hans = textEditName.Text.Trim();
                textEditWorkNamejp.Text = _commonAppService.GetHansBrief(input).Brief;
            }
        }

        private void simpleButtonOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
