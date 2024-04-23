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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Market;
using Guid = System.Guid;

namespace Sw.Hospital.HealthExaminationSystem.Market
{
    /// <summary>
    /// 合同编辑器
    /// </summary>
    public partial class ContractEditor : UserBaseForm
    {
        /// <summary>
        /// 当前项标识
        /// </summary>
        private readonly Guid? _currentId;

        /// <summary>
        /// 初始化 合同编辑器
        /// </summary>
        public ContractEditor(Guid? id = null)
        {
            InitializeComponent();
            _currentId = id;
        }

        /// <summary>
        /// 合同类别
        /// </summary>
        public RepositoryItemLookUpEdit RepositoryItemLookUpEdit合同类别 { get; set; }

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
                if (string.IsNullOrWhiteSpace(textEdit编号.Text))
                {
                    dxErrorProvider.SetError(textEdit编号, "编号不能为空！");
                }
                if (!(lookUpEdit类别.EditValue is Guid))
                {
                    dxErrorProvider.SetError(lookUpEdit类别, "类别不能为空！");
                }
                if (!(searchLookUpEdit单位.EditValue is Guid))
                {
                    dxErrorProvider.SetError(searchLookUpEdit单位, "单位不能为空！");
                }
                if (!(dateEdit提交时间.EditValue is DateTime))
                {
                    dxErrorProvider.SetError(dateEdit提交时间, "提交时间不能为空！");
                }
                if (!(dateEdit有效时间.EditValue is DateTime))
                {
                    dxErrorProvider.SetError(dateEdit有效时间, "有效时间不能为空！");
                }

                if (dxErrorProvider.HasErrors)
                {
                    return;
                }
                var input = new ContractInformationDto();
                input.Name = textEdit名称.Text;
                input.Number = textEdit编号.Text;
                input.ContractCategoryId = (Guid)lookUpEdit类别.EditValue;
                input.CompanyId = (Guid)searchLookUpEdit单位.EditValue;
                input.CompanyRegisterId = searchLookUpEdit预约.EditValue as Guid?;
                input.SubmitTime = dateEdit提交时间.DateTime;
                input.ValidTime = dateEdit有效时间.DateTime;
                input.Signatory = textEdit签字代表.Text;
                input.ImportantMatter = memoEdit重要事项.Text;
                input.Amount = spinEdit总金额.Value;
                if (_currentId.HasValue)
                {
                    input.Id = _currentId.Value;
                }

                var task = DefinedCacheHelper.DefinedApiProxy.ContractAppService.InsertOrUpdateContract(input);
                task.Wait();
                DialogResult = DialogResult.OK;
            });
        }

        /// <summary>
        /// 窗体第一次显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractEditor_Shown(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                if (_currentId.HasValue)
                {
                    var output = DefinedCacheHelper.DefinedApiProxy.ContractAppService
                        .QueryContractInformationById(new EntityDto<Guid>(_currentId.Value)).Result;
                    textEdit名称.Text = output.Name;
                    textEdit编号.Text = output.Number;
                    lookUpEdit类别.EditValue = output.ContractCategoryId;
                    searchLookUpEdit单位.EditValue = output.CompanyId;
                    searchLookUpEdit预约.EditValue = output.CompanyRegisterId;
                    if (output.SubmitTime != null)
                    {
                        dateEdit提交时间.DateTime = output.SubmitTime.Value;
                    }

                    if (output.ValidTime != null)
                    {
                        dateEdit有效时间.DateTime = output.ValidTime.Value;
                    }

                    textEdit签字代表.Text = output.Signatory;
                    memoEdit重要事项.Text = output.ImportantMatter;
                    spinEdit总金额.Value = output.Amount;
                }

                searchLookUpEdit单位.Properties.DataSource = DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryCompanyComboBoxList().Result;

                lookUpEdit类别.Properties.DataSource = DefinedCacheHelper.DefinedApiProxy.ContractAppService
                    .QueryContractCategoryList(new QueryContractCategoryConditionInput()).Result;
            });
        }

        /// <summary>
        /// 单位下拉框下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchLookUpEdit单位_Popup(object sender, EventArgs e)
        {
            searchLookUpEdit单位.Properties.View.BestFitColumns();
        }

        /// <summary>
        /// 单位下拉框编辑值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchLookUpEdit单位_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit单位.EditValue is Guid id)
            {
                AutoLoading(() =>
                {
                    searchLookUpEdit预约.Properties.DataSource = DefinedCacheHelper.DefinedApiProxy.ContractAppService
                        .QueryCompanyRegister(new EntityDto<Guid>(id)).Result;
                });
            }
            else
            {
                searchLookUpEdit预约.EditValue = null;
                searchLookUpEdit预约.Properties.DataSource = null;
            }
        }

        /// <summary>
        /// 类别按钮编辑框点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit类别_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                using (var frm = new ContractCategoryEditor())
                {
                    frm.SaveComplete += Frm_SaveComplete;
                    frm.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// 合同类别保存成功事件
        /// </summary>
        /// <param name="obj"></param>
        private void Frm_SaveComplete(ContractCategoryDto obj)
        {
            var data = lookUpEdit类别.Properties.DataSource as List<ContractCategoryDto> ?? new List<ContractCategoryDto>();
            data.Add(obj);

            lookUpEdit类别.Properties.DataSource = data.OrderBy(r => r.Name).ToList();
            lookUpEdit类别.EditValue = obj.Id;
            if (RepositoryItemLookUpEdit合同类别 != null)
            {
                RepositoryItemLookUpEdit合同类别.DataSource = lookUpEdit类别.Properties.DataSource;
            }
        }
    }
}