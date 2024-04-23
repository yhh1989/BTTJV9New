using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Market
{
    /// <summary>
    /// 合同类别编辑器
    /// </summary>
    public partial class ContractCategoryEditor : UserBaseForm
    {
        /// <summary>
        /// 当前项标识
        /// </summary>
        private readonly Guid? _currentId;

        /// <summary>
        /// 初始化 合同类别编辑器
        /// </summary>
        /// <param name="id">编辑项标识</param>
        public ContractCategoryEditor(Guid? id = null)
        {
            InitializeComponent();
            _currentId = id;
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton保存_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                dxErrorProvider.ClearErrors();
                if (string.IsNullOrWhiteSpace(textEdit名称.Text))
                {
                    dxErrorProvider.SetError(textEdit名称, "名称不能为空！");
                }

                if (dxErrorProvider.HasErrors)
                {
                    return;
                }
                var input = new ContractCategoryDto();
                if (_currentId.HasValue)
                {
                    input.Id = _currentId.Value;
                }

                input.Name = textEdit名称.Text;
                input.MnemonicCode = textEdit助记码.Text;
                input.IsActive = checkEdit启用.Checked;
                var result = DefinedCacheHelper.DefinedApiProxy.ContractAppService.UpdateOrInsertContractCategory(input)
                    .Result;
                SaveComplete?.Invoke(result);
                DialogResult = DialogResult.OK;
            });
        }

        /// <summary>
        /// 窗体第一次显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractCategoryEditor_Shown(object sender, EventArgs e)
        {
            if (_currentId.HasValue)
            {
                AutoLoading(() =>
                {
                    var resultTask =
                        DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryContractCategoryById(
                            new EntityDto<Guid>(_currentId.Value));
                    var result = resultTask.Result;
                    textEdit名称.Text = result.Name;
                    textEdit助记码.Text = result.MnemonicCode;
                    checkEdit启用.Checked = result.IsActive;
                });
            }
        }

        /// <summary>
        /// 名称文本框编辑值己解析事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEdit名称_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            CommonHelper.SetHelpChar(textEdit助记码, textEdit名称, true);
        }

        /// <summary>
        /// 数据保存成功
        /// </summary>
        public event Action<ContractCategoryDto> SaveComplete;
    }
}