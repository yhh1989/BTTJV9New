using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BarSetting;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BarSetting
{
    public partial class BarCodeEditor : UserBaseForm
    {
        private readonly IBarSettingAppService service = new BarSettingAppService();
        public FullBarSettingDto _Model { get; private set; }
        private List<SimpleItemGroupDto> groupls = new List<SimpleItemGroupDto>();

        /// <summary>
        /// 新增
        /// </summary>
        public BarCodeEditor()
        {
            InitializeComponent();
            luePrintAdress.SetClearButton();
            lueSampletype.SetClearButton();
            lueTestType.SetClearButton();
            lueBarNUM.SetClearButton();

            if (_Model == null) _Model = new FullBarSettingDto();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public BarCodeEditor(FullBarSettingDto model) : this()
        {
            _Model = model;
            LoadData();
        }

        #region 事件
        private void BarSettingEdit_Load(object sender, EventArgs e)
        {
            LoadControlData();
        }
        private void sbOk_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void sbAddSingle_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void sbDelSingle_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void gvItemGroupWait_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Add();
            }
        }

        private void gvItemGroupSelected_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Del();
            }
        }

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            AutoLoading(() => {
                lueSampletype.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.SpecimenType);
                lueTestType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.TestType);
                lueBarNUM.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.PrintMethod);
                luePrintAdress.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.PrintPosition);
            });
        }

        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            AutoLoading(() => {
                gcWait.DataSource = DefinedCacheHelper.GetItemGroups();
            });
        }
        #endregion

        /// <summary>
        /// 加载控件数据
        /// </summary>
        private void LoadControlData()
        {
            lueSampletype.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.SpecimenType);
            lueTestType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.TestType);
            lueBarNUM.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.PrintMethod);
            luePrintAdress.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.PrintPosition);

            gcWait.DataSource = DefinedCacheHelper.GetItemGroups();
            groupls = DefinedCacheHelper.GetItemGroups().ToList();
        }

        public void Add()
        {
            var dtos = gcWait.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (dtos.Count == 0) return;
            //gcSelected.AddDtoListItem(baritems, true);
            var baritems = dtos.Select(m => new FullBarItemDto
            {
                ItemGroup = m,
                ItemGroupId = m.Id,
                ItemGroupAlias = m.ItemGroupName
            }).ToList();

            var list = gcSelected.GetDtoListDataSource<FullBarItemDto>();
            if (list == null)
            {
                list = new List<FullBarItemDto>();
                gcSelected.DataSource = list;
            }
            baritems.RemoveAll(m => list.Any(s => s.ItemGroup?.Id == m.ItemGroup.Id));
            list.AddRange(baritems);
            gcSelected.RefreshDataSource();
        }
        
        public void Del()
        {
            var dtos = gcSelected.GetSelectedRowDtos<FullBarItemDto>();
            if (dtos.Count == 0) return;
            //gcSelected.RemoveDtoListItem(dtos);
            gcSelected.GetDtoListDataSource<FullBarItemDto>()?.RemoveAll(m => dtos.Any(i => i.ItemGroupId == m.ItemGroupId));
            gcSelected.RefreshDataSource();
        }

        private bool Ok()
        {
            // 条码名称
            _Model.BarName = teBarName.Text.Trim();
            if (string.IsNullOrEmpty(_Model.BarName))
            {
                dxErrorProvider.SetError(teBarName, string.Format(Variables.MandatoryTips, "条码名称"));
                teBarName.Focus();
                return false;
            }
            // 组合列表
            _Model.Baritems = gcSelected.GetDtoListDataSource<FullBarItemDto>();
            if (_Model.Baritems == null || _Model.Baritems.Count == 0)
            {
                ShowMessageBoxWarning("未选择项目组合，条码没有意义！");
                return false;
            }
            
            // 打印内容
            _Model.Content = teContent.Text.Trim();
            // 样本类型
            _Model.Sampletype = lueSampletype.EditValue == null ? "" : lueSampletype.EditValue.ToString();
            // 试管颜色
            _Model.TubeColor = teTubeColor.Text.Trim();
            // 检验方式 1外送2内检
            _Model.testType = lueTestType.EditValue == null ? 2 : (int?)lueTestType.EditValue;
            // 打印方式 1档案号2档案号累加3自定义累加
            _Model.BarNUM = lueBarNUM.EditValue == null ? 3 : (int?)lueBarNUM.EditValue;
            // 打印个数
            _Model.BarPage = int.TryParse(teBarPage.Text.Trim(), out int barPage) ? (int?)barPage : null;
            // 打印位置 1前台打印2抽血站打印
            _Model.PrintAdress = luePrintAdress.EditValue == null ? 1 : (int?)luePrintAdress.EditValue;
            // 备注
            _Model.Remarks = meRemarks.Text;
            // 是否启用 1启用2停止
            _Model.IsRepeatItemBarcode = ceIsRepeatItemBarcode.Checked ? 1 : 2;
            //条码序号
            _Model.OrderNum = Convert.ToInt32(txtOrderNum.EditValue);
            _Model.StrBar = txtstr.Text;
            bool res = false;
            AutoLoading(() => {
                BarSettingInput input = new BarSettingInput
                {
                    BarSetting = ModelHelper.CustomMapTo2<FullBarSettingDto, CreateOrUpdateBarSettingDto>(_Model),
                    Baritems = new List<BarItemDto>()
                };
                input.Baritems.AddRange(_Model.Baritems);
                FullBarSettingDto dto = null;
                if (_Model.Id == Guid.Empty)
                {
                    dto = service.Add(input);
                }
                else
                {
                    dto = service.Edit(input);
                }
                dto.Baritems = _Model.Baritems;
                _Model = dto;
                res = true;
            }, Variables.LoadingSaveing);
            return res;
        }
        private void LoadData()
        {
            if (_Model == null)
            {
                return;
            }
            txtstr.Text = _Model.StrBar;
            //条码序号
            txtOrderNum.EditValue = _Model.OrderNum;
            // 条码名称
            teBarName.Text = _Model.BarName;
            // 打印内容
            teContent.Text = _Model.Content;
            // 样本类型
            lueSampletype.EditValue = _Model.Sampletype;
            // 试管颜色
            teTubeColor.Text = _Model.TubeColor;
            // 检验方式 1外送2内检
            lueTestType.EditValue = _Model.testType;
            // 打印方式 1档案号2档案号累加3自定义累加
            lueBarNUM.EditValue = _Model.BarNUM;
            // 打印个数
            teBarPage.Text = _Model.BarPage?.ToString();
            // 打印位置 1前台打印2抽血站打印
            luePrintAdress.EditValue = _Model.PrintAdress;
            // 备注
            meRemarks.Text = _Model.Remarks;
            // 是否启用 1启用2停止
            ceIsRepeatItemBarcode.Checked = _Model.IsRepeatItemBarcode == 1;
            // 已选列表
            gcSelected.DataSource = _Model.Baritems;
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }
        public void BindgrdOptionalItemGroup()
        {
            if (searchControl1.Text != "")
            {
                var strup = searchControl1.Text.ToUpper();
                var output = new List<SimpleItemGroupDto>();
                output = groupls.Where(o => o.ItemGroupName.Contains(searchControl1.Text) || o.HelpChar.Contains(strup)).ToList();
                gcWait.DataSource = output;
            }
            else
            {
                gcWait.DataSource = groupls;
            }

        }

        private void lueSampletype_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
